using System;
using System.Collections.Generic;

namespace DataAnalyzer
{
    public class DataProcessor
    {
        private List<string> fieldOrder;

        public DataProcessor(List<string> initialFieldOrder)
        {
            fieldOrder = new List<string>(initialFieldOrder);
        }

        public void UpdateFieldOrder(List<string> newOrder)
        {
            fieldOrder = new List<string>(newOrder);
        }

        public void ProcessData(string[] data)
        {
            Dictionary<string, string> parsedData = new Dictionary<string, string>();

            for (int i = 0; i < fieldOrder.Count; i++)
            {
                parsedData[fieldOrder[i]] = data[i];
            }

            // Обработка данных (например, получаем давление)
            string pressure = parsedData["Pressure"];
            // Дополнительная логика обработки данных
        }

        public void ProcessTelemetry(string[] telemetryData)
        {
            Dictionary<string, string> parsedTelemetry = new Dictionary<string, string>();

            for (int i = 0; i < fieldOrder.Count; i++)
            {
                parsedTelemetry[fieldOrder[i]] = telemetryData[i];
            }

            // Обработка телеметрических данных (например, получаем высоту)
            string altitude = parsedTelemetry["Altitude"];
            // Дополнительная логика обработки телеметрии
        }
    }
}
