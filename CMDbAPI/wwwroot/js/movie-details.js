let imdbID = $("#ImdbID").val();

const moreBtn = document.querySelector('.read-more');
moreBtn.addEventListener('click', readMore);

const likeBtn = document.querySelector('.like-btn')
likeBtn.addEventListener('click', like);
const dislikeBtn = document.querySelector('.dislike-btn')
dislikeBtn.addEventListener('click', dislike);








function like(){
    let url = new URL('/api/movie/' + imdbID + '/like', 'https://localhost:5001');
    $.ajax({
        url: url,
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            console.log(response);
            document.querySelector('#likes').textContent = response.numberOfLikes;
            likeBtn.disabled = true;
            dislikeBtn.disabled = true;
            likeBtn.style.opacity = "0.7";
            dislikeBtn.style.opacity = "0.7";
        },
        error: function (response) { console.log(response) }
    })
}


function dislike() {
    let url = new URL('/api/movie/' + imdbID + '/dislike', 'https://localhost:5001');
    $.ajax({
        url: url,
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            console.log(response);
            document.querySelector('#dislikes').textContent = response.numberOfDislikes;
            likeBtn.disabled = true;
            dislikeBtn.disabled = true;
            likeBtn.style.opacity = "0.3";
            dislikeBtn.style.opacity = "0.3";
        },
        error: function (response) { console.log(response) }
    });
}


function readMore() {
    let dots = document.querySelector("#dots");
    let moreText = document.querySelector('#more');
    let btnText = document.querySelector('.read-more');

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