using AutoMapper;
using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.BusinessLogic.Services.Interfaces;
using ConstructionPlanning.WebApplication.Models.ConstructionObject;
using ConstructionPlanning.WebApplication.Models.ResourcePerObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionPlanning.WebApplication.Controllers
{
    [Authorize]
    public class ConstructionObjectController : Controller
    {
        private readonly IConstructionObjectService _constructionObjectService;
        private readonly IProjectService _projectService;
        private readonly IResourcePerObjectService _resourcePerObjectService;
        private readonly IMapper _mapper;

        public ConstructionObjectController(IConstructionObjectService constructionObjectService,
            IProjectService constructionObjectTypeService, 
            IResourcePerObjectService resourcePerObjectService,
            IMapper mapper)
        {
            _constructionObjectService = constructionObjectService;
            _projectService = constructionObjectTypeService;
            _resourcePerObjectService = resourcePerObjectService;
            _mapper = mapper;
        }

        public async Task<ActionResult> Details(int id, string returnUrl)
        {
            if (returnUrl == null)
            {
                return NotFound();
            }

            var constructionObject =  await _constructionObjectService.GetConstructionObjectById(id);
            var model = _mapper.Map<ConstructionObjectViewModel>(constructionObject);
            var resourcesPerObject = (await _resourcePerObjectService.GetAllResourcePerObjects()).Where(x => x.ConstructionObjectId == constructionObject.Id).AsEnumerable();
            model.ResourcesPerObject = _mapper.Map<IEnumerable<ResourcePerObjectViewModel>>(resourcesPerObject);
            model.ReturnUrl = returnUrl;

            return View(model);
        }

        public async Task<ActionResult> Create(int? projectId, string returnUrl = null)
        {
            if (returnUrl == null || !projectId.HasValue)
            {
                return NotFound();
            }

            var project = await _projectService.GetProjectById(projectId.Value);
            return View(new ConstructionObjectCreateViewModel { ReturnUrl = returnUrl, ProjectName = project.Name });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ConstructionObjectCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var constructionObjectDto = _mapper.Map<ConstructionObjectDto>(model);
                    await _constructionObjectService.AddConstructionObject(constructionObjectDto);
                    return Redirect(model.ReturnUrl);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            var project = await _projectService.GetProjectById(model.ProjectId);
            model.ProjectName = project.Name;

            return View(model);
        }

        public async Task<ActionResult> Edit(int id, string returnUrl = null)
        {
            if (returnUrl == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<ConstructionObjectEditViewModel>(await _constructionObjectService.GetConstructionObjectById(id));
            model.ReturnUrl = returnUrl;
            var project = await _projectService.GetProjectById(model.ProjectId);
            model.ProjectName = project.Name;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ConstructionObjectEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var constructionObjectDto = _mapper.Map<ConstructionObjectDto>(model);
                    await _constructionObjectService.UpdateConstructionObject(constructionObjectDto);
                    return Redirect(model.ReturnUrl);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            
            var project = await _projectService.GetProjectById(model.ProjectId);
            model.ProjectName = project.Name;
            return View(model);
        }

        public async Task<ActionResult> Delete(int id, string returnUrl = null)
        {
            if (returnUrl == null)
            {
                return NotFound();
            }

            var constructionObject = _mapper.Map<ConstructionObjectViewModel>(await _constructionObjectService.GetConstructionObjectById(id));
            constructionObject.ReturnUrl = returnUrl;
            return View(constructionObject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, ConstructionObjectViewModel model)
        {
            if (string.IsNullOrEmpty(model.ReturnUrl))
            {
                return NotFound();
            }

            await _constructionObjectService.DeleteConstructionObjectById(id);
            return Redirect(model.ReturnUrl);
        }
    }
}
