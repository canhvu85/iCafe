
var shop = shop || {};
//var hdnUserSession = $("#hdnUserSession").data("value");
var UserSession = sessionStorage.getItem('user');

let connection = new signalR.HubConnectionBuilder().withUrl("/signalServer").build()

connection.start()

connection.on("refreshShops", function () {
    showList();
})


shop.openAddShop = function (number) {
    $('#addShop').modal('show');
    $('#addShop')
        .find("input")
        .val('')
        .end();
    form.valid();
    $("#avatar").val("");
    $('#blah').css('opacity', 0);
    //  $('#noimg').css('border', "3px dashed #e3e3e3");
    $('#noimg').css('display', 'flex');
    $("#blah").attr("src", "");
};

//function createCustomer(value) {
//    $.ajax({
//        url: "/api/ShopsApi/",
//        method: "POST",
//        dataType: "json",
//        contentType: "application/json",
//        data: JSON.stringify(value)
//    }).done(function (result) {
//        $('#addEditUser').modal('hide');
//        sendMessage("Đã tạo thành công shop mới !");
//        //alert("Was Created");

//        //showList();
//        var idre = result + "";
//        //len truoc
//        var date = new Date(value.time_open);
//        var m1 = date.getMonth() + 1;
//        // t = t.toISOString().substr(0, 10);
//        var dateClose = new Date(value.time_close);
//        var m2 = dateClose.getMonth() + 1;
//        $("#tbList").prepend(
//            "<tr id=c" + idre + ">" +
//            "<td>" + value.name + "</td>" +
//            "<td>" + value.info + "</td>" +
//            "<td>" + date.getDate() + "/" + m1 + "/" + date.getFullYear() + "</td>" +
//            "<td>" + dateClose.getDate() + "/" + m2 + "/" + dateClose.getFullYear() + "</td>" +
//            "<td>" + value.cityName + "</td>" +
//            //'<td><button onclick="openEdit("ibih");">Edit</button></td>' +
//            "<td><a href = 'javascript:;' class= 'btn btn-block btn-primary btn-flat' onclick = 'user.openEdit(" + '"' + result + '",' + '"' + value.name + '",' +
//            '"' + value.info + '",' +
//            '"' + value.time_open + '",' +
//            '"' + value.time_close + '",' +
//            value.status + ',' +
//            '"' + value.permalink + '",' +
//            value.cityId + ")'><i class='fa fa-edit edit-btn'></i>Sửa</a ></td > " +
//            "<td><a href = 'javascript:;' class='btn btn-block btn-primary btn-flat' onclick='deleteItem(" + '"' + value.id + '",' + '"' + value.isDeleted + '",' +
//            '"' + value.deleted_at + '",' +
//            '"' + value.deleted_by + '",' + ")'><i class='fa fa-trash-alt delete-btn'></i>Xóa</a></td>" +
//            "</tr>"
//        );

//    });
//}

//user.save = function () {
//    var name = $("#name").val();
//    var info = $("#info").val();
//    var time_open_temp = $("#time_open").val();
//    var time_open = new Date(time_open_temp);
//    var time_close_temp = $("#time_close").val();
//    var time_close = new Date(time_close_temp);
//   // var status = 0;
//    var cityName = $("#CityId option:selected").html();
//    //if ($('#status').is(":checked")) {
//     //   status = 1;
//   // }
//    var permalink = toSlug(name);
//    var CityId = parseInt($("#CityId").val());

//    var newData = {
//        "name": name,
//        "info": info,
//        "time_open": time_open,
//        "time_close": time_close,
//        "permalink": permalink,
//       // "status": status,
//        "cityId": CityId,
//        "cityName": cityName
//    }
//    createCustomer(newData);
//}

