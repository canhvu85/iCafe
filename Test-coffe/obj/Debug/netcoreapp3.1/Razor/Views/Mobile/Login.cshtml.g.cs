#pragma checksum "F:\Gitlab\github\iCafe\Test-coffe\Views\Mobile\Login.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fdc9cdec0e489e8873ec9f0f991b7b117f9671f7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Mobile_Login), @"mvc.1.0.view", @"/Views/Mobile/Login.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "F:\Gitlab\github\iCafe\Test-coffe\Views\_ViewImports.cshtml"
using Test_coffe;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "F:\Gitlab\github\iCafe\Test-coffe\Views\_ViewImports.cshtml"
using Test_coffe.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fdc9cdec0e489e8873ec9f0f991b7b117f9671f7", @"/Views/Mobile/Login.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"04158a09f2ba594abb4a998dcb66526163e31db0", @"/Views/_ViewImports.cshtml")]
    public class Views_Mobile_Login : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("main"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "F:\Gitlab\github\iCafe\Test-coffe\Views\Mobile\Login.cshtml"
  
    Layout = null;

#line default
#line hidden
#nullable disable
            WriteLiteral("<!DOCTYPE html>\r\n<html>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fdc9cdec0e489e8873ec9f0f991b7b117f9671f73692", async() => {
                WriteLiteral(@"
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>iCafe</title>
    <link rel=""stylesheet"" type=""text/css"" href=""css/bootstrap-4.4.1.min.css"">
    <link rel=""stylesheet"" type=""text/css"" href=""css/font-awesome/css/font-awesome.min.css"">
    <link rel=""stylesheet"" type=""text/css"" href=""css/sweetalert2v8.11.8.css"">
    <link rel=""stylesheet"" type=""text/css"" href=""css/custom-dropdown.css"">
    <link rel=""stylesheet"" href=""css/blue.css"">
    <link rel=""stylesheet"" type=""text/css"" href=""css/login.css"">
    <style type=""text/css"">
        /*#day_open {
            position: relative;
            width: 150px; height: 20px;
            color: white;
        }

        #day_open:before {
            position: absolute;
            top: 3px; left: 3px;
            content: attr(data-date);
            display: inline-block;
            color: black;
        }

        #day_open::-webkit-datetime-edit, #day_open::-webkit-inner-spin-");
                WriteLiteral(@"button, #day_open::-webkit-clear-button {
            display: none;
        }

        #day_open::-webkit-calendar-picker-indicator {
            position: absolute;
            top: 3px;
            right: 0;
            color: black;
            opacity: 1;
        }*/
        .hasDatepicker {
            background-color: #ECECEC !important;
            border-bottom: none !important;
            background-image: url(/Content/images/testdrive_icon_calenda_on.png) !important;
            background-position: right center !important;
            background-repeat: no-repeat !important;
            margin: 0 !important;
            text-indent: 20px;
            cursor: pointer;
        }

        .ui-widget-header {
            background: none;
            background-color: #dd5d5d;
            border: none;
        }

            .ui-widget-header .ui-icon {
                background-image: none;
            }

        .ui-icon, .ui-widget-content .ui-icon {
            ");
                WriteLiteral(@"background-image: none;
        }

        .ui-corner-all {
            border-bottom-right-radius: 0px;
            border-bottom-left-radius: 0px;
            border-top-right-radius: 0px;
            border-top-left-radius: 0px;
        }

        .ui-datepicker-header {
            background-color: #dd5d5d;
            color: #fff;
        }

        .ui-datepicker-prev, .ui-datepicker-next {
            background-color: #dd5d5d;
        }

            .ui-datepicker-prev:hover {
                background: url('/Content/images/arrow-left-24.png') no-repeat 5px 6px;
                background-size: 16px;
                border: none;
                cursor: pointer;
            }

            .ui-datepicker-next:hover {
                background: url('/Content/images/arrow-right-24.png') no-repeat 10px 6px;
                background-size: 16px;
                border: none;
                cursor: pointer;
            }

            .ui-datepicker-prev span, .ui-datepi");
                WriteLiteral(@"cker-next span {
                color: #fff;
            }

        .ui-datepicker {
            padding: 0;
        }


        .ui-datepicker-next {
            background: url('/Content/images/arrow-right-24.png') no-repeat 10px 6px;
            background-size: 16px;
        }

        .ui-datepicker-prev {
            background: url('/Content/images/arrow-left-24.png') no-repeat 5px 6px;
            background-size: 16px;
        }

        .ui-datepicker table {
            border-collapse: collapse;
            border: none;
        }

            .ui-datepicker table thead {
                background-color: #e5e5e5;
            }

            .ui-datepicker table tbody td .ui-state-default {
                border: none;
                background-color: #fff;
                text-align: center;
                padding: 5px;
                margin: 0;
                background: none;
            }

                .ui-datepicker table tbody td .ui-state-default");
                WriteLiteral(@":hover {
                    background-color: #dd5d5d;
                    /*border-radius: 3px;*/
                    color: #fff;
                }

        .ui-datepicker-year, .ui-datepicker-month {
            /*background-color: #dd5d5d;*/
            border: none;
            color: #fff;
        }

        .ui-datepicker table tbody td .ui-state-active {
            background-color: #dd5d5d;
            color: #fff;
            /*border-radius: 3px;*/
        }
    </style>

");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fdc9cdec0e489e8873ec9f0f991b7b117f9671f79446", async() => {
                WriteLiteral(@"
    <div class=""login-box"">
        <div class=""login-box-body"">
            <p class=""login-box-msg"">Đăng nhập hệ thống quản trị</p>
            <div id=""city"">
                <div class=""form-group custom-select-wrapper"">
                    <div class=""custom-select"">
                        <div class=""custom-select__trigger"">
                            <span>Chọn Thành Phố </span>
                            <div class=""arrow""></div>
                        </div>
                        <div class=""custom-options"">

                        </div>
                    </div>
                </div>
            </div>
            <div id=""shop"">
                <div class=""form-group custom-select-wrapper"">
                    <div class=""custom-select"">
                        <div class=""custom-select__trigger"">
                            <span>Chọn tên quán</span>
                            <div class=""arrow""></div>
                        </div>
                        <div ");
                WriteLiteral(@"class=""custom-options"">

                        </div>
                    </div>
                </div>
            </div>
            <!-- <div class=""form-group has-feedback"">
                <input type=""text"" id=""day_open"" class=""form-control"">
                <span class=""glyphicon glyphicon-envelope form-control-feedback""></span>
            </div> -->
            <div class=""form-group has-feedback"">
                <input type=""text"" id=""username"" class=""form-control"" placeholder=""Tài khoản"">
                <span class=""glyphicon glyphicon-envelope form-control-feedback""></span>
            </div>
            <div class=""form-group has-feedback"">
                <input type=""password"" id=""password"" class=""form-control"" placeholder=""Mật khẩu"">
                <span class=""glyphicon glyphicon-lock form-control-feedback""></span>
            </div>
            <div class=""row"">
                <div class=""col-md-12"">
                    <div class=""checkbox icheck"">
               ");
                WriteLiteral("         <label");
                BeginWriteAttribute("class", " class=\"", 6754, "\"", 6762, 0);
                EndWriteAttribute();
                WriteLiteral(@">
                            <div class=""icheckbox_square-blue"" aria-checked=""false"" aria-disabled=""false"" style=""position: relative;"">
                                <input type=""checkbox"" value=""remember"" style=""position: absolute; top: -20%; left: -20%; display: block; width: 140%; height: 140%; margin: 0px; padding: 0px; background: rgb(255, 255, 255); border: 0px; opacity: 0;""><ins class=""		iCheck-helper"" style=""position: absolute; top: -20%; left: -20%; display: block; width: 140%; height: 140%; margin: 0px; padding: 0px; background: rgb(255, 255, 255); border: 0px; opacity: 0;""></ins>
                            </div> Ghi nhớ đăng nhập
                        </label>
                    </div>
                </div>
                <div class=""col-md-12"">
                    <button type=""submit"" onclick=""login();"" class=""btn btn-primary btn-block btn-flat"">Đăng nhập</button>
                </div>
            </div>
        </div>
    </div>
    <script type=""text/javascript"" src=""js/");
                WriteLiteral(@"jquery-3.4.1.js""></script>
    <script type=""text/javascript"" src=""js/jquery-ui-1.12.1.js""></script>
    <script type=""text/javascript"" src=""js/bootstrap-4.4.1.min.js""></script>
    <script type=""text/javascript"" src=""js/sweetalert-dev.js""></script>
    <script type=""text/javascript"" src=""js/custom.dropdown.js""></script>
    <script src=""js/icheck.min.js""></script>
    <!-- <script src=""https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.10.3/moment.min.js""></script> -->
    

    <script type=""text/javascript"">
        // $( document ).ready(function() {
        // 	$(""#day_open"").on(""change"", function() {
        // 	    this.setAttribute(
        // 	        ""data-date"",
        // 	        moment(this.value, ""YYYY-MM-DD"")
        // 	        .format( this.getAttribute(""data-date-format"") )
        // 	    )
        // 	}).trigger(""change"")
        // });

        var user = JSON.parse(localStorage.getItem('user'));
       // console.log(user);

        $('#day_open').datepicker({
  ");
                WriteLiteral(@"          dateFormat: ""dd/mm/yy"",
            // maxDate: ""+30d"",
            minDate: ""+1d"",
            prevText: ""Trước"",
            nextText: ""Sau"",
            currentText: ""Hôm nay"",
            monthNames: [""Tháng một"", ""Tháng hai"", ""Tháng ba"", ""Tháng tư"", ""Tháng năm"", ""Tháng sáu"", ""Tháng bảy"", ""Tháng tám"", ""Tháng chín"", ""Tháng mười"", ""Tháng mười một"", ""Tháng mười hai""],
            monthNamesShort: [""Một"", ""Hai"", ""Ba"", ""Bốn"", ""Năm"", ""Sáu"", ""Bảy"", ""Tám"", ""Chín"", ""Mười"", ""Mười một"", ""Mười hai""],
            dayNames: [""Chủ nhật"", ""Thứ hai"", ""Thứ ba"", ""Thứ tư"", ""Thứ năm"", ""Thứ sáu"", ""Thứ bảy""],
            dayNamesShort: [""CN"", ""Hai"", ""Ba"", ""Tư"", ""Năm"", ""Sáu"", ""Bảy""],
            dayNamesMin: [""CN"", ""T2"", ""T3"", ""T4"", ""T5"", ""T6"", ""T7""],
            weekHeader: ""Tuần"",
            dateFormat: ""dd/mm/yy"",
            firstDay: 1,
            showMonthAfterYear: false,
        });

        function login() {
            let un = $(""#username"").val();
            let pwd = $(""#password"").v");
                WriteLiteral(@"al();
           // let city_id = $(""#city .selected"").data(""value"");
            let shop_id = $(""#shop .selected"").data(""value"");
           // shop_id += """";
            let data_user = JSON.stringify({
                ""username"": un,
                ""password"": pwd,
                ""ShopsId"": shop_id
            });
            console.log(data_user);
           // console.log(data);
             $.ajax({
                    url: ""api/UsersApi/mobile/userlogin"", // endpoint
                    type: ""POST"",                   
                    dataType: ""json"",
                    contentType: ""application/json"",
                    data: data_user,
                    success: function (result) {
                        // success
                        console.log(result.name);
                        Swal.fire(
                            'Thông báo',
                            'Đăng nhập hệ thống thành công.',
                            'success'
                        );");
                WriteLiteral(@"
                        localStorage.setItem('user', data_user);
                        setTimeout(function () {
                        window.location.replace(""mobile/index"");
                        }, 2000);
                    },
                     error: function (errorData) {
                         //onError(errorData);
                         Swal.fire({
                            icon: 'error',
                            title: 'Cảnh báo',
                            text: 'Đăng nhập không thành công!',
                         })
                     }
            });

            //readTextFile(""json/user.json"", function (text) {
            //    let data = JSON.parse(text);
            //    data = data.filter(function (rs) {
            //        return rs.city_id == city_id && rs.shop_id == shop_id && rs.username == un && rs.password == pwd;
            //    });
            //    if (data.length > 0) {
            //        Swal.fire(
            //            '");
                WriteLiteral(@"Thông báo',
            //            'Đăng nhập hệ thống thành công.',
            //            'success'
            //        );
            //        localStorage.setItem('user', JSON.stringify(data));
            //        setTimeout(function () {
            //            window.location.replace(""mobile/index"");
            //        }, 2000);
            //    }
            //    else {
            //        Swal.fire({
            //            icon: 'error',
            //            title: 'Cảnh báo',
            //            text: 'Đăng nhập không thành công!',
            //        })
            //    }
            //});
        }
    </script>
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</html>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
