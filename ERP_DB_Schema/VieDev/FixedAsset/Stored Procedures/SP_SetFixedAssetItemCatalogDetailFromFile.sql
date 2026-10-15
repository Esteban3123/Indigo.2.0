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