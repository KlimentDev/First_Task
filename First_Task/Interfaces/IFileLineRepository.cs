using First_Task.Models;

namespace First_Task.Interfaces
{
    public interface IFileLineRepository
    {
        public void SaveRows(List<FileLine> fileLines);

        public List<FileLine> GetRowsById(int id);
    }
}
