$(document).ready(function () {
    $("#buttonSendEmailRecoverPassword").click(function () {
        if (document.getElementById('formInsertEmail').checkValidity()) {
            const emailRecover = $("#inputEmail").val();

            $.ajax({
                type: "POST",
                url: sendEmailPasswordRecoveryUrl,
                data: { email: emailRecover },
                success: function (data) {
                    if (data.success) {
                        stepWizard($("#formInsertEmailContent"), $("#emailSendContent"));
                    } 
                    else {
                        for (var i = 0; i < data.messages.length; i++) {
                            $("#errorMessagesContainer").append(`<p style="width: 300px" class="m-auto m-0 text-warning">${data.messages[i]}</p>`);
                        }

                        if (data.messages.length > 0) {
                            $("#errorMessagesContainer").removeClass("d-none");
                        }
                        stepWizard($("#formInsertEmailContent"), $("#emailNotSendContent"));
                    }
                }
            });
        } else {
            $("#formInsertEmail").addClass("was-validated");
        }                
    });
});