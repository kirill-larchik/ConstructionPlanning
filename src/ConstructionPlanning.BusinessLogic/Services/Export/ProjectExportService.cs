using ArrayToExcel;
using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.BusinessLogic.Services.Interfaces;

namespace ConstructionPlanning.BusinessLogic.Services.Export
{
    /// <inheritdoc />
    internal class ProjectExportService : IExcelExportService<ProjectDto>
    {
        /// <inheritdoc />
        public byte[] Export(IEnumerable<ProjectDto> data)
        {
            var allConstructionObjects = new List<ConstructionObjectDto>();
            foreach (var project in data)
            {
                allConstructionObjects.AddRange(project.ConstructionObjects);
            }

            var allResources = new List<ResourcePerObjectDto>();
            foreach (var constructionObject in allConstructionObjects)
            {
                allResources.AddRange(constructionObject.ResourcesPerObject);
            }

            return data.ToExcel(schema => schema
                .AddColumn("Номер проекта", x => x.Id)
                .AddColumn("Название", x => x.Name)
                .AddColumn("Дата создания", x => x.DateOfCreate)
                .AddColumn("Крайний срок", x => x.Deadline)
                .AddColumn("Количество выделенных средств", x => x.AllocatedAmount)
                .AddColumn("Итоговые затраты", x => x.TotalCost)
                .AddColumn("Заказчик", x => x.Customer != null ? x.Customer.Name : "Информация остутсвует")
                .ColumnSort(x => x.Name, desc: true)
                .SheetName("Проекты")
                .AddSheet(allConstructionObjects, x =>
                {
                    x.AddColumn("Номер строительного объекта", x => x.Id);
                    x.AddColumn("Наименование", x => x.Name);
                    x.AddColumn("Описание", x => x.Description);
                    x.AddColumn("Проект", x => x.Project.Name);
                    x.AddColumn("Итоговые затраты", x => x.TotalCost);
                    x.ColumnSort(x => x.Name, desc: true);
                    x.SheetName("Строительные объекты");
                })
                .AddSheet(allResources, x =>
                {
                    x.AddColumn("Ресурс", x => x.Resource != null ? x.Resource.Name : "Информация остутсвует");
                    x.AddColumn("Номер строительного объекта", x => x.ConstructionObject.Id);
                    x.AddColumn("Количестов используемых ресурсов", x => x.Count);
                    x.AddColumn("Итоговые затраты", x => x.TotalCost);
                    x.ColumnSort(x => x.Name, desc: true);
                    x.SheetName("Ресурсы на строительные объекты");
                }));
        }
    }
}
