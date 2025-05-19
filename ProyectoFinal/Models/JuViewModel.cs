namespace ProyectoFinal.Models{
    public class JuViewModel{
        public List<Juego> ListaJuegos {get; set;} = new List<Juego>();

        public Juego EditarJuego {get; set;} = new Juego();
    }
}