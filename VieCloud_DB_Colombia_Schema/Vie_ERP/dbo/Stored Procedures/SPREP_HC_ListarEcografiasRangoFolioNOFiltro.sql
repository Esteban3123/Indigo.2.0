
CREATE PROCEDURE [dbo].[SPREP_HC_ListarEcografiasRangoFolioNOFiltro]
(
@CodigoPaciente Varchar(25),
@NumeroFolioInicial int,
@NumeroFolioFinal int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT  
	NUMEFOLIO AS 'NUMERO FOLIO',FECULTECO AS 'FECHA ULTIMA ECOGRAFIA', 
	concat(NUMSEMULT,'  Semanas') AS 'SEMANAS EN ULTIMA ECOGRAFIA', 
	FECHISPAC AS 'FECHA DE HISTORIA', CONCAT(NUMSEMHIS,'  Semanas') AS 'SEMANAS ACTUALES',
    UltrasoundObservations,UltrasoundForEstimatedDueDate,Percentile, Size,UpcomingDateOfParturitionByUltrasound,
	Weight AS peso,AmnioticFluidIndex ,MaximumVerticalPocket
FROM HCANTECOG WITH(NOLOCK)

WHERE IPCODPACI = @CodigoPaciente AND NUMEFOLIO BETWEEN @NumeroFolioInicial AND @NumeroFolioFinal

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los registros de ecografías obstétricas de una paciente embarazada dentro de un rango de folios de historia clínica, sin aplicar filtros adicionales de fecha ni estado. Consulta la tabla HCANTECOG para obtener los datos de cada control ecográfico: número de folio, fecha de la última ecografía, semanas de gestación en la última ecografía, fecha de la historia clínica, semanas actuales de embarazo, observaciones del ultrasonido, fecha probable de parto por ecografía, percentil fetal, tamaño, peso, índice de líquido amniótico y bolsillo vertical máximo. Se usa para reportes obstétricos que requieren revisar la evolución ecográfica de la paciente en un tramo específico de su historia clínica, identificada por su cédula o código de paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_ListarEcografiasRangoFolioNOFiltro';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_ListarEcografiasRangoFolioNOFiltro';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los datos de ecografías obstétricas registradas para un paciente dentro de un rango de folios, sin aplicar filtros adicionales.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarEcografiasRangoFolioNOFiltro';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el paciente identificado en HCANTECOG.; El rango de folios inicial/final debe estar definido para acotar la búsqueda.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarEcografiasRangoFolioNOFiltro';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna registros del paciente indicado y cuyo folio esté dentro del rango (inclusive).; Usa lectura sucia (WITH NOLOCK) para no bloquear la tabla de antecedentes ecográficos.; No aplica filtros adicionales de estado o vigencia: devuelve todas las ecografías del rango sin discriminar.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarEcografiasRangoFolioNOFiltro';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; ecografía; folio de historia clínica; semanas de gestación; fecha probable de parto; líquido amniótico; peso fetal; percentil', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarEcografiasRangoFolioNOFiltro';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCANTECOG: Cuando IPCODPACI coincide con el paciente y NUMEFOLIO está entre el folio inicial y final, retorna el conjunto con datos de la ecografía (folio, fecha última ecografía, semanas, observaciones, FPP por ultrasonido, percentil, tamaño, peso, índice de líquido amniótico y bolsillo vertical máximo).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarEcografiasRangoFolioNOFiltro';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCANTECOG', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarEcografiasRangoFolioNOFiltro';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarEcografiasRangoFolioNOFiltro';
-- GO