function createShop(value) {
    axios({
        url: "/api/mobile/ShopsApi/",
        method: "post",
        //contentType: false,
       // processData: false,
       // async:false,      
        data: value,
        headers: {'Content-Type':'multipart/form-data'}
    }).then(function (response) {
        let result = response.data;
        //signalR
        connection.invoke("SendMessage").catch(function (err) {
            return console.error(err.toString());
        });
        //

        let avatar = result.images != null ? JSON.parse(result.images).thumb : "#";

        clearModalCreate();
        $('#addShop').modal('hide');

        sendMessage("Đã tạo thành công shop mới !");

        var idre = result.id + "";
        //len truoc
        var dateOpen = new Date(value.get('time_open'));
        dateOpen = `${dateOpen.getDate()}/${(dateOpen.getMonth() + 1)}/${dateOpen.getFullYear()}`;
        var dateClose = new Date(value.get('time_close'));
        dateClose = `${dateClose.getDate()}/${(dateClose.getMonth() + 1)}/${dateClose.getFullYear()}`;
        $("#tbList").prepend(
            `<tr id='c${idre}'>
            <td>${value.get('name')}</td>
            <td><img style='max-height:120px; max-width:120px;' src='uploads/shops/${idre}/${avatar}' onerror='loadImageError(this)' /></td>
            <td>${value.get('info')}</td>
            <td>${dateOpen}</td>
            <td>${dateClose}</td>
            <td>${value.get('cityName')}</td>
            <td><a href = "javascript:;" onclick = "shop.openEdit(${result.id},'${value.get('name')}','${value.get('info')}','${avatar}','${value.get('time_open')}','${value.get('time_close')}','${value.get('permalink')}',${value.get('CityId')})">           
            <i class='fa fa-edit edit-btn'></i>Sửa</a ></td >
            <td><a href='javascript:;' onclick='inActiveItem(${result.id})' class='btn btn-primary'><i class='fas fa-toggle-on'></i>  Đang hoạt động</a></td>
            </tr>`
        );

    }).catch(function (error) {
        $('#addShop').modal('hide');
        if (error.response.status == 418) {
            Swal.fire({
                icon: 'error',
                title: 'Cảnh báo',
                text: 'Tên shop này đã có, hãy đặt tên khác!',
            });
        }else
            Swal.fire({
                icon: 'error',
                title: 'Cảnh báo',
                text: 'Đã xảy ra lỗi!',
            });
    });
}

$('#avatar').change(function () {
    $('#blah').css('opacity', 1);
    $('#noimg').css('display', 'none');
})

shop.save1 = async function () {
    var name = $("#name").val().trim();
    var info = $("#info").val().trim();
    var time_open_temp = $("#time_open").val();
    try {
        var time_open = new Date(time_open_temp).toISOString();
    }
    catch (err) {

    }
    var time_close_temp = $("#time_close").val();
    try {
        var time_close = new Date(time_close_temp).toISOString();
    }
    catch (err) {

    }
    //var status = 0;
    var cityName = $("#CityId option:selected").html();
    //if ($('#status').is(":checked")) {
    //    status = 1;
    //}
    var permalink = toSlug(name);
    var CityId = parseInt($("#CityId").val());

    var formData = new FormData();
    // append data
    formData.append('name', name);
    formData.append('info', info);
    formData.append('time_open', time_open);
    formData.append('time_close', time_close);
   // formData.append('status', 0);
    formData.append('permalink', permalink);
    formData.append('CityId', CityId);
    formData.append('cityName', cityName);
 
    var files = $("#avatar").get(0).files;
    // Add the uploaded image content to the form data collection
    if (files.length > 0) {
        formData.append('avatarFile', files[0], toSlug(files[0].name.split(".")[0]) + "." + files[0].name.split(".")[1]);
    } else
        formData.append('avatarFile', null);

    await createShop(formData);
}

function clearModalCreate() {
    $("#name").val(null);
    $("#info").val(null);
    $("#blah").attr('src', null);
    $("#avatar").val(null)
    $("#time_open").val(null);
    $("#time_close").val(null);
   // $('#status').attr('checked', false);
   // $("#permalink").val(null);
}

