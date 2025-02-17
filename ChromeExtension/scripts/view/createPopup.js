function createPopup(parent, score) {
    let popup = el('div', 'labrats-popup');
    a(parent, popup);

    a(popup, getTopDiv(score));
    a(popup, getBottomDiv());
}

function getTopDiv(score) {
    let topDiv = el('div', 'top-div');
    a(topDiv, getProgressRing(score));

    let rightDiv = el('div', 'right-div');
    a(topDiv, rightDiv);

    let title = el('h2', 'lab-rats-h2');
    a(title, document.createTextNode("High Risk Detected"));
    let descr = el('p', 'lab-rats-p');
    a(descr, document.createTextNode("This link is dangerous and may contain security threats."));

    a(rightDiv, title);
    a(rightDiv, descr);

    return topDiv;
}

function getBottomDiv() {
    let bottomDiv = el('div', 'bottom-div');
    let text = el('p');
    a(text, document.createTextNode("For your safety, access is blocked."));

    a(bottomDiv, text);

    return bottomDiv;
}

function getProgressRing(score) {
    let circle = el('div', 'progress-circle');

    drawProgressCircle(circle, 100, '#E5737340', 100, 10, false);
    drawProgressCircle(circle, score, '#E57373', 100, 10, true);
    return circle;
}

const a = (parent, el) => {
    parent.appendChild(el);
}

const el = (el, className) => {
    let element = document.createElement(el);
    if (className)
        element.className = className;
    return element;
}