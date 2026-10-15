CREATE VIEW [MixingStation].[ViewCampaignMainInfo]
AS 
	SELECT cd.Id,
			cd.CampaignId,
			cd.CampaignNumber,
			utd.MSClass,
			cd.UnitDoseTypeId,
			utd.[Description] UnitDoseTypeDescription,
			cd.CampaignStatus,
			CASE cd.CampaignStatus
				WHEN 1 THEN 'ABIERTA'
				WHEN 2 THEN 'CERRADA'
				WHEN 3 THEN 'BLOQUEADA'
				WHEN 4 THEN 'ANULADA'
				WHEN 5 THEN 'PROCESADA'
				WHEN 6 THEN 'TERMINADA'
				ELSE 'SIN ESTADO'
			END DescriptionStatus,
			CONCAT(vur.QFQualityCode, ' - ', vur.QFQualityName) QFQualityCodeName,
			CONCAT(vur.QFProductionCode, ' - ', vur.QFProductionName) QFProductionCodeName,
			CASE cd.CampaignStatus
				WHEN 1 THEN cd.CreationDate
				WHEN 2 THEN cd.ModificationDate 
				WHEN 3 THEN cd.ModificationDate
				WHEN 4 THEN cd.ModificationDate
				WHEN 5 THEN cd.ProcessingDate
				WHEN 6 THEN cd.FinishDate
				ELSE cd.CreationDate 
			END ModificationStatusDate,
			cd.CreationDate,
			cd.ProcessingDate
	FROM MixingStation.CampaignDetail cd
	JOIN MixingStation.UnitDoseType utd ON cd.UnitDoseTypeId = utd.Id
	JOIN MixingStation.ViewCampaignDetailUser vur ON cd.Id = vur.Id