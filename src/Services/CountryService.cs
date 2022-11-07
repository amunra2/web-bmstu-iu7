using ServerING.Interfaces;
using ServerING.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ServerING.Services {
    public interface ICountryService {
        void AddCountry(Country country);
        Country DeleteCountry(Country country);
        void UpdateCountry(Country country);

        Country GetCountryByID(int id);
        IEnumerable<Country> GetAllCountryes();

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

        public void AddCountry(Country country) {
            if (IsExist(country))
                throw new Exception("Such country is already exist");
            else
                countryRepository.Add(country);
        }

        public Country DeleteCountry(Country country) {
            if (!IsExistById(country.Id))
                throw new Exception("No such country");

            return countryRepository.Delete(country.Id);
        }

        public Country GetCountryByID(int id) {
            return countryRepository.GetByID(id);
        }

        public IEnumerable<Country> GetAllCountryes() {
            return countryRepository.GetAll();
        }

        public void UpdateCountry(Country country) {
            if (!IsExistById(country.Id))
                throw new Exception("No such country");

            countryRepository.Update(country);
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
