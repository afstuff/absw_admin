
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

alter PROCEDURE [ABSSP_ADMIN_OUTSOURCE_RPT]
	(@RPT_ST_DATE	DATETIME = '201310'
	,@RPT_END_DATE	DATETIME = '201310'
	,@RPT_ST_BRANCH	CHAR(15) = '1501'
	,@RPT_END_BRANCH CHAR(15) = '1501')

AS

  SELECT 
    WK.ADM_OUTSOURCE_TRANS_TYPE AS RPT_OUTSOURCE_TYPE_CD
    --,WK.ADM_OUTSOURCE_TRANS_NO AS RPT_OUTSOURCE_TRANS_NO
    ,WK.ADM_OUTSOURCE_TRANS_DATE AS RPT_DATE
    ,WK.ADM_OUTSOURCE_BRANCH_CODE AS RPT_BRANCH_CODE
    ,WK.ADM_OUTSOURCE_DEPT_CODE AS RPT_DEPT_CODE
     ,WK.ADM_OUTSOURCE_NO_OF_STAFF AS RPT_NO_OF_STAFF
    ,WK.ADM_OUTSOURCE_DESC AS RPT_DESC
    ,WK.ADM_OUTSOURCE_TRANS_AMT AS RPT_TRANS_AMT
    ,WK.ADM_OUTSOURCE_SUPPLY_COY AS RPT_SUPPLY_COY

    ,(SELECT CTBRA_NAME FROM [ABSBRNCHTAB] WHERE CTBRA_NUM = WK.ADM_OUTSOURCE_BRANCH_CODE)AS RPT_BRANCH_NAME
    ,(SELECT CTINSRD_LONG_DESCR FROM ABSDEPTTAB WHERE CTINSRD_NO = wk.ADM_OUTSOURCE_DEPT_CODE) AS RPT_DEPT_NAME
  --  , TYPE.ADM_TAB_ITEM_DESC AS RPT_OUTSOURCE_TYPE
    , INS.ADM_TAB_ITEM_DESC AS RPT_SUPPLY_COY_NAME

    FROM ADM_OUTSOURCE_BILLS  AS WK

    LEFT JOIN [ADM_MAINT_CODE_TABLE]  AS INS
       ON (INS.ADM_TAB_ITEM_CODE_NO = ADM_OUTSOURCE_SUPPLY_COY AND INS.ADM_TAB_CLASS_CODE = '002')

 WHERE WK.ADM_OUTSOURCE_TRANS_DATE >= @RPT_ST_DATE AND WK.ADM_OUTSOURCE_TRANS_DATE <= @RPT_END_DATE AND
 WK.ADM_OUTSOURCE_BRANCH_CODE >= @RPT_ST_BRANCH AND WK.ADM_OUTSOURCE_BRANCH_CODE <= @RPT_END_BRANCH

    ORDER BY WK.ADM_OUTSOURCE_BRANCH_CODE, WK.ADM_OUTSOURCE_TRANS_DATE

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO
