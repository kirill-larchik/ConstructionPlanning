using AutoMapper;
using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.BusinessLogic.Services.Interfaces;
using ConstructionPlanning.WebApplication.Data;
using ConstructionPlanning.WebApplication.Models;
using ConstructionPlanning.WebApplication.Models.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConstructionPlanning.WebApplication.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _сustomerService;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerService сustomerService, IMapper mapper)
        {
            _сustomerService = сustomerService;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index(int page = 1)
        {
            var сustomers = await _сustomerService.GetAllPaginatedCustomers(page, Constants.PageSize);
            var pageViewModel = new PageViewModel(await _сustomerService.GetTotalCount(), page, Constants.PageSize);
            var indexViewModel = new CustomerIndexViewModel
            {
                PageViewModel = pageViewModel,
                Customers = _mapper.Map<IEnumerable<CustomerViewModel>>(сustomers),
            };

            return View(indexViewModel);
        }

        public async Task<ActionResult> Details(int id)
        {
            var model = _mapper.Map<CustomerViewModel>(await _сustomerService.GetCustomerById(id));
            return View(model);
        }

        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CustomerCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var сustomerDto = _mapper.Map<CustomerDto>(model);
                    await _сustomerService.AddCustomer(сustomerDto);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = _mapper.Map<CustomerEditViewModel>(await _сustomerService.GetCustomerById(id));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CustomerEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var сustomerDto = _mapper.Map<CustomerDto>(model);
                    await _сustomerService.UpdateCustomer(сustomerDto);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(model);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var сustomer = _mapper.Map<CustomerViewModel>(await _сustomerService.GetCustomerById(id));
            return View(сustomer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, CustomerViewModel model)
        {
            await _сustomerService.DeleteCustomerById(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
