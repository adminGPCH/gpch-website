window.setValue = function (element, value) {
    if (element instanceof HTMLElement) {
        element.value = value;
    }
};
