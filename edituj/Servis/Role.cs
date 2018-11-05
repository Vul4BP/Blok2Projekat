using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servis {

    public enum Roles {
        Krompir = 1      //krompir jer "admin" grupa ce uvek postojati na kompu, zato Krompir = Admin
    };    
    public enum Prms {
        CreateDB = 1,
        DeleteDB
    };

    public class Role {
        public Role(Roles cr) {
            currentRole = cr;
            listOfPrms = new List<Prms>();
        }

        public Roles currentRole { get; set; }
        public List<Prms> listOfPrms { get; set; }
    }
}
