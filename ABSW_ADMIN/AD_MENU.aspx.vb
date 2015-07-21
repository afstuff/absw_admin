Public Partial Class AD_MENU
    Inherits System.Web.UI.Page

    Protected strPAGE_TITLE As String
    Protected strPAGE_MENU As String
    Protected strOPT As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            strOPT = CType(Request.QueryString("mopt"), String).ToString
        Catch ex As Exception
            strOPT = ""
        End Try

        Select Case UCase(strOPT)
            Case "TEL_BILL"
                strPAGE_TITLE = "Telephone Bill Menu"
                strPAGE_MENU = ""
                'strPAGE_MENU = strPAGE_MENU & "&nbsp; <a href='AD10HOME.aspx' target='fra_entry_01'>MAIN MENU</a>"
                strPAGE_MENU = strPAGE_MENU & "&nbsp; &nbsp;<a href='AD111.aspx?sopt=006' target='fra_entry_01'>Codes Setup</a>"
                strPAGE_MENU = strPAGE_MENU & "&nbsp; &nbsp;<a href='AD110.aspx' target='fra_entry_01'>Transaction</a>"
                strPAGE_MENU = strPAGE_MENU & "&nbsp; &nbsp;<a href='AD112.aspx' target='fra_entry_01'>Report</a>"
                strPAGE_MENU = strPAGE_MENU & "&nbsp; &nbsp;<a href='Default.aspx?mopt=tel_bill'>HOME PAGE</a>"
                'Me.fra_entry_01.InnerHtml = "home_pg.aspx"
            Case "NEPA_BILL"
                strPAGE_TITLE = "Eletricity Bill Menu"
                strPAGE_MENU = ""
                'strPAGE_MENU = strPAGE_MENU & "&nbsp; <a href='AD20HOME.aspx' target='fra_entry_01'>MAIN MENU</a>"
                strPAGE_MENU = strPAGE_MENU & "&nbsp; &nbsp;<a href='AD111.aspx?sopt=007' target='fra_entry_01'>Codes Setup</a>"
                strPAGE_MENU = strPAGE_MENU & "&nbsp; &nbsp;<a href='AD120.aspx' target='fra_entry_01'>Transaction</a>"
                strPAGE_MENU = strPAGE_MENU & "&nbsp; &nbsp;<a href='AD122.aspx?reptname=ADMIN_ELECTRICITY.rpt " & _
                "&param1=@RPT_ST_DATE" & _
                "&param2=@RPT_END_DATE" & _
                "&param3=@RPT_ST_BRANCH" & _
                "&param4=@RPT_END_BRANCH" & _
                "&param5=Electric'" & _
                " target='fra_entry_01'>Report</a>"
                strPAGE_MENU = strPAGE_MENU & "&nbsp; &nbsp;<a href='Default.aspx?mopt=nepa_bill'>HOME PAGE</a>"
                'Me.fra_entry_01.InnerHtml = "ad20home.aspx"
            Case "VEH"
                strPAGE_TITLE = "Car and Vehicle Maintenance Menu"
                strPAGE_MENU = ""
                'strPAGE_MENU = strPAGE_MENU & "&nbsp; <a href='AD30HOME.aspx' target='fra_entry_01'>MAIN MENU</a>"
                strPAGE_MENU = strPAGE_MENU & "&nbsp; &nbsp;<a href='AD111.aspx?sopt=001' target='fra_entry_01'>Codes Setup</a>"
                strPAGE_MENU = strPAGE_MENU & "&nbsp; &nbsp;<a href='AD130.aspx' target='fra_entry_01'>Transaction</a>"
                strPAGE_MENU = strPAGE_MENU & "&nbsp; &nbsp;<a href='AD132.aspx?reptname=ADMIN_VEH_MAINT_DETAILS.rpt' target='fra_entry_01'>Report</a>"
                strPAGE_MENU = strPAGE_MENU & "&nbsp; &nbsp;<a href='Default.aspx?mopt=veh'>HOME PAGE</a>"
                'Me.fra_entry_01.InnerHtml = "ad30home.aspx"
            Case "REPAIRS"
                strPAGE_TITLE = "Repairs and Services Menu"
                strPAGE_MENU = ""
                'strPAGE_MENU = strPAGE_MENU & "&nbsp; <a href='AD40HOME.aspx' target='fra_entry_01'>MAIN MENU</a>"
                strPAGE_MENU = strPAGE_MENU & "&nbsp; &nbsp;<a href='AD111.aspx?sopt=002' target='fra_entry_01'>Codes Setup</a>"
                strPAGE_MENU = strPAGE_MENU & "&nbsp; &nbsp;<a href='AD140.aspx' target='fra_entry_01'>Transaction</a>"
                strPAGE_MENU = strPAGE_MENU & "&nbsp; &nbsp;<a href='AD142.aspx?reptname=ADMIN_EQUIP_REPAIRS_LIST.rpt " & _
                "&param1=@RPT_ST_DATE" & _
                "&param2=@RPT_END_DATE" & _
                "&param3=@RPT_ST_BRANCH" & _
                "&param4=@RPT_END_BRANCH" & _
                "&param5=@RPT_ST_TYPE" & _
                "&param6=@RPT_END_TYPE'" & _
                "&param7=Equipment'" & _
                " target='fra_entry_01'>Report</a>"
                strPAGE_MENU = strPAGE_MENU & "&nbsp; &nbsp;<a href='Default.aspx?mopt=repairs'>HOME PAGE</a>"
                'Me.fra_entry_01.InnerHtml = "ad40home.aspx"
            Case "DIESEL"
                strPAGE_TITLE = "Diesel Procurement Menu"
                strPAGE_MENU = ""
                'strPAGE_MENU = strPAGE_MENU & "&nbsp; <a href='AD40HOME.aspx' target='fra_entry_01'>MAIN MENU</a>"
                strPAGE_MENU = strPAGE_MENU & "&nbsp; &nbsp;<a href='AD111.aspx?sopt=002' target='fra_entry_01'>Codes Setup</a>"
                strPAGE_MENU = strPAGE_MENU & "&nbsp; &nbsp;<a href='AD150.aspx' target='fra_entry_01'>Transaction</a>"
                strPAGE_MENU = strPAGE_MENU & "&nbsp; &nbsp;<a href='AD142.aspx?reptname=ADMIN_DEISEL_LIST.rpt" & _
                "&param1=@RPT_ST_DATE" & _
                "&param2=@RPT_END_DATE" & _
                "&param3=@RPT_ST_BRANCH" & _
                "&param4=@RPT_END_BRANCH" & _
                "&param5=@RPT_ST_TYPE" & _
                "&param6=@RPT_END_TYPE'" & _
                "&param7=Diesel'" & _
                " target='fra_entry_01'>Report</a>"
                strPAGE_MENU = strPAGE_MENU & "&nbsp; &nbsp;<a href='Default.aspx?mopt=diesel'>HOME PAGE</a>"
                'Me.fra_entry_01.InnerHtml = "ad40home.aspx"
            Case "OUTSOURCE"
                strPAGE_TITLE = "Outsourcing Services Menu"
                strPAGE_MENU = ""
                'strPAGE_MENU = strPAGE_MENU & "&nbsp; <a href='AD40HOME.aspx' target='fra_entry_01'>MAIN MENU</a>"
                strPAGE_MENU = strPAGE_MENU & "&nbsp; &nbsp;<a href='AD111.aspx?sopt=006' target='fra_entry_01'>Codes Setup</a>"
                strPAGE_MENU = strPAGE_MENU & "&nbsp; &nbsp;<a href='AD160.aspx' target='fra_entry_01'>Transaction</a>"
                strPAGE_MENU = strPAGE_MENU & "&nbsp; &nbsp;<a href='AD122.aspx?reptname=ADMIN_OUTSOURCE_LIST.rpt " & _
                "&param1=@RPT_ST_DATE" & _
                "&param2=@RPT_END_DATE" & _
                "&param3=@RPT_ST_BRANCH" & _
                "&param4=@RPT_END_BRANCH" & _
                "&param5=Outsource'" & _
                " target='fra_entry_01'>Report</a>"
                strPAGE_MENU = strPAGE_MENU & "&nbsp; &nbsp;<a href='Default.aspx?mopt=outsource'>HOME PAGE</a>"
                'Me.fra_entry_01.InnerHtml = "ad40home.aspx"
            Case "PREMIUM"
                strPAGE_TITLE = "Premium Paid Menu"
                strPAGE_MENU = ""
                'strPAGE_MENU = strPAGE_MENU & "&nbsp; <a href='AD40HOME.aspx' target='fra_entry_01'>MAIN MENU</a>"
                strPAGE_MENU = strPAGE_MENU & "&nbsp; &nbsp;<a href='AD111.aspx?sopt=005' target='fra_entry_01'>Codes Setup</a>"
                strPAGE_MENU = strPAGE_MENU & "&nbsp; &nbsp;<a href='AD170.aspx' target='fra_entry_01'>Transaction</a>"
                strPAGE_MENU = strPAGE_MENU & "&nbsp; &nbsp;<a href='AD122.aspx?reptname=ADMIN_INS_PREM_LIST.rpt " & _
                "&param1=@RPT_ST_DATE" & _
                "&param2=@RPT_END_DATE" & _
                "&param3=@RPT_ST_BRANCH" & _
                "&param4=@RPT_END_BRANCH" & _
                "&param5=Premium'" & _
                " target='fra_entry_01'>Report</a>"
                strPAGE_MENU = strPAGE_MENU & "&nbsp; &nbsp;<a href='Default.aspx?mopt=insprem'>HOME PAGE</a>"
                'Me.fra_entry_01.InnerHtml = "ad40home.aspx"
            Case "CLAIM"
                strPAGE_TITLE = "Claim Received Menu"
                strPAGE_MENU = ""
                'strPAGE_MENU = strPAGE_MENU & "&nbsp; <a href='AD40HOME.aspx' target='fra_entry_01'>MAIN MENU</a>"
                strPAGE_MENU = strPAGE_MENU & "&nbsp; &nbsp;<a href='AD111.aspx?sopt=005' target='fra_entry_01'>Codes Setup</a>"
                strPAGE_MENU = strPAGE_MENU & "&nbsp; &nbsp;<a href='AD180.aspx' target='fra_entry_01'>Transaction</a>"
                strPAGE_MENU = strPAGE_MENU & "&nbsp; &nbsp;<a href='AD122.aspx?reptname=ADMIN_INS_CLAIM_LIST.rpt " & _
                "&param1=@RPT_ST_DATE" & _
                "&param2=@RPT_END_DATE" & _
                "&param3=@RPT_ST_BRANCH" & _
                "&param4=@RPT_END_BRANCH" & _
                "&param5=Claim'" & _
                " target='fra_entry_01'>Report</a>"
                strPAGE_MENU = strPAGE_MENU & "&nbsp; &nbsp;<a href='Default.aspx?mopt=insclaim'>HOME PAGE</a>"
                'Me.fra_entry_01.InnerHtml = "ad40home.aspx"
            Case Else
                strPAGE_TITLE = "*** MENU ***"
                strPAGE_MENU = ""
                strPAGE_MENU = strPAGE_MENU & "&nbsp; <a href='Default.aspx'>HOME</a>"
        End Select
    End Sub


End Class