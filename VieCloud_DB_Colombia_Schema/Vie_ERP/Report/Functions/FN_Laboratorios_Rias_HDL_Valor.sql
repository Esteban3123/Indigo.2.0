

-- =============================================
-- Author:		Yohana Rozo
-- Create date: <2021-06-09>
-- Description:	<Funcion que retorna el valor correspondiente a HDL por mimima fecha>
-- ==========================================
CREATE FUNCTION [Report].[FN_Laboratorios_Rias_HDL_Valor]
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
             WHERE  F.CODSERIPS ='903815' AND ANALITO LIKE '%ALTA DENS%'
			 AND D.ESTSERIPS<>6 AND DET.NUMMUESTRA ='1'
			 AND D.IPCODPACI = @ipcodpaci
			 AND G.FECREGIST>= @Fecha_Historia
			 group by DET.VALOR, D.IPCODPACI) a
 )
	RETURN @variable

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Función escalar que recupera el valor más reciente del analito HDL (colesterol de alta densidad, identificado por el código CUPS 903815 y el texto ''%ALTA DENS%'') para un paciente específico a partir de una fecha de historia clínica dada. Consulta la cadena de órdenes de laboratorio ambulatorio e integración con laboratorio externo, filtrando muestra número 1 y excluyendo órdenes con estado 6, retornando el valor del primer registro según la fecha de registro mínima.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_HDL_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_HDL_Valor';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene el valor del resultado de laboratorio de Colesterol HDL (CUPS 903815, analito de alta densidad) de un paciente ambulatorio, tomando el registro más temprano a partir de una fecha dada.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_HDL_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos una orden ambulatoria de laboratorio para el paciente con CUPS 903815 (HDL) cuyo analito contenga ''ALTA DENS''.; La orden debe estar correlacionada entre INTERCABE, INTERDETA, INTERLABC, AMBORDLAB, INTERLABD e INTERCTRL mediante sus llaves de orden.; La fecha de registro en INTERCTRL debe ser posterior o igual a la fecha de historia recibida.; El estado del servicio en AMBORDLAB no debe ser 6 y el número de muestra debe ser ''1''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_HDL_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera órdenes ambulatorias (ORDTIP=''AMB'').; Excluye órdenes con estado 6 en AMBORDLAB (ESTSERIPS<>6), interpretado como anuladas/no válidas.; Solo toma la primera muestra del analito (NUMMUESTRA=''1'').; Filtra exclusivamente el examen CUPS 903815 cuyo analito contiene ''ALTA DENS'' (Colesterol HDL).; Solo retorna resultados cuya fecha de registro de control sea mayor o igual a la fecha de historia provista.; Devuelve un único valor escalar (TOP 1) correspondiente al resultado de HDL del paciente.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_HDL_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Colesterol HDL (alta densidad); Laboratorio ambulatorio; Resoluciones RIAS (Rutas Integrales de Atención en Salud); Código CUPS 903815; Orden de laboratorio; Analito; Paciente', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_HDL_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.INTERLABD: Retorna el VALOR del analito HDL (CODSERIPS=''903815'' y ANALITO LIKE ''%ALTA DENS%'') para el paciente, filtrando ESTSERIPS<>6, NUMMUESTRA=''1'' y FECREGIST>=@Fecha_Historia, agrupado por valor y paciente, tomando TOP 1 ordenado implícitamente por la mínima fecha de registro.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_HDL_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INTERCABE; dbo.INTERDETA; dbo.INTERLABC; dbo.AMBORDLAB; dbo.INTERLABD; dbo.INUNIFUNC; dbo.INCUPSIPS; dbo.INTERCTRL; dbo.INPROFSAL; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_HDL_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_HDL_Valor';
GO
