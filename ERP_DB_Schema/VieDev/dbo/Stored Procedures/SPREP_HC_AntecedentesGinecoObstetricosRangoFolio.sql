
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