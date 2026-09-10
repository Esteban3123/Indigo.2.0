

-- =============================================
-- Author:		Yohana Rozo
-- Create date: <2021-06-09>
-- Description:	<Funcion que retorna el valor correspondiente a Hemoglobina por mimima fecha>
-- ==========================================
CREATE FUNCTION [Report].[FN_Laboratorios_Rias_Hemoglobina_Fecha]
(
	-- Add the parameters for the function here
	@Fecha_Historia datetime,
	@ipcodpaci varchar(25)
)
 returns datetime
 as
 begin
   declare @variable datetime=(select a.FECSERIPS from (
    SELECT TOP 1 DET.VALOR, min(G.FECREGIST) FECSERIPS, D.IPCODPACI FROM dbo.INTERCABE AS A 
                 INNER JOIN dbo.INTERDETA AS B ON A.AUTO=B.CODCONCEC AND B.ORDTIP ='AMB'  
                 INNER JOIN dbo.INTERLABC AS C ON B.CODCONCEC=C.ORDEN_INDIGO 
                 INNER JOIN dbo.AMBORDLAB AS D ON B.AUTOLABOR= D.AUTO 
                 INNER JOIN dbo.INTERLABD AS DET ON D.AUTO = DET.AUTOLABOR and c.AUTO = det.CODCONCEC  
                 INNER JOIN dbo.INUNIFUNC AS E ON D.UFUCODIGO=E.UFUCODIGO 
                 INNER JOIN dbo.INCUPSIPS AS F ON B.CODSERIPS=F.CODSERIPS 
                 INNER JOIN dbo.INTERCTRL AS G ON D.AUTO=G.AUTOLABOR AND G.ORDEN_INDIGO = C.ORDEN_INDIGO 
                 LEFT OUTER JOIN dbo.INPROFSAL AS H ON G.CODPROSAL=H.CODPROSAL 
                 LEFT OUTER JOIN dbo.INESPECIA I ON H.CODESPEC1 = I.CODESPECI 
             WHERE F.DESSERIPS LIKE '%HEMOGLOBINA%' AND F.DESSERIPS NOT LIKE '%HEMOGRAMA%' AND ANALITO LIKE 'HEMOGLOBINA'
			 AND D.ESTSERIPS<>6 AND DET.NUMMUESTRA ='1'
			 AND D.IPCODPACI = @ipcodpaci
			 AND G.FECREGIST>= @Fecha_Historia
			 group by DET.VALOR, D.IPCODPACI) a
 )
	RETURN @variable

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Función escalar que, dado un paciente y una fecha de historia clínica, retorna la fecha mínima de registro del resultado de hemoglobina (excluyendo hemogramas) en órdenes ambulatorias con estado distinto de 6 y número de muestra igual a 1. Recorre las tablas de interconsultas, órdenes de laboratorio, resultados por analito y control de procesamiento, filtrando por el servicio CUPS cuya descripción coincida con "HEMOGLOBINA". Devuelve un único valor `datetime` correspondiente al registro más temprano desde la fecha indicada.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Hemoglobina_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Hemoglobina_Fecha';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene la fecha más temprana de registro de un resultado de laboratorio de Hemoglobina (excluyendo Hemograma) para un paciente, a partir de una fecha de historia clínica dada, en el contexto de reportes RIAS.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Hemoglobina_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos una orden de laboratorio ambulatorio (ORDTIP=''AMB'') para el paciente con resultado de analito ''HEMOGLOBINA'' posterior o igual a la fecha de historia.; El servicio CUPS asociado debe describir ''HEMOGLOBINA'' sin ser un ''HEMOGRAMA''.; La orden no debe estar en estado 6 (ESTSERIPS<>6) y la muestra debe ser la número 1.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Hemoglobina_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera órdenes de laboratorio con tipo de orden ambulatorio (ORDTIP=''AMB'').; Excluye órdenes en estado 6 (D.ESTSERIPS<>6), interpretado como anuladas/canceladas.; Solo toma la primera muestra del examen (DET.NUMMUESTRA=''1'').; Filtra exámenes cuyo servicio CUPS contiene ''HEMOGLOBINA'' pero excluye explícitamente los que contienen ''HEMOGRAMA'', y exige que el analito sea exactamente ''HEMOGLOBINA''.; Solo se consideran registros cuya fecha de registro de control (G.FECREGIST) sea mayor o igual a la fecha de historia recibida como parámetro.; Devuelve la mínima fecha de registro (MIN(G.FECREGIST)) entre los resultados que cumplen los filtros.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Hemoglobina_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Hemoglobina; Laboratorio ambulatorio; Orden de laboratorio; Analito; RIAS (Rutas Integrales de Atención en Salud); Paciente; Muestra de laboratorio', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Hemoglobina_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] datetime: Retorna la mínima FECREGIST de INTERCTRL para órdenes ambulatorias de Hemoglobina del paciente cuyo FECREGIST >= @Fecha_Historia, ESTSERIPS<>6, NUMMUESTRA=''1'' y descripción CUPS contiene ''HEMOGLOBINA'' y no ''HEMOGRAMA''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Hemoglobina_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INTERCABE; dbo.INTERDETA; dbo.INTERLABC; dbo.AMBORDLAB; dbo.INTERLABD; dbo.INUNIFUNC; dbo.INCUPSIPS; dbo.INTERCTRL; dbo.INPROFSAL; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Hemoglobina_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Hemoglobina_Fecha';
GO
