-- =============================================
-- Author:      <Author : Julieth Angélica Cárdenas Gómez>
-- Create Date: <Create : 03/11/2024 >
-- Description: <Description: SP que consume el XML para crear el comprobante contable desde Lista de costos - consignación>
-- =============================================
CREATE PROCEDURE [Inventory].[SP_GenerateJournalVoucherByConsignmentCostListDetail]
    @XMLConsignmentCostList XML,
	@CodeUser AS VARCHAR (20)

AS
BEGIN
   SET NOCOUNT ON;

	DECLARE @CodeMessageSP Int ,
			@MessageSP Varchar(Max) 
		/******************************** VARIABLES CONTABLES *******************************/

	-- Tabla temporal de los detalles modificados de la lista de costos 
	DECLARE @ConsignmentCostListDetail TABLE 
	(
	ConsignmentCostListId INT NOT NULL,
	ProductId INT NOT NULL,
	CostNew NUMERIC (12, 2) NULL,
	CostOld NUMERIC (12, 2) NULL
	)
		--Se declara una tabla con los datos para la cabecera del comprobante contable 
	DECLARE @JournalVourcherTmp TABLE 
	(
	    Id INT DEFAULT(0),
		Consecutive BIGINT DEFAULT(0),
		LegalBookId INT,
		IdJournalVoucher INT,
		VoucherDate VARCHAR(30),
		Imported VARCHAR(5) DEFAULT('False'),
		Status TINYINT,
		Detail VARCHAR(MAX),
		EntityCode VARCHAR(20),
		EntityId INT,
		EntityName VARCHAR(250),
		OriginEntityName VARCHAR(250),
		IsClosedYear TINYINT DEFAULT(0),
		CurrencyId INT
	)
		--Se declara una tabla temporal para los detalles del comprobante
	DECLARE @JournalVourcherDetailTmp TABLE 
	(
	    IdAuto INT identity(1,1),
		Id INT DEFAULT(0),
		IdAccounting INT DEFAULT(0),
		IdMainAccount INT,
		IdThirdParty INT,
		IdCostCenter INT,
		DebitValue DECIMAL(18,2),
		CreditValue DECIMAL(18,2),
		Detail VARCHAR(MAX),
		IdRetention INT,
		RetentionRate DECIMAL(6,3),
		BaseValue DECIMAL(18,2),
		BillingValue DECIMAL(18,2)
	)
		--Variable para obtener el xml
	DECLARE @JournalVoucherXML as XML,
			@OfficialCurrencyId as INT

	--tabla temporal para almacenar el resultado deL movimiento contable
	declare @resultJournalVoucher table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)
		/*************************************** PROCESO ************************************/

	BEGIN TRY
	/*se consulta al moneda oficial*/
		select @OfficialCurrencyId = cs.OfficialCurrencyId
		from GeneralLedger.CompanySettings cs
		
	--------------------------------------------------------------------------------------------------------

		INSERT INTO @ConsignmentCostListDetail
		(
			CostNew, ProductId,CostOld,ConsignmentCostListId
		)
		SELECT	
				IIF(t.x.value('CostNew[1]','varchar(20)') = '', null, REPLACE(t.x.value('CostNew[1]','varchar(20)'), ',', '.')) as CostNew,
				t.x.value('(ProductId/text())[1]','int') AS ProductId,
				IIF(t.x.value('CostOld[1]','varchar(20)') = '', null, REPLACE(t.x.value('CostOld[1]','varchar(20)'), ',', '.')) as CostOld,
				t.x.value('(ConsignmentCostListId/text())[1]','int') AS ConsignmentCostListId
		FROM @XMLConsignmentCostList.nodes('/ConsignmentCostListDetail') t(x)

					/***************************** TODOS LOS DETALLES CONTABLES *****************************/

						--Se establece el formato de fecha dd\mm\aaaa

					DECLARE @FormattedDate NVARCHAR(10);

					-- Asignar el formato de la fecha a la variable
					SET @FormattedDate = FORMAT(Common.[GETDATE](), 'dd\\MM\\yyyy');

			INSERT INTO @JournalVourcherDetailTmp
				( IdMainAccount, IdThirdParty, IdCostCenter, Detail, DebitValue, CreditValue, IdRetention, RetentionRate, BaseValue, BillingValue )

					--Cuenta contable debito
					SELECT
						pg.ConsignmentMerchandiseDebitAccountId AS IdMainAccount,
						ccl.SupplierId AS IdThirdParty,
						NULL AS IdCostCenter,
						CONCAT('Valorización de inventario en consignación con fecha:', ' ', @FormattedDate) AS Detail,
						IIF(ccld.CostNew - ccld.CostOld > 1, ABS(ccld.CostNew-ccld.CostOld),0) AS DebitValue,
						IIF(ccld.CostNew - ccld.CostOld < 1, ABS(ccld.CostNew-ccld.CostOld),0) AS CreditValue,
						NULL IdRetention,
						NULL RetentionRate,
						NULL BaseValue,
						NULL BillingValue
					FROM @ConsignmentCostListDetail ccld
					JOIN Inventory.ConsignmentCostList ccl ON ccld.ConsignmentCostListId = ccl.Id
					JOIN Inventory.InventoryProduct ip ON ip.Id = ccld.ProductId
					JOIN Inventory.ProductGroup pg ON ip.ProductGroupId = pg.Id

					UNION ALL

					--Cuenta contable crédito
					SELECT
						pg.ConsignmentMerchandiseCreditAccountId AS IdMainAccount,
						ccl.SupplierId AS IdThirdParty,
						NULL AS IdCostCenter,
						CONCAT('Valorización de inventario en consignación con fecha:','  ', @FormattedDate) AS Detail,
						IIF(ccld.CostNew - ccld.CostOld < 1, ABS(ccld.CostNew-ccld.CostOld),0) AS DebitValue,
						IIF(ccld.CostNew - ccld.CostOld > 1, ABS(ccld.CostNew-ccld.CostOld),0) AS CreditValue,
						NULL IdRetention,
						NULL RetentionRate,
						NULL BaseValue,
						NULL BillingValue
					FROM @ConsignmentCostListDetail ccld
					JOIN Inventory.ConsignmentCostList ccl ON ccld.ConsignmentCostListId = ccl.Id
					JOIN Inventory.InventoryProduct ip ON ip.Id = ccld.ProductId
					JOIN Inventory.ProductGroup pg ON ip.ProductGroupId = pg.Id

			/************************************  CABECERA DEL COMPROBANTE ************************************/
				INSERT INTO @JournalVourcherTmp
					( LegalBookId, IdJournalVoucher, VoucherDate, Status, Detail, EntityCode, EntityId, EntityName, OriginEntityName, CurrencyId)
					SELECT TOP 1
						(SELECT Id from GeneralLedger.LegalBook WHERE OfficialBook = 1 and Status = 1) LegalBookId, 
						(SELECT TOP 1 ValuationConsignmentPriceJournalVoucherTypesId from Inventory.SettingInventory) IdJournalVoucher, 
						Common.GETDATE() VoucherDate, 
						1 Status, 
						CONCAT('Valorización de inventario en consignación con fecha: ', ' ', @FormattedDate)  AS Detail,
						ccl.OperatingUnitId EntityCode,
						ccl.Id EntityId,
						'ConsignmentCostList' EntityName,
						'' OriginEntityName,
						@OfficialCurrencyId CurrencyId
					FROM @ConsignmentCostListDetail ccld
					LEFT JOIN Inventory.ConsignmentCostList ccl ON ccld.ConsignmentCostListId = ccl.Id

							--Obtengo el xml para poder consumir el sp que guarda el comprobante contable
				SELECT @JournalVoucherXML = CONVERT(xml, 
					(
						SELECT * FROM @JournalVourcherTmp JournalVoucher 
						INNER JOIN @JournalVourcherDetailTmp JournalVoucherDetail ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting 
						For xml AUTO,TYPE, ELEMENTS
					)
				)
				--Se consume el sp que guarda el comprobante contable
				Declare @CodeMessage INT,
						@Message Varchar(Max),
						@IdJournalVoucherResult INT

				insert @resultJournalVoucher exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalVoucherXML,@CodeUser 
				select 
					@CodeMessage = rjv.code, 
					@Message = rjv.MessageResult, 
					@IdJournalVoucherResult = rjv.IdJournalVoucher
				from @resultJournalVoucher rjv

				--Se valida que no hayan errores en el guardado del comprobante contable
				IF @CodeMessageSP = 999
				BEGIN

						SELECT 
						@CodeMessageSP = 999,
						@MessageSP = CONCAT('No se creo el comprobante contable de la lista de costos del proveedor: ', CONCAT(tp.Nit, ' - ', tp.Name))
						FROM @ConsignmentCostListDetail ccld 
						JOIN Inventory.ConsignmentCostList ccl ON ccld.ConsignmentCostListId = ccl.Id
						JOIN Common.Supplier s ON ccl.SupplierId = s.Id
						JOIN Common.ThirdParty tp ON s.IdThirdParty = tp.Id
				END
				ELSE
				BEGIN	
					select
					@CodeMessageSP = 0 ,
					@MessageSP = @Message
				END
				
				SELECT
					@CodeMessageSP AS CodeMessageSP,
					@MessageSP AS MessageSP

	END TRY

	BEGIN CATCH
				SELECT 	
				@CodeMessageSP = 999, 
				@MessageSP = CONCAT('Se presentó un error al crear el comprobante contable: ', ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE())

				SELECT
					@CodeMessageSP AS CodeMessageSP,
					@MessageSP AS MessageSP
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera un comprobante contable (asiento de diario) a partir de cambios en la lista de costos de productos en consignación. Recibe un XML con los detalles modificados (producto, costo nuevo y costo anterior) de una lista de consignación, calcula la diferencia de valorización de inventario y construye los movimientos contables de débito y crédito usando las cuentas configuradas en el grupo de producto (ConsignmentMerchandiseDebitAccountId / ConsignmentMerchandiseCreditAccountId), asociando cada línea al proveedor correspondiente. Integra información del catálogo de productos, grupos de producto, lista de costos de consignación y configuración de libros contables para armar tanto el encabezado como el detalle del comprobante de valorización de inventario en consignación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVoucherByConsignmentCostListDetail';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVoucherByConsignmentCostListDetail';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera y registra el comprobante contable de valorización de inventario en consignación a partir de un XML con los cambios de costos (nuevo vs. anterior) por producto y proveedor.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByConsignmentCostListDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe respetar el nodo /ConsignmentCostListDetail con CostNew, CostOld, ProductId y ConsignmentCostListId.; Debe existir registro en GeneralLedger.CompanySettings con OfficialCurrencyId definido.; Debe existir un libro contable oficial activo (LegalBook con OfficialBook=1 y Status=1).; Inventory.SettingInventory debe tener parametrizado ValuationConsignmentPriceJournalVoucherTypesId.; Cada producto del XML debe estar asociado a un ProductGroup con cuentas contables de consignación (débito y crédito) configuradas.; Cada ConsignmentCostListId debe existir en Inventory.ConsignmentCostList con su SupplierId y OperatingUnitId.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByConsignmentCostListDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El comprobante contable se registra siempre contra el libro contable marcado como oficial y activo (OfficialBook=1, Status=1).; El tipo de comprobante usado es el parametrizado en Inventory.SettingInventory.ValuationConsignmentPriceJournalVoucherTypesId.; La moneda del comprobante es la moneda oficial definida en GeneralLedger.CompanySettings.; La entidad de origen del comprobante siempre se identifica como ''ConsignmentCostList'' con el Id de la lista de costos.; Por cada detalle de costo se generan exactamente dos asientos (uno débito y uno crédito) con cuentas tomadas del ProductGroup del producto.; El tercero contable es siempre el proveedor (SupplierId) asociado a la lista de costos de consignación.; El detalle/glosa del comprobante siempre contiene ''Valorización de inventario en consignación con fecha:'' seguido de la fecha en formato dd\\MM\\yyyy.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByConsignmentCostListDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Comprobante contable; Valorización de inventario; Inventario en consignación; Lista de costos de consignación; Cuenta contable débito/crédito; Proveedor (tercero); Libro contable oficial; Moneda oficial; Grupo de producto; Unidad operativa', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByConsignmentCostListDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] GeneralLedger.SP_CreateAndValidateJournalVoucherMovement: Se invoca el SP de contabilidad pasándole el XML armado con la cabecera y los detalles del comprobante para crear y validar el movimiento contable.; [INSERT] JournalVoucherDetail (vía SP_CreateAndValidateJournalVoucherMovement): Por cada detalle del XML se generan dos líneas: una con la cuenta débito de mercancía en consignación (ProductGroup.ConsignmentMerchandiseDebitAccountId) y otra con la cuenta crédito (ConsignmentMerchandiseCreditAccountId), usando como tercero el SupplierId de la lista de costos.; [INSERT] JournalVoucherDetail (vía SP_CreateAndValidateJournalVoucherMovement): Si CostNew - CostOld > 1, el valor absoluto de la diferencia se asigna como DebitValue en la línea débito y como CreditValue en la línea crédito; si CostNew - CostOld < 1, la asignación se invierte (CreditValue en la línea débito y DebitValue en la línea crédito).; [INSERT] JournalVoucher (vía SP_CreateAndValidateJournalVoucherMovement): Se crea cabecera con LegalBookId del libro oficial activo, IdJournalVoucher = ValuationConsignmentPriceJournalVoucherTypesId de SettingInventory, VoucherDate = Common.GETDATE(), Status=1, EntityName=''ConsignmentCostList'', EntityId = Id de la lista de costos, EntityCode = OperatingUnitId, CurrencyId = moneda oficial.; [RETURN_RESULT] Resultado del SP: Devuelve un result set con CodeMessageSP y MessageSP: 0 y mensaje del SP contable en éxito; 999 con el detalle del error o el mensaje ''No se creo el comprobante contable de la lista de costos del proveedor: <Nit> - <Nombre>'' en falla.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByConsignmentCostListDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CostNew - CostOld > 1 → En la línea de la cuenta débito se registra ABS(CostNew-CostOld) como DebitValue (y 0 en CreditValue); en la línea crédito se registra como CreditValue. else Si CostNew - CostOld < 1 se invierte: ABS de la diferencia va a CreditValue en la línea débito y a DebitValue en la línea crédito.; si @CodeMessageSP = 999 tras invocar SP_CreateAndValidateJournalVoucherMovement → Se construye mensaje de error mencionando Nit y nombre del proveedor (vía Supplier→ThirdParty) indicando que no se creó el comprobante contable. else Se asigna CodeMessageSP = 0 y MessageSP con el mensaje devuelto por el SP contable.; si Excepción capturada en BEGIN CATCH → Devuelve CodeMessageSP=999 con mensaje que incluye ERROR_MESSAGE() y ERROR_LINE().', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByConsignmentCostListDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'GeneralLedger.SP_CreateAndValidateJournalVoucherMovement; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByConsignmentCostListDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.CompanySettings; Inventory.ConsignmentCostList; Inventory.InventoryProduct; Inventory.ProductGroup; GeneralLedger.LegalBook; Inventory.SettingInventory; Common.Supplier; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByConsignmentCostListDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByConsignmentCostListDetail';
-- GO
