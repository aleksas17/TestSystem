/*--------Pop up window--------*/

function PopUpBox() {
    document.getElementById("create-new-user-helper").style.display = "flex";
    document.getElementById("create-new-user-helper").style.visibility = "visible";
}

function OpenTestStatistics(testId) {
    PopUpBox();
    $("#create-new-user-helper").load("/TestAdministration/UsersScores/?testId=" + testId);
}

function OpenAssignTest(testId) {
    PopUpBox()
    $("#create-new-user-helper").load("/TestAdministration/AssignTest/" + testId);
}

function OpenCreateTest() {
    PopUpBox()
    $("#create-new-user-helper").load("/TestAdministration/CreateTest/");
}

