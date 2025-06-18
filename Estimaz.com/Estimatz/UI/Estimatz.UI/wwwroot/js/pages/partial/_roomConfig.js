let rowsTable = [];
let model = {};

$(document).ready(function () {
    $("#divideTeamCheckbox").change(function () {
        if ($(this).is(':checked')) {
            $("#intersperseTeamsCheckbox").prop('disabled', false);
            $("#addTeamLink").prop('disabled', false);
        } else {
            $("#intersperseTeamsCheckbox").prop('checked', false);
            $("#intersperseTeamsCheckbox").prop('disabled', true);
            $("#addTeamLink").prop('disabled', true);
            $("#teamsBodyTable").html("");
            rowsTable = [];
        }
    });

    $("#addTeamButton").click(function () {
        var newTeam = $("#newTeamInput").val();

        if (newTeam != "") {
            $.ajax({
                type: "POST",
                url: addTeamOnTableUrl,
                data: { teamName: newTeam, teams: rowsTable },
                success: function (data) {
                    rowsTable = data.teams;
                    loadTableRow(rowsTable);
                    $("#newTeamInput").val("");
                }
            });   
        }  
    })  

    $("#goForwardRoomConfigButton").click(function () {
        validateAndCreateParcialModel();
    });

    $("#freeVoteCheckbox").change(function () {
        if ($("#taskVotingCheckbox").is(':checked'))
        {
            $("#taskVotingCheckbox").prop('checked', false);
        }
    });

    $("#freeVoteCheckbox").click(function () {
        $(this).prop('checked', true);
    });

    $("#taskVotingCheckbox").click(function () {
        $(this).prop('checked', true);
    });

    $("#taskVotingCheckbox").change(function () {
        if ($("#freeVoteCheckbox").is(':checked')) {
            $("#freeVoteCheckbox").prop('checked', false);
        }
    });

    $("#startRoomButton").click(function () {
        let freeVoteCheckbox = $("#freeVoteCheckbox").is(':checked');
        let taskVotingCheckbox = $("#taskVotingCheckbox").is(':checked');

        model.FreeVoting = freeVoteCheckbox;
        model.TaskVoting = taskVotingCheckbox;

        $.ajax({
            type: "POST",
            url: saveRoomConfigUrl,
            data: model,
            success: function (data) {
                if (data.success) {
                    window.location.href = openRoomUrl + "/" + data.roomId;
                } else {
                    
                }
            }
        });   
    })
});

function loadTableRow(rowsTable) {    
    var rowsTablesWithValue = "";

    for (let i = 0; i < rowsTable.length; i++) {
        rowsTablesWithValue += `<tr>
                                    <td id="row_${rowsTable[i].id}"> ${rowsTable[i].name} <i class="bi bi-trash float-end me-2" name="editRow" aria-data="${rowsTable[i].id}" style="cursor: pointer; color:red;"></i></td>
						        </tr>`;
    }

    $("#teamsBodyTable").html(rowsTablesWithValue);

    $("i[name='editRow']").click(function () {
        var rowId = $(this).attr('aria-data');
        removeItemFromArray(rowId, rowsTable);
        loadTableRow(rowsTable)
    })
}

function removeItemFromArray(id, rowsTable) {
    let newRowsTable = [];

    for (let i = 0; i < rowsTable.length; i++) {
        if (rowsTable[i].id == id) {
            newRowsTable = rowsTable.splice(i, 1);
        }
    }

    rowsTable = newRowsTable;
}

function validateAndCreateParcialModel() {
    let selectedDeck = $("#SelectedDeck").val();
    let selectedEstimateType = $("#SelectedEstimateType").val()
    let divideTeamCheckbox = $("#divideTeamCheckbox").is(':checked');
    let intersperseTeamsCheckbox = $("#intersperseTeamsCheckbox").is(':checked');
    let roomNameInput = $("#RoomName").val();

    if (roomNameInput == "") {
        $("#RoomName").addClass("is-invalid");
    } else {
        $("#RoomName").removeClass("is-invalid");
    }

    if (isNaN(selectedDeck)) {
        $("#SelectedDeck").addClass("is-invalid");
    } else {
        $("#SelectedDeck").removeClass("is-invalid");
    }

    if (isNaN(selectedEstimateType)) {
        $("#SelectedEstimateType").addClass("is-invalid");
    } else {
        $("#SelectedEstimateType").removeClass("is-invalid");
    }

    if (divideTeamCheckbox && rowsTable.length == 0) {
        $("#addTeamLink").css('color', 'red');
    } else {
        $("#addTeamLink").css('color', '#00C9EE');
    }

    if (roomNameInput != "" && !isNaN(selectedDeck) && !isNaN(selectedEstimateType) && (!divideTeamCheckbox || (divideTeamCheckbox && rowsTable.length > 0))) {
        model = {
            "SelectedDeck": selectedDeck,
            "SelectedEstimateType": selectedEstimateType,
            "DivideTeams": divideTeamCheckbox,
            "IntersperseTeams": intersperseTeamsCheckbox,
            "Teams": rowsTable,
            "RoomName": roomNameInput
        }

        $("#firstConfigRoomContainer").addClass("d-none");
        $("#secondConfigRoomContainer").removeClass("d-none");        
    }
}