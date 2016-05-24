using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tennis.UI.Model;


namespace Tennis.UI.ViewModel
{
    public class DashboardViewModel
    {
        public List<MemberModel> memberslist;

        public MemberModel CreateMemberModel { get; set; }
        
    }
}