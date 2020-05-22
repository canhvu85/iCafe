//let billsId;
////let user = JSON.parse(localStorage.getItem('user'));
//let totalMn;

////var hdnUserSession = $("#hdnUserSession").data("value");
////console.log("shopsId  " + hdnUserSession.ShopsId);

//let user = JSON.parse(sessionStorage.getItem('user'));

//getBillsId();
//getBillDetails(parseInt($("#billsList tr:first td:first").text()));
//$("#billsList tr:first").addClass("activeClk");
//dataTableChange();
//totalMoney();

//function getBillDetails(billsId) {
//	axios({
//		url: GetBillDetail + "/bills/" + billsId,
//		method: "GET",
//		headers: {
//			'content-type': 'application/json',
//			'Authorization': user.remember_token
//		}
//	}).then(function (response) {
//		$("#billsDTList").html("");
//		if (response.data.length > 0) {
//			let text = '';
//			for (let i = 0; i < response.data.length; i++) {
//				text += "<tr>";
//				text += "<td>" + (i + 1) + ".</td>";
//				text += "<td><div>" + response.data[i].productsName + "</div><div><b>" + addCommas(response.data[i].price) + " vnđ</b></div></td>";
//				text += "<td>" + response.data[i].quantity + "</td>";
//				text += "<td>" + addCommas(response.data[i].total) + " vnđ</td>";
//				text += "</tr>";
//			}
//			$("#billsDTList").html(text);
//		}
//	}).catch(function () {
//		unAuthorized();
//	});
//}

//let startDate,
//	endDate;

//$('#reservation').daterangepicker({
//	"showDropdowns": true,
//	ranges: {
//		'Today': [moment(), moment()],
//		'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
//		'Last 7 Days': [moment().subtract(6, 'days'), moment()],
//		'Last 30 Days': [moment().subtract(29, 'days'), moment()],
//		'This Month': [moment().startOf('month'), moment().endOf('month')],
//		'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
//	},
//	"alwaysShowCalendars": true
//});

//$('.datePicker, .dateRangeLeft, .dateRangeRight').daterangepicker({
//	"singleDatePicker": true,
//	"showDropdowns": true,
//	"locale": {
//		"format": 'DD-MM-YYYY'
//	}
//});

//function findByDate(startDate, endDate) {
//	$("#example1").DataTable({
//		"destroy": true,
//		"paging": true,
//		"searching": true,
//		"ordering": true,
//		"info": true,
//		"ajax": {
//			"url": `api/BillsAPI/shop/${user.ShopsId}/date/${startDate}/${endDate}`,
//			"headers": {
//				'content-type': 'application/json',
//				'Authorization': user.remember_token
//			},
//			"dataSrc": ""
//		},
//		columns: [
//			{ data: 'id' },
//			{ data: 'name' },
//			{ title: "Tài Khoản", data: 'created_by' },
//			{
//				data: 'time_out',
//				render: function (data, type, row, meta) {
//					return moment(data).format('DD-MM-YYYY');
//				}
//			},
//			{
//				data: 'sub_total',
//				render: function (data, type, row, meta) {
//					return addCommas(data) + " vnđ";
//				}
//			},
//			{
//				data: 'fee_service',
//				render: function (data, type, row, meta) {
//					return addCommas(data) + " vnđ";
//				}
//			},
//			{
//				data: 'total_money',
//				render: function (data, type, row, meta) {
//					return addCommas(data) + " vnđ";
//				}
//			},
//			{
//				data: 'status',
//				render: function (data, type, row, meta) {
//					if (data == 0)
//						return "<span class='badge bg-danger'>Chưa thanh toán</span>";
//					else
//						return "<span class='badge bg-secondary'>Đã thanh toán</span>";
//				}
//			}

//		],
//		initComplete: function (data) {
//			if (data.aoData.length > 0) {
//				getBillsId();
//			} else {
//				$("#billsDTList").html("");
//				$("#totalMoney").html("");
//			}
//		}
//	});
//}

