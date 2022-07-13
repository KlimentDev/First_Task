using First_Task.Models;

namespace First_Task.Interfaces
{
    public interface IFileRepository
    {
        public int SaveFile(string fileName, DateTime uploadDate);

        public TextFile? GetLastFile();
    }
}
