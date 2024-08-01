using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
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
        void Delete(HighBoard highBoard);
        void Update(HighBoard highBoard);
        void Add(HighBoard highBoard);
        List<SelectListItem> selectDeans();
        List<SelectListItem> selectHeads();
        void Save();
    }
}
