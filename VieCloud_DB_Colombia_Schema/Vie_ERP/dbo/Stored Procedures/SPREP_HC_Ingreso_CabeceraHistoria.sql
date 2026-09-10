-- Stored Procedure

CREATE PROCEDURE [dbo].[SPREP_HC_Ingreso_CabeceraHistoria]
(
@CodigoPaciente Varchar(25),
@NumeroIngreso Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    SELECT A.NUMINGRES AS INGRESO, A.NUMEFOLIO AS 'NUMERO FOLIO', A.IPCODPACI AS 'CODIGO PACIENTE', RTRIM(B.nomcenate) + ' - ' + RTRIM(C.UFUDESCRI) AS UBICACION, RTRIM(IPPRIAPEL) + ' ' + RTRIM(IPSEGAPEL) AS APELLIDOS, RTRIM(IPPRINOMB) + ' ' + RTRIM(IPSEGNOMB) AS NOMBRES, 
       IPFECNACI AS 'EDAD', '' AS 'FECHA DE NACIMIENTO', CASE IPSEXOPAC WHEN 1 THEN 'MASCULINO' ELSE 'FEMENINO' END AS SEXO,CASE IPTIPODOC WHEN 1 THEN 'CC' WHEN 2 THEN 'CE' WHEN 3 THEN 'TI' WHEN 4 THEN 'RC' WHEN 5 THEN 'PA' WHEN 6 THEN 'AS' WHEN 7 THEN 'MS' WHEN 8 THEN 'NU' END 'TIPO DOCUMENTO', 
       RTRIM(IPDIRECCI) AS DIRECCION,RTRIM(D.IPTELEFON) + ' - ' + RTRIM(D.IPTELMOVI) AS TELEFONO,CASE IPTIPOPAC WHEN 1 THEN 'CONTRIBUTIVO' WHEN 2 THEN 'SUBSIDIADO' WHEN 3 THEN 'VINCULADO' WHEN 4 THEN 'PARTICULAR' WHEN 5 THEN 'OTRO' WHEN 6 THEN 'DESPLAZADO REG. CONTRIBUTIVO' WHEN 7 THEN 'DESPLAZADO REG. SUBSIDIADO' WHEN 8 THEN 'DESPLAZADO NO ASEGURADO' END AS 'TIPO PACIENTE', 
       CASE IPTIPOAFI WHEN 0 THEN 'NO APLICA' WHEN 1 THEN 'COTIZANTE' WHEN 2 THEN 'BENEFICIARIO' WHEN 3 THEN 'ADICIONAL' WHEN 4 THEN 'JUBILADO / RETIRADO' WHEN 5 THEN 'PENSIONADO' END AS 'TIPO AFILIADO',RTRIM(E.Nomentida) AS 'NOMBRE DE LA ENTIDAD', 
       FECINIATE AS 'FECHA INICIAL DE ATENCION', ANALISISP AS ANALISIS,MOTCONSUL AS 'MOTIVO CONSULTA',ENFACTUAL AS 'ENFERMEDAD ACTUAL', REVSISTEMA AS 'REVISION POR SISTEMAS',ANTOTRPAC AS 'OTROS ANTECEDENTES',A.UFUCODIGO AS 'CODIGO UNIDAD FUNCIONAL',
       CASE INDICAPAC WHEN 1 THEN 'TRASLADAR A URGENCIAS' WHEN 2 THEN 'TRASLADAR A OBSERVACIÓN URGENCIAS' WHEN 3 THEN 'TRASLADAR A HOSPITALIZACION' WHEN 4 THEN 'TRASLADAR A  UCI ADULTO' WHEN 5 THEN 'TRASLADAR A UCI PEDIATRICA' WHEN 6 THEN 'TRASLADAR A UCI NEONATAL' WHEN 7 THEN 'TRASLADAR A CONSULTA EXTERNA' WHEN 8 THEN 'TRASLADAR A  CIRUGÍA' WHEN 9 THEN 'HOSPITALIZACIÓN EN CASA' WHEN 10 THEN 'REFERENCIA' WHEN 11 THEN 'MORGUE' WHEN 12 THEN 'SALIDA' WHEN 13 THEN 'CONTINUA EN LA UNIDAD' END AS 'DESTINO DEL PACIENTE',
       INDICAMED AS 'INDICACIONES MEDICAS', H.NOMMEDICO AS 'NOMBRE MEDICO',H.TARJETAPR AS 'TARJETA PROFESIONAL',
       RTRIM(CAST(TENARTSIS AS CHAR)) + '/' + RTRIM(CAST(TENARTDIA AS CHAR)) AS 'TENSION ARTERIAL', TEMPERPAC AS 'TEMPERATURA PACIENTE', FRECARPAC AS 'FRECUENCIA CARDIACA', FRERESPAC AS 'FRECUENCIA RESPIRATORIA', REGSO2PAC AS 'SATURACION DE OXIGENO', TALLAPACI AS 'TALLA PACIENTE', PESOPACIE AS 'PESO PACIENTE',FISOBSPAC AS OBSERVACIONES,
       CASE ANOCABPAC WHEN 1 THEN 0 WHEN 0 THEN 1 END 'CHECK CABEZA NORMAL',ANOCABPAC AS 'CHECK CABEZA ANOMALIA', DESCABPAC AS 'DESCRIPCION ANOMALIA CABEZA', CASE ANOOJOPAC WHEN 1 THEN 0 WHEN 0 THEN 1 END 'CHECK OJOS NORMAL',ANOOJOPAC AS 'CHECK OJOS ANOMALIA', DESOJOPAC AS 'DESCRIPCION ANOMALIA OJOS', CASE ANOORLPAC WHEN 1 THEN 0 WHEN 0 THEN 1 END 'CHECK ORL NORMAL', 
       ANOORLPAC AS 'CHECK ORL ANOMALIA', DESORLPAC AS 'DESCRIPCION ANOMALIA ORL',CASE ANOCUEPAC WHEN 1 THEN 0 WHEN 0 THEN 1 END 'CHECK CUELLO NORMAL', ANOCUEPAC AS 'CHECK CUELLO ANOMALIA', DESCUEPAC AS 'DESCRIPCION ANOMALIA CUELLO', CASE ANOCAPPAC WHEN 1 THEN 0 WHEN 0 THEN 1 END 'CHECK CARDIOPULMONAR NORMAL', ANOCAPPAC AS 'CHECK CARDIOPULMONAR ANOMALIA', DESCAPPAC AS 'DESCRIPCION ANOMALIA CARDIOPULMONAR', 
       CASE ANOABDPAC WHEN 1 THEN 0 WHEN 0 THEN 1 END 'CHECK ABDOMEN NORMAL', ANOABDPAC AS 'CHECK ABDOMEN ANOMALIA', DESABDPAC AS 'DESCRIPCION ANOMALIA ABDOMEN', CASE ANOGEUOAC WHEN 1 THEN 0 WHEN 0 THEN 1 END 'CHECK GENITOURINARIO NORMAL', ANOGEUOAC AS 'CHECK GENITOURINARIO ANOMALIA', DESGEUOAC AS 'DESCRIPCION ANOMALIA GENITOURINARIO', CASE ANOEXTPAC WHEN 1 THEN 0 WHEN 0 THEN 1 END 'CHECK EXTREMIDADES NORMAL', 
       ANOEXTPAC AS 'CHECK EXTREMIDADES ANOMALIA', DESEXTPAC AS 'DESCRIPCION ANOMALIA EXTREMIDADES', CASE ANONEUPAC WHEN 1 THEN 0 WHEN 0 THEN 1 END 'CHECK NEUROLOGICA NORMAL', ANONEUPAC AS 'CHECK NEUROLOGICA ANOMALIA', DESNEUPAC AS 'DESCRIPCION ANOMALIA NEUROLOGICA', CASE ANOPIELPA WHEN 1 THEN 0 WHEN 0 THEN 1 END 'CHECK PIEL NORMAL',ANOPIELPA AS 'CHECK PIEL ANOMALIA', DESPEILPA AS 'DESCRIPCION ANOMALIA PIEL', 
       NEOPERCEF AS 'PERIMETRO CEFALICO', NEOPERTOR AS 'PERIMETRO TORAXICO', NEOPERABD AS 'PERIMETRO ABDOMINAL',CASE SOPVENPAC WHEN 1 THEN 0 WHEN 0 THEN 1 END AS 'CHECK SOPORTE VENTILATORIO NO',SOPVENPAC AS 'CHECK SOPORTE VENTILATORIO SI', SOPVENDES AS 'DESCRIPCION SOPORTE VENTILATORIO',CASE SOPINOPAC WHEN 1 THEN 0 WHEN 0 THEN 1 END AS 'CHECK SOPORTE INOTROPICO NO',SOPINOPAC AS 'CHECK SOPORTE INOTROPICO SI', 
       SOPINODES AS 'DESCRIPCION SOPORTE INOTROPICO', CASE ACCESOPAC WHEN 1 THEN 0 WHEN 0 THEN 1 END AS 'CHECK ACCESO NO', ACCESOPAC AS 'CHECK ACCESO SI', ACCESODES AS 'DESCRIPCION ACCESO', UCIADUPVC AS 'UCI ADULTOS PVC', UCIADUCUN AS 'UCI ADULTOS CUNA', UCIADUPIA AS 'UCI ADULTOS PIA', UCIADUGLU AS 'UCI ADULTOS GLUCOMETRIA', UCIADULRG AS 'UCI ADULTOS RG', UCIADUPIC AS 'UCI ADULTOS PIC', RTRIM(DESESPECI) AS 'DESCRIPCION DE LA ESPECIALIDAD',RTRIM(C.UFUDESCRI) AS 'DESCRIPCION UNIDAD FUNCIONAL'
     
       FROM HCURGING1 A with(nolock)
       INNER JOIN ADcenaten B with(nolock) ON A.CODCENATE=B.codcenate 
	   INNER JOIN INUNIFUNC C with(nolock) ON A.UFUCODIGO=C.UFUCODIGO
	   INNER JOIN INPacient D with(nolock) ON A.IPCODPACI=D.IPCODPACI
	   INNER JOIN HCEXFISIC G with(nolock) ON A.NUMEFOLIO=G.NUMEFOLIO AND A.IPCODPACI=G.IPCODPACI AND A.NUMINGRES=G.NUMINGRES 
	   INNER JOIN INPROFSAL H with(nolock) ON A.CODPROSAL=H.CODPROSAL 
	   INNER JOIN INESPECIA K with(nolock) ON H.CODESPEC1=K.CODESPECI 
	   INNER JOIN ADINGRESO I with(nolock) ON A.NUMINGRES=I.NUMINGRES 
	   INNER JOIN INEntidad E with(nolock) ON I.CODENTIDA=E.Codentida 
	   LEFT OUTER JOIN HCANTPACI F with(nolock) ON A.NUMEFOLIO=F.NUMEFOLIO AND A.IPCODPACI=F.IPCODPACI AND A.NUMINGRES=F.NUMINGRES 
	   
	   WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND F.NUMEFOLIO<='6' 	   

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera la cabecera completa de la historia clínica de urgencias para un ingreso específico de un paciente, combinando datos demográficos del paciente (nombre, documento, fecha de nacimiento, sexo, dirección, teléfono, tipo de afiliación), información del ingreso (número de ingreso, folio, ubicación por sede y unidad funcional), datos del profesional tratante (médico, tarjeta profesional, especialidad) y el contenido clínico inicial de urgencias (motivo de consulta, enfermedad actual, revisión de sistemas, análisis, indicaciones, destino del paciente). Integra el examen físico detallado por sistemas (cabeza, ojos, ORL, cuello, cardiopulmonar, abdomen, genitourinario, extremidades, neurológico, piel) junto con signos vitales (tensión arterial, temperatura, frecuencia cardíaca y respiratoria, saturación de oxígeno, talla, peso) y parámetros especiales de UCI y neonatología. Recibe como parámetros la cédula o código del paciente y el número de ingreso, y se usa para imprimir o visualizar el encabezado oficial de la historia clínica de urgencias en el módulo de historia clínica del ERP.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Ingreso_CabeceraHistoria';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Ingreso_CabeceraHistoria';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la cabecera consolidada de la historia clínica de ingreso (datos del paciente, ubicación, signos vitales, examen físico, soportes y destino) para un paciente y número de ingreso específicos, destinada a reporte.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Ingreso_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el ingreso en HCURGING1 para el paciente y número de ingreso indicados.; El paciente debe tener registros relacionados en INPacient, ADINGRESO, HCEXFISIC y el profesional asociado en INPROFSAL con especialidad en INESPECIA (los INNER JOIN excluyen registros sin estas relaciones).; El ingreso debe estar asociado a una entidad existente en INEntidad.; Debe existir antecedente en HCANTPACI con NUMEFOLIO<=''6'' (el filtro en WHERE convierte el LEFT JOIN en obligatorio).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Ingreso_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El reporte solo incluye registros con NUMEFOLIO<=''6'' en HCANTPACI (limita los folios de antecedentes considerados).; El check de ''normal'' y ''anomalía'' por sistema son mutuamente excluyentes (uno 0, el otro 1).; El campo EDAD se devuelve a partir de la fecha de nacimiento (IPFECNACI) y la ''FECHA DE NACIMIENTO'' se devuelve vacía.; La ubicación se construye concatenando centro de atención (ADcenaten) y unidad funcional (INUNIFUNC).; El teléfono se construye concatenando teléfono fijo y móvil del paciente.; La tensión arterial se reporta como sistólica/diastólica concatenadas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Ingreso_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Historia clínica de urgencias; Centro de atención; Unidad funcional; Entidad (aseguradora); Tipo de documento; Tipo de paciente (régimen); Tipo de afiliado; Profesional de la salud; Especialidad médica; Examen físico por sistemas; Signos vitales; Soporte ventilatorio; Soporte inotrópico; Acceso vascular; UCI adultos (PVC, PIA, PIC, glucometría); Antecedentes del paciente; Destino del paciente; Indicaciones médicas; Perímetros neonatales (cefálico, torácico, abdominal)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Ingreso_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCURGING1: Cuando A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND F.NUMEFOLIO<=''6'', retorna un resultset con la cabecera de la historia clínica del ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Ingreso_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPSEXOPAC = 1 → Reporta sexo ''MASCULINO'' else Reporta sexo ''FEMENINO''; si IPTIPODOC entre 1..8 → Mapea a tipo de documento (CC, CE, TI, RC, PA, AS, MS, NU) else NULL; si IPTIPOPAC entre 1..8 → Mapea tipo de paciente (CONTRIBUTIVO, SUBSIDIADO, VINCULADO, PARTICULAR, OTRO, DESPLAZADO REG. CONTRIBUTIVO/SUBSIDIADO/NO ASEGURADO) else NULL; si IPTIPOAFI entre 0..5 → Mapea tipo de afiliado (NO APLICA, COTIZANTE, BENEFICIARIO, ADICIONAL, JUBILADO/RETIRADO, PENSIONADO) else NULL; si INDICAPAC entre 1..13 → Mapea destino del paciente (urgencias, observación, hospitalización, UCI adulto/pediátrica/neonatal, consulta externa, cirugía, hospitalización en casa, referencia, morgue, salida, continúa en la unidad) else NULL; si Banderas de anomalía por sistema (ANOCABPAC, ANOOJOPAC, ANOORLPAC, ANOCUEPAC, ANOCAPPAC, ANOABDPAC, ANOGEUOAC, ANOEXTPAC, ANONEUPAC, ANOPIELPA) = 1 → Marca ''check anomalía'' = 1 y ''check normal'' = 0 para el sistema correspondiente else Si = 0, marca ''check normal'' = 1 y ''check anomalía'' = 0; si Banderas de soporte (SOPVENPAC, SOPINOPAC, ACCESOPAC) = 1 → Marca ''check SI'' y ''check NO''=0 else Si = 0, marca ''check NO'' = 1', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Ingreso_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCURGING1; dbo.ADcenaten; dbo.INUNIFUNC; dbo.INPacient; dbo.HCEXFISIC; dbo.INPROFSAL; dbo.INESPECIA; dbo.ADINGRESO; dbo.INEntidad; dbo.HCANTPACI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Ingreso_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Ingreso_CabeceraHistoria';
-- GO
