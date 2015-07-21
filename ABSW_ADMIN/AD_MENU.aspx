<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AD_MENU.aspx.vb" Inherits="ABSW_ADMIN.AD_MENU" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="StyleAdmin.css" />
</head>

<body style="background-color: White;">
    <form id="Form1" runat="server">
    <div class="div_container">
            <div class="div_bann_00">
                &nbsp;ABS Administration Manager&nbsp;
            </div>

            <!-- banner -->
            <div class="div_bann_01" style="display: none;">
                    <img id="Img1"  alt="Logo" src="Images/CAILogoN.jpg" class="img_bann_01" runat="server" />
                    <img id="Img2" alt="" src="Images/caibanner.jpg" class="img_bann_02" runat="server" />
            </div>
            <div class="div_bann_02">
            	    &nbsp;Custodian and Allied Insurance Limited &nbsp;
            </div>
            <div class="div_bann_03">
            	    &nbsp;<%=strPAGE_TITLE%>&nbsp;
            </div>
            <div align="right" style="display: none;">
                <asp:Label ID="lblAction" ForeColor="LightGray" Text="Status:" runat="server"></asp:Label>
                &nbsp;<asp:textbox id="txtAction" ForeColor="LightGray" Text="" Visible="true" runat="server" EnableViewState="False" Width="30px"></asp:textbox>
            </div>
            
            <div class="div_menu_02">
                    &nbsp;<%=strPAGE_MENU%>
            </div>

            <div class="div_content_01">
                <iframe id="fra_entry_01" name="fra_entry_01" class="fra_content_01" src="AD_HOME.aspx" frameborder="0" scrolling="auto" runat="server">
                </iframe>
            </div>
        
        </div>
        
    </form>
</body>
</html>
