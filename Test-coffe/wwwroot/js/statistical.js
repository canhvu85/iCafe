let billsId;
let user = JSON.parse(sessionStorage.getItem('user'));
let totalMn;

//var hdnUserSession = $("#hdnUserSession").data("value");
//console.log("shopsId  " + hdnUserSession.ShopsId);


//getBillsId();
//getBillDetails(parseInt($("#billsList tr:first td:first").text()));
$("#billsList tr:first").addClass("activeClk");
dataTableChange();
totalMoney();

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
	//$("#example1").DataTable({
	//	"destroy": true,
	//	"paging": true,
	//	"searching": true,
	//	"ordering": true,
	//	"info": true,
	//	"ajax": {
	//		"url": `api/BillsAPI/shop/${user.ShopsId}/date/${startDate}/${endDate}`,
	//		"headers": {
	//			'content-type': 'application/json',
	//			'Authorization': user.remember_token
	//		},
	//		"dataSrc": ""
	//	},
	//	columns: [
	//		{ data: 'id' },
	//		{ data: 'name' },
	//		{ title: "Tài Khoản", data: 'created_by' },
	//		{
	//			data: 'time_out',
	//			render: function (data, type, row, meta) {
	//				return moment(data).format('DD-MM-YYYY');
	//			}
	//		},
	//		{
	//			data: 'sub_total',
	//			render: function (data, type, row, meta) {
	//				return addCommas(data) + " vnđ";
	//			}
	//		},
	//		{
	//			data: 'fee_service',
	//			render: function (data, type, row, meta) {
	//				return addCommas(data) + " vnđ";
	//			}
	//		},
	//		{
	//			data: 'total_money',
	//			render: function (data, type, row, meta) {
	//				return addCommas(data) + " vnđ";
	//			}
	//		},
	//		{
	//			data: 'status',
	//			render: function (data, type, row, meta) {
	//				if (data == 0)
	//					return "<span class='badge bg-danger'>Chưa thanh toán</span>";
	//				else
	//					return "<span class='badge bg-secondary'>Đã thanh toán</span>";
	//			}
	//		}

	//	],
	//	initComplete: function (data) {
	//		if (data.aoData.length > 0) {
	//			getBillsId();
	//		} else {
	//			$("#billsDTList").html("");
	//			$("#totalMoney").html("");
	//		}
	//	}
	//});

    jQuery(grid_selector).jqGrid({
        subGrid: true,
        subGridOptions: {
            plusicon: "ace-icon fa fa-plus center bigger-110 blue",
            minusicon: "ace-icon fa fa-minus center bigger-110 blue",
            openicon: "ace-icon fa fa-chevron-right center orange"
        },
        subGridRowExpanded: function (subgridDivId, rowId) {
            var subgridTableId = subgridDivId + "_t";
            $("#" + subgridDivId).html("<table id='" + subgridTableId + "'></table>");
            $("#" + subgridTableId).jqGrid({
                url: `${GetBillDetail}/bills/${rowId}`,
                datatype: "json",
                colNames: ['Sản Phẩm', 'Giá', 'Số Lượng', 'Tổng Cộng'],
                colModel: [
                    { name: "productsName", index: "productsName", width: 80 },
                    { name: "price", index: "price", width: 130 },
                    { name: "quantity", index: "quantity", width: 70, align: "right" },
                    { name: "total", index: "total", width: 70, align: "right" }
                ],
                autowidth: true
            });
        },
        url: `${GetBill}/shop/${user.ShopsId}/date/${startDate}/${endDate}`,
        datatype: "json",
        height: 250,
        colNames: ['id', 'Bàn', 'Tài Khoản', 'Ngày Đã Thanh Toán', 'Tổng Tiền', 'Phí Dịch Vụ',
            'Tổng Cộng', 'Tình Trạng'],
        colModel: [

            { name: 'id', index: 'id' },
            { name: 'name', index: 'name' },
            { name: 'created_by', index: 'created_by' },
            { name: 'time_out', index: 'time_out' },
            { name: 'sub_total', index: 'sub_total' },
            { name: 'fee_service', index: 'fee_service' },
            { name: 'total_money', index: 'total_money' },
            { name: 'status', index: 'status' }
        ],
        autowidth: true,
        loadonce: true,
        rowNum: 2,
        rowList: [2, 5, 30],
        pager: pager_selector,
        loadComplete: function () {
            var table = this;
            setTimeout(function () {
                updatePagerIcons(table);
            }, 0);
        },
    });


}

