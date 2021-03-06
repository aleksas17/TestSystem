﻿/*--------Creating csv template for import--------*/
function CreateTemplate() {
    var csv = 'USERNAME;FIRSTNAME;LASTNAME;PASWORD;POSTION;GROUP;ROLE';
    var hiddenElement = document.createElement('a');
    hiddenElement.href = 'data:text/csv;charset=utf-8,' + encodeURI(csv);
    hiddenElement.target = '_blank';
    hiddenElement.download = 'UserTemplate.csv';
    hiddenElement.click();
}

/*--------Bubble Popup--------*/

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

/*--------Create user window--------*/

function OpenCreateUser() {
    document.getElementById("create-new-user-helper").style.display = "flex";
    document.getElementById("create-new-user-helper").style.visibility = "visible";
    $("#create-new-user-helper").load("/Account/AddEditRecord");
}

function OpenEditUser(userId) {
    document.getElementById("create-new-user-helper").style.display = "flex";
    document.getElementById("create-new-user-helper").style.visibility = "visible";
    $("#create-new-user-helper").load("/Account/AddEditRecord/" + userId);
}

function CloseCreateUser() {
    document.getElementById("create-new-user-helper").style.display = "none";
    document.getElementById("create-new-user-helper").style.visibility = "hidden";
}

/*--------Jquery--------*/

$(document).ready(function () {

    $("#create-new-user-helper").on("click", function (e) {
        if (e.target !== this)
            return;
        CloseCreateUser();
    });
    /// TODO: Search on keyup 
    //$("#SearchString").bind("keyup", function () {
    //    $("#list-search").delay(200).submit();
    //});
    
    //$("#list-search").submit(function (event) {
    //    event.preventDefault();
    //    var searchUrl = '/Account/UserList?SearchString=' + $("#SearchString").val();
    //    $.ajax({
    //        type: "post",
    //        dataType: "html",
    //        url: searchUrl,
    //        data: $("#list-search").serialize(),
    //        success: function (response) {
    //            $(".item-container-shadow").load(" .item-container-shadow");
    //        }
    //        });
    //});
});