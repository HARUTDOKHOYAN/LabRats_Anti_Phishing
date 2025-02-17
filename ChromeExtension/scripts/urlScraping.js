const updateLinks = () => {
    document.querySelectorAll("a, [data-href]").forEach(element => {
        element.addEventListener("mouseenter", (event) => {
            const href = element.href || element.getAttribute("data-href");

            if (popupInfo.timeout)
                clearTimeout(popupInfo.timeout);
            
            let score = Math.round(Math.random() * 100);

            let wrapper = document.getElementsByClassName('lab-rats-popup-wrapper')[0];
            wrapper.replaceChildren();

            wrapper.style.display = 'block';

            const rect = element.getBoundingClientRect();
            let x = rect.left + window.scrollX;
            x = x + 400 > window.innerWidth ? x - (400 - (window.innerWidth - x)) : x;
            const y = rect.top + window.scrollY + 40;
            wrapper.style.left = `${x}px`;
            wrapper.style.top = `${y}px`;

            a(wrapper, createPopupContent(score > 50 ? 'danger' : 'warning', score));
        });

        element.addEventListener("mouseleave", () => {
            let wrapper = document.getElementsByClassName('lab-rats-popup-wrapper')[0];
            popupInfo.timeout = setTimeout(() => {
                wrapper.style.display = 'none';
            }, popupInfo.closeTimeMs);
        });
    });
};

function getLinkDOMElements() {
    return [...document.getElementsByTagName('a')].filter(a => isUrlExternal(a.href));
}

function isUrlExternal(url) {
    let domainName = window.location.hostname.split('.')[1];

    if (url.includes("://")) {
        url = url.substring(url.indexOf("://") + 3);
    }

    let slashIndex = url.search(/[\/?#]/);
    if (slashIndex !== -1) {
        url = url.substring(0, slashIndex);
    }
    return !url.includes(domainName);
}
