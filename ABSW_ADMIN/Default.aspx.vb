Partial Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Me.txtAction.Text = "Log_Out" Then
            Call DoProc_LogOut()
            Me.txtAction.Text = ""
            Response.Redirect("ADLOGIN.aspx")
            Exit Sub
        End If

    End Sub

    Protected Sub DoProc_LogOut()

        Dim strSess As String = "STFID"
        Dim intC As Integer = 0
        Dim intCX As Integer = 0
        Dim MyArray(15) As String

        intC = 0
        intCX = 0
        Try
            'Session("STFID") = RTrim(Me.txtNum.Text)
            'Session("STFID") = RTrim("")

            'Session.Keys
            'Session.Count
            'LOOP THROUGH THE SESSION OBJECT
            '*******************************

            'For intC = 0 To Session.Count - 1
            'Response.Write("<br />" & "Item " & intC & " - Key: " & Session.Keys(intC).ToString & " - Value: : " & Session.Item(intC).ToString)
            '
            'Next

            'SAMPLE SESSION DATA
            '*******************
            ''Item 0 - Key: ActiveSess - Value: : 1
            ''Item 1 - Key: StartdDate - Value: : 06/14/2013 7:01:55 PM
            ''Item 2 - Key: connstr - Value: : Provider=SQLOLEDB;Data Source=ABS-PC;Initial Catalog=ABS;User ID=SA;Password=;
            ''Item 3 - Key: connstr_SQL - Value: : Data Source=ABS-PC;Initial Catalog=ABS;User ID=SA;Password=;
            ''Item 4 - Key: CL_COMP_NAME - Value: : CUSTODIAN AND ALLIED INSURANCE PLC
            ''Item(5 - Key) : MyUserIDX(-Value) : ADM()
            ''Item(6 - Key) : MyUserName(-Value) : System(Administrator)
            ''Item 7 - Key: MyUserLevelX - Value: : 0
            ''Item(8 - Key) : MyUserIDX_NIM(-Value) : ADM()
            ''Item(9 - Key) : MyUserName_NIM(-Value) : System(Administrator)
            ''Item 10 - Key: MyUserLevelX_NIM - Value: : 0
            ''Item 11 - Key: MyLogonTime - Value: : 06/14/2013 7:02:05 PM
            ''Item(12 - Key) : MyUserID(-Value) : ADM()


            'For Each strS As String In Session.Keys
            '    '    ' ...
            '    'Response.Write("<br /> Session ID: " & Session.SessionID & " - Key: " & strSess.ToString & " - Value: " & Session.Item(strSess).ToString)

            '    '    If UCase(strSess) = UCase("connstr") Or _
            '    '      UCase(strSess) = UCase("connstr_SQL") Or _
            '    '      UCase(strSess) = UCase("CL_COMP_NAME") Then
            '    '    Else
            '    '        'Session.Remove(strSess)
            '    '    End If
            '    strSess = strS
            '    Response.Write("<br />" & " - Key: " & strSess.ToString & " - Value: : " & Session.Item(strSess).ToString)
            'Next

            For intCX = 0 To Session.Count - 1

                strSess = Session.Keys(intCX).ToString

                If UCase(strSess) = UCase("connstr") Or _
                  UCase(strSess) = UCase("connstr_SQL") Or _
                  UCase(strSess) = UCase("CL_COMP_NAME") Or _
                  UCase(strSess) = UCase("ActiveSess") Or _
                  UCase(strSess) = UCase("StartdDate") Then
                Else
                    intC = intC + 1
                    MyArray(intC) = strSess
                    'Response.Write("<br />" & "Item " & intC & " - Key: " & strSess.ToString & " - Value: : " & Session.Item(strSess).ToString)

                End If

            Next

            'Response.Write("<br />" & "Item ubound(): " & UBound(MyArray).ToString)
            'Response.Write("<br />" & "Item Length: " & MyArray.Length)

            For intCX = 1 To intC

                strSess = MyArray(intCX).ToString

                Response.Write("<br />" & "Removing session Item " & intCX & " - Key: " & strSess.ToString & " - Value: : " & Session.Item(strSess).ToString)
                Session.Remove(strSess.ToString)
                'Session.Contents.Remove(strSess)

            Next

            'Session.Clear()

        Catch ex As Exception
            Response.Write("<br /> Error has Occured at key: " & strSess.ToString & " Reason: " & ex.Message.ToString)
            'Exit Try
        End Try


    End Sub

End Class