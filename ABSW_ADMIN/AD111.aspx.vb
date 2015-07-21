Imports System.Data
Imports CustodianAdmin.Data
Imports CustodianAdmin.Model
Imports CustodianAdmin.Repositories

Partial Public Class AD111
    Inherits System.Web.UI.Page

    Protected FirstMsg As String
    Protected PageLinks As String

    Protected strPOP_UP As String
    Dim acRepo As AdminCodeRepository
    Dim aCode As AdminCode
    Dim updateFlag As Boolean
    Dim strKey As String
    Dim strOption As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'SessionProvider.RebuildSchema()

        If Not Page.IsPostBack Then
            acRepo = New AdminCodeRepository

            Session("acRepo") = acRepo
            updateFlag = False
            Session("updateFlag") = updateFlag
            strKey = Request.QueryString("idd")
            Session("strKey") = strKey

            strOption = Request.QueryString("sopt")
            Session("strOption") = strOption

            'initialize all the dropdown list values
            'the meter types 
            If strOption IsNot Nothing Then
                AdjustScreen()
            End If


            SetComboBinding(ddlBraNum, acRepo.GetAdminCodes("009"), "ItemDesc", "ItemCode")
            SetComboBinding(ddlDeptNum, acRepo.GetAdminCodes("008"), "ItemDesc", "ItemCode")
            SetComboBinding(ddlTransClass, acRepo.GetAdminCodes("010"), "ItemDesc", "ItemCode")
            'SetComboBinding(cmbCodeType, acRepo.GetCarMakes(), "Type", "mId")

            If strKey IsNot Nothing Then
                FillValues()
            Else
                acRepo = CType(Session("acRepo"), AdminCodeRepository)
            End If


        Else 'post back

            acRepo = CType(Session("acRepo"), AdminCodeRepository)

            strOption = CType(Session("strOption"), String)

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
    Protected Sub AdjustScreen()
        cmbCodeClass.SelectedValue = strOption
    End Sub
    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdSave.Click, cmdSaveN.Click
        updateFlag = CType(Session("updateFlag"), String)

        If Not updateFlag Then 'if new record
            aCode = New AdminCode

            'lblError.Visible = False
            If txtTransClass.Text = String.Empty Then
                aCode.Branch = ddlBraNum.SelectedValue.ToString()
            Else
                aCode.Branch = ddlTransClass.SelectedValue.ToString()
            End If
            aCode.Dept = ddlDeptNum.SelectedValue.ToString()
            aCode.ClassCode = cmbCodeClass.SelectedValue()
            aCode.ItemDesc = txtCodeDescr.Text
            aCode.ItemCode = txtCode.Text
            aCode.EntryDate = Date.Now
            aCode.Flag = "A"
            aCode.OperatorId = "001"
            aCode.TransType = cmbCodeType.SelectedValue()

            acRepo.Save(aCode)
            Session("aCode") = aCode
        Else
            acRepo = CType(Session("acRepo"), AdminCodeRepository)
            aCode = CType(Session("aCode"), AdminCode)

            aCode.ItemCode = txtCode.Text
            If txtTransClass.Text = String.Empty Then
                aCode.Branch = ddlBraNum.SelectedValue.ToString()
            Else
                aCode.Branch = ddlTransClass.SelectedValue.ToString()
            End If
            aCode.Dept = ddlDeptNum.SelectedValue.ToString()
            aCode.ClassCode = cmbCodeClass.SelectedValue()
            aCode.ItemDesc = txtCodeDescr.Text
            aCode.TransType = cmbCodeType.SelectedValue()
            acRepo.Save(aCode)

        End If
            grdData.DataBind()
            initializeFields()

    End Sub
    Private Sub initializeFields()
        txtBraName.Text = String.Empty
        txtBraNum.Text = String.Empty
        txtDeptName.Text = String.Empty
        txtDeptNum.Text = String.Empty
        ddlBraNum.SelectedIndex = 0
        ddlDeptNum.SelectedIndex = 0
        cmbCodeType.SelectedIndex = 0
        txtCode.Text = String.Empty
        txtCodeDescr.Text = String.Empty
        txtTransClass.Text = String.Empty
        txtTransclassName.Text = String.Empty



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
        acRepo = CType(Session("acRepo"), AdminCodeRepository)
        aCode = CType(Session("aCode"), AdminCode)

        aCode = acRepo.GetById(strKey)
        txtCodeClass.Text = CType(Session("cmbCodeClassValue"), String)
        If aCode IsNot Nothing Then
            With aCode
                If txtCodeClass.Text.Trim = "011" Then
                    ddlTransClass.SelectedValue = .Branch
                Else
                    ddlBraNum.SelectedValue = .Branch
                End If
                ddlDeptNum.SelectedValue = .Dept
                cmbCodeClass.SelectedValue = .ClassCode
                txtCodeDescr.Text = .ItemDesc
                cmbCodeType.SelectedValue = .TransType
                txtCode.Text = .ItemCode

                Session("aCode") = aCode
            End With

            updateFlag = True
            Session("updateFlag") = updateFlag
            grdData.DataBind()


        End If
    End Sub

    Protected Sub cmdDel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdDel.Click, cmdDelN.Click
        Dim msg As String = String.Empty
        acRepo = CType(Session("acRepo"), AdminCodeRepository)
        aCode = CType(Session("aCode"), AdminCode)
        Try
            acRepo.Delete(aCode)
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
Public Shared Function GetMeterTypeList() As ArrayList
        Dim lstArrMeterType As ArrayList = New ArrayList
        lstArrMeterType.Add(New ListItem("1-Phased-Prepaid", "1-Phased-Prepaid"))
        lstArrMeterType.Add(New ListItem("2-Phased-Prepaid", "2-Phased-Prepaid"))
        lstArrMeterType.Add(New ListItem("3-Phased-Prepaid", "3-Phased-Prepaid"))
        lstArrMeterType.Add(New ListItem("1-Phased", "1-Phased"))
        lstArrMeterType.Add(New ListItem("2-Phased", "2-Phased"))
        lstArrMeterType.Add(New ListItem("3-Phased", "3-Phased"))
        Return lstArrMeterType
    End Function
    <System.Web.Services.WebMethod()> _
Public Shared Function GetMotorTypesInfo() As IList
        Dim motortypelist As IList = Nothing
        Dim vehRepo As New AdminCodeRepository()
        'Dim crit As String = 

        Try
            motortypelist = vehRepo.GetCarMakes()
            Return motortypelist
        Finally
            If motortypelist Is Nothing Then
                Throw New Exception()
            End If
        End Try

    End Function
    <System.Web.Services.WebMethod()> _
Public Shared Function GetMiscAdminInfo(ByVal _classcode As String, ByVal _itemcode As String) As String
        Dim codeinfo As String = String.Empty
        Dim admRepo As New AdminCodeRepository()
        'Dim crit As String = 

        Try
            codeinfo = admRepo.GetMiscAdminInfo(_classcode, _itemcode)
            Return codeinfo
        Finally
            If codeinfo = "<NewDataSet />" Then
                Throw New Exception()
            End If
        End Try

    End Function

    Protected Sub ddlTransClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlTransClass.SelectedIndexChanged
        txtTransClass.Text = ddlTransClass.SelectedValue.ToString()

    End Sub

    Protected Sub cmbCodeClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbCodeClass.SelectedIndexChanged
        Session("cmbCodeClassValue") = cmbCodeClass.SelectedValue.ToString()
    End Sub
End Class