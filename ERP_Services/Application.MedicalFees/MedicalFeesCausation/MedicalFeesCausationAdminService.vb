'***********************************************************************
' Assembly         : Application.MedicalFees
' Author           : Carlos Mario Arias Rubiano
' Created          : 16/12/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.Entity.Core
Imports System.Transactions
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Crystal
Imports Domain.Entities
Imports Domain.Entities.Service
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

Public Class MedicalFeesCausationAdminService
    Implements IMedicalFeesCausationAdminService

#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _medicalFeesCausationRepository As IMedicalFeesCausationRepository

    ''' <summary>
    ''' Repositorio de contratos profesionales de la salud
    ''' </summary>
    ''' <remarks></remarks>
    Private _medicalFeesContractRepository As IMedicalFeesContractRepository

    ''' <summary>
    ''' Repositorio de contratos
    ''' </summary>
    ''' <remarks></remarks>
    Private _contractRepository As IContractRepository

    ''' <summary>
    ''' Repositorio de manual tarifario
    ''' </summary>
    ''' <remarks></remarks>
    Private _rateManualRepository As IRateManualRepository

    ''' <summary>
    ''' Repositorio del medico
    ''' </summary>
    ''' <remarks></remarks>
    Private _healthProfessionalRepository As IHealthProfessionalRepository

    ''' <summary>
    ''' Repositorio de notas de causacion de honorarios medicos
    ''' </summary>
    ''' <remarks></remarks>
    Private _medicalFeesNotesRepository As IMedicalFeesNoteRepository

    ''' <summary>
    ''' Repositorio para los detalles de la liquidacion
    ''' </summary>
    ''' <remarks></remarks>
    Private _medicalFeesLiquidationDetail As IMedicalFeesLiquidationDetailRepository

    ''' <summary>
    ''' Repositorio para entidades cups
    ''' </summary>
    ''' <remarks></remarks>
    Private _cupsEntityRepository As ICupsEntityRepository

    ''' <summary>
    ''' Repositorio para ipsService
    ''' </summary>
    ''' <remarks></remarks>
    Private _ipsServiceRepository As IIPSServicesRepository

    ''' <summary>
    ''' Repositorio para el detalle de la orden de servicio
    ''' </summary>
    ''' <remarks></remarks>
    Private _serviceOrderDetailRepository As IServiceOrderDetailRepository

    ''' <summary>
    ''' Repositorio para el detalle quirurgico de la orden de servicio
    ''' </summary>
    ''' <remarks></remarks>
    Private _serviceOrderDetailSurgicalRepository As IServiceOrderDetailSurgicalRepository

    ''' <summary>
    ''' Repositorio para billing
    ''' </summary>
    ''' <remarks></remarks>
    Private _billingService As IBillingServices

    ''' <summary>
    ''' Repositorio para contratos
    ''' </summary>
    ''' <remarks></remarks>
    Private _contractService As IContractServices

    ''' <summary>
    ''' Repositorio de homologaciones cups
    ''' </summary>
    ''' <remarks></remarks>
    Private _cupsHomologationRepository As ICupsHomologationRepository

    ''' <summary>
    ''' Repositorio de detalles quirurgico de manual tarifario
    ''' </summary>
    ''' <remarks></remarks>
    Private _rateManualDetailSurgicalRepository As IRateManualDetailSurgicalRepository

    ''' <summary>
    ''' Repositorio para detalles de procedimientos quirugicos
    ''' </summary>
    ''' <remarks></remarks>
    Private _surgicalProcedureServiceRepository As ISurgicalProcedureServiceRepository

    ''' <summary>
    ''' Repositorio para los detalles de contratos del médico
    ''' </summary>
    ''' <remarks></remarks>
    Private _healthProfessionalContractRepository As IHealthProfessionalContractRepository
    Private ReadOnly _medicalFeesContractAdminService As IMedicalFeesContractAdminService
    Private ReadOnly _viewListNoSurgicalRepository As IViewListNoSurgicalRepository
    Private ReadOnly _viewListDiagnosticImagingRepository As IViewListDiagnosticImagingRepository
    Private ReadOnly _viewListDiagnosticImagingAmbulatoryRepository As IViewListDiagnosticImagingAmbulatoryRepository
    Private ReadOnly _viewListSurgicalAndPackageRepository As IViewListSurgicalAndPackageRepository
    Private ReadOnly _causationPendignRepository As ICausationPendingRepository
    Private ReadOnly _thirdPartyRepository As IThirdPartyRepository
#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal medicalFeesCausationRepository As IMedicalFeesCausationRepository, ByVal medicalFeesContractRepository As IMedicalFeesContractRepository, ByVal contractRepository As IContractRepository,
                   rateManualRepository As IRateManualRepository, healthProfessionalRepository As IHealthProfessionalRepository, ByVal medicalFeesNotesRepository As IMedicalFeesNoteRepository,
                   medicalFeesLiquidationDetail As IMedicalFeesLiquidationDetailRepository, cupsEntityRepository As ICupsEntityRepository, ipsServiceRepository As IIPSServicesRepository,
                    serviceOrderDetailRepository As IServiceOrderDetailRepository, serviceOrderDetailSurgicalRepository As IServiceOrderDetailSurgicalRepository, billingService As IBillingServices,
                    contractService As IContractServices, cupsHomologationRepository As ICupsHomologationRepository, rateManualDetailSurgicalRepository As IRateManualDetailSurgicalRepository,
                    surgicalProcedureServiceRepository As ISurgicalProcedureServiceRepository, healthProfessionalContractRepository As IHealthProfessionalContractRepository,
                   viewListNoSurgicalRepository As IViewListNoSurgicalRepository, invoiceRepository As IInvoiceRepository,
                   viewListDiagnosticImagingRepository As IViewListDiagnosticImagingRepository, viewListDiagnosticImagingAmbulatoryRepository As IViewListDiagnosticImagingAmbulatoryRepository,
                   viewListSurgicalAndPackageRepository As IViewListSurgicalAndPackageRepository,
                   medicalFeesContractAdminService As IMedicalFeesContractAdminService,
                   causationPendignRepository As ICausationPendingRepository, thirdPartyRepository As IThirdPartyRepository)
        If medicalFeesCausationRepository Is Nothing Then
            Throw New ArgumentNullException("medicalFeesCausationRepository Vacio")
        End If
        If medicalFeesContractRepository Is Nothing Then
            Throw New ArgumentNullException("medicalFeesContractRepository Vacio")
        End If
        If contractRepository Is Nothing Then
            Throw New ArgumentNullException("contractRepository Vacio")
        End If
        If rateManualRepository Is Nothing Then
            Throw New ArgumentNullException("rateManualRepository Vacio")
        End If
        If healthProfessionalRepository Is Nothing Then
            Throw New ArgumentNullException("healthProfessionalRepository Vacio")
        End If
        If medicalFeesNotesRepository Is Nothing Then
            Throw New ArgumentNullException("medicalFeesNotesRepository Vacio")
        End If
        If medicalFeesLiquidationDetail Is Nothing Then
            Throw New ArgumentNullException("medicalFeesLiquidationDetail Vacio")
        End If
        If cupsEntityRepository Is Nothing Then
            Throw New ArgumentNullException("cupsEntityRepository Vacio")
        End If
        If ipsServiceRepository Is Nothing Then
            Throw New ArgumentNullException("ipsServiceRepository Vacio")
        End If
        If serviceOrderDetailRepository Is Nothing Then
            Throw New ArgumentNullException("serviceOrderDetail Vacio")
        End If
        If serviceOrderDetailSurgicalRepository Is Nothing Then
            Throw New ArgumentNullException("serviceOrderDetailSurgical Vacio")
        End If
        If billingService Is Nothing Then
            Throw New ArgumentNullException("billingService Vacio")
        End If
        If contractService Is Nothing Then
            Throw New ArgumentNullException("contractService Vacio")
        End If
        If cupsHomologationRepository Is Nothing Then
            Throw New ArgumentNullException("cupsHomologationRepository Vacio")
        End If
        If rateManualDetailSurgicalRepository Is Nothing Then
            Throw New ArgumentNullException("rateManualDetailSurgicalRepository Vacio")
        End If
        If surgicalProcedureServiceRepository Is Nothing Then
            Throw New ArgumentNullException("surgicalProcedureServiceRepository Vacio")
        End If
        If healthProfessionalContractRepository Is Nothing Then
            Throw New ArgumentNullException("healthProfessionalContractRepository Vacio")
        End If
        _medicalFeesCausationRepository = medicalFeesCausationRepository
        _medicalFeesContractRepository = medicalFeesContractRepository
        _contractRepository = contractRepository
        _rateManualRepository = rateManualRepository
        _healthProfessionalRepository = healthProfessionalRepository
        _medicalFeesNotesRepository = medicalFeesNotesRepository
        _medicalFeesLiquidationDetail = medicalFeesLiquidationDetail
        _cupsEntityRepository = cupsEntityRepository
        _ipsServiceRepository = ipsServiceRepository
        _serviceOrderDetailRepository = serviceOrderDetailRepository
        _serviceOrderDetailSurgicalRepository = serviceOrderDetailSurgicalRepository
        _billingService = billingService
        _contractService = contractService
        _cupsHomologationRepository = cupsHomologationRepository
        _rateManualDetailSurgicalRepository = rateManualDetailSurgicalRepository
        _surgicalProcedureServiceRepository = surgicalProcedureServiceRepository
        _healthProfessionalContractRepository = healthProfessionalContractRepository
        _viewListNoSurgicalRepository = viewListNoSurgicalRepository
        _viewListDiagnosticImagingRepository = viewListDiagnosticImagingRepository
        _viewListDiagnosticImagingAmbulatoryRepository = viewListDiagnosticImagingAmbulatoryRepository
        _viewListSurgicalAndPackageRepository = viewListSurgicalAndPackageRepository
        _medicalFeesContractAdminService = medicalFeesContractAdminService
        _causationPendignRepository = causationPendignRepository
        _thirdPartyRepository = thirdPartyRepository
    End Sub

#End Region

