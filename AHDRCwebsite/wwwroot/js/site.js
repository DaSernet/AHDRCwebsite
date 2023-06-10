// Check the size of the browser and the .body-container element
function checkSize() {
    const browserWidth = window.innerWidth;
    const browserHeight = window.innerHeight;

    const containerElement = document.querySelector('.body-container');
    const containerWidth = containerElement.offsetWidth;
    const containerHeight = containerElement.offsetHeight;
    const footer = document.querySelector('.footer');

    
    
    
    

    if (parseInt(containerHeight) >= 500)
    {
        console.log("relative")
        footer.style.position = 'relative';
    } else
    {
        console.log("fixed")
        footer.style.position = 'fixed';
        footer.style.bottom = '0';
    }
}

// Run the checks when the window is resized
window.addEventListener('resize', function () {
    checkSize();
});

// Initial checks when the page loads
window.addEventListener('DOMContentLoaded', function () {
    checkSize();
});
