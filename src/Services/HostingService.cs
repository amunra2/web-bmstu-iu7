using ServerING.DTO;
using ServerING.Exceptions;
using ServerING.Interfaces;
using ServerING.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ServerING.Services {
    public interface IHostingService {
        WebHosting AddHosting(HostingFormDto hosting);
        WebHosting PatchHosting(int id, HostingFormDto hosting);
        WebHosting PutHosting(int id, HostingFormDto hosting);
        WebHosting DeleteHosting(int id);

        WebHosting GetHostingByID(int id);
        IEnumerable<WebHosting> GetAllHostings();

        WebHosting GetHostingByName(string name);
        IEnumerable<WebHosting> GetHostingByPricePerMonth(int pricePerMonth);
        IEnumerable<WebHosting> GetHostingBySubMonths(ushort subMonths);
    }


    public class HostingService : IHostingService {
        private readonly IHostingRepository hostingRepository;

        public HostingService(IHostingRepository hostingRepository) {
            this.hostingRepository = hostingRepository;
        }

        private bool IsExist(HostingFormDto hosting) {
            return hostingRepository.GetAll()
                .Any(item => item.Name == hosting.Name);
        }

        private bool IsExistById(int id) {
            return hostingRepository.GetByID(id) != null;
        }

        public WebHosting AddHosting(HostingFormDto hosting) {
            if (IsExist(hosting)) {
                var conflictedId = hostingRepository.GetByName(hosting.Name).Id;
                throw new HostingConflictException(conflictedId);
            }

            var transferedHosting = new WebHosting {
                Name = hosting.Name,
                PricePerMonth = hosting.PricePerMonth.Value,
                SubMonths = (ushort) hosting.SubMonths.Value
            };

            return hostingRepository.Add(transferedHosting);
        }

        public WebHosting PutHosting(int id, HostingFormDto hosting) {
            if (IsExist(hosting)) {
                var conflictedId = hostingRepository.GetByName(hosting.Name).Id;
                throw new HostingConflictException(conflictedId);
            }

            if (!IsExistById(id))
                throw null;

            var transferedHosting = new WebHosting {
                Id = id,
                Name = hosting.Name,
                PricePerMonth = hosting.PricePerMonth != null ? hosting.PricePerMonth.Value : 0,
                SubMonths = hosting.SubMonths != null ? (ushort) hosting.SubMonths.Value : (ushort) 0
            };

            return hostingRepository.Update(transferedHosting);
        }

        public WebHosting PatchHosting(int id, HostingFormDto hosting) {
            if (IsExist(hosting)) {
                var conflictedId = hostingRepository.GetByName(hosting.Name).Id;
                throw new HostingConflictException(conflictedId);
            }

            if (!IsExistById(id))
                return null;

            var existedHosting = GetHostingByID(id);

            var transferedHosting = new WebHosting {
                Id = id,
                Name = hosting.Name != null ? hosting.Name : existedHosting.Name,
                PricePerMonth = hosting.PricePerMonth != null ? hosting.PricePerMonth.Value : existedHosting.PricePerMonth,
                SubMonths = hosting.SubMonths != null ? (ushort) hosting.SubMonths.Value : existedHosting.SubMonths
            };

            return hostingRepository.Update(transferedHosting);
        }

        public WebHosting DeleteHosting(int id) {
            return hostingRepository.Delete(id);
        }

        public IEnumerable<WebHosting> GetAllHostings() {
            return hostingRepository.GetAll();
        }

        public WebHosting GetHostingByID(int id) {
            return hostingRepository.GetByID(id);
        }

        public IEnumerable<WebHosting> GetHostingByPricePerMonth(int pricePerMonth) {
            return hostingRepository.GetByPricePerMonth(pricePerMonth);
        }

        public IEnumerable<WebHosting> GetHostingBySubMonths(ushort subMonths) {
            return hostingRepository.GetBySubMonths(subMonths);
        }

        public WebHosting GetHostingByName(string name) {
            return hostingRepository.GetByName(name);
        }
    }
}
