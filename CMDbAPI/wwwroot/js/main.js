//TODO: Ta bort "submit-knappen på Home-index för att istället använda denna metod
function onChange(val) {
    window.location="/home/index"+val
}




const moreButtons = document.querySelectorAll('.read-more');


for (let j = 0; j < moreButtons.length; j++) {
    let button = moreButtons[j];

    let dots = document.querySelectorAll('#dots');
    let moreTexts = document.querySelectorAll('#more');
    let btnTexts = document.querySelectorAll(".read-more");

    button.addEventListener('click', function (){
        if (dots[j].style.display == "none") {
            dots[j].style.display = "inline";
            btnTexts[j].innerHTML = "Read more &darr;";
            moreTexts[j].style.display = "none";
        } else {
            dots[j].style.display = "none";
            btnTexts[j].innerHTML = "Read less &uarr;";
            moreTexts[j].style.display = "inline";
        }
    });
}


