let links = [...document.getElementsByTagName('a')];
console.log(links);

processLinks(links);

let popupWrapper = el('div', 'lab-rats-popup-wrapper');
popupWrapper.style.visibility = 'hidden';
a(document.body, popupWrapper);

popupWrapper.addEventListener('mouseenter', () => {
    clearTimeout(popupInfo.timeout);
});

popupWrapper.addEventListener('mouseleave', () => {
    popupInfo.timeout = setTimeout(() => {
        popupWrapper.style.visibility = 'hidden';
    }, popupInfo.closeTimeMs);
});
