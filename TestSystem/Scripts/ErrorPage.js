/*--------Generates random number in range (min / max)--------*/
function RandomNumberGenerator(min, max) {
    return Math.floor(Math.random() * (max - min + 1) + min);
}
/*--------Animates SVG path(line) stroke dashoffset--------*/
function SVGPathAnimation() {
    // 20 paths(lines) will be pickted and animated
    for (i = 1; i < 20; i++) {
        // pick random path(line)
        var path = RandomNumberGenerator(1, 92);
        // check if object is in animation state or not, if not assign animation
        if ($(".background-topography > div > svg > path:nth-child(" + path + ")").not(":animated")) {
            $(".background-topography > div > svg > path:nth-child(" + path + ")").css("animation", "dash " + RandomNumberGenerator(10, 15) + "s cubic-bezier(0.4, 0.0, 0.2, 1)");
        }
    }
}

$(document).ready(function () {
    SVGPathAnimation();
    // fire every 15 seconds
    window.setInterval(function () {
        SVGPathAnimation();
    }, 15000);
    // on animation end delete animation, this is used for restarting animation
    $('path').on('animationend', function () {
        $(this).css("animation", "none");
    });
});