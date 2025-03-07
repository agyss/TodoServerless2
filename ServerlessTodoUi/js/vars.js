// Put the urls to your local api and your remote API here
var localUrl = "http://localhost:7176/api/todoitem";
var remoteUrl = "https://todolistapigateway.azure-api.net/habschertodolist/todoitem";
var apiUrl = "";


if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
    apiUrl = localUrl;
} else {
    apiUrl = remoteUrl;
}