Imports System.Data
Imports System.IO
Imports CustodianAdmin.Data
Imports CustodianAdmin.Model
Imports CustodianAdmin.Repositories
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web
Imports CrystalDecisions.CrystalReports.Engine

Partial Public Class AD132
    Inherits System.Web.UI.Page
    Dim lifReceipt As ReportDocument
    Dim reportpath As String = SiteGlobal.ReportPath
    Dim reportname As String = String.Empty

    Protected FirstMsg As String
    Protected PageLinks As String
    Protected publicMsgs As String = String.Empty
    Protected strPOP_UP As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

        Else 'post back
        End If


    End Sub
    Private Sub SetComboBinding(ByVal toBind As ListControl, ByVal dataSource As Object, ByVal displayMember As String, ByVal valueMember As String)
        toBind.DataTextField = displayMember
        toBind.DataValueField = valueMember
        toBind.DataSource = dataSource
        toBind.DataBind()
    End Sub

    Private Function ValidDate(ByVal DateValue As String) As DateTime
        Dim dateparts() As String = DateValue.Split(Microsoft.VisualBasic.ChrW(47))
        Dim strDateTest As String = dateparts(1) & "/" & dateparts(0) & "/" & dateparts(2)
        Dim dateIn As Date = Format(CDate(strDateTest), "MM/dd/yyyy")
        Return dateIn
    End Function
    Private Function ValidDateFromDB(ByVal DateValue As Date) As String
        Dim dateparts() As String = DateValue.Date.ToString.Split(Microsoft.VisualBasic.ChrW(47))
        Dim strDateTest As String = dateparts(1) & "/" & dateparts(0) & "/" & Left(dateparts(2), 4)
        Return strDateTest
    End Function

    Protected Sub butPrint_Click(ByVal sender As Object, ByVal e As EventArgs) Handles butPrint.Click

        Print_This()
    End Sub
    Private Sub Print_This()
        '***********************************************
        'Declare variables to be used
        '***********************************************

        'Dim ServerName As String = SiteGlobal.ReportServer
        'Dim DatabaseName As String = SiteGlobal.ReportDBR
        'Dim userIDName As String = SiteGlobal.DBUserR
        'Dim pwdName As String = SiteGlobal.DBPassWd

        Dim crConnectionInfo As New ConnectionInfo
        Dim CrTables As Tables
        Dim CrTable As Table
        Dim crtableLogoninfos As New TableLogOnInfo
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crParameterFieldDefinitions As CrystalDecisions.CrystalReports.Engine.ParameterFieldDefinitions
        Dim crParameterFieldDefinition As CrystalDecisions.CrystalReports.Engine.ParameterFieldDefinition

        Dim crParameterDiscreteValue As New ParameterDiscreteValue
        Dim crParameterDiscreteValue2 As New ParameterDiscreteValue
        Dim crParameterDiscreteValue3 As New ParameterDiscreteValue
        Dim crParameterDiscreteValue4 As New ParameterDiscreteValue
        Dim crParameterValues As New ParameterValues

        Dim oRpt As ReportDocument
        Dim msg As String = String.Empty

        '*********************************************************
        'Power on the report variable, initialize, and load it
        '*********************************************************

        oRpt = New ReportDocument

        reportpath = SiteGlobal.ReportPath
        reportname = Request.QueryString("reptname")
        reportPath = reportPath & reportname
        oRpt.Load(reportpath)


        '*********************************************************
        'Attach connection info
        '*********************************************************
        Try

            With crConnectionInfo
                .ServerName = SiteGlobal.ReportServer
                .DatabaseName = SiteGlobal.ReportDBR
                .UserID = SiteGlobal.DBUserR
                .Password = SiteGlobal.DBPassWd
            End With
            CrTables = oRpt.Database.Tables
            For Each CrTable In CrTables

                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
                CrTable.Location = crConnectionInfo.DatabaseName & ".dbo." & CrTable.Location.Substring(CrTable.Location.LastIndexOf(".") + 1)

            Next

            '*********************************************************
            'Instantiate and apply all report parameters
            '*********************************************************

            crParameterDiscreteValue.Value = ValidDate(txtStartDate.Text)
            crParameterFieldDefinitions = oRpt.DataDefinition.ParameterFields
            crParameterFieldDefinition = crParameterFieldDefinitions.Item("@RPT_ST_DATE")
            crParameterValues = crParameterFieldDefinition.CurrentValues
            crParameterValues.Add(crParameterDiscreteValue)

            crParameterDiscreteValue2.Value = ValidDate(txtEndDate.Text)
            crParameterFieldDefinitions = oRpt.DataDefinition.ParameterFields
            crParameterFieldDefinition = crParameterFieldDefinitions.Item("@RPT_END_DATE")
            crParameterValues = crParameterFieldDefinition.CurrentValues
            crParameterValues.Add(crParameterDiscreteValue2)


            crParameterDiscreteValue3.Value = Trim(txtVehNumStart.Text)
            crParameterFieldDefinitions = oRpt.DataDefinition.ParameterFields
            crParameterFieldDefinition = crParameterFieldDefinitions.Item("@RPT_ST_VEH_NO")
            crParameterValues = crParameterFieldDefinition.CurrentValues
            crParameterValues.Add(crParameterDiscreteValue3)


            crParameterDiscreteValue4.Value = Trim(txtVehNumEnd.Text)
            crParameterFieldDefinitions = oRpt.DataDefinition.ParameterFields
            crParameterFieldDefinition = crParameterFieldDefinitions.Item("@RPT_END_VEH_NO")
            crParameterValues = crParameterFieldDefinition.CurrentValues
            crParameterValues.Add(crParameterDiscreteValue4)

            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

            '*********************************************************
            'Execute the report object. Render it through the viewer 
            'Control or streaming to pdf or excel
            '*********************************************************
            'using a viewer

            With CrystalReportViewer1
                .ParameterFieldInfo = oRpt.ParameterFields
                .ReportSource = oRpt
                .HasPrintButton = True
                .HasRefreshButton = True
                .HasRefreshButton = True
                .HasSearchButton = True
                .HasZoomFactorList = True
                .HasToggleGroupTreeButton = True
                .HasZoomFactorList = True
                .HasPageNavigationButtons = True
                .HasGotoPageButton = True
                .DisplayPage = True
                .DisplayToolbar = True
                .DisplayGroupTree = True
                .DataBind()
            End With

            ''for Pdf
            'Dim oStream As New MemoryStream 'using System.IO
            'oRpt.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape
            'oStream = oRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat)
            'Response.Clear()
            'Response.Buffer = True
            'Response.ContentType = "application/pdf" 'vnd.ms-word
            'Response.BinaryWrite(oStream.ToArray())
            'Response.End()

            'For excel
            'oStream = oRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel)
            'Response.Clear()
            'Response.Buffer = True
            'Response.ContentType = "application/vnd.ms-excel"
            'Response.BinaryWrite(oStream.ToArray())
            'Response.End()

        Catch ex As Exception
            msg = ex.Message
            txtAction.Text = msg
            txtAction.Visible = True
            publicMsgs = "javascript:alert('" + msg + "')"

        End Try

    End Sub
End Class