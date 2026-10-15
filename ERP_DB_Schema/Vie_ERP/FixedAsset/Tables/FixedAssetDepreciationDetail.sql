CREATE TABLE [FixedAsset].[FixedAssetDepreciationDetail] (
    [Id]                                       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetDepreciationId]                 INT             NOT NULL,
    [LegalBookId]                              INT             NOT NULL,
    [ActiveClass]                              TINYINT         NOT NULL,
    [FixedAssetPhysicalAssetId]                INT             NULL,
    [FixedAssetPhysicalAssetDetailBookId]      INT             NULL,
    [FixedAssetPhysicalAssetPartsId]           INT             NULL,
    [FixedAssetPhysicalAssetPartsDetailBookId] INT             NULL,
    [DepreciationValue]                        DECIMAL (18, 2) NOT NULL,
    [LifeTime]                                 INT             NOT NULL,
    [RemainingLifeTime]                        INT             NOT NULL,
    [DepreciatedDays]                          INT             NOT NULL,
    [ValorizationValue]                        DECIMAL (18, 2) NOT NULL,
    [DevaluationValue]                         DECIMAL (18, 2) NOT NULL,
    [AdjustedValue]                            DECIMAL (18, 2) NOT NULL,
    [TransactionValue]                         DECIMAL (18, 2) CONSTRAINT [DF_FixedAssetDepreciationDetail_TransactionValue] DEFAULT ((0)) NOT NULL,
    [AccumulatedDepreciation]                  DECIMAL (18, 2) NOT NULL,
    [ResidualValue]                            DECIMAL (18, 2) NOT NULL,
    [FinantialDiscount]                        DECIMAL (18, 2) CONSTRAINT [DF__FixedAsse__Finan__6BB6EBCD] DEFAULT ((0)) NOT NULL,
    [FinantialDiscountAdjusment]               DECIMAL (18, 2) CONSTRAINT [DF__FixedAsse__Finan__6CAB1006] DEFAULT ((0)) NOT NULL,
    [FinantialDiscountDepreciation]            DECIMAL (18, 2) CONSTRAINT [DF__FixedAsse__Finan__7263E95C] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_FixedAssetDepreciationDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetDepreciationDetail_FixedAssetDepreciation] FOREIGN KEY ([FixedAssetDepreciationId]) REFERENCES [FixedAsset].[FixedAssetDepreciation] ([Id]),
    CONSTRAINT [FK_FixedAssetDepreciationDetail_LegalBook] FOREIGN KEY ([LegalBookId]) REFERENCES [GeneralLedger].[LegalBook] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetDepreciationDetail] NOCHECK CONSTRAINT [FK_FixedAssetDepreciationDetail_FixedAssetDepreciation];


GO
ALTER TABLE [FixedAsset].[FixedAssetDepreciationDetail] NOCHECK CONSTRAINT [FK_FixedAssetDepreciationDetail_LegalBook];


GO
CREATE NONCLUSTERED INDEX [IX_FixedAssetDepreciationDetail__FixedAssetDepreciationId__INC__LegalBookId]
    ON [FixedAsset].[FixedAssetDepreciationDetail]([FixedAssetDepreciationId] ASC)
    INCLUDE([LegalBookId]);


GO
CREATE NONCLUSTERED INDEX [IX_FixedAssetDepreciationDetail__FixedAssetDepreciationId__LegalBookId__INC__FixedAssetPhysicalAssetId__Id]
    ON [FixedAsset].[FixedAssetDepreciationDetail]([FixedAssetDepreciationId] ASC, [LegalBookId] ASC)
    INCLUDE([FixedAssetPhysicalAssetId], [Id]);


