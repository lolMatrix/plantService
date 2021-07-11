using System;
using DataBase;
using Entities.Enums;
using System.Collections.Generic;
using Entities.Models;



namespace StatisticAndSolutions 
{
    public class Program
    {
      
        private string Sens ;

        SortedList<string, int[]> DataSave = new SortedList<string, int[]>();
    }
    public class StatisticHandler
    {
        
        public void ReturnList(SortedList<string,int[]> DataSave)
        {
            
        }
        public Data[] GetDataFromTermoSensor(DateTime start, DateTime end)
        {
            Data[] data = new Data[1];
            data[0].TimeMeasurement = start;
            data[1].TimeMeasurement = end;
            return data;
        }
        public Data[] GetDataFromAirSensor(DateTime start, DateTime end)
        {
            Data[] data = new Data[1];
            data[0].TimeMeasurement = start;
            data[1].TimeMeasurement = end;
            return data;
        }
        public Data[] GetDataFromWaterSensor(DateTime start, DateTime end)
        {
            Data[] data = new Data[1];
            data[0].TimeMeasurement = start;
            data[1].TimeMeasurement = end;
            return data;
        }
    }
}

