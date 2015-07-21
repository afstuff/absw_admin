Imports System.Data
Imports CustodianAdmin.Data
Imports CustodianAdmin.Model
Imports CustodianAdmin.Repositories

Partial Public Class AD110
    Inherits System.Web.UI.Page

    Protected FirstMsg As String
    Protected PageLinks As String

    Protected strPOP_UP As String
    Dim tbRepo As TelephoneBillRepository
    Dim acRepo As AdminCodeRepository
    Dim tbill As TelephoneBill
    Dim updateFlag As Boolean
    Dim strKey As String
    Dim TotTransAmt As Decimal = 0
    Dim TransAmt As Decimal = 0


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'SessionProvider.RebuildSchema()
        txtTelUsersName.Attributes.Add("disabled", "disabled")
        ddlBraNum.Attributes.Add("disabled", "disabled")
        ddlDeptNum.Attributes.Add("disabled", "disabled")

        If Not Page.IsPostBack Then
            tbRepo = New TelephoneBillRepository
            acRepo = New AdminCodeRepository

            Session("tbRepo") = tbRepo
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
                tbRepo = CType(Session("tbRepo"), TelephoneBillRepository)
                acRepo = CType(Session("acRepo"), AdminCodeRepository)
            End If

        Else 'post back

            acRepo = CType(Session("acRepo"), AdminCodeRepository)
            tbRepo = CType(Session("tbRepo"), TelephoneBillRepository)

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
            tbill = New TelephoneBill

            'lblError.Visible = False
            tbill.TransClass = ddlTransClass.SelectedValue.ToString()
            tbill.TransId = ddlTransID.SelectedValue.ToString()
            tbill.BranchCode = ddlBraNum.SelectedValue.ToString()
            tbill.Department = ddlDeptNum.SelectedValue.ToString()
            tbill.UserName = txtTelUsersName.Text
            tbill.Description = txtTransDescr.Text
            tbill.EntryDate = Date.Now
            tbill.EntryFlag = "A"
            tbill.OperatorId = "001"
            tbill.TelephoneNo = txtTransNum.Text
            tbill.TransDate = ValidDate(txtTransDate.Text)
            tbill.TransAmount = Math.Round(CType(txtTransAmt.Text, Decimal), 2)


            tbRepo.Save(tbill)
            Session("tbill") = tbill
        Else
            tbRepo = CType(Session("tbRepo"), TelephoneBillRepository)
            tbill = CType(Session("tbill"), TelephoneBill)

            tbill.TransClass = ddlTransClass.SelectedValue.ToString()
            tbill.TransId = ddlTransID.SelectedValue.ToString()
            tbill.BranchCode = ddlBraNum.SelectedValue.ToString()
            tbill.Department = ddlDeptNum.SelectedValue.ToString()
            tbill.UserName = txtTelUsersName.Text
            tbill.Description = txtTransDescr.Text
            ' tbill.EntryDate = Date.Now
            'tbill.EntryFlag = "A"
            'tbill.OperatorId = "001"
            tbill.TelephoneNo = txtTransNum.Text
            tbill.TransDate = ValidDate(txtTransDate.Text)
            tbill.TransAmount = Math.Round(CType(txtTransAmt.Text, Decimal), 2)
            tbRepo.Save(tbill)

        End If
        grdData.DataBind()
        initializeFields()


    End Sub
    Private Sub initializeFields()
        txtBraName.Text = String.Empty
        txtBraNum.Text = String.Empty
        txtDeptName.Text = String.Empty
        txtDeptNum.Text = String.Empty
        txtTransAmt.Text = String.Empty
        txtTransDate.Text = String.Empty
        txtTelUsersName.Text = String.Empty
        txtTransDescr.Text = String.Empty
        txtTransNum.Text = String.Empty
        ddlBraNum.SelectedIndex = 0
        ddlDeptNum.SelectedIndex = 0
        ddlTransClass.SelectedIndex = 0
        ddlTransID.SelectedIndex = 0

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
        tbRepo = CType(Session("tbRepo"), TelephoneBillRepository)
        tbill = CType(Session("tbill"), TelephoneBill)

        tbill = tbRepo.GetById(strKey)

        If tbill IsNot Nothing Then
            With tbill
                txtTelUsersName.Text = .UserName
                txtTransDate.Text = ValidDateFromDB(.TransDate)
                txtTransDescr.Text = .Description
                ddlTransClass.SelectedValue = tbill.TransClass
                ddlTransID.SelectedValue = tbill.TransId
                ddlBraNum.SelectedValue = tbill.BranchCode
                ddlDeptNum.SelectedValue = tbill.Department
                txtTransNum.Text = tbill.TelephoneNo
                txtTransAmt.Text = Math.Round(.TransAmount, 2)
                Session("tbill") = tbill
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
        tbRepo = CType(Session("tbRepo"), TelephoneBillRepository)
        tbill = CType(Session("tbill"), TelephoneBill)
        Try
            tbRepo.Delete(tbill)
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

    <System.Web.Services.WebMethod()> _
Public Shared Function GetMiscAdminInfo(ByVal _clascod As String, ByVal _itmcode As String) As String
        Dim codeinfo As String = String.Empty
        Dim admRepo As New AdminCodeRepository()
        'Dim crit As String = 

        Try
            codeinfo = admRepo.GetMiscAdminInfo(_clascod, _itmcode)
            Return codeinfo
        Finally
            If codeinfo = "<NewDataSet />" Then
                Throw New Exception()
            End If
        End Try

    End Function

    Protected Sub cmdPrint_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdPrint.Click

    End Sub

    Protected Sub txtTransNum_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtTransNum.TextChanged

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
            Dim drv As TelephoneBill = CType(ea.Row.DataItem, TelephoneBill)
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