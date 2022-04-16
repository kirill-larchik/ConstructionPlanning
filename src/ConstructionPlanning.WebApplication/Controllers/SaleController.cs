using AutoMapper;
using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.BusinessLogic.Services.Interfaces;
using ConstructionPlanning.WebApplication.Data;
using ConstructionPlanning.WebApplication.Models;
using ConstructionPlanning.WebApplication.Models.Sale;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConstructionPlanning.WebApplication.Controllers
{
    [Authorize]
    public class SaleController : Controller
    {
        private readonly ISaleService _saleService;
        private readonly IResourceService _resourceService;
        private readonly IMapper _mapper;
        private readonly IExcelExportService<SaleDto> _excelExportService;

        public SaleController(ISaleService saleService,
            IResourceService resourceService,
            IExcelExportService<SaleDto> excelExportService,
            IMapper mapper)
        {
            _saleService = saleService;
            _resourceService = resourceService;
            _excelExportService = excelExportService;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index(int page = 1)
        {
            var sales = await _saleService.GetAllSalesByPagination(page, Constants.PageSize);
            var pageViewModel = new PageViewModel(await _saleService.GetTotalCount(), page, Constants.PageSize);
            var indexViewModel = new SaleIndexViewModel
            {
                PageViewModel = pageViewModel,
                Sales = _mapper.Map<IEnumerable<SaleViewModel>>(sales),
            };

            return View(indexViewModel);
        }

        public async Task<ActionResult> Details(int id)
        {
            var model = _mapper.Map<SaleViewModel>(await _saleService.GetSaleById(id));
            return View(model);
        }

        public async Task<IActionResult> GetSalesByResourceId(int? resourceId, int page = 1)
        {
            if (!resourceId.HasValue)
            {
                return NotFound();
            }

            var resource = await _resourceService.GetResourceById(resourceId.Value);
            var sales = await _saleService.GetAllSalesByResourceIdWithPagination(resourceId.Value, page, Constants.PageSize);
            var pageViewModel = new PageViewModel(await _saleService.GetTotalCountByResourceId(resourceId.Value), page, Constants.PageSize);
            var indexViewModel = new SaleByResourceViewModel
            {
                ResourceId = resource.Id,
                ResourceName = resource.Name,
                PageViewModel = pageViewModel,
                Sales = _mapper.Map<IEnumerable<SaleViewModel>>(sales),
            };

            const string viewName = "SalesByResource";

            return View(viewName, indexViewModel);
        }

        public async Task<ActionResult> Create()
        {
            await InitSelectLists();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SaleCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var saleDto = _mapper.Map<SaleDto>(model);
                    await _saleService.AddSale(saleDto);
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
            var model = _mapper.Map<SaleEditViewModel>(await _saleService.GetSaleById(id));
            await InitSelectLists();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SaleEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var saleDto = _mapper.Map<SaleDto>(model);
                    await _saleService.UpdateSale(saleDto);
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
            var resource = _mapper.Map<SaleViewModel>(await _saleService.GetSaleById(id));
            return View(resource);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, SaleViewModel model)
        {
            await _saleService.DeleteSaleById(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Export()
        {
            const string fileName = "sales.xlsx";
            var bytes = _excelExportService.Export(await _saleService.GetAllSales());
            return File(bytes, "application/force-download", fileName);
        }

        private async Task InitSelectLists()
        {
            var resource = _mapper.Map<IEnumerable<SelectListModel>>(await _resourceService.GetAllResources());
            ViewBag.Resources = new SelectList(resource, nameof(SelectListModel.Id), nameof(SelectListModel.Name));
        }
    }
}
