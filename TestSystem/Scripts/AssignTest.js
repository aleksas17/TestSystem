function CheckboxesChected() {
    var check = $('.user-list-table-item > div input[type=checkbox]:checked').length;
    $(".user-list-table-controler label").text(check + " item selected");
}

function CheckAllCheckboxes() {
    if ($(".user-list-table-header input").is(":checked")) {
        $(".user-list-table-item input").filter(function () { return $(this).is(':visible'); }).prop("checked", true);
    } else {
        $(".user-list-table-item input").filter(function () { return $(this).is(':visible'); }).prop("checked", false);
    }
}

/*--------Jquery--------*/

$(document).ready(function () {
    /*--------Check or uncheck all checkboxes--------*/
    $(".user-list-table-header input").click(function () {
        CheckAllCheckboxes();
    });

    /*--------Check how many checkboxes are checkted--------*/
    $("input[type=checkbox]").click(function () {
        CheckboxesChected();
    });

    /*--------Filter by Group--------*/
    $(".user-list-table-header > select").change(function () {
        var valueSelected = this.value;
        $(".user-list-table-item").show();
        CheckAllCheckboxes();
        if (valueSelected == "Group Name") {
            $(".user-list-table-item").show();
        }
        else {
            $(".user-list-table-item label:not(:contains(" + valueSelected + "))").parent('.user-list-table-item').hide().find('input:checkbox').prop('checked', false);
        }
        CheckboxesChected();
    });
});