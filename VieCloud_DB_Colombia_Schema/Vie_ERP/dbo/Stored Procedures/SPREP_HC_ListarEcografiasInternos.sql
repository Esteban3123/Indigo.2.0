
CREATE PROCEDURE [dbo].[SPREP_HC_ListarEcografiasInternos]
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
SELECT  NUMEFOLIO AS 'NUMERO FOLIO',FECULTECO AS 'FECHA ULTIMA ECOGRAFIA', NUMSEMULT AS 'SEMANAS EN ULTIMA ECOGRAFIA', FECHISPAC AS 'FECHA DE HISTORIA', NUMSEMHIS AS 'SEMANAS ACTUALES'
FROM HCANTECOI WITH(NOLOCK)

WHERE NUMEFOLIO = @NumeroFolio AND IPCODPACI = @CodigoPaciente AND NUMINGRES=@NumeroIngreso

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta los datos de ecografías registradas en la historia clínica obstétrica de un paciente hospitalizado, a partir de su código de paciente (cédula), número de ingreso y número de folio. Retorna el número de folio, la fecha de la última ecografía, las semanas de gestación en la última ecografía, la fecha de la historia clínica y las semanas de gestación actuales según la historia. Se utiliza para visualizar el seguimiento ecográfico prenatal de pacientes internos en el módulo de historia clínica obstétrica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_ListarEcografiasInternos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_ListarEcografiasInternos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los antecedentes de ecografías internas registrados para un paciente en un folio e ingreso específicos, mostrando fechas y semanas de gestación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarEcografiasInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente, folio e ingreso deben existir como combinación en los antecedentes de ecografías internas para retornar filas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarEcografiasInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna registros que cumplan simultáneamente el folio, paciente e ingreso indicados.; La consulta usa NOLOCK, por lo que admite lecturas sucias sin bloquear la tabla.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarEcografiasInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ecografía; Folio; Ingreso hospitalario; Historia clínica; Semanas de gestación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarEcografiasInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCANTECOI: Cuando coinciden folio, código de paciente e ingreso, se devuelven los datos de última ecografía (fecha, semanas) y datos actuales de la historia (fecha, semanas).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarEcografiasInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCANTECOI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarEcografiasInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarEcografiasInternos';
-- GO
