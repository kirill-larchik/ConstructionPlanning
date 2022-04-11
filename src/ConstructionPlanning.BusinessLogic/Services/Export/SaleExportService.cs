using ArrayToExcel;
using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.BusinessLogic.Services.Interfaces;

namespace ConstructionPlanning.BusinessLogic.Services.Export
{
    /// <inheritdoc />
    public class SaleExportService : IExcelExportService<SaleDto>
    {
        /// <inheritdoc />
        public byte[] Export(IEnumerable<SaleDto> data)
        {
            return data.ToExcel(schema => schema
                .AddColumn("Номер продажи", x => x.Id)
                .AddColumn("Ресурс", x => x.Resource.Name)
                .AddColumn("Дата продажи", x => x.Date)
                .AddColumn("Цена за единицу", x => x.TotalCost / x.Count)
                .AddColumn("Количество ресурсов", x => x.Count)
                .AddColumn("Итоговая стоимость", x => x.TotalCost));
        }
    }
}
