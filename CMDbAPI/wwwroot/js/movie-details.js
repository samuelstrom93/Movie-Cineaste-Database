let imdbID = $("#ImdbID").val();

const likeBtn = document.querySelector('.like-btn')
likeBtn.addEventListener('click', like);
const dislikeBtn = document.querySelector('.dislike-btn')
dislikeBtn.addEventListener('click', dislike);


// Definieras i main.js istället
//let pies = document.querySelectorAll('.pie');
//let ratingsSites = document.querySelectorAll('.rating-source');
//let ratingElement = document.querySelectorAll('.score-text');




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





function like() {
    let url = new URL('/api/movie/' + imdbID + '/like', window.location.origin);
    $.ajax({
        url: url,
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            console.log(response);
            document.querySelector('#likes').textContent = response.numberOfLikes;
            likeBtn.disabled = true;
            dislikeBtn.disabled = true;
            likeBtn.style.opacity = "0.3";
            dislikeBtn.style.opacity = "0.3";
        },
        error: function (response) { console.log(response) }
    })
}


function dislike() {

    let url = new URL('/api/movie/' + imdbID + '/dislike', window.location.origin);
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
