
-- =============================================
-- Author:    
-- Create date: 
-- Description:  Vista que muestra el detalle de auditoría de cambios en contratos
-- =============================================

CREATE   VIEW [Payroll].[VW_ContractAudit_Detail]
AS
SELECT 
    -- Información del Contrato
    ca.[ContractId],
    c.[ContractInitialDate] ,
    c.[ContractEndingDate] ,
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
    u.UserCode + '-' + p.Fullname as UsercodeName
FROM [Payroll].[ContractAudit] ca
INNER JOIN [Payroll].[Contract] c  ON ca.[ContractId] = c.[Id]
LEFT JOIN [Payroll].[FunctionalUnit] fuOld  ON ca.[FieldName] = 'FunctionalUnitId'  AND TRY_CAST(ca.[ValueOld] AS INT) = fuOld.[Id]
LEFT JOIN [Payroll].[FunctionalUnit] fuNew   ON ca.[FieldName] = 'FunctionalUnitId'  AND TRY_CAST(ca.[ValueNew] AS INT) = fuNew.[Id]
LEFT JOIN [Security].[User] u  ON ca.[UserCode] = u.[UserCode]
LEFT JOIN [Security].[Person] p ON p.Id =  u.IdPerson
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vista que muestra el detalle de cambios de auditoría en contratos con información relacionada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'VW_ContractAudit_Detail';
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el detalle legible del historial de auditoría de contratos, resolviendo nombres de unidad funcional y del usuario que realizó cada cambio.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VW_ContractAudit_Detail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existe al menos un registro en ContractAudit asociado a un Contract vigente (INNER JOIN obliga correspondencia ContractId = Contract.Id).; Los valores ValueOld/ValueNew deben ser convertibles a INT cuando FieldName=''FunctionalUnitId'' para resolver el nombre vía TRY_CAST.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VW_ContractAudit_Detail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se muestran auditorías cuyo ContractId aún existe en Payroll.Contract (INNER JOIN).; La resolución de nombre de FunctionalUnit solo aplica al campo ''FunctionalUnitId''; otros campos conservan su valor crudo.; El identificador del usuario modificador se presenta concatenado como ''UserCode-Fullname''.; Si el usuario o la persona asociada no existen, las columnas derivadas quedan en NULL (LEFT JOIN).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VW_ContractAudit_Detail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'auditoría de contratos; contrato laboral; unidad funcional; usuario del sistema; persona', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VW_ContractAudit_Detail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.VW_ContractAudit_Detail: Devuelve una fila por cada registro de ContractAudit unido a su Contract, con valores anterior/nuevo traducidos a nombre de FunctionalUnit cuando FieldName=''FunctionalUnitId''.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VW_ContractAudit_Detail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ca.FieldName = ''FunctionalUnitId'' → Reemplaza ValueOld/ValueNew por el Name de FunctionalUnit resuelto con TRY_CAST(valor AS INT) = FunctionalUnit.Id. else Conserva el valor textual original almacenado en ca.ValueOld / ca.ValueNew.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VW_ContractAudit_Detail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.ContractAudit; Payroll.Contract; Payroll.FunctionalUnit; Security.User; Security.Person', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VW_ContractAudit_Detail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VW_ContractAudit_Detail';
GO
