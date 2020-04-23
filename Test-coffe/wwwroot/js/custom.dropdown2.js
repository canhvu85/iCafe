document.querySelector('#login .city .custom-select-wrapper').addEventListener('click', function() {
    this.querySelector('#login .city .custom-select').classList.toggle('open');
})

document.querySelector('#register .city .custom-select-wrapper').addEventListener('click', function () {
    this.querySelector('#register .city .custom-select').classList.toggle('open');
})

window.addEventListener('click', function(e) {
    const select = document.querySelector('#login .city .custom-select')
    if (!select.contains(e.target)) {
        select.classList.remove('open');
    }
});

document.querySelector('#login .shop .custom-select-wrapper').addEventListener('click', function() {
    this.querySelector('#login .shop .custom-select').classList.toggle('open');
})

window.addEventListener('click', function (e) {
    const select = document.querySelector('#register .city .custom-select')
    if (!select.contains(e.target)) {
        select.classList.remove('open');
    }
});

document.querySelector('#register .shop .custom-select-wrapper').addEventListener('click', function () {
    this.querySelector('#register .shop .custom-select').classList.toggle('open');
})



window.addEventListener('click', function(e) {
    const select = document.querySelector('#login .shop .custom-select')
    if (!select.contains(e.target)) {
        select.classList.remove('open');
    }
});

window.addEventListener('click', function (e) {
    const select = document.querySelector('#register .shop .custom-select')
    if (!select.contains(e.target)) {
        select.classList.remove('open');
    }
});


//function readTextFile(file, callback) {
//    let rawFile = new XMLHttpRequest();
//    rawFile.overrideMimeType("application/json");
//    rawFile.open("GET", file, true);
//    rawFile.onreadystatechange = function () {
//        if (rawFile.readyState === 4 && rawFile.status == "200") {
//            callback(rawFile.responseText);
//        }
//    }
//    rawFile.send(null);
//}

//$("#city .custom-option").click(function() {
//    $("#shop .custom-select__trigger span").html("Chọn tên quán");
//    $("#shop .custom-options").html("");
//    let cityId = $(this).data('value');
//    readTextFile("json/city.json", function (text) {
//        let data = JSON.parse(text);
//        data = data.filter(function(rs) {
//            return rs.city_id == cityId;
//        });
//        let k = data.length;
//        let str = '';
//        let selected = ' selected';
//        for(let i = 0; i < k; i++) {
//            str += '<span class="custom-option'+selected+'" onclick="changeShop(this,\''+data[i].shop_name+'\');" data-value="'+data[i].shop_id+'">'+data[i].shop_name+'</span>';
//            selected = '';
//        }
//        $("#shop .custom-options").html(str);
//    });
//});

//$("#city .custom-option").click(function () {
//    $("#shop .custom-select__trigger span").html("Chọn tên quán");
//    $("#shop .custom-options").html("");
//    let cityId = $(this).data('value');
//    $.ajax({
//        url: "api/ShopsAPI",
//        method: "GET",
//        dataType: "json",
//        contentType: "application/json",
//        success: function (data) {
//            data = data.filter(function (rs) {
//                return rs.cityId == cityId;
//            });
//            let k = data.length;
//            let str = '';
//            let selected = ' selected';
//            for (let i = 0; i < k; i++) {
//                str += '<span class="custom-option' + selected + '" onclick="changeShop(this,\'' + data[i].name + '\');" data-value="' + data[i].id + '">' + data[i].name + '</span>';
//                selected = '';
//            }
//            $("#shop .custom-options").html(str);
//        }
//    });
//});


function changeShop(elem, name) {
    $("#login .shop .custom-option.selected").removeClass("selected");
    $("#login .shop .custom-select__trigger span").html(name);
    elem.classList.add("selected");
}

function changeShopRegister(elem, name) {
    $("#register .shop .custom-option.selected").removeClass("selected");
    $("#register .shop .custom-select__trigger span").html(name);
    elem.classList.add("selected");
}