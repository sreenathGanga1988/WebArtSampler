using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace WebArtSampler.Reports
{
    public partial class Reportform : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if(!IsPostBack)
            {


                try
                {
                    

                    if (Session["Reportype"].ToString().Trim() == "All")
                    {
                        AllDatat();
                    }
                    else if (Session["Reportype"].ToString().Trim() == "Pending")
                    {
                        AllPending();
                    }
                  
                    else if (Session["Reportype"].ToString().Trim() == "Permaster")
                    {
                        SpecificMasteroverperiod(int.Parse(Session["empid"].ToString()), Session["mastername"].ToString(), DateTime.Parse(Session["fromdate"].ToString()), DateTime.Parse(Session["todate"].ToString()));
                    }
                    else if (Session["Reportype"].ToString().Trim() == "AllWithinPeriod")
                    {
                        AllMasteroverperiod(DateTime.Parse(Session["fromdate"].ToString()), DateTime.Parse(Session["todate"].ToString()));
                    }
                    else if (Session["Reportype"].ToString().Trim() == "BuyerWithinPeriod")
                    {

                        BuyerWiseroverperiod(DateTime.Parse(Session["fromdate"].ToString()), DateTime.Parse(Session["todate"].ToString()), Session["BuyerName"].ToString(), int.Parse(Session["BuyerID"].ToString()));
                    }
                    else if (Session["Reportype"].ToString().Trim() == "Fabric Within Period")
                    {

                        FabricWithinaPeriod(DateTime.Parse(Session["fromdate"].ToString()), DateTime.Parse(Session["todate"].ToString()), Session["fabric"].ToString());
                    }

                }
                catch (Exception exp)
                {

                    throw;
                }
            }
           
        }

        public void SpecificMasteroverperiod(int masterid ,String masterName, DateTime fromdate, DateTime todate)
        {
            String Message = "Cut Ticket assigned to Mr " + masterName + " From " + fromdate.ToString() + " to  " + todate.ToString();

           loadDORReport(getAlldataofmasterOfPeriod(masterid,fromdate,todate), Message); ;
        }
        public void AllMasteroverperiod(DateTime fromdate, DateTime todate)
        {
            String Message = "Cut Ticket  Received in sampling From " + fromdate.ToString() + " to " + todate.ToString();

           loadDORReport(getAlldataofperiod(fromdate,todate), Message); ;
        }

        public void BuyerWiseroverperiod(DateTime fromdate, DateTime todate,String Buyername,int buyerid)
        {
            String Message = "Cut Ticket  Received in sampling From " + fromdate.ToString() + "to " + todate.ToString()+"  For Buyer  "+ Buyername;

            loadDORReport(getAlldataofBuyerOfPeriod(buyerid,fromdate, todate), Message); ;
        }


        public void FabricWithinaPeriod(DateTime fromdate, DateTime todate, String Fabricname)
        {
            String Message = "Cut Ticket  Received in sampling From " + fromdate.ToString() + "to " + todate.ToString() + "  For   " + Fabricname;

            loadDORReport(GetAlldataofFabricWithinperiod(Fabricname, fromdate, todate), Message); ;
        }
        public void AllDatat()
        {
            loadDORReport(getAlldata(), "Total Cut Ticket Report of sampling");
        }


        public void AllPending()
        {
            loadDORReportPending(GetAllPending(), "Pending Cutting Ticket as of "+ DateTime.Now);
        }

        public void loadDORReport(DataTable dt, String reporthead)
        {

            int assignedtotal = 0;

            int receivedtotal = 0;

            int SignedbyMaster = 0;

            int MarkCompleted = 0;









            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    try
                    {
                        receivedtotal = dt.Rows.Count;
                    }
                    catch (Exception)
                    {
                        receivedtotal = 0;


                    }


                    try
                    {
                        DataTable newresult = dt.Select("PaternMasterName is not null").CopyToDataTable();
                        assignedtotal = newresult.Rows.Count;
                    }
                    catch (Exception)
                    {
                        assignedtotal = 0;


                    }

                    try
                    {
                        DataTable newresult = dt.Select("SignedBYMaster = 1").CopyToDataTable();
                        SignedbyMaster = newresult.Rows.Count;
                    }
                    catch (Exception)
                    {
                        SignedbyMaster = 0;


                    }

                    try
                    {
                        DataTable newresult = dt.Select("MarkCompleted = 1").CopyToDataTable();
                        MarkCompleted = newresult.Rows.Count;
                    }
                    catch (Exception)
                    {
                        MarkCompleted = 0;


                    }

                }




            }


            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            //this.reportViewer1.LocalReport.ReportEmbeddedResource = "D:\\report1.rdlc";
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\SampCutreqReport.rdlc";
            ReportParameter Heading = new ReportParameter("Heading", reporthead);
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { Heading });

            ReportParameter Recieved = new ReportParameter("Recieved", receivedtotal.ToString());
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { Recieved });

            ReportParameter Assigned = new ReportParameter("Assigned", assignedtotal.ToString());
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { Assigned });

            ReportParameter Signed = new ReportParameter("Signed", SignedbyMaster.ToString());
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { Signed });

            ReportParameter Completed = new ReportParameter("Completed", MarkCompleted.ToString());
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { Completed });
        }



        public void loadDORReportPending(DataTable dt, String reporthead)
        {

            int assignedtotal = 0;

            int receivedtotal = 0;

            int SignedbyMaster = 0;

            int MarkCompleted = 0;









            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    try
                    {
                        receivedtotal = dt.Rows.Count;
                    }
                    catch (Exception)
                    {
                        receivedtotal = 0;


                    }


                    try
                    {
                        DataTable newresult = dt.Select("PaternMasterName is not null").CopyToDataTable();
                        assignedtotal = newresult.Rows.Count;
                    }
                    catch (Exception)
                    {
                        assignedtotal = 0;


                    }

                    try
                    {
                        DataTable newresult = dt.Select("SignedBYMaster = 1").CopyToDataTable();
                        SignedbyMaster = newresult.Rows.Count;
                    }
                    catch (Exception)
                    {
                        SignedbyMaster = 0;


                    }

                    try
                    {
                        DataTable newresult = dt.Select("MarkCompleted = 1").CopyToDataTable();
                        MarkCompleted = newresult.Rows.Count;
                    }
                    catch (Exception)
                    {
                        MarkCompleted = 0;


                    }

                }




            }


            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            if (Session["subtype"].ToString().Trim() == "Sample")
            {
                this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\SampCutreqReportPendingSampleTypeWise.rdlc";
            }
            else
            {
                this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\SampCutreqReportPending.rdlc";
            }
            ReportParameter Heading = new ReportParameter("Heading", reporthead);
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { Heading });

            ReportParameter Recieved = new ReportParameter("Recieved", receivedtotal.ToString());
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { Recieved });

            ReportParameter Assigned = new ReportParameter("Assigned", assignedtotal.ToString());
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { Assigned });

            ReportParameter Signed = new ReportParameter("Signed", SignedbyMaster.ToString());
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { Signed });

            ReportParameter Completed = new ReportParameter("Completed", MarkCompleted.ToString());
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { Completed });
        }
        public DataTable getAlldata()
        {

            string Qry = @"SELECT        SampCutReqMaster.ReqNum, BuyerMaster.BuyerName, PatterRefMaster.PatterRefNum, SampCutReqMaster.Fabric, SampleType.SampleType, SampCutReqMaster.SampleRequiredDate, 
                         SamCutAssignmentMaster.PatternReqDate, SampCutReqMaster.SizeDetail, SampCutReqMaster.Qty, SamCutAssignmentMaster.ReceivedDate, SamCutAssignmentMaster.ReceivedBy, 
                         SamCutAssignmentMaster.AssignedDate, SamCutAssignmentMaster.SignedDate, SamCutAssignmentMaster.CompletedDate, SamCutAssignmentMaster.Remark, SamCutAssignmentMaster.PatternCompletedDate, 
                         PatternMaster.PaternMasterName, SamCutAssignmentMaster.SignedBYMaster, SampCutReqMaster.MarkCompleted, PatternMaster.PatternMasterID,SampCutReqMaster.AddedBy, 
                         SamCutAssignmentMaster.CompletedQty, SamCutAssignmentMaster.PendingReason,
						 ( isnull( SampCutReqMaster.Size1CutQty,0)+ isnull(SampCutReqMaster.Size2CutQty,0)+isnull( SampCutReqMaster.Size3CutQty,0)+ isnull(SampCutReqMaster.Size4CutQty,0)+ isnull(SampCutReqMaster.Size5CutQty,0)+ isnull(SampCutReqMaster.Size6CutQty,0))as CutQty, 
						 (isnull(SampCutReqMaster.Size1SewQty,0)+isnull(SampCutReqMaster.Size2SewQty,0)+ isnull(SampCutReqMaster.Size3SewQty,0)+ isnull(SampCutReqMaster.Size4SewQty,0)+ isnull(SampCutReqMaster.Size5SewQty,0)+ isnull(SampCutReqMaster.Size6SewQty,0))as SewQty, 
                         (isnull(SampCutReqMaster.Size1DeliveredQty,0)+ isnull(SampCutReqMaster.Size2DeliveredQty,0)+ isnull(SampCutReqMaster.Size3DeliveredQty,0)+ isnull(SampCutReqMaster.Size4DeliveredQty,0)+ isnull(SampCutReqMaster.Size5DeliveredQty,0)+
                         isnull(SampCutReqMaster.Size6DeliveredQty,0))As DeliveredQty
FROM            SamCutAssignmentMaster INNER JOIN
                         SampCutReqMaster ON SamCutAssignmentMaster.SampCutreqID = SampCutReqMaster.SampCutreqID INNER JOIN
                         SampleType ON SampCutReqMaster.SampleTypeID = SampleType.SampleTypeID INNER JOIN
                         BuyerMaster ON SampCutReqMaster.BuyerID = BuyerMaster.BuyerID INNER JOIN
                         PatterRefMaster ON SampCutReqMaster.PatternRefID = PatterRefMaster.PatternRefID LEFT OUTER JOIN
                         PatternMaster ON SamCutAssignmentMaster.PatternMasterID = PatternMaster.PatternMasterID";



            return QueryFunctions.ReturnQueryResultDatatable(Qry);
        }

        public DataTable getAlldataofperiod(DateTime fromdate, DateTime todate)
        {


            string Qry = @"SELECT        SampCutReqMaster.ReqNum, BuyerMaster.BuyerName, PatterRefMaster.PatterRefNum, SampCutReqMaster.Fabric, SampleType.SampleType, SampCutReqMaster.SampleRequiredDate, 
                         SamCutAssignmentMaster.PatternReqDate, SampCutReqMaster.SizeDetail, SampCutReqMaster.Qty, SamCutAssignmentMaster.ReceivedDate, SamCutAssignmentMaster.ReceivedBy, 
                         SamCutAssignmentMaster.AssignedDate, SamCutAssignmentMaster.SignedDate, SamCutAssignmentMaster.CompletedDate, SamCutAssignmentMaster.Remark, SamCutAssignmentMaster.PatternCompletedDate, 
                         PatternMaster.PaternMasterName, SamCutAssignmentMaster.SignedBYMaster, SampCutReqMaster.MarkCompleted, PatternMaster.PatternMasterID,SampCutReqMaster.AddedBy, 
                         SamCutAssignmentMaster.CompletedQty, SamCutAssignmentMaster.PendingReason,
						 ( isnull( SampCutReqMaster.Size1CutQty,0)+ isnull(SampCutReqMaster.Size2CutQty,0)+isnull( SampCutReqMaster.Size3CutQty,0)+ isnull(SampCutReqMaster.Size4CutQty,0)+ isnull(SampCutReqMaster.Size5CutQty,0)+ isnull(SampCutReqMaster.Size6CutQty,0))as CutQty, 
						 (isnull(SampCutReqMaster.Size1SewQty,0)+isnull(SampCutReqMaster.Size2SewQty,0)+ isnull(SampCutReqMaster.Size3SewQty,0)+ isnull(SampCutReqMaster.Size4SewQty,0)+ isnull(SampCutReqMaster.Size5SewQty,0)+ isnull(SampCutReqMaster.Size6SewQty,0))as SewQty, 
                         (isnull(SampCutReqMaster.Size1DeliveredQty,0)+ isnull(SampCutReqMaster.Size2DeliveredQty,0)+ isnull(SampCutReqMaster.Size3DeliveredQty,0)+ isnull(SampCutReqMaster.Size4DeliveredQty,0)+ isnull(SampCutReqMaster.Size5DeliveredQty,0)+
                         isnull(SampCutReqMaster.Size6DeliveredQty,0))As DeliveredQty
FROM            SamCutAssignmentMaster INNER JOIN
                         SampCutReqMaster ON SamCutAssignmentMaster.SampCutreqID = SampCutReqMaster.SampCutreqID INNER JOIN
                         SampleType ON SampCutReqMaster.SampleTypeID = SampleType.SampleTypeID INNER JOIN
                         BuyerMaster ON SampCutReqMaster.BuyerID = BuyerMaster.BuyerID INNER JOIN
                         PatterRefMaster ON SampCutReqMaster.PatternRefID = PatterRefMaster.PatternRefID LEFT OUTER JOIN
                         PatternMaster ON SamCutAssignmentMaster.PatternMasterID = PatternMaster.PatternMasterID
WHERE         (SamCutAssignmentMaster.ReceivedDate BETWEEN '" + fromdate + "' AND '" + todate + "' )";





            return QueryFunctions.ReturnQueryResultDatatable(Qry);
        }

        public DataTable getAlldataofmasterOfPeriod(int masterid, DateTime fromdate, DateTime todate)
        {

            string Qry = @"SELECT        SampCutReqMaster.ReqNum, BuyerMaster.BuyerName, PatterRefMaster.PatterRefNum, SampCutReqMaster.Fabric, SampleType.SampleType, SampCutReqMaster.SampleRequiredDate, 
                         SamCutAssignmentMaster.PatternReqDate, SampCutReqMaster.SizeDetail, SampCutReqMaster.Qty, SamCutAssignmentMaster.ReceivedDate, SamCutAssignmentMaster.ReceivedBy, 
                         SamCutAssignmentMaster.AssignedDate, SamCutAssignmentMaster.SignedDate, SamCutAssignmentMaster.CompletedDate, SamCutAssignmentMaster.Remark, SamCutAssignmentMaster.PatternCompletedDate, 
                         PatternMaster.PaternMasterName, SamCutAssignmentMaster.SignedBYMaster, SampCutReqMaster.MarkCompleted, PatternMaster.PatternMasterID,SampCutReqMaster.AddedBy, 
                         SamCutAssignmentMaster.CompletedQty, SamCutAssignmentMaster.PendingReason,
						 ( isnull( SampCutReqMaster.Size1CutQty,0)+ isnull(SampCutReqMaster.Size2CutQty,0)+isnull( SampCutReqMaster.Size3CutQty,0)+ isnull(SampCutReqMaster.Size4CutQty,0)+ isnull(SampCutReqMaster.Size5CutQty,0)+ isnull(SampCutReqMaster.Size6CutQty,0))as CutQty, 
						 (isnull(SampCutReqMaster.Size1SewQty,0)+isnull(SampCutReqMaster.Size2SewQty,0)+ isnull(SampCutReqMaster.Size3SewQty,0)+ isnull(SampCutReqMaster.Size4SewQty,0)+ isnull(SampCutReqMaster.Size5SewQty,0)+ isnull(SampCutReqMaster.Size6SewQty,0))as SewQty, 
                         (isnull(SampCutReqMaster.Size1DeliveredQty,0)+ isnull(SampCutReqMaster.Size2DeliveredQty,0)+ isnull(SampCutReqMaster.Size3DeliveredQty,0)+ isnull(SampCutReqMaster.Size4DeliveredQty,0)+ isnull(SampCutReqMaster.Size5DeliveredQty,0)+
                         isnull(SampCutReqMaster.Size6DeliveredQty,0))As DeliveredQty
FROM            SamCutAssignmentMaster INNER JOIN
                         SampCutReqMaster ON SamCutAssignmentMaster.SampCutreqID = SampCutReqMaster.SampCutreqID INNER JOIN
                         SampleType ON SampCutReqMaster.SampleTypeID = SampleType.SampleTypeID INNER JOIN
                         BuyerMaster ON SampCutReqMaster.BuyerID = BuyerMaster.BuyerID INNER JOIN
                         PatterRefMaster ON SampCutReqMaster.PatternRefID = PatterRefMaster.PatternRefID LEFT OUTER JOIN
                         PatternMaster ON SamCutAssignmentMaster.PatternMasterID = PatternMaster.PatternMasterID
WHERE        (PatternMaster.PatternMasterID = " + masterid + ") AND (SamCutAssignmentMaster.ReceivedDate BETWEEN '" + fromdate + "' AND '" + todate + "' )";



            return QueryFunctions.ReturnQueryResultDatatable(Qry);
        }



        public DataTable getAlldataofBuyerOfPeriod(int buyerid, DateTime fromdate, DateTime todate)
        {

            string Qry = @"SELECT        SampCutReqMaster.ReqNum, BuyerMaster.BuyerName, PatterRefMaster.PatterRefNum, SampCutReqMaster.Fabric, SampleType.SampleType, SampCutReqMaster.SampleRequiredDate, 
                         SamCutAssignmentMaster.PatternReqDate, SampCutReqMaster.SizeDetail, SampCutReqMaster.Qty, SamCutAssignmentMaster.ReceivedDate, SamCutAssignmentMaster.ReceivedBy, 
                         SamCutAssignmentMaster.AssignedDate, SamCutAssignmentMaster.SignedDate, SamCutAssignmentMaster.CompletedDate, SamCutAssignmentMaster.Remark, SamCutAssignmentMaster.PatternCompletedDate, 
                         PatternMaster.PaternMasterName, SamCutAssignmentMaster.SignedBYMaster, SampCutReqMaster.MarkCompleted, PatternMaster.PatternMasterID,SampCutReqMaster.AddedBy, 
                         SamCutAssignmentMaster.CompletedQty, SamCutAssignmentMaster.PendingReason,
						 ( isnull( SampCutReqMaster.Size1CutQty,0)+ isnull(SampCutReqMaster.Size2CutQty,0)+isnull( SampCutReqMaster.Size3CutQty,0)+ isnull(SampCutReqMaster.Size4CutQty,0)+ isnull(SampCutReqMaster.Size5CutQty,0)+ isnull(SampCutReqMaster.Size6CutQty,0))as CutQty, 
						 (isnull(SampCutReqMaster.Size1SewQty,0)+isnull(SampCutReqMaster.Size2SewQty,0)+ isnull(SampCutReqMaster.Size3SewQty,0)+ isnull(SampCutReqMaster.Size4SewQty,0)+ isnull(SampCutReqMaster.Size5SewQty,0)+ isnull(SampCutReqMaster.Size6SewQty,0))as SewQty, 
                         (isnull(SampCutReqMaster.Size1DeliveredQty,0)+ isnull(SampCutReqMaster.Size2DeliveredQty,0)+ isnull(SampCutReqMaster.Size3DeliveredQty,0)+ isnull(SampCutReqMaster.Size4DeliveredQty,0)+ isnull(SampCutReqMaster.Size5DeliveredQty,0)+
                         isnull(SampCutReqMaster.Size6DeliveredQty,0))As DeliveredQty
FROM            SamCutAssignmentMaster INNER JOIN
                         SampCutReqMaster ON SamCutAssignmentMaster.SampCutreqID = SampCutReqMaster.SampCutreqID INNER JOIN
                         SampleType ON SampCutReqMaster.SampleTypeID = SampleType.SampleTypeID INNER JOIN
                         BuyerMaster ON SampCutReqMaster.BuyerID = BuyerMaster.BuyerID INNER JOIN
                         PatterRefMaster ON SampCutReqMaster.PatternRefID = PatterRefMaster.PatternRefID LEFT OUTER JOIN
                         PatternMaster ON SamCutAssignmentMaster.PatternMasterID = PatternMaster.PatternMasterID
WHERE        (SampCutReqMaster.BuyerID = " + buyerid + ") AND (SamCutAssignmentMaster.ReceivedDate BETWEEN '" + fromdate + "' AND '" + todate + "' )";



            return QueryFunctions.ReturnQueryResultDatatable(Qry);
        }



        public DataTable GetAlldataofFabricWithinperiod(String Fabric, DateTime fromdate, DateTime todate)
        {

            string Qry = @"SELECT        SampCutReqMaster.ReqNum, BuyerMaster.BuyerName, PatterRefMaster.PatterRefNum, SampCutReqMaster.Fabric, SampleType.SampleType, SampCutReqMaster.SampleRequiredDate, 
                         SamCutAssignmentMaster.PatternReqDate, SampCutReqMaster.SizeDetail, SampCutReqMaster.Qty, SamCutAssignmentMaster.ReceivedDate, SamCutAssignmentMaster.ReceivedBy, 
                         SamCutAssignmentMaster.AssignedDate, SamCutAssignmentMaster.SignedDate, SamCutAssignmentMaster.CompletedDate, SamCutAssignmentMaster.Remark, SamCutAssignmentMaster.PatternCompletedDate, 
                         PatternMaster.PaternMasterName, SamCutAssignmentMaster.SignedBYMaster, SampCutReqMaster.MarkCompleted, PatternMaster.PatternMasterID,SampCutReqMaster.AddedBy, 
                         SamCutAssignmentMaster.CompletedQty, SamCutAssignmentMaster.PendingReason,,
						 ( isnull( SampCutReqMaster.Size1CutQty,0)+ isnull(SampCutReqMaster.Size2CutQty,0)+isnull( SampCutReqMaster.Size3CutQty,0)+ isnull(SampCutReqMaster.Size4CutQty,0)+ isnull(SampCutReqMaster.Size5CutQty,0)+ isnull(SampCutReqMaster.Size6CutQty,0))as CutQty, 
						 (isnull(SampCutReqMaster.Size1SewQty,0)+isnull(SampCutReqMaster.Size2SewQty,0)+ isnull(SampCutReqMaster.Size3SewQty,0)+ isnull(SampCutReqMaster.Size4SewQty,0)+ isnull(SampCutReqMaster.Size5SewQty,0)+ isnull(SampCutReqMaster.Size6SewQty,0))as SewQty, 
                         (isnull(SampCutReqMaster.Size1DeliveredQty,0)+ isnull(SampCutReqMaster.Size2DeliveredQty,0)+ isnull(SampCutReqMaster.Size3DeliveredQty,0)+ isnull(SampCutReqMaster.Size4DeliveredQty,0)+ isnull(SampCutReqMaster.Size5DeliveredQty,0)+
                         isnull(SampCutReqMaster.Size6DeliveredQty,0))As DeliveredQty
FROM            SamCutAssignmentMaster INNER JOIN
                         SampCutReqMaster ON SamCutAssignmentMaster.SampCutreqID = SampCutReqMaster.SampCutreqID INNER JOIN
                         SampleType ON SampCutReqMaster.SampleTypeID = SampleType.SampleTypeID INNER JOIN
                         BuyerMaster ON SampCutReqMaster.BuyerID = BuyerMaster.BuyerID INNER JOIN
                         PatterRefMaster ON SampCutReqMaster.PatternRefID = PatterRefMaster.PatternRefID LEFT OUTER JOIN
                         PatternMaster ON SamCutAssignmentMaster.PatternMasterID = PatternMaster.PatternMasterID
WHERE        (SampCutReqMaster.Fabric= '" + Fabric + "') AND (SamCutAssignmentMaster.ReceivedDate BETWEEN '" + fromdate + "' AND '" + todate + "' )";



            return QueryFunctions.ReturnQueryResultDatatable(Qry);
        }



        public DataTable GetAllPending()
        {

            string Qry = @"SELECT        SampCutReqMaster.ReqNum, BuyerMaster.BuyerName, PatterRefMaster.PatterRefNum, SampCutReqMaster.Fabric, SampleType.SampleType, SampCutReqMaster.SampleRequiredDate, 
                         SamCutAssignmentMaster.PatternReqDate, SampCutReqMaster.SizeDetail, SampCutReqMaster.Qty, SamCutAssignmentMaster.ReceivedDate, SamCutAssignmentMaster.ReceivedBy, 
                         SamCutAssignmentMaster.AssignedDate, SamCutAssignmentMaster.SignedDate, SamCutAssignmentMaster.CompletedDate, SamCutAssignmentMaster.Remark, SamCutAssignmentMaster.PatternCompletedDate, 
                         PatternMaster.PaternMasterName, SamCutAssignmentMaster.SignedBYMaster, SampCutReqMaster.MarkCompleted, PatternMaster.PatternMasterID, SampCutReqMaster.AddedBy, 
                         SamCutAssignmentMaster.CompletedQty, 0 AS PendingforDay, SamCutAssignmentMaster.PendingReason, PatternStyle.StyleName as StyleDescription,
						 ( isnull( SampCutReqMaster.Size1CutQty,0)+ isnull(SampCutReqMaster.Size2CutQty,0)+isnull( SampCutReqMaster.Size3CutQty,0)+ isnull(SampCutReqMaster.Size4CutQty,0)+ isnull(SampCutReqMaster.Size5CutQty,0)+ isnull(SampCutReqMaster.Size6CutQty,0))as CutQty, 
						 (isnull(SampCutReqMaster.Size1SewQty,0)+isnull(SampCutReqMaster.Size2SewQty,0)+ isnull(SampCutReqMaster.Size3SewQty,0)+ isnull(SampCutReqMaster.Size4SewQty,0)+ isnull(SampCutReqMaster.Size5SewQty,0)+ isnull(SampCutReqMaster.Size6SewQty,0))as SewQty, 
                         (isnull(SampCutReqMaster.Size1DeliveredQty,0)+ isnull(SampCutReqMaster.Size2DeliveredQty,0)+ isnull(SampCutReqMaster.Size3DeliveredQty,0)+ isnull(SampCutReqMaster.Size4DeliveredQty,0)+ isnull(SampCutReqMaster.Size5DeliveredQty,0)+
                         isnull(SampCutReqMaster.Size6DeliveredQty,0))As DeliveredQty
FROM            SamCutAssignmentMaster INNER JOIN
                         SampCutReqMaster ON SamCutAssignmentMaster.SampCutreqID = SampCutReqMaster.SampCutreqID INNER JOIN
                         SampleType ON SampCutReqMaster.SampleTypeID = SampleType.SampleTypeID INNER JOIN
                         BuyerMaster ON SampCutReqMaster.BuyerID = BuyerMaster.BuyerID INNER JOIN
                         PatterRefMaster ON SampCutReqMaster.PatternRefID = PatterRefMaster.PatternRefID INNER JOIN
                         PatternStyle ON SampCutReqMaster.PatternStyleID = PatternStyle.PatternStyleID LEFT OUTER JOIN
                         PatternMaster ON SamCutAssignmentMaster.PatternMasterID = PatternMaster.PatternMasterID
WHERE        (SampCutReqMaster.MarkCompleted IS NULL)
";



            return QueryFunctions.ReturnQueryResultDatatable(Qry);
        }
    }
}