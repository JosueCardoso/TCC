// Example starter JavaScript for disabling form submissions if there are invalid fields
(function () {
    'use strict'

    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    var forms = document.querySelectorAll('.needs-validation')

    // Loop over them and prevent submission
    Array.prototype.slice.call(forms)
        .forEach(function (form) {
            form.addEventListener('submit', function (event) {
                if (!form.checkValidity()) {
                    event.preventDefault()
                    event.stopPropagation()
                }

                form.classList.add('was-validated')
            }, false)
        })
})()

$(document).ready(function () {
    $("#buttonQuickPlay").click(function () {
        stepWizard($("#formLoginContent"), $("#formQuickPlayContent"));
    });

    $("#linkGoBackToLogin").click(function () {
        stepWizard($("#formQuickPlayContent"), $("#formLoginContent"));
    })

    $("#loginForm").submit(function (event) {
        event.preventDefault();

        const dataLogin = $(this).serializeArray(); 
        const data = {};

        $.map(dataLogin, function (n, i) {
            data[n["name"]] = n["value"];
        });

        $.ajax({
            type: "POST",
            url: loginUrl,
            data: data,
            success: function (data) {
                if (data.success == false) {
                    alert("Deu ruim!");
                } else {
                    window.location.href = "/Dashboard/Dashboard"                   
                }
            }
        });
    });

    $("#enterQuickPlayForm").submit(function (event) {
        event.preventDefault();

        const usernameQuickPlay = $("#inputNameQuickPlay").val();
        const roomQuickPlay = $("#idQuickPlay").val();

        if (usernameQuickPlay == "") {
            $("#inputNameQuickPlay").addClass("is-invalid");
        } else {
            $("#inputNameQuickPlay").removeClass("is-invalid");
        }

        if (roomQuickPlay == "") {
            $("#idQuickPlay").addClass("is-invalid");
        } else {
            $("#idQuickPlay").removeClass("is-invalid");
        }

        if (usernameQuickPlay != "" && roomQuickPlay != "") {
            $.ajax({
                type: "POST",
                url: createQuickUserUrl,
                data: { name: usernameQuickPlay },
                success: function (data) {
                    if (data.success) {
                        window.location.href = openRoomUrl + "/" + roomQuickPlay;
                    } else {
                        alert("Deu erro ao achar a sala!")
                    }
                }
            });
        } 
    });
});