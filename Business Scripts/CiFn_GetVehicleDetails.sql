set ansi_nulls off;
go
SET QUOTED_IDENTIFIER ON   
GO

IF OBJECT_ID (N'[dbo].CiFn_GetVehicleDetails', N'TF') IS NOT NULL
begin
   DROP FUNCTION [dbo].CiFn_GetVehicleDetails;
   IF  EXISTS (	SELECT * FROM sys.objects 
				WHERE object_id = OBJECT_ID(N'[dbo].[CiFn_GetVehicleDetails]') 
				AND type in (N'TF')
			  )
		Print '<<< Failed Dropping  FUNCTION [CiFn_GetVehicleDetails] created !!! >>>'
	else
		Print '<<< FUNCTION [dbo].[CiFn_GetVehicleDetails] Dropped >>>'
end   
else
	Print '<<< !!! Error FUNCTION [dbo].[CiFn_GetVehicleDetails] does Not exist >>>' ;   
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function dbo.CiFn_GetVehicleDetails
(
		@pVehicleNo  nvarchar(15)
	,	@pParam1     nvarchar(100)
	,	@pParam2     nvarchar(100)
	,	@pParam3     nvarchar(100)	
)
RETURNS @rGetVehicleInfo Table 
(
		sCustomerName			nvarchar(100) NULL
)
--with encryption
AS
BEGIN
    /***************************************
	    Author      : James C. Nnannah
	    Create date : 2nd  August 2014
	    Description : Used to return the Vehicle details for the admin system . 
	    Version     : 1
    ******************************************/
		
		set @pParam1 = REPLACE(@pParam1,N'''',N'--');
		set @pParam1 = REPLACE(@pParam1,N'--',N'--');		
		set @pParam1 = REPLACE(@pParam1,N';',N'--');				
		set @pParam1 = REPLACE(@pParam1,N'/* ... */',N'--');	
		set @pParam1 = REPLACE(@pParam1,N'xp_',N'--');

		set @pParam2 = REPLACE(@pParam2,N'''',N'--');
		set @pParam2 = REPLACE(@pParam2,N'--',N'--');		
		set @pParam2 = REPLACE(@pParam2,N';',N'--');				
		set @pParam2 = REPLACE(@pParam2,N'/* ... */',N'--');	
		set @pParam2 = REPLACE(@pParam2,N'xp_',N'--');

		set @pParam3 = REPLACE(@pParam3,N'''',N'--');
		set @pParam3 = REPLACE(@pParam3,N'--',N'--');		
		set @pParam3 = REPLACE(@pParam3,N';',N'--');				
		set @pParam3 = REPLACE(@pParam3,N'/* ... */',N'--');	
		set @pParam3 = REPLACE(@pParam3,N'xp_',N'--');

	INSERT INTO @rGetVehicleInfo   
	SELECT
		
		[TBIL_CUST_DESC]
	FROM [TBIL_CUST_DETAIL] p 
	WHERE [TBIL_CUST_CODE] = @pVehicleNo
			
 
     RETURN
END	
GO
IF  EXISTS (SELECT * FROM sys.objects 
			WHERE object_id = OBJECT_ID(N'[dbo].[CiFn_GetVehicleDetails]') 
			AND type in (N'TF'))
	Print '<<< Success creating function  [dbo].[CiFn_GetVehicleDetails] created !!! >>>'
else
	Print '<<< !!! Error creating function [dbo].[CiFn_GetVehicleDetails] Not created >>>'
GO