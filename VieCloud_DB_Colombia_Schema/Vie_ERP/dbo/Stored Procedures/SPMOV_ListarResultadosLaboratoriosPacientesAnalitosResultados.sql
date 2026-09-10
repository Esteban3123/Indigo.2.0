CREATE PROCEDURE [dbo].[SPMOV_ListarResultadosLaboratoriosPacientesAnalitosResultados]
(
@CodigoAuto Char(10),
@Paciente Varchar(25),
@Ingreso Char(20),
@Clasificacion Char(60),
@Analito Char(70)
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT  RTRIM(ANALITO) as ANALITO  ,NUMMUESTRA, VALOR, UNIDAD, VALORMINIMO, VALORMAXIMO, a.FECORDMED
		  FROM HCORDLABO A INNER JOIN
  INTERLABD B ON A.AUTO=B.AUTOLABOR
  Where a.IPCODPACI=@Paciente and a.NUMINGRES=@Ingreso and a.CODSERIPS=@CodigoAuto and b.CLASIFICACION=@Clasificacion and b.ANALITO=@Analito

end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que consulta y devuelve los resultados detallados de un analito específico (prueba individual de laboratorio) para un paciente y un ingreso determinados. Combina las órdenes médicas de laboratorio (HCORDLABO) con el detalle de resultados por analito (INTERLABD) para recuperar el valor obtenido, unidad de medida, rangos de referencia mínimo y máximo, número de muestra y fecha de la orden. Se filtra por cédula del paciente, número de ingreso, código del servicio de laboratorio (CUPS), clasificación del analito y nombre del analito, siendo útil para visualizar en la historia clínica el resultado puntual de un examen de laboratorio solicitado durante una atención hospitalaria o ambulatoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarResultadosLaboratoriosPacientesAnalitosResultados';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarResultadosLaboratoriosPacientesAnalitosResultados';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta los resultados de laboratorio (valor, unidad, rangos de referencia y fecha) de un paciente para un ingreso, orden, clasificación y analito específicos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarResultadosLaboratoriosPacientesAnalitosResultados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros en la orden de laboratorio (HCORDLABO) que coincidan con paciente, ingreso y código de servicio.; Debe existir el detalle de interlab (INTERLABD) asociado por AUTO=AUTOLABOR con la clasificación y analito indicados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarResultadosLaboratoriosPacientesAnalitosResultados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna resultados de laboratorio cuyo encabezado (orden) y detalle (analito) están vinculados por la llave AUTO=AUTOLABOR.; Filtra estrictamente por paciente, ingreso y código de servicio/orden, garantizando aislamiento de resultados por episodio asistencial.; El nombre del analito se devuelve sin espacios a la derecha (RTRIM).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarResultadosLaboratoriosPacientesAnalitosResultados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Resultados de laboratorio; Paciente; Ingreso; Analito; Clasificación de laboratorio; Orden médica de laboratorio; Muestra; Valores de referencia (mínimo/máximo)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarResultadosLaboratoriosPacientesAnalitosResultados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCORDLABO: Cuando coinciden paciente, ingreso, código de servicio, clasificación y analito en el INNER JOIN HCORDLABO-INTERLABD, retorna analito, número de muestra, valor, unidad, valores mínimo/máximo y fecha de orden médica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarResultadosLaboratoriosPacientesAnalitosResultados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABO; dbo.INTERLABD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarResultadosLaboratoriosPacientesAnalitosResultados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarResultadosLaboratoriosPacientesAnalitosResultados';
-- GO
