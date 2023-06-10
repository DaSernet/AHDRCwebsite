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
                var hyperlink = addHyperLinks(result, result2);
                div.innerHTML = div.innerHTML.replace(result, hyperlink);
                console.log(hyperlink)
            });
        }
    }
}

function addHyperLinks(item, item2) {
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

// Retrieve the artwork IDs from session storage
var artworkIds = JSON.parse(sessionStorage.getItem("ArtworkIds"));
console.log("Artwork IDs:", artworkIds);

// Get the current artwork ID from the URL
var urlParams = new URLSearchParams(window.location.search);
var currentIndex = parseInt(urlParams.get("artworkid"));
console.log("Current Index:", currentIndex);

if (artworkIds && currentIndex && artworkIds.length > 0) {
    // Find the index of the current artwork ID
    var currentArtworkIndex = currentIndex;
    console.log("Current Artwork Index:", currentArtworkIndex);

    if (currentArtworkIndex !== -1 || currentArtworkIndex !== 0)
    {
        // Get the previous artwork ID
        var prevArtworkIndex = currentArtworkIndex - 1;
        var prevArtworkId = prevArtworkIndex >= 0 ? artworkIds[prevArtworkIndex-1] : null;
        console.log("Previous Artwork ID:", prevArtworkId);

        // Get the next artwork ID
        var nextArtworkIndex = currentArtworkIndex + 1;
        var nextArtworkId = nextArtworkIndex < artworkIds.length ? artworkIds[nextArtworkIndex-1] : null;
        console.log("Next Artwork ID:", nextArtworkId);
    }
}

document.addEventListener("DOMContentLoaded", function () {
    // Update the href attributes of the previous and next links
    var prevLink = document.getElementById("prevLink");
    var nextLink = document.getElementById("nextLink");

    if (prevLink) {
        if (prevArtworkId !== currentIndex && prevArtworkId !== undefined) {
            var prevUrlParams = getURLParams(prevLink.href);
            prevUrlParams.set("artworkid", prevArtworkId);
            prevLink.href = "/Artworks/Details?" + serializeURLParams(prevUrlParams);
            console.log("Previous Link Href:", prevLink.href);
        } else {
            prevLink.style.display = "none";

            console.log("Previous Link Disabled");
        }
    } else {
        console.log("Previous Link Element Not Found");
    }

    if (nextLink) {
        if (nextArtworkId !== currentIndex && nextArtworkId !== undefined) {
            var nextUrlParams = getURLParams(nextLink.href);
            nextUrlParams.set("artworkid", nextArtworkId);
            nextLink.href = "/Artworks/Details?" + serializeURLParams(nextUrlParams);
            console.log("Next Link Href:", nextLink.href);
        } else {
            nextLink.style.display = "none";

            console.log("Next Link Disabled");
        }
    } else {
        console.log("Next Link Element Not Found");
    }
});

function getURLParams(url) {
    var urlParams = new URLSearchParams(url.split("?")[1]);
    return urlParams;
}

function serializeURLParams(urlParams) {
    var serializedParams = Array.from(urlParams.entries())
        .map(function (pair) {
            return encodeURIComponent(pair[0]) + "=" + encodeURIComponent(pair[1]);
        })
        .join("&");
    return serializedParams;
}


