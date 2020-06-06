let billsId;
let user = JSON.parse(sessionStorage.getItem('user'));
let totalMn;

$("#billsList tr:first").addClass("activeClk");

let startDate,
    endDate;

$('.datePicker, .dateRangeLeft, .dateRangeRight').daterangepicker({
    "singleDatePicker": true,
    "showDropdowns": true,
    "locale": {
        "format": 'DD-MM-YYYY'
    }
});

function findByDate(startDate, endDate) {
    $.jgrid.gridUnload("#grid-table");
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
                    { name: "productsName", index: "productsName" },
                    {
                        name: "price", index: "price",
                        formatter: 'currency',
                        formatoptions: {
                            decimalSeparator: ",",
                            thousandsSeparator: ".",
                            decimalPlaces: 0,
                            suffix: " VND"
                        }
                    },
                    { name: "quantity", index: "quantity" },
                    {
                        name: "total", index: "total",
                        formatter: 'currency',
                        formatoptions: {
                            decimalSeparator: ",",
                            thousandsSeparator: ".",
                            decimalPlaces: 0,
                            suffix: " VND"
                        }
                    }
                ],
                autowidth: true,
                loadComplete: function (data) {
                    $(grid_selector).find("thead>tr>th>div").css("text-align", "center");
                    $(this).find("tbody>tr>td").css("text-align", "center");
                }
            });
        },
        url: `${GetBill}/shop/${user.ShopsId}/date/${startDate}/${endDate}`,
        datatype: "json",
        height: 250,
        colNames: ['id', 'Bàn', 'Tài Khoản', 'Ngày Đã Thanh Toán', 'Tình Trạng', 'Tổng Tiền', 'Phí Dịch Vụ',
            'Tổng Cộng'],
        colModel: [

            { name: 'id', index: 'id' },
            { name: 'name', index: 'name' },
            { name: 'created_by', index: 'created_by' },
            { name: 'time_out', index: 'time_out' },
            { name: 'status', index: 'status', formatter: statusFmatter },
            {
                name: 'sub_total', index: 'sub_total',
                formatter: 'currency',
                formatoptions: {
                    decimalSeparator: ",",
                    thousandsSeparator: ".",
                    decimalPlaces: 0,
                    suffix: " VND"
                }
            },
            {
                name: 'fee_service', index: 'fee_service',
                formatter: 'currency',
                formatoptions: {
                    decimalSeparator: ",",
                    thousandsSeparator: ".",
                    decimalPlaces: 0,
                    suffix: " VND"
                }
            },
            {
                name: 'total_money', index: 'total_money',
                formatter: 'currency',
                formatoptions: {
                    decimalSeparator: ",",
                    thousandsSeparator: ".",
                    decimalPlaces: 0,
                    suffix: " VND"
                }
            }
        ],
        autowidth: true,
        loadonce: true,
        "footerrow": true,
        "userDataOnFooter": false,
        rowNum: 2,
        rowList: [2, 5, 30, 'ALL'],
        pager: pager_selector,
        loadComplete: function (data) {
            $(this).closest("div.ui-jqgrid-view").find("table.ui-jqgrid-htable>thead>tr>th>div").css("text-align", "center");
            $(this).find("tbody>tr>td").css("text-align", "center");
            var table = this;
            $(".ui-pg-selbox option[value='ALL']").val(data.length);

            $(this).jqGrid("footerData", "set", {
                id: "Tổng cộng",
                total_money: $(this).jqGrid('getCol', 'total_money', false, 'sum')
            });

            var $footRow = $(grid_selector).closest(".ui-jqgrid-bdiv")
                .next(".ui-jqgrid-sdiv")
                .find(".footrow");
            $footRow.find('td').each(function () {
                $(this).css("border-right-color", "transparent");
            })


            if ($(grid_selector).getGridParam('records') === 0) {
                oldGrid = $('#grid-table tbody').html();
                $('#grid-table tbody').html("<div style='padding:6px;background:#00000005;text-align:center'>Không tìm thấy dữ liệu</div>");
            }
            else
                oldGrid = "";

            setTimeout(function () {
                updatePagerIcons(table);
            }, 0);
        }
    });
    navButtonsJqgrid();
}

