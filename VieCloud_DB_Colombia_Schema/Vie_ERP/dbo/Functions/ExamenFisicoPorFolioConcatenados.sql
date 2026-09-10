
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que genera un texto resumen concatenado del examen físico registrado en la historia clínica de un paciente (tabla HCEXFISIC), dado su código de paciente, número de ingreso y número de folio. Recopila y formatea en lenguaje legible todos los hallazgos del examen físico por sistemas: cabeza, ojos, oídos/otorrinolaringología, cuello, cardiopulmonar, abdomen, genitourinario, extremidades, neurológico, piel, soporte ventilatorio, soporte inotrópico, accesos vasculares, síndrome metabólico, osteoporosis, anomalía vascular, lactancia materna, sífilis congénita, cardiopatía congénita y altura uterina, entre otros. Omite automáticamente los campos vacíos o sin anomalías, produciendo solo la información clínicamente relevante. Es utilizada para mostrar el resumen del examen físico en la evolución clínica del paciente, informes de historia clínica y pantallas de consulta médica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'ExamenFisicoPorFolioConcatenados';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'ExamenFisicoPorFolioConcatenados';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera una cadena legible que concatena los hallazgos no nulos del examen físico de un paciente, ya sea por folio específico o restringido a las últimas 24 horas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ExamenFisicoPorFolioConcatenados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir registro en dbo.HCEXFISIC que coincida con el paciente y número de ingreso indicados.; Cuando se consulta por folio, debe proporcionarse el folio correspondiente; cuando se consulta por últimas 24h, la fecha de registro debe estar dentro de ese rango respecto a common.GETDATE().', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ExamenFisicoPorFolioConcatenados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los valores booleanos se presentan como ''Si''/''No'' únicamente.; Los campos con valor NULL nunca aparecen en la salida.; Las descripciones en blanco (longitud 0 tras RTRIM) se excluyen.; Las banderas con valor 0 se excluyen del texto, salvo CONTINUALACTANCIA que sí muestra ''No'' cuando es 0.; Cada fragmento incluido se separa con '', ''; al final del último bloque se añade un retorno de carro (CHAR(13)).; La consulta retorna como máximo el dato de un solo registro (asignación escalar a variable).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ExamenFisicoPorFolioConcatenados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Examen físico; Paciente; Ingreso hospitalario; Folio clínico; Anomalías por sistema (cabeza, ojos, ORL, cuello, cardiopulmonar, abdomen, genitourinario, extremidades, neurológico, piel, vascular, osteoporosis); Soporte ventilatorio; Soporte inotrópico; Acceso vascular; Síndrome metabólico; Lactancia materna; Sífilis congénita; Tamizaje de cardiopatía congénita; Tamizaje ocular (rojo retiniano, inspección bilateral); Tamizaje auditivo; Altura uterina; Altura de rodilla; Hábito de dieta; Práctica de ejercicio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ExamenFisicoPorFolioConcatenados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve varchar(max) con los campos del examen físico concatenados en formato ''Etiqueta: valor, ''; campos NULL, vacíos o iguales a 0 en banderas se omiten.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ExamenFisicoPorFolioConcatenados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @SignosUltimoDia = 0 → Filtra HCEXFISIC por paciente, ingreso y NUMEFOLIO = @NumFolio (examen físico de un folio específico). else Filtra HCEXFISIC por paciente e ingreso, restringiendo a registros con FECREGITE en las últimas 24 horas respecto a common.GETDATE().; si Para cada bandera (ANOCABPAC, ANOOJOPAC, ANOORLPAC, ANOCUEPAC, ANOCAPPAC, ANOABDPAC, ANOGEUOAC, ANOEXTPAC, ANONEUPAC, ANOPIELPA, SOPVENPAC, SOPINOPAC, ACCESOPAC, SIDROMETABO, ANOOSTEO, ANOVASCU, SIFILISCONGENITA): valor = 1 → Se traduce a ''Si''. else Si valor = 0 se traduce a ''No'', y si es NULL o 0 el campo se omite por completo.; si Campo de descripción (DESCABPAC, DESOJOPAC, etc.) es NULL o de longitud 0 tras RTRIM → Se omite del texto resultante. else Se incluye con su etiqueta y el valor recortado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ExamenFisicoPorFolioConcatenados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ExamenFisicoPorFolioConcatenados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCEXFISIC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ExamenFisicoPorFolioConcatenados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ExamenFisicoPorFolioConcatenados';
GO
