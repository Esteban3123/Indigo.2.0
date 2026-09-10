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
	declare @ContractDetail table(RowId int, Id int, ContractId int, ContractName varchar(100), ContractNumber varchar(30), Type tinyint, InitialDate datetime, EndDate datetime, BillingInitialDate datetime,
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
			t.x.value('ContractNumber[1]','varchar(30)') as ContractNumber,
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
		declare @Rows int = 1, @RowId int = 0, @DetailId int, @ContractId int, @ContractName varchar(100), @ContractNumber varchar(30), @Type tinyint, @InitialDate datetime, @EndDate datetime, 
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que crea o actualiza contratos suscritos con entidades pagadoras o administradoras de salud (EPS, aseguradoras, etc.), recibiendo toda la información en formato XML. Gestiona de forma integral la cabecera del contrato (código, valor pactado, valor ejecutado, objeto contractual y estado) junto con sus detalles de vigencia, facturación y legalización, así como las novedades y pólizas asociadas a cada detalle. Según el estado enviado, registra el usuario y la fecha de creación, modificación, suspensión o terminación del contrato, garantizando la trazabilidad de cada cambio. Utiliza la tabla Contract.Contract para la cabecera y Contract.ContractDetail para los detalles, apoyándose en Common.SP_GetSequence para la generación de consecutivos.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'PROCEDURE', @level1name = N'SP_SaveContract';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'PROCEDURE', @level1name = N'SP_SaveContract';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Inserta o actualiza un contrato (cabecera, detalles, novedades y pólizas) a partir de un XML, generando código secuencial cuando no se suministra y sincronizando la cabecera con el detalle vigente.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'PROCEDURE', @level1name=N'SP_SaveContract';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe seguir la estructura /Contract con sus nodos ContractDetail, ContractDetailNovelty y ContractDetailPolicy.; Debe existir una unidad operativa válida para invocar la generación de secuencia cuando el código viene vacío.; Los Id > 0 en detalles, novedades y pólizas deben corresponder a registros existentes para que las actualizaciones surtan efecto.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'PROCEDURE', @level1name=N'SP_SaveContract';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda inserción/actualización ocurre dentro de TRY/CATCH; cualquier error se traduce a un resultset con CodeMessage=999 y no propaga excepción.; El código del contrato nunca queda vacío: si no viene se genera vía secuencia.; La cabecera del contrato siempre refleja el ContractName, ContractNumber, InitialDate y EndDate del detalle marcado como ValidRecord = 1.; Las novedades y pólizas se asocian siempre al ContractDetailId del detalle del mismo RowId procesado en el ciclo.; Los detalles marcados con IsDelete = 1 nunca se procesan en el ciclo de upsert porque se eliminan previamente.; Los campos de auditoría de suspensión/finalización solo quedan poblados cuando el Status corresponde (2 o 3), en otro caso se ponen en NULL.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'PROCEDURE', @level1name=N'SP_SaveContract';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] Contract.ContractDetail: Cuando un detalle del XML viene con Id > 0 e IsDelete = 1, se elimina físicamente de Contract.ContractDetail.; [INSERT] Contract.Contract: Cuando @Id = 0 se crea el contrato con CreationUser/CreationDate y los datos de cabecera del XML.; [UPDATE] Contract.Contract: Cuando @Id <> 0 se actualiza la cabecera; si Status = 2 se setea SuspendedUser/SuspendedDate, si Status = 3 se setea FinishedUser/FinishedDate, en otros casos se dejan en NULL.; [INSERT] Contract.ContractDetail: Por cada fila de detalle con Id = 0 se inserta un nuevo ContractDetail asociado al ContractId recién creado/actualizado.; [UPDATE] Contract.ContractDetail: Por cada fila de detalle con Id > 0 (y no marcada para eliminar) se actualizan sus campos en Contract.ContractDetail.; [INSERT] Contract.ContractDetailNovelty: Por cada novedad del XML con Id = 0 y mismo RowId del detalle procesado, se inserta usando el ContractDetailId resultante (SCOPE_IDENTITY o el existente).; [UPDATE] Contract.ContractDetailNovelty: Por cada novedad del XML con Id > 0 y mismo RowId del detalle procesado, se actualizan sus campos en Contract.ContractDetailNovelty.; [INSERT] Contract.ContractDetailPolicy: Por cada póliza del XML con Id = 0 y mismo RowId del detalle procesado, se inserta vinculada al ContractDetailId resultante.; [UPDATE] Contract.ContractDetailPolicy: Por cada póliza del XML con Id > 0 y mismo RowId del detalle procesado, se actualizan sus campos en Contract.ContractDetailPolicy.; [UPDATE] Contract.Contract: Al final, la cabecera toma ContractName, ContractNumber, InitialDate y EndDate del detalle marcado con ValidRecord = 1.; [RETURN_RESULT] (resultset): Si la generación de secuencia retorna Code_Output <> 0, devuelve CodeMessage=999 con el mensaje de error parametrizado para ''Contrato'' y termina sin persistir.; [RETURN_RESULT] (resultset): Al finalizar correctamente devuelve CodeMessage=0, mensaje ''Se guardó correctamente'', el Id del contrato y su código.; [RETURN_RESULT] (resultset): En el CATCH devuelve CodeMessage=999 con ERROR_MESSAGE() y ContractId=0.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'PROCEDURE', @level1name=N'SP_SaveContract';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNULL(@Code,'''') = '''' (no se envió código de contrato) → Invoca Common.SP_GetSequence (tipo 300, concepto 978) para generar el código automáticamente. else Conserva el código suministrado en el XML.; si Code_Output devuelto por la secuencia es distinto de 0 → Retorna error 999 con el mensaje de la secuencia y aborta la operación.; si @Id = 0 → Inserta un nuevo contrato y obtiene su Id por SCOPE_IDENTITY. else Actualiza el contrato existente con datos de auditoría y, según Status, marca como suspendido (2) o finalizado (3).; si Detalle con Id > 0 e IsDelete = 1 → Se elimina el detalle de la tabla y de la variable de trabajo antes del ciclo de upsert.; si Detalle con Id = 0 dentro del ciclo → Inserta nuevo ContractDetail. else Actualiza el ContractDetail existente.; si Status del contrato = 2 → Asigna SuspendedUser y SuspendedDate.; si Status del contrato = 3 → Asigna FinishedUser y FinishedDate.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'PROCEDURE', @level1name=N'SP_SaveContract';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.SP_GetSequence', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'PROCEDURE', @level1name=N'SP_SaveContract';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.Contract; Contract.ContractDetail; Contract.ContractDetailNovelty; Contract.ContractDetailPolicy', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'PROCEDURE', @level1name=N'SP_SaveContract';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'PROCEDURE', @level1name=N'SP_SaveContract';
-- GO
