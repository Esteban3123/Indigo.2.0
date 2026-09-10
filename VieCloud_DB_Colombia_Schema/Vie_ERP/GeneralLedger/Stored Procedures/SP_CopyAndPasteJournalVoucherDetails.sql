-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-09-16
-- Description:	Procedimiento que se encarga de el copyPaste del form comprobante contable
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_CopyAndPasteJournalVoucherDetails]
	@LegalBookId INT,
	@XmlObject XML
AS
BEGIN
	SET NOCOUNT ON

	/*************************************************** VARIABLES ***************************************************/
	
	--Tabla para almacenar los items del listado que viene en el xml y los resultados a devolver
	DECLARE @TableXmlObject TABLE
	(
		RowIndex INT NOT NULL,
		RowColumns INT NOT NULL,
		MainAccount VARCHAR(500),
		ThirdParty VARCHAR(500),
		CostCenter VARCHAR(500) ,
		StringNature VARCHAR(500),
		StringValue VARCHAR(500),
		Detail VARCHAR(MAX),
		RetentionConcept VARCHAR(500),
		StringBaseValue VARCHAR(500),
		StringBillingValue VARCHAR(500),
		--------------------------------
		MainAccountId INT,
		ThirdPartyId INT,
		CostCenterId INT,
		DebitValue DECIMAL(20, 4) NOT NULL DEFAULT(0),
		CreditValue DECIMAL(20, 4) NOT NULL DEFAULT(0),
		RetentionConceptId INT,
		RetentionRate DECIMAL(5, 3),
		BaseValue DECIMAL(18, 2),
		BillingValue DECIMAL(18, 2),
		--------------------------------
		StatusField INT DEFAULT(0), --Estado pendiente de validación
		MessageField VARCHAR(MAX)
	)

	BEGIN TRY

		INSERT INTO @TableXmlObject
		(
			RowIndex, RowColumns, 
			MainAccount, ThirdParty, CostCenter,
			StringNature, StringValue, Detail,
			RetentionConcept, StringBaseValue, StringBillingValue
		)
		SELECT	t.x.value('RowIndex[1]','int') as RowIndex,
				t.x.value('RowColumns[1]','int') as RowColumns,
				t.x.value('MainAccount[1]','varchar(500)') as MainAccount,
				t.x.value('ThirdParty[1]','varchar(500)') as ThirdParty,
				t.x.value('CostCenter[1]','varchar(500)') as CostCenter,
				t.x.value('StringNature[1]','varchar(500)') as StringNature,
				t.x.value('StringValue[1]','varchar(500)') as StringValue,
				t.x.value('Detail[1]','varchar(max)') as Detail,
				t.x.value('RetentionConcept[1]','varchar(500)') as RetentionConcept,
				t.x.value('StringBaseValue[1]','varchar(500)') as StringBaseValue,
				t.x.value('StringBillingValue[1]','varchar(500)') as StringBillingValue
		FROM @XmlObject.nodes('/Data/Row') t(x)

		/***********************************************  VALIDACIONES ***********************************************/

		-- Validar el numero de columnas de acuerdo al tipo de documento
		UPDATE tx
			SET tx.StatusField = 999,
				tx.MessageField = CONCAT('El registro ', tx.RowIndex, ' no tiene la estructura válida')
		FROM @TableXmlObject tx
		WHERE tx.StatusField = 0 AND tx.RowColumns < 6

		-- Validar que no exista dos registros con el mismo indice
		UPDATE tx
			SET tx.StatusField = 999,
				tx.MessageField = CONCAT('El registro ', tx.RowIndex, ' se encuentra duplicado')
		FROM @TableXmlObject tx
		JOIN
		(
			SELECT tx.RowIndex
			FROM @TableXmlObject tx
			GROUP BY tx.RowIndex
			HAVING COUNT(1) > 1
		) txd ON tx.RowIndex = txd.RowIndex
		WHERE tx.StatusField = 0

		-- Validar cuenta contable
		UPDATE tx
			SET tx.StatusField = IIF(txd.MessageField IS NOT NULL, 999, tx.StatusField),
				tx.MessageField = IIF(tx.StatusField = 999,CONCAT(tx.MessageField,'  ', txd.MessageField),txd.MessageField),
				---------------------------------------------------------------
				tx.MainAccount = ISNULL(txd.MainAccount,tx.MainAccount),
				tx.MainAccountId = txd.MainAccountId
		FROM @TableXmlObject tx
		JOIN
		(
			SELECT	tx.RowIndex,
					CONCAT('La cuenta contable ', tx.MainAccount, ' del registro ', tx.RowIndex, ' ') + CASE 
						WHEN ma.Id IS NULL THEN 'no existe'
						WHEN ma.Status = 0 THEN 'se encuentra inactiva'
						WHEN ma.AllowsMovement = 0 THEN 'no permite movimientos'
					END MessageField,
					IIF(ma.Number IS NOT NULL OR ma.Name IS NOT NULL,CONCAT(ma.Number, ' - ', ma.Name),NULL) MainAccount,
					ma.Id MainAccountId
			FROM @TableXmlObject tx
			LEFT JOIN GeneralLedger.MainAccounts ma ON tx.MainAccount = ma.Number AND @LegalBookId = ma.LegalBookId
		) txd ON tx.RowIndex = txd.RowIndex
		
		-- Validar tercero
		UPDATE tx
			SET tx.StatusField = IIF(txd.MessageField IS NOT NULL, 999, tx.StatusField),
				tx.MessageField = IIF(tx.StatusField = 999,CONCAT(tx.MessageField,'  ', txd.MessageField),txd.MessageField),
				---------------------------------------------------------------
				tx.ThirdParty = IIF(txd.ThirdPartyId IS NULL,tx.ThirdParty, txd.ThirdParty),
				tx.ThirdPartyId = txd.ThirdPartyId
		FROM @TableXmlObject tx
		JOIN
		(
			SELECT	tx.RowIndex,
					CONCAT('El tercero ', tx.ThirdParty, ' del registro ', tx.RowIndex, ' ')  + CASE
						WHEN ma.HandlesThirdParty = 0 THEN NULL
						WHEN tp.Id IS NULL THEN 'no existe'
						WHEN tp.State = 0 THEN 'se encuentra inactivo'
					END MessageField,
					CONCAT(tp.Nit, ' - ', tp.Name) ThirdParty,
					IIF(ma.HandlesThirdParty = 0, NULL, tp.Id) ThirdPartyId
			FROM @TableXmlObject tx
			JOIN GeneralLedger.MainAccounts ma ON tx.MainAccountId = ma.Id
			LEFT JOIN Common.ThirdParty tp ON ma.HandlesThirdParty = 1 AND tx.ThirdParty = tp.Nit
		) txd ON tx.RowIndex = txd.RowIndex
		
		-- Validar centro de costo
		UPDATE tx
			SET tx.StatusField = IIF(txd.MessageField IS NOT NULL, 999, tx.StatusField),
				tx.MessageField = IIF(tx.StatusField = 999,CONCAT(tx.MessageField,'  ', txd.MessageField),txd.MessageField),
				---------------------------------------------------------------
				tx.CostCenterId = txd.CostCenterId,
				tx.CostCenter = IIF(txd.CostCenterId IS NULL,tx.CostCenter, txd.CostCenter)
		FROM @TableXmlObject tx
		JOIN
		(
			SELECT	tx.RowIndex,
					CONCAT('El centro de costo ', tx.CostCenter, ' del registro ', tx.RowIndex, ' ')  + CASE
						WHEN ma.HandlesCostCenter = 0 THEN NULL
						WHEN cc.Id IS NULL THEN 'no existe'
						WHEN cc.State = 0 THEN 'se encuentra inactivo'
					END MessageField,
					CONCAT(cc.Code, ' - ', cc.Name) CostCenter,
					IIF(ma.HandlesCostCenter = 0, NULL, cc.Id) CostCenterId
			FROM @TableXmlObject tx
			JOIN GeneralLedger.MainAccounts ma ON tx.MainAccountId = ma.Id
			LEFT JOIN Payroll.CostCenter cc ON ma.HandlesCostCenter = 1 AND tx.CostCenter = cc.Code
		) txd ON tx.RowIndex = txd.RowIndex
		
		-- Validar naturaleza
		UPDATE tx
			SET tx.StatusField = 999,
				tx.MessageField =  IIF(tx.StatusField = 999,CONCAT(tx.MessageField,'  ', CONCAT('La naturaleza ', tx.StringNature, ' del registro ', tx.RowIndex, ' no es válida')),'')   
		FROM @TableXmlObject tx
		WHERE ISNULL(tx.StringNature, '') NOT IN (1, 2)
		
		-- Validar valor
		UPDATE tx
			SET tx.StatusField = IIF(txd.MessageField IS NOT NULL, 999, tx.StatusField),
				tx.MessageField = IIF(tx.StatusField = 999,CONCAT(tx.MessageField,'  ', txd.MessageField),txd.MessageField),
				---------------------------------------------------------------
				tx.DebitValue = txd.DebitValue,
				tx.CreditValue = txd.CreditValue
		FROM @TableXmlObject tx
		JOIN
		(
			SELECT	tx.RowIndex,
					CONCAT('El valor ', tx.StringValue, ' del registro ', tx.RowIndex, ' ')  + CASE
						WHEN ISNUMERIC(tx.StringValue) = 0 THEN 'no es numerico'
						WHEN CAST(tx.StringValue AS DECIMAL(20,4)) <= 0 THEN 'es negativo o cero'
					END MessageField,
					IIF(tx.StringNature = 1 AND ISNUMERIC(tx.StringValue) = 1, CAST(tx.StringValue AS DECIMAL(20,4)), 0) DebitValue,
					IIF(tx.StringNature = 2 AND ISNUMERIC(tx.StringValue) = 1, CAST(tx.StringValue AS DECIMAL(20,4)), 0) CreditValue
			FROM @TableXmlObject tx
		) txd ON tx.RowIndex = txd.RowIndex
		
		-- Validar detalle
		UPDATE tx
			SET tx.StatusField = 999,
				tx.MessageField =  IIF(tx.StatusField = 999,CONCAT(tx.MessageField,'  ', CONCAT('La observación ', tx.Detail, ' del registro ', tx.RowIndex, ' debe tener 500 caracteres o menos')),'')
		FROM @TableXmlObject tx
		WHERE LEN(ISNULL(tx.Detail, '')) > 500

		-- Validar retencion
		UPDATE tx
			SET tx.StatusField = IIF(txd.MessageField IS NOT NULL, 999, tx.StatusField),
				tx.MessageField = IIF(tx.StatusField = 999,CONCAT(tx.MessageField,'  ', txd.MessageField),txd.MessageField),
				---------------------------------------------------------------
				tx.RetentionConcept = IIF(txd.RetentionConceptId IS NULL,tx.RetentionConcept, txd.RetentionConcept),
				tx.RetentionConceptId = txd.RetentionConceptId,
				tx.RetentionRate = txd.RetentionRate
		FROM @TableXmlObject tx
		JOIN
		(
			SELECT	tx.RowIndex,
					CONCAT('El concepto de retención ', tx.RetentionConcept, ' del registro ', tx.RowIndex, ' ')  + CASE
						WHEN ma.RetencionType = 0 THEN NULL
						WHEN rc.Id IS NULL THEN 'no existe'
						WHEN rc.Status = 0 THEN 'se encuentra inactivo'
					END MessageField,
					CONCAT(rc.Code, ' - ', rc.Name) RetentionConcept,
					IIF(ma.RetencionType = 0, NULL, rc.Id) RetentionConceptId,
					IIF(ma.RetencionType = 0, NULL, rc.Rate) RetentionRate
			FROM @TableXmlObject tx
			JOIN GeneralLedger.MainAccounts ma ON tx.MainAccountId = ma.Id
			LEFT JOIN GeneralLedger.RetentionConcepts rc ON ma.RetencionType <> 0 AND tx.RetentionConcept = rc.Code
		) txd ON tx.RowIndex = txd.RowIndex
		
		-- Validar base de retención
		UPDATE tx
			SET tx.StatusField = IIF(txd.MessageField IS NOT NULL, 999, tx.StatusField),
				tx.MessageField = IIF(tx.StatusField = 999,CONCAT(tx.MessageField,'  ', txd.MessageField),txd.MessageField),
				---------------------------------------------------------------
				tx.BaseValue = txd.BaseValue
		FROM @TableXmlObject tx
		JOIN
		(
			SELECT	tx.RowIndex,
					CONCAT('La base de retencion ', tx.StringBaseValue, ' del registro ', tx.RowIndex, ' ')  + CASE
						WHEN tx.RetentionConceptId IS NULL THEN NULL
						WHEN ISNUMERIC(tx.StringBaseValue) = 0 THEN 'no es numerico'
						WHEN CAST(tx.StringBaseValue AS DECIMAL(20,4)) <= 0 THEN 'es negativo o cero'
					END MessageField,
					IIF(tx.RetentionConceptId IS NOT NULL AND ISNUMERIC(tx.StringBaseValue) = 1, CAST(tx.StringBaseValue AS DECIMAL(20,4)), NULL) BaseValue
			FROM @TableXmlObject tx
		) txd ON tx.RowIndex = txd.RowIndex
		
		-- Validar valor facturado
		UPDATE tx
			SET tx.StatusField = IIF(txd.MessageField IS NOT NULL, 999, tx.StatusField),
				tx.MessageField = IIF(tx.StatusField = 999,CONCAT(tx.MessageField,'  ', txd.MessageField),txd.MessageField),
				---------------------------------------------------------------
				tx.BillingValue = txd.BillingValue
		FROM @TableXmlObject tx
		JOIN
		(
			SELECT	tx.RowIndex,
					CONCAT('El valor facturado ', tx.StringBillingValue, ' del registro ', tx.RowIndex, ' ')  + CASE
						WHEN tx.RetentionConceptId IS NULL THEN NULL
						WHEN ISNUMERIC(tx.StringBillingValue) = 0 THEN 'no es numerico'
						WHEN CAST(tx.StringBillingValue AS DECIMAL(20,4)) <= 0 THEN 'es negativo o cero'
					END MessageField,
					IIF(tx.RetentionConceptId IS NOT NULL AND ISNUMERIC(tx.StringBillingValue) = 1, CAST(tx.StringBillingValue AS DECIMAL(20,4)), NULL) BillingValue
			FROM @TableXmlObject tx
		) txd ON tx.RowIndex = txd.RowIndex
		
	END TRY
	BEGIN CATCH
		DELETE FROM @TableXmlObject
		INSERT INTO @TableXmlObject(RowIndex, RowColumns,StatusField, MessageField)
		VALUES (0, 0, 999, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(5)))
	END CATCH

	SELECT	MainAccount,
			MainAccountId,
			ThirdParty,
			ThirdPartyId,
			CostCenter,
			CostCenterId,
			DebitValue,
			CreditValue,
			Detail,
			RetentionConcept,
			RetentionConceptId,
			RetentionRate,
			BaseValue,
			BillingValue,
			--------------------------------
			StatusField, 
			MessageField
	FROM @TableXmlObject
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que procesa el copiado y pegado de líneas de detalle en un comprobante contable (voucher). Recibe un XML con las filas a copiar, cada una con cuenta contable, tercero, centro de costo, naturaleza (débito/crédito), valor, detalle y concepto de retención, y las valida contra el catálogo de cuentas del libro legal indicado (GeneralLedger.MainAccounts), verificando que la cuenta exista, esté activa y permita movimientos, que el tercero y el centro de costo sean válidos, y que los valores numéricos tengan el formato correcto. Devuelve el resultado fila por fila indicando si cada registro es válido o contiene errores, permitiendo al usuario del formulario contable pegar múltiples líneas de un comprobante y recibir retroalimentación inmediata sobre inconsistencias antes de grabar el asiento contable.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteJournalVoucherDetails';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteJournalVoucherDetails';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida y enriquece líneas de un comprobante contable recibidas vía XML (copiar/pegar), resolviendo cuenta, tercero, centro de costo y retención, y devolviendo cada fila con estado y mensajes de error.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteJournalVoucherDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe seguir la estructura /Data/Row con los nodos esperados (RowIndex, RowColumns, MainAccount, ThirdParty, CostCenter, StringNature, StringValue, Detail, RetentionConcept, StringBaseValue, StringBillingValue).; Debe proveerse un LegalBookId válido para resolver las cuentas contables del plan correspondiente.; RowIndex debe ser único por fila para no marcarse como duplicado.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteJournalVoucherDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila válida queda con StatusField=0 (pendiente) y las inválidas con StatusField=999.; Los mensajes de error de una misma fila se concatenan acumulativamente.; DebitValue y CreditValue son mutuamente excluyentes según la naturaleza (1=débito, 2=crédito).; Solo se exigen tercero, centro de costo, retención, base y valor facturado cuando la cuenta contable los habilita.; El procedimiento nunca modifica tablas persistentes; solo valida y devuelve el dataset.; En caso de error no controlado, siempre devuelve una única fila con el detalle del error y línea.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteJournalVoucherDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableXmlObject: Carga una fila por cada nodo /Data/Row del XML con sus valores en bruto.; [UPDATE] @TableXmlObject: Si RowColumns < 6, marca StatusField=999 con mensaje ''no tiene la estructura válida''.; [UPDATE] @TableXmlObject: Si existen varias filas con el mismo RowIndex, marca todas con StatusField=999 y mensaje ''se encuentra duplicado''.; [UPDATE] @TableXmlObject: Para la cuenta contable: si no existe en MainAccounts del LegalBook, o Status=0 (inactiva), o AllowsMovement=0, marca StatusField=999; si existe, asigna MainAccountId y formatea MainAccount como ''Number - Name''.; [UPDATE] @TableXmlObject: Para el tercero: solo se valida si la cuenta tiene HandlesThirdParty=1; si el Nit no existe en Common.ThirdParty o State=0, marca StatusField=999; si la cuenta no maneja terceros, ThirdPartyId queda NULL.; [UPDATE] @TableXmlObject: Para el centro de costo: solo se valida si la cuenta tiene HandlesCostCenter=1; si el Code no existe en Payroll.CostCenter o State=0, marca StatusField=999; si la cuenta no maneja CC, CostCenterId queda NULL.; [UPDATE] @TableXmlObject: Si StringNature no es ''1'' (débito) ni ''2'' (crédito), marca StatusField=999 con mensaje ''no es válida''.; [UPDATE] @TableXmlObject: Si StringValue no es numérico o es <= 0, marca StatusField=999; si es válido, asigna DebitValue cuando StringNature=1 y CreditValue cuando StringNature=2.; [UPDATE] @TableXmlObject: Si LEN(Detail) > 500 caracteres, marca StatusField=999 con mensaje ''debe tener 500 caracteres o menos''.; [UPDATE] @TableXmlObject: Para retención: solo se valida si la cuenta tiene RetencionType<>0; si el código no existe en RetentionConcepts o Status=0, marca StatusField=999; al ser válido, asigna RetentionConceptId y RetentionRate desde el concepto.; [UPDATE] @TableXmlObject: Si hay RetentionConceptId asignado y StringBaseValue no es numérico o <= 0, marca StatusField=999; si es válido, asigna BaseValue.; [UPDATE] @TableXmlObject: Si hay RetentionConceptId asignado y StringBillingValue no es numérico o <= 0, marca StatusField=999; si es válido, asigna BillingValue.; [DELETE] @TableXmlObject: En caso de excepción (CATCH) se limpia la tabla temporal y se inserta una sola fila con StatusField=999 y el mensaje/línea del error.; [RETURN_RESULT] @TableXmlObject: Retorna el resultado final con cuenta, tercero, centro de costo, valores débito/crédito, retención, base y valor facturado, además de StatusField y MessageField por fila.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteJournalVoucherDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si RowColumns < 6 → Fila marcada como inválida (estructura).; si Cuenta contable inexistente / Status=0 / AllowsMovement=0 → Fila marcada como inválida con motivo correspondiente. else Se asignan MainAccountId y nombre formateado.; si MainAccounts.HandlesThirdParty = 1 → Se valida el Nit del tercero contra Common.ThirdParty. else Se ignora el tercero (ThirdPartyId=NULL).; si MainAccounts.HandlesCostCenter = 1 → Se valida el código de centro de costo contra Payroll.CostCenter. else Se ignora el centro de costo (CostCenterId=NULL).; si StringNature = 1 → El valor numérico se asigna a DebitValue. else Si StringNature = 2 se asigna a CreditValue; otro valor invalida la fila.; si MainAccounts.RetencionType <> 0 → Se valida el concepto de retención y se exigen base y valor facturado. else No se valida retención y los campos asociados quedan en NULL.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteJournalVoucherDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.MainAccounts; Common.ThirdParty; Payroll.CostCenter; GeneralLedger.RetentionConcepts', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteJournalVoucherDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteJournalVoucherDetails';
-- GO
