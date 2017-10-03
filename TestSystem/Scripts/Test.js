/*--------JavaScript--------*/

/*--------Main countdown timer metahod--------*/

function CountDownTimer(duration, granularity) {
    this.duration = duration;
    this.granularity = granularity || 1000;
    this.tickFtns = [];
    this.running = false;
}

CountDownTimer.prototype.start = function () {
    if (this.running) {
        return;
    }

    this.running = true;
    var start = Date.now(),
        that = this,
        diff, obj;

    (function timer() {
        diff = that.duration - (((Date.now() - start) / 1000) | 0);

        if (diff > 0) {
            setTimeout(timer, that.granularity);
        }
        else {
            diff = 0;
            that.running = false;
        }

        obj = CountDownTimer.parse(diff);
        that.tickFtns.forEach(function (ftn) {
            ftn.call(this, obj.minutes, obj.seconds);
        }, that);
    }());
};

CountDownTimer.prototype.onTick = function (ftn) {
    if (typeof ftn === 'function') {
        this.tickFtns.push(ftn);
    }
    return this;
};

CountDownTimer.prototype.expired = function () {
    return !this.running;
};

CountDownTimer.parse = function (seconds) {
    return {
        'minutes': (seconds / 60) | 0,
        'seconds': (seconds % 60) | 0
    };
};

/*--------Jquery--------*/

$(document).ready(function () {

    /*--------Validation-------*/

    $("input[type='radio']").change(function () {
        $("button[type='submit']").prop("disabled", false);
    });

    /*--------Timer set up-------*/

    var display = document.querySelector('#test-time > h2'),
        testTimer = new CountDownTimer(testTime);

    testTimer.onTick(format(display)).start();

    function format(display) {
        return function (minutes, seconds) {
            minutes = minutes < 10 ? "0" + minutes : minutes;
            seconds = seconds < 10 ? "0" + seconds : seconds;
            display.textContent = minutes + ':' + seconds;
        };
    }

    /*--------Time loader animation-------*/

    var stepAnimationTime = testTime / 4;

    $(".timer-loader-right").css("animation", stepAnimationTime + "s timer-loader-height-animation-top-to-bottom linear forwards");

    $(".timer-loader-bottom").css("animation", stepAnimationTime + "s timer-loader-width-animation-right-to-left linear forwards");
    $(".timer-loader-bottom").css("animation-delay", stepAnimationTime + "s");

    $(".timer-loader-left").css("animation", stepAnimationTime + "s timer-loader-height-animation-bottom-to-top linear forwards");
    $(".timer-loader-left").css("animation-delay", stepAnimationTime * 2 + "s");

    $(".timer-loader-top").css("animation", stepAnimationTime + "s timer-loader-width-animation-left-to-right linear forwards");
    $(".timer-loader-top").css("animation-delay", stepAnimationTime * 3 + "s");

    /*--------Time loader animation-------*/

    var questions = 10,
        questionAnswered = 5;

    $("#test-progress").css("transform", "translate3d(-" + (100 / questions) * (questions - questionAnswered) + "%,0,0)");

});