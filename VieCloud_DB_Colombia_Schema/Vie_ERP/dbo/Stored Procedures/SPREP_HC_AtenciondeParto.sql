
CREATE PROCEDURE [dbo].[SPREP_HC_AtenciondeParto]
(
@CodigoPaciente Varchar(25),
@NumeroFolio nChar(10),
@NumeroIngreso Char(10)
)
AS
BEGIN

	SET NOCOUNT ON;

	SELECT 
		  NUMEFOLIO AS 'NUMERO FOLIO', IPCODPACI AS 'CODGIO PACIENTE',
		  INITRAPAR AS 'INICIO TRABAJO PARTO', TERTRAPAR AS 'FIN TRABAJO PARTO',
		  PRESENTAC AS PRESENTACION, NUMEROFET AS 'NUMERO DE FETOS',
		  CONCAT(TIERUPMEM,'  Horas') AS 'RUPTURA MEMBRANA', DESLIQAMN AS 'LIQUIDO AMNIOTICO', 
		  CASE WHEN EPISIOTIM='True' THEN 'Si' ELSE 'No' END AS EPISIOTOMIA, PREDESGAR AS DESGARRO, 
		  VALOGRADO AS GRADO, CONTAPIEL AS 'CONTACTO PIEL A PIEL', PINZACORD AS 'PINZAMIENTO CORDON', 
		  CASE WHEN ALUMBRACT='True' THEN 'Si' ELSE 'No' END AS 'ALUMBRAMIENTO ACTIVO',
		  TIPALUMBR AS 'TIPO ALUMBRAMIENTO', DESPLACEN AS PLACENTA, 
		  CASE WHEN REVISUTER='True' THEN 'Si' ELSE 'No' END AS 'REVISION UTERINA', 
		  CANTSANGR AS 'SANGRADO APROXIMADO',
		  CASE WHEN CODIGROJO='True' THEN 'Si' ELSE 'No' END AS 'CODIGO ROJO', 
		  OBSERVACI AS OBSERVACIONES,ANALISISP AS ANALISIS,INDIEPISI,
		  CASE MultiplePregnancy WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END AS MultiplePregnancy, Analgesia,
		  CASE FamilyAccompaniment WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END AS FamilyAccompaniment,
		  Format(ChildbirthDateAndTime, 'dd/MM/yyy HH:mm') AS FechaHoraParto,
		  RTRIM(CompanionName) AS CompanionName,
		  RTRIM(CompanionRelationship) AS CompanionRelationship,
		--   Case PreparationCourse WHEN 1 THEN 'Extrainstitucional' 
        --     WHEN 2 THEN 'Institucional regular' 
        --     WHEN 3 THEN 'Institucional rápido (Express)' 
        --     WHEN 4 THEN 'No asistió' END AS CompanionName,
		  CASE ReceivedAnalgesia WHEN 1 THEN 'Si' ELSE 'No' END AS ReceivedAnalgesia,
		  RTRIM(AnesthesiaType) AS AnesthesiaType,
		  CASE SkinToSkinBreastfeeding WHEN 1 THEN 'Si' ELSE 'No' END AS SkinToSkinBreastfeeding,
		  RTRIM(WhyNotBreastfeed) AS WhyNotBreastfeed
	FROM HCATINPAR with(nolock)
	WHERE NUMEFOLIO = @NumeroFolio AND IPCODPACI = @CodigoPaciente AND NUMINGRES = @NumeroIngreso
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera el registro clínico completo de la atención del parto de una paciente, dado su código (cédula), número de folio e ingreso. Consulta la tabla HCATINPAR y retorna los detalles del trabajo de parto: inicio y fin del trabajo de parto, presentación fetal, número de fetos, ruptura de membranas, líquido amniótico, episiotomía, desgarros y su grado, contacto piel a piel, pinzamiento de cordón, alumbramiento, revisión uterina, sangrado estimado, código rojo obstétrico, analgesia recibida, tipo de anestesia, acompañamiento familiar, nombre y relación del acompañante, curso de preparación para el parto, lactancia materna piel a piel y fecha y hora exacta del nacimiento. Se usa para visualizar e imprimir el formulario clínico del parto en la historia clínica de la paciente obstétrica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_AtenciondeParto';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_AtenciondeParto';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consultar y formatear los datos clínicos de la atención de parto asociada a un paciente, folio e ingreso específicos para su visualización en la historia clínica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AtenciondeParto';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en la tabla de atención de parto que coincida con el folio, código de paciente e ingreso indicados.; Los identificadores de folio, paciente e ingreso deben corresponder a la misma atención de parto.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AtenciondeParto';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna el registro de atención de parto que coincide simultáneamente con folio, paciente e ingreso.; Los campos booleanos (episiotomía, alumbramiento activo, revisión uterina, código rojo, embarazo múltiple, acompañamiento familiar, analgesia recibida, contacto piel a piel para lactancia) se traducen a ''Si''/''No'' para presentación.; El tiempo de ruptura de membrana se presenta concatenado con el sufijo ''Horas''.; La fecha y hora del parto se formatea como ''dd/MM/yyy HH:mm''.; Se aplica RTRIM a campos de texto del acompañante, tipo de anestesia y motivo de no lactancia para eliminar espacios finales.; Se utiliza NOLOCK, por lo que pueden leerse datos no confirmados (lectura sucia).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AtenciondeParto';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Atención de parto; Trabajo de parto; Ruptura de membrana; Líquido amniótico; Episiotomía; Desgarro; Alumbramiento; Placenta; Revisión uterina; Sangrado; Código rojo; Embarazo múltiple; Analgesia; Tipo de anestesia; Acompañante familiar; Contacto piel a piel; Lactancia materna; Pinzamiento de cordón', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AtenciondeParto';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCATINPAR: Cuando NUMEFOLIO, IPCODPACI y NUMINGRES coinciden con los parámetros, se devuelve un conjunto de resultados con la información formateada de la atención de parto.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AtenciondeParto';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCATINPAR', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AtenciondeParto';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AtenciondeParto';
-- GO
