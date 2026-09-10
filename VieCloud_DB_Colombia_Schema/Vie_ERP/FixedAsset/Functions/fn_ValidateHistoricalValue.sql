-- =============================================
-- Author:		Andrés Steven Rojas
-- Create date: 2026-02-13
-- Description:	Función que valida la integridad del valor histórico en FixedAssetPhysicalAssetDetailBook
--              considerando el descuento financiero aplicado al activo físico
-- =============================================
CREATE FUNCTION [FixedAsset].[fn_ValidateHistoricalValue]
(
    @PhysicalAssetId INT,
    @HistoricalValue DECIMAL(18,2),
    @DepreciatedValue DECIMAL(18,2),
    @ResidualValue DECIMAL(18,2),
    @TransactionValue DECIMAL(18,2)
)
RETURNS BIT
AS
BEGIN
    DECLARE @FinancialDiscount DECIMAL(18,2)
    DECLARE @CalculatedValue DECIMAL(18,2)
    DECLARE @IsValid BIT
    
    -- Obtener el descuento financiero del activo físico
    SELECT @FinancialDiscount = ISNULL(FinancialDiscount, 0)
    FROM FixedAsset.FixedAssetPhysicalAsset
    WHERE Id = @PhysicalAssetId
    
    -- Calcular el valor esperado según la fórmula:
    -- HistoricalValue = (DepreciatedValue + ResidualValue) - TransactionValue + FinancialDiscount
    SET @CalculatedValue = (@DepreciatedValue + @ResidualValue) - @TransactionValue + @FinancialDiscount
    
    -- Validar con tolerancia mínima por redondeo (0.01)
    -- Esto permite pequeñas diferencias debido a cálculos con decimales
    IF ABS(@HistoricalValue - @CalculatedValue) <= 0.01
        SET @IsValid = 1
    ELSE
        SET @IsValid = 0
    
    RETURN @IsValid
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que valida la integridad del valor histórico de un activo fijo físico en el libro contable de detalle. Verifica que el valor histórico ingresado sea coherente con la fórmula: (valor depreciado + valor residual) - valor de transacción + descuento financiero del activo, consultando el descuento financiero registrado en la tabla de activos físicos. Permite una tolerancia de $0.01 para cubrir diferencias por redondeo en cálculos con decimales. Retorna 1 si el valor histórico es válido y 0 si existe inconsistencia, garantizando la integridad financiera del módulo de activos fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'FUNCTION', @level1name = N'fn_ValidateHistoricalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'FUNCTION', @level1name = N'fn_ValidateHistoricalValue';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida que el valor histórico de un activo físico coincida, dentro de una tolerancia de 0.01, con la suma de su valor depreciado y residual menos el valor de transacción más el descuento financiero registrado.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'fn_ValidateHistoricalValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en FixedAssetPhysicalAsset con el identificador del activo físico recibido; en caso contrario el descuento financiero quedará en 0 por ISNULL.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'fn_ValidateHistoricalValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El descuento financiero se asume 0 cuando es NULL en el registro del activo físico.; La fórmula de validación es: HistoricalValue ≈ (DepreciatedValue + ResidualValue) - TransactionValue + FinancialDiscount.; Se acepta una tolerancia de redondeo de 0.01 entre el valor histórico recibido y el calculado.; Siempre retorna un valor booleano (1=válido, 0=inválido) sin lanzar errores.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'fn_ValidateHistoricalValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'activo físico; valor histórico; valor depreciado; valor residual; valor de transacción; descuento financiero; depreciación', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'fn_ValidateHistoricalValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultado de función): Cuando ABS(@HistoricalValue - ((@DepreciatedValue + @ResidualValue) - @TransactionValue + FinancialDiscount)) <= 0.01 retorna 1; en caso contrario retorna 0.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'fn_ValidateHistoricalValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ABS(HistoricalValue - CalculatedValue) <= 0.01 → Retorna 1 (valor histórico válido) else Retorna 0 (valor histórico inválido)', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'fn_ValidateHistoricalValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'FixedAsset.FixedAssetPhysicalAsset', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'fn_ValidateHistoricalValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'fn_ValidateHistoricalValue';
GO
