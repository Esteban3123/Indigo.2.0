
CREATE PROCEDURE [dbo].[SPREP_HC_AntecedentesGinecoObstetricosRangoFolio]
(
@CodigoPaciente Varchar(25),
@NumeroFolioInicial int,
@NumeroFolioFinal int,
@NumeroIngreso Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
 SELECT NUMEFOLIO AS 'NUMERO FOLIO',ChagasDate,Chagas, RetrovaginalCultureDate,RetrovaginalCulture,ApplicationQuarterDate,concat(MENARQUIA,'  años') AS MENARQUIA,CONCAT(CICLOSPAC,' / ',MENSTRDUR,' días') AS CICLOS, DateLastHumanPapillomavirusTest,
 concat(PaternalBloodType,PaternalHR)AS GrupoSanguineoPaterno,concat(MaternalBloodType,MaternalHR) AS GrupoSanguineoMaterno,
 case IVEConsultancy when 1 then 'Si' when 2 then 'No' end as IVEConsultancy,GestationalNumber,
 CASE ApplicationQuarter WHEN 1 THEN 'Primer trimestre' WHEN 2 THEN 'Segundo trimestre' WHEN 3 THEN 'Tercer trimestre' END AS ApplicationQuarter,
 case DateListHumanPapillomavirusTestReport when 1 then 'Positivo' when 2 then 'Negativo' end as DateListHumanPapillomavirusTestReport,
 case DateLastCytologyReport when 1 then 'Normal' when 2 then 'Anormal' end AS DateLastCytologyReport,CICLOSPAC AS CICLOS,MENSTRDUR AS 'DURACION MENSTRUACION',CASE WHEN CICLOREGU='True' THEN 'Si' ELSE 'No' END AS 'ES CICLO REGULAR', 
 concat(EDADVIDSE, '  años') AS 'EDAD VIDA SEXUAL', GESTACION AS 'NUMERO GESTACIONES', NUMCESARE AS 'NUMERO DE CESAREAS',
 NUMABORTO AS 'NUMERO de ABORTOS', NUMHIJVIV AS 'NUMERO DE HIJOS VIVOS', NUMETOPIC AS 'NUMERO EMBARAZOS ETOPICOS', NUMPARTO AS 'NUMERO DE PARTOS', NUMMORTIN AS 'NUMERO DE MORTINATOS', FECULTMEN AS 'FUM', FECULTPAR AS 'FUP',FECULTCIT AS 'FUC',  RTRIM(C.DESPLANIF) AS 'DESCRIPCION PLANIFICACION',
 CASE WHEN CONTPRENA ='True' THEN 'Si' ELSE 'No' END AS 'CONTROL PRENATAL', CANTPRENA AS 'CANTIDAD CONTROL PRENATAL',  concat(INICONPRE,'  semanas') AS 'INICIO CONTROL PRENATAL',CONCAT(NOMSEMGES,'  semanas') AS 'SEMANAS DE GESTACION', CASE WHEN RESULTHIV='True' THEN 'Positivo' WHEN RESULTHIV='False' THEN 'Negativo' ELSE 'No Tiene' END AS 'RESULTADO HIV', 
 CASE WHEN IQMTOXOPL='1' THEN 'Positivo' WHEN IQMTOXOPL='2' THEN 'Negativo' ELSE 'No Tiene' END AS 'IQM TOXOPLASMA', FECULTIQM AS 'FECHA ULTIMO IQM', CASE WHEN IGGTOXOPL='True' THEN 'Positivo' WHEN IGGTOXOPL='False' THEN 'Negativo' ELSE 'No Tiene' END AS 'IQG TOXOPLASMA', 
 CANTTOXO AS 'CANTIDAD IQG', FECULTIGG AS 'FECHA ULTIMO IQG', CASE WHEN HEPATITIB='1' THEN 'Positivo' WHEN HEPATITIB='2' THEN 'Negativo' ELSE 'No Tiene' END AS 'HEPATITIS B', CANTHEPAT AS 'CANTIDAD HEPATITIS B', CASE WHEN RESULVDRL='True' THEN 'Reactivo' WHEN IGGTOXOPL='False' THEN 'NO Reactivo' ELSE 'No Tiene' END AS 'RESULTADO VDRL',
 DILUCVDRL AS DILUSIONES, RIESOBTET AS 'RIESGOS OBSTETRICOS',RESCUAHEM AS 'CUADRO HEMATICO',RESPARORI AS 'PARCIAL DE ORINA', TESTSULLI AS 'TEST SULLIVAN', OTROSANTE AS 'OTROS ANTECEDENTES',GLUCBASAL AS 'GLUCEMIA BASAL', NUMEMOLAS AS 'NUMERO DE MOLAS',OTROSOBST,NUMEOVITO,
 
 IIF(ContraceptiveMethodChosen IS NULL,'',  D.DESPLANIF) AS ContraceptiveMethodChosen,	   	
 IIF(PuerperiumAttentionObservations IS NULL,'Sin registro',PuerperiumAttentionObservations) AS PuerperiumAttentionObservations

FROM HCANTGINE A WITH(NOLOCK)
INNER JOIN INPROFSAL B WITH(NOLOCK) ON A.CODPROSAL=B.CODPROSAL 
LEFT OUTER JOIN HCTIPPLAN C WITH(NOLOCK) ON A.CODPLANIF=C.CODPLANIF
LEFT OUTER JOIN HCTIPPLAN D With(Nolock) ON A.ContraceptiveMethodChosen = D.CODPLANIF 

WHERE A.IPCODPACI = @CodigoPaciente AND A.NUMINGRES = @NumeroIngreso AND A.NUMEFOLIO BETWEEN @NumeroFolioInicial AND @NumeroFolioFinal

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de antecedentes ginecológicos y obstétricos de una paciente para un rango de folios de historia clínica. Recupera información prenatal, reproductiva y de tamizaje de la tabla HCANTGINE (antecedentes gineco-obstétricos), enriquecida con el nombre del profesional de salud responsable desde INPROFSAL y la descripción del método de planificación familiar desde HCTIPPLAN. Consolida datos como menarquia, ciclos menstruales, número de gestaciones, partos, abortos, cesáreas, embarazos ectópicos, mortinatos, hijos vivos, fechas de última menstruación/parto/citología, resultados de laboratorios prenatales (VIH, toxoplasma, VDRL, hepatitis B, Chagas, papilomavirus, cultivo retrovaginal), control prenatal, semanas de gestación, grupo sanguíneo materno y paterno, método anticonceptivo elegido y observaciones del puerperio. Se usa para imprimir o visualizar la historia gineco-obstétrica completa de una paciente durante un ingreso específico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_AntecedentesGinecoObstetricosRangoFolio';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_AntecedentesGinecoObstetricosRangoFolio';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta los antecedentes gineco-obstétricos registrados para un paciente en un ingreso específico, dentro de un rango de folios de historia clínica, formateando los valores codificados a etiquetas legibles.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AntecedentesGinecoObstetricosRangoFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir y tener antecedentes gineco-obstétricos registrados en HCANTGINE para el ingreso indicado.; Debe existir el profesional de salud asociado en INPROFSAL (INNER JOIN obligatorio).; El rango de folios inicial/final debe ser válido para filtrar con BETWEEN.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AntecedentesGinecoObstetricosRangoFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna registros del paciente e ingreso indicados, dentro del rango de folios solicitado.; Las claves codificadas (1/2, True/False) se exponen siempre con etiquetas legibles en español.; El método anticonceptivo elegido y la planificación se resuelven contra HCTIPPLAN mediante LEFT JOIN, por lo que la ausencia de plan no excluye el registro.; No realiza modificaciones de datos; es solo de lectura con NOLOCK en todas las tablas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AntecedentesGinecoObstetricosRangoFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Antecedentes gineco-obstétricos; Menarquia; Ciclo menstrual; Gestaciones; Cesáreas; Abortos; Hijos vivos; Embarazos ectópicos; Partos; Mortinatos; FUM (fecha última menstruación); FUP (fecha último parto); FUC (fecha última citología); Planificación familiar / método anticonceptivo; Control prenatal; Semanas de gestación; HIV; Toxoplasmosis (IQM/IGG); Hepatitis B; VDRL; Chagas; Cultivo recto-vaginal; Virus del Papiloma Humano (VPH); Citología; Grupo sanguíneo materno y paterno; IVE (Interrupción Voluntaria del Embarazo); Riesgo obstétrico; Cuadro hemático; Parcial de orina; Test de Sullivan (+4 adicionales)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AntecedentesGinecoObstetricosRangoFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCANTGINE: Devuelve resultset filtrando por IPCODPACI = paciente, NUMINGRES = ingreso y NUMEFOLIO BETWEEN folio inicial y final.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AntecedentesGinecoObstetricosRangoFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IVEConsultancy = 1 / 2 → Mapea a ''Si'' / ''No'' respectivamente.; si ApplicationQuarter = 1, 2 o 3 → Mapea a ''Primer trimestre'', ''Segundo trimestre'' o ''Tercer trimestre''.; si DateListHumanPapillomavirusTestReport = 1 / 2 → Mapea a ''Positivo'' / ''Negativo''.; si DateLastCytologyReport = 1 / 2 → Mapea a ''Normal'' / ''Anormal''.; si CICLOREGU = ''True'' → Reporta ciclo regular como ''Si''. else ''No''.; si CONTPRENA = ''True'' → Reporta control prenatal como ''Si''. else ''No''.; si RESULTHIV = ''True'' / ''False'' → Mapea a ''Positivo'' / ''Negativo''. else ''No Tiene'' cuando es nulo u otro valor.; si IQMTOXOPL = ''1'' / ''2'' → Mapea a ''Positivo'' / ''Negativo''. else ''No Tiene''.; si IGGTOXOPL = ''True'' / ''False'' → Mapea a ''Positivo'' / ''Negativo'' para IQG Toxoplasma. else ''No Tiene''.; si HEPATITIB = ''1'' / ''2'' → Mapea a ''Positivo'' / ''Negativo''. else ''No Tiene''.; si RESULVDRL = ''True'' o IGGTOXOPL = ''False'' → Mapea VDRL a ''Reactivo'' / ''NO Reactivo'' (la rama negativa usa IGGTOXOPL en lugar de RESULVDRL). else ''No Tiene''.; si ContraceptiveMethodChosen IS NULL → Devuelve cadena vacía. else Devuelve la descripción del plan de planificación (D.DESPLANIF).; si PuerperiumAttentionObservations IS NULL → Devuelve ''Sin registro''. else Devuelve el valor original.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AntecedentesGinecoObstetricosRangoFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCANTGINE; dbo.INPROFSAL; dbo.HCTIPPLAN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AntecedentesGinecoObstetricosRangoFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AntecedentesGinecoObstetricosRangoFolio';
-- GO
