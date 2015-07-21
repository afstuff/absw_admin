set ansi_nulls off;
go
SET QUOTED_IDENTIFIER ON   
GO

IF OBJECT_ID (N'[dbo].CiFn_GetMiscAdminCodeDetails', N'TF') IS NOT NULL
begin
   DROP FUNCTION [dbo].CiFn_GetMiscAdminCodeDetails;
   IF  EXISTS (	SELECT * FROM sys.objects 
				WHERE object_id = OBJECT_ID(N'[dbo].[CiFn_GetMiscAdminCodeDetails]') 
				AND type in (N'TF')
			  )
		Print '<<< Failed Dropping  FUNCTION [CiFn_GetMiscAdminCodeDetails] created !!! >>>'
	else
		Print '<<< FUNCTION [dbo].[CiFn_GetMiscAdminCodeDetails] Dropped >>>'
end   
else
	Print '<<< !!! Error FUNCTION [dbo].[CiFn_GetMiscAdminCodeDetails] does Not exist >>>' ;   
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function dbo.CiFn_GetMiscAdminCodeDetails
(
		@pClassCode  nvarchar(10)
	,	@pItemCode  nvarchar(15)
	,	@pParam1     nvarchar(100)
	,	@pParam2     nvarchar(100)
	,	@pParam3     nvarchar(100)	
)
RETURNS @rAdminInfo Table 
(
   		 sItemCode			nvarchar(15) NULL
   		,sItemDesc			nvarchar(100) NULL
		,sTransType			nvarchar(10) NULL
		,sBranch			   nvarchar(5) NULL
		,sDept			   nvarchar(5) NULL
)
--with encryption
AS
BEGIN
    /***************************************
	    Author      : James C. Nnannah
	    Create date : 2nd  August 2014
	    Description : Used to return the details for the admin code system . 
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
	
	--SELECT @pItemCode = ISNULL(@pItemCode, '')
	 
  --  IF (@pItemCode = NULL)
		--BEGIN
		--	INSERT INTO @rAdminInfo   	
		--	SELECT
		--		ADM_TAB_ITEM_DESC
		--		,ADM_COD_TRANS_TYPE
		--		,ADM_TAB_ITEM_BRANCH
		--		,ADM_TAB_ITEM_DEPT
		--	FROM ADM_MAINT_CODE_TABLE p 
		--		WHERE p.ADM_TAB_CLASS_CODE = LTRIM(@pClassCode) 

		--END
		--ELSE
		--	BEGIN
				INSERT INTO @rAdminInfo   	
				SELECT
					ADM_TAB_ITEM_CODE_NO
					,ADM_TAB_ITEM_DESC
					,ADM_COD_TRANS_TYPE
					,ADM_TAB_ITEM_BRANCH
					,ADM_TAB_ITEM_DEPT
				FROM ADM_MAINT_CODE_TABLE p 
			WHERE p.ADM_TAB_CLASS_CODE = @pClassCode and p.ADM_TAB_ITEM_CODE_NO = @pItemCode 
--			END
				
 
     RETURN
--     SELECT
--*	FROM ADM_MAINT_CODE_TABLE p 
END	
GO
IF  EXISTS (SELECT * FROM sys.objects 
			WHERE object_id = OBJECT_ID(N'[dbo].[CiFn_GetMiscAdminCodeDetails]') 
			AND type in (N'TF'))
	Print '<<< Success creating function  [dbo].[CiFn_GetMiscAdminCodeDetails] created !!! >>>'
else
	Print '<<< !!! Error creating function [dbo].[CiFn_GetMiscAdminCodeDetails] Not created >>>'
GO

  