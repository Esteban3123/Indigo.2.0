'***********************************************************************
' Assembly         : Application.Glosas
' Author           : Juan F. Tamayo Puertas
' Created          : 2013-07-10
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Base.Entities
Imports Application.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Common.Entities
Imports Domain.Security
Imports Domain.Security.Entities
Imports System.Data.Entity.Core
Imports Application.Common
Imports System.Transactions

#End Region

''' <summary>
''' Servicio de parametros de tiempo
''' </summary>
Public Class TimeParametersAdminService
    Implements ITimeParametersAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de conjunto de parametros
    ''' </summary>
    Private _timeParametersRepository As ITimeParametersRepository
    ''' <summary>
    ''' Repositorio de días festivos
    ''' </summary>
    Private _holiDayRepository As IHolidayRepository
    ''' <summary>
    ''' Repositorio de auditoria basica
    ''' </summary>
    Private _objectionDRepository As IObjectionsReceptionDRepository
    ''' <summary>
    ''' Repositorio de auditoria basica
    ''' </summary>
    Private _conciliationDRepository As IConciliationDRepository
    ''' <summary>
    ''' Repositorio de auditoria basica
    ''' </summary>
    Private _userRepository As IUserRepository
    ''' <summary>
    ''' Admin de servicio de concepto de glosas
    ''' </summary>
    ''' <remarks></remarks>
    Private _ConceptGlosasAdminService As IConceptGlosasAdminService

#End Region

