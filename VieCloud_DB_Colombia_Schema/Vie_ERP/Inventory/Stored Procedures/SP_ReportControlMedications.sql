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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de medicamentos de control (sustancias controladas o de alto riesgo) para un mes y año específicos indicados como criterios de entrada en formato XML. Consolida para cada producto controlado su saldo anterior al período, las entradas, salidas y número de fórmulas o dispensaciones del mes, calculando además el saldo nuevo de forma acumulada por producto. Integra el catálogo de productos del inventario con la clasificación ATC (nombre genérico, concentración), la forma farmacéutica, el laboratorio fabricante y los movimientos del kardex, diferenciando los registros de dispensación normal de las devoluciones farmacéuticas. Se utiliza para el control regulatorio y seguimiento de existencias de medicamentos controlados en farmacia.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ReportControlMedications';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ReportControlMedications';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte mensual de medicamentos de control con saldo anterior, entradas, salidas, número de fórmulas y nuevo saldo calculado por producto.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportControlMedications';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener Year, Month y CalculateNewBalance bajo el nodo /Data.; Deben existir productos marcados como de control (ProductControl = 1) en el catálogo de inventario.; Los movimientos de Kardex deben tener DocumentDate, MovementType y AffectInventory poblados para ser considerados.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportControlMedications';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El saldo anterior solo agrega movimientos con AffectInventory = 1 y fecha estrictamente anterior al mes/año solicitado.; Las entradas y salidas mensuales se agrupan por producto y por tipo (normal vs. devolución).; Solo se reportan productos cuyo ProductControl = 1.; El nuevo saldo se calcula encadenado por filas del mismo producto, de modo que el PreviousBalance de cada fila es el NewBalance de la anterior.; Los errores no abortan la ejecución: se devuelven como resultset estandarizado con CodeResult=''999''.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportControlMedications';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Medicamentos de control; Kardex (movimientos de inventario); Saldo anterior y nuevo saldo; Entradas y salidas de inventario; Dispensación farmacéutica; Devolución de dispensación; Clasificación ATC; Forma farmacéutica; Laboratorio fabricante; Número de fórmulas', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportControlMedications';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @Table_ControlMedications: Inserta una fila por producto de control (ip.ProductControl = 1) con saldo anterior (movimientos previos al mes/año), entradas, salidas y número de fórmulas del mes solicitado.; [UPDATE] @Table_ControlMedications: Cuando @CalculateNewBalance = 1 (o NULL), recalcula PreviousBalance y NewBalance fila por fila por producto: NewBalance = PreviousBalance + QuantityIn - QuantityOut, propagando el saldo entre filas del mismo ProductId.; [RETURN_RESULT] (resultset): Devuelve el contenido de la tabla con una columna TypeName: ''DEVOLUCIÓN'' cuando Type=2 y cadena vacía cuando Type=1.; [RETURN_RESULT] (resultset): Ante cualquier error capturado, retorna un resultset con CodeResult=''999'' y MessageResult con el mensaje y línea del error.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportControlMedications';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Month = 1 → El periodo previo se ajusta a diciembre del año anterior (@YearPrevious = @Year-1, @MonthPrevious = 12). else El periodo previo es el mes anterior del mismo año.; si k.MovementType = 1 en Kardex → Se considera entrada: suma a PreviousBalance y a QuantityIn. else Se considera salida: resta a PreviousBalance y suma a QuantityOut.; si k.EntityName = ''PharmaceuticalDispensingDevolution'' → El registro se clasifica con Type=2 (devolución). else Se clasifica con Type=1 (movimiento normal).; si k.EntityName = ''PharmaceuticalDispensing'' → El EntityId se cuenta como fórmula distinta en FormulaNumbers. else No se cuenta como fórmula.; si ISNULL(@CalculateNewBalance,1) = 1 → Ejecuta el recálculo iterativo del nuevo saldo por producto. else Omite el cálculo y deja NewBalance en 0.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportControlMedications';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventoryProduct; Inventory.ATC; Inventory.PharmaceuticalForm; Inventory.Kardex; Inventory.Manufacturer', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportControlMedications';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportControlMedications';
-- GO
