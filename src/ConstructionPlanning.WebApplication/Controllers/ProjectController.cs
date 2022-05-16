using AutoMapper;
using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.BusinessLogic.Services.Interfaces;
using ConstructionPlanning.WebApplication.Data;
using ConstructionPlanning.WebApplication.Models;
using ConstructionPlanning.WebApplication.Models.Project;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionPlanning.WebApplication.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IConstructionObjectService _constructionObjectService;
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public ProjectController(IProjectService projectService,
            ICustomerService customerService,
            IConstructionObjectService constructionObjectService,
            IMapper mapper)
        {
            _projectService = projectService;
            _customerService = customerService;
            _constructionObjectService = constructionObjectService;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index(int page = 1)
        {
            var projects = await _projectService.GetAllPaginatedProjects(page, Constants.PageSize);
            var pageViewModel = new PageViewModel(await _projectService.GetTotalCount(), page, Constants.PageSize);
            var indexViewModel = new ProjectIndexViewModel
            {
                PageViewModel = pageViewModel,
                Projects = _mapper.Map<IEnumerable<ProjectViewModel>>(projects),
            };

            return View(indexViewModel);
        }

        public async Task<ActionResult> Details(int id)
        {
            var model = _mapper.Map<ProjectViewModel>(await _projectService.GetProjectById(id));
            model.ConstructionObjects = (await _constructionObjectService.GetAllConstructionObjects()).Where(x => x.ProjectId == id);
            return View(model);
        }

        public async Task<IActionResult> GetProjectsByCustomerId(int? customerId, int page = 1)
        {
            if (!customerId.HasValue)
            {
                return NotFound();
            }

            var customer = await _customerService.GetCustomerById(customerId.Value);
            var projects = await _projectService.GetAllPaginatedProjectsByCustomerId(customerId.Value, page, Constants.PageSize);
            var pageViewModel = new PageViewModel(await _projectService.GetTotalCountByCustomerId(customerId.Value), page, Constants.PageSize);
            var indexViewModel = new ProjectByTypeViewModel
            {
                CustomerId = customer.Id,
                CustomerName = customer.Name,
                PageViewModel = pageViewModel,
                Projects = _mapper.Map<IEnumerable<ProjectViewModel>>(projects),
            };

            const string viewName = "ProjectsByCustomer";
            return View(viewName, indexViewModel);
        }

        public async Task<ActionResult> Create()
        {
            await InitCustomerSelectList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProjectCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var projectDto = _mapper.Map<ProjectDto>(model);
                    await _projectService.AddProject(projectDto);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            await InitCustomerSelectList();
            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = _mapper.Map<ProjectEditViewModel>(await _projectService.GetProjectById(id));
            await InitCustomerSelectList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProjectEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var projectDto = _mapper.Map<ProjectDto>(model);
                    await _projectService.UpdateProject(projectDto);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            await InitCustomerSelectList();
            return View(model);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var project = _mapper.Map<ProjectViewModel>(await _projectService.GetProjectById(id));
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, ProjectViewModel model)
        {
            await _projectService.DeleteProjectById(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task InitCustomerSelectList()
        {
            var customers = _mapper.Map<IEnumerable<SelectListModel>>(await _customerService.GetAllCustomers());
            ViewBag.Customers = new SelectList(customers, nameof(SelectListModel.Id), nameof(SelectListModel.Name));
        }
    }
}
