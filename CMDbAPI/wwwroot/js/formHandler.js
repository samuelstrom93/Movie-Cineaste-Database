function onChange(val) {
    window.location = "/Home/Filter?selectedCount=" + val;
}

function onChangeOrder(val) {
    window.location = "/Home/Filter?selectedSortOrder=" + val;
}