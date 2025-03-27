using ServiceLayer.ViewModels.StoreViewModels;

namespace ServiceLayer.Services.Interfaces
{
    public interface IStoreService
    {
        bool CreateProperty(ManagePropertyByUserViewModel model);
    }
}
