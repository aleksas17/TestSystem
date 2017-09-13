/*--------Pop up window--------*/

function OpenAssignTest(testId) {
    document.getElementById("create-new-user-helper").style.display = "flex";
    document.getElementById("create-new-user-helper").style.visibility = "visible";
    $("#create-new-user-helper").load("/TestAdministration/AssignTest/" + testId);
}