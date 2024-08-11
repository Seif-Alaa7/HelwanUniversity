using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.IRepository
{
    public interface IHighBoardRepository
    {
        List<HighBoard> GetAll();
        HighBoard GetOne(int Id);
        void Delete(int id);
        void Update(HighBoard highBoard);
        void Add(HighBoard highBoard);
        List<SelectListItem> selectDeans();
        List<SelectListItem> selectHeads();
        void Save();
        bool ExistJop(JobTitle JobTitle);
        bool ExistName(string name);
        string GetName(int id);
        HighBoard GetPresident();
        IQueryable<HighBoard> GetHeads();
        IQueryable<HighBoard> GetDeans();
        void DeleteUser(string id);
    }
}
