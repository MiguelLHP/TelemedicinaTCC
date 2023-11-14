"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/Chat").build();
var chartBlock = "\u25A3";
var cont = 1;
var triagemArr = [];

connection.on("ReciveMessage", function (user, message) {
    var chatBox = document.getElementById('chat-box');
    var messageElement = document.createElement('div');
    if (user != "Enfermeira") {
        messageElement.setAttribute('id', "usuario");
        messageElement.innerHTML = '<strong>' + user + ':</strong><br>' + message;
        chatBox.appendChild(messageElement);
    } else {
        messageElement.setAttribute('id', user);
        messageElement.innerHTML = '<strong>' + user + ':</strong><br>' + message;
        chatBox.appendChild(messageElement);
    }

    // Rola o chat para baixo para exibir a última mensagem
    chatBox.scrollTop = chatBox.scrollHeight;
});

connection.start().then(function () {
    var myChannelId = $('input[name=myChannel]:checked').attr('id');
    var myChannelVal = $('input[name=myChannel]:checked').val();
    connection.invoke("SendMessage", "Enfermeira", enfermeira[0], myChannelId, myChannelVal).catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var myChannelId = $('input[name=myChannel]:checked').attr('id');
    var myChannelVal = $('input[name=myChannel]:checked').val();

    var user = document.getElementById("user").innerHTML;
    var userId = document.getElementById("userID").innerHTML;
    var message = document.getElementById("message-input").value;

    if (!user) {
        user = "[Anônimo]";
    }


    connection.invoke("SendMessage", user, message, myChannelId, myChannelVal).catch(function (err) {
        return console.error(err.toString());
    });

    triagemArr.push(message);

    connection.invoke("SendMessage", "Enfermeira", enfermeira[cont], myChannelId, myChannelVal).catch(function (err) {
        return console.error(err.toString());
    });

    if (cont == 4) {
        $("#sendButton").attr('disabled', true)
        setTimeout(function () {
            connection.stop().then(function () {
                console.log("closed", triagemArr);
                connection = null;
                $.ajax({
                    url: `?handler=CreateTriagem&triagem=${triagemArr}&userID=${userId}`,
                    success: function (data) {
                        console.log("Ok");
                        window.location.replace("/Identity/Atendimento/Index");
                    },
                    error: function (error) {
                        console.log("Not Ok", error);
                    }
                })
            })
        }, 2000)

    }





    cont = cont + 1;
    event.preventDefault();
});

const enfermeira = [
    "Olá eu sou a Enfermeira e irei realizar sua triagem.<br>Por favor, responda as perguntas com Sim ou Não<br>Você possui alguma alergia?",
    "Você possui alguma doênça crônica?",
    "Você possui alguma diabetes?",
    "Você possui alguma problema respiratório?",
    "Obrigado! agora aguarde o atendimento do médico!"
]