Imports System.Data
Imports CustodianAdmin.Data
Imports CustodianAdmin.Model
Imports CustodianAdmin.Repositories

Partial Public Class AD130
    Inherits System.Web.UI.Page

    Protected FirstMsg As String
    Protected PageLinks As String

    Protected strPOP_UP As String
    Dim vmRepo As VehicleMaintRepository
    Dim acRepo As AdminCodeRepository
    Dim vmbill As VehicleMaintenance
    Dim updateFlag As Boolean
    Dim strKey As String
    Dim TotTransAmt As Decimal = 0
    Dim TransAmt As Decimal = 0




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'SessionProvider.RebuildSchema()
        'txtTransNum.Attributes.Add("disabled", "disabled")
        ddlBraNum.Attributes.Add("disabled", "disabled")
        ddlDeptNum.Attributes.Add("disabled", "disabled")
        txtServiceComp.Attributes.Add("disabled", "disabled")
        txtUserName.Attributes.Add("disabled", "disabled")


        If Not Page.IsPostBack Then
            vmRepo = New VehicleMaintRepository
            acRepo = New AdminCodeRepository

            Session("vmRepo") = vmRepo
            Session("acRepo") = acRepo
            updateFlag = False
            Session("updateFlag") = updateFlag
            strKey = Request.QueryString("idd")
            Session("strKey") = strKey

            SetComboBinding(cmbVehicle, acRepo.GetAdminCodes("001"), "ItemDesc", "ItemCode")
            SetComboBinding(cmbServiceComp, acRepo.GetAdminCodes("002"), "ItemDesc", "ItemCode")
            SetComboBinding(ddlBraNum, acRepo.GetAdminCodes("009"), "ItemDesc", "ItemCode")
            SetComboBinding(ddlDeptNum, acRepo.GetAdminCodes("008"), "ItemDesc", "ItemCode")

            SetComboBinding(ddlTransClass, acRepo.GetAdminCodes("010"), "ItemDesc", "ItemCode")
            SetComboBinding(ddlTransID, acRepo.GetAdminCodes("011"), "ItemDesc", "ItemCode")

            If strKey IsNot Nothing Then
                FillValues()
            Else
                vmRepo = CType(Session("vmRepo"), VehicleMaintRepository)
            End If

        Else 'post back

            vmRepo = CType(Session("vmRepo"), VehicleMaintRepository)
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
            vmbill = New VehicleMaintenance

            'lblError.Visible = False
            vmbill.TransClass = ddlTransClass.SelectedValue.ToString()
            vmbill.TransId = ddlTransID.SelectedValue.ToString()

            vmbill.BranchCode = ddlBraNum.SelectedValue.ToString()
            vmbill.Department = ddlDeptNum.SelectedValue.ToString()
            vmbill.UserName = txtUserName.Text
            vmbill.Description = txtTransDescr.Text
            vmbill.EntryDate = Date.Now
            vmbill.EntryFlag = "A"
            vmbill.OperatorId = "001"
            vmbill.VehicleNo = txtTransNum.Text
            vmbill.TransDate = ValidDate(txtTransDate.Text)
            vmbill.TransAmount = Math.Round(CType(txtTransAmt.Text, Decimal), 2)

            vmbill.VehicleType = txtVehicleType.Text
            vmbill.ServiceCompany = txtServiceComp.Text
            vmbill.MaintenanceType = txtMaintType.Text

            vmRepo.Save(vmbill)
            Session("vmbill") = vmbill
        Else
            vmRepo = CType(Session("vmRepo"), VehicleMaintRepository)
            vmbill = CType(Session("vmbill"), VehicleMaintenance)

            vmbill.TransClass = ddlTransClass.SelectedValue.ToString()
            vmbill.TransId = ddlTransID.SelectedValue.ToString()
            vmbill.BranchCode = ddlBraNum.SelectedValue.ToString()
            vmbill.Department = ddlDeptNum.SelectedValue.ToString()
            vmbill.UserName = txtUserName.Text
            vmbill.Description = txtTransDescr.Text
            ' vmbill.EntryDate = Date.Now
            'vmbill.EntryFlag = "A"
            'vmbill.OperatorId = "001"
            'vmbill.VehicleNo = txtTransNum.Text
            vmbill.TransDate = ValidDate(txtTransDate.Text)
            vmbill.TransAmount = Math.Round(CType(txtTransAmt.Text, Decimal), 2)

            vmbill.VehicleType = txtVehicleType.Text
            vmbill.ServiceCompany = txtServiceComp.Text
            vmbill.MaintenanceType = txtMaintType.Text

            vmRepo.Save(vmbill)

        End If
        grdData.DataBind()
        initializeFields()




    End Sub
    Private Sub initializeFields()
        'txtBraName.Text = String.Empty
        txtBraNum.Text = String.Empty
        txtDeptName.Text = String.Empty
        txtDeptNum.Text = String.Empty
        txtTransAmt.Text = String.Empty
        txtTransDate.Text = String.Empty
        txtUserName.Text = String.Empty
        txtTransDescr.Text = String.Empty
        txtTransNum.Text = String.Empty
        ddlBraNum.SelectedIndex = 0
        ddlDeptNum.SelectedIndex = 0
        cmbVehicle.SelectedIndex = 0
        txtVehicleType.Text = String.Empty
        txtServiceComp.Text = String.Empty
        txtMaintType.Text = String.Empty
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
        vmRepo = CType(Session("vmRepo"), VehicleMaintRepository)
        vmbill = CType(Session("vmbill"), VehicleMaintenance)

        vmbill = vmRepo.GetById(strKey)

        If vmbill IsNot Nothing Then
            With vmbill
                ddlTransClass.SelectedValue = vmbill.TransClass
                ddlTransID.SelectedValue = vmbill.TransId

                txtUserName.Text = vmbill.UserName
                txtTransNum.Text = vmbill.VehicleNo
                cmbVehicle.SelectedValue = vmbill.VehicleNo

                txtVehicleType.Text = vmbill.VehicleType
                txtServiceComp.Text = vmbill.ServiceCompany
                'cmbServiceComp.Items.FindByText(txtServiceComp.Text.Trim).Selected = True
                cmbServiceComp.SelectedValue = vmbill.ServiceCompany
                txtMaintType.Text = vmbill.MaintenanceType

                txtTransDate.Text = ValidDateFromDB(.TransDate)
                txtTransDescr.Text = .Description

                ddlBraNum.SelectedValue = vmbill.BranchCode
                ddlDeptNum.SelectedValue = vmbill.Department
                txtTransAmt.Text = Math.Round(.TransAmount, 2)
                Session("vmbill") = vmbill
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

    Protected Sub cmdDelN_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdDel.Click, cmdDelN.Click
        Dim msg As String = String.Empty
        vmRepo = CType(Session("vmRepo"), VehicleMaintRepository)
        vmbill = CType(Session("vmbill"), VehicleMaintenance)
        Try
            vmRepo.Delete(vmbill)
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
Public Shared Function GetVehicleInfo(ByVal _vehicleno As String) As String
        Dim vehicleinfo As String = String.Empty
        Dim vehRepo As New VehicleMaintRepository()
        'Dim crit As String = 

        Try
            vehicleinfo = vehRepo.GetVehicleInfo(_vehicleno)
            Return vehicleinfo
        Finally
            If vehicleinfo = "<NewDataSet />" Then
                Throw New Exception()
            End If
        End Try

    End Function
    <System.Web.Services.WebMethod()> _
