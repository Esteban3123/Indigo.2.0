

CREATE VIEW [Report].[ViewProductividadSala] AS

SELECT DISTINCT 
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
HC.IPCODPACI AS PACIENTE_DOCUMENTO_IDENTIFICACION,
HC.NUMINGRES AS PACIENTE_NUMERO_INGRESO,
HC.CODDIAPRE AS DIAGNOSTICO_PREVIO,
HC.CODDIAPOS AS DIAGNOSTICO_POSTERIOR,
HC.SALACIRUG AS SALA_CIRUGIA,
UPPER(HC.DESPROCED) AS DESCRIPCION_PROCEDIMIENTO,
CASE HC.TIPOANEST
WHEN '1' THEN 'LOCAL' 
WHEN '2' THEN 'REGIONAL' 
WHEN '3' THEN 'GENERAL' 
WHEN '4' THEN 'COMBINADA' 
WHEN '5' THEN 'NO APLICA'
ELSE 'DESCONOCIDO' END AS TIPO_ANESTESIA,
	CASE HC.TIPHERCIR
	WHEN '1' THEN 'LIMPIA' 
	WHEN '2' THEN 'LIMPIA CONTAMINADA'
	WHEN '3' THEN 'CONTAMINADA'
	WHEN '4' THEN 'SUCIA E INFECTADA'
	WHEN '5' THEN 'SUCIA'
	WHEN '6' THEN 'INFECTADA'
	WHEN '7' THEN 'NO APLICA'
	ELSE 'DESCONOCIDO' END AS TIPO_HERIDA,
	IC.DESSERIPS AS PROCEDIMIENTO_REALIZADO,
	IC.CODSERIPS AS PROCEDIMIENTO_REALIZADO_CODIGO,
	CASE IC.PRESERIPS
	WHEN '1' THEN 'NO QUIRURGICO' 
	WHEN '2' THEN 'QUIRURGICO'
	WHEN '3' THEN 'PAQUETE'
	ELSE 'DESCONOCIDO' END AS PRESENTACION_SERVICIO,
		CASE IC.TIPSERIPS
		 WHEN '1' THEN 'LABORATORIOS' 
		 WHEN '2' THEN 'PATOLOGIAS' 
		 WHEN '3' THEN 'IMAGENES DIAGNOSTICAS' 
		 WHEN '4' THEN 'PROCEDIMIENTOS NO QX' 
		 WHEN '5' THEN 'PROCEDIMIENTOS QX' 
		 WHEN '6' THEN 'INTERCONSULTAS' 
		 WHEN '7' THEN 'NINGUNO' 
		 WHEN '8' THEN 'CONSULTA EXTERNA'
		 ELSE 'DESCONOCIDO' END AS TIPO_SERVICIO,
			CASE IC.CLASERIPS  
			WHEN '1' THEN 'NINGUNO'
			WHEN '2' THEN 'CIRUJANO'
			WHEN '3' THEN 'ANESTESIOLOGO'
			WHEN '4' THEN 'AYUDANTE'
			WHEN '5' THEN 'DERECHO_SALA'
			WHEN '6' THEN 'MATERIALES_SUTURA'
			WHEN '7' THEN 'INSTRUMENTACION_QUIRURGICA'
			ELSE 'DESCONOCIDO' END AS CLASE_SERVICIO,
