using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using OpenCvSharp;    //添加相应的引用即可

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Mat source = new Mat(@"../../images_small001002003.png", ImreadModes.Color);
                if (source.Data == IntPtr.Zero)
                {
                    MessageBox.Show("无法读取图像，退出。");
                    return;
                }
                else
                {
                    Cv2.ImShow("Demo", source);
                    Cv2.WaitKey(0);
                    MessageBox.Show("读取了图像。");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("无法读取图像，发生异常。");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //  Mat mat1 = new Mat(@"timg.jpg", ImreadModes.Grayscale);
                // Mat mat2 = new Mat(@"文件名.jpg", ImreadModes.Grayscale);
                Mat mat1 = new Mat(@"../../3.jpg", ImreadModes.AnyColor);
                Mat mat2 = new Mat(@"../../3_tem.jpg", ImreadModes.AnyColor);
                Mat mat3 = new Mat();
                //创建result的模板，就是MatchTemplate里的第三个参数
                mat3.Create(mat1.Cols - mat2.Cols + 1, mat1.Rows - mat2.Cols + 1, MatType.CV_32FC1);
                //进行匹配(1母图,2模版子图,3返回的result，4匹配模式_这里的算法比opencv少，具体可以看opencv的相关资料说明)
                Cv2.MatchTemplate(mat1, mat2, mat3, TemplateMatchModes.SqDiff);

                //对结果进行归一化(这里我测试的时候没有发现有什么用,但在opencv的书里有这个操作，应该有什么神秘加成，这里也加上)
                Cv2.Normalize(mat3, mat3, 1, 0, NormTypes.MinMax, -1);
                double minValue, maxValue;
                OpenCvSharp.Point minLocation, maxLocation;
                /// 通过函数 minMaxLoc 定位最匹配的位置
                /// (这个方法在opencv里有5个参数，这里我写的时候发现在有3个重载，看了下可以直接写成拿到起始坐标就不取最大值和最小值了)
                /// minLocation和maxLocation根据匹配调用的模式取不同的点
                Cv2.MinMaxLoc(mat3, out minLocation, out maxLocation);
                //画出匹配的矩，
                //  Cv2.Rectangle(mat1, maxLocation, new Point (maxLocation.X+mat2.Cols, maxLocation.Y+mat2.Rows), Scalar.Red, 2);
                Cv2.Rectangle(mat1, minLocation, new OpenCvSharp.Point(minLocation.X + mat2.Cols, minLocation.Y + mat2.Rows), Scalar.Red, 2);
                Cv2.ImShow("mat1", mat1);
                Cv2.ImShow("mat2", mat2);

                //Console.WriteLine(minValue);
                //Console.WriteLine(maxValue);
                Console.WriteLine(minLocation);
                Console.WriteLine(maxLocation);
                Cv2.WaitKey();
                //--------------------- 
                //作者：老智自动化 
                //来源：CSDN 
                //原文：https://blog.csdn.net/adcm210/article/details/85110983 
                //版权声明：本文为博主原创文章，转载请附上博文链接！
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("无法读取图像，发生异常。");
            }

        }
    }
}
