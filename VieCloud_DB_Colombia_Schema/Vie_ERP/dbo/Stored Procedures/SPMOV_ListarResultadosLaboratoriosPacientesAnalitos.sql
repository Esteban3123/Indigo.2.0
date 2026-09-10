CREATE PROCEDURE [dbo].[SPMOV_ListarResultadosLaboratoriosPacientesAnalitos]
(
@CodigoAuto Char(10),
@Paciente Varchar(25),
@Ingreso Char(20),
@Clasificacion Char(60)
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT  RTRIM(ANALITO) as ANALITO  ,NUMMUESTRA, VALOR, UNIDAD, VALORMINIMO, VALORMAXIMO
		  FROM HCORDLABO A INNER JOIN
  INTERLABD B ON A.AUTO=B.AUTOLABOR
  Where a.IPCODPACI=@Paciente and a.NUMINGRES=@Ingreso and a.CODSERIPS=@CodigoAuto and b.CLASIFICACION=@Clasificacion

end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los resultados de laboratorio por analito (prueba individual) para un paciente, ingreso, examen y clasificación específicos. Cruza las órdenes médicas de laboratorio (HCORDLABO) con el detalle de resultados por analito (INTERLABD) para devolver el nombre del analito, número de muestra, valor obtenido, unidad de medida y rangos de referencia mínimo y máximo. Se usa en la historia clínica para visualizar los resultados de un examen de laboratorio concreto de un paciente, filtrando por cédula del paciente, número de ingreso, código del servicio o examen (CUPS) y clasificación del analito.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarResultadosLaboratoriosPacientesAnalitos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarResultadosLaboratoriosPacientesAnalitos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los resultados de laboratorio (analitos, valores y rangos de referencia) de un paciente para un ingreso, servicio y clasificación de prueba específicos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarResultadosLaboratoriosPacientesAnalitos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una orden de laboratorio en HCORDLABO asociada al paciente, ingreso y servicio indicados; Debe existir el detalle interlaboratorio en INTERLABD vinculado por AUTO=AUTOLABOR con la clasificación indicada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarResultadosLaboratoriosPacientesAnalitos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan resultados cuya orden de laboratorio pertenezca al paciente e ingreso indicados (filtro por IPCODPACI y NUMINGRES); El cruce entre orden y detalle se hace exclusivamente por la llave AUTO=AUTOLABOR; El analito se retorna sin espacios a la derecha (RTRIM)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarResultadosLaboratoriosPacientesAnalitos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Orden de laboratorio; Analito; Muestra de laboratorio; Valores de referencia (mínimo/máximo); Clasificación de prueba; Servicio IPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarResultadosLaboratoriosPacientesAnalitos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCORDLABO/INTERLABD: Cuando coinciden paciente, ingreso, servicio (CODSERIPS) y clasificación, retorna ANALITO, NUMMUESTRA, VALOR, UNIDAD, VALORMINIMO y VALORMAXIMO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarResultadosLaboratoriosPacientesAnalitos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABO; dbo.INTERLABD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarResultadosLaboratoriosPacientesAnalitos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarResultadosLaboratoriosPacientesAnalitos';
-- GO
