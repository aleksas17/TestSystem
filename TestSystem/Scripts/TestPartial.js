$( document ).ready(function() {

    /*--------Validation radio button checked-------*/
            
    $("input[type='radio']").change(function () {
        $("button[type='submit']").prop("disabled", false);
    });
});