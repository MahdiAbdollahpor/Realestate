using DataLayer.Context;
using DataLayer.Models.Store;
using DataLayer.Models.Store.enumProperty;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.PublicClasses;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.StoreViewModels;

namespace ServiceLayer.Services
{
    public class StoreService : IStoreService
    {
        private readonly ApplicationDbContext _db;
        private readonly IFileUploader _fileUploader;


        public StoreService(ApplicationDbContext db, IFileUploader fileUploader)
        {
            _db = db;
            _fileUploader = fileUploader;
        }

        public bool CreateProperty(ManagePropertyByUserViewModel model)
        {
            if (model != null)
            {
                string img1 = UploadFile(model.IndexImage1);
                string img2 = UploadFile(model.IndexImage2);
                string img3 = UploadFile(model.IndexImage3);

                Property property = new Property
                {
                    Type = model.Type,
                    TransactionType = model.TransactionType,
                    Address = model.Address,
                    Price = model.Price,
                    Description = model.Description,
                    Meterage = model.Meterage,
                    Floor = model.Floor,
                    Room = model.Room,
                    Parking = model.Parking,
                    warehouse = model.warehouse,
                    elevator = model.elevator,
                    Bathroom = model.Bathroom,
                    IndexImage1 = img1,
                    IndexImage2 = img2,
                    IndexImage3 = img3,
                    UserId = model.UserId
                };
                _db.Properties.Add(property);
                _db.SaveChanges();
                return true;
            }
            return false;
        }




        public string UploadFile(IFormFile files)
        {
            
            string path = "\\appdata\\Property\\";
            string fileNames = _fileUploader.UploadFile(files, path);
            return fileNames;
        }
    }
}

