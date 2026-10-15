CREATE TABLE [FixedAsset].[FixedAssetPhysicalAssetDetailBook] (
    [Id]                           INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PhysicalAssetId]              INT             NOT NULL,
    [LegalBookId]                  INT             NOT NULL,
    [LifeTime]                     INT             NOT NULL,
    [UnitLifeTime]                 TINYINT         NOT NULL,
    [ValorizationDays]             INT             CONSTRAINT [DF_FixedAssetPhysicalAssetDetailBook_ValorizationDays] DEFAULT ((0)) NOT NULL,
    [DaysPendingDepreciate]        INT             NOT NULL,
    [DepreciatedDays]              INT             NOT NULL,
    [DepreciationType]             TINYINT         CONSTRAINT [DF_FixedAssetPhysicalAssetDetailBook_DepreciationType] DEFAULT ((1)) NOT NULL,
    [TotalProductionUnit]          NUMERIC (18)    CONSTRAINT [DF_FixedAssetPhysicalAssetDetailBook_TotalProductionUnit] DEFAULT ((0)) NOT NULL,
    [PercentageRescue]             NUMERIC (5, 2)  CONSTRAINT [DF_FixedAssetPhysicalAssetDetailBook_PercentageRescue] DEFAULT ((0)) NOT NULL,
    [Valorization]                 DECIMAL (18, 2) NOT NULL,
    [Devaluation]                  DECIMAL (18, 2) NOT NULL,
    [AdjustedValue]                DECIMAL (18, 2) NOT NULL,
    [TransactionValue]             DECIMAL (18, 2) CONSTRAINT [DF_FixedAssetPhysicalAssetDetailBook_TransactionValue] DEFAULT ((0)) NOT NULL,
    [DepreciatedValue]             DECIMAL (18, 2) NOT NULL,
    [DepreciatedValuePart]         DECIMAL (18, 2) NOT NULL,
    [InflationAdjustmentValue]     DECIMAL (18, 2) CONSTRAINT [DF_FixedAssetPhysicalAssetDetailBook_InflationAdjustmentValue] DEFAULT ((0)) NOT NULL,
    [ResidualValue]                DECIMAL (18, 2) NOT NULL,
    [ResidualValuePart]            DECIMAL (18, 2) NOT NULL,
    [HistoricalValue]              DECIMAL (18, 2) CONSTRAINT [DF_FixedAssetPhysicalAssetDetailBook_HistoricalValue] DEFAULT ((0)) NOT NULL,
    [ApplyMinimunAmount]           BIT             CONSTRAINT [DF_FixedAssetPhysicalAssetDetailBook_ApplyMinimunAmount] DEFAULT ((0)) NOT NULL,
    [FinantialDiscountDepreciated] DECIMAL (18, 2) CONSTRAINT [DF__FixedAsse__Finan__73580D95] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_FixedAssetPhysicalAssetDetailBook__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_FixedAssetPhysicalAssetDetailBook_ValidateHistoricalValue] CHECK (FixedAsset.fn_ValidateHistoricalValue([PhysicalAssetId], [HistoricalValue], [DepreciatedValue], [ResidualValue], [TransactionValue]) = 1),
    CONSTRAINT [FK_FixedAssetPhysicalAssetDetailBook_FixedAssetPhysicalAsset] FOREIGN KEY ([PhysicalAssetId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAsset] ([Id]),
    CONSTRAINT [FK_FixedAssetPhysicalAssetDetailBook_LegalBook] FOREIGN KEY ([LegalBookId]) REFERENCES [GeneralLedger].[LegalBook] ([Id]),
    CONSTRAINT [CK_FixedAsset_ResidualValue_Minimum] CHECK ([DepreciationType] <> 3 OR [ResidualValue] >= ([HistoricalValue] * [PercentageRescue] / 100.0))
);


GO
ALTER TABLE [FixedAsset].[FixedAssetPhysicalAssetDetailBook] NOCHECK CONSTRAINT [CK_FixedAssetPhysicalAssetDetailBook_ValidateHistoricalValue];


GO
ALTER TABLE [FixedAsset].[FixedAssetPhysicalAssetDetailBook] NOCHECK CONSTRAINT [FK_FixedAssetPhysicalAssetDetailBook_FixedAssetPhysicalAsset];


GO
ALTER TABLE [FixedAsset].[FixedAssetPhysicalAssetDetailBook] NOCHECK CONSTRAINT [FK_FixedAssetPhysicalAssetDetailBook_LegalBook];


GO 
ALTER TABLE [FixedAsset].[FixedAssetPhysicalAssetDetailBook] NOCHECK CONSTRAINT [CK_FixedAsset_ResidualValue_Minimum]


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_FixedAssetPhysicalAssetDetailBook__PhysicalAssetId__LegalBookId]
    ON [FixedAsset].[FixedAssetPhysicalAssetDetailBook]([PhysicalAssetId] ASC, [LegalBookId] ASC);


