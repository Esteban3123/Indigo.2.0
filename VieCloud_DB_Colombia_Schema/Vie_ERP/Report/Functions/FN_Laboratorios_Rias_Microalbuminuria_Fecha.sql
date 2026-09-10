

-- =============================================
-- Author:		Yohana Rozo
-- Create date: <2021-06-10>
-- Description:	<Funcion que retorna el valor correspondiente a AntigenoHepatitisB por mimima fecha>
-- ==========================================

CREATE FUNCTION [Report].[FN_Laboratorios_Rias_Microalbuminuria_Fecha]
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
             WHERE F.DESCODCUPS LIKE '%MICROALBUMINURIA%'  
			 AND D.ESTSERIPS<>6 AND DET.NUMMUESTRA ='1'
			 AND D.IPCODPACI = @ipcodpaci
			 AND G.FECREGIST>= @Fecha_Historia
			 group by DET.VALOR, D.IPCODPACI) a
 )
	RETURN @variable

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Función escalar que, dado un código de paciente y una fecha de historia, retorna la fecha mínima de registro (`FECREGIST`) del resultado de microalbuminuria encontrado en órdenes de laboratorio ambulatorio cuyo estado no es 6 y corresponde a la muestra número 1. Filtra el catálogo CUPS por la descripción `MICROALBUMINURIA` y considera únicamente registros a partir de la fecha proporcionada.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Microalbuminuria_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Microalbuminuria_Fecha';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene la fecha más temprana (a partir de una fecha base) en que se registró un examen ambulatorio de microalbuminuria para un paciente, usado en reportes RIAS.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Microalbuminuria_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un paciente identificado por @ipcodpaci con órdenes ambulatorias de laboratorio asociadas.; Las tablas de interconsulta/laboratorio (INTERCABE, INTERDETA, INTERLABC, AMBORDLAB, INTERLABD, INTERCTRL) deben estar correctamente vinculadas vía AUTO/CODCONCEC/AUTOLABOR/ORDEN_INDIGO.; El catálogo INCUPSIPS debe contener un código CUPS cuya descripción incluya ''MICROALBUMINURIA''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Microalbuminuria_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera órdenes de laboratorio cuyo CUPS contenga la cadena ''MICROALBUMINURIA'' (F.DESCODCUPS LIKE ''%MICROALBUMINURIA%'').; Excluye órdenes con estado 6 en AMBORDLAB (D.ESTSERIPS<>6), descartando órdenes anuladas/no válidas.; Solo considera el detalle correspondiente a la primera muestra (DET.NUMMUESTRA = ''1'').; Solo se toman registros de control cuya fecha de registro sea igual o posterior a la fecha de historia recibida (G.FECREGIST >= @Fecha_Historia).; Las órdenes deben ser de tipo ambulatorio (B.ORDTIP = ''AMB'').; Devuelve la fecha mínima de registro (MIN(G.FECREGIST)) del control de laboratorio asociado al examen de microalbuminuria del paciente.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Microalbuminuria_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Microalbuminuria; Laboratorio ambulatorio; Orden de laboratorio; RIAS (Rutas Integrales de Atención en Salud); Paciente; CUPS; Muestra de laboratorio', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Microalbuminuria_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] datetime: Retorna la fecha mínima de registro (MIN(G.FECREGIST)) del control de laboratorio de microalbuminuria del paciente cuando F.DESCODCUPS LIKE ''%MICROALBUMINURIA%'', D.ESTSERIPS<>6, DET.NUMMUESTRA=''1'' y G.FECREGIST>=@Fecha_Historia; NULL si no hay coincidencias.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Microalbuminuria_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INTERCABE; dbo.INTERDETA; dbo.INTERLABC; dbo.AMBORDLAB; dbo.INTERLABD; dbo.INUNIFUNC; dbo.INCUPSIPS; dbo.INTERCTRL; dbo.INPROFSAL; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Microalbuminuria_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Microalbuminuria_Fecha';
GO
