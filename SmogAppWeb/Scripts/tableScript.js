$(document).ready(function () {
    var i = 1;
    for (color of ColorMap.values()) {
        $("#legendTable #row" + i).css('background-color', color);
        i++;
    }
});