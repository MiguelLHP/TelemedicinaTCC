"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/Chat").build();
var chartBlock = "\u25A3";
var userId = document.getElementById("userID").innerHTML;
var user = document.getElementById("loggedUser").innerHTML;
var pacientName = document.getElementById("pacientName").innerHTML;
var doctorName = document.getElementById("doctorName").innerHTML;
var idAtendimento = document.getElementById("idAtendimento").innerHTML;
var myChannelId = null;
var myChannelVal = null;


connection.on("ReciveMessage", function (user, message) {
    var chatBox = document.getElementById('chat-box');
    var messageElement = document.createElement('div');

    if (user == doctorName) {
        messageElement.setAttribute('id', "doutor");
        messageElement.innerHTML = '<strong>' + user + ':</strong><br>' + message;
        chatBox.appendChild(messageElement);
    }

    if (user == pacientName) {
        messageElement.setAttribute('id', "usuario");
        messageElement.innerHTML = '<strong>' + user + ':</strong><br>' + message;
        chatBox.appendChild(messageElement);
    }

    if (message.includes("estou encerrando o atendimento!")) {
        setTimeout(function () {
            closeAtendimentoPaciente()
        }, 1000)
    }


    // Rola o chat para baixo para exibir a última mensagem
    chatBox.scrollTop = chatBox.scrollHeight;
});

connection.start().then(function () {
    if (user != pacientName) {
        $.ajax({
            url: `?handler=ChangeAtendimentoStatus&idAtendimento=${idAtendimento}&userID=${userId}`,
            type: "POST",
            headers:
            {
                "RequestVerificationToken": $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (data) {
            },
            error: function (error) {
            }
        })
    }

    if (user == pacientName) {
        connection.invoke("SendMessage", user, "Entrou neste canal...", myChannelId, myChannelVal).catch(function (err) {
            return console.error(err.toString());
        });
    }

}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {

    var message = document.getElementById("message-input").value;

    console.log(myChannelId);
    connection.invoke("SendMessage", user, message, myChannelId, myChannelVal).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

function closeAtendimento() {
    connection.invoke("SendMessage", user, `Desejo melhoras ${pacientName}, estou encerrando o atendimento!`, myChannelId, myChannelVal).catch(function (err) {
        return console.error(err.toString());
    });
    var resultado = $("#resultado").val();
    connection.stop().then(function () {
        console.log("closed");
        connection = null;
        $.ajax({
            url: `?handler=CloseAtendimento&idAtendimento=${idAtendimento}&resultado=${resultado}`,
            type: "POST",
            headers:
            {
                "RequestVerificationToken": $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (data) {
                closeAtendimentoPaciente();
                window.location.replace("/Identity/Atendimento/Index");
            },
            error: function (error) {
                console.log("Not Ok", error);
            }
        })
    });
}

function closeAtendimentoPaciente() {

    window.location.replace("/Identity/Atendimento/Index");
}


