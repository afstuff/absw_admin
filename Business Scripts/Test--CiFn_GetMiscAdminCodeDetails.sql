Declare	@pClassCode  nvarchar(50)
	
	,	@pItemCode  nvarchar(50)
	,	@pParam1     nvarchar(100)
	,	@pParam2     nvarchar(100)
	,	@pParam3     nvarchar(100)	

select
		@pClassCode  = '001'
	,	@pItemCode  = 'KJG 888 NH'
	,	@pParam1     = null
	,	@pParam2     = null
	,	@pParam3     =null	



SELECT * FROM CiFn_GetMiscAdminCodeDetails(@pClassCode,@pItemCode,NULL,NULL,NULL)
