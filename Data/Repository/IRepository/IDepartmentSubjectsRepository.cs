using Models;

namespace Data.Repository.IRepository
{
    public interface IDepartmentSubjectsRepository
    {
        List<DepartmentSubjects> subjectsByDepartment(int id);
        bool Exist(DepartmentSubjects model);
        void Add(DepartmentSubjects model);
        void Save();
        List<DepartmentSubjects> GetAll();
        void Delete(DepartmentSubjects model);
        List<DepartmentSubjects> SubjectDepartments(int subjectId);
        DepartmentSubjects? DeleteRelation(int subjectId, int DepartmentId);
    }
}
