using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servis {
    public enum Roles { Reader = 1, Modifier, Administrator, Error };
    public enum Prms { Read = 1, Modify, Delete, Execute, Administrate };
    public class Role {
        public Role(Roles cr) {
            currentRole = cr;
            listOfPrms = new List<Prms>();
        }

        public Roles currentRole { get; set; }
        public List<Prms> listOfPrms { get; set; }
    }
}
