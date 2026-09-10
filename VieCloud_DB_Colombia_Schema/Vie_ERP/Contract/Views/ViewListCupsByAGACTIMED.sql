

CREATE VIEW [Contract].[ViewListCupsByAGACTIMED]
AS

with cte_Description as  (	SELECT CUPSEntityId, CAST( 1 AS BIT) Flag
							from Contract.CUPSEntityContractDescriptions with(NOLOCK)
							GROUP by CUPSEntityId)

SELECT 
	CUPS.Id
	,RTRIM(LTRIM(I.CODSERIPS)) CODSERIPS 
	,I.DESSERIPS
	,Concat(Trim(I.CODSERIPS), ' - ', Trim(DESSERIPS)) CodeName
	,ACT.CODACTMED
	,I.TIPSERIPS
	,I.APLICARIAS
	, COALESCE(cte.Flag,0) as HaveDescription
FROM dbo.AGACTIMED ACT WITH(NOLOCK)
LEFT JOIN dbo.AGACTMEDD ACTD WITH(NOLOCK) ON ACT.CODACTMED = ACTD.CODACTMED
JOIN dbo.INCUPSIPS I WITH(NOLOCK) on ISNULL(ACTD.CODSERIPS,ACT.CODSERIPS) = I.CODSERIPS
JOIN Contract.CUPSEntity CUPS ON I.CODSERIPS = CUPS.Code
LEFT JOIN cte_Description cte on CUPS.Id=cte.CUPSEntityId
WHERE  I.SIPSESTADO = 1 
GROUP BY CUPS.ID, I.CODSERIPS,I.DESSERIPS,ACT.CODACTMED,I.TIPSERIPS,I.APLICARIAS,cte.Flag
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los servicios CUPS (procedimientos, exámenes y consultas) asociados a cada actividad médica configurada para agendamiento de citas (AGACTIMED), integrando la descripción oficial del servicio, su tipo y si aplica para RIAS. Combina el catálogo de actividades médicas agendables con el maestro de servicios CUPS activos y el catálogo contractual, permitiendo saber si cada procedimiento ya tiene descripciones de contrato y conceptos de facturación definidos. Se usa principalmente en la configuración de agendamiento y facturación para verificar qué servicios CUPS están disponibles por actividad médica y cuáles ya tienen reglas de cobro o contrato asociadas. Las columnas principales exponen el código y descripción del servicio (CODSERIPS, DESSERIPS), el código de la actividad médica de agendamiento (CODACTMED), el tipo de servicio (TIPSERIPS), si aplica para RIAS (APLICARIAS) y si tiene descripción contractual configurada (HaveDescription).', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'VIEW', @level1name = N'ViewListCupsByAGACTIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'VIEW', @level1name = N'ViewListCupsByAGACTIMED';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los códigos CUPS activos asociados a actividades médicas de agendamiento, indicando si tienen descripciones de contrato configuradas.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListCupsByAGACTIMED';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen actividades médicas en AGACTIMED con código CUPS asignado directamente o vía AGACTMEDD; Los códigos CUPS deben existir en INCUPSIPS y estar registrados en Contract.CUPSEntity', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListCupsByAGACTIMED';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen servicios CUPS en estado activo (SIPSESTADO=1); El código CUPS efectivo proviene de AGACTMEDD si existe, en caso contrario de AGACTIMED; Cada fila indica explícitamente si el CUPS tiene o no descripciones de contrato asociadas; Los códigos CUPS se devuelven sin espacios en blanco a los extremos (TRIM/RTRIM/LTRIM)', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListCupsByAGACTIMED';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'CUPS; Actividad médica; Agendamiento; Servicio IPS; Descripciones de contrato', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListCupsByAGACTIMED';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve solo CUPS cuyo estado en INCUPSIPS sea activo (SIPSESTADO = 1)', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListCupsByAGACTIMED';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ACTD.CODSERIPS es NULL (la actividad no tiene detalle en AGACTMEDD) → Se usa el CODSERIPS definido directamente en AGACTIMED para el cruce con INCUPSIPS else Se prioriza el CODSERIPS definido en AGACTMEDD sobre el de AGACTIMED; si Existe al menos un registro en CUPSEntityContractDescriptions para el CUPSEntityId → HaveDescription = 1 else HaveDescription = 0 (COALESCE con 0 cuando el CTE no encuentra coincidencia)', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListCupsByAGACTIMED';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.CUPSEntityContractDescriptions; dbo.AGACTIMED; dbo.AGACTMEDD; dbo.INCUPSIPS; Contract.CUPSEntity', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListCupsByAGACTIMED';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListCupsByAGACTIMED';
GO
