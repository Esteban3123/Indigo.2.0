

-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-12-04
-- Description:	Validaciones que no requieren datos guardados Liquidación (Invoice, AccountReceivable...)
-- =============================================
CREATE PROCEDURE [Billing].[SP_LiquidateFolioValidations_Output]
	@PatientCode VARCHAR(20),
	@AdmissionNumber VARCHAR(20),
	@ContainerCrystal VARCHAR(10),
	@BillingAuthorizationId INT,
	@OperativeUnitId INT,
	@ThirdPartyPatientId INT,
	@UserCode VARCHAR(20),
	@CompanyType TINYINT,
	@RevenueControlDetailCrossingListXml XML,
	@SkipAccountControlValidations BIT,
	--Salidas
	@ResultStatus BIT OUTPUT,
	@ResultMessage VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	/*************************************************** VARIABLES ***************************************************/

	DECLARE @Message VARCHAR(MAX),
			---------------------------
			@Rows INT = 1, 
			@RowId INT = 0,
			@ListPortfolioAdvanceXml XML,
			@TotalCrossingValue DECIMAL(18, 2), 
			@TransactionCurrencyId INT,
			----------------------------
			@OfficialCurrencyId INT,
			@LiquidateMasterAccount BIT

	SET @OfficialCurrencyId = (SELECT TOP 1 OfficialCurrencyId
							   FROM GeneralLedger.CompanySettings
							  )

	DECLARE @RevenueControlDetailCrossingList AS TABLE
	(
		RowId INT IDENTITY(1,1) PRIMARY KEY, 
		RevenueControlDetailId INT,
		ListPortfolioAdvanceCrossing XML,
		TotalPatientDiscount DECIMAL(18, 2),
		OutputDate DATETIME,
		IsCutAccount BIT,
		OutputDiagnosis VARCHAR(10),
		InitialDate DATETIME,
		CutType INT,
		FilePath VARCHAR(MAX),
		CurrencyId INT,
		TRMValue DECIMAL(20,5),
		TaxDevolutionValue NUMERIC(20,2)
	)

	DECLARE @ListPortfolioAdvanceCrossing AS TABLE
	(
		RowId INT IDENTITY(1,1) PRIMARY KEY,
		Id INT,
		Code VARCHAR(20),
		CrossingValue DECIMAL(18, 2),
		CashReceiptDetailIdTmp INT
	)

	SET @LiquidateMasterAccount = (SELECT top 1 sb.LiquidateMasterAccount FROM Billing.SettingsBilling sb WITH(NOLOCK) WHERE sb.IdOperatingUnit =@OperativeUnitId)

	-------------------------------------------------------------------------------------------------------------------

	BEGIN TRY
		INSERT INTO @RevenueControlDetailCrossingList
			SELECT	t.x.value('RevenueControlDetailId[1]', 'INT'),
					t.x.query('ListPortfolioAdvanceCrossing'),
					t.x.value('TotalPatientDiscount[1]', ' DECIMAL(18, 2)'),
					CONVERT(DATETIME, t.x.value('OutputDate[1]', 'nvarchar(19)'), 103),
					t.x.value('IsCutAccount[1]', 'BIT'),
					t.x.value('OutputDiagnosis[1]', 'VARCHAR(10)'),
					CONVERT(DATETIME, t.x.value('InitialDate[1]', 'nvarchar(19)'), 103),
					t.x.value('CutType[1]', 'INT'),
					t.x.value('FilePath[1]', 'VARCHAR(MAX)'),
					t.x.value('CurrencyId[1]', 'INT'),
					t.x.value('TRMValue[1]', 'DECIMAL(20,5)'),
					t.x.value('TaxDevolutionValue[1]', 'Numeric(20,2)')
			FROM @RevenueControlDetailCrossingListXml.nodes('RevenueControlDetailCrossingList') t(x)

		------ 948 -----

		-- Validación de compatibilidad Usuario - Entidad
		DECLARE @UserType NVARCHAR(50);
		SELECT TOP 1 @UserType = IPTIPOPAC FROM INPACIENT WHERE IPCODPACI = @PatientCode

		IF EXISTS
		(
			SELECT 1
			FROM @RevenueControlDetailCrossingList l
			JOIN Billing.RevenueControlDetail rcd ON l.RevenueControlDetailId = rcd.Id
			JOIN Contract.CareGroup cg ON cg.id = rcd.CareGroupId
			WHERE NOT EXISTS
			(
				SELECT 1
				FROM RegulatoryEngine.DecisionTable dt
				JOIN RegulatoryEngine.DecisionTableRow r ON r.DecisionTableId = dt.Id
				JOIN RegulatoryEngine.DecisionTableCell c1 ON c1.DecisionTableRowId = r.Id
				JOIN RegulatoryEngine.DecisionTableColumn dc1 ON dc1.Id = c1.DecisionTableColumnId
				JOIN RegulatoryEngine.DecisionTableCell c2 ON c2.DecisionTableRowId = r.Id
				JOIN RegulatoryEngine.DecisionTableColumn dc2 ON dc2.Id = c2.DecisionTableColumnId
				WHERE dt.Name = 'UserEntityCompatibility'
				AND dc1.ColumnName = 'UserType'
				AND c1.Value = @UserType
				AND dc2.ColumnName = 'EntityType'
				AND c2.Value = cg.EntityType
				AND r.IsAllowed = 1
			)
		)
		BEGIN
			SET @Message = 'El tipo de usuario del paciente no corresponde con el tipo de entidad asociado al grupo de atención a liquidar.'
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END;

		if exists (
			select 1
			from @RevenueControlDetailCrossingList r
			join Billing.RevenueControlDetail rcd on r.RevenueControlDetailId = rcd.Id
			where rcd.ValueCopay > 0 or rcd.ValueFeeModerator > 0 or rcd.ValueVoucher > 0
		) begin
			DECLARE @PurposeCode VARCHAR(50); -- Finalidad del control servicios ambulatorio
			DECLARE @RuleCode VARCHAR(20);
			DECLARE @ValueSetName VARCHAR(200);

			SELECT TOP 1 @PurposeCode = h.Code
			FROM ADINGRESO a
			JOIN Admissions.HealthPurposes h ON a.IdHealthPurposes = h.Id
			WHERE a.NUMINGRES = @AdmissionNumber;

			SELECT
				@ValueSetName = C.ParameterValue,
				@RuleCode = B.RuleCode
			FROM RegulatoryEngine.RegulatoryPack A
			INNER JOIN RegulatoryEngine.RegulatoryRule B ON A.Id = B.RegulatoryPackId
			INNER JOIN RegulatoryEngine.RuleParameter C ON C.RegulatoryRuleId = B.Id
			WHERE C.ParameterName = 'PurposeOfCareValueSet'
			AND A.IsActive = 1
			AND B.IsActive = 1
			AND A.JurisdictionCode = 'CO';

			IF NOT EXISTS
			(
				SELECT 1
				FROM RegulatoryEngine.TerminologyValueSet vs
				INNER JOIN RegulatoryEngine.TerminologyValueSetItem vsi
					ON vsi.TerminologyValueSetId = vs.Id
				WHERE vs.Name = @ValueSetName
				AND vsi.Code = @PurposeCode
				AND vsi.IsActive = 1
			)
			BEGIN
				SET @Message = 'La finalidad seleccionada no aplica para cuota moderadora.'
				SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
			END;
		end

		------ END 948 -----

		/******************************************* PARAMETROS DE ENTRADA *******************************************/

		IF ISNULL(@OperativeUnitId, 0) = 0 BEGIN
			SET @Message = 'No se ha enviado el parámetro Unidad Operativa'
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END

		DECLARE @SettingsBillingId Int
			, @SettingsParticularHealthAdministratorId Int
			, @AccountControlValidation Bit

		SELECT TOP 1 @SettingsBillingId = Id
			, @SettingsParticularHealthAdministratorId = ParticularHealthAdministratorId
			, @AccountControlValidation = AccountControlValidation
		FROM Billing.SettingsBilling WHERE IdOperatingUnit = @OperativeUnitId

		IF @SettingsBillingId IS NULL BEGIN
			SET @Message = 'No se encontraron parámetros de Facturación para la unidad operativa seleccionada'
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END

		IF @SettingsParticularHealthAdministratorId IS NULL BEGIN
			SET @Message = 'No esta parametrizada una entidad administradora para particulares en la unidad operativa seleccionada'
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END

		--IF NOT EXISTS (SELECT 1 FROM Billing.SettingsBilling WITH (NOLOCK) WHERE IdOperatingUnit = @OperativeUnitId) BEGIN
		--	SET @Message = 'No se encontraron parámetros de Facturación para la unidad operativa seleccionada'
		--	SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		--END

		--IF EXISTS (SELECT 1 FROM Billing.SettingsBilling WITH (NOLOCK) WHERE IdOperatingUnit = @OperativeUnitId AND ParticularHealthAdministratorId IS NULL) BEGIN
		--	SET @Message = 'No esta parametrizada una entidad administradora para particulares en la unidad operativa seleccionada'
		--	SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		--END

		IF ISNULL(@PatientCode, '') = '' BEGIN
			SET @Message = 'No se ha enviado el parámetro Código de Paciente'
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END

		IF ISNULL(@ThirdPartyPatientId, 0) = 0 BEGIN
			SET @Message = 'No se ha enviado el parámetro Tercero Paciente'
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END

		IF NOT EXISTS (SELECT 1 FROM dbo.ADINGRESO ai WITH (NOLOCK) WHERE ai.NUMINGRES = @AdmissionNumber AND ai.IPCODPACI = @PatientCode) BEGIN
			SET @Message = 'El paciente no coincide con el del ingreso'
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END

		IF ISNULL(@AdmissionNumber, '') = '' BEGIN
			SET @Message = 'No se ha enviado el parámetro Número de Ingreso'
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END

		IF ISNULL(@BillingAuthorizationId, 0) = 0 BEGIN
			SET @Message = 'No se ha enviado el parámetro Autorización de Facturación'
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END

		IF NOT EXISTS (SELECT 1 FROM @RevenueControlDetailCrossingList) BEGIN
			SET @Message = 'No hay folios para liquidar'
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END

		IF EXISTS (	SELECT 1 
					FROM @RevenueControlDetailCrossingList tmp
					JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) on tmp.RevenueControlDetailId = rcd.Id
					WHERE TotalPatientDiscount > 0 and @LiquidateMasterAccount = 0) BEGIN
			SET @Message = 'Los descuentos globales se encuentran deshabilitados'
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END

		IF ISNULL(@UserCode, '') = '' BEGIN
			SET @Message = 'No se ha enviado el parámetro Usuario Auditoria'
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END

		/*************************************************  CABECERA *************************************************/

		IF NOT EXISTS (
			SELECT 1 
			FROM Billing.BillingAuthorization WITH (NOLOCK) 
			WHERE Id = @BillingAuthorizationId AND [Status] = 1
		) BEGIN
			SET @Message = 'La autorizacion de facturacion esta inactiva'
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END
		ELSE IF EXISTS (
			SELECT 1 
			FROM Billing.BillingAuthorization WITH (NOLOCK) 
			WHERE Id = @BillingAuthorizationId AND [Status] = 1
				AND (Common.[GETDATE]() < InitialDate OR Common.[GETDATE]() >= FinalDate)
		) BEGIN
			SET @Message = 'La autorización asignada no se encuentra vigente'
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END

		IF EXISTS (
			SELECT 1 
			FROM @RevenueControlDetailCrossingList l
			JOIN Billing.Invoice i WITH (NOLOCK) ON l.RevenueControlDetailId = i.RevenueControlDetailId
			WHERE i.Status = 1
		) BEGIN
			SELECT @Message = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', rcd.FolioOrder, ': ' , i.InvoiceNumber)
					FROM @RevenueControlDetailCrossingList l
					JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON l.RevenueControlDetailId = rcd.Id
					JOIN Billing.Invoice i WITH (NOLOCK) ON l.RevenueControlDetailId = i.RevenueControlDetailId
					WHERE i.Status = 1
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			
			SET @Message = 'Los siguientes folios tienen una factura activa: ' + CHAR(13) + CHAR(10) + @Message
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END

		IF EXISTS (
			SELECT 1
			FROM @RevenueControlDetailCrossingList l
			JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON l.RevenueControlDetailId = rcd.Id
			WHERE rcd.TotalPatientSalesPrice <> rcd.TotalPatientWithDiscount AND rcd.IsMasterAccount =0
		) BEGIN
			SELECT @Message = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + rcd.FolioOrder
					FROM @RevenueControlDetailCrossingList l
					JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON l.RevenueControlDetailId = rcd.Id
					WHERE rcd.TotalPatientSalesPrice <> rcd.TotalPatientWithDiscount
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			
			SET @Message = 'Los siguientes folios tienen descuento global, y este se encuentra deshabilitado: ' + CHAR(13) + CHAR(10) + @Message
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END

		IF EXISTS(	SELECT 1 
						FROM @RevenueControlDetailCrossingList l
						JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) on l.RevenueControlDetailId = rcd.Id
						JOIN Billing.ServiceOrderDetailDistribution sodd on sodd.RevenueControlDetailId = rcd.Id
						JOIN Billing.ServiceOrderDetail sod on sodd.ServiceOrderDetailId = sod.Id
						JOIN Contract.CUPSEntity ce on sod.CUPSEntityId = ce.Id
						JOIN Payroll.FunctionalUnit fu WITH(NOLOCK) on fu.Id =sod.PerformsFunctionalUnitId
						join Billing.BillingConcept bc on ce.BillingConceptId = bc.Id
						left join Billing.BillingConceptAccount bca WITH(NOLOCK) on bca.BillingConceptId = bc.Id and bca.UnitType = iif(fu.UnitType not in (1,2,3,4),2,fu.UnitType)
						left join GeneralLedger.MainAccounts ma on ma.Id = bca.DiscountAccountId
						WHERE	sod.RecordType = 1 AND bc.AccountingType=2
								AND (sod.Presentation <> 2 OR sod.Presentation is NULL ) 
								AND sod.IsDelete = 0 AND  @LiquidateMasterAccount=1 AND sodd.GrandTotalDiscount>0 AND ma.Id is NULL
												
					) OR EXISTS (SELECT 1
						FROM @RevenueControlDetailCrossingList l
						JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) on l.RevenueControlDetailId = rcd.Id
						JOIN Billing.ServiceOrderDetailDistribution sodd on sodd.RevenueControlDetailId = rcd.Id
						JOIN Billing.ServiceOrderDetail sod on sodd.ServiceOrderDetailId = sod.Id
						JOIN Inventory.InventoryProduct ip on sod.ProductId = ip.Id
						join Inventory.ProductGroup pg on ip.ProductGroupId = pg.Id
						LEFT JOIN Inventory.ProductGroupFunctionalUnit pgfu WITH(NOLOCK) on pgfu.ProductGroupId= pg.Id and sod.PerformsFunctionalUnitId= pgfu.FunctionalUnitId
						LEFT join GeneralLedger.MainAccounts ma on ma.Id = pgfu.DiscountAccountId
						WHERE       sod.RecordType = 2 
								AND (sod.Presentation <> 2 OR sod.Presentation is NULL )
								AND sod.IsDelete = 0 AND sodd.GrandTotalDiscount>0 AND ma.Id is NULL)  BEGIN

					SELECT  @Message = (		
												SELECT STRING_AGG(x.[name],' || ')
												from (SELECT concat(fu.code,' - ', fu.Name, ' (Concepto facturación : ',bc.code,' - ',bc.name,')') [name]
														FROM @RevenueControlDetailCrossingList l
														JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) on l.RevenueControlDetailId = rcd.Id
														JOIN Billing.ServiceOrderDetailDistribution sodd on sodd.RevenueControlDetailId = rcd.Id
														JOIN Billing.ServiceOrderDetail sod on sodd.ServiceOrderDetailId = sod.Id
														JOIN Contract.CUPSEntity ce on sod.CUPSEntityId = ce.Id
														JOIN Payroll.FunctionalUnit fu WITH(NOLOCK) on fu.Id =sod.PerformsFunctionalUnitId
														join Billing.BillingConcept bc on ce.BillingConceptId = bc.Id
														left join Billing.BillingConceptAccount bca WITH(NOLOCK) on bca.BillingConceptId = bc.Id and bca.UnitType = iif(fu.UnitType not in (1,2,3,4),2,fu.UnitType)
														left join GeneralLedger.MainAccounts ma on ma.Id = bca.DiscountAccountId
														WHERE	sod.RecordType = 1 AND bc.AccountingType=2
																AND sod.Presentation <> 2 
																AND sod.IsDelete = 0 AND  @LiquidateMasterAccount=1 AND sodd.GrandTotalDiscount>0 and ma.Id is null
														GROUP BY fu.Code,fu.Name,bc.Code,bc.Name

														UNION ALL

														SELECT  concat(fu.code,' - ', fu.Name,' (Productos - Grupo :',pg.code,' - ',pg.Name,')') [name]
														FROM @RevenueControlDetailCrossingList l
														JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) on l.RevenueControlDetailId = rcd.Id
														JOIN Billing.ServiceOrderDetailDistribution sodd on sodd.RevenueControlDetailId = rcd.Id
														JOIN Billing.ServiceOrderDetail sod on sodd.ServiceOrderDetailId = sod.Id
														JOIN Inventory.InventoryProduct ip on sod.ProductId = ip.Id
														join Inventory.ProductGroup pg on ip.ProductGroupId = pg.Id
														JOIN Payroll.FunctionalUnit fu WITH(NOLOCK) on fu.Id =sod.PerformsFunctionalUnitId
														left JOIN Inventory.ProductGroupFunctionalUnit pgfu WITH(NOLOCK) on pgfu.ProductGroupId= pg.Id and fu.Id= pgfu.FunctionalUnitId
														left join GeneralLedger.MainAccounts ma on ma.Id = pgfu.DiscountAccountId
														WHERE       sod.RecordType = 2 
																AND (sod.Presentation <> 2 OR sod.Presentation is NULL ) 
																AND sod.IsDelete = 0  AND sodd.GrandTotalDiscount>0 AND ma.Id is NULL
														GROUP BY fu.Code,fu.Name,pg.Code,pg.Name
													) x																			
												) 
					SET @Message = CONCAT('Las siguientes unidades funcionales no tienen parametrizada cuenta de descuento ' , @Message)
					SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)					
		END

		IF EXISTS(	SELECT 1 
					FROM @RevenueControlDetailCrossingList l
					JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) on l.RevenueControlDetailId = rcd.Id
					JOIN Billing.ServiceOrderDetailDistribution sodd on sodd.RevenueControlDetailId = rcd.Id
					JOIN Billing.ServiceOrderDetail sod on sodd.ServiceOrderDetailId = sod.Id
					JOIN Contract.CUPSEntity ce on sod.CUPSEntityId = ce.Id
					JOIN Payroll.FunctionalUnit fu WITH(NOLOCK) on fu.Id =sod.PerformsFunctionalUnitId
					join Billing.BillingConcept bc on ce.BillingConceptId = bc.Id
					left join GeneralLedger.MainAccounts ma on ma.Id = bc.DiscountAccountId
					WHERE	sod.RecordType = 1 AND bc.AccountingType=1
							AND (sod.Presentation <> 2 OR sod.Presentation is NULL ) 
							AND sod.IsDelete = 0 AND @LiquidateMasterAccount=1 AND sodd.GrandTotalDiscount>0 AND ma.Id is NULL
												
					)   BEGIN

					SELECT  @Message = (		
												SELECT STRING_AGG(x.[name],' || ')
												from (SELECT concat(bc.code,' - ', bc.Name) [name]
														FROM @RevenueControlDetailCrossingList l
														JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) on l.RevenueControlDetailId = rcd.Id
														JOIN Billing.ServiceOrderDetailDistribution sodd on sodd.RevenueControlDetailId = rcd.Id
														JOIN Billing.ServiceOrderDetail sod on sodd.ServiceOrderDetailId = sod.Id
														JOIN Contract.CUPSEntity ce on sod.CUPSEntityId = ce.Id
														JOIN Payroll.FunctionalUnit fu WITH(NOLOCK) on fu.Id =sod.PerformsFunctionalUnitId
														join Billing.BillingConcept bc on ce.BillingConceptId = bc.Id
														left join GeneralLedger.MainAccounts ma on ma.Id = bc.DiscountAccountId
														WHERE	sod.RecordType = 1 AND bc.AccountingType=1
																AND sod.Presentation <> 2 
																AND sod.IsDelete = 0 AND @LiquidateMasterAccount=1 AND sodd.GrandTotalDiscount>0 and ma.Id is null
														GROUP BY bc.Code,bc.Name
													) x																			
												) 
					SET @Message = CONCAT('Los siguientes conceptos de facturación no tienen parametrizado una cuenta de descuento ' , @Message)
					SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)					
		END

		IF EXISTS(	SELECT 1 
					FROM @RevenueControlDetailCrossingList l
					JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) on l.RevenueControlDetailId = rcd.Id
					JOIN Billing.ServiceOrderDetailDistribution sodd on sodd.RevenueControlDetailId = rcd.Id
					JOIN Billing.ServiceOrderDetail sod on sodd.ServiceOrderDetailId = sod.Id
					JOIN Contract.CUPSEntity ce on sod.CUPSEntityId = ce.Id
					join Billing.BillingConcept bc on ce.BillingConceptId = bc.Id
					WHERE sod.IsDelete = 0 AND sod.GrandTotalSalesPrice>0 and bc.Status = 0
												
					)   BEGIN

					SELECT @Message = (
										SELECT STRING_AGG(x.[name], ' || ')
										FROM (
											SELECT ce.Code [name]
											FROM @RevenueControlDetailCrossingList l
											JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) ON l.RevenueControlDetailId = rcd.Id
											JOIN Billing.ServiceOrderDetailDistribution sodd ON sodd.RevenueControlDetailId = rcd.Id
											JOIN Billing.ServiceOrderDetail sod ON sodd.ServiceOrderDetailId = sod.Id
											JOIN Contract.CUPSEntity ce ON sod.CUPSEntityId = ce.Id
											JOIN Billing.BillingConcept bc ON ce.BillingConceptId = bc.Id
											WHERE sod.IsDelete = 0 AND sod.GrandTotalSalesPrice > 0 AND bc.Status = 0
											GROUP BY bc.Code, bc.Name, ce.Code
										) x
									)
					SET @Message = CONCAT('Los siguientes CUPS tienen un concepto de facturación inactivo ' , @Message)
					SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)					
		END

		IF EXISTS (
			SELECT 1
			FROM @RevenueControlDetailCrossingList l
			JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON l.RevenueControlDetailId = rcd.Id
			JOIN Common.ThirdParty tp WITH (NOLOCK) ON rcd.ThirdPartyId = tp.Id
			LEFT JOIN Common.Address a WITH (NOLOCK) ON tp.PersonId = a.IdPerson
			WHERE a.Id IS NULL OR a.DepartmentId IS NULL OR a.CityId IS NULL
		) BEGIN
			SELECT @Message = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + tp.Nit
					FROM @RevenueControlDetailCrossingList l
					JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON l.RevenueControlDetailId = rcd.Id
					JOIN Common.ThirdParty tp WITH (NOLOCK) ON rcd.ThirdPartyId = tp.Id
					LEFT JOIN Common.Address a WITH (NOLOCK) ON tp.PersonId = a.IdPerson
					WHERE a.Id IS NULL OR a.DepartmentId IS NULL OR a.CityId IS NULL
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			
			SET @Message = 'Los siguientes terceros no tienen parametrizada una dirección válida: ' + CHAR(13) + CHAR(10) + @Message
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END

		IF EXISTS (
			SELECT 1
			FROM @RevenueControlDetailCrossingList l
			JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON l.RevenueControlDetailId = rcd.Id
			JOIN Contract.CareGroup cg WITH (NOLOCK) ON rcd.CareGroupId = cg.Id
			WHERE cg.Status = 0
		) BEGIN
			SELECT @Message = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', cg.Code, ' - ', cg.Name)
					FROM @RevenueControlDetailCrossingList l
					JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON l.RevenueControlDetailId = rcd.Id
					JOIN Contract.CareGroup cg WITH (NOLOCK) ON rcd.CareGroupId = cg.Id
					WHERE cg.Status = 0
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			
			SET @Message = 'Los siguientes grupos de atención están inactivos: ' + CHAR(13) + CHAR(10) + @Message
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END

		IF EXISTS (
			SELECT 1
			FROM @RevenueControlDetailCrossingList l
			JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON l.RevenueControlDetailId = rcd.Id
			JOIN Contract.CareGroup cg WITH (NOLOCK) ON rcd.CareGroupId = cg.Id
			WHERE rcd.FolioType <> cg.CareGroupType and rcd.IsMasterAccount <> 4
		) BEGIN
			SELECT @Message = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - Folio: ', rcd.FolioOrder, ' (', cg.Code, ' - ', cg.Name, ')')
					FROM @RevenueControlDetailCrossingList l
					JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON l.RevenueControlDetailId = rcd.Id
					JOIN Contract.CareGroup cg WITH (NOLOCK) ON rcd.CareGroupId = cg.Id
					WHERE rcd.FolioType <> cg.CareGroupType
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			
			SET @Message = 'El tipo de grupo de atención de los siguientes folios no corresponde con el tipo del grupo de atención asociado: ' + CHAR(13) + CHAR(10) + @Message
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END

		IF EXISTS (
			SELECT 1
			FROM @RevenueControlDetailCrossingList l
			JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON l.RevenueControlDetailId = rcd.Id
			LEFT JOIN Common.Customer c WITH (NOLOCK) ON rcd.ThirdPartyId = c.ThirdPartyId
			WHERE rcd.FolioType IN (1,2,4) AND c.Id IS NULL AND rcd.IsMasterAccount <> 4
		) BEGIN
			SELECT @Message = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - Folio: ', rcd.FolioOrder)
					FROM @RevenueControlDetailCrossingList l
					JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON l.RevenueControlDetailId = rcd.Id
					LEFT JOIN Common.Customer c WITH (NOLOCK) ON rcd.ThirdPartyId = c.ThirdPartyId
					WHERE rcd.FolioType IN (1,2,4) AND c.Id IS NULL
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			
			SET @Message = 'No existe un cliente asociado al tercero de los siguientes folios: ' + CHAR(13) + CHAR(10) + @Message
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END

		IF EXISTS (
			SELECT 1
			FROM @RevenueControlDetailCrossingList l
			JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON l.RevenueControlDetailId = rcd.Id
			JOIN Contract.CareGroup cg WITH (NOLOCK) ON rcd.CareGroupId = cg.Id
			WHERE cg.ContractAccountingStructureId IS NULL
		) BEGIN
			SELECT @Message = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', cg.Code, ' - ', cg.Name)
					FROM @RevenueControlDetailCrossingList l
					JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON l.RevenueControlDetailId = rcd.Id
					JOIN Contract.CareGroup cg WITH (NOLOCK) ON rcd.CareGroupId = cg.Id
					WHERE cg.ContractAccountingStructureId IS NULL
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			
			SET @Message = 'Los siguientes grupos de atención no tienen una estructura contable asignada: ' + CHAR(13) + CHAR(10) + @Message
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END
		ELSE IF EXISTS (
			SELECT 1
			FROM @RevenueControlDetailCrossingList l
			JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON l.RevenueControlDetailId = rcd.Id
			JOIN Contract.CareGroup cg WITH (NOLOCK) ON rcd.CareGroupId = cg.Id
			JOIN Contract.ContractAccountingStructure cas WITH (NOLOCK) ON cg.ContractAccountingStructureId = cas.Id
			WHERE (cg.CareGroupType = 3 AND cas.AccountParticularId IS NULL)
		) BEGIN
			SELECT @Message = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', cg.Code, ' - ', cg.Name)
					FROM @RevenueControlDetailCrossingList l
					JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON l.RevenueControlDetailId = rcd.Id
					JOIN Contract.CareGroup cg WITH (NOLOCK) ON rcd.CareGroupId = cg.Id
					JOIN Contract.ContractAccountingStructure cas WITH (NOLOCK) ON cg.ContractAccountingStructureId = cas.Id
					WHERE rcd.FolioType = 3 AND cas.AccountParticularId IS NULL
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			
			SET @Message = 'Los siguientes grupos de atención son particulares pero no tiene la cuenta contable a particulares parametrizada: ' + CHAR(13) + CHAR(10) + @Message
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END

		IF EXISTS (
			SELECT 1
			FROM @RevenueControlDetailCrossingList l
			JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON l.RevenueControlDetailId = rcd.Id
			JOIN Contract.CareGroup cg WITH (NOLOCK) ON rcd.CareGroupId = cg.Id
			LEFT JOIN Contract.Contract c WITH (NOLOCK) ON cg.ContractId = c.Id AND c.Status = 1
			WHERE cg.CareGroupType = 1 AND c.Id IS NULL
		) BEGIN
			SELECT @Message = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', cg.Code, ' - ', cg.Name)
					FROM @RevenueControlDetailCrossingList l
					JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON l.RevenueControlDetailId = rcd.Id
					JOIN Contract.CareGroup cg WITH (NOLOCK) ON rcd.CareGroupId = cg.Id
					LEFT JOIN Contract.Contract c WITH (NOLOCK) ON cg.ContractId = c.Id AND c.Status = 1
					WHERE cg.CareGroupType = 1 AND c.Id IS NULL
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			
			SET @Message = 'Los siguientes grupos de atención no tienen contrato vigente o válido: ' + CHAR(13) + CHAR(10) + @Message
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END

		IF EXISTS (
			SELECT 1
			FROM @RevenueControlDetailCrossingList l
			JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON l.RevenueControlDetailId = rcd.Id
			JOIN Contract.CareGroup cg WITH (NOLOCK) ON rcd.CareGroupId = cg.Id
			LEFT JOIN Contract.ContractDetail cd WITH (NOLOCK) ON cg.ContractId = cd.ContractId AND cd.ValidRecord = 1
			WHERE cg.CareGroupType = 1 AND cd.Id IS NULL
		) BEGIN
			SELECT @Message = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', cg.Code, ' - ', cg.Name)
					FROM @RevenueControlDetailCrossingList l
					JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON l.RevenueControlDetailId = rcd.Id
					JOIN Contract.CareGroup cg WITH (NOLOCK) ON rcd.CareGroupId = cg.Id
					LEFT JOIN Contract.ContractDetail cd WITH (NOLOCK) ON cg.ContractId = cd.ContractId AND cd.ValidRecord = 1
					WHERE cg.CareGroupType = 1 AND cd.Id IS NULL
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			
			SET @Message = 'Los siguientes grupos de atención no tienen contrato o este no tiene un detalle válido: ' + CHAR(13) + CHAR(10) + @Message
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END

		IF EXISTS (
			SELECT 1
			FROM @RevenueControlDetailCrossingList l
			JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON l.RevenueControlDetailId = rcd.Id
			JOIN Contract.CareGroup cg WITH (NOLOCK) ON rcd.CareGroupId = cg.Id
			JOIN Contract.ContractDetail cd WITH (NOLOCK) ON cg.ContractId = cd.ContractId AND cd.ValidRecord = 1 AND cd.TerminationControl IN (2, 4)
			WHERE cg.CareGroupType = 1 AND (Common.[GETDATE]() > cd.EndDate OR Common.[GETDATE]() > cd.BillingEndDate)
		) BEGIN
			SELECT @Message = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', cg.Code, ' - ', cg.Name)
					FROM @RevenueControlDetailCrossingList l
					JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON l.RevenueControlDetailId = rcd.Id
					JOIN Contract.CareGroup cg WITH (NOLOCK) ON rcd.CareGroupId = cg.Id
					JOIN Contract.ContractDetail cd WITH (NOLOCK) ON cg.ContractId = cd.ContractId AND cd.ValidRecord = 1 AND cd.TerminationControl IN (2, 4)
					WHERE cg.CareGroupType = 1 AND (Common.[GETDATE]() > cd.EndDate OR Common.[GETDATE]() > cd.BillingEndDate)
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			
			SET @Message = 'Los contratos de los siguientes grupos de atención ya vencieron: ' + CHAR(13) + CHAR(10) + @Message
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END

		IF EXISTS (
			SELECT 1
			FROM @RevenueControlDetailCrossingList l
			JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON l.RevenueControlDetailId = rcd.Id
			JOIN Contract.CareGroup cg WITH (NOLOCK) ON rcd.CareGroupId = cg.Id
			JOIN Contract.Contract c WITH (NOLOCK) ON cg.ContractId = c.Id
			JOIN Contract.ContractDetail cd WITH (NOLOCK) ON cg.ContractId = cd.ContractId AND cd.ValidRecord = 1 AND cd.TerminationControl IN (3, 4)
			WHERE cg.CareGroupType = 1
			GROUP BY c.Id, c.ContractValue, c.ExecuteValue
			HAVING c.ContractValue < c.ExecuteValue + SUM(rcd.TotalFolio - rcd.TotalPatientSalesPrice)
		) BEGIN
			SELECT @Message = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', c.Code, ' - ', c.ContractName)
					FROM @RevenueControlDetailCrossingList l
					JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON l.RevenueControlDetailId = rcd.Id
					JOIN Contract.CareGroup cg WITH (NOLOCK) ON rcd.CareGroupId = cg.Id
					JOIN Contract.Contract c WITH (NOLOCK) ON cg.ContractId = c.Id
					JOIN Contract.ContractDetail cd WITH (NOLOCK) ON cg.ContractId = cd.ContractId AND cd.ValidRecord = 1 AND cd.TerminationControl IN (3, 4)
					WHERE cg.CareGroupType = 1
					GROUP BY c.Id, c.Code, c.ContractName, c.ContractValue, c.ExecuteValue
					HAVING c.ContractValue < c.ExecuteValue + SUM(rcd.TotalFolio - rcd.TotalPatientSalesPrice)
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			
			SET @Message = 'Se supero el valor de los siguientes contratos: ' + CHAR(13) + CHAR(10) + @Message
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END

		IF EXISTS (
			SELECT 1 
			FROM @RevenueControlDetailCrossingList l
			JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON l.RevenueControlDetailId = rcd.Id
			JOIN Contract.CareGroup cg WITH (NOLOCK) ON rcd.CareGroupId = cg.Id
			WHERE cg.EntityType <> 99 AND cg.CareGroupType <> 3 AND HealthAdministratorId IS NULL
		) BEGIN
			SET @Message = 'La factura no puede quedar con la entidad administradora vacia'
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END

		IF EXISTS (
			SELECT 1
			FROM @RevenueControlDetailCrossingList l
			JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON l.RevenueControlDetailId = rcd.Id
			JOIN Contract.CareGroup cg WITH (NOLOCK) ON rcd.CareGroupId = cg.Id
			JOIN Contract.HealthAdministrator ha WITH (NOLOCK) ON rcd.HealthAdministratorId = ha.Id
			WHERE cg.CareGroupType <> 3
				AND rcd.ThirdPartyId <> ha.ThirdPartyId AND rcd.IsMasterAccount <> 4
		) BEGIN
			SET @Message = 'El tercero del folio no corresponde con el tercero de la entidad administradora'
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END

		IF NOT EXISTS (
			SELECT 1
			FROM Portfolio.PortfolioSequence s WITH (NOLOCK)
			JOIN Portfolio.PortfolioSequenceDetail sd WITH (NOLOCK) ON s.Id = sd.IdSequensePortfolioC
			JOIN Common.Sequense cs on sd.IdSequense = cs.Id
			WHERE s.IdForm = '682' AND s.IsManual = 0
				AND 
				(
					(s.Scope = 'O')
					OR
					(s.Scope = 'OU' AND sd.IdOperatingUnit = @OperativeUnitId)
				)
		) BEGIN
			SET @Message = 'No existe una secuencia automatica de Cuentas Por Cobrar'
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END

		IF NOT EXISTS (
			SELECT 1
			FROM Portfolio.PortfolioSequence s WITH (NOLOCK)
			JOIN Portfolio.PortfolioSequenceDetail sd WITH (NOLOCK) ON s.Id = sd.IdSequensePortfolioC
			JOIN Common.Sequense cs on sd.IdSequense = cs.Id
			WHERE s.IdForm = '1510' AND s.IsManual = 0
				AND 
				(
					(s.Scope = 'O')
					OR
					(s.Scope = 'OU' AND sd.IdOperatingUnit = @OperativeUnitId)
				)
		) BEGIN
			SET @Message = 'No existe una secuencia automatica de Pagaré'
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END

		IF EXISTS(	SELECT 1 
					FROM @RevenueControlDetailCrossingList l
					JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) on l.RevenueControlDetailId = rcd.Id
					JOIN Contract.CareGroup cg on rcd.CareGroupId = cg.Id
					LEFT JOIN Billing.BillingConcept bc ON cg.BillingConceptCopayId = bc.Id
					LEFT JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id = iif(cg.LiquidationType = 1, bc.CopayMainAccountId, bc.RecoveryFixedAmountMainAccountId)
					WHERE rcd.TotalPatientSalesPrice > 0 AND @LiquidateMasterAccount= 0 AND ma.Id IS NULL) BEGIN

			SET @Message = 'La cuenta contable para el copago no está parametrizada en el sistema. Por favor, revise la configuración.'
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END

		/*************************************************  DETALLES *************************************************/
		Declare @AllowBillingWithoutAuthorization Int

		SELECT @AllowBillingWithoutAuthorization = SI.AllowBillingWithoutAuthorization 
		FROM Inventory.SettingInventory SI 
		WHERE SI.OperatingUnitId = @OperativeUnitId

		If @AllowBillingWithoutAuthorization = 0 Begin		
			IF EXISTS (
				SELECT 1 FROM (
					SELECT (SUM(ISNULL(pm.CANPEDPRO, 0)) - [dbo].[fnCalcularCantidadDispensada](NUMINGRES, pm.CODPRODUC) - SUM(sod.InvoicedQuantity)) AS Saldo
					FROM @RevenueControlDetailCrossingList l
					JOIN Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK) ON l.RevenueControlDetailId = sodd.RevenueControlDetailId
					JOIN Billing.ServiceOrderDetail AS sod WITH (NOLOCK) ON sodd.ServiceOrderDetailId = sod.Id
					JOIN Inventory.InventoryProduct pr WITH(NOLOCK) ON pr.Id = sod.ProductId
					JOIN Inventory.ATC a WITH(NOLOCK) on pr.ATCId = a.Id
					JOIN IHLISTPRO ih WITH(NOLOCK) ON ih.CODPRODUC = a.Code
					LEFT JOIN ..HCJUNOPOM pm on pm.CODPRODUC = ih.CODPRODUC AND pm.NUMINGRES = @AdmissionNumber
					WHERE sod.SettlementType = 3 AND sodd.GrandTotalSalesPrice > 0 AND ih.NOPOSPROD = 1
					GROUP BY pm.NUMINGRES, pm.CODPRODUC
				) AS t WHERE t.Saldo <= 0
			) BEGIN
				SET @Message = 'Debe contar con Autorización MIPRES para poder facturar' + CHAR(13) + CHAR(10)
				SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
			END
		End

		--If @AllowBillingWithoutAuthorization = 0 Begin		
		--	IF NOT EXISTS (
		--		SELECT 1 FROM (
		--			SELECT (SUM(ISNULL(pm.CANPEDPRO, 0)) - [dbo].[fnCalcularCantidadDispensada](NUMINGRES, pm.CODPRODUC) - SUM(sod.InvoicedQuantity)) AS Saldo
		--			FROM @RevenueControlDetailCrossingList l
		--			JOIN Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK) ON l.RevenueControlDetailId = sodd.RevenueControlDetailId
		--			JOIN Billing.ServiceOrderDetail AS sod WITH (NOLOCK) ON sodd.ServiceOrderDetailId = sod.Id
		--			JOIN Inventory.InventoryProduct pr WITH(NOLOCK) ON pr.Id = sod.ProductId
		--			JOIN Inventory.ATC a WITH(NOLOCK) on pr.ATCId = a.Id
		--			JOIN IHLISTPRO ih WITH(NOLOCK) ON ih.CODPRODUC = a.Code
		--			JOIN ..HCJUNOPOM pm on pm.CODPRODUC = ih.CODPRODUC
		--			WHERE pm.NUMINGRES = @AdmissionNumber AND sod.SettlementType = 3 AND sodd.GrandTotalSalesPrice > 0 AND ih.NOPOSPROD = 1
		--			GROUP BY pm.NUMINGRES, pm.CODPRODUC
		--		) AS t WHERE t.Saldo > 0
		--	) BEGIN
		--		SET @Message = 'Debe contar con Autorización MIPRES para poder facturar' + CHAR(13) + CHAR(10)
		--		SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		--	END
		--End

		IF EXISTS (
			SELECT 1
			FROM @RevenueControlDetailCrossingList l
			JOIN Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK) ON l.RevenueControlDetailId = sodd.RevenueControlDetailId
			JOIN Billing.ServiceOrderDetail AS sod WITH (NOLOCK) ON sodd.ServiceOrderDetailId = sod.Id
			WHERE sod.SettlementType = 3 AND sodd.GrandTotalSalesPrice > 0
		) BEGIN
			SELECT @Message = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - Folio ', rcd.FolioOrder, '(Item: ', ISNULL(ips.Name, ip.Name), ' - Cantidad: ', sod.InvoicedQuantity, ' - Valor Unitario: ', sod.TotalSalesPrice, ' - Total: ', sodd.GrandTotalSalesPrice, ')')
					FROM @RevenueControlDetailCrossingList l
					JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON l.RevenueControlDetailId = rcd.Id
					JOIN Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK) ON l.RevenueControlDetailId = sodd.RevenueControlDetailId
					JOIN Billing.ServiceOrderDetail AS sod WITH (NOLOCK) ON sodd.ServiceOrderDetailId = sod.Id
					LEFT JOIN [Contract].IPSService ips WITH (NOLOCK) ON sod.IPSServiceId = ips.Id
					LEFT JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON sod.ProductId = ip.Id
					WHERE sod.SettlementType = 3 AND sodd.GrandTotalSalesPrice > 0
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			
			SET @Message = 'Los siguientes items se encuentran incluidos al 100% pero el valor es mayor a 0: ' + CHAR(13) + CHAR(10) + @Message
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END

		IF EXISTS (
			SELECT 1
			FROM Billing.ServiceOrder AS so WITH (NOLOCK)
			JOIN Billing.ServiceOrderDetail AS sod WITH (NOLOCK) ON so.Id = sod.ServiceOrderId
			LEFT JOIN Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK) ON sod.Id = sodd.ServiceOrderDetailId
			WHERE so.AdmissionNumber = @AdmissionNumber AND so.Status = 1
				AND sod.Packaging = 0 AND sod.IsDelete = 0 AND sod.InvoicedQuantity > 0	
				AND sodd.Id IS NULL
		) BEGIN
			SELECT @Message = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - Orden de Servicio ', so.Code, '(Item: ', ISNULL(ips.Name, ip.Name), ' - Cantidad: ', sod.InvoicedQuantity, ' - Valor Unitario: ', sod.TotalSalesPrice, ' - Total: ', sodd.GrandTotalSalesPrice, ')')
					FROM Billing.ServiceOrder AS so WITH (NOLOCK)
					JOIN Billing.ServiceOrderDetail AS sod WITH (NOLOCK) ON so.Id = sod.ServiceOrderId
					LEFT JOIN Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK) ON sod.Id = sodd.ServiceOrderDetailId
					LEFT JOIN [Contract].IPSService ips WITH (NOLOCK) ON sod.IPSServiceId = ips.Id
					LEFT JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON sod.ProductId = ip.Id
					WHERE so.AdmissionNumber = @AdmissionNumber AND so.Status = 1
						AND sod.Packaging = 0 AND sod.IsDelete = 0 AND sod.InvoicedQuantity > 0	
						AND sodd.Id IS NULL
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			
			SET @Message = 'Los siguientes items de la admisión no tienen distribucion: ' + CHAR(13) + CHAR(10) + @Message
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END

		/****************Nueva Validacion IVA****************************/

		IF EXISTS(SELECT 1 FROM GeneralLedger.CompanySettings where SalePriceIncludeTax=1) BEGIN
			IF EXISTS (
				SELECT 1
				FROM @RevenueControlDetailCrossingList l
				JOIN Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK) ON l.RevenueControlDetailId = sodd.RevenueControlDetailId
				JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sodd.ServiceOrderDetailId = sod.Id
				WHERE sodd.DistributionType = 1 AND sod.SupplyQuantity - sod.DevolutionQuantity > 0 AND
					(
						ROUND(sod.InvoicedQuantity * sod.TotalSalesPrice, 2) <> sodd.GrandTotalSalesPrice 
						AND
						ROUND(sod.InvoicedQuantity * sod.TotalSalesPrice, -2) <> sodd.GrandTotalSalesPrice
					)
				) BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - Folio ', rcd.FolioOrder, '(Item: ', ISNULL(ips.Name, ip.Name), ' - Cantidad: ', sod.InvoicedQuantity, ' - Valor Unitario: ', sod.TotalSalesPrice, ' - Total: ', sodd.GrandTotalSalesPrice, ')')
						FROM @RevenueControlDetailCrossingList l
						JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON l.RevenueControlDetailId = rcd.Id
						JOIN Contract.CareGroup cg WITH (NOLOCK) ON rcd.CareGroupId = cg.Id
						JOIN Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK) ON l.RevenueControlDetailId = sodd.RevenueControlDetailId
						JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sodd.ServiceOrderDetailId = sod.Id
						LEFT JOIN [Contract].IPSService ips WITH (NOLOCK) ON sod.IPSServiceId = ips.Id
						LEFT JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON sod.ProductId = ip.Id
						WHERE sodd.DistributionType = 1 AND
							(
								ROUND(sod.InvoicedQuantity * sod.TotalSalesPrice, 2) <> sodd.GrandTotalSalesPrice 
								AND
								ROUND(sod.InvoicedQuantity * sod.TotalSalesPrice, -2) <> sodd.GrandTotalSalesPrice
							)
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			
				SET @Message = 'Los siguientes items se encuentran desbalanceados: ' + CHAR(13) + CHAR(10) + @Message
				SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
			END			
		END
		ELSE BEGIN 
			IF EXISTS (
				SELECT 1
				FROM @RevenueControlDetailCrossingList l
				JOIN Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK) ON l.RevenueControlDetailId = sodd.RevenueControlDetailId
				JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sodd.ServiceOrderDetailId = sod.Id
				JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) ON rcd.Id = sodd.RevenueControlDetailId
				left JOIN GeneralLedger.GeneralLedgerIVA iva WITH(NOLOCK) on sod.IvaId = iva.Id
				WHERE sodd.DistributionType = 1 AND
					(
						(rcd.IsMasterAccount is NULL AND sodd.SubTotalSalesPrice=0 AND
								ROUND(((sod.GrossValue * sod.InvoicedQuantity) + ((sod.GrossValue * sod.InvoicedQuantity)*(isnull(iva.Percentage,0)/100)))
										-(((sod.GrossValue + (sod.GrossValue*(isnull(iva.Percentage,0)/100))) * (sod.ThirdPartyDiscountPercentage/100)) * sod.InvoicedQuantity) , 2) <> sodd.GrandTotalSalesPrice 
								AND
								ROUND(((sod.GrossValue * sod.InvoicedQuantity) + ((sod.GrossValue * sod.InvoicedQuantity)*(isnull(iva.Percentage,0)/100)))
									-(((sod.GrossValue + (sod.GrossValue*(isnull(iva.Percentage,0)/100))) * (sod.ThirdPartyDiscountPercentage/100)) * sod.InvoicedQuantity) ,-2) <> sodd.GrandTotalSalesPrice
							) 
							OR ( sodd.SubTotalSalesPrice >0 AND (sodd.SubTotalSalesPrice-sodd.GrandTotalDiscount+sodd.GrandTotalTaxes) <> sodd.GrandTotalSalesPrice)
							
					)
			) BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - Folio ', rcd.FolioOrder, '(Item: ', ISNULL(ips.Name, ip.Name), ' - Cantidad: ', sod.InvoicedQuantity, ' - Valor Unitario: ', sod.TotalSalesPrice, ' - Total: ', sodd.GrandTotalSalesPrice, ')')
						FROM @RevenueControlDetailCrossingList l
						JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON l.RevenueControlDetailId = rcd.Id
						JOIN Contract.CareGroup cg WITH (NOLOCK) ON rcd.CareGroupId = cg.Id
						JOIN Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK) ON l.RevenueControlDetailId = sodd.RevenueControlDetailId
						JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sodd.ServiceOrderDetailId = sod.Id
						LEFT JOIN [Contract].IPSService ips WITH (NOLOCK) ON sod.IPSServiceId = ips.Id
						LEFT JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON sod.ProductId = ip.Id
						left JOIN GeneralLedger.GeneralLedgerIVA iva WITH(NOLOCK) on sod.IvaId = iva.Id
						WHERE sodd.DistributionType = 1 AND
							(
								ROUND((sod.GrossValue * sod.InvoicedQuantity) + ((sod.GrossValue * sod.InvoicedQuantity)*(isnull(iva.Percentage,0)/100)), 2) <> sodd.GrandTotalSalesPrice 
								AND
								ROUND((sod.GrossValue * sod.InvoicedQuantity) + ((sod.GrossValue * sod.InvoicedQuantity)*(isnull(iva.Percentage,0)/100)), -2) <> sodd.GrandTotalSalesPrice
							)
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			
				SET @Message = 'Los siguientes items se encuentran desbalanceados A: ' + CHAR(13) + CHAR(10) + @Message
				SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
			END	
		END

		/***************************************************************/
		
		IF EXISTS (
			SELECT 1
			FROM @RevenueControlDetailCrossingList l
			JOIN Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK) ON l.RevenueControlDetailId = sodd.RevenueControlDetailId 
			JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sodd.ServiceOrderDetailId = sod.Id
			LEFT JOIN Contract.RateManual rm WITH (NOLOCK) ON sod.RateManualId = rm.Id
			JOIN Billing.ServiceOrderDetailSurgical sods WITH (NOLOCK) ON sod.Id = sods.ServiceOrderDetailId
			GROUP BY sodd.Id, sod.Id, sod.GrandTotalSalesPrice
			HAVING sod.GrandTotalSalesPrice <> SUM(sods.TotalSalesPrice) AND
					 sod.GrandTotalSalesPrice <> SUM([Billing].[RoundValue](sods.TotalSalesPrice, ISNULL(rm.RoundService, 2)))
			--Se aplica el porcentaje de descuento cuando el tipo de liquidacion es '% del mismo servicio', sino, se continua condicionando como estaba
		) BEGIN
			SET @Message = 'Existen detalles cuyo valor no corresponde con la sumatoria de los quirurgicos'
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END
		
		IF EXISTS (
			SELECT 1
			FROM @RevenueControlDetailCrossingList l
			JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON l.RevenueControlDetailId = rcd.Id
			LEFT JOIN 
			(
				SELECT l.RevenueControlDetailId, SUM(sodd.ThirdPartySalesPrice) ThirdPartySalesPrice
				FROM @RevenueControlDetailCrossingList l
				JOIN Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK) ON l.RevenueControlDetailId = sodd.RevenueControlDetailId 
				JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sodd.ServiceOrderDetailId = sod.Id AND sod.IsDelete = 0
				GROUP BY l.RevenueControlDetailId
			) sodd ON rcd.Id = sodd.RevenueControlDetailId
			WHERE (rcd.TotalFolio - rcd.TotalPatientSalesPrice) <> ISNULL(sodd.ThirdPartySalesPrice, 0) AND @LiquidateMasterAccount = 0
		) BEGIN
			SELECT @Message = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - Folio ', rcd.FolioOrder, '(Valor: ', FORMAT((rcd.TotalFolio - rcd.TotalPatientSalesPrice), 'C0', 'es-CO'), ' - Detalles: ', FORMAT(ISNULL(sodd.ThirdPartySalesPrice, 0), 'C0', 'es-CO'), ')')
					FROM @RevenueControlDetailCrossingList l
					JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON l.RevenueControlDetailId = rcd.Id
					LEFT JOIN 
					(
						SELECT l.RevenueControlDetailId, SUM(sodd.ThirdPartySalesPrice) ThirdPartySalesPrice
						FROM @RevenueControlDetailCrossingList l
						JOIN Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK) ON l.RevenueControlDetailId = sodd.RevenueControlDetailId 
						JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sodd.ServiceOrderDetailId = sod.Id AND sod.IsDelete = 0
						GROUP BY l.RevenueControlDetailId
					) sodd ON rcd.Id = sodd.RevenueControlDetailId
					WHERE (rcd.TotalFolio - rcd.TotalPatientSalesPrice) <> ISNULL(sodd.ThirdPartySalesPrice, 0)
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			
			SET @Message = 'El valor de los siguientes folios no corresponden con el de sus detalles: ' + CHAR(13) + CHAR(10) + @Message
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END

		/********************************************  CRUCE DE ANTICIPOS ********************************************/

		WHILE @Rows > 0
		BEGIN
			SELECT TOP 1
				@RowId = RowId,
				@TotalCrossingValue = 0,
				@ListPortfolioAdvanceXml = ListPortfolioAdvanceCrossing,
				@Message = NULL,
				@TransactionCurrencyId = CurrencyId
			FROM @RevenueControlDetailCrossingList
			WHERE RowId > @RowId 
			ORDER BY RowId

			SET @Rows = @@RowCount
			IF @Rows = 0 
				BREAK

			-----------------------------------------------------------------------------------------------------------
			DELETE FROM @ListPortfolioAdvanceCrossing
			INSERT INTO @ListPortfolioAdvanceCrossing
				SELECT	t.x.value('Id[1]', 'INT'),
						t.x.value('Code[1]', 'VARCHAR(20)'),
						t.x.value('CrossingValue[1]', 'DECIMAL(18, 2)'),
						t.x.value('CashReceiptDetailIdTmp[1]', 'INT')
				FROM @ListPortfolioAdvanceXml.nodes('ListPortfolioAdvanceCrossing') t(x)

			-----------------------------------------------------------------------------------------------------------

			SET @TotalCrossingValue =Common.CurrencyConverter( COALESCE((SELECT SUM(CrossingValue) FROM @ListPortfolioAdvanceCrossing), 0),@TransactionCurrencyId,@OfficialCurrencyId)

			--IF EXISTS (
			--	SELECT 1
			--	FROM @RevenueControlDetailCrossingList l
			--	JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON l.RevenueControlDetailId = rcd.Id
			--	WHERE l.RowId = @RowId And @TotalCrossingValue > IIF(rcd.FolioType = 3 or rcd.IsMasterAccount <> 0, rcd.TotalFolio, rcd.TotalPatientWithDiscount)					
			--) BEGIN
			--	SELECT @Message = CONCAT('El total a cruzar del folio ', rcd.FolioOrder , ' no debe superar al valor a pagar por el paciente')
			--	FROM @RevenueControlDetailCrossingList l
			--	JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON l.RevenueControlDetailId = rcd.Id
			--	WHERE @TotalCrossingValue > IIF(rcd.FolioType = 3, rcd.TotalFolio, rcd.TotalPatientWithDiscount)					

			--	SET @Message = ISNULL(@Message, 'El total a cruzar no debe superar al valor a pagar por el paciente')
			--	SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
			--END

			IF EXISTS (
				SELECT 1
				FROM @RevenueControlDetailCrossingList l
				JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON l.RevenueControlDetailId = rcd.Id
				WHERE rcd.FolioType <> 3 AND @TotalCrossingValue < rcd.TotalPatientWithDiscount
			) BEGIN

				IF EXISTS(	SELECT 1
							FROM Billing.SettingsBilling sb 
							where sb.IdOperatingUnit =@OperativeUnitId AND sb.GeneratePromissoryNote = 0 ) BEGIN

							SET @Message = 'No existe un anticipo de copago o cuota moderadora asociado al ingreso del paciente'
							SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)				
				END

				IF NOT EXISTS
				(
					SELECT 1
					FROM Security.[User] u
					LEFT JOIN Security.PermissionUser pu ON u.Id = pu.IdUser AND 756 = pu.IdForm AND 59 = pu.Action
					LEFT JOIN Security.PermissionRoll pr ON u.RollCode = pr.IdRoll AND 756 = pr.IdForm AND 59 = pr.Action
					WHERE u.UserCode = @UserCode AND COALESCE(pu.Id, pr.Id, 0) > 0
				)
				BEGIN
					SET @Message = 'El usuario no tiene permiso para realizar pagaré al paciente'
					SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
				END
			END		
			
			/****************VALIDACION TAXDEVOLUTION**************************/
			IF	EXISTS(	SELECT 1 
						FROM @RevenueControlDetailCrossingList temp
						WHERE temp.TaxDevolutionValue > 0 and temp.RowId = @RowId) 
						AND 
				EXISTS(	SELECT COUNT(*)
						FROM(	SELECT 1 tempId
								FROM Portfolio.PortfolioAdvance pa WITH (NOLOCK)
								JOIN 
									(
										SELECT	ipa.Id PortfolioAdvanceId, 
												SUM(ipa.CrossingValue) Value
										FROM @ListPortfolioAdvanceCrossing ipa
										GROUP BY ipa.Id
									) ipa ON pa.Id = ipa.PortfolioAdvanceId
								JOIN Treasury.CashReceipts cr WITH(NOLOCK) on pa.CashReceiptId = cr.Id
								JOIN Treasury.PaymentMethods pm WITH(NOLOCK) ON cr.Id = pm.IdCashReceipt
								GROUP by pm.PaymentMethodTypes)a
						GROUP by a.tempId
						HAVING count(a.tempId) > 1
						
				) BEGIN
					SET @Message = 'Se esta aplicando devolución de IVA, pero los anticipos tienen metodos de pago diferentes.'
					SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
				END
			/*************************************************************************/			
			--IF EXISTS
			--		(
			--			SELECT 1
			--			FROM Portfolio.PortfolioAdvance pa WITH (NOLOCK)
			--			JOIN 
			--			(
			--				SELECT	ipa.Id PortfolioAdvanceId, 
			--						SUM(ipa.CrossingValue) Value
			--				FROM @ListPortfolioAdvanceCrossing ipa
			--				GROUP BY ipa.Id
			--			) ipa ON pa.Id = ipa.PortfolioAdvanceId
			--			WHERE Common.CurrencyConverter(ipa.Value ,@TransactionCurrencyId,ISNULL(pa.CurrencyId,@OfficialCurrencyId)) > pa.Balance
			--		)
			--	BEGIN
			--			SELECT @Message = STUFF((
			--					SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', pa.Code)
			--					FROM Portfolio.PortfolioAdvance pa WITH (NOLOCK)
			--					JOIN 
			--					(
			--						SELECT	ipa.Id PortfolioAdvanceId, 
			--								SUM(ipa.CrossingValue) Value
			--						FROM @ListPortfolioAdvanceCrossing ipa
			--						GROUP BY ipa.Id
			--					) ipa ON pa.Id = ipa.PortfolioAdvanceId
			--					WHERE Common.CurrencyConverterByModule(ipa.Value ,@TransactionCurrencyId,ISNULL(pa.CurrencyId,@OfficialCurrencyId)) > pa.Balance
			--					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			
			--			SET @Message = 'El valor a cruzar supera el saldo de los siguientes anticipo: ' + CHAR(13) + CHAR(10) + @Message
			--			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
			--	END	

		END

		/* Validacion Ingreso al generar cualquier factura siempre y cuando el parametro liquida cuenta madre este activo*/
			IF @LiquidateMasterAccount = 1  BEGIN
			
				EXEC [Billing].[SP_CloseAdmissionValidations_Output] @AdmissionNumber, @ResultStatus OUT, @Message OUT
				IF @ResultStatus = 0
				BEGIN
					SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
				END
			END
		/******************************************* FIN VALIDACION CR **********************************************/	

		/**************** VALIDACIONES CONTROL DE CUENTAS *****************/		
		IF NOT EXISTS (
			SELECT 1
			FROM Billing.RevenueControl rc WITH (NOLOCK)
			JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON rc.Id = rcd.RevenueControlId
			WHERE rc.AdmissionNumber = @AdmissionNumber AND rcd.Status IN (1, 3, 5) 
				AND rcd.Id NOT IN (SELECT RevenueControlDetailId FROM @RevenueControlDetailCrossingList)
		) BEGIN
			IF @SkipAccountControlValidations = 1 AND NOT EXISTS (
				SELECT 1
				FROM Security.[User] u
				LEFT JOIN Security.PermissionUser pu ON u.Id = pu.IdUser AND pu.IdForm = 756 AND pu.Action = 138 AND pu.ActionValue = 1
				LEFT JOIN Security.PermissionRoll pr ON u.RollCode = pr.IdRoll AND pr.IdForm = 756 AND pr.Action = 138 AND pr.ActionValue = 1
				WHERE u.UserCode = @UserCode AND COALESCE(pu.Id, pr.Id, 0) > 0
			) BEGIN
				SET @ResultStatus = CONVERT(BIT, 0)
				SET @Message = ''
				SET @ResultMessage = 'El usuario no posee permisos para evadir las validaciones de control de cuentas'
				RETURN
			END

			/*************************************************  ADMISION *************************************************/
			--Si no liquida cuenta madre entra hacer la verificacion -- COLOMBIA
			-- Porque si liquida cuenta madre ya tuvo que haber validado el ingreso.
			IF isnull( @LiquidateMasterAccount,0) = 0 begin
			EXEC [Billing].[SP_CloseAdmissionValidations_Output] @AdmissionNumber, @ResultStatus OUT, @Message OUT

				IF @ResultStatus = 0
				BEGIN
					SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
				END
			END

			DECLARE  @AdmissionType INT
			SELECT TOP 1 @AdmissionType = TIPOINGRE FROM ADINGRESO WHERE NUMINGRES = @AdmissionNumber

			IF @AccountControlValidation IS NOT NULL
				AND @AccountControlValidation = 1
				AND ((@AdmissionType = '2') or EXISTS(SELECT 1 
														FROM ADINGRESO ad with(NOLOCK)
														JOIN INUNIFUNC inu WITH(NOLOCK) on ad.UFUEGRMED = inu.UFUCODIGO
														where ad.NUMINGRES = @AdmissionNumber and TIPOINGRE ='1' AND inu.UFUTIPUNI in ('1','12','13','18','19','22','23','33','34','35')))
				AND @SkipAccountControlValidations = 0 BEGIN				
				DECLARE @userRolId Int, @UserId Int
				SELECT TOP 1 @userRolId = RollCode, @UserId = Id FROM Security.[User] WHERE UserCode = @UserCode

				--VALORACIONES				
				IF EXISTS (	SELECT 1 
							FROM dbo.HCHISPACA A
							LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) on A.ID=acj.EntityId and acj.EntityName='HCHISPACA' and acj.EntityTap='INDlcgValoraciones'
							LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
							WHERE NUMINGRES = @AdmissionNumber AND GENSERVICEORDER IS NULL AND (isnull(bjc.SkipClearance,0)=0)) BEGIN
					--SET @Message = 'Existen registros de valoraciones pendientes por generar'
					--SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
					SET @ResultStatus = CONVERT(BIT, 0)
					SET @Message = ''
					SET @ResultMessage = '-001'
					RETURN
				END

				--PROCEDIMIENTOS DE ENFERMERIA
				IF EXISTS (SELECT 1 
							FROM dbo.HCHOGASIN A 
							JOIN dbo.HCACTENFE B ON A.CODACTENF=B.CODACTENF 
							LEFT JOIN INCUPSIPS C ON B.CODSERIPS=C.CODSERIPS 
							LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) on A.CONSECUTI=acj.EntityId and acj.EntityName='HCHOGASIN' and acj.EntityTap='INDlcgNurseProcedure'
							LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
							WHERE A.NUMINGRES = @AdmissionNumber AND C.CODSERIPS IS NOT NULL AND A.GENSERVICEORDER IS NULL AND (isnull(bjc.SkipClearance,0)=0)) BEGIN
					--SET @Message = 'Existen registros de procedimientos de enfermería pendientes por generar'
					--SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
					SET @ResultStatus = CONVERT(BIT, 0)
					SET @Message = ''
					SET @ResultMessage = '-001'
					RETURN
				END								
							
				--ESTANCIA
				IF @AdmissionType = 2 
				BEGIN
					--Hospitalario					
					IF EXISTS (
						SELECT 1
						FROM Billing.AccountControlStays acc (NOLOCK)
						LEFT JOIN Billing.AccountControlJustification acj (NOLOCK) ON acj.EntityId = acc.Id AND acj.EntityName = 'AccountControlStays' AND acj.EntityTap = 'INDLcgStays'
						LEFT JOIN Billing.BillingJustificationControl bj (NOLOCK) ON acj.JustificationId = bj.Id
						WHERE acc.AdmissionCode = @AdmissionNumber AND ((acj.Id IS NULL AND acc.LiquidationDate IS NULL) OR bj.SkipClearance = 0)
					)
					BEGIN						
						SET @ResultStatus = CONVERT(BIT, 0)
						SET @Message = ''
						SET @ResultMessage = '-001'
					END
				END

				--CONSUMO DE OXIGENO
				IF EXISTS (SELECT 1 
							FROM dbo.HCCONOXIG A
							LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) on A.IDCONOXIG=acj.EntityId and acj.EntityName='HCCONOXIG' and acj.EntityTap='INDlcgOxigenConsumer'
							LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
							WHERE NUMINGRES = @AdmissionNumber AND GENSERVICEORDER IS NULL AND (isnull(bjc.SkipClearance,0)=0)) BEGIN
					--SET @Message = 'Existen registros de consumo de oxígeno pendientes por generar'
					--SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
					SET @ResultStatus = CONVERT(BIT, 0)
					SET @Message = ''
					SET @ResultMessage = '-001'
					RETURN
				END

				--TERAPIAS
				IF EXISTS (SELECT 1 
							FROM dbo.HCPROCTER A 
							JOIN dbo.INCUPSIPS AS B ON A.CODSERIPS = B.CODSERIPS 
							LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) on A.CODCONSEC=acj.EntityId and acj.EntityName='HCPROCTER' and acj.EntityTap='INDlcgTerapy'
							LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
							WHERE B.TIPSERTER = 1 AND NUMINGRES = @AdmissionNumber AND A.GENSERVICEORDER IS NULL AND (isnull(bjc.SkipClearance,0)=0)) BEGIN
					--SET @Message = 'Existen registros de terapias pendientes por generar'
					--SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
					SET @ResultStatus = CONVERT(BIT, 0)
					SET @Message = ''
					SET @ResultMessage = '-001'
					RETURN
				END

				--HEMOCOMPONENTES				
				IF EXISTS (SELECT 1 AS GENSERVICEORDER 
					FROM HCORHEMSER SER 
					JOIN INCUPSIPS CUPS ON CUPS.CODSERIPS = SER.CODSERIPS 
					JOIN HCORHEMCO SOL ON SOL.ID = SER.HCORHEMCOID 
					JOIN ADINGRESO ING ON SOL.NUMINGRES = ING.NUMINGRES
					LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) on SER.ID=acj.EntityId and acj.EntityName='HCORHEMSER' and acj.EntityTap='INDLcgHemo'
					LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
					WHERE SER.ESTADO = 2 AND SER.TIPOSERVICIO IN (2, 3) AND (SOL.MANEXTPRO = 0 OR ISNULL(ING.TRATAESPECIA, 0) = 3) AND SOL.NUMINGRES = @AdmissionNumber AND SER.ORDSERVICIOID IS NULL
							AND (isnull(bjc.SkipClearance,0)=0)) BEGIN
					--SET @Message = 'Existen registros de hemocomponentes pendientes por generar'
					--SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
					SET @ResultStatus = CONVERT(BIT, 0)
					SET @Message = ''
					SET @ResultMessage = '-001'
					RETURN
				END

				--INTERCONSULTAS
				IF EXISTS (SELECT 1 
							FROM HCORDINTE A
							LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON A.AUTO =acj.EntityId and acj.EntityName='HCORDINTE' and acj.EntityTap='INDlcgInterSearch'
							LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
							WHERE NUMFOLINT IS NOT NULL AND NUMINGRES = @AdmissionNumber AND GENSERVICEORDER IS NULL AND (isnull(bjc.SkipClearance,0)=0)) BEGIN
					--SET @Message = 'Existen registros de interconsultas pendientes por generar'
					--SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
					SET @ResultStatus = CONVERT(BIT, 0)
					SET @Message = ''
					SET @ResultMessage = '-001'
					RETURN
				END

				--NO QX
				IF EXISTS (SELECT 1 
					FROM dbo.HCORDPRON H
					JOIN dbo.INCUPSIPS B ON H.CODSERIPS = B.CODSERIPS
					JOIN dbo.ADINGRESO I ON I.NUMINGRES = H.NUMINGRES
					LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON H.AUTO =acj.EntityId and acj.EntityName='HCORDPRON' and acj.EntityTap='INDlcgProceduresNoQX'
					LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
					WHERE (H.MANEXTPRO = 0 OR ISNULL(I.TRATAESPECIA, 0) = 3) 
					AND H.ESTSERIPS NOT IN ('1', '5') AND B.OXIGENSERVICE = 0
					AND H.NUMINGRES = @AdmissionNumber 
					AND H.GENSERVICEORDER IS NULL AND (isnull(bjc.SkipClearance,0)=0)) BEGIN
					--SET @Message = 'Existen registros de procedimientos no QX pendientes por generar'
					--SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
					SET @ResultStatus = CONVERT(BIT, 0)
					SET @Message = ''
					SET @ResultMessage = '-001'
					RETURN
				END

				--QX
				IF EXISTS (
							SELECT 1
							FROM ViewSurgeriesPerformed v
							WHERE v.NUMINGRES =@AdmissionNumber AND v.GENSERVICEORDER IS NULL AND v.SkipLiquidation =0
				) BEGIN
					--SET @Message = 'Existen registros de procedimientos QX pendientes por generar'
					--SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
					SET @ResultStatus = CONVERT(BIT, 0)
					SET @Message = ''
					SET @ResultMessage = '-001'
					RETURN
				END

				--IMAGES DX
				IF EXISTS (SELECT 1
					FROM HCORDIMAG H 
					JOIN ADINGRESO I ON H.NUMINGRES = I.NUMINGRES 
					JOIN dbo.INCUPSIPS B ON H.CODSERIPS = B.CODSERIPS 
					LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON H.AUTO =acj.EntityId and acj.EntityName='HCORDIMAG' and acj.EntityTap='INDlcgImagesDX'
					LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
					WHERE (H.MANEXTPRO = 0  OR ISNULL(I.TRATAESPECIA, 0) = 3) AND (H.EXMREASIT = 1 OR H.ESTSERIPS IN ('2', '3', '4')) AND H.NUMINGRES = @AdmissionNumber AND H.GENSERVICEORDER  IS NULL
							AND (isnull(bjc.SkipClearance,0)=0)) BEGIN
					--SET @Message = 'Existen registros de imágenes Dx pendientes por generar'
					--SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
					SET @ResultStatus = CONVERT(BIT, 0)
					SET @Message = ''
					SET @ResultMessage = '-001'
					RETURN
				END

				--PATOLOGIAS
				IF EXISTS (SELECT 1 
					FROM HCORDPATO H 
					JOIN dbo.ADINGRESO I ON H.NUMINGRES = I.NUMINGRES
					LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON H.AUTO =acj.EntityId and acj.EntityName='HCORDPATO' and acj.EntityTap='INDlcgPathology'
					LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
					WHERE (H.MANEXTPRO = 0 OR ISNULL(I.TRATAESPECIA, 0) = 3) AND H.ESTSERIPS IN ('2','3','4') AND H.NUMINGRES = @AdmissionNumber AND H.GENSERVICEORDER IS NULL
							AND (isnull(bjc.SkipClearance,0)=0)) BEGIN
					--SET @Message = 'Existen registros de patologías pendientes por generar'
					--SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
					SET @ResultStatus = CONVERT(BIT, 0)
					SET @Message = ''
					SET @ResultMessage = '-001'
					RETURN
				END

				--LABORATORIOS
				IF EXISTS (SELECT 1 
					FROM HCORDLABO H 
					JOIN ADINGRESO I ON H.NUMINGRES = I.NUMINGRES
					LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) on H.AUTO=acj.EntityId and acj.EntityName='HCORDLABO' and acj.EntityTap='INDlcgLaboratories'
					LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
					WHERE (H.MANEXTPRO = 0 OR ISNULL(I.TRATAESPECIA, 0) = 3) AND H.ESTSERIPS IN ('3', '4', '2') AND H.NUMINGRES = @AdmissionNumber AND H.GENSERVICEORDER IS NULL AND (isnull(bjc.SkipClearance,0)=0)) BEGIN
					--SET @Message = 'Existen registros de laboratorios pendientes por generar'
					--SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
					SET @ResultStatus = CONVERT(BIT, 0)
					SET @Message = ''
					SET @ResultMessage = '-001'
					RETURN
				END

				--MEDICAMENTOS
				IF @AdmissionType = 2 AND EXISTS (SELECT 1 FROM dbo.VMedicinesSupplies WHERE Fisico <> 0 AND Ingreso = @AdmissionNumber) BEGIN
					--SET @Message = 'Existen registros de Medicamentos e insumos con cantidades pendientes'
					--SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
					SET @ResultStatus = CONVERT(BIT, 0)
					SET @Message = ''
					SET @ResultMessage = '-001'
					RETURN
				END				
			END
		END

		/************************************************* RESULTADO *************************************************/
		IF ISNULL(@ResultMessage, '') <> ''
		BEGIN
			SET @ResultStatus = CONVERT(BIT, 0)
			RETURN
		END

		SELECT	@ResultStatus = CONVERT(BIT, 1),
				@ResultMessage = ''
	END TRY
	BEGIN CATCH
		SELECT	@ResultStatus = CONVERT(BIT, 0),
				@ResultMessage = CONCAT('Error Validando la Factura: ', ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE())
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de validaciones previas a la liquidación de folios de facturación, que verifica que todos los parámetros obligatorios estén presentes y sean válidos antes de generar facturas, cuentas por cobrar u otros documentos contables. Valida la configuración de facturación de la unidad operativa (incluyendo la entidad administradora para particulares y el control de cuentas), la correspondencia entre el paciente y su número de ingreso, la vigencia y estado de la autorización de facturación DIAN (resolución, prefijo, rango de numeración y fechas), y la consistencia de los cruces de anticipos y descuentos al paciente incluidos en la lista de detalles del control de ingresos (folios). Si alguna validación falla, acumula los mensajes de error en el parámetro de salida @ResultMessage, permitiendo que el módulo de facturación informe al usuario exactamente qué condiciones impiden proceder con la liquidación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_LiquidateFolioValidations_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_LiquidateFolioValidations_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Ejecuta un conjunto de validaciones previas a la liquidación/facturación de folios de un ingreso (sin persistir aún la factura ni la cuenta por cobrar), acumulando mensajes de error en una salida única.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidateFolioValidations_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@OperativeUnitId no debe ser nulo o cero; @PatientCode no debe estar vacío; @ThirdPartyPatientId no debe ser cero; @AdmissionNumber debe existir en dbo.ADINGRESO y coincidir con @PatientCode (IPCODPACI=NUMINGRES); @BillingAuthorizationId debe estar enviado y referenciar una autorización con Status=1 y vigente entre InitialDate y FinalDate; @UserCode debe estar enviado; El XML @RevenueControlDetailCrossingListXml debe contener al menos un folio para liquidar; Debe existir registro en Billing.SettingsBilling para la unidad operativa con ParticularHealthAdministratorId configurado; Deben existir secuencias automáticas de Cuentas Por Cobrar (IdForm=682) y de Pagaré (IdForm=1510) en Portfolio.PortfolioSequence con alcance global o para la unidad operativa', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidateFolioValidations_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] @ResultMessage / @ResultStatus: Si @ResultMessage queda con texto, se retorna @ResultStatus=0 con la concatenación de errores; si todo pasa, @ResultStatus=1 y @ResultMessage=''''; [RETURN_RESULT] @ResultMessage: Si el usuario no posee permiso (IdForm=756, Action=138, ActionValue=1) y @SkipAccountControlValidations=1, retorna ''El usuario no posee permisos para evadir las validaciones de control de cuentas''; [RETURN_RESULT] @ResultMessage: Cuando hay registros pendientes en cualquiera de los controles de cuenta (valoraciones, enfermería, oxígeno, terapias, hemocomponentes, interconsultas, NoQX, QX, imágenes Dx, patologías, laboratorios, medicamentos/insumos o estancias) retorna @ResultMessage=''-001''; [RETURN_RESULT] Billing.SP_CloseAdmissionValidations_Output: Si @LiquidateMasterAccount=1, o no existen otros folios pendientes y no se liquida cuenta madre, ejecuta validaciones de cierre de admisión y agrega su mensaje a @ResultMessage si @ResultStatus=0; [RAISERROR] @ResultMessage: En BEGIN CATCH, retorna @ResultStatus=0 y @ResultMessage=''Error Validando la Factura: ''+ERROR_MESSAGE()+'' - Linea: ''+ERROR_LINE()', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidateFolioValidations_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @LiquidateMasterAccount = 0 (parámetro LiquidateMasterAccount de SettingsBilling está apagado) → Se prohíben descuentos globales (TotalPatientDiscount>0 o TotalPatientSalesPrice<>TotalPatientWithDiscount) y se exige cuenta contable de copago parametrizada en BillingConcept.CopayMainAccountId/RecoveryFixedAmountMainAccountId según LiquidationType else Si @LiquidateMasterAccount=1, se exige cuenta de descuento parametrizada por unidad funcional/concepto (BillingConceptAccount.DiscountAccountId, BillingConcept.DiscountAccountId o ProductGroupFunctionalUnit.DiscountAccountId) cuando GrandTotalDiscount>0, y se ejecuta SP_CloseAdmissionValidations_Output siempre; si GeneralLedger.CompanySettings.SalePriceIncludeTax = 1 → Valida balance de ítems comparando InvoicedQuantity*TotalSalesPrice con sodd.GrandTotalSalesPrice (redondeo a 2 y -2) else Valida balance considerando IVA: GrossValue*Cantidad + IVA% menos descuento de tercero, comparado contra GrandTotalSalesPrice; si rcd.FolioType <> 3 AND total a cruzar (@TotalCrossingValue) < rcd.TotalPatientWithDiscount → Si SettingsBilling.GeneratePromissoryNote=0 reporta ''No existe un anticipo de copago o cuota moderadora...''; además exige permiso (IdForm=756, Action=59) al @UserCode para hacer pagaré; si rcd.TaxDevolutionValue > 0 y los anticipos cruzados tienen métodos de pago distintos (>1 PaymentMethodTypes) → Reporta ''Se esta aplicando devolución de IVA, pero los anticipos tienen metodos de pago diferentes.''; si @AccountControlValidation=1 y (@AdmissionType=2 (hospitalario) o tipo 1 con UFUTIPUNI en (1,12,13,18,19,22,23,33,34,35)) y @SkipAccountControlValidations=0 → Ejecuta el bloque completo de validaciones de control de cuentas (valoraciones, enfermería, oxígeno, terapias, hemocomponentes, interconsultas, NoQX, QX, imágenes Dx, patologías, laboratorios, estancias y medicamentos), retornando ''-001'' si hay pendientes else Omite las validaciones de control de cuentas; si cg.CareGroupType=1 y ContractDetail.TerminationControl IN (3,4) y ContractValue < ExecuteValue + SUM(TotalFolio - TotalPatientSalesPrice) → Reporta ''Se supero el valor de los siguientes contratos''; si cg.CareGroupType=3 (particular) y cas.AccountParticularId IS NULL → Reporta que los grupos de atención particulares no tienen cuenta contable a particulares parametrizada', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidateFolioValidations_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidateFolioValidations_Output';
-- GO
