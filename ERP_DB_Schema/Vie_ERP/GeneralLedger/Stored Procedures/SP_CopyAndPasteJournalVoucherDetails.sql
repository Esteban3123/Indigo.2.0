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