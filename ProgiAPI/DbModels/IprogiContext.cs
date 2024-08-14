using Microsoft.EntityFrameworkCore;

namespace ProgiAPI.DbModels;

public interface IProgiContext
{
    DbSet<AssociationFees> AssociationFees { get; set; }
    DbSet<BuyerSellerFees> BuyerSellerFees { get; set; }
    DbSet<CarTypes> CarTypes { get; set; }
    DbSet<StorageFee?> StorageFee { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}