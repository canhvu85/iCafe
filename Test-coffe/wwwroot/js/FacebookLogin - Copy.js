//$(document).ready(function () {
//    'use strict';
//    window.fbAsyncInit = function () {
//        FB.init({
//            appId: '279018506416227',
//            cookie: true,
//            xfbml: true,
//            version: 'v6.0'
//        });

//        FB.AppEvents.logPageView();

//    };


//    (function (d, s, id) {
//        var js, fjs = d.getElementsByTagName(s)[0];
//        if (d.getElementById(id)) { return; }
//        js = d.createElement(s); js.id = id;
//        js.src = "https://connect.facebook.net/en_US/sdk.js";
//        fjs.parentNode.insertBefore(js, fjs);
//    }(document, 'script', 'facebook-jssdk'));

//    function Login() {
//        FB.login(function (response) {
//            if (response.authResponse) {
//                getFacebookUserInfo();
//            } else {
//                console.log('User cancelled login or did not fully authorize.');
//            }
//        }, {
//                //scope: 'email,user_photos,publish_actions'
//                scope: 'email'
//            });
//    }

//    function getFacebookUserInfo() {
//        FB.api('/me?fields=email,name', function (response) {
//            var token = $('input[name="__RequestVerificationToken"]').val();
//            $.ajax({
//                url: "/Login/FacebookLogin",
//                headers: { "__RequestVerificationToken": token },
//                type: "POST",
//                data: { 'name': response.name, 'email': response.email },
//                success: function (data) {
//                    if(data.success === "True")
//                    {
//                        location.reload();
//                    }
//                },
//                error: function (data) {
//                    console.log(data);
//                }
//            })
//        });
//    }

//    function Logout() {
//        //FB.logout(function () { document.location.reload(); });
//        FB.getLoginStatus(function (response) {
//            if (response && response.status === 'connected') {
//                FB.logout(function (response) {
//                    document.location.reload();
//                });
//            }
//        });
//    }


//    $('#facebook').click(function () {
//        Login();
//    })

//    $('.lbtLogOutFacebook').click(function () {
//        Logout();
//        var token = $('input[name="__RequestVerificationToken"]').val();
//        $.ajax({
//            url: "/Login/FacebookLogout",
//            headers: { "__RequestVerificationToken": token },
//            type: "POST",
//            success: function (data) {
//                if (data.success === "True") {
//                    location.reload();
//                }
//            },
//            error: function (data) {
//                console.log(data);
//            }
//        })
//    })
//});








$(document).ready(function () {
    window.fbAsyncInit = function () {
        FB.init({
            appId: '279018506416227',
            cookie: true,  // enable cookies to allow the server to access the session
            xfbml: true,  // parse social plugins on this page
            version: 'v6.0' 
        });

        FB.getLoginStatus(function (response) {
            if (response.status === 'connected') {
                getFbUserData();
            }
        });
    };

    // Load the JavaScript SDK asynchronously
    (function (d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) { return; }
        js = d.createElement(s); js.id = id;
        js.src = "https://connect.facebook.net/en_US/sdk.js";
        fjs.parentNode.insertBefore(js, fjs);
    }(document, 'script', 'facebook-jssdk'));
});
    function fbLogin() {
        FB.login(function (response) {
            if (response.authResponse) {
                getFbUserData();
            } else {
                document.getElementById('status').innerHTML = 'User cancelled login or did not fully authorize.';
            }
        }, { scope: 'email' });
    }



    function getFbUserData() {
        FB.api('/me', { fields: 'id,name,first_name,last_name,email,link,gender,locale,picture' },
            function (response) {
                document.getElementById('fbLink').setAttribute("onclick", "fbLogout()");
                document.getElementById('fbLink').innerHTML = 'Logout from Facebook';
                document.getElementById('status').innerHTML = 'Thanks for logging in, ' + response.first_name + '!';
                document.getElementById('userData').innerHTML = '<p><b>FB ID:</b> ' + response.id + '</p><p><b>Name:</b> ' + response.first_name + ' ' + response.last_name + '</p><p><b>Email:</b> ' + response.email + '</p><p><b>Gender:</b> ' + response.gender + '</p><p><b>Locale:</b> ' + response.locale + '</p><p><b>Picture:</b> <img src="' + response.picture.data.url + '"/></p><p><b>FB Profile:</b> <a target="_blank" href="' + response.link + '">click to view profile</a></p>';

                var token = $('input[name="__RequestVerificationToken"]').val();
                $.ajax({
                    url: "/Login/FacebookLogin",
                    headers: { "__RequestVerificationToken": token },
                    type: "POST",
                    data: { 'name': response.name, 'email': response.email },
                    success: function (data) {
                        if(data.success === "True")
                        {
                            console.log("login succes:  " + data);
                        }
                    },
                    error: function (data) {
                        console.log(data);
                    }
                })
            });
    }

    function fbLogout() {
        FB.logout(function () {
            document.getElementById('fbLink').setAttribute("onclick", "fbLogin()");
            document.getElementById('fbLink').innerHTML = '<img src="http://saysua.com/demo/fblogin/facebook-sign-in.png"/>';
            document.getElementById('userData').innerHTML = '';
            document.getElementById('status').innerHTML = 'You have successfully logout from Facebook.';

            var token = $('input[name="__RequestVerificationToken"]').val();
            $.ajax({
                url: "/Login/FacebookLogout",
                headers: { "__RequestVerificationToken": token },
                type: "POST",
                success: function (data) {
                    if (data.success === "True") {
                        console.log("logout succes:  " + data);
                    }
                },
                error: function (data) {
                    console.log(data);
                }
            })
        });
    }