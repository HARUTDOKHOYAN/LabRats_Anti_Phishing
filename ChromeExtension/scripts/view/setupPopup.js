function setupPopup() {
    let popupWrapper = el('div', 'lab-rats-popup-wrapper');
    popupWrapper.style.display = 'none';
    a(document.body, popupWrapper);

    popupWrapper.addEventListener('mouseenter', () => {
        clearTimeout(popupInfo.timeout);
    });

    popupWrapper.addEventListener('mouseleave', () => {
        popupInfo.timeout = setTimeout(() => {
            popupWrapper.style.display = 'none';
        }, popupInfo.closeTimeMs);
    });
}
