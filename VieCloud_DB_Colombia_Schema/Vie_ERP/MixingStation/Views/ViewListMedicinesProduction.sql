

CREATE view [MixingStation].[ViewListMedicinesProduction] 
as 
SELECT 
		mp.Id,
		cmc.Id CMConfigurationId,
		mp.Status,
		IIF(mp.AllowsRemnant = 1, 'Si', 'No') AS AllowRemant,
		mp.ATCId,
		CONCAT(atc.Code, ' - ', atc.Name) ATCCodeName,
		CONCAT(udt.Code, ' - ', udt.Description) UnitDoseTypeCodeName,
		ISNULL(RTRIM(adc.CODCENATE) + ' - ' + RTRIM(adc.NOMCENATE), RTRIM(ecc.Code) + ' - ' + RTRIM(ecc.Description)) CenterAttentionCodeName
FROM MixingStation.MedicinesProduction mp (NOLOCK)
JOIN MixingStation.CMConfiguration cmc (NOLOCK) ON  mp.CMConfigurationId = cmc.Id
JOIN Inventory.ATC atc (NOLOCK) ON mp.ATCId = atc.Id
JOIN MixingStation.UnitDoseType udt (NOLOCK) ON mp.UnitDoseTypeId = udt.Id
LEFT JOIN dbo.ADCENATEN adc (NOLOCK) ON mp.CenterAttentionId = adc.CODCENATE
LEFT JOIN MixingStation.ExternalCareCenter ecc (NOLOCK) ON mp.CenterAttentionId = ecc.Code
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista consolidada de medicamentos configurados para producción en la estación de mezclas (dosis unitaria). Combina cada medicamento de producción con su clasificación ATC (código y nombre del principio activo), el tipo de dosis unitaria asignada, la configuración de mezcla a la que pertenece (CMConfiguration) y el centro de atención donde se produce —ya sea una sede interna del sistema (ADCENATEN) o un centro externo (ExternalCareCenter). También indica el estado del medicamento en producción y si se permite el uso de remanentes o sobrantes del preparado. Sirve como fuente para reportes y pantallas de gestión de preparaciones farmacéuticas en servicios de dosis unitaria y mezclas intravenosas.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListMedicinesProduction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListMedicinesProduction';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de listado consolidado de producciones de medicamentos de la estación de mezclas, presentando códigos y descripciones legibles de ATC, tipo de dosis unitaria y centro de atención (interno o externo).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListMedicinesProduction';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'MedicinesProduction debe tener CMConfigurationId, ATCId y UnitDoseTypeId válidos para aparecer en la vista (se aplican INNER JOIN).; El CenterAttentionId puede existir en ADCENATEN o en ExternalCareCenter; si no existe en ninguno, CenterAttentionCodeName será NULL.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListMedicinesProduction';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen producciones de medicamentos que tengan configuración de mezcla (CMConfiguration), ATC y tipo de dosis unitaria asociados (JOIN obligatorio).; El centro de atención puede provenir de ADCENATEN (interno) o de ExternalCareCenter (externo); se prioriza el interno mediante ISNULL.; El indicador de remanente se traduce a texto legible ''Si''/''No'' a partir del bit AllowsRemnant.; Los códigos descriptivos (ATC, UnitDoseType, CenterAttention) se concatenan en formato ''Código - Nombre/Descripción''.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListMedicinesProduction';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Producción de medicamentos; Estación de mezclas; Clasificación ATC; Dosis unitaria; Centro de atención; Centro de atención externo; Remanente', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListMedicinesProduction';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve una fila por cada MedicinesProduction con su configuración de mezcla, ATC, tipo de dosis y centro de atención resuelto (interno vía ADCENATEN o externo vía ExternalCareCenter).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListMedicinesProduction';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si mp.AllowsRemnant = 1 → Se muestra ''Si'' en AllowRemant else Se muestra ''No'' en AllowRemant; si Existe coincidencia de mp.CenterAttentionId con dbo.ADCENATEN.CODCENATE → CenterAttentionCodeName se construye con CODCENATE + NOMCENATE de ADCENATEN else Se usa Code + Description de MixingStation.ExternalCareCenter como fallback (vía ISNULL)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListMedicinesProduction';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.MedicinesProduction; MixingStation.CMConfiguration; Inventory.ATC; MixingStation.UnitDoseType; dbo.ADCENATEN; MixingStation.ExternalCareCenter', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListMedicinesProduction';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListMedicinesProduction';
GO
