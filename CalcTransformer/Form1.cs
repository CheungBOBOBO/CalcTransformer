// @ Title  ：   CalcTransformer
// @ Author ：   by Maples7
// @ Addr   :   SDU.EE
// @ Func   :   Calculate the params of a 3-windings transformer
// @ Date   :   2015-03-19

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalcTransformer
{
    public partial class Form1 : Form
    {
        // 初始化 GUI 窗口
        public Form1()
        {
            InitializeComponent();
        }

        double i1, i2, i3;
        double u1, u2, u3;
        double us1, us2, us3;
        double p12, p13, p23;
        double i_noLoad, p_noLoad;

        // 当用户点击“计算”后，判断用户数据是否给全、是否合法
        int isDataGood()
        {
            // 判断输入数据非空
            if ((I1.Text == "") || (I2.Text == "") || (I3.Text == "") ||
                (U1.Text == "") || (U2.Text == "") || (U3.Text == "") ||
                (Us1.Text == "") || (Us2.Text == "") || (Us3.Text == "") ||
                (P12.Text == "") || (P13.Text == "") || (P23.Text == "") ||
                (I_noLoad.Text == "") || (P_noLoad.Text == ""))
                return 2; // 返回错误代数值

            // 判断输入数据是否合法
            try
            {
                i1 = double.Parse(I1.Text);
                i2 = double.Parse(I2.Text);
                i3 = double.Parse(I3.Text);

                u1 = double.Parse(U1.Text);
                u2 = double.Parse(U2.Text);
                u3 = double.Parse(U3.Text);

                us1 = double.Parse(Us1.Text);
                us2 = double.Parse(Us2.Text);
                us3 = double.Parse(Us3.Text);

                p12 = double.Parse(P12.Text);
                p13 = double.Parse(P13.Text);
                p23 = double.Parse(P23.Text);

                i_noLoad = double.Parse(I_noLoad.Text);
                p_noLoad = double.Parse(P_noLoad.Text);
            }
            catch
            {
                return 1;
            }

            // 输入数据正常
            return 0;
        }

        private void CalcBotton_Click(object sender, EventArgs e)
        {
            if (isDataGood() == 0)
            {
                // 计算结果
                double pk12 = p12 * (i1 / i2) * (i1 / i2);
                double pk13 = p13 * (i1 / i3) * (i1 / i3);
                double pk23 = p23 * (i2 / i3) * (i2 / i3);

                double pk1 = 0.5 * (pk12 + pk13 - pk23);
                double pk2 = 0.5 * (pk12 + pk23 - pk13);
                double pk3 = 0.5 * (pk13 + pk23 - pk12);

                double rt1 = (pk1 * u1 * u1) / (1000 * i1 * i1);
                double rt2 = (pk2 * u1 * u1) / (1000 * i1 * i1);
                double rt3 = (pk3 * u1 * u1) / (1000 * i1 * i1);

                double uk1 = 0.5 * (us1 + us2 - us3);
                double uk2 = 0.5 * (us1 + us3 - us2);
                double uk3 = 0.5 * (us2 + us3 - us1);

                double xt1 = (uk1 * u1 * u1) / (100 * i1);
                double xt2 = (uk2 * u1 * u1) / (100 * i1);
                double xt3 = (uk3 * u1 * u1) / (100 * i1);

                double gt = p_noLoad / (1000 * u1 * u1);
                double bt = (i_noLoad * i1) / (100 * u1 * u1);

                // 显示结果
                Rt1.Text = rt1.ToString("f6");
                Rt2.Text = rt2.ToString("f6");
                Rt3.Text = rt3.ToString("f6");

                Xt1.Text = xt1.ToString("f6");
                Xt2.Text = xt2.ToString("f6");
                Xt3.Text = xt3.ToString("f6");

                Gt.Text = gt.ToString("e6");
                Bt.Text = bt.ToString("e6");
            }
            else if (isDataGood() == 2)
            {
                MessageBox.Show("您输入的数据不全，请检查正确后再次输入！");
            }
            else
            {
                MessageBox.Show("您的输入存在非法数据，请检查正确后再次输入！");
            }
        }

        private void ResetBotton_Click(object sender, EventArgs e)
        {
            I1.Text = "";  I2.Text = "";  I3.Text = "";
            U1.Text = "";  U2.Text = "";  U3.Text = "";
            Us1.Text = "";  Us2.Text = "";  Us3.Text = "";
            P12.Text = "";  P13.Text = "";  P23.Text = "";
            I_noLoad.Text = "";  P_noLoad.Text = "";
        }
    }
}
