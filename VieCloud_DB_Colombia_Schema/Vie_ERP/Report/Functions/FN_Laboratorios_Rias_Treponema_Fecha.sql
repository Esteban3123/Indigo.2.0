

-- =============================================
-- Author:		Yohana Rozo
-- Create date: <2021-06-10>
-- Description:	<Funcion que retorna el valor correspondiente a Treponema por mimima fecha>
-- ==========================================

CREATE FUNCTION [Report].[FN_Laboratorios_Rias_Treponema_Fecha]
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
                 INNER JOIN dbo.INTERDETA AS B ON A.AUTO=B.CODCONCEC AND B.ORDTIP ='INT'  
                 INNER JOIN dbo.INTERLABC AS C ON B.CODCONCEC=C.ORDEN_INDIGO 
                 INNER JOIN dbo.HCORDLABO AS D ON B.AUTOLABOR= D.AUTO 
                 INNER JOIN dbo.INTERLABD AS DET ON D.AUTO = DET.AUTOLABOR and c.AUTO = det.CODCONCEC  
                 INNER JOIN dbo.INUNIFUNC AS E ON D.UFUCODIGO=E.UFUCODIGO 
                 INNER JOIN dbo.INCUPSIPS AS F ON B.CODSERIPS=F.CODSERIPS 
                 INNER JOIN dbo.INTERCTRL AS G ON D.AUTO=G.AUTOLABOR AND G.ORDEN_INDIGO = C.ORDEN_INDIGO 
                 LEFT OUTER JOIN dbo.INPROFSAL AS H ON G.CODPROSAL=H.CODPROSAL 
                 LEFT OUTER JOIN dbo.INESPECIA I ON H.CODESPEC1 = I.CODESPECI 
             WHERE F.CODSERIPS IN ('906039','906915')             
			 AND D.ESTSERIPS<>6 AND DET.NUMMUESTRA ='1'
			 AND D.IPCODPACI = @ipcodpaci
			 AND G.FECREGIST>= @Fecha_Historia
			 group by DET.VALOR, D.IPCODPACI) a
 )
	RETURN @variable

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Función escalar que retorna la fecha mínima de registro (`FECREGIST`) del resultado de Treponema para un paciente específico, a partir de una fecha de historia clínica dada. Filtra por los códigos CUPS `906039` y `906915` (pruebas serológicas de Treponema), considerando solo muestras con número `1` y órdenes con estado distinto a `6`. Es utilizada en reportes de RIAS para determinar la primera fecha en que se realizó dicha prueba al paciente.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Treponema_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Treponema_Fecha';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene la fecha mínima de registro de un resultado de laboratorio de Treponema (CUPS 906039 o 906915) para un paciente, a partir de una fecha de historia clínica dada.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Treponema_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener órdenes de laboratorio asociadas a interconsultas (INTERCABE/INTERDETA con ORDTIP=''INT''); Deben existir resultados con NUMMUESTRA=''1'' en INTERLABD; La orden de laboratorio no debe estar en estado 6 (ESTSERIPS<>6); Debe existir un registro en INTERCTRL con FECREGIST >= a la fecha de historia provista', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Treponema_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los exámenes considerados son exclusivamente Treponema identificados por los CUPS 906039 y 906915; La fecha retornada siempre proviene de INTERCTRL.FECREGIST agregada con MIN; Solo se evalúan órdenes ligadas a interconsultas (ORDTIP=''INT''); Solo se considera la muestra número 1', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Treponema_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Treponema; Laboratorio clínico; Interconsulta; Orden de laboratorio; CUPS; RIAS (Rutas Integrales de Atención en Salud); Paciente; Muestra', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Treponema_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Retorna la mínima FECREGIST de INTERCTRL (FECSERIPS) correspondiente a pruebas de Treponema (CUPS 906039 o 906915) del paciente, filtrando ESTSERIPS<>6, NUMMUESTRA=''1'' y FECREGIST>=@Fecha_Historia; NULL si no hay coincidencias.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Treponema_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si F.CODSERIPS IN (''906039'',''906915'') → Restringe el cálculo únicamente a exámenes CUPS de Treponema; si D.ESTSERIPS <> 6 → Excluye órdenes en estado 6 (anuladas/canceladas); si DET.NUMMUESTRA = ''1'' → Considera solo la primera muestra del resultado; si G.FECREGIST >= @Fecha_Historia → Considera solo registros de control posteriores o iguales a la fecha de historia clínica', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Treponema_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INTERCABE; dbo.INTERDETA; dbo.INTERLABC; dbo.HCORDLABO; dbo.INTERLABD; dbo.INUNIFUNC; dbo.INCUPSIPS; dbo.INTERCTRL; dbo.INPROFSAL; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Treponema_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Treponema_Fecha';
GO
