//TODO: Ta bort "submit-knappen på Home-index för att istället använda denna metod
function onChange(val) {
    window.location="/home/index"+val
}










const moreBtn = document.querySelectorAll('.read-more');
moreBtn.addEventListener('click', readMore);




//moreBtn.forEach.addEventListener('click', readMore);

//moreBtn.forEach.addEventListener('click', readMore);

//moreBtn.forEach(item => {
//    item.addEventListener('click', readMore)
//})



//document.querySelectorAll('.read-more').forEach(item => {
//    item.addEventListener('click', event => {

//    })
//})





//for (let i = 0; i < moreBtn.length; i++) {
//    moreBtn[i].addEventListener('click', readMore(i))
//}

// skicka med index i readmore för att sedan använda på rätt "read-more" knapp







function readMore() {
    //let dots = document.querySelector("#dots");
    //let moreText = document.querySelector('#more');
    //let btnText = document.querySelector('.read-more');


    let dots = document.querySelectorAll("#dots");
    let moreText = document.querySelectorAll("#more");
    let btnText = document.querySelectorAll(".read-more");

    console.log('hej')

    if (dots.style.display === "none") {
        dots.style.display = "inline";
        btnText.innerHTML = "Read more &darr;";
        moreText.style.display = "none";
    } else {
        dots.style.display = "none";
        btnText.innerHTML = "Read less &uarr;";
        moreText.style.display = "inline";
    }
}