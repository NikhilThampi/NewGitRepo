using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis.Service.Interface;
using Tennis.Entity.DB;
using AutoMapper;
using Tennis.UI.Model;
using Tennis.DAL;
using Tennis.DAL.Repositories;

namespace Tennis.Service.Implimentation
{
    public class DashboardService : IDashboardService
    {
        public DashboardService()
        {
            Mapper.CreateMap<Tennis.Entity.DB.Member, Tennis.UI.Model.MemberModel>();
            Mapper.CreateMap<Tennis.UI.Model.MemberModel, Tennis.Entity.DB.Member>();
        }

        public List<MemberModel> GetMembersList()
        {
            UnitOfWork unitofwork = new UnitOfWork();
            Repository<Member> memberrep = unitofwork.GetRepInstance<Member>();

            var memberlist = memberrep.GetAll().Select(x => new {x.Id,x.Forename,x.Surname,x.address,x.Seed}).ToList();

            var reslist = from t in memberlist
                          select new MemberModel
                          { 
                              Id = t.Id,
                              Forename = t.Forename,
                              Surname = t.Surname,                          
                              address = t.address,
                              Seed = t.Seed

                          };

            return reslist.ToList();
        }

        public int Savememberdetails(MemberModel membermodel)
        {
            int memberId = 0;
            UnitOfWork unitofwork = new UnitOfWork();
            try
            {
                Repository<Member> repmem = unitofwork.GetRepInstance<Member>();
                Member updatemember = repmem.GetSingle(x => x.Id == membermodel.Id);

                if (updatemember == null)
                {
                    Member addmember = Mapper.Map<MemberModel, Member>(membermodel);
                       repmem.Add(addmember);
                       unitofwork.Commit();
                       memberId = addmember.Id;

                }
                else
                {
                    updatemember.address = membermodel.address;
                    updatemember.Forename = membermodel.Forename;
                    updatemember.Seed = membermodel.Seed;
                    updatemember.Surname = membermodel.Surname;

                    repmem.Update(updatemember);
                    unitofwork.Commit();
                    memberId = membermodel.Id;

                }
            }
            catch(Exception ex)
            {

            }
            return memberId;
        }

    }


}
