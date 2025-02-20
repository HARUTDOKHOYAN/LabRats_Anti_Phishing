

console.log(window.location.hostname);
if (window.location.hostname == 'www.phishtank.com'){
    [...document.getElementsByClassName('data')[0].getElementsByTagName('tr')].map(tr => tr.children[1]).forEach(td => {
        let a = document.createElement('a');
        a.textContent= td.textContent;
        a.href = td.textContent;
        td.textContent = '';
        td.appendChild(a);
    });
}

updateLinks(onLinkHovered);
setupPopup();

let iii = 0;

async  function GetLinkDangerousResult(link){
    let id = localStorage.getItem(link);
    console.log(id);
    if(!id){
        id = await ScanURLRequest(link);
        console.log(id);
        localStorage.setItem(link, id);
    }
    return  await GetScanResult(id);
}

async function onLinkHovered(link)
{
    console.log(">>>>>>>>>>>>>>>>>");
    let result = await GetLinkDangerousResult(link);
    console.log(result);

    if(!result["isLinkActive"])
        return  { score: 0};

    let score = result["dangerousity"] * 100;
    let dangerType = score > 60 ? "danger" : "success";

    return { score: score, dangerType: dangerType };
}