P.IPNOMCOMP    AS PACIENTE_NOMBRE_COMPLETO,
P.AUUBICACI    AS PACIENTE_UBICACION_GEOGRAFICA,
P.IPFECNACI    AS FECHA_NACIMIENTO,
P.GENCONENTITY AS ENTIDAD_RESPONSABLE_PAGO,
DI.NOMDIAGNO   AS NOMBRE_DIAGNOSTICO_PREVIO,
DF.NOMDIAGNO   AS NOMBRE_DIAGNOSTICO_POSTERIOR,
UF.UFUDESCRI   AS UNIDAD_FUNCIONAL,
PS.NOMMEDICO   AS PROFESIONAL_SALUD,
PS.CODPROSAL   AS PROFESIONAL_SALUD_CODIGO,
CASE WHEN IA1.DESESPECI IS NULL THEN 'NO APLICA' ELSE  IA1.DESESPECI END AS PROFESIONAL_SALUD_ESPECIALIDAD_PRNICIPAL,
CASE WHEN IA2.DESESPECI IS NULL THEN 'NO APLICA' ELSE  IA2.DESESPECI END AS PROFESIONAL_SALUD_ESPECIALIDAD_2,
CASE WHEN IA3.DESESPECI IS NULL THEN 'NO APLICA' ELSE  IA3.DESESPECI END AS PROFESIONAL_SALUD_ESPECIALIDAD_3,
IC.CODSERIPS   AS CODIGO_PROCEDIMIENTO,
HC.FECHORINI AS FECHA_HORA_INICIAL,
HC.FECHORFIN AS FECHA_HORA_FINAL,
1 as 'CANTIDAD',
CAST(HC.FECHORINI AS date) AS 'FECHA BUSQUEDA',
YEAR(HC.FECHORINI) AS 'AÑO FECHA BUSQUEDA',
MONTH(HC.FECHORINI) AS 'MES AÑO FECHA BUSQUEDA',
CASE MONTH(HC.FECHORINI) 
	WHEN 1 THEN 'ENERO'
	WHEN 2 THEN 'FEBRERO'
	WHEN 3 THEN 'MARZO'
	WHEN 4 THEN 'ABRIL'
	WHEN 5 THEN 'MAYO'
	WHEN 6 THEN 'JUNIO'
	WHEN 7 THEN 'JULIO'
	WHEN 8 THEN 'AGOSTO'
	WHEN 9 THEN 'SEPTIEMBRE'
	WHEN 10 THEN 'OCTUBRE'
	WHEN 11 THEN 'NOVIEMBRE'
	WHEN 12 THEN 'DICIEMBRE'
  END AS 'MES NOMBRE FECHA BUSQUEDA',
