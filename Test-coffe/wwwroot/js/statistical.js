let billsId;
//let user = JSON.parse(localStorage.getItem('user'));
let totalMn;

//var hdnUserSession = $("#hdnUserSession").data("value");
//console.log("shopsId  " + hdnUserSession.ShopsId);

let user = JSON.parse(sessionStorage.getItem('user'));

getBillsId();
getBillDetails(parseInt($("#billsList tr:first td:first").text()));
$("#billsList tr:first").addClass("activeClk");
dataTableChange();
totalMoney();

function getBillDetails(billsId) {
	axios({
		url: GetBillDetail + "/bills/" + billsId,
		method: "GET",
		headers: {
			'content-type': 'application/json',
			'Authorization': user.remember_token
		}
	}).then(function (response) {
		$("#billsDTList").html("");
		if (response.data.length > 0) {
			let text = '';
			for (let i = 0; i < response.data.length; i++) {
				text += "<tr>";
				text += "<td>" + (i + 1) + ".</td>";
				text += "<td><div>" + response.data[i].productsName + "</div><div><b>" + addCommas(response.data[i].price) + " vnđ</b></div></td>";
				text += "<td>" + response.data[i].quantity + "</td>";
				text += "<td>" + addCommas(response.data[i].total) + " vnđ</td>";
				text += "</tr>";
			}
			$("#billsDTList").html(text);
		}
	}).catch(function () {
		unAuthorized();
	});
}

let startDate,
	endDate;

$('#reservation').daterangepicker({
	"showDropdowns": true,
	ranges: {
		'Today': [moment(), moment()],
		'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
		'Last 7 Days': [moment().subtract(6, 'days'), moment()],
		'Last 30 Days': [moment().subtract(29, 'days'), moment()],
		'This Month': [moment().startOf('month'), moment().endOf('month')],
		'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
	},
	"alwaysShowCalendars": true
});

$('.datePicker, .dateRangeLeft, .dateRangeRight').daterangepicker({
	"singleDatePicker": true,
	"showDropdowns": true,
	"locale": {
		"format": 'DD-MM-YYYY'
	}
});

function findByDate(startDate, endDate) {
	$("#example1").DataTable({
		"destroy": true,
		"paging": true,
		"searching": true,
		"ordering": true,
		"info": true,
		"ajax": {
			"url": `api/BillsAPI/shop/${user.ShopsId}/date/${startDate}/${endDate}`,
			"headers": {
				'content-type': 'application/json',
				'Authorization': user.remember_token
			},
			"dataSrc": ""
		},
		columns: [
			{ data: 'id' },
			{ data: 'name' },
			{ title: "Tài Khoản", data: 'created_by' },
			{
				data: 'time_out',
				render: function (data, type, row, meta) {
					return moment(data).format('DD-MM-YYYY');
				}
			},
			{
				data: 'sub_total',
				render: function (data, type, row, meta) {
					return addCommas(data) + " vnđ";
				}
			},
			{
				data: 'fee_service',
				render: function (data, type, row, meta) {
					return addCommas(data) + " vnđ";
				}
			},
			{
				data: 'total_money',
				render: function (data, type, row, meta) {
					return addCommas(data) + " vnđ";
				}
			},
			{
				data: 'status',
				render: function (data, type, row, meta) {
					if (data == 0)
						return "<span class='badge bg-danger'>Chưa thanh toán</span>";
					else
						return "<span class='badge bg-secondary'>Đã thanh toán</span>";
				}
			}

		],
		initComplete: function (data) {
			if (data.aoData.length > 0) {
				getBillsId();
			} else {
				$("#billsDTList").html("");
				$("#totalMoney").html("");
			}
		}
	});
}

$("#datePickerBtn").on("click", function () {
	startDate = $("#datePicker").val();
	findByDate(startDate, startDate)
	return false;
});

$('#dateRangeLeft').on('apply.daterangepicker', function (ev, picker) {
	startDate = picker.startDate.format('YYYY-MM-DD');
	$('.dateRangeRight').daterangepicker({
		"singleDatePicker": true,
		"showDropdowns": true,
		"locale": {
			"format": 'DD-MM-YYYY'
		},
		"minDate": new Date(startDate)
	});
});

$("#dateRangeBtn").on("click", function () {
	endDate = $("#dateRangeRight").val();
	findByDate(startDate, endDate);
	return false;
});

$("#monthYearBtn").on("click", function () {
	var month = $("#monthPicker").val();
	var year = $("#yearPicker").val();
	month == 0 ? showErrorbyAlert('Cảnh báo', 'Hãy chọn tháng') : (year == 0 ? showErrorbyAlert('Cảnh báo', 'Hãy chọn năm') : monthYearValid());

	function monthYearValid() {
		startDate = `01-${month}-${year}`;
		endDate = `${lastDay(year, month)}-${month}-${year}`;
		console.log(endDate);
		findByDate(startDate, endDate);
		return false;
	}
});

function getBillsId() {
	$("#billsList tr").on("click", function () {
		billsId = parseInt($(this).find("td:eq(0)").text());
		getBillDetails(billsId);
		$("#billsList tr").removeClass("activeClk");
		$(this).addClass("activeClk");
	});
}

function lastDay(y, m) {
	return new Date(y, m, 0).getDate();
}

function totalMoney() {
	if ($("#example1 tbody tr:first td").length > 1) {
		totalMn = 0;
		$("#example1 tbody tr").each(function () {
			totalMn += parseInt($(this).find("td:eq(6)").html().replace(".", "").replace("vnđ", ""));
		});
		$("#totalMoney").html(addCommas(totalMn) + " vnđ");
	} else
		$("#totalMoney").html("0 vnđ");
}

function dataTableChange() {
	$('#example1').on('init.dt', function () {
		totalMoney();
	});
	$('#example1').dataTable({
		"fnDrawCallback": function (oSettings) {
			totalMoney();
		}
	});
}