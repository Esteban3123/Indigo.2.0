CREATE FUNCTION [FixedAsset].[FixedAssetDepreciationDetail_ValidateResidualValue] ()
	RETURNS BIT
AS 
    BEGIN 
        DECLARE @return BIT = 0
        
		IF (
				NOT EXISTS
				(
					SELECT fad.Id
					FROM FixedAsset.FixedAssetDepreciation fad
					JOIN FixedAsset.FixedAssetDepreciationDetail fadd ON fad.Id = fadd.FixedAssetDepreciationId
					WHERE fad.Status = 2 AND
						--Se valida que el valor residual corresponda al valor residual anterior menor el valor de la depreciacion
						ISNULL
						(
							(
								SELECT TOP 1 fadd2.ResidualValue - fadd2.DepreciationValue + (fadd.TransactionValue - fadd2.TransactionValue)
								FROM FixedAsset.FixedAssetDepreciation fad2
								JOIN FixedAsset.FixedAssetDepreciationDetail fadd2 ON fad2.Id = fadd2.FixedAssetDepreciationId
								WHERE fad2.Status = 2 AND fadd2.FixedAssetPhysicalAssetId = fadd.FixedAssetPhysicalAssetId
									AND fadd2.LegalBookId = fadd.LegalBookId AND fadd2.Id < fadd.Id
								ORDER BY fadd2.Id DESC
							), 
							fadd.ResidualValue
						) <> fadd.ResidualValue
				)
			)
			SET @return = 1

        RETURN @return 
    END