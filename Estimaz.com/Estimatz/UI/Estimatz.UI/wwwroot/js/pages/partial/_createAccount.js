$(document).ready(function () {
    $("#buttonNextStepCreateAccount").click(function () {
        if (document.getElementById('formCreateNewAccount').checkValidity()) {
            window.setTimeout(function () {
                stepWizard($("#formCreateNewAccountContent"), $("#formCreateNewPasswordContent"));
                $("#formCreateNewAccount").removeClass("was-validated");
            }, 300);
        } else {
            $("#formCreateNewAccount").addClass("was-validated");
        }   
    });

    $("#linkPreviousStepCreateAccount").click(function () {
        stepWizard($("#formCreateNewPasswordContent"), $("#formCreateNewAccountContent"));
    });

    $("#buttonCreateAccount").click(function () {
        if (checkValidityCheckbox() == false) {
            $("#inputCreatePassword").addClass("is-invalid");
            $("#inputCreatePassword")[0].setCustomValidity("invalid");
        }

        if (document.getElementById('formCreateNewPassword').checkValidity()) {
            const dataNewAccount = $("#formCreateNewAccount").serializeArray();
            const dataNewPassword = $("#formCreateNewPassword").serializeArray();  
            const data = {};

            $.map(dataNewAccount, function (n, i) {
                data[n["name"]] = n["value"];
            });

            $.map(dataNewPassword, function (n, i) {
                data[n["name"]] = n["value"];
            });

            window.setTimeout(function () {
                $.ajax({
                    type: "POST",
                    url: createAccountUrl,
                    data: data,
                    success: function (data) {
                        if (data.success) {
                            stepWizard($("#formCreateNewPasswordContent"), $("#accountCreatedSuccessfully"));
                        } else {                                 
                            for (var i = 0; i < data.messages.length; i++) {
                                $("#errorMessagesContainer").append(`<p style="width: 300px" class="m-auto m-0 text-warning">${data.messages[i]}</p>`);                                
                            }

                            if (data.messages.length > 0) {
                                $("#errorMessagesContainer").removeClass("d-none");
                            }

                            stepWizard($("#formCreateNewPasswordContent"), $("#accountNotCreatedSuccessfully"));
                        }
                    }
                });
            }, 300);  
        }   
    });

    $("#inputCreatePassword").keyup(function () {
        let inputValue = $("#inputCreatePassword").val();
        checkUpperCaseCharacter(inputValue, $("#checkUppercase"));
        checkLowerCaseCharacter(inputValue, $("#checkLowerCase"));
        checkSpecialCharacter(inputValue, $("#checkSpecialCharacter"));
        checkNumberCharacter(inputValue, $("#checkNumber"));
        checkMinimumCharacter(inputValue, $("#checkMinimumCharacter"));

        if (checkValidityCheckbox()) {
            $("#inputCreatePassword").addClass("is-valid");
            $("#inputCreatePassword")[0].setCustomValidity("");
        } else {
            $("#inputCreatePassword").addClass("is-invalid");
            $("#inputCreatePassword")[0].setCustomValidity("invalid");
        }
    });

    $("#inputCreatePassword").keydown(function () {
        if (checkValidityCheckbox()) {
            $("#inputCreatePassword").addClass("is-valid");
            $("#inputCreatePassword")[0].setCustomValidity("");
        } else {
            $("#inputCreatePassword").addClass("is-invalid");
            $("#inputCreatePassword")[0].setCustomValidity("invalid");
        }
    });

    $("#inputConfirmPassword").keyup(function () {
        if ($("#inputCreatePassword").val() === $("#inputConfirmPassword").val()) {
            $("#inputConfirmPassword").addClass("is-valid");
            $("#inputConfirmPassword")[0].setCustomValidity("");
        } else {
            $("#inputConfirmPassword").addClass("is-invalid");
            $("#inputConfirmPassword")[0].setCustomValidity("invalid");
        }
    });

    $("#inputConfirmPassword").keydown(function () {
        if ($("#inputCreatePassword").val() === $("#inputConfirmPassword").val()) {
            $("#inputConfirmPassword").addClass("is-valid");
            $("#inputConfirmPassword")[0].setCustomValidity("");
        } else {
            $("#inputConfirmPassword").addClass("is-invalid");
            $("#inputConfirmPassword")[0].setCustomValidity("invalid");
        }
    });
});

function checkValidityCheckbox() {
    return $("#checkUppercase")[0].checkValidity() &&
        $("#checkLowerCase")[0].checkValidity() &&
        $("#checkSpecialCharacter")[0].checkValidity() &&
        $("#checkNumber")[0].checkValidity() &&
        $("#checkMinimumCharacter")[0].checkValidity();
}