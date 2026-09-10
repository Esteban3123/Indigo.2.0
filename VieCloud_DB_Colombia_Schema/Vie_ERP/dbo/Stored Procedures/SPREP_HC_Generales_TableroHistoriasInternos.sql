
CREATE PROCEDURE [dbo].[SPREP_HC_Generales_TableroHistoriasInternos]
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
          
SELECT FECHISPAC AS 'FECHA HISTORIA CLINICA',CODCENATE AS 'CODIGO CENTRO DE ATENCION', UFUCODIGO AS 'CODIGO UNIDAD FUNCIONAL',INDICAMED AS 'INDICACIONES MEDICAS',
       CODUSUARI AS 'USUARIO QUE VISA LA HISTORIA', ESTAFOLIO AS ESTADO
FROM HCHISPACI WITH(NOLOCK)
WHERE IPCODPACI=@CodigoPaciente AND NUMINGRES=@NumeroIngreso AND NUMEFOLIO=@NumeroFolio

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que consulta el tablero de historias clínicas internas de pacientes hospitalizados o en atención. Dado el código del paciente (cédula), el número de ingreso y el número de folio, recupera los datos generales de un folio de historia clínica específico: fecha de registro, centro de atención, unidad funcional, indicaciones médicas, usuario que visó la historia y estado del folio. Sirve para que el personal clínico o administrativo visualice el detalle de una evolución o consulta médica documentada dentro del módulo de historia clínica (HC).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_TableroHistoriasInternos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_TableroHistoriasInternos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera los datos de la historia clínica interna de un paciente para mostrar en un tablero, identificada por folio e ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_TableroHistoriasInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente, ingreso y folio deben existir en HCHISPACI para retornar resultados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_TableroHistoriasInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna registros de historia clínica que coincidan exactamente con paciente, ingreso y folio; Lectura no bloqueante (NOLOCK) sobre la historia clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_TableroHistoriasInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'historia clínica; paciente; ingreso; folio; centro de atención; unidad funcional; indicaciones médicas; visación de historia; estado de folio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_TableroHistoriasInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCHISPACI: Devuelve fecha, centro de atención, unidad funcional, indicaciones médicas, usuario visador y estado del folio cuando paciente, ingreso y folio coinciden', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_TableroHistoriasInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_TableroHistoriasInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_TableroHistoriasInternos';
-- GO
