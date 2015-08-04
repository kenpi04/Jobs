using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Job.Core
{
   public static class Extensions
    {
       public static DataTable ListToDataTable(this List<string> list)
       {
           DataTable dt = new DataTable();
           dt.Columns.Add(new DataColumn("Item"));
           foreach(var s in list)
           {
               var r = dt.NewRow();
               r[0] = s;
               dt.Rows.Add(s);
           }
           return dt;
       }
       /// <summary>
       /// Convert object to sql Paramenter
       /// </summary>
       /// <param name="o">object</param>
       /// <param name="nameParameter">Name of parameter</param>
       /// <param name="type">Type</param>
       /// <returns></returns>
       public static SqlParameter ToSqlParameter(this object o,string nameParameter,DbType? type=null)
       {
          var p= new SqlParameter
           {
               ParameterName=nameParameter,             
           };       
          if (o is IList && o.GetType().IsGenericType)
          {
              
              p.SqlDbType = SqlDbType.NVarChar;
                if (o == null)
                    o = new List<object>();
                dynamic a = o;
                string value = "";
                  for (int i = 0; i < a.Count;i++ )
                  {
                      value += a[i].ToString() + ",";
                  }
                  p.Value = value.TrimEnd(',');
          }
          else
          {
              p.Value = o!=null?o:DBNull.Value;
              
              if (type.HasValue)
                  p.DbType = type.Value;
          }
          return p;
          
       }

       #region Convert List To Data Table
       public static DataTable ToDataTable<T>(this List<T> items)
       {
           DataTable dataTable = new DataTable(typeof(T).Name);
           //Get all the properties
           PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
           foreach (PropertyInfo prop in Props)
           {
               //Setting column names as Property names
               dataTable.Columns.Add(prop.Name);
           }

           foreach (T item in items)
           {
               var values = new object[Props.Length];
               for (int i = 0; i < Props.Length; i++)
               {
                   //inserting property values to datatable rows
                   values[i] = Props[i].GetValue(item, null);
               }

               dataTable.Rows.Add(values);

           }
           //put a breakpoint here and check datatable
           return dataTable;
       }
       public static DataTable ToDataTable<T>(this List<T> items,List<string>columnNames,Func<T,T> columnFuc=null)
       {
           DataTable dataTable = new DataTable(typeof(T).Name);
           //Get all the properties
           PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
           foreach (string name in columnNames)
           {
               //Setting column names as Property names
               dataTable.Columns.Add(name);
           }

           foreach (T item in items)
           {
               var values = new object[Props.Length];
               for (int i = 0; i < Props.Length; i++)
               {
                   if (columnFuc != null)
                       columnFuc(item);
                   //inserting property values to datatable rows
                   values[i] = Props[i].GetValue(item, null);
               }

               dataTable.Rows.Add(values);

           }
           //put a breakpoint here and check datatable
           return dataTable;
       }
       #endregion Convert List To Data Table

       public static void ForEach<T>(this IEnumerable<T>source,Action<T>action)
       {
           foreach (T element in source)
               action(element);
       }
       public static bool isMobileBrowser(this HttpRequestBase Request)
       {
          

           //FIRST TRY BUILT IN ASP.NT CHECK
           if (Request.Browser.IsMobileDevice)
           {
               return true;
           }
           //THEN TRY CHECKING FOR THE HTTP_X_WAP_PROFILE HEADER
           if (Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null)
           {
               return true;
           }
           //THEN TRY CHECKING THAT HTTP_ACCEPT EXISTS AND CONTAINS WAP
           if (Request.ServerVariables["HTTP_ACCEPT"] != null &&
              Request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap"))
           {
               return true;
           }
           //AND FINALLY CHECK THE HTTP_USER_AGENT 
           //HEADER VARIABLE FOR ANY ONE OF THE FOLLOWING
           if (Request.ServerVariables["HTTP_USER_AGENT"] != null)
           {
               //Create a list of all mobile types
               string[] mobiles =
                   new[]
                {
                    "midp", "j2me", "avant", "docomo", 
                    "novarra", "palmos", "palmsource", 
                    "240x320", "opwv", "chtml",
                    "pda", "windows ce", "mmp/", 
                    "blackberry", "mib/", "symbian", 
                    "wireless", "nokia", "hand", "mobi",
                    "phone", "cdm", "up.b", "audio", 
                    "SIE-", "SEC-", "samsung", "HTC", 
                    "mot-", "mitsu", "sagem", "sony"
                    , "alcatel", "lg", "eric", "vx", 
                    "NEC", "philips", "mmm", "xx", 
                    "panasonic", "sharp", "wap", "sch",
                    "rover", "pocket", "benq", "java", 
                    "pt", "pg", "vox", "amoi", 
                    "bird", "compal", "kg", "voda",
                    "sany", "kdd", "dbt", "sendo", 
                    "sgh", "gradi", "jb", "dddi", 
                    "moto", "iphone"
                };

               //Loop through each item in the list created above 
               //and check if the header contains that text
               foreach (string s in mobiles)
               {
                   if (Request.ServerVariables["HTTP_USER_AGENT"].
                                                       ToLower().Contains(s.ToLower()))
                   {
                       return true;
                   }
               }
           }

           return false;
       }

    }
}
