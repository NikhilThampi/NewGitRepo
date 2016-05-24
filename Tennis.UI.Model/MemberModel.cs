using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tennis.UI.Model
{
    public partial class MemberModel
    {
        public int Id { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public Nullable<int> Seed { get; set; }
        public string address { get; set; }
    }
}
