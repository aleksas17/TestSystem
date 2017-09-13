
function playVid() {
    myVideo.play();
}    

/*--------Jquery--------*/
$(document).ready(function () {
    /*--------Validation style--------*/
    $("#login-button").on("click",
        function() {
            if ($(".field-validation-error").text().length > 0) {
                $("#Username").css("border-color", "#d50000");
                $("#Password").css("border-color", "#d50000");
            }
        });
});
