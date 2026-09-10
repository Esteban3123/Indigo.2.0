

-- ===============================================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 15/03/2021
-- Description:	Procedimiento que se encarga de guardar los cronogramas
-- ===============================================================================================================================
CREATE PROCEDURE [MixingStation].[SP_SaveProductionSchedule]
	@Xml xml,
	@UserCode varchar(20)
AS
BEGIN
	
	--Variables que se obtienen de generar la cabecera
	declare @Id int, @Code varchar(20), @CMConfigurationId int, @ProductionLineId int, @OperatingUnitId int

	--Tabla de detalles
	declare @ProductionScheduleDetail table(CampaignDetailId int)
	
	Begin try
		
		--Se obtienen los datos para la cabecera
		select 
			@CMConfigurationId = t.x.value('CMConfigurationId[1]','int'),
			@ProductionLineId = t.x.value('ProductionLineId[1]','int'),
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','int')
		from @Xml.nodes('/Data') t(x)

		--Se obtienen los detalles del xml
		insert into @ProductionScheduleDetail
		select 
			t.x.value('CampaignDetailId[1]','int') as CampaignDetailId
		from @Xml.nodes('/Data/Details') t(x)
		
		--Consultamos si la secuencia es con O o OU
		declare @scope varchar(5) = ''
		declare @idSequenceDetail int
		declare @pattern varchar(300)
		declare @NextS int
		select @scope = Scope from MixingStation.MixingStationSequence
		where IdForm = '2227'

		if @scope = 'O' --Si el ambito es por organización
		begin
			select top 1 @pattern = cs.Pattern, @NextS = bsd.[Next] , @idSequenceDetail = bsd.Id  
			from MixingStation.MixingStationSequenceDetail bsd 
			inner join MixingStation.MixingStationSequence bs on bs.Id = bsd.IdSequenseMixingStationC
			inner join Common.Sequense cs on cs.Id = bsd.IdSequense
			where bs.IdForm = '2227'
			order by bsd.Next desc
		end
		else begin --Si el ambito es por unidad operativa
			select top 1 @pattern = cs.Pattern, @NextS = bsd.[Next] , @idSequenceDetail = bsd.Id  
			from MixingStation.MixingStationSequenceDetail bsd 
			inner join MixingStation.MixingStationSequence bs on bs.Id = bsd.IdSequenseMixingStationC
			inner join Common.Sequense cs on cs.Id = bsd.IdSequense
			where bs.IdForm = '2227' and bsd.IdOperatingUnit = @OperatingUnitId
			order by bsd.Next desc
		end
					
		if (@idSequenceDetail is null)
		Begin
			select 999 as CodeMessage, 'Secuencia no encontrada para generar el cronograma' as Message, 0 Id, '' Code
			return
		End

		select @Code = dbo.GetSequence('',@pattern,@NextS)
		update MixingStation.MixingStationSequenceDetail set [Next] += 1 where Id = @idSequenceDetail

		INSERT INTO [MixingStation].[ProductionSchedule]([Code],[CMConfigurationId],[ProductionLineId],[Status],[CreationUser],[CreationDate])
		VALUES(@Code, @CMConfigurationId, @ProductionLineId, 1, @UserCode, Common.GETDATE())

		set @Id = SCOPE_IDENTITY()
		
		--Se insertan los detalles de campañas
		INSERT INTO [MixingStation].[ProductionScheduleDetail]([ProductionScheduleId],[CampaignDetailId])
		select @Id, CampaignDetailId
		from @ProductionScheduleDetail
		
		--Se retorna el ok
		select 0 as CodeMessage, 'Se generó la orden de producción con código ' + @Code as Message, @Id Id, @Code Code
		return

	End try
	Begin Catch

		--Se retorna el error
		select 999 as CodeMessage, ERROR_MESSAGE() as Message, 0 Id, '' Code
		return

	End Catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que crea y guarda un cronograma de producción en la estación de mezcla. A partir de un XML con la configuración de campaña, línea de producción y unidad operativa, genera automáticamente un código consecutivo usando la secuencia configurada para el formulario 2227 (respetando el ámbito por organización o por unidad operativa). Registra la cabecera del cronograma en ProductionSchedule con su código, estado y usuario creador, y luego inserta los detalles de campañas asociados en ProductionScheduleDetail. Retorna el identificador y código del cronograma generado, o un mensaje de error si la secuencia no está configurada o ocurre una falla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_SaveProductionSchedule';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_SaveProductionSchedule';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera y persiste un cronograma (orden) de producción en la estación de mezcla, asignando un consecutivo según el ámbito configurado (organización o unidad operativa) y registrando sus detalles de campaña.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveProductionSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en MixingStation.MixingStationSequence con IdForm=''2227'' que defina el Scope (''O'' u otro) aplicable.; Debe existir al menos un MixingStationSequenceDetail asociado al formulario 2227 (y a la unidad operativa cuando el ámbito no es ''O'') con su patrón en Common.Sequense; de lo contrario se retorna error 999 ''Secuencia no encontrada''.; El XML de entrada debe contener el nodo /Data con CMConfigurationId, ProductionLineId y OperatingUnitId, y los nodos /Data/Details con CampaignDetailId.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveProductionSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todo ProductionSchedule creado nace con Status=1.; El Code del cronograma se genera mediante dbo.GetSequence usando el patrón de Common.Sequense y el valor [Next] vigente, e inmediatamente se incrementa [Next] para garantizar unicidad del consecutivo.; La selección de la secuencia siempre se hace ORDER BY bsd.Next DESC TOP 1 (toma el detalle con mayor Next).; Toda la lógica está envuelta en TRY/CATCH; cualquier excepción se traduce en una respuesta con CodeMessage=999 sin propagar el error.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveProductionSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cronograma/Orden de producción; Estación de mezcla (Mixing Station); Secuencia/Consecutivo; Ámbito por Organización vs Unidad Operativa; Línea de producción; Configuración CM; Detalle de campaña', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveProductionSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] MixingStation.ProductionSchedule: Tras obtener el consecutivo, inserta la cabecera con Status=1 (activo), CreationUser=@UserCode y CreationDate=Common.GETDATE().; [INSERT] MixingStation.ProductionScheduleDetail: Por cada CampaignDetailId provisto en /Data/Details del XML, inserta un detalle vinculado al ProductionSchedule recién creado (SCOPE_IDENTITY).; [UPDATE] MixingStation.MixingStationSequenceDetail: Incrementa en 1 la columna [Next] del registro de secuencia seleccionado (Id=@idSequenceDetail) tras generar el código.; [RETURN_RESULT] (resultset): Si la secuencia no existe retorna CodeMessage=999 con mensaje ''Secuencia no encontrada para generar el cronograma''; en éxito retorna CodeMessage=0 con ''Se generó la orden de producción con código ''+@Code, Id y Code; ante excepción retorna CodeMessage=999 con ERROR_MESSAGE().', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveProductionSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Scope de MixingStationSequence (IdForm=''2227'') = ''O'' → Selecciona el patrón y siguiente consecutivo desde MixingStationSequenceDetail filtrando solo por IdForm=''2227'' (ámbito organización), tomando el de mayor [Next]. else Selecciona el patrón y siguiente consecutivo filtrando además por IdOperatingUnit=@OperatingUnitId (ámbito unidad operativa).; si @idSequenceDetail IS NULL tras la búsqueda de secuencia → Aborta el proceso retornando error 999 ''Secuencia no encontrada para generar el cronograma'' sin insertar nada.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveProductionSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.GetSequence; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveProductionSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.MixingStationSequence; MixingStation.MixingStationSequenceDetail; Common.Sequense', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveProductionSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveProductionSchedule';
-- GO