GO
CREATE TRIGGER [FixedAsset].[tgg_DetectarEliminacion]
   ON  [FixedAsset].[FixedAssetPhysicalAssetDetailBook] 
   AFTER DELETE
AS 
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from<br>
    -- interfering with SELECT statements.<br>
    SET NOCOUNT ON;

        declare @count as integer = 0 

        /*select @count = count(*)  from FixedAsset.FixedAssetPhysicalAssetDetailBook d inner join FixedAsset.FixedAssetPhysicalAsset c on c.Id = d.PhysicalAssetId <br>
        where c.Depreciate = 1 and d.Id in (SELECT deleted.id FROM deleted)   */

        select @count = count(*)  
		from deleted d 
		inner join FixedAsset.FixedAssetPhysicalAsset c on c.Id = d.PhysicalAssetId 
        where c.Depreciate = 1 and (select count(*) from FixedAsset.FixedAssetDepreciationDetail where FixedAssetPhysicalAssetDetailBookId = d.Id) > 0

        --select @count<br>
    -- Insert statements for trigger here<br>

    if @count > 0 begin
            THROW 51000, 'Error generado por control de eliminacion desde trigger', 1
/*    end        else begin<br>
            select 'Error generado por control de eliminacion desde trigger. :) '<br>
            rollback */
    end 
END
GO
-- =============================================
-- Author:		Miguel Angel Fonseca
-- Create date: 2018-05-21
-- Description:	Se valida que el valor residual del registro concuerde con el valor residual del registro anterior, luego de haberse realizado las respectivas operaciones
-- =============================================
CREATE TRIGGER [FixedAsset].[tgg_ValidateValuesFromDepreciationAndInitialBalance]
   ON  [FixedAsset].[FixedAssetPhysicalAssetDetailBook]
   AFTER INSERT, UPDATE
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @plate          NVARCHAR(100)
	DECLARE @actualValue    DECIMAL(18, 2)
	DECLARE @expectedValue  DECIMAL(18, 2)
	DECLARE @difference     DECIMAL(18, 2)
	DECLARE @errorMessage   NVARCHAR(2048)

	--Se valida que el valor depreciado del detalle del activo debe ser igual a la sumatoria del valor de sus depreciaciones + la depreciacion del saldo inicial
	SELECT TOP 1
		@plate         = fapa.Plate,
		@actualValue   = fapadb.DepreciatedValue,
		@expectedValue = ISNULL(fad.DepreciationValue, 0) + ISNULL(faa.AmortizedValue, 0) + ISNULL(faib.DepreciatedValue, 0)
	FROM FixedAsset.FixedAssetPhysicalAsset fapa
	JOIN INSERTED fapadb ON fapa.Id = fapadb.PhysicalAssetId
	LEFT JOIN
	(
		SELECT faibidb.LegalBookId, faibi.Plate, faibidb.DepreciatedValue
		FROM FixedAsset.FixedAssetInitialBalance faib
		JOIN FixedAsset.FixedAssetInitialBalanceItem faibi ON faib.id = faibi.FixedAssetInitialBalanceId
		JOIN FixedAsset.FixedAssetInitialBalanceItemDetailBook faibidb ON faibi.id = faibidb.FixedAssetInitialBalanceItemId
		WHERE faib.Status = 2
	) faib ON fapa.Plate = faib.Plate AND fapadb.LegalBookId = faib.LegalBookId
	LEFT JOIN
	(
		SELECT fadd.LegalBookId, fadd.FixedAssetPhysicalAssetId, SUM(fadd.DepreciationValue - fadd.FinantialDiscountAdjusment) DepreciationValue
		FROM FixedAsset.FixedAssetDepreciation fad
		JOIN FixedAsset.FixedAssetDepreciationDetail fadd ON fad.Id = fadd.FixedAssetDepreciationId
		WHERE fad.Status = 2
		GROUP BY fadd.LegalBookId, fadd.FixedAssetPhysicalAssetId
	) fad ON fapa.Id = fad.FixedAssetPhysicalAssetId AND fapadb.LegalBookId = fad.LegalBookId
	LEFT JOIN
	(
		SELECT fadd.FixedAssetPhysicalAssetDetailBookId, SUM(fadd.AmortizedValue) AmortizedValue
		FROM FixedAsset.FixedAssetDepreciation fad
		JOIN FixedAsset.FixedAssetAmortizationDetail fadd ON fad.Id = fadd.FixedAssetDepreciationId
		WHERE fad.Status = 2
		GROUP BY fadd.FixedAssetPhysicalAssetDetailBookId
	) faa ON fapadb.Id = faa.FixedAssetPhysicalAssetDetailBookId
	WHERE fapa.Depreciate = 1
		AND fapadb.DepreciatedValue <> (ISNULL(fad.DepreciationValue, 0) + ISNULL(faa.AmortizedValue, 0) + ISNULL(faib.DepreciatedValue, 0))

	IF @plate IS NOT NULL
	BEGIN
		SET @difference    = @actualValue - @expectedValue
		SET @errorMessage  = 'Error generado por control desde trigger. El valor depreciado/amortizado del detalle del activo debe ser igual a la sumatoria del valor de sus depreciaciones/amortizaciones mas la depreciacion/amortizacion del saldo inicial.'
		                   + ' Activo: '     + ISNULL(@plate, 'N/A')
		                   + ' | Valor registrado: ' + CAST(@actualValue  AS NVARCHAR(50))
		                   + ' | Valor esperado: '   + CAST(@expectedValue AS NVARCHAR(50))
		                   + ' | Diferencia: '       + CAST(@difference    AS NVARCHAR(50))
		RAISERROR(@errorMessage, 16, 1)
	END
