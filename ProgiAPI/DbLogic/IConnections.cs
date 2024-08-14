using ProgiAPI.DbModels;

namespace ProgiAPI.DbLogic;

public interface IConnections
{
    Task<List<AssociationFees>> GetAssociationFees();
    Task<List<BuyerSellerFees>> GetBuyerSellerFees(int carType);
    Task<List<CarTypes>> GetCarTypes();
    Task<StorageFee?> GetStorageFees();
    Task<bool> UpdateStorageFees(int newStorageFee);
}