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