using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;//pp



namespace ReadMysql
{
    public partial class City : Form
    {

        private MySqlDataAdapter daCountry;//pp
        private DataSet dsCountry;//pp

        MySqlConnection con = new MySqlConnection("server=127.0.0.1;port=3306;user=root;password=CQxcc520*xcc; database=school;");

        public City()
        {
            InitializeComponent();
        }

        private void City_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“worldDataSet.city”中。您可以根据需要移动或删除它。
            this.cityTableAdapter.Fill(this.worldDataSet.city);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("hhh");
            

            //String connetStr = "server=127.0.0.1;port=3306;user=root;password=CQxcc520*xcc; database=world;";
            // server=127.0.0.1/localhost 代表本机，端口号port默认是3306可以不写
            //MySqlConnection conn = new MySqlConnection(connetStr);
            try
            {
                //conn.Open();//打开通道，建立连接，可能出现异常,使用try catch语句
               con.Open();
               Console.WriteLine("已经建立连接");
                /* mDataAdapter = new MySqlDataAdapter("select *from city1", conn);
                 dsall = new DataSet();
                 mDataAdapter.Fill(dsall, "city1");
                 Citydata1.DataSource = dsall;*/

                //2018-12-4pp

                //string sql = "SELECT Code, Name, HeadOfState FROM Country WHERE Region='Eastern Asia'";
                string sql = "SELECT * FROM student";


                //When a MySqlDataAdapter object is created, it is typically given an initial SELECT statement.
                //daCountry = new MySqlDataAdapter(sql, conn);//创建一个接口，这个接口只接入Code, Name, HeadOfState三个字段，并且满足东亚的条件；
                daCountry = new MySqlDataAdapter(sql, con);
                /*
                 * From this SELECT statement the Command Builder can work out the corresponding
                 * INSERT, UPDATE and DELETE statements that would be required to update the database.
                 */
                MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);//实现对从数据接口Adapter对数据进行插入、更新、删除操作；


                dsCountry = new DataSet();
                //Country是数据表
                daCountry.Fill(dsCountry, "Country");//将Country数据表中的几个字段的数据读入数据集中；
                DataGridView1.DataSource = dsCountry;//将数据集作为显示的输入源；
                DataGridView1.DataMember = "Country";//获取更改的元素；使得程序可以更改显示的数据，留着以后更新到数据库中；

            

                //在这里使用代码对数据库进行增删查改

            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //String connetStr1 = "server=127.0.0.1;port=3306;user=root;password=CQxcc520*xcc; database=school;";

                //MySqlConnection con = null;
                //con = new MySqlConnection(connetStr1);
                con.Open();
                if(con.State.ToString()=="Open")
                {
                    Console.WriteLine("成功连接MySQL");
                    MySqlCommand com = new MySqlCommand();
                    com.Connection = con; //获取连接
                    //获取需要执行的INSERT语句
                    com.CommandText = "INSERT INTO `score` (`id`, `stu_id`, `c_name`, `grade`) VALUES ('13', '906', '中文', '88'),('14', '907', '数学', '88')";
                    //插入的记录数
                    int i = com.ExecuteNonQuery();
                    if (i > 0)
                    {
                        Console.WriteLine("插入的记录数为：" + i);
                        i = 0;
                    }
                    
                }
                con.Close();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}
