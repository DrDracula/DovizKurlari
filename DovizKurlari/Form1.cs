using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace DovizKurlari
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load("http://www.tcmb.gov.tr/kurlar/201401/02012014.xml");
            XmlElement rooteleman = xmldoc.DocumentElement;
            XmlNodeList liste = rooteleman.GetElementsByTagName("Currency");
            List<Doviz> dlist = new List<Doviz>();


            foreach (XmlElement item in liste)
            {
                Doviz d = new Doviz();
                XmlElement currency = item;
                string code = currency.Attributes["CurrencyCode"].Value;

                string isim = currency.GetElementsByTagName("Isim").Item(0).InnerText;

                d.CurrencyName = isim;
                string alisFiyat = currency.GetElementsByTagName("ForexBuying").Item(0).InnerText;
                string satisFiyat = currency.GetElementsByTagName("ForexSelling").Item(0).InnerText;

                if (!string.IsNullOrEmpty(alisFiyat))
                {
                    d.ForexBuying = Convert.ToDecimal(alisFiyat);
                }
                if (!string.IsNullOrEmpty(satisFiyat))
                {
                    d.ForexSelling = Convert.ToDecimal(satisFiyat);
                }
                if (!(code is null))
                {
                    d.CurrencyCode = code;
                }
                listBox1.Items.Add(d);
                dlist.Add(d);
            }
            dataGridView1.DataSource = dlist;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Doviz secilenDoviz = (Doviz)listBox1.SelectedItem;
            label5.Text = secilenDoviz.ForexBuying.ToString();
            label6.Text = secilenDoviz.ForexSelling.ToString();
            label4.Text = secilenDoviz.CurrencyName;
        }
    }
}
