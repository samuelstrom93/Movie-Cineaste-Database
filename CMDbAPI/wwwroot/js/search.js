const btnPrevious = document.querySelector('.btnPrevious');
const btnNext = document.querySelector('.btnNext');

var searchString = document.getElementById("searchString").value;
var firstPage = document.getElementById("firstPage").innerHTML;
var currentPage = document.getElementById("currentPage").value;
var totalPages = document.getElementById("totalPages").innerHTML;

if (currentPage==totalPages) {
    btnNext.disabled = true;
}
if (currentPage == firstPage) {
    btnPrevious.disabled = true;
}
