using Microsoft.EntityFrameworkCore;
using ProgiAPI.DbModels;

namespace ProgiAPI.DbLogic;

/// <summary>
/// Provides methods to interact with the database for retrieving latest fees and car types.
/// </summary>
public class Connections : IConnections
{
    private readonly IProgiContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="Connections"/> class.
    /// </summary>
    /// <param name="context">The database context to be used for data retrieval.</param>
    public Connections(IProgiContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves the list of association fees.
    /// </summary>
    /// <returns>A list of <see cref="AssociationFees"/>.</returns>
    public async Task<List<AssociationFees>> GetAssociationFees()
    {
        return await _context.AssociationFees.ToListAsync();
    }

    /// <summary>
    /// Retrieves the buyer and seller fees for a specific car type.
    /// </summary>
    /// <param name="carType">The type of car for which to retrieve the fees.</param>
    /// <returns>A list of <see cref="BuyerSellerFees"/> for the specified car type.</returns>
    public async Task<List<BuyerSellerFees>> GetBuyerSellerFees(int carType)
    {
        return await _context.BuyerSellerFees.Where(fees => fees.CarTypeId != null && fees.CarTypeId.Value == carType).ToListAsync();
    }

    /// <summary>
    /// Retrieves the list of car types.
    /// </summary>
    /// <returns>A list of <see cref="CarTypes"/>.</returns>
    public async Task<List<CarTypes>> GetCarTypes()
    {
        return await _context.CarTypes.ToListAsync();
    }

    /// <summary>
    /// Retrieves the storage fees.
    /// </summary>
    /// <returns>A <see cref="StorageFee"/> if found; otherwise, null.</returns>
    public async Task<StorageFee?> GetStorageFees()
    {
        return await _context.StorageFee.FirstOrDefaultAsync();
    }
    
    /// <summary>
    /// Updates the storage fees.
    /// </summary>
    /// <param name="newStorageFee">The new storage fee amount to be set.</param>
    /// <returns>
    /// A boolean value indicating whether the update was successful.
    /// </returns>
    public async Task<bool> UpdateStorageFees(int newStorageFee)
    {
        var storageFee = await _context.StorageFee.FirstOrDefaultAsync();
        if (storageFee == null)
        {
            return false;
        }
        storageFee.fee_amount = newStorageFee;
        await _context.SaveChangesAsync();
        return true;
    }
}