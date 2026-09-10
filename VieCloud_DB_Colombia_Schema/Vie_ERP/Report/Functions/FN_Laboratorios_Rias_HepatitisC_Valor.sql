

-- =============================================
-- Author:		Yohana Rozo
-- Create date: <2021-06-10>
-- Description:	<Funcion que retorna el valor correspondiente a AntigenoHepatitisB por mimima fecha>
-- ==========================================

CREATE FUNCTION [Report].[FN_Laboratorios_Rias_HepatitisC_Valor]
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
             WHERE F.CODSERIPS  in ('906225','906263')           
			 AND D.ESTSERIPS<>6 AND DET.NUMMUESTRA ='1'
			 AND D.IPCODPACI = @ipcodpaci
			 AND G.FECREGIST>= @Fecha_Historia
			 group by DET.VALOR, D.IPCODPACI) a
 )
	RETURN @variable

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Función escalar que recupera el valor del resultado de laboratorio para Hepatitis C (códigos CUPS 906225 y 906263) de un paciente específico, tomando el registro con la fecha de registro mínima a partir de una fecha de historia clínica dada. Filtra órdenes ambulatorias activas (excluyendo estado 6) y considera únicamente la muestra número 1, retornando el valor del analito correspondiente.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_HepatitisC_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_HepatitisC_Valor';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Retorna el valor del resultado de laboratorio asociado a pruebas de Hepatitis C (CUPS 906225 o 906263) para un paciente, considerando la primera muestra y registros posteriores a una fecha de historia clínica dada.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_HepatitisC_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir orden ambulatoria (INTERDETA.ORDTIP=''AMB'') vinculada al paciente.; El servicio CUPS debe ser ''906225'' o ''906263'' (códigos asociados a Hepatitis C).; La orden de laboratorio no debe estar en estado 6 (AMBORDLAB.ESTSERIPS<>6, es decir no anulada/cancelada).; El detalle de resultado debe corresponder a la primera muestra (INTERLABD.NUMMUESTRA=''1'').; Debe existir registro de control (INTERCTRL) con FECREGIST mayor o igual a la fecha de historia indicada.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_HepatitisC_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera órdenes ambulatorias (ORDTIP=''AMB'').; Excluye órdenes de laboratorio con estado 6 (anuladas).; Solo evalúa la primera muestra (NUMMUESTRA=''1'').; Restringe los resultados a los códigos CUPS ''906225'' y ''906263'' (pruebas de Hepatitis C).; Únicamente toma registros con fecha de control (FECREGIST) igual o posterior a la fecha de historia recibida.; Retorna un solo valor (TOP 1) aunque existan múltiples coincidencias.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_HepatitisC_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Laboratorio clínico; Hepatitis C; Órdenes ambulatorias; Resultados de laboratorio; CUPS (codificación de servicios de salud); RIAS (Rutas Integrales de Atención en Salud); Muestra de laboratorio', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_HepatitisC_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve un único VALOR (varchar(max)) tomado del TOP 1 de INTERLABD.VALOR para el paciente, filtrando por CUPS de Hepatitis C, primera muestra, orden no anulada y FECREGIST >= @Fecha_Historia; si no hay coincidencias retorna NULL.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_HepatitisC_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INTERCABE; dbo.INTERDETA; dbo.INTERLABC; dbo.AMBORDLAB; dbo.INTERLABD; dbo.INUNIFUNC; dbo.INCUPSIPS; dbo.INTERCTRL; dbo.INPROFSAL; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_HepatitisC_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_HepatitisC_Valor';
GO
