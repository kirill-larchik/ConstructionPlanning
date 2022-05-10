using AutoMapper;
using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.BusinessLogic.Services.Interfaces;
using ConstructionPlanning.WebApplication.Data;
using ConstructionPlanning.WebApplication.Models;
using ConstructionPlanning.WebApplication.Models.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConstructionPlanning.WebApplication.Controllers
{
    [Authorize]
    public class ProviderController : Controller
    {
        private readonly IProviderService _providerService;
        private readonly IMapper _mapper;

        public ProviderController(IProviderService providerService, IMapper mapper)
        {
            _providerService = providerService;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index(int page = 1)
        {
            var providers = await _providerService.GetAllPaginatedProviders(page, Constants.PageSize);
            var pageViewModel = new PageViewModel(await _providerService.GetTotalCount(), page, Constants.PageSize);
            var indexViewModel = new ProviderIndexViewModel
            {
                PageViewModel = pageViewModel,
                Providers = _mapper.Map<IEnumerable<ProviderViewModel>>(providers),
            };

            return View(indexViewModel);
        }

        public async Task<ActionResult> Details(int id)
        {
            var model = _mapper.Map<ProviderViewModel>(await _providerService.GetProviderById(id));
            return View(model);
        }

        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProviderCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var providerDto = _mapper.Map<ProviderDto>(model);
                    await _providerService.AddProvider(providerDto);
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
            var model = _mapper.Map<ProviderEditViewModel>(await _providerService.GetProviderById(id));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProviderEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var providerDto = _mapper.Map<ProviderDto>(model);
                    await _providerService.UpdateProvider(providerDto);
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
            var provider = _mapper.Map<ProviderViewModel>(await _providerService.GetProviderById(id));
            return View(provider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, ProviderViewModel model)
        {
            await _providerService.DeleteProviderById(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
