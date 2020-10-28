using LoanOFFER.Data.BusinessLogic;
using LoanOFFER.Data.BusinessObject;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace LoanOFFER.Data.DAL
{
    public class LoanOfferDb
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        SimbrellaLoanList list = new SimbrellaLoanList();
        RemittaLoanList Realist = new RemittaLoanList();
        SalaryTopUpPlusList salaryList = new SalaryTopUpPlusList();

        //public DataTable GetSimbrellaLoanDb(string RequestTime, string LogDate, string CustomerId)
        //{
        //    DataTable data = new DataTable();
        //    string strConString = @"Data Source=172.27.15.103;Initial Catalog=MicroLendingDBNew;Persist Security Info=True;User ID=dev;Password=developers@456;";
        //    using (SqlConnection con = new SqlConnection(strConString))
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("Select Id, Request, customerid, RequestTime, ResponseTime, LogDate, ResponseCode from vx_FastCashEligibiltyInfo where '" + RequestTime + "' and '" + LogDate + "' and '" + CustomerId + "'", con);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(data);
        //    }
        //    return data;
        //}
        public SimbrellaLoanList GetSimbrellaLoanDb(string RequestTime, string LogDate, string CustId)
        {
            var query = $"Select Id, Request, customerid, RequestTime, Response, ResponseTime, LogDate, ResponseCode from vx_FastCashEligibiltyInfo where ltrim(rtrim(customerid))= '{CustId}' and RequestTime BETWEEN '{RequestTime}' AND '{LogDate}'";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["329LoanOffer"].ToString()))
            {
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    try
                    {
                        con.Open();
                        logger.Info("Connection to Simbrella Loan Offer Report was successful!");
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader != null)
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var model = new SimbrellaViewModel()
                                    {
                                        Id = reader["Id"].ToString(),
                                        Request = reader["Request"].ToString(),
                                        customerId = reader["customerid"].ToString(),
                                        RequestTime = reader["RequestTime"].ToString(),
                                        Response = reader["Response"].ToString(),
                                        ResponseTime = reader["ResponseTime"].ToString(),
                                        LogDate = reader["LogDate"].ToString(),
                                        ResponseCode = reader["ResponseCode"].ToString()
                                    };
                                    list.Add(model);
                                }
                            }
                            logger.Info(" Simbrella Loan Offer Report data fetched from database successfully");
                            return list;
                        }
                       
                        //con.Close();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex);
                    }
                }
            }
            return null;
        }
        public RemittaLoanList GetRemittaLoanDb(string RequestTime, string LogDate, string phoneNo)
        {
            
            var query = $"Select Id, Request, PhoneNumber, RequestTime, Response, LogDate FROM [MicroLendingDBNew].[dbo].[vx_RemitaEligibiltyInfo] where ltrim(rtrim(PhoneNumber))= '"+phoneNo+"' and RequestTime BETWEEN '"+RequestTime+"' AND '"+LogDate+"'";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["329LoanOffer"].ToString()))
            {
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    try
                    {
                        con.Open();
                        logger.Info("Connection to database to fetch Remitta Loan Offer Report!");
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader != null)
                        {
                            logger.Info("Reader is not null..");
                            if (reader.HasRows)
                            {
                                logger.Info("Reader is equal to true..");
                                while (reader.Read())
                                {
                                    var model = new RemittaLoanViewModel()
                                    {
                                        Id = reader["Id"].ToString(),
                                        Request = reader["Request"].ToString(),
                                        PhoneNo = reader["PhoneNumber"].ToString(),
                                        RequestTime = reader["RequestTime"].ToString(),
                                        Response = reader["Response"].ToString(),
                                        LogDate = reader["LogDate"].ToString()
                                    };
                                    Realist.Add(model);
                                   
                                }
                            }
                            logger.Info(" Remitta Loan Offer Report data fetched from database successfully");
                            return Realist;
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex);
                    }
                }
            }
                return null;
        }
        public SalaryTopUpPlusList GetSalaryTopPlus(string phoneNo, string RequestDate, string LogDate)
        {
            //var query = $"select * from [ActivityLog].[dbo].[USSDBanking] where requesttype = 'SALARYPLUSTOPUPAMT' and ltrim(rtrim(SourcePhone))= '{phoneNo}'  AND RequestDate BETWEEN '{RequestDate}' AND '{LogDate}'";

            var query = $"Select RecID, RequestID, SourcePhone, Channel, RequestType, RequestDate, ResponseDate, Duration, ResponseCode, ResponseDescr, Remark from [USSDBanking].[dbo].[ActivityLog] where requesttype = 'SALARYPLUSTOPUPAMT' and ltrim(rtrim(SourcePhone))= '"+phoneNo+"'  AND RequestDate BETWEEN '"+RequestDate+"' AND '"+LogDate+"'";
            using(SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LoanOffer"].ToString()))
            {
                using(SqlCommand com = new SqlCommand(query, con))
                {
                    try
                    {
                        con.Open();
                        logger.Info("Connection to database to fetch Salary Top-Up Plus Loan Offer Report!");
                        SqlDataReader reader = com.ExecuteReader();
                        if (reader != null)
                        {
                            logger.Info("Reader is not null..");
                            if (reader.HasRows)
                            {
                                logger.Info("Reader is equal to true..");
                                while (reader.Read())
                                {
                                    var model = new SalaryTopUpViewModel()
                                    {
                                        RecID = reader["RecID"].ToString(),
                                        RequestID = reader["RequestID"].ToString(),
                                        SourcePhone = reader["SourcePhone"].ToString(),
                                        Channel = reader["Channel"].ToString(),
                                        RequestType = reader["RequestType"].ToString(),
                                        RequestDate = reader["RequestDate"].ToString(),
                                        ResponseDate = reader["ResponseDate"].ToString(),
                                        Duration = reader["Duration"].ToString(),
                                        ResponseCode = reader["ResponseCode"].ToString(),
                                        ResponseDescr = reader["ResponseDescr"].ToString(),
                                        Remark = reader["Remark"].ToString()
                                    };
                                    salaryList.Add(model);
                                }
                            }
                            logger.Info(" Salary Top-Up Plus Loan Offer Report data fetched from database successfully");
                            return salaryList;
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex);
                    }
                }
            }
            return null;
        }
        public SimbrellaLoanList GetMoreInfo(string accountNo)
        {
            SimbrellaLoanList otherList = new SimbrellaLoanList();
            string com = ConfigurationManager.ConnectionStrings["FinacleCodeContext"].ConnectionString;
            var query = $"SELECT CUST_NAME, SOL_ID, FREE_CODE_1 SBU_CODE, FREE_CODE_2 BROKER_CODE FROM TBAADM.CMG C, TBAADM.GAM G, TBAADM.FCFTT F " +
                $"WHERE G.CIF_ID = C.CIF_ID AND G.ACID = F.ACID AND FORACID = '"+accountNo+"'";

            using (OracleConnection con = new OracleConnection(com))
            {
                try
                {
                    OracleCommand command = new OracleCommand(query, con);
                    con.Open();
                    using(OracleDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var model = new SimbrellaViewModel()
                                {
                                    CustomerName = reader["CUST_NAME"].ToString(),
                                    BranchSolID = reader["SOL_ID"].ToString(),
                                    BranchSBU = reader["FREE_CODE_1 SBU_CODE"].ToString(),
                                    BrokerCode = reader["FREE_CODE_2 BROKER_CODE"].ToString()
                                };
                                otherList.Add(model);
                            }
                        }
                        logger.Info(" Simbrella Loan Offer other Report data fetched from database successfully");
                        return otherList;
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            }
           return null;
        }
    }
}
