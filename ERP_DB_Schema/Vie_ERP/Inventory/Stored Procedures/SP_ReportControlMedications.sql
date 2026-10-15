-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-02-25
-- Description:	Procedimiento para el reporte de medicamentos de control
-- =============================================
CREATE PROCEDURE [Inventory].[SP_ReportControlMedications]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE	@Year INT,
			@Month INT,
			@CalculateNewBalance BIT,
			---------------------------
			@YearPrevious INT,
			@MonthPrevious INT

	DECLARE @Table_ControlMedications AS TABLE
	(
		Id INT IDENTITY(1,1),
		ProductId INT,
		GenericName VARCHAR(200),
		TradeName VARCHAR(200),
		Concentration VARCHAR(50),
		PharmaceuticalForm VARCHAR(100),
		PreviousBalance INT,
		QuantityIn INT,
		PharmaceuticalLab VARCHAR(100),
		QuantityOut INT,
		FormulaNumbers INT,
		NewBalance INT,
		Type TINYINT
	)

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		SELECT	@Year = t.x.value('Year[1]','int'),
				@Month = t.x.value('Month[1]','int'),
				@CalculateNewBalance = t.x.value('CalculateNewBalance[1]','bit')
		FROM @xmlCriterias.nodes('/Data') t(x)

		SET @YearPrevious = IIF(@Month = 1, @Year - 1, @Year)
		SET @MonthPrevious = IIF(@Month = 1, 12, @Month - 1)

		/**********************************  OBTENCION DE DATOS **********************************/

		INSERT INTO @Table_ControlMedications
			SELECT	ip.Id ProductId,
					atc.Name GenericName,
					ip.Name TradeName,
					atc.Concentration,
					pf.Name PharmaceuticalForm,
					ISNULL(kd.PreviousBalance, 0) PreviousBalance,
					ISNULL(k.QuantityIn, 0) QuantityIn,
					m.Name PharmaceuticalLab,
					ISNULL(k.QuantityOut, 0) QuantityOut,
					ISNULL(k.FormulaNumbers, 0) FormulaNumbers,
					0 NewBalance,
					ISNULL(k.Type, 1) Type
			FROM Inventory.InventoryProduct ip
			LEFT JOIN Inventory.ATC atc ON ip.ATCId = atc.Id
			LEFT JOIN Inventory.PharmaceuticalForm pf ON atc.PharmaceuticalFormId = pf.Id
			LEFT JOIN 
			(	
				SELECT k.ProductId,SUM(k.Quantity * IIF(k.MovementType = 1, 1, -1)) PreviousBalance
				FROM Inventory.Kardex k WITH(NOLOCK)
				WHERE k.AffectInventory =1 
					AND(
					(YEAR(k.DocumentDate) = @Year AND MONTH(k.DocumentDate) < @Month)
					OR
					(YEAR(k.DocumentDate) < @Year))
				GROUP BY k.ProductId
			) kd ON ip.Id = kd.ProductId
			LEFT JOIN
			(
				SELECT	k.ProductId,
						CASE k.EntityName
							WHEN 'PharmaceuticalDispensingDevolution' THEN 2
							ELSE 1
						END Type,
						SUM(IIF(k.MovementType = 1, k.Quantity, 0)) QuantityIn,
						SUM(IIF(k.MovementType = 2, k.Quantity, 0)) QuantityOut,
						COUNT(DISTINCT IIF(k.EntityName = 'PharmaceuticalDispensing', k.EntityId,NULL)) FormulaNumbers
				FROM Inventory.Kardex k
				WHERE YEAR(k.DocumentDate) = @Year AND MONTH(k.DocumentDate) = @Month
				GROUP BY k.ProductId,
					CASE k.EntityName
						WHEN 'PharmaceuticalDispensingDevolution' THEN 2
						ELSE 1
					END
			) k ON ip.Id = k.ProductId
			LEFT JOIN Inventory.Manufacturer m ON ip.ManufacturerId = m.Id
			WHERE ip.ProductControl = 1
			ORDER BY atc.Id, ip.Id, ISNULL(k.Type, 1)

		/************************************* CALCULO SALDO *************************************/

		IF ISNULL(@CalculateNewBalance, 1) = 1
		BEGIN
			DECLARE @ProductRows INT = 1,
					@ProductId INT = 0,
					@PreviousBalance INT,
					@NewBalance INT,
					-----------------------
					@Rows INT,
					@RowId INT

			-- Recorremos los productos
			WHILE @ProductRows > 0
			BEGIN
				SELECT TOP 1
					@ProductId = ProductId,
					@PreviousBalance = PreviousBalance,
					@Rows = 1,
					@RowId = 0
				FROM @Table_ControlMedications
				WHERE ProductId > @ProductId
				ORDER BY ProductId

				SET @ProductRows = @@ROWCOUNT
				IF @ProductRows = 0 
				BEGIN
					BREAK
				END

				WHILE @Rows > 0
				BEGIN
					SELECT TOP 1
						@RowId = Id,
						@NewBalance = @PreviousBalance + QuantityIn - QuantityOut
					FROM @Table_ControlMedications
					WHERE ProductId = @ProductId
						AND Id > @RowId
					ORDER BY Id

					SET @Rows = @@ROWCOUNT
					IF @Rows = 0 
					BEGIN
						BREAK
					END

					UPDATE @Table_ControlMedications 
						SET PreviousBalance = @PreviousBalance,
							NewBalance = @NewBalance 
					WHERE Id = @RowId

					SET @PreviousBalance = @NewBalance
				END
			END
		END

		/**************************************  RESULTADOS **************************************/

		SELECT *, CASE Type
				WHEN 1 THEN ''
				WHEN 2 THEN 'DEVOLUCIÓN'
			END TypeName
		FROM @Table_ControlMedications
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END