#Region "Methods"

    Public Function ListViewSurgicalAndPackageByInvoiceNumber(invoiceNumbers As List(Of String)) As List(Of ViewListSurgicalAndPackage) Implements IMedicalFeesCausationAdminService.ListViewSurgicalAndPackageByInvoiceNumber
        Dim query = _viewListSurgicalAndPackageRepository.ExecuteQueryDR(Of ViewListSurgicalAndPackage)($"SELECT * FROM [Billing].[ViewListSurgicalAndPackage] WHERE InvoiceNumber in ({String.Join(",", invoiceNumbers.Select(Function(x) $"'{x}'"))})", {})
        Return query?.ToList()
    End Function

    Public Function GetViewListNoSurgical(invoiceNumbers As List(Of String)) As List(Of ViewListNoSurgical) Implements IMedicalFeesCausationAdminService.GetViewListNoSurgical
        Dim query = _viewListNoSurgicalRepository.ExecuteQueryDR(Of ViewListNoSurgical)($"SELECT * FROM [Billing].[ViewListNoSurgical] WHERE InvoiceNumber in ({String.Join(",", invoiceNumbers.Select(Function(x) $"'{x}'"))})", {})
        Return query?.ToList()
    End Function

    Public Function GetCupsEntityIdsByServiceTypes(cupsEntityIds As List(Of Integer), serviceTypes As List(Of Byte)) As List(Of Integer) Implements IMedicalFeesCausationAdminService.GetCupsEntityIdsByServiceTypes
        Return _cupsEntityRepository.GetCupsEntityIdsByServiceTypes(cupsEntityIds, serviceTypes)
    End Function

    Public Function GetViewListDiagnosticImaging(serviceOrderDetailId As Integer) As List(Of ViewListDiagnosticImaging) Implements IMedicalFeesCausationAdminService.GetViewListDiagnosticImaging
        Return _viewListDiagnosticImagingRepository.GetByFilter(Function(m) m.ServiceOrderDetailId = serviceOrderDetailId, tracking:=False)?.ToList()
    End Function

    Public Function GetViewListDiagnosticImagingAmbulatory(serviceOrderDetailId As Integer) As List(Of ViewListDiagnosticImagingAmbulatory) Implements IMedicalFeesCausationAdminService.GetViewListDiagnosticImagingAmbulatory
        Return _viewListDiagnosticImagingAmbulatoryRepository.GetByFilter(Function(m) m.ServiceOrderDetailId = serviceOrderDetailId, tracking:=False)?.ToList()
    End Function

    ''' <summary>
    ''' Genera una clave única para un candidato de auto-causación.
    ''' Wrapper sobre BuildCausationKey para el tipo SP_GetCandidatesForAutoCausation_Result.
    ''' </summary>
    Private Function BuildCandidateKey(candidate As SP_GetCandidatesForAutoCausation_Result) As String
        Return BuildCausationKey(candidate.ServiceOrderDetailId, candidate.ServiceOrderDetailSurgicalId)
    End Function

    ''' <summary>
    ''' Genera una clave única con formato "{ServiceOrderDetailId}_{ServiceOrderDetailSurgicalId}".
    ''' Usado para identificar unívocamente un candidato o causación en lookups y HashSets.
    ''' </summary>
    Private Function BuildCausationKey(serviceOrderDetailId As Integer, serviceOrderDetailSurgicalId As Integer?) As String
        Return $"{serviceOrderDetailId}_{If(serviceOrderDetailSurgicalId.HasValue AndAlso serviceOrderDetailSurgicalId.Value > 0, serviceOrderDetailSurgicalId.Value, 0)}"
    End Function

    ''' <summary>
    ''' Crea una entidad MedicalFeesCausation a partir de un candidato de auto-causación.
    ''' Usado exclusivamente en ProcessUnrecognizedCausations para causaciones sin factura.
    ''' </summary>
    Private Function CreateMedicalFeesCausationFromCandidate(
        candidate As SP_GetCandidatesForAutoCausation_Result,
        amountPayable As Decimal,
        medicalFeesContractId As Integer) As MedicalFeesCausation

        Return New MedicalFeesCausation With {
            .AdmissionNumber = candidate.AdmissionNumber,
            .PatientCode = candidate.PatientCode,
            .HealthProfessionalCode = If(candidate.PerformsHealthProfessionalCode, String.Empty).Trim(),
            .ThirdPartyId = candidate.ThirdPartyId.Value,
            .MedicalFeesContractId = medicalFeesContractId,
            .ServiceOrderId = candidate.ServiceOrderId,
            .ServiceOrderDetailId = candidate.ServiceOrderDetailId,
            .ServiceOrderDetailSurgicalId = If(candidate.ServiceOrderDetailSurgicalId.HasValue AndAlso candidate.ServiceOrderDetailSurgicalId.Value > 0,
                                               candidate.ServiceOrderDetailSurgicalId.Value, CType(Nothing, Integer?)),
            .AmountPayable = amountPayable,
            .MedicalFeesContractValue = amountPayable,
            .InvoiceQuantity = candidate.InvoicedQuantity,
            .TotalAmountPayable = amountPayable * candidate.InvoicedQuantity,
            .PercentageCashed = 100,
            .MedicalFeePaid = False,
            .InvoiceDetailId = Nothing,
            .CausationDate = Date.Now
        }
    End Function

    ''' <summary>
    ''' Extrae y valida los valores de causación desde el resultado de CausedValue.
    ''' Parsea el monto (Decimal) y el Id del contrato (Integer).
    ''' Retorna False con mensaje de error si algún valor es inválido o está ausente.
    ''' </summary>
    Private Function TryGetAutoCausationValues(
        causedValueResult As ActionResult(Of List(Of CupsHomologation)),
        ByRef amountPayable As Decimal,
        ByRef medicalFeesContractId As Integer,
        ByRef errorMessage As String) As Boolean

        If causedValueResult Is Nothing Then
            errorMessage = "No se recibió respuesta al calcular el valor de causación."
            Return False
        End If

        If causedValueResult.MessageResult Is Nothing OrElse causedValueResult.MessageResult.Count = 0 OrElse String.IsNullOrWhiteSpace(causedValueResult.MessageResult(0)) Then
            errorMessage = "La respuesta del cálculo no contiene el valor causado."
            Return False
        End If

        If Not Decimal.TryParse(causedValueResult.MessageResult(0), Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, amountPayable) _
           AndAlso Not Decimal.TryParse(causedValueResult.MessageResult(0), Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, amountPayable) Then
            errorMessage = $"No fue posible interpretar el valor causado ({causedValueResult.MessageResult(0)})."
            Return False
        End If

        If String.IsNullOrWhiteSpace(causedValueResult.Message) OrElse Not Integer.TryParse(causedValueResult.Message, medicalFeesContractId) Then
            errorMessage = $"No fue posible interpretar el contrato de honorarios ({causedValueResult.Message})."
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Obtiene el mensaje de error de un ActionResult, buscando en Message y MessageResult.
    ''' Retorna el fallbackMessage si no encuentra ningún mensaje válido.
    ''' </summary>
    Private Function GetActionResultMessage(result As ActionResult(Of List(Of MedicalFeesCausation)), fallbackMessage As String) As String
        If result Is Nothing Then
            Return fallbackMessage
        End If

        If Not String.IsNullOrWhiteSpace(result.Message) Then
            Return result.Message
        End If

        If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Any() AndAlso Not String.IsNullOrWhiteSpace(result.MessageResult(0)) Then
            Return result.MessageResult(0)
        End If

        Return fallbackMessage
    End Function

    ''' <summary>
    ''' Obtiene los IDs de ThirdParty válidos (existen y están activos) mediante UNA consulta masiva
    ''' Optimización crítica: En vez de N consultas (una por item), hace 1 sola consulta
    ''' </summary>
    Public Function GetValidThirdPartyIds(thirdPartyIds As List(Of Integer)) As List(Of Integer) Implements IMedicalFeesCausationAdminService.GetValidThirdPartyIds
        If thirdPartyIds Is Nothing OrElse Not thirdPartyIds.Any() Then
            Return New List(Of Integer)()
        End If

        Try
            ' 🚀 UNA SOLA CONSULTA: Obtener todos los ThirdParty válidos en un solo viaje a la BD
            Dim validIds = _thirdPartyRepository.Query(
                Function(tp) thirdPartyIds.Contains(tp.Id),
                tracking:=False
            )?.Select(Function(tp) tp.Id).ToList()

            Debug.WriteLine($"✅ Cache ThirdParty: {thirdPartyIds.Count} solicitados, {validIds?.Count} válidos encontrados")

            Return If(validIds, New List(Of Integer)())
        Catch ex As Exception
            Debug.WriteLine($"❌ Error obteniendo cache de ThirdParty: {ex.Message}")
            Return New List(Of Integer)()
        End Try
    End Function

    ''' <summary>
    ''' Elimina la entidad
    ''' </summary>
    ''' <param name="ListInfo">Item1=MedicalFeesCausationId, Item2=Position</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteMedicalFeesCausation(ListInfo As List(Of Tuple(Of Integer, Integer)), audit As AuditMessage, Company As String) As ActionResult(Of List(Of Tuple(Of String, Integer, Integer))) Implements IMedicalFeesCausationAdminService.DeleteMedicalFeesCausation
        If ListInfo Is Nothing OrElse ListInfo.Count = 0 Then
            Throw New ArgumentNullException("ListInfo")
        End If

        Using cnx As New System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, Company, False))
            cnx.Open()
            Dim tx As System.Data.SqlClient.SqlTransaction = cnx.BeginTransaction()
            Dim command As New System.Data.SqlClient.SqlCommand("", cnx, tx)
            command.CommandTimeout = 30000
            command.CommandType = CommandType.Text

            Try
                'Listado de errores
                Dim ListErrors As New List(Of Tuple(Of String, Integer, Integer))

                'Se consulta si el medicalFeesCausation ya existe en una liquidación guardada o confirmada
                Dim ListMedicalFeesLiquidationDetail = _medicalFeesLiquidationDetail.ValidateMedicalFeesCausationIdContainInMFLDConfirmedOrRegister(ListInfo)
                If ListMedicalFeesLiquidationDetail IsNot Nothing AndAlso ListMedicalFeesLiquidationDetail.Count > 0 Then
                    ListMedicalFeesLiquidationDetail.ForEach(Sub(item)
                                                                 Dim info = (From t In ListInfo Where t.Item1 = item.MedicalFeesCausationId Select t).FirstOrDefault
                                                                 If info IsNot Nothing Then
                                                                     ListErrors.Add(New Tuple(Of String, Integer, Integer)("El item " + info.Item2.ToString + " no se puede eliminar porque ya existe en una liquidación.", 2, 0))
                                                                     ListInfo.Remove(info)
                                                                 End If
                                                             End Sub)
                End If

                Dim operatorIn As String = String.Empty
                Dim counter As Integer = 0

                If ListInfo IsNot Nothing AndAlso ListInfo.Count > 0 Then
                    'Se consulta si el medicalFeesCausation existe en una liquidación anulada para eliminar esos items y poder eliminar la causación
                    ListMedicalFeesLiquidationDetail = _medicalFeesLiquidationDetail.ValidateMedicalFeesCausationIdContainInMFLDAnnular(ListInfo)
                    If ListMedicalFeesLiquidationDetail IsNot Nothing AndAlso ListMedicalFeesLiquidationDetail.Count > 0 Then

                        For Each item In ListMedicalFeesLiquidationDetail
                            If counter <> 0 Then
                                operatorIn += ","
                            Else
                                counter = 1
                            End If
                            operatorIn += item.Id.ToString
                        Next

                        command.CommandText = "DELETE  FROM [MedicalFees].[MedicalFeesLiquidationDetail] WHERE [Id] in (" + operatorIn + ")"
                        command.ExecuteNonQuery()
                    End If

                    operatorIn = String.Empty
                    counter = 0
                    For Each item In ListInfo
                        If counter <> 0 Then
                            operatorIn += ","
                        Else
                            counter = 1
                        End If
                        operatorIn += item.Item1.ToString
                    Next

                    command.CommandText = "DELETE  FROM [MedicalFees].[MedicalFeesCausation] WHERE [Id] in (" + operatorIn + ")"
                    command.ExecuteNonQuery()

                    ListInfo.ForEach(Sub(item)
                                         ListErrors.Add(New Tuple(Of String, Integer, Integer)("El item " + item.Item2.ToString + " se eliminó satisfactoriamente.", 1, item.Item1))
                                     End Sub)

                    tx.Commit()
                End If

                Return New ActionResult(Of List(Of Tuple(Of String, Integer, Integer))) With {.StateResult = True, .ObjectEmbbeded = ListErrors}
            Catch ex As OptimisticConcurrencyException
                tx.Rollback()
                Return New ActionResult(Of List(Of Tuple(Of String, Integer, Integer))) With {.StateResult = False, .Message = "-999"}
            Catch ex As UpdateException
                tx.Rollback()
                Return New ActionResult(Of List(Of Tuple(Of String, Integer, Integer))) With {.StateResult = False, .Message = "-000"}
            Catch ex As Exception
                tx.Rollback()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of List(Of Tuple(Of String, Integer, Integer))) With {.StateResult = False, .Message = ex.Message}
            Finally
                cnx.Close()
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMedicalFeesCausationById(id As Integer, audit As AuditMessage) As ActionResult(Of MedicalFeesCausation) Implements IMedicalFeesCausationAdminService.GetMedicalFeesCausationById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim MedicalFeesCausation As MedicalFeesCausation = Me._medicalFeesCausationRepository.GetMedicalFeesCausationById(id)
            If MedicalFeesCausation IsNot Nothing AndAlso MedicalFeesCausation.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of MedicalFeesCausation)(MedicalFeesCausation, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of MedicalFeesCausation) With {.StateResult = True, .ObjectEmbbeded = MedicalFeesCausation}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MedicalFeesCausation) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza la entidad
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <remarks></remarks>
    Public Function SaveMedicalFeesCausation(ListMedicalFeesCausation As List(Of MedicalFeesCausation), ByVal ListMedicalFeesNotes As List(Of MedicalFeesNote), ByVal ListDeleteMedicalFeesNotes As List(Of MedicalFeesNote), audit As AuditMessage) As ActionResult(Of List(Of MedicalFeesCausation)) Implements IMedicalFeesCausationAdminService.SaveMedicalFeesCausation
        If ListMedicalFeesCausation Is Nothing Then
            Throw New ArgumentNullException("ListMedicalFeesCausation")
        End If
        Dim unitOfWork As IUnitWork = Me._medicalFeesCausationRepository.UnitWork
        Dim unitOfWorkMedicalFeesNotes As IUnitWork = Me._medicalFeesNotesRepository.UnitWork
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                ' ⚠️ VALIDACIÓN DEFENSIVA: valida en una sola consulta que TODOS los MedicalFeesContractId
                ' referenciados por la lista existan antes de intentar guardar/commitear cualquier registro.
                ' Esto intercepta el problema ANTES del unitOfWork.Commit() (línea donde EF ejecuta el INSERT real
                ' y donde ocurre hoy la excepción reportada como línea 550 por el mapeo de sequence points del PDB).
                Dim distinctContractIds = ListMedicalFeesCausation _
                .Where(Function(x) x.MedicalFeesContractId.HasValue) _
                .Select(Function(x) x.MedicalFeesContractId.Value) _
                .Distinct() _
                .ToList()

                If distinctContractIds.Any() Then
                    Dim existingContractIds = _medicalFeesContractRepository _
                    .Query(Function(c) distinctContractIds.Contains(c.Id), tracking:=False) _
                    ?.Select(Function(c) c.Id) _
                    .ToList()

                    If existingContractIds Is Nothing Then existingContractIds = New List(Of Integer)()

                    Dim missingContractIds = distinctContractIds.Except(existingContractIds).ToList()

                    If missingContractIds.Any() Then
                        Dim affectedItems = ListMedicalFeesCausation _
                        .Where(Function(x) x.MedicalFeesContractId.HasValue AndAlso missingContractIds.Contains(x.MedicalFeesContractId.Value)) _
                        .Select(Function(x) $"ServiceOrderDetailId: {x.ServiceOrderDetailId} (ContratoId: {x.MedicalFeesContractId})") _
                        .ToList()

                        Transaction.Dispose()
                        Return New ActionResult(Of List(Of MedicalFeesCausation)) With {
                        .StateResult = False,
                        .MessageResult = {$"No se puede guardar la causación: el/los contrato(s) de honorarios ({String.Join(", ", missingContractIds)}) no existe(n). Items afectados: {String.Join("; ", affectedItems)}"}.ToList()
                    }
                    End If
                End If

                'Guardo las causaciones de honorarios medicos
                For Each itemMedicalFeesCausation As MedicalFeesCausation In ListMedicalFeesCausation
                    Dim auxMedicalFeesCausation As MedicalFeesCausation = Nothing
                    Dim auditProcess As IndigoAuditSimpleEntity(Of MedicalFeesCausation)
                    Dim status As Integer
                    Dim medicalFeesCausationSave As MedicalFeesCausation

                    If itemMedicalFeesCausation.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        itemMedicalFeesCausation.CreationUser = audit.CodeUser
                        itemMedicalFeesCausation.CreationDate = DateTime.Now
                        '------------------ Se llena los nuevos campos que se utiliza en el modulo de tamayo ------------------
                        'itemMedicalFeesCausation.CausationDate = itemMedicalFeesCausation.CreationDate
                        itemMedicalFeesCausation.Status = 1
                        itemMedicalFeesCausation.InvoiceReversal = False
                        itemMedicalFeesCausation.ObjectionAccepted = False
                        itemMedicalFeesCausation.ValueObjectionAccepted = 0
                        itemMedicalFeesCausation.ReassessmentForReversal = False
                        itemMedicalFeesCausation.ReassessmentForObjection = False
                        '------------------------------------------------------------------------------------------------------
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert

                        medicalFeesCausationSave = itemMedicalFeesCausation
                    Else
                        Dim medicalFeesCausation As MedicalFeesCausation = _medicalFeesCausationRepository.GetMedicalFeesCausationById(itemMedicalFeesCausation.Id)
                        auxMedicalFeesCausation = medicalFeesCausation.OriginalValue
                        medicalFeesCausation.CausationDate = itemMedicalFeesCausation.CausationDate
                        medicalFeesCausation.AmountPayable = itemMedicalFeesCausation.AmountPayable
                        medicalFeesCausation.MedicalFeesContractId = itemMedicalFeesCausation.MedicalFeesContractId
                        medicalFeesCausation.TotalAmountPayable = itemMedicalFeesCausation.TotalAmountPayable
                        medicalFeesCausation.ModificationUser = audit.CodeUser
                        medicalFeesCausation.ModificationDate = DateTime.Now
                        medicalFeesCausation.MarkAsModified()
                        status = Infrastructure.CrossCutting.Audit.Actions.Update

                        medicalFeesCausationSave = medicalFeesCausation
                    End If

                    Me._medicalFeesCausationRepository.SaveEntity(medicalFeesCausationSave)
                    unitOfWork.Commit()
                    auditProcess = New IndigoAuditSimpleEntity(Of MedicalFeesCausation)(itemMedicalFeesCausation, audit, status, auxMedicalFeesCausation)
                    auditProcess.Execute()

                    'Se marca la entidad como sin cambios
                    itemMedicalFeesCausation.MarkAsUnchanged()
                Next

                'Guardo las notas de causaciones
                If ListMedicalFeesNotes IsNot Nothing AndAlso ListMedicalFeesNotes.Count > 0 Then
                    For Each itemNote As MedicalFeesNote In ListMedicalFeesNotes
                        If itemNote.ChangeTracker.State <> ObjectState.Unchanged Then
                            If itemNote.ChangeTracker.State = ObjectState.Added Then
                                itemNote.CreationUser = audit.CodeUser
                                itemNote.CreationDate = DateTime.Now
                            ElseIf itemNote.ChangeTracker.State = ObjectState.Modified Then
                                itemNote.ModificationUser = audit.CodeUser
                                itemNote.ModificationDate = DateTime.Now
                            End If
                            _medicalFeesNotesRepository.SaveEntity(itemNote)
                            unitOfWorkMedicalFeesNotes.Commit()
                        End If
                    Next
                End If
                'Elimino las notas de causaciones
                If ListDeleteMedicalFeesNotes IsNot Nothing AndAlso ListDeleteMedicalFeesNotes.Count > 0 Then
                    For Each itemDelete As MedicalFeesNote In ListDeleteMedicalFeesNotes
                        _medicalFeesNotesRepository.SaveEntity(itemDelete)
                        unitOfWorkMedicalFeesNotes.Commit()
                    Next
                End If

                Transaction.Complete()
                Return New ActionResult(Of List(Of MedicalFeesCausation)) With {.StateResult = True, .ObjectEmbbeded = Nothing}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                unitOfWorkMedicalFeesNotes.RollbackChanges()
                Transaction.Dispose()
                Return New ActionResult(Of List(Of MedicalFeesCausation)) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                unitOfWorkMedicalFeesNotes.RollbackChanges()
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of List(Of MedicalFeesCausation)) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
            End Try
        End Using
    End Function
    ''' <summary>
    ''' Guarda o actualiza la entidad MedicalFeesCausation de forma asincrona
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <remarks></remarks>
    Public Async Function SaveMedicalFeesCausationAsync(ListMedicalFeesCausation As List(Of MedicalFeesCausation), ByVal ListMedicalFeesNotes As List(Of MedicalFeesNote), ByVal ListDeleteMedicalFeesNotes As List(Of MedicalFeesNote), audit As AuditMessage) As Task(Of ActionResult(Of List(Of MedicalFeesCausation))) Implements IMedicalFeesCausationAdminService.SaveMedicalFeesCausationAsync
        If ListMedicalFeesCausation Is Nothing Then
            Throw New ArgumentNullException("ListMedicalFeesCausation")
        End If

        Dim unitOfWork As IUnitWork = Me._medicalFeesCausationRepository.UnitWork

        Using Transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() _
                With {.Timeout = TransactionManager.MaximumTimeout,
                        .IsolationLevel = IsolationLevel.ReadCommitted}, TransactionScopeAsyncFlowOption.Enabled)
            Try
                'Guardo las causaciones de honorarios medicos
                For Each itemMedicalFeesCausation As MedicalFeesCausation In ListMedicalFeesCausation
                    If itemMedicalFeesCausation.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        itemMedicalFeesCausation.CreationUser = audit.CodeUser
                        itemMedicalFeesCausation.CreationDate = DateTime.Now
                        '------------------ Se llena los nuevos campos que se utiliza en el modulo de tamayo ------------------
                        itemMedicalFeesCausation.Status = 1
                        itemMedicalFeesCausation.InvoiceReversal = False
                        itemMedicalFeesCausation.ObjectionAccepted = False
                        itemMedicalFeesCausation.ValueObjectionAccepted = 0
                        itemMedicalFeesCausation.ReassessmentForReversal = False
                        itemMedicalFeesCausation.ReassessmentForObjection = False
                        '------------------------------------------------------------------------------------------------------
                    Else
                        Dim medicalFeesCausation As MedicalFeesCausation = _medicalFeesCausationRepository.GetMedicalFeesCausationById(itemMedicalFeesCausation.Id)
                        medicalFeesCausation.CausationDate = itemMedicalFeesCausation.CausationDate
                        medicalFeesCausation.AmountPayable = itemMedicalFeesCausation.AmountPayable
                        medicalFeesCausation.MedicalFeesContractId = itemMedicalFeesCausation.MedicalFeesContractId
                        medicalFeesCausation.TotalAmountPayable = itemMedicalFeesCausation.TotalAmountPayable
                        medicalFeesCausation.ModificationUser = audit.CodeUser
                        medicalFeesCausation.ModificationDate = DateTime.Now
                        medicalFeesCausation.MarkAsModified()
                    End If
                Next

                Await _medicalFeesCausationRepository.SaveEntityMassiveAsync(ListMedicalFeesCausation)

                'Guardo las notas de causaciones
                If ListMedicalFeesNotes IsNot Nothing AndAlso ListMedicalFeesNotes.Count > 0 Then
                    For Each itemNote As MedicalFeesNote In ListMedicalFeesNotes
                        If itemNote.ChangeTracker.State <> ObjectState.Unchanged Then
                            If itemNote.ChangeTracker.State = ObjectState.Added Then
                                itemNote.CreationUser = audit.CodeUser
                                itemNote.CreationDate = DateTime.Now
                            ElseIf itemNote.ChangeTracker.State = ObjectState.Modified Then
                                itemNote.ModificationUser = audit.CodeUser
                                itemNote.ModificationDate = DateTime.Now
                            End If
                        End If
                    Next

                    Await _medicalFeesNotesRepository.SaveEntityMassiveAsync(ListMedicalFeesNotes)
                End If

                'Elimino las notas de causaciones
                If ListDeleteMedicalFeesNotes IsNot Nothing AndAlso ListDeleteMedicalFeesNotes.Count > 0 Then
                    Await _medicalFeesNotesRepository.SaveEntityMassiveAsync(ListDeleteMedicalFeesNotes)
                End If

                Transaction.Complete()

                Debug.WriteLine($"✅ Transacción completada exitosamente")

                Return New ActionResult(Of List(Of MedicalFeesCausation)) With {.StateResult = True, .ObjectEmbbeded = Nothing}
            Catch ex As OptimisticConcurrencyException
                Debug.WriteLine($"❌ OptimisticConcurrencyException: {ex.Message}")
                Return New ActionResult(Of List(Of MedicalFeesCausation)) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
            Catch ex As Exception
                Debug.WriteLine($"❌ Error guardando causaciones: {ex.Message}")
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of List(Of MedicalFeesCausation)) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Calcula el valor a causar
    ''' </summary>
    ''' <param name="CupsEntityId"></param>
    ''' <param name="CareGroupId"></param>
    ''' <param name="RateManualId"></param>
    ''' <param name="ValueTotal"></param>
    ''' <param name="presentation"></param>
    ''' <param name="IPSServiceId"></param>
    ''' <param name="IPSServiceCodeName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CausedValue(MedicalFeesCausationId As Integer, RateManualType As Integer, CupsEntityId As Integer, CareGroupId As Integer, RateManualId As Integer, ValueTotal As Decimal, presentation As Integer, IPSServiceId As Integer, IPSServiceCodeName As String,
                                healthProfessionalCode As String, thirdPartyDescription As String, IPSServiceIdParent As Integer?,
                                ServiceOrderDetailId As Integer, ServiceOrderDetailSurgicalId As Integer, MedicalFeesContractId As Integer,
                                Optional ListCupsHomologation As List(Of CupsHomologation) = Nothing) As ActionResult(Of List(Of CupsHomologation)) Implements IMedicalFeesCausationAdminService.CausedValue
        Dim medicalFeesServices As New MedicalFeesServices(_medicalFeesContractRepository, _contractRepository, _rateManualRepository, _healthProfessionalRepository, _cupsEntityRepository, _ipsServiceRepository,
                                                           _serviceOrderDetailRepository, _serviceOrderDetailSurgicalRepository, _billingService, _contractService, _cupsHomologationRepository,
                                                           _rateManualDetailSurgicalRepository, _surgicalProcedureServiceRepository, _healthProfessionalContractRepository, _medicalFeesLiquidationDetail)
        Return medicalFeesServices.CausedValue(MedicalFeesCausationId, RateManualType, CupsEntityId, CareGroupId, RateManualId, ValueTotal, presentation, IPSServiceId, IPSServiceCodeName, healthProfessionalCode, thirdPartyDescription, IPSServiceIdParent,
                                               ServiceOrderDetailId, ServiceOrderDetailSurgicalId, MedicalFeesContractId, ListCupsHomologation)
    End Function

    ''' <summary>
    ''' Causa un detalle de factura Qx
    ''' </summary>
    ''' <param name="invoiceDetail"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function CauseInvoiceQx(invoiceDetail As ViewListSurgicalAndPackage, audit As AuditMessage, Optional listCupsHomologations As List(Of CupsHomologation) = Nothing) As ActionResult(Of List(Of CupsHomologation)) Implements IMedicalFeesCausationAdminService.CauseInvoiceQx
        Try
            Dim res = GetCausationInvoiceQx(invoiceDetail, audit, listCupsHomologations)

            If res.StateResult Then
                Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                    Dim resSave = SaveMedicalFeesCausation({res.ObjectEmbbeded.causation}.ToList(), Nothing, Nothing, audit)

                    If Not resSave.StateResult Then
                        scope.Dispose()
                        Throw New IndigoValidationException(resSave.Message)
                    End If

                    scope.Complete()
                    Return New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = True}
                End Using
            Else
                ' Actualizar el campo Error en CausationPending cuando la causación falla
                UpdateCausationPendingErrorQx(invoiceDetail, res.Message)
            End If
            Return New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = False, .StatusCode = res.StatusCode, .ObjectEmbbeded = res.ObjectEmbbeded.homologations, .Message = res.Message}
        Catch ex As IndigoValidationException
            ' Actualizar el campo Error en CausationPending cuando ocurre una excepción
            UpdateCausationPendingErrorQx(invoiceDetail, ex.Message)
            Return New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = False, .Message = ex.Message}
        Catch ex As Exception
            ' Actualizar el campo Error en CausationPending cuando ocurre una excepción
            UpdateCausationPendingErrorQx(invoiceDetail, ex.Message)
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la causación para un detalle Qx
    ''' </summary>
    ''' <param name="invoiceDetail"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetCausationInvoiceQx(invoiceDetail As ViewListSurgicalAndPackage, audit As AuditMessage, Optional listCupsHomologations As List(Of CupsHomologation) = Nothing) As ActionResult(Of (causation As MedicalFeesCausation, homologations As List(Of CupsHomologation))) Implements IMedicalFeesCausationAdminService.GetCausationInvoiceQx
        Try
            Dim validateResult = ValidateCausationExistsQx(invoiceDetail)
            If validateResult IsNot Nothing AndAlso Not validateResult.StateResult Then
                Return New ActionResult(Of (causation As MedicalFeesCausation, homologations As List(Of CupsHomologation))) With {
                    .StateResult = False,
                    .StatusCode = validateResult.StatusCode,
                    .Message = validateResult.Message
                }
            End If
            ValidateMedicalFeesContract(invoiceDetail)

            Dim res = CausedValue(
                If(invoiceDetail.MedicalFeesCausationId, 0),
                If(invoiceDetail.RateManualType, 0),
                invoiceDetail.CupsEntityId,
                invoiceDetail.CareGroupId,
                If(invoiceDetail.RateManualId, 0),
                invoiceDetail.TotalSalesPrice,
                If(invoiceDetail.Presentation, 0),
                invoiceDetail.IPSServiceId,
                invoiceDetail.IPSServiceDescription,
                invoiceDetail.PerformsHealthProfessionalCode,
                invoiceDetail.ThirdPartyDescription,
                Nothing,
                invoiceDetail.ServiceOrderDetailId,
                0,
                invoiceDetail.MedicalFeesContractId,
                listCupsHomologations
            )

            If res.StateResult Then
                Dim medicalFeesContract = _medicalFeesContractAdminService.GetMedicalFeesContractById(Integer.Parse(res.Message), audit:=audit)

                If medicalFeesContract.ObjectEmbbeded.Status <> 1 Then
                    Throw New IndigoValidationException($"No se puede causar el Item seleccionado porque el contrato {medicalFeesContract.ObjectEmbbeded.Code} esta terminado o suspendido")
                End If

                ' ⚠️ CAMBIO: Verificar si CreateMedicalFeesCausationQx retornó Nothing (por ThirdPartyId inválido)
                Dim causation = CreateMedicalFeesCausationQx(invoiceDetail, Decimal.Parse(res.MessageResult(0)), Integer.Parse(res.Message))

                If causation Is Nothing Then
                    ' ThirdPartyId inválido o no existe → retornar error sin lanzar excepción
                    Return New ActionResult(Of (causation As MedicalFeesCausation, homologations As List(Of CupsHomologation))) With {
                        .StateResult = False,
                        .Message = $"El item quirúrgico no puede ser causado porque el ThirdPartyId ({invoiceDetail.ThirdPartyId}) no existe o es inválido. Factura: {invoiceDetail.InvoiceNumber}, ServiceOrderDetailId: {invoiceDetail.ServiceOrderDetailId}"
                    }
                End If

                Return New ActionResult(Of (causation As MedicalFeesCausation, homologations As List(Of CupsHomologation))) With {
                    .StateResult = True,
                    .ObjectEmbbeded = (causation, Nothing)
                }
            ElseIf res.ObjectEmbbeded IsNot Nothing AndAlso res.ObjectEmbbeded.Any() Then
                Return New ActionResult(Of (causation As MedicalFeesCausation, homologations As List(Of CupsHomologation))) With {
                    .StateResult = False,
                    .ObjectEmbbeded = (Nothing, res.ObjectEmbbeded)
                }
            End If

            Throw New IndigoValidationException(res.Message)
        Catch ex As IndigoValidationException
            Return New ActionResult(Of (causation As MedicalFeesCausation, homologations As List(Of CupsHomologation))) With {.StateResult = False, .Message = ex.Message}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of (causation As MedicalFeesCausation, homologations As List(Of CupsHomologation))) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Causa una factura
    ''' </summary>
    ''' <param name="invoiceDetail"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function CauseInvoice(invoiceDetail As ViewListNoSurgical, audit As AuditMessage, Optional listCupsHomologations As List(Of CupsHomologation) = Nothing) As ActionResult(Of List(Of CupsHomologation)) Implements IMedicalFeesCausationAdminService.CauseInvoice
        Try
            Dim res = GetCausationInvoice(invoiceDetail, audit, listCupsHomologations)

            If res.StateResult Then
                Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                    Dim resSave = SaveMedicalFeesCausation({res.ObjectEmbbeded.causation}.ToList(), Nothing, Nothing, audit)

                    If Not resSave.StateResult Then
                        Throw New IndigoValidationException(resSave.Message)
                    End If

                    scope.Complete()
                    Return New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = True}
                End Using
            Else
                ' Actualizar el campo Error en CausationPending cuando la causación falla
                UpdateCausationPendingError(invoiceDetail, res.Message)
            End If
            Return New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = False, .ObjectEmbbeded = res.ObjectEmbbeded.homologations, .Message = res.Message}
        Catch ex As IndigoValidationException
            ' Actualizar el campo Error en CausationPending cuando ocurre una excepción
            UpdateCausationPendingError(invoiceDetail, ex.Message)
            Return New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = False, .Message = ex.Message}
        Catch ex As Exception
            ' Actualizar el campo Error en CausationPending cuando ocurre una excepción
            UpdateCausationPendingError(invoiceDetail, ex.Message)
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la causación de una factura
    ''' </summary>
    ''' <param name="invoiceDetail"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetCausationInvoice(invoiceDetail As ViewListNoSurgical, audit As AuditMessage, Optional listCupsHomologations As List(Of CupsHomologation) = Nothing) As ActionResult(Of (causation As MedicalFeesCausation, homologations As List(Of CupsHomologation))) Implements IMedicalFeesCausationAdminService.GetCausationInvoice
        Try
            ValidateCausationExists(invoiceDetail)
            ValidateMedicalFeesContract(invoiceDetail)

            Dim res = CausedValue(
                invoiceDetail.MedicalFeesCausationId,
                If(invoiceDetail.RateManualType, 0),
                invoiceDetail.CupsEntityId,
                invoiceDetail.CareGroupId,
                invoiceDetail.RateManualId,
                invoiceDetail.TotalSalesPrice,
                If(invoiceDetail.Presentation, 0),
                invoiceDetail.IPSServiceId,
                invoiceDetail.IPSServiceDescription,
                invoiceDetail.PerformsHealthProfessionalCode,
                invoiceDetail.ThirdPartyDescription,
                Nothing,
                invoiceDetail.ServiceOrderDetailId,
                0,
                invoiceDetail.MedicalFeesContractId,
                listCupsHomologations
            )

            If res.StateResult Then
                Dim medicalFeesContract = _medicalFeesContractAdminService.GetMedicalFeesContractById(Integer.Parse(res.Message), audit:=audit)

                If medicalFeesContract.ObjectEmbbeded.Status <> 1 Then
                    Throw New IndigoValidationException($"No se puede causar el Item seleccionado porque el contrato {medicalFeesContract.ObjectEmbbeded.Code} esta terminado o suspendido")
                End If

                ' ⚠️ CAMBIO: Verificar si CreateMedicalFeesCausation retornó Nothing (por ThirdPartyId inválido)
                Dim causation = CreateMedicalFeesCausation(invoiceDetail, Decimal.Parse(res.MessageResult(0)), Integer.Parse(res.Message))

                If causation Is Nothing Then
                    ' ThirdPartyId inválido o no existe → retornar error sin lanzar excepción
                    Return New ActionResult(Of (causation As MedicalFeesCausation, homologations As List(Of CupsHomologation))) With {
                        .StateResult = False,
                        .Message = $"El item NO quirúrgico no puede ser causado porque el ThirdPartyId ({invoiceDetail.ThirdPartyId}) no existe o es inválido. Factura: {invoiceDetail.InvoiceNumber}, ServiceOrderDetailId: {invoiceDetail.ServiceOrderDetailId}"
                    }
                End If

                Return New ActionResult(Of (causation As MedicalFeesCausation, homologations As List(Of CupsHomologation))) With {
                    .StateResult = True,
                    .ObjectEmbbeded = (causation, Nothing)
                }
            ElseIf res.ObjectEmbbeded IsNot Nothing AndAlso res.ObjectEmbbeded.Any() Then
                Return New ActionResult(Of (causation As MedicalFeesCausation, homologations As List(Of CupsHomologation))) With {
                    .StateResult = False,
                    .ObjectEmbbeded = (Nothing, res.ObjectEmbbeded)
                }
            End If

            Throw New IndigoValidationException(res.Message)
        Catch ex As IndigoValidationException
            Return New ActionResult(Of (causation As MedicalFeesCausation, homologations As List(Of CupsHomologation))) With {.StateResult = False, .Message = ex.Message}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of (causation As MedicalFeesCausation, homologations As List(Of CupsHomologation))) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Valida si ya existe causación Qx para el detalle quirúrgico.
    ''' Si es provisional (InvoiceDetailId=0, Status=1), la elimina para permitir la causación real.
    ''' </summary>
    Public Function ValidateCausationExistsQx(invoiceDetail As ViewListSurgicalAndPackage) As ActionResult(Of Boolean)
        Dim causationExists = _medicalFeesCausationRepository.FirstOrDefault(Function(m) m.ServiceOrderDetailId = invoiceDetail.ServiceOrderDetailId _
                                                                                 AndAlso m.Status <> 4 _
                                                                                 AndAlso m.ServiceOrderDetailSurgicalId = invoiceDetail.ServiceOrderDetailSurgicalId, tracking:=False)

        If causationExists IsNot Nothing Then
            ' Si es provisional (auto-causada sin factura), eliminarla para permitir la causación real
            If (Not causationExists.InvoiceDetailId.HasValue OrElse causationExists.InvoiceDetailId = 0) AndAlso causationExists.Status = 1 Then
                _medicalFeesCausationRepository.DeleteEntity(causationExists)
                _medicalFeesCausationRepository.UnitWork.Commit()
                CleanupRelatedPending(invoiceDetail.ServiceOrderDetailId, invoiceDetail.ServiceOrderDetailSurgicalId)
                Return Nothing
            End If

            Return New ActionResult(Of Boolean) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = "Ya existe una causación para el ítem seleccionado."}
        End If

        Return Nothing
    End Function

    ''' <summary>
    ''' Valida si ya existe el registro de causación.
    ''' Si la causación existente es provisional (InvoiceDetailId IS NULL o 0, Status=1),
    ''' la elimina para permitir que el flujo de facturación cree la causación real.
    ''' </summary>
    ''' <param name="invoiceDetail"></param>
    Private Sub ValidateCausationExists(invoiceDetail As ViewListNoSurgical)
        Dim causationExists = _medicalFeesCausationRepository.FirstOrDefault(Function(m) m.ServiceOrderDetailId = invoiceDetail.ServiceOrderDetailId _
                                                                                 AndAlso m.Status <> 4 _
                                                                                 , tracking:=False)

        If causationExists IsNot Nothing Then
            ' Si es provisional (auto-causada sin factura), eliminarla para permitir la causación real
            If (Not causationExists.InvoiceDetailId.HasValue OrElse causationExists.InvoiceDetailId = 0) AndAlso causationExists.Status = 1 Then
                _medicalFeesCausationRepository.DeleteEntity(causationExists)
                _medicalFeesCausationRepository.UnitWork.Commit()
                CleanupRelatedPending(invoiceDetail.ServiceOrderDetailId, Nothing)
                Return
            End If

            Throw New IndigoValidationException("Ya existe una causación para el ítem seleccionado.")
        End If
    End Sub

    ''' <summary>
    ''' Elimina CausationPending asociados a un ServiceOrderDetailId al limpiar una causación provisional.
    ''' Busca por ServiceOrderDetailId en el JSON Data deserializado.
    ''' </summary>
    Private Sub CleanupRelatedPending(serviceOrderDetailId As Integer, serviceOrderDetailSurgicalId As Integer?)
        Try
            Dim pendings = _causationPendignRepository.GetByFilter(Function(m) True)?.ToList()
            If pendings IsNot Nothing AndAlso pendings.Any() Then
                For Each item In pendings
                    Try
                        Dim obj = Utils.DeserializeJsonToEntity(Of ViewListNoSurgical)(item.Data)
                        If obj IsNot Nothing AndAlso obj.ServiceOrderDetailId = serviceOrderDetailId Then
                            ' Para NoQx (surgicalId Nothing): match si obj.ServiceOrderDetailSurgicalId = 0
                            ' Para Qx (surgicalId tiene valor): match si coinciden
                            If (Not serviceOrderDetailSurgicalId.HasValue AndAlso obj.ServiceOrderDetailSurgicalId = 0) _
                               OrElse (serviceOrderDetailSurgicalId.HasValue AndAlso obj.ServiceOrderDetailSurgicalId = serviceOrderDetailSurgicalId.Value) Then
                                _causationPendignRepository.DeleteEntity(item)
                                _causationPendignRepository.UnitWork.Commit()
                                Exit For
                            End If
                        End If
                    Catch
                    End Try
                Next
            End If
        Catch ex As Exception
            Debug.WriteLine($"Error limpiando pending para ServiceOrderDetailId {serviceOrderDetailId}: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Actualiza el campo Error en CausationPending cuando la causación falla
    ''' </summary>
    ''' <param name="invoiceDetail">Detalle de la factura</param>
    ''' <param name="errorMessage">Mensaje de error a registrar</param>
    ''' <remarks>
    ''' OPTIMIZACIÓN: Usa Exit For para deserializar solo hasta encontrar el registro único.
    ''' ServiceOrderDetailId es único, por lo que solo existe un registro a actualizar.
    ''' </remarks>
    Private Sub UpdateCausationPendingError(invoiceDetail As ViewListNoSurgical, errorMessage As String)
        Try
            ' Filtrar por profesional y tipo (no quirúrgico) para reducir el conjunto de datos
            Dim causationPending = _causationPendignRepository.GetByFilter(
                Function(m) Not m.IsQx AndAlso m.PerformsHealthProfessionalCode = invoiceDetail.PerformsHealthProfessionalCode
            )?.ToList()

            If causationPending IsNot Nothing AndAlso causationPending.Any() Then
                ' Buscar el registro único sin deserializar todos
                For Each item In causationPending
                    Dim obj = Utils.DeserializeJsonToEntity(Of ViewListNoSurgical)(item.Data)
                    If obj IsNot Nothing AndAlso obj.ServiceOrderDetailId = invoiceDetail.ServiceOrderDetailId Then
                        ' Actualizar el campo Error con el mensaje
                        item.Error = If(String.IsNullOrWhiteSpace(errorMessage), "Error no especificado", errorMessage)
                        _causationPendignRepository.SaveEntity(item)
                        _causationPendignRepository.UnitWork.Commit()
                        Exit For ' Salir inmediatamente - ServiceOrderDetailId es único
                    End If
                Next
            End If
        Catch ex As Exception
            ' Log del error pero no propagar la excepción para no interferir con el flujo principal
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        End Try
    End Sub

    ''' <summary>
    ''' Actualiza el campo Error en CausationPending cuando la causación quirúrgica falla
    ''' </summary>
    ''' <param name="invoiceDetail">Detalle de la factura quirúrgica</param>
    ''' <param name="errorMessage">Mensaje de error a registrar</param>
    ''' <remarks>
    ''' OPTIMIZACIÓN: Usa Exit For para deserializar solo hasta encontrar el registro único.
    ''' ServiceOrderDetailId es único, por lo que solo existe un registro a actualizar.
    ''' </remarks>
    Private Sub UpdateCausationPendingErrorQx(invoiceDetail As ViewListSurgicalAndPackage, errorMessage As String)
        Try
            ' Filtrar por profesional y tipo (quirúrgico) para reducir el conjunto de datos
            Dim causationPending = _causationPendignRepository.GetByFilter(
                Function(m) m.IsQx AndAlso m.PerformsHealthProfessionalCode = invoiceDetail.PerformsHealthProfessionalCode
            )?.ToList()

            If causationPending IsNot Nothing AndAlso causationPending.Any() Then
                ' Buscar el registro único sin deserializar todos
                For Each item In causationPending
                    Dim obj = Utils.DeserializeJsonToEntity(Of ViewListSurgicalAndPackage)(item.Data)
                    If obj IsNot Nothing AndAlso obj.ServiceOrderDetailId = invoiceDetail.ServiceOrderDetailId Then
                        ' Actualizar el campo Error con el mensaje
                        item.Error = If(String.IsNullOrWhiteSpace(errorMessage), "Error no especificado", errorMessage)
                        _causationPendignRepository.SaveEntity(item)
                        _causationPendignRepository.UnitWork.Commit()
                        Exit For ' Salir inmediatamente - ServiceOrderDetailId es único
                    End If
                Next
            End If
        Catch ex As Exception
            ' Log del error pero no propagar la excepción para no interferir con el flujo principal
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        End Try
    End Sub

    ''' <summary>
    ''' Crea la causación
    ''' </summary>
    ''' <param name="invoiceDetail"></param>
    ''' <param name="amountPayable"></param>
    ''' <param name="medicalFeesContractId"></param>
    ''' <returns></returns>
    Private Function CreateMedicalFeesCausationQx(
        invoiceDetail As ViewListSurgicalAndPackage,
        amountPayable As Decimal,
        medicalFeesContractId As Integer) As MedicalFeesCausation

        ' ⚠️ VALIDACIÓN ADICIONAL: Verificar que ThirdPartyId existe en la base de datos
        Dim thirdPartyExists = _thirdPartyRepository.FirstOrDefault(Function(x) x.Id = invoiceDetail.ThirdPartyId.Value)

        If thirdPartyExists Is Nothing Then
            Dim errorMsg As String = String.Format(
                "❌ ERROR FK ThirdParty - ThirdPartyId NO EXISTE en la base de datos:{0}" &
                "   - InvoiceNumber: {1}{0}" &
                "   - ServiceOrderDetailId: {2}{0}" &
                "   - HealthProfessional: {3}{0}" &
                "   - ThirdPartyId: {4} ❌ NO EXISTE{0}" &
                "⚠️ SOLUCIÓN: Verificar que el tercero exista en Common.ThirdParty o corregir el profesional.",
                vbCrLf,
                invoiceDetail.InvoiceNumber,
                invoiceDetail.ServiceOrderDetailId,
                invoiceDetail.PerformsHealthProfessionalCode,
                invoiceDetail.ThirdPartyId.Value
            )

            Debug.WriteLine(errorMsg)
            ' ⚠️ CAMBIO: Retornar Nothing en vez de lanzar excepción
            Return Nothing
        End If

        Dim causation = New MedicalFeesCausation With {
            .Id = If(invoiceDetail.MedicalFeesCausationId, 0),
            .AdmissionNumber = invoiceDetail.AdmissionNumber,
            .PatientCode = invoiceDetail.PatientCode,
            .HealthProfessionalCode = invoiceDetail.PerformsHealthProfessionalCode.Trim(),
            .ThirdPartyId = invoiceDetail.ThirdPartyId.Value,
            .MedicalFeesContractId = medicalFeesContractId,
            .ServiceOrderId = invoiceDetail.ServiceOrderId,
            .ServiceOrderDetailId = invoiceDetail.ServiceOrderDetailId,
            .ServiceOrderDetailSurgicalId = If(invoiceDetail.ServiceOrderDetailSurgicalId.HasValue AndAlso invoiceDetail.ServiceOrderDetailSurgicalId > 0, invoiceDetail.ServiceOrderDetailSurgicalId.Value, CType(Nothing, Integer?)),
            .AmountPayable = amountPayable,
            .MedicalFeesContractValue = amountPayable,
            .InvoiceQuantity = invoiceDetail.InvoicedQuantity,
            .TotalAmountPayable = amountPayable * invoiceDetail.InvoicedQuantity,
            .PercentageCashed = 100,
            .MedicalFeePaid = False,
            .InvoiceDetailId = If(invoiceDetail.InvoiceDetailId > 0, invoiceDetail.InvoiceDetailId, CType(Nothing, Integer?)),
            .CausationDate = Date.Now
        }

        If causation.Id > 0 Then causation.MarkAsModified()

        Debug.WriteLine($"✅ MedicalFeesCausation Qx creado - ThirdPartyId: {causation.ThirdPartyId}, ServiceOrderDetailId: {causation.ServiceOrderDetailId}, Factura: {invoiceDetail.InvoiceNumber}")

        Return causation
    End Function

    ''' <summary>
    ''' Crea la causación
    ''' </summary>
    ''' <param name="invoiceDetail"></param>
    ''' <param name="amountPayable"></param>
    ''' <param name="medicalFeesContractId"></param>
    ''' <returns></returns>
    Private Function CreateMedicalFeesCausation(
        invoiceDetail As ViewListNoSurgical,
        amountPayable As Decimal,
        medicalFeesContractId As Integer) As MedicalFeesCausation

        ' ⚠️ VALIDACIÓN ADICIONAL: Verificar que ThirdPartyId existe en la base de datos
        Dim thirdPartyExists = _thirdPartyRepository.FirstOrDefault(Function(x) x.Id = invoiceDetail.ThirdPartyId.Value)

        If thirdPartyExists Is Nothing Then
            Dim errorMsg As String = String.Format(
                "❌ ERROR FK ThirdParty - ThirdPartyId NO EXISTE en la base de datos:{0}" &
                "   - InvoiceNumber: {1}{0}" &
                "   - ServiceOrderDetailId: {2}{0}" &
                "   - HealthProfessional: {3}{0}" &
                "   - ThirdPartyId: {4} ❌ NO EXISTE{0}" &
                "⚠️ SOLUCIÓN: Verificar que el tercero exista en Common.ThirdParty o corregir el profesional.",
                vbCrLf,
                invoiceDetail.InvoiceNumber,
                invoiceDetail.ServiceOrderDetailId,
                invoiceDetail.PerformsHealthProfessionalCode,
                invoiceDetail.ThirdPartyId.Value
            )

            Debug.WriteLine(errorMsg)
            ' ⚠️ CAMBIO: Retornar Nothing en vez de lanzar excepción
            Return Nothing
        End If

        Dim causation = New MedicalFeesCausation With {
            .Id = invoiceDetail.MedicalFeesCausationId,
            .AdmissionNumber = invoiceDetail.AdmissionNumber,
            .PatientCode = invoiceDetail.PatientCode,
            .HealthProfessionalCode = invoiceDetail.PerformsHealthProfessionalCode.Trim(),
            .ThirdPartyId = invoiceDetail.ThirdPartyId.Value,
            .MedicalFeesContractId = medicalFeesContractId,
            .ServiceOrderId = invoiceDetail.ServiceOrderId,
            .ServiceOrderDetailId = invoiceDetail.ServiceOrderDetailId,
            .ServiceOrderDetailSurgicalId = IIf(invoiceDetail.ServiceOrderDetailSurgicalId > 0, invoiceDetail.ServiceOrderDetailSurgicalId, Nothing),
            .AmountPayable = amountPayable,
            .MedicalFeesContractValue = amountPayable,
            .InvoiceQuantity = invoiceDetail.InvoicedQuantity,
            .TotalAmountPayable = amountPayable * invoiceDetail.InvoicedQuantity,
            .PercentageCashed = 100,
            .MedicalFeePaid = False,
            .InvoiceDetailId = If(invoiceDetail.InvoiceDetailId > 0, invoiceDetail.InvoiceDetailId, CType(Nothing, Integer?)),
            .CausationDate = Date.Now
        }

        If causation.Id > 0 Then causation.MarkAsModified()

        Debug.WriteLine($"✅ MedicalFeesCausation NoQx creado - ThirdPartyId: {causation.ThirdPartyId}, ServiceOrderDetailId: {causation.ServiceOrderDetailId}, Factura: {invoiceDetail.InvoiceNumber}")

        Return causation
    End Function

    ''' <summary>
    ''' Valida el contrato de la causación
    ''' </summary>
    ''' <param name="invoiceDetail"></param>
    Private Sub ValidateMedicalFeesContract(invoiceDetail As ViewListNoSurgical)
        If invoiceDetail.MedicalFeesCausationId <> 0 Then
            If invoiceDetail.StatusMedicalFeesCausation = 2 Then
                Throw New IndigoValidationException($"El item ({invoiceDetail.IPSServiceDescription}) ya se encuentra en una liquidación registrada.")
            ElseIf invoiceDetail.StatusMedicalFeesCausation = 3 Then
                Throw New IndigoValidationException($"El item ({invoiceDetail.IPSServiceDescription}) ya se encuentra en una liquidación confirmada.")
            End If
        End If
    End Sub

    ''' <summary>
    ''' Valida el contrato de la causación
    ''' </summary>
    ''' <param name="invoiceDetail"></param>
    Private Sub ValidateMedicalFeesContract(invoiceDetail As ViewListSurgicalAndPackage)
        If invoiceDetail.MedicalFeesCausationId <> 0 Then
            If invoiceDetail.StatusMedicalFeesCausation = 2 Then
                Throw New IndigoValidationException($"El item ({invoiceDetail.IPSServiceDescription}) ya se encuentra en una liquidación registrada.")
            ElseIf invoiceDetail.StatusMedicalFeesCausation = 3 Then
                Throw New IndigoValidationException($"El item ({invoiceDetail.IPSServiceDescription}) ya se encuentra en una liquidación confirmada.")
            End If
        End If
    End Sub

    ''' <summary>
    ''' Procesa causaciones automáticas para órdenes de servicio CUPS no reconocidas.
    ''' Itera en batches hasta agotar todos los candidatos o alcanzar el timeout (75 min).
    ''' ALCANCE: Solo Registrado (sin factura), CUPS, sin causación activa. Excluye liquidados.
    ''' Procesa todas las unidades operativas.
    ''' OPTIMIZADO: Pre-fetch de ThirdPartyIds y cache de contratos para evitar N+1 queries.
    ''' </summary>
    ''' <param name="audit">Información de auditoría</param>
    ''' <param name="batchSize">Tamaño de cada batch (default 500 para ejecución manual)</param>
    Public Function ProcessUnrecognizedCausations(audit As AuditMessage, Optional batchSize As Integer = 500) As ActionResult(Of UnrecognizedProcessingResult) Implements IMedicalFeesCausationAdminService.ProcessUnrecognizedCausations
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If

        Dim result As New UnrecognizedProcessingResult()
        Dim effectiveBatchSize = If(batchSize > 0, batchSize, 500)
        Dim maxMinutes = 75
        Dim startTime = DateTime.Now
        Dim batchNumber = 0
        Dim contractCache As New Dictionary(Of Integer, MedicalFeesContract)()
        Dim processedCandidateKeys As New HashSet(Of String)(StringComparer.Ordinal)

        Try
            Do
                ' Verificar timeout
                If DateTime.Now.Subtract(startTime).TotalMinutes >= maxMinutes Then
                    result.Details.Add(New ProcessingDetail() With {
                        .ServiceOrderDetailId = 0,
                        .Status = "Timeout",
                        .Message = $"Timeout alcanzado ({maxMinutes} min). Procesados hasta ahora: {result.SuccessCount} OK, {result.FailedCount} pendientes."
                    })
                    Exit Do
                End If

                ' Obtener siguiente batch de candidatos
                Dim candidates = _medicalFeesCausationRepository.GetCandidatesForAutoCausationBatched(effectiveBatchSize)
                If candidates Is Nothing OrElse Not candidates.Any() Then
                    Exit Do
                End If

                Dim freshCandidates = candidates _
                    .Where(Function(candidate) processedCandidateKeys.Add(BuildCandidateKey(candidate))) _
                    .ToList()

                If Not freshCandidates.Any() Then
                    result.Details.Add(New ProcessingDetail() With {
                        .ServiceOrderDetailId = 0,
                        .Status = "Stopped",
                        .Message = "El repositorio devolvió nuevamente los mismos candidatos sin progreso. Se detiene el proceso para evitar reprocesos en bucle."
                    })
                    Exit Do
                End If

                batchNumber += 1
                result.TotalCandidates += freshCandidates.Count

                ' Pre-fetch de ThirdPartyIds válidos fuera del TransactionScope (lectura pura, no necesita locks)
                Dim allThirdPartyIds = freshCandidates _
                    .Where(Function(c) c.ThirdPartyId.HasValue AndAlso c.ThirdPartyId.Value > 0) _
                    .Select(Function(c) c.ThirdPartyId.Value) _
                    .Distinct() _
                    .ToList()
                Dim validThirdPartyIds = GetValidThirdPartyIds(allThirdPartyIds)
                Dim validThirdPartySet = New HashSet(Of Integer)(validThirdPartyIds)

                ' Candidatos que fallaron y deben registrarse como pending FUERA del TransactionScope
                Dim deferredPendings As New List(Of (candidate As SP_GetCandidatesForAutoCausation_Result, message As String))()
                Dim candidateLookup As New Dictionary(Of String, SP_GetCandidatesForAutoCausation_Result)(StringComparer.Ordinal)
                For Each candidate In freshCandidates
                    candidateLookup(BuildCandidateKey(candidate)) = candidate
                Next

                ' Procesar el batch dentro de su propia transacción
                Dim batchSaveOk = False
                Using Transaction As New TransactionScope(TransactionScopeOption.Required,
                    New TransactionOptions() With {
                        .Timeout = TransactionManager.MaximumTimeout,
                        .IsolationLevel = IsolationLevel.ReadCommitted
                    })

                    Dim causationsToSave As New List(Of MedicalFeesCausation)()
                    Dim detailLookup As New Dictionary(Of String, ProcessingDetail)(StringComparer.Ordinal)

                    For Each candidate In freshCandidates
                        Dim candidateKey = BuildCandidateKey(candidate)
                        Dim detail As New ProcessingDetail() With {
                            .ServiceOrderDetailId = candidate.ServiceOrderDetailId
                        }
                        detailLookup(candidateKey) = detail

                        Try
                            ' Verificar ThirdPartyId válido (en memoria, sin query)
                            If Not candidate.ThirdPartyId.HasValue OrElse candidate.ThirdPartyId.Value <= 0 Then
                                detail.Status = "Failed"
                                detail.Message = $"ThirdPartyId no válido ({If(candidate.ThirdPartyId, 0)}) para profesional {candidate.PerformsHealthProfessionalCode}"
                                result.FailedCount += 1
                                result.Details.Add(detail)
                                deferredPendings.Add((candidate, detail.Message))
                                Continue For
                            End If

                            ' Verificar ThirdParty existe (lookup en HashSet, O(1))
                            If Not validThirdPartySet.Contains(candidate.ThirdPartyId.Value) Then
                                detail.Status = "Failed"
                                detail.Message = $"ThirdPartyId ({candidate.ThirdPartyId.Value}) no existe en la base de datos."
                                result.FailedCount += 1
                                result.Details.Add(detail)
                                deferredPendings.Add((candidate, detail.Message))
                                Continue For
                            End If

                            ' Calcular valor a causar
                            Dim causedValueResult = CausedValue(
                                0,
                                If(candidate.RateManualType, 0),
                                candidate.CupsEntityId,
                                candidate.CareGroupId,
                                If(candidate.RateManualId, 0),
                                candidate.TotalSalesPrice,
                                If(candidate.Presentation, 0),
                                candidate.IPSServiceId,
                                If(candidate.IPSServiceDescription, String.Empty),
                                candidate.PerformsHealthProfessionalCode,
                                If(candidate.ThirdPartyDescription, String.Empty),
                                Nothing,
                                candidate.ServiceOrderDetailId,
                                If(candidate.ServiceOrderDetailSurgicalId.HasValue, candidate.ServiceOrderDetailSurgicalId.Value, 0),
                                0
                            )

                            If causedValueResult.StateResult Then
                                Dim amountPayable As Decimal
                                Dim medicalFeesContractId As Integer
                                Dim parseErrorMessage As String = Nothing
                                If Not TryGetAutoCausationValues(causedValueResult, amountPayable, medicalFeesContractId, parseErrorMessage) Then
                                    detail.Status = "Failed"
                                    detail.Message = parseErrorMessage
                                    result.FailedCount += 1
                                    result.Details.Add(detail)
                                    deferredPendings.Add((candidate, detail.Message))
                                    Continue For
                                End If

                                ' Validar contrato activo (con cache)
                                Dim medicalFeesContract As MedicalFeesContract = Nothing
                                If Not contractCache.TryGetValue(medicalFeesContractId, medicalFeesContract) Then
                                    medicalFeesContract = _medicalFeesContractRepository.GetMedicalFeesContractById(medicalFeesContractId)
                                    If medicalFeesContract IsNot Nothing AndAlso medicalFeesContract.Id > 0 Then
                                        contractCache(medicalFeesContractId) = medicalFeesContract
                                    Else
                                        medicalFeesContract = Nothing
                                    End If
                                End If

                                If medicalFeesContract Is Nothing OrElse medicalFeesContract.Status <> 1 Then
                                    detail.Status = "Failed"
                                    detail.Message = $"Contrato {If(medicalFeesContract?.Code, "?")} terminado o suspendido."
                                    result.FailedCount += 1
                                    result.Details.Add(detail)
                                    deferredPendings.Add((candidate, detail.Message))
                                    Continue For
                                End If

                                Dim causation = CreateMedicalFeesCausationFromCandidate(candidate, amountPayable, medicalFeesContractId)

                                causationsToSave.Add(causation)

                                detail.Status = "OK"
                                detail.Message = $"Causación calculada: ${amountPayable:N2} x {candidate.InvoicedQuantity} = ${causation.TotalAmountPayable:N2}"
                                result.SuccessCount += 1

                            ElseIf causedValueResult.ObjectEmbbeded IsNot Nothing AndAlso causedValueResult.ObjectEmbbeded.Any() Then
                                detail.Status = "Failed"
                                detail.Message = "Existen homólogos CUPS pendientes de resolver."
                                result.FailedCount += 1
                                deferredPendings.Add((candidate, detail.Message))
                            Else
                                detail.Status = "Failed"
                                detail.Message = If(causedValueResult.Message, "Error calculando valor de causación.")
                                result.FailedCount += 1
                                deferredPendings.Add((candidate, detail.Message))
                            End If

                            result.Details.Add(detail)

                        Catch ex As IndigoValidationException
                            If ex.Message.Contains("liquidación registrada") OrElse ex.Message.Contains("liquidación confirmada") Then
                                detail.Status = "ExcludedByLiquidation"
                                detail.Message = ex.Message
                                result.ExcludedByLiquidation += 1
                            Else
                                detail.Status = "Failed"
                                detail.Message = ex.Message
                                result.FailedCount += 1
                                deferredPendings.Add((candidate, ex.Message))
                            End If
                            result.Details.Add(detail)

                        Catch ex As Exception
                            detail.Status = "Failed"
                            detail.Message = ex.Message
                            result.FailedCount += 1
                            result.Details.Add(detail)
                            deferredPendings.Add((candidate, ex.Message))
                        End Try
                    Next

                    ' Guardar causaciones exitosas del batch
                    If causationsToSave.Any() Then
                        Dim saveResult = SaveMedicalFeesCausation(causationsToSave, Nothing, Nothing, audit)
                        If Not saveResult.StateResult Then
                            Dim saveErrorMessage = GetActionResultMessage(saveResult, "No fue posible guardar las causaciones del batch.")
                            For Each causation In causationsToSave
                                Dim saveKey = BuildCausationKey(causation.ServiceOrderDetailId, causation.ServiceOrderDetailSurgicalId)
                                Dim candidateForPending As SP_GetCandidatesForAutoCausation_Result = Nothing
                                candidateLookup.TryGetValue(saveKey, candidateForPending)
                                If candidateForPending IsNot Nothing Then
                                    deferredPendings.Add((candidateForPending, $"Error guardando: {saveErrorMessage}"))
                                End If

                                Dim detail As ProcessingDetail = Nothing
                                If detailLookup.TryGetValue(saveKey, detail) Then
                                    detail.Status = "Failed"
                                    detail.Message = $"Error guardando: {saveErrorMessage}"
                                End If
                            Next
                            result.FailedCount += causationsToSave.Count
                            result.SuccessCount -= causationsToSave.Count
                        Else
                            batchSaveOk = True
                        End If
                    End If

                    If batchSaveOk OrElse Not causationsToSave.Any() Then
                        Transaction.Complete()
                    End If
                End Using

                ' Registrar pendings FUERA del TransactionScope (evita "operación no válida para el estado de la transacción")
                If deferredPendings.Any() Then
                    RegisterCandidatesAsPendingBatch(deferredPendings, audit.CodeUser)
                    _causationPendignRepository.UnitWork.Commit()
                End If

                ' Si el batch trajo menos que el tamaño solicitado, ya no hay más candidatos
                If candidates.Count < effectiveBatchSize Then
                    Exit Do
                End If
            Loop

            Return New ActionResult(Of UnrecognizedProcessingResult) With {
                .StateResult = True,
                .ObjectEmbbeded = result,
                .Message = $"Proceso completado ({batchNumber} batches): {result.SuccessCount} causados, {result.FailedCount} pendientes, {result.ExcludedByLiquidation} excluidos."
            }

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of UnrecognizedProcessingResult) With {
                .StateResult = False,
                .ObjectEmbbeded = result,
                .Message = ex.Message
            }
        End Try
    End Function

    ''' <summary>
    ''' Registra multiples candidatos fallidos como CausationPending en batch.
    ''' 1 query para pre-fetch de pendings existentes + 1 deserializacion por pending (no por candidato).
    ''' Evita el patron N+1 del metodo individual.
    ''' </summary>
    Private Sub RegisterCandidatesAsPendingBatch(
        deferredPendings As List(Of (candidate As SP_GetCandidatesForAutoCausation_Result, message As String)),
        userCode As String)

        Try
            Dim pendingLookup As New Dictionary(Of String, (candidate As SP_GetCandidatesForAutoCausation_Result, message As String))(StringComparer.Ordinal)
            For Each pending In deferredPendings
                pendingLookup(BuildCandidateKey(pending.candidate)) = pending
            Next

            ' 1 sola query: traer todos los pendings SIN-FACTURA existentes
            Dim allExistingPendings = _causationPendignRepository.GetByFilter(
                Function(m) m.InvoiceNumber = "SIN-FACTURA"
            )?.ToList()

            ' Deserializar UNA vez y construir lookup: (ServiceOrderDetailId, SurgicalId) -> CausationPending
            Dim existingLookup As New Dictionary(Of String, CausationPending)()
            If allExistingPendings IsNot Nothing Then
                For Each item In allExistingPendings
                    Try
                        Dim obj = Utils.DeserializeJsonToEntity(Of ViewListNoSurgical)(item.Data)
                        If obj IsNot Nothing Then
                            Dim key = $"{obj.ServiceOrderDetailId}_{obj.ServiceOrderDetailSurgicalId}"
                            If Not existingLookup.ContainsKey(key) Then
                                existingLookup(key) = item
                            End If
                        End If
                    Catch
                    End Try
                Next
            End If

            ' Procesar cada candidato con lookup O(1) en lugar de query + deserializacion
            For Each pending In pendingLookup.Values
                Try
                    Dim candidate = pending.candidate
                    Dim errorMessage = pending.message
                    Dim isQx = candidate.ServiceOrderDetailSurgicalId.HasValue AndAlso candidate.ServiceOrderDetailSurgicalId.Value > 0
                    Dim surgicalId = If(isQx, candidate.ServiceOrderDetailSurgicalId.Value, 0)
                    Dim lookupKey = $"{candidate.ServiceOrderDetailId}_{surgicalId}"

                    ' O(1) lookup: si ya existe, solo actualizar el error
                    If existingLookup.ContainsKey(lookupKey) Then
                        Dim existingItem = existingLookup(lookupKey)
                        existingItem.Error = If(String.IsNullOrWhiteSpace(errorMessage), "Error no especificado", errorMessage)
                        _causationPendignRepository.SaveEntity(existingItem)
                        Continue For
                    End If

                    ' No existe: crear nuevo pending
                    Dim viewListItem As New ViewListNoSurgical() With {
                        .ServiceOrderDetailId = candidate.ServiceOrderDetailId,
                        .ServiceOrderId = candidate.ServiceOrderId,
                        .AdmissionNumber = candidate.AdmissionNumber,
                        .PatientCode = candidate.PatientCode,
                        .CupsEntityId = candidate.CupsEntityId,
                        .CareGroupId = candidate.CareGroupId,
                        .RateManualId = If(candidate.RateManualId, 0),
                        .RateManualType = candidate.RateManualType,
                        .TotalSalesPrice = candidate.TotalSalesPrice,
                        .InvoicedQuantity = candidate.InvoicedQuantity,
                        .IPSServiceId = candidate.IPSServiceId,
                        .IPSServiceDescription = candidate.IPSServiceDescription,
                        .PerformsHealthProfessionalCode = candidate.PerformsHealthProfessionalCode,
                        .ThirdPartyId = candidate.ThirdPartyId,
                        .ThirdPartyDescription = candidate.ThirdPartyDescription,
                        .Presentation = candidate.Presentation,
                        .ServiceDate = candidate.ServiceDate,
                        .ServiceType = candidate.ServiceType,
                        .ServiceOrderDetailSurgicalId = surgicalId,
                        .InvoiceDetailId = 0,
                        .InvoiceNumber = "SIN-FACTURA"
                    }

                    Dim jsonData = Utils.SerializeObjectToJson(viewListItem)

                    Dim newPending = New CausationPending() With {
                        .InvoiceNumber = "SIN-FACTURA",
                        .PatientCode = candidate.PatientCode,
                        .PatientName = If(candidate.HealthProfessionalName, "N/A"),
                        .PerformsHealthProfessionalCode = candidate.PerformsHealthProfessionalCode,
                        .AdmissionNumber = candidate.AdmissionNumber,
                        .InvoiceDate = candidate.ServiceDate,
                        .Error = If(String.IsNullOrWhiteSpace(errorMessage), "Error no especificado", errorMessage),
                        .IsQx = isQx,
                        .Data = jsonData,
                        .CreationDate = DateTime.Now,
                        .Retry = True
                    }

                    _causationPendignRepository.SaveEntity(newPending)

                    ' Agregar al lookup para evitar duplicados dentro del mismo batch
                    existingLookup(lookupKey) = newPending

                Catch ex As Exception
                    Debug.WriteLine($"Error registrando pending para ServiceOrderDetailId {pending.candidate.ServiceOrderDetailId}: {ex.Message}")
                End Try
            Next

        Catch ex As Exception
            Debug.WriteLine($"Error en RegisterCandidatesAsPendingBatch: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Metodo que causa los valores masivamente
    ''' </summary>
    ''' <param name="ListNoSurgical"></param>
    ''' <param name="ListSurgical"></param>
    ''' <param name="ListCupsHomologation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CauseMassively(ListNoSurgical As List(Of NoQxEntity), ListSurgical As List(Of QxEntity), Optional ListCupsHomologation As List(Of CupsHomologation) = Nothing) As ActionResult(Of Tuple(Of List(Of NoQxEntity), List(Of QxEntity))) Implements IMedicalFeesCausationAdminService.CauseMassively
        Dim medicalFeesServices As New MedicalFeesServices(_medicalFeesContractRepository, _contractRepository, _rateManualRepository, _healthProfessionalRepository, _cupsEntityRepository, _ipsServiceRepository,
                                                           _serviceOrderDetailRepository, _serviceOrderDetailSurgicalRepository, _billingService, _contractService, _cupsHomologationRepository,
                                                           _rateManualDetailSurgicalRepository, _surgicalProcedureServiceRepository, _healthProfessionalContractRepository, _medicalFeesLiquidationDetail)
        Return medicalFeesServices.CauseMassively(ListNoSurgical, ListSurgical, ListCupsHomologation)
    End Function

    ''' <summary>
    ''' Elimina una causacion desde la opcion de eliminar honorario
    ''' </summary>
    ''' <param name="MedicalFeesCausationId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteMedicalFeesCausationFromRemoveFees(MedicalFeesCausationId As Integer, audit As AuditMessage) As ActionResult Implements IMedicalFeesCausationAdminService.DeleteMedicalFeesCausationFromRemoveFees
        If MedicalFeesCausationId = 0 Then
            Throw New ArgumentNullException("MedicalFeesCausationId")
        End If
        Dim unitOfWork As IUnitWork = Me._medicalFeesCausationRepository.UnitWork
        Dim unitOfWorkLiquidationDetail As IUnitWork = Me._medicalFeesLiquidationDetail.UnitWork
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim ListDetailLiquidation As List(Of MedicalFeesLiquidationDetail) = _medicalFeesLiquidationDetail.GetListMedicalFeesLiquidationDetailByMedicalFeesCausationId(MedicalFeesCausationId)
                If ListDetailLiquidation IsNot Nothing AndAlso ListDetailLiquidation.Count > 0 Then
                    For Each itemDelete In ListDetailLiquidation
                        _medicalFeesLiquidationDetail.DeleteEntity(itemDelete)
                        unitOfWorkLiquidationDetail.Commit()
                    Next
                End If

                Dim MedicalFeesCausation As MedicalFeesCausation = _medicalFeesCausationRepository.GetMedicalFeesCausationById(MedicalFeesCausationId)
                MedicalFeesCausation.MarkAsDeleted()
                Dim auditProcess As IndigoAuditSimpleEntity(Of MedicalFeesCausation)
                auditProcess = New IndigoAuditSimpleEntity(Of MedicalFeesCausation)(MedicalFeesCausation, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
                Me._medicalFeesCausationRepository.DeleteEntity(MedicalFeesCausation)
                unitOfWork.Commit()
                auditProcess.Execute()

                Transaction.Complete()
                Return New ActionResult With {.StateResult = True}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                unitOfWorkLiquidationDetail.RollbackChanges()
                Transaction.Dispose()
                Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
            Catch ex As UpdateException
                unitOfWork.RollbackChanges()
                unitOfWorkLiquidationDetail.RollbackChanges()
                Transaction.Dispose()
                Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                unitOfWorkLiquidationDetail.RollbackChanges()
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
            End Try
        End Using
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _billingService.Dispose()
                _contractService.Dispose()
            End If
            _medicalFeesCausationRepository = Nothing
            _medicalFeesContractRepository = Nothing
            _contractRepository = Nothing
            _rateManualRepository = Nothing
            _healthProfessionalRepository = Nothing
            _medicalFeesNotesRepository = Nothing
            _medicalFeesLiquidationDetail = Nothing
            _cupsEntityRepository = Nothing
            _ipsServiceRepository = Nothing
            _serviceOrderDetailRepository = Nothing
            _serviceOrderDetailSurgicalRepository = Nothing
            _billingService = Nothing
            _contractService = Nothing
            _cupsHomologationRepository = Nothing
            _rateManualDetailSurgicalRepository = Nothing
            _surgicalProcedureServiceRepository = Nothing
            _healthProfessionalContractRepository = Nothing
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
