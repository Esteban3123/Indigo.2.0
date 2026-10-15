CREATE VIEW [Payroll].[ViewContractAudit]
AS
SELECT 
    -- Información del Contrato
	ca.Id,
	c.InitialContractNumber,
    ca.[ContractId],
	c.EmployeeId,
    c.[ContractInitialDate] ,
    c.[ContractEndingDate] ,
	Case
		WHEN c.Status = 1 THEN 'Vigente'
		WHEN c.Status = 2 THEN 'Liquidado'
		WHEN c.Status = 3 THEN 'Anulado'
		WHEN c.Status = 4 THEN 'Reemplazado'
		WHEN c.Status = 5 THEN 'Parcialmente Retirado'
	END AS ContractStatus,
    -- Información de Auditoría
    ca.[Type],
    ca.[FieldName],
    -- Valor Anterior (usando CASE según el FieldName)
    CASE 
        WHEN ca.[FieldName] = 'FunctionalUnitId' THEN fuOld.[Name]
        ELSE ca.[ValueOld]
    END AS ValueOld,
    -- Valor Nuevo (usando CASE según el FieldName)
    CASE 
        WHEN ca.[FieldName] = 'FunctionalUnitId' THEN fuNew.[Name]
        ELSE ca.[ValueNew]
    END AS ValueNew,
    
    -- Información del Usuario que modificó
    ca.[Date] ,
	u.UserCode + ' - ' + p.Fullname as UserCodeName

FROM [Payroll].[ContractAudit] ca
INNER JOIN [Payroll].[Contract] c 
    ON ca.[ContractId] = c.[Id]
LEFT JOIN [Payroll].[FunctionalUnit] fuOld 
    ON ca.[FieldName] = 'FunctionalUnitId' 
    AND TRY_CAST(ca.[ValueOld] AS INT) = fuOld.[Id]
LEFT JOIN [Payroll].[FunctionalUnit] fuNew 
    ON ca.[FieldName] = 'FunctionalUnitId' 
    AND TRY_CAST(ca.[ValueNew] AS INT) = fuNew.[Id]
LEFT JOIN [Security].[User] u
    ON ca.UserCode = u.UserCode

LEFT JOIN [Security].[Person] p
    ON p.Id = u.IdPerson


GO
