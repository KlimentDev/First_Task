using First_Task.Interfaces;
using First_Task.Models;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace First_Task.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IFileLineRepository _fileLineRepository;

        public FileUploadService(IFileRepository fileRepository, IFileLineRepository fileLineRepository)
        {
            _fileRepository = fileRepository;
            _fileLineRepository = fileLineRepository;
        }

        public async Task<bool> UploadFile(MultipartReader reader, MultipartSection? section)
        {
            var fileDirectory = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "./UploadedFiles"));
            var fullFilePath = string.Empty;
            while (section != null)
            {
                var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(
                    section.ContentDisposition, out var contentDisposition
                );
                if (hasContentDispositionHeader)
                {
                    if (contentDisposition.DispositionType.Equals("form-data") &&

                    (!string.IsNullOrEmpty(contentDisposition.FileName.Value) ||
                    !string.IsNullOrEmpty(contentDisposition.FileNameStar.Value)))
                    {
                        byte[] fileArray;
                        using (var memoryStream = new MemoryStream())
                        {
                            await section.Body.CopyToAsync(memoryStream);
                            fileArray = memoryStream.ToArray();
                        }

                        fullFilePath = Path.Combine(fileDirectory, contentDisposition.FileName.Value);

                        using (var fileStream = File.Create(fullFilePath))
                        {
                            await fileStream.WriteAsync(fileArray);
                        }
                    }
                }
                section = await reader.ReadNextSectionAsync();
            }
            SaveRowsWithFileId(fullFilePath);

            return true;
        }

        public void SaveRowsWithFileId(string fullFilePath)
        {
            var file = new FileInfo(fullFilePath);
            var preparedLines = PrepareFileLines(fullFilePath);
            if (preparedLines.Count > 0)
            {
                var fileId = _fileRepository.SaveFile(file.Name, file.CreationTime);
                preparedLines.ForEach(x => x.TextFileId = fileId);
                _fileLineRepository.SaveRows(preparedLines);
            }
        }

        public List<FileLine> PrepareFileLines(string filePath)
        {
            List<FileLine> validFileLines = new List<FileLine>();
            using (StreamReader streamReader = File.OpenText(filePath))
            {
                string text = streamReader.ReadToEnd();
                string[] lines = text.Split("-");

                foreach (string line in lines)
                {
                    if (line.Length > 0)
                    {
                        string correctedLine = RemoveCharactersFromLine(line);

                        FileLine fileLine = Parse(correctedLine);
                        if (fileLine != null)
                        {
                            validFileLines.Add(fileLine);
                        }
                    }
                }
            }

            return validFileLines;
        }

        public string RemoveCharactersFromLine(string line)
        {
            line = line.Trim();
            line = line.Replace("\r\n", string.Empty);

            return line;
        }

        public FileLine Parse(string line)
        {
            try
            {
                var columns = line.Split(",");
                FileLine fileLine = new FileLine();
                fileLine.Color = columns[0].Trim();
                fileLine.Number = int.Parse(columns[1]);
                fileLine.Label = columns[2];
                fileLine.Validate();
                return fileLine;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
