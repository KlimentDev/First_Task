using First_Task.Context;
using First_Task.Interfaces;
using First_Task.Models;

namespace First_Task.Repository
{
    public class FileLineRepository : IFileLineRepository
    {
        public void SaveRows(List<FileLine> fileLines)
        {
            try
            {
                using var context = new ChartDatabaseContext();
                context.BulkInsert(fileLines, options => options.AutoMapOutputDirection = false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<FileLine> GetRowsById(int id)
        {
            try
            {
                using var context = new ChartDatabaseContext();
                return context.FileLines
                        .SqlQuery($"Select * from FileLines where TextFileId = {id}")
                        .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
