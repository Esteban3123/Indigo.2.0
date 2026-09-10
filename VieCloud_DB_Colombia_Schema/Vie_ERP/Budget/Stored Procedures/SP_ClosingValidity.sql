-- =============================================
-- Author:		Cristhian Mauricio Salazar
-- Create date: 05/01/2017
-- Description:	Proceso de Cierre Presupuestal
-- =============================================
CREATE PROCEDURE [Budget].[SP_ClosingValidity]
	@OperatingUnitId int,
	@IdValidity int
AS
BEGIN

---- Variables para las Secuencias
declare @IdSequence int
declare @Scope varchar(5)
declare @idSequenceDetail int
declare @Prefix varchar(20) = ''
declare @pattern varchar(300)
declare @NextS bigint
---- Fin Variables para secuencias
declare @YearValidity int = (select [Year] from Budget.BudgetaryValidity where Id = @IdValidity)
declare @IdNextValidity int
declare @CxPCreateSingleCategory bit
declare @CxPSingleCategoryId bit
declare @IsESE bit = (
select ESE from  Budget.BudgetaryEntity e
inner join Budget.BudgetaryValidity v on v.BudgetaryEntityId = e.Id
where v.Id = @IdValidity)

----- Valido que la siguiente vigencia exista
if (select count(*) from Budget.BudgetaryValidity where [Year] = (@YearValidity + 1)) = 0 begin
	select '999' as CodeMessage, 'La vigencia ' + cast((@YearValidity + 1) as varchar(10)) + ' no existe, por favor creela' as [Message], cast(3 as tinyint) as [Status]
	return
end
set @IdNextValidity = (select Id from Budget.BudgetaryValidity where [Year] = (@YearValidity + 1))
set @CxPCreateSingleCategory = 0 ---(select CxPCreateSingleCategory from Budget.BudgetaryValidity where [Year] = (@YearValidity + 1))
if @CxPCreateSingleCategory = 1 begin
	set @CxPSingleCategoryId = null--(select CxPSingleCategoryId from Budget.BudgetaryValidity where [Year] = (@YearValidity + 1))
end

