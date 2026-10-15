
CREATE VIEW [Payroll].[ViewReportPaidAgreementsProcess]
AS
SELECT ROW_NUMBER() OVER(ORDER BY E.Id ASC) as Row,
E.Id as IdEmpleado, L.GroupId as IdGrupo, TP.Nit as Cedula, TP.Name as Empleado, L.PayrollDateLiquidated AS FechaPago,'NÓMINA' as Pago, 
CASE	WHEN L.RegisterStatus = '' THEN 'SIN CONFIRMAR' 
		WHEN L.RegisterStatus = 'C' THEN 'CONFIRMADO' END AS Estado, LD.ConceptTotalValue as ValorConcepto
FROM Payroll.Employee E, Common.ThirdParty TP, Payroll.Liquidation L, Payroll.LiquidationDetail LD
WHERE E.Id = L.EmployeeId AND LD.PayrollId = L.Id AND E.ThirdPartyId = TP.Id
and LD.ConceptClass = '041'
UNION
SELECT  ROW_NUMBER() OVER(ORDER BY E.Id ASC) as Row,
E.Id, CONT.GroupId, TP.Nit, TP.Name, V.VacationStartDate, 'VACACIONES', 'CONFIRMADO', VD.Deducted
FROM Payroll.VacationPeriod VP, Payroll.Vacation V, PAyroll.VacationDetail VD, Payroll.Employee E, Common.ThirdParty TP, Payroll.Concept C, PAyroll.Contract CONT
WHERE VP.Id = V.VacationPeriodId AND VD.IdVacation = V.Id AND VP.EmployeeId = E.ID AND E.ThirdPartyId = TP.Id AND C.Id = VD.IdConcept AND C.ConceptClass = '041' AND CONT.Id = VP.ContractId