let toggle = document.getElementById('toggle');

// Verificar si ya se guardó el estado en el localStorage
let darkMode = localStorage.getItem('darkMode') === 'true';

// Si el modo oscuro ya está activado, aplicarlo
if (darkMode) {
    document.body.classList.add('dark');
    toggle.checked = true;  // Asegúrate de que el checkbox esté marcado
}

// Escuchar el cambio del checkbox y actualizar el estado
toggle.addEventListener('change', (event) => {
    let checked = event.target.checked;
    document.body.classList.toggle('dark', checked);  // Aplica o quita el modo oscuro

    // Guardar el estado en localStorage
    localStorage.setItem('darkMode', checked);
});