

-- =============================================
-- Author:		Yohana Rozo
-- Create date: <2021-06-10>
-- Description:	<Funcion que retorna el valor correspondiente a AntigenoHepatitisB por mimima fecha>
-- ==========================================

CREATE FUNCTION [Report].[FN_Laboratorios_Rias_Bxcervicouterino_Fecha]
(
	-- Add the parameters for the function here
	@Fecha_Historia datetime,
	@ipcodpaci varchar(25)
)
 returns datetime
 as
 begin
   declare @variable datetime=(select a.FECSERIPS from (
    SELECT  DET.VALOR, min(G.FECREGIST) FECSERIPS, D.IPCODPACI FROM dbo.INTERCABE AS A 
                 INNER JOIN dbo.INTERDETA AS B ON A.AUTO=B.CODCONCEC AND B.ORDTIP ='AMB'  
                 INNER JOIN dbo.INTERLABC AS C ON B.CODCONCEC=C.ORDEN_INDIGO 
                 INNER JOIN dbo.AMBORDLAB AS D ON B.AUTOLABOR= D.AUTO 
                 INNER JOIN dbo.INTERLABD AS DET ON D.AUTO = DET.AUTOLABOR and c.AUTO = det.CODCONCEC  
                 INNER JOIN dbo.INUNIFUNC AS E ON D.UFUCODIGO=E.UFUCODIGO 
                 INNER JOIN dbo.INCUPSIPS AS F ON B.CODSERIPS=F.CODSERIPS 
                 INNER JOIN dbo.INTERCTRL AS G ON D.AUTO=G.AUTOLABOR AND G.ORDEN_INDIGO = C.ORDEN_INDIGO 
                 LEFT OUTER JOIN dbo.INPROFSAL AS H ON G.CODPROSAL=H.CODPROSAL 
                 LEFT OUTER JOIN dbo.INESPECIA I ON H.CODESPEC1 = I.CODESPECI 
             WHERE F.CODSERIPS IN('671201','671202')  
			 AND D.ESTSERIPS<>6 AND DET.NUMMUESTRA ='1'
			 --AND D.IPCODPACI = @ipcodpaci
			 --AND G.FECREGIST>= @Fecha_Historia
			 group by DET.VALOR, D.IPCODPACI) a
 )
	RETURN @variable

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Función escalar que consulta órdenes de laboratorio ambulatorio para los códigos CUPS 671201 y 671202 (correspondientes a biopsia de cérvix uterino) y retorna la fecha mínima de registro del control de resultados asociada. Atraviesa las tablas de interconsulta, detalle de interfaz, integración con laboratorio externo y control de resultados, filtrando por muestra número 1 y excluyendo órdenes con estado 6. Nota: los filtros por paciente y fecha historia están comentados, por lo que actualmente no aplica restricciones sobre esos parámetros.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Bxcervicouterino_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Bxcervicouterino_Fecha';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Retorna la fecha mínima de registro de una orden ambulatoria de laboratorio asociada a biopsia cervicouterina (CUPS 671201/671202) para usarla en reportes RIAS.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Bxcervicouterino_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas INTERCABE, INTERDETA, INTERLABC, AMBORDLAB, INTERLABD, INTERCTRL deben estar correctamente relacionadas por sus llaves (AUTO, CODCONCEC, AUTOLABOR, ORDEN_INDIGO).; Deben existir códigos CUPS ''671201'' y/o ''671202'' registrados en INCUPSIPS para que la consulta pueda emparejar resultados.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Bxcervicouterino_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran servicios con CUPS ''671201'' o ''671202'' (códigos asociados a biopsia cervicouterina).; Excluye órdenes de laboratorio con ESTSERIPS = 6 (estado anulado/no válido).; Únicamente se toma la primera muestra del resultado (DET.NUMMUESTRA = ''1'').; Las órdenes deben ser de tipo ambulatorio (B.ORDTIP = ''AMB'').; Se selecciona la fecha mínima de registro (MIN(G.FECREGIST)) como fecha de servicio reportada.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Bxcervicouterino_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Biopsia cervicouterina; Órdenes de laboratorio ambulatorias; Interconsultas; RIAS (Rutas Integrales de Atención en Salud); CUPS; Resultados de laboratorio; Muestra de laboratorio', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Bxcervicouterino_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar return): Devuelve un único datetime correspondiente al MIN(G.FECREGIST) de las órdenes ambulatorias cuyo CUPS esté en (''671201'',''671202''), con ESTSERIPS<>6 y NUMMUESTRA=''1''; si no hay coincidencias retorna NULL.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Bxcervicouterino_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INTERCABE; dbo.INTERDETA; dbo.INTERLABC; dbo.AMBORDLAB; dbo.INTERLABD; dbo.INUNIFUNC; dbo.INCUPSIPS; dbo.INTERCTRL; dbo.INPROFSAL; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Bxcervicouterino_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Bxcervicouterino_Fecha';
GO
