function addCommas(nStr) {
    nStr += '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(nStr)) {
        nStr = nStr.replace(rgx, '$1' + '.' + '$2');
    }
    return nStr;
}

function loadImageError(elem) {
    //console.log("image error");
    $(elem).attr("src", "/images/product-not-available-96x96.png");
}

function loadImageErrorCp(elem) {
    //console.log("image error");
    $(elem).attr("src", "/images/no-image-available.png");
}