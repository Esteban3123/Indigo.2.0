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
		
		-- Validar SettingsBilling en una sola consulta
		Declare @SettingsExists Bit = 0, @ParticularHAConfigured Bit = 0
		Select @SettingsExists = 1, 
			   @ParticularHAConfigured = Case When ParticularHealthAdministratorId Is Not Null Then 1 Else 0 End
		From Billing.SettingsBilling With(Nolock) 
		Where IdOperatingUnit = @OperativeUnitId
		
		If @SettingsExists = 0 Begin
			set @ResultMessage = 'No se encontraron parámetros de Facturación para la unidad operativa seleccionada'
			Return
		End
		If @ParticularHAConfigured = 0 Begin
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
		If not exists (select 1 from .ADINGRESO ai With(Nolock) where ai.NUMINGRES = @AdmissionNumber and ai.IPCODPACI = @PatientCode) Begin
			set @ResultMessage = 'El paciente no coincide con el del ingreso'
			Return
		End

		-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		IF EXISTS 
		(
			SELECT 1
			FROM Billing.ServiceOrder AS so With(Nolock)
			JOIN Billing.ServiceOrderDetail AS sod With(Nolock) ON so.Id = sod.ServiceOrderId
			LEFT JOIN Billing.ServiceOrderDetailDistribution sodd With(Nolock) ON sod.Id = sodd.ServiceOrderDetailId
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
			JOIN Billing.RevenueControlDetail rcd With(Nolock) ON l.RevenueControlDetailId = rcd.Id
			JOIN Contract.CareGroup cg With(Nolock) ON rcd.CareGroupId = cg.Id
			JOIN Contract.HealthAdministrator ha With(Nolock) ON rcd.HealthAdministratorId = ha.Id
			WHERE cg.CareGroupType <> 3
				AND rcd.ThirdPartyId <> ha.ThirdPartyId
		)
		BEGIN
			set @ResultMessage = 'El tercero del folio no corresponde con el tercero de la entidad administradora.'
			Return
		END
		
		-- Validar direcciones en una sola consulta
		SELECT @errorList = STUFF((
				SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + tp.Nit
				FROM @RevenueControlDetailCrossingList l
				JOIN Billing.RevenueControlDetail rcd With(Nolock) ON l.RevenueControlDetailId = rcd.Id
				JOIN Common.ThirdParty tp With(Nolock) ON rcd.ThirdPartyId = tp.Id
				LEFT JOIN Common.Address a With(Nolock) ON tp.PersonId = a.IdPerson
				WHERE a.Id IS NULL OR a.DepartmentId IS NULL OR a.CityId IS NULL
				FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
		
		IF @errorList IS NOT NULL
		BEGIN
			set @ResultMessage = 'Los siguientes terceros no tienen parametrizada una dirección válida: ' + CHAR(13) + CHAR(10) + @errorList
			Return
		END
		
		-- Precalcular valores de @RevenueControlDetailCrossingList para evitar subconsultas repetidas
		Declare @RevenueControlId Int,
				@TotalFoliosToLiquidate Int
		
		Select Top 1 @RevenueControlId = RevenueControlId From @RevenueControlDetailCrossingList
		Select @TotalFoliosToLiquidate = Count(1) From @RevenueControlDetailCrossingList
		
		Declare @IsTotalLiquidate Bit = 0,
				@CountFoliosWithOutLiquidate Int

		Select @CountFoliosWithOutLiquidate = Count(1) 
		From Billing.RevenueControlDetail rcd With(Nolock)
		Where rcd.RevenueControlId = @RevenueControlId
			And (rcd.[Status] = @Uno Or rcd.[Status] = @Tres)

		If @CountFoliosWithOutLiquidate > 0 And @CountFoliosWithOutLiquidate = @TotalFoliosToLiquidate
			Set @IsTotalLiquidate= 1
		Else
			Set @IsTotalLiquidate = 0

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

		-- Variables de salida del SP (declaradas fuera del loop para eficiencia)
		Declare @StatusResultInvoice Bit,
				@MessageResultInvoice Varchar(Max),
				@MessageInvoice Varchar(Max),
				@InvoiceId Int,
				@InvoiceNumber Varchar(20)

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
		set @ResultStatus  =  Convert(Bit, 0)
		set @ResultMessageInvoice = ''
		set @ResultMessage = 'Error en línea ' + Cast(Error_Line() As Varchar(10)) + ': ' + Error_Message()
		set @ResultXml = ''

		return
	End Catch	
End