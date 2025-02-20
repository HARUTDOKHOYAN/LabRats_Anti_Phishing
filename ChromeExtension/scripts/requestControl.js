    async function ScanURLRequest(link)
        {
        const myHeaders = new Headers();
        myHeaders.append("accept", "text/plain; ver=1.0");
        myHeaders.append("Content-Type", "application/json; ver=1.0");

        const raw = "{\n  \"url\": \"" + link + "\"\n}";
        console.log(raw);

        const requestOptions = {
            method: "POST",
            headers: myHeaders,
            body: raw,
            mode: "cors",
            redirect: "follow"
        };
            try {
                const response = await fetch("http://localhost:5234/api/PhishingDetector", requestOptions);
                return await response.text();
            } catch (error) {
                console.error("Error fetching data:", error);
                return null;
            }
    }
    async function GetScanResult(id) {
        const myHeaders = new Headers();
        myHeaders.append("accept", "text/plain; ver=1.0");

        const requestOptions = {
            method: "GET",
            mode: "cors",
            headers: myHeaders,
            redirect: "follow"
        };

        try{
            const response = await fetch("http://localhost:5234/api/PhishingDetector/Get/" + id, requestOptions);
            return await response.json();
        }
        catch (error) {
            console.error("Error fetching data:", error);
            return null;
        }
    }