"use stric"

const roomID = $('#roomId').text();
let userID = null;
let localStream = null;
const Peers = {}

const connection = new signalR.HubConnectionBuilder().withUrl("/Meeting").build();

const myPeer = new Peer();
myPeer.on('open', id => {
    userId = id;
    var startSignalR = async () => {
        await connection.start();
        await connection.invoke("JoimRoom", roomID, userId)
    }
    startSignalR();
})

const videoGrid = document.querySelector('#video-grid')
const myVideo = document.createElement('video')
myVideo.muted = true;

navigator.mediaDevices.getUserMedia({
    audio: true,
    video: true
}).then(stream => {
    addVideoStream(myVideo, stream)

    localStream = stream
})

connection.on('user-connected', id => {
    if (userId === id) return;
    console.log(`User Connected: ${id}`)
    connectNewUser(id, localStream)
})

connection.on('user-disconnected', id => {
    console.log(`User disconnected ${id}`)

    window.location.replace("/Identity/Atendimento/Index")
})

myPeer.on('call', call => {
    call.answer(localStream)

    const userVideo = document.createElement('video')
    call.on('stream', userVideoStream => {
        addVideoStream(userVideo, userVideoStream)
    })
})

const addVideoStream = (video, stream) => {
    video.srcObject = stream;
    video.addEventListener('loadedmetadata', () => {
        video.play()
    })
    videoGrid.appendChild(video)
}

const connectNewUser = (userId, localStream) => {
    const userVideo = document.createElement('video')
    const call = myPeer.call(userId, localStream)

    call.on('stream', userVideoStream => {
        addVideoStream(userVideo, userVideoStream)
    })

    call.on('close', () => {
        console.log("close")
        userVideo.remove()
    })

    Peers[userId] = call
}

function closeAtendimento() {
    var resultado = $("#resultado").val();
    var idAtendimento = document.getElementById("idAtendimento").innerHTML;
    connection.stop().then(function () {
        $.ajax({
            url: `?handler=CloseAtendimento&idAtendimento=${idAtendimento}&resultado=${resultado}`,
            type: "POST",
            headers:
            {
                "RequestVerificationToken": $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (data) {
                window.location.replace("/Identity/Atendimento/Index");
            },
            error: function (error) {
                console.log("Not Ok", error);
            }
        })
    });
}

function closeAtendimentoPaciente() {
    window.location.replace("/Identity/Atendimento/Index")
}