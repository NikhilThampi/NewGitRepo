using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tennis.Service;
using Tennis.Service.Interface;
using Tennis.UI;
using Tennis.UI.Model;
using Tennis.UI.ViewModel;


namespace Tennis.UI.Controllers
{
    public class DashboardController : Controller
    {
       
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dasboardservice)
        {
            _dashboardService = dasboardservice;
        }

        [HttpGet]
        public ActionResult MembersList(int MemberId = 0)
        {
            
                DashboardViewModel dashboardviewmodel = new DashboardViewModel();
                dashboardviewmodel.memberslist  = _dashboardService.GetMembersList();
                return View(dashboardviewmodel);
            
        }

        //
        // GET: /Dashboard/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {

            return PartialView("_CreateMember");
        }
        [HttpPost]
        public ActionResult Create(DashboardViewModel dashboardviewmodel)
        {
            
            
                int res = _dashboardService.Savememberdetails(dashboardviewmodel.CreateMemberModel);



                return RedirectToAction("MembersList");
        }

    }
}
