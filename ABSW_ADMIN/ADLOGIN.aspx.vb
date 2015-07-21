Public Partial Class ADLOGIN
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not (Page.IsPostBack) Then
            Me.txtUserID.Enabled = True
            Me.txtUserID.Focus()
        End If

    End Sub

    Protected Sub LoginBtn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LoginBtn.Click
        If CType(Request.QueryString("Goto"), String) <> "" Then
            Response.Redirect(Request.QueryString("Goto"))
        Else
            Response.Redirect("default.aspx")
        End If

    End Sub
End Class