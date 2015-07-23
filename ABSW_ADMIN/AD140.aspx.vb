Imports System.Data
Imports CustodianAdmin.Data
Imports CustodianAdmin.Model
Imports CustodianAdmin.Repositories

Partial Public Class AD140
    Inherits System.Web.UI.Page

    Protected FirstMsg As String
    Protected PageLinks As String

    Protected strPOP_UP As String
    Dim rbRepo As RepairsBillRepository
    Dim acRepo As AdminCodeRepository
    Dim rbill As RepairsBill
    Dim updateFlag As Boolean
    Dim strKey As String
    Dim TotTransAmt As Decimal = 0
    Dim TransAmt As Decimal = 0



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'SessionProvider.RebuildSchema()
        'ddlBraNum.Attributes.Add("disabled", "disabled")
        ddlDeptNum.Attributes.Add("disabled", "disabled")
        txtTransNum.Attributes.Add("disabled", "disabled")



        If Not Page.IsPostBack Then
            rbRepo = New RepairsBillRepository
            acRepo = New AdminCodeRepository

            Session("rbRepo") = rbRepo
            Session("acRepo") = acRepo

            updateFlag = False
            Session("updateFlag") = updateFlag
            strKey = Request.QueryString("idd")
            Session("strKey") = strKey

            SetComboBinding(ddlServiceComp, rbRepo.GetAdminCodes("002"), "ItemDesc", "ItemCode")
            'SetComboBinding(ddlTransType, rbRepo.GetAdminCodes("003"), "ItemDesc", "ItemCode")

            SetComboBinding(ddlBraNum, acRepo.GetAdminCodes("009"), "ItemDesc", "ItemCode")
            SetComboBinding(ddlDeptNum, acRepo.GetAdminCodes("008"), "ItemDesc", "ItemCode")

            SetComboBinding(ddlTransClass, acRepo.GetAdminCodes("010"), "ItemDesc", "ItemCode")
            SetComboBinding(ddlTransID, acRepo.GetAdminCodes("011"), "ItemDesc", "ItemCode")


            If strKey IsNot Nothing Then
                FillValues()
            Else
                rbRepo = CType(Session("rbRepo"), RepairsBillRepository)
            End If

        Else 'post back

            rbRepo = CType(Session("rbRepo"), RepairsBillRepository)
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
            rbill = New RepairsBill
            txtTransNum.Text = rbRepo.GetNextSerialNumber("L01", "002", "01", Date.Now.Year.ToString(), "", "01", "01")

            'lblError.Visible = False
            rbill.TransClass = ddlTransClass.SelectedValue.ToString()
            rbill.TransId = ddlTransID.SelectedValue.ToString()

            rbill.BranchCode = ddlBraNum.SelectedValue.ToString()
            rbill.Department = ddlDeptNum.SelectedValue.ToString()
            rbill.Description = txtTransDescr.Text
            rbill.EntryDate = Date.Now
            rbill.EntryFlag = "A"
            rbill.OperatorId = "001"
            rbill.TransNo = Trim(txtTransNum.Text)
            rbill.TransDate = ValidDate(txtTransDate.Text)
            rbill.TransAmount = Math.Round(CType(txtTransAmt.Text, Decimal), 2)
            rbill.ServiceCoy = txtServiceComp.Text.Trim
            rbill.ServiceHrs = 0 'CType(txtServiceHrs.Text, Integer)

            rbRepo.Save(rbill)
            Session("rbill") = rbill
        Else
            rbRepo = CType(Session("rbRepo"), RepairsBillRepository)
            rbill = CType(Session("rbill"), RepairsBill)

            rbill.TransClass = ddlTransClass.SelectedValue.ToString()
            rbill.TransId = ddlTransID.SelectedValue.ToString()

            rbill.BranchCode = ddlBraNum.SelectedValue.ToString()
            rbill.Department = ddlDeptNum.SelectedValue.ToString()
            rbill.Description = txtTransDescr.Text
            rbill.TransDate = ValidDate(txtTransDate.Text)
            rbill.TransAmount = Math.Round(CType(txtTransAmt.Text, Decimal), 2)
            rbill.ServiceCoy = txtServiceComp.Text.Trim
            rbill.ServiceHrs = 0 ' CType(txtServiceHrs.Text, Integer)

            rbRepo.Save(rbill)

        End If
        grdData.DataBind()
        initializeFields()




    End Sub
    Private Sub initializeFields()
        txtBraNum.Text = String.Empty
        txtDeptName.Text = String.Empty
        txtDeptNum.Text = String.Empty
        txtTransAmt.Text = String.Empty
        txtTransDate.Text = String.Empty
        txtTransDescr.Text = String.Empty
        '        txtTransNum.Text = String.Empty
        ddlBraNum.SelectedIndex = 0
        ddlDeptNum.SelectedIndex = 0
        ddlServiceComp.SelectedIndex = 0
        txtServiceHrs.Text = String.Empty
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
        rbRepo = CType(Session("rbRepo"), RepairsBillRepository)
        rbill = CType(Session("rbill"), RepairsBill)

        rbill = rbRepo.GetById(strKey)

        If rbill IsNot Nothing Then
            With rbill
                txtTransDate.Text = ValidDateFromDB(.TransDate)
                txtTransDescr.Text = .Description

                ddlBraNum.SelectedValue = rbill.BranchCode
                ddlDeptNum.SelectedValue = rbill.Department
                txtTransNum.Text = rbill.TransNo
                ddlTransClass.SelectedValue = rbill.TransClass
                ddlTransID.SelectedValue = rbill.TransId
                txtTransAmt.Text = Math.Round(.TransAmount, 2)
                txtServiceHrs.Text = .ServiceHrs
                txtServiceComp.Text = .ServiceCoy
                ddlServiceComp.SelectedValue = .ServiceCoy
                Session("rbill") = rbill
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
        rbRepo = CType(Session("rbRepo"), RepairsBillRepository)
        rbill = CType(Session("rbill"), RepairsBill)
        Try
            rbRepo.Delete(rbill)
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
        updateFlag = False 'Switches to first time load
        Session("updateFlag") = updateFlag

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
            Dim drv As RepairsBill = CType(ea.Row.DataItem, RepairsBill)
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
        SetComboBinding(ddlServiceComp, acRepo.GetAdminCodes("002", txtServiceComp_Search.Text.Trim), "ItemDesc", "ItemCode")

    End Sub
    Protected Sub DoProc_Company_Change()
        txtServiceComp.Text = ddlServiceComp.SelectedValue

    End Sub

    Private Sub cmbServiceComp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlServiceComp.SelectedIndexChanged
        DoProc_Company_Change()
    End Sub

    Protected Sub ddlTransClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlTransClass.SelectedIndexChanged
        SetComboBinding(ddlTransID, acRepo.GetAdminOtherCodes("011", ddlTransClass.SelectedValue), "ItemDesc", "ItemCode")

    End Sub

    Protected Sub DoProc_Company_Search(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCompany_Search.Click

    End Sub

End Class