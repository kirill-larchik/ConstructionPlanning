using ConstructionPlanning.BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConstructionPlanning.WebApplication.Models.Project
{
    public class ProjectViewModel
    {
        [Display(Name = "Номер проекта")]
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Дата создания")]
        [DataType(DataType.Date)]
        public DateTime DateOfCreate { get; set; }

        [Display(Name = "Крайний срок")]
        [DataType(DataType.Date)]
        public DateTime Deadline { get; set; }

        [Display(Name = "Выделенные средства")]
        public int AllocatedAmount { get; set; }

        [Display(Name = "Итоговые затраты")]
        public int TotalCost { get; set; }

        [Display(Name = "Заказчик")]
        public string CustomerName { get; set; }

        public IEnumerable<ConstructionObjectDto> ConstructionObjects { get; set; }
    }
}
