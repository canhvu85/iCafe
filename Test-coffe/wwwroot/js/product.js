

var product = product || {};

//let connection = new signalR.HubConnectionBuilder().withUrl("/signalServer").build()

//connection.start()

//connection.on("refreshEmployees", function () {
//    showList();
//})
var catalogeList = [];
getCataloges();

for (let i = 0; i < catalogeList.length; i++) {
    var z = document.createElement("option");
    z.setAttribute("value", catalogeList[i].id);
    var t = document.createTextNode(catalogeList[i].name);
    z.appendChild(t);
    document.getElementById("FilterCataloge").appendChild(z);
}

for (let i = 0; i < catalogeList.length; i++) {
    var z = document.createElement("option");
    z.setAttribute("value", catalogeList[i].id);
    var t = document.createTextNode(catalogeList[i].name);
    z.appendChild(t);   
    document.getElementById("CatalogeId").appendChild(z);   
}

for (let i = 0; i < catalogeList.length; i++) {
    var z = document.createElement("option");
    z.setAttribute("value", catalogeList[i].id);
    var t = document.createTextNode(catalogeList[i].name);
    z.appendChild(t);
    document.getElementById("eCatalogeId").appendChild(z);
}

function getCataloges() {
    $.ajax({
        url: "api/mobile/CatalogesAPI/?shop_id=1",
        method: "GET",
        dataType: "json",
        async: false,
        contentType: "application/json"
    }).done(function (data) {
        catalogeList = data;
    });
}


product.openAddProduct = function (number) {
    $('#addProduct').modal('show');
    $('#addProduct')
        .find("input")
        .val('')
        .end();
        //.find("input[type=checkbox], input[type=radio]")
        //.prop("checked", "")
    //.end();
    form.valid();
    $("#avatar").val("");
   // $("#blah").attr("src", "/no-img.jpg");
    $('#blah').css('opacity', 0);
  //  $('#noimg').css('border', "3px dashed #e3e3e3");
    $('#noimg').css('display', 'flex');
    $("#blah").attr("src","");
    
    console.log($("#FilterCataloge").val());
    $('#tbl2_wrapper .row:eq(0), #tbl2_wrapper .row:eq(2)').hide();
    if ($("#FilterCataloge").val() != "") {
        $("#CatalogeId").val(parseInt($("#FilterCataloge").val()));
    };
};


function createCustomer1(value) {
    $.ajax({
        url: "/api/mobile/ProductsApi/",
        method: "POST",
        contentType: false,
        processData: false,
        async: false,
        data: value
    }).fail(function () {
        $('#addEditUser').modal('hide');
        Swal.fire({
            icon: 'error',
            title: 'Cảnh báo',
            text: 'Đã xảy ra lỗi!',
        });
    }).done(function (result) {
        //connection.invoke("SendMessage").catch(function (err) {
        //    return console.error(err.toString());
        //});
       // clearModalCreate();
        $('#addProduct').modal('hide');
        sendMessage("Đã tạo thành công sản phẩm mới !");

        var idre = result.id + "";
        let avatar = result.images != null ? JSON.parse(result.images).thumb : "#";
 
        $('#tbl2').dataTable().fnAddData([
            idre,
            value.get('name'),
            avatar,
            value.get('catalogeName'),
            value.get('price'),
            value.get('CatalogeId')

        ]);

    });
   
}

$('#avatar').change(function () {
    $('#blah').css('opacity', 1);
    $('#noimg').css('display', 'none');
})

