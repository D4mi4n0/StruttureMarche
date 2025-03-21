using Marche.modelli;

namespace StruttureMarche.ViewModels
{
    public class RicercaViewModel
    {
        public string Denominazione { get; set; }
        public string Comune { get; set; }
        public string Provincia { get; set; }
        public List<ModelliServiziMarche> Strutture { get; set; }
    }
}
