function TestStatisticsUsersPartial() {
    $("#create-new-user-helper").load("/TestAdministration/TestStatisticsUsers/?testId=" + PushedTestId);
}

function TestStatisticsQuestionPartial() {
    $(".tab-holder").load("/TestAdministration/TestStatisticsQuestion");
}

$(document).ready(function () {
    $(".tab-holder").load("/TestAdministration/TestStatisticsUsers/?testId=" + PushedTestId);
    // First tab open
    $(".tab-controler > div:first-child").click(function () {
            TestStatisticsUsersPartial()
            $(".tab-controler").removeClass("after-helper");
    });
    // Second tab open
    $(".tab-controler > div:last-child").click(function () {
            TestStatisticsQuestionPartial()
            $(".tab-controler").addClass("after-helper");
    });
});
