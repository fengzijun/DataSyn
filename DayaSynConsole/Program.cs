using SmsData;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayaSynConsole
{
    class Program
    {
        static string connection = System.Configuration.ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        static string connection2 = System.Configuration.ConfigurationManager.ConnectionStrings["connection2"].ConnectionString;
        static void Main(string[] args)
        {
            ClearData();
            InsertToBN_INF_APPLY();
            InsertToBN_INF_APPLYResult();
            InsertToBN_INF_APPLYProcess();
            Console.ReadKey();
        }


        static void ClearData()
        {
            string sql = "delete [dbo].[BN_INF_APPLY]";
            SqlHelper.ExecuteNonQuery(connection2, System.Data.CommandType.Text, sql, null);
            sql = "delete [dbo].[BN_INF_APPLY_PROCESS]";
            SqlHelper.ExecuteNonQuery(connection2, System.Data.CommandType.Text, sql, null);
            sql = "delete [dbo].[BN_INF_APPLY_RESULT]";
            SqlHelper.ExecuteNonQuery(connection2, System.Data.CommandType.Text, sql, null);
        }

        static void InsertToBN_INF_APPLY()
        {
            string sql = @"select businessid,txr_UserName,yyzzzch,lxr,checkbz1 , a.xgrqsj from gczj_zbkzjbaglb a 
inner join UEPP_Qyjbxx b on a.txr_UserID = b.userid";
            var datareader = SqlHelper.ExecuteReader(connection, System.Data.CommandType.Text, sql);
            Random random = new Random();
            while(datareader.Read())
            {
                string businessid = datareader["businessid"] == DBNull.Value ? string.Empty : datareader["businessid"].ToString();
                string txr_UserName = datareader["txr_UserName"] == DBNull.Value ? string.Empty : datareader["txr_UserName"].ToString();
                string yyzzzch = datareader["yyzzzch"] == DBNull.Value ? string.Empty : datareader["yyzzzch"].ToString();
                string lxr = datareader["lxr"] == DBNull.Value ? string.Empty : datareader["lxr"].ToString();
                string checkbz1 = datareader["checkbz1"] == DBNull.Value ? string.Empty : datareader["checkbz1"].ToString();
                DateTime time = datareader["xgrqsj"] == DBNull.Value ?DateTime.MinValue: DateTime.Parse( datareader["xgrqsj"].ToString());
                AddBN_INF_APPLYItem(businessid, txr_UserName, yyzzzch, lxr, checkbz1, "100020500020171212" + random.Next(1, 9999).ToString().PadLeft(4, '0'), time);
                Console.WriteLine(businessid);
            }

            Console.WriteLine("OK");

        }

        static void InsertToBN_INF_APPLYResult()
        {
            string sql = @"select businessid,shrqsj1 from gczj_zbkzjbaglb";
            var datareader = SqlHelper.ExecuteReader(connection, System.Data.CommandType.Text, sql);
            Random random = new Random();
            while (datareader.Read())
            {
                string businessid = datareader["businessid"] == DBNull.Value ? string.Empty : datareader["businessid"].ToString();
                DateTime? time  = datareader["shrqsj1"] == DBNull.Value ?(DateTime?) null : DateTime.Parse(datareader["shrqsj1"].ToString());

                AddBN_INF_APPLY_RESULTItem(businessid, time);
                Console.WriteLine(businessid);
            }

            Console.WriteLine("OK");

        }

        static void InsertToBN_INF_APPLYProcess()
        {
            string sql = @"select businessid,shrqsj1 from gczj_zbkzjbaglb";
            var datareader = SqlHelper.ExecuteReader(connection, System.Data.CommandType.Text, sql);
            Random random = new Random();
            int index = 1;
            while (datareader.Read())
            {
                string businessid = datareader["businessid"] == DBNull.Value ? string.Empty : datareader["businessid"].ToString();
                DateTime? time = datareader["shrqsj1"] == DBNull.Value ? (DateTime?)null : DateTime.Parse(datareader["shrqsj1"].ToString());

                AddBN_INF_APPLY_PROCESSItem(businessid, time, index);
                index++;
                Console.WriteLine(businessid);
            }

            Console.WriteLine("OK");

        }

        static void AddBN_INF_APPLYItem(string businessid, string username, string yyzzzch, string lxr, string checkbz1, string seqno, DateTime time)
        {
            string sql = @"INSERT INTO [dbo].[BN_INF_APPLY]
           ([BelongXiaQuCode]
           ,[OperateUserName]
           ,[OperateDate]
           ,[YearFlag]
           ,[RowGuid]
           ,[SYNC_ERROR_DESC]
           ,[SYNC_SIGN]
           ,[SYNC_DATE]
           ,[DATA_SOURCES]
           ,[BJ_STATU]
           ,[APPLY_DATE]
           ,[WAPPLY_DATE]
           ,[PROMISE_TYPE]
           ,[PROMISE]
           ,[ANTICIPATE_DAY_TYPE]
           ,[ANTICIPATE]
           ,[SJ_FILE_REMARK]
           ,[YE_MS]
           ,[LINKMAN_EMAIL]
           ,[LINKMAN_ZIPCODE]
           ,[LINKMAN_ADDRESS]
           ,[LINKMAN_PHONE]
           ,[LINKMAN_MOBILE]
           ,[LINKMAN_PAPER_CODE]
           ,[LINKMAN_PAPER_TYPE]
           ,[LINKMAN_NAME]
           ,[OPER_MAN_NAME]
           ,[APPLICANT_CODE]
           ,[APPLICANT_EMALL]
           ,[APPLICANT_ZIPCODE]
           ,[APPLICANT_ADDRESS]
           ,[APPLICANT_PHONE]
           ,[APPLICANT_MOBILE]
           ,[APPLICANT_PAPER_CODE]
           ,[APPLICANT_PAPER_TYPE]
           ,[APPLICANT_NAME]
           ,[APPLICANT_TYPE]
           ,[SQ_WAY]
           ,[IF_URGENT]
           ,[CONTENT]
           ,[TRANSACT_AFFAIR_NAME]
           ,[DEPARTMENT]
           ,[DEPT_YW_NAME]
           ,[DEPT_YW_REG_NO]
           ,[DEPT_QL_NAME]
           ,[DEPT_QL_REG_NO]
           ,[INTERNAL_NO]
           ,[ORG_NAME]
           ,[ORG_ID]
           ,[AREA_NO]
           ,[NO]
           ,[AREA_NAME])
     VALUES
           (@BelongXiaQuCode
           ,@OperateUserName
           ,@OperateDate
           ,@YearFlag
           ,@RowGuid
           ,@SYNC_ERROR_DESC
           ,@SYNC_SIGN
           ,@SYNC_DATE
           ,@DATA_SOURCES
           ,@BJ_STATU
           ,@APPLY_DATE
           ,@WAPPLY_DATE
           ,@PROMISE_TYPE
           ,@PROMISE
           ,@ANTICIPATE_DAY_TYPE
           ,@ANTICIPATE
           ,@SJ_FILE_REMARK
           ,@YE_MS
           ,@LINKMAN_EMAIL
           ,@LINKMAN_ZIPCODE
           ,@LINKMAN_ADDRESS
           ,@LINKMAN_PHONE
           ,@LINKMAN_MOBILE
           ,@LINKMAN_PAPER_CODE
           ,@LINKMAN_PAPER_TYPE
           ,@LINKMAN_NAME
           ,@OPER_MAN_NAME
           ,@APPLICANT_CODE
           ,@APPLICANT_EMALL
           ,@APPLICANT_ZIPCODE
           ,@APPLICANT_ADDRESS
           ,@APPLICANT_PHONE
           ,@APPLICANT_MOBILE
           ,@APPLICANT_PAPER_CODE
           ,@APPLICANT_PAPER_TYPE
           ,@APPLICANT_NAME
           ,@APPLICANT_TYPE
           ,@SQ_WAY
           ,@IF_URGENT
           ,@CONTENT
           ,@TRANSACT_AFFAIR_NAME
           ,@DEPARTMENT
           ,@DEPT_YW_NAME
           ,@DEPT_YW_REG_NO
           ,@DEPT_QL_NAME
           ,@DEPT_QL_REG_NO
           ,@INTERNAL_NO
           ,@ORG_NAME
           ,@ORG_ID
           ,@AREA_NO
           ,@businessid
           ,@AREA_NAME)";
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("@BelongXiaQuCode",null),
                new SqlParameter("@OperateUserName",null),
                new SqlParameter("@OperateDate",null),
                new SqlParameter("@YearFlag",null),
                new SqlParameter("@RowGuid",null),
                new SqlParameter("@SYNC_ERROR_DESC",null),
                new SqlParameter("@SYNC_SIGN",null),
                new SqlParameter("@SYNC_DATE",DateTime.Now),
                new SqlParameter("@DATA_SOURCES","2"),
                new SqlParameter("@BJ_STATU",checkbz1),
                new SqlParameter("@APPLY_DATE",time),
                new SqlParameter("@WAPPLY_DATE",time),
                new SqlParameter("@PROMISE_TYPE","1"),
                new SqlParameter("@PROMISE","4"),
                new SqlParameter("@ANTICIPATE_DAY_TYPE","1"),
                new SqlParameter("@ANTICIPATE","7"),
                new SqlParameter("@SJ_FILE_REMARK",null),
                new SqlParameter("@YE_MS",null),
                new SqlParameter("@LINKMAN_EMAIL",null),
                new SqlParameter("@LINKMAN_ZIPCODE",null),
                new SqlParameter("@LINKMAN_ADDRESS",null),
                new SqlParameter("@LINKMAN_PHONE",null),
                new SqlParameter("@LINKMAN_MOBILE",null),
                new SqlParameter("@LINKMAN_PAPER_CODE",null),
                new SqlParameter("@LINKMAN_PAPER_TYPE",null),
                new SqlParameter("@LINKMAN_NAME",lxr),
                new SqlParameter("@OPER_MAN_NAME",null),
                  new SqlParameter("@APPLICANT_CODE",null),
                    new SqlParameter("@APPLICANT_EMALL",null),
                      new SqlParameter("@APPLICANT_ZIPCODE",null),
                        new SqlParameter("@APPLICANT_ADDRESS",null),
                          new SqlParameter("@APPLICANT_PHONE",null),
                            new SqlParameter("@APPLICANT_MOBILE",null),
                              new SqlParameter("@APPLICANT_PAPER_CODE",yyzzzch),
                                new SqlParameter("@APPLICANT_PAPER_TYPE","8"),
                                  new SqlParameter("@APPLICANT_NAME",username),
                                  new SqlParameter("@APPLICANT_TYPE","2"),
                                    new SqlParameter("@SQ_WAY","1"),
                                     new SqlParameter("@IF_URGENT",null),
                                      new SqlParameter("@CONTENT",null),
                                        new SqlParameter("@TRANSACT_AFFAIR_NAME",null),
                                          new SqlParameter("@DEPARTMENT","住建局定额站"),
                                           new SqlParameter("@DEPT_YW_NAME","国有投资建设工程项目招标控制价备查"),
                                            new SqlParameter("@DEPT_YW_REG_NO","32050900000001420376501000205000"),
                                             new SqlParameter("@DEPT_QL_NAME","国有投资建设工程项目招标控制价备查"),
                                              new SqlParameter("@DEPT_QL_REG_NO","32050900000001420376501000205000"),
                                               new SqlParameter("@INTERNAL_NO",seqno),
                                                    new SqlParameter("@ORG_NAME","吴江区住建局"),
                                                         new SqlParameter("@ORG_ID","14203765"),
                                                              new SqlParameter("@AREA_NO","320509"),
                                                                     new SqlParameter("@businessid",businessid),
                                                                            new SqlParameter("@AREA_NAME","吴江区")
            };
            SqlHelper.ExecuteNonQuery(connection2, System.Data.CommandType.Text, sql, parms);
        }

        static void AddBN_INF_APPLY_PROCESSItem(string businessid, DateTime? time, int EVENT_NUMBER)
        {
            string sql = @"INSERT INTO [dbo].[BN_INF_APPLY_PROCESS]
           ([BelongXiaQuCode]
           ,[OperateUserName]
           ,[OperateDate]
           ,[YearFlag]
           ,[RowGuid]
           ,[SYNC_ERROR_DESC]
           ,[SYNC_SIGN]
           ,[SYNC_DATE]
           ,[DATA_SOURCES]
           ,[REMARK]
           ,[END_USER_NAME]
           ,[PROCESS_REPORT]
           ,[PROCESS_REPORT_NAME]
           ,[END_NOTE]
           ,[END_TIME]
           ,[START_PHONE]
           ,[START_TEL]
           ,[START_USER_NAME]
           ,[START_NOTE]
           ,[EVENT_TIME_TYPE]
           ,[EVENT_TIME]
           ,[START_TIME]
           ,[DEPARTMENT]
           ,[EVENT_NAME]
           ,[EVENT_CODE]
           ,[EVENT_NUMBER]
           ,[INTERNAL_NO]
           ,[NO])
     VALUES
           (@BelongXiaQuCode
           ,@OperateUserName
           ,@OperateDate
           ,@YearFlag
           ,@RowGuid
           ,@SYNC_ERROR_DESC
           ,@SYNC_SIGN
           ,@SYNC_DATE
           ,@DATA_SOURCES
           ,@REMARK
           ,@END_USER_NAME
           ,@PROCESS_REPORT
           ,@PROCESS_REPORT_NAME
           ,@END_NOTE
           ,@END_TIME
           ,@START_PHONE
           ,@START_TEL
           ,@START_USER_NAME
           ,@START_NOTE
           ,@EVENT_TIME_TYPE
           ,@EVENT_TIME
           ,@START_TIME
           ,@DEPARTMENT
           ,@EVENT_NAME
           ,@EVENT_CODE
           ,@EVENT_NUMBER
           ,@INTERNAL_NO
           ,@businessid)";
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("@BelongXiaQuCode",null),
                new SqlParameter("@OperateUserName",null),
                new SqlParameter("@OperateDate",null),
                new SqlParameter("@YearFlag",null),
                new SqlParameter("@RowGuid",null),
                new SqlParameter("@SYNC_ERROR_DESC",null),
                new SqlParameter("@SYNC_SIGN",null),
                new SqlParameter("@SYNC_DATE",DateTime.Now),
                new SqlParameter("@DATA_SOURCES","2"),
                new SqlParameter("@REMARK",null),
                new SqlParameter("@END_USER_NAME",null),
                new SqlParameter("@PROCESS_REPORT",null),
                new SqlParameter("@PROCESS_REPORT_NAME",null),
                new SqlParameter("@END_NOTE","4"),
                new SqlParameter("@END_TIME",time),
                new SqlParameter("@START_PHONE",null),
                new SqlParameter("@START_TEL",null),
                new SqlParameter("@START_USER_NAME",null),
                new SqlParameter("@START_NOTE",null),
                new SqlParameter("@EVENT_TIME_TYPE",null),
                new SqlParameter("@EVENT_TIME",0),
                new SqlParameter("@START_TIME",null),
                new SqlParameter("@DEPARTMENT",null),
                new SqlParameter("@EVENT_NAME",null),
                new SqlParameter("@EVENT_CODE","B9"),
                new SqlParameter("@EVENT_NUMBER",EVENT_NUMBER),
                new SqlParameter("@INTERNAL_NO",null),

                new SqlParameter("@businessid",businessid)

            };
            SqlHelper.ExecuteNonQuery(connection2, System.Data.CommandType.Text, sql, parms);
        }

        static void AddBN_INF_APPLY_RESULTItem(string businessid, DateTime? shrqsj1)
        {
            string sql = @"INSERT INTO [dbo].[BN_INF_APPLY_RESULT]
           ([BelongXiaQuCode]
           ,[OperateUserName]
           ,[OperateDate]
           ,[YearFlag]
           ,[RowGuid]
           ,[SYNC_ERROR_DESC]
           ,[SYNC_SIGN]
           ,[SYNC_DATE]
           ,[DATA_SOURCES]
           ,[CREATE_DATE]
           ,[RESULT_FILE]
           ,[RESULT_FILE_NAME]
           ,[NOTE]
           ,[RESULT_NO]
           ,[STATUS]
           ,[INTERNAL_NO]
           ,[NO])
     VALUES
           (@BelongXiaQuCode
           ,@OperateUserName
           ,@OperateDate
           ,@YearFlag
           ,@RowGuid
           ,@SYNC_ERROR_DESC
           ,@SYNC_SIGN
           ,@SYNC_DATE
           ,@DATA_SOURCES
           ,@CREATE_DATE
           ,@RESULT_FILE
           ,@RESULT_FILE_NAME
           ,@NOTE
           ,@RESULT_NO
           ,@STATUS
           ,@INTERNAL_NO
           ,@businessid)";
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("@BelongXiaQuCode",null),
                new SqlParameter("@OperateUserName",null),
                new SqlParameter("@OperateDate",null),
                new SqlParameter("@YearFlag",null),
                new SqlParameter("@RowGuid",null),
                new SqlParameter("@SYNC_ERROR_DESC",null),
                new SqlParameter("@SYNC_SIGN",null),
                new SqlParameter("@SYNC_DATE",DateTime.Now),
                new SqlParameter("@DATA_SOURCES","2"),
              new SqlParameter("@CREATE_DATE",shrqsj1),
                  new SqlParameter("@RESULT_FILE",null),
                      new SqlParameter("@RESULT_FILE_NAME",null),
                          new SqlParameter("@NOTE",null),
                              new SqlParameter("@RESULT_NO",null),
                                  new SqlParameter("@STATUS","A"),
                new SqlParameter("@INTERNAL_NO",null),

                new SqlParameter("@businessid",businessid)

            };
            SqlHelper.ExecuteNonQuery(connection2, System.Data.CommandType.Text, sql, parms);
        }
    }
}
