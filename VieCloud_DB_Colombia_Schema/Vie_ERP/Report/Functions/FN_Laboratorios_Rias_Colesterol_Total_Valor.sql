

-- =============================================
-- Author:		Yohana Rozo
-- Create date: <2021-06-09>
-- Description:	<Funcion que retorna el valor correspondiente a Glicosilada por mimima fecha>
-- ==========================================
CREATE FUNCTION [Report].[FN_Laboratorios_Rias_Colesterol_Total_Valor]
(
	-- Add the parameters for the function here
	@Fecha_Historia datetime,
	@ipcodpaci varchar(25)
)
 returns varchar(max)
 as
 begin
   declare @variable varchar(max)=(select a.valor from (
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
             WHERE  F.CODSERIPS ='903818' AND ANALITO LIKE '%COLESTEROL%'
			 AND D.ESTSERIPS<>6 AND DET.NUMMUESTRA ='1'
			 AND D.IPCODPACI = @ipcodpaci
			 AND G.FECREGIST>= @Fecha_Historia
			 group by DET.VALOR, D.IPCODPACI) a
 )
	RETURN @variable

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Función escalar que retorna el valor del analito de colesterol total (CUPS 903818, analito que contiene ''COLESTEROL'') registrado en el resultado de laboratorio ambulatorio de un paciente específico, tomando el registro más antiguo a partir de una fecha de historia clínica dada. Forma parte de los reportes de RIAs (Rutas Integrales de Atención en Salud), específicamente para el seguimiento del perfil lipídico del paciente.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Colesterol_Total_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Colesterol_Total_Valor';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Retorna el valor del resultado de laboratorio de Colesterol Total (CUPS 903818) más antiguo registrado para un paciente a partir de una fecha de referencia, para uso en reportes RIAS.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Colesterol_Total_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir en órdenes ambulatorias de laboratorio (AMBORDLAB).; Debe existir una orden con CUPS ''903818'' y al menos un analito cuyo nombre contenga ''COLESTEROL''.; La orden debe tener ESTSERIPS distinto de 6 (no anulada/excluida).; El detalle del analito debe corresponder a NUMMUESTRA = ''1''.; Debe existir un registro en INTERCTRL con FECREGIST mayor o igual a la fecha de historia indicada.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Colesterol_Total_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera órdenes de laboratorio ambulatorias (INTERDETA.ORDTIP = ''AMB'').; Solo considera el CUPS de Colesterol Total: ''903818''.; Excluye resultados de órdenes con ESTSERIPS = 6.; Solo considera la primera muestra (NUMMUESTRA = ''1'').; Selecciona el resultado correspondiente a la mínima fecha de registro (FECREGIST) posterior o igual a la fecha de historia.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Colesterol_Total_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Orden de laboratorio ambulatoria; Resultado de laboratorio; Analito Colesterol Total; CUPS 903818; RIAS (Rutas Integrales de Atención en Salud); Estado de servicio (ESTSERIPS); Muestra de laboratorio', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Colesterol_Total_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Retorna el VALOR del analito de COLESTEROL para el paciente, filtrado por CUPS 903818, NUMMUESTRA=''1'', ESTSERIPS<>6 y FECREGIST >= @Fecha_Historia, tomando el TOP 1 asociado a la mínima FECREGIST.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Colesterol_Total_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INTERCABE; dbo.INTERDETA; dbo.INTERLABC; dbo.AMBORDLAB; dbo.INTERLABD; dbo.INUNIFUNC; dbo.INCUPSIPS; dbo.INTERCTRL; dbo.INPROFSAL; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Colesterol_Total_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Colesterol_Total_Valor';
GO
