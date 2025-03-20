using Marche.modelli;

namespace StruttureMarche.ViewModels
{
    public class RicercaViewModel
    {
        public string Denominazione { get; set; } // Add this line
        public string Comune { get; set; } // Add this line
        public string Provincia { get; set; } // Add this line
        public List<ModelliServiziMarche> Strutture { get; set; }
    }

}
