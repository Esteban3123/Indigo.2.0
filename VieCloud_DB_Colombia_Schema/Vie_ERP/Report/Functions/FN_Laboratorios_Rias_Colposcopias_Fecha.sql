

-- =============================================
-- Author:		Yohana Rozo
-- Create date: <2021-06-10>
-- Description:	<Funcion que retorna el valor correspondiente a AntigenoHepatitisB por mimima fecha>
-- ==========================================

CREATE FUNCTION [Report].[FN_Laboratorios_Rias_Colposcopias_Fecha]
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
             WHERE F.CODSERIPS LIKE '%702203%'  
			 AND D.ESTSERIPS<>6 AND DET.NUMMUESTRA ='1'
			 AND D.IPCODPACI = @ipcodpaci
			 AND G.FECREGIST>= @Fecha_Historia
			 group by DET.VALOR, D.IPCODPACI) a
 )
	RETURN @variable

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Función escalar que, dado un paciente y una fecha de historia, retorna la fecha de registro más antigua (`FECREGIST`) de un resultado de laboratorio correspondiente al código de servicio CUPS que contiene `702203` (colposcopia), filtrando órdenes ambulatorias activas con número de muestra `1` y fecha posterior o igual a la indicada. Consulta la cadena de órdenes de laboratorio ambulatorio, sus detalles de resultados y el control de procesamiento en Indigo.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Colposcopias_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Colposcopias_Fecha';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene la fecha mínima de registro de control de una orden de laboratorio ambulatorio asociada al CUPS 702203 (colposcopia) para un paciente, a partir de una fecha de historia dada, dentro del flujo RIAS.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Colposcopias_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una interconsulta (INTERCABE) con detalle (INTERDETA) cuyo ORDTIP sea ''AMB''; La orden de laboratorio (AMBORDLAB) debe tener ESTSERIPS distinto de 6 (no anulada/excluida); El detalle de resultado (INTERLABD) debe tener NUMMUESTRA = ''1''; El CUPS asociado (INCUPSIPS.CODSERIPS) debe contener ''702203''; La fecha de registro del control (INTERCTRL.FECREGIST) debe ser mayor o igual a la fecha de historia recibida', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Colposcopias_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera órdenes ambulatorias (ORDTIP=''AMB''); Excluye órdenes con estado 6 en AMBORDLAB; Solo evalúa la primera muestra (NUMMUESTRA=''1''); El procedimiento se identifica por coincidencia parcial del código CUPS ''702203'' (colposcopia); Se restringe a controles registrados en o después de la fecha de historia parámetro', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Colposcopias_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Orden de laboratorio ambulatorio; Interconsulta; CUPS (procedimiento 702203 - colposcopia); RIAS (Rutas Integrales de Atención en Salud); Muestra de laboratorio; Profesional de la salud; Especialidad médica', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Colposcopias_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] scalar_datetime: Retorna la mínima FECREGIST de INTERCTRL (FECSERIPS) para el paciente y procedimiento 702203 cumpliendo los filtros de orden ambulatoria, primera muestra y estado distinto de 6; null si no hay coincidencias.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Colposcopias_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si INTERDETA.ORDTIP = ''AMB'' AND AMBORDLAB.ESTSERIPS <> 6 AND INTERLABD.NUMMUESTRA = ''1'' AND INCUPSIPS.CODSERIPS LIKE ''%702203%'' AND INTERCTRL.FECREGIST >= @Fecha_Historia → Se considera la fila como candidata y se toma la mínima FECREGIST por VALOR/paciente (TOP 1) else Se retorna NULL', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Colposcopias_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INTERCABE; dbo.INTERDETA; dbo.INTERLABC; dbo.AMBORDLAB; dbo.INTERLABD; dbo.INUNIFUNC; dbo.INCUPSIPS; dbo.INTERCTRL; dbo.INPROFSAL; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Colposcopias_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Colposcopias_Fecha';
GO
