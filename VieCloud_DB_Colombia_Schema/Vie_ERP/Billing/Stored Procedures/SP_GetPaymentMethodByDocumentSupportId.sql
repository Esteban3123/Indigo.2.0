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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene el método de pago y la fecha de vencimiento asociados a un documento soporte electrónico de facturación, dado su identificador. Según el tipo de entidad vinculada al documento (cuenta por pagar a proveedor o transacción de tesorería), consulta las tablas correspondientes para determinar el código de medio de pago (efectivo, transferencia, crédito u otro) y la fecha límite de pago. Se usa en el proceso de facturación electrónica entre proveedor y cliente para informar las condiciones de pago requeridas en la transmisión del documento soporte ante la DIAN.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GetPaymentMethodByDocumentSupportId';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GetPaymentMethodByDocumentSupportId';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina el método de pago, código y fecha de vencimiento aplicables a un documento soporte electrónico, según la entidad origen (cuenta por pagar, comprobante de tesorería o el propio documento soporte).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetPaymentMethodByDocumentSupportId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El documento soporte electrónico debe existir en Billing.ElectronicSupportDocument para resolver EntityId/EntityName.; Si EntityId es NULL en ElectronicSupportDocument se asume el propio Id del documento (ISNULL(EntityId, Id)).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetPaymentMethodByDocumentSupportId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran VoucherTransaction con VoucherClass=1.; Cuando no se puede determinar un código específico, el código por defecto es ''ZZZ''.; Los errores no propagan excepción: se devuelven como una fila con StateResult=0 y MessageResult descriptivo.; Las filas exitosas siempre se devuelven con StateResult=1 y MessageResult=''OK''.; El campo Detail siempre se retorna NULL en los flujos de éxito.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetPaymentMethodByDocumentSupportId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Documento soporte electrónico; Cuenta por pagar; Comprobante de tesorería (VoucherTransaction); Método de pago; Fecha de vencimiento de pago; Tipo de gasto (ExpenseType)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetPaymentMethodByDocumentSupportId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] #Table_Result: Cuando EntityName=''AccountPayable'', se inserta PaymentMethodId=2, PaymentMethodCode=''ZZZ'' y PaymentDueDate=ExpirationDate de Payments.AccountPayable.; [INSERT] #Table_Result: Cuando EntityName=''VoucherTransaction'' y VoucherClass=1, se inserta PaymentMethodId=1 con PaymentMethodCode=''20'' si PaymentMethod=1, ''3'' si PaymentMethod=2, ''10'' si ExpenseType IN (2,3), de lo contrario ''ZZZ''; PaymentDueDate=DocumentDate.; [INSERT] #Table_Result: Cuando EntityName no es ''AccountPayable'' ni ''VoucherTransaction'', se inserta PaymentMethodId=1, PaymentMethodCode=''ZZZ'' y PaymentDueDate=DocumentDate del propio ElectronicSupportDocument.; [DELETE] #Table_Result: En el bloque CATCH se vacía la tabla temporal antes de registrar el error.; [INSERT] #Table_Result: Ante error, se inserta StateResult=0 y MessageResult con el texto del ERROR_MESSAGE() y ERROR_LINE() concatenados.; [RETURN_RESULT] #Table_Result: Devuelve SELECT DISTINCT del resultado con método, código, fecha de vencimiento, detalle, estado y mensaje.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetPaymentMethodByDocumentSupportId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si EntityName = ''AccountPayable'' → Toma el método de pago desde Payments.AccountPayable usando ExpirationDate como fecha de vencimiento y código ''ZZZ'' con PaymentMethodId=2.; si EntityName = ''VoucherTransaction'' → Toma datos de Treasury.VoucherTransaction filtrando VoucherClass=1 y mapea PaymentMethodCode según PaymentMethod/ExpenseType. else Si no aplica AccountPayable ni VoucherTransaction, usa el propio ElectronicSupportDocument con código ''ZZZ'' y DocumentDate.; si Dentro de VoucherTransaction: vt.PaymentMethod=1 → PaymentMethodCode=''20'' else Evalúa siguientes ramas (PaymentMethod=2 → ''3''; ExpenseType IN (2,3) → ''10''; en otro caso ''ZZZ'').', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetPaymentMethodByDocumentSupportId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.ElectronicSupportDocument; Payments.AccountPayable; Treasury.VoucherTransaction', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetPaymentMethodByDocumentSupportId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetPaymentMethodByDocumentSupportId';
-- GO
