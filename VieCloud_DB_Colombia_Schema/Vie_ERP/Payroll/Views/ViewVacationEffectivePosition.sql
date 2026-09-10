/****** Object:  View [Payroll].[ViewVacationEffectivePosition]    Script Date: 9/09/2026 8:50:33 a. m. ******/
CREATE VIEW [Payroll].[ViewVacationEffectivePosition]
AS
SELECT
    v.Id AS VacationId,
    vp.EmployeeId,
    c.Id AS EffectiveContractId,
    c.PositionId AS EffectivePositionId,
    LTRIM(RTRIM(pos.[Name])) AS EffectivePositionName,
    referenceDate.EffectiveDate
FROM Payroll.Vacation v
INNER JOIN Payroll.VacationPeriod vp ON vp.Id = v.VacationPeriodId
CROSS APPLY (SELECT COALESCE(v.VacationStartDateReal, v.VacationStartDate) AS EffectiveDate) referenceDate
INNER JOIN Payroll.[Contract] c
    ON c.EmployeeId = vp.EmployeeId
    AND c.Status <> 3 -- excluye Anulado: nunca representó un tramo real de la linea de tiempo del empleado
    AND referenceDate.EffectiveDate BETWEEN c.ContractInitialDate AND c.ContractEndingDate
INNER JOIN Payroll.Position pos ON pos.Id = c.PositionId
GO