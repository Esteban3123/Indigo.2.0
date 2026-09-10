

CREATE PROCEDURE [dbo].[SPREP_HC_Generales_TableroHistorias]
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
          
SELECT ID, FECHISPAC AS 'FECHA HISTORIA CLINICA',CODCENATE AS 'CODIGO CENTRO DE ATENCION', UFUCODIGO AS 'CODIGO UNIDAD FUNCIONAL',INDICAMED AS 'INDICACIONES MEDICAS',
       CODUSUARI AS 'USUARIO QUE VISA LA HISTORIA', ESTAFOLIO AS ESTADO, FECVISREG AS 'FECHA VISADO', AssistedConsultationProfessional AS 'MEDICORESIDENTE',
	   HCIRENAL,CASE CONCILIACIONMED  WHEN '1' THEN 'X' END AS 'CONCILIACION_SI',CASE CONCILIACIONMED WHEN '0' THEN 'X' END AS 'CONCILIACION_NO', PLAINDMED AS 'PLANTILLA_RECOMENDACIONES'
FROM HCHISPACA with(noLock)
WHERE IPCODPACI=@CodigoPaciente AND NUMINGRES=@NumeroIngreso AND NUMEFOLIO=@NumeroFolio

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que consulta el tablero de historias clínicas de un paciente específico, retornando los datos generales de un folio particular dentro de un ingreso determinado. Recibe como parámetros la cédula del paciente, el número de ingreso y el número de folio, y devuelve información clave del documento clínico: fecha de la historia, centro de atención, unidad funcional, indicaciones médicas del profesional, usuario que visó el folio, estado del folio y fecha de visado. Consulta directamente la tabla de historias clínicas HCHISPACA y se utiliza para alimentar tableros o paneles de visualización de la historia clínica del paciente en el contexto de una atención u hospitalización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_TableroHistorias';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_TableroHistorias';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los datos generales de la historia clínica de un paciente para mostrarlos en un tablero de historias, identificando la atención por paciente, ingreso y folio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_TableroHistorias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener una historia clínica registrada con el folio e ingreso indicados en HCHISPACA.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_TableroHistorias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La consulta se realiza con NOLOCK, permitiendo lecturas sucias.; El resultado se filtra siempre por la combinación paciente + ingreso + folio (clave de identificación de la historia).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_TableroHistorias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Historia clínica; Paciente; Folio; Ingreso; Centro de atención; Unidad funcional; Indicaciones médicas; Visado de historia; Estado del folio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_TableroHistorias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCHISPACA: Cuando coinciden paciente, número de ingreso y número de folio, se retorna fecha de la historia, centro de atención, unidad funcional, indicaciones médicas, usuario que visa, estado del folio y fecha de visado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_TableroHistorias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_TableroHistorias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_TableroHistorias';
-- GO
