using System.Collections.Generic;

namespace KPMGAssignment.Models.ViewModels
{
    public class ImportResultViewModel
    {
        public int LinesImported { get; set; }

        public List<string> LinesIgnored { get; set; }
    }
}
