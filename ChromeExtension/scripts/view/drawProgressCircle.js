var drawProgressCircle = function (el, percent, color, size, lineWidth, withLabel) {

    var canvas = document.createElement('canvas');
    if (typeof (G_vmlCanvasManager) !== 'undefined') {
        G_vmlCanvasManager.initElement(canvas);
    }

    var ctx = canvas.getContext('2d');
    canvas.width = canvas.height = size;

    if (withLabel) {
        var span = document.createElement('span');
        span.textContent = percent;
        el.appendChild(span);
    }
    el.appendChild(canvas);

    ctx.translate(size / 2, size / 2); // change center
    ctx.rotate(-90 * Math.PI / 180); // rotate -90 deg

    var radius = (size - lineWidth) / 2;

    percent = Math.min(Math.max(0, (percent / 100) || 1), 1);
    ctx.beginPath();
    ctx.arc(0, 0, radius, 0, Math.PI * 2 * percent, false);
    ctx.strokeStyle = color;
    ctx.lineCap = 'round'; // butt, round or square
    ctx.lineWidth = lineWidth
    ctx.stroke();
};