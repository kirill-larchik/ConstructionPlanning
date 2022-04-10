using AutoMapper;
using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.BusinessLogic.Services.Interfaces;
using ConstructionPlanning.WebApplication.Models;
using ConstructionPlanning.WebApplication.Models.ResourceType;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConstructionPlanning.WebApplication.Controllers
{
    public class ResourceTypeController : Controller
    {
        private readonly IResourceTypeService _resourceTypeService;
        private readonly IMapper _mapper;

        public ResourceTypeController(IResourceTypeService resourceTypeService, IMapper mapper)
        {
            _resourceTypeService = resourceTypeService;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index(int page = 1)
        {
            const int pageSize = 5;

            var resourceTypes = await _resourceTypeService.GetAllResourceTypesByPagination(page, pageSize);
            var pageViewModel = new PageViewModel(await _resourceTypeService.GetTotalCount(), page, pageSize);
            var indexViewModel = new ResourceTypeIndexViewModel
            {
                PageViewModel = pageViewModel,
                ResourceTypes = _mapper.Map<IEnumerable<ResourceTypeViewModel>>(resourceTypes),
            };

            return View(indexViewModel);
        }

        public async Task<ActionResult> Details(int id)
        {
            var model = _mapper.Map<ResourceTypeViewModel>(await _resourceTypeService.GetResourceTypeById(id));
            return View(model);
        }

        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ResourceTypeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var resourceTypeDto = _mapper.Map<ResourceTypeDto>(model);
                    await _resourceTypeService.AddResourceType(resourceTypeDto);
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
            var model = _mapper.Map<ResourceTypeEditViewModel>(await _resourceTypeService.GetResourceTypeById(id));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ResourceTypeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var resourceTypeDto = _mapper.Map<ResourceTypeDto>(model);
                    await _resourceTypeService.UpdateResourceType(resourceTypeDto);
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
            var resourceType = _mapper.Map<ResourceTypeViewModel>(await _resourceTypeService.GetResourceTypeById(id));
            return View(resourceType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, ResourceTypeViewModel model)
        {
            await _resourceTypeService.DeleteResourceTypeById(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
