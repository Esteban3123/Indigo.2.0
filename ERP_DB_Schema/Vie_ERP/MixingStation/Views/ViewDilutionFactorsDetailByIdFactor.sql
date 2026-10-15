




CREATE VIEW [MixingStation].[ViewDilutionFactorsDetailByIdFactor] 
AS
		Select CONCAT(DFD.Id, ' ') Id
		,DF.Code as CodeFactor
		, DF.Id as DilutionFactorId
		, dfd.Id as DilutionFactorDetailId
		, df.ATCId as ATCDilutionFactor
		, DFD.AtcId as ATCDilutionFactorDetail
		,dfd.Volume
		,dfd.Dilution
		,DFD.Concentration
		,DFD.AmountTime
		,DFD.TimeUnit
		, a.Code
		, a.Name
		,df.CreationDate
		,df.ModificationDate
		From MixingStation.DilutionFactorsDetail DFD
		Inner Join MixingStation.DilutionFactors DF On DFD.DilutionFactorsId = DF.Id
		INNER join Inventory.ATC A on DFD.AtcId = A.Id