using Data.Repository.IRepository;
using Models;
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

        public void Delete(HighBoard highBoard)
        {
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
            var highboards = context.HighBoards
                .ToList();
            return highboards;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
