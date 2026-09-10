

-- =============================================
-- Author:		Yohana Rozo
-- Create date: <2021-06-10>
-- Description:	<Funcion que retorna el valor correspondiente a AntigenoHepatitisB por mimima fecha>
-- ==========================================

CREATE FUNCTION [Report].[FN_Laboratorios_Rias_Baciloscopia_Valor]
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
             WHERE F.CODSERIPS in ('901101','901111')          
			 AND D.ESTSERIPS<>6 AND DET.NUMMUESTRA ='1'
			 AND D.IPCODPACI = @ipcodpaci
			 AND G.FECREGIST>= @Fecha_Historia
			 group by DET.VALOR, D.IPCODPACI) a
 )
	RETURN @variable

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Función escalar que recupera el valor del resultado de **Baciloscopia** (códigos CUPS 901101 y 901111) correspondiente a la primera fecha de registro (`min(FECREGIST)`) para un paciente y una fecha de historia clínica dadas. Consulta la cadena de órdenes ambulatorias de laboratorio, filtrando por muestra número 1 y excluyendo órdenes con estado 6. Retorna el valor del analito como cadena de texto o `NULL` si no existe resultado.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Baciloscopia_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Baciloscopia_Valor';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el valor del resultado de baciloscopia (CUPS 901101 o 901111) más antiguo registrado para un paciente desde una fecha dada, usado en reportes RIAS.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Baciloscopia_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener órdenes de laboratorio ambulatorias (ORDTIP=''AMB'') con CUPS 901101 o 901111; Debe existir al menos un resultado con NUMMUESTRA=''1'' y estado de servicio distinto de 6 (anulado/no vigente); La fecha de registro de control (INTERCTRL.FECREGIST) debe ser igual o posterior a la fecha de historia recibida', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Baciloscopia_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran órdenes ambulatorias (INTERDETA.ORDTIP=''AMB''); Solo se consideran muestras con NUMMUESTRA=''1'' (primera muestra); Excluye órdenes con ESTSERIPS=6; Filtra exclusivamente exámenes de baciloscopia identificados por CUPS 901101 o 901111; Selecciona el resultado por mínima fecha de registro (MIN(FECREGIST)) usando TOP 1', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Baciloscopia_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Baciloscopia; RIAS (Rutas Integrales de Atención en Salud); Orden de laboratorio ambulatoria; CUPS; Resultado de laboratorio; Paciente; Muestra', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Baciloscopia_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Retorna INTERLABD.VALOR del primer registro (TOP 1) que cumpla CUPS in (''901101'',''901111''), ESTSERIPS<>6, NUMMUESTRA=''1'', IPCODPACI=@ipcodpaci y FECREGIST>=@Fecha_Historia', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Baciloscopia_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INTERCABE; dbo.INTERDETA; dbo.INTERLABC; dbo.AMBORDLAB; dbo.INTERLABD; dbo.INUNIFUNC; dbo.INCUPSIPS; dbo.INTERCTRL; dbo.INPROFSAL; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Baciloscopia_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Baciloscopia_Valor';
GO
