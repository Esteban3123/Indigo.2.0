

-- =============================================
-- Author:		Yohana Rozo
-- Create date: <2021-06-09>
-- Description:	<Funcion que retorna el valor correspondiente a Hemoglobina por mimima fecha>
-- ==========================================
CREATE FUNCTION [Report].[FN_Laboratorios_Rias_Creatinina_Valor]
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
             WHERE F.CODSERIPS ='903895' AND ANALITO LIKE '%CREATININA%'
			 AND D.ESTSERIPS<>6 AND DET.NUMMUESTRA ='1'
			 AND D.IPCODPACI = @ipcodpaci
			 AND G.FECREGIST>= @Fecha_Historia
			 group by DET.VALOR, D.IPCODPACI) a
 )
	RETURN @variable

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Función escalar que recupera el valor más reciente del analito **Creatinina** (CUPS 903895) registrado en el sistema de laboratorio para un paciente específico, a partir de una fecha de historia clínica dada. Consulta la cadena de órdenes ambulatorias e interconsultas, filtrando por muestra número 1, estado de orden distinto a 6 y nombre de analito que contenga "CREATININA", retornando el resultado como texto.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Creatinina_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Creatinina_Valor';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el valor del resultado de laboratorio de Creatinina (CUPS 903895, primera muestra) más reciente registrado para un paciente ambulatorio a partir de una fecha de historia clínica dada.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Creatinina_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una orden de laboratorio ambulatoria para el paciente con CUPS 903895 (Creatinina) cuyo registro de control sea igual o posterior a la fecha de historia indicada.; El paciente identificado por @ipcodpaci debe existir en AMBORDLAB y tener trazabilidad completa a través de INTERCABE → INTERDETA → INTERLABC → INTERCTRL → INTERLABD.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Creatinina_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera órdenes de laboratorio de tipo ambulatorio (INTERDETA.ORDTIP = ''AMB'').; Filtra exclusivamente el examen CUPS ''903895'' y analitos cuyo nombre contenga ''CREATININA''.; Excluye órdenes con estado de servicio 6 en AMBORDLAB (ESTSERIPS<>6), es decir, órdenes anuladas/no válidas.; Considera únicamente la primera muestra del resultado (DET.NUMMUESTRA=''1'').; Solo retorna resultados cuya fecha de registro de control (INTERCTRL.FECREGIST) sea posterior o igual a la fecha de historia clínica recibida.; Retorna un único valor (TOP 1) de creatinina, asociado a la mínima fecha de registro del control.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Creatinina_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Creatinina; Laboratorio ambulatorio; Analito; Orden de laboratorio; CUPS (903895); Paciente; RIAS (Rutas Integrales de Atención en Salud)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Creatinina_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Retorna un VARCHAR(MAX) con el valor (DET.VALOR) del analito CREATININA del CUPS 903895, para órdenes ambulatorias no anuladas (ESTSERIPS<>6), primera muestra (NUMMUESTRA=''1''), del paciente @ipcodpaci con FECREGIST >= @Fecha_Historia; si no hay coincidencias retorna NULL.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Creatinina_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INTERCABE; dbo.INTERDETA; dbo.INTERLABC; dbo.AMBORDLAB; dbo.INTERLABD; dbo.INUNIFUNC; dbo.INCUPSIPS; dbo.INTERCTRL; dbo.INPROFSAL; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Creatinina_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Laboratorios_Rias_Creatinina_Valor';
GO
