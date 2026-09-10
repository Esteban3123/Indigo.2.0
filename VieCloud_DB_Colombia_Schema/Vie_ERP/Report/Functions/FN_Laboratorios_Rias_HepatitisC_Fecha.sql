

-- =============================================
-- Author:		Yohana Rozo
-- Create date: <2021-06-10>
-- Description:	<Funcion que retorna el valor correspondiente a AntigenoHepatitisB por mimima fecha>
-- ==========================================

CREATE FUNCTION [Report].[FN_Laboratorios_Rias_HepatitisC_Fecha]
(
	-- Add the parameters for the function here
	@Fecha_Historia datetime,
	@ipcodpaci varchar(25)
)
 returns datetime
 as
 begin
   declare @variable datetime=(select a.FECSERIPS from (
    SELECT TOP 1 DET.VALOR, min(G.FECREGIST ) FECSERIPS, D.IPCODPACI FROM dbo.INTERCABE AS A 
                 INNER JOIN dbo.INTERDETA AS B ON A.AUTO=B.CODCONCEC AND B.ORDTIP ='AMB'  
                 INNER JOIN dbo.INTERLABC AS C ON B.CODCONCEC=C.ORDEN_INDIGO 
                 INNER JOIN dbo.AMBORDLAB AS D ON B.AUTOLABOR= D.AUTO 
                 INNER JOIN dbo.INTERLABD AS DET ON D.AUTO = DET.AUTOLABOR and c.AUTO = det.CODCONCEC  
                 INNER JOIN dbo.INUNIFUNC AS E ON D.UFUCODIGO=E.UFUCODIGO 
                 INNER JOIN dbo.INCUPSIPS AS F ON B.CODSERIPS=F.CODSERIPS 
                 INNER JOIN dbo.INTERCTRL AS G ON D.AUTO=G.AUTOLABOR AND G.ORDEN_INDIGO = C.ORDEN_INDIGO 
                 LEFT OUTER JOIN dbo.INPROFSAL AS H ON G.CODPROSAL=H.CODPROSAL 
                 LEFT OUTER JOIN dbo.INESPECIA I ON H.CODESPEC1 = I.CODESPECI 
             WHERE F.CODSERIPS in ('906225','996263')
			 AND D.ESTSERIPS<>6 AND DET.NUMMUESTRA ='1'
			 AND D.IPCODPACI = @ipcodpaci
			 AND G.FECREGIST >= @Fecha_Historia
			 group by DET.VALOR, D.IPCODPACI) a
 )
	RETURN @variable

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Función escalar que retorna la fecha de registro más antigua (`FECREGIST`) de un resultado de laboratorio asociado a Hepatitis C para un paciente dado, a partir de una fecha de historia clínica específica. Filtra órdenes ambulatorias por los códigos CUPS `906225` y `996263`, excluyendo órdenes con estado 6 y considerando únicamente la muestra número 1. Sirve para identificar la primera fecha en que se procesó el examen de Hepatitis C en el contexto de reportes RIAS.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_HepatitisC_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_HepatitisC_Fecha';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Retorna la fecha mínima de registro de un resultado de laboratorio asociado a la prueba de Hepatitis C (CUPS 906225 o 996263) para un paciente, a partir de una fecha de historia clínica dada.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_HepatitisC_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener órdenes de laboratorio ambulatorias (AMBORDLAB) vinculadas a interconsultas (INTERCABE/INTERDETA con ORDTIP=''AMB'').; Debe existir al menos un resultado registrado en INTERCTRL con FECREGIST >= @Fecha_Historia.; El servicio CUPS debe ser ''906225'' o ''996263'' (pruebas de Hepatitis C).; La orden de laboratorio (AMBORDLAB) no debe estar en estado 6 (anulada/cancelada).; El detalle de resultado (INTERLABD) debe corresponder a la primera muestra (NUMMUESTRA=''1'').', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_HepatitisC_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Se filtran exclusivamente órdenes de tipo ambulatorio (ORDTIP=''AMB'').; Excluye órdenes con ESTSERIPS=6 (estado anulado/no válido).; Solo considera la primera muestra del examen (NUMMUESTRA=''1'').; Solo considera registros posteriores o iguales a la fecha de historia clínica suministrada.; Restringe a códigos CUPS de Hepatitis C: 906225 y 996263.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_HepatitisC_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Hepatitis C; Laboratorio ambulatorio; Orden de laboratorio; CUPS; RIAS (Rutas Integrales de Atención en Salud); Resultado de examen; Paciente; Muestra de laboratorio', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_HepatitisC_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve la mínima FECREGIST de INTERCTRL para órdenes con CODSERIPS IN (''906225'',''996263''), ESTSERIPS<>6, NUMMUESTRA=''1'', IPCODPACI=@ipcodpaci y FECREGIST>=@Fecha_Historia; NULL si no hay coincidencias.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_HepatitisC_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INTERCABE; dbo.INTERDETA; dbo.INTERLABC; dbo.AMBORDLAB; dbo.INTERLABD; dbo.INUNIFUNC; dbo.INCUPSIPS; dbo.INTERCTRL; dbo.INPROFSAL; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_HepatitisC_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_HepatitisC_Fecha';
GO
