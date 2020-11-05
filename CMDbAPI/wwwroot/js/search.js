document.getElementById("searchString").style.visibility = "hidden";
document.getElementById("currentPage").style.visibility = "hidden";
document.getElementById("firstPage").style.visibility = "hidden";
document.getElementById("totalPages").style.visibility = "hidden";
document.getElementById("cinematicType").style.visibility = "hidden";


const btnPrevious = document.querySelector('.btnPrevious');
const btnNext = document.querySelector('.btnNext');

if (currentPage==totalPages) {
    btnNext.disabled = true;
}
if (currentPage == firstPage) {
    btnPrevious.disabled = true;
}

//var searchString = document.getElementById("searchString").value;
//var firstPage = document.getElementById("firstPage").value;
//var currentPage = document.getElementById("currentPage").value;
//var totalPages = document.getElementById("totalPages").value;