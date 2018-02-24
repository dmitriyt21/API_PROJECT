


DROP TABLE OverAll1Activity
SELECT 
[OverAllActivityId] = IDENTITY(INT, 1, 1)														--THIS
,* INTO OverAll1Activity FROM 
(

			select BaseTable.*,
			RANK() OVER  (PARTITION BY SalesDate ORDER BY OverAllPoints DESC) AS Place,
			DENSE_RANK() OVER(Order By SalesDate) as Day_Index
			--ROW_NUMBER() OVER  (PARTITION BY SalesDate ORDER BY OverAll DESC) AS ROW   
			from
			(
				select E.AgentId, 
				E.AgentName,
				ISNULL(E.Points,0) as EnrollmentsPoints, 
				ISNULL(S.Points,0) as SalesPoints, 
				ISNULL(SP.Points,0) as SpecialProductsPoints, 
				ISNULL(D.Points,0) as DiscountsPoints,
				ISNULL(E.Points,0)+ISNULL(S.Points,0)+ISNULL(SP.Points,0)+ISNULL(D.Points,0) as OverAllPoints,
				E.SalesDate
				from
				[dbo].[EnrollmentActivity] as E 
				inner join [dbo].[SalesActivity] as S on S.AgentId = E.AgentId and S.SalesDate=E.SalesDate
				inner join [dbo].[SpecialProductsActivity] as SP on SP.AgentId = E.AgentId and SP.SalesDate=E.SalesDate
				inner join [dbo].[DiscountsActivity] as D on D.AgentId = E.AgentId and D.SalesDate=E.SalesDate
				where month(E.SalesDate) = 1
			)
			as BaseTable
) as a


--ALTER TABLE OverAllActivity																	--THIS
-- DROP COLUMN TargetId,RANK, ROW,

--P100, EnrollmentId																				--THIS

--, MonthTarget
ALTER TABLE OverAll1Activity ADD PRIMARY KEY([OverAllActivityId])										--THIS

SELECT * FROM OverAll1Activity 