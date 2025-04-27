using Models.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LAHJAAPI.Models
{
    public class PlanServices
    {
        public required long NumberRequests { get; set; }
        [DefaultValue(1)]
        public int Scope { get; set; } = 1;
        [EnumDataType(typeof(ProcessorType))]
        public ProcessorType Processor { get; set; }

        [EnumDataType(typeof(ConnectionType))]
        public ConnectionType ConnectionType { get; set; }

        public string PlanId { get; set; }
        public Plan Plan { get; set; }

        public string ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
