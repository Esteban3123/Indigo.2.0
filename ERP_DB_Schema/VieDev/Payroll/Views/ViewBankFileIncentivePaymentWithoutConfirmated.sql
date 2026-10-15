




CREATE VIEW [Payroll].[ViewBankFileIncentivePaymentWithoutConfirmated]
AS
		SELECT 
			I.Id Id,
			P.Name Position ,
			P.id PositionId,
			TP.NIT Nit,
			TP.NAME EmployeeName,
			B.Name Bank,
			C.BankAccountNumber BankAccount,
			I.PaidValue PaidValue,
			C.Status Status,
			C.Valid Valid,
			C.Id ContractId,
			C.BankAccountType EmployeeBankTypeAccount,
			I.RegisterStatus RegisterStatus, 
			I.Period [Period], 
			I.PeriodEndDate PeriodEndDate,	
			G.[Name] GroupName,
			G.id GroupId,
			F.[Name] FunctionalUnitName,
			F.id FunctionalUnitId,
			I.TotalAccrued,
			I.TotalDeducted,
			C.BasicSalary,
			E.id EmployeeId,
			B.id BankId,
			0 BankFileStatus,
			1 Process
		from Payroll.IncentivePayment I 
		INNER JOIN  Payroll.[Contract] C ON I.ContractId = C.Id
		INNER JOIN Payroll.Employee E ON C.EmployeeId= E.Id 
		INNER JOIN Common.ThirdParty TP ON E.ThirdPartyId = TP.Id
		INNER JOIN Payroll.Position P ON P.Id = C.PositionId
		INNER JOIN Payroll.Bank B ON B.Id = C.BankId
		INNER JOIN Payroll.[Group] G on G.Id = C.GroupId
		INNER JOIN Payroll.FunctionalUnit F ON F.id = C.FunctionalUnitId
		GROUP BY TP.NIT,  
         I.Id,
         P.Name,
         P.id,
         TP.NAME,
         B.Name,
         C.BankAccountNumber,
         I.PaidValue,
         C.Status,
         C.Valid,
         C.Id,
         C.BankAccountType,
         I.RegisterStatus, 
         I.Period, 
         I.PeriodEndDate,	
         G.[Name],
         G.id,
         F.[Name],
         F.id,
         I.TotalAccrued,
         I.TotalDeducted,
         C.BasicSalary,
         E.id,
         B.id