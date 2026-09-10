

-- =============================================
-- Author:		Yohana Rozo
-- Create date: <2021-06-09>
-- Description:	<Funcion que retorna el valor correspondiente a AntigenoHepatitisB por mimima fecha>
-- ==========================================

CREATE FUNCTION [Report].[FN_Laboratorios_Rias_SerologiaSifilis_Valor]
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
             WHERE F.CODSERIPS in ('906915','906039')             
			 AND D.ESTSERIPS<>6 AND DET.NUMMUESTRA ='1'
			 AND D.IPCODPACI = @ipcodpaci
			 AND G.FECREGIST>= @Fecha_Historia
			 group by DET.VALOR, D.IPCODPACI) a
 )
	RETURN @variable

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Función escalar que retorna el valor del resultado de serología para sífilis (códigos CUPS 906915 y 906039) de un paciente específico, tomando el registro más antiguo a partir de una fecha de historia clínica dada. Consulta la cadena completa de órdenes de laboratorio ambulatorio, su integración con el laboratorio externo y el detalle de resultados por analito, filtrando la muestra número 1 y excluyendo órdenes con estado 6.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_SerologiaSifilis_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_SerologiaSifilis_Valor';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene el valor del resultado de laboratorio de serología de sífilis (CUPS 906915/906039) correspondiente a la orden ambulatoria más temprana de un paciente a partir de una fecha de historia clínica dada.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_SerologiaSifilis_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente identificado debe tener órdenes de laboratorio ambulatorias (ORDTIP=''AMB'') asociadas a interconsultas en INTERCABE/INTERDETA.; Debe existir al menos una orden con CUPS ''906915'' o ''906039'' (pruebas de serología de sífilis) con estado distinto de 6 y con NUMMUESTRA=''1''.; Debe existir un registro en INTERCTRL con FECREGIST mayor o igual a la fecha de historia provista.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_SerologiaSifilis_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran órdenes de laboratorio cuyo CUPS (CODSERIPS) sea ''906915'' o ''906039'', asociados a serología de sífilis.; Excluye órdenes con ESTSERIPS = 6 (estado considerado inválido/anulado).; Solo se toma el detalle correspondiente a la primera muestra (NUMMUESTRA = ''1'').; Solo se consideran órdenes de tipo ambulatorio (ORDTIP = ''AMB'').; Únicamente se evalúan registros de control (INTERCTRL) cuya FECREGIST sea posterior o igual a la fecha de historia recibida.; Devuelve un único valor (TOP 1) para el paciente solicitado.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_SerologiaSifilis_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Serología de sífilis; RIAS (Rutas Integrales de Atención en Salud); Orden de laboratorio ambulatorio; Resultado de laboratorio; Paciente; Códigos CUPS 906915 y 906039; Muestra de laboratorio', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_SerologiaSifilis_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.INTERLABD: Retorna DET.VALOR del primer registro (TOP 1) que cumple: CUPS in (''906915'',''906039''), ESTSERIPS<>6, NUMMUESTRA=''1'', IPCODPACI=@ipcodpaci y FECREGIST>=@Fecha_Historia; si no hay coincidencias retorna NULL.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_SerologiaSifilis_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INTERCABE; dbo.INTERDETA; dbo.INTERLABC; dbo.AMBORDLAB; dbo.INTERLABD; dbo.INUNIFUNC; dbo.INCUPSIPS; dbo.INTERCTRL; dbo.INPROFSAL; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_SerologiaSifilis_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_SerologiaSifilis_Valor';
GO
