let links = [...document.getElementsByTagName('a')];
console.log(links);

processLinks(links);

let popupWrapper = el('div', 'lab-rats-popup-wrapper');
popupWrapper.style.visibility = 'hidden';
a(document.body, popupWrapper);