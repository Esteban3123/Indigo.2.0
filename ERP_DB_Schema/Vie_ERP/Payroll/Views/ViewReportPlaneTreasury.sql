CREATE VIEW [Payroll].[ViewReportPlaneTreasury]
AS
SELECT
ROW_NUMBER() OVER(ORDER BY thi.Nit ASC) as Row, 
thi.Nit,
  thi.Name, 
  li.TotalPaid,
  BankAccountNumber,
  ba.Name AS 'NameBank',
  g.Code,
  li.PayrollDateLiquidated,
  li.LiquidationPeriod,
  g.Name AS 'NameGroup'   
  FROM 
  Payroll.Liquidation AS li
  INNER JOIN Payroll.[Group] AS g ON g.Id = li.GroupId
  LEFT JOIN Payroll.Bank AS ba ON ba.Id = li.BankId
  INNER JOIN Payroll.Employee AS em ON em.Id = li.EmployeeId
  INNER JOIN Common.ThirdParty AS thi ON thi.Id = em.ThirdPartyId