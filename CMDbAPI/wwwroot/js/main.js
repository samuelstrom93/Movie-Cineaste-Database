//import { type } from "jquery";

////TODO: Ta bort "submit-knappen på Home-index för att istället använda denna metod
//function onChange(val) {
//    window.location="/home/index"+val
//}


window.onload = function () {
    var valueCount = document.getElementById("count").value;
    document.getElementById(valueCount).selected = true;

    var valueSortOrder = document.getElementById("sortOrder").value;
    document.getElementById(valueSortOrder).selected = true;

    var valueType = document.getElementById("type").value;
    document.getElementById(valueType).selected = true;
}       

//function GetSelectedItem() {
//    var listboxes = document.getElementsByClassName("form-submit");
//    // var x = document.getElementByClassName("form-submit").options.length;
//    var optionValues = document.getElementById("count");
//    var valueCount = document.getElementById("count").value;  //Hämtar det nya värdet från VM proppen "SelectCount"
//    var newValue;


//    for (var i = 0; i < optionValues.children.length; i++) {
//        if (optionValues.children[i].firstChild.nodeValue == valueCount) {
//            optionValues.children[i].selected;
//            // newValue = optionValues.children[i].firstChild.nodeValue;
//        }
//    }

//    var optionValues1 = document.getElementById("type");
//    var valueType = document.getElementById("type").value;  //Hämtar det nya värdet från VM proppen "SelectCount"

//    for (var i = 0; i < optionValues1.children.length; i++) {
//        if (optionValues1.children[i].firstChild.nodeValue == valueType) {
//            optionValues1.children[i].selected;
//            // newValue = optionValues.children[i].firstChild.nodeValue;
//        }
//    }

//    var optionValues2 = document.getElementById("sortOrder");
//    var valueSortOrder = document.getElementById("sortOrder").value;  //Hämtar det nya värdet från VM proppen "SelectCount"

//    for (var i = 0; i < optionValues2.children.length; i++) {
//        if (optionValues2.children[i].attributes.value.nodeValue == valueSortOrder) //den här är rätt
//        {
//            document.getElementById(valueSortOrder).selected = true;
//            //optionValues2.children[i].attributes.value.selected
//            // newValue = optionValues.children[i].firstChild.nodeValue;
//        }
//    }

//    //document.getElementById('count').selected = true;
//    //var selectedOptions = selectElement.options.length; 
//    var x = document.getElementsByClassName("form-submit");
//    x[0].submit();
//}
