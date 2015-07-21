set ansi_nulls off;
go
SET QUOTED_IDENTIFIER ON   
GO

IF OBJECT_ID (N'[dbo].CiFn_GetAdmPolicyInfo', N'TF') IS NOT NULL
begin
   DROP FUNCTION [dbo].CiFn_GetAdmPolicyInfo;
   IF  EXISTS (	SELECT * FROM sys.objects 
				WHERE object_id = OBJECT_ID(N'[dbo].[CiFn_GetAdmPolicyInfo]') 
				AND type in (N'TF')
			  )
		Print '<<< Failed Dropping  FUNCTION [CiFn_GetAdmPolicyInfo] created !!! >>>'
	else
		Print '<<< FUNCTION [dbo].[CiFn_GetAdmPolicyInfo] Dropped >>>'
end   
else
	Print '<<< !!! Error FUNCTION [dbo].[CiFn_GetAdmPolicyInfo] does Not exist >>>' ;   
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function dbo.CiFn_GetAdmPolicyInfo
(
		@pPolicyNo  nvarchar(50)
	,	@pParam1     nvarchar(100)
	,	@pParam2     nvarchar(100)
	,	@pParam3     nvarchar(100)	
)
RETURNS @rAdminInfo Table 
(
   		 sBrokerName			nvarchar(100) NULL
   		,sInsuredName			nvarchar(100) NULL
		,sInsuranceClass			nvarchar(10) NULL
)
--with encryption
AS
BEGIN
    /***************************************
	    Author      : James C. Nnannah
	    Create date : 8th  September 2014
	    Description : Used to return the insurance details for the admin system . 
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
	
				INSERT INTO @rAdminInfo   	
				SELECT
					[ADM_INS_PREM_BROKER_NAME]
					,[ADM_INS_PREM_INSURER_NAME]
					,[ADM_INS_PREM_COVER_TYPE]
				FROM ADM_INSURANCE_PREM_BILLS p 
			WHERE p.[ADM_INS_PREM_POLICY_NO] = LTRIM(@pPolicyNo) 
				
 
     RETURN
--     SELECT
--*	FROM dbo.ADM_INSURANCE_PREM_BILLS
END	
GO
IF  EXISTS (SELECT * FROM sys.objects 
			WHERE object_id = OBJECT_ID(N'[dbo].[CiFn_GetAdmPolicyInfo]') 
			AND type in (N'TF'))
	Print '<<< Success creating function  [dbo].[CiFn_GetAdmPolicyInfo] created !!! >>>'
else
	Print '<<< !!! Error creating function [dbo].[CiFn_GetAdmPolicyInfo] Not created >>>'
GO

  