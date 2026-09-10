

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de nómina que valida e importa reglas de reconocimiento automático bancario a partir de un archivo XML (función de copiar/pegar o carga masiva). Procesa cada fila del XML verificando que el concepto de nota de tesorería exista en Treasury.NoteConcepts (con su cuenta contable asociada en GeneralLedger.MainAccounts), que el centro de costo sea válido y esté vinculado a la cuenta contable correspondiente, y que la descripción de transacción y el código de regla bancaria cumplan el formato requerido. Devuelve el resultado fila por fila indicando si cada registro es válido o contiene errores, con mensajes descriptivos para que el usuario pueda corregir la información antes de su grabación definitiva en las reglas de reconocimiento automático del banco en el módulo de Nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_SetBankAutomaticRecognitionRules';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_SetBankAutomaticRecognitionRules';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida un lote XML de reglas de reconocimiento bancario automático (código, descripción de transacción, concepto de nota y centro de costo) y devuelve cada fila enriquecida con identificadores resueltos y mensajes de error.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SetBankAutomaticRecognitionRules';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe seguir la estructura /Data/Row con los nodos RowIndex, RowColumns, CodeBankAutomaticRecognitionRules, DescriptionTransaction, NoteConceptCode y CostCenterCode.; Cada fila debe traer al menos 3 columnas (RowColumns >= 3).; RowIndex debe ser único dentro del lote.; El NoteConceptCode debe existir en Treasury.NoteConcepts.; Si se informa CostCenterCode, debe existir en Payroll.CostCenter y estar asociado a la cuenta contable (MainAccount) del concepto de nota vía GeneralLedger.MainAccountRestrictions.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SetBankAutomaticRecognitionRules';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada validación posterior solo se aplica sobre filas que aún están en estado pendiente (StatusField=0), preservando el primer error detectado.; StatusField=999 indica fila inválida; cualquier otro valor (0) representa fila válida.; El centro de costo solo se considera válido si está enlazado a la cuenta contable principal del concepto de nota mediante MainAccountRestrictions.; Los códigos descriptivos devueltos (NoteConceptCode, CostCenterCode, MainAccountNumberName) se reemplazan por el formato ''codigo - nombre'' cuando la entidad existe.; El procedimiento nunca persiste cambios en tablas físicas; solo trabaja sobre una tabla variable y devuelve el resultado.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SetBankAutomaticRecognitionRules';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reglas de reconocimiento bancario automático; Concepto de nota de tesorería; Cuenta contable principal (PUC); Restricciones de cuenta contable; Centro de costo de nómina; Descripción de transacción bancaria; Carga/importación masiva por XML', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SetBankAutomaticRecognitionRules';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableXmlObject: Inserta una fila por cada nodo /Data/Row del XML con sus valores extraídos y StatusField=0 (pendiente de validación).; [UPDATE] @TableXmlObject: Si RowColumns < 3 marca StatusField=999 con mensaje ''El registro X no tiene la estructura válida''.; [UPDATE] @TableXmlObject: Si existen filas con el mismo RowIndex, marca StatusField=999 con mensaje ''El registro X se encuentra duplicado''.; [UPDATE] @TableXmlObject: Si NoteConceptCode no existe en Treasury.NoteConcepts marca StatusField=999 con mensaje ''El concepto de nota con codigo ... no existe''; si existe, completa NoteConceptCode (code - description), NoteConceptId, MainAccountId y MainAccountNumberName desde GeneralLedger.MainAccounts.; [UPDATE] @TableXmlObject: Si CostCenterCode no es vacío y no se encuentra en Payroll.CostCenter ligado al MainAccountId via GeneralLedger.MainAccountRestrictions, marca StatusField=999 con mensaje ''... no existe o no está asociado a la cuenta contable dispuesta''; si existe, completa CostCenterCode (code - Name) y CostCenterId.; [UPDATE] @TableXmlObject: Si DescriptionTransaction está vacío o LEN > 20 marca StatusField=999 con mensaje ''La Descripción de Transacción ... no es válida''.; [UPDATE] @TableXmlObject: Si CodeBankAutomaticRecognitionRules está vacío o LEN > 4 marca StatusField=999 con mensaje ''El código ... no es válido''.; [RETURN_RESULT] @TableXmlObject: Retorna un SELECT con todas las filas validadas, sus identificadores resueltos (NoteConceptId, MainAccountId, CostCenterId), nombres compuestos y StatusField/MessageField.; [DELETE] @TableXmlObject: Ante una excepción capturada en CATCH borra el contenido y registra una única fila con RowIndex=0, StatusField=999 y MessageField conteniendo ERROR_MESSAGE() + línea.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SetBankAutomaticRecognitionRules';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si RowColumns < 3 → Marca la fila como inválida (estructura no válida). else Continúa con las siguientes validaciones.; si Existen filas duplicadas por RowIndex → Marca las filas duplicadas como inválidas.; si NoteConceptCode no se resuelve en Treasury.NoteConcepts → Marca la fila como inválida con mensaje de concepto inexistente. else Carga NoteConceptId, MainAccountId y descriptivos.; si CostCenterCode informado y no asociado al MainAccount via MainAccountRestrictions → Marca la fila como inválida. else Asigna CostCenterId y descriptivo.; si Las validaciones siguientes solo aplican si StatusField = 0 → Cada UPDATE de validación filtra por StatusField=0 para no sobrescribir errores previos.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SetBankAutomaticRecognitionRules';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.NoteConcepts; GeneralLedger.MainAccounts; GeneralLedger.MainAccountRestrictions; Payroll.CostCenter', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SetBankAutomaticRecognitionRules';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SetBankAutomaticRecognitionRules';
-- GO
