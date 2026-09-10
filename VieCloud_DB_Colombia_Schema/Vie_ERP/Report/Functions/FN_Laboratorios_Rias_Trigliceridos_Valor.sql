

-- =============================================
-- Author:		Yohana Rozo
-- Create date: <2021-06-09>
-- Description:	<Funcion que retorna el valor correspondiente a Trigliceridos por mimima fecha>
-- ==========================================
CREATE FUNCTION [Report].[FN_Laboratorios_Rias_Trigliceridos_Valor]
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
             WHERE  F.CODSERIPS ='903868' AND ANALITO LIKE '%TRIGLIC%'
			 AND D.ESTSERIPS<>6 AND DET.NUMMUESTRA ='1'
			 AND D.IPCODPACI = @ipcodpaci
			 AND G.FECREGIST>= @Fecha_Historia
			 group by DET.VALOR, D.IPCODPACI) a
 )
	RETURN @variable

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Función escalar que recupera el valor más reciente del analito Triglicéridos (CUPS 903868) para un paciente específico a partir de una fecha de historia clínica dada. Recorre la cadena de interconsultas, órdenes ambulatorias de laboratorio y sus resultados, filtrando por analito con nombre similar a "TRIGLIC", muestra número 1 y órdenes no canceladas (estado distinto de 6), retornando el valor registrado en la fecha mínima de control encontrada.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Trigliceridos_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Trigliceridos_Valor';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Retorna el valor del resultado de laboratorio de Triglicéridos (CUPS 903868) más antiguo registrado a partir de una fecha dada para un paciente, usado en reportes RIAS.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Trigliceridos_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener al menos una orden de laboratorio ambulatoria (ORDTIP=''AMB'') con servicio CUPS ''903868'' y analito que contenga ''TRIGLIC''.; La orden no debe estar en estado 6 (ESTSERIPS<>6, presumiblemente anulada).; Debe existir un registro de control (INTERCTRL) con FECREGIST >= @Fecha_Historia.; El detalle de resultado debe corresponder a la primera muestra (NUMMUESTRA=''1'').', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Trigliceridos_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera órdenes ambulatorias (ORDTIP=''AMB'').; Solo considera el servicio CUPS ''903868'' (Triglicéridos).; Excluye órdenes con ESTSERIPS=6.; Restringe a la primera muestra del laboratorio (NUMMUESTRA=''1'').; Filtra por analito cuyo nombre contiene ''TRIGLIC''.; Solo retorna resultados con fecha de registro de control posterior o igual a la fecha de historia indicada.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Trigliceridos_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Orden de laboratorio ambulatoria; Resultado de laboratorio; Analito Triglicéridos; CUPS 903868; RIAS (Rutas Integrales de Atención en Salud); Muestra de laboratorio', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Trigliceridos_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve el VALOR de INTERLABD para el primer registro (TOP 1) agrupado por valor y paciente, asociado al CUPS 903868, analito ''TRIGLIC%'', muestra 1 y fecha de control >= @Fecha_Historia.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Trigliceridos_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INTERCABE; dbo.INTERDETA; dbo.INTERLABC; dbo.AMBORDLAB; dbo.INTERLABD; dbo.INUNIFUNC; dbo.INCUPSIPS; dbo.INTERCTRL; dbo.INPROFSAL; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Trigliceridos_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Trigliceridos_Valor';
GO
