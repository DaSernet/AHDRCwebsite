var hyperLinkString = "";

window.onload = function matchString() {
    var string = document.getElementById("otherObjects").innerText;
    var result = string.match(/[A-Za-z0-9]+-[0-9]+-[0-9]+/g);
    result.forEach(addHyperLinks)
    document.getElementById("otherObjects").innerHTML = hyperLinkString;
}

function addHyperLinks(item) {
    item = "<a href='../Artworks?SearchString=" + item + "&selectedCategory=ao&selectedCategory=ph&selectedCategory=wh&selectedCategory=bk&selectedCategory=xp&selectedCategory=co&selectedCategory=au'>" + item + "</a>,"
    hyperLinkString = hyperLinkString.concat(item)
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