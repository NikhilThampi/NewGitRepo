using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis.UI.Model;

namespace Tennis.Service.Interface
{
     public interface IDashboardService
    {
         List<MemberModel> GetMembersList();
         int Savememberdetails(MemberModel membermodel);
    }
}
