using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Hoto.DBUtility;
using Hoto.Common;

namespace Hoto.DAL
{
	/// <summary>
	/// 扩展属性
	/// </summary>
	public partial class attribute_value
	{
        public attribute_value()
		{}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.attribute_value> GetList(int article_id)
        {
            List<Model.attribute_value> modelList = new List<Model.attribute_value>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,article_id,attribute_id,title,content from dt_attribute_value ");
            strSql.Append(" where article_id=" + article_id);
            DataTable dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];

            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.attribute_value model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.attribute_value();
                    if (dt.Rows[n]["id"] != null && dt.Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(dt.Rows[n]["id"].ToString());
                    }
                    if (dt.Rows[n]["article_id"] != null && dt.Rows[n]["article_id"].ToString() != "")
                    {
                        model.article_id = int.Parse(dt.Rows[n]["article_id"].ToString());
                    }
                    if (dt.Rows[n]["attribute_id"] != null && dt.Rows[n]["attribute_id"].ToString() != "")
                    {
                        model.attribute_id = int.Parse(dt.Rows[n]["attribute_id"].ToString());
                    }
                    if (dt.Rows[n]["title"] != null && dt.Rows[n]["title"].ToString() != "")
                    {
                        model.title = dt.Rows[n]["title"].ToString();
                    }
                    if (dt.Rows[n]["content"] != null && dt.Rows[n]["content"].ToString() != "")
                    {
                        model.content = dt.Rows[n]["content"].ToString();
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

	}
}

