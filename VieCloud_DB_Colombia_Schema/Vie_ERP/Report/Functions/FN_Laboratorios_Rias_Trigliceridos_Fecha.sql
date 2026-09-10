

-- =============================================
-- Author:		Yohana Rozo
-- Create date: <2021-06-10>
-- Description:	<Funcion que retorna el valor correspondiente a Trigliceridos por mimima fecha>
-- ==========================================

CREATE FUNCTION [Report].[FN_Laboratorios_Rias_Trigliceridos_Fecha]
(
	-- Add the parameters for the function here
	@Fecha_Historia datetime,
	@ipcodpaci varchar(25)
)
 returns datetime
 as
 begin
   declare @variable datetime=(select CAST(a.FECSERIPS AS DATE) from (
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
             WHERE F.CODSERIPS ='903868' 
			 AND D.ESTSERIPS<>6 AND DET.NUMMUESTRA ='1'
			 AND D.IPCODPACI = @ipcodpaci
			 AND G.FECREGIST>= @Fecha_Historia
			 group by DET.VALOR, D.IPCODPACI) a
 )
	RETURN @variable

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Función escalar que, dado un paciente y una fecha de corte, retorna la fecha mínima de registro del resultado de triglicéridos (código CUPS 903868) en órdenes de laboratorio ambulatorio. Consulta la cadena de integración entre interconsultas, órdenes y resultados de laboratorio, filtrando por muestra número 1, excluyendo estados cancelados (ESTSERIPS≠6) y considerando solo registros posteriores o iguales a la fecha recibida. Se enmarca en reportes de seguimiento RIAS para el programa de riesgo cardiovascular.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Trigliceridos_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Trigliceridos_Fecha';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene la fecha más temprana en que se registró un resultado de laboratorio de Triglicéridos (CUPS 903868) ambulatorio válido para un paciente, posterior a una fecha de historia clínica dada, para reportes RIAS.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Trigliceridos_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos una orden de laboratorio ambulatoria (ORDTIP=''AMB'') asociada al paciente con CUPS ''903868'' (Triglicéridos), con NUMMUESTRA=''1'' y estado distinto de 6, con fecha de registro mayor o igual a la fecha de historia.; Las tablas INTERCABE, INTERDETA, INTERLABC, AMBORDLAB, INTERLABD, INTERCTRL e INCUPSIPS deben estar correctamente relacionadas para resolver la trazabilidad de la orden.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Trigliceridos_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera órdenes de laboratorio con tipo de orden ambulatorio (ORDTIP=''AMB'').; Solo considera el examen de Triglicéridos identificado por el código CUPS ''903868''.; Excluye órdenes en estado 6 (ESTSERIPS<>6), interpretado como anuladas/canceladas.; Solo toma en cuenta la primera muestra del examen (NUMMUESTRA=''1'').; Únicamente considera registros cuya fecha de control (FECREGIST) sea posterior o igual a la fecha de historia recibida.; Devuelve la fecha mínima de registro del control de laboratorio (MIN(G.FECREGIST)) truncada a DATE.; Retorna NULL si no existe ninguna orden de Triglicéridos que cumpla las condiciones para el paciente.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Trigliceridos_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Triglicéridos; Laboratorio ambulatorio; RIAS (Rutas Integrales de Atención en Salud); Orden de laboratorio; Paciente; Código CUPS', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Trigliceridos_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar return): Retorna como DATE la mínima FECREGIST de INTERCTRL para órdenes de laboratorio ambulatorias del paciente con CUPS=903868 (Triglicéridos), NUMMUESTRA=''1'', ESTSERIPS<>6 y FECREGIST>=@Fecha_Historia; NULL si no hay coincidencias.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Trigliceridos_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INTERCABE; dbo.INTERDETA; dbo.INTERLABC; dbo.AMBORDLAB; dbo.INTERLABD; dbo.INUNIFUNC; dbo.INCUPSIPS; dbo.INTERCTRL; dbo.INPROFSAL; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Trigliceridos_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Trigliceridos_Fecha';
GO