function showList() {
    axios({
        url: "/api/mobile/ShopsApi/",
        method: "GET",
        headers: {
            'Content-Type': 'application/json',
            'Authorization': UserSession.remember_token           
        }
    }).then(function (response) {
        let data = response.data;
        $("#tbList").html("");
        $.each(data, function (index, value) {
            console.log(value.images);
            let avatar = value.images != "" ? JSON.parse(value.images).thumb : "#";
            //len truoc
            var dateOpen = new Date(value.time_open);
            dateOpen = `${dateOpen.getDate()}/${(dateOpen.getMonth() + 1)}/${dateOpen.getFullYear()}`;
            var dateClose = new Date(value.time_close);
            dateClose = `${dateClose.getDate()}/${(dateClose.getMonth() + 1)}/${dateClose.getFullYear()}`;
            console.log(value.isDeleted);
            let btn = "";
            if (!value.isDeleted) {
                btn = `<td><a href='javascript:;' onclick='inActiveItem(${value.id})' class='btn btn-primary'><i class='fas fa-toggle-on'></i>  Đang hoạt động</a></td>`;
               
            } else 
                btn = `<td><a href='javascript:;' onclick='activeItem(${value.id})' class='btn btn-warning'><i class='fas fa-toggle-off'></i>  Không hoạt động</a></td>`;
           
            $("#tbList").prepend(
                `<tr id='c${value.id}'>
                <td>${value.name}</td>
                <td><img style='max-height:120px; max-width:120px;' src='uploads/shops/${value.id}/${avatar}' onerror='loadImageError(this)'/></td>
                <td>${value.info}</td>
                <td>${dateOpen}</td>
                <td>${dateClose}</td>
                <td>${value.cityName}</td>
                <td><a href = "javascript:;" onclick = "shop.openEdit(${value.id},'${value.name}','${value.info}','${avatar}','${value.time_open}','${value.time_close}','${value.permalink}',${value.cityId})">             
                <i class='fa fa-edit edit-btn'></i>Sửa</a ></td >
                ${btn}
                </tr>`
            );
            
        });
    });
}


var idEdit;

shop.openEdit = function (id, name, info, avatar, time_open, time_close, permalink, cityId) {

    formEdit.valid();

    $('#eblah').css('opacity', 0);
    // $('#enoimg').css('border', "3px dashed #e3e3e3");
    $('#enoimg').css('display', 'flex');
    $("#eblah").attr("src", "");

    if (avatar != 'no-image.png') {
        $('#eblah').css('opacity', 1);
        $('#enoimg').css('display', 'none');
    }
    // console.log(time_open);
    idEdit = parseInt(id);
    //getEditInfo(id);
    $("#showEdit").modal("show");
    $("#ename").val(name);
    $("#einfo").val(info);
    $("#eblah").attr('src', `uploads/shops/${id}/${avatar}`);
    $("#eavatar").val(null);
    $("#eavatar").change(function () {
        readURL(this, "#eblah");
    });
    var to = new Date(time_open);
    //console.log(to);
    let mon = to.getMonth() + 1;
    let d = to.getDate();
    let y = to.getFullYear();
    if (mon.toString().length < 2) {
        mon = "0" + mon;
    }
    if (d.toString().length < 2) {
        d = "0" + d;
    }
    let fullday = y + "-" + mon + "-" + d;
    $("#etime_open").val(fullday);

    var t = new Date(time_close);
    // t.setMonth(time_close.getMonth());
    let mon1 = t.getMonth() + 1;
    let d1 = t.getDate();
    let y1 = t.getFullYear();
    if (mon1.toString().length < 2) {
        mon1 = "0" + mon1;
    }
    if (d1.toString().length < 2) {
        d1 = "0" + d1;
    }
    let fullday1 = y1 + "-" + mon1 + "-" + d1;
    $("#etime_close").val(fullday1);

    //if (status == 1) {
    //    $('#estatus').attr('checked', true);
    //} else
    //    $('#estatus').attr('checked', false);
    var permalink1 = toSlug(name);
    $("#epermalink").val(permalink1);
    $("#eCityId").val(cityId);
}


