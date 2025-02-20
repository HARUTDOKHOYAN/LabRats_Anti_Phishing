
updateLinks(onLinkHovered);
setupPopup();

async function GetLinkDangerousResult(link) {
    let id = localStorage.getItem(link);
    console.log(id);
    if (!id) {
        id = await ScanURLRequest(link);
        console.log(id);
        localStorage.setItem(link, id);
    }
    return await GetScanResult(id);
}

async function onLinkHovered(link) {
    let result = await GetLinkDangerousResult(link);
    console.log(result);

    if (!result["isLinkActive"])
        return { score: 0 };

    let score = result["dangerousity"] * 100;
    let dangerType = score > 60 ? "danger" : "warning";

    return { score: score, dangerType: dangerType };
}