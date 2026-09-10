

-- =============================================
-- Author:		Yohana Rozo
-- Create date: <2021-06-09>
-- Description:	<Funcion que retorna el valor correspondiente a Hemoglobina por mimima fecha>
-- ==========================================
CREATE FUNCTION [Report].[FN_Laboratorios_Rias_Microalbuminuria_Valor]
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
             WHERE  F.DESCODCUPS LIKE '%MICROALBUMINURIA%' AND ANALITO LIKE '%MICROALBUMINURIA%'
			 AND D.ESTSERIPS<>6 AND DET.NUMMUESTRA ='1'
			 AND D.IPCODPACI = @ipcodpaci
			 AND G.FECREGIST>= @Fecha_Historia
			 group by DET.VALOR, D.IPCODPACI) a
 )
	RETURN @variable

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Función escalar que recupera el valor del analito Microalbuminuria para un paciente específico a partir de una fecha de historia dada. Consulta el resultado registrado en el detalle de laboratorio filtrando por la descripción CUPS que contenga "MICROALBUMINURIA" y el analito homónimo, excluyendo órdenes canceladas (estado 6) y restringiéndose a la muestra número 1. Retorna el valor correspondiente a la fecha de registro más antigua que cumpla los criterios.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Microalbuminuria_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Microalbuminuria_Valor';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene el valor del resultado de laboratorio de microalbuminuria de un paciente, tomando la muestra principal de órdenes ambulatorias registradas a partir de una fecha de historia clínica dada, para reportería de RIAS.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Microalbuminuria_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una orden ambulatoria de laboratorio para el paciente con servicio CUPS y analito que contengan el texto ''MICROALBUMINURIA''.; La orden no debe estar en estado 6 (ESTSERIPS<>6).; Debe existir al menos un detalle de resultado con NUMMUESTRA=''1''.; La fecha de registro del control de la orden debe ser >= a la fecha de historia suministrada.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Microalbuminuria_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran órdenes de laboratorio de tipo ambulatorio (ORDTIP=''AMB'').; Se excluyen órdenes con estado 6 (ESTSERIPS<>6), interpretado como anuladas o no válidas para reporte.; Solo se toma la primera muestra del analito (NUMMUESTRA=''1'').; El servicio CUPS y el analito deben corresponder ambos a ''MICROALBUMINURIA'' (doble filtro por DESCODCUPS y ANALITO).; Solo se consideran resultados con fecha de registro de control (G.FECREGIST) posterior o igual a la fecha de historia clínica de referencia.; La función retorna un único valor escalar (TOP 1); si no existe coincidencia, retorna NULL.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Microalbuminuria_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Microalbuminuria; Laboratorio ambulatorio; Resultado de analito; Orden de laboratorio; Paciente; RIAS (Rutas Integrales de Atención en Salud)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Microalbuminuria_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.INTERLABD: Retorna DET.VALOR del primer registro (TOP 1) que cumpla: CUPS y analito LIKE ''%MICROALBUMINURIA%'', orden ambulatoria activa (ESTSERIPS<>6), NUMMUESTRA=''1'', paciente=@ipcodpaci y FECREGIST>=@Fecha_Historia.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Microalbuminuria_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INTERCABE; dbo.INTERDETA; dbo.INTERLABC; dbo.AMBORDLAB; dbo.INTERLABD; dbo.INUNIFUNC; dbo.INCUPSIPS; dbo.INTERCTRL; dbo.INPROFSAL; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Microalbuminuria_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Microalbuminuria_Valor';
GO
