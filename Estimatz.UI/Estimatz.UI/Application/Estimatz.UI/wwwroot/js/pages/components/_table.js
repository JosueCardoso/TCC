$(document).ready(function () {
    usersConnected = [];
    window.planning = createPlanningController();    
    window.planning.connectUserToChat();

    //if (selectedRow.status == 2) {
    //    $("#stopVotingButton").prop('disabled', false);
    //}    

    $("#stopVotingButton").off().click(function () {
        window.planning.stopVotingAdm();
    })
    
    $("#startVotingButton").off().click(function () {
        window.planning.startVotingAdm();
    })

    $("#turnCardButton").off().click(function () {
        window.planning.turnCards();
    })

    $("#refreshVotingButton").off().click(function () {
        window.planning.refreshVotes();
    })

    let selectedCard = "";

    $("div[name='cardForSelect']").off().click(function () {
        let votingValue = $(this).attr('aria-data');

        if (selectedCard != "") {
            deselectCard($(`#${selectedCard}`));
            selectedCard = $(this).attr('id');
            selectCard($(this));
        } else {
            selectedCard = $(this).attr('id');
            selectCard($(this));
        }

        window.planning.voting(votingValue)
    });
})

function createPlanningController() {    
    const user = {
        userId: userId,
        roomId: $("#Id").val(),
        name: userName,
        role: playerRole,
        isAdmin: isAdmin,
        votes: []
    };

    return {
        state: user,
        connection: null,
        connectUserToChat: function () {      
            startConnection(this);
        },
        loadPlayers: function () {
            this.connection.on('playerOn', (users) => loadPlayers(users));
        },
        removePlayers: function () {
            this.connection.on('playerOff', (users) => removePlayers(users));
        },
        startVotingAdm: function () {
            $.ajax({
                type: "POST",
                url: updateStatusStoryUrl,
                data: { newStatus: 1, storyId: selectedRow.id, roomId: this.state.roomId },
                success: function (data) {
                    $('#storyStatus').html("Status: " + data.statusDescription);
                    updateStoryStatus(selectedRow.id, 1);
                }
            });

            this.connection.invoke("StartVotingAdm", user.roomId, selectedRow.id).catch(err => console.log(x = err));
        },
        startVotingReceive: function () {
            this.connection.on("startVoting", (storyId) => startVoting(storyId));
        },
        stopVotingAdm: function () {
            $.ajax({
                type: "POST",
                url: updateStatusStoryUrl,
                data: { newStatus: 2, storyId: selectedRow.id, roomId: planning.state.roomId },
                success: function (data) {
                    $('#storyStatus').html("Status: " + data.statusDescription);
                    updateStoryStatus(selectedRow.id, 2);
                }
            });

            this.connection.invoke("StopVotingAdm", user.roomId, selectedRow.id).catch(err => console.log(x = err));
        },
        stopVotingReceive: function () {
            this.connection.on("stopVoting", (storyId) => stopVoting(storyId));
        },
        voting: function (voting) {
            this.connection.invoke("Voting", user.roomId, user.userId, storyId, voting).catch(err => console.log(x = err));
        },
        playerVoted: function () {
            this.connection.on("playerVote", (userId) => playerVoted(userId));
        },
        turnCards: function () {
            this.connection.invoke("TurnCards", user.roomId, storyId).catch(err => console.log(x = err));
        },
        turnedCards: function () {
            this.connection.on("turnedCards", (users, resultVoting) => setTurnedCards(users, resultVoting));
        },
        refreshVotes: function () {
            this.connection.invoke("RefreshVotes", user.roomId, storyId).catch(err => console.log(x = err));
        },
        restartedVotes: function () {
            this.connection.on("restartedVotes", (users) => restartedVotes(users));
        },
        addStory: function () {

        },
        storyAdded: function () {

        },
        removeStory: function () {

        },
        storyRemoved: function () {

        }
        //sendMessage: function (to) {
        //    var chatMessage = {
        //        sender: this.state,
        //        message: to.message,
        //        destination: to.destination
        //    };

        //    //Esse trecho é responsável por encaminhar a mensagem para o usuário selecionado
        //    this.connection.invoke("SendMessage", (chatMessage))
        //        .catch(err => console.log(x = err));

        //    //Método responsável por inserir a mensagem no chat
        //    insertMessage(chatMessage.destination, 'me', chatMessage.message);
        //    to.field.val('').focus();
        //},

        ////Método responsável por receber as mensagens
        //onReceiveMessage: function () {
        //    this.connection.on("Receive", (sender, message) => {
        //        openChat(null, sender, message);
        //    });
        //}
    };
}

