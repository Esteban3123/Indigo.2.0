

-- =============================================
-- Author: Mariana Gonzalez   
-- Create date: 25/11/2025
-- Description:  Vista que muestra el detalle de auditoría de cambios en contratos
-- =============================================

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
INNER JOIN [Payroll].[Contract] c ON ca.[ContractId] = c.[Id]
LEFT JOIN [Payroll].[FunctionalUnit] fuOld ON ca.[FieldName] = 'FunctionalUnitId' AND TRY_CAST(ca.[ValueOld] AS INT) = fuOld.[Id]
LEFT JOIN [Payroll].[FunctionalUnit] fuNew ON ca.[FieldName] = 'FunctionalUnitId' AND TRY_CAST(ca.[ValueNew] AS INT) = fuNew.[Id]
LEFT JOIN [Security].[User] u  ON ca.UserCode = u.UserCode
LEFT JOIN [Security].[Person] p ON p.Id = u.IdPerson

GO
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Historial de auditoría de contratos laborales del personal: muestra todos los cambios realizados sobre los contratos de nómina, indicando qué campo fue modificado, el valor anterior y el nuevo valor, la fecha del cambio y el usuario que lo realizó. Integra los datos del contrato (número, empleado, fechas de inicio y fin, estado como Vigente, Liquidado, Anulado, Reemplazado o Parcialmente Retirado), el registro de auditoría y la información del usuario responsable del cambio. Cuando el campo modificado corresponde a una unidad funcional, traduce el código interno al nombre legible del área o departamento. Se utiliza para trazabilidad, control interno y seguimiento de modificaciones sobre los vínculos contractuales del talento humano.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewContractAudit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewContractAudit';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el detalle legible de la bitácora de cambios de contratos laborales, traduciendo códigos de estado y referencias a unidades funcionales, e identificando al usuario que efectuó cada modificación.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewContractAudit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las filas de Payroll.ContractAudit deben tener un ContractId que exista en Payroll.Contract para aparecer en la vista (INNER JOIN).; Para resolver nombres de unidad funcional, los valores ValueOld/ValueNew deben ser convertibles a INT; de lo contrario TRY_CAST devuelve NULL y no se asocia FunctionalUnit.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewContractAudit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila de auditoría se asocia obligatoriamente a un contrato existente (INNER JOIN con Payroll.Contract por ContractId).; Cuando el campo auditado es ''FunctionalUnitId'', los valores se presentan resueltos a su nombre legible en lugar del Id numérico.; El estado del contrato se traduce a una etiqueta textual fija según el código numérico Status (1..5); valores fuera de ese rango quedan como NULL en ContractStatus.; El identificador del usuario modificador se presenta concatenado como ''UserCode - Fullname''.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewContractAudit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Auditoría de contratos; Contrato laboral; Estado de contrato (Vigente, Liquidado, Anulado, Reemplazado, Parcialmente Retirado); Unidad funcional; Usuario que realizó la modificación; Empleado', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewContractAudit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.ContractAudit: Devuelve un registro por cada cambio auditado, enriquecido con datos del contrato, traducción del estado, resolución de nombres para FunctionalUnitId y datos del usuario (UserCode + Fullname).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewContractAudit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si c.Status = 1 → Se muestra ContractStatus = ''Vigente''; si c.Status = 2 → Se muestra ContractStatus = ''Liquidado''; si c.Status = 3 → Se muestra ContractStatus = ''Anulado''; si c.Status = 4 → Se muestra ContractStatus = ''Reemplazado''; si c.Status = 5 → Se muestra ContractStatus = ''Parcialmente Retirado''; si ca.FieldName = ''FunctionalUnitId'' → ValueOld y ValueNew se resuelven al Name de Payroll.FunctionalUnit usando TRY_CAST(ValueOld/ValueNew AS INT) contra fu.Id else Se muestran los valores literales ca.ValueOld y ca.ValueNew tal como están registrados en la auditoría', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewContractAudit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.ContractAudit; Payroll.Contract; Payroll.FunctionalUnit; Security.User; Security.Person', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewContractAudit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewContractAudit';
GO
