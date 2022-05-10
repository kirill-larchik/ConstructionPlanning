using ArrayToExcel;
using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.BusinessLogic.Services.Interfaces;

namespace ConstructionPlanning.BusinessLogic.Services.Export
{
    /// <inheritdoc />
    public class DeliveryExportService : IExcelExportService<DeliveryDto>
    {
        /// <inheritdoc />
        public byte[] Export(IEnumerable<DeliveryDto> data)
        {
            return data.ToExcel(schema => schema
                .AddColumn("Номер поставки", x => x.Id)
                .AddColumn("Поставщик", x => x.Provider.Name)
                .AddColumn("Ресурс", x => x.Resource.Name)
                .AddColumn("Дата поставки", x => x.Date)
                .AddColumn("Цена за единицу", x => x.UnitCost)
                .AddColumn("Количество ресурсов", x => x.Count)
                .AddColumn("Итоговая стоимость", x => x.TotalCost));
        }
    }
}