shop.edit = function () {

    var name = $("#ename").val().trim();
    var info = $("#einfo").val().trim();
    var CityId = parseInt($("#eCityId").val());

    var time_open_temp = $("#etime_open").val();
    var time_open = new Date(time_open_temp).toISOString();

    var time_close_temp = $("#etime_close").val();
    var time_close = new Date(time_close_temp).toISOString();

    var permalink1 = toSlug(name);
    var cityName = $("#eCityId option:selected").html();
    //var status = 0;
    //if ($('#estatus').is(":checked")) {
    //    status = 1;
    //}
    var permalink = toSlug(name);

    //var newData = {
    //    "id": idEdit,
    //    "name": name,
    //    "info": info,
    //    "time_open": time_open,
    //    "time_close": time_close,
    //    "permalink": permalink1,
    //    "status": status,
    //    "cityId": CityId,
    //    "cityName": cityName,
    //    "updated_by":"vu"
    //}

    // init form data:
    var formData = new FormData();
    // append data
    formData.append('id', idEdit);
    formData.append('name', name);
    formData.append('info', info);
    formData.append('time_open', time_open);
    formData.append('time_close', time_close);
   // formData.append('status', status);
    formData.append('permalink', permalink);
    formData.append('CityId', CityId);
    formData.append('cityName', cityName);
  
    var files = $("#eavatar").get(0).files;
    if (files.length > 0) {
        formData.append('avatarFile', files[0]);
    }

    editBtn(idEdit, formData);
};

$('#eavatar').change(function () {
    $('#enoimg').css('display', 'none');
    $('#eblah').css('opacity', 1);
})

function editBtn(idEdit, value) {
    axios({
        url: "/api/mobile/ShopsApi/" + idEdit,
        method: "put",
        data: value,
        headers: { 'Content-Type': 'multipart/form-data' }
    }).then((response) => {
        if (response.status == 200) {
            Swal.fire({
                icon: 'error',
                title: 'Cảnh báo',
                text: response.data
            });
        }else
        if (response.status == 201) {
            let result = response.data;
            console.log(result);
            $("#showEdit").modal("hide");
            // sendMessage("Đã cập nhật thành công thông tin Shop !");
            Swal.fire(
                'Thông báo',
                'Thêm thông tin thành công.',
                'success'
            );
            var lastTD = $("#c" + shop_id + " td:last-child").html();
            let avatar = result != null ? result.thumb : "#";
            //len truoc
            idEdit += "";
            var dateOpen = new Date(value.get('time_open'));
            dateOpen = `${dateOpen.getDate()}/${(dateOpen.getMonth() + 1)}/${dateOpen.getFullYear()}`;
            var dateClose = new Date(value.get('time_close'));
            dateClose = `${dateClose.getDate()}/${(dateClose.getMonth() + 1)}/${dateClose.getFullYear()}`;
            $("#c" + idEdit).html(
                `<td>${value.get('name')}</td>
            <td><img style='max-height:120px; max-width:120px;' src='uploads/shops/${idEdit}/${avatar}' onerror='loadImageError(this)' /></td>
            <td>${value.get('info')}</td>
            <td>${dateOpen}</td>
            <td>${dateClose}</td>
            <td>${value.get('cityName')}</td>
            <td><a href = "javascript:;" onclick = "shop.openEdit(${value.get('id')},'${value.get('name')}','${value.get('info')}','${avatar}','${value.get('time_open')}','${value.get('time_close')}','${value.get('permalink')}',${value.get('CityId')})">
            <i class='fa fa-edit edit-btn'></i>Sửa</a ></td >
            <td>${lastTD}</td>`
            );
        }
    }, (error) => {
        $("#showEdit").modal("hide");
        Swal.fire({
            icon: 'error',
            title: 'Cảnh báo',
            text: 'Đã xảy ra lỗi!'
        });
    });
}

var idDel;

function deleteItem(id) {
    idDel = parseInt(id);
    var delete_at = new Date();

    var newData = {
        "id": idDel,
        "isDeleted": true,
        "deleted_by": "vu"

    }
    deleteBtn(idDel, newData);
};

