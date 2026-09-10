CREATE Function [Billing].[GetIncomeMainAccount]
(
	@CareGroupId Int,	
	@RecordType Tinyint,
	@BillingConceptId Int,
	@PerformsFunctionalUnitId Int,
	@ProductId Int = Null,
	@ServiceOrderDetailSurgicalXml Xml
)
Returns @IncomeDetail Table
(
	StateResult Bit,
	MessageResult Varchar(255),
	IncomeMainAccountId Int Null,
	ServiceOrderDetailSurgicalXml Xml Null
) 
As
Begin
	
	Insert Into @IncomeDetail
	Values (1, '', Null, Null)

	Declare @AccountingType Tinyint,
		@FunctionalUnitId Int,
		@IncomeMainAccount Int,
		@BillingConceptEntityIncomeAccountId Int,
		@BillingConceptIndividualIncomeAccountId Int,
		@CareGroupType Tinyint

	Select @CareGroupType = CareGroupType
	From [Contract].CareGroup With(Nolock) Where Id = @CareGroupId
		
	If @RecordType = 1 Begin
		--Es Servicio
		Select @AccountingType = AccountingType,
			@BillingConceptEntityIncomeAccountId = EntityIncomeAccountId,
			@BillingConceptIndividualIncomeAccountId = IndividualIncomeAccountId
		From Billing.BillingConcept bc With(Nolock)
		Where bc.Id = @BillingConceptId
		--Si el tipo de contabilizacion es 2 entonces miramos el tipo de unidad de la unidad funcional asociada al detalle de orden de servicio
		--y luego se busca en los detalles de parametros de facturación y de allí se toman las cuentas
		If @AccountingType = 2 Begin
			--Cuenta por Tipo de Unidad Nota
			Declare @Entity Bit = 1
				, @UnitType Tinyint
				, @FunctionalUnitUnitType Tinyint

			If @CareGroupType = 3 --Particular
				Set @Entity = 0
			
			Select @FunctionalUnitId = Id
				, @FunctionalUnitUnitType = UnitType
			From Payroll.FunctionalUnit With(Nolock)
			Where Id = @PerformsFunctionalUnitId

			If @FunctionalUnitId Is Null Or @FunctionalUnitId = 0 Begin
				Update @IncomeDetail Set StateResult = 0, MessageResult = 'La unidad funcional no se encuentra homologada en Indigo Vie'
				Return
			End
			
			If @FunctionalUnitUnitType In (1,23)
				Set @UnitType = 1
			Else If @FunctionalUnitUnitType In (2,5,6,7,8,9,10,11,16,17,18)
				Set @UnitType = 2
			Else If @FunctionalUnitUnitType In (19)
				Set @UnitType = 3
			Else If @FunctionalUnitUnitType In (3,4,12,13,14,15,20,21,22,24,25)
				Set @UnitType = 4
			Else
				Set @UnitType = 0

			If @Entity = 1
				Select Top 1 @IncomeMainAccount = EntityIncomeAccountId
				From Billing.BillingConceptAccount With(Nolock)
				Where BillingConceptId = @BillingConceptId And UnitType = @UnitType
			Else
				Select Top 1 @IncomeMainAccount = IndividualIncomeAccountId
				From Billing.BillingConceptAccount With(Nolock)
				Where BillingConceptId = @BillingConceptId And UnitType = @UnitType
		End
		Else Begin
			If @CareGroupType In (1,2,4)
				Set @IncomeMainAccount = @BillingConceptEntityIncomeAccountId
			Else If @CareGroupType = 3
				Set @IncomeMainAccount = @BillingConceptIndividualIncomeAccountId
		End
	End
	Else If @RecordType = 2 Begin
		--Es producto

		DECLARE @SettingInventoryId INT,
				@AssociateCostMainAccount TINYINT,
				@ProductGroupIncomeAccountId Int,
				@ProductGroupCodeName VARCHAR(300)

		SELECT TOP 1
			@SettingInventoryId = Id,
			@AssociateCostMainAccount = AssociateCostMainAccount
		FROM Inventory.SettingInventory WITH (NOLOCK) 
		--WHERE OperatingUnitId = ISNULL(@OperatingUnitId, OperatingUnitId)

		SELECT @ProductGroupCodeName = CONCAT(pg.Code, ' - ', pg.Name), @ProductGroupIncomeAccountId = ISNULL(sifu.SalesAccountId, pgfu.SalesAccountId)
		FROM Inventory.InventoryProduct ipr WITH (NOLOCK)
		JOIN Inventory.ProductGroup pg WITH (NOLOCK) ON ipr.ProductGroupId = pg.Id
		LEFT JOIN Inventory.SettingInventoryFunctionalUnit sifu WITH (NOLOCK) ON @AssociateCostMainAccount = 1 AND sifu.SettingInventoryId = @SettingInventoryId AND @PerformsFunctionalUnitId = sifu.FunctionalUnitId
		LEFT JOIN Inventory.ProductGroupFunctionalUnit pgfu WITH (NOLOCK) ON @AssociateCostMainAccount = 2 AND ipr.ProductGroupId = pgfu.ProductGroupId AND @PerformsFunctionalUnitId = pgfu.FunctionalUnitId
		WHERE ipr.Id = @ProductId

		If @ProductGroupIncomeAccountId Is Null Begin
			Declare @FunctionalUnitCodeName Varchar(300)
			Select @FunctionalUnitCodeName = Concat(Code, ' - ', [Name])
			From Payroll.FunctionalUnit With(Nolock)
			Where Id = @PerformsFunctionalUnitId

			Update @IncomeDetail Set StateResult = 0, MessageResult = 'No se encontró cuenta contable en los ' + IIF(@AssociateCostMainAccount = 1, 'parámetros de inventarios', CONCAT('el grupo de producto ', @ProductGroupCodeName)) + ' para la unidad funcional ' + @FunctionalUnitCodeName
			Return
		End
		Else
			Set @IncomeMainAccount = @ProductGroupIncomeAccountId

	End

	Declare @serviceOrderDetailSurgical Table(
		Idx Int Identity(1,1) Primary Key,
		RowId Int,
		Id Int,
		ServiceOrderDetailId Int,
		CodeNameIpsService Varchar(320),
		IPSServiceId Int,
		InvoicedQuantity Int,
		LiquidationPercentage Decimal(5, 2),
		TotalSalesPrice Decimal(18, 0),
		ClassServiceIps Varchar(30),
		RateManualSalePrice Decimal(18, 0),
		PerformsHealthProfessionalCode Char(20) Null,
		PerformsHealthProfessionalThirdPartyId Int Null,
		CostValue Decimal(18, 2),
		BillingConceptId Int,
		CostCenterId Int,
		RateManualDetailSurgicalId Int Null,
		SurchargeApply Bit,
		IncomeMainAccountId Int Null,
		RoundService int,
		AllowValueChange Bit)

	Insert Into @serviceOrderDetailSurgical
	Select t.x.value('RowId[1]', 'Int'),
		t.x.value('Id[1]', 'Int'),
		t.x.value('ServiceOrderDetailId[1]', 'Int'),		
		t.x.value('CodeNameIpsService[1]', 'Varchar(320)'),
		t.x.value('IPSServiceId[1]', 'Int'),
		t.x.value('InvoicedQuantity[1]', 'Int'),
		t.x.value('LiquidationPercentage[1]', 'Decimal(5, 2)'),
		t.x.value('TotalSalesPrice[1]', 'Decimal(18, 0)'),
		t.x.value('ClassServiceIps[1]', 'Varchar(30)'),
		t.x.value('RateManualSalePrice[1]', 'Decimal(18, 0)'),
		t.x.value('PerformsHealthProfessionalCode[1]', 'Char(20)'),
		t.x.value('PerformsHealthProfessionalThirdPartyId[1]', 'Int'),
		t.x.value('CostValue[1]', 'Decimal(18, 2)'),
		t.x.value('BillingConceptId[1]', 'Int'),
		t.x.value('CostCenterId[1]', 'Int'),
		t.x.value('RateManualDetailSurgicalId[1]', 'Int'),
		t.x.value('SurchargeApply[1]', 'Bit'),
		t.x.value('IncomeMainAccountId[1]', 'Int'),
		t.x.value('RoundService[1]', 'Int'),
		ISNULL(t.x.value('AllowValueChange[1]', 'Bit'), 0)
	From @ServiceOrderDetailSurgicalXml.nodes('/ServiceOrderDetailSurgical') t(x)

	If (Select Count(1) From @serviceOrderDetailSurgical) > 0 Begin

		Declare @BillingConceptIdS Int,
			@RowId Int

		Declare @Rows Int, @RowIdx Int
		Set @Rows = 1
		Set @RowIdx = 1

		While @Rows > 0
		begin
			
			Select Top 1 @RowIdx = Idx, @RowId = RowId, @BillingConceptIdS = BillingConceptId 
			From @serviceOrderDetailSurgical Where Idx >= @RowIdx Order By Idx

			Set @Rows = @@ROWCOUNT
			If @Rows = 0 
				Break

			Declare @ConceptType Tinyint,
				@AccountingTypes Tinyint,
				@BillingConceptCode Varchar(20),
				@IncomeMainAccountSurgical Int

			Select @ConceptType = ConceptType,
				@AccountingTypes = AccountingType,
				@BillingConceptCode = Code,
				@BillingConceptEntityIncomeAccountId = EntityIncomeAccountId,
				@BillingConceptIndividualIncomeAccountId = IndividualIncomeAccountId
			From Billing.BillingConcept With(Nolock)
			Where Id = @BillingConceptIdS

			If @ConceptType = 1 Begin
				--Facturación básica
				Update @IncomeDetail Set StateResult = 0, MessageResult = 'El concepto de facturación ' + @BillingConceptCode + ' no puede ser de tipo facturación básica'
				Return
			End

			If @AccountingTypes = 2 Begin
				--Cuenta por Tipo de Unidad Nota
				Declare @entity1 Bit = 1

				If @CareGroupType = 3
					Set @entity1 = 0

				Select @FunctionalUnitId = Id
					, @FunctionalUnitUnitType = UnitType
				From Payroll.FunctionalUnit With(Nolock)
				Where Id = @PerformsFunctionalUnitId

				If @FunctionalUnitId Is Null Or @FunctionalUnitId = 0 Begin
					Update @IncomeDetail Set StateResult = 0, MessageResult = 'La unidad funcional no se encuentra homologada en Indigo Vie'
					Return
				End
			
				If @FunctionalUnitUnitType In (1,23)
					Set @UnitType = 1
				Else If @FunctionalUnitUnitType In (2,5,6,7,8,9,10,11,16,17,18)
					Set @UnitType = 2
				Else If @FunctionalUnitUnitType In (19)
					Set @UnitType = 3
				Else If @FunctionalUnitUnitType In (3,4,12,13,14,15,20,21,22,24,25)
					Set @UnitType = 4
				Else
					Set @UnitType = 0

				If @entity1 = 1
					Select Top 1 @IncomeMainAccountSurgical = EntityIncomeAccountId
					From Billing.BillingConceptAccount With(Nolock)
					Where BillingConceptId = @BillingConceptIdS And UnitType = @UnitType
				Else
					Select Top 1 @IncomeMainAccountSurgical = IndividualIncomeAccountId
					From Billing.BillingConceptAccount With(Nolock)
					Where BillingConceptId = @BillingConceptIdS And UnitType = @UnitType
			End
			Else Begin
				If @CareGroupType In (1,2,4)
					Set @IncomeMainAccountSurgical = @BillingConceptEntityIncomeAccountId
				Else If @CareGroupType = 3
					Set @IncomeMainAccountSurgical = @BillingConceptIndividualIncomeAccountId
			End

			Update @serviceOrderDetailSurgical Set IncomeMainAccountId = @IncomeMainAccountSurgical
			Where RowId = @RowId

			Set @RowIdx += 1
		End

		Update @IncomeDetail Set ServiceOrderDetailSurgicalXml = (
			Select *
			From @serviceOrderDetailSurgical For Xml Path('ServiceOrderDetailSurgical'), Elements
		)

	End
	
	Update @IncomeDetail Set IncomeMainAccountId = @IncomeMainAccount

	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que determina la cuenta contable principal de ingresos que debe afectarse al facturar un servicio o producto en el proceso de liquidación. Recibe el grupo de atención del contrato, el tipo de registro (servicio o medicamento/insumo), el concepto de facturación y la unidad funcional ejecutante, y resuelve la cuenta contable correcta según el tipo de contabilización definido en el concepto de facturación, el tipo de grupo de atención (entidad o particular) y el tipo de unidad funcional; para productos, consulta los parámetros de inventario y el grupo de producto según la unidad funcional. Adicionalmente, procesa un XML con detalles de servicios quirúrgicos para asignarles la misma cuenta contable resuelta. Es utilizada en el motor de facturación para garantizar que cada ítem de una factura quede asociado a la cuenta de ingresos contable correcta antes de generar el comprobante.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'FUNCTION', @level1name = N'GetIncomeMainAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'FUNCTION', @level1name = N'GetIncomeMainAccount';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Resuelve la cuenta contable de ingresos a aplicar a un ítem de facturación (servicio o producto) según grupo de atención, concepto de facturación, unidad funcional y configuración de inventario, y propaga la misma lógica a cada detalle quirúrgico recibido por XML.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetIncomeMainAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@CareGroupId debe existir en Contract.CareGroup para obtener CareGroupType; Si @RecordType=1, @BillingConceptId debe existir en Billing.BillingConcept; Si @RecordType=1 y AccountingType=2, @PerformsFunctionalUnitId debe corresponder a una Payroll.FunctionalUnit existente (homologada); Si @RecordType=2, @ProductId debe existir en Inventory.InventoryProduct y debe haber al menos un registro en Inventory.SettingInventory; El XML @ServiceOrderDetailSurgicalXml debe seguir la estructura /ServiceOrderDetailSurgical con los elementos esperados (RowId, Id, BillingConceptId, etc.)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetIncomeMainAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'StateResult inicia en 1 con mensaje vacío y solo cambia a 0 ante errores controlados (unidad funcional no homologada, concepto tipo facturación básica, cuenta no encontrada para producto); Cuando CareGroupType=3 (Particular) siempre se usa la cuenta de ingresos individual; cuando CareGroupType in (1,2,4) siempre se usa la cuenta de ingresos entidad; El mapeo FunctionalUnitUnitType→UnitType es fijo (1/23→1, 2/5-11/16-18→2, 19→3, 3/4/12-15/20-22/24-25→4, resto→0); Para servicios con AccountingType=2 la cuenta se busca en BillingConceptAccount filtrando por BillingConceptId y UnitType derivado; Para productos, la fuente de la cuenta depende de AssociateCostMainAccount: 1=parámetros de inventario por unidad funcional, 2=grupo de producto por unidad funcional; Todos los detalles quirúrgicos del XML se actualizan con la misma lógica de cuenta y se devuelven serializados nuevamente como XML', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetIncomeMainAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Concepto de facturación; Cuenta contable de ingresos; Unidad funcional; Tipo de unidad; Grupo de atención (CareGroup) particular vs entidad; Grupo de producto; Parámetros de inventario; Detalle quirúrgico de orden de servicio; Facturación básica', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetIncomeMainAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @RecordType = 1 (servicio) y AccountingType del concepto = 2 → Resuelve la cuenta vía Billing.BillingConceptAccount usando un UnitType derivado del UnitType de la unidad funcional; usa EntityIncomeAccountId si CareGroupType<>3 o IndividualIncomeAccountId si CareGroupType=3 (Particular) else Si AccountingType<>2: usa EntityIncomeAccountId del BillingConcept cuando CareGroupType in (1,2,4) o IndividualIncomeAccountId cuando CareGroupType=3; si @RecordType = 2 (producto) → Consulta Inventory.SettingInventory para obtener AssociateCostMainAccount; si =1 toma SalesAccountId desde Inventory.SettingInventoryFunctionalUnit (por SettingInventoryId + FunctionalUnit); si =2 toma SalesAccountId desde Inventory.ProductGroupFunctionalUnit (por ProductGroup + FunctionalUnit); si FunctionalUnitUnitType IN (1,23) → UnitType=1 else IN (2,5,6,7,8,9,10,11,16,17,18)→2; IN (19)→3; IN (3,4,12,13,14,15,20,21,22,24,25)→4; cualquier otro valor→0; si Para productos, @ProductGroupIncomeAccountId resultó NULL → Devuelve StateResult=0 con mensaje indicando que no hay cuenta contable en ''parámetros de inventarios'' (si AssociateCostMainAccount=1) o en el ''grupo de producto X'' (si =2) para la unidad funcional dada else Asigna esa cuenta como IncomeMainAccount; si Por cada fila del XML quirúrgico, ConceptType del BillingConcept = 1 (facturación básica) → Devuelve StateResult=0 con mensaje ''El concepto de facturación X no puede ser de tipo facturación básica'' y termina else Continúa resolviendo cuenta de ingresos por la misma lógica AccountingType=2 vs CareGroupType', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetIncomeMainAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.CareGroup; Billing.BillingConcept; Payroll.FunctionalUnit; Billing.BillingConceptAccount; Inventory.SettingInventory; Inventory.InventoryProduct; Inventory.ProductGroup; Inventory.SettingInventoryFunctionalUnit; Inventory.ProductGroupFunctionalUnit', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetIncomeMainAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetIncomeMainAccount';
GO
