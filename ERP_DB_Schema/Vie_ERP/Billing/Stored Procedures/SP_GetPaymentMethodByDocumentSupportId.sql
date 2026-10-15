-- ===============================================================================================================
-- Author:		Juan David Capera Núñez
-- Create date: 2022-07-25
-- Description:	Procedimiento para obtener los pagos relacionados a un documento de soporte electrónico
-- ==============================================================================================================
CREATE PROCEDURE [Billing].[SP_GetPaymentMethodByDocumentSupportId]
	@DocumentSupportId INT
AS
BEGIN

	DECLARE @EntityId INT,
			@EntityName VARCHAR(250)
			
	IF  OBJECT_ID('tempdb..#Table_Result') IS NOT NULL DROP TABLE #TableResult

	CREATE TABLE #Table_Result
	(
		PaymentMethodId TINYINT,
		PaymentMethodCode VARCHAR(100),
		PaymentDueDate DATE,
		Detail VARCHAR(250),
		----------------------------
		StateResult BIT,
		MessageResult VARCHAR(500)
	)

	SELECT @EntityId = ISNULL(EntityId, Id),
		   @EntityName = EntityName
	FROM Billing.ElectronicSupportDocument
	WHERE Id = @DocumentSupportId

	/*----------------------------------------- OBTENCIÓN DE DATOS -----------------------------------------*/
	BEGIN TRY
		IF @EntityName = 'AccountPayable'
		BEGIN

			INSERT INTO #Table_Result
			SELECT 
				2 AS PaymentMethodId,
				'ZZZ' AS PaymentMethodCode,
				ap.ExpirationDate AS PaymentDueDate,
				NULL AS Detail,
				1, 'OK'
			FROM Payments.AccountPayable ap
			WHERE ap.Id = @EntityId

		END
		ELSE IF @EntityName = 'VoucherTransaction'
		BEGIN

			INSERT INTO #Table_Result
			SELECT	
				1 AS PaymentMethodId,
				CASE  
					WHEN vt.PaymentMethod = 1 THEN '20'
					WHEN vt.PaymentMethod = 2 THEN '3'
					WHEN vt.ExpenseType IN (2,3) THEN '10'
					ELSE 'ZZZ'
				END AS PaymentMethodCode,
				vt.DocumentDate AS PaymentDueDate,
				NULL AS Detail,
				1, 'OK'
			FROM Treasury.VoucherTransaction vt
			WHERE vt.Id = @EntityId
				AND vt.VoucherClass = 1

		END
		ELSE 
		BEGIN 
			
			INSERT INTO #Table_Result
			SELECT 
				1 AS PaymentMethodId,
				'ZZZ' AS PaymentMethodCode,
				esd.DocumentDate AS PaymentDueDate,
				NULL AS Detail,
				1, 'OK'
			FROM Billing.ElectronicSupportDocument esd
			WHERE esd.Id = @EntityId

		END

	END TRY
	BEGIN CATCH	
		DELETE FROM #Table_Result
		INSERT INTO #Table_Result (StateResult, MessageResult)
		SELECT 0, CONCAT('Se presento un error obteniendo la información de los métodos de pagos del documento soporte: ',ERROR_MESSAGE(), ' Línea: ', ERROR_LINE())
	END CATCH
	/*----------------------------------------- DEVOLVEMOS RESULTADOS -----------------------------------------*/
	SELECT DISTINCT
		PaymentMethodId,
		PaymentMethodCode,
		PaymentDueDate,
		Detail,
		----------------------------
		StateResult,
		MessageResult
	FROM #Table_Result
END