//$("#datePickerBtn").on("click", function () {
//	startDate = $("#datePicker").val();
//	findByDate(startDate, startDate)
//	return false;
//});

//$('#dateRangeLeft').on('apply.daterangepicker', function (ev, picker) {
//	startDate = picker.startDate.format('YYYY-MM-DD');
//	$('.dateRangeRight').daterangepicker({
//		"singleDatePicker": true,
//		"showDropdowns": true,
//		"locale": {
//			"format": 'DD-MM-YYYY'
//		},
//		"minDate": new Date(startDate)
//	});
//});

//$("#dateRangeBtn").on("click", function () {
//	endDate = $("#dateRangeRight").val();
//	findByDate(startDate, endDate);
//	return false;
//});

//$("#monthYearBtn").on("click", function () {
//	var month = $("#monthPicker").val();
//	var year = $("#yearPicker").val();
//	month == 0 ? showErrorbyAlert('Cảnh báo', 'Hãy chọn tháng') : (year == 0 ? showErrorbyAlert('Cảnh báo', 'Hãy chọn năm') : monthYearValid());

//	function monthYearValid() {
//		startDate = `01-${month}-${year}`;
//		endDate = `${lastDay(year, month)}-${month}-${year}`;
//		console.log(endDate);
//		findByDate(startDate, endDate);
//		return false;
//	}
//});

//function getBillsId() {
//	$("#billsList tr").on("click", function () {
//		billsId = parseInt($(this).find("td:eq(0)").text());
//		getBillDetails(billsId);
//		$("#billsList tr").removeClass("activeClk");
//		$(this).addClass("activeClk");
//	});
//}

//function lastDay(y, m) {
//	return new Date(y, m, 0).getDate();
//}

//function totalMoney() {
//	if ($("#example1 tbody tr:first td").length > 1) {
//		totalMn = 0;
//		$("#example1 tbody tr").each(function () {
//			totalMn += parseInt($(this).find("td:eq(6)").html().replace(".", "").replace("vnđ", ""));
//		});
//		$("#totalMoney").html(addCommas(totalMn) + " vnđ");
//	} else
//		$("#totalMoney").html("0 vnđ");
//}

//function dataTableChange() {
//	$('#example1').on('init.dt', function () {
//		totalMoney();
//	});
//	$('#example1').dataTable({
//		"fnDrawCallback": function (oSettings) {
//			totalMoney();
//		}
//	});
//}












$(document).ready(function () {
	var grid_selector = "#list4";
	var pager_selector = "#pager2";
	jQuery(grid_selector).jqGrid({
		url: 'https://localhost:5001/api/BillsAPI/?TableId=1',
		datatype: "json",
		colNames: ['id', 'tablesName', 'created_by', 'sub_total', 'fee_service', 'total_money'],
		colModel: [
			{ name: 'id', index: 'id', width: 55 },
			{ name: 'tablesName', index: 'tablesName', width: 90 },
			{ name: 'created_by', index: 'created_by', width: 100 },
			{ name: 'sub_total', index: 'sub_total', width: 80 },
			{ name: 'fee_service', index: 'fee_service', width: 80 },
			{ name: 'total_money', index: 'total_money', width: 80, sortable: false }
		],
		//loadonce: true,
		rowNum: 2,
		rowList: [2, 20, 30],
		pager: pager_selector,
		sortname: 'id',
		viewrecords: true,
		guiStyle: "bootstrap",
		iconSet: "fontAwesome",
		rownumbers: true,
		sortorder: "desc",
		caption: "JSON Example"
	});
	jQuery(grid_selector).jqGrid('navGrid', pager_selector, { edit: false, add: false, del: false });
});


