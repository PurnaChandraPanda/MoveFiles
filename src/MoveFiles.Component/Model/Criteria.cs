using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MoveFiles.Component.Model
{
    public class Criteria
    {
        public string FilterFor { get; set; }
        public FilterCriteria FilterKind { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
    }

    public enum FilterCriteria
    {
        Equals = 0,     
        Contains,
        NotEquals,
        NotContains,
        None
    }
}
