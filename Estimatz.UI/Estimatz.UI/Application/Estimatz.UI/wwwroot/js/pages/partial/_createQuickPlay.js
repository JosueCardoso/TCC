$(document).ready(function () {
    $("#goForwardQuickPlayButton").click(function () {
        let username = $("#usernameQuickPlayInput").val();

        if (username != "") {
            $.ajax({
                type: "POST",
                url: createQuickUserUrl,
                data: { name: username },
                success: function (data) {
                    if (data.success) {
                        stepWizard($("#usernameQuickPlayContainer"), $("#roomConfigQuickPlayContainer"));
                    } else {
                        alert("Deu erro ao criar o nome")
                    }
                }
            }); 
        } else {
            $("#usernameQuickPlayInput").addClass("is-invalid");
        }
    });
})