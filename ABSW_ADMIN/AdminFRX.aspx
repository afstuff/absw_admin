<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AdminFRX.aspx.vb" Inherits="ABSW_ADMIN.AdminFRX" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ABS Administration Manager</title>
</head>
<!-- 
FRAMESET Element Object

Properties
==========
border - Value: Integer
borderColor - Value: Hexadecimal triplet or color name string
cols - Value: String.  percent symbols or asterisks of frame height. 
frameBorder - Value: yes | no | 1 | 0 as strings
frameSpacing - Value: Integer
rows - Value: String. percent symbols or asterisks of frame width. 


FRAME Element Object

Properties
===========
borderColor - Value: Hexadecimal triplet or color name string
contentDocument - A reference to the document contained by that frame
Document - An object reference to frame document
frameBorder - Value: yes | no | 1 | 0 as strings
height - Integer value
longDesc - A URL String. The longDesc property is the scripted equivalent of the LONGDESC attribute of the <FRAME> tag.
marginHeight - Integer value
marginWidth - Integer value
noResize - A boolean value. let the user resize the frame (true) or restrict the user from resizing the frame (false)
scrolling - Value: yes | no | 1 | 0 as strings
src - The URL of the page to display in the frame
width - integer


IFRAME Element Object

Properties
==========
align - Value: String. Any of the possible value (absbottom, absmiddle, baseline, bottom, left, middle, right, texttop, top)
contentDocument
Document
frameBorder - Value: yes | no | 1 | 0 as strings
frameSpacing - Value: Integer
hspace - Value: Integer Read/Write. representing the number of pixels of padding between the element and surrounding content
longDesc
marginHeight - Integer value
marginWidth - Integer value
scrolling - Value: yes | no | 1 | 0 as strings
src - The URL of the page to display in the frame
vspace - Value: Integer Read/Write. representing the number of pixels of padding between the element and surrounding content

--> 


	<frameset id="frameset1" cols="30%, 70%" runat="server" border="0" frameborder="no">
		<frame name="fraLeftBar" src="AdminMnu.aspx" marginwidth="1" marginheight="1" frameborder="no" scrolling="yes" noresize="yes"></frame>
    	<frameset id="frameset2" rows="5%, 85%" runat="server" border="0" frameborder="no">
	        <frame name="fraTopBar" src="" frameborder="no" scrolling="no" noresize="no"></frame>
	        <frame name="fraDetails" src="Welcome.aspx" frameborder="no" scrolling="yes" noresize="yes"></frame>
	    </frameset>
	</frameset>

</html>
