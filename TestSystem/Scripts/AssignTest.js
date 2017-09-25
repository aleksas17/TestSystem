/*--------Ckecks how many checkboxes are checkted--------*/
function CheckboxesChected() {
    var check = $('.test-statics-table > .t-s-table-main input[type=checkbox]:checked').length;
    if (check > 0) {
        $(".test-statics-table > header > nav > h1").text(check + " user selected");
        if (!$(".test-statics-table > header > nav").hasClass("selected-items")) {
            $(".test-statics-table > header > nav").toggleClass("selected-items");
        }
    }
    else {
        $(".test-statics-table > header > nav > h1").text("User List");
        if ($(".test-statics-table input[type=checkbox]:checked").length == 0)
            $(".test-statics-table > header > nav").toggleClass("selected-items");
    }
}

/*--------Ckecks or uncheck all checkboxes--------*/
function CheckAllCheckboxes() {
    if ($(".test-statics-table > header > ul > li:first-child input[type=checkbox]").is(":checked")) {
        $(".test-statics-table input[type=checkbox]").filter(function () { return $(this).is(':visible'); }).prop("checked", true);
    } else {
        $(".test-statics-table input[type=checkbox]").filter(function () { return $(this).is(':visible'); }).prop("checked", false);
    }
}

/*--------Jquery--------*/
$(document).ready(function () {
    // Check or uncheck all checkboxes
    $(".test-statics-table > header > ul > li:first-child input[type=checkbox]").click(function () {
        CheckAllCheckboxes();
    });
    // Check how many checkboxes are checkted
    $("input[type=checkbox]").click(function () {
        CheckboxesChected();
    });
    // Fileter by group
    $(".test-statics-table > header > ul > li > select").change(function () {
        var valueSelected = this.value;
        $(".t-s-table-main > li").show();
        CheckAllCheckboxes();
        if (valueSelected == "Group Name") {
            $(".t-s-table-main > li").show();
        }
        else {
            $(".t-s-table-main > li > div:last-child:not(:contains(" + valueSelected + "))").parent('.t-s-table-main > li').hide().find('input:checkbox').prop('checked', false);
        }
        CheckboxesChected();
    });
});