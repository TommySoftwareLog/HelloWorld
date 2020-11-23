using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace HelloWorld
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DateTime date = DateTime.Now;
            var t = Task.Run(() => CrawlerStockByDate(date));
            t.Wait();
        }

        public static async Task CrawlerStockByDate(DateTime date)
        {
            using (var client = new HttpClient())
            {
                string json = await client.GetStringAsync($"https://www.twse.com.tw/exchangeReport/MI_INDEX?response=json&date={date.ToString("yyyyMMdd")}&type=ALLBUT0999&_=1586529875476");
                var resDatas = JsonSerializer.Deserialize<Datas>(json); //直接用.NET Core 的 System.Text.Json 來解析資料
            }
        }
    }

    public class Datas
    {
        public List<List<string>> data9 { get; set; }
        public List<string> fields9 { get; set; }
    }
}
