
    document.querySelectorAll('a, button[type="submit"]').forEach(el => {
        el.addEventListener('click', function (e) {
            const body = document.body;
            // Evita la navegación inmediata
            e.preventDefault();

            // Aplica la animación de salida
            body.setAttribute("transition-style", "out:diamond:hesitate");

            
            setTimeout(() => {
                if (el.tagName.toLowerCase() === 'a') {
                    window.location = el.href;
                } else if (el.type === "submit") {
                    el.closest("form").submit();
                }
            }, 1500); 
        });
    });

