using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DataAnalyzer
{
    public partial class Form1 : Form
    {
        private List<DataEntry> dataEntries;
        private float initialPressure = 95000; // Начальное значение давления

        public Form1()
        {
            InitializeComponent();
        }

        private void BtnLoadFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadDataFromFile(openFileDialog.FileName);
                CreateCharts();
                
            }
        }

        private void BtnLoadFile2_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadDataFromFile2(openFileDialog.FileName);
                CreateChartsForFile2();
            }
        }


        private void BtnSaveCharts_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                SaveAllCharts(folderBrowserDialog.SelectedPath);
            }
        }

        private void BtnSetInitialPressure_Click(object sender, EventArgs e)
        {
            string userInput = Microsoft.VisualBasic.Interaction.InputBox("Введите начальное значение давления (в Па):", "Настройка начального давления", initialPressure.ToString());

            if (!string.IsNullOrEmpty(userInput))
            {
                if (float.TryParse(userInput, out float newInitialPressure))
                {
                    initialPressure = newInitialPressure;
                    MessageBox.Show($"Начальное давление установлено на {initialPressure} Па.");
                }
                else
                {
                    MessageBox.Show("Некорректное значение давления.");
                }
            }
        }

        private void LoadDataFromFile(string filePath)
        {
            dataEntries = new List<DataEntry>();

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(';');
                if (parts.Length == 15)
                {
                    dataEntries.Add(new DataEntry
                    {
                        TeamID = parts[0],
                        Time = float.Parse(parts[1]) / 1000,
                        Altitude = float.Parse(parts[2]) / 100,
                        Pressure = float.Parse(parts[3]),
                        Temperature = float.Parse(parts[4]) / 100,
                        Accel1 = float.Parse(parts[5]) * 9.81f / 100,
                        Accel2 = float.Parse(parts[6]) * 9.81f / 100,
                        Accel3 = float.Parse(parts[7]) * 9.81f / 100,
                        Gyro1 = float.Parse(parts[8]) / 100,
                        Gyro2 = float.Parse(parts[9]) / 100,
                        Gyro3 = float.Parse(parts[10]) / 100,
                        Flag1 = int.Parse(parts[11]),
                        Flag2 = int.Parse(parts[12]),
                        Flag3 = int.Parse(parts[13]),
                        Flag4 = int.Parse(parts[14])
                    });
                }
            }

            Console.WriteLine($"Загружено {dataEntries.Count} записей.");
            Console.WriteLine($"Количество записей с Flag1 == 1: {dataEntries.Count(entry => entry.Flag1 == 1)}");
            Console.WriteLine($"Количество записей с Flag4 == 1: {dataEntries.Count(entry => entry.Flag4 == 1)}");
        }

        private void LoadDataFromFile2(string filePath)
        {
            dataEntries = new List<DataEntry>();

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(';');
                if (parts.Length == 10)
                {
                    dataEntries.Add(new DataEntry
                    {
                        TeamID = parts[0],
                        Time = float.Parse(parts[1]) / 1000,
                        Altitude = float.Parse(parts[2]) / 100,
                        Accel1 = float.Parse(parts[3]) * 9.81f / 100,
                        Accel2 = float.Parse(parts[4]) * 9.81f / 100,
                        Accel3 = float.Parse(parts[5]) * 9.81f / 100,
                        Flag1 = int.Parse(parts[6]),
                        Flag2 = int.Parse(parts[7]),
                        Flag3 = int.Parse(parts[8]),
                        Flag4 = int.Parse(parts[9])
                    });
                }
            }

            Console.WriteLine($"Загружено {dataEntries.Count} записей.");

            // Фильтрация данных до 10 строк до флага старта и после флага приземления
            FilterDataByFlags();
        }

        private void FilterDataByFlags()
        {
            // Находим индекс старта (первый индекс, где хотя бы один флаг == 1)
            int startIndex = dataEntries.FindIndex(entry => entry.Flag1 == 1 || entry.Flag2 == 1 || entry.Flag3 == 1 || entry.Flag4 == 1);
            int endIndex = dataEntries.FindLastIndex(entry => entry.Flag1 == 1 || entry.Flag2 == 1 || entry.Flag3 == 1 || entry.Flag4 == 1);

            // Если старт не найден, выводим сообщение об ошибке
            if (startIndex == -1)
            {
                MessageBox.Show("Записи с флагами, равными 1, не найдены.");
                return;
                
            }

            // Обрезаем данные до 10 строк до старта и после приземления
            int startTrimIndex = Math.Max(0, startIndex - 10);
            int endTrimIndex = Math.Min(dataEntries.Count - 1, endIndex + 10);

            dataEntries = dataEntries.GetRange(startTrimIndex, endTrimIndex - startTrimIndex + 1);
        }
        

        private void CreateCharts()
        {
            chart1.Controls.Clear();

            var startIdx = dataEntries.FindIndex(entry => entry.Flag1 == 1 || entry.Flag2 == 1 || entry.Flag3 == 1 || entry.Flag4 == 1);
            var endIdx = dataEntries.FindLastIndex(entry => entry.Flag1 == 1 || entry.Flag2 == 1 || entry.Flag3 == 1 || entry.Flag4 == 1);

            if (startIdx == -1 || endIdx == -1)
            {
                MessageBox.Show("Не удалось найти записи с флагом 1 в данных.");
                return;
            }

            startIdx = Math.Max(0, startIdx - 10);
            endIdx = Math.Min(dataEntries.Count - 1, endIdx + 10);

            var filteredData = dataEntries.Skip(startIdx).Take(endIdx - startIdx + 1).ToList();

            if (filteredData.Count == 0)
            {
                MessageBox.Show("Нет данных для построения графиков.");
                return;
            }

            var timeData = filteredData.Select(entry => entry.Time).ToArray();

            CreateChart("Зависимость высоты от времени", "Время (с)", "Высота (м)", false, false, timeData, filteredData.Select(entry => entry.Altitude).ToArray());
            CreateChart("Зависимость давления от времени", "Время (с)", "Давление (Па)", false, false, timeData, filteredData.Select(entry => entry.Pressure).ToArray());
            CreateChart("Зависимость высоты от температуры", "Температура (°C)", "Высота (м)", false, true, filteredData.Select(entry => entry.Temperature).ToArray(), filteredData.Select(entry => entry.Altitude).ToArray());

            var accelX = filteredData.Select(entry => entry.Accel1).ToArray();
            var accelY = filteredData.Select(entry => entry.Accel2).ToArray();
            var accelZ = filteredData.Select(entry => entry.Accel3).ToArray();
            var vectorAccel = new float[accelX.Length];

            for (int i = 0; i < accelX.Length; i++)
            {
                vectorAccel[i] = (float)Math.Sqrt(Math.Pow(accelX[i], 2) + Math.Pow(accelY[i], 2) + Math.Pow(accelZ[i], 2));
            }

            var chartAltitude = CreateChart("Зависимость высоты от времени", "Время (с)", "Высота (м)", false, false, timeData, filteredData.Select(entry => entry.Altitude).ToArray());
            chart1.Controls.Add(chartAltitude);

            var chartPressure = CreateChart("Зависимость давления от времени", "Время (с)", "Давление (Па)", false, false, timeData, filteredData.Select(entry => entry.Pressure).ToArray());
            chart1.Controls.Add(chartPressure);

            var chartHeightTemperature = CreateChart("Зависимость высоты от температуры", "Температура (°C)", "Высота (м)", false, true, filteredData.Select(entry => entry.Temperature).ToArray(), filteredData.Select(entry => entry.Altitude).ToArray());
            chart1.Controls.Add(chartHeightTemperature);

            var chartAcceleration = CreateChart("Зависимость линейного ускорения от времени", "Время (с)", "Линейное ускорение (м/с²)", true, false, timeData, accelX, accelY, accelZ, vectorAccel);
            AddZeroLine(chartAcceleration.ChartAreas[0]); // Добавление линии с цифрой 0 только для этого графика
            chart1.Controls.Add(chartAcceleration);

            var chartAngularVelocity = CreateChart("Зависимость угловой скорости от времени", "Время (с)", "Угловая скорость (°/с)", true, false, timeData,
                filteredData.Select(entry => entry.Gyro1).ToArray(),
                filteredData.Select(entry => entry.Gyro2).ToArray(),
                filteredData.Select(entry => entry.Gyro3).ToArray());
            AddZeroLine(chartAngularVelocity.ChartAreas[0]); // Добавление линии с цифрой 0 только для этого графика
            chart1.Controls.Add(chartAngularVelocity);
        }

        private Chart CreateChart(string title, string xAxisTitle, string yAxisTitle, bool addSeriesNames, bool isTemperature, float[] xData, params float[][] yDataSeries)
        {
            var chart = new Chart
            {
                Width = 1000,
                Height = 600
            };

            var chartArea = new ChartArea();
            chart.ChartAreas.Add(chartArea);

            var legend = new Legend();
            chart.Legends.Add(legend);

            string[] seriesNames = { "X", "Y", "Z", "Векторное ускорение" };

            for (int i = 0; i < yDataSeries.Length; i++)
            {
                var series = new Series
                {
                    ChartType = SeriesChartType.Line,
                    Name = addSeriesNames ? seriesNames[i] : "Линия зависимости",
                    BorderWidth = 3
                };

                HashSet<float> xValuesSet = new HashSet<float>();

                for (int j = 0; j < xData.Length; j++)
                {
                    float xValue = xData[j];
                    if (!xValuesSet.Contains(xValue))
                    {
                        xValuesSet.Add(xValue);

                        string xLabel = xAxisTitle == "Время (с)" ? xValue.ToString("0") : xValue.ToString("0.##");

                        float yValue = yDataSeries[i][j];
                        // Округляем значение высоты, если график зависимости высоты от температуры
                        if (title.Contains("Зависимость высоты от температуры"))
                        {
                            yValue = (float)Math.Round(yValue);
                        }

                        series.Points.AddXY(xLabel, yValue);
                    }
                }

                chart.Series.Add(series);
            }

            chart.Titles.Add(title);
            chartArea.AxisX.Title = xAxisTitle;
            chartArea.AxisY.Title = yAxisTitle;

            if (yAxisTitle == "Давление (Па)")
            {
                // Устанавливаем минимальное значение оси Y для графика давления с учетом начального давления
                chartArea.AxisY.Minimum = initialPressure;
            }
            else
            {
                FormatChartAreaAxes(chartArea, isTemperature, title.Contains("Зависимость высоты от температуры"));
            }

            return chart;
        }

        private void CreateChartsForFile2()
        {
            chart1.Controls.Clear();

            if (dataEntries.Count == 0)
            {
                MessageBox.Show("Нет данных для построения графиков.");
                return;
            }

           var timeData = dataEntries.Select(entry => entry.Time).ToArray();
           var altitudeData = dataEntries.Select(entry => entry.Altitude).ToArray();

            // Создаем график зависимости высоты от времени
            var chartHeightTime = CreateChart("Зависимость высоты от времени", "Время (с)", "Высота (м)", false, false, timeData, altitudeData);
            chart1.Controls.Add(chartHeightTime);

            var accelX = dataEntries.Select(entry => entry.Accel1).ToArray();
            var accelY = dataEntries.Select(entry => entry.Accel2).ToArray();
            var accelZ = dataEntries.Select(entry => entry.Accel3).ToArray();
            var vectorAccel = new float[accelX.Length];

            for (int i = 0; i < accelX.Length; i++)
            {
                vectorAccel[i] = (float)Math.Sqrt(Math.Pow(accelX[i], 2) + Math.Pow(accelY[i], 2) + Math.Pow(accelZ[i], 2));
            }

            var chartAcceleration = CreateChart("Зависимость линейного ускорения от времени", "Время (с)", "Линейное ускорение (м/с²)", true, false, timeData, accelX, accelY, accelZ, vectorAccel);
            AddZeroLine(chartAcceleration.ChartAreas[0]); // Добавление линии с цифрой 0 только для этого графика
            chart1.Controls.Add(chartAcceleration);
        }

        private void CreateHeightTimeChart()
        {
            var timeData = dataEntries.Select(entry => entry.Time).ToArray();
            var altitudeData = dataEntries.Select(entry => entry.Altitude).ToArray();

            var chartHeightTime = CreateChart("Зависимость высоты от времени", "Время (с)", "Высота (м)", false, false, timeData, altitudeData);
            chart1.Controls.Add(chartHeightTime);
        }




        private void AddZeroLine(ChartArea chartArea)
        {
            var zeroLine = new StripLine
            {
                IntervalOffset = 0,
                StripWidth = 0.1,
                Interval = 0,
                BorderColor = Color.Red,
                BorderWidth = 2
            };
            chartArea.AxisY.StripLines.Add(zeroLine);
        }

        private void SetYAxisLimits(ChartArea chartArea, float[][] yDataSeries)
        {
            float minY = yDataSeries.SelectMany(y => y).Min();
            float maxY = yDataSeries.SelectMany(y => y).Max();

            float padding = (maxY - minY) * 0.0f;

            chartArea.AxisY.Minimum = minY - padding;
            chartArea.AxisY.Maximum = maxY + padding;
        }

        private void FormatChartAreaAxes(ChartArea chartArea, bool isTemperature, bool isHeightVsTemperature)
        {
            chartArea.AxisX.LabelStyle.Format = "0.#";

            if (isHeightVsTemperature)
            {
                chartArea.AxisY.LabelStyle.Format = "0";
            }
            else if (isTemperature)
            {
                chartArea.AxisY.LabelStyle.Format = "0.##";
            }
            else
            {
                chartArea.AxisY.LabelStyle.Format = GetAxisFormat(chartArea.AxisY.Minimum, chartArea.AxisY.Maximum);
            }
        }

        private string GetAxisFormat(double minValue, double maxValue)
        {
            double range = maxValue - minValue;

            if (range <= 10)
            {
                return "0.##";
            }
            else if (range <= 100)
            {
                return "0.#";
            }
            else
            {
                return "0";
            }
        }

        private void SaveAllCharts(string folderPath)
        {
            foreach (var control in chart1.Controls)
            {
                if (control is Chart chart)
                {
                    var chartTitle = chart.Titles[0].Text;
                    var sanitizedTitle = string.Concat(chartTitle.Split(Path.GetInvalidFileNameChars()));
                    var filePath = Path.Combine(folderPath, $"{sanitizedTitle}.png");
                    chart.SaveImage(filePath, ImageFormat.Png);
                }
            }

            MessageBox.Show("Графики успешно сохранены.");
        }

        private void SaveChart(Chart chart, string fileName)
        {
            using (Bitmap bmp = new Bitmap(chart.Width, chart.Height))
            {
                chart.DrawToBitmap(bmp, new Rectangle(0, 0, chart.Width, chart.Height));
                bmp.Save(fileName, ImageFormat.Png);
            }
        }

        public class DataEntry
        {
            public string TeamID { get; set; }
            public float Time { get; set; }
            public float Pressure { get; set; }
            public float Temperature { get; set; }
            public float Altitude { get; set; }
            public float Accel1 { get; set; }
            public float Accel2 { get; set; }

            public float Accel3 { get; set; }
            public float Gyro1 { get; set; }
            public float Gyro2 { get; set; }
            public float Gyro3 { get; set; }
            public int Flag1 { get; set; }
            public int Flag2 { get; set; }
            public int Flag3 { get; set; }
            public int Flag4 { get; set; }
        }
    }
}


