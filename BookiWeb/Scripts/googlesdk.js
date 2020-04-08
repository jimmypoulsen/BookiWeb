var GoogleAuth; // Google Auth object.
var SCOPE = 'https://www.googleapis.com/auth/drive.metadata.readonly';

function handleClientLoad() {
    // Load the API's client and auth2 modules.
    // Call the initClient function after the modules load.
    gapi.load('client:auth2', initClient);
}

function initClient() {
    // Retrieve the discovery document for version 3 of Google Drive API.
    // In practice, your app can retrieve one or more discovery documents.
    var discoveryUrl = 'https://www.googleapis.com/discovery/v1/apis/drive/v3/rest';
    var signInButton = document.getElementById('google-sign-in');

    // Initialize the gapi.client object, which app uses to make API requests.
    // Get API key and client ID from API Console.
    // 'scope' field specifies space-delimited list of access scopes.
    gapi.client.init({
        'apiKey': google_api_key,
        'clientId': google_client_id,
        'discoveryDocs': [discoveryUrl],
        'scope': SCOPE
    }).then(function () {
        GoogleAuth = gapi.auth2.getAuthInstance();

        // Listen for sign-in state changes.
        GoogleAuth.isSignedIn.listen(updateSigninStatus);

        // Handle initial sign-in state. (Determine if user is already signed in.)
        var user = GoogleAuth.currentUser.get();
        setSigninStatus();

        // Call handleAuthClick function when user clicks on
        //      "Sign In/Authorize" button.
        signInButton.addEventListener('click', () => {
            handleAuthClick();
        });

        revokeAccessButton.addEventListener('click', () => {
            handleRevokeAccessClick();
        });
    });
}

function handleAuthClick() {
    if (GoogleAuth.isSignedIn.get()) {
        // User is authorized and has clicked "Sign out" button.
        GoogleAuth.signOut();
    } else {
        // User is not signed in. Start Google auth flow.
        GoogleAuth.signIn();
    }
}

function handleRevokeAccessClick() {
    GoogleAuth.disconnect();
}

function setSigninStatus(isSignedIn) {
    var user = GoogleAuth.currentUser.get();
    var isAuthorized = user.hasGrantedScopes(SCOPE);
    var authStatus = document.getElementById('google-auth-status');
    var signInButton = document.getElementById('google-sign-in');
    var revokeAccessButton = document.getElementById('google-revoke-access');
    if (isAuthorized) {
        signInButton.innerHTML = "Sign out";
        revokeAccessButton.classList.toggle('d-none');
        authStatus.innerHTML = "You are currently signed in and have granted access to this app";
        authStatus.classList.remove('d-none');
    } else {
        signInButton.innerHTMl = "Sign In/Authorize";
        revokeAccessButton.classList.toggle('d-none');
        authStatus.innerHTML = "You have not authorized this app or you are signed out";
    }
}

function updateSigninStatus(isSignedIn) {
    setSigninStatus();
}