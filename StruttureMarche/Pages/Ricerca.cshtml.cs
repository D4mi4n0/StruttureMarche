using Marche.modelli;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StruttureMarche.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace StruttureMarche.Pages
{
    public class RicercaModel : PageModel
    {
        private const int PageSize = 10;

        [BindProperty]
        public RicercaViewModel Ricerca { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Denominazione { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Comune { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Provincia { get; set; }
        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;
        public int TotalPages { get; set; }

        public async Task OnGetAsync()
        {
            var strutture = await GetFilteredStrutture(Denominazione, Comune, Provincia);
            TotalPages = (int)Math.Ceiling(strutture.Count / (double)PageSize);

            Ricerca = new RicercaViewModel
            {
                Strutture = strutture.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList()
            };
        }

        private async Task<List<Marche.modelli.ModelliServiziMarche>> GetFilteredStrutture(string denominazione, string comune, string provincia)
        {
            var allStrutture = await GetAllStrutture(); // Metodo per ottenere tutte le strutture
            var filteredStrutture = allStrutture.AsQueryable();

            if (!string.IsNullOrEmpty(denominazione))
            {
                filteredStrutture = filteredStrutture.Where(s =>
                    s.Denominazione.Contains(denominazione, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(comune))
            {
                filteredStrutture = filteredStrutture.Where(s =>
                    s.Comune.Contains(comune, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(provincia))
            {
                filteredStrutture = filteredStrutture.Where(s =>
                    s.Provincia.Contains(provincia, StringComparison.OrdinalIgnoreCase));
            }

            return filteredStrutture.ToList();
        }

        private async Task<List<Marche.modelli.ModelliServiziMarche>> GetAllStrutture()
        {
            var strutture = await FunzioniInterrogazioniServiziMarche.DaiTuttiIServizi();
            return strutture.ToList();
        }
    }
}
