var PushedTestId;

/*--------Creating csv template for import--------*/
function CreateTemplate() {
    var csv = 'TESTO PAVADINIMAS;TESTO LAIKAS (min)\nTesto Pavadinimas;20\nKLAUSIMAS;KURIS TEISINGAS ATSAKYMAS (nr.);ATSAKYMAI\n1 klausimas;3;1 atsakymas;2 atsakymas;3 atsakymas;4 atsakymas';
    var hiddenElement = document.createElement('a');
    hiddenElement.href = 'data:text/csv;charset=utf-8,' + encodeURI(csv);
    hiddenElement.target = '_blank';
    hiddenElement.download = 'TestTemplate.csv';
    hiddenElement.click();
}

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
    // Check if there any test list is empty
    $(".no-test-in-list").hide();
    if ($(".item-container-shadow li").length <= 0) {
        $(".item-header, .item-container-shadow").hide();
        $(".no-test-in-list").show();
    }
    else {
        $(".item-header, .item-container-shadow").show();
        $(".no-test-in-list").hide();
    }
});




