using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.IRepository
{
    public interface ISubjectRepository
    {
        List<Subject> GetAll();
        Subject GetOne(int Id);
        void Delete(Subject subject);
        void Update(Subject subject);
        void Add(Subject subject);
        List<SelectListItem> Select();
        void Save();
        bool ExistSubject(string Subject);
    }
}
