function createPopupContent(level, score, link) {
    const colors = getColors(level);

    const popup = el('div', 'labrats-popup');

    a(popup, getTopDiv(level, colors, score, link));
    a(popup, getBottomDiv(level, colors));
    return popup;
}

function getTopDiv(level, colors, score, link) {
    const topDiv = el('div', 'top-div');
    a(topDiv, getProgressRing(colors, score));

    const rightDiv = el('div', 'right-div');
    a(topDiv, rightDiv);

    const title = el('h2', 'lab-rats-h2');
    title.style.color = colors.main;
    a(title, document.createTextNode(level == 'danger' ? 'High Risk Detected' : 'Potential Risk'));
    a(rightDiv, title);

    const descr = el('p', 'lab-rats-p');
    a(descr, document.createTextNode(level == 'danger' ? 'This link is dangerous and may contain security threats' : 'This link may be unsafe, proceed with caution'));
    a(rightDiv, descr);

    const linkElement = el('a');
    if (level == 'warning') {
        linkElement.style.background = colors.main;
    } else {
        linkElement.style.color = colors.main;
    }
    linkElement.href = link;
    a(linkElement, document.createTextNode('Continue anyway'));
    const linkWrapper = el('div', level == 'warning' ? 'warning-link-wrapper' : 'danger-link-wrapper');
    a(linkWrapper, linkElement);
    a(rightDiv, linkWrapper);

    return topDiv;
}

function getBottomDiv(level, colors) {
    const bottomDiv = el('div', 'bottom-div');

    if (level == 'danger') {
        const text = el('p');
        text.style.color = colors.main;
        a(text, document.createTextNode('For your safety, access is blocked.'));

        a(bottomDiv, text);
    }

    return bottomDiv;
}

function getProgressRing(colors, score) {
    const circle = el('div', 'progress-circle');

    drawProgressCircle(circle, 100, colors.secondary, 100, 10, false);
    drawProgressCircle(circle, score, colors.main, 100, 10, true);
    return circle;
}

const a = (parent, el) => {
    parent.appendChild(el);
}

const el = (el, className) => {
    const element = document.createElement(el);
    if (className)
        element.className = className;
    return element;
}
