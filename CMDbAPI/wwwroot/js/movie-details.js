

const moreBtn = document.querySelector('.read-more');
moreBtn.addEventListener('click', readMore);


// Behöver kanske ändar till getelementbyid
function readMore() {
    let dots = document.getElementById("dots");
    let moreText = document.querySelector('#more');
    let btnText = document.querySelector('.read-more');

    if (dots.style.display === "none") {
        dots.style.display = "inline";
        btnText.innerHTML = "Read more"; // textContent
        moreText.style.display = "none";
    } else {
        dots.style.display = "none";
        btnText.innerHTML = "Read less"; // textContent
        moreText.style.display = "inline";
    }
}


alert('baseUrl');


// Länka till
//<script type="text/javascript">
//    var testProperty = '@Model.TestProperty';
//</script>



// kunna klicka en gång på knappen och sen disabled

// Funkar

//const btn = document.querySelector('.test-btn');
//form.addEventListener('click', (event) => event.preventDefault(form))

//form.addEventListener('click', dislike(btn);


//function dislike(element) {
//    element.preventDefault();
//    let result = $this
//}

//$("form").submit(function (e) {
//    e.preventDefault();
//    let form = $(this).serialize();
//    $.ajax({
//        url: 'https://localhost:5001/api/movie/tt0028815/dislike',
//        method: 'GET',
//        data: form,
//        dataType: 'json',
//        success: function (data) {
//            //success function
//            //return data form server
//            console.log(data)
//        },
//        error: function (data) { //error function
//            console.log(data)
//        }
//    });

//});


//(function (e) {
//    form.addEventListener('click', (event) => event.preventDefault(form));
//    let form = $(this).serialize();
//    $.ajax({
//        url: 'https://localhost:5001/api/movie/tt0028815/dislike',
//        method: 'GET',
//        data: form,
//        dataType: 'json',
//        success: function (data) { console.log(data) }, error: function (data) { console.log(data) }
//    });
//});

const testBtn = document.querySelector('.test-btn')
testBtn.addEventListener('click', dislike);

//function lobbyLeader() {
//    var obj;
//    $.ajax({
//        async: false;
//        data: { "id": 1, "request": "lobbyinfo", "method": "read" },
//        url: 'https://localhost:5001/api/movie/tt0028815/dislike',
//        dataType: 'json',
//        success: function (data) {
//            obj = JSON.parse(data);
//        }
//    });
//    return obj;
//}





function dislike() {
    let data = $(this).serialize();
    $.ajax({
        url: 'https://localhost:5001/api/movie/tt0028815/dislike',
        type: 'GET',
        dataType: 'json',
        data: data,
        success: function (response) { console.log(response.data) },
        error: function (response) { console.log(response.data) }
    });
    return response.data;

    alert('går igenom metoden iaf')
}



//console.log(json.);



//string endpoint = $"{baseUrl}i={imdbId}{accessKey}";

// Kom åt imdbID med javascript. Kan komma åt @Model?











