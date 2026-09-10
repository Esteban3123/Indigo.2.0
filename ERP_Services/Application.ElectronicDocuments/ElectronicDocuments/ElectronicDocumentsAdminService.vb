Imports System.Data.SqlClient
Imports System.IO
Imports System.IO.Compression
Imports System.Net
Imports System.Text
Imports System.Threading
Imports System.Xml.Linq
Imports DevExpress.XtraPrinting.Preview
Imports DistributedServices.DIAN.WcfDianCustomerServices
Imports Domain.Base.Entities
Imports Domain.ElectronicDocuments.Entities.UBL2_1.maindoc
Imports Domain.ElectronicDocuments.Service
Imports Domain.Entities
Imports Domain.Notification
Imports Domain.Security
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.AzureBlobStorage
Imports Infrastructure.CrossCutting.AzureBlobStorage.Factory
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Root
Imports NewRelic.Api.Agent
Imports RestSharp
Imports Utils = Infrastructure.CrossCutting.Base.Utils

Public Class ElectronicDocumentsAdminService
    Implements IElectronicDocumentsAdminService

#Region "Fields"

    Private _billingAuthorizationRepository As IBillingAuthorizationRepository
    Private _billingNoteRepository As IBillingNoteRepository
    Private _electronicDocumentRepository As IElectronicDocumentRepository
    Private _electronicDocumentDetailRepository As IElectronicDocumentDetailRepository
    Private _electronicDocumentNotificationRepository As IElectronicDocumentNotificationRepository
    Private _invoiceRepository As IInvoiceRepository
    Private _operatingUnitRepository As IOperatingUnitRepository
    Private _thirdPartyRepository As IThirdPartyRepository
    Private _settingsAccountRepository As ISettingsAccountRepository
    Private _revenueControlDetailRepository As IRevenueControlDetailRepository
    Private _electronicsPropertiesRepository As IElectronicsPropertiesRepository
    Private _endpointsRepository As IEndpointsRepository
    Private _endPoints As Endpoints
    Private ReadOnly _factoryStorage As IFactoryStorage
    Private ReadOnly _storage As IStorage
    Private _invoiceCopayRepository As IInvoiceCopayRepository
#End Region