END
GO
CREATE TRIGGER [FixedAsset].[tgg_DetectarDiasEnCero]
   ON  [FixedAsset].[FixedAssetPhysicalAssetDetailBook] 
   AFTER UPDATE
AS 
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

declare @count as integer = 0

 
/* select @count = count(*)  from deleted d inner join FixedAsset.FixedAssetPhysicalAsset c on c.Id = d.PhysicalAssetId 
where c.Depreciate = 1 and d.Id = d.id */

 

--SELECT  @count = count(*)
--FROM INSERTED fpadb 
--inner join FixedAsset.FixedAssetPhysicalAsset fapa on fpadb.PhysicalAssetId=fapa.Id 
--where fapa.HasOutput = 0 and fpadb.ResidualValue > 0 and fpadb.DepreciatedDays = 0 

SELECT  @count = count(*)
FROM INSERTED i 
inner join deleted d on d.Id = i.Id
where i.DepreciatedDays < d.DepreciatedDays
    
if @count > 0 begin
	THROW 51000, 'Error generado por control de actualización desde trigger. No puede pasar que hayan DepreciatedDays en Cero cuando ResidualValue  todavia tenga saldo', 1
end 
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor depreciado del descuento financiero', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'FinantialDiscountDepreciated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el activo se deprecia como Activo Fijo de Mínima Cuantía de acuerdo con el  artículo 1.2.1.18.5 del Decreto 1625 de 2016 Único Reglamentario en Materia Tributaria', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'ApplyMinimunAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Valor Historico del activo por Libro', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'HistoricalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor en libros de las partes, es decir el valor que aun falta por depreciar', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'ResidualValuePart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor que falta por depreciar o el valor en libros    La formula para obtener el valor residual deberia ser   Valor En Libros = (Valor Historico + Transacciones - Depreciaciones + ValorAjustadoPorInflacion)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'ResidualValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Valor del Ajuste por Inflacion Acumulado', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'InflationAdjustmentValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor de las partes que se han depreciado', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciatedValuePart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor que se ha depreciado', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciatedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor de las transacciones, es decir las valorizaciones que afectaron el valor en libros', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'TransactionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor ajustado del activo, el cual debe se calcula y debe coincidir con la siguiente formula     Valor Hostorico + Valorizaciones - Desvalorizaciones', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'AdjustedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica todas la desvalorizaciones que se le han realizado al activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'Devaluation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica todas las valorizaciones que se le han realizado al activo en el libro correspondiente', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'Valorization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el porcentaje de salvamento. este solo se solicita si el Tipo de Depreciacion es de Reduccion de saldos, de lo contrario debe ir en cero', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'PercentageRescue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el total de unidades producidas del articulo, esto solo aplica para cuando el Metodo de Depreciacion es por Unidades Producidas', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'TotalProductionUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el metodo de depreacion del Articulo  1 - Linea Recta  2 - Suma de Digitos  3 - Reduccion de Saldos  4 - Unidades de Produccion', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica los dias que ya fueron depreciados', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciatedDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica los dias pendientes por depreciar    Nota: Este campo esta expresado en dias y cuando se registra el activo se registra la vida util con la que se ingreso y cuando se realice una transaccion que afecte el tiempo de vida util del activo este campo aumenta', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'DaysPendingDepreciate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica los dias que se ha valorizado el activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'ValorizationDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Año,   2 - Mes,   3 - Dia    se guarda como la haya digitado el usuario', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'UnitLifeTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vida Util expresada en dias, con el que se registra el activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'LifeTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Libro Contable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'LegalBookId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Activo fijo relacionado', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'Id';

