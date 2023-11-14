
import Typebot from 'https://cdn.jsdelivr.net/npm/@typebot.io/js@0.2/dist/web.js'
//No mostrar si la ventana es menor a 425px
if (window.innerWidth > 425) {
    Typebot.initBubble({
        typebot: "my-typebot-yqzh0ac",
        previewMessage: {
            message: "Estoy Aqui para Ayudarte!",
            autoShowDelay: 5000,
            avatarUrl:
                "https://s3.typebot.io/public/workspaces/clnyrmsbo000tmw0f6fvvcoo7/typebots/clnyrn1hl003xl90ftyqzh0ac/hostAvatar?v=1697830542773",
        },
        theme: {
            button: { backgroundColor: "#008dc9", size: "large" },
            chatWindow: { backgroundColor: "#FFFFFF" },
        },
    });
}
//}
//Typebot.initBubble({
//    typebot: "my-typebot-yqzh0ac",
//    previewMessage: {
//        message: "Estoy Aqui para Ayudarte!",
//        autoShowDelay: 5000,
//        avatarUrl:
//            "https://s3.typebot.io/public/workspaces/clnyrmsbo000tmw0f6fvvcoo7/typebots/clnyrn1hl003xl90ftyqzh0ac/hostAvatar?v=1697830542773",
//    },
//    theme: {
//        button: { backgroundColor: "#008dc9", size: "large" },
//        chatWindow: { backgroundColor: "#FFFFFF" },
//    },
//});