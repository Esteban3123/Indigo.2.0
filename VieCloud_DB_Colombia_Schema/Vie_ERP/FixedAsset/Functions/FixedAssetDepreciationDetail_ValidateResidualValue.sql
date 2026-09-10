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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de validación contable que verifica la consistencia del valor residual en el detalle de depreciación de activos fijos. Recorre los registros de depreciación con estado aprobado/procesado (Status = 2) y comprueba que el valor residual de cada línea de detalle sea igual al valor residual anterior menos el valor depreciado en ese período, ajustado por diferencias en el valor de transacción. Si todos los valores residuales son coherentes con el histórico de depreciaciones del activo físico y el libro legal correspondiente, retorna 1 (válido); de lo contrario retorna 0. Se usa como control de integridad antes de cerrar o aprobar procesos de depreciación de activos fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'FUNCTION', @level1name = N'FixedAssetDepreciationDetail_ValidateResidualValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'FUNCTION', @level1name = N'FixedAssetDepreciationDetail_ValidateResidualValue';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida que el valor residual de cada detalle de depreciación de activos fijos sea coherente con el valor residual del período anterior menos la depreciación, ajustado por cambios en el valor de transacción.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'FixedAssetDepreciationDetail_ValidateResidualValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros en FixedAssetDepreciation y FixedAssetDepreciationDetail relacionados.; Los registros considerados deben tener Status=2 en el encabezado de depreciación.; Los detalles deben estar asociados a un activo físico (FixedAssetPhysicalAssetId) y a un libro contable (LegalBookId).', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'FixedAssetDepreciationDetail_ValidateResidualValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor residual de cada detalle debe ser igual al valor residual del detalle inmediatamente anterior (mismo activo físico y mismo libro contable) menos el valor depreciado, ajustado por la diferencia del valor de transacción.; Solo se validan registros cuyo encabezado de depreciación tiene Status=2.; Para el primer detalle de un activo/libro (sin antecedente), el valor residual se considera válido por sí mismo (ISNULL toma el propio ResidualValue).; La trazabilidad del valor residual se compara contra el detalle previo según orden descendente de Id.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'FixedAssetDepreciationDetail_ValidateResidualValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Depreciación de activos fijos; Valor residual; Valor de depreciación; Libro contable (LegalBook); Activo físico; Valor de transacción', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'FixedAssetDepreciationDetail_ValidateResidualValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Cuando no existe ningún detalle con Status=2 cuyo ResidualValue difiera del cálculo esperado (residual anterior - depreciación + delta de TransactionValue), retorna 1; en caso contrario retorna 0.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'FixedAssetDepreciationDetail_ValidateResidualValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe ningún detalle de depreciación con Status=2 cuyo ResidualValue difiera del valor calculado (ResidualValue anterior - DepreciationValue + diferencia de TransactionValue) → Retorna 1 (validación exitosa) else Retorna 0 (existe inconsistencia en el valor residual)', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'FixedAssetDepreciationDetail_ValidateResidualValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'FixedAsset.FixedAssetDepreciation; FixedAsset.FixedAssetDepreciationDetail', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'FixedAssetDepreciationDetail_ValidateResidualValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'FixedAssetDepreciationDetail_ValidateResidualValue';
GO
