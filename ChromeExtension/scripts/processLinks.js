let timeout;

function processLinks(links) {
    links.forEach((link, i) => processLink(link, i));
}

function processLink(a, score) {
    a.addEventListener('mouseenter', () => onMouseEnter(a, score));
    a.addEventListener('mouseleave', onMouseLeave);
}

function onMouseEnter(a, score) {
    if (timeout)
        clearTimeout(timeout);

    let wrapper = document.getElementsByClassName('lab-rats-popup-wrapper')[0];
    wrapper.replaceChildren();

    wrapper.style.visibility = 'visible';

    const rect = a.getBoundingClientRect();
    const x = rect.left + window.scrollX;
    const y = rect.top + window.scrollY + 40;
    wrapper.style.left = `${x}px`;
    wrapper.style.top = `${y}px`;

    createPopup(wrapper, score);
    //scan link
    //add popup element to the link
    //on element load add circle start
}

function onMouseLeave(e) {
    let wrapper = document.getElementsByClassName('lab-rats-popup-wrapper')[0];
    timeout = setTimeout(() => {
        wrapper.style.visibility = 'hidden';
    }, 200);
}
