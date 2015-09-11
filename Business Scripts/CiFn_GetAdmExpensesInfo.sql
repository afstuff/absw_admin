set ansi_nulls off;
go  
SET QUOTED_IDENTIFIER ON
GO
--IF OBJECT_ID (N'[dbo].CiFn_GetAdmExpensesInfo', N'P') IS NOT NULL
--begin
--   DROP FUNCTION [dbo].CiFn_GetAdmExpensesInfo;
--   IF  EXISTS (	SELECT * FROM sys.objects 
--				WHERE object_id = OBJECT_ID(N'[dbo].[CiFn_GetAdmExpensesInfo]') 
--				AND type in (N'P')
--			  )
--		Print '<<< Failed Dropping  Function [CiFn_GetAdmExpensesInfo] created !!! >>>'
--	else
--		Print '<<< Function [dbo].[CiFn_GetAdmExpensesInfo] Dropped >>>'
--end   
--else
--	Print '<<< !!! Error Procedure [dbo].[CiFn_GetAdmExpensesInfo] does Not exist >>>' ;   
--GO

alter function [dbo].[CiFn_GetAdmExpensesInfo]
(
		@pStartDate  datetime
	,	@pEndDate	 datetime
	,	@pTransClass nvarchar(15)
	,	@pTransID    nvarchar(5)
	,	@pParam1     nvarchar(100)
	,	@pParam2     nvarchar(100)
	,	@pParam3     nvarchar(100)	
)
RETURNS @rOfficeExpensesInfo Table 
(
 			S_REC_ID [int] IDENTITY(1,1) NOT NULL,
			S_TRANS_CLASS nvarchar(15) NULL,
			S_TRANS_CLASS_DESC nvarchar(50) NULL,
			S_TRANS_ID [nvarchar](5) NULL,
			S_TRANS_ID_DESC nvarchar(50) NULL,
			S_TRANS_NO [nvarchar](22) NULL, --TEL #, VEH. #, meter #
			S_TRANS_TYPE [nvarchar](20) NULL, -- claims #, electrc accounts #
			S_TRANS_DATE [datetime] NULL,
			S_BRANCH_CODE [nvarchar](5) NULL,
			S_BRANCH_NAME [nvarchar](100) NULL,
			S_DEPT_CODE [nvarchar](5) NULL,
			S_DEPT_NAME [nvarchar](100) NULL,
			S_TRANS_AMT_1 [decimal](19, 5) NULL, --claims paid
			S_TRANS_AMT_2 [decimal](19, 5) NULL, -- claims requested
			S_ADM_USER_NAME [nvarchar](100) NULL,
			S_TRANS_DESC [nvarchar](250) NULL
)
--with encryption
AS
BEGIN
    /***************************************
	    Author      : James C. Nnannah
	    Create date : 24th  July 2015
	    Description : Used to return the expenses records in all the different expenditure
	    tables for the admin system . 
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
	
		DECLARE @OfficeExpenses  table
		(
			T_REC_ID [int] IDENTITY(1,1) NOT NULL,
			T_TRANS_CLASS nvarchar(15) NULL,
			T_TRANS_ID [nvarchar](5) NULL,
			T_TRANS_NO [nvarchar](22) NULL, --TEL #, VEH. #, meter #
			T_TRANS_TYPE [nvarchar](20) NULL, -- claims #, electrc accounts #
			T_TRANS_DATE [datetime] NULL,
			T_BRANCH_CODE [nvarchar](5) NULL,
			T_DEPT_CODE [nvarchar](5) NULL,
			T_TRANS_AMT_1 [decimal](19, 5) NULL, --claims paid
			T_TRANS_AMT_2 [decimal](19, 5) NULL, -- claims requested
			T_ADM_USER_NAME [nvarchar](100) NULL,
			T_TRANS_DESC [nvarchar](250) NULL

		)
		
			
		--Get the vehicl maintenance data into the temp table
				INSERT INTO @OfficeExpenses   	
				SELECT 
					   [ADM_VEH_MAINT_TRANS_CLASS]
					  ,[ADM_VEH_MAINT_TRANS_ID]
					  ,[ADM_VEH_MAINT_VEH_NO]
					  ,[ADM_VEH_MAINT_VEH_TYPE]
					  ,[ADM_VEH_MAINT_TRANS_DATE]
					  ,[ADM_VEH_MAINT_BRANCH_CODE]
					  ,[ADM_VEH_MAINT_DEPT_CODE]
					  ,[ADM_VEH_MAINT_TRANS_AMT]
					  ,0
					  ,[ADM_VEH_MAINT_USER_NAME]
					  ,[ADM_VEH_MAINT_TRANS_DESC]
				  FROM [ADM_VEH_MAINT_BILLS] p			
				  WHERE p.[ADM_VEH_MAINT_TRANS_DATE] BETWEEN LTRIM(@pStartDate) AND  LTRIM(@pEndDate) 
						AND p.[ADM_VEH_MAINT_TRANS_CLASS] =  LTRIM(@pTransClass) AND p.[ADM_VEH_MAINT_TRANS_ID] =  LTRIM(@pTransID)
				
		--Get the Telephone data into the temp table
				INSERT INTO @OfficeExpenses   	
				SELECT 
				   [ADM_TRANS_CLASS]
				  ,[ADM_TRANS_ID]
				  ,[ADM_TELEPHONE_NO]
				  ,'Telephone'
				  ,[ADM_TRANS_DATE]
				  ,[ADM_BRANCH_CODE]
				  ,[ADM_DEPT_CODE]
				  ,[ADM_TRANS_AMT]
				  ,0
				  ,[ADM_USER_NAME]
				  ,[ADM_TRANS_DESC]
			  FROM [ADM_TELEPHONE_BILLS] p
		   --   WHERE p.[ADM_TRANS_DATE] BETWEEN '20150101' AND  '20150630'
			  --AND p.[ADM_TRANS_CLASS] =  '001' AND p.[ADM_TRANS_ID] =  '001'
		      WHERE p.[ADM_TRANS_DATE] BETWEEN LTRIM(@pStartDate) AND  LTRIM(@pEndDate)
			  AND p.[ADM_TRANS_CLASS] =  LTRIM(@pTransClass) AND p.[ADM_TRANS_ID] =  LTRIM(@pTransID)
 
		--Get the Repairs data into the temp table
				INSERT INTO @OfficeExpenses   	
				SELECT 
					   [ADM_REPAIRS_TRANS_CLASS]
					  ,[ADM_REPAIRS_TRANS_ID]
					  ,[ADM_REPAIRS_TRANS_NO]
					  ,'Repairs'
					  ,[ADM_REPAIRS_TRANS_DATE]
					  ,[ADM_REPAIRS_BRANCH_CODE]
					  ,[ADM_REPAIRS_DEPT_CODE]
					  ,[ADM_REPAIRS_TRANS_AMT]
					  ,0
					  ,null
					  ,[ADM_REPAIRS_TRANS_DESC]
				FROM [ADM_REPAIRS_BILLS] p
				WHERE p.[ADM_REPAIRS_TRANS_DATE] BETWEEN LTRIM(@pStartDate) AND  LTRIM(@pEndDate)
				AND p.[ADM_REPAIRS_TRANS_CLASS] =  LTRIM(@pTransClass) AND p.[ADM_REPAIRS_TRANS_ID] =  LTRIM(@pTransID)

		--Get the Outsource data into the temp table
				INSERT INTO @OfficeExpenses   	
				 SELECT 
					   [ADM_OUTSOURCE_TRANS_CLASS]
					  ,[ADM_OUTSOURCE_TRANS_ID]
					  ,[ADM_OUTSOURCE_TRANS_NO]
					  ,[ADM_OUTSOURCE_TRANS_TYPE]
					  ,[ADM_OUTSOURCE_TRANS_DATE]
					  ,[ADM_OUTSOURCE_BRANCH_CODE]
					  ,[ADM_OUTSOURCE_DEPT_CODE]
					  ,[ADM_OUTSOURCE_TRANS_AMT]
					  ,0
					  ,[ADM_OUTSOURCE_NO_OF_STAFF]
					  ,[ADM_OUTSOURCE_DESC]
				  FROM [ADM_OUTSOURCE_BILLS] p
				  WHERE p.[ADM_OUTSOURCE_TRANS_DATE] BETWEEN LTRIM(@pStartDate) AND  LTRIM(@pEndDate)
				  AND p.[ADM_OUTSOURCE_TRANS_CLASS] =  LTRIM(@pTransClass) AND p.[ADM_OUTSOURCE_TRANS_ID] =  LTRIM(@pTransID)

		--Get the Electricity data into the temp table
				INSERT INTO @OfficeExpenses   	
				SELECT 
				   [ADM_ELEC_TRANS_CLASS]
				  ,[ADM_ELEC_TRANS_ID]
				  ,[ADM_ELEC_METER_NO]
				  ,'Electricity'
				  ,[ADM_ELEC_TRANS_DATE]
				  ,[ADM_ELEC_BRANCH_CODE]
				  ,[ADM_ELEC_DEPT_CODE]
				  ,[ADM_ELEC_TRANS_AMT]
				  ,0
				  ,[ADM_ELEC_ACCOUNT_NO]
				  ,[ADM_ELEC_PERIOD_PAID_FOR]
			  FROM [ADM_ELECTRIC_BILLS] p
				WHERE p.[ADM_ELEC_TRANS_DATE] BETWEEN LTRIM(@pStartDate) AND  LTRIM(@pEndDate)
				AND p.[ADM_ELEC_TRANS_CLASS] =  LTRIM(@pTransClass) AND p.[ADM_ELEC_TRANS_ID] =  LTRIM(@pTransID)
			  
		--Get the Diesel data into the temp table
				INSERT INTO @OfficeExpenses   	
				SELECT 
				   [ADM_DIESEL_TRANS_CLASS]
				  ,[ADM_DIESEL_TRANS_ID]
				  ,[ADM_DIESEL_TRANS_NO]
				  ,[ADM_DIESEL_TRANS_TYPE]
				  ,[ADM_DIESEL_TRANS_DATE]
				  ,[ADM_DIESEL_BRANCH_CODE]
				  ,[ADM_DIESEL_DEPT_CODE]
				  ,[ADM_DIESEL_TRANS_AMT]
				  ,0
				  ,null
				  ,[ADM_DIESEL_TRANS_DESC]
			  FROM [ADM_DIESEL_BILLS] p
				WHERE p.[ADM_DIESEL_TRANS_DATE] BETWEEN LTRIM(@pStartDate) AND  LTRIM(@pEndDate)
				AND p.[ADM_DIESEL_TRANS_CLASS] =  LTRIM(@pTransClass) AND p.[ADM_DIESEL_TRANS_ID] =  LTRIM(@pTransID)
			  
		--Get the insurance premium data into the temp table
				INSERT INTO @OfficeExpenses   	
				SELECT 
					   999
					   ,998
					   ,[ADM_INS_PREM_TRANS_NO]
					  ,[ADM_INS_PREM_COVER_TYPE]
					  ,[ADM_INS_PREM_TRANS_DATE]
					  ,[ADM_INS_PREM_BRANCH_CODE]
					  ,[ADM_INS_PREM_DEPT_CODE]
					  ,[ADM_INS_PREM_PREMIUM_AMT]
					  ,[ADM_INS_PREM_SUM_INSURED]
					  ,'Pol #: ' + [ADM_INS_PREM_POLICY_NO]
					  ,[ADM_INS_PREM_DESC]
				  FROM [ADM_INSURANCE_PREM_BILLS] p			  
				WHERE p.[ADM_INS_PREM_TRANS_DATE] BETWEEN LTRIM(@pStartDate) AND  LTRIM(@pEndDate)

		--Get the insurance claims data into the temp table
				INSERT INTO @OfficeExpenses   	
			SELECT 
				   888
				  ,889
				  ,[ADM_INS_CLAIM_TRANS_NO]
				  ,[ADM_INS_CLAIM_COVER_TYPE]
				  ,[ADM_INS_CLAIM_TRANS_DATE]
				  ,[ADM_INS_CLAIM_BRANCH_CODE]
				  ,[ADM_INS_CLAIM_DEPT_CODE]
				  ,[ADM_INS_CLAIM_PAID]
				  ,[ADM_INS_CLAIM_REQUESTED]
				  ,'Pol #: ' + ADM_INS_CLAIM_POLICY_NO
				  ,[ADM_INS_CLAIM_DESC]
			  FROM [ADM_INSURANCE_CLAIM_BILLS] p	
			  WHERE p.[ADM_INS_CLAIM_TRANS_DATE] BETWEEN LTRIM(@pStartDate) AND  LTRIM(@pEndDate)
			  
			INSERT INTO  @rOfficeExpensesInfo
			SELECT 
				T_TRANS_CLASS,
			    (SELECT [ADM_TAB_ITEM_DESC] from [ADM_MAINT_CODE_TABLE] 
				where  [ADM_TAB_ITEM_CODE_NO] = T_TRANS_CLASS and [ADM_TAB_CLASS_CODE] = '010') as Trans_Class_Desc,
				T_TRANS_ID,
			    (SELECT [ADM_TAB_ITEM_DESC] from [ADM_MAINT_CODE_TABLE] 
				where  [ADM_TAB_ITEM_CODE_NO] = T_TRANS_ID and [ADM_TAB_CLASS_CODE] = '011') as Trans_ID_Desc ,
				T_TRANS_NO,
				T_TRANS_TYPE, 
				T_TRANS_DATE,
				T_BRANCH_CODE,
				(SELECT [ADM_TAB_ITEM_DESC] from [ADM_MAINT_CODE_TABLE] 
				where  [ADM_TAB_ITEM_CODE_NO] = T_BRANCH_CODE and [ADM_TAB_CLASS_CODE] = '009') as Branch_Name ,
				T_DEPT_CODE,
				(SELECT [ADM_TAB_ITEM_DESC] from [ADM_MAINT_CODE_TABLE] 
				where  [ADM_TAB_ITEM_CODE_NO] = T_DEPT_CODE and [ADM_TAB_CLASS_CODE] = '008') as Dept_Name,
				T_TRANS_AMT_1,
				T_TRANS_AMT_2,
				T_ADM_USER_NAME,
				T_TRANS_DESC 
			
			FROM @OfficeExpenses

	RETURN

END	


GO


--IF  EXISTS (SELECT * FROM sys.objects 
--			WHERE object_id = OBJECT_ID(N'[dbo].[CiFn_GetAdmExpensesInfo]') 
--			AND type in (N'P'))
--	Print '<<< Success Procedure [dbo].[CiFn_GetAdmExpensesInfo] created !!! >>>'
--else
--	Print '<<< !!! Error Procedure [dbo].[CiFn_GetAdmExpensesInfo] Not created >>>'
--GO		  