product.save = function () {  
    var name = $("#name").val().trim();
    var price = $("#price").val();
    if (price == "") {
        price = 0;
    };
    var catalogeName = $("#CatalogeId option:selected").html();
    var permalink = toSlug(name);
    var CatalogeId = parseInt($("#CatalogeId").val());
    // init form data:
    var formData = new FormData();
    // append data
    formData.append('name', name);
    formData.append('price', price);
    formData.append('permalink', permalink);
    formData.append('CatalogeId', CatalogeId);
    formData.append('catalogeName', catalogeName);
    // get data
    //formData.get('username'); // Returns "Chris"
    var files = $("#avatar").get(0).files;
    // Add the uploaded image content to the form data collection
    if (files.length > 0) {
        formData.append('avatarFile', files[0], toSlug(files[0].name.split(".")[0]) + "." + files[0].name.split(".")[1]);
    } else
        formData.append('avatarFile', null);
    let data = tableDT
                .rows()
                .data();
   // console.log("dl:"+data.length);
   // console.log("5");
    if (data.length > 0) {
        $('#tbl2_wrapper .row:eq(0), #tbl2_wrapper .row:eq(2)').hide();
        if ($("#FilterCataloge").val() == $("#CatalogeId").val()) {
            $('#tbl2').DataTable().destroy();
            showList($("#FilterCataloge").val());
        } else
            if ($("#FilterCataloge").val() == "") {              
                $('#tbl2').DataTable().destroy();
                showList("");
            } else {              
                $('#tbl2').DataTable().destroy();
                showList($("#CatalogeId").val());
                $("#FilterCataloge").val($("#CatalogeId").val());
            }
    } else {
       // $('#tbl2').DataTable().destroy();
        showList("");
    }

    //$("#FilterCataloge").val("");
    //showList("");
    // console.log(toSlug(files[0].name));
   // if (checkform() == true) {
       createCustomer1(formData);
   // }
    
}

function clearModalCreate() {
    $("#name").val(null);
    $("#price").val(null);
    $("#blah").attr('src', null);
    $("#avatar").val(null)
   // $("#permalink").val(null);
}

var dataset1 = [];
var tableDT;

function showList(cataId) {
    var st = "/api/mobile/ProductsApi/";
    if (cataId != "") {
        st += "?cataId=" + cataId;
    }

    $.ajax({
        url: st,
        method: "GET",
        dataType: "json",
        async: false,
        contentType: "application/json"
    }).done(function (data) {
        //console.log(data);
        var ss = [];

        $.each(data, function (index, value) {
            let avatar = value.images != null ? JSON.parse(value.images).thumb : "#";
            var m = [];
            m.push(value.id);
            m.push(value.name);
            m.push(avatar);
            m.push(value.catalogeName);
            m.push(value.price);
            m.push(value.catalogeId);
            //  m.push("");
            ss.push(m);
            console.log("ss :", ss);
        });
        console.log(ss);
        //alert("abc");
        //ss = data;

        $('#tbl2').DataTable({
            data: ss,
            order: [0, 'des'],
            columnDefs: [
                //{
                //    "targets": 2,
                //    "orderData": 2
                //},
               
                {
                    "targets": [1, 2,3,5,6], // your case first column
                    "className": "text-center"

                },
                {
                    "targets": 4,
                    "className": "text-right",
                }],
            columns: [
                { title: "ID", data: 0 },
                // { data: 0, visible: false, searchable: false, orderable: true, name: 'id' },
                { title: "Tên sản phẩm", data: 1 },
                {
                    title: "Hình ảnh",
                    data: 0,
                   // orderData: 0,
                    // orderable: true,
                    render: function (data, type, row, meta) {

                        return `<img style='max-height:120px; max-width:120px;' src='uploads/products/${data}/${row[2]}' onerror='loadImageError(this)' />`;
                    }
                },
                { title: "Danh mục sản phẩm", data: 3 },
                {
                    title: "Giá",
                    data: 4,
                    render: function (data, type, row, meta) {
                        return addCommas(data) + " đ";
                    }
                },
                {
                    data: 0,
                    render: function (data, type, row, meta) {
                        return `<a href = 'javascript:; ' onclick='product.openEdit("${data}")'><i class='fa fa-edit edit-btn'></i>Sửa</a>`;
                        //return "<a href = 'javascript:; ' class='btn btn- block btn - primary btn - flat'>Edit</a>";
                    }
                },
                {
                    data: 0,
                    render: function (data, type, row, meta) {
                        return `<a href = 'javascript:; ' onclick='deleteItem("${data}")'><i class='fa fa-trash-alt delete-btn'></i>Xóa</a>`;
                    }
                }
            ],
            createdRow: function (row, data, index) {
                // $(row).addClass('blue');   //add class to row

                $(row).attr('id', "c" + data[0]);
                $(row).attr('data-name', data[1]);
                $(row).attr('data-avatar', data[2]);
                $(row).attr('data-price', data[4]);
                $(row).attr('data-catalogeid', data[5]);
                $(row).attr('data-sort', data[0]);
                //$('td', row).eq(2).css('font-weight', 'bold');  //add style to cell in third column
            }
        });

        tableDT = $('#tbl2').DataTable();
        //$('#tbl2').dataTable({
        //    "order": [0, 'asc']
        //});

        //$("#tbList").html("");
        //$.each(data, function (index, value) {
        //    //len truoc
        //    var date = new Date(value.time_open);
        //    var m1 = date.getMonth() + 1;
        //    var dateClose = new Date(value.time_close);
        //    var m2 = dateClose.getMonth() + 1;
        //    $("#tbList").prepend(
        //        "<tr id='c" + value.id + "'>" +
        //        "<td>" + value.name + "</td>" +
        //        "<td>" + "<img style='height:200px; width:200px' src='uploads/product/" + value.id + "/avatar/" + value.avatar + "' />" + "</td>" +
        //        "<td>" + "<img style='height:200px; width:200px' src='uploads/product/" + value.id + "/thumb/" + value.avatar + "' />" + "</td>" +
        //        "<td>" + value.price + "</td>" +                  
        //        "<td>" + value.permalink + "</td>" +
        //        "<td>" + value.catalogeName + "</td>" +
        //        "<td>" + value.isDeleted + "</td>" +
        //        "<td>" + value.deleted_at + "</td>" +
        //        "<td>" + value.deleted_by + "</td>" +
        //        "<td>" + value.created_at + "</td>" +
        //        "<td>" + value.created_by + "</td>" +
        //        "<td>" + value.updated_at + "</td>" +
        //        "<td>" + value.updated_by + "</td>" +
        //        //'<td><button onclick="openEdit("ibih");">Edit</button></td>' +
        //        "<td><a href = 'javascript:;' class= 'btn btn-block btn-primary btn-flat' onclick = 'user.openEdit(" + '"' + value.id + '",' + '"' + value.name + '",' +
        //                                                                                                                '"' + value.price + '",' +
        //                                                                                                                '"' + value.avatar + '",' +
        //                                                                                                                '"' + value.permalink + '",' +
        //                                                                                                                value.catalogeId + ")'> Edit</a ></td > " +
        //        "<td><a href = 'javascript:;' class='btn btn-block btn-primary btn-flat' onclick='deleteItem(" + '"' + value.id + '",' + '"' + value.isDeleted + '",' +
        //                                                                                                            '"' + value.deleted_at + '",' +
        //                                                                                                            '"' + value.deleted_by + '",' + ")'>Delete</a></td>" +
        //        "</tr>"
        //    );
        //});
    });  
}


