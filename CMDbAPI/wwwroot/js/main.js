//window.onload = function () {
//    var valueCount = document.getElementById("count").value;
//    document.getElementById(valueCount).selected = true;

//    var valueSortOrder = document.getElementById("sortOrder").value;
//    document.getElementById(valueSortOrder).selected = true;

//    var valueType = document.getElementById("type").value;
//    document.getElementById(valueType).selected = true;
//}       




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



let pies = document.querySelectorAll('.pie');
let ratingsSites = document.querySelectorAll('.rating-source');
let ratingElement = document.querySelectorAll('.score-text');




// Färglägger paj-progressbars

for (let i = 0; i < pies.length; i++) {
    let element;
    let value;
    switch (ratingsSites[i].textContent) {
        case "Internet Movie Database":
            value = ratingElement[i].textContent.substring(0, 1) + ratingElement[i].textContent.substring(2, 3)
            pies[i].style.background = `conic-gradient(darkgreen ${value}%, red 0%)`;
            break;
        case "Rotten Tomatoes":
            if (ratingElement[i].textContent.substring(0, 3) === '100') {
                pies[i].style.background = `conic-gradient(darkgreen 100%, red 0%)`;
            }
            else {
                value = ratingElement[i].textContent.substring(0, 2)
                pies[i].style.background = `conic-gradient(darkgreen ${value}%, red 0%)`;
            }
            break;
        case "Metacritic":
            if (ratingElement[i].textContent.substring(0, 3) === '100') {
                pies[i].style.background = `conic-gradient(darkgreen 100%, red 0%)`;
            }
            else {
                value = ratingElement[i].textContent.substring(0, 2)
                pies[i].style.background = `conic-gradient(darkgreen ${value}%, red 0%)`;
            }
            break;
        default:
            console.log("Ingen rating alls")
    }
}
