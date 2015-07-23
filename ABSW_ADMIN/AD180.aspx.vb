Imports System.Data
Imports CustodianAdmin.Data
Imports CustodianAdmin.Model
Imports CustodianAdmin.Repositories

Partial Public Class AD180
    Inherits System.Web.UI.Page

    Protected FirstMsg As String
    Protected PageLinks As String

    Protected strPOP_UP As String


    Dim cbRepo As InsuranceClaimRepository
    Dim acRepo As AdminCodeRepository
    Dim cbill As InsuranceClaim
    Dim updateFlag As Boolean
    Dim strKey As String
    Dim TotClaimPaid As Decimal = 0
    Dim ClaimPaid As Decimal = 0
    Dim TotClaimRequested As Decimal = 0
    Dim ClaimRequested As Decimal = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'SessionProvider.RebuildSchema()
        txtTransNum.Attributes.Add("disabled", "disabled")

        If Not Page.IsPostBack Then
            cbRepo = New InsuranceClaimRepository
            acRepo = New AdminCodeRepository

            Session("cbRepo") = cbRepo
            Session("acRepo") = acRepo
            updateFlag = False
            Session("updateFlag") = updateFlag
            strKey = Request.QueryString("idd")
            Session("strKey") = strKey


            SetComboBinding(cmbInsurerName, acRepo.GetAdminCodes("004"), "ItemDesc", "ItemCode")
            SetComboBinding(cmbBrokerName, acRepo.GetAdminCodes("005"), "ItemDesc", "ItemCode")
            SetComboBinding(ddlBraNum, acRepo.GetAdminCodes("009"), "ItemDesc", "ItemCode")
            SetComboBinding(ddlDeptNum, acRepo.GetAdminCodes("008"), "ItemDesc", "ItemCode")

            If strKey IsNot Nothing Then
                FillValues()
            Else
                cbRepo = CType(Session("cbRepo"), InsuranceClaimRepository)
                acRepo = CType(Session("acRepo"), AdminCodeRepository)
            End If

        Else 'post back

            cbRepo = CType(Session("cbRepo"), InsuranceClaimRepository)
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
            cbill = New InsuranceClaim
            txtTransNum.Text = acRepo.GetNextSerialNumber("L01", "005", "01", Date.Now.Year.ToString(), "", "01", "01")
            'lblError.Visible = False
            cbill.BranchCode = ddlBraNum.SelectedValue.ToString()
            cbill.Department = ddlDeptNum.SelectedValue.ToString()
            cbill.TransDescription = txtTransDescr.Text
            cbill.EntryDate = Date.Now
            cbill.EntryFlag = "A"
            cbill.OperatorId = "001"
            cbill.TransNo = Trim(txtTransNum.Text)
            cbill.TransDate = ValidDate(txtTransDate.Text)
            cbill.ClaimRequested = Math.Round(CType(txtClaimRequested.Text, Decimal), 2)
            cbill.ClaimPaid = Math.Round(CType(txtClaimPaid.Text, Decimal), 2)
            cbill.TransactionType = cmbTransType.SelectedValue

            cbill.PolicyNo = txtPolicyNo.Text
            cbill.ClaimNo = txtClaimNo.Text
            cbill.InsurerName = cmbInsurerName.SelectedValue
            cbill.BrokerName = cmbBrokerName.SelectedValue
            cbill.LossDate = ValidDate(txtLossDate.Text)

            cbRepo.Save(cbill)
            Session("cbill") = cbill
        Else
            cbRepo = CType(Session("cbRepo"), InsuranceClaimRepository)
            cbill = CType(Session("cbill"), InsuranceClaim)

            cbill.BranchCode = ddlBraNum.SelectedValue.ToString()
            cbill.Department = ddlDeptNum.SelectedValue.ToString()
            cbill.TransDescription = txtTransDescr.Text
            cbill.TransDate = ValidDate(txtTransDate.Text)
            cbill.TransactionType = cmbTransType.SelectedValue

            cbill.TransDate = ValidDate(txtTransDate.Text)
            cbill.ClaimRequested = Math.Round(CType(txtClaimRequested.Text, Decimal), 2)
            cbill.ClaimPaid = Math.Round(CType(txtClaimPaid.Text, Decimal), 2)
            cbill.TransactionType = cmbTransType.SelectedValue

            cbill.PolicyNo = txtPolicyNo.Text
            cbill.ClaimNo = txtClaimNo.Text
            cbill.InsurerName = cmbInsurerName.SelectedValue
            cbill.BrokerName = cmbBrokerName.SelectedValue
            cbill.LossDate = ValidDate(txtLossDate.Text)
            cbRepo.Save(cbill)

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
        txtClaimRequested.Text = String.Empty
        txtClaimPaid.Text = String.Empty
        txtPolicyNo.Text = String.Empty
        txtInsurerName.Text = String.Empty
        txtClaimNo.Text = String.Empty
        txtLossDate.Text = String.Empty
        txtBrokerName.Text = String.Empty
        cmbInsurerName.SelectedIndex = 0
        cmbBrokerName.SelectedIndex = 0


        updateFlag = False 'Switches to first time load
        Session("updateFlag") = updateFlag

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
        cbRepo = CType(Session("cbRepo"), InsuranceClaimRepository)
        cbill = CType(Session("cbill"), InsuranceClaim)

        cbill = cbRepo.GetById(strKey)

        If cbill IsNot Nothing Then
            With cbill
                txtTransDate.Text = ValidDateFromDB(.TransDate)
                txtTransDescr.Text = .TransDescription

                ddlBraNum.SelectedValue = cbill.BranchCode
                ddlDeptNum.SelectedValue = cbill.Department
                txtTransNum.Text = cbill.TransNo
                cmbTransType.SelectedValue = .TransactionType

                txtClaimRequested.Text = Math.Round(cbill.ClaimRequested, 2)
                txtClaimPaid.Text = Math.Round(cbill.ClaimPaid, 2)

                txtPolicyNo.Text = cbill.PolicyNo
                txtClaimNo.Text = cbill.ClaimNo
                cmbInsurerName.SelectedValue = cbill.InsurerName
                cmbBrokerName.SelectedValue = cbill.BrokerName
                txtLossDate.Text = ValidDateFromDB(cbill.LossDate)

                Session("cbill") = cbill
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

    Protected Sub cmdDel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdDel.Click, cmdDelN.Click
        Dim msg As String = String.Empty
        cbRepo = CType(Session("cbRepo"), InsuranceClaimRepository)
        cbill = CType(Session("cbill"), InsuranceClaim)
        Try
            cbRepo.Delete(cbill)
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

    Protected Sub cmdNew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdNew.Click
        initializeFields()
        txtTransNum.Text = String.Empty
    End Sub
    <System.Web.Services.WebMethod()> _
