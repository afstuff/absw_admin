Imports System.Data
Imports CustodianAdmin.Data
Imports CustodianAdmin.Model
Imports CustodianAdmin.Repositories

Partial Public Class AD170
    Inherits System.Web.UI.Page

    Protected FirstMsg As String
    Protected PageLinks As String

    Protected strPOP_UP As String

    Dim ibRepo As InsurancePremRepository
    Dim acRepo As AdminCodeRepository
    Dim ibill As InsurancePrem
    Dim updateFlag As Boolean
    Dim strKey As String
    Dim TotSumInsAmt As Decimal = 0
    Dim SumInsAmt As Decimal = 0
    Dim TotPremAmt As Decimal = 0
    Dim PremAmt As Decimal = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'SessionProvider.RebuildSchema()
        txtTransNum.Attributes.Add("disabled", "disabled")

        If Not Page.IsPostBack Then
            ibRepo = New InsurancePremRepository
            acRepo = New AdminCodeRepository

            Session("ibRepo") = ibRepo
            Session("acRepo") = acRepo
            updateFlag = False
            Session("updateFlag") = updateFlag
            strKey = Request.QueryString("idd")
            Session("strKey") = strKey


            SetComboBinding(cmbInsurerName, acRepo.GetAdminCodes("004"), "ItemDesc", "ItemCode")
            SetComboBinding(cmbCoInsurerName1, acRepo.GetAdminCodes("004"), "ItemDesc", "ItemCode")
            SetComboBinding(cmbCoInsurerName2, acRepo.GetAdminCodes("004"), "ItemDesc", "ItemCode")
            SetComboBinding(cmbCoInsurerName3, acRepo.GetAdminCodes("004"), "ItemDesc", "ItemCode")
            SetComboBinding(cmbCoInsurerName4, acRepo.GetAdminCodes("004"), "ItemDesc", "ItemCode")
            SetComboBinding(cmbCoInsurerName5, acRepo.GetAdminCodes("004"), "ItemDesc", "ItemCode")
            SetComboBinding(cmbBrokerName, acRepo.GetAdminCodes("005"), "ItemDesc", "ItemCode")
            SetComboBinding(ddlBraNum, acRepo.GetAdminCodes("009"), "ItemDesc", "ItemCode")
            SetComboBinding(ddlDeptNum, acRepo.GetAdminCodes("008"), "ItemDesc", "ItemCode")

            If strKey IsNot Nothing Then
                FillValues()
            Else
                ibRepo = CType(Session("ibRepo"), InsurancePremRepository)
                acRepo = CType(Session("acRepo"), AdminCodeRepository)
            End If

        Else 'post back

            ibRepo = CType(Session("ibRepo"), InsurancePremRepository)
            acRepo = CType(Session("acRepo"), AdminCodeRepository)

            Me.Validate()
            If (Not Me.IsValid) Then
                Dim msg As String
                ' Loop through all validation controls to see which 
                ' generated the error(s).
                Dim oValidator As IValidator
                For Each oValidator In Validators
                    If oValidator.IsValid = False Then
                        msg = msg & "\n" & oValidator.ErrorMessage
                    End If
                Next

                'lblError.Text = msg
                'lblError.Visible = True
                'publicMsgs = "javascript:alert('" + msg + "')"
            End If



        End If


        Try
            strPOP_UP = CType(Request.QueryString("popup"), String)
        Catch ex As Exception
            strPOP_UP = "NO"
        End Try

        If UCase(Trim(strPOP_UP)) = "YES" Then
            Me.PageAnchor_Return_Link.Visible = False
            PageLinks = "<a href='javascript:void(0);' onclick='javascript:window.close();'>CLOSE PAGE...</a>"
        Else
            Me.PageAnchor_Return_Link.Visible = True
            PageLinks = ""
        End If

    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdSave.Click, cmdSaveN.Click
        updateFlag = CType(Session("updateFlag"), String)

        If Not updateFlag Then 'if new record
            ibill = New InsurancePrem
            txtTransNum.Text = acRepo.GetNextSerialNumber("L01", "003", "01", Date.Now.Year.ToString(), "", "01", "01")
            'lblError.Visible = False
            ibill.BranchCode = ddlBraNum.SelectedValue.ToString()
            ibill.Department = ddlDeptNum.SelectedValue.ToString()
            ibill.TransDescription = txtTransDescr.Text
            ibill.EntryDate = Date.Now
            ibill.EntryFlag = "A"
            ibill.OperatorId = "001"
            ibill.TransNo = Trim(txtTransNum.Text)
            ibill.TransDate = ValidDate(txtTransDate.Text)
            ibill.SumInsured = Math.Round(CType(txtSumInsured.Text, Decimal), 2)
            ibill.PremiumAmt = Math.Round(CType(txtPremAmt.Text, Decimal), 2)
            ibill.TransactionType = cmbTransType.SelectedValue


            ibill.PolicyNo = txtPolicyNo.Text
            ibill.InsurerName = cmbInsurerName.SelectedValue
            ibill.CoInsurer1 = cmbCoInsurerName1.SelectedValue
            ibill.CoInsurer2 = cmbCoInsurerName2.SelectedValue
            ibill.CoInsurer3 = cmbCoInsurerName3.SelectedValue
            ibill.CoInsurer4 = cmbCoInsurerName4.SelectedValue
            ibill.CoInsurer5 = cmbCoInsurerName5.SelectedValue

            ibill.BrokerName = cmbBrokerName.SelectedValue
            ibill.EndDate = ValidDate(txtEndDate.Text)
            ibill.StartDate = ValidDate(txtStartDate.Text)

            ibRepo.Save(ibill)
            Session("ibill") = ibill
        Else
            ibRepo = CType(Session("ibRepo"), InsurancePremRepository)
            ibill = CType(Session("ibill"), InsurancePrem)

            ibill.BranchCode = ddlBraNum.SelectedValue.ToString()
            ibill.Department = ddlDeptNum.SelectedValue.ToString()
            ibill.TransDescription = txtTransDescr.Text
            ibill.TransDate = ValidDate(txtTransDate.Text)
            ibill.TransactionType = cmbTransType.SelectedValue

            ibill.SumInsured = Math.Round(CType(txtSumInsured.Text, Decimal), 2)
            ibill.PremiumAmt = Math.Round(CType(txtPremAmt.Text, Decimal), 2)
            ibill.TransactionType = cmbTransType.SelectedValue

            ibill.PolicyNo = txtPolicyNo.Text
            ibill.InsurerName = cmbInsurerName.SelectedValue
            ibill.CoInsurer1 = cmbCoInsurerName1.SelectedValue
            ibill.CoInsurer2 = cmbCoInsurerName2.SelectedValue
            ibill.CoInsurer3 = cmbCoInsurerName3.SelectedValue
            ibill.CoInsurer4 = cmbCoInsurerName4.SelectedValue
            ibill.CoInsurer5 = cmbCoInsurerName5.SelectedValue

            ibill.BrokerName = cmbBrokerName.SelectedValue
            ibill.EndDate = ValidDate(txtEndDate.Text)
            ibill.StartDate = ValidDate(txtStartDate.Text)

            ibRepo.Save(ibill)

        End If
        grdData.DataBind()
        initializeFields()

    End Sub
    Private Sub initializeFields()
        txtBraNum.Text = String.Empty
        txtDeptName.Text = String.Empty
        txtDeptNum.Text = String.Empty
        txtTransDate.Text = String.Empty
        txtTransDescr.Text = String.Empty
        ddlBraNum.SelectedIndex = 0
        ddlDeptNum.SelectedIndex = 0
        cmbTransType.SelectedIndex = 0
        txtTransDescr.Text = String.Empty
        txtSumInsured.Text = String.Empty
        txtPremAmt.Text = String.Empty
        txtPolicyNo.Text = String.Empty
        txtInsurerName.Text = String.Empty
        txtEndDate.Text = String.Empty
        txtStartDate.Text = String.Empty
        cmbInsurerName.SelectedIndex = 0
        cmbCoInsurerName1.SelectedIndex = 0
        cmbCoInsurerName2.SelectedIndex = 0
        cmbCoInsurerName3.SelectedIndex = 0
        cmbCoInsurerName4.SelectedIndex = 0
        cmbCoInsurerName5.SelectedIndex = 0
        cmbBrokerName.SelectedIndex = 0

    End Sub
    Private Sub SetComboBinding(ByVal toBind As ListControl, ByVal dataSource As Object, ByVal displayMember As String, ByVal valueMember As String)
        toBind.DataTextField = displayMember
        toBind.DataValueField = valueMember
        toBind.DataSource = dataSource
        toBind.DataBind()
        toBind.Items.Insert(0, New ListItem("Select", "NA"))
    End Sub

    Private Sub FillValues()
        strKey = CType(Session("strKey"), String)
        ibRepo = CType(Session("ibRepo"), InsurancePremRepository)
        ibill = CType(Session("ibill"), InsurancePrem)

        ibill = ibRepo.GetById(strKey)

        If ibill IsNot Nothing Then
            With ibill
                txtTransDate.Text = ValidDateFromDB(.TransDate)
                txtTransDescr.Text = .TransDescription

                ddlBraNum.SelectedValue = ibill.BranchCode
                ddlDeptNum.SelectedValue = ibill.Department
                txtTransNum.Text = ibill.TransNo
                cmbTransType.SelectedValue = .TransactionType

                txtSumInsured.Text = Math.Round(ibill.SumInsured, 2)
                txtPremAmt.Text = Math.Round(ibill.PremiumAmt, 2)

                txtPolicyNo.Text = ibill.PolicyNo
                txtEndDate.Text = ValidDateFromDB(ibill.EndDate)
                txtStartDate.Text = ValidDateFromDB(ibill.StartDate)
                cmbInsurerName.SelectedValue = ibill.InsurerName
                cmbCoInsurerName1.SelectedValue = ibill.CoInsurer1
                cmbCoInsurerName2.SelectedValue = ibill.CoInsurer2
                cmbCoInsurerName3.SelectedValue = ibill.CoInsurer3
                cmbCoInsurerName4.SelectedValue = ibill.CoInsurer4
                cmbCoInsurerName5.SelectedValue = ibill.CoInsurer5

                cmbBrokerName.SelectedValue = ibill.BrokerName

                Session("ibill") = ibill
            End With

            updateFlag = True
            Session("updateFlag") = updateFlag
            grdData.DataBind()


        End If
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


    Protected Sub cmdNew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdNew.Click
        initializeFields()
        txtTransNum.Text = String.Empty
        updateFlag = False 'Switches to first time load
        Session("updateFlag") = updateFlag


    End Sub

    Protected Sub cmdDel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdDel.Click, cmdDelN.Click
        Dim msg As String = String.Empty
        ibRepo = CType(Session("ibRepo"), InsurancePremRepository)
        ibill = CType(Session("ibill"), InsurancePrem)
        Try
            ibRepo.Delete(ibill)
            msg = "Delete Successful"
            ' lblError.Text = msg
            grdData.DataBind()

        Catch ex As Exception
            msg = ex.Message
            'lblError.Text = msg
            'lblError.Visible = True
            'publicMsgs = "javascript:alert('" + msg + "')"
        End Try
        initializeFields()



    End Sub

    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim lblPrice As Label = CType(e.Row.FindControl("lblTransAmt"), Label)
            SumInsAmt = (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "SumInsured")))
            TotSumInsAmt = (TotSumInsAmt + SumInsAmt)

            PremAmt = (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "SumInsured")))
            TotPremAmt = (TotPremAmt + PremAmt)

        End If
        If (e.Row.RowType = DataControlRowType.Footer) Then
            Dim lblTotal As Label = CType(e.Row.FindControl("lbltxtTotalSumIns"), Label)
            lblTotal.Text = Math.Round(TotSumInsAmt, 2).ToString
        End If
        If (e.Row.RowType = DataControlRowType.Footer) Then
            Dim lblTotal1 As Label = CType(e.Row.FindControl("lbltxtTotalPrem"), Label)
            lblTotal1.Text = Math.Round(TotPremAmt, 2).ToString
        End If

        'format fields
        Dim ea As GridViewRowEventArgs = CType(e, GridViewRowEventArgs)
        If (ea.Row.RowType = DataControlRowType.DataRow) Then
            Dim drv As InsurancePrem = CType(ea.Row.DataItem, InsurancePrem)
            If Not Convert.IsDBNull(drv.PremiumAmt) Then
                Dim iParsedValue As Decimal = 0
                If Decimal.TryParse(drv.PremiumAmt.ToString, iParsedValue) Then
                    Dim cell As TableCell = ea.Row.Cells(6)
                    cell.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N}", New Object() {iParsedValue})
                End If
            End If
            If Not Convert.IsDBNull(drv.SumInsured) Then
                Dim iParsedValue As Decimal = 0
                If Decimal.TryParse(drv.SumInsured.ToString, iParsedValue) Then
                    Dim cell As TableCell = ea.Row.Cells(5)
                    cell.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N}", New Object() {iParsedValue})
                End If
            End If


        End If

    End Sub

    Protected Sub grdData_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles grdData.SelectedIndexChanged

    End Sub
    Protected Sub DoProc_BrokerName_Search()
        SetComboBinding(cmbBrokerName, acRepo.GetAdminCodes("005", txtBrokerName_Search.Text.Trim), "ItemDesc", "ItemCode")

    End Sub
    Protected Sub DoProc_Broker_Change()
        txtBrokerName.Text = cmbBrokerName.SelectedValue

    End Sub

    Private Sub cmbBrokerName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbBrokerName.SelectedIndexChanged
        DoProc_Broker_Change()
    End Sub

End Class