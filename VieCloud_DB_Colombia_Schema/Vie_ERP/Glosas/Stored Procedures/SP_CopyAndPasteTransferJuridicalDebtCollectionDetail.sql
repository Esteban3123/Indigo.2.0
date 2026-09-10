-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-04-05
-- Description:	Procedimiento que se encarga del Copy & Paste de los detalles de traslados a cobro jurídico
-- =============================================
CREATE PROCEDURE [Glosas].[SP_CopyAndPasteTransferJuridicalDebtCollectionDetail] 
	@XmlParameter AS XML,
	@XmlObject AS XML
AS
BEGIN
	SET NOCOUNT ON

	/************************************* VARIABLES ************************************/
	
	--Parametros
	DECLARE @OperatingUnitId INT,
			@CustomerId INT,
			@TransferJuridicalDebtCollectionCId INT,
			@Type INT, /* 1. Copy & Paste - 2. Import - 3. SearchLookupEdit */
			---------------------------
			@UnReconciledInvoice BIT = 0,
			@CustomerNit VARCHAR(20)

	--Tabla para almacenar los items del listado que viene en el xml y los resultados a devolver
	DECLARE @TableXmlObject TABLE
	(
		RowIndex INT,
		RowColumns INT,
		--------------------------------
		PortfolioGlosaId INT,
		AccountReceivableId INT,
		LegalTransferValue MONEY,
		InvoiceNumber VARCHAR(50),
		AccountReceivableDate DATETIME,
		--------------------------------
		StatusField INT DEFAULT(99), 
		MessageField VARCHAR(MAX)
	)

	BEGIN TRY

		SELECT	@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
				@CustomerId = t.x.value('CustomerId[1]','int'),
				@TransferJuridicalDebtCollectionCId = t.x.value('TransferJuridicalDebtCollectionCId[1]','int'),
				@Type = t.x.value('Type[1]','int')
		FROM @XmlParameter.nodes('/Data/Row') t(x)

		INSERT INTO @TableXmlObject
			(RowIndex, RowColumns, InvoiceNumber)
			SELECT	t.x.value('RowIndex[1]','int') as RowIndex,
					t.x.value('RowColumns[1]','int') as RowColumns,
					t.x.value('InvoiceNumber[1]','varchar(500)') as InvoiceNumber
			FROM @XmlObject.nodes('/Data/Row') t(x)

		--------------------------------  OBTENGO DATOS NECESARIOS --------------------------------

		-- Se obtiene parametro que indica si se puede trasladar facturas con glosas
		SELECT @UnReconciledInvoice = sp.UnReconciledInvoice
		FROM Portfolio.SettingPortfolio sp
		WHERE sp.OperatingUnitId = @OperatingUnitId

		-- Se obtiene parametro que indica si se puede trasladar facturas con glosas
		SELECT @CustomerNit = c.Nit
		FROM Common.Customer c
		WHERE c.Id = @CustomerId

		--------------------------------------  VALIDACIONES --------------------------------------

		-- Validar el numero de columnas de acuerdo al tipo de documento
		UPDATE tx
			SET tx.StatusField = 999,
				tx.MessageField = CONCAT('El registro ', tx.RowIndex, ' no tiene la estructura válida')
		FROM @TableXmlObject tx
		WHERE NOT (tx.RowColumns >= 1)

		-- Validar que no existan registros duplicados
		UPDATE tx
			SET tx.StatusField = 999,
				tx.MessageField = CONCAT('El registro ', tx.RowIndex, ' se encuentra duplicado')
		FROM @TableXmlObject tx
		JOIN
		(
			SELECT tx.InvoiceNumber
			FROM @TableXmlObject tx
			WHERE tx.StatusField = 99
			GROUP BY tx.InvoiceNumber
			HAVING COUNT(1) > 1
		) txd ON ISNULL(tx.InvoiceNumber, '') = ISNULL(txd.InvoiceNumber, '')
		WHERE tx.StatusField = 99

		-- Validar la factura ingresada
		UPDATE tx
			SET tx.StatusField = 999,
				tx.MessageField = IIF
					(
						ar.Id IS NULL, 
						CONCAT(IIF(@Type IN (3, 4), CONCAT('La factura ', tx.InvoiceNumber), CONCAT('La factura del registro ', tx.RowIndex)), ' no existe o no esta asignada al tercero seleccionado'), 
						IIF
						(
							NOT (ar.Balance > 0),
							CONCAT(IIF(@Type IN (3, 4), CONCAT('La factura ', tx.InvoiceNumber), CONCAT('La factura del registro ', tx.RowIndex)), ' no tiene saldo'),
							CONCAT(IIF(@Type IN (3, 4), CONCAT('La factura ', tx.InvoiceNumber), CONCAT('La factura del registro ', tx.RowIndex)), ' se encuentra ', CASE ar.PortfolioStatus 
																								WHEN 1 THEN 'sin radicar'
																								WHEN 2 THEN 'radicada sin confirmar'
																								WHEN 4 THEN 'radicada sin confirmar'
																								WHEN 16 THEN 'trasladada a cobro jurídico'
																							  END)
						)
					)
		FROM @TableXmlObject tx
		LEFT JOIN Portfolio.AccountReceivable ar ON @CustomerId = ar.CustomerId AND tx.InvoiceNumber = ar.InvoiceNumber AND ar.Balance > 0
		WHERE tx.StatusField = 99 
			AND 
			(
				ar.Id IS NULL
				OR ar.PortfolioStatus IN (1, 2, 4, 16)
			)

		-- Validar las facturas glosadas
		UPDATE tx
			SET tx.StatusField = 999,
				tx.MessageField = CONCAT(IIF(@Type IN (3, 4), CONCAT('La factura ', tx.InvoiceNumber), CONCAT('La factura del registro ', tx.RowIndex)), ' se encuentra ', CASE gpg.State
																										WHEN 1 THEN 'pendiente de confirmar glosa'																										
																										WHEN 4 THEN 'pendiente de confirmar reiteración'
																										WHEN 7 THEN 'pendiente confirmar conciliacón'
																										WHEN 8 THEN 'conciliada'
																										WHEN 9 THEN 'en conciliación parcial'
																										WHEN 13 THEN 'pendiente confirmar pago parcial'
																									END)
		FROM @TableXmlObject tx
		JOIN Glosas.GlosaPortfolioGlosada gpg ON tx.InvoiceNumber = gpg.InvoiceNumber
		WHERE tx.StatusField = 99 
			AND 
			(
				(@UnReconciledInvoice = 0 AND gpg.State IN (1, 4, 7, 8, 9, 13))
				OR
				(@UnReconciledInvoice = 1 AND gpg.State IN (13))
			)

		-- Validar si la factura ya esta agregada en un traslado a cobro jurídico
		UPDATE tx
			SET tx.StatusField = 999,
				tx.MessageField = CONCAT(IIF(@Type IN (3, 4), CONCAT('La factura ', tx.InvoiceNumber), CONCAT('La factura del registro ', tx.RowIndex)), ' ya esta en el traslado a cobro jurídico: ', tjdc.JuridicalTransferConsecutive)
		FROM @TableXmlObject tx
		JOIN Glosas.TransferJuridicalDebtCollectionD tjdcd ON tx.InvoiceNumber = tjdcd.InvoiceNumber
		JOIN Glosas.TransferJuridicalDebtCollectionC tjdc ON tjdcd.TransferJuridicalDebtCollectionCId = tjdc.Id
		WHERE tx.StatusField = 99 AND
			(
				(
					(@Type <> 4 OR tjdc.Id <> @TransferJuridicalDebtCollectionCId)
					AND
					(tjdc.State IN (1, 2) OR (tjdc.State IN (4) AND tjdcd.ReversedUser IS NULL))
				)
			)

		------------------------------ ACTUALIZAR INFORMACIóN DEL REGISTRO ------------------------------

		UPDATE tx
			SET tx.StatusField = 0,
				tx.PortfolioGlosaId = gpg.Id,
				tx.AccountReceivableId = ar.Id,
				tx.LegalTransferValue = 0,
				tx.InvoiceNumber = ar.InvoiceNumber,
				tx.AccountReceivableDate = ar.AccountReceivableDate
		FROM @TableXmlObject tx
		JOIN Portfolio.AccountReceivable ar ON @CustomerId = ar.CustomerId AND tx.InvoiceNumber = ar.InvoiceNumber
		LEFT JOIN Glosas.GlosaPortfolioGlosada gpg 
			ON ar.InvoiceNumber = gpg.InvoiceNumber AND @CustomerNit = gpg.Nit
				AND
				(
					(@UnReconciledInvoice = 0 AND gpg.State IN (1, 4, 7, 8, 10, 13))
					OR
					(@UnReconciledInvoice = 1 AND gpg.State IN (10, 13))
				)
		WHERE tx.StatusField = 99

	END TRY
	BEGIN CATCH
		INSERT INTO @TableXmlObject(StatusField, MessageField)
		VALUES (999, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(5)))
	END CATCH

	SELECT	PortfolioGlosaId,
			AccountReceivableId,
			LegalTransferValue,
			InvoiceNumber,
			AccountReceivableDate,
			--------------------------------
			StatusField, 
			MessageField
	FROM @TableXmlObject
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que valida y registra el traslado de facturas a cobro jurídico dentro del módulo de Glosas y Cartera. Recibe por XML un listado de números de factura y los parámetros del encabezado del traslado (unidad operativa, cliente/tercero y tipo de operación), luego ejecuta una serie de validaciones de negocio: verifica que las facturas existan y estén asignadas al tercero correcto, que tengan saldo pendiente, que no se encuentren en estados de cartera incompatibles (sin radicar, radicada sin confirmar, ya trasladada), que no tengan glosas abiertas según la configuración de cartera (SettingPortfolio), y que no hayan sido incluidas previamente en otro traslado jurídico. Utiliza las tablas de Cuentas por Cobrar (AccountReceivable), Cartera Glosada (GlosaPortfolioGlosada) y la configuración de cartera (SettingPortfolio) para componer el resultado, devolviendo por fila si fue aceptada o rechazada con el mensaje de error correspondiente. Este procedimiento existe para garantizar la integridad del proceso de cobro jurídico, evitando traslados duplicados o inválidos de facturas que aún están en proceso de conciliación de glosas o con estados de radicación pendientes.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteTransferJuridicalDebtCollectionDetail';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteTransferJuridicalDebtCollectionDetail';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida y enriquece un listado de facturas (recibido por XML) que se intentan trasladar a cobro jurídico, verificando existencia, saldo, estado de cartera, glosas y duplicidades antes de permitir la operación.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteTransferJuridicalDebtCollectionDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de parámetros debe contener OperatingUnitId, CustomerId, TransferJuridicalDebtCollectionCId y Type; El XML de objeto debe traer al menos RowIndex, RowColumns e InvoiceNumber por fila; Debe existir configuración en Portfolio.SettingPortfolio para la unidad operativa para determinar si se permite trasladar facturas con glosa (UnReconciledInvoice); El cliente (CustomerId) debe existir en Common.Customer para obtener su NIT', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteTransferJuridicalDebtCollectionDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se aceptan facturas asignadas al cliente seleccionado y con saldo positivo; No se permiten facturas duplicadas dentro del mismo lote; Las facturas con PortfolioStatus 1, 2, 4 o 16 nunca son aprobadas para traslado; El parámetro UnReconciledInvoice de la unidad operativa controla qué estados de glosa son aceptables para trasladar a cobro jurídico; Una factura no puede estar simultáneamente en dos traslados a cobro jurídico activos (State 1, 2 o 4 sin reversar); El procedimiento nunca modifica tablas físicas; solo valida y devuelve resultados; LegalTransferValue se inicializa siempre en 0 para los registros válidos', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteTransferJuridicalDebtCollectionDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Cuenta por cobrar; Glosa; Cartera; Traslado a cobro jurídico; Conciliación de glosa; Reiteración de glosa; Pago parcial; Radicación de factura; NIT del tercero/cliente', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteTransferJuridicalDebtCollectionDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] @TableXmlObject: Devuelve el conjunto de filas con StatusField=0 cuando la factura es válida, o StatusField=999 con MessageField descriptivo cuando falla alguna validación; [UPDATE] @TableXmlObject: Si RowColumns < 1 marca el registro como estructura inválida (StatusField=999); [UPDATE] @TableXmlObject: Si el InvoiceNumber aparece más de una vez en el lote, marca los duplicados con StatusField=999; [UPDATE] @TableXmlObject: Si la factura no existe en Portfolio.AccountReceivable para el cliente, o no tiene Balance>0, o su PortfolioStatus está en (1,2,4,16), marca StatusField=999 con mensaje según el estado (sin radicar, radicada sin confirmar, trasladada a cobro jurídico); [UPDATE] @TableXmlObject: Si UnReconciledInvoice=0 y la glosa está en estado (1,4,7,8,9,13), o si UnReconciledInvoice=1 y la glosa está en estado 13, marca StatusField=999 indicando el estado de la glosa; [UPDATE] @TableXmlObject: Si la factura ya está en otro traslado a cobro jurídico con State en (1,2) o State=4 sin reversar, marca StatusField=999 (excepto cuando Type=4 y es el mismo TransferJuridicalDebtCollectionCId); [UPDATE] @TableXmlObject: Para los registros que pasaron todas las validaciones (StatusField=99) actualiza StatusField=0 y completa PortfolioGlosaId, AccountReceivableId, InvoiceNumber, AccountReceivableDate y LegalTransferValue=0; [INSERT] @TableXmlObject: Ante excepción captura ERROR_MESSAGE y ERROR_LINE en una fila con StatusField=999', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteTransferJuridicalDebtCollectionDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Type IN (3, 4) → Los mensajes de error referencian la factura por su InvoiceNumber else Los mensajes de error referencian la factura por el RowIndex del registro; si @UnReconciledInvoice = 0 → Rechaza facturas con glosas en estados (1,4,7,8,9,13) y al enriquecer enlaza glosas en estados (1,4,7,8,10,13) else Solo rechaza glosas en estado 13 y enlaza únicamente glosas en estados (10,13); si @Type = 4 y tjdc.Id = @TransferJuridicalDebtCollectionCId → No marca como error que la factura ya esté en ese mismo traslado (permite edición del mismo traslado) else Si la factura ya está en otro traslado activo, marca error', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteTransferJuridicalDebtCollectionDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.SettingPortfolio; Common.Customer; Portfolio.AccountReceivable; Glosas.GlosaPortfolioGlosada; Glosas.TransferJuridicalDebtCollectionD; Glosas.TransferJuridicalDebtCollectionC', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteTransferJuridicalDebtCollectionDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteTransferJuridicalDebtCollectionDetail';
-- GO
