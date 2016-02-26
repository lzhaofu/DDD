using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Web;
using System.Data;

namespace DDD.Utility
{
    /// <summary>
    /// DropDownListTree�����б����״
    /// </summary>
    public class DropDownListTree
    {
        private System.Web.UI.WebControls.DropDownList selp;
        private DataTable _dt;
        private string _pid;
        private string _id;
        private string _name;
        private string _root;
        private int _rootvalue;

        /// <summary>
        /// ���������ؼ���
        /// </summary>
        /// <param name="selp">Ҫ�󶨵ķ����������е�������</param>
        /// <param name="_dt">�������޼��������Ե����ݼ�</param>
        /// <param name="_pid">�ֶ���������ID</param>
        /// <param name="_id">�ֶ�������ʶid</param>
        /// <param name="_name">�ֶ���������</param>
        /// <param name="_root">����Text</param>
        /// <param name="_rootvalue">����Value</param>
        public DropDownListTree(DropDownList selp, DataTable _dt, string _pid, string _id, string _name, string _root, int _rootvalue)
        {
            this.selp = selp;
            this._dt = _dt;
            this._pid = _pid;
            this._id = _id;
            this._name = _name;
            this._root = _root;
            this._rootvalue = _rootvalue;
        }


        public void bind_select()//�󶨷���
        {
            this.selp.Items.Clear();
            this.selp.Items.Add(new ListItem("��" + this._root, this._rootvalue.ToString()));
            bindtoSelect(this._rootvalue, this._dt, "��");
        }
        protected void bindtoSelect(int pid, DataTable dt, string header)//�ݹ�
        {
            //DataRow[] rows = dt.Select(this._pid + "=" + pid);
            DataRow[] rows = dt.Select(this._pid + "=" + " '" + pid.ToString() + "'");
            header += "��";
            foreach (DataRow row in rows)
            {
                this.selp.Items.Add(new ListItem(header + Convert.ToString(row[this._name]), Convert.ToString(row[this._id])));
                bindtoSelect(Convert.ToInt32(row[this._id]), dt, header);
            }
        }
    }
}
