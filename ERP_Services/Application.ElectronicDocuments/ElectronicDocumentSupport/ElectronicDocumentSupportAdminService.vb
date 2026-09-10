Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports System.Text
Imports DevExpress.XtraPrinting.Preview
Imports DistributedServices.DIAN.WcfDianCustomerServices
Imports Domain.Base.Entities
Imports Domain.ElectronicDocuments.Service
Imports Domain.Entities
Imports Domain.Notification
Imports NewRelic.Api.Agent
Imports Infrastructure.CrossCutting.AzureBlobStorage
Imports Infrastructure.CrossCutting.AzureBlobStorage.Factory
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Root
Imports Infrastructure.CrossCutting.Signature
Imports Utils = Infrastructure.CrossCutting.Base.Utils

Public Class ElectronicDocumentSupportAdminService
    Implements IElectronicDocumentSupportAdminService

#Region "Fields"
    Private _billingAuthorizationRepository As IBillingAuthorizationRepository
    Private _electronicSupportDocumentRepository As IElectronicSupportDocumentRepository
    Private _electronicSupportDocumentDetailRepository As IElectronicSupportDocumentDetailRepository
    Private _operatingUnitRepository As IOperatingUnitRepository
    Private _thirdPartyRepository As IThirdPartyRepository
    Private _settingsAccountRepository As ISettingsAccountRepository
    Private _electronicSupportDocumentAdjustmentNoteRepository As IElectronicSupportDocumentAdjustmentNoteRepository
    Private _electronicSupportDocumentAdjustmentNoteDetailRepository As IElectronicSupportDocumentAdjustmentNoteDetailRepository
    Private ReadOnly _factoryStorage As IFactoryStorage
    Private ReadOnly _storage As IStorage
