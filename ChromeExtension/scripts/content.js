setupPopup();

updateLinks(onLinkHovered);

let iii = 0;

async function onLinkHovered(link) {
    await new Promise(resolve => setTimeout(resolve, 500));
    iii++;

    if (iii % 2 == 1)
        return { score: 80, dangerType: 'danger' };
    else
        return { score: 40, dangerType: 'warning' };
}