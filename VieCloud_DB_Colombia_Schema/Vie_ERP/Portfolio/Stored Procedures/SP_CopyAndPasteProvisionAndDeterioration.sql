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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que aplica masivamente provisiones y deterioro de cartera copiando y pegando configuraciones desde un formulario de gestión de cuentas por cobrar. Recibe un XML con listado de facturas (número de factura, expectativa de cobro y porcentaje de provisión o deterioro), valida cada registro contra las cuentas por cobrar existentes y las edades de cartera configuradas para la unidad operativa, y calcula el tramo de vencimiento (aging) de cada factura según los días transcurridos desde su fecha de confirmación hasta la fecha de corte indicada. Utiliza la configuración de rangos de edad de cartera (Portfolio.AgesPortfolio y Portfolio.SettingPortfolio) para determinar el porcentaje aplicable y registra el resultado de provisión o deterioro sobre cada factura procesada, devolviendo un detalle del estado de éxito o error por cada registro cargado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteProvisionAndDeterioration';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteProvisionAndDeterioration';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida y arma, a partir de un XML pegado por el usuario, los registros de facturas para los formularios de provisión o deterioro de cartera, calculando edad, porcentaje y datos asociados según la fecha de corte y la unidad operativa.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteProvisionAndDeterioration';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir configuración en Portfolio.SettingPortfolio para la unidad operativa indicada.; Deben existir rangos de edades (Portfolio.AgesPortfolio) asociados a la SettingPortfolio de la unidad operativa.; Cada nodo /Data/Row del XML debe traer CountFields, InvoiceNumber, Expectative y Percentage.; Process debe indicar 1=provisión o 2=deterioro/cartera; applyDeterioration=1 indica que aplica a cartera.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteProvisionAndDeterioration';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila del XML produce exactamente un registro en @TableResult (sea de éxito o de error).; El primer error encontrado por fila corta el procesamiento de esa fila (CONTINUE) y no se evalúan validaciones posteriores.; Sólo errores fatales globales (sin edades configuradas o excepción capturada) se marcan con Exception=1; los errores por fila usan Exception=0.; La edad (días) se calcula siempre como DATEDIFF(day, DocumentDate de la factura, @CourtDate).; El procedimiento no modifica tablas físicas: sólo lee y devuelve un result set.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteProvisionAndDeterioration';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cartera (cuentas por cobrar); Provisión de cartera; Deterioro de cartera; Edades de cartera (aging); Factura / Cuenta por cobrar; Glosa; Saldo y valor glosado; Régimen; Tercero / NIT; Unidad operativa; Fecha de corte; Expectativa de recaudo', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteProvisionAndDeterioration';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableResult: Si no existen edades de cartera para la unidad operativa (no hay join SettingPortfolio-AgesPortfolio), se inserta un único registro de error con Exception=1 y se retorna inmediatamente.; [INSERT] @TableResult: Si CountFields <> 3 se inserta error ''no tiene la estructura requerida'' (Exception=0) y se continúa con el siguiente registro.; [INSERT] @TableResult: Si InvoiceNumber no existe en Portfolio.AccountReceivable se inserta error indicando que la factura no existe.; [INSERT] @TableResult: Si Expectative no es numérica o es < 0 se inserta error ''la expectativa ... no es válida''.; [INSERT] @TableResult: Si Percentage no es numérico o es < 0 se inserta error ''el porcentaje ... no es válida''.; [INSERT] @TableResult: Si la factura no aparece en ViewAccountReceivableByPortfolioProvision se inserta error ''no tiene saldo o no está radicada''.; [INSERT] @TableResult: Cuando @Process=2 y @applyDeterioration=1, si la factura tiene GlosaPortfolioGlosadaId NOT NULL en la vista, se inserta error ''está en un proceso de glosa y no se puede pegar''.; [INSERT] @TableResult: Si la diferencia en días (DATEDIFF(day, DocumentDate, @CourtDate)) es <= MaximunAgeRange pero no cae dentro de ningún rango [InitialRange, EndRange] de AgesPortfolio para la unidad, se inserta error ''no existe en el rango de edades de cartera''.; [INSERT] @TableResult: Cuando todas las validaciones pasan, se inserta un registro con StatusField=1, ''Registro Correcto'', con datos de la factura (fecha, valor, saldo, glosado, deterioro previo, régimen, NIT) y el porcentaje/edad calculados.; [RETURN_RESULT] @TableResult: Al final del bucle (o al capturar excepción) se retorna SELECT * FROM @TableResult.; [INSERT] @TableResult: En el bloque CATCH se inserta un registro con ERROR_MESSAGE()+ERROR_LINE() y Exception=1.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteProvisionAndDeterioration';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Process = 2 AND @applyDeterioration = 1 (aplica a cartera) → Adicionalmente verifica que la factura no esté en proceso de glosa (GlosaPortfolioGlosadaId IS NOT NULL); si lo está, descarta el registro. else Omite la validación de glosa.; si @Process = 1 (provisión) vs distinto (deterioro) → Toma ap.ProvisionPercentage como porcentaje calculado. else Toma ap.DeteriorationPercentage como porcentaje calculado.; si @Expectative del XML = 0 → Si @Process=2 usa la expectativa actual de la factura (v.Expectative); si no, deja 0. else Conserva la expectativa proporcionada en el XML.; si @Percentage del XML = 0 → Usa el porcentaje calculado del rango de edad (provisión o deterioro según Process). else Conserva el porcentaje proporcionado en el XML.; si @cDiferenceDays <= MaximunAgeRange y existe rango → Asigna AgesId y descripción ''De X a Y''. else Si AgesId es NULL (supera el rango máximo) usa sp.NameMaximumAgeRange como descripción.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteProvisionAndDeterioration';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.SettingPortfolio; Portfolio.AgesPortfolio; Portfolio.AccountReceivable; Portfolio.ViewAccountReceivableByPortfolioProvision', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteProvisionAndDeterioration';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteProvisionAndDeterioration';
-- GO
