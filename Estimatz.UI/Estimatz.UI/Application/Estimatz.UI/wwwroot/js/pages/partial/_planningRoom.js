let rowsTable = stories;
let selectedRow = {};
let usersConnected = [];

$(document).ready(function () {    
    selectedRow = firstStoryJson;

    $("#confirmAddStoryButton").click(function () {
        let story = $("#newStoryInput").val();
        let roomId = $("#Id").val();

        if (story != "") {            
            $.ajax({
                type: "POST",
                url: addStoryUrl,
                data: { storyName: story, roomId: roomId, stories: rowsTable },
                success: function (data) {
                    rowsTable = data.stories;
                    selectedRow = rowsTable[rowsTable.length - 1];
                    
                    loadTableRow(rowsTable);
                    $("#newStoryInput").val("");
                }
            });
        }      
    })

    addEventsOnRow();
});

async function loadTableRow(rowsTable) { //TODO: Organizar essa parada para que seja um componente e o botão de excluir só seja habilitado para admin
    
    let rowsTablesWithValue = "";
    let firstStory = {};    

    //Estilizar a linha
    for (let i = 0; i < rowsTable.length; i++) {
        if (i == 0) {
            firstStory = rowsTable[i];            
        } 

        rowsTable[i].storySelected = selectedRow.id == rowsTable[i].id;
        rowsTable[i].userIsAdmin = isAdmin;

        rowsTablesWithValue += await getCardStoryRow(rowsTable[i]);
    }

    setTimeout(() => { //TODO: Preciso criar as promisses pra esse tipo de situação        
        $("#storiesBodyTable").html(rowsTablesWithValue);

        if (rowsTable.length == 0) {
            loadStoryPartialView(null);
        } else if (rowsTable.length == 1) {
            loadStoryPartialView(firstStory);
        } else {
            loadStoryPartialView(selectedRow);
        }

        addEventsOnRow();
    }, 300);       
}

async function getCardStoryRow(rowTable) {
    const response = await $.ajax({
        type: "POST",
        url: getCardStoryRowUrl,
        data: { story: rowTable }
    });

    return response;
}

function addEventsOnRow() {    
    $("i[name='editRow']").off().click(function () {
        const rowId = $(this).attr('aria-data');
        const roomId = $("#Id").val();

        $.ajax({
            type: "POST",
            url: removeStoryUrl,
            data: { storyId: rowId, roomId: roomId, stories: rowsTable },
            success: function (data) {   
                rowsTable = data.stories;

                if (selectedRow.id == rowId) {
                    selectedRow = rowsTable[0];
                }

                loadTableRow(rowsTable);
            }
        });
    })

    $("div[name='rowStory']").off().click(function () {
        const rowId = $(this).attr('aria-data');
        const found = rowsTable.find(element => element.id == rowId);
        selectedRow = found;
        loadTableRow(rowsTable);
    })
}

function removeEventsOnRow() {
    $("div[name='rowStory']").off();
    $("i[name='editRow']").off();
}

function loadStoryPartialView(storyModel) {
    const roomId = $("#Id").val();

    $.ajax({
        type: "POST",
        url: getStoryUrl,
        data: { roomId: roomId, storyId: storyModel.id },
        success: function (data) {
            $("#planningRoomStoryContent").html(data); //TODO: Criar um loading pra não ficar muito feio a troca de US
        }
    });
}

function updateStoryStatus(storyId, newStatus) {
    for (let i = 0; i < rowsTable.length; i++)
    {
        if (rowsTable[i].id == storyId) {
            rowsTable[i].status = newStatus;

            if (rowsTable[i].id == selectedRow.id) {
                selectedRow.status = newStatus;
            }
        }
    }
}

function updateResultVote(storyId, result) {
    for (let i = 0; i < rowsTable.length; i++) {
        if (rowsTable[i].id == storyId) {
            rowsTable[i].votingResult = result;

            if (rowsTable[i].id == selectedRow.id) {
                selectedRow.votingResult = result;
            }
        }
    }
}

async function selectRowOnStartVote(rowId) {
    const found = rowsTable.find(element => element.id == rowId);    
    selectedRow = found;
    await loadTableRow(rowsTable);
}