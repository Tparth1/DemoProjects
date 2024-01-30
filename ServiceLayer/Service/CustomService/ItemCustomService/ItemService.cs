using DomainLayer.Model;
using DomainLayer.ViewModel;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.IRepository;
using RepositoryLayer.Repository;
using ServiceLayer.Service.CustomService.CategoryCustomService;
using ServiceLayer.Service.GeneralService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ServiceLayer.Service.CustomService.ItemCustomService
{
    public class ItemService : IItemService
    {
        private readonly IUserRepository<Item> _item;
        private readonly IUserRepository<User> _user;
        private readonly IUserRepository<UserType> _userType;
        private readonly IUserRepository<SupplierItem> _supplierRepo;
        private readonly IUserRepository<CustomerItem> _customerRepo;
        private readonly IUserRepository<ItemImage> _itemImage;


        public ItemService(IUserRepository<Item> item, IUserRepository<User> user, IUserRepository<ItemImage> itemImage, IUserRepository<UserType> userType, IUserRepository<SupplierItem> supplierRepo, IUserRepository<CustomerItem> customerRepo)
        {
            _item = item;
            _user = user;
            _userType = userType;
            _supplierRepo = supplierRepo;
            _customerRepo = customerRepo;
            _itemImage = itemImage;
        }

        public async Task<bool> AddItemAsync(ItemInsertViewModal itemInsertView, string photo)
        {
            try
            {
                var user = await _user.Find(x => x.ID == itemInsertView.UserId);
                var userType = await _userType.Find(x => x.ID == user.UserTypeId);

                if (user == null || userType == null)
                {
                    return false;
                }

                var newItem = new Item
                {
                    ItemName = itemInsertView.ItemName,
                    ItemCode = itemInsertView.ItemCode,
                    ItemDescription = itemInsertView.ItemDescription,
                    ItemPrice = itemInsertView.ItemPrice,
                    CategoryId = itemInsertView.CategoryId,
                    IsActive = itemInsertView.IsActive,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                };

                var isItemAdded = await _item.AddUser(newItem);

                if (isItemAdded)
                {
                    if (userType.UserTypeName == "Supplier")
                    {
                        SupplierItem supplierItem = new()
                        {
                            ItemId = newItem.ID,
                            UserId = itemInsertView.UserId,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,
                            IsActive = itemInsertView.IsActive
                        };

                        ItemImage itemImage = new()
                        {
                            ItemImageId = newItem.ID,
                            ItemImages = photo,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,
                            IsActive = itemInsertView.IsActive
                        };

                        var resultItemImage = await _itemImage.AddUser(itemImage);

                        if (resultItemImage)
                        {
                            await _supplierRepo.AddUser(supplierItem);
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (user.UserType.UserTypeName == "Customer")
                    {
                        CustomerItem customerItem = new CustomerItem
                        {
                            UserId = user.ID,
                            ItemId = newItem.ID,
                            User = user,
                            Item = newItem
                        };

                        await _customerRepo.AddUser(customerItem);
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteItemAsync(ItemViewModal itemViewModal)
        {
            try
            {
                var item = await _item.GetById(itemViewModal.ItemId);

                if (item == null)
                {
                    return false;
                }

                return await _item.DeleteUser(item);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<ItemViewModal>> GetAllItemsAsync()
        {
            var items = await _item.GetAllUser();
            return items.Select(x => new ItemViewModal
            {
                
                ItemName = x.ItemName,
                ItemCode = x.ItemCode,
                CategoryId = x.CategoryId,
                ItemDescription = x.ItemDescription,
                ItemId = x.ID,
                ItemPrice = x.ItemPrice,
            });
        }

        public async Task<ItemViewModal> GetItemByIdAsync(Guid id)
        {
            var itemId = await _item.GetById(id);
            if (itemId == null)
            {
                return null;
            }
            return new ItemViewModal
            { 
                ItemId = itemId.ID,
                ItemName = itemId.ItemName,
                ItemPrice = itemId.ItemPrice,
                ItemDescription = itemId.ItemDescription,
                ItemCode = itemId.ItemCode,
                CategoryId = itemId.CategoryId,
               
            };
        }

        public async Task<bool> UpdateItemAsync(ItemUpdateViewModal itemUpdateView)
        {
            var itemId = await _item.GetById(itemUpdateView.ItemId);
            if (itemId == null)
            {
                return false;
            }
            itemId.ItemName = itemUpdateView.ItemName;
            itemId.ItemPrice = itemUpdateView.ItemPrice;
            itemId.ItemDescription= itemUpdateView.ItemDescription;
            itemId.IsActive = itemUpdateView.IsActive;
            itemId.UpdatedAt = DateTime.Now;
            itemId.CreatedAt = DateTime.Now;
            return await _item.UpdateUser(itemId);
      
        }
    }
}