#End Region
#Region "Properties"

    Private ReadOnly statusCodeErrors As String() =
    {
        "500",
        "503"
    }

    Private Const electronicSupportDocument As String = "ElectronicSupportDocument"
    Private Const electronicSupportDocumentAdjustmentNote As String = "ElectronicSupportDocumentAdjustmentNote"

    ''' <summary>
    ''' Control máximo de envíos
    ''' </summary>
    Private maxRetry As Integer = 5

#End Region
#Region "Builders"
    Public Sub New(billingAuthorizationRepository As IBillingAuthorizationRepository,
                   electronicSupportDocumentRepository As IElectronicSupportDocumentRepository,
                   electronicSupportDocumentDetailRepository As IElectronicSupportDocumentDetailRepository,
                   operatingUnitRepository As IOperatingUnitRepository,
                   thirdPartyRepository As IThirdPartyRepository,
                   settingsAccountRepository As ISettingsAccountRepository,
                   electronicSupportDocumentAdjustmentNoteRepository As IElectronicSupportDocumentAdjustmentNoteRepository,
                   electronicSupportDocumentAdjustmentNoteDetailRepository As IElectronicSupportDocumentAdjustmentNoteDetailRepository,
                   factoryStorage As IFactoryStorage)

        Me._billingAuthorizationRepository = billingAuthorizationRepository
        Me._electronicSupportDocumentRepository = electronicSupportDocumentRepository
        Me._electronicSupportDocumentDetailRepository = electronicSupportDocumentDetailRepository
        Me._operatingUnitRepository = operatingUnitRepository
        Me._thirdPartyRepository = thirdPartyRepository
        Me._settingsAccountRepository = settingsAccountRepository
        Me._electronicSupportDocumentAdjustmentNoteRepository = electronicSupportDocumentAdjustmentNoteRepository
        Me._electronicSupportDocumentAdjustmentNoteDetailRepository = electronicSupportDocumentAdjustmentNoteDetailRepository
        Me._factoryStorage = factoryStorage
        Me._storage = Me._factoryStorage.CreateStorageControl()

        ' Setting TLS 1.2 protocol '
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        ServicePointManager.ServerCertificateValidationCallback = Function(sender1, certificate, chain, sslPolicyErrors)
                                                                      Return True
                                                                  End Function
    End Sub
#End Region
#Region "Methods"
    Public Async Function ExecuteProcessElectronicSupportDocument() As Task(Of ActionResult(Of String)) Implements IElectronicDocumentSupportAdminService.ExecuteProcessElectronicSupportDocument
        Try
            Return Await ExecuteProcessElectronicSupportDocumentAsync()
        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function
#End Region
#Region "Private Methods"
    Private Async Function ExecuteProcessElectronicSupportDocumentAsync() As Task(Of ActionResult(Of String))
        Dim erros As New StringBuilder
        Dim listElectronicDocumentIds = _electronicSupportDocumentRepository.GetElectronicDocumentIds
        For Each electronicDocument In listElectronicDocumentIds
            Dim result As New ActionResult(Of String)
            If electronicDocument.Entity = electronicSupportDocument Then
                result = Await ProcessElectronicSupportDocument(CInt(electronicDocument.EntityId))
            ElseIf electronicDocument.Entity = electronicSupportDocumentAdjustmentNote Then
                result = Await ProcessElectronicAdjustmentNote(CInt(electronicDocument.EntityId))
            End If

            If Not result.StateResult Then
                erros.Append(result.Message)
            End If
        Next
        Return New ActionResult(Of String) With {.StateResult = IIf(erros.Length = 0, True, False), .Message = erros.ToString()}
    End Function

    ''' <summary>
    ''' Metodo donde arranca el proceso de envío y respuesta del documento soporte electrónico
    ''' </summary>
    ''' <returns></returns>
    <Transaction>
    Private Async Function ProcessElectronicSupportDocument(electronicSupportDocumentId As Integer) As Task(Of ActionResult(Of String))
        'Diccionarios
        Dim dictionarySettingsAccount As New Dictionary(Of Integer, GeneralLedgerSettings)()
        Dim dictionarySupplierThirdParty As New Dictionary(Of Integer, ThirdParty)()
        Dim dictionaryCustomerThirdParty As New Dictionary(Of Integer, ThirdParty)()
        Dim errors As New StringBuilder

        'Entidades
        Dim settingsAccount As GeneralLedgerSettings
        Dim supplierThirdParty As ThirdParty
        Dim customerThirdParty As ThirdParty

        Dim ElectronicSupportDocument = _electronicSupportDocumentRepository.GetDocumentSupportById(electronicSupportDocumentId)

        'Validación para intentar procesar un documento dentro de un intervalo cuando tiene rechazos o errores 
        Dim standByStates = If({0, 4}.Contains(ElectronicSupportDocument.StatusElectronic), 5, 0)
        Dim minuteToAdd = Utils.GetMinutesToAdd(ElectronicSupportDocument.Retry + standByStates)
        Dim timeForRetry = DateAdd(DateInterval.Minute, minuteToAdd, ElectronicSupportDocument.CreationDate)

        'Bandera para saber si es un proceso manual
        Dim forcedProcess = If(ElectronicSupportDocument.StatusElectronic = 66, True, False)
        If ElectronicSupportDocument.StatusElectronic <> 66 AndAlso DateTime.Now < timeForRetry Then
            Return New ActionResult(Of String) With {.StateResult = True, .Message = errors.ToString()}
        ElseIf ElectronicSupportDocument.Retry > Me.maxRetry AndAlso Not forcedProcess Then 'Validación por numero de intentos
            Return New ActionResult(Of String) With {.StateResult = True, .Message = errors.ToString()}
        End If

        'Si es un estado erroneo o invalido cambia a estado registrado
        If {0, 4, 66, 88}.Contains(ElectronicSupportDocument.StatusElectronic) Then
            ElectronicSupportDocument.StatusElectronic = 1
        End If

        'UnitWorks
        Dim electronicSupportDocumentUnitWork = _electronicSupportDocumentRepository.UnitWork
        Dim electronicSupportDocumentDetailUnitWork = _electronicSupportDocumentDetailRepository.UnitWork

        Dim currentStatus = IIf({88, 66}.Contains(ElectronicSupportDocument.StatusElectronic), 1, IIf(ElectronicSupportDocument.StatusElectronic = 99, 2, ElectronicSupportDocument.StatusElectronic))

        'Aumentamos el numero de envío
        ElectronicSupportDocument.Retry += 1

        Try
            If ElectronicSupportDocument.Retry <= Me.maxRetry Or forcedProcess Then

                ElectronicSupportDocument.StatusElectronic = IIf(ElectronicSupportDocument.StatusElectronic = 1, 88, IIf(ElectronicSupportDocument.StatusElectronic = 2, 99, ElectronicSupportDocument.StatusElectronic)) 'En proceso
                electronicSupportDocumentUnitWork.Commit()

                'Cargamos parametros de contabilidad para la unidad operativa
                If Not dictionarySettingsAccount.ContainsKey(ElectronicSupportDocument.OperativeUnitId) Then
                    settingsAccount = _settingsAccountRepository.GetSettingAccountSimple(ElectronicSupportDocument.OperativeUnitId)
                    dictionarySettingsAccount.Add(ElectronicSupportDocument.OperativeUnitId, settingsAccount)
                Else
                    settingsAccount = dictionarySettingsAccount(ElectronicSupportDocument.OperativeUnitId)
                End If

                'Cargamos el proveedor al que se le hace el documento
                If Not dictionarySupplierThirdParty.ContainsKey(ElectronicSupportDocument.SupplierThirdPartyId) Then
                    supplierThirdParty = _thirdPartyRepository.GetThirdPartyById(ElectronicSupportDocument.SupplierThirdPartyId, False)
                    dictionarySupplierThirdParty.Add(ElectronicSupportDocument.SupplierThirdPartyId, supplierThirdParty)
                Else
                    supplierThirdParty = dictionarySupplierThirdParty(ElectronicSupportDocument.SupplierThirdPartyId)
                End If

                'Cargamos el adquiriente, yo como empresa
                If Not dictionaryCustomerThirdParty.ContainsKey(settingsAccount.IdDian) Then
                    customerThirdParty = _thirdPartyRepository.GetThirdPartyById(settingsAccount.IdDian, False)
                    dictionaryCustomerThirdParty.Add(settingsAccount.IdDian, customerThirdParty)
                Else
                    customerThirdParty = dictionaryCustomerThirdParty(settingsAccount.IdDian)
                End If

                'proceso 
                If currentStatus = 1 Then
                    'Si no se ha definido una ruta de archivo la generamos
                    If String.IsNullOrEmpty(ElectronicSupportDocument.FilePath) Then
                        ElectronicSupportDocument.FilePath = System.IO.Path.Combine(
                                Utils.GetPathElectronicDocuments(),
                                ServerSessionValues.Current.CurrentContainer,
                                ElectronicSupportDocument.Year,
                                ElectronicSupportDocument.DocumentDate.Month.ToString(),
                                Utils.GetDescriptionTypeDocumentElectronic(TypeElectronicDocument.DocumentoSoporte),
                                ElectronicSupportDocument.DocumentNumber
                            )
                    End If

                    'Validamos el documentos antes de enviar a la DIAN
                    currentStatus = Await Me.ValidateDIAN(TypeElectronicDocument.DocumentoSoporte, settingsAccount, customerThirdParty, currentStatus, ElectronicSupportDocument, Nothing, True)
                    If currentStatus <> 3 Then
                        'Generamos XML
                        Dim response = Me.GenerateXML(TypeElectronicDocument.DocumentoSoporte, settingsAccount, supplierThirdParty, customerThirdParty, ElectronicSupportDocument)
                        If response.StateResult Then
                            currentStatus = Me.SendToDIAN(TypeElectronicDocument.DocumentoSoporte, settingsAccount, supplierThirdParty, customerThirdParty, response.ObjectEmbbeded, currentStatus, ElectronicSupportDocument)
                        Else
                            currentStatus = IIf(response.StateResultAux, currentStatus, 0)

                            'Documento invalido
                            _electronicSupportDocumentDetailRepository.SaveEntity(New ElectronicSupportDocumentDetail With
                           {
                            .ElectronicSupportDocumentId = ElectronicSupportDocument.Id,
                            .Destination = 0, 'Error en el proceso
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
                    currentStatus = Await Me.ValidateDIAN(TypeElectronicDocument.DocumentoSoporte, settingsAccount, customerThirdParty, currentStatus, ElectronicSupportDocument)
                End If

                ElectronicSupportDocument.StatusElectronic = currentStatus
            Else
                _electronicSupportDocumentDetailRepository.SaveEntity(New ElectronicSupportDocumentDetail With
                          {
                           .ElectronicSupportDocumentId = ElectronicSupportDocument.Id,
                           .Destination = 0, 'Error en el proceso
                           .CreationDate = DateTime.Now,
                           .Status = False,
                           .Response = Enums.ElectronicDocuments.StatusCode.BadRequest,
                           .Comments = "Error por control",
                           .ResponseData = "Documento supera el limite de intentos permitidos, se debe procesar manualmente"
                          })

                ElectronicSupportDocument.StatusElectronic = 0
            End If
        Catch ex As Exception
            'Si ocurre un error en el proceso almacenamos el error
            _electronicSupportDocumentDetailRepository.SaveEntity(New ElectronicSupportDocumentDetail With
            {
                .ElectronicSupportDocumentId = ElectronicSupportDocument.Id,
                .Destination = 0, 'Error en el proceso
                .CreationDate = DateTime.Now,
                .Status = False,
                .Response = Enums.ElectronicDocuments.StatusCode.NotImplemented,
                 .Comments = "Error ejecutando el proceso",
                 .ResponseData = Utils.GetInnerExceptionMessageToString(ex)
            })

            errors.AppendLine(Utils.GetInnerExceptionMessageToString(ex))
        End Try


        _electronicSupportDocumentRepository.SaveEntity(ElectronicSupportDocument)

        electronicSupportDocumentUnitWork.Commit()
        electronicSupportDocumentDetailUnitWork.Commit()

        Return New ActionResult(Of String) With {.StateResult = IIf(errors.Length = 0, True, False), .Message = errors.ToString()}
    End Function

    ''' <summary>
    ''' Metodo principal donde se procesan las notas de ajuste
    ''' </summary>
    ''' <param name="electronicAdjusmentNoteId"></param>
    ''' <returns></returns>
    <Transaction>
    Private Async Function ProcessElectronicAdjustmentNote(electronicAdjusmentNoteId As Integer) As Task(Of ActionResult(Of String))
        'Diccionarios
        Dim dictionarySettingsAccount As New Dictionary(Of Integer, GeneralLedgerSettings)()
        Dim dictionarySupplierThirdParty As New Dictionary(Of Integer, ThirdParty)()
        Dim dictionaryCustomerThirdParty As New Dictionary(Of Integer, ThirdParty)()
        Dim errors As New StringBuilder

        'Entidades
        Dim settingsAccount As GeneralLedgerSettings
        Dim supplierThirdParty As ThirdParty
        Dim customerThirdParty As ThirdParty

        Dim SupportDocumentAdjustmentNote = Me._electronicSupportDocumentAdjustmentNoteRepository.GetElectronicAdjustmentWithAggregateNoteById(electronicAdjusmentNoteId)

        'Validación para intentar procesar un documento dentro de un intervalo cuando tiene rechazos o errores 
        Dim standByStates = If({0, 4}.Contains(SupportDocumentAdjustmentNote.StatusElectronic), 5, 0)
        Dim minuteToAdd = Utils.GetMinutesToAdd(SupportDocumentAdjustmentNote.Retry + standByStates)
        Dim timeForRetry = DateAdd(DateInterval.Minute, minuteToAdd, SupportDocumentAdjustmentNote.CreationDate)

        'Bandera para saber si es un proceso manual
        Dim forcedProcess = If(SupportDocumentAdjustmentNote.StatusElectronic = 66, True, False)
        If SupportDocumentAdjustmentNote.StatusElectronic <> 66 AndAlso DateTime.Now < timeForRetry Then
            Return New ActionResult(Of String) With {.StateResult = True, .Message = errors.ToString()}
        ElseIf SupportDocumentAdjustmentNote.Retry > Me.maxRetry AndAlso Not forcedProcess Then
            Return New ActionResult(Of String) With {.StateResult = True, .Message = errors.ToString()}
        End If

        'Si es un estado erroneo o invalido cambia a estado registrado
        If {0, 4, 66, 88}.Contains(SupportDocumentAdjustmentNote.StatusElectronic) Then
            SupportDocumentAdjustmentNote.StatusElectronic = 1
        End If

        'UnitWorks
        Dim electronicSupportDocumentAdjustmentNoteUO = _electronicSupportDocumentAdjustmentNoteRepository.UnitWork
        Dim electronicSupportDocumentAdjustmentNoteDetailUO = _electronicSupportDocumentAdjustmentNoteDetailRepository.UnitWork

        Dim currentStatus = IIf({88, 66}.Contains(SupportDocumentAdjustmentNote.StatusElectronic), 1, IIf(SupportDocumentAdjustmentNote.StatusElectronic = 99, 2, SupportDocumentAdjustmentNote.StatusElectronic))

        'Aumentamos el numero de envío
        SupportDocumentAdjustmentNote.Retry += 1

        Try
            If SupportDocumentAdjustmentNote.Retry <= Me.maxRetry Or forcedProcess Then

                SupportDocumentAdjustmentNote.StatusElectronic = IIf(SupportDocumentAdjustmentNote.StatusElectronic = 1, 88, IIf(SupportDocumentAdjustmentNote.StatusElectronic = 2, 99, SupportDocumentAdjustmentNote.StatusElectronic)) 'En proceso
                electronicSupportDocumentAdjustmentNoteUO.Commit()

                'Cargamos parametros de contabilidad para la unidad operativa
                If Not dictionarySettingsAccount.ContainsKey(SupportDocumentAdjustmentNote.OperativeUnitId) Then
                    settingsAccount = _settingsAccountRepository.GetSettingAccountSimple(SupportDocumentAdjustmentNote.OperativeUnitId)
                    dictionarySettingsAccount.Add(SupportDocumentAdjustmentNote.OperativeUnitId, settingsAccount)
                Else
                    settingsAccount = dictionarySettingsAccount(SupportDocumentAdjustmentNote.OperativeUnitId)
                End If

                'Cargamos el proveedor al que se le hace el documento
                If Not dictionarySupplierThirdParty.ContainsKey(SupportDocumentAdjustmentNote.ElectronicSupportDocument.SupplierThirdPartyId) Then
                    supplierThirdParty = _thirdPartyRepository.GetThirdPartyById(SupportDocumentAdjustmentNote.ElectronicSupportDocument.SupplierThirdPartyId, False)
                    dictionarySupplierThirdParty.Add(SupportDocumentAdjustmentNote.ElectronicSupportDocument.SupplierThirdPartyId, supplierThirdParty)
                Else
                    supplierThirdParty = dictionarySupplierThirdParty(SupportDocumentAdjustmentNote.ElectronicSupportDocument.SupplierThirdPartyId)
                End If

                'cargamos al adquiriente
                If Not dictionaryCustomerThirdParty.ContainsKey(settingsAccount.IdDian) Then
                    customerThirdParty = _thirdPartyRepository.GetThirdPartyById(settingsAccount.IdDian, False)
                    dictionaryCustomerThirdParty.Add(settingsAccount.IdDian, customerThirdParty)
                Else
                    customerThirdParty = dictionaryCustomerThirdParty(settingsAccount.IdDian)
                End If

                'Proceso
                If currentStatus = 1 Then
                    'Si no se ha definido una ruta de archivo la generamos
                    If String.IsNullOrEmpty(SupportDocumentAdjustmentNote.FilePath) Then
                        SupportDocumentAdjustmentNote.FilePath = System.IO.Path.Combine(
                            Utils.GetPathElectronicDocuments(),
                            ServerSessionValues.Current.CurrentContainer,
                            SupportDocumentAdjustmentNote.Year,
                            SupportDocumentAdjustmentNote.DocumentDate.Month,
                            Utils.GetDescriptionTypeDocumentElectronic(TypeElectronicDocument.NotaDeAjuste),
                            SupportDocumentAdjustmentNote.Code)
                    End If

                    'Validamos el documento previo a enviar
                    currentStatus = Await Me.ValidateDIAN(TypeElectronicDocument.NotaDeAjuste, settingsAccount, customerThirdParty, currentStatus, Nothing, SupportDocumentAdjustmentNote, True)
                    If currentStatus <> 3 Then
                        'Generamos XML
                        Dim response = Me.GenerateXML(TypeElectronicDocument.NotaDeAjuste, settingsAccount, supplierThirdParty, customerThirdParty, Nothing, SupportDocumentAdjustmentNote)
                        If response.StateResult Then
                            'Enviamos a la DIAN
                            currentStatus = Me.SendToDIAN(TypeElectronicDocument.NotaDeAjuste, settingsAccount, supplierThirdParty, customerThirdParty, response.ObjectEmbbeded, currentStatus, Nothing, SupportDocumentAdjustmentNote)
                        Else
                            currentStatus = IIf(response.StateResultAux, currentStatus, 0)

                            _electronicSupportDocumentAdjustmentNoteDetailRepository.SaveEntity(New ElectronicSupportDocumentAdjustmentNoteDetail With
                            {
                                .ElectronicSupportDocumentAdjustmentNoteId = SupportDocumentAdjustmentNote.Id,
                                .Destination = 0, 'Error en el proceso
                                .CreationDate = DateTime.Now,
                                .Status = False,
                                .Response = Enums.ElectronicDocuments.StatusCode.BadRequest,
                                .Comments = "Errores de validación",
                                .ResponseData = response.Message
                            })
                        End If
                    End If
                End If

                'Si ya se envió pero no se ha validado
                If currentStatus = 2 Then
                    currentStatus = Await Me.ValidateDIAN(TypeElectronicDocument.NotaDeAjuste, settingsAccount, customerThirdParty, currentStatus, Nothing, SupportDocumentAdjustmentNote)
                End If
                SupportDocumentAdjustmentNote.StatusElectronic = currentStatus
            Else
                    _electronicSupportDocumentAdjustmentNoteDetailRepository.SaveEntity(New ElectronicSupportDocumentAdjustmentNoteDetail With
                    {
                        .ElectronicSupportDocumentAdjustmentNoteId = SupportDocumentAdjustmentNote.Id,
                        .Destination = 0, 'Error en el proceso
                        .CreationDate = DateTime.Now,
                        .Status = False,
                        .Response = Enums.ElectronicDocuments.StatusCode.BadRequest,
                        .Comments = "Error por control",
                        .ResponseData = "Documento supera el limite de intentos permitidos, se debe procesar manualmente"
                    })
                SupportDocumentAdjustmentNote.StatusElectronic = 0
            End If
        Catch ex As Exception
            'Si ocurre un error en el proceso lo almacenamos
            _electronicSupportDocumentAdjustmentNoteDetailRepository.SaveEntity(New ElectronicSupportDocumentAdjustmentNoteDetail With
                {
                    .ElectronicSupportDocumentAdjustmentNoteId = SupportDocumentAdjustmentNote.Id,
                    .Destination = 0,
                    .CreationDate = DateTime.Now,
                    .Status = False,
                    .Response = Enums.ElectronicDocuments.StatusCode.NotImplemented,
                    .Comments = "Error ejecutando el proceso",
                    .ResponseData = Utils.GetInnerExceptionMessageToString(ex)
                })
            errors.AppendLine(Utils.GetInnerExceptionMessageToString(ex))
        End Try

        _electronicSupportDocumentAdjustmentNoteRepository.SaveEntity(SupportDocumentAdjustmentNote)

        electronicSupportDocumentAdjustmentNoteUO.Commit()
        electronicSupportDocumentAdjustmentNoteDetailUO.Commit()

        Return New ActionResult(Of String) With {.StateResult = IIf(errors.Length = 0, True, False), .Message = errors.ToString()}
    End Function

    Private Function GenerateXML(typeDocument As TypeElectronicDocument, settingsAccount As GeneralLedgerSettings, supplierThirdParty As ThirdParty, customerThirdParty As ThirdParty,
                                 Optional electronicSupportDocument As ElectronicSupportDocument = Nothing, Optional electronicSupportDocumentAdjustmentNote As ElectronicSupportDocumentAdjustmentNote = Nothing) As ActionResult(Of String)
        Try
            Dim paymentMethods As List(Of SP_GetPaymentMethodByDocumentSupportId_Result) = Nothing
            Dim billingAuthorization As BillingAuthorization = Nothing

            Dim errors As New StringBuilder

            'Validación por unida operativa
            If settingsAccount Is Nothing Then
                errors.AppendLine("No se encontro parámetros de contabilidad en la unidad operativa del documento.")
            End If

            'Validaciones por tercero
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

            If (customerThirdParty.Person.Address Is Nothing OrElse customerThirdParty.Person.Address.Count = 0) Then
                errors.AppendLine("El tercero '" & customerThirdParty.Person.IdentificationNumber & "' no tiene parametrizada una dirección.")
            ElseIf (customerThirdParty.Person.Address.Where(Function(a) a.DepartmentId IsNot Nothing AndAlso a.CityId IsNot Nothing).Count = 0) Then
                errors.AppendLine("El tercero '" & customerThirdParty.Person.IdentificationNumber & "' no tiene parametrizada una dirección con ciudad y departamento.")
            End If

            'Validaciones por tipo
            If typeDocument = TypeElectronicDocument.DocumentoSoporte Then

                'Consulto los detalles del documento
                electronicSupportDocument.documentSupportDetails = _electronicSupportDocumentRepository.GetDocumentSupportDetails(electronicSupportDocument.Id)
                If electronicSupportDocument.documentSupportDetails Is Nothing AndAlso electronicSupportDocument.documentSupportDetails.Count = 0 Then
                    errors.AppendLine("No se encontraron detalles al documento electrónico")
                End If

                'Consulto los medios de pago
                paymentMethods = _electronicSupportDocumentRepository.GetDocumentSupportPaymentMethods(electronicSupportDocument.Id)
                If paymentMethods Is Nothing AndAlso paymentMethods.Count = 0 Then
                    errors.AppendLine("No se encontraron metodos de pago al documento electrónico")
                End If

                billingAuthorization = _billingAuthorizationRepository.GetBillingAuthorizationById(electronicSupportDocument.BillingAuthorizationId)
                If billingAuthorization Is Nothing Then
                    errors.AppendLine("No se encontro una autorización de factura al documento electrónico")
                End If

                If errors.Length > 0 Then
                    Return New ActionResult(Of String) With {.StateResult = False, .Message = errors.ToString()}
                End If

                'Cargo variables del documento soporte
                electronicSupportDocument.SupplierNit = supplierThirdParty.Nit
                electronicSupportDocument.IndigoCompanyNit = customerThirdParty.Nit
                electronicSupportDocument.SoftwarePin = settingsAccount.SupportDocumentPin
                electronicSupportDocument.Environment = IIf(settingsAccount.SupportDocumentEnvironment, 1, 2)
                electronicSupportDocument.dueDatePayment = paymentMethods.FirstOrDefault.PaymentDueDate

            ElseIf typeDocument = TypeElectronicDocument.NotaDeAjuste Then
                If errors.Length > 0 Then
                    Return New ActionResult(Of String) With {.StateResult = False, .Message = errors.ToString()}
                End If

                electronicSupportDocumentAdjustmentNote.ElectronicSupportDocument.SupplierNit = supplierThirdParty.Nit
                electronicSupportDocumentAdjustmentNote.ElectronicSupportDocument.IndigoCompanyNit = customerThirdParty.Nit
                electronicSupportDocumentAdjustmentNote.ElectronicSupportDocument.SoftwarePin = settingsAccount.SupportDocumentPin
                electronicSupportDocumentAdjustmentNote.ElectronicSupportDocument.Environment = IIf(settingsAccount.SupportDocumentEnvironment, 1, 2)

            End If

            'Genero el CUDS dependiendo del tipo
            If typeDocument = TypeElectronicDocument.DocumentoSoporte Then
                If electronicSupportDocument.CUDS <> electronicSupportDocument.GetCUDSCode() Then
                    electronicSupportDocument.CUDS = electronicSupportDocument.GetCUDSCode()
                    electronicSupportDocument.QR = electronicSupportDocument.GetQRCode
                End If
            Else
                If electronicSupportDocumentAdjustmentNote.CUDS <> electronicSupportDocumentAdjustmentNote.GetCUDSCode() Then
                    electronicSupportDocumentAdjustmentNote.CUDS = electronicSupportDocumentAdjustmentNote.GetCUDSCode()
                    electronicSupportDocumentAdjustmentNote.QR = electronicSupportDocumentAdjustmentNote.getQRDefinition()
                End If
            End If

            Dim ubl As New DIAN.UBL2_1.UBL2_1(customerThirdParty, typeDocument, settingsAccount, supplierThirdParty, _storage)
            ubl.ElectronicSupportDocument = electronicSupportDocument
            ubl.SupportDocumentAdjustmentNote = electronicSupportDocumentAdjustmentNote
            ubl.BillingAuthorization = billingAuthorization
            ubl.PaymentMethodsSupportDocument = paymentMethods

            Return ubl.GenerateXML
        Catch ex As SqlException
            Return New ActionResult(Of String) With {.StateResult = False, .StateResultAux = Utils.ValidateSqlCodeExceptions(ex, {1205}), .Message = Utils.GetInnerExceptionMessageToString(ex)}
        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Private Function SendToDIAN(typeDocument As TypeElectronicDocument, settingsAccount As GeneralLedgerSettings, supplierThirdParty As ThirdParty, customerThirdParty As ThirdParty, fileName As String, currentStatus As Byte,
                                Optional electronicSupportDocument As ElectronicSupportDocument = Nothing, Optional electronicSupportDocumentAdjustmentNote As ElectronicSupportDocumentAdjustmentNote = Nothing) As Byte
        Using client As New DistributedServices.DIAN.ServiceClient(settingsAccount.GetElectronicDocumentUrl(), settingsAccount.DigitalCertificate, settingsAccount.DigitalCertificateKey, _storage)
            Dim responseSend As ActionResult(Of DianResponse)
            If typeDocument = TypeElectronicDocument.DocumentoSoporte Then
                If settingsAccount.SupportDocumentEnvironment Then
                    responseSend = client.SendBill(electronicSupportDocument.FilePath, fileName)
                Else
                    responseSend = client.SendTestSet(settingsAccount.SupportDocumentTestSetId, electronicSupportDocument.FilePath, fileName)
                End If
            Else
                If settingsAccount.SupportDocumentEnvironment Then
                    responseSend = client.SendBill(electronicSupportDocumentAdjustmentNote.FilePath, fileName)
                Else
                    responseSend = client.SendTestSet(settingsAccount.SupportDocumentTestSetId, electronicSupportDocumentAdjustmentNote.FilePath, fileName)
                End If
            End If
            If responseSend.StateResult Then
                Dim documentResponse = responseSend.ObjectEmbbeded
                If documentResponse IsNot Nothing Then
                    Dim errorMessage As New StringBuilder
                    If settingsAccount.SupportDocumentEnvironment Then
                        If documentResponse.ErrorMessage IsNot Nothing AndAlso documentResponse.ErrorMessage.Any() Then
                            For Each message In documentResponse.ErrorMessage
                                errorMessage.AppendLine(message)
                            Next
                        End If

                        currentStatus = If(documentResponse.IsValid, 3, If(errorMessage.Length = 0 OrElse statusCodeErrors.Contains(documentResponse.StatusCode), currentStatus, 4))
                        If errorMessage.ToString.Contains("Documento procesado anteriormente") Then
                            currentStatus = 3

                            Dim fileNameElectronicDocument = String.Empty
                            If typeDocument = TypeElectronicDocument.DocumentoSoporte Then
                                fileNameElectronicDocument = electronicSupportDocument.GetFileName(customerThirdParty, typeDocument)
                                _storage.DeleteFile(electronicSupportDocument.FilePath, fileNameElectronicDocument)
                            Else 'nota de ajuste
                                fileNameElectronicDocument = electronicSupportDocumentAdjustmentNote.GetFileName(customerThirdParty, typeDocument)
                                _storage.DeleteFile(electronicSupportDocumentAdjustmentNote.FilePath, fileNameElectronicDocument)
                            End If
                        End If
                    Else
                        currentStatus = If(documentResponse.IsValid, 2, 4)
                        If typeDocument = TypeElectronicDocument.DocumentoSoporte Then
                            electronicSupportDocument.ZipKey = documentResponse.StatusMessage
                        Else
                            electronicSupportDocumentAdjustmentNote.ZipKey = documentResponse.StatusMessage
                        End If

                    End If
                    'Guardamos los detalles respectivos
                    If typeDocument = TypeElectronicDocument.DocumentoSoporte Then
                        'Actualizo la fecha de envío del documento soporte
                        electronicSupportDocument.ShippingDate = DateTime.Now

                        _electronicSupportDocumentDetailRepository.SaveEntity(New ElectronicSupportDocumentDetail With
                        {
                            .ElectronicSupportDocumentId = electronicSupportDocument.Id,
                            .CreationDate = DateTime.Now,
                            .Status = documentResponse.IsValid,
                            .Response = documentResponse.StatusCode,
                            .Comments = String.Format("{0} - {1}", documentResponse.StatusDescription, documentResponse.StatusMessage),
                            .ResponseData = errorMessage.ToString()
                        })
                    Else
                        'Actualizo la fecha de envío de la nota de ajuste
                        electronicSupportDocumentAdjustmentNote.ShippingDate = DateTime.Now

                        _electronicSupportDocumentAdjustmentNoteDetailRepository.SaveEntity(New ElectronicSupportDocumentAdjustmentNoteDetail With
                        {
                            .ElectronicSupportDocumentAdjustmentNoteId = electronicSupportDocumentAdjustmentNote.Id,
                            .CreationDate = DateTime.Now,
                            .Status = documentResponse.IsValid,
                            .Response = documentResponse.StatusCode,
                            .Comments = String.Format("{0} - {1}", documentResponse.StatusDescription, documentResponse.StatusMessage),
                            .ResponseData = errorMessage.ToString()
                        })
                    End If
                    'Agregamos al ApplicationResponse
                    Me.PostValidationEvent(settingsAccount, customerThirdParty, documentResponse, currentStatus, electronicSupportDocument, electronicSupportDocumentAdjustmentNote)
                End If
            Else
                currentStatus = 1
                If typeDocument = TypeElectronicDocument.DocumentoSoporte Then
                    electronicSupportDocument.ShippingDate = DateTime.Now

                    _electronicSupportDocumentDetailRepository.SaveEntity(New ElectronicSupportDocumentDetail With
                    {
                        .ElectronicSupportDocumentId = electronicSupportDocument.Id,
                        .CreationDate = DateTime.Now,
                        .Destination = IIf(settingsAccount.SupportDocumentEnvironment, 2, 1),
                        .Status = False,
                        .Comments = "Error al realizar el envio",
                        .ResponseData = responseSend.Message
                    })
                Else
                    electronicSupportDocumentAdjustmentNote.ShippingDate = DateTime.Now

                    _electronicSupportDocumentAdjustmentNoteDetailRepository.SaveEntity(New ElectronicSupportDocumentAdjustmentNoteDetail With
                    {
                        .ElectronicSupportDocumentAdjustmentNoteId = electronicSupportDocumentAdjustmentNote.Id,
                        .CreationDate = DateTime.Now,
                        .Destination = IIf(settingsAccount.SupportDocumentEnvironment, 2, 1),
                        .Status = False,
                        .Comments = "Error al realizar el envio",
                        .ResponseData = responseSend.Message
                    })
                End If
            End If
        End Using

        '_electronicSupportDocumentDetailRepository.SaveEntity(electronicSupportDocumentDetails)
        Return currentStatus
    End Function

    Private Function ValidateDIAN(TypeDocument As TypeElectronicDocument, settingsAccount As GeneralLedgerSettings, customerThirdParty As ThirdParty, currentStatus As Byte,
                                  Optional electronicDocumentSupport As ElectronicSupportDocument = Nothing, Optional electronicSupportDocumentAdjustmentNote As ElectronicSupportDocumentAdjustmentNote = Nothing,
                                  Optional beforeSend As Boolean = False)
        Using client As New DistributedServices.DIAN.ServiceClient(settingsAccount.GetElectronicDocumentUrl(), settingsAccount.DigitalCertificate, settingsAccount.DigitalCertificateKey, _storage)
            Dim responseValidate As ActionResult(Of DianResponse)
            If TypeDocument = TypeElectronicDocument.DocumentoSoporte Then
                'Enviamos documento soporte
                If settingsAccount.Environment Then
                    'Producción
                    responseValidate = client.GetStatus(electronicDocumentSupport.CUDS)
                Else
                    'Pruebas        
                    responseValidate = client.GetStatusZip(electronicDocumentSupport.ZipKey)
                End If
            Else
                'Enviamos notas de ajuste
                If settingsAccount.Environment Then
                    'Producción
                    responseValidate = client.GetStatus(electronicSupportDocumentAdjustmentNote.CUDS)
                Else
                    'Pruebas
                    responseValidate = client.GetStatusZip(electronicSupportDocumentAdjustmentNote.ZipKey)
                End If
            End If

            If responseValidate.StateResult Then
                Dim documentResponse = responseValidate.ObjectEmbbeded
                If documentResponse IsNot Nothing Then
                    Dim errorMessage As New StringBuilder
                    If documentResponse.ErrorMessage IsNot Nothing AndAlso documentResponse.ErrorMessage.Any() Then
                        For Each message In documentResponse.ErrorMessage
                            errorMessage.AppendLine(message)
                        Next
                    End If

                    currentStatus = If(documentResponse.IsValid, 3, If(errorMessage.Length = 0 OrElse statusCodeErrors.Contains(documentResponse.StatusCode), currentStatus, 4))
                    If errorMessage.ToString.Contains("Documento procesado anteriormente") Then
                        currentStatus = 3

                        Dim fileNameElectronicDocument = String.Empty
                        If TypeDocument = TypeElectronicDocument.DocumentoSoporte Then
                            fileNameElectronicDocument = electronicDocumentSupport.GetFileName(customerThirdParty, TypeDocument)
                            _storage.DeleteFile(electronicDocumentSupport.FilePath, fileNameElectronicDocument)
                        Else
                            fileNameElectronicDocument = electronicSupportDocumentAdjustmentNote.GetFileName(customerThirdParty, TypeDocument)
                            _storage.DeleteFile(electronicSupportDocumentAdjustmentNote.FilePath, fileNameElectronicDocument)
                        End If

                        If String.IsNullOrEmpty(documentResponse.StatusCode) OrElse documentResponse.StatusCode <> "66" Then
                            If beforeSend = False OrElse currentStatus = 3 Then
                                If TypeDocument = TypeElectronicDocument.DocumentoSoporte Then
                                    If beforeSend = False OrElse electronicDocumentSupport.StatusElectronic <> 3 Then
                                        'ValidationDate
                                        _electronicSupportDocumentDetailRepository.SaveEntity(New ElectronicSupportDocumentDetail With
                                    {
                                        .ElectronicSupportDocumentId = electronicDocumentSupport.Id,
                                        .Destination = 2, 'Validado por la DIAN
                                        .CreationDate = DateTime.Now,
                                        .Status = documentResponse.IsValid,
                                        .Response = documentResponse.StatusCode,
                                        .Comments = String.Format("{0} - {1}", documentResponse.StatusDescription, documentResponse.StatusMessage),
                                        .ResponseData = errorMessage.ToString()
                                    })
                                    End If
                                Else
                                    If beforeSend = False OrElse electronicSupportDocumentAdjustmentNote.StatusElectronic <> 3 Then
                                        'Registro validado por la DIAN
                                        _electronicSupportDocumentAdjustmentNoteDetailRepository.SaveEntity(New ElectronicSupportDocumentAdjustmentNoteDetail With
                                        {
                                            .ElectronicSupportDocumentAdjustmentNoteId = electronicSupportDocumentAdjustmentNote.Id,
                                            .Destination = 2, 'Validado
                                            .CreationDate = DateTime.Now,
                                            .Status = documentResponse.IsValid,
                                            .Response = documentResponse.StatusCode,
                                            .Comments = String.Format("{0} - {1}", documentResponse.StatusDescription, documentResponse.StatusMessage),
                                            .ResponseData = errorMessage.ToString()
                                        })
                                    End If
                                End If
                                Me.PostValidationEvent(settingsAccount, customerThirdParty, documentResponse, currentStatus, electronicDocumentSupport, electronicSupportDocumentAdjustmentNote)
                            End If
                        End If
                    End If
                End If
            End If
        End Using
        Return Task.FromResult(Of Byte)(currentStatus)
    End Function

    ''' <summary>
    ''' Metodo que se ejecuta una vez pasen todas las validaciones de la DIAN
    ''' </summary>
    Private Sub PostValidationEvent(settingsAccount As GeneralLedgerSettings, customerThirdParty As ThirdParty, documentResponse As DianResponse, currentStatus As Byte,
                                   Optional electronicSupportDocument As ElectronicSupportDocument = Nothing, Optional electronicSupportDocumentAdjusment As ElectronicSupportDocumentAdjustmentNote = Nothing)
        Try
            If currentStatus = 3 AndAlso documentResponse?.XmlBase64Bytes IsNot Nothing Then
                If settingsAccount.SupportDocumentEnvironment Then
                    Dim FileNameApplicationResponse As String = String.Empty
                    If electronicSupportDocument IsNot Nothing Then
                        'Guardamos el ApplicationResponse
                        FileNameApplicationResponse = electronicSupportDocument.GetFileName(customerThirdParty, TypeElectronicDocument.ApplicationResponse)
                        'Eliminamos el archivo anterior
                        _storage.DeleteFile(electronicSupportDocument.FilePath, FileNameApplicationResponse)
                        If _storage.ValidateIfNotExists(electronicSupportDocument.FilePath, FileNameApplicationResponse) Then
                            'Aqui se crea el archivo recibido
                            _storage.WriteFile(electronicSupportDocument.FilePath, FileNameApplicationResponse, documentResponse.XmlBase64Bytes)
                        End If
                    Else
                        FileNameApplicationResponse = electronicSupportDocumentAdjusment.GetFileName(customerThirdParty, TypeElectronicDocument.ApplicationResponse)
                        'Eliminamos el archivo anterior
                        _storage.DeleteFile(electronicSupportDocumentAdjusment.FilePath, FileNameApplicationResponse)
                        If _storage.ValidateIfNotExists(electronicSupportDocumentAdjusment.FilePath, FileNameApplicationResponse) Then
                            _storage.WriteFile(electronicSupportDocumentAdjusment.FilePath, FileNameApplicationResponse, documentResponse.XmlBase64Bytes)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            If electronicSupportDocument IsNot Nothing Then
                _electronicSupportDocumentDetailRepository.SaveEntity(New ElectronicSupportDocumentDetail With
                {
                    .ElectronicSupportDocumentId = electronicSupportDocument.Id,
                    .Destination = 3, 'Generación datos para el envío 
                    .CreationDate = DateTime.Now,
                    .Status = False,
                    .Response = Enums.ElectronicDocuments.StatusCode.NotImplemented,
                    .Comments = "Error ejecutando el proceso de post-validación",
                    .ResponseData = Utils.GetInnerExceptionMessageToString(ex)
                })
            Else
                _electronicSupportDocumentAdjustmentNoteDetailRepository.SaveEntity(New ElectronicSupportDocumentAdjustmentNoteDetail With
                {
                    .ElectronicSupportDocumentAdjustmentNoteId = electronicSupportDocumentAdjusment.Id,
                    .Destination = 3,
                    .CreationDate = DateTime.Now,
                    .Status = False,
                    .Response = Enums.ElectronicDocuments.StatusCode.NotImplemented,
                    .Comments = "Error ejecutando el proceso de post-validación",
                    .ResponseData = Utils.GetInnerExceptionMessageToString(ex)
                })
            End If
        End Try
    End Sub
#End Region
#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            Me._billingAuthorizationRepository = Nothing
            Me._electronicSupportDocumentRepository = Nothing
            Me._electronicSupportDocumentDetailRepository = Nothing
            Me._operatingUnitRepository = Nothing
            Me._settingsAccountRepository = Nothing
            Me._thirdPartyRepository = Nothing
            Me._electronicSupportDocumentAdjustmentNoteRepository = Nothing
            Me._electronicSupportDocumentAdjustmentNoteDetailRepository = Nothing

        End If
        disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
    End Sub
#End Region


End Class