//Método responsável por realizar a conexão do usuário no nosso Hub
async function startConnection(planning) {
    try {        
        planning.connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:7221/planning?user=" + JSON.stringify(planning.state)).build();
        await planning.connection.start();
        
        //Caso a conexão caia por algum motivo, esse trecho fará o trabalho para reconectar o cliente
        planning.connection.onclose(async () => {
            await startConnection(planning);
        });

        //Realiza o bind das nossas funções
        if (count < 1) {
            planning.loadPlayers();
            planning.removePlayers();
            planning.startVotingReceive();
            planning.stopVotingReceive();
            planning.playerVoted();
            planning.turnedCards();
            planning.restartedVotes();
            count++;
        }
        
    } catch (err) {
        console.log(err)
        setTimeout(() => startConnection(planning.connection), 5000);
    }
};

//Função para carregar usuários no chat
function loadPlayers(users) {
    for (let i = 0; i < users.length; i++) {
        if (!checkIfUserExists(usersConnected, users[i])) {
            usersConnected.push(users[i]);
            console.log(users[i])
            addPlayers(users[i]);
        }
    }
}

function checkIfUserExists(users, user) {
    const result = users.some((x) => {
        return x.userId === user.userId;
    });

    return result;
}

function removePlayers(users) {    
    for (let i = 0; i < usersConnected.length; i++) {        
        if (!checkIfUserExists(users, usersConnected[i])) {
            var elementId = 'player_' + usersConnected[i].userId;            
            $(`#${elementId}`).remove();
            usersConnected.splice(i, 1);
        }
    }
}

async function startVoting(storyId) {
    if (isAdmin == "False") {
        await selectRowOnStartVote(storyId);
    }

    $("#startVotingButton").prop('disabled', true);
    $("#startVotingButton").addClass("d-none");
    $("#addStoryButton").prop('disabled', true);

    $("#turnCardButton").prop('disabled', false);
    $("#votingDeckContainer").removeClass("d-none");
    $('#storyStatus').html("Status: Votação em andamento"); //TODO: Deixar essa parada dinamica de acordo com o BD... O nome do status pode mudar futuramente

    removeEventsOnRow();

}

function stopVoting(storyId) {
    $("#startVotingButton").prop('disabled', true);
    $("#startVotingButton").addClass("d-none");
    $("#addStoryButton").prop('disabled', false);

    $("#stopVotingButton").prop('disabled', true);
    $("#refreshVotingButton").prop('disabled', true);
    $("#turnCardButton").prop('disabled', true);
    $('#storyStatus').html("Status: Votação finalizada"); //TODO: Deixar essa parada dinamica de acordo com o BD... O nome do status pode mudar futuramente
    $("#playersConnected").addClass("d-none");

    addEventsOnRow();

   //for (let i = 0; i < users.length; i++) { //TODO: Verificar qual a pira aqui
   //    users[i].vote = "";
   //}
}

function playerVoted(userId) {
    selectCard($(`#player_${userId}`))    
}

function setTurnedCards(users, resultVoting) {
    $('#playersConnected').html("");

    for (let i = 0; i < users.length; i++) {
        addPlayers(users[i]);        
    }

    $("#refreshVotingButton").prop('disabled', false);
    $("#stopVotingButton").prop('disabled', false);
    $("#turnCardButton").prop('disabled', true);

    $.ajax({
        type: "POST",
        url: showVoteResultUrl,
        data: { model: resultVoting },
        success: function (data) {
            updateResultVote(storyId, resultVoting);
            $("#votingResultContainer").html(data);
            $("#votingDeckContainer").addClass("d-none");
            $("#votingResultContainer").removeClass("d-none");
        }
    });
}

function restartedVotes(users) {
    $('#playersConnected').html("");

    for (let i = 0; i < users.length; i++) {
        addPlayers(users[i]); 
    }

    $("#refreshVotingButton").prop('disabled', true);
    $("#stopVotingButton").prop('disabled', true);
    $("#turnCardButton").prop('disabled', false);

    $("#votingDeckContainer").removeClass("d-none");
    $("#votingResultContainer").addClass("d-none");

    var deckCards = $("div[name='cardForSelect']");
    for (let i = 0; i < deckCards.length; i++) {
        deselectCard($(deckCards[i]));
    }
}

function selectCard(card) {
    card.removeClass("border-1");
    card.removeClass("shadow-sm");

    card.addClass("border-3");
    card.addClass("shadow");
    card.addClass("border-info");
}

function deselectCard(card) {
    card.addClass("border-1");
    card.addClass("shadow-sm");

    card.removeClass("border-3");
    card.removeClass("shadow");
    card.removeClass("border-info");
}

function addPlayers(user) {
    user.selectedStory = storyId;

    $.ajax({
        type: "POST",
        url: addNewPlayerUrl,
        data: { player: user },
        success: function (data) {
            $('#playersConnected').append(data);

            if (user.votes != undefined) {
                for (let c = 0; c < user.votes.length; c++) {
                    if (user.votes[c].storyId == storyId && user.votes[c].vote != "") {
                        selectCard($(`#player_${user.userId}`));
                    } else {
                        deselectCard($(`#player_${user.userId}`));
                    }
                }
            }
        }
    });
}

