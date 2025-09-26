using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;
using System.Linq;

namespace DataAnalyzer
{
    public partial class Form1 : Form
    {
        private Dictionary<string, List<double>> data;
        private string[] skeleton;
        private List<Chart> charts;

        public Form1()
        {
            InitializeComponent();
            data = new Dictionary<string, List<double>>();
            charts = new List<Chart>();
        }



        private void buttonLoadData_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] lines = File.ReadAllLines(openFileDialog.FileName);
                ParseData(lines);
                CreateCharts();
            }
        }

        private void ParseData(string[] lines)
        {
            data.Clear();
            skeleton = txtSkeleton.Text.Split(';');

            // Инициализируем словарь, привязывая ключи из скелета
            foreach (string key in skeleton)
            {
                data[key] = new List<double>();
            }

            foreach (string line in lines)
            {
                string[] values = line.Split(';');

                // Проверка на совпадение количества данных с количеством ключей в скелете
                if (values.Length != skeleton.Length)
                {
                    continue; // Пропускаем строки с неправильным количеством данных
                }

                for (int i = 0; i < skeleton.Length; i++)
                {
                    // Игнорируем нечисловые данные (например, "ID")
                    if (skeleton[i] == "ID")
                    {
                        continue; // Пропускаем строку ID, так как это не числовое значение
                    }

                    // Преобразуем значения в числа и добавляем в соответствующие ключи
                    if (double.TryParse(values[i], out double result))
                    {
                        if (skeleton[i] == "h") // Высота в см, переводим в метры
                        {
                            data[skeleton[i]].Add(result / 100);
                        }
                        else if (skeleton[i] == "t") // Время в мс, переводим в секунды
                        {
                            data[skeleton[i]].Add(result / 1000);
                        }
                        else if (skeleton[i] == "T") // Температура в градусах, делим на 100
                        {
                            data[skeleton[i]].Add(result / 100);
                        }
                        else if (skeleton[i].StartsWith("a")) // Акселерометр: умножаем на 9.81 и делим на 100
                        {
                            data[skeleton[i]].Add(result * 9.81 / 100);
                        }
                        else if (skeleton[i].StartsWith("g")) // Гироскоп: делим на 100
                        {
                            data[skeleton[i]].Add(result / 100);
                        }
                        else // Другие значения (например, давление)
                        {
                            data[skeleton[i]].Add(result);
                        }
                    }
                    else
                    {
                        continue; // Пропускаем, если не удалось преобразовать в число
                    }
                }
            }
        }

        private void TrimDataBasedOnFlags()
        {
            

            int startIndex = 0;
            int endIndex = data["t"].Count - 1;

            // Если есть срабатывание f1, обрезаем начало данных
            

            // Обрезаем все массивы данных
            foreach (var key in data.Keys.ToList())
            {
                data[key] = data[key].Skip(startIndex).Take(endIndex - startIndex + 1).ToList();
            }
        }





        private void CreateCharts()
        {
            TrimDataBasedOnFlags();

            chartContainer.Controls.Clear();
            charts.Clear();

            // График высоты от времени
            if (data.ContainsKey("t") && data.ContainsKey("h"))
            {
                Chart altitudeChart = CreateChart("Зависимость высоты от времени", "Время (с)", "Высота (м)", data["t"], data["h"]);
                AddChartToContainer(altitudeChart);
            }

            // График давления от времени
            // График давления от времени
            if (data.ContainsKey("t") && data.ContainsKey("p"))
            {
                Chart pressureChart = CreateChart("Зависимость давления от времени", "Время (с)", "Давление (Па)", data["t"], data["p"]);

                // Устанавливаем минимальное значение оси Y рядом с самой нижней точкой
                double minPressure = data["p"].Min();
                pressureChart.ChartAreas[0].AxisY.Minimum = minPressure - (minPressure * 0.05); // Немного ниже минимальной точки

                AddChartToContainer(pressureChart);
            }


            // График высоты от температуры
            if (data.ContainsKey("T") && data.ContainsKey("h"))
            {
                Chart heightTempChart = CreateChart("Зависимость высоты от температуры", "Температура (°C)", "Высота (m)", data["T"], data["h"]);
                heightTempChart.ChartAreas[0].AxisX.LabelStyle.Format = "F2"; // Округляем до сотых
                AddChartToContainer(heightTempChart);
            }

            // График ускорения (по трём осям и векторное ускорение)
            if (data.ContainsKey("t") && data.ContainsKey("aX") && data.ContainsKey("aY") && data.ContainsKey("aZ"))
            {
                Chart accelChart = new Chart();
                accelChart.ChartAreas.Add(new ChartArea("Зависимость линейного ускорения от времени"));

                // Добавляем данные по каждой оси
                AddSeries(accelChart, "ось X", data["t"], data["aX"], SeriesChartType.Line, 3);
                AddSeries(accelChart, "ось Y", data["t"], data["aY"], SeriesChartType.Line, 3);
                AddSeries(accelChart, "ось Z", data["t"], data["aZ"], SeriesChartType.Line, 3);

                // Рассчёт векторного ускорения
                List<double> vectorAccel = data["aX"].Zip(data["aY"], (x, y) => new { x, y })
                    .Zip(data["aZ"], (xy, z) => Math.Sqrt(xy.x * xy.x + xy.y * xy.y + z * z))
                    .ToList();

                AddSeries(accelChart, "Абсолютное ускорение", data["t"], vectorAccel, SeriesChartType.Line, 3);

                accelChart.Titles.Add("Зависимость линейного ускорения от времени");

                // Добавляем легенду
                accelChart.Legends.Add(new Legend());

                // Устанавливаем соотношение сторон 16:9
                accelChart.Width = 960; // 16
                accelChart.Height = 540; // 9

                AddChartToContainer(accelChart);
            }

            // График угловой скорости (по трём осям)
            if (data.ContainsKey("t") && data.ContainsKey("gX") && data.ContainsKey("gY") && data.ContainsKey("gZ"))
            {
                Chart gyroChart = new Chart();
                gyroChart.ChartAreas.Add(new ChartArea("Зависимость угловой скорости от времени"));

                AddSeries(gyroChart, "ось X", data["t"], data["gX"], SeriesChartType.Line, 3);
                AddSeries(gyroChart, "ось Y", data["t"], data["gY"], SeriesChartType.Line, 3);
                AddSeries(gyroChart, "ось Z", data["t"], data["gZ"], SeriesChartType.Line, 3);

                gyroChart.Titles.Add("Зависимость угловой скорости от времени");

                // Добавляем легенду
                gyroChart.Legends.Add(new Legend());

                // Устанавливаем соотношение сторон 16:9
                gyroChart.Width = 960; // 16
                gyroChart.Height = 540; // 9

                AddChartToContainer(gyroChart);
            }
        }

        private Chart CreateChart(string title, string xTitle, string yTitle, List<double> xValues, List<double> yValues)
        {
            Chart chart = new Chart();
            chart.ChartAreas.Add(new ChartArea(title));

            // Устанавливаем размеры графика для соотношения 16:9
            chart.Width = 800; // или нужная вам ширина
            chart.Height = 450; // 800 * 9 / 16 для соотношения 16:9

            AddSeries(chart, title, xValues, yValues, SeriesChartType.Line, 3);

            chart.Titles.Add(title);
            chart.ChartAreas[0].AxisX.Title = xTitle;
            chart.ChartAreas[0].AxisY.Title = yTitle;
            chart.ChartAreas[0].AxisX.LabelStyle.Format = "{0;0,}"; // Округление оси X (время)

            // Добавляем легенду
            chart.Legends.Add(new Legend());

            return chart;
        }

        private void AddSeries(Chart chart, string seriesName, List<double> xValues, List<double> yValues, SeriesChartType chartType, int lineWidth)
        {
            Series series = new Series(seriesName)
            {
                ChartType = chartType,
                XValueType = ChartValueType.Double,
                YValueType = ChartValueType.Double,
                BorderWidth = lineWidth // Толщина линии
            };
            series.Points.DataBindXY(xValues, yValues);
            chart.Series.Add(series);
            chart.ChartAreas[0].AxisX.LabelStyle.Format = "{0;0,}"; // Округление оси X (время)
        }





        private Tuple<List<double>, List<double>> TrimData(List<double> xValues, List<double> yValues, double threshold)
        {
            int startIndex = 0;
            int endIndex = xValues.Count - 1;

            // Найти начало полета
            for (int i = 0; i < yValues.Count; i++)
            {
                if (yValues[i] > threshold)
                {
                    startIndex = i;
                    break;
                }
            }

            // Найти конец полета
            for (int i = yValues.Count - 1; i >= 0; i--)
            {
                if (yValues[i] > threshold)
                {
                    endIndex = i;
                    break;
                }
            }

            // Создание новых обрезанных списков
            var trimmedX = xValues.Skip(startIndex).Take(endIndex - startIndex + 1).Select(x => Math.Round(x / 1000, 2)).ToList();
            var trimmedY = yValues.Skip(startIndex).Take(endIndex - startIndex + 1).Select(y => Math.Round(y / 100, 2)).ToList();

            return new Tuple<List<double>, List<double>>(trimmedX, trimmedY);
        }

        private List<double> ConvertToAcceleration(List<double> values)
        {
            return values.Select(v => Math.Round(v * 9.81 / 100, 2)).ToList();
        }

        private List<double> ConvertToGyro(List<double> values)
        {
            return values.Select(v => Math.Round(v / 100, 2)).ToList();
        }

        private List<double> CalculateVectorAcceleration(List<double> ax, List<double> ay, List<double> az)
        {
            return ax.Zip(ay, (x, y) => new { x, y })
                     .Zip(az, (xy, z) => Math.Round(Math.Sqrt(xy.x * xy.x + xy.y * xy.y + z * z), 2))
                     .ToList();
        }

        

        private void AddChartToContainer(Chart chart)
        {
            chart.Dock = DockStyle.Top;
            chartContainer.Controls.Add(chart);
            charts.Add(chart);
        }

        private void btnSaveCharts_Click(object sender, EventArgs e)
        {
            foreach (var chart in charts)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PNG Image|*.png",
                    Title = "Save Chart as Image File",
                    FileName = chart.Titles[0].Text + ".png"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    chart.SaveImage(saveFileDialog.FileName, ChartImageFormat.Png);
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
