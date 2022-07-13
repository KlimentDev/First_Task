using ChartJSCore.Helpers;
using ChartJSCore.Models;
using First_Task.Models;
using System.Drawing;

namespace First_Task.Controllers
{
    public static class FileControllerUtility
    {
        public static FileInfo[] GetFilesFromDirectory()
        {
            DirectoryInfo info = new DirectoryInfo("./UploadedFiles");
            return info.GetFiles().OrderByDescending(p => p.CreationTime).ToArray();
        }

        public static Chart CreateChart(List<FileLine> fileLines, string fileName)
        {
            Chart chart = new Chart();

            chart.Type = Enums.ChartType.Bar;

            Data data = new Data();
            data.Datasets = new List<Dataset>();
            data.Labels = new List<string>() { fileName };

            foreach (var line in fileLines)
            {
                var color = Color.FromName(line.Color);
                BarDataset dataset = new BarDataset()
                {
                    Label = line.Label,
                    Data = new List<double?> { line.Number },
                    BackgroundColor = new List<ChartColor?> { ChartColor.FromRgb(color.R, color.G, color.B) },
                    BorderColor = new List<ChartColor?> { ChartColor.FromRgb(color.R, color.G, color.B) },
                    BorderWidth = new List<int>() { 1 },
                    BarPercentage = 5,
                    BarThickness = 50,
                    MaxBarThickness = 100,
                    MinBarLength = 5

                };
                data.Datasets.Add(dataset);
            }

            chart.Data = data;

            return chart;
        }
    }
}
