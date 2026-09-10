
CREATE PROCEDURE [dbo].[SPREP_CH_ListarEcografias]
(
@NumeroFolio nChar(10),
@CodigoPaciente Varchar(25)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT  FECULTECO AS 'FECHA ULTIMA ECOGRAFIA', NUMSEMULT AS 'SEMANAS EN ULTIMA ECOGRAFIA', FECHISPAC AS 'FECHA DE HISTORIA', NUMSEMHIS AS 'SEMANAS ACTUALES'
FROM HCANTECOG WITH(NOLOCK)
--where IDETPHIS???

WHERE NUMEFOLIO = @NumeroFolio AND IPCODPACI = @CodigoPaciente

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los registros de ecografías obstétricas de una paciente embarazada, consultando la tabla de antecedentes ecográficos (HCANTECOG). Recibe como parámetros el número de folio de la historia clínica y el código o cédula de la paciente, y retorna la fecha de la última ecografía realizada, las semanas de gestación en ese momento, la fecha de registro en la historia clínica y las semanas de gestación actuales. Se utiliza para visualizar el seguimiento ecográfico prenatal de la paciente dentro de su historia clínica obstétrica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_CH_ListarEcografias';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_CH_ListarEcografias';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los antecedentes ecográficos de un paciente (fecha y semanas de última ecografía vs. fecha y semanas actuales de historia) asociados a un folio específico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_ListarEcografias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el folio y el código de paciente en la tabla de antecedentes ecográficos para retornar filas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_ListarEcografias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las consultas se hacen con NOLOCK (lectura sucia permitida).; Solo se retornan registros que coincidan simultáneamente en folio y código de paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_ListarEcografias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; ecografía; folio; historia clínica; semanas de gestación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_ListarEcografias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCANTECOG: Cuando NUMEFOLIO coincide con el folio recibido y IPCODPACI coincide con el código de paciente, devuelve fecha/semana de última ecografía y fecha/semanas actuales de historia.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_ListarEcografias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCANTECOG', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_ListarEcografias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_ListarEcografias';
-- GO
