
DROP TABLE OverAll1Diff																		
SELECT 
[OverAll1DiffId] = IDENTITY(INT, 1, 1)														
,* INTO OverAll1Diff FROM 
(

SELECT [AgentId]
      ,[SalesDate]
      ,[AgentName]
      ,case when (LAG (Place, 1, 0) OVER (PARTITION BY AgentId ORDER BY SalesDate)=0) then Place else (LAG (Place, 1, 0) OVER (PARTITION BY AgentId ORDER BY SalesDate)-Place) end as PlaceDiff,
	   case when (LAG (Place, 1, 0) OVER (PARTITION BY AgentId ORDER BY SalesDate)=0) then ISNULL(OverAllPoints,0) else (-LAG (ISNULL(OverAllPoints,0), 1, 0) OVER (PARTITION BY AgentId ORDER BY SalesDate)+ISNULL(OverAllPoints,0)) end as PointsDiff,
	   case when (LAG (Place, 1, 0) OVER (PARTITION BY AgentId ORDER BY SalesDate)=0) then ISNULL(EnrollmentsPoints,0) else (-LAG (ISNULL(EnrollmentsPoints,0), 1, 0) OVER (PARTITION BY AgentId ORDER BY SalesDate)+ISNULL(EnrollmentsPoints,0)) end as EnrollmentsPointsDiff,
  	   case when (LAG (Place, 1, 0) OVER (PARTITION BY AgentId ORDER BY SalesDate)=0) then ISNULL(SalesPoints,0) else (-LAG (ISNULL(SalesPoints,0), 1, 0) OVER (PARTITION BY AgentId ORDER BY SalesDate)+ISNULL(SalesPoints,0)) end as SalesPointsDiff,
	   case when (LAG (Place, 1, 0) OVER (PARTITION BY AgentId ORDER BY SalesDate)=0) then ISNULL(SpecialProductsPoints,0) else (-LAG (ISNULL(SpecialProductsPoints,0), 1, 0) OVER (PARTITION BY AgentId ORDER BY SalesDate)+ISNULL(SpecialProductsPoints,0)) end as SpecialProductsPointsDiff,                
	   case when (LAG (Place, 1, 0) OVER (PARTITION BY AgentId ORDER BY SalesDate)=0) then ISNULL(DiscountsPoints,0) else (-LAG (ISNULL(DiscountsPoints,0), 1, 0) OVER (PARTITION BY AgentId ORDER BY SalesDate)+ISNULL(DiscountsPoints,0)) end as DiscountsPointsDiff                
        
  FROM [dbo].[OverAll1Activity]
  ) as a

ALTER TABLE OverAll1Diff ADD PRIMARY KEY([OverAll1DiffId])										

SELECT * FROM OverAll1Diff 
  order by AgentId,SalesDate
