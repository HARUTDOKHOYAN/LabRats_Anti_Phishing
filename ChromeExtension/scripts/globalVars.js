const popupInfo = {
    timeout: null,
    closeTimeMs: 400
}

function getColors(level) {
    if (level == 'danger') {
        return {
            main: "#E57373",
            secondary: "#E5737340"
        };
    }
    else if (level == 'warning'){
        return {
            main: "#FFB74D",
            secondary: "#FFB74D40"
        };
    }
}