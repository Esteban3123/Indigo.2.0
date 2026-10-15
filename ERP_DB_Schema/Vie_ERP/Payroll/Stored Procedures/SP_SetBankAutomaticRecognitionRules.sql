

-- =====================================================
-- Author:		Cristian camilo Fierro Rojas
-- Create date: 2024-01-17
-- Description:	Procedimiento que se encarga de copiar y pegar o la importacion de archivo para los detalles de banco
-- =====================================================
CREATE PROCEDURE [Payroll].[SP_SetBankAutomaticRecognitionRules]
    @XmlObject  XML	
AS
BEGIN
	SET NOCOUNT ON

	--Tabla para almacenar los items del listado que viene en el xml y los resultados a devolver
	DECLARE @TableXmlObject TABLE
	(       
			RowIndex INT NOT NULL,
		    RowColumns INT NOT NULL,	
			CodeBankAutomaticRecognitionRules varchar(4),
			DescriptionTransaction varchar(20),
			NoteConceptCode varchar(275),		
			CostCenterCode varchar(MAX),			
			
			-------------------------------------------------------
			NoteConceptId int,
			MainAccountId int,
			CostCenterId int,
			MainAccountNumberName varchar(150),
			--------------------------------------------------------
			StatusField INT DEFAULT(0), --Estado pendiente de validación
		    MessageField VARCHAR(MAX)
    )	

	BEGIN TRY
		--Se obtiene los detalles que vienen en el xml
			INSERT INTO @TableXmlObject
			(
			RowIndex, RowColumns,
			CodeBankAutomaticRecognitionRules, DescriptionTransaction,
            NoteConceptCode, CostCenterCode--,MainAccountNumber
			)
				SELECT	t.x.value('RowIndex[1]','int') as RowIndex,
						t.x.value('RowColumns[1]','int') as RowColumns,						
						t.x.value('CodeBankAutomaticRecognitionRules[1]','varchar(4)') AS CodeBankAutomaticRecognitionRules ,
						t.x.value('DescriptionTransaction[1]','varchar(20)') AS DescriptionTransaction ,
						t.x.value('NoteConceptCode[1]','varchar(20)') AS NoteConceptCode,
						t.x.value('CostCenterCode[1]','varchar(20)') AS CostCenterCode

				FROM @XmlObject.nodes('/Data/Row') t(x)

				/***********************************************  VALIDACIONES ***********************************************/

		-- Validar el numero de columnas de acuerdo al tipo de documento
		UPDATE tx
			SET tx.StatusField = 999,
				tx.MessageField = CONCAT('El registro ', tx.RowIndex, ' no tiene la estructura válida')
		FROM @TableXmlObject tx
		WHERE tx.StatusField = 0 AND tx.RowColumns < 3

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

		--- Validar conceptos de Notas
		UPDATE tx 
		    SET tx.StatusField = IIF(txd.MessageField IS NOT NULL, 999, tx.StatusField),
				tx.MessageField = txd.MessageField,
				---------------------------------------------------------------
				tx.NoteConceptCode = ISNULL(txd.NoteConceptCode,tx.NoteConceptCode),
				tx.NoteConceptId = txd.NoteConceptId,
				tx.MainAccountId = txd.MainAccountId,
				tx.MainAccountNumberName = txd.MainAccountNumberName
		FROM @TableXmlObject tx
		JOIN
		(
			SELECT tx.RowIndex,
			       CONCAT('El concepto de nota con codigo ', tx.NoteConceptCode, ' del registro ', tx.RowIndex, ' ' ) + CASE 
						WHEN nc.id IS NULL then 'no existe'
			       END MessageField,
			       IIF(nc.Code IS NOT NULL OR nc.Description IS NOT NULL, CONCAT(nc.code, ' - ', nc.Description), NULL) NoteConceptCode,
				   nc.Id NoteConceptId,
				   ma.Id MainAccountId,
				   iif(ma.Number IS NOT NULL OR ma.Name IS NOT NULL, CONCAT(ma.Number, ' - ', ma.Name), NULL) MainAccountNumberName
			FROM @TableXmlObject tx
			LEFT JOIN Treasury.NoteConcepts nc ON nc.Code = tx.NoteConceptCode
		    LEFT JOIN GeneralLedger.MainAccounts ma ON   ma.Id = nc.IdMainAccount 
			WHERE tx.StatusField = 0
		) txd ON tx.RowIndex = txd.RowIndex
		
		
		--- Validar centro de costo
		UPDATE tx 
		    SET tx.StatusField = IIF(txd.MessageField IS NOT NULL, 999, tx.StatusField),
				tx.MessageField = txd.MessageField,
				-----------------------------------------------------------------------
				tx.CostCenterCode = txd.CostCenterCode,
				tx.CostCenterId = txd.CostCenterId
		FROM @TableXmlObject tx
		JOIN
		(		
			SELECT tx.RowIndex,				
				CONCAT('El Código del centro de costo ', tx.CostCenterCode, ' del registro ', tx.RowIndex, ' ' ) + CASE
					WHEN  cm.Id IS NULL THEN ' no existe o no está asociado a la cuenta contable dispuesta'
				END MessageField,
				ISNULL(CostCenterCodeName, tx.CostCenterCode) CostCenterCode,
				cm.Id CostCenterId
		from @TableXmlObject tx		
		LEFT JOIN (SELECT concat(cc.code, ' - ', cc.Name) CostCenterCodeName, mar.MainAccountId, cc.Id, cc.Code FROM GeneralLedger.MainAccountRestrictions mar
					JOIN Payroll.CostCenter cc on cc.Id = mar.CostCenterId) cm on cm.MainAccountId = tx.MainAccountId AND cm.Code = tx.CostCenterCode 
		WHERE tx.StatusField = 0 AND tx.CostCenterCode <> ''
		) txd ON tx.RowIndex = txd.RowIndex	
		
		-- Validar las descripcion de transación 
		UPDATE tx
			SET tx.StatusField = 999,
				tx.MessageField = CONCAT('La Descripción de Transacción ', tx.DescriptionTransaction, ' del registro ', tx.RowIndex, ' no es válida')
		FROM @TableXmlObject tx
		WHERE tx.StatusField = 0 AND ISNULL(tx.DescriptionTransaction, '') LIKE '' OR LEN(ISNULL(tx.DescriptionTransaction, '')) > 20

		-- Validar las descripcion de transación 
		UPDATE tx
			SET tx.StatusField = 999,
				tx.MessageField = CONCAT('El código  ', tx.CodeBankAutomaticRecognitionRules, ' del registro ', tx.RowIndex, ' no es válido')
		FROM @TableXmlObject tx
		WHERE tx.StatusField = 0 AND ISNULL(tx.CodeBankAutomaticRecognitionRules, '') LIKE '' OR LEN(ISNULL(tx.CodeBankAutomaticRecognitionRules, '')) > 4


	END TRY
	BEGIN CATCH
		DELETE FROM @TableXmlObject
		INSERT INTO @TableXmlObject(RowIndex, RowColumns,StatusField, MessageField)
		VALUES (0, 0, 999, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(5)))
	END CATCH

	SELECT CodeBankAutomaticRecognitionRules ,
			DescriptionTransaction ,
			NoteConceptCode ,		
			CostCenterCode ,			
			
			-------------------------------------------------------
			NoteConceptId ,
			MainAccountId,
			CostCenterId,
			MainAccountNumberName ,
			--------------------------------------------------------
			StatusField ,
		    MessageField 
	FROM @TableXmlObject

END