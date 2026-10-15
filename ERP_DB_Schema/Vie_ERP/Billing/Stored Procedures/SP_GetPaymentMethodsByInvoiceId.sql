-- ===============================================================================================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-11-24
-- Description:	Procedimiento que se encarga de obtener los pagos relacionados a una factura
-- ==============================================================================================================
CREATE PROCEDURE [Billing].[SP_GetPaymentMethodsByInvoiceId]
	@InvoiceId INT
AS
BEGIN
	
	DECLARE @IncoiceCopayId INT = NULL
	DECLARE @ConceptCollection varchar(2) = NULL


	--verifica si la fact salud tiene asociado fact copago
	IF EXISTS(SELECT 1 FROM Billing.InvoiceCopay WHERE InvoiceId = @InvoiceId) BEGIN
		
		SELECT top 1 @IncoiceCopayId = bb.InvoiceId
		FROM Billing.InvoiceCopay ic WITH(NOLOCK)
		JOIN Billing.BasicBilling bb WITH(NOLOCK) on ic.BasicBillingId = bb.Id
		WHERE ic.InvoiceId = @InvoiceId	
	END

	--Valida que la factura sea de tipo SALUD para detectar el tipo de recaudo
	IF EXISTS(SELECT 1 
				FROM Billing.Invoice WITH(NOLOCK) where Id =@InvoiceId and DocumentType in (1,2))
	BEGIN
			SELECT TOP 1 @ConceptCollection = CASE id.RecoveryFeeType 
													WHEN 2 THEN '02'
													WHEN 3 THEN '01'
													WHEN 4 THEN '03'
													ELSE '05'
												END
			FROM Billing.Invoice i with(NOLOCK)
			JOIN Billing.InvoiceDetail id WITH(NOLOCK) on i.Id =id.InvoiceId
			where i.Id =@InvoiceId
	END


	SELECT	pm.*,
			CASE pm.PaymentMethodTypes
				WHEN 1 THEN '10'
				WHEN 2 THEN '20'
				WHEN 3 THEN '48'
				WHEN 4 THEN '42'
				ELSE 'ZZZ'
			END MethodTypeCode,
			@ConceptCollection as ConceptCollection
	FROM
	(
		SELECT	v.AccountReceivableType,
				v.Code, 
				v.DocumentDate,
				v.PaymentMethodTypes, 
				v.Value
		FROM [Billing].[ViewPaymentMethods] v
		WHERE v.InvoiceId =COALESCE(@IncoiceCopayId,@InvoiceId) 
		
		UNION ALL

		SELECT	ar.AccountReceivableType,
				ar.Code, 
				ar.AccountReceivableDate DocumentDate,
				CAST(99 AS TINYINT) PaymentMethodTypes, 
				(ar.Value - ISNULL(v.Value, 0)) Value
		FROM Portfolio.AccountReceivable ar WITH (NOLOCK)
		LEFT JOIN 
		(
			SELECT v.InvoiceId, v.AccountReceivableType, SUM(v.Value) Value
			FROM [Billing].[ViewPaymentMethods] v 
			GROUP BY v.InvoiceId, v.AccountReceivableType
		) v ON ar.InvoiceId = v.InvoiceId AND ar.AccountReceivableType = v.AccountReceivableType
		WHERE ar.InvoiceId = COALESCE(@IncoiceCopayId,@InvoiceId)
			AND ar.AccountReceivableType IN (4, 6)
			AND ar.Value > ISNULL(v.Value, 0)
	) pm
END