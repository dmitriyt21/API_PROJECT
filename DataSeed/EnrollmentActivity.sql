DROP TABLE EnrollmentActivity
SELECT [EnrollmentActivityId] = IDENTITY(INT, 1, 1)												--THIS
,* INTO EnrollmentActivity FROM 
(
		select BaseTable.*, RANK as Place,
		P.P100,																					--THIS
		SUM(P.P100) OVER(PARTITION BY RANK)/COUNT(P.P100) OVER(PARTITION BY RANK) AS Points		--THIS
		from [dbo].[Points] as P right join
		(
			select BaseTable.*,
			RANK() OVER  (PARTITION BY SalesDate ORDER BY PercOfTarget DESC) AS RANK,
			ROW_NUMBER() OVER  (PARTITION BY SalesDate ORDER BY PercOfTarget DESC) AS ROW   
			from
			(
				select 
				Base.*
				, 
				T.EnrollmentTarget,																--THIS
				cast(
				(Base.ExistingCustomers+Base.NewCustomers)										--THIS
				as float)
				/
				cast(
				T.EnrollmentTarget																--THIS
				as float)
				/DT.MonthTarget  as PercOfTarget,
				A.AgentName, DT.MonthTarget 
				from
				[dbo].[Enrollments] as Base														--THIS
				inner join [dbo].[Agents] as A on Base.AgentId=A.AgentId
				inner join [dbo].[Targets] as T on Base.AgentId=T.AgentId and Base.SalesDate=T.ValueDate
				inner join [dbo].[DailyTargets] as DT on Base.SalesDate=DT.Date
				)
			as BaseTable
			)
		as BaseTable on P.Place=BaseTable.ROW
) as a

ALTER TABLE EnrollmentActivity
 DROP COLUMN TargetId,RANK, ROW,

P100, EnrollmentId																				--THIS

, MonthTarget


ALTER TABLE EnrollmentActivity ADD PRIMARY KEY([EnrollmentActivityId])
--ALTER TABLE EnrollmentActivity ADD COLUMN [EnrollmentActivityId] AUTOINCREMENT NOT NULL																	--THIS

SELECT * FROM EnrollmentActivity 
--DROP TABLE #TemporaryTable