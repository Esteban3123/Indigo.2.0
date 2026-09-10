

-- =============================================
-- Author:		Yohana Rozo
-- Create date: <2021-06-10>
-- Description:	<Funcion que retorna el valor correspondiente a LDL por mimima fecha>
-- ==========================================

CREATE FUNCTION [Report].[FN_Laboratorios_Rias_LDL_Fecha]
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
             WHERE F.CODSERIPS IN ('903817','903816') 
			 AND D.ESTSERIPS<>6 AND DET.NUMMUESTRA ='1'
			 AND D.IPCODPACI = @ipcodpaci
			 AND G.FECREGIST>= @Fecha_Historia
			 group by DET.VALOR, D.IPCODPACI) a
 )
	RETURN @variable

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Función escalar que retorna la **fecha mínima de registro** (`FECREGIST`) del resultado de colesterol LDL para un paciente específico, considerando únicamente resultados con fecha igual o posterior a una fecha de historia clínica dada. Filtra por los códigos CUPS `903817` y `903816` (asociados a LDL), excluye órdenes con estado 6 y restringe la muestra al número `1`. Se usa en reportes de RIAS para trazabilidad del primer resultado LDL registrado.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_LDL_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_LDL_Fecha';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Retorna la fecha mínima de registro de un resultado de laboratorio de LDL (CUPS 903817/903816) para un paciente, a partir de una fecha de historia clínica dada, usado en reportes RIAS.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_LDL_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener órdenes de laboratorio ambulatorias (AMBORDLAB) con resultados en INTERLABD vinculadas vía INTERCABE/INTERDETA/INTERLABC/INTERCTRL.; Se requieren registros con CODSERIPS en (''903817'',''903816'') correspondientes a pruebas de LDL.; Las órdenes deben tener ESTSERIPS distinto de 6 (no anuladas/excluidas).; El detalle de resultado debe corresponder a la primera muestra (NUMMUESTRA=''1'').; INTERDETA.ORDTIP debe ser ''AMB'' (orden ambulatoria).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_LDL_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera órdenes ambulatorias (ORDTIP=''AMB'').; Excluye órdenes con ESTSERIPS = 6.; Solo toma la primera muestra del analito (NUMMUESTRA=''1'').; Únicamente reconoce LDL mediante los códigos CUPS ''903817'' y ''903816''.; Solo incluye resultados con fecha de registro posterior o igual a la fecha de historia suministrada.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_LDL_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Laboratorio clínico; Examen de LDL (colesterol); Orden ambulatoria; RIAS (Rutas Integrales de Atención en Salud); CUPS; Paciente; Resultado de laboratorio', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_LDL_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve la mínima FECREGIST de INTERCTRL para resultados de LDL (CODSERIPS 903817 o 903816) del paciente cuya FECREGIST sea >= @Fecha_Historia; si no hay coincidencias retorna NULL.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_LDL_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INTERCABE; dbo.INTERDETA; dbo.INTERLABC; dbo.AMBORDLAB; dbo.INTERLABD; dbo.INUNIFUNC; dbo.INCUPSIPS; dbo.INTERCTRL; dbo.INPROFSAL; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_LDL_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_LDL_Fecha';
GO
