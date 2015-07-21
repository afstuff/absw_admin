<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AD122.aspx.vb" Inherits="ABSW_ADMIN.AD122" %>

<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <!--
    <base target="fra_content_01X" runat="server" />
    -->
    
    <title>Electricity Bill</title>
	<link rel="stylesheet" href="calendar.css" />
    <link rel="stylesheet" type="text/css" href="StyleAdmin.css" />
    <script src="jquery.simplemodal.js" type="text/javascript"></script>
    <script src="jquery-1.11.0.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="Script/ScriptJS.js">
    </script>

    <script language="vbscript" type="text/vbscript" src="Script/ScriptVB.vbs">
    </script>


<script type="text/javascript">

    function cancelEvent(event) {
        window[event] = function() { null }
    } 

    // calling jquery functions once document is ready
    $(document).ready(function() {

        $("#ddlBraNumStart").on('focusout', function(e) {
            e.preventDefault();
            document.getElementById('txtBraNumStart').value = $("#ddlBraNumStart").val();
            return false;
        });


        $("#ddlBraNumEnd").on('focusout', function(e) {
            e.preventDefault();
            document.getElementById('txtBraNumEnd').value = $("#ddlBraNumEnd").val();
            return false;
        });


    }); //end of doc ready
</script>




</head>
<body onload="<%=publicMsgs%>" onclick="return cancelEvent('onbeforeunload')">
    <form id="AD112" runat="server">
    
    <div class="div_container_02">
    <br />
    
    <table align="center" border="0" class="tbl_main">
        <tr>
            <td align="left" colspan="7" valign="top">
                <table border="0" class="tbl_butt">
                    <tr>
                        <td colspan="5" valign="top">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;
                        </td>
                        <td id="td_Return_Link" colspan="1" valign="top" runat="server">
                            &nbsp;|&nbsp;&nbsp;<a id="PageAnchor_Return_Link" runat="server" href="Blankpg.aspx" style="font-size:large; font-weight:bold;">CLOSE PAGE</a>
                            <%=PageLinks%>&nbsp;&nbsp;|
                        </td>
                        <td colspan="1" valign="top">    
        	                <div style="display: none;">
        	                    &nbsp;&nbsp;Status:&nbsp;<asp:textbox id="txtAction" Visible="true" runat="server" EnableViewState="false" Width="50px"></asp:textbox>&nbsp;
        	                </div>
                            
        	            </td>
                    </tr>                    
                </table>
            </td>
        </tr>

                <tr>
                    <td colspan="7" valign="top"><asp:Label ID="lblMessage" Text="Status:" ForeColor="Red" runat="server"></asp:Label></td>
                </tr>    

        <tr>
            <td colspan="7" valign="top">
                <table align="left" width="100%">
                    <tr>
                        <td align="left" colspan="2" valign="top" class="tbl_caption">
                            Electricity Bills Print
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top"><asp:Label ID="Label1" Text="Start Print Date:" runat="server"></asp:Label>&nbsp;</td>
                        <td valign="top"><asp:TextBox ID="txtStartDate" MaxLength="10" runat="server"></asp:TextBox>
                        <script language="JavaScript" type="text/javascript">
                            new tcal({ 'formname': 'AD112', 'controlname': 'txtTransDate' });</script>&nbsp;&nbsp;dd/mm/yyyy</td>                       
                    </tr>
                    <tr>
                        <td align="right" valign="top"><asp:Label ID="lblTransDate" Text="End Print Date:" runat="server"></asp:Label>&nbsp;</td>
                        <td valign="top"><asp:TextBox ID="txtEndDate" MaxLength="10" runat="server"></asp:TextBox>
                        <script language="JavaScript" type="text/javascript">
                            new tcal({ 'formname': 'AD112', 'controlname': 'txtTransDate' });</script>&nbsp;&nbsp;dd/mm/yyyy</td>                       
                    </tr>
                    <tr>
                        <td align="right" valign="top"><asp:Label ID="lblBraNum" Text="Start Branch:" runat="server"></asp:Label>&nbsp;</td>
                        <td valign="top"><asp:DropDownList ID="ddlBraNumStart" AutoPostBack="true" Width="150" runat="server"></asp:DropDownList>                       
                            &nbsp;<asp:TextBox ID="txtBraNumStart" Enabled="true" Width="100" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                                        <tr>
                        <td align="right" valign="top"><asp:Label ID="lblDeptNum" Text="End Branch:" runat="server"></asp:Label>&nbsp;</td>
                        <td valign="top"><asp:DropDownList ID="ddlBraNumEnd" Width="150" runat="server"></asp:DropDownList>                       
                            &nbsp;<asp:TextBox ID="txtBraNumEnd"  Enabled="true" Width="100" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                      <td colspan="5" valign="top">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="butPrint" CssClass="cmd_butt" Enabled="true" Text="Print" runat="server" />
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" valign="top">&nbsp; </td>
                    </tr>

                </table>
            </td>                
        </tr>

                <tr>
                    <td colspan="7" valign="top">
                    <table align="center" width="90%" style="border-style:groove;">
                        <tr>
                            <td align="left" valign="top" colspan="4" class="tbl_caption">
                                Bills Report Preview</td>
                        </tr>

                        <tr>
                            <td colspan="4" valign="top" style="width:auto;">                                
                                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
                            </td>
                        </tr>
                    </table>

                    </td>    
                </tr>


                <tr>
                    <td colspan="7" valign="top">&nbsp;</td>
                    
                </tr>    
        
    </table>
    
    </div>
    </form>
</body>
</html>
