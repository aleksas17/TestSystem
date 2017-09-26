/*--------Jquery--------*/
$(document).ready(function () {
    /*---------Notifier if there are no test-----------*/
    if ($(".item-container div").length > 1)
        $(".no-test-assigned").css("display", "none");
});