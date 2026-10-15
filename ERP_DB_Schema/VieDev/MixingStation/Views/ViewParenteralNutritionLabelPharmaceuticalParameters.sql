CREATE VIEW [MixingStation].[ViewParenteralNutritionLabelPharmaceuticalParameters]
AS
	SELECT 
		npp.ID Id
		, npp.IDHCNUTPAREC ParenteralNutritionId
		--------------------------------------
		, npp.NAMEFORM ParameterName
		, npp.RESULT Result
	FROM HCNUTPAREFD npp