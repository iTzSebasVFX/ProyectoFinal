// Modal de registro
var modalRegistro = document.getElementById("abrirR");
var btnRegistro = document.getElementById("Registro");
var spanRegistro = modalRegistro.querySelector(".close");

btnRegistro.onclick = function () {
    modalRegistro.style.display = "block";
};

spanRegistro.onclick = function () {
    modalRegistro.style.display = "none";
};

window.onclick = function (event) {
    if (event.target == modalRegistro) {
        modalRegistro.style.display = "none";
    }
};