#Region "Properties"

    Private ReadOnly statusCodeErrors As String() =
    {
        "500",
        "503"
    }

    ''' <summary>
    ''' Constante codigo para identificar la url de RCM
    ''' </summary>
    Private Const Revenue_Cycle = "revenue-cycle"

    ''' <summary>
    ''' Propiedad lectura de la entidad enpoint
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property Endpoint As Endpoints
        Get
            If _endPoints Is Nothing Then
                _endPoints = _endpointsRepository.GetEndpointsByContainerCode(ServerSessionValues.Current.CurrentContainer, Revenue_Cycle)
            End If
            Return _endPoints
        End Get
    End Property

    ''' <summary>
    ''' constante que establece el codigo de envio inicial desde el servicio DIAN
    ''' </summary>
    Private Const MESSAGE_CODE_ERIPS As String = "00"
    ''' <summary>
    ''' Constante que establece el mensaje inicial desde el servicio DIAN
    ''' </summary>
    Private Const MESSAGE_ERIPS As String = "Mensaje Disparado desde servicio DIAN"

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(billingAuthorizationRepository As IBillingAuthorizationRepository,
                   billingNoteRepository As IBillingNoteRepository,
                   electronicDocumentRepository As IElectronicDocumentRepository,
                   electronicDocumentDetailRepository As IElectronicDocumentDetailRepository,
                   electronicDocumentNotificationRepository As IElectronicDocumentNotificationRepository,
                   invoiceRepository As IInvoiceRepository,
                   operatingUnitRepository As IOperatingUnitRepository,
                   thirdPartyRepository As IThirdPartyRepository,
                   settingsAccountRepository As ISettingsAccountRepository,
                   revenueControlDetailRepository As IRevenueControlDetailRepository,
                   electronicsPropertiesRepository As IElectronicsPropertiesRepository,
                   endpointsRepository As IEndpointsRepository,
                   factoryStorage As IFactoryStorage,
                   invoiceCopayRepository As IInvoiceCopayRepository)
        Me._billingAuthorizationRepository = billingAuthorizationRepository
        Me._billingNoteRepository = billingNoteRepository
        Me._electronicDocumentRepository = electronicDocumentRepository
        Me._electronicDocumentDetailRepository = electronicDocumentDetailRepository
        Me._electronicDocumentNotificationRepository = electronicDocumentNotificationRepository
        Me._invoiceRepository = invoiceRepository
        Me._operatingUnitRepository = operatingUnitRepository
        Me._thirdPartyRepository = thirdPartyRepository
        Me._settingsAccountRepository = settingsAccountRepository
        Me._revenueControlDetailRepository = revenueControlDetailRepository
        Me._electronicsPropertiesRepository = electronicsPropertiesRepository
        Me._endpointsRepository = endpointsRepository
        Me._factoryStorage = factoryStorage
        Me._storage = Me._factoryStorage.CreateStorageControl()
        Me._invoiceCopayRepository = invoiceCopayRepository
        ' Setting TLS 1.2 protocol '
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        ServicePointManager.ServerCertificateValidationCallback = Function(sender1, certificate, chain, sslPolicyErrors)
                                                                      Return True
                                                                  End Function
    End Sub

#End Region

#Region "Methods"

    Public Function GetBillingAuthorizationResolution(operatingUnitId As Integer, billingAuthorization As BillingAuthorization) As ActionResult(Of BillingAuthorization) Implements IElectronicDocumentsAdminService.GetBillingAuthorizationResolution
        Try
            Dim settingsAccount = _settingsAccountRepository.GetSettingAccountSimple(operatingUnitId)
            Dim supplierThirdParty = _thirdPartyRepository.GetThirdPartyById(settingsAccount.IdDian, False)

            Using client As New DistributedServices.DIAN.ServiceClient(settingsAccount.GetElectronicDocumentUrl(), settingsAccount.DigitalCertificate, settingsAccount.DigitalCertificateKey, _storage)
                Dim response = client.GetNumberingRange(supplierThirdParty.Nit, settingsAccount.SoftwareIdentifier)
                If response.StateResult Then
                    Dim documentResponse = response.ObjectEmbbeded
                    If documentResponse IsNot Nothing AndAlso documentResponse.ResponseList IsNot Nothing AndAlso documentResponse.ResponseList.Any Then
                        Dim range = documentResponse.ResponseList.FirstOrDefault(Function(d) d.ResolutionNumber = billingAuthorization.ResolutionNumber AndAlso d.Prefix = billingAuthorization.InvoicePrefix)
                        If range IsNot Nothing Then
                            billingAuthorization.ResolutionDate = range.ResolutionDate
                            billingAuthorization.InitialDate = range.ValidDateFrom
                            billingAuthorization.FinalDate = range.ValidDateTo
                            billingAuthorization.InitialInvoice = range.FromNumber
                            billingAuthorization.FinalInvoice = range.ToNumber
                            billingAuthorization.InvoicePrefix = range.Prefix
                            billingAuthorization.TechnicalKey = range.TechnicalKey
                        Else
                            Return New ActionResult(Of BillingAuthorization) With {.StateResult = False, .Message = "No se encontró el prefijo asociado al número de resolución"}
                        End If
                    Else
                        Return New ActionResult(Of BillingAuthorization) With {.StateResult = False, .Message = documentResponse.OperationDescription}
                    End If
                Else
                    Return New ActionResult(Of BillingAuthorization) With {.StateResult = False, .Message = response.Message}
                End If

                Return New ActionResult(Of BillingAuthorization) With {.StateResult = True, .ObjectEmbbeded = billingAuthorization}
            End Using
        Catch ex As Exception
            Return New ActionResult(Of BillingAuthorization) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Async Function ExecuteProcess() As Task(Of ActionResult(Of String)) Implements IElectronicDocumentsAdminService.ExecuteProcess
        Try
            Return Await ExecuteProcessAsync({0, 1, 2, 4, 66, 88})
        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Async Function ExecuteSendMailProcess() As Task(Of ActionResult(Of String)) Implements IElectronicDocumentsAdminService.ExecuteSendMailProcess
        Try
            Return Await ExecuteSendMailProcessAsync()
        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

#End Region

#Region "Private Methods"

    Private Async Function ExecuteProcessAsync(status As Integer()) As Task(Of ActionResult(Of String))
        'Diccionarios
        Dim dictionarySettingsAccount As New Dictionary(Of Integer, GeneralLedgerSettings)()
        Dim dictionarySupplierThirdParty As New Dictionary(Of Integer, ThirdParty)()
        Dim dictionaryCustomerThirdParty As New Dictionary(Of Integer, ThirdParty)()
        Dim errors As New StringBuilder
        'Entidades
        Dim settingsAccount As GeneralLedgerSettings
        Dim supplierThirdParty As ThirdParty
        Dim customerThirdParty As ThirdParty

        'A partir de la fecha de creacion se procede a validar cuando se puede realizar el reintento
        'Si el estado es erroneo, es porque se esta presentando un error, se debe dar tiempo para que lo corrijan, al menos media hora
        Dim listElectronicDocuments = _electronicDocumentRepository.GetListElectronicDocumentIdsByStatus(status).Where(Function(d) d.Status = 66 OrElse DateTime.Now >= DateAdd(DateInterval.Minute, Utils.GetMinutesToAdd(d.Retry + If(d.Status = 0, 5, 0)), d.CreationDate)).ToList()
        For Each electronicDocument In listElectronicDocuments
            If status.Contains(electronicDocument.Status) Then

                'Si es un estado erroneo o invalido cambia a estado registrado
                If {0, 4, 66, 88}.Contains(electronicDocument.Status) Then
                    electronicDocument.Status = 1
                End If

                'UnitWorks
                Dim electronicDocumentUnitWork = _electronicDocumentRepository.UnitWork
                Dim electronicDocumentDetailUnitWork = _electronicDocumentDetailRepository.UnitWork
                Dim electronicDocumentNotificationUnitWork = _electronicDocumentNotificationRepository.UnitWork

                Dim currentStatus = IIf({88, 66}.Contains(electronicDocument.Status), 1, IIf(electronicDocument.Status = 99, 2, electronicDocument.Status))
                electronicDocument.Retry = electronicDocument.Retry + 1
                electronicDocument.Status = IIf(electronicDocument.Status = 1, 88, IIf(electronicDocument.Status = 2, 99, electronicDocument.Status)) 'En proceso
                electronicDocumentUnitWork.Commit()

                Try
                    'Cargamos parametros de contabilidad para la unidad operativa
                    If Not dictionarySettingsAccount.ContainsKey(electronicDocument.OperatingUnitId) Then
                        settingsAccount = _settingsAccountRepository.GetSettingAccountSimple(electronicDocument.OperatingUnitId)
                        dictionarySettingsAccount.Add(electronicDocument.OperatingUnitId, settingsAccount)
                    Else
                        settingsAccount = dictionarySettingsAccount(electronicDocument.OperatingUnitId)
                    End If

                    'Validamos la versión de facturación electrónica
                    If electronicDocument.DianVersion <> settingsAccount.DianVersion Then
                        currentStatus = 77
                        Throw New Exception(String.Format("La versión actual de facturación electrónica es: {0}", settingsAccount.DianVersion))
                    End If

                    'Cargamos el proveedor
                    If Not dictionarySupplierThirdParty.ContainsKey(settingsAccount.IdDian) Then
                        supplierThirdParty = _thirdPartyRepository.GetThirdPartyById(settingsAccount.IdDian, False)
                        dictionarySupplierThirdParty.Add(settingsAccount.IdDian, supplierThirdParty)
                    Else
                        supplierThirdParty = dictionarySupplierThirdParty(settingsAccount.IdDian)
                    End If

                    'Cargamos el cliente
                    If Not dictionaryCustomerThirdParty.ContainsKey(electronicDocument.CustomerPartyId) Then
                        customerThirdParty = _thirdPartyRepository.GetThirdPartyById(electronicDocument.CustomerPartyId, False)
                        dictionaryCustomerThirdParty.Add(electronicDocument.CustomerPartyId, customerThirdParty)
                    Else
                        customerThirdParty = dictionaryCustomerThirdParty(electronicDocument.CustomerPartyId)
                    End If

                    'Si esta en estado registrado, generamos XML y Enviamos a la DIAN
                    If currentStatus = 1 Then
                        'Validamos la factura antes de realizar el envío
                        currentStatus = Await Me.ValidateDIAN(electronicDocument, settingsAccount, supplierThirdParty, customerThirdParty, currentStatus, True)
                        If currentStatus <> 3 Then
                            'Generamos XML
                            Dim response = Me.GenerateXML(electronicDocument, settingsAccount, supplierThirdParty, customerThirdParty)
                            If response.StateResult Then
                                currentStatus = Me.SendToDIAN(settingsAccount, supplierThirdParty, customerThirdParty, electronicDocument, response.ObjectEmbbeded, currentStatus)
                            Else
                                currentStatus = IIf(response.StateResultAux, currentStatus, 0)
                                'La factura no es valida, deben revisarse los detalles
                                _electronicDocumentDetailRepository.SaveEntity(New ElectronicDocumentDetail With
                                {
                                    .ElectronicDocumentId = electronicDocument.Id,
                                    .Destination = 0, 'Validate
                                    .CreationDate = DateTime.Now,
                                    .Status = False,
                                    .Response = Enums.ElectronicDocuments.StatusCode.BadRequest,
                                    .Comments = "Errores de validacion",
                                    .ResponseData = response.Message
                                })
                            End If
                        End If
                    End If

                    'Si ya se envio a la DIAN pero no se ha validado
                    If currentStatus = 2 Then
                        'Validamos el envio realizado a la DIAN
                        currentStatus = Await Me.ValidateDIAN(electronicDocument, settingsAccount, supplierThirdParty, customerThirdParty, currentStatus)
                    End If
                Catch ex As Exception
                    'Si ocurre un error en el proceso almacenamos el error
                    _electronicDocumentDetailRepository.SaveEntity(New ElectronicDocumentDetail With
                    {
                        .ElectronicDocumentId = electronicDocument.Id,
                        .Destination = 0, 'Error en el proceso
                        .CreationDate = DateTime.Now,
                        .Status = False,
                        .Response = Enums.ElectronicDocuments.StatusCode.NotImplemented,
                        .Comments = "Error ejecutando el proceso",
                        .ResponseData = Utils.GetInnerExceptionMessageToString(ex)
                    })

                    errors.AppendLine(Utils.GetInnerExceptionMessageToString(ex))
                End Try

                electronicDocument.Status = currentStatus
                _electronicDocumentRepository.SaveEntity(electronicDocument)

                electronicDocumentUnitWork.Commit()
                electronicDocumentDetailUnitWork.Commit()
                electronicDocumentNotificationUnitWork.Commit()
            End If
        Next

        Return New ActionResult(Of String) With {.StateResult = (errors.Length = 0), .Message = errors.ToString()}
    End Function

    Private Async Function ExecuteSendMailProcessAsync() As Task(Of ActionResult(Of String))
        Dim errors As New StringBuilder

        Dim electronicDocumentNotificationIds = _electronicDocumentNotificationRepository.GetElectronicDocumentNotificationIdsByStatus(False)

        For Each electronicDocumentNotificationId In electronicDocumentNotificationIds
            Dim message = Await SendMailToClientAsync(electronicDocumentNotificationId)
            If message IsNot Nothing Then
                errors.Append(message)
            End If
        Next

        Return New ActionResult(Of String) With {.StateResult = (errors.Length = 0), .Message = errors.ToString()}
    End Function

    <Transaction>
    Private Async Function SendMailToClientAsync(electronicDocumentNotificationId As Integer) As Task(Of String)

        'entorno para monitoreo
        Dim apm As ApmHandler = New NewRelicAPM()

        'Diccionarios
        Dim dictionarySettingsAccount As New Dictionary(Of Integer, GeneralLedgerSettings)()
        Dim dictionarySupplierThirdParty As New Dictionary(Of Integer, ThirdParty)()
        Dim dictionaryCustomerThirdParty As New Dictionary(Of Integer, ThirdParty)()

        'Entidades
        Dim settingsAccount As GeneralLedgerSettings
        Dim supplierThirdParty As ThirdParty
        Dim customerThirdParty As ThirdParty


        Dim electronicDocumentNotification = _electronicDocumentNotificationRepository.GetElectronicDocumentNotificationById(electronicDocumentNotificationId)
        Dim electronicDocument = electronicDocumentNotification.ElectronicDocument

        If Not electronicDocumentNotification.Status Then
            'UnitWorks
            Dim electronicDocumentDetailUnitWork = _electronicDocumentDetailRepository.UnitWork
            Dim electronicDocumentNotificationUnitWork = _electronicDocumentNotificationRepository.UnitWork

            Try
                'Cargamos parametros de contabilidad para la unidad operativa
                If Not dictionarySettingsAccount.ContainsKey(electronicDocument.OperatingUnitId) Then
                    settingsAccount = _settingsAccountRepository.GetSettingAccountSimple(electronicDocument.OperatingUnitId)
                    dictionarySettingsAccount.Add(electronicDocument.OperatingUnitId, settingsAccount)
                Else
                    settingsAccount = dictionarySettingsAccount(electronicDocument.OperatingUnitId)
                End If

                'Cargamos el proveedor
                If Not dictionarySupplierThirdParty.ContainsKey(settingsAccount.IdDian) Then
                    supplierThirdParty = _thirdPartyRepository.GetThirdPartyById(settingsAccount.IdDian, False)
                    dictionarySupplierThirdParty.Add(settingsAccount.IdDian, supplierThirdParty)
                Else
                    supplierThirdParty = dictionarySupplierThirdParty(settingsAccount.IdDian)
                End If

                'Cargamos el cliente
                If Not dictionaryCustomerThirdParty.ContainsKey(electronicDocument.CustomerPartyId) Then
                    customerThirdParty = _thirdPartyRepository.GetThirdPartyById(electronicDocument.CustomerPartyId, False)
                    dictionaryCustomerThirdParty.Add(electronicDocument.CustomerPartyId, customerThirdParty)
                Else
                    customerThirdParty = dictionaryCustomerThirdParty(electronicDocument.CustomerPartyId)
                End If

                ' ---------------------------------- Atributos para monitoreo
                apm.AddCustomAttribute("database", electronicDocument.Container)
                apm.AddCustomAttribute("company", String.Format("{0} - {1}", supplierThirdParty.Nit, supplierThirdParty.Name))
                apm.AddCustomAttribute("entityName", electronicDocument.EntityName)
                apm.AddCustomAttribute("documentNumber", String.Format("{0}{1}", electronicDocument.Prefix, electronicDocument.DocumentNumber))
                apm.AddCustomAttribute("state", electronicDocument.Status)
                If electronicDocument.getDocumentType() = TypeElectronicDocument.Invoice Then
                    Dim invoice As Invoice = _invoiceRepository.GetInvoiceById(electronicDocument.EntityId)
                    If invoice.RevenueControlDetailId IsNot Nothing AndAlso invoice.RevenueControlDetailId > 0 Then
                        electronicDocument.printMode = _revenueControlDetailRepository.GetPrintgModeByIdRevenueControl(invoice.RevenueControlDetailId)
                    End If
                    apm.AddCustomAttribute("value", invoice.TotalValue)
                    apm.AddCustomAttribute("typeInvoice", invoice.getDocumentTypeName())
                    apm.AddCustomAttribute("typeDocument", electronicDocument.EntityName)
                End If
                ' ----------------------------------

                Dim fileNameElectronicDocument = electronicDocument.GetFileName(supplierThirdParty, electronicDocument.getDocumentType())
                Dim fileNameApplicationResponse = electronicDocument.GetFileName(supplierThirdParty, TypeElectronicDocument.ApplicationResponse)
                Dim fileNameAttachedDocument = electronicDocument.GetFileName(supplierThirdParty, TypeElectronicDocument.AttachedDocument)
                Dim fileNamePDF = String.Format("{0}.pdf", electronicDocument.GetDocumentNumber())
                Dim fileNameZip = String.Format("{0}.zip", electronicDocument.GetDocumentNumber())

                'Validamos que exista el documento electronico
                If Me._storage.ValidateIfNotExists(electronicDocument.FilePath, fileNameElectronicDocument) Then
                    'Obtenemos el documento de la DIAN
                    Using client As New DistributedServices.DIAN.ServiceClient(settingsAccount.GetElectronicDocumentUrl(), settingsAccount.DigitalCertificate, settingsAccount.DigitalCertificateKey, _storage)
                        If Not client.GetElectronicDocument(electronicDocument.CUFE, electronicDocument.FilePath, fileNameElectronicDocument) Then
                            apm.NoticeError(String.Format("No se encontro el documento con CUFE {0} en los servicios de la DIAN", electronicDocument.CUFE))
                            Return Nothing
                        End If
                    End Using
                End If

                'Validamos si el ApplicationResponse ya esta creado
                If Me._storage.ValidateIfNotExists(electronicDocument.FilePath, fileNameApplicationResponse) Then
                    'Validamos el envio realizado a la DIAN
                    Dim electronictDocumentStatus = Await Me.ValidateDIAN(electronicDocument, settingsAccount, supplierThirdParty, customerThirdParty, 4, True)
                    If electronictDocumentStatus <> 3 Then
                        apm.NoticeError(String.Format("El documento con CUFE {0} no se encuentra validado", electronicDocument.CUFE))
                        Return Nothing
                    End If
                End If

                'Generamos El AttachedDocument
                Dim ubl = New DIAN.UBL2_1.UBL2_1(customerThirdParty, Infrastructure.CrossCutting.Root.TypeElectronicDocument.AttachedDocument, settingsAccount, supplierThirdParty, _storage)
                ubl.ElectronicDocument = electronicDocument
                Dim response = ubl.GenerateXML()
                If Not response.StateResult Then
                    'No se generó el AttachedDocument, deben revisarse los detalles
                    _electronicDocumentDetailRepository.SaveEntity(New ElectronicDocumentDetail With
                            {
                                .ElectronicDocumentId = electronicDocument.Id,
                                .Destination = 3, 'Envío al Cliente
                                .CreationDate = DateTime.Now,
                                .Status = False,
                                .Response = Enums.ElectronicDocuments.StatusCode.BadRequest,
                                .Comments = "No se generó el AttachedDocument",
                                .ResponseData = response.Message
                            })

                    apm.NoticeError(response.Message)
                    Return Nothing
                End If

                'Valores de session
                SessionValues.Instance.IndigoCompanyNit = supplierThirdParty.Nit
                SessionValues.Instance.IndigoCompanyName = supplierThirdParty.Name

                'Validamos si existe un pdf ya creado
                Dim resultGeneratePDF = Me.GeneratePDF(electronicDocument, fileNamePDF)
                If Not resultGeneratePDF.StateResult Then
                    apm.NoticeError(resultGeneratePDF.Message)
                    Dim eventLog As New EventLog
                    eventLog.Source = "Indigo Vie"
                    eventLog.WriteEntry($"Error generando el PDF: {resultGeneratePDF.Message}", EventLogEntryType.Error)
                    Return resultGeneratePDF.Message
                End If

                'Lectura de los archivos para armar el zip
                Dim fileDictionary = New Dictionary(Of String, Byte())
                Dim AttachedDocument = _storage.ReadFile(electronicDocument.FilePath, fileNameAttachedDocument)
                Dim filePDF = _storage.ReadFile(electronicDocument.FilePath, fileNamePDF)
                _storage.DeleteFile(electronicDocument.FilePath, fileNameZip)
                fileDictionary.Add(fileNameAttachedDocument, AttachedDocument)
                fileDictionary.Add(fileNamePDF, filePDF)
                Dim newFileZipContent = Utils.CompressMultipleFileStream(fileDictionary, electronicDocument.FilePath)
                _storage.WriteFile(electronicDocument.FilePath, fileNameZip, newFileZipContent)

                'envio del correo
                Dim subject As String = String.Format("{0};{1};{2};{3};{1}", supplierThirdParty.Nit, supplierThirdParty.Name, String.Concat(electronicDocument.Prefix, electronicDocument.DocumentNumber), electronicDocument.getDocumentTypeCode())
                Dim body As String = String.Format(EmailService.GetFormat(), customerThirdParty.Nit, customerThirdParty.Name, supplierThirdParty.Nit, supplierThirdParty.Name, electronicDocument.DocumentDate.ToString("yyyy-MM-dd"), electronicDocument.getDocumentTypeName(), String.Concat(electronicDocument.Prefix, electronicDocument.DocumentNumber))
                Dim misByte As Byte() = Me._storage.ReadFile(electronicDocument.FilePath, fileNameZip)
                Dim message As New MessageNotification With
                    {
                        .From = Utils.GetAppSettingValueByKey("FromEmailNotification"),
                        .To = electronicDocumentNotification.Email,
                        .Subject = subject,
                        .Message = body,
                        .Attach = True,
                        .FileName = fileNameZip,
                        .File = Convert.ToBase64String(misByte)
                    }

                Using manager As New MessageManager(Utils.GetAppSettingValueByKey("FxGetUrlNotification"), Utils.GetAppSettingValueByKey("FxEmailNotification"))
                    response = Task.Run(Function() manager.Send(message)).Result
                    If response.StateResult Then
                        electronicDocumentNotification.Status = True
                        electronicDocumentNotification.ShippingDate = DateTime.Now
                        electronicDocumentNotification.ChangeTracker.State = ObjectState.Modified
                        _electronicDocumentNotificationRepository.SaveEntity(electronicDocumentNotification)
                    Else
                        'Si ocurre un error en el proceso almacenamos el error
                        _electronicDocumentDetailRepository.SaveEntity(New ElectronicDocumentDetail With
                            {
                                .ElectronicDocumentId = electronicDocumentNotification.ElectronicDocument.Id,
                                .Destination = 3, 'Envío de Email
                                .CreationDate = DateTime.Now,
                                .Status = False,
                                .Response = Enums.ElectronicDocuments.StatusCode.NotImplemented,
                                .Comments = "Error al enviar el correo electrónico",
                                .ResponseData = response.Message
                            })
                        apm.NoticeError(response.Message)
                    End If
                End Using
            Catch ex As Exception
                'Si ocurre un error en el proceso almacenamos el error
                _electronicDocumentDetailRepository.SaveEntity(New ElectronicDocumentDetail With
                    {
                        .ElectronicDocumentId = electronicDocumentNotification.ElectronicDocument.Id,
                        .Destination = 3, 'Envío de Email
                        .CreationDate = DateTime.Now,
                        .Status = False,
                        .Response = Enums.ElectronicDocuments.StatusCode.NotImplemented,
                        .Comments = "Error ejecutando el proceso de envío correo electrónico",
                        .ResponseData = Utils.GetInnerExceptionMessageToString(ex)
                    })
                apm.NoticeError(ex)
                Return Utils.GetInnerExceptionMessageToString(ex)
            End Try

            electronicDocumentDetailUnitWork.Commit()
            electronicDocumentNotificationUnitWork.Commit()
        End If
    End Function

    ''' <summary>
    ''' Iniciar con la contrucción del XML
    ''' </summary>
    ''' <param name="electronicDocument"></param>
    ''' <param name="settingsAccount"></param>
    ''' <param name="supplierThirdParty"></param>
    ''' <param name="customerThirdParty"></param>
    ''' <returns></returns>
    Private Function GenerateXML(electronicDocument As ElectronicDocument, settingsAccount As GeneralLedgerSettings, supplierThirdParty As ThirdParty, customerThirdParty As ThirdParty) As ActionResult(Of String)
        Try
            Dim invoice As Invoice = Nothing
            Dim billingAuthorization As BillingAuthorization = Nothing
            Dim paymentMethods As List(Of SP_GetPaymentMethodsByInvoiceId_Result) = Nothing
            Dim billingNote As BillingNote = Nothing

            Dim errors As New StringBuilder

            If supplierThirdParty.Person.IdentificationNumber <> supplierThirdParty.Nit Then
                errors.AppendLine("El numero de documento de tercero '" & supplierThirdParty.Nit & "' no corresponde con el numero de documento de la persona asociada '" & supplierThirdParty.Person.IdentificationNumber & "'.")
            End If

            If customerThirdParty.Person.IdentificationNumber <> customerThirdParty.Nit Then
                errors.AppendLine("El numero de documento de tercero '" & customerThirdParty.Nit & "' no corresponde con el numero de documento de la persona asociada '" & customerThirdParty.Person.IdentificationNumber & "'.")
            End If

            If (supplierThirdParty.Person.Address Is Nothing OrElse supplierThirdParty.Person.Address.Count = 0) Then
                errors.AppendLine("El tercero '" & supplierThirdParty.Person.IdentificationNumber & "' no tiene parametrizada una dirección.")
            ElseIf (supplierThirdParty.Person.Address.Where(Function(a) a.DepartmentId IsNot Nothing AndAlso a.CityId IsNot Nothing).Count = 0) Then
                errors.AppendLine("El tercero '" & supplierThirdParty.Person.IdentificationNumber & "' no tiene parametrizada una dirección con ciudad y departamento.")
            End If

            'Valido direcciones en caso de que lo requiera
            If settingsAccount.ValidateClientData AndAlso customerThirdParty.PersonType = 1 Then
                If (customerThirdParty.Person.Address Is Nothing OrElse customerThirdParty.Person.Address.Count = 0) Then
                    errors.AppendLine("El tercero '" & customerThirdParty.Person.IdentificationNumber & "' no tiene parametrizada una dirección.")
                ElseIf Not settingsAccount.ValidateClientData AndAlso customerThirdParty.PersonType = 0 AndAlso (customerThirdParty.Person.Address.Where(Function(a) a.DepartmentId IsNot Nothing AndAlso a.CityId IsNot Nothing).Count = 0) Then
                    errors.AppendLine("El tercero '" & customerThirdParty.Person.IdentificationNumber & "' no tiene parametrizada una dirección con ciudad y departamento.")
                End If
            End If

            If electronicDocument.getDocumentType() = Infrastructure.CrossCutting.Root.TypeElectronicDocument.Invoice Then
                invoice = _invoiceRepository.GetInvoiceById(electronicDocument.EntityId)
                If invoice Is Nothing OrElse invoice.Id = 0 Then
                    errors.AppendLine("La factura no fue encontrada")
                Else
                    invoice.InvoiceDetails = _invoiceRepository.GetInvoiceDetailsByInvoiceId(invoice.Id)
                    Dim invoiceCopay = _invoiceCopayRepository.GetInvoiceCopayInfo(invoice.Id)

                    'si existe info de copago se verifica que el tercero de la fact salud, tenga los datos minimos para relacionarlo a la fact copago
                    If invoiceCopay IsNot Nothing Then
                        Dim thirdPartyEAPB As ThirdParty = Nothing

                        If invoiceCopay?.ThirdPartyEAPB.Id > 0 Then
                            thirdPartyEAPB = _thirdPartyRepository.GetThirdPartyById(invoiceCopay.ThirdPartyEAPB.Id, False)
                        End If

                        If thirdPartyEAPB Is Nothing OrElse thirdPartyEAPB?.Person Is Nothing Then
                            errors.AppendLine("No hay información de la entidad person de la factura Salud para relacionarla en la factura Copago")
                        Else
                            invoiceCopay.ThirdPartyEAPB = thirdPartyEAPB
                            invoice.InvoiceCopayCustom = invoiceCopay
                        End If
                    End If

                    'Validamos que los datos de la factura sean consistentes
                    If (invoice.InvoiceDetails Is Nothing OrElse invoice.InvoiceDetails.Count = 0) Then
                        errors.AppendLine("Factura sin detalles")
                    Else
                        If invoice.ValFac <> invoice.InvoiceDetails.Sum(Function(d) d.LineExtensionAmountValue) Then
                            errors.AppendLine(String.Format("El valor de los detalles ({0}) no coincide con el Valor Total Facturado ({1})", invoice.InvoiceDetails.Sum(Function(d) d.LineExtensionAmountValue), invoice.ValFac))
                        End If

                        If invoice.ValImp1 <> invoice.InvoiceDetails.Sum(Function(d) d.IVAValue) Then
                            errors.AppendLine(String.Format("El valor de los detalles ({0}) no coincide con el Valor del IVA de la Factura ({1})", invoice.InvoiceDetails.Sum(Function(d) d.IVAValue), invoice.ValPag))
                        End If

                        If invoice.ThirdPartySalesValue <> invoice.InvoiceDetails.Sum(Function(d) d.LineExtensionAmount + d.IVAValue - d.WithholdingIVAValue - d.WithholdingValue - d.WithholdingICAValue) Then
                            errors.AppendLine(String.Format("El valor de los detalles ({0}) no coincide con el Valor a Pagar de la Factura ({1})", invoice.InvoiceDetails.Sum(Function(d) d.LineExtensionAmount + d.IVAValue - d.WithholdingIVAValue - d.WithholdingValue - d.WithholdingICAValue), invoice.ThirdPartySalesValue))
                        End If

                        If electronicDocument.DianVersion = 2.1 AndAlso errors.Length = 0 Then
                            Dim Value = invoice.ValFac
                            Dim IVAValue = invoice.ValImp1
                            invoice.WithholdingTax = invoice.InvoiceDetails.Where(Function(d) d.WithholdingIVAPercentage > 0).Sum(Function(d) d.WithholdingIVAValue)
                            invoice.RTFValue = invoice.InvoiceDetails.Where(Function(d) d.WithholdingPercentage > 0).Sum(Function(d) d.WithholdingValue)
                            invoice.WithholdingICA = invoice.InvoiceDetails.Where(Function(d) d.WithholdingICAPercentage > 0).Sum(Function(d) d.WithholdingICAValue)
                            Dim valFac = Value + IVAValue - invoice.WithholdingTax - invoice.RTFValue - invoice.WithholdingICA
                            Dim valPag As Decimal = 0D

                            paymentMethods = _invoiceRepository.GetPaymentMethodsByInvoiceId(invoice.Id)
                            invoice.InvoicePrepaidPayment = _invoiceRepository.GetPrepaidPaymentHealth(invoice.Id)

                            If paymentMethods IsNot Nothing AndAlso paymentMethods.Any() Then
                                valPag = paymentMethods.Sum(Function(d) d.Value)

                                If valPag > valFac Then
                                    Return New ActionResult(Of String) With {.StateResult = False, .Message = String.Format("El valor pagado ({0}) es mayor al valor de la factura ({1})", valPag, valFac)}
                                End If
                            End If
                            Dim TotalWithholdings = (invoice.WithholdingTax + invoice.RTFValue + invoice.WithholdingICA)
                            invoice.TotalValue = valFac - valPag + TotalWithholdings
                        End If
                    End If
                End If

                If errors.Length > 0 Then
                    Return New ActionResult(Of String) With {.StateResult = False, .Message = errors.ToString()}
                End If

                'Si no hay errores
                invoice.InvoiceMoreInformation = _invoiceRepository.GetInvoiceMoreInformationByInvoiceId(invoice.Id)
                billingAuthorization = _billingAuthorizationRepository.GetBillingAuthorizationById(invoice.BillingAuthorizationId)

                invoice.DianVersion = electronicDocument.DianVersion
                invoice.NitFE = supplierThirdParty.Person.IdentificationNumber
                invoice.TipAdq = customerThirdParty.Person.getAcquirerType()
                invoice.NumAdq = customerThirdParty.Person.IdentificationNumber
                invoice.ClTec = billingAuthorization.TechnicalKey
                invoice.SoftwarePin = settingsAccount.SoftwarePin
                invoice.Environment = settingsAccount.Environment
                invoice.ThirdPartyContributionType = supplierThirdParty.ContributionType

                If invoice.CUFE <> invoice.getCUFE() Then
                    invoice.CUFE = invoice.getCUFE()
                    invoice.QR = invoice.GetQRCode
                    electronicDocument.CUFE = invoice.CUFE

                    If electronicDocument.Status <> 3 Then
                        _invoiceRepository.SaveEntity(invoice)
                        _invoiceRepository.UnitWork.Commit()
                    End If
                End If
            ElseIf electronicDocument.getDocumentType() = Infrastructure.CrossCutting.Root.TypeElectronicDocument.DebitNote OrElse electronicDocument.getDocumentType() = Infrastructure.CrossCutting.Root.TypeElectronicDocument.CreditNote Then
                billingNote = _billingNoteRepository.GetBillingNoteByIdWithAggregates(electronicDocument.EntityId)
                If billingNote Is Nothing OrElse billingNote.Id = 0 Then
                    errors.AppendLine("La nota no fue encontrada")
                Else
                    'Validamos que los datos de la nota sean consistentes
                    If (billingNote.BillingNoteDetail Is Nothing OrElse billingNote.BillingNoteDetail.Count = 0) Then
                        errors.AppendLine("La nota no tiene detalles")
                    Else
                        'Validamos que los detalles de la version actual se encuentren validados
                        If billingNote.BillingNoteDetail.Any(Function(d) d.DianVersion = 2.1 AndAlso d.Status <> 3 AndAlso d.CUFE <> "") Then
                            For Each detail In billingNote.BillingNoteDetail.Where(Function(d) d.DianVersion = 2.1 AndAlso d.Status <> 3 AndAlso d.CUFE <> "")
                                errors.AppendLine(String.Format("La Factura '{0}' no se encuentra validada", detail.InvoiceNumber))
                            Next
                        End If

                        'Validamos que el ajuste corresponda con sus detalles
                        If billingNote.BillingNoteDetail.Any(Function(d) d.BillingNoteDetailTax.Any()) Then
                            For Each detail In billingNote.BillingNoteDetail
                                Dim taxValue As Decimal = detail.BillingNoteDetailTax.Sum(Function(t) t.TaxValue)
                                If detail.AdjusmentValue <> detail.BillingValue + taxValue Then
                                    errors.AppendLine(String.Format("El valor ajustado ({0}) no corresponde al valor de la factura mas el detalle de sus impuestos ({1})", detail.AdjusmentValue, (detail.BillingValue + taxValue)))
                                End If
                            Next
                        End If
                    End If
                End If

                Dim number As Int64
                'Validamos que los datos de la nota sean consistentes
                If Not Int64.TryParse(electronicDocument.DocumentNumber, number) Then
                    errors.AppendLine("El consecutivo de la Nota debe ser numerico")
                End If

                If errors.Length > 0 Then
                    Return New ActionResult(Of String) With {.StateResult = False, .Message = errors.ToString()}
                End If

                Dim hashSet As HashSet(Of EElectronicDocumentType) = Utils.ElectronicDocumentTypeNotAllowRIPS
                hashSet.Add(EElectronicDocumentType.CapitationControl)

                'Actualizamos el CUFE del detalle de acuerdo a la información de la Nota
                Parallel.ForEach(billingNote.BillingNoteDetail, Sub(x)
                                                                    If x.DianVersion <> 0 Then
                                                                        x.CUFE = x.Invoice.CUFE
                                                                    End If
                                                                    If Not hashSet.Contains(x.Invoice.DocumentType) Then
                                                                        x.InvoiceMoreInformation = _invoiceRepository.GetInvoiceMoreInformationByInvoiceId(x.InvoiceId)
                                                                    End If
                                                                End Sub)


                billingNote.NitFE = supplierThirdParty.Person.IdentificationNumber
                billingNote.NumAdq = customerThirdParty.Person.IdentificationNumber
                billingNote.SoftwarePin = settingsAccount.SoftwarePin
                billingNote.Environment = settingsAccount.Environment

                If billingNote.CUDE <> billingNote.getCUDE() Then
                    billingNote.CUDE = billingNote.getCUDE()
                    billingNote.QR = billingNote.GetQRCode()
                    electronicDocument.CUFE = billingNote.CUDE

                    If electronicDocument.Status <> 3 Then
                        _billingNoteRepository.SaveEntity(billingNote)
                        _billingNoteRepository.UnitWork.Commit()
                    End If
                End If
            End If

            Dim healthSegmentFromInvoice As CustomTagGeneralType = Nothing
            If electronicDocument.getDocumentType() = TypeElectronicDocument.CreditNote OrElse electronicDocument.getDocumentType() = TypeElectronicDocument.DebitNote Then
                Dim firstDetail = billingNote?.BillingNoteDetail?.FirstOrDefault
                If firstDetail IsNot Nothing AndAlso firstDetail.InvoiceId > 0 Then
                    Dim invoiceEd = _electronicDocumentRepository.GetElectronicDocumentByInvoiceId(firstDetail.InvoiceId)
                    If invoiceEd IsNot Nothing AndAlso invoiceEd.Id > 0 AndAlso Not String.IsNullOrEmpty(invoiceEd.FilePath) AndAlso _storage IsNot Nothing Then
                        healthSegmentFromInvoice = GetHealthSegmentFromInvoiceXml(invoiceEd, supplierThirdParty)
                    End If
                End If
            End If

            Dim ubl As Object = Nothing

            If electronicDocument.DianVersion = 2.1 Then
                ubl = New DIAN.UBL2_1.UBL2_1(customerThirdParty, electronicDocument.getDocumentType(), settingsAccount, supplierThirdParty, _storage)
                ubl.BillingAuthorization = billingAuthorization
                ubl.BillingNote = billingNote
                ubl.ElectronicDocument = electronicDocument
                ubl.Invoice = invoice
                ubl.PaymentMethods = paymentMethods
                ubl.HealthSegmentFromInvoice = healthSegmentFromInvoice
            End If

            Return ubl.GenerateXML()
        Catch ex As SqlException
            Return New ActionResult(Of String) With {.StateResult = False, .StateResultAux = Utils.ValidateSqlCodeExceptions(ex, {1205}), .Message = Utils.GetInnerExceptionMessageToString(ex)}
        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Private Function SendToDIAN(settingsAccount As GeneralLedgerSettings, supplierThirdParty As ThirdParty, customerThirdParty As ThirdParty, electronicDocument As ElectronicDocument, fileName As String, currentStatus As Byte) As Byte
        'Enviamos a la DIAN
        If electronicDocument.DianVersion = 2.1 Then
            Dim electronicDocumentDetail As New ElectronicDocumentDetail With
                {
                    .ElectronicDocumentId = electronicDocument.Id,
                    .CreationDate = DateTime.Now
                }

            electronicDocumentDetail.Destination = If(settingsAccount.Environment, 2, 1)
            Using client As New DistributedServices.DIAN.ServiceClient(settingsAccount.GetElectronicDocumentUrl(), settingsAccount.DigitalCertificate, settingsAccount.DigitalCertificateKey, _storage)
                Dim responseSend As ActionResult(Of DianResponse)
                If settingsAccount.Environment Then
                    responseSend = client.SendBill(electronicDocument.FilePath, fileName)
                Else
                    responseSend = client.SendTestSet(settingsAccount.TestSetId, electronicDocument.FilePath, fileName)
                End If

                If responseSend.StateResult Then
                    Dim documentResponse = responseSend.ObjectEmbbeded
                    If documentResponse IsNot Nothing Then
                        Dim errorMessage As New StringBuilder

                        If settingsAccount.Environment Then
                            If documentResponse.ErrorMessage IsNot Nothing AndAlso documentResponse.ErrorMessage.Any() Then
                                For Each message In documentResponse.ErrorMessage
                                    errorMessage.AppendLine(message)
                                Next
                            End If

                            currentStatus = If(documentResponse.IsValid, 3, If(errorMessage.Length = 0 OrElse statusCodeErrors.Contains(documentResponse.StatusCode), currentStatus, 4))
                            If errorMessage.ToString.Contains("procesado anteriormente.") AndAlso responseSend.ObjectEmbbeded.XmlDocumentKey = electronicDocument.CUFE Then
                                currentStatus = 3

                                Dim fileNameElectronicDocument = electronicDocument.GetFileName(supplierThirdParty, electronicDocument.getDocumentType())
                                Me._storage.DeleteFile(electronicDocument.FilePath, fileNameElectronicDocument)
                            End If
                        Else
                            currentStatus = If(documentResponse.IsValid, 2, 4)
                            electronicDocument.ZipKey = documentResponse.StatusMessage
                        End If

                        electronicDocument.ShippingDate = DateTime.Now
                        electronicDocument.ValidationDate = DateTime.Now

                        electronicDocumentDetail.Status = documentResponse.IsValid
                        electronicDocumentDetail.Response = documentResponse.StatusCode
                        electronicDocumentDetail.Comments = String.Concat(documentResponse.StatusDescription, " - ", documentResponse.StatusMessage)
                        electronicDocumentDetail.ResponseData = errorMessage.ToString()
                        _electronicDocumentDetailRepository.SaveEntity(electronicDocumentDetail)

                        Me.PostValidationEventAsync(settingsAccount, electronicDocument, supplierThirdParty, customerThirdParty, documentResponse, currentStatus).Wait()
                    End If
                Else
                    electronicDocument.ShippingDate = DateTime.Now
                    currentStatus = 1

                    electronicDocumentDetail.Status = False
                    electronicDocumentDetail.Response = Enums.ElectronicDocuments.StatusCode.ServiceUnavailable
                    electronicDocumentDetail.Comments = "Error al realizar el envio"
                    electronicDocumentDetail.ResponseData = responseSend.Message
                    _electronicDocumentDetailRepository.SaveEntity(electronicDocumentDetail)
                End If
            End Using
        End If

        Return currentStatus
    End Function

    Private Function ValidateDIAN(electronicDocument As ElectronicDocument, settingsAccount As GeneralLedgerSettings, supplierThirdParty As ThirdParty, customerThirdParty As ThirdParty, currentStatus As Byte, Optional beforeSend As Boolean = False) As Task(Of Byte)
        If electronicDocument.DianVersion = 2.1 Then
            Using client As New DistributedServices.DIAN.ServiceClient(settingsAccount.GetElectronicDocumentUrl(), settingsAccount.DigitalCertificate, settingsAccount.DigitalCertificateKey, _storage)
                'Validamos el envio realizado a la DIAN
                Dim responseValidate As ActionResult(Of DianResponse)
                If settingsAccount.Environment Then
                    responseValidate = client.GetStatus(electronicDocument.CUFE)
                Else
                    responseValidate = client.GetStatusZip(electronicDocument.ZipKey)
                End If
                If responseValidate.StateResult = True Then
                    Dim documentResponse = responseValidate.ObjectEmbbeded
                    If documentResponse IsNot Nothing Then
                        Dim errorMessage As New StringBuilder
                        If documentResponse.ErrorMessage IsNot Nothing AndAlso documentResponse.ErrorMessage.Any() Then
                            For Each message In documentResponse.ErrorMessage
                                errorMessage.AppendLine(message)
                            Next
                        End If

                        currentStatus = If(documentResponse.IsValid, 3, If(errorMessage.Length = 0 OrElse statusCodeErrors.Contains(documentResponse.StatusCode), currentStatus, 4))
                        If errorMessage.ToString.Contains("procesado anteriormente.") Then
                            currentStatus = 3

                            Dim fileNameElectronicDocument = electronicDocument.GetFileName(supplierThirdParty, electronicDocument.getDocumentType())
                            Me._storage.DeleteFile(electronicDocument.FilePath, fileNameElectronicDocument)
                        End If

                        If String.IsNullOrEmpty(documentResponse.StatusCode) OrElse documentResponse.StatusCode <> "66" Then
                            If beforeSend = False OrElse currentStatus = 3 Then
                                If beforeSend = False OrElse electronicDocument.Status <> 3 Then
                                    electronicDocument.ValidationDate = DateTime.Now

                                    _electronicDocumentDetailRepository.SaveEntity(New ElectronicDocumentDetail With
                                    {
                                        .ElectronicDocumentId = electronicDocument.Id,
                                        .Destination = 2, 'Validate To DIAN
                                        .CreationDate = DateTime.Now,
                                        .Status = documentResponse.IsValid,
                                        .Response = documentResponse.StatusCode,
                                        .Comments = String.Concat(documentResponse.StatusDescription, " - ", documentResponse.StatusMessage),
                                        .ResponseData = errorMessage.ToString()
                                    })
                                End If
                                Me.PostValidationEventAsync(settingsAccount, electronicDocument, supplierThirdParty, customerThirdParty, documentResponse, currentStatus).Wait()
                            End If
                        End If
                    End If
                End If
            End Using
        End If

        Return Task.FromResult(Of Byte)(currentStatus)
    End Function

    ''' <summary>
    ''' Servicio que se encarga de verificar si un archivo existe en el almacenamiento con un tiempo de espera y un intervalo de sondeo especificados.
    ''' </summary>
    ''' <param name="path"> ruta de acceso al blobstorage</param>
    ''' <param name="fileName">nombre especifico del archivo que se busca</param>
    ''' <param name="timeout">tiempo de espera para encontrar el archivo</param>
    ''' <param name="pollInterval">intervalo de tiempo para reintentos de busqueda</param>
    ''' <param name="ct">token de cancelación</param>
    ''' <returns></returns>
    Private Async Function WaitForFileExistsAsync(path As String,
                                              fileName As String,
                                              timeout As TimeSpan,
                                              pollInterval As TimeSpan,
                                              Optional ct As CancellationToken = Nothing) As Task(Of Boolean)
        Dim sw = Stopwatch.StartNew()
        Do
            If _storage.ValidateFileExists(path, fileName) Then
                Return True
            End If
            Await Task.Delay(pollInterval, ct)
        Loop While sw.Elapsed < timeout

        Return False
    End Function

    ''' <summary>
    ''' este metodo se ejecuta cuando los documentos se validan corrrectamente ante la DIAN
    ''' </summary>
    ''' <param name="settingsAccount"></param>
    ''' <param name="electronicDocument"></param>
    ''' <param name="supplierThirdParty"></param>
    ''' <param name="customerThirdParty"></param>
    ''' <param name="documentResponse"></param>
    ''' <param name="currentStatus"></param>
    Private Async Function PostValidationEventAsync(settingsAccount As GeneralLedgerSettings,
                                                electronicDocument As ElectronicDocument,
                                                supplierThirdParty As ThirdParty,
                                                customerThirdParty As ThirdParty,
                                                documentResponse As DianResponse,
                                                currentStatus As Byte,
                                                Optional ct As CancellationToken = Nothing) As Task
        Try
            If currentStatus = 3 Then
                ' 1) Asegurar ApplicationResponse
                Dim fileNameApplicationResponse = electronicDocument.GetFileName(supplierThirdParty, TypeElectronicDocument.ApplicationResponse)
                If Me._storage.ValidateIfNotExists(electronicDocument.FilePath, fileNameApplicationResponse) Then
                    Me._storage.WriteFile(electronicDocument.FilePath, fileNameApplicationResponse, documentResponse.XmlBase64Bytes)
                End If

                ' 2) Esperar visibilidad con reintentos
                Dim created As Boolean = Await WaitForFileExistsAsync(
                electronicDocument.FilePath,
                fileNameApplicationResponse,
                timeout:=TimeSpan.FromSeconds(30),
                pollInterval:=TimeSpan.FromMilliseconds(500),
                ct:=ct)

                ' 3) Generar AttachedDocument solo si el ApplicationResponse ya es visible
                If created Then
                    Dim fileNameAttachedDocument = electronicDocument.GetFileName(supplierThirdParty, TypeElectronicDocument.AttachedDocument)
                    Dim ubl = New DIAN.UBL2_1.UBL2_1(customerThirdParty,
                                                 Infrastructure.CrossCutting.Root.TypeElectronicDocument.AttachedDocument,
                                                 settingsAccount, supplierThirdParty, _storage)
                    ubl.ElectronicDocument = electronicDocument
                    Dim response = ubl.GenerateXML()
                    If Not response.StateResult Then
                        'No se generó el AttachedDocument, deben revisarse los detalles
                        _electronicDocumentDetailRepository.SaveEntity(New ElectronicDocumentDetail With {
                        .ElectronicDocumentId = electronicDocument.Id,
                        .Destination = 3, 'Envío al Cliente
                        .CreationDate = DateTime.Now,
                        .Status = False,
                        .Response = Enums.ElectronicDocuments.StatusCode.BadRequest,
                        .Comments = "No se generó el AttachedDocument",
                        .ResponseData = response.Message
                    })
                    End If
                    Dim attachedReady As Boolean = Await WaitForFileExistsAsync(electronicDocument.FilePath, fileNameAttachedDocument, timeout:=TimeSpan.FromSeconds(60),
                                                                                pollInterval:=TimeSpan.FromMilliseconds(400),
                                                                                ct:=ct)
                    If Not attachedReady Then
                        _electronicDocumentDetailRepository.SaveEntity(New ElectronicDocumentDetail With {
                            .ElectronicDocumentId = electronicDocument.Id,
                            .Destination = 3,
                            .CreationDate = DateTime.Now,
                            .Status = False,
                            .Response = Enums.ElectronicDocuments.StatusCode.NotImplemented,
                            .Comments = "AttachedDocument no visible tras el tiempo de espera",
                            .ResponseData = $"Archivo: {fileNameAttachedDocument}"
                        })
                    End If
                Else
                    ' No se alcanzó a ver el archivo dentro del timeout
                    _electronicDocumentDetailRepository.SaveEntity(New ElectronicDocumentDetail With {
                        .ElectronicDocumentId = electronicDocument.Id,
                        .Destination = 3, 'Generación datos para el envío
                        .CreationDate = DateTime.Now,
                        .Status = False,
                        .Response = Enums.ElectronicDocuments.StatusCode.NotImplemented,
                        .Comments = "ApplicationResponse no visible tras el tiempo de espera; no se generó AttachedDocument",
                        .ResponseData = $"Archivo: {fileNameApplicationResponse}"})
                End If

                Await QueueElectronicsRIPS(electronicDocument)

                ' 5) Notificaciones por email
                If electronicDocument.Status <> 3 AndAlso settingsAccount.Environment Then
                    If customerThirdParty.Person.Email IsNot Nothing AndAlso customerThirdParty.Person.Email.Any(Function(e) e.Type = 2) Then
                        For Each email In customerThirdParty.Person.Email.Where(Function(e) e.Type = 2)
                            If email.IsValidEmailFormat() Then
                                _electronicDocumentNotificationRepository.SaveEntity(New ElectronicDocumentNotification With {
                                .ElectronicDocumentId = electronicDocument.Id,
                                .Email = email.Email1,
                                .Status = 0,
                                .CreationDate = DateTime.Now
                            })
                            End If
                        Next
                    End If
                End If
            End If
        Catch ex As OperationCanceledException
            _electronicDocumentDetailRepository.SaveEntity(New ElectronicDocumentDetail With {
            .ElectronicDocumentId = electronicDocument.Id,
            .Destination = 3,
            .CreationDate = DateTime.Now,
            .Status = False,
            .Response = Enums.ElectronicDocuments.StatusCode.NotImplemented,
            .Comments = "Proceso cancelado durante postvalidación",
            .ResponseData = Utils.GetInnerExceptionMessageToString(ex)
        })
        Catch ex As Exception
            'Si ocurre un error en el proceso almacenamos el error
            _electronicDocumentDetailRepository.SaveEntity(New ElectronicDocumentDetail With {
            .ElectronicDocumentId = electronicDocument.Id,
            .Destination = 3, 'Generación datos para el envío 
            .CreationDate = DateTime.Now,
            .Status = False,
            .Response = Enums.ElectronicDocuments.StatusCode.NotImplemented,
            .Comments = "Error ejecutando el proceso de postvalidación",
            .ResponseData = Utils.GetInnerExceptionMessageToString(ex)
        })
        End Try
    End Function

    ''' <summary>
    ''' metodo que valida (si debe o no disparar la generacion de rips) y envia a generar los RIPS Electronicos
    ''' </summary>
    Public Async Function QueueElectronicsRIPS(electronicDocument As ElectronicDocument) As Task

        Dim flagToSendRIPS As Boolean = False
        ' se valida si es una nota para consultar el tipo de documento de la factura original para saber si es de salud
        If {EElectronicDocumentType.CreditNote, EElectronicDocumentType.DebitNote, EElectronicDocumentType.CreditNotePV, EElectronicDocumentType.DebitNotePV}.Contains(electronicDocument.DocumentType) AndAlso electronicDocument.EntityName = NameOf(BillingNote) Then
            flagToSendRIPS = _electronicsPropertiesRepository.ValidateOriginNoteToRIPS(electronicDocument.EntityId)
        Else ' se valida si es una fact si el tipo de documento corresponde a fact salud a exepcion de fact capita
            flagToSendRIPS = Not Utils.ElectronicDocumentTypeNotAllowRIPS.Contains(electronicDocument.DocumentType) _
                            AndAlso electronicDocument.EntityName = NameOf(Invoice) _
                            AndAlso electronicDocument.DocumentDate >= New DateTime(2025, 2, 1)
        End If
        ' de no pasar la validacion (osea no ser un documento asociado o de una fact salud) lo saca del metodo y no envia a generar E- rips
        If Not flagToSendRIPS Then
            Return
        End If

        Dim DocumentCode As String = $"{If(electronicDocument.Prefix, String.Empty)}{electronicDocument.DocumentNumber}"

        Dim unitWork As Domain.Base.IUnitWork = Me._electronicsPropertiesRepository.UnitWork

        Dim electronicProperties As ElectronicsProperties = Me._electronicsPropertiesRepository _
            .FirstOrDefault(Function(x) x.EntityId = electronicDocument.EntityId AndAlso x.EntityName = electronicDocument.EntityName, True, {"ElectronicsRIPS.ElectronicsRIPSDetail"})

        If electronicProperties Is Nothing OrElse electronicProperties.Id = 0 Then
            electronicProperties = New ElectronicsProperties
            With electronicProperties
                .EntityId = electronicDocument.EntityId
                .EntityName = electronicDocument.EntityName
                .CreationUser = "999"
                .CreationDate = electronicDocument.CreationDate
                .StatusRIPS = EStatusERIPS.Register
            End With
        End If

        Dim electronicsRIPS As ElectronicsRIPS
        If electronicProperties.ElectronicsRIPS?.Any() Then

            'se valida si el mensaje previamente fue disparado desde el servicio de la DIAN
            If electronicProperties.ElectronicsRIPS _
                .Any(Function(x) x.ElectronicsRIPSDetail IsNot Nothing _
                    AndAlso x.ElectronicsRIPSDetail.Any(Function(f) f.MessageCode = MESSAGE_CODE_ERIPS AndAlso f.Message = MESSAGE_ERIPS)) Then
                Return
            End If

            electronicProperties.MarkAsModified()
            electronicsRIPS = electronicProperties.ElectronicsRIPS.FirstOrDefault
            electronicsRIPS.Retry += 1
            electronicsRIPS.MarkAsModified()
        Else
            electronicsRIPS = New ElectronicsRIPS()
            electronicsRIPS.RadicateDate = electronicDocument.CreationDate
            electronicsRIPS.sendDate = If(electronicDocument.ValidationDate, electronicDocument.CreationDate)
            electronicsRIPS.Retry = 0
            electronicsRIPS.CosmoDBId = ""
            electronicsRIPS.FilePath = ""
            electronicsRIPS.CreationDate = DateTime.Now()
            electronicsRIPS.CreationUser = "999"
        End If

        With electronicProperties
            Dim electronicsRIPSDetail = New ElectronicsRIPSDetail()
            electronicsRIPSDetail.MessageCode = MESSAGE_CODE_ERIPS
            electronicsRIPSDetail.Message = MESSAGE_ERIPS
            electronicsRIPSDetail.CreationUser = "999"
            electronicsRIPSDetail.CreationDate = DateTime.Now()
            electronicsRIPS.ElectronicsRIPSDetail.Add(electronicsRIPSDetail)
            .ElectronicsRIPS.Add(electronicsRIPS)
        End With

        Try
            If Me.Endpoint Is Nothing Then
                Throw New ArgumentNullException(NameOf(Me.Endpoint))
            End If
            Dim client = New RestClient(String.Format("{0}/electronicRIPS/sendRIPS/{1}", Endpoint.UrlBase, electronicDocument.EntityName))
            Dim req = New RestRequest()
            req.AddHeader("_ContainerName_", SessionValues.Instance.TransactionalContainer)
            req.AddHeader("_ContainerHisName_", SessionValues.Instance.HisContainer)
            req.AddHeader("CodeUser", "999")
            req.AddParameter("application/json", Utils.SerializeObjectToJson({DocumentCode}.ToList()), ParameterType.RequestBody)

            Dim response = Await client.PostAsync(Of RequestResponse(Of String))(req)

            If response?.Status Is Nothing OrElse Not response.Status Then
                Throw New Exception(If(response?.Message, "Error respuesta API SendRIPS"))
            End If

        Catch ex As Exception
            electronicProperties.StatusRIPS = EStatusERIPS.Exception
            With electronicProperties.ElectronicsRIPS.FirstOrDefault.ElectronicsRIPSDetail.FirstOrDefault
                .MessageCode = "99"
                .Message = Utils.GetInnerExceptionMessageToString(ex)
            End With
        Finally
            _electronicsPropertiesRepository.SaveEntity(electronicProperties)
            unitWork.Commit()
        End Try
    End Function

    ''' <summary>
    ''' metodo que genera el pdf para enviar al adquiriente
    ''' </summary>
    ''' <param name="electronicDocument"></param>
    ''' <param name="fileNamePDF"></param>
    ''' <returns></returns>
    Public Function GeneratePDF(electronicDocument As ElectronicDocument, fileNamePDF As String) As ActionResult(Of String)
        Try
            Me._storage.DeleteFile(electronicDocument.FilePath, fileNamePDF)
            If Me._storage.ValidateIfNotExists(electronicDocument.FilePath, fileNamePDF) Then
                Dim reportsPath = Utils.GetAppSettingValueByKey("_ReportsPath_")
                Dim viewer = New DocumentViewer() '' Necesaria para que el customizado funcione bien

                If electronicDocument.getDocumentType() = TypeElectronicDocument.Invoice Then
                    If electronicDocument.DocumentType = 7 Then 'Factura de Venta de Productos
                        Dim rptInvoiceProducts As New Presentation.Reporter.rptInvoiceProducts
                        Dim customReportPath = Utils.GetDefaultReport(reportsPath, 1561, "rptInvoiceProducts")
                        If File.Exists(customReportPath) Then
                            rptInvoiceProducts.LoadLayout(customReportPath)
                        End If

                        rptInvoiceProducts.ParametrosReporte = New Object() {0, electronicDocument.EntityId}
                        rptInvoiceProducts.CargarDataSource()

                        'Guarda el pdf en el servicio de storage
                        Using memoryStream As New MemoryStream()
                            rptInvoiceProducts.ExportToPdf(memoryStream)
                            _storage.WriteFile(electronicDocument.FilePath, fileNamePDF, memoryStream.ToArray())
                        End Using

                        rptInvoiceProducts.Dispose()
                    ElseIf electronicDocument.DocumentType = 6 Then 'Factura Basica
                        Dim rptBasicBilling As New Presentation.Reporter.rptBasicBilling
                        Dim customReportPath = Utils.GetDefaultReport(reportsPath, 2038, "rptBasicBilling")
                        If File.Exists(customReportPath) Then
                            rptBasicBilling.LoadLayout(customReportPath)
                        End If

                        rptBasicBilling.ParametrosReporte = New Object() {0, electronicDocument.EntityId}
                        rptBasicBilling.CargarDataSource()

                        'Guarda el pdf en el servicio de storage
                        Using memoryStream As New MemoryStream()
                            rptBasicBilling.ExportToPdf(memoryStream)
                            _storage.WriteFile(electronicDocument.FilePath, fileNamePDF, memoryStream.ToArray())
                        End Using

                        rptBasicBilling.Dispose()
                    ElseIf electronicDocument.DocumentType = 4 Then 'Factura Monto Fijo
                        Dim rptSaleInvoiceCapitated As New Presentation.Reporter.rptSaleInvoiceCapitated
                        Dim customReportPath = Utils.GetDefaultReport(reportsPath, 758, "rptSaleInvoiceCapitated")
                        If File.Exists(customReportPath) Then
                            rptSaleInvoiceCapitated.LoadLayout(customReportPath)
                        End If

                        rptSaleInvoiceCapitated.ParametrosReporte = New Object() {electronicDocument.EntityId}
                        rptSaleInvoiceCapitated.CargarDataSource2()

                        'Guarda el pdf en el servicio de storage
                        Using memoryStream As New MemoryStream()
                            rptSaleInvoiceCapitated.ExportToPdf(memoryStream)
                            _storage.WriteFile(electronicDocument.FilePath, fileNamePDF, memoryStream.ToArray())
                        End Using

                        rptSaleInvoiceCapitated.Dispose()
                    Else 'Factura Ley 100
                        Dim rptSaleInvoice As New Presentation.Reporter.rptSaleInvoice
                        Dim customReportPath = Utils.GetDefaultReport(reportsPath, 756, "rptSaleInvoice")
                        If File.Exists(customReportPath) Then
                            rptSaleInvoice.LoadLayout(customReportPath)
                        End If

                        rptSaleInvoice.SetValueCodingServices = CInt(electronicDocument.printMode)
                        rptSaleInvoice.ParametrosReporte = New Object() {electronicDocument.EntityId}
                        rptSaleInvoice.CargarDataSource2()

                        'Guarda el pdf en el servicio de storage
                        Using memoryStream As New MemoryStream()
                            rptSaleInvoice.ExportToPdf(memoryStream)
                            _storage.WriteFile(electronicDocument.FilePath, fileNamePDF, memoryStream.ToArray())
                        End Using

                        rptSaleInvoice.Dispose()
                    End If
                Else
                    Dim noteType = ValidateNoteType(electronicDocument.Id)
                    Dim rptBillingNote = Nothing

                    If noteType = 1 Then
                        rptBillingNote = New Presentation.Reporter.rptBillingNote()
                    Else
                        rptBillingNote = New Presentation.Reporter.rptBillingNoteDetail()
                    End If

                    Dim customReportPath = Utils.GetDefaultReport(reportsPath, 2039, If(noteType = 1, "rptBillingNote", "rptBillingNoteDetail"))
                    If File.Exists(customReportPath) Then
                        rptBillingNote.LoadLayout(customReportPath)
                    End If

                    rptBillingNote.ParametrosReporte = New Object() {electronicDocument.EntityId}
                    rptBillingNote.CargarDataSource()

                    'Guarda el pdf en el servicio de storage
                    Using memoryStream As New MemoryStream()
                        rptBillingNote.ExportToPdf(memoryStream)
                        _storage.WriteFile(electronicDocument.FilePath, fileNamePDF, memoryStream.ToArray())
                    End Using

                    rptBillingNote.Dispose()
                End If
            End If

            Return New ActionResult(Of String) With {.StateResult = True, .Message = "OK"}
        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Método que devuelve el tipo de nota, para armar el reporte correspondiente
    ''' </summary>
    ''' <param name="electronicDocumentId"></param>
    ''' <returns></returns>
    Private Function ValidateNoteType(electronicDocumentId As Integer)
        Return _electronicDocumentNotificationRepository.GetElectronicNoteType(electronicDocumentId)
    End Function

#End Region

#Region "Health Segment From Invoice XML"

    ''' <summary>
    ''' Obtiene el segmento de interoperabilidad/sector salud del XML de la factura asociada.
    ''' Si el XML no existe o no tiene el segmento, retorna Nothing.
    ''' </summary>
    Private Function GetHealthSegmentFromInvoiceXml(invoiceElectronicDocument As ElectronicDocument, supplierThirdParty As ThirdParty) As CustomTagGeneralType
        Try
            Dim filePath = invoiceElectronicDocument.FilePath
            Dim fileName = invoiceElectronicDocument.GetFileName(supplierThirdParty, TypeElectronicDocument.Invoice)
            Dim xmlString As String = String.Empty

            Dim xmlExists As Boolean = Not _storage.ValidateIfNotExists(filePath, fileName)

            If Not xmlExists Then
                Dim zipName = fileName
                If zipName.StartsWith("fv", StringComparison.OrdinalIgnoreCase) Then
                    zipName = "z" & zipName.Substring(2)
                End If
                zipName = Path.ChangeExtension(zipName, ".zip")

                Dim zipExists As Boolean = Not _storage.ValidateIfNotExists(filePath, zipName)
                If Not zipExists Then
                    Return Nothing
                End If

                Dim zipBytes = _storage.ReadFile(filePath, zipName)
                Using zipStream As New MemoryStream(zipBytes)
                    Using zip As New ZipArchive(zipStream, ZipArchiveMode.Read)
                        Dim xmlEntry = zip.Entries.FirstOrDefault(Function(e) e.FullName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                        If xmlEntry Is Nothing Then
                            Return Nothing
                        End If
                        Using entryStream = xmlEntry.Open()
                            Using reader As New StreamReader(entryStream, Encoding.UTF8)
                                xmlString = reader.ReadToEnd()
                            End Using
                        End Using
                    End Using
                End Using
            Else
                Dim fileBytes = _storage.ReadFile(filePath, fileName)
                xmlString = Encoding.UTF8.GetString(fileBytes)
            End If

            If String.IsNullOrEmpty(xmlString) Then
                Return Nothing
            End If

            Dim invoiceType = Utils.Deserialize(Of InvoiceType)(XDocument.Parse(xmlString))
            If invoiceType?.UBLExtensions Is Nothing Then
                Return Nothing
            End If

            Dim healthExtension = invoiceType.UBLExtensions.FirstOrDefault(
                Function(ext) ext?.ExtensionContent?.CustomTagGeneral IsNot Nothing AndAlso
                              ext.ExtensionContent.CustomTagGeneral.Interoperabilidad IsNot Nothing)

            Return healthExtension?.ExtensionContent?.CustomTagGeneral
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

#End Region

#Region "IDisposable Support"

    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                'Others Services
            End If

            _billingAuthorizationRepository = Nothing
            _billingNoteRepository = Nothing
            _electronicDocumentRepository = Nothing
            _electronicDocumentDetailRepository = Nothing
            _electronicDocumentNotificationRepository = Nothing
            _invoiceRepository = Nothing
            _operatingUnitRepository = Nothing
            _thirdPartyRepository = Nothing
            _settingsAccountRepository = Nothing
            _revenueControlDetailRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class