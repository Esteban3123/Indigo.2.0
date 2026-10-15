
CREATE FUNCTION [dbo].[ExamenFisicoPorFolioConcatenados](@CodigoPaciente as varchar(25), @NumIngreso as char(10), @NumFolio as varchar(10), @SignosUltimoDia As bit)
RETURNS varchar(max)
AS
BEGIN
    DECLARE @ExamenFisico as varchar(max)

IF @SignosUltimoDia = 0
BEGIN
	SELECT 
	@ExamenFisico = CONCAT(
		IIF(PRAEJEPAC IS NULL,'',Concat('Practica ejercicio: ', RTRIM(PRAEJEPAC), ', ')),   
		IIF(HABDIEPAC IS NULL,'',Concat('Hábito de dieta: ', HABDIEPAC, ', ')),   
		IIF(FISOBSPAC IS NULL OR LEN(RTRIM(FISOBSPAC)) = 0,'',Concat('Observaciones: ', RTRIM(FISOBSPAC), ', ')),  
		IIF(ANOCABPAC IS NULL OR ANOCABPAC = 0,'',Concat('Anomalía cabeza: ', CASE ANOCABPAC WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),   
		IIF(DESCABPAC IS NULL OR LEN(RTRIM(DESCABPAC)) = 0,'',Concat('Descripción anomalía cabeza: ', RTRIM(DESCABPAC), ', ')),   
		IIF(ANOOJOPAC IS NULL OR ANOOJOPAC = 0,'',Concat('Anomalía ojos: ', CASE ANOOJOPAC WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),  
		IIF(DESOJOPAC IS NULL OR LEN(RTRIM(DESOJOPAC)) = 0,'',Concat('Descripción anomalía ojos: ', RTRIM(DESOJOPAC), ', ')),   
		IIF(ANOORLPAC IS NULL OR ANOORLPAC = 0,'',Concat('Anomalia ORL (Otorrinolaringológico): ', CASE ANOORLPAC WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),   
		IIF(DESORLPAC IS NULL OR LEN(RTRIM(DESORLPAC)) = 0,'',Concat('Descripción anomalía ORL (Otorrinolaringológico): ', RTRIM(DESORLPAC), ', ')),  
		IIF(ANOCUEPAC IS NULL OR ANOCUEPAC = 0,'',Concat('Anomalía cuello: ', CASE ANOCUEPAC WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),   
		IIF(DESCUEPAC IS NULL OR LEN(RTRIM(DESCUEPAC)) = 0,'',Concat('Descripción anomalía cuello: ', RTRIM(DESCUEPAC), ', ')),   
		IIF(ANOCAPPAC IS NULL OR ANOCAPPAC = 0,'',Concat('Anomalía cardiopulmonar: ', CASE ANOCAPPAC WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),  
		IIF(DESCAPPAC IS NULL OR LEN(RTRIM(DESCAPPAC)) = 0,'',Concat('Descripción anomalía cardiopulmonar: ', RTRIM(DESCAPPAC), ', ')),   
		IIF(ANOABDPAC IS NULL OR ANOABDPAC = 0,'',Concat('Anomalía abdomen: ', CASE ANOABDPAC WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),   
		IIF(DESABDPAC IS NULL OR LEN(RTRIM(DESABDPAC)) = 0,'',Concat('Descripción anomalía abdomen: ', RTRIM(DESABDPAC), ', ')),  
		IIF(ANOGEUOAC IS NULL OR ANOGEUOAC = 0,'',Concat('Anomalía genitourinario: ', CASE ANOGEUOAC WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),   
		IIF(DESGEUOAC IS NULL OR LEN(RTRIM(DESGEUOAC)) = 0,'',Concat('Descripción anomalía genitourinario: ', RTRIM(DESGEUOAC), ', ')),   
		IIF(ANOEXTPAC IS NULL OR ANOEXTPAC = 0,'',Concat('Anomalía extremidades: ', CASE ANOEXTPAC WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),  
		IIF(DESEXTPAC IS NULL OR LEN(RTRIM(DESEXTPAC)) = 0,'',Concat('Descripción anomalía extremidades: ', RTRIM(DESEXTPAC), ', ')),   
		IIF(ANONEUPAC IS NULL OR ANONEUPAC = 0,'',Concat('Anomalía neurológica: ', CASE ANONEUPAC WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),   
		IIF(DESNEUPAC IS NULL OR LEN(RTRIM(DESNEUPAC)) = 0,'',Concat('Descripción anomalía neurológica: ', RTRIM(DESNEUPAC), ', ')),  
		IIF(ANOPIELPA IS NULL OR ANOPIELPA = 0,'',Concat('Anomalía piel: ', CASE ANOPIELPA WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),   
		IIF(DESPEILPA IS NULL OR LEN(RTRIM(DESPEILPA)) = 0,'',Concat('Descripción anomalía piel: ', RTRIM(DESPEILPA), ', ')),   
		IIF(SOPVENPAC IS NULL OR SOPVENPAC = 0,'',Concat('Soporte ventilatorio: ', CASE SOPVENPAC WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),  
		IIF(SOPVENDES IS NULL OR LEN(RTRIM(SOPVENDES)) = 0,'',Concat('Descripción soporte ventilatorio: ', RTRIM(SOPVENDES), ', ')),   
		IIF(SOPINOPAC IS NULL OR SOPINOPAC = 0,'',Concat('Soporte inotrópico: ', CASE SOPINOPAC WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),   
		IIF(SOPINODES IS NULL OR LEN(RTRIM(SOPINODES)) = 0,'',Concat('Descripción soporte inotrópico: ', RTRIM(SOPINODES), ', ')),  
		IIF(ACCESOPAC IS NULL OR ACCESOPAC = 0,'',Concat('Acceso: ', CASE ACCESOPAC WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),   
		IIF(ACCESODES IS NULL OR LEN(RTRIM(ACCESODES)) = 0,'',Concat('Descripción acceso: ', RTRIM(ACCESODES), ', ')),   
		IIF(ACCVASCULAR IS NULL OR ACCVASCULAR = 0,'',Concat('Acceso vascular: ', ACCVASCULAR, ', ')),   
		IIF(SIDROMETABO IS NULL OR SIDROMETABO = 0,'',Concat('Síndrome metabólico: ', CASE SIDROMETABO WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),   
		IIF(ANOOSTEO IS NULL OR ANOOSTEO = 0,'',Concat('Anomalía osteoporosis: ', CASE ANOOSTEO WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),   
		IIF(DESOSTEO IS NULL OR LEN(RTRIM(DESOSTEO)) = 0,'',Concat('Descripción anomalía osteoporosis: ', RTRIM(DESOSTEO), ', ')),   
		IIF(ANOVASCU IS NULL OR ANOVASCU = 0,'',Concat('Anomalía vascular: ', CASE ANOVASCU WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),  
		IIF(DESVASCU IS NULL OR LEN(RTRIM(DESVASCU)) = 0,'',Concat('Descripción anomalía vascular: ', RTRIM(DESVASCU), ', ')),   
		IIF(AR IS NULL OR AR = 0,'',Concat('Altura rodilla: ', AR, ', ')),   
		IIF(CONTINUALACTANCIA IS NULL,'',Concat('¿Continúa lactancia materna?: ', CASE CONTINUALACTANCIA WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),   
		IIF(PORQUENOCONTINUALACTANCIA IS NULL OR LEN(RTRIM(PORQUENOCONTINUALACTANCIA)) = 0,'',Concat('¿Por qué no continúa la lactancia materna?: ', RTRIM(PORQUENOCONTINUALACTANCIA), ', ')),      
		IIF(SIFILISCONGENITA IS NULL OR SIFILISCONGENITA = 0,'',Concat('Sífilis congénita: ', CASE SIFILISCONGENITA WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),   
		IIF(INTERPRETACIONTAMIZAJECARDIOPATIA IS NULL,'',Concat('Interpretación del tamizaje para cardiopatía congénita: ', RTRIM(INTERPRETACIONTAMIZAJECARDIOPATIA), ', ')),   
		IIF(INTERALTURAUTERINA IS NULL OR LEN(RTRIM(INTERALTURAUTERINA)) = 0,'',Concat('Interpretación de gráfica altura uterina: ', INTERALTURAUTERINA)),   
		IIF(OBSERVACIONESCARDIOPATIA IS NULL OR LEN(RTRIM(OBSERVACIONESCARDIOPATIA)) = 0,'',Concat('Observaciones cardiopatía: ', RTRIM(OBSERVACIONESCARDIOPATIA), ', ')),   
		IIF(INSPECCIONOCULARBILATERAL IS NULL,'',Concat('Inspección ocular bilateral: ', INSPECCIONOCULARBILATERAL, ', ')),  
		IIF(ROJORETINIANODERECHO IS NULL,'',Concat('Rojo retiniano ojo derecho: ', RTRIM(ROJORETINIANODERECHO), ', ')),   
		IIF(ROJORETINIANOIZQUIERDO IS NULL,'',Concat('Rojo retiniano ojo izquierdo: ', ROJORETINIANOIZQUIERDO, ', ')),   
		IIF(INTERPRETACIONTAMIZAJEOCULAR IS NULL,'',Concat('Interpretación del tamizaje ocular: ', INTERPRETACIONTAMIZAJEOCULAR, ', ')),  
		IIF(OBSERVACIONESOCULAR IS NULL OR LEN(RTRIM(OBSERVACIONESOCULAR)) = 0,'',Concat('Observaciones tamizaje ocular: ', RTRIM(OBSERVACIONESOCULAR), ', ')),   
		IIF(INTERPRETACIONTAMIZAJEAUDITIVO IS NULL,'',Concat('Interpretación del tamizaje auditivo: ', INTERPRETACIONTAMIZAJEAUDITIVO, ', ', CHAR(13))))
	FROM dbo.HCEXFISIC 
	WHERE IPCODPACI=@CodigoPaciente AND NUMINGRES= @NumIngreso AND NUMEFOLIO=@NumFolio
END
ELSE
BEGIN
	SELECT 
	@ExamenFisico = CONCAT(
		IIF(PRAEJEPAC IS NULL,'',Concat('Practica ejercicio: ', RTRIM(PRAEJEPAC), ', ')),   
		IIF(HABDIEPAC IS NULL,'',Concat('Hábito de dieta: ', HABDIEPAC, ', ')),   
		IIF(FISOBSPAC IS NULL OR LEN(RTRIM(FISOBSPAC)) = 0,'',Concat('Observaciones: ', RTRIM(FISOBSPAC), ', ')),  
		IIF(ANOCABPAC IS NULL OR ANOCABPAC = 0,'',Concat('Anomalía cabeza: ', CASE ANOCABPAC WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),   
		IIF(DESCABPAC IS NULL OR LEN(RTRIM(DESCABPAC)) = 0,'',Concat('Descripción anomalía cabeza: ', RTRIM(DESCABPAC), ', ')),   
		IIF(ANOOJOPAC IS NULL OR ANOOJOPAC = 0,'',Concat('Anomalía ojos: ', CASE ANOOJOPAC WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),  
		IIF(DESOJOPAC IS NULL OR LEN(RTRIM(DESOJOPAC)) = 0,'',Concat('Descripción anomalía ojos: ', RTRIM(DESOJOPAC), ', ')),   
		IIF(ANOORLPAC IS NULL OR ANOORLPAC = 0,'',Concat('Anomalia ORL (Otorrinolaringológico): ', CASE ANOORLPAC WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),   
		IIF(DESORLPAC IS NULL OR LEN(RTRIM(DESORLPAC)) = 0,'',Concat('Descripción anomalía ORL (Otorrinolaringológico): ', RTRIM(DESORLPAC), ', ')),  
		IIF(ANOCUEPAC IS NULL OR ANOCUEPAC = 0,'',Concat('Anomalía cuello: ', CASE ANOCUEPAC WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),   
		IIF(DESCUEPAC IS NULL OR LEN(RTRIM(DESCUEPAC)) = 0,'',Concat('Descripción anomalía cuello: ', RTRIM(DESCUEPAC), ', ')),   
		IIF(ANOCAPPAC IS NULL OR ANOCAPPAC = 0,'',Concat('Anomalía cardiopulmonar: ', CASE ANOCAPPAC WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),  
		IIF(DESCAPPAC IS NULL OR LEN(RTRIM(DESCAPPAC)) = 0,'',Concat('Descripción anomalía cardiopulmonar: ', RTRIM(DESCAPPAC), ', ')),   
		IIF(ANOABDPAC IS NULL OR ANOABDPAC = 0,'',Concat('Anomalía abdomen: ', CASE ANOABDPAC WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),   
		IIF(DESABDPAC IS NULL OR LEN(RTRIM(DESABDPAC)) = 0,'',Concat('Descripción anomalía abdomen: ', RTRIM(DESABDPAC), ', ')),  
		IIF(ANOGEUOAC IS NULL OR ANOGEUOAC = 0,'',Concat('Anomalía genitourinario: ', CASE ANOGEUOAC WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),   
		IIF(DESGEUOAC IS NULL OR LEN(RTRIM(DESGEUOAC)) = 0,'',Concat('Descripción anomalía genitourinario: ', RTRIM(DESGEUOAC), ', ')),   
		IIF(ANOEXTPAC IS NULL OR ANOEXTPAC = 0,'',Concat('Anomalía extremidades: ', CASE ANOEXTPAC WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),  
		IIF(DESEXTPAC IS NULL OR LEN(RTRIM(DESEXTPAC)) = 0,'',Concat('Descripción anomalía extremidades: ', RTRIM(DESEXTPAC), ', ')),   
		IIF(ANONEUPAC IS NULL OR ANONEUPAC = 0,'',Concat('Anomalía neurológica: ', CASE ANONEUPAC WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),   
		IIF(DESNEUPAC IS NULL OR LEN(RTRIM(DESNEUPAC)) = 0,'',Concat('Descripción anomalía neurológica: ', RTRIM(DESNEUPAC), ', ')),  
		IIF(ANOPIELPA IS NULL OR ANOPIELPA = 0,'',Concat('Anomalía piel: ', CASE ANOPIELPA WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),   
		IIF(DESPEILPA IS NULL OR LEN(RTRIM(DESPEILPA)) = 0,'',Concat('Descripción anomalía piel: ', RTRIM(DESPEILPA), ', ')),   
		IIF(SOPVENPAC IS NULL OR SOPVENPAC = 0,'',Concat('Soporte ventilatorio: ', CASE SOPVENPAC WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),  
		IIF(SOPVENDES IS NULL OR LEN(RTRIM(SOPVENDES)) = 0,'',Concat('Descripción soporte ventilatorio: ', RTRIM(SOPVENDES), ', ')),   
		IIF(SOPINOPAC IS NULL OR SOPINOPAC = 0,'',Concat('Soporte inotrópico: ', CASE SOPINOPAC WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),   
		IIF(SOPINODES IS NULL OR LEN(RTRIM(SOPINODES)) = 0,'',Concat('Descripción soporte inotrópico: ', RTRIM(SOPINODES), ', ')),  
		IIF(ACCESOPAC IS NULL OR ACCESOPAC = 0,'',Concat('Acceso: ', CASE ACCESOPAC WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),   
		IIF(ACCESODES IS NULL OR LEN(RTRIM(ACCESODES)) = 0,'',Concat('Descripción acceso: ', RTRIM(ACCESODES), ', ')),   
		IIF(ACCVASCULAR IS NULL OR ACCVASCULAR = 0,'',Concat('Acceso vascular: ', ACCVASCULAR, ', ')),   
		IIF(SIDROMETABO IS NULL OR SIDROMETABO = 0,'',Concat('Síndrome metabólico: ', CASE SIDROMETABO WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),   
		IIF(ANOOSTEO IS NULL OR ANOOSTEO = 0,'',Concat('Anomalía osteoporosis: ', CASE ANOOSTEO WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),   
		IIF(DESOSTEO IS NULL OR LEN(RTRIM(DESOSTEO)) = 0,'',Concat('Descripción anomalía osteoporosis: ', RTRIM(DESOSTEO), ', ')),   
		IIF(ANOVASCU IS NULL OR ANOVASCU = 0,'',Concat('Anomalía vascular: ', CASE ANOVASCU WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),  
		IIF(DESVASCU IS NULL OR LEN(RTRIM(DESVASCU)) = 0,'',Concat('Descripción anomalía vascular: ', RTRIM(DESVASCU), ', ')),   
		IIF(AR IS NULL OR AR = 0,'',Concat('Altura rodilla: ', AR, ', ')),   
		IIF(CONTINUALACTANCIA IS NULL,'',Concat('¿Continúa lactancia materna?: ', CASE CONTINUALACTANCIA WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),   
		IIF(PORQUENOCONTINUALACTANCIA IS NULL OR LEN(RTRIM(PORQUENOCONTINUALACTANCIA)) = 0,'',Concat('¿Por qué no continúa la lactancia materna?: ', RTRIM(PORQUENOCONTINUALACTANCIA), ', ')),      
		IIF(SIFILISCONGENITA IS NULL OR SIFILISCONGENITA = 0,'',Concat('Sífilis congénita: ', CASE SIFILISCONGENITA WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END, ', ')),   
		IIF(INTERPRETACIONTAMIZAJECARDIOPATIA IS NULL,'',Concat('Interpretación del tamizaje para cardiopatía congénita: ', RTRIM(INTERPRETACIONTAMIZAJECARDIOPATIA), ', ')),   
		IIF(INTERALTURAUTERINA IS NULL OR LEN(RTRIM(INTERALTURAUTERINA)) = 0,'',Concat('Interpretación de gráfica altura uterina: ', INTERALTURAUTERINA)),   
		IIF(OBSERVACIONESCARDIOPATIA IS NULL OR LEN(RTRIM(OBSERVACIONESCARDIOPATIA)) = 0,'',Concat('Observaciones cardiopatía: ', RTRIM(OBSERVACIONESCARDIOPATIA), ', ')),   
		IIF(INSPECCIONOCULARBILATERAL IS NULL,'',Concat('Inspección ocular bilateral: ', INSPECCIONOCULARBILATERAL, ', ')),  
		IIF(ROJORETINIANODERECHO IS NULL,'',Concat('Rojo retiniano ojo derecho: ', RTRIM(ROJORETINIANODERECHO), ', ')),   
		IIF(ROJORETINIANOIZQUIERDO IS NULL,'',Concat('Rojo retiniano ojo izquierdo: ', ROJORETINIANOIZQUIERDO, ', ')),   
		IIF(INTERPRETACIONTAMIZAJEOCULAR IS NULL,'',Concat('Interpretación del tamizaje ocular: ', INTERPRETACIONTAMIZAJEOCULAR, ', ')),  
		IIF(OBSERVACIONESOCULAR IS NULL OR LEN(RTRIM(OBSERVACIONESOCULAR)) = 0,'',Concat('Observaciones tamizaje ocular: ', RTRIM(OBSERVACIONESOCULAR), ', ')),   
		IIF(INTERPRETACIONTAMIZAJEAUDITIVO IS NULL,'',Concat('Interpretación del tamizaje auditivo: ', INTERPRETACIONTAMIZAJEAUDITIVO, ', ', CHAR(13))))
	FROM dbo.HCEXFISIC 
	WHERE IPCODPACI=@CodigoPaciente AND NUMINGRES= @NumIngreso AND FECREGITE BETWEEN DATEADD(hour, -24, common.GETDATE()) AND Common.GETDATE()
END
    RETURN @ExamenFisico
END