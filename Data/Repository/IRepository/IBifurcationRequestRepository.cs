using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.IRepository
{
    public interface IBifurcationRequestRepository
    {
        void Save();
        void Add(BifurcationRequest bifurcationRequest);
    }
}
