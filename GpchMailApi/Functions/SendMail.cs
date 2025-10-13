using Azure.Identity;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Graph.Users.Item.SendMail;
using System.Net;
using System.Text.Json;

namespace GpchMailApi.Functions
{
    public class SendMail(ILogger<SendMail> logger)
    {
        private readonly ILogger<SendMail> _logger = logger;

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        [Function("SendMail")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "mail/send")] HttpRequestData req)
        {
            MailRequest? request;

            try
            {
                request = await JsonSerializer.DeserializeAsync<MailRequest>(req.Body, _jsonOptions);
            }
            catch (JsonException ex)
            {
                _logger.LogError("Error de deserialización JSON: {Message}", ex.Message);
                var error = req.CreateResponse(HttpStatusCode.BadRequest);
                await error.WriteStringAsync("Error al interpretar el cuerpo JSON.");
                return error;
            }

            if (request is null || string.IsNullOrWhiteSpace(request.Email))
            {
                var error = req.CreateResponse(HttpStatusCode.BadRequest);
                await error.WriteStringAsync("Solicitud inválida: cuerpo vacío o malformado.");
                return error;
            }

            _logger.LogInformation("Procesando mensaje desde {Email}", request.Email);

            try
            {
                var tenantId = Environment.GetEnvironmentVariable("GRAPH_TENANT_ID");
                var clientId = Environment.GetEnvironmentVariable("GRAPH_CLIENT_ID");
                var clientSecret = Environment.GetEnvironmentVariable("GRAPH_CLIENT_SECRET");
                var senderEmail = Environment.GetEnvironmentVariable("GRAPH_SENDER_EMAIL");

                var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
                var graphClient = new GraphServiceClient(credential);

                var message = new Message
                {
                    Subject = $"Mensaje desde GPCH.cl de {request.Name}",
                    Body = new ItemBody
                    {
                        ContentType = BodyType.Text,
                        Content = $"Correo desde GPCH.cl\n\nNombre: {request.Name}\nEmail: {request.Email}\n\nMensaje:\n{request.Message}"
                    },
                    ToRecipients =
                    [
                        new Recipient
                        {
                            EmailAddress = new EmailAddress
                            {
                                Address = senderEmail
                            }
                        }
                    ]
                };

                await graphClient.Users[senderEmail]
                    .SendMail
                    .PostAsync(new SendMailPostRequestBody
                    {
                        Message = message,
                        SaveToSentItems = true
                    });

                var response = req.CreateResponse(HttpStatusCode.OK);
                await response.WriteStringAsync("Correo enviado correctamente desde GPCH.cl");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al enviar correo: {Message}", ex.Message);
                var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
                await errorResponse.WriteStringAsync("Error al enviar el correo.");
                return errorResponse;
            }
        }
    }

    public class MailRequest
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Message { get; set; }
    }
}
