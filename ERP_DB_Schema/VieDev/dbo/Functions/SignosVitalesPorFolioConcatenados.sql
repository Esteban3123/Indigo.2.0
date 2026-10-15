
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