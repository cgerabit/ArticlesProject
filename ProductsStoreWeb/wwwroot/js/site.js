// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const apiBase = "https://localhost:7149/api"; 
let currentUserId = null;
let currentUserRoles = [];
function getToken() {
    return localStorage.getItem("token");
}

function setAuthHeader(xhr) {
    const token = getToken();
    if (token) {
        xhr.setRequestHeader("Authorization", "Bearer " + token);
    }
}

document.addEventListener("DOMContentLoaded", function () {
    const token = localStorage.getItem("token");

    if (token) {
        $.ajax({
            url: "https://localhost:7149/api/auth/me",
            method: "GET",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Bearer " + token);
            },
            success: function (user) {
                $("#userEmail").text(user.email);
                $("#userEmailDisplay").removeClass("d-none");
                $("#logoutLink").removeClass("d-none");
                $("#manageArticlesLink").removeClass("d-none");
                $("#loginLink, #registerLink").addClass("d-none");

                currentUserId = user.userId;
                currentUserRoles = user.roles || [];
            },
            error: function () {
                logout();
            }
        });
    }
});

$.ajaxSetup({
    beforeSend: function (xhr) {
        const token = localStorage.getItem("token");
        if (token) {
            xhr.setRequestHeader("Authorization", "Bearer " + token);
        }
    }
});
function logout() {
    localStorage.removeItem("token");
    location.href = "/auth/login";
}

