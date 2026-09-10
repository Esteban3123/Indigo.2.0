-- =============================================
-- Author:		Andrés Steven Rojas 
-- Create date: 08/04/2024
-- Description:	Store Procedure que valida de Excel
-- =============================================
CREATE PROCEDURE [FixedAsset].[SP_SetFixedAssetItemCatalogDetailFromFile]
	@XmlObject Xml
AS 
BEGIN
	SET NOCOUNT ON;
	
	-- TABLA DEL XML
	DECLARE @TableXmlObject TABLE
		(
			AccountingStructure VARCHAR(50),
			LoanSpendAccount VARCHAR(50),
			LoanLeasingSpendAccount VARCHAR(50),
			ExpenseLoanAccount VARCHAR(50),
			LoanFinancialRentingAccount VARCHAR(50),
			CostCenter VARCHAR(50),
			DistributionPercentage DECIMAL(8, 4)
		)

	-- Datos correctos Depreciación por Distribución
	DECLARE @DatosCorrectos TABLE
		(
			Id INT IDENTITY PRIMARY KEY,
			LoanSpendAccount VARCHAR(50),
			LoanSpendAccountId INT,
			LoanLeasingSpendAccount VARCHAR(50),
			LoanLeasingSpendAccountId INT,
			ExpenseLoanAccount VARCHAR(50),
			ExpenseLoanAccountId INT,
			LoanFinancialRentingAccount VARCHAR(50),
			LoanFinancialRentingAccountId INT,
			CostCenter VARCHAR(50),
			CostCenterId INT,
			DistributionPercentage DECIMAL(8, 4)
		)

	-- TABLA DE RETORNO
	DECLARE @ReturnTable TABLE
	(
		Id INT IDENTITY PRIMARY KEY, 
		StatusField varchar(3),
		AccountingStructureId int,
		LoanSpendAccountId int,
		LoanLeasingSpendAccountId int,
		ExpenseLoanAccountId int,
		LoanFinancialRentingAccountId int,
		CostCenterId int,
		DistributionPercentage decimal(20, 4),
		CodeNameAccountingStructure varchar(255),
		NumberNameLoanSpendAccountingAccount varchar(255),
		NumberNameLoanLeasingAccountingAccount varchar(255),
		ExpenseLoanAccountDescription varchar(255),
		LoanFinancialRentingAccountDescription varchar(255),
		NumberNameCostCenter varchar(255),
		MessageField varchar(max)
	)

	BEGIN TRY

		-- INSERT A LA TABLA XML
		INSERT INTO @TableXmlObject
		(
			LoanSpendAccount,
			LoanLeasingSpendAccount,
			ExpenseLoanAccount,
			LoanFinancialRentingAccount,
			CostCenter,
			DistributionPercentage
		)
		SELECT
			t.x.value('LoanSpendAccount[1]','varchar(50)') as LoanSpendAccount,
			t.x.value('LoanLeasingSpendAccount[1]','varchar(50)') as LoanLeasingSpendAccount,
			t.x.value('ExpenseLoanAccount[1]','varchar(50)') as ExpenseLoanAccount,
			t.x.value('LoanFinancialRentingAccount[1]','varchar(50)') as LoanFinancialRentingAccount,
			t.x.value('CostCenter[1]','varchar(50)') as CostCenter,
			t.x.value('DistributionPercentage[1]','decimal(8, 4)') as DistributionPercentage
		FROM @XmlObject.nodes('/Data/Row') t(x);

		-- VALIDANDO EXISTENCIA DE DATOS
		INSERT INTO @DatosCorrectos
			(
				LoanSpendAccount,
				LoanSpendAccountId,
				LoanLeasingSpendAccount,
				LoanLeasingSpendAccountId,
				ExpenseLoanAccount,
				ExpenseLoanAccountId,
				LoanFinancialRentingAccount,
				LoanFinancialRentingAccountId,
				CostCenter,
				CostCenterId,
				DistributionPercentage
			)
			SELECT 
				t.LoanSpendAccount,
				MAX(mSpend.Id) AS LoanSpendAccountId,
				t.LoanLeasingSpendAccount,
				MAX(mLeasing.Id) AS LoanLeasingSpendAccountId,
				t.ExpenseLoanAccount,
				MAX(mExpense.Id) AS ExpenseLoanAccountId,
				t.LoanFinancialRentingAccount,
				MAX(mRenting.Id) AS LoanFinancialRentingAccountId,
				t.CostCenter,
				MAX(cc.Id) AS CostCenterId,
				t.DistributionPercentage
			FROM @TableXmlObject t
			INNER JOIN Payroll.CostCenter cc ON t.CostCenter = cc.Code
			LEFT JOIN GeneralLedger.MainAccounts mSpend ON t.LoanSpendAccount = mSpend.Number
			LEFT JOIN GeneralLedger.MainAccounts mLeasing ON t.LoanLeasingSpendAccount = mLeasing.Number
			LEFT JOIN GeneralLedger.MainAccounts mExpense ON t.ExpenseLoanAccount = mExpense.Number
			LEFT JOIN GeneralLedger.MainAccounts mRenting ON t.LoanFinancialRentingAccount = mRenting.Number
			WHERE (mSpend.AllowsMovement = 1 OR mLeasing.AllowsMovement = 1 OR mExpense.AllowsMovement = 1 OR mRenting.AllowsMovement = 1)
			AND (mSpend.Status = 1 OR mLeasing.Status = 1 OR mExpense.Status = 1 OR mRenting.Status = 1)
			AND cc.State = 1
			GROUP BY 
				t.LoanSpendAccount,
				t.LoanLeasingSpendAccount,
				t.ExpenseLoanAccount,
				t.LoanFinancialRentingAccount,
				t.CostCenter,
				t.DistributionPercentage
												   

		-- REALIZANDO INSERT EN LA TABLA RETORNO
		INSERT INTO @ReturnTable
			( 
				StatusField,
				LoanSpendAccountId,
				LoanLeasingSpendAccountId,
				ExpenseLoanAccountId,
				LoanFinancialRentingAccountId,
				CostCenterId,
				DistributionPercentage,
				NumberNameLoanSpendAccountingAccount,
				NumberNameLoanLeasingAccountingAccount,
				ExpenseLoanAccountDescription,
				LoanFinancialRentingAccountDescription,
				NumberNameCostCenter,
				MessageField
			)
			SELECT 
				CASE 
					WHEN mar.MainAccountId IS NOT NULL THEN '333'  -- Si existe una restricción
					ELSE '001'  -- Si no hay restricciones
				END AS StatusField,
				dc.LoanSpendAccountId,
				dc.LoanLeasingSpendAccountId,
				dc.ExpenseLoanAccountId,
				dc.LoanFinancialRentingAccountId,
				dc.CostCenterId,
				dc.DistributionPercentage,
				mSpend.Number + ' - ' + mSpend.Name AS NumberNameLoanSpendAccountingAccount,
				mLeasing.Number + ' - ' + mLeasing.Name AS NumberNameLoanLeasingAccountingAccount,
				mExpense.Number + ' - ' + mExpense.Name AS ExpenseLoanAccountDescription,
				mRenting.Number + ' - ' + mRenting.Name AS LoanFinancialRentingAccountDescription,
				cc.Code + ' - ' + cc.Name AS NumberNameCostCenter,
				CASE 
					WHEN mar.MainAccountId IS NOT NULL THEN 'Esta cuenta tiene restricción con el Centro de Costo'
					ELSE 'Proceso Exitoso'
				END AS MessageField
			FROM @DatosCorrectos dc
			LEFT JOIN GeneralLedger.MainAccounts mSpend ON dc.LoanSpendAccountId = mSpend.Id
			LEFT JOIN GeneralLedger.MainAccounts mLeasing ON dc.LoanLeasingSpendAccountId = mLeasing.Id
			LEFT JOIN GeneralLedger.MainAccounts mExpense ON dc.ExpenseLoanAccountId = mExpense.Id
			LEFT JOIN GeneralLedger.MainAccounts mRenting ON dc.LoanFinancialRentingAccountId = mRenting.Id
			INNER JOIN Payroll.CostCenter cc ON dc.CostCenterId = cc.Id
			LEFT JOIN GeneralLedger.MainAccountRestrictions mar ON dc.LoanSpendAccountId = mar.MainAccountId
				AND dc.CostCenterId = mar.CostCenterId
				AND mar.RestrictionType = 2;

			-- VALIDANDO DATOS NO ENCONTRADOS
			IF (SELECT COUNT(*) FROM @ReturnTable) <> (SELECT COUNT(*) FROM @TableXmlObject)
			BEGIN
				-- Declarar una tabla temporal para almacenar los datos no encontrados
				DECLARE @DatosNoEncontrados TABLE (
					CostCenter VARCHAR(50))

				-- Insertar en la tabla temporal los datos no encontrados
				INSERT INTO @DatosNoEncontrados
				SELECT 
					txo.CostCenter
				FROM 
					@TableXmlObject txo
				WHERE 
					NOT EXISTS (SELECT 1 FROM @DatosCorrectos dc 
								WHERE txo.CostCenter = dc.CostCenter
								   OR txo.LoanSpendAccount = dc.LoanSpendAccount
								   OR txo.LoanLeasingSpendAccount = dc.LoanSpendAccount
								   OR txo.ExpenseLoanAccount = dc.ExpenseLoanAccount
								   OR txo.LoanFinancialRentingAccount = dc.LoanFinancialRentingAccount)
							
				-- Insertar en @ReturnTable los datos no encontrados
				INSERT INTO @ReturnTable (StatusField, MessageField, NumberNameCostCenter)
				SELECT '777', 'Alguno de los datos de este Centro de Costos o el Centro de Costos es incorrecto', 
					   dne.CostCenter
				FROM @DatosNoEncontrados dne
			END

		SELECT * FROM @ReturnTable
		RETURN

	END TRY
	BEGIN CATCH
		-- GESTIÓN DE ERRORES
		INSERT INTO @ReturnTable (StatusField, MessageField)
				SELECT '999', ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10))
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que valida y procesa la carga masiva desde un archivo Excel (en formato XML) del detalle del catálogo de ítems de activos fijos, específicamente la configuración de depreciación por distribución. Lee cada fila del archivo y cruza las cuentas contables del libro mayor (cuenta de gasto de préstamo, leasing, renting financiero y gasto de préstamo de expensas) contra las cuentas principales activas de contabilidad general (GeneralLedger.MainAccounts), y el centro de costo contra los centros de costo de nómina (Payroll.CostCenter) activos. Retorna un conjunto de resultados con el estado de cada registro (''001'' si es válido o ''333'' si la cuenta tiene restricción con el centro de costo), los identificadores resueltos de cada cuenta y centro de costo, sus descripciones legibles (número y nombre), y un mensaje de resultado por fila, permitiendo al usuario verificar la correcta importación antes de persistir el catálogo de activos fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_SetFixedAssetItemCatalogDetailFromFile';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_SetFixedAssetItemCatalogDetailFromFile';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida un lote de filas (provenientes de un Excel en XML) que asocian cuentas contables de gasto/leasing/renting con centros de costo y porcentaje de distribución, retornando estado por fila según existencia y restricciones.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SetFixedAssetItemCatalogDetailFromFile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe seguir la estructura /Data/Row con los nodos LoanSpendAccount, LoanLeasingSpendAccount, ExpenseLoanAccount, LoanFinancialRentingAccount, CostCenter y DistributionPercentage.; El centro de costo enviado debe existir en Payroll.CostCenter y estar en estado activo (State=1).; Al menos una de las cuentas contables referenciadas debe existir en GeneralLedger.MainAccounts con AllowsMovement=1 y Status=1.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SetFixedAssetItemCatalogDetailFromFile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se aceptan como válidas filas cuyo centro de costo está activo (State=1) en Payroll.CostCenter.; Solo se consideran cuentas contables que permiten movimiento (AllowsMovement=1) y están activas (Status=1).; Las restricciones se evalúan únicamente sobre la cuenta de gasto (LoanSpendAccount) frente al centro de costo, con RestrictionType=2.; El procedimiento nunca modifica datos persistentes; solo lee y devuelve un result set en variables tabla.; Cada fila retornada lleva un StatusField codificado: ''001'' éxito, ''333'' restricción, ''777'' dato no encontrado, ''999'' error de ejecución.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SetFixedAssetItemCatalogDetailFromFile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de costo; Cuenta contable principal (PUC); Cuenta de gasto por préstamo; Cuenta de leasing; Cuenta de renting financiero; Restricción cuenta-centro de costo; Porcentaje de distribución; Carga desde Excel/XML; Activos fijos', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SetFixedAssetItemCatalogDetailFromFile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @ReturnTable: Cuando existe coincidencia válida y NO hay restricción en GeneralLedger.MainAccountRestrictions (RestrictionType=2) entre la cuenta de gasto y el centro de costo, se inserta fila con StatusField=''001'' y MessageField=''Proceso Exitoso''.; [INSERT] @ReturnTable: Cuando existe registro en GeneralLedger.MainAccountRestrictions con RestrictionType=2 para el par (LoanSpendAccountId, CostCenterId), se inserta fila con StatusField=''333'' y mensaje ''Esta cuenta tiene restricción con el Centro de Costo''.; [INSERT] @ReturnTable: Cuando el conteo de filas válidas difiere del total del XML, por cada fila no encontrada se inserta StatusField=''777'' con mensaje ''Alguno de los datos de este Centro de Costos o el Centro de Costos es incorrecto''.; [INSERT] @ReturnTable: En caso de excepción (CATCH) se inserta StatusField=''999'' con ERROR_MESSAGE() y la línea del error.; [RETURN_RESULT] @ReturnTable: Al finalizar el flujo se devuelve el contenido de @ReturnTable como result set.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SetFixedAssetItemCatalogDetailFromFile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si mar.MainAccountId IS NOT NULL (existe restricción RestrictionType=2 entre cuenta de gasto y centro de costo) → Marca la fila como Status ''333'' con mensaje de restricción else Marca la fila como Status ''001'' / ''Proceso Exitoso''; si Conteo(@ReturnTable) <> Conteo(@TableXmlObject) (hay filas del XML que no quedaron como datos correctos) → Inserta filas adicionales con Status ''777'' por cada centro de costo no encontrado/incorrecto; si Cuenta cumple AllowsMovement=1 y Status=1 y CostCenter.State=1 → La fila se considera ''dato correcto'' y entra a @DatosCorrectos else La fila se descarta del conjunto válido y será reportada como ''777''', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SetFixedAssetItemCatalogDetailFromFile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.CostCenter; GeneralLedger.MainAccounts; GeneralLedger.MainAccountRestrictions', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SetFixedAssetItemCatalogDetailFromFile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SetFixedAssetItemCatalogDetailFromFile';
-- GO
