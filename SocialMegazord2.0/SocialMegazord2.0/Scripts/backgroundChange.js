let imageFolder = '/Content/Images';

$("#button1")
    .mouseenter(function () {
        changeBackgroundImage(`${imageFolder}/entertainment.jpg`);
    })
   // .mouseleave(function () {
   //     changeBackgroundImage('');
   // });

$("#button2")
    .mouseenter(function () {
        changeBackgroundImage(`${imageFolder}/mutualhelp.jpg`);
    });

$("#button3")
    .mouseenter(function () {
        changeBackgroundImage(`${imageFolder}/science.png`);
    });

function changeBackgroundImage(imageUrl) {
    $('body').css("background-image", `url(${imageUrl})`);
}