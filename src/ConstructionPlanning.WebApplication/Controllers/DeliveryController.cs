using AutoMapper;
using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.BusinessLogic.Services.Interfaces;
using ConstructionPlanning.WebApplication.Data;
using ConstructionPlanning.WebApplication.Models;
using ConstructionPlanning.WebApplication.Models.Delivery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConstructionPlanning.WebApplication.Controllers
{
    [Authorize]
    public class DeliveryController : Controller
    {
        private readonly IDeliveryService _deliveryService;
        private readonly IResourceService _resourceService;
        private readonly IProviderService _providerService;
        private readonly IMapper _mapper;
        private readonly IExcelExportService<DeliveryDto> _excelExportService;

        public DeliveryController(IDeliveryService deliveryService,
            IResourceService resourceService,
            IProviderService providerService,
            IExcelExportService<DeliveryDto> excelExportService,
            IMapper mapper)
        {
            _deliveryService = deliveryService;
            _resourceService = resourceService;
            _providerService = providerService;
            _excelExportService = excelExportService;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index(int page = 1)
        {
            var deliveries = await _deliveryService.GetAllPaginatedDeliveries(page, Constants.PageSize);
            var pageViewModel = new PageViewModel(await _deliveryService.GetTotalCount(), page, Constants.PageSize);
            var indexViewModel = new DeliveryIndexViewModel
            {
                PageViewModel = pageViewModel,
                Deliveries = _mapper.Map<IEnumerable<DeliveryViewModel>>(deliveries),
            };

            return View(indexViewModel);
        }

        public async Task<ActionResult> Details(int id)
        {
            var model = _mapper.Map<DeliveryViewModel>(await _deliveryService.GetDeliveryById(id));
            return View(model);
        }

        public async Task<IActionResult> GetDeliveriesByProviderId(int? providerId, int page = 1)
        {
            if (!providerId.HasValue)
            {
                return NotFound();
            }

            var provider = await _providerService.GetProviderById(providerId.Value);
            var deliveries = await _deliveryService.GetAllPaginatedDeliveriesByProviderId(providerId.Value, page, Constants.PageSize);
            var pageViewModel = new PageViewModel(await _deliveryService.GetTotalCountByProviderId(providerId.Value), page, Constants.PageSize);
            var indexViewModel = new DeliveryByProviderViewModel
            {
                ProviderId = provider.Id,
                ProviderName = provider.Name,
                PageViewModel = pageViewModel,
                Deliveries = _mapper.Map<IEnumerable<DeliveryViewModel>>(deliveries),
            };

            const string viewName = "DeliveriesByProvider";

            return View(viewName, indexViewModel);
        }

        public async Task<IActionResult> GetDeliveriesByResourceId(int? resourceId, int page = 1)
        {
            if (!resourceId.HasValue)
            {
                return NotFound();
            }

            var resource = await _resourceService.GetResourceById(resourceId.Value);
            var deliveries = await _deliveryService.GetAllPaginatedDeliveriesByResourceId(resourceId.Value, page, Constants.PageSize);
            var pageViewModel = new PageViewModel(await _deliveryService.GetTotalCountByResourceId(resourceId.Value), page, Constants.PageSize);
            var indexViewModel = new DeliveryByResourceViewModel
            {
                ResourceId = resource.Id,
                ResourceName = resource.Name,
                PageViewModel = pageViewModel,
                Deliveries = _mapper.Map<IEnumerable<DeliveryViewModel>>(deliveries),
            };

            const string viewName = "DeliveriesByResource";

            return View(viewName, indexViewModel);
        }

        public async Task<ActionResult> Create()
        {
            await InitSelectLists();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DeliveryCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var deliveryDto = _mapper.Map<DeliveryDto>(model);
                    await _deliveryService.AddDelivery(deliveryDto);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            await InitSelectLists();
            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = _mapper.Map<DeliveryEditViewModel>(await _deliveryService.GetDeliveryById(id));
            await InitSelectLists();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DeliveryEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var deliveryDto = _mapper.Map<DeliveryDto>(model);
                    await _deliveryService.UpdateDelivery(deliveryDto);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            await InitSelectLists();
            return View(model);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var resource = _mapper.Map<DeliveryViewModel>(await _deliveryService.GetDeliveryById(id));
            return View(resource);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, DeliveryViewModel model)
        {
            await _deliveryService.DeleteDeliveryById(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Export()
        {
            const string fileName = "deliveries.xlsx";
            var bytes = _excelExportService.Export(await _deliveryService.GetAllDeliveries());
            return File(bytes, "application/force-download", fileName);
        }

        private async Task InitSelectLists()
        {
            var resource = _mapper.Map<IEnumerable<SelectListModel>>(await _resourceService.GetAllResources());
            ViewBag.Resources = new SelectList(resource, nameof(SelectListModel.Id), nameof(SelectListModel.Name));

            var providers = _mapper.Map<IEnumerable<SelectListModel>>(await _providerService.GetAllProviders());
            ViewBag.Providers = new SelectList(providers, nameof(SelectListModel.Id), nameof(SelectListModel.Name));
        }
    }
}
