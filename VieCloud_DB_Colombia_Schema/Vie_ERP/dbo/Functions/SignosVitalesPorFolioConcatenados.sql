
CREATE FUNCTION [dbo].[SignosVitalesPorFolioConcatenados](@CodigoPaciente as varchar(25), @NumIngreso as char(10), @NumFolio as varchar(10), @SignosUltimoDia As bit)
RETURNS varchar(max)
AS
BEGIN
    DECLARE @SignosVitales as varchar(max)

IF @SignosUltimoDia = 0
BEGIN
	SELECT 
	@SignosVitales = CONCAT(
		IIF(TENARTSIS IS NULL,'',Concat('Tensión arterial: ', RTRIM(TENARTSIS), '/', RTRIM(TENARTDIA), ', ')),   
		IIF(TEMPERPAC IS NULL OR LEN(RTRIM(TEMPERPAC)) = 0,'',Concat('Temperatura: ', RTRIM(TEMPERPAC), '°', ', ')),   
		IIF(FRECARPAC IS NULL OR FRECARPAC = 0,'',Concat('Frecuencia cardíaca: ', RTRIM(FRECARPAC), ', ')),   
		IIF(FRERESPAC IS NULL OR FRERESPAC = 0,'',Concat('Frecuencia respiratoria: ', RTRIM(FRERESPAC), ', ')),  
		IIF(REGSO2PAC IS NULL OR REGSO2PAC = 0,'',Concat('Saturación oxígeno: ', RTRIM(REGSO2PAC), ', ')),   
		IIF(TALLAPACI IS NULL OR TALLAPACI = 0,'',Concat('Talla: ', TALLAPACI, ', ')),   
		IIF(PESOPACIE IS NULL OR PESOPACIE = 0,'',Concat('Peso: ', FORMAT(PESOPACIE/1000, 'N1'), ', ')),  
		IIF(NEOPERCEF IS NULL OR NEOPERCEF = 0,'',Concat('Perímetro cefálico: ', NEOPERCEF, ', ')),  
		IIF(NEOPERTOR IS NULL OR NEOPERTOR = 0,'',Concat('Perímetro toráxico: ', RTRIM(NEOPERTOR), ', ')),   
		IIF(NEOPERABD IS NULL OR NEOPERABD = 0,'',Concat('Perímetro abdominal: ', NEOPERABD, ', ')), 
		IIF(PESOSEC IS NULL OR PESOSEC = 0,'',Concat('Peso seco: ', PESOSEC, ', ')), 
		IIF(TFG IS NULL OR LEN(RTRIM(TFG)) = 0,'',Concat('Tasa de filtración glomerular: ', RTRIM(TFG), ', ')),  
		IIF(ESTADIO IS NULL OR ESTADIO = 0,'',Concat('Estadio: ', RTRIM(ESTADIO), ', ')),   
		IIF(UCIADUCUN IS NULL OR UCIADUCUN = 0,'',Concat('Cuña: ', RTRIM(UCIADUCUN), ', ')),   
		IIF(UCIADUPIA IS NULL OR UCIADUPIA = 0,'',Concat('PIA: ', UCIADUPIA, ', ')),   
		IIF(UCIADUPVC IS NULL OR UCIADUPVC = 0,'',Concat('PVC: ', UCIADUPVC, ', ')),  
		IIF(UCIADUGLU IS NULL OR UCIADUGLU = 0,'',Concat('Glucometría: ', UCIADUGLU, ', ')),  
		IIF(UCIADULRG IS NULL OR UCIADULRG = 0,'',Concat('RG: ', RTRIM(UCIADULRG), ', ')),   
		IIF(UCIADUPIC IS NULL OR UCIADUPIC = 0,'',Concat('PIC: ', UCIADUPIC, ', ')),   
		IIF(ULTRAFILTRA IS NULL OR ULTRAFILTRA = 0,'',Concat('Ultrafiltración: ', CASE ULTRAFILTRA WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),  
		IIF(ULTRAFILTRAV IS NULL OR ULTRAFILTRAV = 0,'',Concat('Valor ultrafiltración: ', RTRIM(ULTRAFILTRAV), ', ')),   
		IIF(TIEMPOSESIO IS NULL OR TIEMPOSESIO = 0,'',Concat('Tiempo sesión: ', TIEMPOSESIO, ', ')),   
		IIF(KTV IS NULL OR KTV = 0,'',Concat('KTV: ', KTV)),  
		IIF(DIURESIS IS NULL OR DIURESIS = 0,'',Concat('Diuresis: ', CASE DIURESIS WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),   
		IIF(MECONIO IS NULL OR MECONIO = 0,'',Concat('Meconio: ', CASE MECONIO WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),   
		IIF(SUCCION IS NULL OR SUCCION = 0,'',Concat('Succión: ', CASE SUCCION WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),  
		IIF(DEGLUCION IS NULL OR DEGLUCION = 0,'',Concat('Deglución: ', CASE DEGLUCION WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),   
		IIF(VALORTAM IS NULL OR VALORTAM = 0,'',Concat('Valor tensión arterial media: ', VALORTAM, ', ')),   
		IIF(SENSIBILIZADA IS NULL OR SENSIBILIZADA = 0,'',Concat('Sensibilizada: ', SENSIBILIZADA)),   
		IIF(COOMBS IS NULL OR COOMBS = 0,'',Concat('Coombs: ', COOMBS, ', ')),  
		IIF(TSH IS NULL OR TSH = 0,'',Concat('TSH(mUI/L): ', RTRIM(TSH), ', ')),
		IIF(PB IS NULL OR PB = 0,'',Concat('Perímetro braquial: ', RTRIM(PB), ', ')),   
		IIF(DOLOR IS NULL,'',Concat('Dolor: ', DOLOR, ', ')),
		IIF(FETOCARDIA IS NULL OR FETOCARDIA = 0,'',Concat('Fetocardia: ', FETOCARDIA, ', ')),  
		IIF(GRUPOSANGUINEO IS NULL,'',Concat('Grupo sanguíneo: ', GRUPOSANGUINEO, ', ')),  
		IIF(RHSANGUINEO IS NULL ,'',Concat('RH: ', CASE RHSANGUINEO WHEN 1 THEN '+' WHEN 0 THEN '-' END, ', ')),   
		IIF(PESOPARAEDADGESTACIONAL IS NULL,'',Concat('Peso para la edad gestacional: ', CASE PESOPARAEDADGESTACIONAL WHEN 1 THEN 'Pequeño para la edad gestacional' WHEN 2 THEN 'Adecuado para la edad gestacional' WHEN 3 THEN 'Grande para la edad gestacional' END, ', ')),  
		IIF(SATURACIONPREDUCTAL IS NULL OR SATURACIONPREDUCTAL = 0,'',Concat('Saturación preductal: ', RTRIM(SATURACIONPREDUCTAL), ', ')),   
		IIF(SATURACIONPOSDUCTAL IS NULL OR SATURACIONPOSDUCTAL = 0,'',Concat('Saturación posductal: ', SATURACIONPOSDUCTAL, ', ')),   
		IIF(PAMIEMBROSUPERIORDERECHO IS NULL OR PAMIEMBROSUPERIORDERECHO = 0,'',Concat('Presión arterial del miembro superiror derecho: ', PAMIEMBROSUPERIORDERECHO, ', ')),  
		IIF(PAMIEMBROSUPERIORIZQUIERDO IS NULL OR PAMIEMBROSUPERIORIZQUIERDO = 0,'',Concat('Presión arterial del miembro superiror izquierdo: ', RTRIM(PAMIEMBROSUPERIORIZQUIERDO), ', ')),   
		IIF(PAMIEMBROINFERIORDERECHO IS NULL OR PAMIEMBROINFERIORDERECHO = 0,'',Concat('Presión arterial del miembro inferior derecho: ', PAMIEMBROINFERIORDERECHO)),   
		IIF(PAMIEMBROINFERIORIZQUIERDO IS NULL OR PAMIEMBROINFERIORIZQUIERDO = 0,'',Concat('Presión arterial del miembro inferior izquierdo: ', PAMIEMBROINFERIORIZQUIERDO)),  
		IIF(INTERPESOPARATALLA IS NULL OR LEN(RTRIM(INTERPESOPARATALLA)) = 0,'',Concat('Interpretación de gráfica peso para la talla: ', INTERPESOPARATALLA, ', ')),  
		IIF(INTERINDICEMASACO IS NULL OR LEN(RTRIM(INTERINDICEMASACO)) = 0,'',Concat('Interpretación de gráfica indice masa corporal: ', RTRIM(INTERINDICEMASACO), ', ')),   
		IIF(INTERPESOPARAEDAD IS NULL OR LEN(RTRIM(INTERPESOPARAEDAD)) = 0,'',Concat('Interpretación de gráfica peso para la edad: ', INTERPESOPARAEDAD)),   
		IIF(INTERPERIMETROCEFA IS NULL OR LEN(RTRIM(INTERPERIMETROCEFA)) = 0,'',Concat('Interpretación de gráfica perímetro cefálico: ', INTERPERIMETROCEFA, ', ')),  
		IIF(INTERTALLAPARAEDAD IS NULL OR LEN(RTRIM(INTERTALLAPARAEDAD)) = 0,'',Concat('Interpretación de grafica talla para la edad: ', RTRIM(INTERTALLAPARAEDAD), ', ')),   
		IIF(INTERIMCPARALAEDAD IS NULL OR LEN(RTRIM(INTERIMCPARALAEDAD)) = 0,'',Concat('Interpretación de gráfica IMC para la edad: ', INTERIMCPARALAEDAD, ', ')))
	FROM dbo.HCEXFISIC 
	WHERE IPCODPACI=@CodigoPaciente AND NUMINGRES= @NumIngreso AND NUMEFOLIO=@NumFolio
END
ELSE
BEGIN
	SELECT 
	@SignosVitales = STRING_AGG(CONCAT(
		CONCAT(CONVERT(varchar(50), FECREGITE, 20), ': '),
		IIF(TENARTSIS IS NULL,'',Concat('Tensión arterial: ', RTRIM(TENARTSIS), '/', RTRIM(TENARTDIA), ', ')),   
		IIF(TEMPERPAC IS NULL OR LEN(RTRIM(TEMPERPAC)) = 0,'',Concat('Temperatura: ', RTRIM(TEMPERPAC), '°', ', ')),   
		IIF(FRECARPAC IS NULL OR FRECARPAC = 0,'',Concat('Frecuencia cardíaca: ', RTRIM(FRECARPAC), ', ')),   
		IIF(FRERESPAC IS NULL OR FRERESPAC = 0,'',Concat('Frecuencia respiratoria: ', RTRIM(FRERESPAC), ', ')),  
		IIF(REGSO2PAC IS NULL OR REGSO2PAC = 0,'',Concat('Saturación oxígeno: ', RTRIM(REGSO2PAC), ', ')),   
		IIF(TALLAPACI IS NULL OR TALLAPACI = 0,'',Concat('Talla: ', TALLAPACI, ', ')),   
		IIF(PESOPACIE IS NULL OR PESOPACIE = 0,'',Concat('Peso: ', FORMAT(PESOPACIE/1000, 'N1'), ', ')),  
		IIF(NEOPERCEF IS NULL OR NEOPERCEF = 0,'',Concat('Perímetro cefálico: ', NEOPERCEF, ', ')),  
		IIF(NEOPERTOR IS NULL OR NEOPERTOR = 0,'',Concat('Perímetro toráxico: ', RTRIM(NEOPERTOR), ', ')),   
		IIF(NEOPERABD IS NULL OR NEOPERABD = 0,'',Concat('Perímetro abdominal: ', NEOPERABD, ', ')), 
		IIF(PESOSEC IS NULL OR PESOSEC = 0,'',Concat('Peso seco: ', PESOSEC, ', ')), 
		IIF(TFG IS NULL OR LEN(RTRIM(TFG)) = 0,'',Concat('Tasa de filtración glomerular: ', RTRIM(TFG), ', ')),  
		IIF(ESTADIO IS NULL OR ESTADIO = 0,'',Concat('Estadio: ', RTRIM(ESTADIO), ', ')),   
		IIF(UCIADUCUN IS NULL OR UCIADUCUN = 0,'',Concat('Cuña: ', RTRIM(UCIADUCUN), ', ')),   
		IIF(UCIADUPIA IS NULL OR UCIADUPIA = 0,'',Concat('PIA: ', UCIADUPIA, ', ')),   
		IIF(UCIADUPVC IS NULL OR UCIADUPVC = 0,'',Concat('PVC: ', UCIADUPVC, ', ')),  
		IIF(UCIADUGLU IS NULL OR UCIADUGLU = 0,'',Concat('Glucometría: ', UCIADUGLU, ', ')),  
		IIF(UCIADULRG IS NULL OR UCIADULRG = 0,'',Concat('RG: ', RTRIM(UCIADULRG), ', ')),   
		IIF(UCIADUPIC IS NULL OR UCIADUPIC = 0,'',Concat('PIC: ', UCIADUPIC, ', ')),   
		IIF(ULTRAFILTRA IS NULL OR ULTRAFILTRA = 0,'',Concat('Ultrafiltración: ', CASE ULTRAFILTRA WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),  
		IIF(ULTRAFILTRAV IS NULL OR ULTRAFILTRAV = 0,'',Concat('Valor ultrafiltración: ', RTRIM(ULTRAFILTRAV), ', ')),   
		IIF(TIEMPOSESIO IS NULL OR TIEMPOSESIO = 0,'',Concat('Tiempo sesión: ', TIEMPOSESIO, ', ')),   
		IIF(KTV IS NULL OR KTV = 0,'',Concat('KTV: ', KTV)),  
		IIF(DIURESIS IS NULL OR DIURESIS = 0,'',Concat('Diuresis: ', CASE DIURESIS WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),   
		IIF(MECONIO IS NULL OR MECONIO = 0,'',Concat('Meconio: ', CASE MECONIO WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),   
		IIF(SUCCION IS NULL OR SUCCION = 0,'',Concat('Succión: ', CASE SUCCION WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),  
		IIF(DEGLUCION IS NULL OR DEGLUCION = 0,'',Concat('Deglución: ', CASE DEGLUCION WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),   
		IIF(VALORTAM IS NULL OR VALORTAM = 0,'',Concat('Valor tensión arterial media: ', VALORTAM, ', ')),   
		IIF(SENSIBILIZADA IS NULL OR SENSIBILIZADA = 0,'',Concat('Sensibilizada: ', SENSIBILIZADA)),   
		IIF(COOMBS IS NULL OR COOMBS = 0,'',Concat('Coombs: ', COOMBS, ', ')),  
		IIF(TSH IS NULL OR TSH = 0,'',Concat('TSH(mUI/L): ', RTRIM(TSH), ', ')),
		IIF(PB IS NULL OR PB = 0,'',Concat('Perímetro braquial: ', RTRIM(PB), ', ')),   
		IIF(DOLOR IS NULL,'',Concat('Dolor: ', DOLOR, ', ')),
		IIF(FETOCARDIA IS NULL OR FETOCARDIA = 0,'',Concat('Fetocardia: ', FETOCARDIA, ', ')),  
		IIF(GRUPOSANGUINEO IS NULL,'',Concat('Grupo sanguíneo: ', GRUPOSANGUINEO, ', ')),  
		IIF(RHSANGUINEO IS NULL ,'',Concat('RH: ', CASE RHSANGUINEO WHEN 1 THEN '+' WHEN 0 THEN '-' END, ', ')),   
		IIF(PESOPARAEDADGESTACIONAL IS NULL,'',Concat('Peso para la edad gestacional: ', CASE PESOPARAEDADGESTACIONAL WHEN 1 THEN 'Pequeño para la edad gestacional' WHEN 2 THEN 'Adecuado para la edad gestacional' WHEN 3 THEN 'Grande para la edad gestacional' END, ', ')),  
		IIF(SATURACIONPREDUCTAL IS NULL OR SATURACIONPREDUCTAL = 0,'',Concat('Saturación preductal: ', RTRIM(SATURACIONPREDUCTAL), ', ')),   
		IIF(SATURACIONPOSDUCTAL IS NULL OR SATURACIONPOSDUCTAL = 0,'',Concat('Saturación posductal: ', SATURACIONPOSDUCTAL, ', ')),   
		IIF(PAMIEMBROSUPERIORDERECHO IS NULL OR PAMIEMBROSUPERIORDERECHO = 0,'',Concat('Presión arterial del miembro superiror derecho: ', PAMIEMBROSUPERIORDERECHO, ', ')),  
		IIF(PAMIEMBROSUPERIORIZQUIERDO IS NULL OR PAMIEMBROSUPERIORIZQUIERDO = 0,'',Concat('Presión arterial del miembro superiror izquierdo: ', RTRIM(PAMIEMBROSUPERIORIZQUIERDO), ', ')),   
		IIF(PAMIEMBROINFERIORDERECHO IS NULL OR PAMIEMBROINFERIORDERECHO = 0,'',Concat('Presión arterial del miembro inferior derecho: ', PAMIEMBROINFERIORDERECHO)),   
		IIF(PAMIEMBROINFERIORIZQUIERDO IS NULL OR PAMIEMBROINFERIORIZQUIERDO = 0,'',Concat('Presión arterial del miembro inferior izquierdo: ', PAMIEMBROINFERIORIZQUIERDO)),  
		IIF(INTERPESOPARATALLA IS NULL OR LEN(RTRIM(INTERPESOPARATALLA)) = 0,'',Concat('Interpretación de gráfica peso para la talla: ', INTERPESOPARATALLA, ', ')),  
		IIF(INTERINDICEMASACO IS NULL OR LEN(RTRIM(INTERINDICEMASACO)) = 0,'',Concat('Interpretación de gráfica indice masa corporal: ', RTRIM(INTERINDICEMASACO), ', ')),   
		IIF(INTERPESOPARAEDAD IS NULL OR LEN(RTRIM(INTERPESOPARAEDAD)) = 0,'',Concat('Interpretación de gráfica peso para la edad: ', INTERPESOPARAEDAD)),   
		IIF(INTERPERIMETROCEFA IS NULL OR LEN(RTRIM(INTERPERIMETROCEFA)) = 0,'',Concat('Interpretación de gráfica perímetro cefálico: ', INTERPERIMETROCEFA, ', ')),  
		IIF(INTERTALLAPARAEDAD IS NULL OR LEN(RTRIM(INTERTALLAPARAEDAD)) = 0,'',Concat('Interpretación de grafica talla para la edad: ', RTRIM(INTERTALLAPARAEDAD), ', ')),   
		IIF(INTERIMCPARALAEDAD IS NULL OR LEN(RTRIM(INTERIMCPARALAEDAD)) = 0,'',Concat('Interpretación de gráfica IMC para la edad: ', INTERIMCPARALAEDAD, ', ')))
		,', ')
	FROM dbo.HCEXFISIC 
	WHERE IPCODPACI=@CodigoPaciente AND NUMINGRES= @NumIngreso AND FECREGITE BETWEEN DATEADD(hour, -24, common.GETDATE()) AND Common.GETDATE()
END
    RETURN @SignosVitales
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que recopila y concatena en un único texto legible todos los signos vitales y medidas antropométricas registradas en la historia clínica de un paciente para un folio específico de atención. Consulta la tabla HCEXFISIC, que almacena el examen físico del paciente, y extrae valores como tensión arterial sistólica y diastólica, temperatura, frecuencia cardíaca, frecuencia respiratoria, saturación de oxígeno, talla, peso, perímetros (cefálico, torácico, abdominal, braquial), así como parámetros especializados de UCI (cuña, PIA, PVC, glucometría, PIC), datos de diálisis (ultrafiltración, tiempo de sesión, KTV, peso seco, tasa de filtración glomerular), indicadores neonatales (meconio, succión, deglución, fetocardia, grupo sanguíneo, RH, saturaciones pre y posductal, presiones en miembros) y escalas de dolor e interpretaciones de curvas de crecimiento. Recibe como parámetros el código del paciente (cédula/identificación), el número de ingreso, el número de folio y un indicador que permite filtrar únicamente los signos del último día de atención. Se usa principalmente para generar el resumen de signos vitales en la impresión o visualización de notas clínicas, evoluciones médicas y reportes de historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'SignosVitalesPorFolioConcatenados';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'SignosVitalesPorFolioConcatenados';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye una cadena legible que concatena los signos vitales y mediciones clínicas de un paciente, ya sea para un folio específico o para los registros de las últimas 24 horas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'SignosVitalesPorFolioConcatenados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros en HCEXFISIC para el paciente y el ingreso indicados.; Para el modo por folio, el folio debe corresponder a un registro existente del paciente/ingreso.; Para el modo de últimas 24 horas, la función common.GETDATE() debe estar disponible y devolver la hora actual de referencia.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'SignosVitalesPorFolioConcatenados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los campos nulos, vacíos o iguales a cero se omiten de la cadena resultante (excepto DOLOR, GRUPOSANGUINEO, RHSANGUINEO y PESOPARAEDADGESTACIONAL que solo se omiten si son NULL).; El peso (PESOPACIE) siempre se presenta dividido entre 1000 con formato numérico de un decimal (interpretado como conversión de gramos a kilogramos).; La temperatura se sufija con el símbolo ''°''.; TSH se etiqueta en unidades mUI/L.; En el modo de últimas 24 horas, cada bloque de signos vitales se antecede con la fecha/hora del registro en formato ''yyyy-mm-dd hh:mi:ss''.; La ventana temporal del modo últimas 24 horas se calcula respecto a common.GETDATE() (no GETDATE() del servidor).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'SignosVitalesPorFolioConcatenados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Signos vitales; Tensión arterial; Temperatura; Frecuencia cardíaca; Frecuencia respiratoria; Saturación de oxígeno; Talla; Peso; Perímetro cefálico/torácico/abdominal/braquial; Peso seco; Tasa de filtración glomerular; Estadio renal; Cuña, PIA, PVC, PIC (UCI adultos); Glucometría; Ultrafiltración y tiempo de sesión (diálisis); KTV; Diuresis, meconio, succión, deglución (neonatal); Tensión arterial media; Sensibilización y Coombs; TSH; Dolor; Fetocardia; Grupo sanguíneo y RH; Peso para la edad gestacional; Saturación pre/postductal; Presión arterial en miembros superiores e inferiores; Interpretaciones antropométricas (peso/talla, IMC, peso/edad, perímetro cefálico, talla/edad, IMC/edad); Paciente, ingreso y folio (historia clínica)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'SignosVitalesPorFolioConcatenados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Cuando @SignosUltimoDia = 0 → retorna la concatenación de signos vitales del registro identificado por paciente, ingreso y folio.; [RETURN_RESULT] : Cuando @SignosUltimoDia = 1 → retorna la agregación (STRING_AGG) de signos vitales registrados en las últimas 24 horas, prefijando cada bloque con su fecha/hora de registro.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'SignosVitalesPorFolioConcatenados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @SignosUltimoDia = 0 → Selecciona un único registro filtrado por IPCODPACI, NUMINGRES y NUMEFOLIO y arma la cadena sin prefijo de fecha. else Agrega con STRING_AGG todos los registros del paciente e ingreso cuya FECREGITE esté entre (ahora - 24h) y ahora, prefijando cada bloque con la fecha/hora del registro.; si ULTRAFILTRA / DIURESIS / MECONIO / SUCCION / DEGLUCION = 1 ó 0 → Se traduce a ''Si'' o ''No'' respectivamente.; si RHSANGUINEO = 1 ó 0 → Se traduce a ''+'' o ''-'' respectivamente.; si PESOPARAEDADGESTACIONAL ∈ {1,2,3} → Se traduce a ''Pequeño/Adecuado/Grande para la edad gestacional''.; si Cada métrica numérica IS NULL o = 0 (o longitud cero en textos) → Se omite del resultado (no se concatena su etiqueta).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'SignosVitalesPorFolioConcatenados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'SignosVitalesPorFolioConcatenados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCEXFISIC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'SignosVitalesPorFolioConcatenados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'SignosVitalesPorFolioConcatenados';
GO
