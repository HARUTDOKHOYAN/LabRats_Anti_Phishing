setupPopup();

updateLinks(onLinkHovered);

let iii = 0;

function onLinkHovered(link) {
    iii++;

    if (iii % 2 == 1)
        return { score: 0, dangerType: 'danger' };
    else
        return { score: 40, dangerType: 'warning' };
}