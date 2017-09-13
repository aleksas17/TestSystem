/*--------Hamburger Menu--------*/

function HamburgerOpen() {
    document.getElementById("sideControl").style.transform = "translate3d(0,0,0)";
    document.getElementById("navHelper").style.display = "inline";
}

function HamburgerClose() {
    document.getElementById("sideControl").style.transform = "translate3d(-280px,0,0)";
    document.getElementById("navHelper").style.display = "none";
}

/*------------------------------*/