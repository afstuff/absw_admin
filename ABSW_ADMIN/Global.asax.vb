Imports System.Web.SessionState

Public Class Global_asax
    Inherits System.Web.HttpApplication

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application is started
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started

        ' Change Session Timeout to 20 minutes (if you need to)
        'Session.Timeout = 20
        ' Set a Session Start Time

        '//Ensure that this page expires within 10 minutes...	
        '   Response.Expires = 10        
        '//...or before Jan 1, 2001, which ever comes first.
        '   Response.ExpiresAbsolute = "Jan 1, 2001 13:30:15";
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when an error occurs
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.

        'Member name    Description
        '-----------    -----------
        'Off            Session state is disabled. 
        'InProc         Session state is in process with an ASP.NET worker process. 
        'StateServer    Session state is using the out-of-process ASP.NET State Service to store state information. 
        'SQLServer      Session state is using an out-of-process SQL Server database to store state information. 
        'Custom         Session state is using a custom data store to store session-state information. 
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
    End Sub

End Class