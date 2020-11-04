document.getElementById("searchString").style.visibility = "hidden";
document.getElementById("currentPage").style.visibility = "hidden";
document.getElementById("firstPage").style.visibility = "hidden";
document.getElementById("totalPages").style.visibility = "hidden";

const btnPrevious = document.querySelector('.btnPrevious');
const btnNext = document.querySelector('.btnNext');


var firstPage = document.getElementById("firstPage").value;
var currentPage = document.getElementById("currentPage").value;
var totalPages = document.getElementById("totalPages").value;
if (currentPage==totalPages) {
    btnNext.disabled = true;
}
if (currentPage == firstPage) {
    btnPrevious.disabled = true;
}