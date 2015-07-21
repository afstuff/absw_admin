<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ADRPT.aspx.vb" Inherits="ABSW_ADMIN.ADRPT" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Report</title>
    <link rel="stylesheet" type="text/css" href="StyleAdmin.css" />
    <script language="javascript" type="text/javascript" src="Script/ScriptJS.js">
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <br />
    <table align="center" border="0" cellpadding="2" cellspacing="2" width="90%"  style="border-width: thick;">
        <tr>
            <td colspan="2" valign="top" style="height: 600px;">
                <table align="center" width="100%">
                    <caption>Reports</caption>
                    <tr>
                        <td align="center">&nbsp;<a href="Welcome.aspx" style="font-weight:bold;">Return to Main Menu</a>&nbsp</td>
                    </tr>
                    <tr>
                        <td>&nbsp;<img width="15px" style="vertical-align:bottom;" src="Images/ballred.gif" alt="Image" />&nbsp;<a href="RPT110.aspx" target="fraDetails">View/Print Telephone Bill</a></td>
                    </tr>
                    <tr>
                        <td>&nbsp;<img width="15px" style="vertical-align:bottom;" src="Images/ballred.gif" alt="Image" />&nbsp;<a href="RPT120.aspx" target="fraDetails">View/Print Electricity Bill</a></td>
                    </tr>
                    <tr>
                        <td>&nbsp;<img width="15px" style="vertical-align:bottom;" src="Images/ballred.gif" alt="Image" />&nbsp;<a href="RPT130.aspx" target="fraDetails">View/Print Car or Vehicle Maintenance</a></td>
                    </tr>
                    <tr>
                        <td>&nbsp;<hr /></td>
                    </tr>

                    <tr>
                        <td>&nbsp;<img width="15px" style="vertical-align:bottom;" src="Images/ballred.gif" alt="Image" />&nbsp;<a href="RPT140.aspx" target="fraDetails">View/Print Repairs and Services</a></td>
                    </tr>
                    <tr>
                        <td>&nbsp;<img width="15px" style="vertical-align:bottom;" src="Images/ballred.gif" alt="Image" />&nbsp;<a href="RPT150.aspx" target="fraDetails">View/Print Diesel Record</a></td>
                    </tr>
                    <tr>
                        <td>&nbsp;<hr /></td>
                    </tr>
                    <tr>
                        <td>&nbsp;<img width="15px" style="vertical-align:bottom;" src="Images/ballred.gif" alt="Image" />&nbsp;<a href="RPT160.aspx" target="fraDetails">View/Print Outsourcing Record</a></td>
                    </tr>
                    <tr>
                        <td>&nbsp;<img width="15px" style="vertical-align:bottom;" src="Images/ballred.gif" alt="Image" />&nbsp;<a href="RPT170.aspx" target="fraDetails">View/Print Insurance Records - Premium and Claims</a></td>
                    </tr>

                </table>
            </td>
        </tr>        
    </table>
    
    </div>
    </form>
</body>
</html>