function deleteBtn(shop_id) {
    Swal.fire({
        title: 'Bạn chắc chắn muốn xóa thông tin này?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Vâng, xóa nó đi!',
        cancelButtonText: 'Hủy',
    }).then((result) => {
        if (result.value) {
            axios({
                url: '/api/mobile/ShopsApi/del/' + parseInt(shop_id) + "/?name=vu",
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' }
                // data: JSON.stringify(data),
            }).then(function () {
                    // Do something with the result
                    //showList();
                    $("#c" + shop_id).html("");              
            });
        }
    })
}


function inActiveItem(shop_id) {  
    Swal.fire({
        title: 'Bạn chắc chắn muốn đóng shop này?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Vâng, đóng nó đi!',
        cancelButtonText: 'Hủy',
    }).then((result) => {
        if (result.value) {
            axios({
                url: '/api/mobile/ShopsApi/inactive/' + parseInt(shop_id) + "/?name=" + UserSession.username,
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' }
                // data: JSON.stringify(data),
            }).then(function () {
                $("#c" + shop_id + " td:last-child").html(`<a href='javascript:;' onclick='activeItem(${shop_id})' class='btn btn-warning'><i class='fas fa-toggle-off'></i>  Không hoạt động</a>`);
            });
        }
    })
}

function activeItem(shop_id) {
    Swal.fire({
        title: 'Bạn chắc chắn muốn mở lại shop này?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Vâng, mở nó đi!',
        cancelButtonText: 'Hủy',
    }).then((result) => {
        if (result.value) {
            axios({
                url: '/api/mobile/ShopsApi/active/' + parseInt(shop_id) + "/?name=" + UserSession.username,
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' }
                // data: JSON.stringify(data),
            }).then(function () {
                $("#c" + shop_id + " td:last-child").html(`<a href='javascript:;' onclick='inActiveItem(${shop_id})' class='btn btn-primary'><i class='fas fa-toggle-on'></i>  Đang hoạt động</a>`);
            });
        }
    })
}

function sendMessage(message) {
    $(".alert, .alert-success").show();
    $(".alert, .alert-success").html(message);
    $(".alert, .alert-success").fadeTo(2000, 500).slideUp(500, function () {
        $(".alert, .alert-success").slideUp(2000);
    });
}

function readURL(input, id) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $(id).attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}

$("#avatar").change(function () {
    readURL(this, "#blah");
});

$(document).ready(function () {
    $(".alert, .alert-success").hide();
    showList();
   
    //let connection = new signalR.HubConnectionBuilder().withUrl("/signalServer").build()

    //connection.start();

    //connection.on("refreshShops", function () {
    //    showList();
    //})
});

var form = $("#frmAddShop");
form.submit(function (event) {
    event.preventDefault();
    //jQuery.noConflict();
    if (!$(this).valid()) return false;
    shop.save1();
    $('#closeBtn').click();   
});

form.validate({
    rules: {
        name: {
            required: true,
            normalizer: function (value) {
                return $.trim(value);
            },
            minlength: 3
        },
        time_open: {
            required: true
        },
        time_close: {
            required: true
        }
    },
    messages: {
        name: {
            required: "Vui lòng nhập tên shop",
            minlength: "Phải nhập 3 ký tự trở lên"
        },
        time_open: {
            required: "Vui lòng nhập ngày hoạt động"
        },
        time_close: {
            required: "Vui lòng nhập ngày hết hạn"
        }
    }
});


var formEdit = $("#frmEditShop");
formEdit.submit(function (event) {
    event.preventDefault();
   // jQuery.noConflict();
    if (!$(this).valid()) return false;
    shop.edit();
    $('#ecloseBtn').click();
});

formEdit.validate({
    rules: {
        ename: {
            required: true,
            normalizer: function (value) {
                return $.trim(value);
            },
            minlength: 3
        },
        etime_open: {
            required: true
        },
        etime_close: {
            required: true
        }
    },
    messages: {
        ename: {
            required: "Vui lòng nhập tên shop",
            minlength: "Phải nhập 3 ký tự trở lên"
        },
        etime_open: {
            required: "Vui lòng nhập ngày hoạt động"
        },
        etime_close: {
            required: "Vui lòng nhập ngày hết hạn"
        }
    }
});