$(document).ready(function () {
    $("button[name='openRoomButton']").click(function () {
        let id = $(this).attr('aria-data');
        window.location.href = openRoomUrl + "/" + id;
    })

    $("button[name='deleteRoomButton']").click(function () {
        let id = $(this).attr('aria-data');
        $("#itemIdForDeleteInput").val(id);         
    })

    $("#confirmDeleteRoomButton").click(function () {
        let id = $("#itemIdForDeleteInput").val();
        $.ajax({
            type: "POST",
            url: deleteRoomUrl,
            data: { id: id },
            success: function (data) {
                if (data.success) {
                    window.location.href = "/Room/Rooms";
                } else {
                    alert("Não foi possível excluir!");
                }
            }
        });
    });
});