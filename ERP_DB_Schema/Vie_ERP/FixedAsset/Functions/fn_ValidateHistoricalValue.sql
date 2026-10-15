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
