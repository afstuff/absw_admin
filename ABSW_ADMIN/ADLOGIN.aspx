<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ADLOGIN.aspx.vb" Inherits="ABSW_ADMIN.ADLOGIN" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Login</title>
    <link href="StyleAdmin.css" rel="Stylesheet" type="text/css" />
</head>

<body style="background-color: White;">
    <form id="Form1" name="Form1" runat="server">
    
    <div class="div_container_login">

            <div class="div_bann_00">
                &nbsp;ABS Administration Manager&nbsp;
            </div>

            <!-- banner -->
            <div class="div_bann_01">
                    <img id="Img1"  alt="Logo" src="Images/CAILogoN.jpg" class="img_bann_01" runat="server" />
                    <img id="Img2" alt="" src="Images/caibanner.jpg" class="img_bann_02" runat="server" />
            </div>
            <div class="div_bann_02">
            	    &nbsp;Custodian and Allied Insurance Limited &nbsp;
            </div>

<table align="center" border="0" class="tbl_login">
	<tr>
	   <td valign="top">
		    <table align="center" border="0" style="width:100%;">
                <tr>
                    <td valign="top" colspan="1" style="height: 100%;">
                        <!-- Login page -->
                        <table align="center" border="0" style="width:100%;" >
                            <tr>
                                <td align="left" colspan="2" valign="top" class="td_login_header">&nbsp;CAI Admin Login</td>
           	   	            </tr>

           	                <tr>
                                <td colspan="2" valign="top"></td>
           	   	            </tr>
                            <tr>
                                <td align="right" colspan="1" valign="top"><asp:Label ID="lblUserID" Text="User ID:" runat="server"></asp:Label>&nbsp;</td>
                                <td align="left" colspan="1" valign="top"><asp:TextBox ID="txtUserID" EnableViewState="true" Width="120px" AutoPostBack="false" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="right" colspan="1" valign="top"><asp:Label ID="lblUserPass" Text="Password:" runat="server"></asp:Label>&nbsp;</td>
                                <td align="left" colspan="1" valign="top"><asp:TextBox ID="txtUserPass" TextMode="Password" Width="120px" AutoCompleteType="None" runat="server" /></td>
                            </tr>

                		    <tr>
    	                        <td nowrap align="right" valign="top"><asp:Label ID="lblUserName" Text="User Name:" Enabled="false" runat="server"></asp:Label>&nbsp;</td>
    		                    <td align="left" colspan="1" valign="top"><asp:textbox id="txtUserName" Width="250px" Font-Bold="true" Enabled="false" 
                                    runat="server" EnableViewState="true"></asp:textbox></td>
                  	        </tr>

            	            <tr>
    	                        <td align="left" nowrap colspan="2" valign="top"><asp:Label id="lblMessage" runat="server" Font-Size="Small" ForeColor="Red" Font-Bold="True"></asp:Label></td>
            	           	</tr>
           	                <tr>
                                <td colspan="2" valign="top"><hr /></td>
           	   	            </tr>
                      	   	<tr>
               	                <td align="center" colspan="2" valign="top">
    	   	                        <asp:button id="LoginBtn" Font-Bold="false" Font-Size="Large" Width="100px" runat="server" text="Login..."></asp:button>
    	   	                        &nbsp;&nbsp;<asp:button id="CancelBtn" Font-Bold="false" Font-Size="Large" Width="100px" runat="server" text="Exit..."  OnClientClick="javascript:window.close();"></asp:button>
    	   	                    </td>
                           </tr>
           	                <tr>
                                <td colspan="2" valign="top"><hr /></td>
           	   	            </tr>
    	   	               <tr>
                                <td align="left" colspan="2" valign="top" class="td_login_header">&nbsp;Copyright © - All rights reserved.</td>
    	   	               </tr>
                        </table>
                                
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
