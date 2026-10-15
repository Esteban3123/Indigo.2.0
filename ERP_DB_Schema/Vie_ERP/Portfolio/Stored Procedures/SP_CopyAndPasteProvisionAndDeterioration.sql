-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 03/12/2015
-- Description:	Procedimiento que se encarga de el copyPaste del form provisiones y deterioro
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_CopyAndPasteProvisionAndDeterioration] 
	@XmlObject as Xml,
	@CourtDate as date,
	@Process as int,	
	@OperatingUnitId as int,
	@applyDeterioration as int
AS
BEGIN
	
	--Tabla para almacenar los items del listado que viene en el xml
	declare @TableXmlObject table
	(
		Id int IDENTITY PRIMARY KEY, 
		CountFields int, 
		StatusField int, 
		MessageField varchar(max), 
		ConfirmDate datetime,
		InvoiceNumber varchar(20), 
		Expectative int, 
		[Percentage] numeric(7,4)
	)
	
	--Tabla para devolver los resultados
	declare @TableResult table
	(
		Id int IDENTITY PRIMARY KEY, 
		StatusField int, 
		MessageField varchar(max), 
		Exception bit, 

		ConfirmDate datetime, 
		AccountReceivableId int, 
		InvoiceNumber varchar(20), 
		Days int,
		AgesId int, 
		AgesDescription varchar(100), 
		InvoiceValue numeric(18,2),
		Balance numeric(18,2), 
		ValueGlosado numeric(18,2),
		Expectative int,
		[Percentage] numeric(7,4), 
		DeteriorationBalance numeric(18,2),

		RegimenName varchar(200), 
		ThirdPartyNitName varchar(100)
	)
		
	declare @MaxAgesInitialRange int,			--Rango inicial de la máxima edad
			@cConfirmDate datetime,				--Fecha confirmación factura
			@cAccountReceivableId int,			--Id de la factura
			@cDiferenceDays int,				--Diferencia en días entre la fecha de confirmación y la fecha de corte
			@cAgesId int,						--Id de la edad
			@cAgesDescription varchar(100),		--Descripción de la edad
			@cInvoiceValue numeric(18,2),		-- Valor facturado
			@cBalance numeric(18,2),			--Saldo de la factura	
			@cValueGlosado numeric(18,2),		--Valor glosado
			@cExpectative int,					--Ultima expectativa ingresada para la factura
			@cPercentage numeric(7,4),			--Porcentaje de la provision o el deterioro
			@cDeteriorationBalance numeric(18,2), --Valor deteriorado anteriormente			
			@cRegimenName varchar(200),
			@cThirdPartyNitName varchar(100)			

	Begin try
		
		SELECT	@MaxAgesInitialRange = MaximunAgeRange
		FROM Portfolio.SettingPortfolio 
		WHERE OperatingUnitId = @OperatingUnitId

		--Se valida que hayan edades de cartera con la unidad operativa escogida
		if NOT EXISTS (select 1 from Portfolio.SettingPortfolio sp join Portfolio.AgesPortfolio ap on ap.SettingPortfolioId = sp.Id where sp.OperatingUnitId = @OperatingUnitId)
		Begin
			--Se actualiza los campos con el estado en false y el mensaje de error
			insert into @TableResult(StatusField, MessageField, Exception)			
				values(0, 'No existen edades de cartera para la unidad operativa escogida', 1)

			select * from @TableResult
			return
		End

		insert into @TableXmlObject
		(
			CountFields, StatusField, MessageField, ConfirmDate, InvoiceNumber,  Expectative, Percentage
		)
		SELECT	t.x.value('CountFields[1]','int') as CountFields,
				t.x.value('StatusField[1]','int') as StatusField,
				t.x.value('MessageField[1]','varchar(100)') as MessageField,				
				convert(datetime, t.x.value('ConfirmDate[1]','varchar(20)'), 103) as ConfirmDate,
				t.x.value('InvoiceNumber[1]','varchar(20)') as InvoiceNumber,
				ISNULL(t.x.value('Expectative[1]','int'), 0) as Expectative,
				ISNULL(t.x.value('Percentage[1]','numeric(7, 4)'), 0) as [Percentage]
		from @XmlObject.nodes('/Data/Row') t(x)

		
		DECLARE @Rows INT = 1,
				@RowId INT = 0,
				--Variables de control
				@CountFields as int,
				@InvoiceNumber as varchar(20),
				@Expectative as int,
				@Percentage as DECIMAL(7,4)

		WHILE @Rows > 0
		BEGIN
			Select TOP 1
					@RowId = [Id], 
					@CountFields = [CountFields], 
					@InvoiceNumber = [InvoiceNumber], 
					@Expectative = Expectative, 
					@Percentage = Percentage 
			From @TableXmlObject
			WHERE Id > @RowId
			ORDER BY Id

			SET @Rows = @@ROWCOUNT
			IF @Rows = 0 
			BEGIN
				BREAK
			END

			-------------------------------------------------------------------

			--Se valida que cada registro tenga la estructura requerida
			if @CountFields <> 3
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				insert into @TableResult(StatusField, MessageField, Exception)
					values(0, 'El registro ' + convert(varchar(3),@RowId) + ' no tiene la estructura requerida', 0)				
				CONTINUE
			End		

			--Se valida que la factura exista en la tabla
			if NOT EXISTS (select 1 from Portfolio.AccountReceivable where InvoiceNumber = @InvoiceNumber)
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				insert into @TableResult(StatusField, MessageField, Exception)
					values(0, 'El No. de factura ' + @InvoiceNumber +  ' del registro ' + convert(varchar(3),@RowId) + ' no existe', 0)
				CONTINUE
			End

			--Se valida que la expectativa sea un numero valido
			if ISNUMERIC(@Expectative) = 0 OR @Expectative < 0
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				insert into @TableResult(StatusField, MessageField, Exception)
					values(0, 'La expectativa del registro ' + convert(varchar(3),@RowId) + ' no es válida', 0)
				CONTINUE
			end

			--Se valida que el porcentaje sea un numero valido
			if ISNUMERIC(@Percentage) = 0 OR @Percentage < 0
			begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				insert into @TableResult(StatusField, MessageField, Exception)
					values(0, 'El porcentaje del registro ' + convert(varchar(3),@RowId) + ' no es válida', 0)
				CONTINUE
			end
			
			--Se valida que la factura sea válida
			if NOT EXISTS
			(
				SELECT 1
				FROM Portfolio.ViewAccountReceivableByPortfolioProvision v
				WHERE v.InvoiceNumber = @InvoiceNumber
			)
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				insert into @TableResult(StatusField, MessageField, Exception)
					values(0, 'El No. de factura ' + @InvoiceNumber +  ' del registro ' + convert(varchar(3),@RowId) + ' no tiene saldo o no esta radicada', 0)
				CONTINUE
			End

			--Se valida si aplica a cartera que la factura no este en una glosa
			IF @Process = 2 AND @applyDeterioration = 1 --Aplica a cartera
			BEGIN				
				--Se valida que la factura no este en una glosa
				IF EXISTS
				(
					SELECT 1
					FROM Portfolio.ViewAccountReceivableByPortfolioProvision v					
					WHERE v.InvoiceNumber = @InvoiceNumber AND v.GlosaPortfolioGlosadaId IS NOT NULL
				)
				begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					insert into @TableResult(StatusField, MessageField, Exception)
						values(0, 'La factura ' + @InvoiceNumber +  ' del registro ' + convert(varchar(3),@RowId) + ' está en un proceso de glosa y no se puede pegar', 0)
					CONTINUE
				end					
			END

			--Se obtienen los campos necesarios
			SELECT	@cConfirmDate = v.DocumentDate, 
					@cAccountReceivableId = v.Id, 
					@cInvoiceValue = v.[Value], 
					@cBalance = v.Balance, 
					@cValueGlosado = v.ValueGlosado, 
					@cExpectative = v.Expectative,
					@cDeteriorationBalance = v.DeteriorationBalance,				
					@cRegimenName = v.RegimenName,
					@cThirdPartyNitName = CONCAT(v.ThirdPartyNit, ' - ', v.ThirdPartyName),
					--Se calcula la diferencia en días entre la fecha de corte y la fecha de confirmación
					@cDiferenceDays = DATEDIFF(day, v.DocumentDate, @CourtDate)
			FROM Portfolio.ViewAccountReceivableByPortfolioProvision v
			WHERE v.InvoiceNumber = @InvoiceNumber
			
			--Se valida si la diferencia en días se encuentra en el rango de edades
			IF @cDiferenceDays <= @MaxAgesInitialRange AND NOT EXISTS
			(
				select sp.Id
				from Portfolio.SettingPortfolio sp
				join Portfolio.AgesPortfolio ap on ap.SettingPortfolioId = sp.Id
				where sp.OperatingUnitId = @OperatingUnitId And @cDiferenceDays >= ap.InitialRange And @cDiferenceDays <= ap.EndRange
			)
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				insert into @TableResult(StatusField, MessageField, Exception)
					values(0, 'La diferencia en días del registro ' + convert(varchar(3),@RowId) + ' no existe en el rango de edades de cartera', 0)
				CONTINUE
			End
			
			--Se obtiene el rango de la edad dependiendo de la diferencia en días
			select	@cAgesId = ap.Id, 
					@cAgesDescription = IIF(ap.Id IS NULL, sp.NameMaximumAgeRange, 'De ' + CAST(ap.InitialRange as varchar) + ' a ' + CAST(ap.EndRange as varchar)),
					@cPercentage = case @Process when 1 then ap.ProvisionPercentage else ap.DeteriorationPercentage end
			from Portfolio.SettingPortfolio sp
			left join Portfolio.AgesPortfolio ap on ap.SettingPortfolioId = sp.Id And @cDiferenceDays >= ap.InitialRange And @cDiferenceDays <= ap.EndRange
			where sp.OperatingUnitId = @OperatingUnitId
			
			 --Se actualiza los campos con que se necesitan para armar el objeto en el formulario
			insert into @TableResult
			(
				StatusField, 
				MessageField, 
				Exception, 
				  
				ConfirmDate, 
				AccountReceivableId, 
				InvoiceNumber, 
				Days,
				AgesId, 
				AgesDescription, 
				InvoiceValue, 
				Balance, 
				ValueGlosado, 
				Expectative,				  
				[Percentage], 	
				DeteriorationBalance,
				RegimenName, 
				ThirdPartyNitName
			)
			values
			(	
				1, 
				'Registro Correcto', 
				0, 

				@cConfirmDate, 
				@cAccountReceivableId, 
				@InvoiceNumber, 
				@cDiferenceDays,
				@cAgesId, 
				@cAgesDescription, 
				@cInvoiceValue, 
				@cBalance, 
				@cValueGlosado,
				IIF(@Expectative = 0, IIF(@Process = 2, @cExpectative, 0), @Expectative),
				IIF(@Percentage = 0, @cPercentage, @Percentage), 
				@cDeteriorationBalance,				
				@cRegimenName, 
				@cThirdPartyNitName 				
			)
		END
		
		--Se retorna la tabla
		select * from @TableResult		
	end try
	begin catch
		insert into @TableResult(StatusField, MessageField, Exception)
		values (0, ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() as varchar(5)), 1)

		select * from @TableResult
	end catch
END