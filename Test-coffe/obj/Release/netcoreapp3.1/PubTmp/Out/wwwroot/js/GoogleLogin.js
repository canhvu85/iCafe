//function onSignIn(googleUser) {
//    var profile = googleUser.getBasicProfile();
//    console.log(profile);
//    console.log(JSON.stringify(profile));
//    console.log('ID: ' + profile.getId()); // Do not send to your backend! Use an ID token instead.
//    console.log('Name: ' + profile.getName());
//    console.log('Image URL: ' + profile.getImageUrl());
//    console.log('Email: ' + profile.getEmail()); // This is null if the 'email' scope is not present.

//    $.ajax({
//        url: "/Login/GoogleLogin",
//        headers: {'X-Requested-With': 'XMLHttpRequest'},
//        type: "POST",
//        data: { 'name': profile.getName(), 'email': profile.getEmail() },
//        success: function (data) {
//            if (data.success === "True") {
//                console.log("login succes:  " + data);
//            }
//        },
//        error: function (data) {
//            console.log(data);
//        }
//    })
//}

//function signOut() {
//    var auth2 = gapi.auth2.getAuthInstance();
//    auth2.signOut().then(function () {
//        console.log('User signed out.');
//    });
//}

function onLoadGoogleCallback() {
    gapi.load('auth2', function () {
        auth2 = gapi.auth2.init({
            client_id: '693194563576-bghmj4rllkhsmre4hb3kc89nfos0g2hg.apps.googleusercontent.com',
            cookiepolicy: 'single_host_origin',
            scope: 'profile'
        });

        auth2.attachClickHandler(element, {},
            function (googleUser) {
                var profile = googleUser.getBasicProfile();
                console.log('ID: ' + profile.getId()); // Do not send to your backend! Use an ID token instead.
                console.log('Name: ' + profile.getName());
                console.log('Image URL: ' + profile.getImageUrl());
                console.log('Email: ' + profile.getEmail()); // This is null if the 'email' scope is not present.

                $.ajax({
                    url: "/Login/GoogleLogin",
                    headers: {'X-Requested-With': 'XMLHttpRequest'},
                    type: "POST",
                    data: { 'name': profile.getName(), 'email': profile.getEmail() }
                })
            }, function (error) {
                console.log('Sign-in error', error);
            }
        );
    });

    element = document.getElementById('ggSign');
}

function signOut() {
    var auth2 = gapi.auth2.getAuthInstance();
    auth2.signOut().then(function () {
        console.log('User signed out.');
        $.ajax({
            url: "/Login/GoogleLogout",
            headers: { 'X-Requested-With': 'XMLHttpRequest' },
            type: "POST"
        })
    });
}