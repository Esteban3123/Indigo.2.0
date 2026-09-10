
CREATE PROCEDURE [dbo].[SPREP_HC_Evolucion_CabeceraHistoriaInternos]
(
@CodigoPaciente Varchar(25),
@NumeroIngreso Char(10),
@NumeroFolio nchar(10)	
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

 -- Insert statements for procedure here
    SELECT TOP 10 A.NUMINGRES AS INGRESO, A.NUMEFOLIO AS 'NUMERO FOLIO', RTRIM(B.nomcenate) + ' - ' + RTRIM(C.UFUDESCRI) AS UBICACION, FECINIATE AS 'FECHA INICIAL DE ATENCION', SUBJETIVO, ANALISISP AS ANALISIS, 
      dbo.DestinoPaciente(INDICAPAC) AS 'DESTINO DEL PACIENTE', INDICAMED AS 'INDICACIONES MEDICAS', D.NOMMEDICO AS 'NOMBRE MEDICO',D.MEDIFIRMA AS 'FIRMA PROFESIONAL',D.TARJETAPR AS 'TARJETA PROFESIONAL',RTRIM(CAST(TENARTSIS AS CHAR)) + '/' + RTRIM(CAST(TENARTDIA AS CHAR)) AS 'TENSION ARTERIAL', (CAST(TENARTSIS AS DECIMAL) + (CAST(TENARTDIA AS DECIMAL)*2))/3 AS 'TENSION ARTERIAL MEDIA',TEMPERPAC AS 'TEMPERATURA PACIENTE', FRECARPAC AS 'FRECUENCIA CARDIACA', FRERESPAC AS 'FRECUENCIA RESPIRATORIA', REGSO2PAC AS 'SATURACION DE OXIGENO', TALLAPACI AS 'TALLA PACIENTE', PESOPACIE AS 'PESO PACIENTE',
       CASE WHEN(PESOPACIE <> 0 AND TALLAPACI <> 0) THEN (CAST(PESOPACIE AS DECIMAL)/1000)/((CAST(TALLAPACI AS DECIMAL)/ 100)*(CAST(TALLAPACI AS DECIMAL)/ 100)) END AS 'INDICE DE MASA CORPORAL',FISOBSPAC AS OBSERVACIONES, CASE ANOCABPAC WHEN 1 THEN 0 WHEN 0 THEN 1 END 'CHECK CABEZA NORMAL',ANOCABPAC AS 'CHECK CABEZA ANOMALIA', DESCABPAC AS 'DESCRIPCION ANOMALIA CABEZA', CASE ANOOJOPAC WHEN 1 THEN 0 WHEN 0 THEN 1 END 'CHECK OJOS NORMAL',ANOOJOPAC AS 'CHECK OJOS ANOMALIA', DESOJOPAC AS 'DESCRIPCION ANOMALIA OJOS', CASE ANOORLPAC WHEN 1 THEN 0 WHEN 0 THEN 1 END 'CHECK ORL NORMAL', 
       ANOORLPAC AS 'CHECK ORL ANOMALIA', DESORLPAC AS 'DESCRIPCION ANOMALIA ORL',CASE ANOCUEPAC WHEN 1 THEN 0 WHEN 0 THEN 1 END 'CHECK CUELLO NORMAL', ANOCUEPAC AS 'CHECK CUELLO ANOMALIA', DESCUEPAC AS 'DESCRIPCION ANOMALIA CUELLO', CASE ANOCAPPAC WHEN 1 THEN 0 WHEN 0 THEN 1 END 'CHECK CARDIOPULMONAR NORMAL', ANOCAPPAC AS 'CHECK CARDIOPULMONAR ANOMALIA', DESCAPPAC AS 'DESCRIPCION ANOMALIA CARDIOPULMONAR', 
       CASE ANOABDPAC WHEN 1 THEN 0 WHEN 0 THEN 1 END 'CHECK ABDOMEN NORMAL', ANOABDPAC AS 'CHECK ABDOMEN ANOMALIA', DESABDPAC AS 'DESCRIPCION ANOMALIA ABDOMEN', CASE ANOGEUOAC WHEN 1 THEN 0 WHEN 0 THEN 1 END 'CHECK GENITOURINARIO NORMAL', ANOGEUOAC AS 'CHECK GENITOURINARIO ANOMALIA', DESGEUOAC AS 'DESCRIPCION ANOMALIA GENITOURINARIO', CASE ANOEXTPAC WHEN 1 THEN 0 WHEN 0 THEN 1 END 'CHECK EXTREMIDADES NORMAL', 
       ANOEXTPAC AS 'CHECK EXTREMIDADES ANOMALIA', DESEXTPAC AS 'DESCRIPCION ANOMALIA EXTREMIDADES', CASE ANONEUPAC WHEN 1 THEN 0 WHEN 0 THEN 1 END 'CHECK NEUROLOGICA NORMAL', ANONEUPAC AS 'CHECK NEUROLOGICA ANOMALIA', DESNEUPAC AS 'DESCRIPCION ANOMALIA NEUROLOGICA', CASE ANOPIELPA WHEN 1 THEN 0 WHEN 0 THEN 1 END 'CHECK PIEL NORMAL',ANOPIELPA AS 'CHECK PIEL ANOMALIA', DESPEILPA AS 'DESCRIPCION ANOMALIA PIEL', 
       NEOPERCEF AS 'PERIMETRO CEFALICO', NEOPERTOR AS 'PERIMETRO TORAXICO', NEOPERABD AS 'PERIMETRO ABDOMINAL',CASE SOPVENPAC WHEN 1 THEN 0 WHEN 0 THEN 1 END AS 'CHECK SOPORTE VENTILATORIO NO',SOPVENPAC AS 'CHECK SOPORTE VENTILATORIO SI', SOPVENDES AS 'DESCRIPCION SOPORTE VENTILATORIO',CASE SOPINOPAC WHEN 1 THEN 0 WHEN 0 THEN 1 END AS 'CHECK SOPORTE INOTROPICO NO',SOPINOPAC AS 'CHECK SOPORTE INOTROPICO SI', 
       SOPINODES AS 'DESCRIPCION SOPORTE INOTROPICO', CASE ACCESOPAC WHEN 1 THEN 0 WHEN 0 THEN 1 END AS 'CHECK ACCESO NO', ACCESOPAC AS 'CHECK ACCESO SI', ACCESODES AS 'DESCRIPCION ACCESO', UCIADUPVC AS 'UCI ADULTOS PVC', UCIADUCUN AS 'UCI ADULTOS CUNA', UCIADUPIA AS 'UCI ADULTOS PIA', UCIADUGLU AS 'UCI ADULTOS GLUCOMETRIA', UCIADULRG AS 'UCI ADULTOS RG', UCIADUPIC AS 'UCI ADULTOS PIC', RTRIM(E.DESESPECI) AS 'DESCRIPCION DE LA ESPECIALIDAD',RTRIM(C.UFUDESCRI) AS 'DESCRIPCION UNIDAD FUNCIONAL',
       COALESCE(NULLIF(F.IAUTORIZA,''),'') AS 'NUMERO DE AUTORIZACION DE INGRESO',F.ITIPORIES AS 'TIPO DE RIESGO'
       FROM HCURGEVOI A WITH(NOLOCK)
       INNER JOIN ADcenaten B WITH(NOLOCK) ON A.CODCENATE=B.codcenate 
	   INNER JOIN INUNIFUNC C WITH(NOLOCK) ON A.UFUCODIGO=C.UFUCODIGO
	   INNER JOIN INPROFSAL D WITH(NOLOCK) ON A.CODPROSAL=D.CODPROSAL 
	   INNER JOIN INESPECIA E WITH(NOLOCK) ON A.CODESPTRA=E.CODESPECI 
	   INNER JOIN ADINGRESO F WITH(NOLOCK) ON A.NUMINGRES=F.NUMINGRES 
	   LEFT OUTER JOIN HCEXFISII G WITH(NOLOCK) ON A.NUMEFOLIO=G.NUMEFOLIO AND A.IPCODPACI=G.IPCODPACI AND A.NUMINGRES=G.NUMINGRES 

WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO=@NumeroFolio

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el encabezado completo de la historia clínica de evolución para pacientes internos (hospitalizados o en urgencias), combinando la nota de evolución médica en formato SOAP (subjetivo y análisis), los signos vitales y el examen físico por sistemas corporales con los datos del centro de atención, la unidad funcional, el profesional de salud tratante y su especialidad. Recibe como parámetros la cédula del paciente, el número de ingreso y el número de folio, y devuelve hasta 10 registros con información clínica detallada incluyendo tensión arterial, temperatura, frecuencia cardiaca, frecuencia respiratoria, saturación de oxígeno, talla, peso, índice de masa corporal, hallazgos por sistemas (cabeza, ojos, ORL, cuello, cardiopulmonar, abdomen, genitourinario, extremidades, neurológico, piel), soportes ventilatorio e inotrópico, parámetros de UCI adultos, indicaciones médicas, destino del paciente, número de autorización de ingreso y tipo de riesgo. Se usa principalmente para imprimir o visualizar la nota de evolución clínica del paciente durante su hospitalización o atención en urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Evolucion_CabeceraHistoriaInternos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Evolucion_CabeceraHistoriaInternos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera la cabecera de la evolución de historia clínica de pacientes internos, consolidando datos de ingreso, signos vitales, examen físico, soporte clínico, profesional tratante y autorización.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Evolucion_CabeceraHistoriaInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro de evolución (HCURGEVOI) que coincida con paciente, ingreso y folio recibidos.; Deben existir las relaciones de centro de atención, unidad funcional, profesional de salud, especialidad e ingreso administrativo asociadas a la evolución.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Evolucion_CabeceraHistoriaInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La tensión arterial media se calcula con la fórmula (sistólica + 2*diastólica)/3.; Los indicadores de ''normal'' por sistema corporal son siempre el complemento binario del indicador de ''anomalía''.; El número de autorización de ingreso nunca se devuelve como NULL: si está vacío o nulo se entrega cadena vacía.; El examen físico (HCEXFISII) es opcional: la consulta no excluye evoluciones sin examen físico registrado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Evolucion_CabeceraHistoriaInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Folio de historia clínica; Evolución médica; Signos vitales (TA, FC, FR, SatO2, temperatura); Tensión arterial media; Índice de masa corporal; Examen físico por sistemas (cabeza, ojos, ORL, cuello, cardiopulmonar, abdomen, genitourinario, extremidades, neurológico, piel); Perímetros neonatales (cefálico, torácico, abdominal); Soporte ventilatorio; Soporte inotrópico; Acceso vascular; Monitoreo UCI adultos (PVC, CUNA, PIA, glucometría, RG, PIC); Indicaciones médicas; Destino del paciente; Profesional tratante (firma y tarjeta profesional); Especialidad médica; Unidad funcional; Centro de atención; Autorización de ingreso; Tipo de riesgo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Evolucion_CabeceraHistoriaInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCURGEVOI: Devuelve hasta 10 registros (TOP 10) filtrando por paciente, número de ingreso y número de folio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Evolucion_CabeceraHistoriaInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PESOPACIE <> 0 AND TALLAPACI <> 0 → Calcula el Índice de Masa Corporal como (peso/1000)/((talla/100)^2) else El IMC se devuelve como NULL; si Campos de anomalía (ANOCABPAC, ANOOJOPAC, ANOORLPAC, ANOCUEPAC, ANOCAPPAC, ANOABDPAC, ANOGEUOAC, ANOEXTPAC, ANONEUPAC, ANOPIELPA) = 1 → Marca el indicador ''normal'' del sistema correspondiente como 0 (no normal) else Si valor = 0, marca ''normal'' como 1; si Indicadores de soporte (SOPVENPAC, SOPINOPAC, ACCESOPAC) = 1 → Marca el check ''NO'' como 0 (es decir, sí tiene soporte) else Si valor = 0, marca check ''NO'' como 1', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Evolucion_CabeceraHistoriaInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.DestinoPaciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Evolucion_CabeceraHistoriaInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCURGEVOI; dbo.ADcenaten; dbo.INUNIFUNC; dbo.INPROFSAL; dbo.INESPECIA; dbo.ADINGRESO; dbo.HCEXFISII', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Evolucion_CabeceraHistoriaInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Evolucion_CabeceraHistoriaInternos';
-- GO
