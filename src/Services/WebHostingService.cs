using ServerING.Interfaces;
using ServerING.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ServerING.Services {
    public interface IWebHostingService {
        void AddWebHosting(WebHosting webHosting);
        WebHosting DeleteWebHosting(WebHosting webHosting);
        void UpdateWebHosting(WebHosting webHosting);

        WebHosting GetWebHostingByID(int id);
        IEnumerable<WebHosting> GetAllWebHostings();

        WebHosting GetWebHostingByName(string name);
        IEnumerable<WebHosting> GetWebHostingByPricePerMonth(int pricePerMonth);
        IEnumerable<WebHosting> GetWebHostingBySubMonths(ushort subMonths);
    }


    public class WebHostingService : IWebHostingService {
        private readonly IWebHostingRepository webHostingRepository;

        private bool IsExist(WebHosting webHosting) {
            return webHostingRepository.GetAll()
                .Any(item =>
                    item.Name == webHosting.Name &&
                    item.PricePerMonth == webHosting.PricePerMonth &&
                    item.SubMonths == webHosting.SubMonths
                    );
        }


        private bool IsExistById(int id) {
            return webHostingRepository.GetByID(id) != null;
        }


        public WebHostingService(IWebHostingRepository webHostingRepository) {
            this.webHostingRepository = webHostingRepository;
        }

        public void AddWebHosting(WebHosting webHosting) {
            if (IsExist(webHosting))
                throw new Exception("Such webHosting is already exist");

            webHostingRepository.Add(webHosting);
        }

        public WebHosting DeleteWebHosting(WebHosting webHosting) {
            if (!IsExistById(webHosting.Id))
                throw new Exception("No such webHosting");

            return webHostingRepository.Delete(webHosting.Id);
        }

        public IEnumerable<WebHosting> GetAllWebHostings() {
            return webHostingRepository.GetAll();
        }

        public WebHosting GetWebHostingByID(int id) {
            return webHostingRepository.GetByID(id);
        }

        public IEnumerable<WebHosting> GetWebHostingByPricePerMonth(int pricePerMonth) {
            return webHostingRepository.GetByPricePerMonth(pricePerMonth);
        }

        public IEnumerable<WebHosting> GetWebHostingBySubMonths(ushort subMonths) {
            return webHostingRepository.GetBySubMonths(subMonths);
        }

        public void UpdateWebHosting(WebHosting webHosting) {
            if (!IsExistById(webHosting.Id))
                throw new Exception("No such webHosting");

            webHostingRepository.Update(webHosting);
        }

        public WebHosting GetWebHostingByName(string name) {
            return webHostingRepository.GetByName(name);
        }
    }
}