$("#datePickerBtn").on("click", function () {
    startDate = $("#datePicker").val();
    findByDate(startDate, startDate);
    startDate = null;
    return false;
});

$('#dateRangeLeft').on('apply.daterangepicker', function (ev, picker) {
    startDate = picker.startDate.format('DD-MM-YYYY');
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
    if (startDate == null)
        startDate = $("#dateRangeLeft").val();
    endDate = $("#dateRangeRight").val();
    console.log(startDate);
    console.log(endDate);
    findByDate(startDate, endDate);
    return false;
});

$("#monthYearBtn").on("click", function () {
    var month = $("#monthPicker").val();
    //if (month.length == 1)
    //    month = 0 + month;
    var year = $("#yearPicker").val();
    month == 0 ? showErrorbyAlert('Hãy chọn tháng') : (year == 0 ? showErrorbyAlert('Hãy chọn năm') : monthYearValid());

    function monthYearValid() {
        startDate = `01-${month}-${year}`;
        endDate = `${lastDay(year, month)}-${month}-${year}`;
        console.log(startDate);
        console.log(endDate);
        findByDate(startDate, endDate);
        return false;
    }
});

function lastDay(y, m) {
    return new Date(y, m, 0).getDate();
}


var grid_selector = "#grid-table";
var pager_selector = "#grid-pager";

var parent_column = $(grid_selector).closest('[class*="col-"]');

