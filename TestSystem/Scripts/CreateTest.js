/*--------Jquery--------*/
var currentQuestion = 0;
var currentChoise = 1;

/*--------Add choise--------*/
function AddChoice() {
    $(".field-validation-error:last").after("<div class='choice'>"
        + '<input type="hidden" name="Questions[' + currentQuestion + '].Answers.Index" value="' + currentChoise + '" />'
        + '<div class="input-radio-checkbox">'
        + '<input class="question-' + currentQuestion + '" type="radio" name="Questions[' + currentQuestion + '].Answers[' + currentChoise + '].IsCorrect" id="Questions[' + currentQuestion + '].Answers[' + currentChoise + ']">'
        + '<label class="check" for="Questions[' + currentQuestion + '].Answers[' + currentChoise + ']"></label>'
        + '</div>'
        + '<div class="input-holder">'
        + '<input type="text" name="Questions[' + currentQuestion + '].Answers[' + currentChoise + '].Name" required>'
        + '<span class="input-bar"></span>'
        + '<label>Choise</label>'
        + '</div>'
        + '<div class="remove-choice">'
        + '<svg viewBox="0 0 24 24">'
        + '<path d="M19 6.41L17.59 5 12 10.59 6.41 5 5 6.41 10.59 12 5 17.59 6.41 19 12 13.41 17.59 19 19 17.59 13.41 12z"/>'
        + '</svg>'
        + '</div>'
        + '</div>'
        + '<span class="field-validation-error"><span>Choice cant be empty</span></span>');
    if (currentChoise == 0) {
        $('input[name="Questions[' + currentQuestion + '].Name"]').focus();
    }
    else {
        $('input[name="Questions[' + currentQuestion + '].Answers[' + currentChoise + '].Name"]').focus();
    }
    currentChoise += 1;
}

/*--------When document fully loads--------*/
$(document).ready(function () {
    /*--------Custom radio button by class grouping, add and remove check from item when needed--------*/
    $('body').on("change", "input[type=radio]", function () {
        var checkboxClassName = $(this).attr("class");
        $("." + checkboxClassName).not(this).prop('checked', false);
        $("." + checkboxClassName).not(this).prop('value', 0);
        $(this).prop('value', 1);
    });

    /*--------Add choise on clicking add choice--------*/
    $(".add-answer").on("click", function () {
        AddChoice();
    });

    /*--------Remove choice universal--------*/
    $("body").on("click", ".remove-choice", function () {
        if ($(this).parents(".question").length) {
            /*--------Remove choice from question in test--------*/
            if ($(this).closest(".question").children(".choice").length == 1 || $(this).parent(".input-holder-question").length) {
                // If choice is last one, delete question.
                $(this).closest(".question").remove();
            }
            else {
                $(this).parent().remove();
            }
        }
        else {
            /*--------Remove choice question-creator-tools--------*/
            if ($("#question-creator .choice").length == 1) {
                // If choice is last one, don't delete question.
                return;
            }
            else {
                $(this).parent().remove();
            }
        }
    });

    /*--------Add question and choises to test--------*/
    $(".question-creator-tools > div").on("click", function () {
        $("#question-creator .choice").clone().prependTo(".question:last-of-type");
        $("#question-creator > .input-holder-question").clone().prependTo(".question:last-of-type");

        $(".question:last-of-type .input-holder-question .input-holder").after("<div class='remove-choice'><svg viewBox='0 0 24 24'><path d='M19 6.41L17.59 5 12 10.59 6.41 5 5 6.41 10.59 12 5 17.59 6.41 19 12 13.41 17.59 19 19 17.59 13.41 12z'/></svg></div>");
        $(".question:last-of-type").after("<div class='question'></div>");
        // Clear question-creator-tools.
        $("#question-creator").children(".choice").remove();
        currentQuestion += 1;
        currentChoise = 0;
        $("#question-creator .input-holder-question input[type=hidden]").prop("value", currentQuestion);
        $("#question-creator .input-holder-question .input-holder input[type=text]").prop("name", "Questions[" + currentQuestion + "].Name");
        $('input[name="Questions[' + currentQuestion + '].Name"]').val("");
        AddChoice();
        // Seting up firs choice to be checked when we clear question-creator
        $("#question-creator .input-radio-checkbox input[type=radio]").prop('checked', true);
        $("#question-creator .input-radio-checkbox input[type=radio]").prop('value', 1);
    });

    /*--------Bypass required field--------*/
    $(".create-new-test button").click(function () {
        $("#question-creator input").val(" ");
    });

    /*--------Create test form POST--------*/
    $(".create-new-test").submit(function () {
        $("#question-creator input").prop("disabled", true);
        return true; //send
    });
});