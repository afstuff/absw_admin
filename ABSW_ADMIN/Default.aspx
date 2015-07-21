<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="ABSW_ADMIN._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">    
    <title>Main Menu</title>     
    <link href="StyleAdmin.css" type="text/css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript" src="Script/ScriptJS.js"></script>

    <script language="javascript" type="text/javascript">

    // Note: all browsers that support XDM also support iframe contentWindow
    // var iframeWindow = document.getElementById("myframe").contentWindow;
    // iframeWindow.postMessage("A secret", "http://www.wrox.com");

    //  For example, the syntax for accessing the backgroundColor style of the body object in an iframe is: 
    // sColor = document.frames("sFrameName").document.body.style.backgroundColor;
    //  Other example
    // sBorderValue = document.all.oFrame.style.border;
    // var collAll = document.frames("IFrame1").document.all

        function MyLoad_XX(myurl) {
            var myobj_xx = document.getElementById("fra_content_01X");
            alert("Default Page: " + myobj_xx.src);
            // myobj_xx.src = "AD110.aspx";
             //myobj_xx.src = myurl;
             myobj_xx.src = "http://localhost49170/" + myurl;
             document.forms["Form1"].submit();

        }

        function changeFrame(){
            var iframeWindow = document.getElementById("fra_content_01X").contentWindow;
            iframeWindow.postMessage("A secret", "http://www.wrox.com");

//          alert (document.all.fra_content_01X.src);
//          document.all.fra_content_01X.src="http://www.microsoft.com/";
//          document.forms["Form1"].submit();
//          alert (document.all.fra_content_01X.src);

        }        
        
    </script>
    
</head>

<body>
    <form id="Form1" name="Form1" runat="server">
    
        <!-- container -->
        <div class="div_container">

            <div class="div_bann_00">
                &nbsp;ABS Administration Manager&nbsp;
            </div>

            <!-- banner -->
            <div class="div_bann_01">
                    <img  alt="Logo" src="Images/CAILogoN.jpg" class="img_bann_01" runat="server" />
                    <img alt="" src="Images/caibanner.jpg" class="img_bann_02" runat="server" />
            </div>
            <div class="div_bann_02">
            	    &nbsp;Custodian and Allied Insurance Limited &nbsp;
            </div>
            <div align="right" style="display: none;">
                <asp:Label ID="lblAction" ForeColor="LightGray" Text="Status:" runat="server"></asp:Label>
                &nbsp;<asp:textbox id="txtAction" ForeColor="LightGray" Text="" Visible="true" runat="server" EnableViewState="False" Width="30px"></asp:textbox>
            </div>

            <div class="div_menu_01">
                    &nbsp;<a href="HOME_PG.aspx" target="fra_content_01X">HOME</a>
                    &nbsp;&nbsp;<asp:LinkButton ID="LNK_AD_10" PostBackUrl="AD_MENU.aspx?mopt=tel_bill" Text="Telephone Bill" runat="server"></asp:LinkButton>
                    &nbsp;&nbsp;<asp:LinkButton ID="LNK_AD_20" PostBackUrl="AD_MENU.aspx?mopt=nepa_bill" Text="Electricity Bill" runat="server"></asp:LinkButton>
                    &nbsp;&nbsp;<asp:LinkButton ID="LNK_AD_30" PostBackUrl="AD_MENU.aspx?mopt=veh" Text="Vehicle Maintenance" runat="server"></asp:LinkButton>
                    &nbsp;&nbsp;<asp:LinkButton ID="LNK_AD_40" PostBackUrl="AD_MENU.aspx?mopt=repairs" Text="Repairs and Services" runat="server"></asp:LinkButton>
                    &nbsp;&nbsp;<a href="ADOTH.aspx" target="fra_content_01X">Other Records</a>
                    &nbsp;&nbsp;<a href="javascript:JSDO_LOG_OUT()">Log Off</a>

            </div>
            
            <div class="div_content_01">
                <iframe id="fra_content_01X" name="fra_content_01X" class="fra_content_01" src="HOME_PG.aspx" frameborder="0" scrolling="auto" runat="server">
                </iframe>
            </div>
        
        </div>

    </form>
</body>
</html>
