//document.querySelector('.read-more').textContent = 'Klicka'
//document.querySelector('.center').textContent = 'Hej'



const moreBtn = document.querySelector('.read-more');
moreBtn.addEventListener('click', readMore);





// Behöver kanske ändar till getelementbyid
function readMore() {
    let dots = document.getElementById("dots");
    let moreText = document.querySelector('#more');
    let btnText = document.querySelector('.read-more');

    if (dots.style.display === "none") {
        dots.style.display = "inline";
        btnText.innerHTML = "Read more";
        moreText.style.display = "none";
    } else {
        dots.style.display = "none";
        btnText.innerHTML = "Read less";
        moreText.style.display = "inline";
    }



    //if (moreText.style.display === "none") {
    //    btnText.innerHTML = "Read less"; // textContent?
    //    moreText.style.display = "inline";
    //}
    //else {
    //    btnText.innerHTML = "Read More";
    //    moreText.style.display = "none";
    //}
}

