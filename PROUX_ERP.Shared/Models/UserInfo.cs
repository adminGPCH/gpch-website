//D:\GPCH\APP\PROUX_ERP.Shared\Models\UserInfo.cs
namespace PROUX_ERP.Shared.Models;

public class UserInfo
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public List<ClaimItem> Claims { get; set; } = [];
}

public class ClaimItem
{
    public string Type { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}
