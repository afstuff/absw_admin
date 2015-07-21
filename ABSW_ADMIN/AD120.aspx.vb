Imports System.Data
Imports System.Data.OleDb
Imports CustodianAdmin.Data
Imports CustodianAdmin.Model
Imports CustodianAdmin.Repositories

Partial Public Class AD120
    Inherits System.Web.UI.Page
    Protected FirstMsg As String
    Protected PageLinks As String
    Protected strPOP_UP As String
    Dim ebRepo As ElectricBillRepository
    Dim acRepo As AdminCodeRepository

    Dim ebill As ElectricityBill
    Dim updateFlag As Boolean
    Dim strKey As String
    Dim TotTransAmt As Decimal = 0
    Dim TransAmt As Decimal = 0



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' SessionProvider.RebuildSchema()
        ddlBraNum.Attributes.Add("disabled", "disabled")
        ddlDeptNum.Attributes.Add("disabled", "disabled")

        If Not Page.IsPostBack Then
            ebRepo = New ElectricBillRepository
            acRepo = New AdminCodeRepository

            Session("ebRepo") = ebRepo
            Session("acRepo") = acRepo

            updateFlag = False
            Session("updateFlag") = updateFlag
            strKey = Request.QueryString("idd")
            Session("strKey") = strKey


            SetComboBinding(ddlBraNum, acRepo.GetAdminCodes("009"), "ItemDesc", "ItemCode")
            SetComboBinding(ddlDeptNum, acRepo.GetAdminCodes("008"), "ItemDesc", "ItemCode")
            SetComboBinding(ddlTransClass, acRepo.GetAdminCodes("010"), "ItemDesc", "ItemCode")
            SetComboBinding(ddlTransID, acRepo.GetAdminCodes("011"), "ItemDesc", "ItemCode")

            If strKey IsNot Nothing Then
                FillValues()
            Else
                ebRepo = CType(Session("ebRepo"), ElectricBillRepository)
                acRepo = CType(Session("acRepo"), AdminCodeRepository)
            End If

        Else 'post back

            ebRepo = CType(Session("ebRepo"), ElectricBillRepository)
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
            ebill = New ElectricityBill

            'lblError.Visible = False
            ebill.TransClass = ddlTransClass.SelectedValue.ToString()
            ebill.TransId = ddlTransID.SelectedValue.ToString()

            ebill.BranchCode = ddlBraNum.SelectedValue.ToString()
            ebill.Department = ddlDeptNum.SelectedValue.ToString()
            ebill.MeterNo = txtTransNum.Text
            ebill.PeriodPaidFor = txtPeriodPaidFor.Text
            ebill.AccountNo = txtAccountNo.Text
            ebill.EntryDate = Date.Now
            ebill.EntryFlag = "A"
            ebill.OperatorId = "001"
            ebill.TransDate = ValidDate(txtTransDate.Text)
            ebill.TransAmount = Math.Round(CType(txtTransAmt.Text, Decimal), 2)


            ebRepo.Save(ebill)
            Session("ebill") = ebill
        Else
            ebRepo = CType(Session("ebRepo"), ElectricBillRepository)
            ebill = CType(Session("ebill"), ElectricityBill)

            ebill.TransClass = ddlTransClass.SelectedValue.ToString()
            ebill.TransId = ddlTransID.SelectedValue.ToString()

            ebill.BranchCode = ddlBraNum.SelectedValue.ToString()
            ebill.Department = ddlDeptNum.SelectedValue.ToString()

            'ebill.MeterNo = txtTransNum.Text
            ebill.PeriodPaidFor = txtPeriodPaidFor.Text
            ebill.AccountNo = txtAccountNo.Text
            ' ebill.EntryDate = Date.Now
            'ebill.EntryFlag = "A"
            'ebill.OperatorId = "001"
            ebill.TransDate = ValidDate(txtTransDate.Text)
            ebill.TransAmount = Math.Round(CType(txtTransAmt.Text, Decimal), 2)
            ebRepo.Save(ebill)
            Session("ebill") = ebill


        End If
        grdData.DataBind()
        initializeFields()

    End Sub
    Private Sub initializeFields()
        ddlBraNum.SelectedIndex = -1
        ddlDeptNum.SelectedIndex = -1
        txtTransNum.Text = String.Empty
        txtPeriodPaidFor.Text = String.Empty
        txtAccountNo.Text = String.Empty
        txtTransDate.Text = String.Empty
        txtTransAmt.Text = "0.00"
        ddlTransClass.SelectedIndex = 0
        ddlTransID.SelectedIndex = 0

    End Sub
    Private Sub SetComboBinding(ByVal toBind As ListControl, ByVal dataSource As Object, ByVal displayMember As String, ByVal valueMember As String)
        toBind.DataTextField = displayMember
        toBind.DataValueField = valueMember
        toBind.DataSource = dataSource
        toBind.DataBind()
    End Sub

    Private Sub FillValues()
        strKey = CType(Session("strKey"), String)
        ebRepo = CType(Session("ebRepo"), ElectricBillRepository)
        ebill = CType(Session("ebill"), ElectricityBill)

        ebill = ebRepo.GetById(strKey)

        If ebill IsNot Nothing Then
            With ebill
                ddlTransClass.SelectedValue = ebill.TransClass
                ddlTransID.SelectedValue = ebill.TransId
                ddlBraNum.SelectedValue = ebill.BranchCode
                ddlDeptNum.SelectedValue = ebill.Department
                txtTransNum.Text = ebill.MeterNo
                txtPeriodPaidFor.Text = ebill.PeriodPaidFor
                txtAccountNo.Text = ebill.AccountNo
                txtTransDate.Text = ValidDateFromDB(ebill.TransDate)
                txtTransAmt.Text = Math.Round(CType(ebill.TransAmount, Decimal), 2)
                Session("ebill") = ebill
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
        ebRepo = CType(Session("ebRepo"), ElectricBillRepository)
        ebill = CType(Session("ebill"), ElectricityBill)
        Try
            ebRepo.Delete(ebill)
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
        updateFlag = False 'Switches to first time load
        Session("updateFlag") = updateFlag

    End Sub

    Protected Sub cmdPrint_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdPrint.Click

    End Sub

    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim lblPrice As Label = CType(e.Row.FindControl("lblTransAmt"), Label)
            TransAmt = (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TransAmount")))
            TotTransAmt = (TotTransAmt + TransAmt)

        End If
        If (e.Row.RowType = DataControlRowType.Footer) Then
            Dim lblTotal As Label = CType(e.Row.FindControl("lbltxtTotal"), Label)
            lblTotal.Text = Math.Round(TotTransAmt, 2).ToString
        End If

        'format fields
        Dim ea As GridViewRowEventArgs = CType(e, GridViewRowEventArgs)
        If (ea.Row.RowType = DataControlRowType.DataRow) Then
            Dim drv As ElectricityBill = CType(ea.Row.DataItem, ElectricityBill)
            If Not Convert.IsDBNull(drv.TransAmount) Then
                Dim iParsedValue As Decimal = 0
                If Decimal.TryParse(drv.TransAmount.ToString, iParsedValue) Then
                    Dim cell As TableCell = ea.Row.Cells(4)
                    cell.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N}", New Object() {iParsedValue})
                End If
            End If
        End If

    End Sub

    Protected Sub grdData_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles grdData.SelectedIndexChanged

    End Sub

    Protected Sub ddlTransClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlTransClass.SelectedIndexChanged
        SetComboBinding(ddlTransID, acRepo.GetAdminOtherCodes("011", ddlTransClass.SelectedValue), "ItemDesc", "ItemCode")

    End Sub
End Class