//$(document).ready(function () {
//    window.chat = createChatController();
//    window.chat.loadUser();
//});

//function createChatController() {
//    var user = {
//        userId: null,
//        name: null,
//        dtConnection: null
//    }

//    return {
//        state: user,
//        connection: null,
//        loadUser: function () {
//            this.state.name = prompt('Digite seu nome para entrar no chat', 'Usuário');
//            this.state.userId = new Date().valueOf();
//            this.state.dtConnection = new Date();
//            this.connectUserToChat();
//        },
//        connectUserToChat: function () {
//            //Aqui iniciamos a conexão e deixamos ela aberta
//            startConnection(this);
//        },
//        sendMessage: function (to) {
//            var chatMessage = {
//                sender: this.state,
//                message: to.message,
//                destination: to.destination
//            };

//            //Esse trecho é responsável por encaminhar a mensagem para o usuário selecionado
//            this.connection.invoke("SendMessage", (chatMessage))
//                .catch(err => console.log(x = err));

//            //Método responsável por inserir a mensagem no chat
//            insertMessage(chatMessage.destination, 'me', chatMessage.message);
//            to.field.val('').focus();
//        },
//        //Método responsável por receber as mensagens
//        onReceiveMessage: function () {
//            this.connection.on("Receive", (sender, message) => {
//                openChat(null, sender, message);
//            });
//        }
//    };
//}

////Método responsável por realizar a conexão do usuário no nosso Hub
//async function startConnection(chat) {
//    try {
//        chat.connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:7221/planning?user=" + JSON.stringify(window.chat.state)).build();
//        await chat.connection.start();

//        //Carrega usuários no chat
//        loadChat(chat.connection);

//        //Caso a conexão caia por algum motivo, esse trecho fará o trabalho para reconectar o cliente
//        //chat.connection.onclose(async () => {
//        //    await startConnection(chat);
//        //});

//        //Realiza o bind da nossa função para receber mensagem
//        chat.onReceiveMessage();

//    } catch (err) {
//        setTimeout(() => startConnection(chat.connection), 5000);
//    }
//};

////Função para carregar usuários no chat
//async function loadChat(connection) {
//    connection.on('planning', (users, user) => {
//        const listUsers = (data) => {
//            return users.map((u) => {
//                if (!checkIfElementExist(u.key, 'id') && u.key != window.chat.state.key)
//                    return (
//                        `
//              <section class="user box_shadow_0" onclick="openChat(this)" data-id="${u.key}" data-name="${u.name}">
//              <span class="user_icon">${u.name.charAt(0)}</span>
//              <p class="user_name"> ${u.name} </p>
//              <span class="user_date"> ${new Date(u.dtConnection).toLocaleDateString()}</span>
//              </section>
//              `
//                    )
//            }).join('')
//        }
//        console.log(users);
//        $('.main').append(listUsers);
//    });
//}

////Método responsável por iniciar um novo chat
//function openChat(e, sender, message) {

//    var user = {
//        id: e ? $(e).data('id') : sender.key,
//        name: e ? $(e).data('name') : sender.name
//    }

//    if (!checkIfElementExist(user.id, 'chat')) {
//        const chat =
//            `
//        <section class="chat" data-chat="${user.id}">
//        <header>
//            ${user.name}
//        </header>
//        <main>
//        </main>
//        <footer>
//            <input type="text" placeholder="Digite aqui sua mensagem" data-chat="${user.id}">
//            <a onclick="sendMessage(this)" data-chat="${user.id}">Enviar</a>
//        </footer>
//        </section>
//        `

//        $('.chats_wrapper').append(chat);
//    }
//    if (sender && message)
//        insertMessage(sender.key, 'their', message);
//}

////Método responsável por inserir a mensagem no chat
//function insertMessage(target, who, message) {
//    const chatMessage = `
//    <div class="message ${who}">${message} <span>${new Date().toLocaleTimeString()}</span></div>
//    `;
//    $(`section[data-chat="${target}"]`).find('main').append(chatMessage);
//}

////Método responsável por capturar a mensagem e enviar
//function sendMessage(e) {

//    var input = {
//        destination: $(e).data('chat'),
//        field: $(`input[data-chat="${$(e).data('chat')}"]`),
//        message: $(`input[data-chat="${$(e).data('chat')}"]`).val()
//    }

//    window.chat.sendMessage(input);
//}

////Função genérica para verificar se o elemento já existe na DOM
//function checkIfElementExist(id, data) {
//    return $('section[data-' + data + '="' + id + '"]') && $('section[data-' + data + '="' + id + '"]').length > 0;
//}