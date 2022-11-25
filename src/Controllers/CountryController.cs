using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using ServerING.DTO;
using ServerING.Exceptions;
using ServerING.Models;
using ServerING.Services;

namespace ServerING.Controllers
{
    [ApiController]   
    [Route("/api/v1/countries")]
    public class CountryController : Controller
    {
        private ICountryService countryService;

        public CountryController(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Country>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        public IActionResult GetAll()
        {
            var countries = countryService.GetAllCountries();
            return countries.Any() ? Ok(countries) : NoContent();
        }

        [HttpPost]
        [ProducesResponseType(typeof(Country), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Add(CountryDto countryDto)
        {
            try
            {
                return Ok(countryService.AddCountry(countryDto));
            }
            catch (CountryAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Country), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var country = countryService.GetCountryByID(id);
            return country != null ? Ok(country) : NotFound();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Country), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Put(int id, CountryDto countryDto) {
            try
            {
                var countryUpdateDto = new CountryUpdateDto()
                {
                    Id = id,
                    Name = countryDto.Name,
                    LevelOfInterest = countryDto.LevelOfInterest,
                    OverallPlayers = countryDto.OverallPlayers
                };

                return Ok(countryService.UpdateCountry(countryUpdateDto));
            }
            catch (CountryAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (CountryNotExistsException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(Country), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Patch(int id, CountrySparceDto countryDto) {
            try
            {
                var countryUpdateDto = new CountryUpdateSparceDto()
                {
                    Id = id,
                    Name = countryDto.Name,
                    LevelOfInterest = countryDto.LevelOfInterest,
                    OverallPlayers = countryDto.OverallPlayers
                };

                return Ok(countryService.PatchUpdateCountry(countryUpdateDto));
            }
            catch (CountryAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (CountryNotExistsException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Country), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var deletedCountry = countryService.DeleteCountry(id);
            return deletedCountry != null ? Ok(deletedCountry) : NotFound();
        }

    }
}