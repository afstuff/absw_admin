Imports System.Data
Imports System.IO
Imports CustodianAdmin.Data
Imports CustodianAdmin.Model
Imports CustodianAdmin.Repositories
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web
Imports CrystalDecisions.CrystalReports.Engine

Partial Public Class AD112
    Inherits System.Web.UI.Page

    Dim lifReceipt As ReportDocument
    Dim reportpath As String = SiteGlobal.ReportPath
    Dim reportname As String = String.Empty

    Protected FirstMsg As String
    Protected PageLinks As String
    Dim acRepo As AdminCodeRepository

    Protected publicMsgs As String = String.Empty
    Protected strPOP_UP As String
    Dim rParams As String() = {"nw", "nw", "new", "new", "new", "new", "new", "new", "new"}

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            acRepo = New AdminCodeRepository

            SetComboBinding(ddlTransClass, acRepo.GetAdminCodes("010"), "ItemDesc", "ItemCode")
            SetComboBinding(ddlTransID, acRepo.GetAdminCodes("011"), "ItemDesc", "ItemCode")
            Session("acRepo") = acRepo

        Else 'post back
            acRepo = CType(Session("acRepo"), AdminCodeRepository)


        End If


    End Sub
    Private Sub SetComboBinding(ByVal toBind As ListControl, ByVal dataSource As Object, ByVal displayMember As String, ByVal valueMember As String)
        toBind.DataTextField = displayMember
        toBind.DataValueField = valueMember
        toBind.DataSource = dataSource
        toBind.DataBind()
        toBind.Items.Insert(0, New ListItem("Select", "NA"))
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
        Print_That()
    End Sub
    Private Sub Print_This()
        ''***********************************************
        ''Declare variables to be used
        ''***********************************************

        ''Dim ServerName As String = SiteGlobal.ReportServer
        ''Dim DatabaseName As String = SiteGlobal.ReportDBR
        ''Dim userIDName As String = SiteGlobal.DBUserR
        ''Dim pwdName As String = SiteGlobal.DBPassWd

        'Dim crConnectionInfo As New ConnectionInfo
        'Dim CrTables As Tables
        'Dim CrTable As Table
        'Dim crtableLogoninfos As New TableLogOnInfo
        'Dim crtableLogoninfo As New TableLogOnInfo
        'Dim crParameterFieldDefinitions As CrystalDecisions.CrystalReports.Engine.ParameterFieldDefinitions
        'Dim crParameterFieldDefinition As CrystalDecisions.CrystalReports.Engine.ParameterFieldDefinition

        'Dim crParameterDiscreteValue As New ParameterDiscreteValue
        'Dim crParameterDiscreteValue2 As New ParameterDiscreteValue
        'Dim crParameterDiscreteValue3 As New ParameterDiscreteValue
        'Dim crParameterDiscreteValue4 As New ParameterDiscreteValue
        'Dim crParameterValues As New ParameterValues

        'Dim oRpt As ReportDocument
        'Dim msg As String = String.Empty

        ''*********************************************************
        ''Power on the report variable, initialize, and load it
        ''*********************************************************

        'oRpt = New ReportDocument

        'reportpath = SiteGlobal.ReportPath
        'reportname = "ADMIN_ALLEXP_LIST.rpt"
        'reportPath = reportPath & reportname
        'oRpt.Load(reportpath)
        ''oRpt.r()


        ''*********************************************************
        ''Attach connection info
        ''*********************************************************
        'Try

        '    With crConnectionInfo
        '        .ServerName = SiteGlobal.ReportServer
        '        .DatabaseName = SiteGlobal.ReportDBR
        '        .UserID = SiteGlobal.DBUserR
        '        .Password = SiteGlobal.DBPassWd
        '    End With
        '    CrTables = oRpt.Database.Tables
        '    For Each CrTable In CrTables

        '        crtableLogoninfo = CrTable.LogOnInfo
        '        crtableLogoninfo.ConnectionInfo = crConnectionInfo
        '        CrTable.ApplyLogOnInfo(crtableLogoninfo)
        '        CrTable.Location = crConnectionInfo.DatabaseName & ".dbo." & CrTable.Location.Substring(CrTable.Location.LastIndexOf(".") + 1)

        '    Next

        '    '*********************************************************
        '    'Instantiate and apply all report parameters
        '    '*********************************************************

        '    'crParameterDiscreteValue.Value = ValidDate(txtStartDate.Text)
        '    'crParameterFieldDefinitions = oRpt.DataDefinition.ParameterFields
        '    'crParameterFieldDefinition = crParameterFieldDefinitions.Item("@pStartDate")
        '    'crParameterValues = crParameterFieldDefinition.CurrentValues
        '    'crParameterValues.Add(crParameterDiscreteValue)

        '    'crParameterDiscreteValue2.Value = ValidDate(txtEndDate.Text)
        '    'crParameterFieldDefinitions = oRpt.DataDefinition.ParameterFields
        '    'crParameterFieldDefinition = crParameterFieldDefinitions.Item("@pEndDate")
        '    'crParameterValues = crParameterFieldDefinition.CurrentValues
        '    'crParameterValues.Add(crParameterDiscreteValue2)


        '    'crParameterDiscreteValue3.Value = Trim(txtTransClass.Text)
        '    'crParameterFieldDefinitions = oRpt.DataDefinition.ParameterFields
        '    'crParameterFieldDefinition = crParameterFieldDefinitions.Item("@pTransClass")
        '    'crParameterValues = crParameterFieldDefinition.CurrentValues
        '    'crParameterValues.Add(crParameterDiscreteValue3)


        '    'crParameterDiscreteValue4.Value = Trim(txtTransID.Text)
        '    'crParameterFieldDefinitions = oRpt.DataDefinition.ParameterFields
        '    'crParameterFieldDefinition = crParameterFieldDefinitions.Item("@pTransID")
        '    'crParameterValues = crParameterFieldDefinition.CurrentValues
        '    'crParameterValues.Add(crParameterDiscreteValue4)


        '    crParameterFieldDefinitions = oRpt.DataDefinition.ParameterFields

        '    crParameterDiscreteValue.Value = ValidDate(txtStartDate.Text)
        '    crParameterFieldDefinition = crParameterFieldDefinitions.Item("@pStartDate")
        '    crParameterValues = crParameterFieldDefinition.CurrentValues
        '    crParameterValues.Clear()
        '    crParameterValues.Add(crParameterDiscreteValue)

        '    crParameterDiscreteValue2.Value = ValidDate(txtEndDate.Text)
        '    'crParameterFieldDefinitions = oRpt.DataDefinition.ParameterFields
        '    crParameterFieldDefinition = crParameterFieldDefinitions.Item("@pEndDate")
        '    crParameterFieldDefinition.CurrentValues.Clear()
        '    'crParameterValues = crParameterFieldDefinition.CurrentValues
        '    crParameterValues.Add(crParameterDiscreteValue2)


        '    crParameterDiscreteValue3.Value = Trim(txtTransClass.Text)
        '    'crParameterFieldDefinitions = oRpt.DataDefinition.ParameterFields
        '    crParameterFieldDefinition = crParameterFieldDefinitions.Item("@pTransClass")
        '    crParameterFieldDefinition.CurrentValues.Clear()
        '    'crParameterValues = crParameterFieldDefinition.CurrentValues
        '    crParameterValues.Add(crParameterDiscreteValue3)


        '    crParameterDiscreteValue4.Value = Trim(txtTransID.Text)
        '    'crParameterFieldDefinitions = oRpt.DataDefinition.ParameterFields
        '    crParameterFieldDefinition = crParameterFieldDefinitions.Item("@pTransID")
        '    crParameterFieldDefinition.CurrentValues.Clear()
        '    'crParameterValues = crParameterFieldDefinition.CurrentValues
        '    crParameterValues.Add(crParameterDiscreteValue4)



        '    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)
        '    ' oRpt.ParameterFields = crParameterFieldDefinition.CurrentValues
        '    '*********************************************************
        '    'Execute the report object. Render it through the viewer 
        '    'Control or streaming to pdf or excel
        '    '*********************************************************
        '    'using a viewer
        '    oRpt.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape

        '    With CrystalReportViewer1
        '        .ParameterFieldInfo = oRpt.ParameterFields
        '        .ReportSource = oRpt
        '        .HasPrintButton = True
        '        .HasRefreshButton = True
        '        .HasRefreshButton = True
        '        .HasSearchButton = True
        '        .HasZoomFactorList = True
        '        .HasToggleGroupTreeButton = True
        '        .HasZoomFactorList = True
        '        .HasPageNavigationButtons = True
        '        .HasGotoPageButton = True
        '        .DisplayPage = True
        '        .DisplayToolbar = True
        '        .DisplayGroupTree = True
        '        .DataBind()
        '    End With

        ''for Pdf
        'Dim oStream As New MemoryStream 'using System.IO
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

        'Catch ex As Exception
        '    msg = ex.Message
        '    txtAction.Text = msg
        '    txtAction.Visible = True
        '    publicMsgs = "javascript:alert('" + msg + "')"

        'End Try

    End Sub
    Private Sub Print_That()
        Dim reportname As String

        Dim sStartDate As String = ConvertMyDate(txtStartDate.Text)
        Dim sEndDate As String = ConvertMyDate(txtEndDate.Text)

        'reportpath = SiteGlobal.ReportPath
        reportname = "AdminExpenses"


        rParams(0) = reportname
        rParams(1) = "pStartDate="
        rParams(2) = sStartDate.Trim + "&"
        rParams(3) = "pEndDate="
        rParams(4) = sEndDate.Trim + "&"
        rParams(5) = "pTransClass="
        rParams(6) = txtTransClass.Text.Trim + "&"
        rParams(7) = "pTransID="
        rParams(8) = txtTransID.Text + "&"


        Session("ReportParams") = rParams
        Response.Redirect("PrintView.aspx")
    End Sub
    Protected Sub ddlTransClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlTransClass.SelectedIndexChanged
        txtTransClass.Text = ddlTransClass.SelectedValue.ToString()
        SetComboBinding(ddlTransID, acRepo.GetAdminOtherCodes("011", ddlTransClass.SelectedValue), "ItemDesc", "ItemCode")


    End Sub

    Protected Sub ddlTransID_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlTransID.SelectedIndexChanged
        txtTransID.Text = ddlTransID.SelectedValue.ToString()
    End Sub
    Public Function ConvertMyDate(ByVal inDate As String) As String
        Dim myDateArray As Array = inDate.Split("/")
        Dim myNewDate As String = myDateArray(1) & "/" & myDateArray(0) & "/" & myDateArray(2)
        ConvertMyDate = myNewDate
    End Function
End Class