#Region "Buildes"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase <see cref="TimeParametersAdminService" />
    ''' </summary>
    ''' <param name="timeParametersRepository">Repositorio de conjunto de parametros</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal timeParametersRepository As ITimeParametersRepository, ByVal holiDayRepository As IHolidayRepository, ByVal objectionDRepository As IObjectionsReceptionDRepository,
                   ByVal conciliationDRepository As IConciliationDRepository, ByVal userRepository As IUserRepository, ByVal ConceptGlosasAdminService As IConceptGlosasAdminService)
        If timeParametersRepository Is Nothing Then
            Throw New ArgumentNullException("timeParametersRepository", "El repositorio no puede ser nulo")
        End If
        If holiDayRepository Is Nothing Then
            Throw New ArgumentNullException("holiDayRepository", "El repositorio no puede ser nulo")
        End If
        If objectionDRepository Is Nothing Then
            Throw New ArgumentNullException("objectionDRepository", "El repositorio no puede ser nulo")
        End If
        If conciliationDRepository Is Nothing Then
            Throw New ArgumentNullException("conciliationDRepository", "El repositorio no puede ser nulo")
        End If
        If userRepository Is Nothing Then
            Throw New ArgumentNullException("userRepository", "El repositorio no puede ser nulo")
        End If
        If ConceptGlosasAdminService Is Nothing Then
            Throw New ArgumentNullException("userRepository", "El repositorio no puede ser nulo")
        End If
        Me._timeParametersRepository = timeParametersRepository
        Me._holiDayRepository = holiDayRepository
        Me._conciliationDRepository = conciliationDRepository
        Me._objectionDRepository = objectionDRepository
        Me._userRepository = userRepository
        _ConceptGlosasAdminService = ConceptGlosasAdminService
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un conjunto de parametros de tiempo por su numero de Id
    ''' </summary>
    ''' <param name="Id">Id del conjunto de parametros</param>
    ''' <returns>El conjunto de parametros</returns>
    Public Function GetTimeParameters(Id As String) As TimeParameters Implements ITimeParametersAdminService.GetTimeParameters
        If String.IsNullOrEmpty(Id) = True Then
            Throw New ArgumentNullException("Id", "El parametro no puede ser nulo ni vacio")
        End If
        Try
            Return _timeParametersRepository.GetTimeParameters(Id.Trim())
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New TimeParameters
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el primero o por defecto conjunto de parametros de tiempo
    ''' </summary>
    ''' <returns>El conjunto de parametros</returns>
    Public Function GetTimeParametersSingleOrDefault(ByVal _IdIOperatingUnit As Integer, Entity As String) As TimeParameters Implements ITimeParametersAdminService.GetTimeParametersSingleOrDefault
        Try
            Return _timeParametersRepository.GetTimeParametersSingleOrDefault(_IdIOperatingUnit, Entity)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New TimeParameters
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los conjuntos de parametros de tiempo
    ''' </summary>
    ''' <returns>Lista de conjuntos de parametros</returns>
    Public Function ListAllTimeParameters(ByVal _IdIOperatingUnit As Integer, Optional Control As String = "") As List(Of TimeParameters) Implements ITimeParametersAdminService.ListAllTimeParameters
        Try
            Return _timeParametersRepository.ListAllTimeParameters(_IdIOperatingUnit, Control)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una lista de registros para mostrar los limites de tiempos 
    ''' en cada etapa del proceso de glosas
    ''' </summary>
    ''' <param name="Invoice">Numero Factura</param>
    Public Function ListControlParametersTime(Invoice As String, Entity As String, ByVal _IdIOperatingUnit As Integer) As List(Of ControlParametersTime) Implements ITimeParametersAdminService.ListControlParametersTime
        Try
            If String.IsNullOrEmpty(Invoice) = True Then
                Throw New ArgumentNullException("Factura vacia")
            End If
            If String.IsNullOrEmpty(Entity) = True Then
                Throw New ArgumentNullException("Entidad vacia")
            End If
            If String.IsNullOrEmpty(Invoice) = True Then
                Throw New ArgumentNullException("Compañia vacia")
            End If
            Dim controlParameter = _timeParametersRepository.GetTimeParametersSingleOrDefault(_IdIOperatingUnit, Entity)
            If controlParameter.Id = 0 Then
                controlParameter = GetTimeParametersSingleOrDefault(_IdIOperatingUnit, "0")
            End If
            Dim objGlosa As TrazabilityParametersTime = _objectionDRepository.getObjectionDParametersTime(Invoice, "1")
            Dim objReiteration As TrazabilityParametersTime = _objectionDRepository.getObjectionDParametersTime(Invoice, "2")
            Dim objConciliation As TrazabilityParametersTime = _conciliationDRepository.getConciliationDParametersTime(Invoice)
            Dim objUserGlosa As User = Nothing
            Dim objUserReiteration As User = Nothing
            Dim objUserConciliation As User = Nothing
            If objGlosa IsNot Nothing Then
                If objGlosa.ConfirmerUser IsNot Nothing Then
                    objUserGlosa = _userRepository.GetUserById(objGlosa.ConfirmerUser.ToString)
                End If
                With objGlosa
                    .MaxTimeResponse = controlParameter.MaxTimeResponse
                    .MaxTimeExtemporaneousGlosa = controlParameter.MaxTimeExtemporaneousGlosa
                    .MaxTimeSendingDocumentResponse = controlParameter.MaxTimeSendingDocumentResponse
                End With
            End If
            If objReiteration IsNot Nothing Then
                If objReiteration.ConfirmerUser IsNot Nothing Then
                    objUserReiteration = _userRepository.GetUserById(objReiteration.ConfirmerUser.ToString)
                End If
                With objReiteration
                    .MaxTimeResponse = controlParameter.MaxTimeReiterationResponse
                    .MaxTimeExtemporaneousGlosa = controlParameter.MaxTimeExtemporaneousReiteration
                    .MaxTimeSendingDocumentResponse = controlParameter.MaxTimeSendingReiterationDocumentResponse
                End With
            End If
            If objConciliation IsNot Nothing Then
                If objConciliation.ConfirmerUser IsNot Nothing Then
                    objUserConciliation = _userRepository.GetUserById(objConciliation.ConfirmerUser.ToString)
                End If
                With objConciliation
                    .MaxTimeResponse = controlParameter.MaxTimeConciliation
                End With
            End If
            Dim userGlosa As String = String.Empty
            Dim userReiteration As String = String.Empty
            Dim userConciliation As String = String.Empty
            If objUserGlosa IsNot Nothing AndAlso objUserGlosa.Id > 0 Then
                userGlosa = (objUserGlosa.UserCode.TrimEnd + " - " + IIf(objUserGlosa.Person IsNot Nothing, objUserGlosa.Person.Fullname.Trim, "")).ToString
            End If
            If objUserReiteration IsNot Nothing AndAlso objUserReiteration.Id > 0 Then
                userReiteration = (objUserReiteration.UserCode.TrimEnd + " - " + IIf(objUserReiteration.Person IsNot Nothing, objUserReiteration.Person.Fullname.Trim, "")).ToString
            End If
            If objUserConciliation IsNot Nothing AndAlso objUserConciliation.Id > 0 Then
                userConciliation = (objUserConciliation.UserCode.TrimEnd + " - " + IIf(objUserConciliation.Person IsNot Nothing, objUserConciliation.Person.Fullname.Trim, "")).ToString
            End If
            Dim listControl As List(Of ControlParametersTime) = ParametersTimeService.createListParametersTime(objGlosa, objReiteration, objConciliation, userGlosa, userReiteration, userConciliation)

            For Each item As ControlParametersTime In listControl
                Dim Fecini = item.OperationDate
                Dim FecFinal = item.LimitDate
                Dim timeSpanDay = TimeSpan.FromDays(1)
                Dim dif As TimeSpan = FecFinal - Fecini
                Dim diasCount As Integer = CInt(dif.Days)
                Dim FecSec As DateTime
                Dim timeRemaining = 0
                For n As Integer = 0 To diasCount
                    Dim controlRemaining = 0
                    FecSec = Fecini.AddDays(n)
                    If FecSec.Date = Date.Now.Date Then
                        controlRemaining = 1
                    End If
                    If FecSec.DayOfWeek = DayOfWeek.Saturday Then
                        If Not controlParameter.IsDaybusinessSaturday Then
                            If controlRemaining <> 0 Then
                                timeRemaining = timeRemaining + 1
                            End If
                            diasCount = diasCount + 1
                            FecFinal = FecFinal.AddDays(1)
                            Continue For
                        End If
                    End If
                    If FecSec.DayOfWeek = DayOfWeek.Sunday Then
                        If Not controlParameter.IsDaybusinessSunday Then
                            If controlRemaining <> 0 Then
                                If Not controlParameter.IsDaybusinessSaturday Then
                                    timeRemaining = timeRemaining + 2
                                Else
                                    timeRemaining = timeRemaining + 1
                                End If
                            End If
                            diasCount = diasCount + 1
                            FecFinal = FecFinal.AddDays(1)
                            Continue For
                        End If
                    End If
                    Dim holiDay = _holiDayRepository.GetHoliday(FecSec)
                    If holiDay IsNot Nothing AndAlso holiDay.Id > 0 Then
                        If controlRemaining <> 0 Then
                            If controlParameter.IsDaybusinessSaturday OrElse controlParameter.IsDaybusinessSunday Then
                                timeRemaining = timeRemaining + 1
                            End If
                        End If
                        diasCount = diasCount + 1
                        FecFinal = FecFinal.AddDays(1)
                        Continue For
                    End If
                Next
                item.LimitDate = FecFinal
                Dim newRemaining As Integer = IIf((item.LimitDate - Date.Now).TotalDays < 0, 0, (item.LimitDate - Date.Now).TotalDays)
                item.RemainingTime = item.RemainingTime + timeRemaining
                If item.CompleteDate IsNot Nothing Then
                    item.RemainingTime = 0
                End If
            Next
            Return listControl
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una lista de registros para mostrar los limites de tiempos 
    ''' en cada etapa del proceso de glosas
    ''' </summary>
    ''' <param name="Invoice">Numero Factura</param>
    Public Function ListControlParametersTimeMassive(controlParameter As TimeParameters, Invoice As String, listGlosas As List(Of TrazabilityParametersTime), listReiterations As List(Of TrazabilityParametersTime), listConciliations As List(Of TrazabilityParametersTime)) As List(Of ControlParametersTime) Implements ITimeParametersAdminService.ListControlParametersTimeMassive
        Try
            Dim userGlosa As String = String.Empty
            Dim userReiteration As String = String.Empty
            Dim userConciliation As String = String.Empty
            Dim dictionaryResponsible As New Dictionary(Of Integer, String)

            Dim objGlosa As TrazabilityParametersTime = listGlosas.Where(Function(d) d.InvoiceNumber = Invoice).FirstOrDefault()
            Dim objReiteration As TrazabilityParametersTime = listReiterations.Where(Function(d) d.InvoiceNumber = Invoice).FirstOrDefault()
            Dim objConciliation As TrazabilityParametersTime = listConciliations.Where(Function(d) d.InvoiceNumber = Invoice).FirstOrDefault()

            If objGlosa IsNot Nothing Then
                If objGlosa.ConfirmerUser IsNot Nothing Then
                    If Not dictionaryResponsible.ContainsKey(objGlosa.ConfirmerUser) Then
                        Dim objUserGlosa = _userRepository.GetUserById(objGlosa.ConfirmerUser.ToString)
                        If objUserGlosa IsNot Nothing AndAlso objUserGlosa.Id > 0 Then
                            userGlosa = (objUserGlosa.UserCode.TrimEnd + " - " + If(objUserGlosa.Person IsNot Nothing, objUserGlosa.Person.Fullname.Trim, "")).ToString
                        End If
                        dictionaryResponsible.Add(objGlosa.ConfirmerUser, userGlosa)
                    Else
                        userGlosa = dictionaryResponsible(objGlosa.ConfirmerUser)
                    End If
                End If

                With objGlosa
                    .MaxTimeResponse = controlParameter.MaxTimeResponse
                    .MaxTimeExtemporaneousGlosa = controlParameter.MaxTimeExtemporaneousGlosa
                    .MaxTimeSendingDocumentResponse = controlParameter.MaxTimeSendingDocumentResponse
                End With
            End If

            If objReiteration IsNot Nothing Then
                If objReiteration.ConfirmerUser IsNot Nothing Then
                    If Not dictionaryResponsible.ContainsKey(objReiteration.ConfirmerUser) Then
                        Dim objUserGlosa = _userRepository.GetUserById(objReiteration.ConfirmerUser.ToString)
                        If objUserGlosa IsNot Nothing AndAlso objUserGlosa.Id > 0 Then
                            userReiteration = (objUserGlosa.UserCode.TrimEnd + " - " + If(objUserGlosa.Person IsNot Nothing, objUserGlosa.Person.Fullname.Trim, "")).ToString
                        End If
                        dictionaryResponsible.Add(objReiteration.ConfirmerUser, userReiteration)
                    Else
                        userReiteration = dictionaryResponsible(objReiteration.ConfirmerUser)
                    End If
                End If

                With objReiteration
                    .MaxTimeResponse = controlParameter.MaxTimeReiterationResponse
                    .MaxTimeExtemporaneousGlosa = controlParameter.MaxTimeExtemporaneousReiteration
                    .MaxTimeSendingDocumentResponse = controlParameter.MaxTimeSendingReiterationDocumentResponse
                End With
            End If

            If objConciliation IsNot Nothing Then
                If objConciliation.ConfirmerUser IsNot Nothing Then
                    If Not dictionaryResponsible.ContainsKey(objConciliation.ConfirmerUser) Then
                        Dim objUserGlosa = _userRepository.GetUserById(objConciliation.ConfirmerUser.ToString)
                        If objUserGlosa IsNot Nothing AndAlso objUserGlosa.Id > 0 Then
                            userConciliation = (objUserGlosa.UserCode.TrimEnd + " - " + If(objUserGlosa.Person IsNot Nothing, objUserGlosa.Person.Fullname.Trim, "")).ToString
                        End If
                        dictionaryResponsible.Add(objConciliation.ConfirmerUser, userConciliation)
                    Else
                        userConciliation = dictionaryResponsible(objConciliation.ConfirmerUser)
                    End If
                End If

                With objConciliation
                    .MaxTimeResponse = controlParameter.MaxTimeConciliation
                End With
            End If

            Dim listControl As List(Of ControlParametersTime) = ParametersTimeService.createListParametersTime(objGlosa, objReiteration, objConciliation, userGlosa, userReiteration, userConciliation)

            For Each item As ControlParametersTime In listControl
                Dim Fecini = item.OperationDate
                Dim FecFinal = item.LimitDate
                Dim timeSpanDay = TimeSpan.FromDays(1)
                Dim dif As TimeSpan = FecFinal - Fecini
                Dim diasCount As Integer = CInt(dif.Days)
                Dim FecSec As DateTime
                Dim timeRemaining = 0
                For n As Integer = 0 To diasCount
                    Dim controlRemaining = 0
                    FecSec = Fecini.AddDays(n)
                    If FecSec.Date = Date.Now.Date Then
                        controlRemaining = 1
                    End If
                    If FecSec.DayOfWeek = DayOfWeek.Saturday Then
                        If Not controlParameter.IsDaybusinessSaturday Then
                            If controlRemaining <> 0 Then
                                timeRemaining = timeRemaining + 1
                            End If
                            diasCount = diasCount + 1
                            FecFinal = FecFinal.AddDays(1)
                            Continue For
                        End If
                    End If
                    If FecSec.DayOfWeek = DayOfWeek.Sunday Then
                        If Not controlParameter.IsDaybusinessSunday Then
                            If controlRemaining <> 0 Then
                                If Not controlParameter.IsDaybusinessSaturday Then
                                    timeRemaining = timeRemaining + 2
                                Else
                                    timeRemaining = timeRemaining + 1
                                End If
                            End If
                            diasCount = diasCount + 1
                            FecFinal = FecFinal.AddDays(1)
                            Continue For
                        End If
                    End If
                    Dim holiDay = _holiDayRepository.GetHoliday(FecSec)
                    If holiDay IsNot Nothing AndAlso holiDay.Id > 0 Then
                        If controlRemaining <> 0 Then
                            If controlParameter.IsDaybusinessSaturday OrElse controlParameter.IsDaybusinessSunday Then
                                timeRemaining = timeRemaining + 1
                            End If
                        End If
                        diasCount = diasCount + 1
                        FecFinal = FecFinal.AddDays(1)
                        Continue For
                    End If
                Next

                item.LimitDate = FecFinal
                Dim newRemaining As Integer = IIf((item.LimitDate - Date.Now).TotalDays < 0, 0, (item.LimitDate - Date.Now).TotalDays)
                item.RemainingTime = item.RemainingTime + timeRemaining
                If item.CompleteDate IsNot Nothing Then
                    item.RemainingTime = 0
                End If
            Next

            Return listControl
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Graba o actualiza un conjunto de parametros de tiempo
    ''' </summary>
    ''' <param name="obj">Conjunto de parametros de tiempo</param>
    ''' <returns>Un objeto con el resultado de la accion</returns>
    Public Function SaveTimeParameters(obj As TimeParameters, ByVal audit As AuditMessage) As ActionResult(Of TimeParameters) Implements ITimeParametersAdminService.SaveTimeParameters
        If obj Is Nothing Then
            Throw New ArgumentNullException("obj", "El parametro no puede ser nulo")
        End If
        Dim unitOfWork As IUnitWork = Me._timeParametersRepository.UnitWork
        Try
            If obj.ChangeTracker.State = ObjectState.Added Or obj.ChangeTracker.State = ObjectState.Modified Then
                Me._timeParametersRepository.SaveEntity(obj)
                unitOfWork.CommitAndRefreshChanges()
            End If
            'Auditoria basica
            If obj.ChangeTracker.State = ObjectState.Added Then
                IndigoAuditBasic.Execute("TimeParameters", audit.Functional, obj.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                Dim auditObject As New IndigoAuditSimpleEntity(Of TimeParameters)(obj, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                auditObject.Execute()
            ElseIf obj.ChangeTracker.State = ObjectState.Modified Then
                IndigoAuditBasic.Execute("TimeParameters", audit.Functional, obj.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                Dim auditObject As New IndigoAuditSimpleEntity(Of TimeParameters)(obj, audit, Infrastructure.CrossCutting.Audit.Actions.Update, obj.OriginalValue)
                auditObject.Execute()
            End If
            '================
            'obj.MarkAsUnchanged()
            Return New ActionResult(Of TimeParameters) With {.StateResult = True, .MessageResult = New List(Of String)(), .ObjectEmbbeded = obj}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of TimeParameters) With {.StateResult = False, .MessageResult = {"-999"}.ToList}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of TimeParameters) With {.StateResult = False, .MessageResult = {ex.Message}.ToList()}
        End Try
    End Function

    ''' <summary>
    ''' Graba o actualiza una lista de parametros de tiempo
    ''' </summary>
    ''' <param name="listParameters">Lista de parametros de tiempo</param>
    ''' <returns>Un objeto con el resultado de la accion</returns>
    Public Function SaveTimeListParameters(listParameters As List(Of TimeParameters), ByVal listConceptGloss As List(Of Domain.Entities.ConceptGlosas), ByVal Session As SessionValues) As ActionResult(Of List(Of TimeParameters)) Implements ITimeParametersAdminService.SaveTimeListParameters
        If listParameters Is Nothing OrElse listParameters.Count = 0 Then
            Throw New ArgumentNullException("Parameters", "Lista de parametros vacía")
        End If
        Dim unitOfWork As IUnitWork = Me._timeParametersRepository.UnitWork
        Dim auditProcess As IndigoAuditSimpleEntity(Of TimeParameters)
        Dim AuxTimeParameters As TimeParameters = Nothing
        Dim status As Integer
        Dim audit As AuditMessage = Session.AuditMessageWcf
        Try
            'configuro la transaccion
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.DefaultTimeout
            txSettings.IsolationLevel = IsolationLevel.ReadCommitted
            'inicio la transaccion
            Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
                For Each itemParameter As TimeParameters In listParameters
                    If itemParameter.ChangeTracker.State = ObjectState.Added Or itemParameter.ChangeTracker.State = ObjectState.Modified Then
                        If itemParameter.ChangeTracker.State = ObjectState.Added Then
                            If itemParameter.Customer IsNot Nothing Then
                                Dim _customer = itemParameter.Customer.Id
                                itemParameter.Customer = Nothing
                                itemParameter.IdCustomer = _customer
                            End If
                            itemParameter.CreationUser = audit.CodeUser
                            itemParameter.CreationDate = Date.Now()
                            status = Infrastructure.CrossCutting.Audit.Actions.Insert
                        ElseIf itemParameter.ChangeTracker.State = ObjectState.Modified Then
                            AuxTimeParameters = itemParameter.OriginalValue
                            itemParameter.ModificationUser = audit.CodeUser
                            itemParameter.ModificationDate = Date.Now()
                            status = Infrastructure.CrossCutting.Audit.Actions.Update
                        End If
                        Me._timeParametersRepository.SaveEntity(itemParameter)
                        unitOfWork.CommitAndRefreshChanges()
                        auditProcess = New IndigoAuditSimpleEntity(Of TimeParameters)(itemParameter, audit, status, AuxTimeParameters)
                        auditProcess.Execute()
                    End If
                Next
                If Session.IndigoGlossesIntegration = EGlossesIntegration.Native Or Session.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNet Or Session.IndigoGlossesIntegration = EGlossesIntegration.IntegractionFox Or Session.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNATIVE_MIGRATIONS Then
                    Dim ObjParametersTimeDefault As TimeParameters = (From e In listParameters Where e.IdCustomer Is Nothing Select e).ToList().SingleOrDefault
                    If ObjParametersTimeDefault IsNot Nothing Then
                        'Si descuenta honorarios procedemos a guardar la configuracion de los conceptos
                        If ObjParametersTimeDefault.DiscountedMedicalFees = True Then
                            Dim result As ActionResult = _ConceptGlosasAdminService.SaveConceptXFeesMedical(listConceptGloss, audit)
                            If result.StateResult = False Then
                                Return New ActionResult(Of List(Of TimeParameters)) With {.StateResult = False, .MessageResult = New List(Of String)(), .ObjectEmbbeded = listParameters}
                            End If
                        End If
                    End If
                End If
                scope.Complete()
            End Using
            Return New ActionResult(Of List(Of TimeParameters)) With {.StateResult = True, .MessageResult = New List(Of String)(), .ObjectEmbbeded = listParameters}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of List(Of TimeParameters)) With {.StateResult = False, .MessageResult = {"-999"}.ToList}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return New ActionResult(Of List(Of TimeParameters)) With {.StateResult = False, .MessageResult = {ex.Message}.ToList()}
        End Try
    End Function

    ''' <summary>
    ''' Eliminar un conjunto de parametros de tiempo
    ''' </summary>
    ''' <param name="obj">Parametros de Tiempo</param>
    ''' <param name="audit">Auditoria</param>
    ''' <returns>Action Result</returns>
    Public Function DeleteTimeParameters(obj As TimeParameters, audit As AuditMessage) As ActionResult Implements ITimeParametersAdminService.DeleteTimeParameters
        If obj Is Nothing Then
            Throw New ArgumentNullException("Objeto Parametros de Tiempo vacío")
        End If
        Dim unitOfWork As IUnitWork = _timeParametersRepository.UnitWork
        Try
            'elimino los parametros
            _timeParametersRepository.DeleteEntity(obj)
            unitOfWork.Commit()
            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute("TimeParameters", audit.Functional, obj.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            '/***** Auditoria Avanzada *****/
            Dim auditObject As New IndigoAuditSimpleEntity(Of TimeParameters)(obj, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False}
        End Try
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _ConceptGlosasAdminService.Dispose()
            End If
            _timeParametersRepository = Nothing
            _holiDayRepository = Nothing
            _conciliationDRepository = Nothing
            _objectionDRepository = Nothing
            _userRepository = Nothing
            _ConceptGlosasAdminService = Nothing
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
