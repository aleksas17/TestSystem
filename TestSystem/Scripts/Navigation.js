/*--------Jquery--------*/
// Animation for navigation drawer
function NavAnimation() {
    $(".hamburger-logo").toggleClass("active");
    $("body > nav").toggleClass("slide-nav-back");
    $("nav > div:first-child").toggleClass("start-animation");
}

$(document).ready(function () {
    $(".hamburger-logo").on("click", function () {
        NavAnimation();
        // binding event handler to check if click is outside navigation
        $(document).bind("click.navAnimation", function (e) {
            var navigation = $("body > nav");
            // check that your clicked element is not navigation and is not child of navigation
            if (!navigation.is(e.target) && navigation.has(e.target).length === 0) {
                NavAnimation();
                $(document).unbind("click.navAnimation");
            }
        });
    });
});
