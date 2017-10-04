$(document).ready(function () {
    // Hover function for test question statics table
    $(".t-s-table-questions-main > li > ul:not(:first-child):not(:last-child):not(:nth-last-child(2)) > li").hover(
        function () {
            // On hover
            var hoverdItemUser = $(this).parent().index() + 1;
            var hoverdItemQuestion = $(this).index() + 1;
            $(".t-s-table-questions-main > li > ul:first-child > li:nth-child(" + hoverdItemQuestion + ")").addClass("table-item-hover");
            $(".t-s-table-questions-main > nav > li:nth-child(" + hoverdItemUser + ")").addClass("table-item-hover");
            $(this).parent().addClass("table-item-hover");
        }, function () {
            // Leave hover
            var hoverdItemUser = $(this).parent().index() + 1;
            var hoverdItemQuestion = $(this).index() + 1;
            $(".t-s-table-questions-main > li > ul:first-child > li:nth-child(" + hoverdItemQuestion + ")").removeClass("table-item-hover");
            $(".t-s-table-questions-main > nav > li:nth-child(" + hoverdItemUser + ")").removeClass("table-item-hover");
            $(this).parent().removeClass("table-item-hover");
        }
    );
    // Tooltip only for question Name
    $('.t-s-table-questions-main > li > ul:first-child > li').hover(function () {
        // Hover over code
        var title = $(this).attr('title');
        $(this).data('tipText', title).removeAttr('title');
        $('<p class="tool-tip"></p>').text(title).appendTo('body').fadeIn('none');
    }, function () {
        // Hover out code
        $(this).attr('title', $(this).data('tipText'));
        $('.tool-tip').remove();
    }).mousemove(function (e) {
        var mousex = e.pageX + 20; //Get X coordinates
        var mousey = e.pageY + 20; //Get Y coordinates
        if (mousex > $("body").width() / 2) {
            $('.tool-tip').css({ top: mousey, right: $("body").width() - mousex + 100, left: "auto" })
        }
        else {
            $('.tool-tip').css({ top: mousey, left: mousex, right: "auto" })
        }
    });
    // function for bad answers
    var totalQuestions = $(".t-s-table-questions-main > li > ul:first-child").find('li').length;
    for (var i = 1; i <= totalQuestions; i++) {
        var totalQuetionsBad = totalUserAnswers - +($(".t-s-table-questions-main > li > ul:nth-last-child(2) > li:nth-child(" + i + ")").text());
        $(".t-s-table-questions-main > li > ul:last-child").append("<li>" + totalQuetionsBad + "</li>")
    }
    // Change page in statistic table without reloading main window
    $(".tab-holder a").on("click", function (event) {
        event.preventDefault();
        $.ajax({
            url: $(this).attr('href') + "&testId=" + PushedTestId,
            type: "GET",
        }).done(function (partialViewResult) {
            $(".tab-holder").html(partialViewResult);
        });
    });
    
});