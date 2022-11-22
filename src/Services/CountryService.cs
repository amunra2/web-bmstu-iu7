using ServerING.Interfaces;
using ServerING.Models;
using System;
using System.Collections.Generic;
using System.Linq;

using ServerING.Exceptions;
using ServerING.DTO;


namespace ServerING.Services {
    public interface ICountryService {
        Country AddCountry(CountryDto country);
        Country UpdateCountry(CountryUpdateDto country);
        Country PatchUpdateCountry(CountryUpdateSparceDto country);
        Country DeleteCountry(int id);

        Country GetCountryByID(int id);
        IEnumerable<Country> GetAllCountries();

        Country GetCountryByName(string name);
        IEnumerable<Country> GetCountryByOverallPlayers(ushort overallPlayers);
        IEnumerable<Country> GetCountryByLevelOfInterest(int levelOfIntereset);
    }

    public class CountryService : ICountryService {
        private readonly ICountryRepository countryRepository;

        public CountryService(ICountryRepository countryRepository) {
            this.countryRepository = countryRepository;
        }

        private bool IsExist(Country country) {
            return countryRepository.GetAll()
                .Any(item =>
                    item.Name == country.Name &&
                    item.LevelOfInterest == country.LevelOfInterest &&
                    item.OverallPlayers == country.OverallPlayers
                    );
        }

        private bool IsExistById(int id) {
            return countryRepository.GetByID(id) != null;
        }

        public Country AddCountry(CountryDto countryDto) {
            var country = new Country()
            {
                Name = countryDto.Name,
                LevelOfInterest = countryDto.LevelOfInterest,
                OverallPlayers = countryDto.OverallPlayers
            };

            if (IsExist(country))
                throw new CountryAlreadyExistsException("Country already exists");

            return countryRepository.Add(country);
        }

        public Country DeleteCountry(int id) {
            return countryRepository.Delete(id);
        }

        public Country GetCountryByID(int id) {
            return countryRepository.GetByID(id);
        }

        public IEnumerable<Country> GetAllCountries() {
            return countryRepository.GetAll();
        }

        public Country UpdateCountry(CountryUpdateDto countryDto) {
            var country = new Country()
            {
                Id = countryDto.Id,
                Name = countryDto.Name,
                LevelOfInterest = countryDto.LevelOfInterest,
                OverallPlayers = countryDto.OverallPlayers
            };

            if (!IsExistById(country.Id))
                throw new CountryNotExistsException("No country with such id");

            if (IsExist(country))
                throw new CountryAlreadyExistsException("Country already exists");

            return countryRepository.Update(country);
        }

        public Country PatchUpdateCountry(CountryUpdateSparceDto countrySparceDto) {
            if (!IsExistById(countrySparceDto.Id))
                throw new CountryNotExistsException("No country with such id");

            var dbCountry = GetCountryByID(countrySparceDto.Id);

            var country = new Country()
            {
                Id = countrySparceDto.Id,
                Name = countrySparceDto.Name ?? dbCountry.Name,
                LevelOfInterest = countrySparceDto.LevelOfInterest ?? dbCountry.LevelOfInterest,
                OverallPlayers = countrySparceDto.OverallPlayers ?? dbCountry.OverallPlayers
            };

            if (IsExist(country))
                throw new CountryAlreadyExistsException("Country already exists");

            return countryRepository.Update(country);
        }

        public Country GetCountryByName(string name) {
            return countryRepository.GetByName(name);
        }

        public IEnumerable<Country> GetCountryByOverallPlayers(ushort overallPlayers) {
            return countryRepository.GetByOverallPlayers(overallPlayers);
        }

        public IEnumerable<Country> GetCountryByLevelOfInterest(int levelOfInterest) {
            return countryRepository.GetByLevelOfInterest(levelOfInterest);
        }
    }
}
