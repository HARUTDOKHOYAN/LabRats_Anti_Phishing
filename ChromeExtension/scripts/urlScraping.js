const updateLinks = (onLinkHovered) => {
    [...document.querySelectorAll("a")].filter(isExternalUrl).forEach(element => {
        blockLink(element);

        element.addEventListener("focus", e => onEnter(e, onLinkHovered));
        element.addEventListener("mouseenter", e => onEnter(e, onLinkHovered));

        element.addEventListener("focusout", onLeave);
        element.addEventListener("mouseleave", onLeave);
    });
};

async function onEnter(e, onLinkHovered) {
    if (popupInfo.isInProcess)
        return;
    const element = e.target;
    const link = element.getAttribute('href-back');

    changeCursor(element, 'progress');

    let model = { score: 0, dangerType: 'none' };

    const rect = element.getBoundingClientRect();
    let x = rect.left + window.scrollX;
    x = x + 400 > window.innerWidth ? x - (400 - (window.innerWidth - x)) : x;
    const y = rect.top + window.scrollY + rect.height;

    try {
        popupInfo.isInProcess = true;
        addTooltip('Checking safety of this link...', x, y);
        let res = await onLinkHovered(link);
        if (res != null)
            model = { ...model, ...res };
    } finally {
        removeTooltip();
        changeCursor(element, '');
        popupInfo.isInProcess = false;
    }

    if (model.score === 0) {
        unblockLink(element);
        return;
    }

    element.style['border-bottom'] = `solid 2px ${getColors(model.dangerType).main}`;

    if (popupInfo.timeout)
        clearTimeout(popupInfo.timeout);

    const wrapper = getPopupWrapper();
    wrapper.replaceChildren();

    wrapper.style.display = 'block';
    wrapper.style.left = x + 'px';
    wrapper.style.top = y + 'px';

    a(wrapper, createPopupContent(model.dangerType, model.score, link));
}

function onLeave() {
    const wrapper = getPopupWrapper();
    popupInfo.timeout = setTimeout(() => {
        wrapper.style.display = 'none';
    }, popupInfo.closeTimeMs);
}

function addTooltip(text, x, y) {
    const tooltip = document.getElementsByClassName('labrats-tooltip')[0];
    tooltip.textContent = text;
    tooltip.style.display = 'block';
    tooltip.style.left = x + 'px';
    tooltip.style.top = y + 'px';
}

function removeTooltip() {
    const tooltip = document.getElementsByClassName('labrats-tooltip')[0];
    tooltip.textContent = '';
    tooltip.style.display = 'none';
}

function blockLink(element) {
    element.setAttribute("href-back", element.href);
    element.href = "javascript:void(0)";
}

function unblockLink(element) {
    element.href = element.getAttribute("href-back");
}

function getPopupWrapper() {
    return document.getElementsByClassName('lab-rats-popup-wrapper')[0];
}

const isExternalUrl = (a) =>
    a.href.startsWith('http') &&
    URL.canParse(a.href) &&
    new URL(a.href).origin !== location.origin;

function changeCursor(element, cursor) {
    element.style.cursor = cursor;
}