Public Shared Function GetVehicleAndOwners() As IList
        Dim carownerslist As IList = Nothing
        Dim vehRepo As New AdminCodeRepository()
        'Dim crit As String = 

        Try
            carownerslist = vehRepo.GetAdminCodes("001")
            Return carownerslist
        Finally
            If carownerslist Is Nothing Then
                Throw New Exception()
            End If
        End Try

    End Function

    Protected Sub DoProc_Company_Search()
        SetComboBinding(cmbServiceComp, acRepo.GetAdminCodes("002", txtServiceComp_Search.Text.Trim), "ItemDesc", "ItemCode")

    End Sub
    Protected Sub DoProc_Company_Change()
        txtServiceComp.Text = cmbServiceComp.SelectedValue

    End Sub

    Private Sub cmbServiceComp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbServiceComp.SelectedIndexChanged
        DoProc_Company_Change()
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
            Dim drv As VehicleMaintenance = CType(ea.Row.DataItem, VehicleMaintenance)
            If Not Convert.IsDBNull(drv.TransAmount) Then
                Dim iParsedValue As Decimal = 0
                If Decimal.TryParse(drv.TransAmount.ToString, iParsedValue) Then
                    Dim cell As TableCell = ea.Row.Cells(4)
                    cell.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N}", New Object() {iParsedValue})
                End If
            End If
        End If

    End Sub
    Protected Sub ddlTransClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlTransClass.SelectedIndexChanged
        SetComboBinding(ddlTransID, acRepo.GetAdminOtherCodes("011", ddlTransClass.SelectedValue), "ItemDesc", "ItemCode")

    End Sub
    Protected Sub grdData_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles grdData.SelectedIndexChanged

    End Sub
End Class