FORMAT(DAY(HC.FECHORINI), '00') AS 'DIA FECHA BUSQUEDA',
CONCAT(FORMAT(MONTH(HC.FECHORINI), '00') ,' - ', 
	   CASE MONTH(HC.FECHORINI) 
	        WHEN 1 THEN 'ENERO'
			WHEN 2 THEN 'FEBRERO'
			WHEN 3 THEN 'MARZO'
			WHEN 4 THEN 'ABRIL'
			WHEN 5 THEN 'MAYO'
			WHEN 6 THEN 'JUNIO'
			WHEN 7 THEN 'JULIO'
			WHEN 8 THEN 'AGOSTO'
			WHEN 9 THEN 'SEPTIEMBRE'
			WHEN 10 THEN 'OCTUBRE'
			WHEN 11 THEN 'NOVIEMBRE'
			WHEN 12 THEN 'DICIEMBRE'
		END) MES_LABEL_BUSQUEDA,
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM DBO.HCQXINFOR HC  WITH (NOLOCK)
LEFT JOIN DBO.INPACIENT P  WITH (NOLOCK) ON HC.IPCODPACI=P.IPCODPACI
LEFT JOIN DBO.INDIAGNOS DI WITH (NOLOCK) ON HC.CODDIAPRE=DI.CODDIAGNO 
LEFT JOIN DBO.INDIAGNOS DF WITH (NOLOCK) ON HC.CODDIAPOS=DF.CODDIAGNO 
LEFT JOIN DBO.INUNIFUNC UF WITH (NOLOCK) ON HC.UFUCODIGO=UF.UFUCODIGO
LEFT JOIN DBO.INPROFSAL PS WITH (NOLOCK) ON HC.CODPROSAL=PS.CODPROSAL
LEFT JOIN DBO.INCUPSIPS IC WITH (NOLOCK) ON HC.CODSERIPS=IC.CODSERIPS
LEFT JOIN DBO.INESPECIA IA1 WITH (NOLOCK) ON PS.CODESPEC1=IA1.CODESPECI 
LEFT JOIN DBO.INESPECIA IA2 WITH (NOLOCK) ON PS.CODESPEC2=IA2.CODESPECI 
LEFT JOIN DBO.INESPECIA IA3 WITH (NOLOCK) ON PS.CODESPEC3=IA3.CODESPECI
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting orientada al análisis de productividad quirúrgica por sala. Consolida en un registro aplanado los procedimientos realizados en quirófano, combinando datos del acto quirúrgico (sala, tipo de anestesia, clasificación de herida, horarios de inicio y fin) con información del paciente, diagnósticos previo y posterior, unidad funcional, profesional de salud con hasta tres especialidades, y el servicio CUPS/RIPS asociado. Expone dimensiones temporales desagregadas (año, mes, día) para facilitar el consumo en herramientas de inteligencia de negocios o reportes de gestión hospitalaria.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewProductividadSala';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewProductividadSala';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone un dataset consolidado de productividad de sala de cirugía, integrando información del paciente, diagnósticos, procedimientos, profesional de salud y dimensiones temporales para reportería.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewProductividadSala';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en HCQXINFOR (cabecera de procedimientos quirúrgicos) con códigos consistentes hacia INPACIENT, INDIAGNOS, INUNIFUNC, INPROFSAL e INCUPSIPS para enriquecer la información.; El servidor debe soportar la zona horaria ''Pakistan Standard Time'' usada en el cálculo de ULT_ACTUAL.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewProductividadSala';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila representa un registro quirúrgico de HCQXINFOR; siempre incluye una constante CANTIDAD = 1 para conteos en reportería.; ID_COMPANY siempre es el nombre de la base de datos actual (DB_NAME()) truncado a 9 caracteres.; La descripción del procedimiento (DESPROCED) se expone siempre en mayúsculas.; ULT_ACTUAL siempre se calcula convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''.; Las dimensiones temporales (año, mes numérico, mes nombre, día con dos dígitos, etiqueta mes) se derivan exclusivamente de HC.FECHORINI.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewProductividadSala';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Diagnóstico previo y posterior (CIE); Sala de cirugía; Procedimiento quirúrgico; Tipo de anestesia; Tipo de herida quirúrgica; Servicio CUPS; Profesional de salud y especialidades; Unidad funcional; Entidad responsable de pago; Productividad quirúrgica', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewProductividadSala';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewProductividadSala: Retorna filas DISTINCT con datos quirúrgicos enriquecidos; las relaciones con paciente, diagnóstico previo/posterior, unidad funcional, profesional, CUPS y especialidades se hacen vía LEFT JOIN, por lo que la fila se conserva aunque falten esos catálogos.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewProductividadSala';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si HC.TIPOANEST ∈ {''1'',''2'',''3'',''4'',''5''} → Mapea a etiqueta de tipo de anestesia: LOCAL, REGIONAL, GENERAL, COMBINADA o NO APLICA. else Etiqueta como ''DESCONOCIDO''.; si HC.TIPHERCIR ∈ {''1''..''7''} → Clasifica la herida quirúrgica (LIMPIA, LIMPIA CONTAMINADA, CONTAMINADA, SUCIA E INFECTADA, SUCIA, INFECTADA, NO APLICA). else Etiqueta como ''DESCONOCIDO''.; si IC.PRESERIPS ∈ {''1'',''2'',''3''} → Clasifica presentación del servicio como NO QUIRURGICO, QUIRURGICO o PAQUETE. else ''DESCONOCIDO''.; si IC.TIPSERIPS ∈ {''1''..''8''} → Clasifica tipo de servicio (LABORATORIOS, PATOLOGIAS, IMAGENES DIAGNOSTICAS, PROCEDIMIENTOS NO QX, PROCEDIMIENTOS QX, INTERCONSULTAS, NINGUNO, CONSULTA EXTERNA). else ''DESCONOCIDO''.; si IC.CLASERIPS ∈ {''1''..''7''} → Clasifica clase de servicio (NINGUNO, CIRUJANO, ANESTESIOLOGO, AYUDANTE, DERECHO_SALA, MATERIALES_SUTURA, INSTRUMENTACION_QUIRURGICA). else ''DESCONOCIDO''.; si IA1/IA2/IA3.DESESPECI IS NULL → La especialidad del profesional se reporta como ''NO APLICA''. else Se reporta la descripción de la especialidad.; si MONTH(HC.FECHORINI) entre 1 y 12 → Traduce el mes a su nombre en español (ENERO..DICIEMBRE) y construye etiqueta ''MM - NOMBRE''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewProductividadSala';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DBO.HCQXINFOR; DBO.INPACIENT; DBO.INDIAGNOS; DBO.INUNIFUNC; DBO.INPROFSAL; DBO.INCUPSIPS; DBO.INESPECIA', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewProductividadSala';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewProductividadSala';
GO
