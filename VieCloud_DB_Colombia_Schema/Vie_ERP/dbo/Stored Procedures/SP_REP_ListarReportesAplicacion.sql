
CREATE PROCEDURE [dbo].[SP_REP_ListarReportesAplicacion]
AS
BEGIN
		SET NOCOUNT ON;
		
	SELECT [NUMCONSEC]
      ,[NOMREPARC]
      ,[NOMREPVIS]
      ,[DESCREPOR]
      ,[INDMODULO]
  FROM [dbo].[INREPAPLI]
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los reportes e informes disponibles en la aplicación, consultando el catálogo de reportes configurados por módulo (INREPAPLI). Retorna el número consecutivo de cada reporte, su nombre técnico de archivo, el nombre visible para el usuario, la descripción del reporte y el módulo al que pertenece. Se usa para poblar menús o pantallas de selección de reportes, permitiendo al usuario ver qué informes están disponibles según el módulo del sistema (facturación, historia clínica, agendamiento, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_REP_ListarReportesAplicacion';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_REP_ListarReportesAplicacion';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista el catálogo de reportes configurados en el sistema, exponiendo su identificador, nombre técnico, nombre visible, descripción y módulo asociado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REP_ListarReportesAplicacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'No aplica filtros: siempre retorna el universo completo del catálogo de reportes.; No modifica datos (operación de solo lectura).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REP_ListarReportesAplicacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Catálogo de reportes; Módulo de aplicación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REP_ListarReportesAplicacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.INREPAPLI: Devuelve todos los registros del catálogo de reportes con sus atributos de identificación, nombres, descripción y módulo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REP_ListarReportesAplicacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INREPAPLI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REP_ListarReportesAplicacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REP_ListarReportesAplicacion';
-- GO
