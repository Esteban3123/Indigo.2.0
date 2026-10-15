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