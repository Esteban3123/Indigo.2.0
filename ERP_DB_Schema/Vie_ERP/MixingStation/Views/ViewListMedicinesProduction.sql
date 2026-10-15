




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