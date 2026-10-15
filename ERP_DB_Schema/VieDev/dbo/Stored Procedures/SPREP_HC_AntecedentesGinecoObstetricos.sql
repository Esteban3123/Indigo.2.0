CREATE PROCEDURE [dbo].[SPREP_HC_AntecedentesGinecoObstetricos] (@CodigoPaciente Varchar(25), @NumeroFolio Char(10), @NumeroIngreso nChar(10)) AS BEGIN -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.

SET NOCOUNT ON;

-- Insert statements for procedure here

SELECT NUMEFOLIO AS 'NUMERO FOLIO',
       ChagasDate,
       Chagas,
       RetrovaginalCultureDate,
       RetrovaginalCulture,
       ApplicationQuarterDate,
       concat(PaternalBloodType, PaternalHR)AS GrupoSanguineoPaterno,
       concat(MaternalBloodType, MaternalHR) AS GrupoSanguineoMaterno,
       CASE ApplicationQuarter
           WHEN 1 THEN 'Primer trimestre'
           WHEN 2 THEN 'Segundo trimestre'
           WHEN 3 THEN 'Tercer trimestre'
       END AS ApplicationQuarter,
       CASE IVEConsultancy
           WHEN 1 THEN 'Si'
           WHEN 2 THEN 'No'
       END AS IVEConsultancy,
       GestationalNumber,
       DateLastHumanPapillomavirusTest,
       CASE DateListHumanPapillomavirusTestReport
           WHEN 1 THEN 'Positivo'
           WHEN 2 THEN 'Negativo'
       END AS DateListHumanPapillomavirusTestReport,
       CASE DateLastCytologyReport
           WHEN 1 THEN 'Normal'
           WHEN 2 THEN 'Anormal'
       END AS DateLastCytologyReport,
       concat(MENARQUIA, '  años') AS MENARQUIA,
       CONCAT(CICLOSPAC, ' / ', MENSTRDUR,' días') AS CICLOS,
       MENSTRDUR AS 'DURACION MENSTRUACION',
       CASE
           WHEN CICLOREGU='True' THEN 'Si'
           ELSE 'No'
       END AS 'ES CICLO REGULAR',
       concat(EDADVIDSE, '  años') AS 'EDAD VIDA SEXUAL',
       GESTACION AS 'NUMERO GESTACIONES',
       NUMCESARE AS 'NUMERO DE CESAREAS',
       NUMABORTO AS 'NUMERO de ABORTOS',
       NUMHIJVIV AS 'NUMERO DE HIJOS VIVOS',
       NUMETOPIC AS 'NUMERO EMBARAZOS ETOPICOS',
       NUMPARTO AS 'NUMERO DE PARTOS',
       NUMMORTIN AS 'NUMERO DE MORTINATOS',
       FECULTMEN AS 'FUM',
       FECULTPAR AS 'FUP',
       FECULTCIT AS 'FUC',
       RTRIM(C.DESPLANIF) AS 'DESCRIPCION PLANIFICACION',
       CASE
           WHEN CONTPRENA ='True' THEN 'Si'
           ELSE 'No'
       END AS 'CONTROL PRENATAL',
       CANTPRENA AS 'CANTIDAD CONTROL PRENATAL',
       concat(INICONPRE, '  Semanas') AS 'INICIO CONTROL PRENATAL',
       CONCAT(NOMSEMGES, '  Semanas') AS 'SEMANAS DE GESTACION',
       CASE
           WHEN RESULTHIV='1' THEN 'Positivo'
           WHEN RESULTHIV='2' THEN 'Negativo'
		   WHEN RESULTHIV='3' THEN 'No Tiene'          
       END AS 'RESULTADO HIV',
       CASE
           WHEN IQMTOXOPL='1' THEN 'Positivo'
           WHEN IQMTOXOPL='2' THEN 'Negativo'
		   WHEN IQMTOXOPL='3' THEN 'No Tiene'
           WHEN IQMTOXOPL = '4' THEN 'Indeterminado'          
       END AS 'IQM TOXOPLASMA',
       FECULTIQM AS 'FECHA ULTIMO IQM',
       CASE
           WHEN IGGTOXOPL='1' THEN 'Positivo'
           WHEN IGGTOXOPL='2' THEN 'Negativo'
		   WHEN IGGTOXOPL='3' THEN 'No Tiene'
           WHEN IGGTOXOPL = '4' THEN 'Indeterminado'          
       END AS 'IQG TOXOPLASMA',
       CANTTOXO AS 'CANTIDAD IQG',
       FECULTIGG AS 'FECHA ULTIMO IQG',
       CASE
           WHEN HEPATITIB='1' THEN 'Positivo'
           WHEN HEPATITIB='2' THEN 'Negativo'
		   WHEN HEPATITIB='3' THEN 'No Tiene'         
       END AS 'HEPATITIS B',
       CANTHEPAT AS 'CANTIDAD HEPATITIS B',
       CASE RESULVDRL
           WHEN 1 THEN 'Positivo'
           WHEN 2 THEN 'Negativo'
		   WHEN 3 THEN 'No Tiene'           
       END AS 'RESULTADO VDRL',
       DILUCVDRL AS DILUSIONES,
       RIESOBTET AS 'RIESGOS OBSTETRICOS',
       RESCUAHEM AS 'CUADRO HEMATICO',
       RESPARORI AS 'PARCIAL DE ORINA',
       TESTSULLI AS 'TEST SULLIVAN',
       OTROSANTE AS 'OTROS ANTECEDENTES',
       GLUCBASAL AS 'GLUCEMIA BASAL',
       NUMEMOLAS AS 'NUMERO DE MOLAS',
       OTROSOBST,
       NUMEOVITO,
       FECPROPAR AS 'FECHA PROBABLE PARTO',
       CASE IGMRUBEO
           WHEN 1 THEN 'Positivo'
           WHEN 2 THEN 'Negativo'
           WHEN 3 THEN 'No tiene'
       END AS IGMRUBEO,
       CASE IGGRUBEO
           WHEN 1 THEN 'Positivo'
           WHEN 2 THEN 'Negativo'
           WHEN 3 THEN 'No tiene'
       END AS IGGRUBEO,
       CASE PRURASIF
           WHEN 1 THEN 'Positivo'
           WHEN 2 THEN 'Negativo'
           WHEN 3 THEN 'No tiene'
       END AS PRURASIF,
       UROCULTI,
       FECHAPTOG,
       FECHAIGM,
       FECHAIGG,
       FECHASIFI,
       FECHAURO,
       FECHAORI,
       FECHACUA,
       FECHAGLI,
       FECHAVDRL,
       FECHAHIV,
       FECHAHEPB,
       HEMOGLO,
       PLAQUETA,
       CASE FTAABS
           WHEN 1 THEN 'Positivo'
           WHEN 2 THEN 'Negativo'
           WHEN 3 THEN 'No tiene'
       END AS FTAABS,
       FECHAHEMO,
       FECHAPLA,
       FECHAFTA,
	   -- SE MANEJA EL 0 Y TAMBIEN EL 2 PARA INDICAR UN 'NO'
	   CASE PuerperiumAttention 
		   WHEN 1 THEN 'Sí'
		   WHEN 0 THEN 'No'
		   WHEN 2 THEN 'No'
	   END AS PuerperiumAttention,

	   CASE ContraceptiveAttention 
		WHEN 1 THEN 'Sí'
		WHEN 0 THEN 'No'
		WHEN 2 THEN 'No'
	   END AS ContraceptiveAttention,

	   CASE DischargeWithContraceptive 
	   	WHEN 1 THEN 'Sí'
		WHEN 0 THEN 'No'
		WHEN 2 THEN 'No'
	   END AS DischargeWithContraceptive,

	   	IIF(ContraceptiveMethodChosen IS NULL,'',  D.DESPLANIF) AS ContraceptiveMethodChosen,	   	
		IIF(PuerperiumAttentionObservations IS NULL,'Sin registro',PuerperiumAttentionObservations) AS PuerperiumAttentionObservations,

		CASE Planning
			WHEN 1 THEN 'Sí'
			WHEN 0 THEN 'No'
		END AS Planning,

		CASE CervixCancerScreening
			WHEN 1 THEN 'Si'
			WHEN 0 THEN 'No'
		END AS CervixCancerScreening,

		CASE WhyNotCervixCancerScreening
			WHEN 0 THEN 'No aplica'
			WHEN 1 THEN 'No se realiza por una tradición'
			WHEN 2 THEN 'No se realiza por una condición de salud'
			WHEN 3 THEN 'No se realiza por negación de la usuaria'
			WHEN 4 THEN 'No se realiza por tener datos de contacto de la usuaria no actualizados'
			WHEN 5 THEN 'No se realiza por otras razones'
			WHEN 6 THEN 'Riesgo no evaluado'
		END AS WhyNotCervixCancerScreening,

		CASE CervixCancerScreeningType
			WHEN 1 THEN 'Citología cérvico uterina'
			WHEN 2 THEN 'Prueba ADN - VPH'
			WHEN 3 THEN 'Técnica de inspección visual'
			WHEN 4 THEN 'Prueba ADN - VPH y citología cérvico uterina'
		END AS CervixCancerScreeningType,

		CASE CytologyResult
			WHEN 1 THEN 'ASC-US (células escamosas atípicas de significado indeterminado)'
			WHEN 2 THEN 'ASC-H (células escamosas atípicas de significado indeterminado sugestivo de LEI de alto grado)'
			WHEN 3 THEN 'Lesión intraepitelial escamosa (LEI) de bajo grado -HPV (NIC I) (LEI BG)'
			WHEN 4 THEN 'Lesión intraepitelial escamosa (LEI) de alto grado (NIC II-III CA INSITU) (LEI AG)'
			WHEN 5 THEN 'Lesión intraepitelial escamosa de alto grado sospechosa de infiltración'
			WHEN 6 THEN 'Carcinoma de células escamosas (Escamocelular) glandulares'
			WHEN 7 THEN 'Células endocervicales atípicas sin ningún otro significado'
			WHEN 8 THEN 'Células endometriales atípicas sin ningún otro significado'
			WHEN 9 THEN 'Células glandulares atípicas sin ningún otro significado'
			WHEN 10 THEN 'Células endocervicales atípicas sospechosas de neoplasia'
			WHEN 11 THEN 'Células endometriales atípicas sospechosas de neoplasia'
			WHEN 12 THEN 'Células glandulares atípicas sospechosas de neoplasia'
			WHEN 13 THEN 'Adenocarcinoma endocervical in situ'
			WHEN 14 THEN 'Adenocarcinoma endocervical'
			WHEN 15 THEN 'Adenocarcinoma endometrial'
			WHEN 16 THEN 'Otras neoplasias'
			WHEN 17 THEN 'Negativa para lesión intraepitelial o neoplasia'
			WHEN 18 THEN 'Inadecuada para lectura'
		END AS CytologyResult,
		LatestVisualInspectionTechniqueDate,

		CASE PostVisualInspectionTechniqueTreatment
			WHEN 0 THEN 'No aplica'
			WHEN 1 THEN 'Si, se realizó tratamiento ablativo'
			WHEN 2 THEN 'Si, se realizó tratamiento de escisión'
			WHEN 3 THEN 'Si, se realizó tratamiento homologable a ablativo o de escisión'
			WHEN 4 THEN 'No se realizó ablación, escisión, ni tratamiento homologable, se requiere de otro procedimiento'
			WHEN 5 THEN 'No se realizó ablación ni escisión por otras razonas'
			WHEN 6 THEN 'Registro no evaluado'
		END AS PostVisualInspectionTechniqueTreatment,
		LastColposcopy,
		CASE ColposcopyReport
           WHEN 1 THEN 'Normal'
           WHEN 2 THEN 'Anormal'
		END AS ColposcopyReport,
		LastBiopsyCervixCancerDate,
		BiopsyCervixCancerReportDate,

	   CASE BreastCancerScreening
	   		WHEN 1 THEN 'Si'
			WHEN 0 THEN 'No'
	   END AS BreastCancerScreening,

	   CASE WhyNotBreastCancerScreening
			WHEN 0 THEN 'No aplica'
			WHEN 1 THEN 'No se realiza por una tradición'
			WHEN 2 THEN 'No se realiza por una condición de salud'
			WHEN 3 THEN 'No se realiza por negación de la usuaria'
			WHEN 4 THEN 'No se realiza por tener datos de contacto de la usuaria no actualizados'
			WHEN 5 THEN 'No se realiza por otras razones'
			WHEN 6 THEN 'Riesgo no evaluado'
		END AS WhyNotBreastCancerScreening,

		CASE BreastCancerScreeningType
			WHEN 1 THEN 'Examen médico manual de mama'
			WHEN 2 THEN 'Ecografía mamaria'
			WHEN 3 THEN 'Mamografía'
		END AS BreastCancerScreeningType,
		LastMedicalManualBreastExamDate,

		CASE MedicalManualBreastExamResult
           WHEN 1 THEN 'Normal'
           WHEN 2 THEN 'Anormal'
		END AS MedicalManualBreastExamResult,		
		LastMammogramDate,

		CASE MammogramResult
			WHEN 0 THEN 'No aplica'
			WHEN 1 THEN 'BIRADS 0: necesidad de nuevo estudio imagenológico o mamograma previo para evaluación'
			WHEN 2 THEN 'BIRADS 1: negativo'
			WHEN 3 THEN 'BIRADS 2: hallazgos benignos'
			WHEN 4 THEN 'BIRADS 3: probablemente benigno'
			WHEN 5 THEN 'BIRADS 4: anormalidad sospechosa'
			WHEN 6 THEN 'BIRADS 5: altamente sospechoso de malignidad'
			WHEN 7 THEN 'BIRADS 6: malignidad por biopsia conocida'
			WHEN 8 THEN 'Riesgo no evaluado'
		END AS MammogramResult,
		LastBiopsyBreastCancerDate,
		BiopsyBreastCancerReportDate,

		CASE BiopsyBreastCancerResult
			WHEN 0 THEN 'No aplica'
			WHEN 1 THEN 'Benigna'
			WHEN 2 THEN 'Atípica (indeterminada)'
			WHEN 3 THEN 'Malignidad sospechosa/probable'
			WHEN 4 THEN 'Maligna'
			WHEN 5 THEN 'No satisfactoria'
			WHEN 6 THEN 'Riesgo no evaluado'
		END AS BiopsyBreastCancerResult,

		CASE LowBirthWeightNewborn
	   		WHEN 1 THEN 'Si'
			WHEN 0 THEN 'No'
	    END AS LowBirthWeightNewborn,

		CASE NewbornMacrosomicPreviousPregnancy
	   		WHEN 1 THEN 'Si'
			WHEN 0 THEN 'No'
	    END AS NewbornMacrosomicPreviousPregnancy,

		CASE PreviousPretermBirth
	   		WHEN 1 THEN 'Si'
			WHEN 0 THEN 'No'
	    END AS PreviousPretermBirth,

		CASE IntergenicPeriodLess24Months
	   		WHEN 1 THEN 'Si'
			WHEN 0 THEN 'No'
	    END AS IntergenicPeriodLess24Months,

		CASE RHIncompatibilityInPreviousPregnancy
	   		WHEN 1 THEN 'Si'
			WHEN 0 THEN 'No'
	    END AS RHIncompatibilityInPreviousPregnancy,

		CASE PreviousGestationalPreeclampsia
	   		WHEN 1 THEN 'Si'
			WHEN 0 THEN 'No'
	    END AS PreviousGestationalPreeclampsia,

		CASE PreviousGestationalHemorrhage
	   		WHEN 1 THEN 'Si'
			WHEN 0 THEN 'No'
	    END AS PreviousGestationalHemorrhage,

		CASE HistoryOfPostpartumDepression
	   		WHEN 1 THEN 'Si'
			WHEN 0 THEN 'No'
	    END AS HistoryOfPostpartumDepression,

		IIF(LowBirthWeightNewborn IS NULL AND NewbornMacrosomicPreviousPregnancy IS NULL AND PreviousPretermBirth IS NULL AND IntergenicPeriodLess24Months IS NULL AND
			RHIncompatibilityInPreviousPregnancy IS NULL AND PreviousGestationalPreeclampsia IS NULL AND PreviousGestationalHemorrhage IS NULL AND HistoryOfPostpartumDepression IS NULL AND
			GESTACION IS NULL AND NUMCESARE IS NULL AND NUMABORTO IS NULL AND NUMHIJVIV IS NULL AND NUMETOPIC IS NULL AND NUMPARTO IS NULL AND NUMMORTIN IS NULL AND
			FECULTPAR IS NULL AND NUMEMOLAS IS NULL AND OTROSOBST IS NULL AND NUMEOVITO IS NULL, 'No', 'Si') AS VisibilityObstetricFields

	   --*****************************

FROM HCANTGINE A With(Nolock)
INNER JOIN INPROFSAL B With(Nolock) ON A.CODPROSAL=B.CODPROSAL
LEFT OUTER JOIN HCTIPPLAN C With(Nolock) ON A.CODPLANIF=C.CODPLANIF
LEFT OUTER JOIN HCTIPPLAN D With(Nolock) ON A.ContraceptiveMethodChosen = D.CODPLANIF 

WHERE A.NUMEFOLIO = @NumeroFolio
  AND A.IPCODPACI = @CodigoPaciente
  AND A.NUMINGRES = @NumeroIngreso END