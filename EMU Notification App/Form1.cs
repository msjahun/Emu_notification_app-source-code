using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace website_scraping
{
   
    public partial class Form1 : Form
    {
        DataTable table;
        
       
        HtmlWeb web = new HtmlWeb();
        public Form1()
        {
            InitializeComponent();
            InitTable();
            
        }
        private void InitTable()
        {
            table = new DataTable("GameRankingDataTable");
            
            table.Columns.Add("Name", typeof(string));
           
            gameRankingDataView.DataSource = table;
        }

        private async Task<List<NameAnd>> GameRankingFromPage()
        {
           string url = "https://stdportal.emu.edu.tr/";
           // string url = "C:/Users/Musa Jahun/Desktop/Students Portal.html";



            var doc = await Task.Factory.StartNew(() => web.Load(url));
            var nameNodes = doc.DocumentNode.SelectNodes("//*[@id=\"grdwNews\"]//tr//td//table//tr//td//a");



            if (nameNodes == null)
                return new List<NameAnd>();


            var names = nameNodes.Select(node => node.InnerText);
           var scores = nameNodes.Select(node => node.InnerText);
            return names.Zip(scores, (name, score) => new NameAnd() { Name = name }).ToList();

        }


        private async void Form1_Load(object sender, EventArgs e)
        {

         
            var rankings = await GameRankingFromPage();
          //  while (rankings.Count < 5)
           // {
                foreach (var ranking in rankings)
                    table.Rows.Add(ranking.Name);
                
               
                rankings = await GameRankingFromPage();
                
               
           // }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
    public class NameAnd
    {
        public string Name { get; set; }

    }
}
