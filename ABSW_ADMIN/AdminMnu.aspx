<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AdminMnu.aspx.vb" Inherits="ABSW_ADMIN.AdminMnu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ABS Administration Manager</title>
    <link rel="stylesheet" type="text/css" href="StyleAdmin.css" />
    <script language="javascript" type="text/javascript" src="Script/ScriptJS.js">
    </script>
</head>

<body>
    <form id="form1" runat="server">

    <div style=" border: 1px solid #c0c0c0; margin: 1px auto; width: 1000px;">
    <table border="0" width="100%">
        <tr>
            <td align="left" colspan="2">
                <table border="0" width="100%">
                    <tr>
                        <td align="left" colspan="2" style="font-size:small; font-weight:lighter">Date:&nbsp;<%= dteMydate %>&nbsp;</td>          
                    </tr>
                    <tr>
                        <td align="left" colspan="2"style="background-color:#f1f1f1; color: Black; height: 30px; font-weight:bold; TEXT-TRANSFORM:uppercase; vertical-align:baseline;">
                            Custodian and Allied Insurance
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </div>

    <div style=" border: 1px solid #c0c0c0; margin: 1px auto; height:550px; width: 1000px;">
    <table border="0" width="100%">
        <tr>
            <td align="left" colspan="2" valign="top">
                <table border="0" width="100%">
                    <tr>
                        <td align="left" style="background-color: #4682B4; color: White; font-size:large; height: 30px;">ABS Administration Manager Main Menu</td>
                    </tr>
                    <tr>
                        <td align="left">&nbsp;</td>
                    </tr>

                    <tr>
                        <td align="left">&nbsp;<img width="15px" style="vertical-align:bottom;" src="Images/ballred.gif" alt="Image" />&nbsp;<a href="javascript:jsDoPopNew_Full('AD110.aspx?popup=yes');">Telephone Bill</a></td>
                    </tr>
                    <tr>
                        <td align="left">&nbsp;<img width="15px" style="vertical-align:bottom;" src="Images/ballred.gif" alt="Image" />&nbsp;<a href="javascript:jsDoPopNew_Big('AD120.aspx?popup=yes')">Electricity Bill</a></td>
                    </tr>
                    <tr>
                        <td align="left">&nbsp;<img width="15px" style="vertical-align:bottom;" src="Images/ballred.gif" alt="Image" />&nbsp;<a href="javascript:jsDoPopNew_Big('AD130.aspx?popup=yes')">Car or Vehicle Maintenance</a></td>
                    </tr>
                    <tr>
                        <td align="left">&nbsp;<img width="15px" style="vertical-align:bottom;" src="Images/ballred.gif" alt="Image" />&nbsp;<a href="javascript:jsDoPopNew_Big('AD140.aspx?popup=yes')">Repairs and Services</a></td>
                    </tr>
                    <tr>
                        <td align="left">&nbsp;<img width="15px" style="vertical-align:bottom;" src="Images/ballred.gif" alt="Image" />&nbsp;<a href="javascript:jsDoPopNew_Big('AD150.aspx?popup=yes')">Diesel Record</a></td>
                    </tr>
                    <tr>
                        <td align="left">&nbsp;<hr /></td>
                    </tr>
                    <tr>
                        <td align="left">&nbsp;<img width="15px" style="vertical-align:bottom;" src="Images/ballred.gif" alt="Image" />&nbsp;<a href="javascript:jsDoPopNew_Big('AD160.aspx?popup=yes')">Outsourcing Record</a></td>
                    </tr>
                    <tr>
                        <td align="left">&nbsp;<img width="15px" style="vertical-align:bottom;" src="Images/ballred.gif" alt="Image" />&nbsp;<a href="javascript:jsDoPopNew_Big('AD170.aspx?popup=yes')">Insurance Records - Premium and Claims</a></td>
                    </tr>

                </table>
            </td>
        </tr>
    </table>
    </div>
    
    <div style=" border: 1px solid #c0c0c0; margin: 1px auto; width: 1000px;">
    <table border="0" width="100%">
        <tr>
            <td align="left" colspan="2" valign="bottom">
                <table border="0" width="100%">
                    <tr>
        	            <td align="left" valign="top" style="background-color: #4682B4; color: White; height: 30px;">
        	                <%=strCopyRight%>&nbsp;                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        
    </table>
    
    </div>
    </form>
    
</body>
</html>
