using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Integration.Process.Domain.Entities
{
    public  class PointWork
    {
        public  int? Id{ get; set; }        
        public DateTime CreateAt { get; set; }
        public string Name { get; set; }
        public int Registration { get; set; }
        public int ChargeId { get; set; }
        public int EmployeerId { get; set; }
        public Object TotalHours { get; set; }
        public string Charge { get; set; }
    }
}
