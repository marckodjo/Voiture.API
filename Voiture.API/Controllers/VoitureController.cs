using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Voiture.API.Models;

namespace Voiture.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class VoitureController : ControllerBase
    {
        private static readonly List<Models.Voiture> voitures = new()
        {
            new Models.Voiture { Id = 1, Marque = "Toyota", Modele = "Corolla", Annee = 2020, Couleur = "Rouge" },
            new Models.Voiture { Id = 2, Marque = "Honda", Modele = "Civic", Annee = 2019, Couleur = "Bleu" },
            new Models.Voiture { Id = 3, Marque = "Ford", Modele = "Focus", Annee = 2021, Couleur = "Noir" },
            new Models.Voiture { Id = 4, Marque = "Chevrolet", Modele = "Malibu", Annee = 2018, Couleur = "Blanc" },
        };

        [HttpGet("voitures")]
        public ActionResult<IEnumerable<Models.Voiture>> GetVoitures()
        {
            return Ok(voitures);
        }

        [HttpGet("voitures/{Id}")]
        public ActionResult<Models.Voiture> GetVoiture(int Id)
        {
            Models.Voiture voiture = voitures.FirstOrDefault(x => x.Id == Id);
            //if (voiture == null)
            //{
            //    return NotFound("Voiture non trouvée!");
            //}
            return voiture == null? NotFound("Voiture non trouvée!"): Ok(voiture);
        }

        [HttpPost("voitures")]
        public ActionResult<List<Models.Voiture>> AddVoiture([FromBody] Models.Voiture voiture)
        {
            voitures.Add(voiture);
            return Ok(voitures);
        }


        [HttpPut("voitures/{Id}")]
        public ActionResult<Models.Voiture> UpdateVoiture(int Id, Models.Voiture voiture)
        {
            if (Id != voiture.Id)
                return BadRequest();

            var voitureExistante = voitures.FindIndex(x=>x.Id == Id);
            if (voitureExistante == -1)
                return NotFound();

            voitures[voitureExistante] = voiture;

            return Ok(voitures[voitureExistante]);
        }


        [HttpDelete("voitures/{Id}")]
        public IActionResult DeleteVoiture(int Id)
        {
            var voiture = voitures.FirstOrDefault(x => x.Id == Id);
            if (voiture == null)
              return NotFound("Voiture non trouvée!!");

            voitures.Remove(voiture);

            return NoContent();
        }
    }
}
