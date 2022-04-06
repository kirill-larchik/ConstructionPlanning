﻿using AutoMapper;
using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.BusinessLogic.Services.Interfaces;
using ConstructionPlanning.WebApplication.Models;
using ConstructionPlanning.WebApplication.Models.Resource;
using ConstructionPlanning.WebApplication.Models.ResourceType;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConstructionPlanning.WebApplication.Controllers
{
    public class ResourceController : Controller
    {
        private readonly IResourceService _resourceService;
        private readonly IResourceTypeService _resourceTypeService;
        private readonly IMapper _mapper;

        public ResourceController(IResourceService resourceService, IResourceTypeService resourceTypeService, IMapper mapper)
        {
            _resourceService = resourceService;
            _resourceTypeService = resourceTypeService;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index(int page = 1)
        {
            const int pageSize = 5;

            var resources = await _resourceService.GetAllResourcesByPageAndPageSize(page, pageSize);
            var pageViewModel = new PageViewModel(await _resourceService.GetTotalCount(), page, pageSize);
            var indexViewModel = new ResourceIndexViewModel
            {
                PageViewModel = pageViewModel,
                Resources = _mapper.Map<IEnumerable<ResourceViewModel>>(resources),
            };

            return View(indexViewModel);
        }

        public async Task<ActionResult> Details(int id)
        {
            var model = _mapper.Map<ResourceViewModel>(await _resourceService.GetResourceById(id));
            return View(model);
        }

        public async Task<ActionResult> Create()
        {
            await InitResourceTypeSelectList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ResourceCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var resourceDto = _mapper.Map<ResourceDto>(model);
                    await _resourceService.AddResource(resourceDto);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            await InitResourceTypeSelectList();
            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = _mapper.Map<ResourceEditViewModel>(await _resourceService.GetResourceById(id));
            await InitResourceTypeSelectList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ResourceEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var resourceDto = _mapper.Map<ResourceDto>(model);
                    await _resourceService.UpdateResource(resourceDto);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            await InitResourceTypeSelectList();
            return View(model);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var resource = _mapper.Map<ResourceViewModel>(await _resourceService.GetResourceById(id));
            return View(resource);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, ResourceViewModel model)
        {
            await _resourceService.DeleteResourceById(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task InitResourceTypeSelectList()
        {
            var resourceTypes = _mapper.Map<IEnumerable<ResourceTypeSelectListModel>>(await _resourceTypeService.GetAllResourceTypes());
            ViewBag.ResourceTypes = new SelectList(resourceTypes, nameof(ResourceTypeSelectListModel.Id), nameof(ResourceTypeSelectListModel.Name));
        }
    }
}