function handleLogout() {
    switch (Cookies.get("AuthenticatedWith")) {
        case "Facebook":
            FB.getLoginStatus(function (response) {
                if (response.status == 'connected') {
                    FB.logout(function (response) {
                        document.getElementById('logout_form').submit();
                    });
                }
            });
            break;
        case "Google":
            GoogleAuth.signOut();
            document.getElementById('logout_form').submit();
            break;
        default:
    }
}