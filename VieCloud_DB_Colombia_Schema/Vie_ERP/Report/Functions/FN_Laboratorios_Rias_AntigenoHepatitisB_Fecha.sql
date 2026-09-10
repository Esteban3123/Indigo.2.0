

-- =============================================
-- Author:		Yohana Rozo
-- Create date: <2021-06-10>
-- Description:	<Funcion que retorna el valor correspondiente a AntigenoHepatitisB por mimima fecha>
-- ==========================================

CREATE FUNCTION [Report].[FN_Laboratorios_Rias_AntigenoHepatitisB_Fecha]
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
             WHERE F.CODSERIPS LIKE '906317'  
			 AND D.ESTSERIPS<>6 AND DET.NUMMUESTRA ='1'
			 AND D.IPCODPACI = @ipcodpaci
			 AND G.FECREGIST>= @Fecha_Historia
			 group by DET.VALOR, D.IPCODPACI) a
 )
	RETURN @variable

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Función escalar que, dado un paciente y una fecha de historia clínica, retorna la fecha mínima de registro (`FECREGIST`) del resultado del examen con código CUPS `906317` (Antígeno de Superficie de Hepatitis B) para órdenes ambulatorias activas con muestra número 1. Recorre la cadena de interconsulta, órdenes de laboratorio, resultados por analito y control de procesamiento, filtrando solo registros posteriores o iguales a la fecha proporcionada y excluyendo órdenes con estado 6.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_AntigenoHepatitisB_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_AntigenoHepatitisB_Fecha';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene la fecha mínima de registro de una orden ambulatoria de laboratorio para el examen de Antígeno de Hepatitis B (CUPS 906317) de un paciente, a partir de una fecha de referencia, en el contexto de reportes RIAS.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_AntigenoHepatitisB_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener órdenes de laboratorio ambulatorias (INTERDETA.ORDTIP=''AMB'') asociadas al CUPS 906317; Las órdenes deben estar en estado distinto de 6 (AMBORDLAB.ESTSERIPS<>6, es decir no anuladas/excluidas); El detalle del resultado debe corresponder a la primera muestra (INTERLABD.NUMMUESTRA=''1''); La fecha de registro del control (INTERCTRL.FECREGIST) debe ser mayor o igual a la fecha de historia recibida', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_AntigenoHepatitisB_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera órdenes de tipo ambulatorio (ORDTIP=''AMB''); Solo considera el CUPS 906317 (Antígeno de superficie Hepatitis B); Excluye órdenes con ESTSERIPS=6; Solo evalúa la primera muestra (NUMMUESTRA=''1''); Devuelve un único valor (TOP 1 sobre agregación MIN de FECREGIST)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_AntigenoHepatitisB_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Antígeno de Hepatitis B; Reporte RIAS (Rutas Integrales de Atención en Salud); Orden de laboratorio ambulatoria; CUPS 906317; Muestra de laboratorio; Fecha de registro de resultado', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_AntigenoHepatitisB_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Retorna el FECREGIST mínimo de INTERCTRL para órdenes ambulatorias del CUPS ''906317'' del paciente, filtradas por NUMMUESTRA=''1'', ESTSERIPS<>6 y FECREGIST>=@Fecha_Historia; NULL si no hay coincidencias', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_AntigenoHepatitisB_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INTERCABE; dbo.INTERDETA; dbo.INTERLABC; dbo.AMBORDLAB; dbo.INTERLABD; dbo.INUNIFUNC; dbo.INCUPSIPS; dbo.INTERCTRL; dbo.INPROFSAL; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_AntigenoHepatitisB_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_AntigenoHepatitisB_Fecha';
GO
