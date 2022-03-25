using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelperLand.ViewModels
{
    public class ScheduleAndPlanViewModel
    {
        public string Code { get; set; }


        [Required(ErrorMessage = "Please select service date")]
        public DateTime ServiceStartDate { get; set; }

        
        public DateTime StartTime { get; set; }

        
        public float ServiceHrs { get; set; }

        public float ExtraHours { get; set; }

        public string Comments { get; set; }

        public bool HavePets { get; set; }

        public float Total { get; set; }

        public int saveHelper { get; set; }
    }
}