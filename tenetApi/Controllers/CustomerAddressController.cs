using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tenetApi.Context;
using tenetApi.Model;
using tenetApi.ViewModel;

namespace tenetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAddressController : ControllerBase
    {
        private readonly AppDbContext _context;
        private IEnumerable<CustomerAddressViewModel> _customerAddressViewModel { get; set; }
        public CustomerAddressController(AppDbContext context)
        {
            _context = context;
        }
        // list of each customer addresses
        [HttpPost]
        [Route("AddressByCustomerID")] 
        public async Task<ActionResult<IEnumerable<CustomerAddressViewModel>>> GetAddressByCustomerID([FromBody] long CustomerID)
        {
            _customerAddressViewModel = _context.customerAddresses.Select(c => new CustomerAddressViewModel()
            {
                AddressTitle = c.AddressTitle.Replace("_", " "),//change form (ex: "my_new_address" to "my new address")
                CustomerAddressID = c.CustomerAddressID,
                CustomerID = c.CustomerID,
                CustomerLatitude = c.CustomerLatitude,
                CustomerLongitude = c.CustomerLongitude,
                CreatedDate = c.CreatedDate,
                IsDeleted = c.IsDeleted
            }).ToList().Where(c=> c.CustomerID == CustomerID);

            if (_customerAddressViewModel == null)
            {
                return NotFound();
            }
            
            return _customerAddressViewModel.ToList();
        }
        //create a new address for customer
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
                return BadRequest();
            }

            CustomerAddress theCustomerAddress = new CustomerAddress();
            theCustomerAddress.AddressTitle = customerAddress.AddressTitle;
            theCustomerAddress.CustomerID = customerAddress.CustomerID;
            theCustomerAddress.CustomerLatitude = customerAddress.CustomerLatitude;
            theCustomerAddress.CustomerLongitude = customerAddress.CustomerLongitude;
            theCustomerAddress.IsDeleted = customerAddress.IsDeleted;
            theCustomerAddress.CreatedDate = DateTime.Now;

            _context.customerAddresses.Add(theCustomerAddress);
            await _context.SaveChangesAsync();

            return Ok();
        }
        //make changes in addresses
        [HttpPost]
        [Route("CustomerAddressUpdate")]
        public async Task<ActionResult<CustomerViewModel>> UpdateCustomer([FromBody] CustomerAddressViewModel customerAddress)
        {
            var custId = _context.customerAddresses.FirstOrDefault(c => c.CustomerAddressID == customerAddress.CustomerAddressID);

            if (custId == null)
            {
                return BadRequest("Customer not found!");
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

            return Ok();
        }
        [HttpPost]
        [Route("CustomerAddressDelete")]
        public async Task<ActionResult<CustomerViewModel>> DeleteCustomer([FromBody] long customerAddressID)
        {

            CustomerAddress theCustomerAddress = _context.customerAddresses.FirstOrDefault(c => c.CustomerAddressID == customerAddressID);
            theCustomerAddress.IsDeleted = true;

            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpPost]
        [Route("CustomerAddressUndoDelete")]
        public async Task<ActionResult<CustomerViewModel>> UndoDeleteCustomer([FromBody] long customerAddressID)
        {

            CustomerAddress theCustomerAddress = _context.customerAddresses.FirstOrDefault(c => c.CustomerAddressID == customerAddressID);
            theCustomerAddress.IsDeleted = false;

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
