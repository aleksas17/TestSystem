var PushedTestId;

/*--------Popup window--------*/

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

/*--------Close popup--------*/

function CloseCreateUser() {
    document.getElementById("create-new-user-helper").style.display = "none";
    document.getElementById("create-new-user-helper").style.visibility = "hidden";
}

/*--------Bubble popup--------*/

var bubbleVisibleName;

function HideBubblePopups() {
    document.getElementById(bubbleVisibleName).style.visibility = "hidden";
    document.getElementById("bubble-popup-helper").style.display = "none";
}

function PopupBubbleOpen(myId) {
    document.getElementById("list-item-bubble-" + myId).style.visibility = "visible";
    document.getElementById("bubble-popup-helper").style.display = "inline";
    bubbleVisibleName = "list-item-bubble-" + myId;
}

/*--------Search box--------*/

function SearchFocus() {
    document.getElementById("list-search").style.background = "#ffffff";
    document.getElementById("list-search").style.boxShadow = "rgba(0,0,0,0.24) 0px 1px 1px 0px";
}

function SearchBlur() {
    document.getElementById("list-search").style.background = "#f5f5f5";
    document.getElementById("list-search").style.boxShadow = "none";
}

$(document).ready(function () {
    $("#create-new-user-helper").on("click", function (e) {
        if (e.target !== this)
            return;
        CloseCreateUser();
    });
});



