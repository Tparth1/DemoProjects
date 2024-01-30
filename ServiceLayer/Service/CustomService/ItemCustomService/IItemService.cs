using DomainLayer.Model;
using DomainLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service.CustomService.ItemCustomService
{
    public interface IItemService
    {
        Task<bool> AddItemAsync(ItemInsertViewModal itemInsertView, string photo);
        Task<ItemViewModal> GetItemByIdAsync(Guid id);
        Task<IEnumerable<ItemViewModal>> GetAllItemsAsync();
        Task<bool> DeleteItemAsync(ItemViewModal itemViewModal);
        Task<bool> UpdateItemAsync(ItemUpdateViewModal itemUpdateView);
    }
}
