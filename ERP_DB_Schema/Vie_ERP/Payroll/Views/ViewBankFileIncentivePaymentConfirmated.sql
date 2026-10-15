/****** Object:  View [Payroll].[ViewBankFileIncentivePaymentConfirmated]    Script Date: 28/11/2025 1:05:29 a. m. ******/

CREATE VIEW [Payroll].[ViewBankFileIncentivePaymentConfirmated]
AS
	SELECT 
		bfd.id,
		bf.Id AS BankFileId,
		i.Id AS IncentivePaymentId,
		p.Name AS Position,
		p.id AS PositionId,
		tp.NIT AS Nit,
		tp.NAME AS EmployeeName,
		b.Name AS Bank,
		c.BankAccountNumber AS BankAccount,
		i.PaidValue AS PaidValue,
		c.Status AS Status,
		c.Valid AS Valid,
		c.Id AS ContractId,
		c.BankAccountType AS EmployeeBankTypeAccount,
		i.RegisterStatus AS RegisterStatus, 
		i.Period AS [Period], 
		i.PeriodEndDate AS PeriodEndDate,	
		g.[Name] AS GroupName,
		g.id AS GroupId,
		f.[Name] AS FunctionalUnitName,
		f.id AS FunctionalUnitId,
		i.TotalAccrued,
		i.TotalDeducted,
		c.BasicSalary,
		e.id AS EmployeeId,
		b.id AS BankId,
		bf.Status AS BankFileStatus,
		bf.Process AS Process
	FROM Payroll.IncentivePayment i 
		INNER JOIN Payroll.[Contract] c ON i.ContractId = c.Id
		INNER JOIN Payroll.Employee e ON c.EmployeeId = e.Id
		INNER JOIN Common.ThirdParty tp ON e.ThirdPartyId = tp.Id
		INNER JOIN Payroll.Position p ON p.Id = c.PositionId
		INNER JOIN Payroll.Bank b ON b.Id = c.BankId
		INNER JOIN Payroll.[Group] g ON g.Id = c.GroupId
		INNER JOIN Payroll.FunctionalUnit f ON f.id = c.FunctionalUnitId	
		INNER JOIN Payroll.BankFileDetail bfd ON e.Id = bfd.EmployeeId
		INNER JOIN Payroll.BankFile bf ON bfd.BankFileId = bf.Id 		
	GROUP BY 
		bfd.id,
		bf.Id,
		i.Id,
		p.Name,
		p.id,
		tp.NIT,
		tp.NAME,
		b.Name,
		c.BankAccountNumber,
		i.PaidValue,
		c.Status,
		c.Valid,
		c.Id,
		c.BankAccountType,
		i.RegisterStatus, 
		i.Period, 
		i.PeriodEndDate,	
		g.[Name],
		g.id,
		f.[Name],
		f.id,
		i.TotalAccrued,
		i.TotalDeducted,
		c.BasicSalary,
		e.id,
		b.id,
		bf.Status,
		bf.Process
GO
