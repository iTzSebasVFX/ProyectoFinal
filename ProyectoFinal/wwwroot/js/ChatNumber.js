const input = document.getElementById("message");
const sendButton = document.getElementById("sendButton");

const whatsappBtn = document.getElementById("whatsappBtn");
const whatsappBox = document.getElementById("whatsappBox");
const sendPhone = document.getElementById("sendPhone");
const phoneInput = document.getElementById("phoneInput");
const phoneError = document.getElementById("phoneError");
const messagesList = document.getElementById("messagesList"); // Asegurate de tener este UL en tu HTML

// Enviar mensaje común
sendButton.addEventListener("click", sendMessage);
input.addEventListener("keypress", function (e) {
  if (e.key === "Enter") sendMessage();
});

function sendMessage() {
  const message = input.value.trim();
  if (message) {
    console.log("Mensaje enviado:", message);
    input.value = "";
  }
}

// Mostrar caja para número de WhatsApp
whatsappBtn.addEventListener("click", () => {
  whatsappBox.style.display = "block";
  phoneInput.focus();
});

// Enviar número por botón o Enter
sendPhone.addEventListener("click", sendPhoneNumber);
phoneInput.addEventListener("keypress", function (e) {
  if (e.key === "Enter") sendPhoneNumber();
});

function sendPhoneNumber() {
  const phone = phoneInput.value.trim();
  const numericPhone = phone.replace(/[^0-9]/g, "");

  if (numericPhone === "" || numericPhone.length < 6) {
    phoneError.textContent = "Por favor ingresá un número válido.";
    return;
  }

  phoneError.textContent = ""; // Limpiar error si todo bien

  const whatsappLink = `https://wa.me/${numericPhone}`;
  const li = document.createElement("li");
  li.innerHTML = `🔗 <a href="${whatsappLink}" target="_blank">Ir a tu WhatsApp</a>`;
  messagesList.appendChild(li);

  phoneInput.value = "";
  whatsappBox.style.display = "none";
}