//onclick = 'user.openEdit(" + "ffghj" + ")'

var idEdit;

product.openEdit = function (id) {
    // console.log(time_open);
    formEdit.valid();
    $('#eblah').css('opacity', 0);
   // $('#enoimg').css('border', "3px dashed #e3e3e3");
    $('#enoimg').css('display', 'flex');
    $("#eblah").attr("src", "");
    idEdit = parseInt(id);
    var itemName = $("#c" + id).data("name");
    var itemPrice = $("#c" + id).data("price");
   // itemPrice = Number(itemPrice.replace(/[^0-9.-]+/g, ""));
    var itemAvatar = $("#c" + id).data("avatar");
    if (itemAvatar != 'no-image.png') {
         $('#eblah').css('opacity', 1);
         $('#enoimg').css('display', 'none');
    }
    var itemCatalogeId = $("#c" + id).data("catalogeid");
   // console.log(id);
   // console.log(itemName);
    //getEditInfo(id);
    $("#showEdit").modal("show");
    $("#ename").val(itemName);
    $("#eprice").val(itemPrice);
    $("#eblah").attr('src', `uploads/products/${id}/${itemAvatar}`);
    $("#eavatar").val(null);
    $("#eavatar").change(function () {
        readURL(this, "#eblah");
    });
   // $('#eblah').css('opacity', 1);
   // $('#enoimg').css('border', "3px dashed #e3e3e3");
   // var permalink1 = toSlug(itemName);
   // $("#epermalink").val(permalink1);
    $("#eCatalogeId").val(parseInt(itemCatalogeId));
}

//user.openEdit = function (id, name, price, avatar, permalink, catalogeId) {
//    // console.log(time_open);
//    idEdit = parseInt(id);
//    //getEditInfo(id);
//    $("#showEdit").modal("show");
//    $("#ename").val(name);
//    $("#eprice").val(price);
//    $("#eblah").attr('src', 'uploads/product/' + id + '/thumb/' + avatar);
//    $("#eavatar").val(null);
//    $("#eavatar").change(function () {
//        readURL(this, "#eblah");
//    });

