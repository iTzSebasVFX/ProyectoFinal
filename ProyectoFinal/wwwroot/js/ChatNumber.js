//limpieza del imput

const input = document.getElementById("message");
  const sendButton = document.getElementById(id="sendButton");

  sendButton.addEventListener("click", () => {
    const message = input.value.trim();

    if (message) {
      // Acá podés hacer lo que necesites con el mensaje
      console.log("Mensaje enviado:", message);

      // Limpia el input
      input.value = "";
    }
  });

  //name

  const userInput = document.getElementById("user");

  userInput.addEventListener("change", function () {
    if (userInput.value.trim() !== "") {
      userInput.style.display = "none";
    }
  });

// wsp
  const whatsappBtn = document.getElementById("whatsappBtn");
  const whatsappBox = document.getElementById("whatsappBox");
  const sendPhone = document.getElementById("sendPhone");
  const phoneInput = document.getElementById("message");
  const messagesList = document.getElementById("messagesList");

  // Mostrar input cuando se hace click en el botón
  whatsappBtn.addEventListener("click", () => {
    whatsappBox.style.display = "block";
    phoneInput.focus();
  });

  // Enviar número y mostrar link en el chat
  sendPhone.addEventListener("click", () => {
    const phone = phoneInput.value.trim();
    if (phone !== "") {
      const whatsappLink = `https://wa.me/${phone.replace(/[^0-9]/g, "")}`;
      const li = document.createElement("li");
      li.innerHTML = `🔗 <a href="${whatsappLink}" target="_blank">Ir a tu WhatsApp</a>`;
      messagesList.appendChild(li);

      // Limpiar y ocultar el input
      phoneInput.value = "";
      whatsappBox.style.display = "none";
    }
  });