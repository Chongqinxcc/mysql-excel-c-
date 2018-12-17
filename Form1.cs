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

            //第一列添加复选框
            DataGridViewCheckBoxColumn dtchk = new DataGridViewCheckBoxColumn();
            // dtchk.DataPropertyName = "chk";
            dtchk.Name = "chk";
            dtchk.HeaderText = "全选";

            //DataGridView1.ColumnHeadersVisible = false;//需要显示列标题
            dtchk.TrueValue = true;
            dtchk.FalseValue = false;
            dtchk.DataPropertyName = "IsChecked";

            DataGridView1.Columns.Add(dtchk);
            DataGridView1.Columns[0].Width = 45;


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

        //选中单元格的内容时 处理事件
        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)this.DataGridView1.Rows[e.RowIndex].Cells["chk"];
                Boolean flag = Convert.ToBoolean(checkCell.Value);
                if (flag == true)
                {
                    checkCell.Value = false;
                    Console.WriteLine("没选中");
                }
                else
                {
                    checkCell.Value = true;
                    Console.WriteLine("选中");
                }
            }
            */
            try
            {
                int index = DataGridView1.CurrentRow.Index;
                this.DataGridView1.Rows[e.RowIndex].Selected = true;
                if (Convert.ToBoolean(DataGridView1.Rows[index].Cells[0].Value))
                {
                    DataGridView1.Rows[index].Cells[0].Value = false;
                }
                else
                {
                    DataGridView1.Rows[index].Cells[0].Value = true;
                    ////其他的都是false
                    //for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    //{
                    //    if (i != index)
                    //    {
                    //        dataGridView1.Rows[i].Cells[0].Value = false;
                    //    }
                    //}
                }
            }
            catch
            {
                Console.WriteLine("错误");
            }

        }
        
        //单击单元格的时候发生的事件
        /// <summary>
        /// 如果单击列表头，全选．但是小箭头在的一行不会被选中，不知道为啥
        /// 暂时保留
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
            if (e.RowIndex == -1)//如果单击列表头，全选．
            {
                int i;
                for (i = 0; i < this.DataGridView1.RowCount; i++)
                {
                    if ((Convert.ToBoolean(DataGridView1.Rows[i].Cells[0].Value) == false))
                    {
                        this.DataGridView1.Rows[i].Cells[0].Value = "true";//如果为true则为选中,false未选中
                        Console.WriteLine("当前行"+i);
                    }
                        
                }
            }
            */
            try
            {
                int index = DataGridView1.CurrentRow.Index;
                this.DataGridView1.Rows[e.RowIndex].Selected = true;
                if (Convert.ToBoolean(DataGridView1.Rows[index].Cells[0].Value))
                {
                    DataGridView1.Rows[index].Cells[0].Value = false;
                }
                else
                {
                    DataGridView1.Rows[index].Cells[0].Value = true;
                    ////其他的都是false
                    //for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    //{
                    //    if (i != index)
                    //    {
                    //        dataGridView1.Rows[i].Cells[0].Value = false;
                    //    }
                    //}
                }
            }
            catch
            {
                Console.WriteLine("错误");
            }


        }

        // 全选

        private void selectAllbutton_Click(object sender, EventArgs e)
        {
            //遍历datagridview中的每一行，判断是否选中，若为选中，则选中
            for (int i = 0; i < DataGridView1.Rows.Count; i++)
            {
                if ((Convert.ToBoolean(DataGridView1.Rows[i].Cells[0].Value) == false))
                {
                    DataGridView1.Rows[i].Cells[0].Value = "True";
                }
                else
                    continue;
            }
        }

        ///取消全选
        private void cancelAllbutton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DataGridView1.Rows.Count; i++)
            {
                if ((Convert.ToBoolean(DataGridView1.Rows[i].Cells[0].Value) == true))
                {
                    DataGridView1.Rows[i].Cells[0].Value = "False";
                }
                else
                    continue;
            }
        }
    }
}
