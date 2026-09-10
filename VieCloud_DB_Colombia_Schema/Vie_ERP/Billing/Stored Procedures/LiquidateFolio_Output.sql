-- =============================================
-- Author:		Diego A. Roldán
-- Create date: 2018-07-19
-- Description:	Liquidación
-- =============================================
CREATE Procedure [Billing].[LiquidateFolio_Output]
	@PatientCode Varchar(20),
	@AdmissionNumber Varchar(20),
	@ContainerCrystal Varchar(10),
	@BillingAuthorizationId Int,
	@OperativeUnitId Int,
	@ThirdPartyPatientId Int,
	@UserCode Varchar(20),
	@CompanyType Tinyint, --se saca de la auditoria
	@RevenueControlDetailCrossingListXml Xml,
	--Salidas
	@ResultStatus bit output,
	@ResultMessageInvoice varchar(Max) output,
	@ResultMessage varchar(Max) output,
	@ResultXml XML OUTPUT
AS
Begin
	Set Nocount On;

	set @ResultStatus  =  Convert(Bit, 0)
	set @ResultMessageInvoice = ''
	set @ResultMessage = ''
	set @ResultXml = ''

	Begin Try
		print 'sdsd'
		--Validamos que existan los datos de entrada
		If IsNull(@PatientCode, '') = '' Begin
			set @ResultMessage = 'No se ha enviado el parámetro Código de Paciente'
			Return
		End
		If IsNull(@AdmissionNumber, '') = '' Begin
			set @ResultMessage = 'No se ha enviado el parámetro Número de Ingreso'
			Return
		End
		If IsNull(@ContainerCrystal, '') = '' Begin
			set @ResultMessage = 'No se ha enviado el parámetro Contenedor HIS'
			Return
		End
		If @BillingAuthorizationId = 0 Begin
			set @ResultMessage = 'No se ha enviado el parámetro Autorización de Facturación'
			Return
		End
		If @OperativeUnitId = 0 Begin
			set @ResultMessage = 'No se ha enviado el parámetro Unidad Operativa'
			Return
		End
		If NOT EXISTS (SELECT 1 FROM Billing.SettingsBilling WHERE IdOperatingUnit = @OperativeUnitId) Begin
			set @ResultMessage = 'No se encontraron parámetros de Facturación para la unidad operativa seleccionada'
			Return
		End
		If EXISTS (SELECT 1 FROM Billing.SettingsBilling WHERE IdOperatingUnit = @OperativeUnitId AND ParticularHealthAdministratorId IS NULL) Begin
			set @ResultMessage = 'No esta parametrizada una entidad administradora para particulares en la unidad operativa seleccionada'
			Return
		End
		If @ThirdPartyPatientId = 0 Begin
			set @ResultMessage = 'No se ha enviado el parámetro Tercero Paciente'
			Return
		End
		If IsNull(@UserCode, '') = '' Begin
			set @ResultMessage = 'No se ha enviado el parámetro Usuario Auditoria'
			Return
		End
		If @RevenueControlDetailCrossingListXml Is Null Begin
			set @ResultMessage = 'No hay folios para liquidar'
			Return
		End
		If not exists (select 1 from .ADINGRESO ai where ai.NUMINGRES = @AdmissionNumber and ai.IPCODPACI = @PatientCode) Begin
			set @ResultMessage = 'El paciente no coincide con el del ingreso'
			Return
		End

		-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		IF EXISTS 
		(
			SELECT 1
			FROM Billing.ServiceOrder AS so
			JOIN Billing.ServiceOrderDetail AS sod ON so.Id = sod.ServiceOrderId
			LEFT JOIN Billing.ServiceOrderDetailDistribution sodd ON sod.Id = sodd.ServiceOrderDetailId
			WHERE so.AdmissionNumber = @AdmissionNumber
				AND so.Status = 1
				AND sod.Packaging = 0
				AND sod.IsDelete = 0
				AND sod.InvoicedQuantity > 0	
				AND sodd.Id IS NULL
		)
		BEGIN
			set @ResultMessage = 'Existen detalles de la admisión sin su distribución. Por favor contacte con el administrador'
			RETURN
		END
		-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

		Declare @Uno Tinyint = 1,
				@Tres Tinyint = 3

		Declare @errorList As Varchar(Max) = ''
		Declare @consecutiveList Varchar(Max) = ''
		Declare @messageNotification Varchar(Max) = ''

		Declare @RevenueControlDetailCrossingList Table
		(
			RowId Int Identity(1,1) Primary Key, 
			FolioOrder Tinyint,
			FolioType Tinyint,
			CareGroupId Int,
			RevenueControlDetailId Int,
			ListPortfolioAdvanceCrossing Xml,
			TotalPatientDiscount Decimal(18, 2),
			RevenueControlId Int,
			OutputDate DateTime,
			IsCutAccount Bit,
			OutputDiagnosis Varchar(10),
			InitialDate DateTime,
			CutType Int
		)

		Delete From @RevenueControlDetailCrossingList
		Insert Into @RevenueControlDetailCrossingList
			Select t.x.value('FolioOrder[1]', 'Tinyint'),
				t.x.value('FolioType[1]', 'Tinyint'),
				t.x.value('CareGroupId[1]', 'Int'),
				t.x.value('RevenueControlDetailId[1]', 'Int'),
				t.x.query('ListPortfolioAdvanceCrossing'),
				t.x.value('TotalPatientDiscount[1]', ' Decimal(18, 2)'),
				t.x.value('RevenueControlId[1]', 'Int'),
				Convert(DateTime, t.x.value('OutputDate[1]', 'nvarchar(19)'), 103),
				t.x.value('IsCutAccount[1]', 'Bit'),
				t.x.value('OutputDiagnosis[1]', 'Varchar(10)'),
				Convert(DateTime, t.x.value('InitialDate[1]', 'nvarchar(19)'), 103),
				t.x.value('CutType[1]', 'Int')
			From @RevenueControlDetailCrossingListXml.nodes('RevenueControlDetailCrossingList') t(x)

		IF EXISTS (
			SELECT 1
			FROM @RevenueControlDetailCrossingList l
			JOIN Billing.RevenueControlDetail rcd ON l.RevenueControlDetailId = rcd.Id
			JOIN Contract.CareGroup cg ON rcd.CareGroupId = cg.Id
			JOIN Contract.HealthAdministrator ha ON rcd.HealthAdministratorId = ha.Id
			WHERE cg.CareGroupType <> 3
				AND rcd.ThirdPartyId <> ha.ThirdPartyId
		)
		BEGIN
			set @ResultMessage = 'El tercero del folio no corresponde con el tercero de la entidad administradora.'
			Return
		END
		
		IF EXISTS (
			SELECT 1
			FROM @RevenueControlDetailCrossingList l
			JOIN Billing.RevenueControlDetail rcd ON l.RevenueControlDetailId = rcd.Id
			JOIN Common.ThirdParty tp ON rcd.ThirdPartyId = tp.Id
			LEFT JOIN Common.Address a ON tp.PersonId = a.IdPerson
			WHERE a.Id IS NULL OR a.DepartmentId IS NULL OR a.CityId IS NULL
		)
		BEGIN
			SELECT @errorList = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + tp.Nit
					FROM @RevenueControlDetailCrossingList l
					JOIN Billing.RevenueControlDetail rcd ON l.RevenueControlDetailId = rcd.Id
					JOIN Common.ThirdParty tp ON rcd.ThirdPartyId = tp.Id
					LEFT JOIN Common.Address a ON tp.PersonId = a.IdPerson
					WHERE a.Id IS NULL OR a.DepartmentId IS NULL OR a.CityId IS NULL
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			
			set @ResultMessage = 'Los siguientes terceros no tienen parametrizada una dirección válida: ' + CHAR(13) + CHAR(10) + @errorList
			Return
		END
		
		Declare @IsTotalLiquidate Bit = 0,
				@CountFoliosWithOutLiquidate Int

		Select @CountFoliosWithOutLiquidate = Count(1) 
		From Billing.RevenueControlDetail rcd With(Nolock)
		Where rcd.RevenueControlId = (Select Top 1 RevenueControlId From @RevenueControlDetailCrossingList) 
			And (rcd.[Status] = @Uno Or rcd.[Status] = @Tres)

		If @CountFoliosWithOutLiquidate > 0 And @CountFoliosWithOutLiquidate = (Select Count(1) From @RevenueControlDetailCrossingList)
			Set @IsTotalLiquidate= 1
		Else
			Set @IsTotalLiquidate = 0
		/**/

		Declare @closeAdmission Bit = 0

		Declare @invoiceResultTmp Table
		(
			--RowId Int Identity(1,1) Primary Key,
			StatusResult Bit Null,
			MessageResult Varchar(Max) Null,
			[Message] Varchar(Max) Null,
			InvoiceId Int,
			InvoiceNumber Varchar(20)
		)

		Delete From @invoiceResultTmp
		Declare @Rows Int = 1, 
				@RowId Int = 0
		
		Declare @RevenueControlDetailId Int,
				@TotalPatientDiscount Decimal(18, 2),
				@OutputDate DateTime,
				@IsCutAccount Bit,
				@OutputDiagnosis Varchar(10),
				@InitialDate DateTime,
				@ListPortfolioAdvanceXml Xml,
				@CutType Int

		While @Rows > 0
		Begin
			Select Top 1 @RowId = RowId,
				@RevenueControlDetailId = RevenueControlDetailId,
				@TotalPatientDiscount = TotalPatientDiscount,
				@OutputDate = OutputDate,
				@IsCutAccount = IsCutAccount,
				@OutputDiagnosis = OutputDiagnosis,
				@InitialDate = InitialDate,
				@ListPortfolioAdvanceXml = ListPortfolioAdvanceCrossing,
				@CutType = CutType
			From @RevenueControlDetailCrossingList 
			Where RowId > @RowId 
			Order By RowId

			Set @Rows = @@RowCount
			If @Rows = 0 
				Break

			If @IsTotalLiquidate = 1 And @closeAdmission = 0 
			Begin
				Set @IsTotalLiquidate = 0
				Set @closeAdmission = 1
			End
			Else
				Set @closeAdmission = 0

			Declare @StatusResultInvoice Bit,
					@MessageResultInvoice Varchar(Max),
					@MessageInvoice Varchar(Max),
					@InvoiceId Int,
					@InvoiceNumber Varchar(20)
			
			Exec [Billing].[CreateInvoice] @RevenueControlDetailId, 
										   @BillingAuthorizationId, 
										   @PatientCode,
										   @AdmissionNumber, 
										   @ContainerCrystal, 
										   @TotalPatientDiscount, 
										   @closeAdmission, 
										   @OutputDate,
										   @IsCutAccount, 
										   @OutputDiagnosis, 
										   @InitialDate, 
										   @CutType, 
										   @CompanyType, 
										   @ThirdPartyPatientId,
										   @OperativeUnitId, 
										   @UserCode, 
										   @ListPortfolioAdvanceXml,
										   --Salidas
										   @StatusResultInvoice Output, 
										   @MessageResultInvoice Output,
										   @MessageInvoice Output, 
										   @InvoiceId Output, 
										   @InvoiceNumber Output
			
			print @StatusResultInvoice
			If @StatusResultInvoice = 0 
			Begin
				Set @errorList += @MessageInvoice + Char(13) + Char(10)
			End
			Else 
			Begin
				Set @consecutiveList += 'El Folio se liquidó correctamente generando: ' + Char(13) + Char(10) + @MessageResultInvoice + '@' + Char(13) + Char(10)
				
				Set @messageNotification += @MessageInvoice + Char(13) + Char(10)
				Insert Into @invoiceResultTmp (InvoiceId, InvoiceNumber) Values (@InvoiceId, @InvoiceNumber)
			End
		End
		
		SELECT @ResultXml = CONVERT
		(
			XML, 
			(
				SELECT 
					InvoiceId,
					InvoiceNumber
				FROM @invoiceResultTmp AS Data
				For xml AUTO,TYPE, ELEMENTS
			)
		)
		
		If @errorList <> '' 
		Begin		
			If Exists (Select 1 From @invoiceResultTmp) Begin
				Update @invoiceResultTmp Set StatusResult = 0, [Message] = @errorList				
				Select 
					@ResultStatus  = StatusResult,
					@ResultMessageInvoice = MessageResult,
					@ResultMessage = Message
				From @invoiceResultTmp
			End
			Else Begin
				set @ResultStatus  =  Convert(Bit, 0)
				set @ResultMessageInvoice = ''
				set @ResultMessage = @errorList
				set @ResultXml = ''
			End
			Return
		End
				
		Update @invoiceResultTmp 
			Set StatusResult = 1, 
				[Message] = @messageNotification, 
				MessageResult = @consecutiveList

		Select 
			@ResultStatus  = StatusResult,
			@ResultMessageInvoice = MessageResult,
			@ResultMessage = Message
		From @invoiceResultTmp

		return
	End Try
	Begin Catch
		print 'fallo'
		set @ResultStatus  =  Convert(Bit, 0)
		set @ResultMessageInvoice = ''
		set @ResultMessage = Error_Message()
		set @ResultXml = ''

		return
	End Catch	
