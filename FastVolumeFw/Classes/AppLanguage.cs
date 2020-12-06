using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastVolumeFw.Classes
{
    public class AppLanguage
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public AppLanguage(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
