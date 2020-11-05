let firstPage = $("#FirstPage").val();
let totalPages = $("#TotalPages").val();


const btnPrevious = document.querySelector('.btnPrevious');
const btnNext = document.querySelector('.btnNext');

var searchString = document.getElementById("searchString").value;
var currentPage = document.getElementById("currentPage").value;

if (currentPage==totalPages) {
    btnNext.disabled = true;
}
if (currentPage == firstPage) {
    btnPrevious.disabled = true;
}