End
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de liquidación de folios de facturación para un paciente y su ingreso (admisión) específicos. Valida que existan los parámetros obligatorios (cédula del paciente, número de ingreso, unidad operativa, autorización de facturación, tercero pagador y usuario auditor), verifica la configuración de facturación por unidad operativa en SettingsBilling, confirma que el paciente coincida con el ingreso en ADINGRESO, y comprueba que todos los ítems facturados en las órdenes de servicio (ServiceOrder, ServiceOrderDetail) tengan su distribución financiera correspondiente (ServiceOrderDetailDistribution) antes de proceder. Procesa una lista de folios enviada en formato XML (RevenueControlDetailCrossingListXml), liquida cada folio aplicando cruces con anticipos, descuentos al paciente y cortes de cuenta, y retorna el estado del proceso, mensajes de error o éxito y el resultado en XML; es el punto central del cierre económico de la cuenta de un paciente en el módulo de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'LiquidateFolio_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'LiquidateFolio_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Liquida los folios de facturación de un ingreso recorriendo cada detalle de control de ingresos enviado por XML, validando datos maestros y delegando la creación de cada factura, devolviendo el consolidado de éxitos y errores.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'LiquidateFolio_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@PatientCode, @AdmissionNumber, @ContainerCrystal y @UserCode no pueden ser nulos ni vacíos; @BillingAuthorizationId, @OperativeUnitId y @ThirdPartyPatientId deben ser distintos de 0; @RevenueControlDetailCrossingListXml no puede ser NULL (debe contener folios); Debe existir registro en Billing.SettingsBilling para la unidad operativa indicada; En Billing.SettingsBilling el ParticularHealthAdministratorId no puede ser NULL para la unidad operativa; El paciente (IPCODPACI) debe coincidir con el del ingreso (NUMINGRES) en ADINGRESO; No deben existir detalles de orden de servicio activos (Status=1, Packaging=0, IsDelete=0, InvoicedQuantity>0) sin distribución (ServiceOrderDetailDistribution); Para folios cuyo CareGroupType<>3, el ThirdPartyId del RevenueControlDetail debe coincidir con el ThirdPartyId de la HealthAdministrator; Los terceros (ThirdParty) de los folios deben tener al menos una Address con DepartmentId y CityId no nulos', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'LiquidateFolio_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'closeAdmission solo puede ser 1 en una única iteración del bucle (la primera) cuando se está liquidando la totalidad de folios pendientes del RevenueControl; Si hay errores parciales pero también facturas creadas, el resultado final marca StatusResult=0 conservando los InvoiceId generados; Cualquier excepción no controlada se captura y se devuelve ResultStatus=0 con el Error_Message(), limpiando ResultXml y ResultMessageInvoice; Solo se consideran como pendientes de liquidar los RevenueControlDetail con Status=1 o Status=3; Las fechas OutputDate e InitialDate del XML se interpretan en formato 103 (dd/mm/yyyy)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'LiquidateFolio_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Liquidación de folios; Facturación; Autorización de facturación; Unidad operativa; Folio / Control de ingresos (RevenueControl); Tercero pagador / Entidad administradora (EPS); Grupo de atención (CareGroup); Paciente particular; Cuenta de cobro / cierre de admisión; Diagnóstico de salida; Descuento al paciente (copago/cuota moderadora); Cruce de cartera / anticipos (PortfolioAdvanceCrossing); Corte de cuenta', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'LiquidateFolio_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @invoiceResultTmp: Por cada folio cuyo CreateInvoice retorna StatusResult=1, se inserta el InvoiceId e InvoiceNumber generado; [INSERT] Billing.Invoice: Indirectamente vía EXEC Billing.CreateInvoice por cada RevenueControlDetail del XML, marcando closeAdmission=1 únicamente en la primera iteración cuando todos los folios pendientes (Status=1 o 3) del RevenueControl están siendo liquidados; [RETURN_RESULT] @ResultXml: Devuelve XML con InvoiceId/InvoiceNumber de las facturas creadas exitosamente; [RETURN_RESULT] @ResultMessage: Si @errorList no está vacío, retorna el listado de errores acumulados; si todo fue exitoso, retorna @messageNotification con los mensajes de notificación; [RETURN_RESULT] @ResultMessageInvoice: Si hubo facturas creadas, retorna @consecutiveList con los consecutivos generados precedidos por ''El Folio se liquidó correctamente generando:''', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'LiquidateFolio_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @CountFoliosWithOutLiquidate > 0 y es igual al total de folios en el XML (todos los folios pendientes Status=1 o 3 del RevenueControl están siendo liquidados) → Se marca @IsTotalLiquidate=1 y en la primera iteración del while se envía closeAdmission=1 a CreateInvoice (cierra la admisión) else closeAdmission=0 en todas las iteraciones (liquidación parcial); si Existe folio con CareGroupType<>3 cuyo ThirdPartyId no coincide con el ThirdPartyId de la HealthAdministrator → Aborta con mensaje ''El tercero del folio no corresponde con el tercero de la entidad administradora''; si Algún tercero del folio no tiene dirección con departamento y ciudad parametrizados → Aborta listando los NIT de los terceros sin dirección válida; si CreateInvoice retorna StatusResult=0 para algún folio → Acumula el mensaje en @errorList y al final, si hubo facturas creadas, actualiza @invoiceResultTmp con StatusResult=0 y el listado de errores else Acumula consecutivo en @consecutiveList y notificación en @messageNotification e inserta el InvoiceId/InvoiceNumber en @invoiceResultTmp; si Existen detalles de orden de servicio (Status=1, Packaging=0, IsDelete=0, InvoicedQuantity>0) sin registro en ServiceOrderDetailDistribution → Aborta con mensaje ''Existen detalles de la admisión sin su distribución''', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'LiquidateFolio_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Billing.CreateInvoice', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'LiquidateFolio_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.SettingsBilling; Billing.ServiceOrder; Billing.ServiceOrderDetail; Billing.ServiceOrderDetailDistribution; Billing.RevenueControlDetail; Contract.CareGroup; Contract.HealthAdministrator; Common.ThirdParty; Common.Address; ADINGRESO', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'LiquidateFolio_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'LiquidateFolio_Output';
-- GO
