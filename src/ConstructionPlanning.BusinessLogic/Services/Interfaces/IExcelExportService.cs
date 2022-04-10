using ConstructionPlanning.BusinessLogic.DTO;

namespace ConstructionPlanning.BusinessLogic.Services.Interfaces
{
    /// <summary>
    /// Сервис для экспорта данных в формат .xlsc
    /// </summary>
    public interface IExcelExportService<T>
        where T : IBaseDtoObject
    {
        /// <summary>
        /// Возвращает файл в виде массива байт.
        /// </summary>
        byte[] Export(IEnumerable<T> data);
    }
}
