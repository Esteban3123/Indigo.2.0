
CREATE PROCEDURE [dbo].[SPREP_HC_Evolucion_CabeceraHistoria]
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
    SELECT A.NUMINGRES AS INGRESO, A.NUMEFOLIO AS 'NUMERO FOLIO', RTRIM(B.nomcenate) + ' - ' + RTRIM(C.UFUDESCRI) AS UBICACION, FECINIATE AS 'FECHA INICIAL DE ATENCION', SUBJETIVO, ANALISISP AS ANALISIS, 
      dbo.DestinoPaciente(INDICAPAC) AS 'DESTINO DEL PACIENTE', INDICAMED AS 'INDICACIONES MEDICAS', D.NOMMEDICO AS 'NOMBRE MEDICO',D.MEDIFIRMA AS 'FIRMA PROFESIONAL',D.TARJETAPR AS 'TARJETA PROFESIONAL',RTRIM(CAST(TENARTSIS AS CHAR)) + '/' + RTRIM(CAST(TENARTDIA AS CHAR)) AS 'TENSION ARTERIAL', (CAST(TENARTSIS AS DECIMAL) + (CAST(TENARTDIA AS DECIMAL)*2))/3 AS 'TENSION ARTERIAL MEDIA',TEMPERPAC AS 'TEMPERATURA PACIENTE', FRECARPAC AS 'FRECUENCIA CARDIACA', FRERESPAC AS 'FRECUENCIA RESPIRATORIA', REGSO2PAC AS 'SATURACION DE OXIGENO', TALLAPACI AS 'TALLA PACIENTE', PESOPACIE AS 'PESO PACIENTE',
       CASE WHEN(PESOPACIE <> 0 AND TALLAPACI <> 0) THEN (CAST(PESOPACIE AS DECIMAL)/1000)/((CAST(TALLAPACI AS DECIMAL)/ 100)*(CAST(TALLAPACI AS DECIMAL)/ 100)) END AS 'INDICE DE MASA CORPORAL',FISOBSPAC AS OBSERVACIONES, CASE ANOCABPAC WHEN 1 THEN 0 WHEN 0 THEN 1 END 'CHECK CABEZA NORMAL',ANOCABPAC AS 'CHECK CABEZA ANOMALIA', DESCABPAC AS 'DESCRIPCION ANOMALIA CABEZA', CASE ANOOJOPAC WHEN 1 THEN 0 WHEN 0 THEN 1 END 'CHECK OJOS NORMAL',ANOOJOPAC AS 'CHECK OJOS ANOMALIA', DESOJOPAC AS 'DESCRIPCION ANOMALIA OJOS', CASE ANOORLPAC WHEN 1 THEN 0 WHEN 0 THEN 1 END 'CHECK ORL NORMAL', 
       ANOORLPAC AS 'CHECK ORL ANOMALIA', DESORLPAC AS 'DESCRIPCION ANOMALIA ORL',CASE ANOCUEPAC WHEN 1 THEN 0 WHEN 0 THEN 1 END 'CHECK CUELLO NORMAL', ANOCUEPAC AS 'CHECK CUELLO ANOMALIA', DESCUEPAC AS 'DESCRIPCION ANOMALIA CUELLO', CASE ANOCAPPAC WHEN 1 THEN 0 WHEN 0 THEN 1 END 'CHECK CARDIOPULMONAR NORMAL', ANOCAPPAC AS 'CHECK CARDIOPULMONAR ANOMALIA', DESCAPPAC AS 'DESCRIPCION ANOMALIA CARDIOPULMONAR', 
       CASE ANOABDPAC WHEN 1 THEN 0 WHEN 0 THEN 1 END 'CHECK ABDOMEN NORMAL', ANOABDPAC AS 'CHECK ABDOMEN ANOMALIA', DESABDPAC AS 'DESCRIPCION ANOMALIA ABDOMEN', CASE ANOGEUOAC WHEN 1 THEN 0 WHEN 0 THEN 1 END 'CHECK GENITOURINARIO NORMAL', ANOGEUOAC AS 'CHECK GENITOURINARIO ANOMALIA', DESGEUOAC AS 'DESCRIPCION ANOMALIA GENITOURINARIO', CASE ANOEXTPAC WHEN 1 THEN 0 WHEN 0 THEN 1 END 'CHECK EXTREMIDADES NORMAL', 
       ANOEXTPAC AS 'CHECK EXTREMIDADES ANOMALIA', DESEXTPAC AS 'DESCRIPCION ANOMALIA EXTREMIDADES', CASE ANONEUPAC WHEN 1 THEN 0 WHEN 0 THEN 1 END 'CHECK NEUROLOGICA NORMAL', ANONEUPAC AS 'CHECK NEUROLOGICA ANOMALIA', DESNEUPAC AS 'DESCRIPCION ANOMALIA NEUROLOGICA', CASE ANOPIELPA WHEN 1 THEN 0 WHEN 0 THEN 1 END 'CHECK PIEL NORMAL',ANOPIELPA AS 'CHECK PIEL ANOMALIA', DESPEILPA AS 'DESCRIPCION ANOMALIA PIEL', 
       NEOPERCEF AS 'PERIMETRO CEFALICO', NEOPERTOR AS 'PERIMETRO TORAXICO', NEOPERABD AS 'PERIMETRO ABDOMINAL',CASE SOPVENPAC WHEN 1 THEN 0 WHEN 0 THEN 1 END AS 'CHECK SOPORTE VENTILATORIO NO',SOPVENPAC AS 'CHECK SOPORTE VENTILATORIO SI', SOPVENDES AS 'DESCRIPCION SOPORTE VENTILATORIO',CASE SOPINOPAC WHEN 1 THEN 0 WHEN 0 THEN 1 END AS 'CHECK SOPORTE INOTROPICO NO',SOPINOPAC AS 'CHECK SOPORTE INOTROPICO SI', 
       SOPINODES AS 'DESCRIPCION SOPORTE INOTROPICO', CASE ACCESOPAC WHEN 1 THEN 0 WHEN 0 THEN 1 END AS 'CHECK ACCESO NO', ACCESOPAC AS 'CHECK ACCESO SI', ACCESODES AS 'DESCRIPCION ACCESO', UCIADUPVC AS 'UCI ADULTOS PVC', UCIADUCUN AS 'UCI ADULTOS CUNA', UCIADUPIA AS 'UCI ADULTOS PIA', UCIADUGLU AS 'UCI ADULTOS GLUCOMETRIA', UCIADULRG AS 'UCI ADULTOS RG', UCIADUPIC AS 'UCI ADULTOS PIC', RTRIM(E.DESESPECI) AS 'DESCRIPCION DE LA ESPECIALIDAD',RTRIM(C.UFUDESCRI) AS 'DESCRIPCION UNIDAD FUNCIONAL',
       COALESCE(NULLIF(F.IAUTORIZA,''),'') AS 'NUMERO DE AUTORIZACION DE INGRESO',F.ITIPORIES AS 'TIPO DE RIESGO', H.FECFIRFOL AS 'FECHA FIRMA FOLIO',
	   PESOSEC, TFG, ESTADIO, ACCVASCULAR, ULTRAFILTRA, ULTRAFILTRAV, TIEMPOSESIO, KTV, G.IDHCPAREXFISC,PLAINDMED AS 'PLANTILLA RECOMENDACIONES',VALORTAM  AS 'MASA CORPORAL',G.PB as 'PB',g.DOLOR as 'DOLOR' ,
	    CASE WHEN(PESOPACIE <> 0 AND TALLAPACI <> 0) THEN ( CASE  WHEN DATEDIFF(YEAR, IPFECNACI, FECINIATE) >= 18 THEN Sqrt( ( (CAST(PESOPACIE AS DECIMAL) / 1000) * CAST(TALLAPACI AS DECIMAL)) / 3600) 
         WHEN DATEDIFF(YEAR, IPFECNACI, FECINIATE) < 18 THEN ( CASE	WHEN PESOPACIE < 10000 THEN ( ( ( ( CAST(PESOPACIE AS DECIMAL) / 1000) * 4) + 9) / 100 ) 
        WHEN PESOPACIE >= 10000 THEN ( ( ( ( CAST(PESOPACIE AS DECIMAL) / 1000) * 4) + 7) /  ( ( CAST(PESOPACIE AS DECIMAL) / 1000) + 90) )  END )	END )  END AS 'SUPERFICIE CORPORAL TOTAL'
      
	  FROM HCURGEVO1 A with(nolock)
       INNER JOIN ADcenaten B with(nolock) ON A.CODCENATE=B.codcenate 
	   INNER JOIN INUNIFUNC C with(nolock) ON A.UFUCODIGO=C.UFUCODIGO
	   INNER JOIN INPROFSAL D with(nolock) ON A.CODPROSAL=D.CODPROSAL 
	   INNER JOIN INESPECIA E with(nolock) ON A.CODESPTRA=E.CODESPECI 
	   INNER JOIN ADINGRESO F with(nolock) ON A.NUMINGRES=F.NUMINGRES 
	   LEFT OUTER JOIN HCEXFISIC G with(nolock) ON A.NUMEFOLIO=G.NUMEFOLIO AND A.IPCODPACI=G.IPCODPACI AND A.NUMINGRES=G.NUMINGRES 
	   LEFT OUTER JOIN HCFIRMFOL H with(nolock) ON A.IPCODPACI = H.IPCODPACI AND A.NUMEFOLIO = H.NUMEFOLIO 
	   INNER JOIN INPACIENT AS PAC with(nolock) ON A.IPCODPACI = PAC.IPCODPACI 

WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO=@NumeroFolio

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que obtiene la cabecera completa de una evolución clínica de urgencias para un folio específico de un paciente e ingreso determinados. Consolida información del episodio clínico: datos del centro de atención y unidad funcional, profesional tratante con su firma y tarjeta profesional, especialidad, signos vitales (tensión arterial, temperatura, frecuencia cardíaca, frecuencia respiratoria, saturación de oxígeno), medidas antropométricas (talla, peso, IMC, superficie corporal total calculada según edad), examen físico por sistemas (cabeza, ojos, ORL, cuello, cardiopulmonar, abdomen, genitourinario, extremidades, neurológico, piel), notas SOAP (subjetivo, análisis, indicaciones médicas, destino del paciente), parámetros de UCI y nefrología (PVC, PIA, glucometría, KTV, estadio, acceso vascular, ultrafiltración), firma del folio y número de autorización de ingreso. Combina la evolución de urgencias (HCURGEVO1) con el examen físico (HCEXFISIC), el ingreso (ADINGRESO), el profesional (INPROFSAL), la especialidad (INESPECIA), la unidad funcional (INUNIFUNC), el centro de atención (ADCENATEN), la firma del folio (HCFIRMFOL) y el paciente (INPACIENT). Se usa para imprimir o visualizar el reporte de la evolución médica en la historia clínica de urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Evolucion_CabeceraHistoria';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Evolucion_CabeceraHistoria';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera la cabecera de la historia clínica de evolución de urgencias para un paciente, ingreso y folio específicos, consolidando datos de examen físico, signos vitales, soportes clínicos, autorización y firma del folio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Evolucion_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros relacionados en HCURGEVO1, ADcenaten, INUNIFUNC, INPROFSAL, INESPECIA, ADINGRESO e INPACIENT para que el INNER JOIN devuelva filas.; El paciente, número de ingreso y folio enviados deben coincidir con un registro existente en HCURGEVO1.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Evolucion_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La tensión arterial media se calcula como (sistólica + 2*diastólica)/3.; El IMC solo se calcula cuando peso y talla son distintos de cero, evitando división por cero.; La superficie corporal se calcula con fórmula de Mosteller para adultos (≥18 años) y fórmulas pediátricas alternativas según peso para menores.; Los flags de normalidad y anomalía por sistema corporal son mutuamente excluyentes (suman 1).; El número de autorización de ingreso nunca retorna NULL: se sustituye por cadena vacía mediante COALESCE/NULLIF.; Se usa NOLOCK en todas las lecturas, asumiendo tolerancia a lecturas sucias.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Evolucion_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Historia clínica de urgencias; Evolución médica; Examen físico por sistemas (cabeza, ojos, ORL, cuello, cardiopulmonar, abdomen, genitourinario, extremidades, neurológico, piel); Signos vitales (TA, FC, FR, SatO2, temperatura); Índice de Masa Corporal; Superficie corporal total; Soporte ventilatorio; Soporte inotrópico; Acceso vascular; UCI adultos (PVC, PIA, PIC, glucometría); Diálisis/Nefrología (TFG, estadio, KTV, ultrafiltración, tiempo de sesión, peso seco); Autorización de ingreso; Tipo de riesgo; Firma de folio; Especialidad médica; Unidad funcional; Centro de atención; Profesional de salud (firma y tarjeta profesional); Antropometría neonatal (perímetros cefálico, torácico, abdominal); Indicaciones médicas; Destino del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Evolucion_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCURGEVO1: Devuelve los datos de la evolución filtrando por IPCODPACI, NUMINGRES y NUMEFOLIO recibidos como parámetros.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Evolucion_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PESOPACIE <> 0 AND TALLAPACI <> 0 → Calcula el Índice de Masa Corporal como (peso/1000) / ((talla/100)^2). else Devuelve NULL para IMC y para superficie corporal total.; si DATEDIFF(YEAR, IPFECNACI, FECINIATE) >= 18 → Calcula la superficie corporal total como Sqrt((peso_kg * talla) / 3600) (fórmula de Mosteller para adultos). else Aplica fórmulas pediátricas dependientes del peso.; si Paciente menor de 18 años y PESOPACIE < 10000 (gramos) → Superficie corporal = ((peso_kg * 4) + 9) / 100. else Si peso >= 10000 g: ((peso_kg * 4) + 7) / (peso_kg + 90).; si Campos de anomalía (ANOCABPAC, ANOOJOPAC, ANOORLPAC, etc.) = 1 → Marca el check de anomalía y desmarca el de normalidad (valor 0). else Si = 0, marca el check de normalidad (valor 1).; si SOPVENPAC/SOPINOPAC/ACCESOPAC = 1 → Indica presencia (SI) y desmarca el check NO. else Indica ausencia (NO) cuando el valor es 0.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Evolucion_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.DestinoPaciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Evolucion_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCURGEVO1; dbo.ADcenaten; dbo.INUNIFUNC; dbo.INPROFSAL; dbo.INESPECIA; dbo.ADINGRESO; dbo.HCEXFISIC; dbo.HCFIRMFOL; dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Evolucion_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Evolucion_CabeceraHistoria';
-- GO
