using First_Task.Context;
using First_Task.Interfaces;
using First_Task.Models;

namespace First_Task.Repository
{
    public class FileRepository : IFileRepository
    {
        public int SaveFile(string fileName, DateTime uploadDate)
        {
            try
            {
                using (var context = new ChartDatabaseContext())
                {
                    var fileToSave = new TextFile()
                    {
                        Name = fileName,
                        CreationDate = uploadDate
                    };

                    context.TextFiles.Add(fileToSave);
                    context.SaveChanges();
                    return fileToSave.Id;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TextFile? GetLastFile()
        {
            try
            {
                using (var context = new ChartDatabaseContext())
                {
                    return context.TextFiles.SqlQuery("Select top 1 * from TextFiles Order by Id desc").FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
