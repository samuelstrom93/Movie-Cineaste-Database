let imdbID = $("#ImdbID").val();

const moreBtn = document.querySelector('.read-more');
moreBtn.addEventListener('click', readMore);

function readMore() {
    let dots = document.querySelector("#dots"); 
    let moreText = document.querySelector('#more');
    let btnText = document.querySelector('.read-more');

    if (dots.style.display === "none") {
        dots.style.display = "inline";
        btnText.textContent = "Read more"; 
        moreText.style.display = "none";
    } else {
        dots.style.display = "none";
        btnText.textContent = "Read less"; 
        moreText.style.display = "inline";
    }
}




// kunna klicka en gång på knappen och sen disabled
// Funkar

//const btn = document.querySelector('.test-btn');
//form.addEventListener('click', (event) => event.preventDefault(form))




const dislikeBtn = document.querySelector('.dislike-btn')
dislikeBtn.addEventListener('click', dislike);

// Lägg till så att dislike skickar med parameter och en event.PreventDefault-knappen

function dislike() {
    let url = new URL('/api/movie/' + imdbID + '/dislike', 'https://localhost:5001');
    $.ajax({
        url: url,
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            console.log(response);
            document.querySelector('#dislikes').textContent = response.numberOfDislikes;
        },
        error: function (response) { console.log(response) }
    });
}



// event.preventDefault

const likeBtn = document.querySelector('.like-btn')
likeBtn.addEventListener('click', like);

function like() {
    let url = new URL('/api/movie/' + imdbID + '/like', 'https://localhost:5001');
    $.ajax({
        url: url,
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            console.log(response);
            document.querySelector('#likes').textContent = response.numberOfLikes;
        },
        error: function (response) { console.log(response) }
    });
}

