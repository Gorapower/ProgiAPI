using Microsoft.AspNetCore.Mvc;
using ProgiAPI.DbLogic;

namespace ProgiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : Controller
    {
        private readonly IConnections _conections;

        public MainController(IConfiguration configuration, IConnections connections)
        {
            _conections = connections;
        }
        
        /// <summary>
        /// Retrieves the association fees.
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the association fees.
        /// </returns>
        /// <remarks>
        /// This method calls the <see cref="IConnections.GetAssociationFees"/> method to fetch the data.
        /// </remarks>
        [HttpGet(nameof(GetAssociationFees))]
        public async Task<IActionResult> GetAssociationFees()
        {
            return Ok(await _conections.GetAssociationFees());
        }

        /// <summary>
        /// Retrieves the buyer and seller fees for a specific car type.
        /// </summary>
        /// <param name="carType">The type of car for which to retrieve the fees.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the buyer and seller fees for the specified car type.
        /// </returns>
        /// <remarks>
        /// This method calls the <see cref="IConnections.GetBuyerSellerFees(int)"/> method to fetch the data.
        /// If no fees are found, it returns a <see cref="NotFoundResult"/>.
        /// </remarks>
        [HttpGet(nameof(GetBuyerSellerFees)+"/{carType}")]
        public async Task<IActionResult> GetBuyerSellerFees(int carType)
        {
            var fees = await _conections.GetBuyerSellerFees(carType);
            if(fees.Count == 0)
            {
                return NotFound();
            }
            return Ok(await _conections.GetBuyerSellerFees(carType));
        }

        /// <summary>
        /// Retrieves the list of car types.
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of car types.
        /// </returns>
        /// <remarks>
        /// This method calls the <see cref="IConnections.GetCarTypes"/> method to fetch the data.
        /// </remarks>
        [HttpGet(nameof(GetCarTypes))]
        public async Task<IActionResult> GetCarTypes()
        {
            return Ok(await _conections.GetCarTypes());
        }
        /// <summary>
        /// Retrieves the storage fees.
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the storage fees.
        /// </returns>
        /// <remarks>
        /// This method calls the <see cref="IConnections.GetStorageFees"/> method to fetch the data.
        /// </remarks>
        [HttpGet(nameof(GetStorageFees))]
        public async Task<IActionResult> GetStorageFees()
        {
            var storageFees = await _conections.GetStorageFees();
            if(storageFees == null)
            {
                return NotFound();
            }
            return Ok(storageFees);
        }
        
        /// <summary>
        /// Updates the storage fees.
        /// </summary>
        /// <param name="newStorageFee">The new storage fee amount to be set.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the update operation.
        /// </returns>
        /// <remarks>
        /// This method calls the <see cref="IConnections.UpdateStorageFees(int)"/> method to update the storage fees.
        /// If the update is successful, it returns an <see cref="OkResult"/>.
        /// If the update fails, it returns a <see cref="StatusCodeResult"/> with status code 500.
        /// </remarks>
        [HttpPut(nameof(UpdateStorageFees)+ "/{newStorageFee}")]
        public async Task<IActionResult> UpdateStorageFees(int newStorageFee)
        {
            if(await _conections.UpdateStorageFees(newStorageFee))
            {
                return Ok();
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
