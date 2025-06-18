// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
});

function switchCulture(culture) {    
    $.ajax({
        type: "POST",
        url: switchLanguageUrl,
        data: { culture: culture },
        dataType: "text",
        success: function () {
            location.reload()
        }
    });
}

function stepWizard(outComponent, inComponent) {    
    window.setTimeout(function () {
        outComponent.addClass("d-none");
        inComponent.removeClass("d-none");
    }, 300);

    outComponent.addClass("fade-out");
    inComponent.removeClass("fade-out");
}

function checkUpperCaseCharacter(text, checkbox) {
    if (/[A-Z]/.test(text)) {
        checkbox.prop('checked', true);
    } else {
        checkbox.prop('checked', false);
    }
}

function checkLowerCaseCharacter(text, checkbox) {
    if (/[a-z]/.test(text)) {
        checkbox.prop('checked', true);
    } else {
        checkbox.prop('checked', false);
    }
}

function checkSpecialCharacter(text, checkbox) {
    if (/[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]/.test(text)) {
        checkbox.prop('checked', true);
    } else {
        checkbox.prop('checked', false);
    }
}

function checkNumberCharacter(text, checkbox) {
    if (/[0-9]/.test(text)) {
        checkbox.prop('checked', true);
    } else {
        checkbox.prop('checked', false);
    }
}

function checkMinimumCharacter(text, checkbox) {
    if (text.length >= 8) {
        checkbox.prop('checked', true);
    } else {
        checkbox.prop('checked', false);
    }
}