$("#datePickerBtn").on("click", function () {
	startDate = $("#datePicker").val();
	console.log(startDate);
	findByDate(startDate, startDate);
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

//function getBillsId() {
//	$("#billsList tr").on("click", function () {
//		billsId = parseInt($(this).find("td:eq(0)").text());
//		getBillDetails(billsId);
//		$("#billsList tr").removeClass("activeClk");
//		$(this).addClass("activeClk");
//	});
//}

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





var grid_selector = "#grid-table";
var pager_selector = "#grid-pager";

var parent_column = $(grid_selector).closest('[class*="col-"]');

jQuery(function ($) {
    //resize to fit page size
    $(window).on('resize.jqGrid', function () {
        $(grid_selector).jqGrid('setGridWidth', parent_column.width());
    })

    jQuery(grid_selector).jqGrid({
        subGrid: true,
        subGridOptions: {
            plusicon: "ace-icon fa fa-plus center bigger-110 blue",
            minusicon: "ace-icon fa fa-minus center bigger-110 blue",
            openicon: "ace-icon fa fa-chevron-right center orange"
        },
        subGridRowExpanded: function (subgridDivId, rowId) {
            var subgridTableId = subgridDivId + "_t";
            $("#" + subgridDivId).html("<table id='" + subgridTableId + "'></table>");
            $("#" + subgridTableId).jqGrid({
				url: `${GetBillDetail}/bills/${rowId}`,
                datatype: "json",
                colNames: ['Sản Phẩm', 'Giá', 'Số Lượng', 'Tổng Cộng'],
                colModel: [
                    { name: "productsName", index: "productsName", width: 80 },
                    { name: "price", index: "price", width: 130 },
                    { name: "quantity", index: "quantity", width: 70, align: "right" },
                    { name: "total", index: "total", width: 70, align: "right" }
                ],
                autowidth: true
            });
        },
        //url: `${GetBill}/shop/${user.ShopsId}`,
        url: `${GetBill}/shop/${user.ShopsId}/date/2020-05-25/2020-05-25`,
        datatype: "json",
        height: 250,
        colNames: ['id', 'Bàn', 'Tài Khoản', 'Ngày Đã Thanh Toán', 'Tổng Tiền', 'Phí Dịch Vụ',
            'Tổng Cộng', 'Tình Trạng'],
        colModel: [

            { name: 'id', index: 'id' },
            { name: 'name', index: 'name' },
            { name: 'created_by', index: 'created_by' },
            { name: 'time_out', index: 'time_out' },
            { name: 'sub_total', index: 'sub_total' },
            { name: 'fee_service', index: 'fee_service' },
            { name: 'total_money', index: 'total_money' },
            { name: 'status', index: 'status' }
        ],
        autowidth: true,
        loadonce: true,
        rowNum: 2,
        rowList: [2, 5, 30],
        pager: pager_selector,
        loadComplete: function () {
            var table = this;
            setTimeout(function () {
                updatePagerIcons(table);
            }, 0);
        },
    });
    $(window).triggerHandler('resize.jqGrid');//trigger window resize to make the grid get the correct size


    //navButtons
    jQuery(grid_selector).jqGrid('navGrid', pager_selector,
        { 	//navbar options
            edit: false,
            add: false,
            del: false,
            search: true,
            searchicon: 'ace-icon fa fa-search orange',
            refresh: true,
            refreshicon: 'ace-icon fa fa-refresh green',
            view: false
        },
        {
            //search form
            recreateForm: true,
            afterShowSearch: function (e) {
                var form = $(e[0]);
                form.closest('.ui-jqdialog').find('.ui-jqdialog-title').wrap('<div class="widget-header" />')
                style_search_form(form);
            },
            afterRedraw: function () {
                style_search_filters($(this));
            }
            ,
            multipleSearch: true
        }
    )

    function style_search_filters(form) {
        form.find('.delete-rule').val('X');
        form.find('.add-rule').addClass('btn btn-xs btn-primary');
        form.find('.add-group').addClass('btn btn-xs btn-success');
        form.find('.delete-group').addClass('btn btn-xs btn-danger');
    }
    function style_search_form(form) {
        var dialog = form.closest('.ui-jqdialog');
        var buttons = dialog.find('.EditTable')
        buttons.find('.EditButton a[id*="_reset"]').addClass('btn btn-sm btn-info').find('.ui-icon').attr('class', 'ace-icon fa fa-retweet');
        buttons.find('.EditButton a[id*="_query"]').addClass('btn btn-sm btn-inverse').find('.ui-icon').attr('class', 'ace-icon fa fa-comment-o');
        buttons.find('.EditButton a[id*="_search"]').addClass('btn btn-sm btn-purple').find('.ui-icon').attr('class', 'ace-icon fa fa-search');
    }

    //replace icons with FontAwesome icons like above
    function updatePagerIcons(table) {
        var replacement =
        {
            'ui-icon-seek-first': 'ace-icon fa fa-angle-double-left bigger-140',
            'ui-icon-seek-prev': 'ace-icon fa fa-angle-left bigger-140',
            'ui-icon-seek-next': 'ace-icon fa fa-angle-right bigger-140',
            'ui-icon-seek-end': 'ace-icon fa fa-angle-double-right bigger-140'
        };
        $('.ui-pg-table:not(.navtable) > tbody > tr > .ui-pg-button > .ui-icon').each(function () {
            var icon = $(this);
            var $class = $.trim(icon.attr('class').replace('ui-icon', ''));

            if ($class in replacement) icon.attr('class', 'ui-icon ' + replacement[$class]);
        })
    }
    //$(document).one('ajaxloadstart.page', function (e) {
    //    $.jgrid.gridDestroy(grid_selector);
    //    $('.ui-jqdialog').remove();
    //});
});