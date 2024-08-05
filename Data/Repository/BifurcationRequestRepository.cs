using Data.Repository.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class BifurcationRequestRepository : IBifurcationRequestRepository
    {
        private readonly ApplicationDbContext context;
        public BifurcationRequestRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Save()
        {
            context.SaveChanges();
        }
        public void Add(BifurcationRequest bifurcationRequest)
        {
            context.BifurcationRequests.Add(bifurcationRequest);
        }
    }
}