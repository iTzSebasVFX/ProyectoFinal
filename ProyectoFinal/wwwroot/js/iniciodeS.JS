var modalLogin = document.getElementById("myModal");
var btnLogin = document.getElementById("openModalBtn");
var spanLogin = modalLogin.querySelector(".close");

btnLogin.onclick = function () {
    modalLogin.style.display = "block";
};

spanLogin.onclick = function () {
    modalLogin.style.display = "none";
};

window.onclick = function (event) {
    if (event.target == modalLogin) {
        modalLogin.style.display = "none";
    }
};