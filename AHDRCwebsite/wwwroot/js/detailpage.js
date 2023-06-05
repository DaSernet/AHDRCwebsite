var hyperLinkString = "";

window.onload = function matchString() {
    var parentDiv = document.getElementById("AllInformation");
    var divs = parentDiv.getElementsByTagName("div");

    for (var i = 0; i < divs.length; i++) {
        var div = divs[i];
        var string = div.innerText;

        var result = string.match(/[A-Za-z]+-[0-9]+-[0-9]+/g);
        var result2 = string.match(/[A-Za-z]+-[0-9]+/g);
        if (result) {
            console.log(result);
            console.log(result2);
            console.log(result.length);
            result.forEach(function (result) {
                var hyperlink = addHyperLinks(result,result2);
                div.innerHTML = div.innerHTML.replace(result, hyperlink);
                console.log(hyperlink)
            });
        }
    }
}

function addHyperLinks(item,item2) {
    return "<a href='../Artworks?SearchString=" + item2 + "&selectedCategory=ao&selectedCategory=ph&selectedCategory=wh&selectedCategory=bk&selectedCategory=xp&selectedCategory=co&selectedCategory=au'>" + item + "</a>,"
}

function hideObjectInfo() {
    var divName = "objectInfo";
    hideDiv(divName);
}

function hidePhysicalInfo() {
    var divName = "physicalInfo";
    hideDiv(divName);
}
function hideGeographicalInfo() {
    var divName = "geographicalInfo";
    hideDiv(divName);
}
function hideAuthorshipInfo() {
    var divName = "authorshipInfo";
    hideDiv(divName);
}
function hideHistoryInfo() {
    var divName = "historyInfo";
    hideDiv(divName);
}
function hideNotesInfo() {
    var divName = "notesInfo";
    hideDiv(divName);
}
function hideEventsInfo() {
    var divName = "eventsInfo";
    hideDiv(divName);
}
function hideClassificationInfo() {
    var divName = "classificationInfo";
    hideDiv(divName);
}
function hideAuthorityInfo() {
    var divName = "authorityInfo";
    hideDiv(divName);
}

function hideDiv(divName) {
    var x = document.getElementById(divName);
    if (x.style.display === "none") {
        x.style.display = "block";
    } else {
        x.style.display = "none";
    }
}