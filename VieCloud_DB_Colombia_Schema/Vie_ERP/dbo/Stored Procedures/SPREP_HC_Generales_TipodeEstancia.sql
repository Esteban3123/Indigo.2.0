

CREATE PROCEDURE [dbo].[SPREP_HC_Generales_TipodeEstancia]
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
          
SELECT RTRIM(DESTIPEST) AS 'DESCRIPCION TIPO DE ESTANCIA',
A.JUSCAMANT 'JUSTIFICACION'           
FROM HCREGEST A with(noLock)
INNER JOIN CHTIPESTA B with(noLock) ON A.CODTIPEST=B.CODTIPEST
WHERE IPCODPACI=@CodigoPaciente AND NUMINGRES=@NumeroIngreso AND NUMEFOLIO=@NumeroFolio

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de tipos de estancia registrados en la historia clínica de un paciente durante un ingreso hospitalario específico. Dado el código del paciente (cédula), el número de ingreso y el número de folio, consulta el historial de estados clínicos del paciente cruzando los registros de cambios de estancia con el catálogo de tipos de estancia, para obtener la descripción legible del tipo de estancia y la justificación del cambio registrada por el profesional. Se utiliza para visualizar en la historia clínica qué tipo de estancia (hospitalización, urgencias, UCI, etc.) tuvo el paciente en cada etapa de su ingreso y el motivo documentado del cambio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_TipodeEstancia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_TipodeEstancia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta la descripción del tipo de estancia y su justificación registrada en la historia clínica para un paciente, ingreso y folio específicos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_TipodeEstancia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro de estancia en HCREGEST que coincida con paciente, ingreso y folio dados.; El código de tipo de estancia en HCREGEST debe estar catalogado en CHTIPESTA.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_TipodeEstancia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna registros cuyo tipo de estancia esté parametrizado en el catálogo CHTIPESTA (INNER JOIN).; Lectura sin bloqueo (NOLOCK), puede leer datos no confirmados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_TipodeEstancia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Folio; Tipo de estancia; Justificación de cama; Historia clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_TipodeEstancia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCREGEST: Cuando IPCODPACI, NUMINGRES y NUMEFOLIO coinciden con los parámetros, retorna la descripción del tipo de estancia y la justificación de cama anterior.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_TipodeEstancia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCREGEST; dbo.CHTIPESTA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_TipodeEstancia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_TipodeEstancia';
-- GO
