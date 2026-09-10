
-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 09/12/2015
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar o anular el cruce de anricipo vs cxc
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_SaveTransfer] @PortfolioTransferXml AS XML, 
                                              @CodeUser AS             VARCHAR(20)
AS
    BEGIN
        SET NOCOUNT ON;
        DECLARE @CodeMessage INT, @Message VARCHAR(MAX), @IdTransfer INT, @CodeTransfer VARCHAR(20);
        EXEC [Portfolio].[SP_SaveTransfer_Output] 
             @PortfolioTransferXml, 
             @CodeUser, 
             @CodeMessage OUTPUT, 
             @Message OUTPUT, 
             @IdTransfer OUTPUT, 
             @CodeTransfer OUTPUT;
        SELECT @CodeMessage AS CodeMessage, 
               @Message AS Message, 
               @IdTransfer AS Id, 
               @CodeTransfer AS CodeTransfer;
        RETURN;

        ----Se declaran las variables para obtener la cabecera
        --declare @Id int, @Code varchar(20), @DocumentDate datetime, @CustomerId int, @ThirdPartyId int, @PortfolioAdvanceId int, @TransferType tinyint, @MainAccountId int, @CostCenterId int, @Observations varchar(300), @OperatingUnitId int, @Status tinyint
        ----Tabla temporal para obtener los detalles de portfolioTransfer
        --declare @PortfolioTransferDetail table(Id int, PortfolioTrasferId int, AccountReceivableId int, MainAccountId int, CostCenterId int, Value decimal(18,0), ChangeTracker int)
        ----Tabla temporal para obtener los detalles de portfolioTransferOtherConcept
        --declare @PortfolioTransferOtherConcept table(Id int, PortfolioTrasferId int, PortfolioNoteConceptId int, MainAccountId int, CostCenterId int, Nature tinyint, Value decimal(18,0), ChangeTracker int, ThirdPartyId int)
        ----Se declara la variable que retorna la validacion del traslado
        --declare @resultValidateTransfer table (CodeMessage varchar(20),MessageResult varchar(max),Id int)
        ----begin transaction
        --Begin try
        --	--Se obtiene la cabecera del xml(PortfolioTransfer)
        --	select 
        --	@Id = t.x.value('Id[1]','int'),
        --	@Code = t.x.value('Code[1]','varchar(20)'),
        --	@DocumentDate = convert(datetime, t.x.value('DocumentDate[1]','varchar(20)'), 103),
        --	@CustomerId = t.x.value('CustomerId[1]','int'),
        --	@ThirdPartyId = t.x.value('ThirdPartyId[1]','int'),
        --	@PortfolioAdvanceId = t.x.value('PortfolioAdvanceId[1]','int'),
        --	@TransferType = t.x.value('TransferType[1]','tinyint'),
        --	@MainAccountId = t.x.value('MainAccountId[1]','int'),
        --	@CostCenterId = case when t.x.value('CostCenterId[1]','int') = 0 then null else t.x.value('CostCenterId[1]','int') end,
        --	@Observations = t.x.value('Observations[1]','varchar(300)'),
        --	@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
        --	@Status = t.x.value('Status[1]','tinyint')
        --	from @PortfolioTransferXml.nodes('/PortfolioTransfer') t(x)
        --	--Se obtiene los detalles del xml(PortfolioTransferDetail)
        --	insert into @PortfolioTransferDetail
        --	select 
        --	t.x.value('Id[1]','int') as Id,
        --	t.x.value('PortfolioTrasferId[1]','int') as PortfolioTrasferId,
        --	t.x.value('AccountReceivableId[1]','int') as AccountReceivableId,
        --	t.x.value('MainAccountId[1]','int') as MainAccountId,
        --	case when t.x.value('CostCenterId[1]','int') = 0 then null else t.x.value('CostCenterId[1]','int') end as CostCenterId,
        --	t.x.value('Value[1]','decimal(18, 0)') as Value,
        --	t.x.value('ChangeTracker[1]','int') as ChangeTracker
        --	from @PortfolioTransferXml.nodes('/PortfolioTransfer/PortfolioTransferDetail') t(x)
        --	--Se obtiene los detalles del xml(PortfolioTrabsferOtherConcept)
        --	insert into @PortfolioTransferOtherConcept
        --	select 
        --	t.x.value('Id[1]','int') as Id,
        --	t.x.value('PortfolioTransferId[1]','int') as PortfolioTransferId,
        --	t.x.value('PortfolioNoteConceptId[1]','int') as PortfolioNoteConceptId,
        --	t.x.value('MainAccountId[1]','int') as MainAccountId,
        --	case when t.x.value('CostCenterId[1]','int') = 0 then null else t.x.value('CostCenterId[1]','int') end as CostCenterId,
        --	t.x.value('Nature[1]','tinyint') as Nature,
        --	t.x.value('Value[1]','decimal(18, 0)') as Value,
        --	t.x.value('ChangeTracker[1]','int') as ChangeTracker,
        --	case when t.x.value('ThirdPartyId[1]','int') = 0 then null else t.x.value('ThirdPartyId[1]','int') end as ThirdPartyId
        --	from @PortfolioTransferXml.nodes('/PortfolioTransfer/PortfolioTransferOtherConcept') t(x)
        --	--Se crea el consecutivo siempre y cuando el código este vacío
        --	if @Code = '' And @Id = 0
        --	Begin
        --		-- Consultamos la secuencia numerica del form
        --		declare @idSequenceDetail int
        --		declare @pattern varchar(300)
        --		declare @NextS int
        --		select @pattern = cs.Pattern, @NextS = bsd.[Next] , @idSequenceDetail = bsd.Id  from Portfolio.PortfolioSequenceDetail bsd inner join Portfolio.PortfolioSequence bs on bs.Id = bsd.IdSequensePortfolioC inner join Common.Sequense cs on cs.Id = bsd.IdSequense
        --		where bs.IdForm = '687'
        --		if (@idSequenceDetail is null)
        --		Begin
        --		 select 999 as CodeMessage, 'Secuencia no encontrada'  as Message, 0 Id, '' as CodeTransfer
        --		 return
        --		End
        --		select @Code = dbo.GetSequence('',@pattern,@NextS)
        --		if @Code = '__ERROR_MAXVALUE__' begin
        --			select 999 as CodeMessage, 'La secuencia alcanzo su valor maximo'  as Message, 0 Id, '' as CodeTransfer
        --			return
        --		end
        --		update Portfolio.PortfolioSequenceDetail set [Next] += 1 where Id = @idSequenceDetail
        --	End
        --	--Se pregunta por el estado y dependiendo del estado se realizan las acciones
        --	if @Status = 1 --Estado Registrado
        --	Begin
        --		--Si el objeto viene sin ser creado(ChangeTracker = Added)
        --		if @Id = 0
        --		Begin
        --			--Se crea un registro en la tabla de control(PortfolioControl)
        --			INSERT INTO Portfolio.PortfolioControl(DocumentNumber,DocumentType,DocumentUser, DocumentDate)
        --			values (@Code,2,@CodeUser,@DocumentDate)
        --		End
        --		--Se valida el traslado
        --		insert @resultValidateTransfer exec [Portfolio].[SP_ValidateTransfers] @PortfolioTransferXml,0	
        --		if (select CodeMessage  from @resultValidateTransfer) = '999'
        --		Begin			
        --			declare @error varchar(max)
        --			select @error = MessageResult  from @resultValidateTransfer 
        --			select 999 as CodeMessage, @error  as Message, 0 Id, '' as CodeTransfer
        --			return 
        --		End  
        --	End
        --	Else If @Status = 3 --Estado anulado
        --	Begin
        --		--Se elimina el registro de la tabla de control(PortfolioControl)
        --		delete from Portfolio.PortfolioControl where DocumentNumber = @Code and DocumentType = 2
        --	End
        --	--Se empieza el registro del objeto
        --	if @Id = 0 --Si el registro es nuevo guardo
        --	Begin
        --		--Inserto la cabecera
        --		INSERT INTO [Portfolio].[PortfolioTransfer] ([Code],[DocumentDate],[CustomerId],[ThirdPartyId],[PortfolioAdvanceId],[TransferType],[MainAccountId],[CostCenterId],[Observations],[OperatingUnitId],[Status],[CreationUser],[CreationDate])
        --		values (@Code, @DocumentDate,@CustomerId,@ThirdPartyId,@PortfolioAdvanceId,@TransferType,@MainAccountId,@CostCenterId,@Observations,@OperatingUnitId,@Status,@CodeUser,[Common].[GETDATE]())
        --		--Obtengo el id de la cabcera
        --		set @Id = SCOPE_IDENTITY()
        --	End
        --	Else --Si se esta modificando
        --	Begin
        --		--Actualizo la cabecera
        --		declare @AnnulmentUser as varchar(20) = case when @Status <> 3 then null else @CodeUser end
        --		declare @AnnulmentDate as datetime = case when @Status <> 3 then null else [Common].[GETDATE]() end
        --		Update [Portfolio].[PortfolioTransfer] set Code = @Code, DocumentDate = @DocumentDate, CustomerId = @CustomerId, ThirdPartyId = @ThirdPartyId, PortfolioAdvanceId = @PortfolioAdvanceId,
        --		TransferType = @TransferType, MainAccountId = @MainAccountId, CostCenterId = @CostCenterId, Observations = @Observations, OperatingUnitId = @OperatingUnitId, [Status] = @Status,
        --		ModificationUser = @CodeUser, ModificationDate = [Common].[GETDATE](), AnnulmentUser = @AnnulmentUser, AnnulmentDate = @AnnulmentDate
        --		where Id = @Id
        --	End
        --	-------------------------------------------------------  PortfolioTransferDetail ------------------------------------------------------------------------------
        --	--Inserto los detalles(PortfolioTransferDetail) nuevos si hay
        --	if (select count(*) from @PortfolioTransferDetail where Id = 0) > 0 
        --	Begin
        --		INSERT INTO [Portfolio].[PortfolioTransferDetail] ([PortfolioTrasferId],[AccountReceivableId],[MainAccountId],[CostCenterId],[Value])
        --		select @Id, AccountReceivableId, MainAccountId, CostCenterId, Value from @PortfolioTransferDetail where Id = 0
        --	End
        --	--Actualizo los detalles(PortfolioTransferDetail) si hay
        --	if (select count(*) from @PortfolioTransferDetail where Id > 0) > 0 
        --	Begin
        --		UPDATE ptd set ptd.AccountReceivableId = ptdtemp.AccountReceivableId, ptd.MainAccountId = ptdtemp.MainAccountId, ptd.CostCenterId = ptdtemp.CostCenterId, ptd.Value = ptdtemp.Value
        --		from [Portfolio].[PortfolioTransferDetail] ptd 
        --		inner join @PortfolioTransferDetail ptdtemp on ptd.Id = ptdtemp.Id
        --	End
        --	--Elimino los detalles(PortfolioTransferDetail) si hay
        --	if (select count(*) from @PortfolioTransferDetail where Id > 0 and ChangeTracker = 1) > 0 
        --	Begin
        --		delete [Portfolio].[PortfolioTransferDetail] where Id in (select Id from @PortfolioTransferDetail where Id > 0 and ChangeTracker = 1)
        --	End
        --	-------------------------------------------------------- Fin PortfolioTransferDetail -------------------------------------------------------------------------------
        --	--------------------------------------------------------- PortfolioTransferOtherConcept -----------------------------------------------------------------------------
        --	--Inserto los otros conceptos(PortfolioTransferOtherConcept) nuevos si hay
        --	if (select count(*) from @PortfolioTransferOtherConcept where Id = 0) > 0 
        --	Begin
        --		INSERT INTO [Portfolio].[PortfolioTransferOtherConcept] ([PortfolioTransferId],[PortfolioNoteConceptId],[MainAccountId],[CostCenterId], [Nature],[Value], [ThirdPartyId])
        --		select @Id, PortfolioNoteConceptId, MainAccountId, CostCenterId, Nature, Value, ThirdPartyId from @PortfolioTransferOtherConcept where Id = 0
        --	End
        --	--Actualizo los otros conceptos(PortfolioTransferOtherConcept) si hay
        --	if (select count(*) from @PortfolioTransferOtherConcept where Id > 0) > 0 
        --	Begin
        --		UPDATE ptoc set ptoc.PortfolioNoteConceptId = ptoctemp.PortfolioNoteConceptId, ptoc.MainAccountId = ptoctemp.MainAccountId, ptoc.CostCenterId = ptoctemp.CostCenterId,
        --		 ptoc.Nature = ptoctemp.Nature, ptoc.Value = ptoctemp.Value, ptoc.ThirdPartyId = ptoctemp.ThirdPartyId
        --		from [Portfolio].[PortfolioTransferOtherConcept] ptoc 
        --		inner join @PortfolioTransferOtherConcept ptoctemp on ptoc.Id = ptoctemp.Id
        --	End
        --	--Elimino los detalles(PortfolioTransferOtherConcept) si hay
        --	if (select count(*) from @PortfolioTransferOtherConcept where Id > 0 and ChangeTracker = 1) > 0 
        --	Begin
        --		delete [Portfolio].[PortfolioTransferOtherConcept] where Id in (select Id from @PortfolioTransferOtherConcept where Id > 0 and ChangeTracker = 1)
        --	End
        --	-------------------------------------------------------- Fin PortfolioTransferOtherConcept -------------------------------------------------------------------------------
        --	--commit transaction
        --	select 0 as CodeMessage, 'Se guardó correctamente' as Message, @Id as Id, @Code as CodeTransfer
        --end try
        --begin catch
        --	--rollback transaction
        --	select 999 as CodeMessage, ERROR_MESSAGE() as Message, 0 Id, '' as CodeTransfer
        --end catch

    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que gestiona el cruce (traslado) entre anticipos y cuentas por cobrar (CxC) en el módulo de Cartera. Permite crear, actualizar, confirmar o anular un traslado de portafolio, procesando la información mediante un documento XML que contiene la cabecera y el detalle del traslado. Delega toda la lógica de negocio al procedimiento interno SP_SaveTransfer_Output, recibiendo como resultado el código de mensaje, descripción del resultado, identificador y código del traslado generado. Es el punto de entrada para registrar operaciones de cruce de anticipos contra facturas o cuentas por cobrar de clientes o terceros.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_SaveTransfer';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_SaveTransfer';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que invoca el procedimiento de persistencia del traslado de cartera y devuelve como resultset el código de mensaje, mensaje, Id y código del traslado generados.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe respetar el contrato esperado por SP_SaveTransfer_Output (cabecera de PortfolioTransfer y sus detalles).; Debe suministrarse el código del usuario que ejecuta la operación.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El procedimiento delega toda la lógica de persistencia en Portfolio.SP_SaveTransfer_Output y solo expone su resultado.; La salida siempre se entrega como result set con las columnas CodeMessage, Message, Id y CodeTransfer.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cruce de anticipo vs cuentas por cobrar; Traslado de cartera', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Tras ejecutar SP_SaveTransfer_Output, retorna un SELECT con CodeMessage, Message, Id y CodeTransfer obtenidos por parámetros OUTPUT.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Portfolio.SP_SaveTransfer_Output', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTransfer';
-- GO
