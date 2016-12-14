$("#button1")
    .mouseenter(function () {
        changeBackgroundImage('images/entertainment.jpg');
    })
   // .mouseleave(function () {
   //     changeBackgroundImage('');
   // });

$("#button2")
    .mouseenter(function () {
        changeBackgroundImage('images/mutualhelp.jpg');
    });

$("#button3")
    .mouseenter(function () {
        changeBackgroundImage('images/science.png');
    });

function changeBackgroundImage(imageUrl) {
    $('body').css("background-image", `url(${imageUrl})`);
}