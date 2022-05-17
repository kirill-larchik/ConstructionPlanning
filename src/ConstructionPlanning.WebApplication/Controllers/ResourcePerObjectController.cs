using AutoMapper;
using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.BusinessLogic.Services.Interfaces;
using ConstructionPlanning.WebApplication.Models;
using ConstructionPlanning.WebApplication.Models.ResourcePerObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConstructionPlanning.WebApplication.Controllers
{
    [Authorize]
    public class ResourcePerObjectController : Controller
    {
        private readonly IResourcePerObjectService _resourcePerObjectService;
        private readonly IConstructionObjectService _constructionObjectService;
        private readonly IResourceService _resourceService;
        private readonly IMapper _mapper;

        public ResourcePerObjectController(IResourcePerObjectService resourcePerObjectService,
            IConstructionObjectService resourcePerObjectTypeService,
            IResourceService resourceService,
            IMapper mapper)
        {
            _resourcePerObjectService = resourcePerObjectService;
            _constructionObjectService = resourcePerObjectTypeService;
            _resourceService = resourceService;
            _mapper = mapper;
        }

        public async Task<ActionResult> Details(int id, string returnUrl)
        {
            if (returnUrl == null)
            {
                return NotFound();
            }

            var resourcePerObject =  await _resourcePerObjectService.GetResourcePerObjectById(id);
            var model = _mapper.Map<ResourcePerObjectViewModel>(resourcePerObject);
            model.ReturnUrl = returnUrl;

            await InitSelectLists();
            return View(model);
        }

        public async Task<ActionResult> Create(int? constructionObjectId, string returnUrl = null)
        {
            if (returnUrl == null || !constructionObjectId.HasValue)
            {
                return NotFound();
            }

            await InitSelectLists();
            var constructionObject = await _constructionObjectService.GetConstructionObjectById(constructionObjectId.Value);
            return View(new ResourcePerObjectCreateViewModel { ReturnUrl = returnUrl, ConstructionObjectName = constructionObject.Name });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ResourcePerObjectCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var resourcePerObjectDto = _mapper.Map<ResourcePerObjectDto>(model);
                    await _resourcePerObjectService.AddResourcePerObject(resourcePerObjectDto);
                    return Redirect(model.ReturnUrl);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            var constructionObject = await _constructionObjectService.GetConstructionObjectById(model.ConstructionObjectId);
            model.ConstructionObjectName = constructionObject.Name;

            await InitSelectLists();
            return View(model);
        }

        public async Task<ActionResult> Edit(int id, string returnUrl = null)
        {
            if (returnUrl == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<ResourcePerObjectEditViewModel>(await _resourcePerObjectService.GetResourcePerObjectById(id));
            model.ReturnUrl = returnUrl;
            var constructionObject = await _constructionObjectService.GetConstructionObjectById(model.ConstructionObjectId);
            model.ConstructionObjectName = constructionObject.Name;

            await InitSelectLists();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ResourcePerObjectEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var resourcePerObjectDto = _mapper.Map<ResourcePerObjectDto>(model);
                    await _resourcePerObjectService.UpdateResourcePerObject(resourcePerObjectDto);
                    return Redirect(model.ReturnUrl);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            
            var constructionObject = await _constructionObjectService.GetConstructionObjectById(model.ConstructionObjectId);
            model.ConstructionObjectName = constructionObject.Name;
            await InitSelectLists();
            return View(model);
        }

        public async Task<ActionResult> Delete(int id, string returnUrl = null)
        {
            if (returnUrl == null)
            {
                return NotFound();
            }

            var resourcePerObject = _mapper.Map<ResourcePerObjectViewModel>(await _resourcePerObjectService.GetResourcePerObjectById(id));
            resourcePerObject.ReturnUrl = returnUrl;
            return View(resourcePerObject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, ResourcePerObjectViewModel model)
        {
            if (string.IsNullOrEmpty(model.ReturnUrl))
            {
                return NotFound();
            }

            await _resourcePerObjectService.DeleteResourcePerObjectById(id);
            return Redirect(model.ReturnUrl);
        }

        private async Task InitSelectLists()
        {
            var resource = _mapper.Map<IEnumerable<SelectListModel>>(await _resourceService.GetAllResources());
            ViewBag.Resources = new SelectList(resource, nameof(SelectListModel.Id), nameof(SelectListModel.Name));
        }
    }
}