//    var permalink1 = toSlug(name);
//    $("#epermalink").val(permalink1);
//    $("#eCatalogeId").val(catalogeId);
//}

var n = [];
product.edit = function () {
    n = [];
    var name = $("#ename").val().trim();
    var price = parseInt($("#eprice").val());
    if (price == "") {
        price = 0;
    };
    var CatalogeId = parseInt($("#eCatalogeId").val());
    //var permalink1 = toSlug(name);
    var catalogeName = $("#eCatalogeId option:selected").html();
    var permalink = toSlug(name);

    // init form data:
    var formData = new FormData();
    // append data
    formData.append('id', idEdit);
    formData.append('name', name);
    formData.append('price', price);
    formData.append('permalink', permalink);
    formData.append('CatalogeId', CatalogeId);
    formData.append('catalogeName', catalogeName);
    // get data
    //formData.get('username'); // Returns "Chris"
    var files = $("#eavatar").get(0).files;
    // Add the uploaded image content to the form data collection
    if (files.length > 0) {
        formData.append('avatarFile', files[0]);
    }
   
    n.push(idEdit);
    n.push(name);
    n.push("");
    n.push(catalogeName);
    n.push(price);
    n.push(CatalogeId);

    editBtn(idEdit, formData);
};

$('#eavatar').change(function () {
    $('#enoimg').css('display', 'none');
    $('#eblah').css('opacity', 1);
})

