Imports System.Data
Imports CustodianAdmin.Data
Imports CustodianAdmin.Model
Imports CustodianAdmin.Repositories

Partial Public Class AD150
    Inherits System.Web.UI.Page

    Protected FirstMsg As String
    Protected PageLinks As String

    Protected strPOP_UP As String

    Dim dbRepo As DieselBillRepository
    Dim acRepo As AdminCodeRepository
    Dim dbill As DieselBill
    Dim updateFlag As Boolean
    Dim strKey As String
    Dim TotTransAmt As Decimal = 0
    Dim TransAmt As Decimal = 0



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'SessionProvider.RebuildSchema()
        txtTransNum.Attributes.Add("disabled", "disabled")
        'txtTransAmt1.Attributes.Add("disabled", "disabled")

        If Not Page.IsPostBack Then
            dbRepo = New DieselBillRepository
            acRepo = New AdminCodeRepository

            Session("dbRepo") = dbRepo
            Session("acRepo") = acRepo
            updateFlag = False
            Session("updateFlag") = updateFlag
            strKey = Request.QueryString("idd")
            Session("strKey") = strKey


            SetComboBinding(cmbServiceComp, acRepo.GetAdminCodes("002"), "ItemDesc", "ItemCode")
            SetComboBinding(ddlBraNum, acRepo.GetAdminCodes("009"), "ItemDesc", "ItemCode")
            SetComboBinding(ddlDeptNum, acRepo.GetAdminCodes("008"), "ItemDesc", "ItemCode")

            SetComboBinding(ddlTransClass, acRepo.GetAdminCodes("010"), "ItemDesc", "ItemCode")
            SetComboBinding(ddlTransID, acRepo.GetAdminCodes("011"), "ItemDesc", "ItemCode")


            If strKey IsNot Nothing Then
                FillValues()
            Else
                dbRepo = CType(Session("dbRepo"), DieselBillRepository)
                acRepo = CType(Session("acRepo"), AdminCodeRepository)
            End If

        Else 'post back

            dbRepo = CType(Session("dbRepo"), DieselBillRepository)
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
        Dim transAmt As Decimal = Convert.ToDecimal(txtTransAmt1.Text)
        If Not updateFlag Then 'if new record
            dbill = New DieselBill

            'lblError.Visible = False
            txtTransNum.Text = acRepo.GetNextSerialNumber("L01", "004", "01", Date.Now.Year.ToString(), "", "01", "01")

            dbill.TransClass = ddlTransClass.SelectedValue.ToString()
            dbill.TransId = ddlTransID.SelectedValue.ToString()
            dbill.BranchCode = ddlBraNum.SelectedValue.ToString()
            dbill.Department = ddlDeptNum.SelectedValue.ToString()
            dbill.TransDescription = txtTransDescr.Text
            dbill.EntryDate = Date.Now
            dbill.EntryFlag = "A"
            dbill.OperatorId = "001"
            dbill.TransNo = Trim(txtTransNum.Text)
            dbill.TransDate = ValidDate(txtTransDate.Text)
            dbill.TransAmount = Math.Round(CType(transAmt, Decimal), 2)


            dbill.UnitPrice = Math.Round(CType(txtTransRate.Text, Decimal), 2)
            dbill.TransactionType = cmbTransType.SelectedValue
            dbill.Quantity = Math.Round(CType(txtTransQty.Text, Decimal), 2)
            dbill.SupplyCompany = txtServiceComp.Text


            dbRepo.Save(dbill)
            Session("dbill") = dbill
        Else
            dbRepo = CType(Session("dbRepo"), DieselBillRepository)
            dbill = CType(Session("dbill"), DieselBill)


            dbill.TransClass = ddlTransClass.SelectedValue.ToString()
            dbill.TransId = ddlTransID.SelectedValue.ToString()
            dbill.BranchCode = ddlBraNum.SelectedValue.ToString()
            dbill.Department = ddlDeptNum.SelectedValue.ToString()
            dbill.TransDescription = txtTransDescr.Text
            dbill.TransDate = ValidDate(txtTransDate.Text)
            dbill.TransAmount = Math.Round(CType(txtTransAmt1.Text, Decimal), 2)

            dbill.UnitPrice = Math.Round(CType(txtTransRate.Text, Decimal), 2)
            dbill.TransactionType = cmbTransType.SelectedValue
            dbill.Quantity = Math.Round(CType(txtTransQty.Text, Decimal), 2)
            dbill.SupplyCompany = txtServiceComp.Text

            dbRepo.Save(dbill)

        End If
        grdData.DataBind()
        initializeFields()




    End Sub
    Private Sub initializeFields()
        txtBraNum.Text = String.Empty
        txtDeptName.Text = String.Empty
        txtDeptNum.Text = String.Empty
        txtTransAmt1.Text = String.Empty
        txtTransDate.Text = String.Empty
        txtTransDescr.Text = String.Empty
        txtTransNum.Text = String.Empty
        ddlBraNum.SelectedIndex = 0
        ddlDeptNum.SelectedIndex = 0
        txtServiceComp.Text = String.Empty
        txtTransQty.Text = String.Empty
        txtTransRate.Text = String.Empty
        cmbTransType.SelectedIndex = 0
        cmbServiceComp.SelectedIndex = 0
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
        dbRepo = CType(Session("dbRepo"), DieselBillRepository)
        dbill = CType(Session("dbill"), DieselBill)

        dbill = dbRepo.GetById(strKey)

        If dbill IsNot Nothing Then
            With dbill
                txtTransDate.Text = ValidDateFromDB(.TransDate)
                txtTransDescr.Text = .TransDescription

                ddlBraNum.SelectedValue = dbill.BranchCode
                ddlDeptNum.SelectedValue = dbill.Department
                ddlTransClass.SelectedValue = dbill.TransClass
                ddlTransID.SelectedValue = dbill.TransId
                txtTransNum.Text = dbill.TransNo
                txtTransAmt1.Text = Math.Round(.TransAmount, 2)
                txtTransRate.Text = Math.Round(.UnitPrice, 2)
                txtTransQty.Text = Math.Round(.Quantity, 2)
                cmbTransType.SelectedValue = .TransactionType
                cmbServiceComp.SelectedValue = .SupplyCompany
                txtServiceComp.Text = .SupplyCompany

                Session("dbill") = dbill
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
        updateFlag = False 'Switches to first time load
        Session("updateFlag") = updateFlag


    End Sub

    Protected Sub cmdDel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdDel.Click, cmdDelN.Click

        Dim msg As String = String.Empty
        dbRepo = CType(Session("dbRepo"), DieselBillRepository)
        dbill = CType(Session("dbill"), DieselBill)
        Try
            dbRepo.Delete(dbill)
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
            Dim drv As DieselBill = CType(ea.Row.DataItem, DieselBill)
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

    Protected Sub DoProc_Company_Search()
        SetComboBinding(cmbServiceComp, acRepo.GetAdminCodes("002", txtServiceComp_Search.Text.Trim), "ItemDesc", "ItemCode")

    End Sub
    Protected Sub DoProc_Company_Change()
        txtServiceComp.Text = cmbServiceComp.SelectedValue

    End Sub

    Private Sub cmbServiceComp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbServiceComp.SelectedIndexChanged
        DoProc_Company_Change()
    End Sub

    Protected Sub ddlTransClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlTransClass.SelectedIndexChanged
        SetComboBinding(ddlTransID, acRepo.GetAdminOtherCodes("011", ddlTransClass.SelectedValue), "ItemDesc", "ItemCode")

    End Sub

End Class