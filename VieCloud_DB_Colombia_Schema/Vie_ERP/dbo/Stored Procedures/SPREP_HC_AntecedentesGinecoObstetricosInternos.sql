
CREATE PROCEDURE [dbo].[SPREP_HC_AntecedentesGinecoObstetricosInternos]
(
@CodigoPaciente Varchar(25),
@NumeroFolio Char(10),
@NumeroIngreso nChar(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
 SELECT NUMEFOLIO AS 'NUMERO FOLIO', MENARQUIA, CICLOSPAC AS CICLOS,MENSTRDUR AS 'DURACION MENSTRUACION',CASE WHEN CICLOREGU='True' THEN 'Si' ELSE 'No' END AS 'ES CICLO REGULAR', EDADVIDSE AS 'EDAD VIDA SEXUAL', GESTACION AS 'NUMERO GESTACIONES', NUMCESARE AS 'NUMERO DE CESAREAS',
 NUMABORTO AS 'NUMERO de ABORTOS', NUMHIJVIV AS 'NUMERO DE HIJOS VIVOS', NUMETOPIC AS 'NUMERO EMBARAZOS ETOPICOS', NUMPARTO AS 'NUMERO DE PARTOS', NUMMORTIN AS 'NUMERO DE MORTINATOS', FECULTMEN AS 'FUM', FECULTPAR AS 'FUP',FECULTCIT AS 'FUC',  RTRIM(DESPLANIF) AS 'DESCRIPCION PLANIFICACION',
 CASE WHEN CONTPRENA ='True' THEN 'Si' ELSE 'No' END AS 'CONTROL PRENATAL', CANTPRENA AS 'CANTIDAD CONTROL PRENATAL', INICONPRE AS 'INICIO CONTROL PRENATAL', NOMSEMGES AS 'SEMANAS DE GESTACION', CASE WHEN RESULTHIV='True' THEN 'Positivo' WHEN RESULTHIV='False' THEN 'Negativo' ELSE 'No Tiene' END AS 'RESULTADO HIV', 
 CASE WHEN IQMTOXOPL='1' THEN 'Positivo' WHEN IQMTOXOPL='2' THEN 'Negativo' ELSE 'No Tiene' END AS 'IQM TOXOPLASMA', FECULTIQM AS 'FECHA ULTIMO IQM', CASE WHEN IGGTOXOPL='True' THEN 'Positivo' WHEN IGGTOXOPL='False' THEN 'Negativo' ELSE 'No Tiene' END AS 'IQG TOXOPLASMA', 
 CANTTOXO AS 'CANTIDAD IQG', FECULTIGG AS 'FECHA ULTIMO IQG', CASE WHEN HEPATITIB='1' THEN 'Positivo' WHEN HEPATITIB='2' THEN 'Negativo' ELSE 'No Tiene' END AS 'HEPATITIS B', CANTHEPAT AS 'CANTIDAD HEPATITIS B', CASE RESULVDRL WHEN 1 THEN 'Reactivo' WHEN 0 THEN 'No Reactivo' ELSE 'No Tiene' END AS 'RESULTADO VDRL',
 DILUCVDRL AS DILUSIONES, RIESOBTET AS 'RIESGOS OBSTETRICOS',RESCUAHEM AS 'CUADRO HEMATICO',RESPARORI AS 'PARCIAL DE ORINA', TESTSULLI AS 'TEST SULLIVAN', OTROSANTE AS 'OTROS ANTECEDENTES',GLUCBASAL AS 'GLUCEMIA BASAL', NUMEMOLAS AS 'NUMERO DE MOLAS',OTROSOBST,NUMEOVITO
 
FROM HCANTGINI A WITH(NOLOCK)
INNER JOIN INPROFSAL B WITH(NOLOCK) ON A.CODPROSAL=B.CODPROSAL 
LEFT OUTER JOIN HCTIPPLAN C WITH(NOLOCK) ON A.CODPLANIF=C.CODPLANIF

WHERE A.NUMEFOLIO = @NumeroFolio AND A.IPCODPACI = @CodigoPaciente AND A.NUMINGRES = @NumeroIngreso

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera el detalle completo de los antecedentes ginecológicos y obstétricos registrados en la historia clínica de una paciente, dado su código (cédula), número de folio e ingreso. Consulta el registro clínico de ginecología (ciclo menstrual, menarquia, gestaciones, partos, cesáreas, abortos, embarazos ectópicos, mortinatos, hijos vivos, fecha de última menstruación, último parto y última citología), resultados de exámenes de control prenatal (VIH, toxoplasma IgM/IgG, hepatitis B, VDRL, cuadro hemático, parcial de orina, test de Sullivan, glucemia basal), datos de planificación familiar y riesgos obstétricos. Combina la tabla de antecedentes gineco-obstétricos con el maestro de profesionales de la salud y el catálogo de tipos de planificación para presentar información legible y lista para imprimir en reportes de historia clínica ginecológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_AntecedentesGinecoObstetricosInternos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_AntecedentesGinecoObstetricosInternos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta y devuelve los antecedentes ginecoobstétricos internos registrados para un paciente, folio e ingreso específicos, formateando indicadores clínicos a etiquetas legibles.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AntecedentesGinecoObstetricosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros de antecedentes ginecoobstétricos asociados al folio, paciente e ingreso indicados; El profesional de salud referenciado debe existir en el maestro de profesionales; El código de planificación, si existe, debe estar registrado en el catálogo de tipos de planificación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AntecedentesGinecoObstetricosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan antecedentes cuyo folio, paciente e ingreso coincidan exactamente con los parámetros; Los resultados serológicos (HIV, Toxoplasma IgM/IgG, Hepatitis B, VDRL) siempre se exponen como etiquetas estandarizadas en lugar de códigos crudos; El profesional de salud asociado debe existir (INNER JOIN), por lo que registros sin profesional válido no se retornan; La planificación familiar es opcional (LEFT JOIN); su ausencia no excluye el registro; Se usa NOLOCK en todas las tablas, por lo que es lectura sin bloqueo (puede leer datos no confirmados)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AntecedentesGinecoObstetricosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Antecedentes ginecoobstétricos; Menarquia; Ciclo menstrual; Vida sexual; Gestaciones; Cesáreas; Abortos; Hijos vivos; Embarazos ectópicos; Partos; Mortinatos; FUM (fecha última menstruación); FUP (fecha último parto); FUC (fecha última citología); Planificación familiar; Control prenatal; Semanas de gestación; HIV; Toxoplasmosis (IgM/IgG); Hepatitis B; VDRL; Riesgos obstétricos; Cuadro hemático; Parcial de orina; Test de Sullivan; Glucemia basal; Molas; Óvitos; Paciente; Folio (+2 adicionales)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AntecedentesGinecoObstetricosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCANTGINI: Cuando coinciden folio, paciente e ingreso se devuelve el conjunto de antecedentes ginecoobstétricos con campos booleanos/códigos transformados a etiquetas (Si/No, Positivo/Negativo, Reactivo/No Reactivo, No Tiene)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AntecedentesGinecoObstetricosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CICLOREGU = ''True'' → Se reporta el ciclo como ''Si'' regular else Se reporta como ''No'' regular; si CONTPRENA = ''True'' → Se reporta control prenatal como ''Si'' else Se reporta como ''No''; si RESULTHIV = ''True'' / ''False'' / otro → Se traduce a ''Positivo'' / ''Negativo'' / ''No Tiene'' respectivamente; si IQMTOXOPL = ''1'' / ''2'' / otro → Se traduce a ''Positivo'' / ''Negativo'' / ''No Tiene'' para IgM toxoplasma; si IGGTOXOPL = ''True'' / ''False'' / otro → Se traduce a ''Positivo'' / ''Negativo'' / ''No Tiene'' para IgG toxoplasma; si HEPATITIB = ''1'' / ''2'' / otro → Se traduce a ''Positivo'' / ''Negativo'' / ''No Tiene'' para Hepatitis B; si RESULVDRL = 1 / 0 / otro → Se traduce a ''Reactivo'' / ''No Reactivo'' / ''No Tiene'' para VDRL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AntecedentesGinecoObstetricosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCANTGINI; dbo.INPROFSAL; dbo.HCTIPPLAN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AntecedentesGinecoObstetricosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AntecedentesGinecoObstetricosInternos';
-- GO
