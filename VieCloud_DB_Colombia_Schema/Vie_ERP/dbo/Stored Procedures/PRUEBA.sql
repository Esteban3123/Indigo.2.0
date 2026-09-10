
CREATE procedure [dbo].[PRUEBA]
@FECHAINI AS DATETIME, --='2022-07-01 00:00:00',
@FECHAFIN AS DATETIME --='2022-07-05 23:59:59'
AS

select * from dbo.ADINGRESO 
where IFECHAING between @FECHAINI  and @FECHAFIN
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de consulta que retorna todos los ingresos o admisiones de pacientes registrados en un rango de fechas indicado. Recibe una fecha de inicio y una fecha de fin, y filtra los episodios de ingreso (urgencias, hospitalizaciones, consulta externa, entre otros) cuya fecha de ingreso se encuentre dentro de ese período. Útil para obtener un listado de atenciones o admisiones ocurridas en un intervalo de tiempo determinado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'PRUEBA';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'PRUEBA';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta los ingresos/admisiones de pacientes registrados dentro de un rango de fechas indicado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'PRUEBA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proporcionar una fecha inicial y una fecha final válidas para delimitar el rango de búsqueda.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'PRUEBA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen ingresos cuya fecha de ingreso cae dentro del rango cerrado [FECHAINI, FECHAFIN].', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'PRUEBA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso/admisión de paciente; Fecha de ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'PRUEBA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ADINGRESO: Devuelve todas las columnas de los ingresos cuya fecha de ingreso (IFECHAING) esté entre @FECHAINI y @FECHAFIN.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'PRUEBA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'PRUEBA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'PRUEBA';
-- GO
