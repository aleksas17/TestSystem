﻿$(document).ready(function () {
    // Change page in statistic table without reloading main window
    $(".tab-holder a").on("click", function (event) {
        event.preventDefault();
        if ($(this).attr('href') != null) {
            $.ajax({
                url: $(this).attr('href') + "&testId=" + PushedTestId,
                type: "GET",
            }).done(function (partialViewResult) {
                $(".tab-holder").html(partialViewResult);
            });
        }
    });
});