if (select count(*) from Budget.[Availability] where Status = 1 and BudgetaryValidityId = @IdValidity) > 0 begin
	declare @errorsUnConfirmed varchar(MAX)
	select @errorsUnConfirmed=stuff((select N';  ' + Code
	from Budget.[Availability] where Status = 1 and BudgetaryValidityId = @IdValidity
	for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
	select '999' as CodeMessage, 'Las siguientes disponibilidades se encuentran sin confirmar: ' + @errorsUnConfirmed as [Message], cast(3 as tinyint) as [Status]
	return
end
if (select count(*) from Budget.[Availability] a inner join Budget.[AvailabilityDetail] ad on a.Id = ad.AvailabilityId where a.Status = 2 and a.BudgetaryValidityId = @IdValidity and ad.Balance > 0) > 0 begin
	select '780' as CodeMessage, 'Existen disponibilidades con saldo' as [Message], cast(3 as tinyint) as [Status]
	return
end
if (select count(*) from Budget.Commitment where Status = 1 and BudgetaryValidityId = @IdValidity) > 0 begin
	declare @errorsUnConfirmedCommitment varchar(MAX)
	select @errorsUnConfirmedCommitment=stuff((select N';  ' + Code
	from Budget.Commitment where Status = 1 and BudgetaryValidityId = @IdValidity
	for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
	select '999' as CodeMessage, 'Los siguientes compromisos se encuentran sin confirmar: ' + @errorsUnConfirmedCommitment as [Message], cast(3 as tinyint) as [Status]
	return
end
--- Valido que no hayan ordenes de pago sin confirmar
if (select count(*) from Budget.PaymentOrder where Status = 1 and BudgetaryValidityId = @IdValidity) > 0 begin
	declare @errorsUnConfirmedPayment varchar(MAX)
	select @errorsUnConfirmedPayment=stuff((select N';  ' + Code
	from Budget.PaymentOrder where Status = 1 and BudgetaryValidityId = @IdValidity
	for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
	select '999' as CodeMessage, 'Las siguientes obligaciones se encuentran sin confirmar: ' + @errorsUnConfirmedPayment as [Message], cast(3 as tinyint) as [Status]
	return
end

----- Si es una ESE se debe validar que los compromisos esten todos con saldo en cero ya que las ESE no permiten hacer reservas
if @IsESE = 1 begin
	if (select count(*) from Budget.Commitment c inner join Budget.CommitmentDetail cd on c.Id = cd.CommitmentId where c.Status = 2 and c.BudgetaryValidityId = @IdValidity and cd.Balance > 0) > 0 begin
		select '781' as CodeMessage, 'Existen compromisos con saldo' as [Message], cast(3 as tinyint) as [Status]
		select distinct c.*,cd.*,ca.* from Budget.Commitment c inner join Budget.CommitmentDetail cd on c.Id = cd.CommitmentId inner join Budget.Category ca on ca.Id = cd.CategoryId where c.Status = 2 and c.BudgetaryValidityId = @IdValidity and cd.Balance > 0
		return
	end
end
else begin --- Si no es una ESE entonces trato de crear los compromisos como reservas
	--- Valido que los rubros de los compromisos que voy a crear esten creados en la nueva vigencia 
	if (select count(*) from Budget.Commitment c
	inner join Budget.CommitmentDetail cd on c.Id = cd.CommitmentId
	inner join Budget.Category ca on ca.Id = cd.CategoryId
	left join Budget.Category canew on canew.Code = ca.Code and canew.ItemType = ca.ItemType and canew.BudgetaryValidityId = @IdNextValidity
	where c.BudgetaryValidityId = @IdValidity and canew.Id is null and cd.Balance > 0 and c.Status = 2) > 0 begin
		declare @errorsCategory varchar(MAX)
		select @errorsCategory=stuff((select N';  ' + canew.Code
		from Budget.Commitment c
		inner join Budget.CommitmentDetail cd on c.Id = cd.CommitmentId
		inner join Budget.Category ca on ca.Id = cd.CategoryId
		left join Budget.Category canew on canew.Code = ca.Code and canew.ItemType = ca.ItemType and canew.BudgetaryValidityId = @IdNextValidity
		where c.BudgetaryValidityId = @IdValidity and canew.Id is null and cd.Balance > 0 and c.Status = 2
		for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
		select '999' as CodeMessage, 'Los siguientes rubros no se encuentran creados en la vigencia '+ cast((@YearValidity + 1) as varchar(10)) +': ' + @errorsCategory as [Message], cast(3 as tinyint) as [Status]
		return
	end
	--- Validamos que esten creadas las fuentes de financiacion 
	if (select count(*) from Budget.Commitment c
	inner join Budget.CommitmentDetail cd on c.Id = cd.CommitmentId
	inner join Budget.RevenueType rt on rt.Id = cd.RevenueTypeId
	left join Budget.RevenueType rtnew on rtnew.Code = rt.Code and rtnew.BudgetaryValidityId = @IdNextValidity
	where c.BudgetaryValidityId = @IdValidity and rtnew.Id is null and cd.Balance > 0 and c.Status = 2) > 0 begin
		declare @errorsRevenueType varchar(MAX)
		select @errorsRevenueType = stuff((select N';  ' + rtnew.Code
		from Budget.Commitment c
		inner join Budget.CommitmentDetail cd on c.Id = cd.CommitmentId
		inner join Budget.RevenueType rt on rt.Id = cd.RevenueTypeId
		left join Budget.RevenueType rtnew on rtnew.Code = rt.Code and rtnew.BudgetaryValidityId = @IdNextValidity
		where rtnew.Id is null and cd.Balance > 0 and c.Status = 2
		for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
		select '999' as CodeMessage, 'Las siguientes fuentes de financiacion no se encuentran creados en la vigencia '+ cast((@YearValidity + 1) as varchar(10)) +': ' + @errorsRevenueType as [Message], cast(3 as tinyint) as [Status]
		return
	end
	--- Validamos la secuencia numerica
	if(select count(*) from [Budget].[BudgetSequence] where IdForm = 231) = 0 begin
		select '999' as CodeMessage, 'No existe secuencia numerica para los Compromisos' as Message, cast(3 as tinyint) as [Status]
		return
	end
	declare @CodeCommitment varchar(20)

	select @IdSequence = Id, @Scope = Scope from [Budget].[BudgetSequence] where IdForm = 231
	if @Scope = 'O' begin --- Secuencia por Prefijo
		select top 1 @pattern = cs.Pattern, @idSequenceDetail = psd.Id  from [Budget].[BudgetSequenceDetail] psd inner join Common.Sequense cs on cs.Id = psd.IdSequense where psd.[IdSequenseBudgetC] = @IdSequence
	end
	else begin -- Secuencia por Unidad operativa
		select @pattern = cs.Pattern, @idSequenceDetail = psd.Id  from [Budget].[BudgetSequenceDetail] psd inner join Common.Sequense cs on cs.Id = psd.IdSequense where psd.[IdSequenseBudgetC] = @IdSequence and IdOperatingUnit = @OperatingUnitId
	end

	declare @IdCommitmentReservation int
	---Si todo esta bien entonces creo las reservas en la nueva vigencia
	declare @IdCommitment int
	DECLARE commitment_cursor CURSOR FOR   
	select distinct c.Id
	from Budget.Commitment c
	inner join Budget.CommitmentDetail cd on c.Id = cd.CommitmentId
	where c.BudgetaryValidityId = @IdValidity and c.Status = 2 and cd.Balance > 0 and c.CommitmentType <> 2
	
	OPEN commitment_cursor  
  
	FETCH NEXT FROM commitment_cursor   
	INTO @IdCommitment
  
	WHILE @@FETCH_STATUS = 0  
	BEGIN 
		
		update [Budget].[BudgetSequenceDetail] set @NextS = [Next] += 1 where Id = @idSequenceDetail
		set @CodeCommitment = dbo.GetSequence(@Prefix,@pattern,(@NextS - 1))

		INSERT INTO [Budget].[Commitment]
           ([Code]
           ,[BudgetaryValidityId]
           ,[ThirdPartyId]
           ,[DocumentSource]
           ,[Document]
           ,[DocumentDate]
           ,[CommitmentType]
           ,[Observations]
           ,[Status]
           ,[CreationUser]
           ,[CreationDate]
           ,[ConfirmationUser]
           ,[ConfirmationDate])
		select @CodeCommitment, @IdNextValidity, ThirdPartyId, 1, 'Reserva creada por el Compromiso ' + Code, DocumentDate, 2, Observations, 2, CreationUser, CreationDate, ConfirmationUser, ConfirmationDate
		from Budget.Commitment where Id = @IdCommitment

		set @IdCommitmentReservation = SCOPE_IDENTITY()

		INSERT INTO [Budget].[CommitmentDetail]
           ([CommitmentId]
           ,[AvailabilityDetailId]
           ,[CategoryId]
           ,[RevenueTypeId]
           ,[ExpiredDate]
           ,[InitialValue]
           ,[DebitModificationValue]
           ,[CreditModificationValue]
           ,[TotalCommitment]
           ,[ExecutedValue]
           ,[Balance])
		select @IdCommitmentReservation, null, canew.Id, rtnew.Id, cd.ExpiredDate, cd.Balance, 0, 0, cd.Balance, 0, cd.Balance
		from Budget.Commitment c
		inner join Budget.CommitmentDetail cd on c.Id = cd.CommitmentId
		inner join Budget.Category ca on ca.Id = cd.CategoryId
		inner join Budget.Category canew on canew.Code = ca.Code and canew.ItemType = ca.ItemType and canew.BudgetaryValidityId = @IdNextValidity
		inner join Budget.RevenueType rt on rt.Id = cd.RevenueTypeId
		inner join Budget.RevenueType rtnew on rtnew.Code = rt.Code and rtnew.BudgetaryValidityId = @IdNextValidity
		where c.Id = @IdCommitment and cd.Balance > 0

		FETCH NEXT FROM commitment_cursor   
		INTO @IdCommitment
	END   
	CLOSE commitment_cursor;  
	DEALLOCATE commitment_cursor; 
end --- FIN Si no es una ESE entonces trato de crear los compromisos como reservas

--- Valido que no haya Obligaciones Pendientes por confirmar 
if (select count(*) from Budget.Commitment where Status = 1 and BudgetaryValidityId = @IdValidity) > 0 begin
	declare @errorsUnConfirmedObligation varchar(MAX)
	select @errorsUnConfirmedObligation=stuff((select N';  ' + Code
	from Budget.Obligation where Status = 1 and BudgetaryValidityId = @IdValidity
	for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
	select '999' as CodeMessage, 'Las siguientes obligaciones se encuentran sin confirmar: ' + @errorsUnConfirmedObligation as [Message], cast(3 as tinyint) as [Status]
	return
end
--- Valido que todos los rubros esten homologados en la nueva vigencia
if (select count(*) from Budget.Obligation o
	inner join Budget.ObligationDetail od on o.Id = od.ObligationId
	inner join Budget.Category ca on ca.Id = od.CategoryId
	left join Budget.Category canew on canew.Code = ca.Code and canew.ItemType = ca.ItemType and canew.BudgetaryValidityId = @IdNextValidity
	where canew.Id is null and od.Balance > 0 and o.Status = 2) > 0 begin
		declare @errorsCategoryObligation varchar(MAX)
		select @errorsCategoryObligation=stuff((select N';  ' + canew.Code
		from Budget.Obligation o
		inner join Budget.ObligationDetail od on o.Id = od.ObligationId
		inner join Budget.Category ca on ca.Id = od.CategoryId
		left join Budget.Category canew on canew.Code = ca.Code and canew.ItemType = ca.ItemType and canew.BudgetaryValidityId = @IdNextValidity
		where canew.Id is null and od.Balance > 0 and o.Status = 2
		for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
		select '999' as CodeMessage, 'Los siguientes rubros no se encuentran creados en la vigencia '+ cast((@YearValidity + 1) as varchar(10)) +': ' + @errorsCategoryObligation as [Message], cast(3 as tinyint) as [Status]
		return
end
--- Valido que las fuentes de financiacion esten homologadas
if (select count(*) from Budget.Obligation o
	inner join Budget.ObligationDetail od on o.Id = od.ObligationId
	inner join Budget.RevenueType rt on rt.Id = od.RevenueTypeId
	left join Budget.RevenueType rtnew on rtnew.Code = rt.Code and rtnew.BudgetaryValidityId = @IdNextValidity
	where rtnew.Id is null and od.Balance > 0 and o.Status = 2) > 0 begin
		declare @errorsRevenueTypeObligation varchar(MAX)
		select @errorsRevenueTypeObligation = stuff((select N';  ' + rtnew.Code
		from Budget.Obligation o
		inner join Budget.ObligationDetail od on o.Id = od.ObligationId
		inner join Budget.RevenueType rt on rt.Id = od.RevenueTypeId
		left join Budget.RevenueType rtnew on rtnew.Code = rt.Code and rtnew.BudgetaryValidityId = @IdNextValidity
		where rtnew.Id is null and od.Balance > 0 and o.Status = 2
		for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
		select '999' as CodeMessage, 'Las siguientes fuentes de financiacion no se encuentran creados en la vigencia '+ cast((@YearValidity + 1) as varchar(10)) +': ' + @errorsRevenueTypeObligation as [Message], cast(3 as tinyint) as [Status]
		return
end
--- Creo las cuentas por pagar en la nueva vigencia 
select @IdSequence = Id, @Scope = Scope from [Budget].[BudgetSequence] where IdForm = 235
if @Scope = 'O' begin --- Secuencia por Prefijo
	select top 1 @pattern = cs.Pattern, @idSequenceDetail = psd.Id  from [Budget].[BudgetSequenceDetail] psd inner join Common.Sequense cs on cs.Id = psd.IdSequense where psd.[IdSequenseBudgetC] = @IdSequence
end
else begin -- Secuencia por Unidad operativa
	select @pattern = cs.Pattern, @idSequenceDetail = psd.Id  from [Budget].[BudgetSequenceDetail] psd inner join Common.Sequense cs on cs.Id = psd.IdSequense where psd.[IdSequenseBudgetC] = @IdSequence and IdOperatingUnit = @OperatingUnitId
end

declare @CodeObligation varchar(20)
declare @IdObligation int
declare @IdNextObligation int
DECLARE obligation_cursor CURSOR FOR   
select distinct o.Id
from Budget.Obligation o
inner join Budget.ObligationDetail od on o.Id = od.ObligationId
where o.Status = 2 and od.Balance > 0
  
OPEN obligation_cursor  
  
FETCH NEXT FROM obligation_cursor   
INTO @IdObligation
  
WHILE @@FETCH_STATUS = 0  
BEGIN  
	update [Budget].[BudgetSequenceDetail] set @NextS = [Next] += 1 where Id = @idSequenceDetail
	print ':P'
	print @Prefix
	print @pattern
	print @NextS
	set @CodeObligation = dbo.GetSequence(@Prefix,@pattern,(@NextS - 1))
	
	INSERT INTO [Budget].[Obligation]
           ([Code]
           ,[BudgetaryValidityId]
           ,[ThirdPartyId]
           ,[Document]
           ,[DocumentDate]
           ,[ObligationType]
           ,[Observations]
           ,[Status]
           ,[CreationUser]
           ,[CreationDate]
           ,[ConfirmationUser]
           ,[ConfirmationDate])
	select @CodeObligation, @IdNextValidity, ThirdPartyId, 'CxP Generada por la Obligacion ' + Code, DocumentDate, 2, Observations, 2, CreationUser, CreationDate, ConfirmationUser, ConfirmationDate
	from Budget.Obligation where Id = @IdObligation

	set @IdNextObligation = SCOPE_IDENTITY()

	INSERT INTO [Budget].[ObligationDetail]
           ([ObligationId]
           ,[CommitmentDetailId]
           ,[CategoryId]
           ,[RevenueTypeId]
           ,[ExpiredDate]
           ,[InitialValue]
           ,[DebitModificationValue]
           ,[CreditModificationValue]
           ,[TotalObligation]
           ,[ExecutedValue]
           ,[Balance])
	select @IdNextObligation, null, case @CxPSingleCategoryId when 1 then @CxPSingleCategoryId else canew.Id end, rtnew.Id, od.ExpiredDate, od.Balance, 0, 0, od.Balance, 0, od.Balance
	from Budget.Obligation o
	inner join Budget.ObligationDetail od on o.Id = od.ObligationId
	inner join Budget.Category ca on ca.Id = od.CategoryId
	inner join Budget.Category canew on canew.Code = ca.Code and canew.ItemType = ca.ItemType and canew.BudgetaryValidityId = @IdNextValidity
	inner join Budget.RevenueType rt on rt.Id = od.RevenueTypeId
	inner join Budget.RevenueType rtnew on rtnew.Code = rt.Code and rtnew.BudgetaryValidityId = @IdNextValidity
	where o.Status = 2 and od.Balance > 0 and o.Id = @IdObligation

	FETCH NEXT FROM obligation_cursor   
    INTO @IdObligation
END   
CLOSE obligation_cursor;  
DEALLOCATE obligation_cursor;  

update Budget.BudgetaryValidity set Status = 3 where Id = @IdValidity
update Budget.BudgetaryValidity set Status = 2 where Id = @IdNextValidity

select '0' as CodeMessage, 'Se realizo correctamente el Cierre Presupuestal de la Vigencia ' + cast(@YearValidity as varchar(20)) as [Message], cast(1 as tinyint) as [Status]

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de cierre presupuestal de una vigencia fiscal: valida que no existan disponibilidades (CDP), compromisos u órdenes de pago sin confirmar ni con saldo pendiente antes de permitir el cierre del año presupuestal. Si la entidad presupuestaria es una ESE, exige que todos los compromisos tengan saldo en cero; si no lo es, traslada los compromisos con saldo como reservas presupuestales a la vigencia siguiente, verificando que los rubros (categorías) y fuentes de financiación existan en el nuevo año. Opera sobre las tablas de vigencia presupuestaria, disponibilidades, compromisos y órdenes de pago, retornando mensajes de error de negocio cuando alguna condición impide el cierre.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ClosingValidity';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ClosingValidity';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Ejecuta el cierre de una vigencia presupuestal: valida la integridad de documentos pendientes y, si la entidad no es ESE, traslada compromisos y obligaciones con saldo como reservas y cuentas por pagar a la vigencia siguiente.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ClosingValidity';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una vigencia con Year = YearValidity + 1 en Budget.BudgetaryValidity (de lo contrario retorna mensaje 999).; No deben existir disponibilidades (Budget.Availability) con Status = 1 (sin confirmar) para la vigencia.; No deben existir disponibilidades con Status = 2 cuyo AvailabilityDetail.Balance > 0 (no debe haber saldo en disponibilidades).; No deben existir compromisos (Budget.Commitment) con Status = 1 para la vigencia.; No deben existir órdenes de pago (Budget.PaymentOrder) con Status = 1 para la vigencia.; No deben existir obligaciones (Budget.Obligation) con Status = 1 para la vigencia.; Si la entidad es ESE, no deben existir compromisos confirmados (Status=2) con CommitmentDetail.Balance > 0.; Si NO es ESE, todos los rubros (Category.Code+ItemType) y fuentes de financiación (RevenueType.Code) usados en compromisos y obligaciones con saldo deben existir en la vigencia siguiente.; Si NO es ESE, debe existir secuencia numérica configurada en Budget.BudgetSequence con IdForm = 231 (compromisos) y debe existir IdForm = 235 (cuentas por pagar).', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ClosingValidity';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Budget.Commitment: Si no es ESE, por cada compromiso con Status=2, Balance>0 y CommitmentType<>2 inserta un compromiso tipo reserva (CommitmentType=2, Status=2) en la vigencia siguiente con Document = ''Reserva creada por el Compromiso '' + Code y DocumentSource=1.; [INSERT] Budget.CommitmentDetail: Por cada compromiso replicado inserta sus líneas en la nueva vigencia mapeando CategoryId y RevenueTypeId al equivalente (mismo Code/ItemType) en la vigencia siguiente, copiando Balance como InitialValue, TotalCommitment y Balance, con AvailabilityDetailId NULL.; [INSERT] Budget.Obligation: Por cada obligación con Status=2 y ObligationDetail.Balance>0 inserta una nueva obligación (ObligationType=2, Status=2) en la vigencia siguiente con Document = ''CxP Generada por la Obligacion '' + Code.; [INSERT] Budget.ObligationDetail: Inserta los detalles de la nueva obligación (CxP) mapeando Category y RevenueType a la vigencia siguiente; CommitmentDetailId queda NULL y Balance se copia como InitialValue, TotalObligation y Balance.; [UPDATE] Budget.BudgetSequenceDetail: Antes de cada inserción de Commitment u Obligation incrementa Next en 1 sobre el detalle de secuencia (IdForm=231 para compromisos, IdForm=235 para CxP) seleccionado por Scope (''O'' = global por prefijo, otro = por OperatingUnitId).; [UPDATE] Budget.BudgetaryValidity: Al finalizar exitosamente, marca la vigencia cerrada con Status=3 y la vigencia siguiente con Status=2.; [RETURN_RESULT] Budget.Commitment: Si la entidad es ESE y existen compromisos con saldo, retorna mensaje ''781'' y un resultset con el detalle de compromisos/CommitmentDetail/Category con saldo > 0.; [RETURN_RESULT] Budget.BudgetaryValidity: Retorna CodeMessage ''999''/''780''/''781'' con Status=3 ante validaciones fallidas; retorna CodeMessage ''0'' con Status=1 y mensaje de cierre exitoso al concluir.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ClosingValidity';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe vigencia con Year = YearValidity+1 → Retorna CodeMessage ''999'' indicando que la vigencia siguiente no existe y termina.; si Existen disponibilidades con Status=1 para la vigencia → Retorna CodeMessage ''999'' listando los códigos de disponibilidades sin confirmar y termina.; si Existen disponibilidades confirmadas con Balance>0 → Retorna CodeMessage ''780'' (''Existen disponibilidades con saldo'') y termina.; si Existen compromisos/órdenes de pago/obligaciones con Status=1 → Retorna CodeMessage ''999'' con la lista de códigos sin confirmar y termina.; si @IsESE = 1 → Si hay compromisos confirmados con saldo, retorna error ''781'' y detalle; no se crean reservas en la vigencia siguiente. else Valida homologación de Category y RevenueType en la nueva vigencia, valida secuencia IdForm=231, y crea compromisos tipo reserva (CommitmentType=2) en la vigencia siguiente para cada compromiso confirmado con saldo y CommitmentType<>2.; si Scope de BudgetSequence = ''O'' → Toma el primer BudgetSequenceDetail por IdSequenseBudgetC (secuencia por prefijo/organización). else Selecciona BudgetSequenceDetail filtrando por IdOperatingUnit = @OperatingUnitId.; si Faltan rubros (Category) o fuentes (RevenueType) homologados en la vigencia siguiente para compromisos u obligaciones con saldo → Retorna CodeMessage ''999'' listando los códigos faltantes y termina sin crear documentos.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ClosingValidity';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ClosingValidity';
-- GO
