
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que actualiza la información parametrizada del módulo de presupuesto al cambiar de vigencia presupuestaria. Recibe criterios en formato XML indicando la vigencia destino y las secciones a migrar: dependencias (centros de costo) y rubros presupuestales (categorías). Para las dependencias, reasigna los centros de costo en la configuración de facturación (SettingsBilling) y de cartera/cuentas por cobrar (SettingPortfolio) de cada unidad operativa o sede, apuntando a los equivalentes de la nueva vigencia según el mismo código de dependencia; si alguna dependencia no tiene homóloga en la vigencia destino, registra un mensaje de advertencia con código 999. Para los rubros, crea el encabezado presupuestal de la nueva vigencia si no existe y reemplaza los tipos de ingreso y rubros de gasto en la configuración de facturación básica, grupos de atención contractual, pagarés, cuentas por cobrar de vigencia anterior y parámetros de cartera, asegurando la continuidad operativa entre vigencias presupuestarias sin perder la trazabilidad de la información contable y financiera.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_UpdateParameterizedInformation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_UpdateParameterizedInformation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Migra/homologa la información parametrizada (dependencias y rubros presupuestales) desde su vigencia actual hacia una vigencia presupuestal destino, validando equivalencias y reasignando referencias en facturación, cartera, contratos e inventario.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateParameterizedInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML @xmlCriterias debe contener un nodo /Data con ValidityIdDestiny, Dependencies, Categories y UserCode.; @ValidityIdDestiny debe corresponder a una Budget.BudgetaryValidity existente para que el flujo de Categories pueda obtener/crear BudgetHeader.; Para que las homologaciones tengan éxito, deben existir en la vigencia destino registros equivalentes (por Code) en Budget.Dependency, Budget.FinancialSource, Budget.Category y Budget.RevenueType; en caso contrario se generan advertencias 999.; @UserCode se utiliza como CreationUser en BudgetHeader y Budget; debe ser un código de usuario válido.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateParameterizedInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las nuevas líneas Budget se crean siempre con todos los valores monetarios (InitialValue, DebitValueModification, CreditValueModification, DebitValueTransfer, CreditValueTransfer, TotalBudget, ExecutedValue, SuspendedValue, Balance) en 0.; Los BudgetHeader creados por este flujo se generan con Type=1 y Status=1.; La homologación de Dependency se realiza por igualdad de Code entre la vigencia origen y la vigencia destino (@ValidityIdDestiny).; La homologación de RevenueType se realiza por (Type, Code); la de FinancialSource por Code; la de Category por (FinancialSourceId destino, Code).; Solo se inserta una línea Budget en la vigencia destino si no existe ya una con misma CategoryId y RevenueTypeId (LEFT JOIN ... WHERE bd.Id IS NULL).; Las inconsistencias no abortan la ejecución: se acumulan como filas en @Result_Table con Code=999 y se devuelven al final.; Cualquier excepción capturada se reporta también con Code=999 incluyendo ERROR_MESSAGE() y la línea del error.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateParameterizedInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'vigencia presupuestal; homologación entre vigencias; dependencia presupuestal; rubro presupuestal; tipo de ingreso; fuente de financiación; categoría presupuestal; factura básica; grupo de atención; pagarés; cuentas por cobrar vigencia anterior; recuperación de cartera; configuración de facturación; configuración de cartera; grupo de productos / gastos', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateParameterizedInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Dependencies = 1 (extraído del XML) → Valida homologación de dependencias por Code en la vigencia destino para SettingsBilling.DependencyId, SettingsBilling.BasicBillingDependencyId y SettingPortfolio.DependencyId; luego reasigna esos Id a las dependencias de la vigencia destino. else Omite el bloque de homologación y reasignación de dependencias.; si @Categories = 1 (extraído del XML) → Crea (si no existe) un BudgetHeader Type=1, Status=1 para la vigencia destino; valida homologación de RevenueType y Category por (Type, Code) y (FinancialSource.Code, Category.Code); inserta líneas Budget faltantes en valores cero y reasigna los *BudgetId en SettingsBilling, CareGroup e Inventory.ProductGroup. else Omite todo el procesamiento de rubros y presupuesto.; si En el bloque Categories: existe BudgetHeader con BudgetaryValidityId=@ValidityIdDestiny y Type=1 → Reutiliza ese encabezado (@BudgetHeaderIdDestiny). else Inserta uno nuevo en Budget.BudgetHeader antes de continuar.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateParameterizedInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateParameterizedInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.SettingsBilling; Common.OperatingUnit; Budget.Dependency; Portfolio.SettingPortfolio; Budget.BudgetaryValidity; Budget.BudgetHeader; Budget.Budget; Budget.RevenueType; Contract.CareGroup; Budget.Category; Budget.FinancialSource; Inventory.ProductGroup', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateParameterizedInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateParameterizedInformation';
-- GO
