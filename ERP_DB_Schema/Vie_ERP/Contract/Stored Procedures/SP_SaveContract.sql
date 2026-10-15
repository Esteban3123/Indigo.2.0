-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 11/02/2020
-- Description:	Procedimiento que se encarga de guardar y actualizar los contratos
-- =============================================
CREATE PROCEDURE [Contract].[SP_SaveContract] 
	@Xml as xml,
	@CodeUser as varchar(20)
AS
BEGIN

	--Variables de la cabecera del contrato
	declare @Id int, 
			@ContractEntityId int, 
			@HealthAdministratorId int, 
			@Code varchar(20), 
			@ContractValue numeric(18,0), 
			@ExecuteValue numeric(18,0), 
			@ContractObject varchar(max), 
			@Status tinyint, 
			@OperatingUnitId int,
			-------------------------------------------------------------------
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)

	--Detalles del contrato
	declare @ContractDetail table(RowId int, Id int, ContractId int, ContractName varchar(100), ContractNumber varchar(100), Type tinyint, InitialDate datetime, EndDate datetime, BillingInitialDate datetime,
	BillingEndDate datetime, Legalized bit, DateLegalization datetime, RadicatedBillingDate datetime, Observations varchar(max),PermanentObservationOfTheInvoice varchar(max), PrintingMode tinyint, TerminationControl tinyint, 
	NotificationValueType tinyint, PercentageNotification numeric(5,2), NotificationValue numeric(18,0), NotificationTimeType tinyint, NotificationDays int, 
	PercentageApplyPaymentSoon numeric(5,2), AgesPortfolioId int, ValidRecord bit, IsDelete bit)

	--Detalle de novedades del contrato
	declare @ContractDetailNovelty table(RowId int, Id int, ContractDetailId int, NoveltyDate datetime, NoveltySource tinyint, Name varchar(200), Description varchar(500), Status tinyint)

	--Detalle de polizas del contrato
	declare @ContractDetailPolicy table(RowId int, Id int, ContractDetailId int, FixedAssetPolicyId int, FixedAssetInsuranceId int, PolicyNumber varchar(50), EmissionDate datetime, AmountInsured numeric(18,2), 
	CoveragePercentage numeric(5,2), Observation varchar(500), Status bit)

	Begin try
	
		--Se obtiene la cabecera del xml
		select 
			@Id = t.x.value('Id[1]','int'),
			@ContractEntityId = IIF(t.x.value('ContractEntityId[1]','varchar(20)') = '', null, t.x.value('ContractEntityId[1]','varchar(20)')),
			@HealthAdministratorId = t.x.value('HealthAdministratorId[1]','int'),
			@Code = t.x.value('Code[1]','varchar(20)'),
			@ContractValue = REPLACE(t.x.value('ContractValue[1]','varchar(20)'), ',', '.'),
			@ExecuteValue = REPLACE(t.x.value('ExecuteValue[1]','varchar(20)'), ',', '.'),
			@ContractObject = t.x.value('ContractObject[1]','varchar(max)'),
			@Status = t.x.value('Status[1]','tinyint'),
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','int')
		from @Xml.nodes('/Contract') t(x)
		
		--Se obtiene los detalles del xml
		insert into @ContractDetail
		select 
			t.x.value('RowId[1]','int') as RowId,
			t.x.value('Id[1]','int') as Id,
			t.x.value('ContractId[1]','int') as ContractId,
			t.x.value('ContractName[1]','varchar(100)') as ContractName,
			t.x.value('ContractNumber[1]','varchar(100)') as ContractNumber,
			t.x.value('Type[1]','tinyint') as Type,
			t.x.value('InitialDate[1]','datetime') as InitialDate,
			t.x.value('EndDate[1]','datetime') as EndDate,
			t.x.value('BillingInitialDate[1]','datetime') as BillingInitialDate,
			t.x.value('BillingEndDate[1]','datetime') as BillingEndDate,
			t.x.value('Legalized[1]','bit') as Legalized,
			IIF(t.x.value('DateLegalization[1]','varchar(20)') = '', null, t.x.value('DateLegalization[1]','varchar(20)')) as DateLegalization, 
			t.x.value('RadicatedBillingDate[1]','datetime') as RadicatedBillingDate,
			IIF(t.x.value('Observations[1]','varchar(max)') = '', null, t.x.value('Observations[1]','varchar(max)')) as Observations,
			IIF(t.x.value('PermanentObservationOfTheInvoice[1]','varchar(max)') = '', null, t.x.value('PermanentObservationOfTheInvoice[1]','varchar(max)')) as Observations,
			t.x.value('PrintingMode[1]','tinyint') as PrintingMode,
			t.x.value('TerminationControl[1]','tinyint') as TerminationControl,
			IIF(t.x.value('NotificationValueType[1]','varchar(20)') = '', null, t.x.value('NotificationValueType[1]','varchar(20)')) as NotificationValueType, 
			IIF(t.x.value('PercentageNotification[1]','varchar(20)') = '', null, REPLACE(t.x.value('PercentageNotification[1]','varchar(20)'), ',', '.')) as PercentageNotification, 
			IIF(t.x.value('NotificationValue[1]','varchar(20)') = '', null, REPLACE(t.x.value('NotificationValue[1]','varchar(20)'), ',', '.')) as NotificationValue, 
			IIF(t.x.value('NotificationTimeType[1]','varchar(20)') = '', null, t.x.value('NotificationTimeType[1]','varchar(20)')) as NotificationTimeType,
			IIF(t.x.value('NotificationDays[1]','varchar(20)') = '', null, t.x.value('NotificationDays[1]','varchar(20)')) as NotificationDays,
			REPLACE(t.x.value('PercentageApplyPaymentSoon[1]','varchar(20)'), ',', '.') as PercentageApplyPaymentSoon,
			IIF(t.x.value('AgesPortfolioId[1]','varchar(20)') = '', null, t.x.value('AgesPortfolioId[1]','varchar(20)')) as AgesPortfolioId, 
			t.x.value('ValidRecord[1]','bit') as ValidRecord,
			t.x.value('IsDelete[1]','bit') as IsDelete
		from @Xml.nodes('/Contract/ContractDetail') t(x)
		
		--Se obtiene las novedades
		insert into @ContractDetailNovelty
		select 
			t.x.value('RowId[1]','int') as RowId,
			t.x.value('Id[1]','int') as Id,
			t.x.value('ContractDetailId[1]','int') as ContractDetailId,
			t.x.value('NoveltyDate[1]','datetime') as NoveltyDate,
			t.x.value('NoveltySource[1]','tinyint') as NoveltySource,
			t.x.value('Name[1]','varchar(200)') as Name,
			IIF(t.x.value('Description[1]','varchar(500)') = '', null, t.x.value('Description[1]','varchar(500)')) as Description, 
			t.x.value('Status[1]','tinyint') as Status
		from @Xml.nodes('/Contract/ContractDetail/ContractDetailNovelty') t(x)

		--Se obtiene las polizas
		insert into @ContractDetailPolicy
		select 
			t.x.value('RowId[1]','int') as RowId,
			t.x.value('Id[1]','int') as Id,
			t.x.value('ContractDetailId[1]','int') as ContractDetailId,
			t.x.value('FixedAssetPolicyId[1]','int') as FixedAssetPolicyId,
			t.x.value('FixedAssetInsuranceId[1]','int') as FixedAssetInsuranceId,
			t.x.value('PolicyNumber[1]','varchar(50)') as PolicyNumber,
			t.x.value('EmissionDate[1]','datetime') as EmissionDate,
			REPLACE(t.x.value('AmountInsured[1]','varchar(20)'), ',', '.') as AmountInsured,
			REPLACE(t.x.value('CoveragePercentage[1]','varchar(20)'), ',', '.') as CoveragePercentage,
			IIF(t.x.value('Observation[1]','varchar(500)') = '', null, t.x.value('Observation[1]','varchar(500)')) as Observation, 
			t.x.value('Status[1]','bit') as Status
		from @Xml.nodes('/Contract/ContractDetail/ContractDetailPolicy') t(x)
			
		--Se eliminan los detalles
		delete from Contract.ContractDetail where Id in (select Id from @ContractDetail where Id > 0 and IsDelete = 1)
		delete from @ContractDetail where Id > 0 and IsDelete = 1

		--Si no viene el código se genera
		if ISNULL(@Code, '') = ''
		begin
			DECLARE @IsManual BIT
				
			EXEC Common.SP_GetSequence 300, 978, @OperatingUnitId, NULL, NULL, @IsManual OUT, @Code OUT, @Code_Output OUT, @Message_Output OUT

			IF @Code_Output <> 0
			BEGIN
				SELECT	999 CodeMessage, REPLACE(@Message_Output, '{0}', 'Contrato') Message, 0 ContractId, '' ContractCode
				RETURN
			END
		end
		
		--Si se va a guardar
		if @Id = 0
		begin
			insert into [Contract].[Contract]([ContractEntityId], [HealthAdministratorId], [Code], [ContractValue], [ExecuteValue], [ContractObject], [Status], [CreationUser], [CreationDate])
			values(@ContractEntityId, @HealthAdministratorId, @Code, @ContractValue, @ExecuteValue, @ContractObject, @Status, @CodeUser, [Common].[GETDATE]())

			set @Id = SCOPE_IDENTITY()
		end
		else begin --Si se va actualizar		
			update Contract.Contract 
				set ContractEntityId = @ContractEntityId, HealthAdministratorId = @HealthAdministratorId, Code = @Code, ContractValue = @ContractValue, 
					ExecuteValue = @ExecuteValue, ContractObject = @ContractObject, Status = @Status, ModificationUser = @CodeUser, ModificationDate = [Common].[GETDATE](),
					SuspendedUser = IIF(@Status = 2, @CodeUser, null), SuspendedDate = IIF(@Status = 2, [Common].[GETDATE](), null),
					FinishedUser = IIF(@Status = 3, @CodeUser, null), FinishedDate = IIF(@Status = 3, [Common].[GETDATE](), null)
			where Id = @Id
		end
			
		--Variables para recorrer
		declare @Rows int = 1, @RowId int = 0, @DetailId int, @ContractId int, @ContractName varchar(100), @ContractNumber varchar(100), @Type tinyint, @InitialDate datetime, @EndDate datetime, 
		@BillingInitialDate datetime, @BillingEndDate datetime, @Legalized bit, @DateLegalization datetime, @RadicatedBillingDate datetime, @Observations varchar(max), @PermanentObservationOfTheInvoice varchar(max), @PrintingMode tinyint,
		@TerminationControl tinyint, @NotificationValueType tinyint, @PercentageNotification numeric(5,2), @NotificationValue numeric(18,0), @NotificationTimeType tinyint, 
		@NotificationDays int, @PercentageApplyPaymentSoon numeric(5,2), @AgesPortfolioId int, @ValidRecord bit

		while @Rows > 0
		begin
			select top 1 
				@RowId = RowId, @DetailId = Id, @ContractId = ContractId, @ContractName = ContractName, @ContractNumber = ContractNumber, @Type = Type, 
				@InitialDate = InitialDate, @EndDate = EndDate, @BillingInitialDate = BillingInitialDate, @BillingEndDate = BillingEndDate, @Legalized = Legalized, 
				@DateLegalization = DateLegalization, @RadicatedBillingDate = RadicatedBillingDate, @Observations = Observations, @PermanentObservationOfTheInvoice = PermanentObservationOfTheInvoice, 
				@PrintingMode = PrintingMode, @TerminationControl = TerminationControl, @NotificationValueType = NotificationValueType, @PercentageNotification = PercentageNotification, 
				@NotificationValue = NotificationValue, @NotificationTimeType = NotificationTimeType, @NotificationDays = NotificationDays, 
				@PercentageApplyPaymentSoon = PercentageApplyPaymentSoon, @AgesPortfolioId = AgesPortfolioId, @ValidRecord = ValidRecord
			from @ContractDetail
			where RowId > @RowId 
			order by RowId

			set @Rows = @@RowCount
			if @Rows = 0 
				break

			if @DetailId = 0 --Se inserta el detalle
			begin
				insert into [Contract].[ContractDetail]([ContractId], [ContractName], [ContractNumber], [Type], [InitialDate], [EndDate], [BillingInitialDate], [BillingEndDate], 
				[Legalized], [DateLegalization], [RadicatedBillingDate], [Observations], [PermanentObservationOfTheInvoice], [PrintingMode], [TerminationControl], [NotificationValueType], [PercentageNotification], 
				[NotificationValue], [NotificationTimeType], [NotificationDays], [PercentageApplyPaymentSoon], [AgesPortfolioId], [ValidRecord])
				values(@Id, @ContractName, @ContractNumber, @Type, @InitialDate, @EndDate, @BillingInitialDate, @BillingEndDate, @Legalized, @DateLegalization, 
				@RadicatedBillingDate, @Observations, @PermanentObservationOfTheInvoice, @PrintingMode, @TerminationControl, @NotificationValueType, @PercentageNotification, @NotificationValue, @NotificationTimeType,
				@NotificationDays, @PercentageApplyPaymentSoon, @AgesPortfolioId, @ValidRecord)

				set @DetailId = SCOPE_IDENTITY()
			end
			else begin --Se actualiza el detalle
				update [Contract].[ContractDetail] set ContractId = @ContractId, ContractName = @ContractName, ContractNumber = @ContractNumber, Type = @Type, InitialDate = @InitialDate, 
				EndDate = @EndDate, BillingInitialDate = @BillingInitialDate, BillingEndDate = @BillingEndDate, Legalized = @Legalized, DateLegalization = @DateLegalization, 
				RadicatedBillingDate = @RadicatedBillingDate, Observations = @Observations, PermanentObservationOfTheInvoice = @PermanentObservationOfTheInvoice, PrintingMode = @PrintingMode, TerminationControl = @TerminationControl, 
				NotificationValueType = @NotificationValueType, PercentageNotification = @PercentageNotification, NotificationValue = @NotificationValue, NotificationTimeType = @NotificationTimeType,
				NotificationDays = @NotificationDays, PercentageApplyPaymentSoon = @PercentageApplyPaymentSoon, AgesPortfolioId = @AgesPortfolioId, ValidRecord = @ValidRecord
				where Id = @DetailId
			end

			--Se insertan las novedades
			insert into [Contract].[ContractDetailNovelty]([ContractDetailId], [NoveltyDate], [NoveltySource], [Name], [Description], [Status])
			select @DetailId, NoveltyDate, NoveltySource, Name, Description, Status
			from @ContractDetailNovelty
			where Id = 0 and RowId = @RowId

			--Se actualizan las novedades
			update n set n.ContractDetailId = nTemp.ContractDetailId, n.NoveltyDate = nTemp.NoveltyDate, n.NoveltySource = nTemp.NoveltySource, n.Name = nTemp.Name, 
			n.Description = nTemp.Description, n.Status = nTemp.Status
			from @ContractDetailNovelty nTemp
			inner join Contract.ContractDetailNovelty n WITH (NOLOCK) on n.Id = nTemp.Id
			where nTemp.Id > 0 and nTemp.RowId = @RowId

			--Se insertan las polizas
			insert into [Contract].[ContractDetailPolicy]([ContractDetailId], [FixedAssetPolicyId], [FixedAssetInsuranceId], [PolicyNumber], [EmissionDate], [AmountInsured], 
			[CoveragePercentage], [Observation], [Status])
			select @DetailId, FixedAssetPolicyId, FixedAssetInsuranceId, PolicyNumber, EmissionDate, AmountInsured, CoveragePercentage, Observation, Status
			from @ContractDetailPolicy
			where Id = 0 and RowId = @RowId

			--Se actualizan las polizas
			update p set p.ContractDetailId = pTemp.ContractDetailId, p.FixedAssetPolicyId = pTemp.FixedAssetPolicyId, p.FixedAssetInsuranceId = pTemp.FixedAssetInsuranceId, 
			p.PolicyNumber = pTemp.PolicyNumber, p.EmissionDate = pTemp.EmissionDate, p.AmountInsured = pTemp.AmountInsured, p.CoveragePercentage = pTemp.CoveragePercentage, 
			p.Observation = pTemp.Observation, p.Status = pTemp.Status
			from @ContractDetailPolicy pTemp
			inner join Contract.ContractDetailPolicy p WITH (NOLOCK) on p.Id = pTemp.Id
			where pTemp.Id > 0 and pTemp.RowId = @RowId
		end

		--Actualizar la cabecera
		UPDATE c
			SET c.ContractName = cd.ContractName, 
				c.ContractNumber = cd.ContractNumber,
				c.InitialDate = cd.InitialDate, 
				c.EndDate = cd.EndDate
		FROM Contract.Contract c WITH (NOLOCK)
		JOIN Contract.ContractDetail cd WITH (NOLOCK) ON c.Id = cd.ContractId AND cd.ValidRecord = 1
		WHERE c.Id = @Id

		select 0 as CodeMessage, 'Se guardó correctamente' as Message, @Id ContractId, @Code ContractCode
		return
	end try
	begin catch
		select 999 as CodeMessage, ERROR_MESSAGE() as Message, 0 ContractId, '' ContractCode
		return
	end catch
END