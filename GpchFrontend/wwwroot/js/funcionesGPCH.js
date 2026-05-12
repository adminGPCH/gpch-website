window.setValue = function (element, value) {
    if (element instanceof HTMLElement) {
        element.value = value;
    }
};

window.gpchMouseTracker = {
    init: function (dotnetHelper) {
        document.addEventListener("mousemove", function (e) {
            dotnetHelper.invokeMethodAsync("UpdateMousePosition", e.clientX, e.clientY);
        });
    }
};

window.playSound = function (path) {
    var audio = new Audio(path);
    audio.load(); // fuerza precarga
    audio.play();
};

