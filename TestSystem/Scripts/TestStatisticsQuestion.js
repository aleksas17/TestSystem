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

});