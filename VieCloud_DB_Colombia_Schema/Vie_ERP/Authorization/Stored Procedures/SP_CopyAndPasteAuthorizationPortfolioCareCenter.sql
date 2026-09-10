-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 05/05/2020
-- Description:	Procedimiento que se encarga del Copy & Paste de los centros de atención al portafolio de autorización
-- =============================================
CREATE PROCEDURE [Authorization].[SP_CopyAndPasteAuthorizationPortfolioCareCenter] 
	@XmlObject as Xml
AS
BEGIN
	SET NOCOUNT ON

	--Tabla para almacenar los items del listado que viene en el xml y poder guardar las homologaciones de cuenta
	declare @TableXmlObject table(Id int IDENTITY PRIMARY KEY, CountFields int, StatusField int, MessageField varchar(max), 
	CareCenterCode varchar(20), CareCenterCodeName varchar(200))

	--begin transaction
	Begin try
	
		insert into @TableXmlObject
		select 
		t.x.value('CountFields[1]','int') as CountFields,
		t.x.value('StatusField[1]','int') as StatusField,
		t.x.value('MessageField[1]','varchar(100)') as MessageField,
		t.x.value('CareCenterCode[1]','varchar(20)') as CareCenterCode,
		t.x.value('CareCenterCodeName[1]','varchar(200)') as CareCenterCodeName
		from @XmlObject.nodes('/Data/Row') t(x)

		--Se declara el contador de posiciones para enviar en los mensajes de error
		Declare @Position as int = 0
		--Se declara la variable para poder realizar las validaciones
		Declare @Count as int

		--Se declara un cursor y las variables que lleva el cursor
		Declare @CountFields as int
		Declare @StatusField as int
		Declare @MessageField as varchar(100)
		Declare @Id as int
		Declare @CareCenterCode as varchar(20)
		Declare @CareCenterCodeName as varchar(200)
		
		Declare InfoItem Cursor For Select [CountFields], [StatusField], [MessageField], 
										   [Id], CareCenterCode, CareCenterCodeName From @TableXmlObject

		Open InfoItem

		Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @CareCenterCode, @CareCenterCodeName

		While @@fetch_status = 0
		Begin
		
			--Se incrementa la posicion
			set @Position = @Position + 1
			
			--Se valida que cada registro tenga la estructura requerida
			if @CountFields <> 1
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El registro ' + convert(varchar(3),@Position) + ' no tiene la estructura requerida'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @CareCenterCode, @CareCenterCodeName
				continue
			End		

			--Se valida que el código de centro de atención exista
			if not exists(select 1 from .ADCENATEN where CODCENATE = @CareCenterCode)
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'No existe el centro de atención con código ' + @CareCenterCode + ' del registro ' + convert(varchar(3),@Position)
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @CareCenterCode, @CareCenterCodeName
				continue
			end

			--Se obtiene la información necesaria
			select @CareCenterCodeName = RTRIM(LTRIM(CODCENATE)) + ' - ' + RTRIM(LTRIM(NOMCENATE)) from .ADCENATEN where CODCENATE = @CareCenterCode

			 --Se actualiza los campos con que se necesitan para armar el objeto en el formulario
			update @TableXmlObject set StatusField = 1, CareCenterCodeName = @CareCenterCodeName
			where Id = @Id	
			
			--Se pasa a la siguiente posicion del cursor
			Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @CareCenterCode, @CareCenterCodeName
			continue

		End

		Close InfoItem
		Deallocate InfoItem

		
		--Se retorna la tabla
		select * from @TableXmlObject
		
	end try
	begin catch

		--rollback transaction
		select * from @TableXmlObject

	end catch
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que realiza la operación de copiar y pegar centros de atención en el portafolio de autorización. Recibe un listado de centros de atención en formato XML, valida que cada registro tenga la estructura correcta y que el código de centro de atención exista en la tabla maestra de sedes (ADCENATEN). Por cada centro válido, construye el nombre completo combinando el código y el nombre de la sede, y retorna el listado procesado con el estado de validación y los mensajes de error correspondientes. Se utiliza en el módulo de autorizaciones para asociar o replicar centros de atención habilitados dentro del portafolio de servicios autorizados.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteAuthorizationPortfolioCareCenter';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteAuthorizationPortfolioCareCenter';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida y enriquece una lista de centros de atención recibida en XML para su pegado masivo en el portafolio de autorización, marcando estado y armando el descriptor "código - nombre".', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteAuthorizationPortfolioCareCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe seguir la estructura /Data/Row con los nodos CountFields, StatusField, MessageField, CareCenterCode y CareCenterCodeName.; Cada fila debe traer CountFields = 1 para considerarse estructuralmente válida.; El código de centro de atención debe existir en ADCENATEN (CODCENATE).', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteAuthorizationPortfolioCareCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila del XML resulta clasificada con StatusField=0 (con mensaje de error) o StatusField=1 (válida con descriptor armado).; El procedimiento no realiza modificaciones sobre tablas físicas; solo valida y devuelve un resultset.; Los errores capturados en el CATCH no propagan excepción: igualmente se devuelve el contenido de la tabla temporal.; El descriptor del centro siempre se compone como ''CODCENATE - NOMCENATE'' con espacios recortados.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteAuthorizationPortfolioCareCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de atención; Portafolio de autorización; Copy & Paste de configuración; Autorización', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteAuthorizationPortfolioCareCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableXmlObject: Se cargan todas las filas del XML (/Data/Row) en la tabla temporal para procesarlas secuencialmente.; [UPDATE] @TableXmlObject: Si CountFields <> 1, se marca StatusField = 0 y MessageField = ''El registro N no tiene la estructura requerida''.; [UPDATE] @TableXmlObject: Si el CareCenterCode no existe en ADCENATEN, se marca StatusField = 0 y MessageField = ''No existe el centro de atención con código X del registro N''.; [UPDATE] @TableXmlObject: Si las validaciones pasan, se marca StatusField = 1 y se asigna CareCenterCodeName = RTRIM(LTRIM(CODCENATE)) + '' - '' + RTRIM(LTRIM(NOMCENATE)).; [RETURN_RESULT] @TableXmlObject: Al finalizar (o ante cualquier excepción capturada en el CATCH) se retorna el contenido completo de @TableXmlObject con el resultado de cada fila.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteAuthorizationPortfolioCareCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @CountFields <> 1 → Marca el registro como inválido (StatusField=0) con mensaje de estructura y continúa con el siguiente. else Procede con la validación de existencia del centro de atención.; si NOT EXISTS en ADCENATEN para el CareCenterCode → Marca el registro como inválido (StatusField=0) con mensaje de centro inexistente y continúa. else Marca StatusField=1 y construye CareCenterCodeName con ''código - nombre''.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteAuthorizationPortfolioCareCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'ADCENATEN', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteAuthorizationPortfolioCareCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteAuthorizationPortfolioCareCenter';
-- GO
