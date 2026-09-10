-- ===============================================================================================================
-- Author:		Juan David Capera Núñez
-- Create date: 2022-07-25
-- Description:	Procedimiento para obtener los detalles relacionados a un documento de soporte electrónico
-- ==============================================================================================================
CREATE PROCEDURE [Billing].[SP_GetDocumentSupportDetailsById]
	@DocumentSupportId INT
AS
BEGIN

	DECLARE @EntityId INT,
			@EntityName VARCHAR(250)

	IF  OBJECT_ID('tempdb..#Table_Result') IS NOT NULL DROP TABLE #TableResult

	CREATE TABLE #Table_Result
	(
		StateResult BIT,
		MessageResult VARCHAR(500),
	    --------------------------------------
		EntityName VARCHAR(250),
		EntityCode VARCHAR(20),
		CodeItem VARCHAR(20),
		NameItem VARCHAR(500),
		--------------------------------------
		InvoiceQuantity INT,
		Value DECIMAL(18,2),
		DebitValue DECIMAL(18,2),
		CreditValue DECIMAL(18,2),
		BaseValue DECIMAL(18,2),
		DiscountValue DECIMAL(18,2),
		DiscountPercentage DECIMAL(5,2),
		SurchargeValue DECIMAL(18,2),
		SurchargePercentage DECIMAL(5,2),
		-------------------------------------
		IVAPercentage DECIMAL(5,2),
		IVAValue DECIMAL(18,2),
		ICAPercentage DECIMAL(5,2),
		ICAValue DECIMAL(18,2),
		----------------------------------
		WithholdingPercentage DECIMAL(5,2),
		WithholdingValue DECIMAL(18,2),
		WithholdingIVAPercentage DECIMAL(5,2),
		WithholdingIVAValue DECIMAL(18,2),
		WithholdingICAPercentage DECIMAL(5,2),
		WithholdingICAValue DECIMAL(18,2)
	)

	SELECT @EntityId = ISNULL(EntityId, Id),
		   @EntityName = EntityName
	FROM Billing.ElectronicSupportDocument
	WHERE Id = @DocumentSupportId

	BEGIN TRY
		IF @EntityName = 'AccountPayable'
		BEGIN

			INSERT INTO #Table_Result
			SELECT 0 AS StateResult, 'OK' AS MessageResult,
				   'AccountPayable' AS EntityName, ap.Code AS EntityCode, '001' CodeItem, ap.Coments NameItem, 1 InvoiceQuantity,
					ap.InvoiceValue AS Value, ap.InvoiceValue AS DebitValue, 0 AS CreditValue, 0 BaseValue,
					0 DiscountValue, 0 DiscountPercentage, 0 SurchargeValue, 0 SurchargePercentage,
					0 IVAPercentage, 0 IVAValue,
					0 ICAPercentage, 0 ICAValue,
					----------------------------------
					0 WithholdingPercentage, 0 WithholdingValue,
					0 WithholdingIVAPercentage, 0 WithholdingIVAValue,
					0 WithholdingICAPercentage, 0 WithholdingICAValue
			FROM Payments.AccountPayable ap
			WHERE ap.Id = @EntityId

		END
		ELSE IF @EntityName = 'VoucherTransaction'
		BEGIN

			INSERT INTO #Table_Result
			SELECT 0 AS StateResult, 'OK' AS MessageResult,
			'VoucherTransaction' AS EntityName, vt.Code AS EntityCode, '001' CodeItem, vt.Detail AS NameItem, 1 InvoiceQuantity, 
			(vtd.DebitValue - vtd.CreditValue) Value, vtd.DebitValue, vtd.CreditValue, 0 BaseValue, 
			0 DiscountValue, 0 DiscountPercentage, 0 SurchargeValue, 0 SurchargePercentage,
			0 IVAPercentage, 0 IVAValue,
			0 ICAPercentage, 0 ICAValue,
			----------------------------------
			0 WithholdingPercentage, 0 WithholdingValue,
			0 WithholdingIVAPercentage, 0 WithholdingIVAValue,
			0 WithholdingICAPercentage, 0 WithholdingICAValue
			FROM Treasury.VoucherTransaction vt
			JOIN 
			(
				SELECT 
					IdVoucherTransaction,
					SUM(IIF(vtd.Nature = 1, vtd.Value, 0)) DebitValue,
					SUM(IIF(vtd.Nature = 1, 0, vtd.Value)) CreditValue,
					0 TaxValue
				FROM Treasury.VoucherTransactionDetails vtd
				JOIN Treasury.ExpenseConcepts ec WITH(NOLOCK) ON vtd.IdExpenseConcept = ec.Id
				WHERE ec.Behavior = 6
				GROUP BY IdVoucherTransaction
			) vtd ON vt.Id = vtd.IdVoucherTransaction
			WHERE vt.Id = @EntityId
				AND vt.VoucherClass = 1

		END
		ELSE 
		BEGIN

			INSERT INTO #Table_Result
			SELECT 0 AS StateResult, 'OK' AS MessageResult,
			NULL EntityName, NULL EntityCode, NULL Code, NULL Name, 1 InvoiceQuantity, 
			esd.TotalValue, esd.TotalValue DebitValue, 0 CreditValue,  esd.SubTotalValue AS BaseValue, 
			0 DiscountValue, 0 DiscountPercentage, 0 SurchargeValue, 0 SurchargePercentage, 
			0 IVAPercentage, esd.TaxValue IVAValue,
			0 ICAPercentage, 0 ICAValue,
			----------------------------------
			0 WithholdingPercentage, 0 WithholdingValue,
			0 WithholdingIVAPercentage, 0 WithholdingIVAValue,
			0 WithholdingICAPercentage, 0 WithholdingICAValue
			FROM Billing.ElectronicSupportDocument esd
			WHERE esd.Id = @EntityId

		END
	END TRY
	BEGIN CATCH
		DELETE FROM #Table_Result

		INSERT INTO #Table_Result(StateResult, MessageResult)
		SELECT 1, CONCAT('Se presento un error al intentar obtener los detalles del documento soporte: ',ERROR_MESSAGE(), ' Línea: ',ERROR_LINE())
	END CATCH 

	SELECT * FROM #Table_Result
END