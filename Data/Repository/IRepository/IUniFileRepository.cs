using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.IRepository
{
    public interface IUniFileRepository
    {
        List<UniFile> GetAllImages();
        List<UniFile> GetAllVideos();
        UniFile GetFile(int id);
        void Update(UniFile File);
        void Add(UniFile File);
        void Delete(UniFile File);
    }
}