GO
-- =============================================
-- Author:		Miguel Angel Fonseca
-- Create date: 2018-05-21
-- Description:	Se valida que el valor residual del registro concuerde con el valor residual del registro anterior, luego de haberse realizado las respectivas operaciones
-- =============================================
CREATE TRIGGER [FixedAsset].[tgg_ValidateResidualValueInDepreciation]
   ON  [FixedAsset].[FixedAssetDepreciationDetail] 
   AFTER INSERT, UPDATE, DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @count INT = 0
	DECLARE @errorSource VARCHAR(20) = ''
	DECLARE @errorDetail NVARCHAR(MAX) = ''
    
	IF EXISTS (SELECT * FROM INSERTED) -- INSERT OR UPDATE
	BEGIN		
		--Se valida que el valor residual corresponda al valor residual anterior menor el valor de la depreciacion mas el valor de la transaccion del periodo
		SELECT @count = fadd.FixedAssetPhysicalAssetDetailBookId
		FROM FixedAsset.FixedAssetDepreciation fad
		JOIN INSERTED fadd ON fad.Id = fadd.FixedAssetDepreciationId
		JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON fapa.Id = fadd.FixedAssetPhysicalAssetId
		CROSS APPLY (
			SELECT 
				TOP 1 faddp.ResidualValue, faddp.DepreciationValue, faddp.FinantialDiscountAdjusment, faddp.TransactionValue
			FROM FixedAsset.FixedAssetDepreciation fadp
			JOIN FixedAsset.FixedAssetDepreciationDetail faddp ON fadp.Id = faddp.FixedAssetDepreciationId
			WHERE fadp.Status = 2 
				AND faddp.FixedAssetPhysicalAssetId = fadd.FixedAssetPhysicalAssetId
				AND faddp.LegalBookId = fadd.LegalBookId 
				AND 
				(
					(fadp.ClosingYear = fad.ClosingYear AND fadp.ClosingMonth < fad.ClosingMonth)
					OR
					(fadp.ClosingYear < fad.ClosingYear)
				)
			ORDER BY fadp.ClosingYear DESC, fadp.ClosingMonth DESC
		) faddp
		WHERE (
			(faddp.ResidualValue - faddp.DepreciationValue + faddp.FinantialDiscountAdjusment) 
			<> 
			(fadd.ResidualValue - (fadd.TransactionValue - faddp.TransactionValue))
		)
		AND (
			(faddp.ResidualValue - faddp.DepreciationValue + faddp.FinantialDiscountAdjusment - ISNULL(fapa.FinancialDiscount, 0)) 
			<> 
			(fadd.ResidualValue - (fadd.TransactionValue - faddp.TransactionValue))
		)

		IF @count > 0
			SET @errorSource = 'INSERT_UPDATE'

		IF @count = 0 AND EXISTS (SELECT * FROM DELETED) -- UPDATE
		BEGIN
			-- Si es una actualización verifico registros posteriores
			SELECT @count = faddp.FixedAssetPhysicalAssetDetailBookId
			FROM FixedAsset.FixedAssetDepreciation fadp
			JOIN INSERTED faddp ON fadp.Id = faddp.FixedAssetDepreciationId
			JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON fapa.Id = faddp.FixedAssetPhysicalAssetId
			CROSS APPLY (
				SELECT 
				TOP 1 fadd.ResidualValue, fadd.TransactionValue
			FROM FixedAsset.FixedAssetDepreciation fad
			JOIN FixedAsset.FixedAssetDepreciationDetail fadd ON fad.Id = fadd.FixedAssetDepreciationId
			WHERE fad.Status = 2 
				AND faddp.FixedAssetPhysicalAssetId = fadd.FixedAssetPhysicalAssetId
				AND faddp.LegalBookId = fadd.LegalBookId 
				AND 
				(
					(fadp.ClosingYear = fad.ClosingYear AND fadp.ClosingMonth < fad.ClosingMonth)
					OR
					(fadp.ClosingYear < fad.ClosingYear)
				)
			ORDER BY fad.ClosingYear, fad.ClosingMonth
		) fadd
		WHERE (
			(faddp.ResidualValue - faddp.DepreciationValue + faddp.FinantialDiscountAdjusment) 
			<> 
			(fadd.ResidualValue - (fadd.TransactionValue - faddp.TransactionValue))
		)
		AND (
			(faddp.ResidualValue - faddp.DepreciationValue + faddp.FinantialDiscountAdjusment - ISNULL(fapa.FinancialDiscount, 0)) 
			<> 
			(fadd.ResidualValue - (fadd.TransactionValue - faddp.TransactionValue))
		)

			IF @count > 0
				SET @errorSource = 'UPDATE_NEXT'
		END

	END
	ELSE IF EXISTS (SELECT * FROM DELETED) -- DELETE
	BEGIN 		
		--Se valida que el valor residual corresponda al valor residual anterior menor el valor de la depreciacion mas el valor de la transaccion del periodo
		--Comparando el anterior mes con el mes siguiente de los que quedan sin eliminar		
		SELECT @count = COUNT(fad.Id)
		FROM FixedAsset.FixedAssetDepreciation fad
		JOIN DELETED fadd ON fad.Id = fadd.FixedAssetDepreciationId
		WHERE EXISTS
			(
				SELECT 1
				FROM FixedAsset.FixedAssetDepreciation fad2
				JOIN FixedAsset.FixedAssetDepreciationDetail fadd2 ON fad2.Id = fadd2.FixedAssetDepreciationId
				WHERE fad2.Status = 2 
					AND fadd2.FixedAssetPhysicalAssetId = fadd.FixedAssetPhysicalAssetId
					AND fadd2.LegalBookId = fadd.LegalBookId 
					AND fad2.ClosingYear >= fad.ClosingYear 
					AND fad2.ClosingMonth >= fad.ClosingMonth
					AND fadd2.Id <> fadd.Id
			)			
			AND ISNULL
			(
				(
					SELECT 
						TOP 1 fadd2.ResidualValue - fadd2.DepreciationValue + fadd2.FinantialDiscountAdjusment + (fadd.TransactionValue - fadd2.TransactionValue)
					FROM FixedAsset.FixedAssetDepreciation fad2
					JOIN FixedAsset.FixedAssetDepreciationDetail fadd2 ON fad2.Id = fadd2.FixedAssetDepreciationId
					WHERE fad2.Status = 2 
						AND fadd2.FixedAssetPhysicalAssetId = fadd.FixedAssetPhysicalAssetId
						AND fadd2.LegalBookId = fadd.LegalBookId 
						AND fad2.ClosingYear <= fad.ClosingYear 
						AND fad2.ClosingMonth <= fad.ClosingMonth
						AND fadd2.Id <> fadd.Id
					ORDER BY fad2.ClosingYear DESC, fad2.ClosingMonth DESC
				), 
				fadd.ResidualValue
			)
			<> 
			ISNULL
			(
				(
					SELECT 
						TOP 1 fadd2.ResidualValue - fadd2.DepreciationValue + fadd2.FinantialDiscountAdjusment + (fadd.TransactionValue - fadd2.TransactionValue)
					FROM FixedAsset.FixedAssetDepreciation fad2
					JOIN FixedAsset.FixedAssetDepreciationDetail fadd2 ON fad2.Id = fadd2.FixedAssetDepreciationId
					WHERE fad2.Status = 2 
						AND fadd2.FixedAssetPhysicalAssetId = fadd.FixedAssetPhysicalAssetId
						AND fadd2.LegalBookId = fadd.LegalBookId 
						AND fad2.ClosingYear >= fad.ClosingYear 
						AND fad2.ClosingMonth >= fad.ClosingMonth
						AND fadd2.Id <> fadd.Id
					ORDER BY fad2.ClosingYear, fad2.ClosingMonth
				), 
				fadd.ResidualValue
			)

		IF @count > 0
			SET @errorSource = 'DELETE'
	END

	IF @count > 0 
	BEGIN
		IF @errorSource = 'INSERT_UPDATE'
		BEGIN
			SELECT TOP 1 @errorDetail = CONCAT(
				'[CHECK: INSERT/UPDATE vs Periodo Anterior]',
				' Placa=', fapa.Plate,
				' PhysicalAssetId=', fadd.FixedAssetPhysicalAssetId,
				' BookId=', fadd.FixedAssetPhysicalAssetDetailBookId,
				' LegalBookId=', fadd.LegalBookId,
				' | PREV: Residual=', faddp.ResidualValue,
				' Dep=', faddp.DepreciationValue,
				' Adj=', faddp.FinantialDiscountAdjusment,
				' Trans=', faddp.TransactionValue,
				' | CURR: Residual=', fadd.ResidualValue,
				' Trans=', fadd.TransactionValue,
				' FD_Detail=', fadd.FinantialDiscount,
				' | ASSET: FD=', ISNULL(fapa.FinancialDiscount, 0),
				' NetHistVal=', fapa.NetHistoricalValue,
				' | CALC: Esperado=', CAST((faddp.ResidualValue - faddp.DepreciationValue + faddp.FinantialDiscountAdjusment) AS VARCHAR(30)),
				' Esperado-FD=', CAST((faddp.ResidualValue - faddp.DepreciationValue + faddp.FinantialDiscountAdjusment - ISNULL(fapa.FinancialDiscount, 0)) AS VARCHAR(30)),
				' Actual=', CAST((fadd.ResidualValue - (fadd.TransactionValue - faddp.TransactionValue)) AS VARCHAR(30)),
				' Diff=', CAST((faddp.ResidualValue - faddp.DepreciationValue + faddp.FinantialDiscountAdjusment) - (fadd.ResidualValue - (fadd.TransactionValue - faddp.TransactionValue)) AS VARCHAR(30)),
				' Diff-FD=', CAST((faddp.ResidualValue - faddp.DepreciationValue + faddp.FinantialDiscountAdjusment - ISNULL(fapa.FinancialDiscount, 0)) - (fadd.ResidualValue - (fadd.TransactionValue - faddp.TransactionValue)) AS VARCHAR(30))
			)
			FROM FixedAsset.FixedAssetDepreciation fad
			JOIN INSERTED fadd ON fad.Id = fadd.FixedAssetDepreciationId
			JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON fapa.Id = fadd.FixedAssetPhysicalAssetId
			CROSS APPLY (
				SELECT 
					TOP 1 faddp.ResidualValue, faddp.DepreciationValue, faddp.FinantialDiscountAdjusment, faddp.TransactionValue
				FROM FixedAsset.FixedAssetDepreciation fadp
				JOIN FixedAsset.FixedAssetDepreciationDetail faddp ON fadp.Id = faddp.FixedAssetDepreciationId
				WHERE fadp.Status = 2 
					AND faddp.FixedAssetPhysicalAssetId = fadd.FixedAssetPhysicalAssetId
					AND faddp.LegalBookId = fadd.LegalBookId 
					AND 
					(
						(fadp.ClosingYear = fad.ClosingYear AND fadp.ClosingMonth < fad.ClosingMonth)
						OR
						(fadp.ClosingYear < fad.ClosingYear)
					)
				ORDER BY fadp.ClosingYear DESC, fadp.ClosingMonth DESC
			) faddp
			WHERE (
				(faddp.ResidualValue - faddp.DepreciationValue + faddp.FinantialDiscountAdjusment) 
				<> 
				(fadd.ResidualValue - (fadd.TransactionValue - faddp.TransactionValue))
			)
			AND (
				(faddp.ResidualValue - faddp.DepreciationValue + faddp.FinantialDiscountAdjusment - ISNULL(fapa.FinancialDiscount, 0)) 
				<> 
				(fadd.ResidualValue - (fadd.TransactionValue - faddp.TransactionValue))
			)
		END
		ELSE IF @errorSource = 'UPDATE_NEXT'
		BEGIN
			SELECT TOP 1 @errorDetail = CONCAT(
				'[CHECK: UPDATE vs Periodo Posterior]',
				' Placa=', fapa.Plate,
				' PhysicalAssetId=', faddp.FixedAssetPhysicalAssetId,
				' BookId=', faddp.FixedAssetPhysicalAssetDetailBookId,
				' LegalBookId=', faddp.LegalBookId,
				' | UPDATED: Residual=', faddp.ResidualValue,
				' Dep=', faddp.DepreciationValue,
				' Adj=', faddp.FinantialDiscountAdjusment,
				' Trans=', faddp.TransactionValue,
				' FD_Detail=', faddp.FinantialDiscount,
				' | NEXT: Residual=', fadd.ResidualValue,
				' Trans=', fadd.TransactionValue,
				' | ASSET: FD=', ISNULL(fapa.FinancialDiscount, 0),
				' | CALC: Esperado=', CAST((faddp.ResidualValue - faddp.DepreciationValue + faddp.FinantialDiscountAdjusment) AS VARCHAR(30)),
				' Esperado-FD=', CAST((faddp.ResidualValue - faddp.DepreciationValue + faddp.FinantialDiscountAdjusment - ISNULL(fapa.FinancialDiscount, 0)) AS VARCHAR(30)),
				' Actual=', CAST((fadd.ResidualValue - (fadd.TransactionValue - faddp.TransactionValue)) AS VARCHAR(30)),
				' Diff=', CAST((faddp.ResidualValue - faddp.DepreciationValue + faddp.FinantialDiscountAdjusment) - (fadd.ResidualValue - (fadd.TransactionValue - faddp.TransactionValue)) AS VARCHAR(30)),
				' Diff-FD=', CAST((faddp.ResidualValue - faddp.DepreciationValue + faddp.FinantialDiscountAdjusment - ISNULL(fapa.FinancialDiscount, 0)) - (fadd.ResidualValue - (fadd.TransactionValue - faddp.TransactionValue)) AS VARCHAR(30))
			)
			FROM FixedAsset.FixedAssetDepreciation fadp
			JOIN INSERTED faddp ON fadp.Id = faddp.FixedAssetDepreciationId
			JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON fapa.Id = faddp.FixedAssetPhysicalAssetId
			CROSS APPLY (
				SELECT 
				TOP 1 fadd.ResidualValue, fadd.TransactionValue
			FROM FixedAsset.FixedAssetDepreciation fad
			JOIN FixedAsset.FixedAssetDepreciationDetail fadd ON fad.Id = fadd.FixedAssetDepreciationId
			WHERE fad.Status = 2 
				AND faddp.FixedAssetPhysicalAssetId = fadd.FixedAssetPhysicalAssetId
				AND faddp.LegalBookId = fadd.LegalBookId 
				AND 
				(
					(fadp.ClosingYear = fad.ClosingYear AND fadp.ClosingMonth < fad.ClosingMonth)
					OR
					(fadp.ClosingYear < fad.ClosingYear)
				)
			ORDER BY fad.ClosingYear, fad.ClosingMonth
			) fadd
			WHERE (
				(faddp.ResidualValue - faddp.DepreciationValue + faddp.FinantialDiscountAdjusment) 
				<> 
				(fadd.ResidualValue - (fadd.TransactionValue - faddp.TransactionValue))
			)
			AND (
				(faddp.ResidualValue - faddp.DepreciationValue + faddp.FinantialDiscountAdjusment - ISNULL(fapa.FinancialDiscount, 0)) 
				<> 
				(fadd.ResidualValue - (fadd.TransactionValue - faddp.TransactionValue))
			)
		END
		ELSE IF @errorSource = 'DELETE'
		BEGIN
			SET @errorDetail = '[CHECK: DELETE] Se intenta eliminar un registro que romperia la cadena de residuales'
		END

		DECLARE @msg NVARCHAR(MAX) = CONCAT(
			'Error generado por control desde trigger. El valor residual de la depreciación debe coincidir con el valor residual del mes anterior. ',
			@errorDetail
		)
		;THROW 51000, @msg, 1
	END
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Depreciación de descuento Financiero', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'FinantialDiscountDepreciation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ajuste del Descuento Financiero', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'FinantialDiscountAdjusment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descuento Financiero', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'FinantialDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor que falta por depreciar o el valor en libros que tenia antes de efectuar el cierre  ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'ResidualValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor acumulado de las depreciaciones antes de efectuar el cierre', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'AccumulatedDepreciation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor de las transacciones, es decir las valorizaciones que afectaron el valor en libros', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'TransactionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor ajustado del activo al momento de realizar el cierre', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'AdjustedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica todas la desvalorizaciones que se le han realizado al activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'DevaluationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica todas las valorizaciones que se le han realizado al activo en el libro correspondiente', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'ValorizationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cantidad de dias depreciados', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'DepreciatedDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vida util restante despues de efectuar el cierre', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'RemainingLifeTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la vida util al efectuar el cierre', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'LifeTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la depreciacion del mes', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'DepreciationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id del detalle contable de la parte    Este campo solo se llena si la Clase del Activo es 2', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetPartsDetailBookId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id de la parte del activo    Este campo solo se llena si la Clase del Activo es 2', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetPartsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle contable del activo    Este campo solo se llena si la clase del activo es 1', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetDetailBookId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del activo dijo    Este campo solo se llena si la Clase del Activo es 1', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo del activo que se esta depreciando  1 - Activo Principal  2 - Parte del Activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'ActiveClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de libro legal', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'LegalBookId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de amortización de activos fijos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetDepreciationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'Id';

