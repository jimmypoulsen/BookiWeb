window.fbAsyncInit = function() {
    FB.init({
        appId      : facebook_app_id,
        cookie     : true,
        xfbml      : true,
        version    : facebook_api_version
    }, { scope: 'user_about_me,email,public_profile' });
        
    FB.AppEvents.logPageView();
};

(function(d, s, id){
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) {return;}
    js = d.createElement(s); js.id = id;
    js.src = "https://connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));

function checkLoginState() {
    FB.getLoginStatus(function(response) {
        console.log(JSON.stringify(response, null, 2));

        if (response.status == "connected") {
            let userId = response.authResponse.userID;
            console.log(userId);
            console.log("connected");
            FB.api('/me', { fields: 'email, name' }, function (response) {
                let email = response.email;
                let name = response.name;

                console.log(email);

                $.get(`https://localhost:44314/api/customers/email?email=${email}`, function (data) {
                    if (data.length) {
                        // User exists in database
                        // TODO: Login user
                        console.log(data[0]);
                        Cookies.set('AuthCookies', `email=${email}&customerId=${data[0].Id}`);
                        Cookies.set('FacebookAuth', 1);
                        document.location.href = "/";
                    } else {
                        // User doesn't exist in database
                        // TODO: Register user
                        let modal = $('#registerModal');
                        // fill in relevant information collected from Facebook
                        modal.find('#Name').val(name);
                        modal.find('#Email').val(email);
                        modal.find('#Password').val(email);
                        modal.find('#FacebookUserID').val(userId);
                        modal.modal('show');
                    }
                });
            });
        }
    });
}