var PushedTestId;

/*--------Pop up window--------*/

function PopUpBox() {
    document.getElementById("create-new-user-helper").style.display = "flex";
    document.getElementById("create-new-user-helper").style.visibility = "visible";
}

function OpenTestStatistics(id) {
    PushedTestId = id;
    PopUpBox();
    $("#create-new-user-helper").load("/TestAdministration/UsersStatistics/");
}

function OpenAssignTest(testId) {
    PopUpBox()
    $("#create-new-user-helper").load("/TestAdministration/AssignTest/" + testId);
}

function OpenCreateTest() {
    PopUpBox()
    $("#create-new-user-helper").load("/TestAdministration/CreateTest/");
}