jQuery(function ($) {
    //resize to fit page size
    $(window).on('resize.jqGrid', function () {
        $(grid_selector).jqGrid('setGridWidth', parent_column.width());
    })

    $.ajaxSetup({
        headers: {
            'content-type': 'application/json',
            'Authorization': user.remember_token
        }
    });

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
                    { name: "productsName", index: "productsName" },
                    {
                        name: "price", index: "price",
                        formatter: 'currency',
                        formatoptions: {
                            decimalSeparator: ",",
                            thousandsSeparator: ".",
                            decimalPlaces: 0,
                            suffix: " VND"
                        }
                    },
                    { name: "quantity", index: "quantity" },
                    {
                        name: "total", index: "total",
                        formatter: 'currency',
                        formatoptions: {
                            decimalSeparator: ",",
                            thousandsSeparator: ".",
                            decimalPlaces: 0,
                            suffix: " VND"
                        }
                    }
                ],
                autowidth: true,
                loadComplete: function (data) {
                    $(grid_selector).find("thead>tr>th>div").css("text-align", "center");
                    $(this).find("tbody>tr>td").css("text-align", "center");
                }
            });
        },
        url: `${GetBill}/shop/${user.ShopsId}`,
        datatype: "json",
        height: 250,
        colNames: ['id', 'Bàn', 'Tài Khoản', 'Ngày Đã Thanh Toán', 'Tình Trạng', 'Tổng Tiền', 'Phí Dịch Vụ',
            'Tổng Cộng'],
        colModel: [

            { name: 'id', index: 'id' },
            { name: 'name', index: 'name' },
            { name: 'created_by', index: 'created_by' },
            { name: 'time_out', index: 'time_out' },
            { name: 'status', index: 'status', formatter: statusFmatter },
            {
                name: 'sub_total', index: 'sub_total',
                formatter: 'currency',
                formatoptions: {
                    decimalSeparator: ",",
                    thousandsSeparator: ".",
                    decimalPlaces: 0,
                    suffix: " VND"
                }
            },
            {
                name: 'fee_service', index: 'fee_service',
                formatter: 'currency',
                formatoptions: {
                    decimalSeparator: ",",
                    thousandsSeparator: ".",
                    decimalPlaces: 0,
                    suffix: " VND"
                }
            },
            {
                name: 'total_money', index: 'total_money',
                formatter: 'currency',
                formatoptions: {
                    decimalSeparator: ",",
                    thousandsSeparator: ".",
                    decimalPlaces: 0,
                    suffix: " VND"
                }
            }
        ],
        autowidth: true,
        loadonce: true,
        "footerrow": true,
        "userDataOnFooter": false,
        shrinktofit: true,
        rowNum: 2,
        rowList: [2, 5, 30, 'ALL'],
        pager: pager_selector,
        loadComplete: function (data) {
            $(this).closest("div.ui-jqgrid-view").find("table.ui-jqgrid-htable>thead>tr>th>div").css("text-align", "center");
            $(grid_selector).find("tbody>tr>td").css("text-align", "center");
            var table = this;
            $(".ui-pg-selbox option[value='ALL']").val(data.length);

            $(this).jqGrid("footerData", "set", {
                id: "Tổng cộng",
                total_money: $(this).jqGrid('getCol', 'total_money', false, 'sum')
            });

            //var $self = $(this),
            //    sum = $self.jqGrid("getCol", "total_money", false, "sum"),
            //    i,
            //    iCol = $("#" + $.jgrid.jqID(this.id) + "_" + "total_money")[0].cellIndex, // get index of "amount" column
            //    sumFormatted = this.formatter("", sum, iCol);

            //$self.jqGrid(
            //    "footerData",
            //    "set",
            //    {
            //        id: "Tổng cộng",
            //        total_money: sum < 100000 ? "<span style='color:red'>" + sumFormatted + "</span>" : sum
            //    },
            //    false
            //);

            var $footRow = $(grid_selector).closest(".ui-jqgrid-bdiv")
                .next(".ui-jqgrid-sdiv")
                .find(".footrow");

            //$footRow.find('>td:eq(5)')
            //    .css("border-right-color", "transparent");

            $footRow.find('td').each(function () {
                $(this).css("border-right-color", "transparent");
            });

            if ($(grid_selector).getGridParam('records') === 0) {
                oldGrid = $('#grid-table tbody').html();
                $('#grid-table tbody').html("<div style='padding:6px;background:#00000005;text-align:center'>Không tìm thấy dữ liệu</div>");
            }
            else
                oldGrid = "";

            setTimeout(function () {
                updatePagerIcons(table);
            }, 0);
        }
    });



    //$(grid_selector).jqGrid('footerData', 'set', { "total_money": "Total:" }, true);
    //$(window).triggerHandler('resize.jqGrid');//trigger window resize to make the grid get the correct size
    //$(window).triggerHandler($("#grid-table").setGridWidth($("#gridTb").width()));
    $(".sidebar-mini .navbar-nav .nav-item:eq(0)").click(function () {
        //setTimeout(function () {
        //    $(grid_selector).setGridWidth($("#gridTb").width());
        //}, 200);
        //while ($("body").hasClass("sidebar-collapse") == false) {
        //    console.log("sdads");
        //};
        //while (!$("body").hasClass("sidebar-collapse")) {
        //    await new Promise(r => setTimeout(r, 500));
        //}
        var waitForEl = function (selector, callback) {
            if (jQuery(selector).length) {
                callback();
            } else {
                setTimeout(function () {
                    waitForEl(selector, callback);
                }, 100);
            }
        };

        waitForEl(".sidebar-collapse", function () {
            setTimeout(function () {
                $(grid_selector).setGridWidth($("#gridTb").width());
            }, 300);
        });
    });
    navButtonsJqgrid();
});

function statusFmatter(cellvalue, options, rowObject) {
    if (cellvalue == 0)
        return "<span class='badge bg-danger'>Chưa thanh toán</span>";
    else
        return "<span class='badge bg-secondary'>Đã thanh toán</span>";
}

function navButtonsJqgrid() {
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
}

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