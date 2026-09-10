Imports System.Data.Entity.Core
Imports System.ServiceModel.Description
Imports Application.Security
Imports Domain.AccountManagement.Model
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Security
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

Public Class FolioTransferAdminService
    Implements IFolioTransferAdminService, Inject

#Region "Fields"
    Private _folioTransferRepository As IFolioTransferRepository
    Private _folioAlertRepository As IFolioAlertRepository
    Private _rejectionReasonRepository As IRejectionReasonRepository
    Private _managementAreasAdminService As IManagementAreasAdminService
    Private _userAdminService As IUserAdminService
    Private _userRepository As IUserRepository
    Private _IUserAdminService As IUserAdminService
#End Region

#Region "Builder"

    Public Sub New(ByVal folioTransferRepository As IFolioTransferRepository, folioAlertRepository As IFolioAlertRepository, rejectionReasonRepository As IRejectionReasonRepository, managementAreasAdminService As IManagementAreasAdminService, userAdminService As IUserAdminService, userRepository As IUserRepository, IUserAdminService As IUserAdminService)
        _folioTransferRepository = folioTransferRepository
        _folioAlertRepository = folioAlertRepository
        _rejectionReasonRepository = rejectionReasonRepository
        _managementAreasAdminService = managementAreasAdminService
        _userAdminService = userAdminService
        _userRepository = userRepository
        _IUserAdminService = IUserAdminService
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Función que se encarga de realizar la solicitud de traslado de folio a un usuario
    ''' </summary>
    ''' <returns></returns>
    Public Function RequestFolioTransferToUser(folioTransferRequest As List(Of FolioTransfer), audit As AuditMessage) As ActionResult Implements IFolioTransferAdminService.RequestFolioTransferToUser
        Dim unitOfWork As IUnitWork = _folioTransferRepository.UnitWork
        Dim messages As New List(Of String)
        Dim hasError As Boolean = False
        Try
            If folioTransferRequest Is Nothing OrElse folioTransferRequest.Count = 0 Then
                Return New ActionResult With {.StateResult = False, .Message = $"No se recibió ninguna solicitud de traslado."}
            End If

            For Each request In folioTransferRequest
                ' Validación de usuario destino distinto al actual
                If request.PreviousUser = request.ReceivingUser Then
                    messages.Add($"El usuario destino no puede ser igual al actual para los folios del ingreso {request.AdmissionNumber}, elija un usuario diferente.")
                    hasError = True
                    Continue For
                End If

                ' Validar si ya existe una solicitud pendiente para este folio
                Dim alreadyExists = _folioTransferRepository.GetAll().Any(Function(ft) ft.AdmissionNumber = request.AdmissionNumber _
                              AndAlso ft.RevenueControlDetailId = request.RevenueControlDetailId _
                              AndAlso ft.TransferStatus = 2)

                If alreadyExists Then
                    messages.Add($"Ya existe una solicitud pendiente para los folios del ingreso {request.AdmissionNumber}, por favor desmarquelos.") 'Se puede agregar el paciente o el orden del folio, algo que identifique el folio.
                    hasError = True
                    Continue For
                End If

                If request.ChangeTracker.State = ObjectState.Added Then
                    request.CreationDate = DateTime.Now
                    request.CreationUser = audit.CodeUser
                End If

                ' Forzar estado y limpieza de ID
                request.TransferStatus = 2
                request.Id = 0

                ' Agregar al repositorio
                _folioTransferRepository.SaveEntity(request)
            Next

            If hasError Then
                Return New ActionResult With {.StateResult = False, .Message = String.Join(Environment.NewLine, messages)}
            End If

            unitOfWork.Commit()
            Return New ActionResult With {.StateResult = True, .Message = $"Solicitud de traslado realizada con éxito para {folioTransferRequest.Count} folio(s)."}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Función que permite el guardado de una o más alertas.
    ''' </summary>
    ''' <param name="folioAlert">Alerta a crear</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveFolioAlert(folioAlert As FolioAlert, audit As AuditMessage) As ActionResult(Of FolioAlert) Implements IFolioTransferAdminService.SaveFolioAlert
        Dim unitOfWork As IUnitWork = _folioTransferRepository.UnitWork
        Try
            Dim managementArea As ManagementAreas = _managementAreasAdminService.ListManagementAreasByUserCode(audit.CodeUser).FirstOrDefault()

            ' Inicializa valores por defecto
            folioAlert.Status = True
            folioAlert.CreationDate = DateTime.Now
            folioAlert.CreationUser = audit.CodeUser
            folioAlert.ManagementAreaId = managementArea.Id

            ' Guarda la alerta
            _folioAlertRepository.SaveEntity(folioAlert)
            unitOfWork.Commit()

            Return New ActionResult(Of FolioAlert) With {.StateResult = True, .Message = $"Alerta creada con éxito", .ObjectEmbbeded = folioAlert}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FolioAlert) With {.StateResult = False, .Message = ex.Message.ToString()}
        End Try
    End Function

    ''' <summary>
    ''' Función que permite la suspensión (resolución) de una alerta
    ''' </summary>
    ''' <param name="folioAlert">Alerta a resolver</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SuspendFolioAlert(folioAlert As FolioAlert, audit As AuditMessage) As ActionResult Implements IFolioTransferAdminService.SuspendFolioAlert
        Dim unitOfWork As IUnitWork = _folioTransferRepository.UnitWork

        Try
            Dim existingAlert = _folioAlertRepository.GetExistingAlert(folioAlert.Id)

            If existingAlert Is Nothing Then
                Return New ActionResult With {.StateResult = False, .Message = "No se encontró una alerta por resolver"}
            End If

            existingAlert.Status = False

            _folioAlertRepository.SaveEntity(existingAlert)
            unitOfWork.Commit()

            Return New ActionResult With {.StateResult = True, .Message = $"Alerta resuelta con éxito"}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = ex.Message.ToString()}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene todas las alertas de traslado de folios
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllAlerts() As ActionResult(Of List(Of FolioAlert)) Implements IFolioTransferAdminService.GetAllAlerts
        Try
            Dim alerts = _folioAlertRepository.GetAllAlerts()

            Dim listUserCodes = New List(Of String)
            For Each alert In alerts
                listUserCodes.Add(alert.CreationUser)
            Next

            Dim listUsers = _userAdminService.ListUsersByCodes(listUserCodes)

            If listUsers IsNot Nothing AndAlso listUsers.Count > 0 Then
                For Each alert In alerts
                    alert.UserName = listUsers.Find(Function(u) u.UserCode = alert.CreationUser).Person.Fullname
                    alert.ManagementAreaName = alert.ManagementAreas.Name
                Next
            End If

            If alerts IsNot Nothing And alerts.Count > 0 Then
                Return New ActionResult(Of List(Of FolioAlert)) With {.StateResult = True, .ObjectEmbbeded = alerts}
            Else
                Return New ActionResult(Of List(Of FolioAlert)) With {.StateResult = False, .Message = "No se encontró ninguna alerta de traslado de folios"}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of FolioAlert)) With {.StateResult = False, .Message = ex.Message.ToString()}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una lista de todos los eventos de folios y también permite filtrar por numero admission, codigo de paciente o centro de atención
    ''' </summary>
    ''' <param name="admissionNumber">Número de ingreso</param>
    ''' <param name="patientCode">Código del paciente</param>
    ''' <param name="attentionCenter">Centro de atención</param>
    ''' <returns></returns>                       
    Public Function ListFolioEvents(attentionCenter As String, Optional admissionNumber As String = Nothing, Optional patientCode As String = Nothing, Optional userCode As String = Nothing) As ActionResult(Of List(Of VFolioTraceabilityProperties)) Implements IFolioTransferAdminService.ListFolioEvents
        Try
            If String.IsNullOrWhiteSpace(admissionNumber) Then
                admissionNumber = Nothing
            Else
                admissionNumber = admissionNumber.Trim()
            End If

            If String.IsNullOrWhiteSpace(patientCode) Then
                patientCode = Nothing
            Else
                patientCode = patientCode.Trim()
            End If

            If String.IsNullOrWhiteSpace(attentionCenter) Then
                attentionCenter = Nothing
            Else
                attentionCenter = attentionCenter.Trim()
            End If

            Dim resultList = _folioTransferRepository.ListFolioEvents(attentionCenter, admissionNumber, patientCode, userCode)
            If resultList Is Nothing OrElse resultList.Count = 0 Then
                Return New ActionResult(Of List(Of VFolioTraceabilityProperties)) With {.StateResult = False, .Message = "No se encontraron eventos de folios con los filtros indicados."}
            End If

            'Se unen todos los usuarios de la lista que necesitamos obtener y se llama al repo de user
            If resultList IsNot Nothing AndAlso resultList.Count > 0 Then
                Dim allUserCodes = resultList.Where(Function(t) Not String.IsNullOrEmpty(t.CurrentUserCode)).Select(Function(t) t.CurrentUserCode).Union(
                           resultList.Where(Function(t) Not String.IsNullOrEmpty(t.PreviousUserCode)).Select(Function(t) t.PreviousUserCode)).Union(
                           resultList.Where(Function(t) Not String.IsNullOrEmpty(t.ReceivingUserCode)).Select(Function(t) t.ReceivingUserCode)).Distinct().ToList()

                If allUserCodes.Count > 0 Then
                    Dim allUsers = _userRepository.ListUsersByCodes(allUserCodes).ToDictionary(Function(u) u.UserCode)

                    'Se recorre la lista para asignar el nombre completo a cada usuario que está presente
                    For Each folioEvent In resultList
                        If Not String.IsNullOrEmpty(folioEvent.PreviousUserCode) AndAlso allUsers.ContainsKey(folioEvent.PreviousUserCode) Then
                            folioEvent.PreviousUserCodeName = allUsers(folioEvent.PreviousUserCode).Person.Fullname
                        End If

                        If Not String.IsNullOrEmpty(folioEvent.ReceivingUserCode) AndAlso allUsers.ContainsKey(folioEvent.ReceivingUserCode) Then
                            folioEvent.ReceivingUserCodeName = allUsers(folioEvent.ReceivingUserCode).Person.Fullname
                        End If
                    Next
                End If
            End If

            Return New ActionResult(Of List(Of VFolioTraceabilityProperties)) With {.StateResult = True, .ObjectEmbbeded = resultList}
        Catch ex As Exception
            Return New ActionResult(Of List(Of VFolioTraceabilityProperties)) With {.StateResult = False, .Message = ex.Message.ToString()}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una lista de todas las solicitudes de traslado de folio por el centro de atención y el usuario respectivo 
    ''' </summary>
    ''' <param name="attentionCenter"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Public Function ListFolioTransferRequests(attentionCenter As String, managementAreaCode As String, userCode As String) As ActionResult(Of List(Of VDashboardProperties)) Implements IFolioTransferAdminService.ListFolioTransferRequests

        If String.IsNullOrEmpty(attentionCenter) OrElse String.IsNullOrEmpty(userCode) OrElse String.IsNullOrEmpty(managementAreaCode) Then
            Return New ActionResult(Of List(Of VDashboardProperties)) With {.StateResult = False, .Message = "Parámetros inválidos: centro de atención, usuario o area de gestión vacío"}
        End If

        'Se obtiene la lista de traslados con los filtros requeridos
        Dim listTransfers = _folioTransferRepository.ListFolioTransferRequests(attentionCenter, userCode, managementAreaCode)

        'Se unen todos los usuarios de la lista que necesitamos obtener y se llama al repo de user
        Dim allUserCodes = listTransfers.Select(Function(t) t.ReceivingUser).Union(
                   listTransfers.Select(Function(t) t.PreviousUser)).Distinct().ToList()

        Dim allUsers = _userRepository.ListUsersByCodes(allUserCodes).ToDictionary(Function(u) u.UserCode)

        'Se recorre la lista para asignarle una franja de tiempo de manejo y el nombre completo a cada usuario que está presente
        If listTransfers IsNot Nothing AndAlso listTransfers.Count > 0 Then
            For Each transfer In listTransfers
                Dim areaCode = transfer.AdmissionManagementArea.Split(" - ")(0)
                transfer.TimeStatus = _managementAreasAdminService.GetTimeStatus(transfer.AdmissionDate, areaCode)

                If allUsers.ContainsKey(transfer.ReceivingUser) Then
                    transfer.ReceivingUserCodeName = allUsers(transfer.ReceivingUser).Person.Fullname
                End If

                If allUsers.ContainsKey(transfer.PreviousUser) Then
                    transfer.PreviousUserCodeName = allUsers(transfer.PreviousUser).Person.Fullname
                End If
            Next

            Return New ActionResult(Of List(Of VDashboardProperties)) With {.StateResult = True, .ObjectEmbbeded = listTransfers}
        Else
            Return New ActionResult(Of List(Of VDashboardProperties)) With {.StateResult = False, .Message = "No se encontró ningun traslado de folio pendiente"}
        End If
    End Function

    ''' <summary>
    ''' Procesa la aceptación o rechazo de una solicitud de traslado de folio
    ''' </summary>
    ''' <param name="newState"></param>
    ''' <param name="operation"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ProcessTransferRequest(folioTransfer As FolioTransfer, newState As Byte, operation As String, audit As AuditMessage) As ActionResult(Of FolioTransfer) Implements IFolioTransferAdminService.ProcessTransferRequest
        Dim unitOfWork As IUnitWork = _folioTransferRepository.UnitWork

        Try
            Dim folio = _folioTransferRepository.GetFolioTransferById(folioTransfer.Id)

            If folio.TransferStatus = 2 Then ' Pendiente
                folio.TransferStatus = newState
                folio.ModificationDate = Date.UtcNow()
                folio.ModificationUser = audit.CodeUser

                ' Solo asignar si hay valor
                If folioTransfer.RejectionReasonId.HasValue Then
                    folio.RejectionReasonId = folioTransfer.RejectionReasonId

                    Dim rejectionReasonName = _rejectionReasonRepository.GetByFilter(Function(r) r.Id = folioTransfer.RejectionReasonId).FirstOrDefault().Name

                    Dim folioAlert = New FolioAlert() With {
                        .FolioTransferId = folio.Id,
                        .CreationDate = Date.UtcNow(),
                        .CreationUser = audit.CodeUser,
                        .ManagementAreaId = folio.ManagementAreaId,
                        .Status = True,
                        .RevenueControlDetailId = folio.RevenueControlDetailId,
                        .Comments = If(String.IsNullOrWhiteSpace(folioTransfer.RejectionObservation),
                                      rejectionReasonName,
                                      rejectionReasonName & " - " & folioTransfer.RejectionObservation)
                    }
                    _folioAlertRepository.SaveEntity(folioAlert)
                Else
                    folio.RejectionReasonId = Nothing
                End If

                If Not String.IsNullOrWhiteSpace(folioTransfer.RejectionObservation) Then
                    folio.RejectionObservation = folioTransfer.RejectionObservation
                Else
                    folio.RejectionObservation = Nothing
                End If

                folio.MarkAsModified()


                _folioTransferRepository.SaveEntity(folio)
                unitOfWork.Commit()

                Return New ActionResult(Of FolioTransfer) With {
                    .StateResult = True,
                    .ObjectEmbbeded = folio,
                    .Message = $"Operación exitosa"
                }
            Else
                Return New ActionResult(Of FolioTransfer) With {
                    .StateResult = False,
                    .Message = $"El traslado no se puede {operation} porque no está en estado 'Pendiente de aprobación'."
                }
            End If
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of FolioTransfer) With {
                .StateResult = False,
                .Message = $"Error al intentar {operation} el traslado: {ex.Message}"
            }
        End Try
    End Function

    ''' <summary>
    ''' Obtiene los folios asociados a un usuario a partir del SP AccountManagement.SP_GetUserFolios
    ''' </summary>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Public Function GetUserFolios(userCode As String) As ActionResult(Of List(Of GetUserFolios)) Implements IFolioTransferAdminService.GetUserFolios
        Try
            If String.IsNullOrWhiteSpace(userCode) Then
                Return New ActionResult(Of List(Of GetUserFolios)) With {.StateResult = False, .Message = "El código de usuario no puede estar vacío."}
            End If
            Dim folios = _folioTransferRepository.ExecuteStoredProcedure(Of GetUserFolios)("[AccountManagement].[SP_GetUserFolios]", {("@UserCode", userCode)})

            If folios IsNot Nothing AndAlso folios.Count > 0 Then
                Dim ListUserCodes As New List(Of String)
                For Each bu In folios
                    ListUserCodes.Add(bu.AssignedUserCode)
                Next
                'Se saca el listado de ids de usuario para enviar
                Dim ListUsers As New List(Of Domain.Security.Entities.User)
                'Se obtiene el listado de usuarios
                ListUsers = _IUserAdminService.ListUsersByCodes(ListUserCodes)
                If ListUsers IsNot Nothing AndAlso ListUsers.Count > 0 Then
                    'Se asigna el nombre completo del usuario al listado que se va a asignar al datasource de la rejilla
                    For Each bu In folios
                        Dim user = (From e In ListUsers Where e.UserCode = bu.AssignedUserCode Select e).FirstOrDefault
                        If user IsNot Nothing AndAlso user.Id > 0 AndAlso user.Person IsNot Nothing Then
                            bu.AssignedUserFullname = user.Person.Fullname
                        End If
                    Next
                End If
                Return New ActionResult(Of List(Of GetUserFolios)) With {.StateResult = True, .ObjectEmbbeded = folios}
            Else
                Return New ActionResult(Of List(Of GetUserFolios)) With {.StateResult = False, .Message = "No se encontraron folios asociados al usuario."}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of GetUserFolios)) With {.StateResult = False, .Message = ex.Message.ToString()}
        End Try
    End Function


#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            _folioTransferRepository = Nothing
            _folioAlertRepository = Nothing
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
