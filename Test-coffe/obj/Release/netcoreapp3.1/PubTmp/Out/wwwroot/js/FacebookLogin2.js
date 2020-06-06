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
                console.log('User cancelled login or did not fully authorize.');
            }
        }, { scope: 'email' });
    }



    function getFbUserData() {
        FB.api('/me', { fields: 'id,name,first_name,last_name,email,link,gender,locale,picture' },
            function (response) {
                console.log("info  " + JSON.stringify(response));
                var token = $('input[name="__RequestVerificationToken"]').val();
                console.log(token);
                $.ajax({
                    url: "/Login/FacebookLogin",
                    //headers: { "__RequestVerificationToken": token },
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