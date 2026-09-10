
CREATE PROCEDURE [dbo].[SPREP_HC_ListarEcografias]
(
@CodigoPaciente Varchar(25),
@NumeroFolio nChar(10),
@NumeroIngreso Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT  NUMEFOLIO AS 'NUMERO FOLIO',UltrasoundObservations,
        UltrasoundForEstimatedDueDate,Percentile, Size,
		FECULTECO AS 'FECHA ULTIMA ECOGRAFIA',
		UpcomingDateOfParturitionByUltrasound, 
		concat(NUMSEMULT,'  semanas')  AS 'SEMANAS EN ULTIMA ECOGRAFIA', 
		FECHISPAC AS 'FECHA DE HISTORIA', 
         CONCAT(NUMSEMHIS,'  semanas') AS 'SEMANAS ACTUALES',
		 Weight AS peso,AmnioticFluidIndex ,MaximumVerticalPocket
FROM HCANTECOG With(Nolock)
WHERE NUMEFOLIO = @NumeroFolio AND IPCODPACI = @CodigoPaciente AND NUMINGRES=@NumeroIngreso

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los registros de ecografías obstétricas de una paciente embarazada para un ingreso y folio de historia clínica específicos. Recupera desde la tabla HCANTECOG los datos de cada control ecográfico: semanas de gestación en la última ecografía, semanas actuales según historia, fecha de la última ecografía, fecha probable de parto estimada por ultrasonido, peso fetal, índice de líquido amniótico, bolsillo vertical máximo, percentiles, tamaño fetal y observaciones del ultrasonido. Se usa en la visualización del historial de ecografías dentro de la historia clínica perinatal u obstétrica de la paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_ListarEcografias';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_ListarEcografias';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar los antecedentes ecográficos obstétricos de un paciente (última ecografía, semanas, peso, líquido amniótico, percentil, fecha probable de parto) asociados a un folio e ingreso específicos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarEcografias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente, folio e ingreso deben existir y estar relacionados en los antecedentes ecográficos para retornar filas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarEcografias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna antecedentes ecográficos correspondientes a la combinación exacta de folio, paciente e ingreso (filtro AND).; Uso de NOLOCK: permite lecturas sucias sobre los antecedentes de ecografía.; No modifica datos; es una consulta de solo lectura.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarEcografias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; folio; ingreso; ecografía; historia clínica; antecedentes obstétricos; edad gestacional; líquido amniótico; percentil fetal; peso fetal', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarEcografias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCANTECOG: Cuando NUMEFOLIO, IPCODPACI y NUMINGRES coinciden con los parámetros, se retorna el conjunto de datos ecográficos (observaciones, fecha y semanas de última ecografía, fecha probable de parto, peso, índice de líquido amniótico, bolsillo vertical máximo, percentil, tamaño, fecha de historia y semanas actuales).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarEcografias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCANTECOG', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarEcografias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarEcografias';
-- GO
