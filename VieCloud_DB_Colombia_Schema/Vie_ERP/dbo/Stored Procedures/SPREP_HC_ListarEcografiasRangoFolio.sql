
CREATE PROCEDURE [dbo].[SPREP_HC_ListarEcografiasRangoFolio]
(
@CodigoPaciente Varchar(25),
@NumeroFolioInicial int,
@NumeroFolioFinal int,
@NumeroIngreso char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT 
	NUMEFOLIO AS 'NUMERO FOLIO',FECULTECO AS 'FECHA ULTIMA ECOGRAFIA', 
	concat(NUMSEMULT,'  semanas')  AS 'SEMANAS EN ULTIMA ECOGRAFIA', 
	FECHISPAC AS 'FECHA DE HISTORIA', 
	CONCAT(NUMSEMHIS,'  semanas') AS 'SEMANAS ACTUALES',
	UltrasoundObservations,UltrasoundForEstimatedDueDate,Percentile, Size,UpcomingDateOfParturitionByUltrasound,
	Weight AS peso,AmnioticFluidIndex ,MaximumVerticalPocket
FROM HCANTECOG WITH(NOLOCK)

WHERE IPCODPACI = @CodigoPaciente AND NUMINGRES=@NumeroIngreso AND NUMEFOLIO BETWEEN @NumeroFolioInicial AND @NumeroFolioFinal

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los registros de ecografías obstétricas de una paciente embarazada dentro de un rango de folios de su historia clínica. Consulta la tabla HCANTECOG para obtener los datos de cada control ecográfico: fecha de la última ecografía, semanas de gestación (en la ecografía y actuales), observaciones del ultrasonido, fecha probable de parto por ultrasonido, percentil, tamaño, peso fetal, índice de líquido amniótico y bolsillo vertical máximo. Se utiliza en la visualización del control prenatal dentro de la historia clínica, filtrando por cédula de la paciente, número de ingreso y rango de folios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_ListarEcografiasRangoFolio';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_ListarEcografiasRangoFolio';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los antecedentes ecográficos obstétricos de un paciente en un ingreso específico, filtrados por un rango de folios de historia clínica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarEcografiasRangoFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el paciente y su ingreso en los antecedentes ecográficos para devolver filas.; El rango de folios inicial-final debe ser coherente (inicial ≤ final) para obtener resultados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarEcografiasRangoFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La consulta se restringe simultáneamente al paciente, al número de ingreso y al rango de folios indicado.; Los datos se leen con NOLOCK, permitiendo lecturas sucias sobre los antecedentes ecográficos.; Las semanas (última y actual) se devuelven concatenadas con el sufijo '' semanas'' como texto.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarEcografiasRangoFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; ingreso; folio de historia clínica; ecografía obstétrica; semanas de gestación; líquido amniótico; percentil; peso fetal; fecha probable de parto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarEcografiasRangoFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCANTECOG: Cuando el paciente, el ingreso y el folio están dentro del rango solicitado, retorna los datos ecográficos (fecha y semanas de la última ecografía, fecha e historia actual, observaciones, FPP por ecografía, percentil, tamaño, peso, índice de líquido amniótico y bolsillo vertical máximo).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarEcografiasRangoFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCANTECOG', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarEcografiasRangoFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarEcografiasRangoFolio';
-- GO
