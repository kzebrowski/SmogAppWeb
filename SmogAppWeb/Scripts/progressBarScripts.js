$(document).ready(function () {

    circles.forEach(function (item, key, mapObj) {
        var name = "#" + key.toString();
        setCircleBar($(name + ' #svg #bar'), $(name + ' #svg #text') ,item);
    });
});

function setCircleBar(circle, circleText, value) {
    if (value == -1) {
        circleText.html("B/D");
    }
    else {
        var percent = (value / 300) * 100;
        var iterateMore = true;

        var color
        ColorMap.forEach(function (item, key, mapObj) {
            if (value < key && iterateMore) {
                iterateMore = false;
                color = item;
            }
        });;
        if (iterateMore) color = '#3E2723';

        var r = circle.attr('r');
        var c = Math.PI * (r * 2);

        circleText.html(Math.floor(percent) + "%");

        if (percent < 0) { percent = 0; }
        if (percent > 100) { percent = 100; }

        var pct = ((100 - percent) / 100) * c;

        circleText.css({ fill: color });
        circle.css({ stroke: color });
        circle.css({ strokeDashoffset: pct });
    }
}