function editBtn(idEdit, value) {
    $.ajax({
        url: "/api/mobile/ProductsApi/" + idEdit,
        method: "PUT",
        contentType: false,
        processData: false,
        //async: false,
        data: value
    }).fail(function () {
        $("#showEdit").modal("hide");
        Swal.fire({
            icon: 'error',
            title: 'Cảnh báo',
            text: 'Đã xảy ra lỗi!',
        });
    }).done(function (result) {
        $("#showEdit").modal("hide");
        // sendMessage("Đã cập nhật thành công thông tin Shop !");
        Swal.fire(
            'Thông báo',
            'Sửa thông tin thành công.',
            'success'
        );
        //len truoc
        idEdit += "";
        //$("#c" + idEdit).html(
        //    // "<tr>" +
        //    "<td>" + value.get('name') + "</td>" +
        //    "<td>" + "<img style='height:200px; width:200px' src='uploads/product/" + idEdit + "/avatar/" + result + "' />" + "</td>" +
        //    "<td>" + "<img style='height:200px; width:200px' src='uploads/product/" + idEdit + "/thumb/" + result + "' />" + "</td>" +
        //    "<td>" + value.get('price') + "</td>" +
        //    "<td>" + value.get('permalink') + "</td>" +
        //    "<td>" + value.get('catalogeName') + "</td>" +
        //    "<td>" + "" + "</td>" +
        //    "<td>" + "" + "</td>" +
        //    "<td>" + "" + "</td>" +
        //    "<td>" + "" + "</td>" +
        //    "<td>" + "" + "</td>" +
        //    "<td>" + "" + "</td>" +
        //    "<td>" + "" + "</td>" +
        //    "<td><a href = 'javascript:;' class= 'btn btn-block btn-primary btn-flat' onclick = 'user.openEdit(" + '"' + value.get('id') + '",' + '"' + value.get('name') + '",' +
        //                                                                                                            '"' + value.get('price') + '",' +
        //                                                                                                            '"' + result + '",' +
        //                                                                                                            '"' + value.get('permalink') + '",' +
        //                                                                                                            value.get('CatalogeId') + ")'> Edit</a ></td > " +
        //    "<td><a href = 'javascript:;' class='btn btn-block btn-primary btn-flat' onclick='deleteItem(" + '"' + value.get('id') + '",' + '"' + value.isDeleted + '",' +
        //                                                                                                        '"' + value.deleted_at + '",' +
        //                                                                                                        '"' + value.deleted_by + '",' + ")'>Delete</a></td>"
        //    // "</tr>"
        //);
        n[2] = JSON.parse(result).thumb;

        //temp[0] = 'Tom';
        var st1 = `<a href = 'javascript:; '  onclick='product.openEdit('${n[0]}')'><i class='fa fa-edit edit-btn'></i>Sửa</a>`;
        var st2 = `<a href = 'javascript:; '  onclick='deleteItem('${n[0]}')'><i class='fa fa-trash-alt delete-btn'></i>Xóa</a>`;
        var img = `<img style='max-height:120px; max-width:120px;' src='uploads/products/${n[0]}/${n[2]}' onerror='loadImageError(this)'/>`;
        // $('#tbl2').dataTable().fnUpdate([n[1], n[2], n[3], n[4], st1, st2], "#c" + idEdit, undefined, false);

        var table = $('#tbl2').DataTable();
        var d = table.row("#c" + idEdit).data();
        d[0] = n[0];
        d[1] = n[1];
        d[2] = n[2];
        d[3] = n[3];
        d[4] = n[0];
        d[5] = n[0];
        table.row("#c" + idEdit).data(d);
      

        $('#tbl2').dataTable().fnUpdate([n[0],n[1], img, n[3], n[4], st1, st2], "#c" + idEdit, undefined, false);

        var row = "#c" + idEdit;
        console.log(n);
        // $(row).attr('id', "c" + data[0]);
        $(row).data('name', n[1]);
        $(row).data('avatar', n[2]);
        $(row).data('price', n[4]);
        $(row).data('catalogeid', n[5]);
        console.log(n);
        n = [];

        //$('#tbl2_wrapper .row:eq(0), #tbl2_wrapper .row:eq(2)').hide();
        if ($("#FilterCataloge").val() != "") {
            $('#tbl2').DataTable().destroy();
            $('#tbl2_wrapper .row:eq(0), #tbl2_wrapper .row:eq(2)').hide();
            showList($("#eCatalogeId").val());
            // $('#tbl2_wrapper .row:eq(0), #tbl2_wrapper .row:eq(2)').hide();
            $("#FilterCataloge").val($("#eCatalogeId").val())
        } else {
            $('#tbl2').DataTable().destroy();
            $('#tbl2_wrapper .row:eq(0), #tbl2_wrapper .row:eq(2)').hide();
            showList("");
            // $('#tbl2_wrapper .row:eq(0), #tbl2_wrapper .row:eq(2)').hide();
        }
        //$('#tbl2').DataTable().ajax.reload();

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

function deleteBtn(product_id, data) {
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
            $.ajax({
                url: '/api/mobile/ProductsApi/del/' + parseInt(product_id) + "/?name=vu",
                type: 'PUT',
                dataType: "json",
                contentType: "application/json",
               // data: JSON.stringify(data),
                success: function () {
                    // Do something with the result
                    //showList();
                    //$("#c" + shop_id).html("");
                    $('#tbl2').dataTable().fnDeleteRow(document.getElementById("c" + product_id));
                }
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

$("#FilterCataloge").change(function () {
    //console.log(this.value);
    $('#tbl2').DataTable().destroy();
    showList(this.value);
});

//function checkform() {
//    if ($("#name").val().length == 0 )
//    {
//        // something is wrong
//        alert('Bạn chưa nhập tên');
//        return false;
//    }
//	else if ($("#price").val()<=0)
//    {
//        // something else is wrong
//        alert('Bạn chưa nhập giá');
//        return false;
//    }
//    // If the script gets this far through all of your fields
//    // without problems, it's ok and you can submit the form

//    return true;
//}

var formatter = new Intl.NumberFormat("ru", {
    style: "currency",
    currency: "VND"
});

var dataSet = [
    ["Tiger Nixon", "System Architect", "Edinburgh", "5421", "2011/04/25", "$320,800"],
    ["Garrett Winters", "Accountant", "Tokyo", "8422", "2011/07/25", "$170,750"],
    ["Ashton Cox", "Junior Technical Author", "San Francisco", "1562", "2009/01/12", "$86,000"],
    ["Cedric Kelly", "Senior Javascript Developer", "Edinburgh", "6224", "2012/03/29", "$433,060"],
    ["Airi Satou", "Accountant", "Tokyo", "5407", "2008/11/28", "$162,700"],
    ["Brielle Williamson", "Integration Specialist", "New York", "4804", "2012/12/02", "$372,000"],
    ["Herrod Chandler", "Sales Assistant", "San Francisco", "9608", "2012/08/06", "$137,500"],
    ["Rhona Davidson", "Integration Specialist", "Tokyo", "6200", "2010/10/14", "$327,900"],
    ["Colleen Hurst", "Javascript Developer", "San Francisco", "2360", "2009/09/15", "$205,500"],
    ["Sonya Frost", "Software Engineer", "Edinburgh", "1667", "2008/12/13", "$103,600"],
    ["Jena Gaines", "Office Manager", "London", "3814", "2008/12/19", "$90,560"],
    ["Quinn Flynn", "Support Lead", "Edinburgh", "9497", "2013/03/03", "$342,000"],
    ["Charde Marshall", "Regional Director", "San Francisco", "6741", "2008/10/16", "$470,600"],
    ["Haley Kennedy", "Senior Marketing Designer", "London", "3597", "2012/12/18", "$313,500"],
    ["Tatyana Fitzpatrick", "Regional Director", "London", "1965", "2010/03/17", "$385,750"],
    ["Michael Silva", "Marketing Designer", "London", "1581", "2012/11/27", "$198,500"],
    ["Paul Byrd", "Chief Financial Officer (CFO)", "New York", "3059", "2010/06/09", "$725,000"],
    ["Gloria Little", "Systems Administrator", "New York", "1721", "2009/04/10", "$237,500"],
    ["Bradley Greer", "Software Engineer", "London", "2558", "2012/10/13", "$132,000"],
    ["Dai Rios", "Personnel Lead", "Edinburgh", "2290", "2012/09/26", "$217,500"],
    ["Jenette Caldwell", "Development Lead", "New York", "1937", "2011/09/03", "$345,000"],
    ["Yuri Berry", "Chief Marketing Officer (CMO)", "New York", "6154", "2009/06/25", "$675,000"],
    ["Caesar Vance", "Pre-Sales Support", "New York", "8330", "2011/12/12", "$106,450"],
    ["Doris Wilder", "Sales Assistant", "Sydney", "3023", "2010/09/20", "$85,600"],
    ["Angelica Ramos", "Chief Executive Officer (CEO)", "London", "5797", "2009/10/09", "$1,200,000"],
    ["Gavin Joyce", "Developer", "Edinburgh", "8822", "2010/12/22", "$92,575"],
    ["Jennifer Chang", "Regional Director", "Singapore", "9239", "2010/11/14", "$357,650"],
    ["Brenden Wagner", "Software Engineer", "San Francisco", "1314", "2011/06/07", "$206,850"],
    ["Fiona Green", "Chief Operating Officer (COO)", "San Francisco", "2947", "2010/03/11", "$850,000"],
    ["Shou Itou", "Regional Marketing", "Tokyo", "8899", "2011/08/14", "$163,000"],
    ["Michelle House", "Integration Specialist", "Sydney", "2769", "2011/06/02", "$95,400"],
    ["Suki Burks", "Developer", "London", "6832", "2009/10/22", "$114,500"],
    ["Prescott Bartlett", "Technical Author", "London", "3606", "2011/05/07", "$145,000"],
    ["Gavin Cortez", "Team Leader", "San Francisco", "2860", "2008/10/26", "$235,500"],
    ["Martena Mccray", "Post-Sales support", "Edinburgh", "8240", "2011/03/09", "$324,050"],
    ["Unity Butler", "Marketing Designer", "San Francisco", "5384", "2009/12/09", "$85,675"]
];

var form = $("#frmAddProduct");
//form.validate();
form.submit(function (event) {
    event.preventDefault();
    jQuery.noConflict();
    if (!$(this).valid()) return false;
    product.save();
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
        price: {
           // required: true,
            number: true
        }
    },
    messages: {
        name: {
            required: "Vui lòng nhập tên sản phẩm",
            minlength: "Phải nhập 3 ký tự trở lên"
        },
        price: {
           // required: "Vui lòng nhập giá sản phẩm",
            number:"Vui lòng nhập số"
        }
    }
});


var formEdit = $("#frmEditProduct");
formEdit.submit(function (event) {
    event.preventDefault();
    jQuery.noConflict();
    if (!$(this).valid()) return false;
    product.edit();
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
        eprice: {
           // required: true,
            number: true
        }
    },
    messages: {
        ename: {
            required: "Vui lòng nhập tên sản phẩm",
            minlength: "Phải nhập 3 ký tự trở lên"
        },
        eprice: {
           // required: "Vui lòng nhập giá sản phẩm",
            number: "Vui lòng nhập số"
        }
    }
});

$(document).ready(function () {
    $(".alert, .alert-success").hide();

    showList("");

    //let connection = new signalR.HubConnectionBuilder().withUrl("/signalServer").build()

    //connection.start()

    //connection.on("refreshEmployees", function () {
    //    showList();
    //})
});