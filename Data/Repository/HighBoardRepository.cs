using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class HighBoardRepository : IHighBoardRepository
    {
        private readonly ApplicationDbContext context;

        public HighBoardRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Add(HighBoard highBoard)
        {
            context.HighBoards.Add(highBoard);
        }

        public void Update(HighBoard highBoard)
        {
            context.HighBoards.Update(highBoard);
        }

        public void Delete(int id)
        {
            var highBoard = GetOne(id);
            context.HighBoards.Remove(highBoard);
        }

        public HighBoard GetOne(int Id)
        {
            var highboard = context.HighBoards
                .Find(Id);
            return highboard;
        }

        public List<HighBoard> GetAll()
        {
            var highboards = context.HighBoards.ToList();
            return highboards;
        }
        public List<SelectListItem> selectDeans()
        {
            var options = context.HighBoards.Where(x => x.JobTitle == Models.Enums.JobTitle.DeanOfFaculty).Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name
            }).ToList();
            return options;
        }
        public List<SelectListItem> selectHeads()
        {
            var options = context.HighBoards.Where(x => x.JobTitle == Models.Enums.JobTitle.HeadOfDepartment).Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name
            }).ToList();
            return options;
        }
        public void Save()
        {
            context.SaveChanges();
        }
        public bool ExistName(string name)
        {
            var exist = context.HighBoards.Any(x => x.Name == name);
            return exist;
        }
        public bool ExistJop(JobTitle JobTitle)
        {
            var exist = context.HighBoards.Any(x=>x.JobTitle == JobTitle);
            return exist;
        }
        public string GetName(int id)
        {
            var name = context.HighBoards.FirstOrDefault(x=>x.Id == id)?.Name;
            return name;
        }
        public HighBoard GetPresident()
        {
            var president = context.HighBoards.FirstOrDefault(x=>x.JobTitle == JobTitle.President);
            return president;
        }
    }
}
