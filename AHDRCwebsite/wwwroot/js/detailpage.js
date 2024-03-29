﻿var hyperLinkString = "";

window.onload = function matchString() {
    var parentDiv = document.getElementById("AllInformation");
    if (parentDiv !== null && parentDiv !== undefined) {
        var divs = parentDiv.getElementsByTagName("div");

        for (var i = 0; i < divs.length; i++) {
            var div = divs[i];
            var string = div.innerText;

            var result = string.match(/[A-Za-z]+-[0-9]+-[0-9]+/g);
            var result2 = string.match(/[A-Za-z]+-[0-9]+/g);
            if (result) {



                result.forEach(function (result) {
                    var hyperlink = addHyperLinks(result, result2);
                    div.innerHTML = div.innerHTML.replace(result, hyperlink);
                });
            }
        }
    }
}
$(document).ready(function () {
    $('#custCarousel').on('slid.bs.carousel', function () {
        var activeItem = document.querySelector('.carousel-item.active a');
        var artworkImageId = activeItem.getAttribute('data-artworkimageid');
        var deleteLink = document.getElementById('deleteLink');
        deleteLink.href = "/ArtworkImages/Delete?artworkimageid=" + artworkImageId;
    });
});

function addHyperLinks(item, item2) {
    debugger;
    return "<a href='../Artworks?SearchString=" + item2 + "&selectedCategory=ao&selectedCategory=ph&selectedCategory=wh&selectedCategory=bk&selectedCategory=xp&selectedCategory=co&selectedCategory=au'>" + item + "</a>"
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


// Get the current artwork ID from the URL
var urlParams = new URLSearchParams(window.location.search);
var currentIndex = parseInt(urlParams.get("artworkid"));



if (artworkIds && currentIndex && artworkIds.length > 0) {
    // Find the index of the current artwork ID
    var currentArtworkIndex = currentIndex;

    var foundIndex = -1;

    // Loop through the artworkIds array to find the artworkNumber
    for (var i = 0; i < artworkIds.length; i++) {
        console.log(artworkIds[i]);
        if (artworkIds[i] == currentArtworkIndex) {
            foundIndex = i; // Store the index when found
            console.log(foundIndex);
            break; // Exit the loop since we found the value
        }
    }

    console.log()
    if (foundIndex !== -1 ) {
        let prevArtworkIndex = -1;
        var prevArtworkId = null;
        if (foundIndex != 0) {
            console.log('setting prev info');
            prevArtworkIndex = foundIndex - 1;
            prevArtworkId = artworkIds[prevArtworkIndex];
        }


        let nextArtworkIndex = -1;
        var nextArtworkId = null;
        if (foundIndex != artworkIds.length) {
            nextArtworkIndex = foundIndex + 1;
            nextArtworkId = artworkIds[nextArtworkIndex];
        }

        // Get the next artwork ID
        //var nextArtworkIndex = foundIndex + 1;
        //var nextArtworkId = nextArtworkIndex < artworkIds.length ? artworkIds[nextArtworkIndex - 1] : null;
    }

    
}

document.addEventListener("DOMContentLoaded", function () {
    // Update the href attributes of the previous and next links
    var prevLink = document.getElementById("prevLink");
    var nextLink = document.getElementById("nextLink");
    if (prevLink) {
        if (prevArtworkId !== currentIndex && prevArtworkId !== undefined && prevArtworkId !== null) {
            var prevUrlParams = getURLParams(prevLink.href);
            prevUrlParams.set("artworkid", prevArtworkId);
            prevLink.href = "/Artworks/Details?" + serializeURLParams(prevUrlParams);

        } else {
            prevLink.style.display = "none";


        }
    } else {

    }

    if (nextLink) {
        if (nextArtworkId !== currentIndex && nextArtworkId !== undefined && nextArtworkId !== null) {
            var nextUrlParams = getURLParams(nextLink.href);
            nextUrlParams.set("artworkid", nextArtworkId);
            nextLink.href = "/Artworks/Details?" + serializeURLParams(nextUrlParams);

        } else {
            nextLink.style.display = "none";


        }
    } else {

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

$(document).ready(function () {
    var infoImage = $("#info-image");
    var infoDivOffset = $(".info-div").offset().top;
    var cardOverlay = $(".card-overlay");

    cardOverlay.on("scroll", function () {
        var scrollTop = $(this).scrollTop();
        var distance = infoDivOffset - scrollTop;
        if (distance <= 0) {
            $("div.carousel-item.active > a > img").css({
                position: "fixed",
                top: "5px",
                left: "75px",
                width: "40vw"
            });
            $("#carousel-thumbnails").css({
                position: "fixed",
                top: "-50px",
                left: "75px",
                width: "40px"
            });

        } else {
            $("div.carousel-item.active > a  > img").removeAttr("style");
            $("#carousel-thumbnails").removeAttr("style");
        }
    });
});
