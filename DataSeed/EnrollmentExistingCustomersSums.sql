	DROP TABLE EnrollmentExistingCustomersSums		

	select [EnrollmentExistingCustomersSumsId] = IDENTITY(INT, 1, 1)														
		,* 
	into EnrollmentExistingCustomersSums
	from
		(
			select sum(ExistingCustomers) as SumOfExistingCustomers,
			sum(ExistingCustomers)/(sum(EnrollmentTarget) - round(0.1*sum(EnrollmentTarget),0)) as PercOfTargetExistingCustomers
			 FROM [dbo].[EnrollmentActivity]
			 where [SalesDate] in (
				 SELECT MAX([SalesDate]) as max_date
				 FROM [dbo].[EnrollmentActivity])
		 ) a ,

		 (
			 select [AgentName] as BestAgentName, ExistingCustomers/(EnrollmentTarget - round(0.1*EnrollmentTarget,0)) as BestAgentPercOfTarget
			 FROM [dbo].[EnrollmentActivity]
			 where (ExistingCustomers/(EnrollmentTarget - round(0.1*EnrollmentTarget,0))) = (
				 SELECT MAX(ExistingCustomers/(EnrollmentTarget - round(0.1*EnrollmentTarget,0))) as PercOfTarget
				 FROM [dbo].[EnrollmentActivity]
				  where [SalesDate] in (
				 SELECT MAX([SalesDate]) as max_date
				 FROM [dbo].[EnrollmentActivity])
				 )
				 and [SalesDate] in (
				 SELECT MAX([SalesDate]) as max_date
				 FROM [dbo].[EnrollmentActivity])
		 ) b

 ALTER TABLE EnrollmentExistingCustomersSums ADD PRIMARY KEY([EnrollmentExistingCustomersSumsId])
 SELECT * FROM EnrollmentExistingCustomersSums 