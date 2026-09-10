
CREATE PROCEDURE [dbo].[SPREP_HC_AntecedentesGinecoObstetricosRangoFolioNOFiltro]
(
@CodigoPaciente Varchar(25),
@NumeroFolioInicial int,
@NumeroFolioFinal int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
 SELECT NUMEFOLIO AS 'NUMERO FOLIO',ChagasDate,Chagas,DateLastHumanPapillomavirusTest,RetrovaginalCultureDate,RetrovaginalCulture,
 ApplicationQuarterDate,concat(PaternalBloodType,PaternalHR)AS GrupoSanguineoPaterno,concat(MaternalBloodType,MaternalHR) AS GrupoSanguineoMaterno,
 CASE ApplicationQuarter WHEN 1 THEN 'Primer trimestre' WHEN 2 THEN 'Segundo trimestre' WHEN 3 THEN 'Tercer trimestre' END AS ApplicationQuarter,
 case IVEConsultancy when 1 then 'Si' when 2 then 'No' end as IVEConsultancy,GestationalNumber,DateLastHumanPapillomavirusTest,
 case DateListHumanPapillomavirusTestReport when 1 then 'Positivo' when 2 then 'Negativo' end as DateListHumanPapillomavirusTestReport,
 case DateLastCytologyReport when 1 then 'Normal' when 2 then 'Anormal' end AS DateLastCytologyReport,
 concat(MENARQUIA,'  años') AS MENARQUIA,CONCAT(CICLOSPAC,' / ',MENSTRDUR,' días') AS CICLOS,MENSTRDUR AS 'DURACION MENSTRUACION',CASE WHEN CICLOREGU='True' THEN 'Si' ELSE 'No' END AS 'ES CICLO REGULAR',  concat(EDADVIDSE, '  Años') AS 'EDAD VIDA SEXUAL', GESTACION AS 'NUMERO GESTACIONES', NUMCESARE AS 'NUMERO DE CESAREAS',
 NUMABORTO AS 'NUMERO de ABORTOS', NUMHIJVIV AS 'NUMERO DE HIJOS VIVOS', NUMETOPIC AS 'NUMERO EMBARAZOS ETOPICOS', NUMPARTO AS 'NUMERO DE PARTOS', NUMMORTIN AS 'NUMERO DE MORTINATOS', FECULTMEN AS 'FUM', FECULTPAR AS 'FUP',FECULTCIT AS 'FUC',  RTRIM(DESPLANIF) AS 'DESCRIPCION PLANIFICACION',
 CASE WHEN CONTPRENA ='True' THEN 'Si' ELSE 'No' END AS 'CONTROL PRENATAL', CANTPRENA AS 'CANTIDAD CONTROL PRENATAL', concat(INICONPRE,'  semanas') AS 'INICIO CONTROL PRENATAL', CONCAT(NOMSEMGES,'  semanas') AS 'SEMANAS DE GESTACION', CASE WHEN RESULTHIV='True' THEN 'Positivo' WHEN RESULTHIV='False' THEN 'Negativo' ELSE 'No Tiene' END AS 'RESULTADO HIV', 
 CASE WHEN IQMTOXOPL='1' THEN 'Positivo' WHEN IQMTOXOPL='2' THEN 'Negativo' ELSE 'No Tiene' END AS 'IQM TOXOPLASMA', FECULTIQM AS 'FECHA ULTIMO IQM', CASE WHEN IGGTOXOPL='True' THEN 'Positivo' WHEN IGGTOXOPL='False' THEN 'Negativo' ELSE 'No Tiene' END AS 'IQG TOXOPLASMA', 
 CANTTOXO AS 'CANTIDAD IQG', FECULTIGG AS 'FECHA ULTIMO IQG', CASE WHEN HEPATITIB='1' THEN 'Positivo' WHEN HEPATITIB='2' THEN 'Negativo' ELSE 'No Tiene' END AS 'HEPATITIS B', CANTHEPAT AS 'CANTIDAD HEPATITIS B', CASE WHEN RESULVDRL='True' THEN 'Reactivo' WHEN IGGTOXOPL='False' THEN 'NO Reactivo' ELSE 'No Tiene' END AS 'RESULTADO VDRL',
 DILUCVDRL AS DILUSIONES, RIESOBTET AS 'RIESGOS OBSTETRICOS',RESCUAHEM AS 'CUADRO HEMATICO',RESPARORI AS 'PARCIAL DE ORINA', TESTSULLI AS 'TEST SULLIVAN', OTROSANTE AS 'OTROS ANTECEDENTES',GLUCBASAL AS 'GLUCEMIA BASAL', NUMEMOLAS AS 'NUMERO DE MOLAS',OTROSOBST,NUMEOVITO
 
FROM HCANTGINE A WITH(NOLOCK)
INNER JOIN INPROFSAL B WITH(NOLOCK) ON A.CODPROSAL=B.CODPROSAL 
LEFT OUTER JOIN HCTIPPLAN C WITH(NOLOCK) ON A.CODPLANIF=C.CODPLANIF

WHERE A.IPCODPACI = @CodigoPaciente AND A.NUMEFOLIO BETWEEN @NumeroFolioInicial AND @NumeroFolioFinal

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta el historial gineco-obstétrico completo de una paciente en un rango de folios de historia clínica, sin aplicar filtros adicionales más allá del código de paciente y el rango de folios. Recupera antecedentes reproductivos y obstétricos como número de gestaciones, partos, abortos, cesáreas, mortinatos, hijos vivos y embarazos ectópicos; datos del ciclo menstrual como menarquia, duración y regularidad; resultados de laboratorio prenatal como HIV, toxoplasma, hepatitis B, VDRL y Chagas; tamizajes de cáncer de cuello uterino (citología y prueba de VPH); información de control prenatal y semanas de gestación; grupos sanguíneos materno y paterno; y método de planificación familiar. Combina la tabla de antecedentes ginecológicos (HCANTGINE) con el maestro de profesionales de la salud (INPROFSAL) para identificar al profesional que registró la atención, y con los tipos de planificación (HCTIPPLAN) para describir el método anticonceptivo o de planificación utilizado. Se usa principalmente en reportes clínicos y de control prenatal dentro de la historia clínica de la paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_AntecedentesGinecoObstetricosRangoFolioNOFiltro';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_AntecedentesGinecoObstetricosRangoFolioNOFiltro';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta los antecedentes ginecoobstétricos de una paciente dentro de un rango de folios, formateando resultados clínicos para reportes de historia clínica y control prenatal.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AntecedentesGinecoObstetricosRangoFolioNOFiltro';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La paciente debe existir y tener registros en HCANTGINE con folios dentro del rango solicitado; Debe existir el profesional de salud asociado en INPROFSAL (INNER JOIN obligatorio); El tipo de planificación en HCTIPPLAN es opcional (LEFT OUTER JOIN)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AntecedentesGinecoObstetricosRangoFolioNOFiltro';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan filas con NUMEFOLIO dentro del rango [inicial, final] inclusivo; Solo se retornan registros de un único paciente (filtrado por IPCODPACI); Los grupos sanguíneos paterno y materno se concatenan con su factor RH; Los ciclos menstruales se presentan con formato ''ciclos / duración días''; Las edades y semanas se presentan con sufijo textual (''años'',''semanas''); Usa NOLOCK en todas las tablas, permitiendo lecturas sucias', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AntecedentesGinecoObstetricosRangoFolioNOFiltro';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Antecedentes ginecoobstétricos; Menarquia; Ciclo menstrual; Gestaciones; Cesáreas; Abortos; Partos; Mortinatos; Embarazos ectópicos; Hijos vivos; Planificación familiar; Control prenatal; Semanas de gestación; FUM (fecha última menstruación); FUP (fecha último parto); FUC (fecha última citología); HIV; Toxoplasmosis (IgM/IgG); Hepatitis B; VDRL; Chagas; Virus del Papiloma Humano (VPH); Citología cervicovaginal; Cultivo retrovaginal; Grupo sanguíneo y factor RH materno/paterno; Trimestre de aplicación; Consultoría IVE (Interrupción Voluntaria del Embarazo); Riesgo obstétrico; Cuadro hemático; Parcial de orina (+4 adicionales)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AntecedentesGinecoObstetricosRangoFolioNOFiltro';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCANTGINE: Devuelve registros de antecedentes ginecoobstétricos cuando IPCODPACI coincide con la paciente y NUMEFOLIO está entre el folio inicial y final', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AntecedentesGinecoObstetricosRangoFolioNOFiltro';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ApplicationQuarter = 1, 2 o 3 → Etiqueta como ''Primer trimestre'', ''Segundo trimestre'' o ''Tercer trimestre'' respectivamente; si IVEConsultancy = 1 o 2 → Traduce a ''Si'' o ''No''; si DateListHumanPapillomavirusTestReport = 1 o 2 → Traduce a ''Positivo'' o ''Negativo''; si DateLastCytologyReport = 1 o 2 → Traduce a ''Normal'' o ''Anormal''; si CICLOREGU = ''True'' → Reporta ciclo regular como ''Si'' else Reporta ''No''; si CONTPRENA = ''True'' → Reporta control prenatal como ''Si'' else Reporta ''No''; si RESULTHIV = ''True''/''False'' → Traduce a ''Positivo''/''Negativo'' else Si es nulo u otro valor reporta ''No Tiene''; si IQMTOXOPL = ''1''/''2'' → Traduce a ''Positivo''/''Negativo'' else Reporta ''No Tiene''; si IGGTOXOPL = ''True''/''False'' → Traduce a ''Positivo''/''Negativo'' else Reporta ''No Tiene''; si HEPATITIB = ''1''/''2'' → Traduce a ''Positivo''/''Negativo'' else Reporta ''No Tiene''; si RESULVDRL = ''True'' o IGGTOXOPL = ''False'' → Reporta VDRL como ''Reactivo'' o ''NO Reactivo'' else Reporta ''No Tiene''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AntecedentesGinecoObstetricosRangoFolioNOFiltro';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCANTGINE; dbo.INPROFSAL; dbo.HCTIPPLAN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AntecedentesGinecoObstetricosRangoFolioNOFiltro';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AntecedentesGinecoObstetricosRangoFolioNOFiltro';
-- GO
