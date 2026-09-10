

CREATE PROCEDURE [dbo].[SPREP_CH_AntecedentesGinecoObstetricos]
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
SELECT NUMEFOLIO ,MENARQUIA, CICLOSPAC AS CICLOS,MENSTRDUR AS 'DURACION MENSTRUACION', CICLOREGU AS 'ES CICLO REGULAR', EDADVIDSE AS 'EDAD VIDA SEXUAL', GESTACION AS 'NUMERO GESTACIONES', NUMCESARE AS 'NUMERO DE CESAREAS', NUMABORTO AS 'NUMERO de ABORTOS', NUMHIJVIV AS 'NUMERO DE HIJOS VIVOS', NUMETOPIC AS 'NUMERO EMBARAZOS ETOPICOS', NUMMORTIN AS 'NUMERO DE MORTINATOS', FECULTMEN AS 'FUM', FECULTPAR AS 'FUP',FECULTCIT AS 'FUC', RTRIM(DESPLANIF) AS 'DESCRIPCION PLANIFICACION',CASE WHEN CONTPRENA ='1' THEN 'Si' ELSE 'No' END AS 'CONTROL PRENATAL', CANTPRENA AS 'CANTIDAD CONTROL PRENATAL', NOMSEMGES AS 'SEMANAS DE GESTACION', RESULTHIV AS 'RESULTADO HIV', IQMTOXOPL AS 'IQM TOXOPLASMA', FECULTIQM AS 'FECHA ULTIMO IQM',IGGTOXOPL AS 'IQG TOXOPLASMA', CANTTOXO AS 'CANTIDAD IQG', FECULTIGG AS 'FECHA ULTIMO IQG', HEPATITIB AS 'HEPATITIS B', CANTHEPAT AS 'CANTIDAD HEPATITIS B', RESULVDRL AS 'RESULTADO VDRL', DILUCVDRL AS DILUSIONES, RIESOBTET AS 'RIESGOS OBSTETRICOS', RESCUAHEM AS 'CUADRO HEMATICO',RESPARORI AS 'PARCIAL DE ORINA', TESTSULLI AS 'TEST SULLIVAN', OTROSANTE AS 'OTROS ANTECEDENTES',  FECPROPAR AS 'FECHA PROBABLE PARTO'
FROM HCANTGINE A WITH(NOLOCK)
INNER JOIN INPROFSAL B WITH(NOLOCK) ON A.CODPROSAL=B.CODPROSAL 
LEFT OUTER JOIN HCTIPPLAN C WITH(NOLOCK) ON A.CODPLANIF=C.CODPLANIF

WHERE A.NUMEFOLIO = @NumeroFolio AND A.IPCODPACI = @CodigoPaciente

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta y devuelve el resumen completo de antecedentes ginecológicos y obstétricos de una paciente, identificada por su número de folio de historia clínica y código de paciente (cédula). Extrae de la historia clínica ginecológica (HCANTGINE) datos como menarquia, ciclos menstruales, número de gestaciones, partos, cesáreas, abortos, embarazos ectópicos, mortinatos, hijos vivos, fecha de última menstruación, última citología y último parto, junto con información de control prenatal, semanas de gestación y resultados de laboratorio prenatal (HIV, toxoplasma IgM e IgG, hepatitis B, VDRL, cuadro hemático, parcial de orina, test de Sullivan) y riesgos obstétricos. Complementa la información con el método de planificación familiar (HCTIPPLAN) y los datos del profesional tratante (INPROFSAL). Este procedimiento es utilizado para la generación de reportes clínicos y visualización del historial reproductivo de la paciente en la historia clínica electrónica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_CH_AntecedentesGinecoObstetricos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_CH_AntecedentesGinecoObstetricos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera los antecedentes gineco-obstétricos registrados para un paciente en un folio clínico específico, presentándolos con etiquetas legibles para reportes.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_AntecedentesGinecoObstetricos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCANTGINE asociado al folio y paciente indicados; El profesional de salud referenciado debe existir en INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_AntecedentesGinecoObstetricos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El control prenatal siempre se traduce a un valor binario textual (''Si''/''No''); La descripción de planificación se devuelve sin espacios finales (RTRIM); La consulta del plan de planificación es opcional (LEFT OUTER JOIN), por lo que la ausencia de plan no excluye al registro; Lecturas con NOLOCK: pueden incluir datos no confirmados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_AntecedentesGinecoObstetricos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Antecedentes gineco-obstétricos; Menarquia; Ciclo menstrual; Vida sexual; Gestaciones; Cesáreas; Abortos; Hijos vivos; Embarazos ectópicos; Mortinatos; FUM (fecha última menstruación); FUP (fecha último parto); FUC (fecha última citología); Planificación familiar; Control prenatal; Semanas de gestación; HIV; Toxoplasma (IgM/IgG); Hepatitis B; VDRL; Riesgo obstétrico; Cuadro hemático; Parcial de orina; Test de Sullivan; Fecha probable de parto; Folio clínico; Paciente; Profesional de salud', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_AntecedentesGinecoObstetricos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCANTGINE: Cuando NUMEFOLIO=@NumeroFolio e IPCODPACI=@CodigoPaciente, devuelve los antecedentes gineco-obstétricos asociados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_AntecedentesGinecoObstetricos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CONTPRENA = ''1'' → Se reporta ''Si'' como control prenatal else Se reporta ''No'' como control prenatal', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_AntecedentesGinecoObstetricos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCANTGINE; dbo.INPROFSAL; dbo.HCTIPPLAN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_AntecedentesGinecoObstetricos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_AntecedentesGinecoObstetricos';
-- GO
