CREATE   VIEW [Payroll].[VW_ElectronicPayrollConcepts]
AS
SELECT 
    Id,
    Code,
    Name,
    ConceptType,
    CASE ConceptType
        WHEN 1 THEN 'Devengados'
        WHEN 2 THEN 'Deducciones'
        WHEN 3 THEN 'No Aplica'
        ELSE 'Desconocido'
    END AS TipoConcepto,
    InternalCode,
    State,
    CreationUser,
    CreationDate,
    ModificationUser,
    ModificationDate
FROM Payroll.ElectronicPayrollConcepts;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista que expone el catálogo de conceptos de nómina electrónica reportados ante la DIAN, añadiendo la columna calculada `TipoConcepto` que traduce el valor numérico del tipo (1, 2, 3) a sus etiquetas en español: Devengados, Deducciones y No Aplica. Sirve como capa de presentación o consulta para procesos que requieren los conceptos con su clasificación legible, incluyendo metadatos de auditoría como usuario y fecha de creación y modificación.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VW_ElectronicPayrollConcepts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VW_ElectronicPayrollConcepts';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el catálogo de conceptos de nómina electrónica añadiendo una descripción legible del tipo de concepto (Devengados/Deducciones/No Aplica/Desconocido) a partir del código numérico ConceptType.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VW_ElectronicPayrollConcepts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La tabla Payroll.ElectronicPayrollConcepts debe existir y contener los conceptos de nómina electrónica.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VW_ElectronicPayrollConcepts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todo registro expone una etiqueta legible TipoConcepto derivada de ConceptType, garantizando que valores no contemplados (distintos de 1,2,3) se rotulen como ''Desconocido''.; La vista no filtra registros: expone todos los conceptos del catálogo, incluyendo cualquier estado en State.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VW_ElectronicPayrollConcepts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Nómina electrónica; Conceptos de nómina; Devengados; Deducciones', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VW_ElectronicPayrollConcepts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve todas las filas de Payroll.ElectronicPayrollConcepts agregando la columna calculada TipoConcepto mediante CASE sobre ConceptType (1→''Devengados'', 2→''Deducciones'', 3→''No Aplica'', otro→''Desconocido'').', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VW_ElectronicPayrollConcepts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ConceptType = 1 → TipoConcepto = ''Devengados''; si ConceptType = 2 → TipoConcepto = ''Deducciones''; si ConceptType = 3 → TipoConcepto = ''No Aplica'' else TipoConcepto = ''Desconocido'' para cualquier otro valor', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VW_ElectronicPayrollConcepts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.ElectronicPayrollConcepts', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VW_ElectronicPayrollConcepts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VW_ElectronicPayrollConcepts';
GO
