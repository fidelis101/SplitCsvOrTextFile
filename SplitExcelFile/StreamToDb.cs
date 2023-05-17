using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace Splitfiles
{
    public partial class Splitter
    {
        public FileStream? fs;

        List<string> Packets = new List<string>();


        int counter = 0;
        string? line;

        public bool FilterOutFees(string SourceFile)
        {
            var file = new StreamReader(SourceFile);
            FileInfo fs = new FileInfo(SourceFile);

            StringBuilder sb = new StringBuilder();

            string baseFileName = Path.GetFileNameWithoutExtension(SourceFile);
            string Extension = Path.GetExtension(SourceFile);

            FileStream outputFile = new FileStream(Path.GetDirectoryName(SourceFile) + "\\" + "Filtered_" + baseFileName + "001" +
            Extension, FileMode.Create, FileAccess.Write);
            StreamWriter sr = new StreamWriter(outputFile);
            try
            {
                using (SqlConnection conn = new SqlConnection("Server=localhost;database=RPATest;User id=sa;Password=P@ssword1;TrustServerCertificate=True;MultipleActiveResultSets=true;Integrated Security=False"))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        int count = 0;
                        conn.Open();
                        cmd.Connection = conn;
                        var positionOfColumns = new Dictionary<string, int>();

                        while ((line = file.ReadLine()) != null)
                        {

                            cmd.CommandType = CommandType.Text;

                            var values = line.Split(',');
                            if (count == 0)
                            {
                                for(int i =0; i < values.Length; i++)
                                {
                                    positionOfColumns.Add(values[i].ToUpper(), i);
                                }
                            }
                            else
                            {
                                cmd.CommandText = @"INSERT INTO autopay_recon_settlement_agency_records ([DateTime],[Bank_Card_Brand],[Currency_Name],[Local_Date_Time]
                                                    ,[Terminal_ID],[Merchant_ID],[Merchant_Name_Location],[STAN],[PAN],[Message_Type]
                                                    ,[From_Account_ID],[From_Account_Type],[To_Account_ID],[To_Account_Type],[Card_Account_Nr]
                                                      ,[Tran_Type_Description],[Beneficiary_Account]
                                                      ,[Response_Code_Description],[Tran_Amount_Req],[Tran_Amount_Rsp]
                                                      ,[Surcharge],[Amount_Impact],[Settlement_Impact] ,[Settlement_Impact_Desc],[Auth_ID]
                                                      ,[Tran_ID],[Retrieval_Reference_Nr],[Totals_Group],[Region],[Transaction_Status],[Transaction_Type_Impact]
                                                      ,[Message_Type_Desc],[Trxn_Category]) 
                                        VALUES(@DateTime,@Bank_Card_Brand,@Currency_Name,@Local_Date_Time,@Terminal_ID
                                                      ,@Merchant_ID,@Merchant_Name_Location,@STAN,@PAN,@Message_Type
                                                      ,@From_Account_ID,@From_Account_Type ,@To_Account_ID,@To_Account_Type
                                                      ,@Card_Account_Nr,@Tran_Type_Description,@Beneficiary_Account,@Response_Code_Description,@Tran_Amount_Req
                                                      ,@Tran_Amount_Rsp,@Surcharge,@Amount_Impact,@Settlement_Impact,@Settlement_Impact_Desc
                                                      ,@Auth_ID,@Tran_ID,@Retrieval_Reference_Nr
                                                      ,@Totals_Group,@Region,@Transaction_Status,@Transaction_Type_Impact,@Message_Type_Desc
                                                      ,@Trxn_Category)";

                                        cmd.Parameters.AddWithValue("@DateTime", values[positionOfColumns["DATETIME"]]);
                                        cmd.Parameters.AddWithValue("@Bank_Card_Brand", values[positionOfColumns["BANK_CARD_BRAND"]]);
                                        cmd.Parameters.AddWithValue("@Currency_Name", values[positionOfColumns["CURRENCY_NAME"]]);
                                        cmd.Parameters.AddWithValue("@Local_Date_Time", values[positionOfColumns["LOCAL_DATE_TIME"]]);
                                        cmd.Parameters.AddWithValue("@Terminal_ID", values[positionOfColumns["TERMINAL_ID"]]);
                                       cmd.Parameters.AddWithValue("@Merchant_ID", values[positionOfColumns["MERCHANT_ID"]]);
                                        cmd.Parameters.AddWithValue("@Merchant_Name_Location", values[positionOfColumns["MERCHANT_NAME_LOCATION"]]);
                                        cmd.Parameters.AddWithValue("@STAN", values[positionOfColumns["STAN"]]);
                                        cmd.Parameters.AddWithValue("@PAN", values[positionOfColumns["PAN"]]);
                                        cmd.Parameters.AddWithValue("@Message_Type", values[positionOfColumns["MESSAGE_TYPE"]]);
                                        cmd.Parameters.AddWithValue("@Card_Account_Nr", values[positionOfColumns["CARD_ACCOUNT_NR"]]);
                                cmd.Parameters.AddWithValue("@From_Account_ID", values[positionOfColumns["FROM_ACCOUNT_ID"]]);
                                cmd.Parameters.AddWithValue("@From_Account_Type", values[positionOfColumns["FROM_ACCOUNT_TYPE"]]);
                                cmd.Parameters.AddWithValue("@To_Account_ID", values[positionOfColumns["TO_ACCOUNT_ID"]]);
                                cmd.Parameters.AddWithValue("@To_Account_Type", values[positionOfColumns["TO_ACCOUNT_TYPE"]]);
                                cmd.Parameters.AddWithValue("@Tran_Amount_Rsp", values[positionOfColumns["TRAN_AMOUNT_RSP"]]);
                                cmd.Parameters.AddWithValue("@Surcharge", values[positionOfColumns["SURCHARGE"]]);
                                cmd.Parameters.AddWithValue("@Amount_Impact", values[positionOfColumns["AMOUNT_IMPACT"]]);

                                cmd.Parameters.AddWithValue("@Settlement_Impact", values[positionOfColumns["SETTLEMENT_IMPACT"]]);
                                cmd.Parameters.AddWithValue("@Settlement_Impact_Desc", values[positionOfColumns["SETTLEMENT_IMPACT_DESC"]]);
                                cmd.Parameters.AddWithValue("@Auth_ID", values[positionOfColumns["AUTH_ID"]]);
                                cmd.Parameters.AddWithValue("@Tran_ID", values[positionOfColumns["TRAN_ID"]]);
                                cmd.Parameters.AddWithValue("@Retrieval_Reference_Nr", values[positionOfColumns["RETRIEVAL_REFERENCE_NR"]]);
                                cmd.Parameters.AddWithValue("@Totals_Group", values[positionOfColumns["TOTALS_GROUP"]]);

                                cmd.Parameters.AddWithValue("@Region", values[positionOfColumns["REGION"]]);
                                cmd.Parameters.AddWithValue("@Transaction_Status", values[positionOfColumns["TRANSACTION_STATUS"]]);
                                cmd.Parameters.AddWithValue("@Transaction_Type_Impact", values[positionOfColumns["TRANSACTION_TYPE_IMPACT"]]);
                                cmd.Parameters.AddWithValue("@Tran_Type_Description", values[positionOfColumns["TRAN_TYPE_DESCRIPTION"]]);
                                cmd.Parameters.AddWithValue("@Message_Type_Desc", values[positionOfColumns["MESSAGE_TYPE_DESC"]]);
                                cmd.Parameters.AddWithValue("@Trxn_Category", values[positionOfColumns["TRXN_CATEGORY"]]);
                                cmd.Parameters.AddWithValue("@Beneficiary_Account", values[positionOfColumns["BENEFICIARY_ACCOUNT"]]);
                                cmd.Parameters.AddWithValue("@Response_Code_Description", values[positionOfColumns["RESPONSE_CODE_DESCRIPTION"]]);
                                cmd.Parameters.AddWithValue("@Tran_Amount_Req", values[positionOfColumns["TRAN_AMOUNT_REQ"]]);

                                try
                                {
                                    cmd.ExecuteNonQuery();
                                    cmd.Parameters.Clear();
                                }
                                catch (SqlException e)
                                {
                                    string err = e.Message.ToString();
                                }
                            }
                            count++;
                        }
                    }
                }

                outputFile.Close();

            }
            catch (Exception Ex)
            {
                throw new ArgumentException(Ex.Message);
            }
            return true;
        }

    }
}