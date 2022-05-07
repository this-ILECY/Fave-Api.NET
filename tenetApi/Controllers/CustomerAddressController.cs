using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tenetApi.Context;
using tenetApi.Exception;
using tenetApi.Model;
using tenetApi.ViewModel;

namespace tenetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAddressController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CustomerAddressController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("AddressByCustomerID")] 
        public async Task<ActionResult<IEnumerable<CustomerAddressViewModel>>> GetAddressByCustomerID(long CustomerID)
        {
            IEnumerable<CustomerAddressViewModel> _customerAddressViewModelByCustomerID;
            _customerAddressViewModelByCustomerID = _context.customerAddresses.Select(c => new CustomerAddressViewModel()
            {
                AddressTitle = c.AddressTitle.Replace("_", " "),//change form (ex: "my_new_address" to "my new address")
                CustomerAddressID = c.CustomerAddressID,
                CustomerID = c.CustomerID,
                CustomerLatitude = c.CustomerLatitude,
                CustomerLongitude = c.CustomerLongitude,
                CreatedDate = c.CreatedDate,
                IsDeleted = c.IsDeleted
            }).ToList().Where(c=> c.CustomerID == CustomerID);

            if (_customerAddressViewModelByCustomerID == null)
            {
                return NotFound(Responses.NotFound("customer address"));
            }
            
            return _customerAddressViewModelByCustomerID.ToList();
        }

        [HttpPost]
        [Route("CustomerAddressAdd")]
        public async Task<ActionResult> AddCustomerAddress([FromBody] CustomerAddressViewModel customerAddress)
        {
            customerAddress.AddressTitle = customerAddress.AddressTitle.ToLower();//for better search, all characters Lowercase!
            if (customerAddress.AddressTitle.Contains(" "))//change space to underline
            {
                customerAddress.AddressTitle = customerAddress.AddressTitle.Replace(" ", "_");
            }

            var custId = _context.customers.FirstOrDefault(c => c.CustomerID == customerAddress.CustomerID);

            if (custId == null)
            {
                return BadRequest(Responses.BadResponse("customer", "invalid"));
            }

            CustomerAddress theCustomerAddress = new CustomerAddress();
            theCustomerAddress.AddressTitle = customerAddress.AddressTitle;
            theCustomerAddress.CustomerID = customerAddress.CustomerID;
            theCustomerAddress.CustomerLatitude = customerAddress.CustomerLatitude;
            theCustomerAddress.CustomerLongitude = customerAddress.CustomerLongitude;
            theCustomerAddress.IsDeleted = customerAddress.IsDeleted;
            theCustomerAddress.CreatedDate = (DateTime.Now).ToString();

            _context.customerAddresses.Add(theCustomerAddress);
            await _context.SaveChangesAsync();

            return Ok(Responses.OkResponse("customer address", "add"));
        }

        [HttpPut]
        [Route("CustomerAddressUpdate")]
        public async Task<ActionResult<CustomerViewModel>> UpdateCustomer([FromBody] CustomerAddressViewModel customerAddress)
        {
            var custId = _context.customerAddresses.FirstOrDefault(c => c.CustomerAddressID == customerAddress.CustomerAddressID);

            if (custId == null)
            {
                return BadRequest(Responses.BadResponse("customer", "invalid"));
            }
            if (customerAddress.AddressTitle.Contains(" "))//change space to underline
            {
                customerAddress.AddressTitle = customerAddress.AddressTitle.Replace(" ", "_");
            }
            CustomerAddress theCustomerAddress = _context.customerAddresses.FirstOrDefault(c => c.CustomerAddressID == customerAddress.CustomerAddressID);
            theCustomerAddress.AddressTitle = customerAddress.AddressTitle;
            theCustomerAddress.CustomerLatitude = customerAddress.CustomerLatitude;
            theCustomerAddress.CustomerLongitude = customerAddress.CustomerLongitude;

            await _context.SaveChangesAsync();

            return Ok(Responses.OkResponse("customer address", "mod"));
        }
        
        [HttpDelete]
        [Route("CustomerAddressDelete")]
        public async Task<ActionResult<CustomerViewModel>> DeleteCustomer(long customerAddressID)
        {
            if (!_context.customerAddresses.Any(c => c.CustomerAddressID == customerAddressID))
            {
                return NotFound(Responses.NotFound("customer address"));
            }
            CustomerAddress theCustomerAddress = _context.customerAddresses.FirstOrDefault(c => c.CustomerAddressID == customerAddressID);
            theCustomerAddress.IsDeleted = true;

            await _context.SaveChangesAsync();

            return Ok(Responses.OkResponse("customer address","del"));
        }
        
        [HttpPost]
        [Route("CustomerAddressUndoDelete")]
        public async Task<ActionResult<CustomerViewModel>> UndoDeleteCustomer(long customerAddressID)
        {
            if (!_context.customerAddresses.Any(c => c.CustomerAddressID == customerAddressID))
            {
                return NotFound(Responses.NotFound("customer address"));
            }
            CustomerAddress theCustomerAddress = _context.customerAddresses.FirstOrDefault(c => c.CustomerAddressID == customerAddressID);
            theCustomerAddress.IsDeleted = false;

            await _context.SaveChangesAsync();

            return Ok(Responses.OkResponse("customer address","undel"));
        }
    }
}
