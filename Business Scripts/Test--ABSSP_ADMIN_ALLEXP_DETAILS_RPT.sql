DECLARE				@pStartDate  datetime
	,	@pEndDate	 datetime
	,	@pTransClass nvarchar(15)
	,	@pTransID    nvarchar(5)
	,	@pParam1     nvarchar(100)
	,	@pParam2     nvarchar(100)
	,	@pParam3     nvarchar(100)	
	,	@pDebug		 int = 1
	
SELECT			@pStartDate = '01/01/2015'
	,	@pEndDate  =  '06/30/2015'
	,	@pParam1= NULL
	,	@pParam2= NULL
	,	@pParam3= NULL
	
	--EXEC  ABSSP_ADMIN_ALLEXP_DETAILS_RPT @pStartDate,@pEndDate,'001','001',@pParam1,@pParam2,@pParam3
--	select * from   [CiFn_GetAdmExpensesInfo]( @pStartDate,@pEndDate,'001','001',@pParam1,@pParam2,@pParam3)
	SELECT        *
FROM            dbo.CiFn_GetAdmExpensesInfo(@pStartDate, @pEndDate, @pTransClass, @pTransID, @pParam1, @pParam2, @pParam3) AS CiFn_GetAdmExpensesInfo_1
	