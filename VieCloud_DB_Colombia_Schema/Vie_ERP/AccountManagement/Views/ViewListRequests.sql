
CREATE VIEW [AccountManagement].[ViewListRequests]
AS
SELECT 
    ai.NUMINGRES AdmissionNumber,
    ai.IFECHAING AdmissionDate,
    ai.IDADTIPOIDENTIFICA PatientIdentification,
    ai.UFUCODIGO  FunctionalUnitCode,
	ai.GENCAREGROUP CareGroup,
	ai.CODCENATE as CareGroupCode,
    ai.UFUINGHOS Bed,
    ai.CODDIAING Diagnosis,
    ai.INUMERORE Folio,
    NULL AS 'Asignado',
    ai.INUMERORE TypeIncome,
    ai.CODUSUCRE UserCreation,
    ai.CODUSUMOD UserModificacion
FROM 
    dbo.ADINGRESO ai
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de consulta sobre la tabla de admisiones de pacientes (`dbo.ADINGRESO`) que expone los datos principales de cada episodio de ingreso: número de admisión, fecha, identificación del paciente, unidad funcional, grupo de atención, cama, diagnóstico y folio. Está orientada al módulo de gestión de cuentas (`AccountManagement`) para listar solicitudes o ingresos pendientes de procesamiento. El campo `Asignado` se retorna siempre como `NULL`, lo que sugiere que la asignación aún no está implementada o se calcula externamente.', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewListRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewListRequests';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone un listado plano de ingresos/admisiones de pacientes con sus datos básicos (identificación, unidad funcional, cama, diagnóstico, grupo de atención y auditoría) para alimentar la gestión de solicitudes de cuenta.', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewListRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La vista expone un registro por cada fila de dbo.ADINGRESO sin aplicar filtros (no excluye anulados, cerrados ni por fecha).; El campo ''Asignado'' siempre se devuelve como NULL (placeholder constante).; Tanto ''Folio'' como ''TypeIncome'' se mapean a la misma columna origen INUMERORE, por lo que su valor siempre coincide.', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewListRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'ingreso/admisión; paciente; unidad funcional; grupo de atención; cama; diagnóstico de ingreso; folio', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewListRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ADINGRESO: Devuelve todas las filas de dbo.ADINGRESO renombrando columnas a nomenclatura de negocio en inglés y agregando una columna constante ''Asignado'' = NULL.', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewListRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewListRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewListRequests';
GO
