
-- =============================================
-- Author:		<Author,Juan David Patiño Cabrea,Name>
-- Create date: <Create Date,08-05-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila549]
(
  @IdFicha as Int
)

AS
BEGIN
  SET NOCOUNT ON;
  				
			Select 
				CASE INGRESAREMI WHEN '1' THEN 'X' END AS 'remitido_SI',CASE WHEN INGRESAREMI IS NULL OR INGRESAREMI = '0' THEN 'X' END AS 'remitido_NO' 
				,TIEMTRAMITE as 'TiempoRemision'
				,INSTITUCIONREF1 as 'Institucion1'
				,INSTITUCIONREF2 as 'Institucion2'
				,NUMGESTA as 'NumeroGestacione'
				,PARVAGINALES as 'PartosVaginales'
				,CESAREAS 
				,ABORTOS 
				,MOLAS 
				,ECTOPICOS
				,MUERTOS
				,VIVOS
				,convert(varchar(10),FECHULTMENS,103) as 'FechaUltimaGestacion'
				,CONTPRENATALES 
				,SEMAINICIOCPN as 'semanaInicio_CPN'
				,CASE TERMGESTACION WHEN '1' THEN 'X' END AS 'TerminaGestacion_Aborto',CASE TERMGESTACION WHEN '2' THEN 'X' END AS 'TerminaGestacion_Parto',CASE TERMGESTACION WHEN '3' THEN 'X' END AS 'TerminaGestacion_PartoInstrumentado',CASE TERMGESTACION WHEN '4' THEN 'X' END AS 'TerminaGestacion_cesarea',CASE TERMGESTACION WHEN '1' THEN 'X' END AS 'TerminaGestacion_continuaEmb'
				,CASE MOMOCUGESTACION WHEN '1' THEN 'X' END AS 'OcurrecianTerminacioGesta_Antes',CASE MOMOCUGESTACION WHEN '2' THEN 'X' END AS 'OcurrecianTerminacioGesta_Durante',CASE MOMOCUGESTACION WHEN '3' THEN 'X' END AS 'OcurrecianTerminacioGesta_Despues'
				,CASE ECLAMPSIA WHEN '1' THEN 'X' END AS 'Enfermedad_ECLAMPSIA_SI',CASE  WHEN ECLAMPSIA IS NULL OR ECLAMPSIA = '0' THEN 'X' END AS 'Enfermedad_ECLAMPSIA_NO'
				,CASE SEPSIS WHEN '1' THEN 'X' END AS 'Enfermedad_SEPSIS_SI',CASE  WHEN  SEPSIS IS NULL  OR  SEPSIS = '0'  THEN 'X' END AS 'Enfermedad_SEPSIS_NO'
				,CASE HEMORRAGIA WHEN '1' THEN 'X' END AS 'Enfermedad_HEMORRAGIA_SI',CASE  WHEN  HEMORRAGIA IS NULL  OR  HEMORRAGIA = '0'  THEN 'X' END AS 'Enfermedad_HEMORRAGIA_NO'
				,CASE PREECLAMPSIA WHEN '1' THEN 'X' END AS 'Enfermedad_PREECLAMPSIA_SI',CASE  WHEN  PREECLAMPSIA IS NULL  OR  PREECLAMPSIA = '0'  THEN 'X' END AS 'Enfermedad_PREECLAMPSIA_NO'
				,CASE RUPTUTERINA WHEN '1' THEN 'X' END AS 'Enfermedad_RUPTUTERINA_SI',CASE  WHEN  RUPTUTERINA IS NULL  OR  RUPTUTERINA = '0'  THEN 'X' END AS 'Enfermedad_RUPTUTERINA_NO'
				,CASE ABOSEPTICO WHEN '1' THEN 'X' END AS 'Enfermedad_ABOSEPTICO_SI',CASE  WHEN  ABOSEPTICO IS NULL  OR  ABOSEPTICO = '0'  THEN 'X' END AS 'Enfermedad_ABOSEPTICO_NO'
				,CASE EMBAECTOPICO  WHEN '1' THEN 'X' END AS 'Enfermedad_EMBAECTOPICO_SI',CASE  WHEN  EMBAECTOPICO IS NULL  OR  EMBAECTOPICO = '0'  THEN 'X' END AS 'Enfermedad_EMBAECTOPICO_NO'
				,CASE AUTOINMUNE WHEN '1' THEN 'X' END AS 'Enfermedad_AUTOINMUNE_SI',CASE  WHEN  AUTOINMUNE IS NULL  OR  AUTOINMUNE = '0'  THEN 'X' END AS 'Enfermedad_AUTOINMUNE_NO'
				,CASE HEMATOLOGICA WHEN '1' THEN 'X' END AS 'Enfermedad_HEMATOLOGICA_SI',CASE  WHEN  HEMATOLOGICA IS NULL  OR  HEMATOLOGICA = '0'  THEN 'X' END AS 'Enfermedad_HEMATOLOGICA_NO'
				,CASE ONCOLOGIA WHEN '1' THEN 'X' END AS 'Enfermedad_ONCOLOGIA_SI',CASE  WHEN  ONCOLOGIA IS NULL  OR  ONCOLOGIA = '0'  THEN 'X' END AS 'Enfermedad_ONCOLOGIA_NO'
				,CASE ENDOCRINO WHEN '1' THEN 'X' END AS 'Enfermedad_ENDOCRINO_SI',CASE  WHEN  ENDOCRINO IS NULL  OR  ENDOCRINO = '0'  THEN 'X' END AS 'Enfermedad_ENDOCRINO_NO'
				,CASE RENALES WHEN '1' THEN 'X' END AS 'Enfermedad_RENALES_SI',CASE  WHEN  RENALES IS NULL  OR  RENALES = '0'  THEN 'X' END AS 'Enfermedad_RENALES_NO'
				,CASE GASTROINTESTINALES WHEN '1' THEN 'X' END AS 'Enfermedad_GASTROINTESTINALES_SI',CASE  WHEN  GASTROINTESTINALES IS NULL  OR  GASTROINTESTINALES = '0'  THEN 'X' END AS 'Enfermedad_GASTROINTESTINALES_NO'
				,CASE EVETROMBOEMBOLI WHEN '1' THEN 'X' END AS 'Enfermedad_EVETROMBOEMBOLI_SI',CASE  WHEN  EVETROMBOEMBOLI IS NULL  OR  EVETROMBOEMBOLI = '0'  THEN 'X' END AS 'Enfermedad_EVETROMBOEMBOLI_NO'
				,CASE CARDIOCEREBROVASC WHEN '1' THEN 'X' END AS 'Enfermedad_CARDIOCEREBROVASC_SI',CASE  WHEN  CARDIOCEREBROVASC IS NULL  OR  CARDIOCEREBROVASC = '0'  THEN 'X' END AS 'Enfermedad_CARDIOCEREBROVASC_NO'
				,CASE OTRAS WHEN '1' THEN 'X' END AS 'Enfermedad_OTRAS_SI',CASE  WHEN  OTRAS IS NULL  OR  OTRAS = '0'  THEN 'X' END AS 'Enfermedad_OTRAS_NO'
				,CASE CARDIACA WHEN '1' THEN 'X' END AS 'FallaOrganica_CARDIACA_SI',CASE  WHEN  CARDIACA IS NULL  OR  CARDIACA = '0'  THEN 'X' END AS 'FallaOrganica_CARDIACA_NO'
				,CASE VASCULAR WHEN '1' THEN 'X' END AS 'FallaOrganica_VASCULAR_SI',CASE  WHEN  VASCULAR IS NULL  OR  VASCULAR = '0'  THEN 'X' END AS 'FallaOrganica_VASCULAR_NO'
				,CASE RENAL WHEN '1' THEN 'X' END AS 'FallaOrganica_RENAL_SI',CASE  WHEN  RENAL IS NULL  OR  RENAL = '0'  THEN 'X' END AS 'FallaOrganica_RENAL_NO'
				,CASE HEPATICA WHEN '1' THEN 'X' END AS 'FallaOrganica_HEPATICA_SI',CASE  WHEN  HEPATICA IS NULL  OR  HEPATICA = '0'  THEN 'X' END AS 'FallaOrganica_HEPATICA_NO'
				,CASE METABOLICA WHEN '1' THEN 'X' END AS 'FallaOrganica_METABOLICA_SI',CASE  WHEN  METABOLICA IS NULL  OR  METABOLICA = '0'  THEN 'X' END AS 'FallaOrganica_METABOLICA_NO'
				,CASE CEREBRAL WHEN '1' THEN 'X' END AS 'FallaOrganica_CEREBRAL_SI',CASE  WHEN  CEREBRAL IS NULL  OR  CEREBRAL = '0'  THEN 'X' END AS 'FallaOrganica_CEREBRAL_NO'
				,CASE RESPIRATORIA WHEN '1' THEN 'X' END AS 'FallaOrganica_RESPIRATORIA_SI',CASE  WHEN  RESPIRATORIA IS NULL  OR  RESPIRATORIA = '0'  THEN 'X' END AS 'FallaOrganica_RESPIRATORIA_NO'
				,CASE COAGULACION WHEN '1' THEN 'X' END AS 'FallaOrganica_COAGULACION_SI',CASE  WHEN  COAGULACION IS NULL  OR  COAGULACION = '0'  THEN 'X' END AS 'FallaOrganica_COAGULACION_NO'
				,TOTALCRITERIOS
				,CASE INGRESOUCI WHEN '1' THEN 'X' END AS 'INGRESOUCI_SI',CASE   WHEN  INGRESOUCI IS NULL  OR  INGRESOUCI = '0'  THEN 'X' END AS 'INGRESOUCI_NO'
				,CASE CIRUADICIONAL WHEN '1' THEN 'X' END AS 'CIRUADICIONAL_SI',CASE   WHEN  CIRUADICIONAL IS NULL  OR  CIRUADICIONAL = '0'  THEN 'X' END AS 'CIRUADICIONAL_NO'
				,CASE TRANSFUSION WHEN '1' THEN 'X' END AS 'TRANSFUSION_SI',CASE   WHEN  TRANSFUSION IS NULL  OR  TRANSFUSION = '0'  THEN 'X' END AS 'TRANSFUSION_NO'
				,CASE ACCIDENTE WHEN '1' THEN 'X' END AS 'ACCIDENTE_SI',CASE   WHEN  ACCIDENTE IS NULL  OR  ACCIDENTE = '0'  THEN 'X' END AS 'ACCIDENTE_NO'
				,CASE INTOXACCIDENTAL WHEN '1' THEN 'X' END AS 'INTOXACCIDENTAL_SI',CASE   WHEN  INTOXACCIDENTAL IS NULL  OR  INTOXACCIDENTAL = '0'  THEN 'X' END AS 'INTOXACCIDENTAL_NO'
				,CASE INTESUICIDA WHEN '1' THEN 'X' END AS 'INTESUICIDA_SI',CASE   WHEN  INTESUICIDA IS NULL  OR  INTESUICIDA = '0'  THEN 'X' END AS 'INTESUICIDA_NO'
				,CASE VICTVIOLENCIA WHEN '1' THEN 'X' END AS 'VICTVIOLENCIA_SI',CASE   WHEN  VICTVIOLENCIA IS NULL  OR  VICTVIOLENCIA = '0'  THEN 'X' END AS 'VICTVIOLENCIA_NO'
				,CASE OTROEVENSALUDPUB WHEN '1' THEN 'X' END AS 'OTROEVENSALUDPUB_SI',CASE   WHEN  OTROEVENSALUDPUB IS NULL  OR  OTROEVENSALUDPUB = '0'  THEN 'X' END AS 'OTROEVENSALUDPUB_NO'
				,CUALOTROEVENSALUDPUB as 'OtroEvento'
				,DIASESTAHOSPITA as 'DiasEstanciaHosp'
				,DIASESTANCIAUCI as 'DiasUCi'
				,CASE CIRUADICIONAL1  WHEN '1' THEN 'X' END AS 'CirugiaAdicional1_Histerectomia',CASE CIRUADICIONAL1 WHEN '2' THEN 'X' END AS 'CirugiaAdicional1_Laparotomio',CASE CIRUADICIONAL1 WHEN '3' THEN 'X' END AS 'CirugiaAdicional1_Legrado',CASE CIRUADICIONAL1 WHEN '4' THEN 'X' END AS 'CirugiaAdicional1_Otra'
				,CODDIAGNO_CAUSAPRINCI + ' - ' + rtrim(D.NOMDIAGNO)  as 'DiagnosticoPrincipal'
				,CASE CAUPRINTRASTORNOS  WHEN '1' THEN 'X' END AS 'CAUPRINTRASTORNOS'
				,CASE CAUPRINCOMPLIHEMORRA  WHEN '1' THEN 'X' END AS 'CAUPRINCOMPLIHEMORRA'
				,CASE CAUPRINCOMPLIABORTO  WHEN '1' THEN 'X' END AS 'CAUPRINCOMPLIABORTO'
				,CASE CAUPRINSEPSISOBSTETRICO  WHEN '1' THEN 'X' END AS 'CAUPRINSEPSISOBSTETRICO'
				,CASE CAUPRINSEPSISNOOBSTETRICO  WHEN '1' THEN 'X' END AS 'CAUPRINSEPSISNOOBSTETRICO'
				,CASE CAUPRINSEPSISPULMONAR  WHEN '1' THEN 'X' END AS 'CAUPRINSEPSISPULMONAR'
				,CASE CAUPRINPREEXISTENTE  WHEN '1' THEN 'X' END AS 'CAUPRINPREEXISTENTE'
				,CASE CAUPRINOTRACAUSA  WHEN '1' THEN 'X' END AS 'CAUPRINOTRACAUSA'
				,CODDIAGNO_CAUSAASOCIADA1 + ' - ' + rtrim(D1.NOMDIAGNO)  as 'DiagnosticoAsociado1'
				,CODDIAGNO_CAUSAASOCIADA2 + ' - ' + rtrim(D2.NOMDIAGNO)  as 'DiagnosticoAsociado2'
				,CODDIAGNO_CAUSAASOCIADA3 + ' - ' + rtrim(D3.NOMDIAGNO)  as 'DiagnosticoAsociado3'
				,convert(varchar(10),FECHAEGRESO,103) as 'FechaEgreso'
				,CASE EGRESO WHEN '1' THEN 'X' END AS 'Para Casa', CASE EGRESO WHEN '2' THEN 'X' END AS 'Remitida'
				,CASE ABORTOSEPTICO WHEN '1' THEN 'X' END AS 'Abortoseptico_SI',CASE   WHEN  ABORTOSEPTICO IS NULL  OR  ABORTOSEPTICO = '0'  THEN 'X' END AS 'Abortoseptico_NO'
				,CASE ENFERMEDADMOLAR  WHEN '1' THEN 'X' END AS 'Molar_SI',CASE    WHEN  ENFERMEDADMOLAR IS NULL  OR  ENFERMEDADMOLAR = '0'  THEN 'X' END AS 'Molar_NO'
				,CASE EMBARAZOECTOPICO  WHEN '1' THEN 'X' END AS 'EmbarazoEctopico_SI',CASE    WHEN  EMBARAZOECTOPICO IS NULL  OR  EMBARAZOECTOPICO = '0'  THEN 'X' END AS 'EmbarazoEctopico_NO'
				,CASE ENFAUTOINMUNE  WHEN '1' THEN 'X' END AS 'Inmune_SI',CASE    WHEN  ENFAUTOINMUNE IS NULL  OR  ENFAUTOINMUNE = '0'  THEN 'X' END AS 'Inmune_NO'
				,CASE ENFGASTROINTESTINAL  WHEN '1' THEN 'X' END AS 'Gastro_SI',CASE    WHEN  ENFGASTROINTESTINAL IS NULL  OR  ENFGASTROINTESTINAL = '0'  THEN 'X' END AS 'Gastro_NO'
				,CASE EVENTOS  WHEN '1' THEN 'X' END AS 'Eventos_SI',CASE    WHEN EVENTOS  IS NULL  OR  EVENTOS = '0'  THEN 'X' END AS 'Eventos_NO'
				,CASE ENFCARDIOVASCULAR  WHEN '1' THEN 'X' END AS 'Cardiovascular_SI',CASE    WHEN  ENFCARDIOVASCULAR IS NULL  OR  ENFCARDIOVASCULAR = '0'  THEN 'X' END AS 'Cardiovascular_NO'
				,CASE OTRASENFERESPECIFICARELACIONADA  WHEN '1' THEN 'X' END AS 'OtrasEnfer_SI',CASE    WHEN  OTRASENFERESPECIFICARELACIONADA IS NULL  OR  OTRASENFERESPECIFICARELACIONADA = '0'  THEN 'X' END AS 'OtrasEnfer_NO'
				, VERSION AS 'VERSION' 
				, JSON As 'JSON' 
				, RTRIM( JSON_VALUE(JSON,'$.CIRUADICIONAL1_CUAL') )  AS 'CirugiaAdicional1_Cual'
				, CASE JSON_VALUE(JSON,'$.CIRUADICIONAL2_TIPO')  WHEN '1' THEN 'X' END AS 'CirugiaAdicional2_Histerectomia',CASE JSON_VALUE(JSON,'$.CIRUADICIONAL2_TIPO') WHEN '2' THEN 'X' END AS 'CirugiaAdicional2_Laparotomio',CASE JSON_VALUE(JSON,'$.CIRUADICIONAL2_TIPO') WHEN '3' THEN 'X' END AS 'CirugiaAdicional2_Legrado',CASE JSON_VALUE(JSON,'$.CIRUADICIONAL2_TIPO') WHEN '4' THEN 'X' END AS 'CirugiaAdicional2_Otra'
				, RTRIM( JSON_VALUE(JSON,'$.CIRUADICIONAL2_CUAL') ) AS 'CirugiaAdicional2_Cual'

			From 
				HCFICHA549 F 
				LEFT JOIN INDIAGNOS D on F.CODDIAGNO_CAUSAPRINCI = D.CODDIAGNO
				LEFT JOIN INDIAGNOS D1 on F.CODDIAGNO_CAUSAASOCIADA1 = D1.CODDIAGNO
				LEFT JOIN INDIAGNOS D2 on F.CODDIAGNO_CAUSAASOCIADA2 = D2.CODDIAGNO
				LEFT JOIN INDIAGNOS D3 on F.CODDIAGNO_CAUSAASOCIADA3 = D3.CODDIAGNO

			where 
				F.IDFICHANOTIFICACION  = @IdFicha
				AND ISJSON(F.JSON) > 0		

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera el detalle completo de una ficha SIVIGILA 549 (notificación obligatoria de morbilidad materna extrema) identificada por su ID de ficha. Consolida información obstétrica de la paciente: antecedentes ginecológicos (número de gestaciones, partos vaginales, cesáreas, abortos, ectópicos, molas, vivos y muertos), datos del control prenatal, forma de terminación de la gestación y momento de ocurrencia, así como la presencia o ausencia de enfermedades asociadas (eclampsia, preeclampsia, sepsis, hemorragia, ruptura uterina, aborto séptico, embarazo ectópico, patologías autoinmunes, hematológicas, oncológicas, endocrinas, renales, gastrointestinales, tromboembólicas y cardiocerebrovasculares) y falla orgánica por sistema (cardiaca, vascular, renal, hepática, metabólica, cerebral, respiratoria y coagulación). Incluye también datos de remisión (si la paciente fue remitida, tiempo de trámite e instituciones de referencia), diagnósticos CIE-10 de causa principal y causas asociadas enriquecidos con su nombre desde el catálogo INDIAGNOS, ingreso a UCI, cirugías adicionales y condición de egreso. Se utiliza para imprimir o visualizar la ficha de reporte epidemiológico obligatorio de morbilidad materna extrema exigida por el SIVIGILA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila549';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila549';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera y formatea los datos de una ficha de notificación SIVIGILA 549 (Morbilidad Materna Extrema) presentando indicadores clínicos como marcas ''X'' y enriqueciendo diagnósticos con su descripción.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila549';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La ficha identificada debe existir en HCFICHA549.; La columna JSON debe contener un JSON válido (ISJSON(JSON) > 0); de lo contrario no se devuelve fila.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila549';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Para cada par SI/NO solo se marca una opción: ''1'' produce SI; NULL o ''0'' produce NO; otros valores quedan sin marca.; Los diagnósticos se concatenan como ''CODIGO - NOMBRE'' usando LEFT JOIN, por lo que un código sin coincidencia conserva el código pero con nombre nulo.; Las fechas se devuelven en formato dd/mm/yyyy (estilo 103).; Solo se procesa la fila cuya columna JSON sea un JSON válido.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila549';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha SIVIGILA 549 (Morbilidad Materna Extrema); Antecedentes obstétricos (gestaciones, partos, cesáreas, abortos, molas, ectópicos); Control prenatal; Terminación de la gestación; Enfermedades maternas (eclampsia, sepsis, hemorragia, preeclampsia, ruptura uterina, aborto séptico, embarazo ectópico); Falla orgánica (cardíaca, vascular, renal, hepática, metabólica, cerebral, respiratoria, coagulación); Ingreso a UCI; Cirugía adicional (histerectomía, laparotomía, legrado); Transfusión; Diagnóstico principal y diagnósticos asociados (CIE); Egreso hospitalario / Remisión; Eventos de salud pública (intoxicación, intento suicida, violencia)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila549';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCFICHA549: Devuelve la ficha cuando IDFICHANOTIFICACION coincide con el parámetro y el contenido de JSON es válido (ISJSON > 0).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila549';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si INGRESAREMI = ''1'' → Marca ''remitido_SI'' = ''X'' else Si INGRESAREMI IS NULL o ''0'' marca ''remitido_NO'' = ''X''; si TERMGESTACION ∈ {1,2,3,4} → Mapea a Aborto / Parto / Parto Instrumentado / Cesárea respectivamente; si MOMOCUGESTACION ∈ {1,2,3} → Indica si la terminación de la gestación ocurrió Antes / Durante / Después; si EGRESO = ''1'' vs ''2'' → Marca egreso ''Para Casa'' o ''Remitida''; si CIRUADICIONAL1 ∈ {1,2,3,4} → Marca tipo de cirugía adicional: Histerectomía / Laparotomía / Legrado / Otra; si JSON_VALUE(JSON,''$.CIRUADICIONAL2_TIPO'') ∈ {1,2,3,4} → Marca segunda cirugía adicional: Histerectomía / Laparotomía / Legrado / Otra; si Para cada indicador clínico (ECLAMPSIA, SEPSIS, HEMORRAGIA, PREECLAMPSIA, RUPTUTERINA, ABOSEPTICO, EMBAECTOPICO, AUTOINMUNE, HEMATOLOGICA, ONCOLOGIA, ENDOCRINO, RENALES, GASTROINTESTINALES, EVETROMBOEMBOLI, CARDIOCEREBROVASC, OTRAS, fallas orgánicas, INGRESOUCI, TRANSFUSION, ACCIDENTE, etc.) valor = ''1'' → Marca columna _SI = ''X'' else Si NULL o ''0'' marca columna _NO = ''X''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila549';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA549; dbo.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila549';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila549';
-- GO
