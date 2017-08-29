using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PGAlineupBuilder.ViewModels
{
    public class JqGridInViewModel
    {
        //number of records to fetch
        public int rows { get; set; }

        // the page number
        public int page { get; set; }

        // sort the column
        public string sidx { get; set; }

        // sort order
        public string sord { get; set; }

    }
}