//$(function () {
//    "use strict";
//    $.ajax({
//        url: "https://localhost:5001/api/BillsAPI/?TableId=1",
//        method: "GET",
//        dataType: "json",
//        contentType: "application/json"
//    }).done(function (data) {
//        jQuery("#list4").jqGrid({
//            datatype: "local",
//            height: 250,
//            colNames: ['id', 'tablesName', 'created_by', 'sub_total', 'fee_service', 'total_money'],
//            colModel: [
//                { name: 'id', index: 'id', width: 55 },
//                { name: 'tablesName', index: 'tablesName', width: 90 },
//                { name: 'created_by', index: 'created_by', width: 100 },
//                { name: 'sub_total', index: 'sub_total', width: 80 },
//                { name: 'fee_service', index: 'fee_service', width: 80 },
//                { name: 'total_money', index: 'total_money', width: 80, sortable: false }
//            ],
//            multiselect: true,
//            caption: "Manipulating Array Data"
//        });
//        var mydata = data;
//        for (var i = 0; i <= mydata.length; i++)
//            jQuery("#list4").jqGrid('addRowData', i + 1, mydata[i]);
//    });

//});

//$(function () {
//    "use strict";
//    $("#list4").jqGrid({
//            colModel: [
//                { name: "name", label: "Client", width: 53 },
//                { name: "invdate", label: "Date", width: 75, align: "center", sorttype: "date",
//                    formatter: "date", formatoptions: { newformat: "d-M-Y" } },
//                { name: "amount", label: "Amount", width: 65, template: "number" },
//                { name: "tax", label: "Tax", width: 41, template: "number" },
//                { name: "total", label: "Total", width: 51, template: "number" },
//                { name: "closed", label: "Closed", width: 59, template: "booleanCheckbox", firstsortorder: "desc" },
//                { name: "ship_via", label: "Shipped via", width: 87, align: "center", formatter: "select",
//                    formatoptions: { value: "FE:FedEx;TN:TNT;DH:DHL", defaultValue: "DH" } }
//            ],
//            data: [
//                { id: "10",  invdate: "2015-10-01", name: "test",   amount: "" },
//                { id: "20",  invdate: "2015-09-01", name: "test2",  amount: "300.00", tax:"20.00", closed:false, ship_via:"FE", total:"320.00"},
//                { id: "30",  invdate: "2015-09-01", name: "test3",  amount: "400.00", tax:"30.00", closed:false, ship_via:"FE", total:"430.00"},
//                { id: "40",  invdate: "2015-10-04", name: "test4",  amount: "200.00", tax:"10.00", closed:true,  ship_via:"TN", total:"210.00"},
//                { id: "50",  invdate: "2015-10-31", name: "test5",  amount: "300.00", tax:"20.00", closed:false, ship_via:"FE", total:"320.00"},
//                { id: "60",  invdate: "2015-09-06", name: "test6",  amount: "400.00", tax:"30.00", closed:false, ship_via:"FE", total:"430.00"},
//                { id: "70",  invdate: "2015-10-04", name: "test7",  amount: "200.00", tax:"10.00", closed:true,  ship_via:"TN", total:"210.00"},
//                { id: "80",  invdate: "2015-10-03", name: "test8",  amount: "300.00", tax:"20.00", closed:false, ship_via:"FE", total:"320.00"},
//                { id: "90",  invdate: "2015-09-01", name: "test9",  amount: "400.00", tax:"30.00", closed:false, ship_via:"TN", total:"430.00"},
//                { id: "100", invdate: "2015-09-08", name: "test10", amount: "500.00", tax:"30.00", closed:true,  ship_via:"TN", total:"530.00"},
//                { id: "110", invdate: "2015-09-08", name: "test11", amount: "500.00", tax:"30.00", closed:false, ship_via:"FE", total:"530.00"},
//                { id: "120", invdate: "2015-09-10", name: "test12", amount: "500.00", tax:"30.00", closed:false, ship_via:"FE", total:"530.00"}
//        ],
//        loadonce: true,
//            guiStyle: "bootstrap",
//            iconSet: "fontAwesome",
//            idPrefix: "gb1_",
//            rownumbers: true,
//            sortname: "invdate",
//            sortorder: "desc",
//            caption: "The grid, which uses predefined formatters and templates"
//    });
//    jQuery("#list4").jqGrid('navGrid', { edit: false, add: false, del: false });
//});