Public Shared Function GetPolicyInformation(ByVal _polnum As String) As String
        Dim polinfos As String = String.Empty
        Dim recRepo As New InsuranceClaimRepository()
        'Dim crit As String = 

        Try
            polinfos = recRepo.GetPolicyInfo(_polnum)
            Return polinfos
        Finally
            If polinfos = "<NewDataSet />" Then
                Throw New Exception()
            End If
        End Try

    End Function

    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim lblPrice As Label = CType(e.Row.FindControl("lblClaimPaid"), Label)
            ClaimPaid = (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ClaimPaid")))
            TotClaimPaid = (TotClaimPaid + ClaimPaid)

            Dim lblClmReq As Label = CType(e.Row.FindControl("lblClaimReq"), Label)
            ClaimPaid = (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ClaimRequested")))
            TotClaimRequested = (TotClaimRequested + ClaimRequested)

        End If
        If (e.Row.RowType = DataControlRowType.Footer) Then
            Dim lblTotal As Label = CType(e.Row.FindControl("lbltxtTotal"), Label)
            lblTotal.Text = Math.Round(TotClaimPaid, 2).ToString
        End If
        If (e.Row.RowType = DataControlRowType.Footer) Then
            Dim lblTotal As Label = CType(e.Row.FindControl("lbltxtTotalReq"), Label)
            lblTotal.Text = Math.Round(TotClaimRequested, 2).ToString
        End If

        'format fields
        Dim ea As GridViewRowEventArgs = CType(e, GridViewRowEventArgs)
        If (ea.Row.RowType = DataControlRowType.DataRow) Then
            Dim drv As InsuranceClaim = CType(ea.Row.DataItem, InsuranceClaim)
            If Not Convert.IsDBNull(drv.ClaimRequested) Then
                Dim iParsedValue As Decimal = 0
                If Decimal.TryParse(drv.ClaimRequested.ToString, iParsedValue) Then
                    Dim cell As TableCell = ea.Row.Cells(6)
                    cell.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N}", New Object() {iParsedValue})
                End If
            End If
            If Not Convert.IsDBNull(drv.ClaimPaid) Then
                Dim iParsedValue As Decimal = 0
                If Decimal.TryParse(drv.ClaimPaid.ToString, iParsedValue) Then
                    Dim cell As TableCell = ea.Row.Cells(7)
                    cell.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N}", New Object() {iParsedValue})
                End If
            End If
        End If

    End Sub

    Protected Sub grdData_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles grdData.SelectedIndexChanged

    End Sub
End Class