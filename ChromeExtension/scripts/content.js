
function IsURLExternal(url)
{
    let domainName =  window.location.hostname.split('.')[1]

        if (url.includes("://")) {
            url = url.substring(url.indexOf("://") + 3);
        }

        let slashIndex = url.search(/[\/?#]/);
        if (slashIndex !== -1) {
            url = url.substring(0, slashIndex);
        }
        return !url.includes(domainName);
}
function URLScanPostRequest(url)
{
    const myHeaders = new Headers();
    myHeaders.append("accept", "*/*");
    myHeaders.append("Content-Type", "application/json; ver=1.0");
    myHeaders.append("Access-Control-Allow-Origin", "*");
    myHeaders.append("Access-Control-Allow-Methods", "POST, GET, OPTIONS");

    const raw = "{\n    \"Text\" : \"data\"\n}";

    const requestOptions = {
        method: "POST",
        mode: "cors",
        headers: myHeaders,
        body: raw,
        redirect: "follow"
    };

    fetch("http://localhost:5234/api/PhishingDetector", requestOptions)
        .then((response) => response.text())
        .then((result) => console.log(result))
        .catch((error) => console.error(error));

}

function GetURLResultAfterScanGetRequest(id)
{
    const myHeaders = new Headers();
    myHeaders.append("accept", "text/plain; ver=1.0");

    const requestOptions = {
        method: "GET",
        headers: myHeaders,
        redirect: "follow"
    };

    fetch("http://localhost:5234/api/PhishingDetector/" + id, requestOptions)
        .then((response) => response.text())
        .then((result) => console.log(result))
        .catch((error) => console.error(error));
}

document.addEventListener("mouseover", function(event) {
    if(event.target.href != null && IsURLExternal(event.target.href))
        URLScanPostRequest(event.target.href)
    else console.log("Not external URL");
});




