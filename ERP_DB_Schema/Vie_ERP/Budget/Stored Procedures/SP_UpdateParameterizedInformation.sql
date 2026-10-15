
-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-12-14
-- Description:	Procedimiento que se encarga de actualizar la información parametrizada
-- =============================================
CREATE PROCEDURE [Budget].[SP_UpdateParameterizedInformation]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @ValidityIdDestiny INT,
			@Dependencies BIT,
			@Categories BIT,
			@BudgetHeaderIdDestiny INT,
			@UserCode VARCHAR(20)

	DECLARE @Result_Table AS TABLE
	(
		Code INT,
		Message VARCHAR(MAX)
	)

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		--Se obtienen los datos de los criterios
		SELECT	@ValidityIdDestiny = t.x.value('ValidityIdDestiny[1]','int'),			
				@Dependencies = t.x.value('Dependencies[1]','bit'),
				@Categories = t.x.value('Categories[1]','bit'),
				@UserCode = t.x.value('UserCode[1]','varchar(20)')
		FROM @xmlCriterias.nodes('/Data') t(x)

		/********************************  ACTUALIZAR DEPENDECIAS ********************************/

		IF @Dependencies = 1
		BEGIN
			INSERT INTO @Result_Table
				SELECT 999, 'La dependencia de los parámetros de facturación de la unidad operativa ' + ou.UnitCode + ' - ' + ou.UnitName + ' no esta homologada'
				FROM Billing.SettingsBilling sb
				JOIN Common.OperatingUnit ou ON sb.IdOperatingUnit = ou.Id
				JOIN Budget.Dependency d ON sb.DependencyId = d.Id
				LEFT JOIN Budget.Dependency dd ON d.Code = dd.Code AND dd.BudgetaryValidityId = @ValidityIdDestiny
				WHERE dd.Id IS NULL

			INSERT INTO @Result_Table
				SELECT 999, 'La dependencia de factura básica de los parámetros de facturación de la unidad operativa ' + ou.UnitCode + ' - ' + ou.UnitName + ' no esta homologada'
				FROM Billing.SettingsBilling sb
				JOIN Common.OperatingUnit ou ON sb.IdOperatingUnit = ou.Id
				JOIN Budget.Dependency d ON sb.BasicBillingDependencyId = d.Id
				LEFT JOIN Budget.Dependency dd ON d.Code = dd.Code AND dd.BudgetaryValidityId = @ValidityIdDestiny
				WHERE dd.Id IS NULL

			INSERT INTO @Result_Table
				SELECT 999, 'La dependencia de los parámetros de cuentas por cobrar de la unidad operativa ' + ou.UnitCode + ' - ' + ou.UnitName + ' no esta homologada'
				FROM Portfolio.SettingPortfolio sp
				JOIN Common.OperatingUnit ou ON sp.OperatingUnitId = ou.Id
				JOIN Budget.Dependency d ON sp.DependencyId = d.Id
				LEFT JOIN Budget.Dependency dd ON d.Code = dd.Code AND dd.BudgetaryValidityId = @ValidityIdDestiny
				WHERE dd.Id IS NULL

			/********************************** ACTUALIZACIONES **********************************/

			UPDATE sb
				SET sb.DependencyId = dd.Id
			FROM Billing.SettingsBilling sb
			JOIN Budget.Dependency d ON sb.DependencyId = d.Id
			JOIN Budget.Dependency dd ON d.Code = dd.Code AND dd.BudgetaryValidityId = @ValidityIdDestiny

			UPDATE sb
				SET sb.BasicBillingDependencyId = dd.Id
			FROM Billing.SettingsBilling sb
			JOIN Budget.Dependency d ON sb.BasicBillingDependencyId = d.Id
			JOIN Budget.Dependency dd ON d.Code = dd.Code AND dd.BudgetaryValidityId = @ValidityIdDestiny

			UPDATE sp
				SET sp.DependencyId = dd.Id
			FROM Portfolio.SettingPortfolio sp
			JOIN Budget.Dependency d ON sp.DependencyId = d.Id
			JOIN Budget.Dependency dd ON d.Code = dd.Code AND dd.BudgetaryValidityId = @ValidityIdDestiny
		END

		/*********************************** ACTUALIZAR RUBROS ***********************************/

		IF @Categories = 1
		BEGIN
			INSERT INTO [Budget].[BudgetHeader]
			(
				[BudgetaryValidityId],[Type],[Status],[CreationUser],[CreationDate]
			)
				SELECT bv.Id, 1, 1, @UserCode, [Common].[GETDATE]()
				FROM Budget.BudgetaryValidity bv
				LEFT JOIN Budget.BudgetHeader bh ON bv.Id = bh.BudgetaryValidityId AND bh.Type = 1
				WHERE bv.Id = @ValidityIdDestiny AND bh.Id IS NULL

			SELECT @BudgetHeaderIdDestiny = bh.Id
			FROM Budget.BudgetaryValidity bv
			JOIN Budget.BudgetHeader bh ON bv.Id = bh.BudgetaryValidityId AND bh.Type = 1
			WHERE bv.Id = @ValidityIdDestiny

			/****************************** VALIDAR TIPO DE INGRESO ******************************/

			INSERT INTO @Result_Table
				SELECT 999, 'El tipo de ingreso de factura básica de los parámetros de facturación de la unidad operativa ' + ou.UnitCode + ' - ' + ou.UnitName + ' no esta homologada'
				FROM Billing.SettingsBilling sb
				JOIN Common.OperatingUnit ou ON sb.IdOperatingUnit = ou.Id
				JOIN Budget.Budget b ON sb.BasicBillingBudgetId = b.Id
				JOIN Budget.RevenueType rt ON b.RevenueTypeId = rt.Id
				LEFT JOIN Budget.RevenueType rtd ON rt.Type = rtd.Type AND rt.Code = rtd.Code AND rtd.BudgetaryValidityId = @ValidityIdDestiny
				WHERE rtd.Id IS NULL

			INSERT INTO @Result_Table
				SELECT 999, 'El tipo de ingreso de facturas del grupo de atención ' + cg.Code + ' - ' + cg.Name + ' no esta homologada'
				FROM Contract.CareGroup cg
				JOIN Budget.Budget b ON cg.BillingBudgetId = b.Id
				JOIN Budget.RevenueType rt ON b.RevenueTypeId = rt.Id
				LEFT JOIN Budget.RevenueType rtd ON rt.Type = rtd.Type AND rt.Code = rtd.Code AND rtd.BudgetaryValidityId = @ValidityIdDestiny
				WHERE rtd.Id IS NULL

			INSERT INTO @Result_Table
				SELECT 999, 'El tipo de ingreso de pagarés del grupo de atención ' + cg.Code + ' - ' + cg.Name + ' no esta homologada'
				FROM Contract.CareGroup cg
				JOIN Budget.Budget b ON cg.PromissoryNoteBudgetId = b.Id
				JOIN Budget.RevenueType rt ON b.RevenueTypeId = rt.Id
				LEFT JOIN Budget.RevenueType rtd ON rt.Type = rtd.Type AND rt.Code = rtd.Code AND rtd.BudgetaryValidityId = @ValidityIdDestiny
				WHERE rtd.Id IS NULL

			INSERT INTO @Result_Table
				SELECT 999, 'El tipo de ingreso de cuentas por cobrar vigencia anterior del grupo de atención ' + cg.Code + ' - ' + cg.Name + ' no esta homologada'
				FROM Contract.CareGroup cg
				JOIN Budget.Budget b ON cg.AccountReceivablePreviousValidityBudgetId = b.Id
				JOIN Budget.RevenueType rt ON b.RevenueTypeId = rt.Id
				LEFT JOIN Budget.RevenueType rtd ON rt.Type = rtd.Type AND rt.Code = rtd.Code AND rtd.BudgetaryValidityId = @ValidityIdDestiny
				WHERE rtd.Id IS NULL

			INSERT INTO @Result_Table
				SELECT 999, 'El tipo de ingreso de recuperación de cartera del grupo de atención ' + cg.Code + ' - ' + cg.Name + ' no esta homologada'
				FROM Contract.CareGroup cg
				JOIN Budget.Budget b ON cg.PortfolioRecoveryBudgetId = b.Id
				JOIN Budget.RevenueType rt ON b.RevenueTypeId = rt.Id
				LEFT JOIN Budget.RevenueType rtd ON rt.Type = rtd.Type AND rt.Code = rtd.Code AND rtd.BudgetaryValidityId = @ValidityIdDestiny
				WHERE rtd.Id IS NULL

			/**********************************  VALIDAR RUBROS **********************************/

			INSERT INTO @Result_Table
				SELECT 999, 'El rubro de factura básica de los parámetros de facturación de la unidad operativa ' + ou.UnitCode + ' - ' + ou.UnitName + ' no esta homologada'
				FROM Billing.SettingsBilling sb
				JOIN Common.OperatingUnit ou ON sb.IdOperatingUnit = ou.Id
				JOIN Budget.Budget b ON sb.BasicBillingBudgetId = b.Id
				JOIN Budget.Category c ON b.CategoryId = c.Id
				JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
				JOIN Budget.FinancialSource fsd ON fs.Code = fsd.Code AND fsd.BudgetaryValidityId = @ValidityIdDestiny
				LEFT JOIN Budget.Category cd ON fsd.Id = cd.FinancialSourceId AND c.Code = cd.Code AND cd.BudgetaryValidityId = @ValidityIdDestiny
				WHERE cd.Id IS NULL

			INSERT INTO @Result_Table
				SELECT 999, 'El rubro de facturas del grupo de atención ' + cg.Code + ' - ' + cg.Name + ' no esta homologada'
				FROM Contract.CareGroup cg
				JOIN Budget.Budget b ON cg.BillingBudgetId = b.Id
				JOIN Budget.Category c ON b.CategoryId = c.Id
				JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
				JOIN Budget.FinancialSource fsd ON fs.Code = fsd.Code AND fsd.BudgetaryValidityId = @ValidityIdDestiny
				LEFT JOIN Budget.Category cd ON fsd.Id = cd.FinancialSourceId AND c.Code = cd.Code AND cd.BudgetaryValidityId = @ValidityIdDestiny
				WHERE cd.Id IS NULL

			INSERT INTO @Result_Table
				SELECT 999, 'El rubro de pagarés del grupo de atención ' + cg.Code + ' - ' + cg.Name + ' no esta homologada'
				FROM Contract.CareGroup cg
				JOIN Budget.Budget b ON cg.PromissoryNoteBudgetId = b.Id
				JOIN Budget.Category c ON b.CategoryId = c.Id
				JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
				JOIN Budget.FinancialSource fsd ON fs.Code = fsd.Code AND fsd.BudgetaryValidityId = @ValidityIdDestiny
				LEFT JOIN Budget.Category cd ON fsd.Id = cd.FinancialSourceId AND c.Code = cd.Code AND cd.BudgetaryValidityId = @ValidityIdDestiny
				WHERE cd.Id IS NULL

			INSERT INTO @Result_Table
				SELECT 999, 'El rubro de cuentas por cobrar vigencia anterior del grupo de atención ' + cg.Code + ' - ' + cg.Name + ' no esta homologada'
				FROM Contract.CareGroup cg
				JOIN Budget.Budget b ON cg.AccountReceivablePreviousValidityBudgetId = b.Id
				JOIN Budget.Category c ON b.CategoryId = c.Id
				JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
				JOIN Budget.FinancialSource fsd ON fs.Code = fsd.Code AND fsd.BudgetaryValidityId = @ValidityIdDestiny
				LEFT JOIN Budget.Category cd ON fsd.Id = cd.FinancialSourceId AND c.Code = cd.Code AND cd.BudgetaryValidityId = @ValidityIdDestiny
				WHERE cd.Id IS NULL

			INSERT INTO @Result_Table
				SELECT 999, 'El rubro de recuperación de cartera del grupo de atención ' + cg.Code + ' - ' + cg.Name + ' no esta homologada'
				FROM Contract.CareGroup cg
				JOIN Budget.Budget b ON cg.PortfolioRecoveryBudgetId = b.Id
				JOIN Budget.Category c ON b.CategoryId = c.Id
				JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
				JOIN Budget.FinancialSource fsd ON fs.Code = fsd.Code AND fsd.BudgetaryValidityId = @ValidityIdDestiny
				LEFT JOIN Budget.Category cd ON fsd.Id = cd.FinancialSourceId AND c.Code = cd.Code AND cd.BudgetaryValidityId = @ValidityIdDestiny
				WHERE cd.Id IS NULL

			/*******************************  INSERTAR PRESUPUESTO *******************************/

			INSERT INTO Budget.Budget 
			(
				BudgetHeaderId, CategoryId, RevenueTypeId, 
				InitialValue, DebitValueModification, CreditValueModification, DebitValueTransfer, CreditValueTransfer, TotalBudget, ExecutedValue, SuspendedValue, Balance,
				CreationUser, CreationDate
			)
				SELECT DISTINCT	
						@BudgetHeaderIdDestiny, cd.Id, rtd.Id, 
						0, 0, 0, 0, 0, 0, 0, 0, 0, 
						@UserCode, [Common].[GETDATE]()
				FROM Billing.SettingsBilling sb
				JOIN Budget.Budget b ON sb.BasicBillingBudgetId = b.Id
				JOIN Budget.RevenueType rt ON b.RevenueTypeId = rt.Id
				JOIN Budget.Category c ON b.CategoryId = c.Id
				JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
				-------------------------------------------------------------
				JOIN Budget.RevenueType rtd ON rt.Type = rtd.Type AND rt.Code = rtd.Code AND rtd.BudgetaryValidityId = @ValidityIdDestiny 
				JOIN Budget.FinancialSource fsd ON fs.Code = fsd.Code AND fsd.BudgetaryValidityId = @ValidityIdDestiny
				JOIN Budget.Category cd ON fsd.Id = cd.FinancialSourceId AND c.Code = cd.Code AND cd.BudgetaryValidityId = @ValidityIdDestiny			
				LEFT JOIN Budget.Budget bd ON cd.Id = bd.CategoryId AND rtd.Id= bd.RevenueTypeId
				WHERE bd.Id IS NULL

			INSERT INTO Budget.Budget 
			(
				BudgetHeaderId, CategoryId, RevenueTypeId, 
				InitialValue, DebitValueModification, CreditValueModification, DebitValueTransfer, CreditValueTransfer, TotalBudget, ExecutedValue, SuspendedValue, Balance,
				CreationUser, CreationDate
			)
				SELECT DISTINCT	
						@BudgetHeaderIdDestiny, cd.Id, rtd.Id, 
						0, 0, 0, 0, 0, 0, 0, 0, 0, 
						@UserCode, [Common].[GETDATE]()
				FROM Contract.CareGroup cg
				JOIN Budget.Budget b ON cg.BillingBudgetId = b.Id
				JOIN Budget.RevenueType rt ON b.RevenueTypeId = rt.Id
				JOIN Budget.Category c ON b.CategoryId = c.Id
				JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
				-------------------------------------------------------------
				JOIN Budget.RevenueType rtd ON rt.Type = rtd.Type AND rt.Code = rtd.Code AND rtd.BudgetaryValidityId = @ValidityIdDestiny 
				JOIN Budget.FinancialSource fsd ON fs.Code = fsd.Code AND fsd.BudgetaryValidityId = @ValidityIdDestiny
				JOIN Budget.Category cd ON fsd.Id = cd.FinancialSourceId AND c.Code = cd.Code AND cd.BudgetaryValidityId = @ValidityIdDestiny			
				LEFT JOIN Budget.Budget bd ON cd.Id = bd.CategoryId AND rtd.Id= bd.RevenueTypeId
				WHERE bd.Id IS NULL

			INSERT INTO Budget.Budget 
			(
				BudgetHeaderId, CategoryId, RevenueTypeId, 
				InitialValue, DebitValueModification, CreditValueModification, DebitValueTransfer, CreditValueTransfer, TotalBudget, ExecutedValue, SuspendedValue, Balance,
				CreationUser, CreationDate
			)
				SELECT DISTINCT	
						@BudgetHeaderIdDestiny, cd.Id, rtd.Id, 
						0, 0, 0, 0, 0, 0, 0, 0, 0, 
						@UserCode, [Common].[GETDATE]()
				FROM Contract.CareGroup cg
				JOIN Budget.Budget b ON cg.PromissoryNoteBudgetId = b.Id
				JOIN Budget.RevenueType rt ON b.RevenueTypeId = rt.Id
				JOIN Budget.Category c ON b.CategoryId = c.Id
				JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
				-------------------------------------------------------------
				JOIN Budget.RevenueType rtd ON rt.Type = rtd.Type AND rt.Code = rtd.Code AND rtd.BudgetaryValidityId = @ValidityIdDestiny 
				JOIN Budget.FinancialSource fsd ON fs.Code = fsd.Code AND fsd.BudgetaryValidityId = @ValidityIdDestiny
				JOIN Budget.Category cd ON fsd.Id = cd.FinancialSourceId AND c.Code = cd.Code AND cd.BudgetaryValidityId = @ValidityIdDestiny			
				LEFT JOIN Budget.Budget bd ON cd.Id = bd.CategoryId AND rtd.Id= bd.RevenueTypeId
				WHERE bd.Id IS NULL

			INSERT INTO Budget.Budget 
			(
				BudgetHeaderId, CategoryId, RevenueTypeId, 
				InitialValue, DebitValueModification, CreditValueModification, DebitValueTransfer, CreditValueTransfer, TotalBudget, ExecutedValue, SuspendedValue, Balance,
				CreationUser, CreationDate
			)
				SELECT DISTINCT	
						@BudgetHeaderIdDestiny, cd.Id, rtd.Id, 
						0, 0, 0, 0, 0, 0, 0, 0, 0, 
						@UserCode, [Common].[GETDATE]()
				FROM Contract.CareGroup cg
				JOIN Budget.Budget b ON cg.AccountReceivablePreviousValidityBudgetId = b.Id
				JOIN Budget.RevenueType rt ON b.RevenueTypeId = rt.Id
				JOIN Budget.Category c ON b.CategoryId = c.Id
				JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
				-------------------------------------------------------------
				JOIN Budget.RevenueType rtd ON rt.Type = rtd.Type AND rt.Code = rtd.Code AND rtd.BudgetaryValidityId = @ValidityIdDestiny 
				JOIN Budget.FinancialSource fsd ON fs.Code = fsd.Code AND fsd.BudgetaryValidityId = @ValidityIdDestiny
				JOIN Budget.Category cd ON fsd.Id = cd.FinancialSourceId AND c.Code = cd.Code AND cd.BudgetaryValidityId = @ValidityIdDestiny			
				LEFT JOIN Budget.Budget bd ON cd.Id = bd.CategoryId AND rtd.Id= bd.RevenueTypeId
				WHERE bd.Id IS NULL

			INSERT INTO Budget.Budget 
			(
				BudgetHeaderId, CategoryId, RevenueTypeId, 
				InitialValue, DebitValueModification, CreditValueModification, DebitValueTransfer, CreditValueTransfer, TotalBudget, ExecutedValue, SuspendedValue, Balance,
				CreationUser, CreationDate
			)
				SELECT DISTINCT
						@BudgetHeaderIdDestiny, cd.Id, rtd.Id, 
						0, 0, 0, 0, 0, 0, 0, 0, 0, 
						@UserCode, [Common].[GETDATE]()
				FROM Contract.CareGroup cg
				JOIN Budget.Budget b ON cg.PortfolioRecoveryBudgetId = b.Id
				JOIN Budget.RevenueType rt ON b.RevenueTypeId = rt.Id
				JOIN Budget.Category c ON b.CategoryId = c.Id
				JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
				-------------------------------------------------------------
				JOIN Budget.RevenueType rtd ON rt.Type = rtd.Type AND rt.Code = rtd.Code AND rtd.BudgetaryValidityId = @ValidityIdDestiny 
				JOIN Budget.FinancialSource fsd ON fs.Code = fsd.Code AND fsd.BudgetaryValidityId = @ValidityIdDestiny
				JOIN Budget.Category cd ON fsd.Id = cd.FinancialSourceId AND c.Code = cd.Code AND cd.BudgetaryValidityId = @ValidityIdDestiny			
				LEFT JOIN Budget.Budget bd ON cd.Id = bd.CategoryId AND rtd.Id= bd.RevenueTypeId
				WHERE bd.Id IS NULL			

			/*****************************  ACTUALIZACIONES INGRESOS *****************************/

			UPDATE sb
				SET sb.BasicBillingBudgetId = bd.Id
			FROM Billing.SettingsBilling sb
			JOIN Budget.Budget b ON sb.BasicBillingBudgetId = b.Id
			JOIN Budget.RevenueType rt ON b.RevenueTypeId = rt.Id
			JOIN Budget.Category c ON b.CategoryId = c.Id
			JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
			-------------------------------------------------------------
			JOIN Budget.RevenueType rtd ON rt.Type = rtd.Type AND rt.Code = rtd.Code AND rtd.BudgetaryValidityId = @ValidityIdDestiny 
			JOIN Budget.FinancialSource fsd ON fs.Code = fsd.Code AND fsd.BudgetaryValidityId = @ValidityIdDestiny
			JOIN Budget.Category cd ON fsd.Id = cd.FinancialSourceId AND c.Code = cd.Code AND cd.BudgetaryValidityId = @ValidityIdDestiny			
			JOIN Budget.Budget bd ON cd.Id = bd.CategoryId AND rtd.Id= bd.RevenueTypeId

			UPDATE cg
				SET cg.BillingBudgetId = bd.Id
			FROM Contract.CareGroup cg
			JOIN Budget.Budget b ON cg.BillingBudgetId = b.Id
			JOIN Budget.RevenueType rt ON b.RevenueTypeId = rt.Id
			JOIN Budget.Category c ON b.CategoryId = c.Id
			JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
			-------------------------------------------------------------
			JOIN Budget.RevenueType rtd ON rt.Type = rtd.Type AND rt.Code = rtd.Code AND rtd.BudgetaryValidityId = @ValidityIdDestiny 
			JOIN Budget.FinancialSource fsd ON fs.Code = fsd.Code AND fsd.BudgetaryValidityId = @ValidityIdDestiny
			JOIN Budget.Category cd ON fsd.Id = cd.FinancialSourceId AND c.Code = cd.Code AND cd.BudgetaryValidityId = @ValidityIdDestiny			
			JOIN Budget.Budget bd ON cd.Id = bd.CategoryId AND rtd.Id= bd.RevenueTypeId

			UPDATE cg
				SET cg.PromissoryNoteBudgetId = bd.Id
			FROM Contract.CareGroup cg
			JOIN Budget.Budget b ON cg.PromissoryNoteBudgetId = b.Id
			JOIN Budget.RevenueType rt ON b.RevenueTypeId = rt.Id
			JOIN Budget.Category c ON b.CategoryId = c.Id
			JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
			-------------------------------------------------------------
			JOIN Budget.RevenueType rtd ON rt.Type = rtd.Type AND rt.Code = rtd.Code AND rtd.BudgetaryValidityId = @ValidityIdDestiny 
			JOIN Budget.FinancialSource fsd ON fs.Code = fsd.Code AND fsd.BudgetaryValidityId = @ValidityIdDestiny
			JOIN Budget.Category cd ON fsd.Id = cd.FinancialSourceId AND c.Code = cd.Code AND cd.BudgetaryValidityId = @ValidityIdDestiny			
			JOIN Budget.Budget bd ON cd.Id = bd.CategoryId AND rtd.Id= bd.RevenueTypeId

			UPDATE cg
				SET cg.AccountReceivablePreviousValidityBudgetId = bd.Id
			FROM Contract.CareGroup cg
			JOIN Budget.Budget b ON cg.AccountReceivablePreviousValidityBudgetId = b.Id
			JOIN Budget.RevenueType rt ON b.RevenueTypeId = rt.Id
			JOIN Budget.Category c ON b.CategoryId = c.Id
			JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
			-------------------------------------------------------------
			JOIN Budget.RevenueType rtd ON rt.Type = rtd.Type AND rt.Code = rtd.Code AND rtd.BudgetaryValidityId = @ValidityIdDestiny 
			JOIN Budget.FinancialSource fsd ON fs.Code = fsd.Code AND fsd.BudgetaryValidityId = @ValidityIdDestiny
			JOIN Budget.Category cd ON fsd.Id = cd.FinancialSourceId AND c.Code = cd.Code AND cd.BudgetaryValidityId = @ValidityIdDestiny			
			JOIN Budget.Budget bd ON cd.Id = bd.CategoryId AND rtd.Id= bd.RevenueTypeId

			UPDATE cg
				SET cg.PortfolioRecoveryBudgetId = bd.Id
			FROM Contract.CareGroup cg
			JOIN Budget.Budget b ON cg.PortfolioRecoveryBudgetId = b.Id
			JOIN Budget.RevenueType rt ON b.RevenueTypeId = rt.Id
			JOIN Budget.Category c ON b.CategoryId = c.Id
			JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
			-------------------------------------------------------------
			JOIN Budget.RevenueType rtd ON rt.Type = rtd.Type AND rt.Code = rtd.Code AND rtd.BudgetaryValidityId = @ValidityIdDestiny 
			JOIN Budget.FinancialSource fsd ON fs.Code = fsd.Code AND fsd.BudgetaryValidityId = @ValidityIdDestiny
			JOIN Budget.Category cd ON fsd.Id = cd.FinancialSourceId AND c.Code = cd.Code AND cd.BudgetaryValidityId = @ValidityIdDestiny			
			JOIN Budget.Budget bd ON cd.Id = bd.CategoryId AND rtd.Id= bd.RevenueTypeId

			/*****************************  ACTUALIZACIONES GASTOS *****************************/

			UPDATE pg
				SET pg.BudgetId = bd.Id
			FROM Inventory.ProductGroup pg
			JOIN Budget.Budget b ON pg.BudgetId = b.Id
			JOIN Budget.RevenueType rt ON b.RevenueTypeId = rt.Id
			JOIN Budget.Category c ON b.CategoryId = c.Id
			JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
			-------------------------------------------------------------
			JOIN Budget.RevenueType rtd ON rt.Type = rtd.Type AND rt.Code = rtd.Code AND rtd.BudgetaryValidityId = @ValidityIdDestiny 
			JOIN Budget.FinancialSource fsd ON fs.Code = fsd.Code AND fsd.BudgetaryValidityId = @ValidityIdDestiny
			JOIN Budget.Category cd ON fsd.Id = cd.FinancialSourceId AND c.Code = cd.Code AND cd.BudgetaryValidityId = @ValidityIdDestiny			
			JOIN Budget.Budget bd ON cd.Id = bd.CategoryId AND rtd.Id= bd.RevenueTypeId
		END
	END TRY
	BEGIN CATCH
		INSERT INTO @Result_Table
			SELECT 999 AS Code, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) AS Message
	END CATCH

	/****************************************  RESULTADOS ****************************************/

	SELECT * FROM @Result_Table
END