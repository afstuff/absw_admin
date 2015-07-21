Public Partial Class AdminMnu
    Inherits System.Web.UI.Page

    Protected strCopyRight As String
    Protected dteMydate As String

    Private Sub AdminMnu_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dteMydate = Format(Now, "dddd, dd-MMMM-yyyy")
        strCopyRight = "Copyright &copy;" & Year(Now) & " " & ". All rights reserved."

    End Sub
End Class