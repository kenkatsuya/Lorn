using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {//<dt:ローン開始月を選択させる　borrwing: ローン総額　year:ローン返済期間（年 * 12）rate: ローン年利>
        private DateTime dt = DateTime.Now;
        private int borrowing;
        private int year;
        private double rate;
        private int saveBorrowing;


        public Form1()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dt = dateTimePicker1.Value;
        }

        private void Borrowing_textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                borrowing = int.Parse(textBox1.Text) * 10000;
                saveBorrowing = borrowing;
            }
            catch
            {
                //MessageBox.Show("正しい値を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BorrowingPeriod_comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            year = Convert.ToInt32(comboBox1.SelectedItem);
        }

        private void Ganrikintou_CheckedChanged(object sender, EventArgs e) { }

        private void Gankinkintou_CheckedChanged(object sender, EventArgs e) { }

        private void Rate_textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                rate = double.Parse(textBox3.Text) * 0.01;
            }
            catch 
            {
                //MessageBox.Show("0～100までの正しい値を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            //境界線を3Dにする
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.GridColor = Color.White;
            //背景色
            dataGridView1.DefaultCellStyle.BackColor = Color.WhiteSmoke;

            //交互に背景色を付ける
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Lavender;

            // カラム数を指定
            dataGridView1.ColumnCount = 8;

            // カラム名を指定
            dataGridView1.Columns[0].HeaderText = "返済月";
            dataGridView1.Columns[1].HeaderText = "返済回数";
            dataGridView1.Columns[2].HeaderText = "返済額";
            dataGridView1.Columns[3].HeaderText = "元金";
            dataGridView1.Columns[4].HeaderText = "利息";
            dataGridView1.Columns[5].HeaderText = "残高";
            dataGridView1.Columns[6].HeaderText = "ﾎﾞｰﾅｽ返済";
            dataGridView1.Columns[7].HeaderText = "繰越返済";
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //ヘッダーとすべてのセルをグリッドいっぱいに、列の幅を自動調整する
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //計算表示フィールドを個別にセンタリング、右寄せを指定する
            dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //境界線を3Dにする
            dataGridView2.BorderStyle = BorderStyle.Fixed3D;
            // カラム数を指定
            dataGridView2.ColumnCount = 7;

            // カラム名を指定
            dataGridView2.Columns[0].HeaderText = "返済方法";
            dataGridView2.Columns[1].HeaderText = "金利(年利)";
            dataGridView2.Columns[2].HeaderText = "支払総回数";
            dataGridView2.Columns[3].HeaderText = "支払い終了";
            dataGridView2.Columns[4].HeaderText = "返済総額";
            dataGridView2.Columns[5].HeaderText = "元金";
            dataGridView2.Columns[6].HeaderText = "利息分";
            dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //ヘッダーとすべてのセルをグリッドいっぱいに、列の幅を自動調整する
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //計算表示フィールドを個別にセンタリング、右寄せを指定する
            dataGridView2.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView2.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView2.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();　//2回目の描画用
            dataGridView2.Rows.Clear();

            string date;
            int monthlyPayment;
            int sougaku = borrowing;
            int resultBorrowing = borrowing;
            int sumLorn = 0;
            int sumRisoku = 0;
            string rb_Name = "";

            if (radioButton1.Checked == true)
            {
                rb_Name = "元利均等";
                monthlyPayment = (int)(borrowing * rate / 12 * Math.Pow((double)1 + (rate / 12), year * 12) / (Math.Pow((double)1 + (rate / 12), year * 12) - 1));
                for (int i = 0; i < year * 12; i++)
                {
                    
                    date = dt.AddMonths(i).ToString("yyyy年MM月");

                    int risoku = (int)(borrowing * (rate / 12)+0.001);
                    int gankin = (int)(monthlyPayment - risoku);
                    borrowing -= gankin;
                    

                    if (i == year * 12 - 1)
                    {
                        dataGridView1.Rows.Add($"{date}", $"{i + 1:N0}", $"{monthlyPayment+ borrowing:N0}", $"{monthlyPayment + borrowing - risoku:N0}", $"{risoku:N0}", 0);
                        sumLorn += monthlyPayment + borrowing;
                    }
                    else
                    {
                        dataGridView1.Rows.Add($"{date}", $"{i + 1:N0}", $"{monthlyPayment:N0}", $"{gankin:N0}", $"{risoku:N0}", $"{borrowing:N0}");
                        sumLorn += monthlyPayment;
                    }
                    
                    sumRisoku += risoku;

                }
            }

            else if (radioButton2.Checked == true)
            {
                rb_Name = "元金均等";
                for (int i = 0; i < year * 12; i++)
                {
                    date = dt.AddMonths(i).ToString("yyyy年MM月");
                    int gankin = borrowing / (year * 12);
                    monthlyPayment = (int)(gankin + (borrowing - gankin * i) * (rate / 12));
                    int risoku = monthlyPayment - gankin;
                    sougaku -= gankin;
                    if (i == year * 12 - 1)
                    {
                        dataGridView1.Rows.Add($"{date}", $"{i + 1:N0}", $"{borrowing - (gankin * year * 12) + gankin + risoku:N0}", $"{borrowing - (gankin * year * 12) + gankin:N0}", $"{risoku:N0}", 0);
                        sumLorn += borrowing - (gankin * year * 12) + gankin + risoku;
                    }
                    else
                    {
                        dataGridView1.Rows.Add($"{date}", $"{i + 1:N0}", $"{monthlyPayment:N0}", $"{gankin:N0}", $"{risoku:N0}", $"{sougaku:N0}");
                        sumLorn += monthlyPayment;
                    }

                    sumRisoku += risoku;

                }
            }
            borrowing = saveBorrowing;
            //<上段のdataGridView2の表示式>
            dataGridView2.Rows.Add($"{rb_Name}",$"{textBox3.Text} %",$"{year * 12}",$"{dataGridView1.Rows[year * 12 -1].Cells[0].Value}",$"{sumLorn:N0}", $"{resultBorrowing:N0}",$"{sumRisoku:N0}");
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            try
            {
                var txtBox = sender as TextBox;
                if (!string.IsNullOrWhiteSpace(txtBox.Text))
                {
                    txtBox.Text = int.Parse(txtBox.Text.Replace(",", "")).ToString("#,##0");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //0～9と、バックスペース以外の時は、イベントをキャンセルする
            if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //0～9と、バックスペース以外の時は、イベントをキャンセルする
            if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
    }
}
//https://keisan.casio.jp/exec/system/1256183644
//https://fp-eye.com/housing-loan/monthly-payments/
//https://www.jibunbank.co.jp/products/homucts/homunbank.co.jp/products/homcode=HLCS0000071A&argument=3omucts/homunbank.co.jp/products/homcode=HLCS0000071A&argument=3FYeHBQU&dmai=a5e2e2bcca2fe5&gclid=Cj0KCQiAhMOMBhDhARIsAPVml-FJx045nO7DSlas4P5jvanLvi4ebqgZFdOcDd_oNLDRsS4ziQjvQ00aAnq4EALw_wcB#/new/debt