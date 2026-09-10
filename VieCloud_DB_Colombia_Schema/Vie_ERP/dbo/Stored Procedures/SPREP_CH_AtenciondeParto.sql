
CREATE PROCEDURE [dbo].[SPREP_CH_AtenciondeParto]
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
SELECT NUMEFOLIO AS 'NUMERO FOLIO', IPCODPACI AS 'CODGIO PACIENTE', INITRAPAR AS 'INICIO TRABAJO PARTO', TERTRAPAR AS 'FIN TRABAJO PARTO', PRESENTAC AS PRESENTACION, NUMEROFET AS 'NUMERO DE FETOS',TIERUPMEM AS 'RUPTURA MEMBRANA', DESLIQAMN AS 'LIQUIDO AMNIOTICO', EPISIOTIM AS EPISIOTOMIA,PREDESGAR AS DESGARRO, VALOGRADO AS GRADO, CONTAPIEL AS 'CONTACTO PIEL A PIEL', PINZACORD AS 'PINZAMIENTO CORDON', ALUMBRACT AS 'ALUMBRAMIENTO ACTIVO', TIPALUMBR AS 'TIPO ALUMBRAMIENTO', DESPLACEN AS PLACENTA, REVISUTER AS 'REVISION UTERINA', CANTSANGR AS 'SANGRADO APROXIMADO', CODIGROJO AS 'CODIGO ROJO', OBSERVACI AS OBSERVACIONES,ANALISISP AS ANALISIS
FROM HCATINPAR WITH(NOLOCK)

WHERE NUMEFOLIO = @NumeroFolio AND IPCODPACI = @CodigoPaciente

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera el registro completo de atención del parto de una paciente, consultando la historia clínica de trabajo de parto y parto (tabla HCATINPAR). Recibe como parámetros el número de folio y el código o cédula de la paciente, y retorna información clínica detallada del evento obstétrico: inicio y fin del trabajo de parto, presentación fetal, número de fetos, ruptura de membranas, líquido amniótico, episiotomía, desgarros, contacto piel a piel, pinzamiento del cordón, tipo de alumbramiento, revisión uterina, sangrado aproximado, activación de código rojo, observaciones y análisis del parto. Se usa para generar el reporte o impresión de la atención del parto en la historia clínica de la paciente obstétrica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_CH_AtenciondeParto';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_CH_AtenciondeParto';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera los datos clínicos registrados sobre la atención del parto de un paciente para un folio específico, devolviéndolos como reporte legible.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_AtenciondeParto';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCATINPAR que coincida simultáneamente con el folio y el código de paciente recibidos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_AtenciondeParto';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La consulta usa WITH(NOLOCK), por lo que admite lecturas sucias.; La identificación del registro siempre se realiza por la combinación folio + código de paciente.; El procedimiento no modifica datos; es solo de lectura.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_AtenciondeParto';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Atención de parto; Trabajo de parto; Presentación fetal; Ruptura de membrana; Líquido amniótico; Episiotomía; Desgarro perineal; Contacto piel a piel; Pinzamiento de cordón; Alumbramiento; Placenta; Revisión uterina; Sangrado; Código rojo obstétrico; Folio; Paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_AtenciondeParto';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCATINPAR: Cuando NUMEFOLIO y IPCODPACI coinciden con los parámetros, se retorna la información de la atención de parto (trabajo de parto, presentación, fetos, ruptura de membrana, líquido amniótico, episiotomía, desgarro, alumbramiento, placenta, sangrado, código rojo, etc.).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_AtenciondeParto';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCATINPAR', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_AtenciondeParto';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_AtenciondeParto';
-- GO
