CREATE PROCEDURE [dbo].[ESE_SP_depreciacion_activos] @depre INT, 
                                                    @libro INT
AS
     SELECT padb.legalbookid, 
            pa.ItemId, 
            AR.Code, 
            AR.Description, 
            pa.Plate, 
            pa.HistoricalValue, 
            pa.FairValue, 
            RES.Code, 
            TER.Name, 
            LO.Code, 
            LO.Name, 
            padb.DepreciatedValue, 
            padb.Id, 
            dd.AccumulatedDepreciation, 
            dd.AdjustedValue, 
            dd.DepreciatedDays, 
            dd.DepreciationValue, 
            dd.DevaluationValue, 
            dd.LifeTime, 
            dd.TransactionValue, 
            dd.ValorizationValue, 
            padb.HistoricalValue AS 'vhistorico niif'
     FROM FixedAsset.FixedAssetPhysicalAssetDetailBook Padb
          LEFT JOIN FixedAsset.FixedAssetDepreciationDetail dd ON dd.FixedAssetPhysicalAssetDetailBookId = padb.Id
                                                                  AND dd.FixedAssetDepreciationId = @depre
                                                                  AND dd.LegalBookId = @libro
          INNER JOIN FixedAsset.FixedAssetPhysicalAsset pa ON padb.PhysicalAssetId = pa.Id
          INNER JOIN FixedAsset.FixedAssetItem AR ON pa.itemid = AR.Id
          INNER JOIN FixedAsset.FixedAssetResponsible RES ON pa.ResponsibleId = RES.Id
          INNER JOIN Common.ThirdParty TER ON RES.ThirdPartyId = TER.Id
          INNER JOIN FixedAsset.FixedAssetLocation LO ON pa.LocationId = LO.Id
     WHERE padb.LegalBookId = @libro;