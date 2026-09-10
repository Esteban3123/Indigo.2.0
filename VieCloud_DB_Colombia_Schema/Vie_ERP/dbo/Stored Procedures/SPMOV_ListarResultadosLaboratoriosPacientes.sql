CREATE PROCEDURE [dbo].[SPMOV_ListarResultadosLaboratoriosPacientes]
(
@CodigoAuto Char(10),
@Paciente Varchar(25),
@Ingreso Char(20)
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT  rtrim(CLASIFICACION) 
		  FROM HCORDLABO A INNER JOIN
  INTERLABD B ON A.AUTO=B.AUTOLABOR
  Where a.IPCODPACI=@Paciente and a.NUMINGRES=@Ingreso and a.CODSERIPS=@CodigoAuto
  group by rtrim(CLASIFICACION) 
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que lista las clasificaciones de resultados de laboratorio disponibles para un paciente específico en un ingreso determinado. Recibe como parámetros la cédula del paciente, el número de ingreso y el código del servicio o examen de laboratorio (CUPS). Combina las órdenes de laboratorio de historia clínica (HCORDLABO) con el detalle de resultados por analito (INTERLABD) para obtener, sin duplicados, las categorías o clasificaciones de pruebas con resultado registrado. Se usa para consultar qué tipos de resultados de laboratorio existen para un paciente en una atención o ingreso hospitalario específico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarResultadosLaboratoriosPacientes';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarResultadosLaboratoriosPacientes';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las clasificaciones distintas de laboratorio asociadas a las órdenes de un paciente, para un ingreso y un código de servicio determinados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarResultadosLaboratoriosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos una orden de laboratorio para el paciente, ingreso y código de servicio indicados, con detalle asociado en la tabla de interfaz de laboratorio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarResultadosLaboratoriosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Devuelve clasificaciones únicas (agrupadas) sin espacios a la derecha.; Solo considera órdenes de laboratorio que tienen detalle relacionado en INTERLABD vía AUTO=AUTOLABOR (INNER JOIN excluye órdenes sin detalle).; El filtro exige coincidencia simultánea de paciente, ingreso y código de servicio/autorización.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarResultadosLaboratoriosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; ingreso; resultados de laboratorio; clasificación de laboratorio; orden de laboratorio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarResultadosLaboratoriosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCORDLABO: Cuando IPCODPACI=@Paciente, NUMINGRES=@Ingreso y CODSERIPS=@CodigoAuto, retorna las CLASIFICACION distintas (RTRIM y GROUP BY) provenientes de INTERLABD unida a HCORDLABO por AUTO=AUTOLABOR.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarResultadosLaboratoriosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABO; dbo.INTERLABD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarResultadosLaboratoriosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarResultadosLaboratoriosPacientes';
-- GO
