

-- =============================================
-- Author:		Yohana Rozo
-- Create date: <2021-06-10>
-- Description:	<Funcion que retorna el valor correspondiente a AntigenoHepatitisB por mimima fecha>
-- ==========================================

CREATE FUNCTION [Report].[FN_Laboratorios_Rias_Creatinina_Fecha]
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
             WHERE F.CODSERIPS LIKE '903895'  
			 AND D.ESTSERIPS<>6 AND DET.NUMMUESTRA ='1'
			 AND D.IPCODPACI = @ipcodpaci
			 AND G.FECREGIST>= @Fecha_Historia
			 group by DET.VALOR, D.IPCODPACI) a
 )
	RETURN @variable

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Función escalar que retorna la fecha mínima de registro (`FECREGIST`) del resultado de creatinina (CUPS `903895`) para un paciente específico, a partir de una fecha de historia clínica dada. Consulta la cadena completa de órdenes ambulatorias de laboratorio —desde la interconsulta hasta el detalle del analito— filtrando por muestra número 1 y excluyendo órdenes con estado 6. Devuelve un `datetime` único correspondiente al primer registro encontrado.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Creatinina_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Creatinina_Fecha';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene la fecha mínima de registro de un examen de laboratorio de creatinina (CUPS 903895) realizado a un paciente a partir de una fecha de historia clínica dada.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Creatinina_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir y tener órdenes de laboratorio ambulatorio asociadas vía INTERCABE/INTERDETA/AMBORDLAB.; Deben existir resultados en INTERLABD con NUMMUESTRA=''1'' para la orden.; Debe existir al menos un registro de control en INTERCTRL con FECREGIST mayor o igual a la fecha de historia recibida.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Creatinina_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera órdenes ambulatorias (INTERDETA.ORDTIP=''AMB'').; Excluye órdenes de laboratorio con estado 6 (ESTSERIPS<>6, presumiblemente anuladas/canceladas).; Solo considera la primera muestra (NUMMUESTRA=''1'').; Filtra exclusivamente el código de servicio CUPS ''903895'' correspondiente a creatinina.; Solo se evalúan registros de control cuya fecha de registro sea posterior o igual a la fecha de historia clínica suministrada.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Creatinina_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Orden de laboratorio ambulatorio; Examen de creatinina (CUPS 903895); Muestra de laboratorio; Profesional de la salud; Especialidad médica; Unidad funcional; RIAS (Rutas Integrales de Atención en Salud)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Creatinina_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Retorna la mínima FECREGIST de INTERCTRL para el examen de laboratorio con CODSERIPS=''903895'' (creatinina), del paciente indicado, donde ESTSERIPS<>6, NUMMUESTRA=''1'' y FECREGIST >= @Fecha_Historia; NULL si no hay coincidencias.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Creatinina_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INTERCABE; dbo.INTERDETA; dbo.INTERLABC; dbo.AMBORDLAB; dbo.INTERLABD; dbo.INUNIFUNC; dbo.INCUPSIPS; dbo.INTERCTRL; dbo.INPROFSAL; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Creatinina_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Creatinina_Fecha';
GO
