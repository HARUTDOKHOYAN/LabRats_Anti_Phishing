function createPopupContent(level, score) {
    let colors = getColors(level);

    let popup = el('div', 'labrats-popup');

    a(popup, getTopDiv(level, colors, score));
    a(popup, getBottomDiv(level, colors));
    return popup;
}

function getTopDiv(level, colors, score) {
    let topDiv = el('div', 'top-div');
    a(topDiv, getProgressRing(colors, score));

    let rightDiv = el('div', 'right-div');
    a(topDiv, rightDiv);

    let title = el('h2', 'lab-rats-h2');
    title.style.color = colors.main;
    a(title, document.createTextNode(level == 'danger' ? 'High Risk Detected' : 'Potential Risk'));
    let descr = el('p', 'lab-rats-p');
    a(descr, document.createTextNode(level == 'danger' ? 'This link is dangerous and may contain security threats' : 'This link may be unsafe, proceed with caution'));

    a(rightDiv, title);
    a(rightDiv, descr);

    return topDiv;
}

function getBottomDiv(level, colors) {
    let bottomDiv = el('div', 'bottom-div');

    if (level == 'danger') {
        let text = el('p');
        text.style.color = colors.main;
        a(text, document.createTextNode('For your safety, access is blocked.'));

        a(bottomDiv, text);
    } else {
        
    }

    return bottomDiv;
}

function getProgressRing(colors, score) {
    let circle = el('div', 'progress-circle');

    drawProgressCircle(circle, 100, colors.secondary, 100, 10, false);
    drawProgressCircle(circle, score, colors.main, 100, 10, true);
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
