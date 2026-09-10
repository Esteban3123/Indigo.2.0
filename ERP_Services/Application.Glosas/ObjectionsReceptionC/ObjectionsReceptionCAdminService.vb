'***********************************************************************
' Assembly         : Application.Glosas
' Author           : RafaelPatiño
' Created          : 09-04-2013
'
' Last Modified By : RafaelPatiño
' Last Modified On : 2013-09-22
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports System.Transactions

Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Common.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Base
Imports Domain.InterfaceERPGlosa
Imports System.Data.SqlClient


Public Class ObjectionsReceptionCAdminService
    Implements IObjectionsReceptionCAdminService


    Dim _ObjectionsReceptionCRepository As IObjectionsReceptionCRepository
    Dim _ObjectionsReceptionDRepository As IObjectionsReceptionDRepository
    Dim _PortfolioGlosadaRepository As IPortfolioGlosadaRepository
    Dim _InvoiceDeatilRepository As IInvoiceDetailRepository
    Dim _CustomerRepository As ICustomerRepository
    Dim _ConsecutiveRepository As IConsecutiveRepository
    Dim _MovementGlosaRepository As IMovementGlosaRepository
    Private _IInterfaceParametersRepository As IInterfaceParametersRepository
    Private _InterfacePublicFOX As IInterfacePublicFOX
    Private _IResponsibleRepository As IResponsibleRepository
    Private _timeParametersRepository As ITimeParametersRepository
    Private _objectionDRepository As IObjectionsReceptionDRepository
    Private _conciliationDRepository As IConciliationDRepository

    ''' <summary>
    ''' Initializa una nueva instancia de la clase <see cref="ObjectionsReceptionCAdminService" />.
    ''' </summary>
    ''' <param name="ObjectionsReceptionCRepository">el repositorio para el manejo de la recepcion de objeciones.</param>
    Public Sub New(ByVal ConsecutiveRepository As IConsecutiveRepository, ByVal ObjectionsReceptionDRepository As IObjectionsReceptionDRepository, ByVal ObjectionsReceptionCRepository As IObjectionsReceptionCRepository, ByVal customerRepository As ICustomerRepository, ByVal PortfolioGlosadaRepository As IPortfolioGlosadaRepository, MovementGlosaRepository As IMovementGlosaRepository, IInterfaceParametersRepository As IInterfaceParametersRepository, InterfacePublicFOX As IInterfacePublicFOX,
                   IResponsibleRepository As IResponsibleRepository, InvoiceDeatilRepository As IInvoiceDetailRepository, ByVal timeParametersRepository As ITimeParametersRepository, ByVal objectionDRepository As IObjectionsReceptionDRepository, ByVal conciliationDRepository As IConciliationDRepository)
        If ObjectionsReceptionCRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Recepción de Objeciones Vacio")
        End If
        If ObjectionsReceptionCRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Recepción Detalle de Objeciones Vacio")
        End If
        If PortfolioGlosadaRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Cartera Glosada Vacio")
        End If
        If MovementGlosaRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Movimiento Factura Vacio")
        End If

        _ObjectionsReceptionCRepository = ObjectionsReceptionCRepository
        _ObjectionsReceptionDRepository = ObjectionsReceptionDRepository
        _PortfolioGlosadaRepository = PortfolioGlosadaRepository
        _MovementGlosaRepository = MovementGlosaRepository
        _CustomerRepository = customerRepository
        _ConsecutiveRepository = ConsecutiveRepository
        _IInterfaceParametersRepository = IInterfaceParametersRepository
        _InterfacePublicFOX = InterfacePublicFOX
        _IResponsibleRepository = IResponsibleRepository
        _InvoiceDeatilRepository = InvoiceDeatilRepository
        _timeParametersRepository = timeParametersRepository
        _objectionDRepository = objectionDRepository
        _conciliationDRepository = conciliationDRepository
    End Sub

    ''' <summary>
    ''' Funcion para anular un Oficio
    ''' </summary>
    ''' <param name="ObjectionsReceptionC">Oficio</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function InvalidateObjectionsReceptionC(ObjectionsReceptionC As Domain.Entities.GlosaObjectionsReceptionC, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult Implements IObjectionsReceptionCAdminService.InvalidateObjectionsReceptionC
        If ObjectionsReceptionC Is Nothing Then
            Throw New ArgumentNullException("Recepción Vacio")
        End If
        Dim AuxObjC As GlosaObjectionsReceptionC = Nothing
        'AuxObjC = ObjectionsReceptionC.clone
        'para la auditoria
        If ObjectionsReceptionC.ChangeTracker.State = ObjectState.Modified Then
            AuxObjC = ObjectionsReceptionC.OriginalValue
        End If
        Dim unitWorkObjectionsReceptionC As IUnitWork = _ObjectionsReceptionCRepository.UnitWork
        Dim unitWorkObjectionsReceptionD As IUnitWork = _ObjectionsReceptionDRepository.UnitWork
        Dim unitWorkMovementGlosas As IUnitWork = _MovementGlosaRepository.UnitWork
        Dim unitWorkPortfolio As IUnitWork = _PortfolioGlosadaRepository.UnitWork
        'variable para retornar un ActionResult 
        Dim _actionResult As ActionResult = New ActionResult()
        'variable para almacenar la lista de mensaje a retornar
        Dim _Listmessage As New List(Of String)
        'validacio
        Dim ValidMovement As Boolean = True
        Try
            'configuro la transaccion
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.DefaultTimeout
            txSettings.IsolationLevel = IsolationLevel.ReadCommitted
            'inicio la transaccion
            Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
                'grabo la cabecera de la objecion, el detalle "lista facturas" y el detalle de cada factura 
                If ObjectionsReceptionC.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    Dim Invalidate As Boolean = True
                    'valido primero que no tenga movimientos
                    'consulto el numero de factura mediante el objeto ObjectionreceptionD
                    Dim ListD As List(Of GlosaObjectionsReceptionD) = _ObjectionsReceptionDRepository.ListAllObjectionsReceptionDWithIncludes(ObjectionsReceptionC.Id)
                    If ListD.Count > 0 Then
                        If ListD.Any(Function(x) x.State = 1) Then
                            Return New ActionResult With {.StateResult = False, .Message = "Elimine las facturas sin confirmar del Documento para proceder con la anulación"}
                        End If
                        For i As Integer = 0 To ListD.Count - 1
                            Dim ListMovement = _MovementGlosaRepository.ListAllMovementGlosaByInvoiceNumber((ListD(i).InvoiceNumber))
                            If ListMovement.Count > 0 Then
                                If ListD(i).DocumentType = "2" Then
                                    For Each itemMovement In ListMovement
                                        If itemMovement.ResponsibleReiterationId IsNot Nothing Then
                                            If ListD(i).GlosaPortfolioGlosada.State = 4 AndAlso itemMovement.State = 3 Then
                                                itemMovement.ResponsibleReiterationId = Nothing
                                                itemMovement.ValueReiterated = Nothing
                                                itemMovement.RationaleReiteration = Nothing
                                                itemMovement.RationaleDateReiteration = Nothing
                                                itemMovement.State = 2
                                                itemMovement.TempState = 3
                                                _MovementGlosaRepository.SaveEntity(itemMovement)
                                                unitWorkMovementGlosas.Commit()
                                            Else
                                                Invalidate = False
                                                Exit For
                                            End If
                                        End If
                                    Next
                                ElseIf ListD(i).DocumentType = "1" Then
                                    Invalidate = False
                                End If

                                If Invalidate = False Then
                                    Exit For
                                End If
                            End If
                        Next
                    End If
                    'si tiene ya moviminetos, no permito anular el oficio
                    If Invalidate = False Then
                        ValidMovement = False
                        unitWorkMovementGlosas.RollbackChanges()
                        unitWorkObjectionsReceptionD.RollbackChanges()
                        unitWorkObjectionsReceptionC.RollbackChanges()
                    Else
                        For Each itemD As GlosaObjectionsReceptionD In ListD
                            itemD.State = 4 'anulamos factura para liberarla
                            If itemD.GlosaPortfolioGlosada IsNot Nothing Then
                                If itemD.GlosaPortfolioGlosada.State = 4 Then
                                    itemD.GlosaPortfolioGlosada.State = 11
                                    itemD.GlosaPortfolioGlosada.TempState = 4
                                End If
                            End If
                            _ObjectionsReceptionDRepository.SaveEntity(itemD)
                            unitWorkObjectionsReceptionD.Commit()
                        Next

                        'confirmo la unidad de trabajo  la cabecera de la objecion
                        _ObjectionsReceptionCRepository.SaveEntity(ObjectionsReceptionC)
                        unitWorkObjectionsReceptionC.Commit()
                        ValidMovement = True
                        scope.Complete()
                    End If
                End If
            End Using
            '/***** Auditoria ********/
            If ValidMovement Then
                IndigoAuditBasic.Execute("GlosaObjectionsReceptionC", audit.Functional, ObjectionsReceptionC.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Anular, audit.Company, audit.ContainerSecurity)
                Dim auditObject As New IndigoAuditSimpleEntity(Of GlosaObjectionsReceptionC)(ObjectionsReceptionC, audit, Infrastructure.CrossCutting.Audit.Actions.Update, AuxObjC)
                auditObject.Execute()
            End If
            _actionResult.StateResult = ValidMovement
            Return _actionResult
        Catch ex As OptimisticConcurrencyException
            unitWorkMovementGlosas.RollbackChanges()
            unitWorkObjectionsReceptionD.RollbackChanges()
            unitWorkObjectionsReceptionC.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitWorkMovementGlosas.RollbackChanges()
            unitWorkObjectionsReceptionD.RollbackChanges()
            unitWorkObjectionsReceptionC.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Dim Mensaje As New List(Of String)
            Mensaje.Add(ex.Message)
            Return New ActionResult With {.StateResult = False, .MessageResult = Mensaje}
            Return _actionResult
        End Try
    End Function
    ''' <summary>
    ''' Funcion para confirmar una objecion
    ''' </summary>
    ''' <param name="ObjectionsReceptionC">objeto cabecera de recepcion</param>
    ''' <param name="audit"></param>
    ''' <returns>Objeto ActioResult</returns>
    Public Function ConfirmObjectionsReceptionC(ObjectionsReceptionC As Domain.Entities.GlosaObjectionsReceptionC, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult Implements IObjectionsReceptionCAdminService.ConfirmObjectionsReceptionC
        Dim unitWorkObjectionsReceptionC As IUnitWork = _ObjectionsReceptionCRepository.UnitWork
        Dim unitWorkObjectionsReceptionD As IUnitWork = TryCast(_ObjectionsReceptionDRepository.UnitWork, IUnitWork)
        Dim unitWorkPortFolioGlosada As IUnitWork = TryCast(_PortfolioGlosadaRepository.UnitWork, IUnitWork)
        Try
            If ObjectionsReceptionC Is Nothing Then
                Throw New ArgumentNullException("Recepción Vacio")
            End If
            Dim AuxObjC As GlosaObjectionsReceptionC = Nothing

            'Respuesta de radicación
            If ObjectionsReceptionC.RadicateResponse IsNot Nothing AndAlso ObjectionsReceptionC.RadicateResponse.Id > 0 Then
                ObjectionsReceptionC.ChangeTracker.State = ObjectState.Modified
            ElseIf ObjectionsReceptionC.State = 2 AndAlso ObjectionsReceptionC.RadicateResponse IsNot Nothing Then
                ObjectionsReceptionC.RadicateResponse.CreationUser = audit.CodeUser
                ObjectionsReceptionC.RadicateResponse.CreationDate = Date.Now()
            End If

            'para la auditoria
            If ObjectionsReceptionC.ChangeTracker.State = ObjectState.Modified Then
                AuxObjC = ObjectionsReceptionC.OriginalValue
            End If
            'variable para retornar un ActionResult 
            Dim _actionResult As ActionResult = New ActionResult()
            'variable para almacenar la lista de mensaje a retornar
            Dim _Listmessage As New List(Of String)
            'validacio
            Dim ValidMovement As Boolean = True
            'configuro la transaccion
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.DefaultTimeout
            txSettings.IsolationLevel = IsolationLevel.ReadCommitted
            'inicio la transaccion
            Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
                'grabo la cabecera de la objecion, el detalle "lista facturas" y el detalle de cada factura 
                If ObjectionsReceptionC.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    _ObjectionsReceptionCRepository.SaveEntity(ObjectionsReceptionC)
                    unitWorkObjectionsReceptionC.Commit()
                    ValidMovement = True
                End If
                scope.Complete()
            End Using
            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute("GlosaObjectionsReceptionC", audit.Functional, ObjectionsReceptionC.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Confirmar, audit.Company, audit.ContainerSecurity)
            '/***** Auditoria Avanzada *****/
            Dim auditObject As New IndigoAuditSimpleEntity(Of GlosaObjectionsReceptionC)(ObjectionsReceptionC, audit, Infrastructure.CrossCutting.Audit.Actions.Update, AuxObjC)
            auditObject.Execute()
            _actionResult.StateResult = ValidMovement
            Return _actionResult
        Catch ex As OptimisticConcurrencyException
            unitWorkObjectionsReceptionD.RollbackChanges()
            unitWorkPortFolioGlosada.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitWorkObjectionsReceptionD.RollbackChanges()
            unitWorkPortFolioGlosada.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Dim Mensaje As List(Of String) = New List(Of String)
            Mensaje.Add(If(String.IsNullOrEmpty(ex?.InnerException?.InnerException?.Message), ex.Message, ex.InnerException.InnerException.Message))
            Return New ActionResult With {.StateResult = False, .MessageResult = Mensaje}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una objecion completa con sus agregados por su numero de Id
    ''' </summary>
    ''' <param name="id">Id de la objecion</param>
    ''' <returns>La objecion</returns>
    Public Function GetObjectionCWithAgregatesById(id As String, timeParameterAdmin As ITimeParametersAdminService, ByVal _IdIOperatingUnit As Integer) As GlosaObjectionsReceptionC Implements IObjectionsReceptionCAdminService.GetObjectionCWithAgregatesById
        If String.IsNullOrEmpty(id) = True Then
            Throw New ArgumentNullException("id", "Codigo de objecion Vacio")
        End If
        Try
            Dim Busqueda = _ObjectionsReceptionCRepository.GetObjectionCWithAgregatesById(id.Trim())
            If Busqueda.GlosaObjectionsReceptionD.Any Then
                Dim controlParameter = _timeParametersRepository.GetTimeParametersSingleOrDefault(_IdIOperatingUnit, Busqueda.CustomerId)
                If controlParameter.Id = 0 Then
                    controlParameter = _timeParametersRepository.GetTimeParametersSingleOrDefault(_IdIOperatingUnit, "0")
                End If
                Dim invoices = Busqueda.GlosaObjectionsReceptionD.Select(Function(d) d.InvoiceNumber).ToArray()
                Dim listGlosas As List(Of TrazabilityParametersTime) = _objectionDRepository.getListObjectionDParametersTime(invoices, "1")
                Dim listReiterations As List(Of TrazabilityParametersTime) = _objectionDRepository.getListObjectionDParametersTime(invoices, "2")
                Dim listConciliations As List(Of TrazabilityParametersTime) = _conciliationDRepository.getListConciliationDParametersTime(invoices)

                For Each item As GlosaObjectionsReceptionD In Busqueda.GlosaObjectionsReceptionD
                    If item.GlosaPortfolioGlosada.State = "3" Or item.GlosaPortfolioGlosada.State = "6" Then
                        item.StateRecord = True
                    End If
                    Dim time = timeParameterAdmin.ListControlParametersTimeMassive(controlParameter, item.InvoiceNumber, listGlosas, listReiterations, listConciliations)
                    Dim timeMove As Integer
                    Dim timeParameter As Integer
                    If item.DocumentType = "1" Then
                        timeMove = time.Where(Function(c) c.Code = "03").SingleOrDefault.RemainingTime
                        timeParameter = time.Where(Function(c) c.Code = "03").SingleOrDefault.TimeParameters
                    End If
                    If item.DocumentType = "2" Then
                        timeMove = time.Where(Function(c) c.Code = "06").SingleOrDefault.RemainingTime
                        timeParameter = time.Where(Function(c) c.Code = "06").SingleOrDefault.TimeParameters
                    End If
                    Dim state As String = String.Empty
                    If timeParameter > 0 Then
                        Dim percent As Integer = (timeMove * 100) / timeParameter
                        If percent >= 55 Then
                            state = "1"
                        End If
                        If percent < 55 Then
                            state = "2"
                        End If
                        If percent = 0 Then
                            state = "3"
                        End If
                    Else
                        state = "1"
                    End If
                    item.State = state
                Next
            End If
            Return Busqueda
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una objecion sin sus agregados por su numero de Id
    ''' </summary>
    ''' <param name="id">Id de la objecion</param>
    ''' <returns>La objecion</returns>
    Public Function GetObjectionCWithoutAgregatesById(id As Integer, ByVal _IdIOperatingUnit As Integer) As GlosaObjectionsReceptionC Implements IObjectionsReceptionCAdminService.GetObjectionCWithoutAgregatesById
        Try
            Return _ObjectionsReceptionCRepository.GetObjectionCWithoutAgregatesById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' obtiene una recepcion de objeciones
    ''' </summary>
    ''' <param name="codeObjectionReceptionC">codigo de la recepcion </param>
    ''' <returns>una recepcion de objecion </returns>
    Public Function GetObjection(codeObjectionReceptionC As String) As Domain.Entities.GlosaObjectionsReceptionC Implements IObjectionsReceptionCAdminService.GetObjection
        If String.IsNullOrEmpty(codeObjectionReceptionC) = True Then
            Throw New ArgumentNullException("Codigo de objecion Vacio")
        End If
        Try
            Return _ObjectionsReceptionCRepository.GetObjection(codeObjectionReceptionC)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' lista de todas las recepcion de objeciones
    ''' </summary>
    ''' <returns>lista de recpcion de objeciones</returns>
    ''' <remarks></remarks>
    Public Function ListAllObjectionsReceptionC() As List(Of Domain.Entities.GlosaObjectionsReceptionC) Implements IObjectionsReceptionCAdminService.ListAllObjectionsReceptionC
        Try
            Return _ObjectionsReceptionCRepository.ListAllObjectionsReceptionC()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' guarda una objecion con su detalle y se persiste  el detalle de la factura
    ''' </summary>
    ''' <param name="ObjectionsReceptionC">la objecion de recepcion</param>
    ''' <param name="audit">mensaje de auditoria</param>
    ''' <returns>valor de si guardo o no</returns>
    Public Function SaveObjectionsReceptionC(ObjectionsReceptionC As Domain.Entities.GlosaObjectionsReceptionC, IndigoCompany As String, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult Implements IObjectionsReceptionCAdminService.SaveObjectionsReceptionC
        If ObjectionsReceptionC Is Nothing Then
            Throw New ArgumentNullException("Recepción Vacio")
        End If

        Dim AuxObjC As GlosaObjectionsReceptionC = Nothing
        If ObjectionsReceptionC.ChangeTracker.State = ObjectState.Modified Then
            AuxObjC = ObjectionsReceptionC.OriginalValue
        End If
        'variable para retornar un ActionResult 
        Dim _actionResult As ActionResult = New ActionResult()
        'asigno la lista generica de errores al objeto final de retorno
        _actionResult.MessageResult = New List(Of String)
        'creo la unidad de trabajo para el manejo del de la cabecera del la objecion
        Dim unitWorkObjectionsReceptionC As IUnitWork = TryCast(_ObjectionsReceptionCRepository.UnitWork, IUnitWork)
        Dim unitOfWorkConsecutive As IUnitWork = TryCast(_ConsecutiveRepository.UnitWork, IUnitWork)
        Try
            'configuro la transaccion
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.DefaultTimeout
            txSettings.IsolationLevel = IsolationLevel.ReadCommitted
            If _actionResult.MessageResult.Count <= 0 Then
                _actionResult.StateResult = True
                Dim consecutive As Domain.Entities.Consecutive = Nothing
                If ObjectionsReceptionC.ChangeTracker.State = ObjectState.Added Then
                    consecutive = _ConsecutiveRepository.GetConsecutiveByCode("1") 'consecutivo de radicacion de facturas
                    If consecutive.Id = 0 Then
                        Return New ActionResult With {.StateResult = False, .MessageResult = {"No se encontro consecutivo con código 1 para Recepcion de Objeciones"}.ToList()}
                    End If
                    'Actualizamos el numero de consecutivo
                    consecutive.NumberConsecutive += 1
                    _ConsecutiveRepository.UpdateEntity(consecutive)
                    unitOfWorkConsecutive.Commit()
                End If
                Dim auditProcess As IndigoAuditSimpleEntity(Of GlosaObjectionsReceptionC)
                Dim auxGlosaObjectionsReceptionC As GlosaObjectionsReceptionC = Nothing
                Dim status As Integer
                'inicio la transaccion
                Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
                    'grabo la cabecera de la objecion, el detalle "lista facturas" y el detalle de cada factura 
                    If ObjectionsReceptionC.ChangeTracker.State = ObjectState.Added Then
                        'si es nuevo incrementamos el consecutivo
                        ObjectionsReceptionC.RadicatedConsecutive = CInt(consecutive.NumberConsecutive)
                        ObjectionsReceptionC.CreationUser = audit.CodeUser
                        ObjectionsReceptionC.CreationDate = Date.Now()
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert
                        _ObjectionsReceptionCRepository.AddEntity(ObjectionsReceptionC)
                        'confirmo la unidad de trabajo  la cabecera de la objecion
                        unitWorkObjectionsReceptionC.Commit()
                        auditProcess = New IndigoAuditSimpleEntity(Of GlosaObjectionsReceptionC)(ObjectionsReceptionC, audit, status)
                        auditProcess.Execute()
                    ElseIf ObjectionsReceptionC.ChangeTracker.State = ObjectState.Modified Then
                        'actualizo la entidad
                        ObjectionsReceptionC.ModificationUser = audit.CodeUser
                        ObjectionsReceptionC.ModificationDate = Date.Now()
                        status = Infrastructure.CrossCutting.Audit.Actions.Update
                        _ObjectionsReceptionCRepository.UpdateEntity(ObjectionsReceptionC)
                        'confirmo la unidad de trabajo  la cabecera de la objecion
                        unitWorkObjectionsReceptionC.Commit()
                        auditProcess = New IndigoAuditSimpleEntity(Of GlosaObjectionsReceptionC)(ObjectionsReceptionC, audit, status, AuxObjC)
                        auditProcess.Execute()
                    End If
                    'confirmo la transaccion
                    scope.Complete()
                End Using
                'variable lista de string para retornar el numero de factura y el id de la nueva recepción
                Dim Consecutivo As List(Of String) = New List(Of String)
                'agrego a la lista el id de la recepción
                Consecutivo.Add(ObjectionsReceptionC.Id.ToString)
                Consecutivo.Add(ObjectionsReceptionC.RadicatedConsecutive.ToString)
                'asignacion a la variable de retorno
                _actionResult.MessageResult = Consecutivo
            Else
                'En caso de error
                _actionResult.StateResult = False
            End If
            'retorno objeto de respuesta
            Return _actionResult
        Catch ex As OptimisticConcurrencyException
            unitWorkObjectionsReceptionC.RollbackChanges()
            unitOfWorkConsecutive.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            'descarto los cambios en eliminacion cabecera de la objeción 
            unitWorkObjectionsReceptionC.RollbackChanges()
            unitOfWorkConsecutive.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Dim Mensaje As New List(Of String)
            Mensaje.Add(ex.Message)
            Return New ActionResult With {.StateResult = False, .MessageResult = Mensaje}
            Return _actionResult
        End Try
    End Function
    ''' <summary>
    ''' Guardar el detalle de oficio
    ''' </summary>
    ''' <param name="RadicatedConsecutive"></param>
    ''' <param name="ContainerName"></param>
    ''' <param name="GlosaObjectionsReceptionD"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveObjectionsReceptionDAndPersist(RadicatedConsecutive As String, ContainerName As String, GlosaObjectionsReceptionD As GlosaObjectionsReceptionD, session As SessionValues) As ActionResult Implements IObjectionsReceptionCAdminService.SaveObjectionsReceptionDAndPersist
        Dim Mensaje As New List(Of String)
        If GlosaObjectionsReceptionD Is Nothing Then
            Throw New ArgumentNullException("Lista de Detalle no puede ser vacia")
        End If
        'variable para retornar un ActionResult 
        Dim _actionResult As ActionResult = New ActionResult()
        'asigno la lista generica de errores al objeto final de retorno
        _actionResult.MessageResult = New List(Of String)
        Dim _GlosaPortfolioGlosada As GlosaPortfolioGlosada = Nothing
        'creo la unidad de trabajo para el detalle de la objeción
        Dim unitWorkObjectionsReceptionD As IUnitWork = TryCast(_ObjectionsReceptionDRepository.UnitWork, IUnitWork)
        Dim UnitWorkPortfolioGlosada As IUnitWork = TryCast(_PortfolioGlosadaRepository.UnitWork, IUnitWork)
        Dim unitWorkInvoiceDetails As IUnitWork = _InvoiceDeatilRepository.UnitWork
        'si el detalle de la recepción esta como adicionado se procede a realizar la persistencia del detalle de factura
        If GlosaObjectionsReceptionD.ChangeTracker.State = ObjectState.Added And GlosaObjectionsReceptionD.DocumentType = 1 And GlosaObjectionsReceptionD.Invalidate = False Then
            'por cada factura lanzo la funcion de cargar el detalle de la misma mediante el numero de factura y el numero de ingreso o consecutivo
            Dim _ListInvoiceDetailPersist As New List(Of SP_invoiceDetailList_Result)
            Dim _ListInvoiceDetailPersistFOX As New List(Of SP_invoiceDetailList__FOX_Result)
            Dim _ListInvoiceDetailPersistNET As New List(Of SP_invoiceDetailList__NET_Result)
            Dim _ListInvoiceDetailPersistNaviteMigrations As New List(Of SP_invoiceDetailList_NAVITEINTEGRATION_Result)
            'If session.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
            '    _ListInvoiceDetailPersist = Me.ListInvoiceDetails(ContainerName, GlosaObjectionsReceptionD.GlosaPortfolioGlosada.InvoiceNumber, GlosaObjectionsReceptionD.GlosaPortfolioGlosada.IngressNumber, session)
            'ElseIf session.IndigoGlossesIntegration = EGlossesIntegration.Native Then
            '    If _GlosaPortfolioGlosada.OpeningBalance = True Then
            '        _ListInvoiceDetailPersistFOX = _ObjectionsReceptionCRepository.ListInvoiceDetailFOX(ContainerName, session.HisContainer, session.SecurityContainer, GlosaObjectionsReceptionD.GlosaPortfolioGlosada.InvoiceNumber, GlosaObjectionsReceptionD.GlosaPortfolioGlosada.IngressNumber)
            '    Else
            '        _ListInvoiceDetailPersist = Me.ListInvoiceDetails(ContainerName, GlosaObjectionsReceptionD.GlosaPortfolioGlosada.InvoiceNumber, GlosaObjectionsReceptionD.GlosaPortfolioGlosada.IngressNumber, session)
            '    End If
            'End If
            _GlosaPortfolioGlosada = GlosaObjectionsReceptionD.GlosaPortfolioGlosada

            'Facturas por saldo inicial
            If _GlosaPortfolioGlosada.OpeningBalance = True Then
                If session.IndigoGlossesIntegration = 3 Then
                    _ListInvoiceDetailPersistFOX = _ObjectionsReceptionCRepository.ListInvoiceDetailFOX(ContainerName, session.HisContainer, session.SecurityContainer, GlosaObjectionsReceptionD.GlosaPortfolioGlosada.InvoiceNumber, GlosaObjectionsReceptionD.GlosaPortfolioGlosada.IngressNumber)
                    If _ListInvoiceDetailPersistFOX IsNot Nothing Then
                        For j As Integer = 0 To _ListInvoiceDetailPersistFOX.Count - 1
                            'creo y asingo valores de un objeto detalle factura "GlosaInvoiceDetail"
                            Dim _invoiceDetail As New GlosaInvoiceDetail
                            With _invoiceDetail
                                .InvoiceNumber = _ListInvoiceDetailPersistFOX(j).InvoiceNumber.ToString.Trim()
                                .ServiceDate = _ListInvoiceDetailPersistFOX(j).ServiceDate.ToString.Trim()
                                .ServiceCode = _ListInvoiceDetailPersistFOX(j).ServiceCode.ToString.Trim()
                                .ServiceName = _ListInvoiceDetailPersistFOX(j).ServiceName.ToString.Trim()
                                If _ListInvoiceDetailPersistFOX(j).InvoiceDetailId IsNot Nothing Then
                                    .InvoiceDetailNativeId = _ListInvoiceDetailPersistFOX(j).InvoiceDetailId
                                End If
                                If _ListInvoiceDetailPersistFOX(j).ServiceOrderDetailId IsNot Nothing Then
                                    .ServiceOrderDetailId = _ListInvoiceDetailPersistFOX(j).ServiceOrderDetailId
                                End If
                                .ServiceAreaCode = _ListInvoiceDetailPersistFOX(j).ServiceAreaCode.ToString.Trim()
                                .DescriptionServiceArea = _ListInvoiceDetailPersistFOX(j).DescriptionServiceArea.ToString.Trim()
                                .MedicalCode = _ListInvoiceDetailPersistFOX(j).MedicalCode.ToString.Trim()
                                .MedicalName = _ListInvoiceDetailPersistFOX(j).MedicalName.ToString.Trim()
                                If _ListInvoiceDetailPersistFOX(j).BillerCode IsNot Nothing Then
                                    .BillerCode = _ListInvoiceDetailPersistFOX(j).BillerCode.ToString.Trim()
                                End If
                                If _ListInvoiceDetailPersistFOX(j).BillerName IsNot Nothing Then
                                    .BillerName = _ListInvoiceDetailPersistFOX(j).BillerName.ToString.Trim()
                                End If
                                .BillingGroupCode = _ListInvoiceDetailPersistFOX(j).BillingGroupCode.ToString.Trim()
                                .BillingGroup = _ListInvoiceDetailPersistFOX(j).BillingGroup.ToString.Trim()
                                .ValueServiceManual = _ListInvoiceDetailPersistFOX(j).ValueServiceManual.ToString.Trim()
                                .UnitValue = _ListInvoiceDetailPersistFOX(j).UnitValue.ToString.Trim()
                                .InvoicedValue = _ListInvoiceDetailPersistFOX(j).InvoicedValue.ToString.Trim()
                                .ValorEntidad = .InvoicedValue
                                .Ammount = _ListInvoiceDetailPersistFOX(j).Ammount.ToString.Trim()
                                .CostCenterCode = _ListInvoiceDetailPersistFOX(j).CostCenterCode.ToString.Trim()
                                .CostCenterName = _ListInvoiceDetailPersistFOX(j).CostCenterName.ToString.Trim()
                                .TypeServiceProduct = _ListInvoiceDetailPersistFOX(j).TypeServiceProduct.ToString.Trim()
                                .TypeProcedure = _ListInvoiceDetailPersistFOX(j).TypeProcedure.ToString.Trim()
                                .AccountantAccountIncome = _ListInvoiceDetailPersistFOX(j).AccountantAccountIncome.ToString.Trim()

                                'si el tipo de servicio es 2 lanzamos consulta de cargar el detalle de los quirurgicos
                                If .TypeProcedure = 2 Then
                                    'lista para cargar los detalles quirurgicos
                                    Dim _ListInvoiceDetailPersistQXFOX As New List(Of SP_invoiceDetailListQX__FOX_Result)
                                    'cargamos la lista de detalle quirurgicos
                                    _ListInvoiceDetailPersistQXFOX = _ObjectionsReceptionCRepository.ListInvoiceDetailListQXFOX(ContainerName, _ListInvoiceDetailPersistFOX(j).Ingress, _ListInvoiceDetailPersistFOX(j).ServiceOrder, _ListInvoiceDetailPersistFOX(j).ServiceCode, _ListInvoiceDetailPersistFOX(j).consecutiveOrder, _ListInvoiceDetailPersistFOX(j).ServiceNumber, _ListInvoiceDetailPersistFOX(j).ConsecutivoInventory)
                                    For k As Integer = 0 To _ListInvoiceDetailPersistQXFOX.Count - 1
                                        'objeto item detalle quirurgico
                                        Dim _InvoiceDetailQX As GlosaInvoiceDetailQX = New GlosaInvoiceDetailQX
                                        With _InvoiceDetailQX
                                            .ServiceCode = _ListInvoiceDetailPersistQXFOX(k).ServiceCode.ToString.Trim()
                                            .ServiceName = _ListInvoiceDetailPersistQXFOX(k).ServiceName.ToString.Trim()
                                            If _ListInvoiceDetailPersistQXFOX(k).ServiceOrderDetailSurgicalId IsNot Nothing Then
                                                .ServiceOrderDetailSurgicalId = _ListInvoiceDetailPersistQXFOX(k).ServiceOrderDetailSurgicalId
                                            End If
                                            If _ListInvoiceDetailPersistQXFOX(k).MedicalCode IsNot Nothing Then
                                                .MedicalCode = _ListInvoiceDetailPersistQXFOX(k).MedicalCode.ToString.Trim()
                                            End If
                                            If _ListInvoiceDetailPersistQXFOX(k).MedicalName IsNot Nothing Then
                                                .MedicalName = _ListInvoiceDetailPersistQXFOX(k).MedicalName.ToString.Trim()
                                            End If
                                            If _ListInvoiceDetailPersistQXFOX(k).ValueServiceManual IsNot Nothing Then
                                                .ValueServiceManual = _ListInvoiceDetailPersistQXFOX(k).ValueServiceManual.ToString.Trim()
                                            End If
                                            If _ListInvoiceDetailPersistQXFOX(k).UnitValue IsNot Nothing Then
                                                .UnitValue = _ListInvoiceDetailPersistQXFOX(k).UnitValue.ToString.Trim()
                                            End If
                                            If _ListInvoiceDetailPersistQXFOX(k).InvoicedValue IsNot Nothing Then
                                                .InvoicedValue = _ListInvoiceDetailPersistQXFOX(k).InvoicedValue.ToString.Trim()
                                            End If
                                            If _ListInvoiceDetailPersistQXFOX(k).Ammount IsNot Nothing Then
                                                .Ammount = _ListInvoiceDetailPersistQXFOX(k).Ammount.ToString.Trim()
                                            End If
                                            If _ListInvoiceDetailPersistQXFOX(k).CostCenterCode IsNot Nothing Then
                                                .CostCenterCode = _ListInvoiceDetailPersistQXFOX(k).CostCenterCode.ToString.Trim()
                                            End If
                                            If _ListInvoiceDetailPersistQXFOX(k).CostCenterName IsNot Nothing Then
                                                .CostCenterName = _ListInvoiceDetailPersistQXFOX(k).CostCenterName.ToString.Trim()
                                            End If
                                            If _ListInvoiceDetailPersistQXFOX(k).ServiceAreaCode IsNot Nothing Then
                                                .ServiceAreaCode = _ListInvoiceDetailPersistQXFOX(k).ServiceAreaCode.ToString.Trim()
                                            End If
                                            If _ListInvoiceDetailPersistQXFOX(k).DescriptionServiceArea IsNot Nothing Then
                                                .DescriptionServiceArea = _ListInvoiceDetailPersistQXFOX(k).DescriptionServiceArea.ToString.Trim()
                                            End If
                                            If _ListInvoiceDetailPersistQXFOX(k).AccountantAccountIncome IsNot Nothing Then
                                                .AccountantAccountIncome = _ListInvoiceDetailPersistQXFOX(k).AccountantAccountIncome.ToString.Trim()
                                            End If
                                        End With
                                        'agrego el nuevo objeto detalle quirurgico a la lista a guardar
                                        .GlosaInvoiceDetailQX.Add(_InvoiceDetailQX)
                                    Next
                                End If
                            End With
                            'agrego el objeto InvoiceDetail a las lista de detalle de la factura.
                            GlosaObjectionsReceptionD.GlosaInvoiceDetail.Add(_invoiceDetail)
                        Next
                    End If
                ElseIf session.IndigoGlossesIntegration = 4 Then
                    _ListInvoiceDetailPersistNET = _ObjectionsReceptionCRepository.ListInvoiceDetailNET(ContainerName, session.HisContainer, session.SecurityContainer, GlosaObjectionsReceptionD.GlosaPortfolioGlosada.InvoiceNumber, GlosaObjectionsReceptionD.GlosaPortfolioGlosada.IngressNumber)
                    If _ListInvoiceDetailPersistNET IsNot Nothing Then
                        For j As Integer = 0 To _ListInvoiceDetailPersistNET.Count - 1
                            'creo y asingo valores de un objeto detalle factura "GlosaInvoiceDetail"
                            Dim _invoiceDetail As New GlosaInvoiceDetail
                            With _invoiceDetail
                                .InvoiceNumber = _ListInvoiceDetailPersistNET(j).InvoiceNumber.ToString.Trim()
                                .ServiceDate = _ListInvoiceDetailPersistNET(j).ServiceDate.ToString.Trim()
                                .ServiceCode = _ListInvoiceDetailPersistNET(j).ServiceCode.ToString.Trim()
                                .ServiceName = _ListInvoiceDetailPersistNET(j).ServiceName.ToString.Trim()
                                If _ListInvoiceDetailPersistNET(j).InvoiceDetailId IsNot Nothing Then
                                    .InvoiceDetailNativeId = _ListInvoiceDetailPersistNET(j).InvoiceDetailId
                                End If
                                If _ListInvoiceDetailPersistNET(j).ServiceOrderDetailId IsNot Nothing Then
                                    .ServiceOrderDetailId = _ListInvoiceDetailPersistNET(j).ServiceOrderDetailId
                                End If
                                .ServiceAreaCode = _ListInvoiceDetailPersistNET(j).ServiceAreaCode.ToString.Trim()
                                .DescriptionServiceArea = _ListInvoiceDetailPersistNET(j).DescriptionServiceArea.ToString.Trim()
                                .MedicalCode = _ListInvoiceDetailPersistNET(j).MedicalCode.ToString.Trim()
                                .MedicalName = _ListInvoiceDetailPersistNET(j).MedicalName.ToString.Trim()
                                If _ListInvoiceDetailPersistNET(j).BillerCode IsNot Nothing Then
                                    .BillerCode = _ListInvoiceDetailPersistNET(j).BillerCode.ToString.Trim()
                                End If
                                If _ListInvoiceDetailPersistNET(j).BillerName IsNot Nothing Then
                                    .BillerName = _ListInvoiceDetailPersistNET(j).BillerName.ToString.Trim()
                                End If
                                .BillingGroupCode = _ListInvoiceDetailPersistNET(j).BillingGroupCode.ToString.Trim()
                                .BillingGroup = _ListInvoiceDetailPersistNET(j).BillingGroup.ToString.Trim()
                                .ValueServiceManual = _ListInvoiceDetailPersistNET(j).ValueServiceManual.ToString.Trim()
                                .UnitValue = _ListInvoiceDetailPersistNET(j).UnitValue.ToString.Trim()
                                .InvoicedValue = _ListInvoiceDetailPersistNET(j).InvoicedValue.ToString.Trim()
                                .ValorEntidad = .InvoicedValue
                                .Ammount = _ListInvoiceDetailPersistNET(j).Ammount.ToString.Trim()
                                .CostCenterCode = _ListInvoiceDetailPersistNET(j).CostCenterCode.ToString.Trim()
                                .CostCenterName = _ListInvoiceDetailPersistNET(j).CostCenterName.ToString.Trim()
                                .TypeServiceProduct = _ListInvoiceDetailPersistNET(j).TypeServiceProduct.ToString.Trim()
                                .TypeProcedure = _ListInvoiceDetailPersistNET(j).TypeProcedure.ToString.Trim()
                                .AccountantAccountIncome = _ListInvoiceDetailPersistNET(j).AccountantAccountIncome.ToString.Trim()

                                'si el tipo de servicio es 2 lanzamos consulta de cargar el detalle de los quirurgicos
                                If .TypeProcedure = 2 Then
                                    'lista para cargar los detalles quirurgicos
                                    Dim _ListInvoiceDetailPersistQXNET As New List(Of SP_invoiceDetailListQX__NET_Result)
                                    'cargamos la lista de detalle quirurgicos
                                    _ListInvoiceDetailPersistQXNET = _ObjectionsReceptionCRepository.ListInvoiceDetailListQXNET(ContainerName, _ListInvoiceDetailPersistNET(j).Ingress, _ListInvoiceDetailPersistNET(j).ServiceOrder, _ListInvoiceDetailPersistNET(j).ServiceCode, _ListInvoiceDetailPersistNET(j).consecutiveOrder, _ListInvoiceDetailPersistNET(j).ServiceNumber, _ListInvoiceDetailPersistNET(j).ConsecutivoInventory)
                                    For k As Integer = 0 To _ListInvoiceDetailPersistQXNET.Count - 1
                                        'objeto item detalle quirurgico
                                        Dim _InvoiceDetailQX As GlosaInvoiceDetailQX = New GlosaInvoiceDetailQX
                                        With _InvoiceDetailQX
                                            .ServiceCode = _ListInvoiceDetailPersistQXNET(k).ServiceCode.ToString.Trim()
                                            .ServiceName = _ListInvoiceDetailPersistQXNET(k).ServiceName.ToString.Trim()
                                            If _ListInvoiceDetailPersistQXNET(k).ServiceOrderDetailSurgicalId IsNot Nothing Then
                                                .ServiceOrderDetailSurgicalId = _ListInvoiceDetailPersistQXNET(k).ServiceOrderDetailSurgicalId
                                            End If
                                            If _ListInvoiceDetailPersistQXNET(k).MedicalCode IsNot Nothing Then
                                                .MedicalCode = _ListInvoiceDetailPersistQXNET(k).MedicalCode.ToString.Trim()
                                            End If
                                            If _ListInvoiceDetailPersistQXNET(k).MedicalName IsNot Nothing Then
                                                .MedicalName = _ListInvoiceDetailPersistQXNET(k).MedicalName.ToString.Trim()
                                            End If
                                            If _ListInvoiceDetailPersistQXNET(k).ValueServiceManual IsNot Nothing Then
                                                .ValueServiceManual = _ListInvoiceDetailPersistQXNET(k).ValueServiceManual.ToString.Trim()
                                            End If
                                            If _ListInvoiceDetailPersistQXNET(k).UnitValue IsNot Nothing Then
                                                .UnitValue = _ListInvoiceDetailPersistQXNET(k).UnitValue.ToString.Trim()
                                            End If
                                            If _ListInvoiceDetailPersistQXNET(k).InvoicedValue IsNot Nothing Then
                                                .InvoicedValue = _ListInvoiceDetailPersistQXNET(k).InvoicedValue.ToString.Trim()
                                            End If
                                            If _ListInvoiceDetailPersistQXNET(k).Ammount IsNot Nothing Then
                                                .Ammount = _ListInvoiceDetailPersistQXNET(k).Ammount.ToString.Trim()
                                            End If
                                            If _ListInvoiceDetailPersistQXNET(k).CostCenterCode IsNot Nothing Then
                                                .CostCenterCode = _ListInvoiceDetailPersistQXNET(k).CostCenterCode.ToString.Trim()
                                            End If
                                            If _ListInvoiceDetailPersistQXNET(k).CostCenterName IsNot Nothing Then
                                                .CostCenterName = _ListInvoiceDetailPersistQXNET(k).CostCenterName.ToString.Trim()
                                            End If
                                            If _ListInvoiceDetailPersistQXNET(k).ServiceAreaCode IsNot Nothing Then
                                                .ServiceAreaCode = _ListInvoiceDetailPersistQXNET(k).ServiceAreaCode.ToString.Trim()
                                            End If
                                            If _ListInvoiceDetailPersistQXNET(k).DescriptionServiceArea IsNot Nothing Then
                                                .DescriptionServiceArea = _ListInvoiceDetailPersistQXNET(k).DescriptionServiceArea.ToString.Trim()
                                            End If
                                            If _ListInvoiceDetailPersistQXNET(k).AccountantAccountIncome IsNot Nothing Then
                                                .AccountantAccountIncome = _ListInvoiceDetailPersistQXNET(k).AccountantAccountIncome.ToString.Trim()
                                            End If
                                        End With
                                        'agrego el nuevo objeto detalle quirurgico a la lista a guardar
                                        .GlosaInvoiceDetailQX.Add(_InvoiceDetailQX)
                                    Next
                                End If
                            End With
                            'agrego el objeto InvoiceDetail a las lista de detalle de la factura.
                            GlosaObjectionsReceptionD.GlosaInvoiceDetail.Add(_invoiceDetail)
                        Next
                    End If
                ElseIf session.IndigoGlossesIntegration = 5 Then
                    _ListInvoiceDetailPersistNaviteMigrations = _ObjectionsReceptionCRepository.ListInvoiceDetailNAVITEINTEGRATION(ContainerName, session.HisContainer, session.SecurityContainer, GlosaObjectionsReceptionD.GlosaPortfolioGlosada.InvoiceNumber, GlosaObjectionsReceptionD.GlosaPortfolioGlosada.IngressNumber)
                    If _ListInvoiceDetailPersistNaviteMigrations IsNot Nothing Then
                        For j As Integer = 0 To _ListInvoiceDetailPersistNaviteMigrations.Count - 1
                            'creo y asingo valores de un objeto detalle factura "GlosaInvoiceDetail"
                            Dim _invoiceDetail As New GlosaInvoiceDetail
                            With _invoiceDetail
                                .InvoiceNumber = _ListInvoiceDetailPersistNaviteMigrations(j).InvoiceNumber.ToString.Trim()
                                .ServiceDate = _ListInvoiceDetailPersistNaviteMigrations(j).ServiceDate.ToString.Trim()
                                .ServiceCode = _ListInvoiceDetailPersistNaviteMigrations(j).ServiceCode.ToString.Trim()
                                If _ListInvoiceDetailPersistNaviteMigrations(j).ServiceName.ToString.Trim().Length > 250 Then
                                    .ServiceName = _ListInvoiceDetailPersistNaviteMigrations(j).ServiceName.ToString.Trim().Substring(0, 250)
                                Else
                                    .ServiceName = _ListInvoiceDetailPersistNaviteMigrations(j).ServiceName.ToString.Trim()
                                End If

                                If _ListInvoiceDetailPersistNaviteMigrations(j).InvoiceDetailId IsNot Nothing Then
                                    .InvoiceDetailNativeId = _ListInvoiceDetailPersistNaviteMigrations(j).InvoiceDetailId
                                End If
                                If _ListInvoiceDetailPersistNaviteMigrations(j).ServiceOrderDetailId IsNot Nothing Then
                                    .ServiceOrderDetailId = _ListInvoiceDetailPersistNaviteMigrations(j).ServiceOrderDetailId
                                End If
                                .ServiceAreaCode = _ListInvoiceDetailPersistNaviteMigrations(j).ServiceAreaCode.ToString.Trim()
                                .DescriptionServiceArea = _ListInvoiceDetailPersistNaviteMigrations(j).DescriptionServiceArea.ToString.Trim()
                                .MedicalCode = _ListInvoiceDetailPersistNaviteMigrations(j).MedicalCode.ToString.Trim()
                                .MedicalName = _ListInvoiceDetailPersistNaviteMigrations(j).MedicalName.ToString.Trim()
                                If _ListInvoiceDetailPersistNaviteMigrations(j).BillerCode IsNot Nothing Then
                                    .BillerCode = _ListInvoiceDetailPersistNaviteMigrations(j).BillerCode.ToString.Trim()
                                End If
                                If _ListInvoiceDetailPersistNaviteMigrations(j).BillerName IsNot Nothing Then
                                    .BillerName = _ListInvoiceDetailPersistNaviteMigrations(j).BillerName.ToString.Trim()
                                End If
                                .BillingGroupCode = _ListInvoiceDetailPersistNaviteMigrations(j).BillingGroupCode.ToString.Trim()
                                .BillingGroup = _ListInvoiceDetailPersistNaviteMigrations(j).BillingGroup.ToString.Trim()
                                .ValueServiceManual = _ListInvoiceDetailPersistNaviteMigrations(j).ValueServiceManual.ToString.Trim()
                                .UnitValue = _ListInvoiceDetailPersistNaviteMigrations(j).UnitValue.ToString.Trim()
                                .InvoicedValue = _ListInvoiceDetailPersistNaviteMigrations(j).InvoicedValue.ToString.Trim()
                                .ValorEntidad = _ListInvoiceDetailPersistNaviteMigrations(j).EntityValue.ToString.Trim()
                                .ValorPaciente = _ListInvoiceDetailPersistNaviteMigrations(j).PatientValue.ToString.Trim()
                                .Ammount = _ListInvoiceDetailPersistNaviteMigrations(j).Ammount.ToString.Trim()
                                .CostCenterCode = _ListInvoiceDetailPersistNaviteMigrations(j).CostCenterCode.ToString.Trim()
                                .CostCenterName = _ListInvoiceDetailPersistNaviteMigrations(j).CostCenterName.ToString.Trim()
                                .TypeServiceProduct = _ListInvoiceDetailPersistNaviteMigrations(j).TypeServiceProduct.ToString.Trim()
                                .TypeProcedure = _ListInvoiceDetailPersistNaviteMigrations(j).TypeProcedure.ToString.Trim()
                                .AccountantAccountIncome = _ListInvoiceDetailPersistNaviteMigrations(j).AccountantAccountIncome.ToString.Trim()

                                'si el tipo de servicio es 2 lanzamos consulta de cargar el detalle de los quirurgicos
                                If .TypeProcedure = 2 Then
                                    'lista para cargar los detalles quirurgicos
                                    Dim _ListInvoiceDetailPersistNativeMigrationQX As New List(Of SP_invoiceDetailListQX_NATIVEINTEGRATION_Result)
                                    'cargamos la lista de detalle quirurgicos
                                    _ListInvoiceDetailPersistNativeMigrationQX = _ObjectionsReceptionCRepository.ListInvoiceDetailListQXNAVITEINTEGRATION(ContainerName, _ListInvoiceDetailPersistNaviteMigrations(j).Ingress, _ListInvoiceDetailPersistNaviteMigrations(j).ServiceOrder, _ListInvoiceDetailPersistNaviteMigrations(j).ServiceCode, _ListInvoiceDetailPersistNaviteMigrations(j).consecutiveOrder, _ListInvoiceDetailPersistNaviteMigrations(j).ServiceNumber, _ListInvoiceDetailPersistNaviteMigrations(j).ConsecutivoInventory)
                                    For k As Integer = 0 To _ListInvoiceDetailPersistNativeMigrationQX.Count - 1
                                        'objeto item detalle quirurgico
                                        Dim _InvoiceDetailQX As GlosaInvoiceDetailQX = New GlosaInvoiceDetailQX
                                        With _InvoiceDetailQX
                                            .ServiceCode = _ListInvoiceDetailPersistNativeMigrationQX(k).ServiceCode.ToString.Trim()
                                            .ServiceName = _ListInvoiceDetailPersistNativeMigrationQX(k).ServiceName.ToString.Trim()
                                            If _ListInvoiceDetailPersistNativeMigrationQX(k).ServiceOrderDetailSurgicalId IsNot Nothing Then
                                                .ServiceOrderDetailSurgicalId = _ListInvoiceDetailPersistNativeMigrationQX(k).ServiceOrderDetailSurgicalId
                                            End If
                                            If _ListInvoiceDetailPersistNativeMigrationQX(k).MedicalCode IsNot Nothing Then
                                                .MedicalCode = _ListInvoiceDetailPersistNativeMigrationQX(k).MedicalCode.ToString.Trim()
                                            End If
                                            If _ListInvoiceDetailPersistNativeMigrationQX(k).MedicalName IsNot Nothing Then
                                                .MedicalName = _ListInvoiceDetailPersistNativeMigrationQX(k).MedicalName.ToString.Trim()
                                            End If
                                            If _ListInvoiceDetailPersistNativeMigrationQX(k).ValueServiceManual IsNot Nothing Then
                                                .ValueServiceManual = _ListInvoiceDetailPersistNativeMigrationQX(k).ValueServiceManual.ToString.Trim()
                                            End If
                                            If _ListInvoiceDetailPersistNativeMigrationQX(k).UnitValue IsNot Nothing Then
                                                .UnitValue = _ListInvoiceDetailPersistNativeMigrationQX(k).UnitValue.ToString.Trim()
                                            End If
                                            If _ListInvoiceDetailPersistNativeMigrationQX(k).InvoicedValue IsNot Nothing Then
                                                .InvoicedValue = _ListInvoiceDetailPersistNativeMigrationQX(k).InvoicedValue.ToString.Trim()
                                            End If
                                            If _ListInvoiceDetailPersistNativeMigrationQX(k).Ammount IsNot Nothing Then
                                                .Ammount = _ListInvoiceDetailPersistNativeMigrationQX(k).Ammount.ToString.Trim()
                                            End If
                                            If _ListInvoiceDetailPersistNativeMigrationQX(k).CostCenterCode IsNot Nothing Then
                                                .CostCenterCode = _ListInvoiceDetailPersistNativeMigrationQX(k).CostCenterCode.ToString.Trim()
                                            End If
                                            If _ListInvoiceDetailPersistNativeMigrationQX(k).CostCenterName IsNot Nothing Then
                                                .CostCenterName = _ListInvoiceDetailPersistNativeMigrationQX(k).CostCenterName.ToString.Trim()
                                            End If
                                            If _ListInvoiceDetailPersistNativeMigrationQX(k).ServiceAreaCode IsNot Nothing Then
                                                .ServiceAreaCode = _ListInvoiceDetailPersistNativeMigrationQX(k).ServiceAreaCode.ToString.Trim()
                                            End If
                                            If _ListInvoiceDetailPersistNativeMigrationQX(k).DescriptionServiceArea IsNot Nothing Then
                                                .DescriptionServiceArea = _ListInvoiceDetailPersistNativeMigrationQX(k).DescriptionServiceArea.ToString.Trim()
                                            End If
                                            If _ListInvoiceDetailPersistNativeMigrationQX(k).AccountantAccountIncome IsNot Nothing Then
                                                .AccountantAccountIncome = _ListInvoiceDetailPersistNativeMigrationQX(k).AccountantAccountIncome.ToString.Trim()
                                            End If
                                        End With
                                        'agrego el nuevo objeto detalle quirurgico a la lista a guardar
                                        .GlosaInvoiceDetailQX.Add(_InvoiceDetailQX)
                                    Next
                                End If
                            End With
                            'agrego el objeto InvoiceDetail a las lista de detalle de la factura.
                            GlosaObjectionsReceptionD.GlosaInvoiceDetail.Add(_invoiceDetail)
                        Next
                    End If
                End If
            Else
                'Facturas normales
                _ListInvoiceDetailPersist = Me.ListInvoiceDetails(ContainerName, GlosaObjectionsReceptionD.GlosaPortfolioGlosada.InvoiceNumber, GlosaObjectionsReceptionD.GlosaPortfolioGlosada.IngressNumber, session)
                If _ListInvoiceDetailPersist IsNot Nothing Then
                    For j As Integer = 0 To _ListInvoiceDetailPersist.Count - 1
						'creo y asingo valores de un objeto detalle factura "GlosaInvoiceDetail"
						Dim invoiceDetailId = _ListInvoiceDetailPersist(j).InvoiceDetailId
						Dim serviceOrderDetailId = _ListInvoiceDetailPersist(j).ServiceOrderDetailId

						' Verificamos si ya existe ese detalle con esa combinación en la lista actual para evitar datos duplicados
						Dim alreadyExists = GlosaObjectionsReceptionD.GlosaInvoiceDetail.Any(Function(x) x.InvoiceDetailNativeId = invoiceDetailId AndAlso x.ServiceOrderDetailId = serviceOrderDetailId)
						If alreadyExists Then
							Continue For
						End If

						Dim _invoiceDetail As New GlosaInvoiceDetail
                        With _invoiceDetail
                            .InvoiceNumber = _ListInvoiceDetailPersist(j).InvoiceNumber.ToString.Trim()
                            .ServiceDate = _ListInvoiceDetailPersist(j).ServiceDate.ToString.Trim()
                            .ServiceCode = _ListInvoiceDetailPersist(j).ServiceCode.ToString.Trim()
                            If _ListInvoiceDetailPersist(j).ServiceName.ToString.Trim().Length > 250 Then
                                .ServiceName = _ListInvoiceDetailPersist(j).ServiceName.ToString.Trim().Substring(0, 250)
                            Else
                                .ServiceName = _ListInvoiceDetailPersist(j).ServiceName.ToString.Trim()
                            End If

                            If _ListInvoiceDetailPersist(j).InvoiceDetailId IsNot Nothing Then
                                .InvoiceDetailNativeId = _ListInvoiceDetailPersist(j).InvoiceDetailId
                            End If
                            If _ListInvoiceDetailPersist(j).ServiceOrderDetailId IsNot Nothing Then
                                .ServiceOrderDetailId = _ListInvoiceDetailPersist(j).ServiceOrderDetailId
                            End If
                            .ServiceAreaCode = _ListInvoiceDetailPersist(j).ServiceAreaCode.ToString.Trim()
                            .DescriptionServiceArea = _ListInvoiceDetailPersist(j).DescriptionServiceArea.ToString.Trim()
                            .MedicalCode = _ListInvoiceDetailPersist(j).MedicalCode.ToString.Trim()
                            .MedicalName = _ListInvoiceDetailPersist(j).MedicalName.ToString.Trim()
                            If _ListInvoiceDetailPersist(j).BillerCode IsNot Nothing Then
                                .BillerCode = _ListInvoiceDetailPersist(j).BillerCode.ToString.Trim()
                            End If
                            If _ListInvoiceDetailPersist(j).BillerName IsNot Nothing Then
                                .BillerName = _ListInvoiceDetailPersist(j).BillerName.ToString.Trim()
                            End If
                            .BillingGroupCode = _ListInvoiceDetailPersist(j).BillingGroupCode.ToString.Trim()
                            .BillingGroup = _ListInvoiceDetailPersist(j).BillingGroup.ToString.Trim()
                            .ValueServiceManual = _ListInvoiceDetailPersist(j).ValueServiceManual.ToString.Trim()
                            .UnitValue = _ListInvoiceDetailPersist(j).UnitValue.ToString.Trim()
                            .InvoicedValue = _ListInvoiceDetailPersist(j).InvoicedValue.ToString.Trim()
                            .ValorEntidad = _ListInvoiceDetailPersist(j).EntityValue.ToString.Trim()
                            .ValorPaciente = _ListInvoiceDetailPersist(j).PatientValue.ToString.Trim()
                            .Ammount = _ListInvoiceDetailPersist(j).Ammount.ToString.Trim()
                            .CostCenterCode = _ListInvoiceDetailPersist(j).CostCenterCode.ToString.Trim()
                            .CostCenterName = _ListInvoiceDetailPersist(j).CostCenterName.ToString.Trim()
                            .TypeServiceProduct = _ListInvoiceDetailPersist(j).TypeServiceProduct.ToString.Trim()
                            .TypeProcedure = _ListInvoiceDetailPersist(j).TypeProcedure.ToString.Trim()
                            .AccountantAccountIncome = _ListInvoiceDetailPersist(j).AccountantAccountIncome.ToString.Trim()

                            'si el tipo de servicio es 2 lanzamos consulta de cargar el detalle de los quirurgicos
                            If .TypeProcedure = 2 Then
                                'lista para cargar los detalles quirurgicos
                                Dim _ListInvoiceDetailPersistQX As New List(Of SP_invoiceDetailListQX_Result)
                                'cargamos la lista de detalle quirurgicos
                                _ListInvoiceDetailPersistQX = Me.ListInvoiceDetailListQX(ContainerName, _ListInvoiceDetailPersist(j).Ingress, _ListInvoiceDetailPersist(j).ServiceOrder, _ListInvoiceDetailPersist(j).ServiceCode, _ListInvoiceDetailPersist(j).consecutiveOrder, _ListInvoiceDetailPersist(j).ServiceNumber, _ListInvoiceDetailPersist(j).ConsecutivoInventory, session)
                                For k As Integer = 0 To _ListInvoiceDetailPersistQX.Count - 1
                                    'objeto item detalle quirurgico
                                    Dim _InvoiceDetailQX As GlosaInvoiceDetailQX = New GlosaInvoiceDetailQX
                                    With _InvoiceDetailQX
                                        .ServiceCode = _ListInvoiceDetailPersistQX(k).ServiceCode.ToString.Trim()
                                        .ServiceName = _ListInvoiceDetailPersistQX(k).ServiceName.ToString.Trim()
                                        If _ListInvoiceDetailPersistQX(k).ServiceOrderDetailSurgicalId IsNot Nothing Then
                                            .ServiceOrderDetailSurgicalId = _ListInvoiceDetailPersistQX(k).ServiceOrderDetailSurgicalId
                                        End If
                                        If _ListInvoiceDetailPersistQX(k).MedicalCode IsNot Nothing Then
                                            .MedicalCode = _ListInvoiceDetailPersistQX(k).MedicalCode.ToString.Trim()
                                        End If
                                        If _ListInvoiceDetailPersistQX(k).MedicalName IsNot Nothing Then
                                            .MedicalName = _ListInvoiceDetailPersistQX(k).MedicalName.ToString.Trim()
                                        End If
                                        If _ListInvoiceDetailPersistQX(k).ValueServiceManual IsNot Nothing Then
                                            .ValueServiceManual = _ListInvoiceDetailPersistQX(k).ValueServiceManual.ToString.Trim()
                                        End If
                                        If _ListInvoiceDetailPersistQX(k).UnitValue IsNot Nothing Then
                                            .UnitValue = _ListInvoiceDetailPersistQX(k).UnitValue.ToString.Trim()
                                        End If
                                        If _ListInvoiceDetailPersistQX(k).InvoicedValue IsNot Nothing Then
                                            .InvoicedValue = _ListInvoiceDetailPersistQX(k).InvoicedValue.ToString.Trim()
                                        End If
                                        If _ListInvoiceDetailPersistQX(k).Ammount IsNot Nothing Then
                                            .Ammount = _ListInvoiceDetailPersistQX(k).Ammount.ToString.Trim()
                                        End If
                                        If _ListInvoiceDetailPersistQX(k).CostCenterCode IsNot Nothing Then
                                            .CostCenterCode = _ListInvoiceDetailPersistQX(k).CostCenterCode.ToString.Trim()
                                        End If
                                        If _ListInvoiceDetailPersistQX(k).CostCenterName IsNot Nothing Then
                                            .CostCenterName = _ListInvoiceDetailPersistQX(k).CostCenterName.ToString.Trim()
                                        End If
                                        If _ListInvoiceDetailPersistQX(k).ServiceAreaCode IsNot Nothing Then
                                            .ServiceAreaCode = _ListInvoiceDetailPersistQX(k).ServiceAreaCode.ToString.Trim()
                                        End If
                                        If _ListInvoiceDetailPersistQX(k).DescriptionServiceArea IsNot Nothing Then
                                            .DescriptionServiceArea = _ListInvoiceDetailPersistQX(k).DescriptionServiceArea.ToString.Trim()
                                        End If
                                        If _ListInvoiceDetailPersistQX(k).AccountantAccountIncome IsNot Nothing Then
                                            .AccountantAccountIncome = _ListInvoiceDetailPersistQX(k).AccountantAccountIncome.ToString.Trim()
                                        End If
                                    End With
                                    'agrego el nuevo objeto detalle quirurgico a la lista a guardar
                                    .GlosaInvoiceDetailQX.Add(_InvoiceDetailQX)
                                Next
                            End If
                        End With
                        'agrego el objeto InvoiceDetail a las lista de detalle de la factura.
                        GlosaObjectionsReceptionD.GlosaInvoiceDetail.Add(_invoiceDetail)
                    Next
                End If
            End If

        ElseIf GlosaObjectionsReceptionD.ChangeTracker.State = ObjectState.Added And GlosaObjectionsReceptionD.DocumentType = 2 And GlosaObjectionsReceptionD.State <> 4 Then
            _GlosaPortfolioGlosada = _PortfolioGlosadaRepository.GetPortfolioGlosada(GlosaObjectionsReceptionD.InvoiceNumber)
            _GlosaPortfolioGlosada.State = 4  '4 -pendiente confirmar reiteracion
            GlosaObjectionsReceptionD.PortfolioGlosaId = _GlosaPortfolioGlosada.Id
            _PortfolioGlosadaRepository.UpdateEntity(_GlosaPortfolioGlosada)
            UnitWorkPortfolioGlosada.Commit()
            If session.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                Dim UpdateState As Boolean
                Dim OblParameters As GlosasParametersInterface = _IInterfaceParametersRepository.GetInterfacesParametersById(GlosaObjectionsReceptionD.GlosasParametersInterfaceId)
                'para el metodo fox publico debemos actualizar estado de cartera para la reiteracion
                If OblParameters.AccountingMethod = eTypeInterface.FoxPublic Then
                    UpdateState = _InterfacePublicFOX.UpdtaeStateReiteration(GlosaObjectionsReceptionD.InvoiceNumber, OblParameters.ContainerName, "2") 'estado de careta 2
                    If UpdateState = False Then
                        Mensaje.Add("Error Actualizando Estado de Cartera ERP")
                        Return New ActionResult With {.StateResult = False, .MessageResult = Mensaje}
                        Return _actionResult
                    End If
                End If
            End If
        ElseIf GlosaObjectionsReceptionD.ChangeTracker.State = ObjectState.Added And GlosaObjectionsReceptionD.Invalidate = True Then 'si es una factura anulada
            _GlosaPortfolioGlosada = _PortfolioGlosadaRepository.GetPortfolioGlosada(GlosaObjectionsReceptionD.InvoiceNumber)
            GlosaObjectionsReceptionD.PortfolioGlosaId = _GlosaPortfolioGlosada.Id
            GlosaObjectionsReceptionD.State = 1 'como glosaa
        End If

		Try
			'configuro la transaccion
			Dim txSettings As New TransactionOptions()
			txSettings.Timeout = New System.TimeSpan(0, 10, 0) 'TransactionManager.MaximumTimeout
			txSettings.IsolationLevel = IsolationLevel.ReadCommitted
			If _actionResult.MessageResult.Count <= 0 Then
				_actionResult.StateResult = True
				'inicio la transaccion
				Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
					'grabo la cabecera de la objecion, el detalle "lista facturas" y el detalle de cada factura 
					If GlosaObjectionsReceptionD.ChangeTracker.State = ObjectState.Added Then
						_ObjectionsReceptionDRepository.AddEntity(GlosaObjectionsReceptionD)
						'confirmo la unidad de trabajo  la cabecera de la objecion
						unitWorkObjectionsReceptionD.Commit()
						'para las anulaciones, actualizamos detalles de factura al nuevo Id OBJD
						If GlosaObjectionsReceptionD.ChangeTracker.State = ObjectState.Added And GlosaObjectionsReceptionD.Invalidate = True Then 'si es una factura anulada
							Dim ListInvoiceDetail As List(Of GlosaInvoiceDetail) = _InvoiceDeatilRepository.ListGlosaInvoiceDetailwithoutAggregates(GlosaObjectionsReceptionD.InvoiceNumber)
							For Each item As GlosaInvoiceDetail In ListInvoiceDetail
								item.ObjectionsReceptionDId = GlosaObjectionsReceptionD.Id
								_InvoiceDeatilRepository.SaveEntity(item)
							Next
							unitWorkInvoiceDetails.Commit()
						End If
					ElseIf GlosaObjectionsReceptionD.ChangeTracker.State = ObjectState.Modified Then
						_ObjectionsReceptionDRepository.UpdateEntity(GlosaObjectionsReceptionD)
						'confirmo la unidad de trabajo  la cabecera de la objecion
						unitWorkObjectionsReceptionD.Commit()
					End If
					'confirmo la transaccion
					scope.Complete()
					'variable lista de string para retornar el numero de factura y el id de la nueva recepción
					Dim Consecutivo As List(Of String) = New List(Of String)
				End Using
				Dim audit = session.AuditMessageWcf
				If GlosaObjectionsReceptionD.ChangeTracker.State = ObjectState.Added Then
					IndigoAuditBasic.Execute("GlosaObjectionsReceptionD", audit.Functional, GlosaObjectionsReceptionD.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
				ElseIf GlosaObjectionsReceptionD.ChangeTracker.State = ObjectState.Modified Then
					IndigoAuditBasic.Execute("GlosaObjectionsReceptionD", audit.Functional, GlosaObjectionsReceptionD.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
				End If
				'retorno objeto de respuesta
				Return _actionResult
			Else
				'En caso de error
				_actionResult.StateResult = False
			End If
		Catch ex As Exception
			'descarto los cambios en la eliminacion del detalle
			unitWorkObjectionsReceptionD.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Mensaje.Add(ex.Message)
            Return New ActionResult With {.StateResult = False, .MessageResult = Mensaje}
			Return _actionResult
		End Try
	End Function
    ''' <summary>
    ''' Lista las facturas por contenedor y nit de la entidad ademas del total de registro
    ''' </summary>
    ''' <param name="nameContainer">recibe el nombre del contenedor</param>
    ''' <param name="nit">nit del tercro a buscar facturas</param>
    ''' <param name="InvoiceNumber">Numero de factura</param>
    ''' <param name="IndigoCompany">numero de contenedor</param>
    '''  <param name="stringSQl">cadena Sql de Filtro avanzados</param>
    ''' <param name="TopQuery">Cantidad de Registro a retornar</param>
    ''' <returns>Un objeto result que tiene una lista de facturas y el conteo de las misma</returns>
    Public Function ListAllInvoice(nameContainer As String, nit As String, InvoiceNumber As String, IndigoCompany As String, stringSQl As String, ByVal TopQuery As String, ByVal FlagNotConfirmInvoice As String, session As SessionValues) As ActionResult(Of List(Of SP_invoiceList_Result)) Implements IObjectionsReceptionCAdminService.ListAllInvoice
        If session.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
            If String.IsNullOrEmpty(nameContainer) = True Then
                Throw New ArgumentNullException("Nombre del contenedor Vacio")
            End If
        End If
        If String.IsNullOrEmpty(nit) = True Then
            Throw New ArgumentNullException("Nit Vacio")
        End If
        If String.IsNullOrEmpty(IndigoCompany) = True Then
            Throw New ArgumentNullException("Codigo contenedor Indigo Vacio ")
        End If
        Dim _actionResult As New ActionResult(Of List(Of SP_invoiceList_Result))
        Try
            _actionResult.ObjectEmbbeded = _ObjectionsReceptionCRepository.ListAllInvoce(nameContainer, nit, InvoiceNumber, IndigoCompany, session.HisContainer, stringSQl, TopQuery, FlagNotConfirmInvoice)
            _actionResult.MessageResult = New List(Of String)
            _actionResult.StateResult = True
            Return _actionResult
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            _actionResult.StateResult = False
            Return _actionResult
        End Try
    End Function
    ''' <summary>
    ''' carga una factura 
    ''' </summary>
    ''' <param name="nameContainer">recibe el nombre del contenedor</param>
    ''' <param name="nit">nit del tercro a buscar facturas</param>
    ''' <param name="InvoiceNumber">numero de factura</param>
    ''' <returns>una factura</returns>
    Public Function GetInvoice(nameContainer As String, nit As String, InvoiceNumber As String, IndigoCompany As String, ByVal stringSQl As String, ByVal FlagNotConfirmInvoice As String, session As SessionValues) As SP_invoiceList_Result Implements IObjectionsReceptionCAdminService.GetInvoce
        If session.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
            If String.IsNullOrEmpty(nameContainer) = True Then
                Throw New ArgumentNullException("Nombre del contenedor Vacio")
            End If
        End If
        If String.IsNullOrEmpty(nit) = True Then
            Throw New ArgumentNullException("Nit Vacio")
        End If
        If String.IsNullOrEmpty(InvoiceNumber) = True Then
            Throw New ArgumentNullException("Numero de Factura Vacio")
        End If
        If String.IsNullOrEmpty(IndigoCompany) = True Then
            Throw New ArgumentNullException("Codigo contenedor Indigo Vacio ")
        End If
        Try
            Return _ObjectionsReceptionCRepository.GetInvoice(nameContainer, nit, InvoiceNumber, IndigoCompany, session.HisContainer, stringSQl, FlagNotConfirmInvoice)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' lista del detalle de una factura mediante un SP
    ''' </summary>
    ''' <param name="container">nombre del contenedor o BD a cargar Detalles de facturas</param>
    ''' <param name="invoiceNumber">numero de factura</param>
    ''' <param name="ingressNumber">numero consecutivo</param>
    ''' <returns>una lista del detalle de una factura</returns>
    Public Function ListInvoiceDetails(ByVal container As String, invoiceNumber As String, ingressNumber As String, session As SessionValues) As List(Of SP_invoiceDetailList_Result) Implements IObjectionsReceptionCAdminService.ListInvoiceDetails
        Try
            If session.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                If String.IsNullOrEmpty(container) = True Then
                    Throw New Exception("Nombre del contenedor Vacio")
                End If
            End If
            If String.IsNullOrEmpty(invoiceNumber) Then
                Throw New Exception("Numero de Factura vacio")
            End If
            If String.IsNullOrEmpty(ingressNumber) Then
                Throw New Exception("Numero de ingreso o consecutivo vacio")
            End If
            Return _ObjectionsReceptionCRepository.ListInvoiceDetail(container, session.HisContainer, session.SecurityContainer, invoiceNumber, ingressNumber)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' obtiene una lista de detalle de facturas de servico quirurgicos
    ''' </summary>
    ''' <param name="container">nombre del contenedor de datos</param>
    ''' <param name="consecutiveNumber">numero consecutivo</param>
    ''' <param name="ServiceOrder">orden de servicio</param>
    ''' <returns>lista de detalle quirurgicos</returns>
    Public Function ListInvoiceDetailListQX(container As String, consecutiveNumber As String, ServiceOrder As String, ByVal ServiceCode As String, ByVal consecutiveOrder As String, ByVal ServiceNumber As String, ByVal ConsecutivoInventory As String, session As SessionValues) As List(Of SP_invoiceDetailListQX_Result) Implements IObjectionsReceptionCAdminService.ListInvoiceDetailListQX
        If session.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
            If String.IsNullOrEmpty(container) = True Then
                Throw New ArgumentNullException("Nombre del contenedor Vacio")
            End If
        End If
        If String.IsNullOrEmpty(consecutiveNumber) = True Then
            Throw New ArgumentNullException("Consecutivo vacio")
        End If
        If String.IsNullOrEmpty(ServiceCode) = True Then
            Throw New ArgumentNullException("Codigo de servicio vacio")
        End If
        Try
            Return _ObjectionsReceptionCRepository.ListInvoiceDetailListQX(container, consecutiveNumber, ServiceOrder, ServiceCode, consecutiveOrder, ServiceNumber, ConsecutivoInventory)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return Nothing
        End Try
    End Function
    ' ''' <summary>
    ' ''' funcion que retorna la observacion y/o estado actual de como viene la factura a persistir
    ' ''' </summary>
    ' ''' <param name="code">codigo estado</param>
    ' ''' <returns>uan Observacion de fatura</returns>
    'Public Function GetObservationInvoice(code As String) As ObservationInvoice Implements IObjectionsReceptionCAdminService.GetObservationInvoice
    '    If String.IsNullOrEmpty(code) = True Then
    '        Throw New ArgumentNullException("El codigo de la observacion factura vacio")
    '    End If
    '    Try
    '        Return _ObjectionsReceptionCRepository.GetObservationInvoice(code)
    '    Catch ex As Exception
    '        IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
    '        Return Nothing
    '    End Try
    'End Function
    ''' <summary>
    ''' Función para validar y agregar facturas desde el sp
    ''' </summary>
    ''' <param name="ListInvoices">Lista de facturas</param>
    ''' <param name="Nit">Numero de Nit</param>
    ''' <param name="container">nombre Contenedor</param>
    ''' <returns>Lista de Factura </returns>
    Public Function ValidateListInvoiceSp(ListInvoices As List(Of String), Nit As String, container As String, ByVal Session As SessionValues) As ActionResult(Of List(Of GlosaObjectionsReceptionD)) Implements IObjectionsReceptionCAdminService.ValidateListInvoiceSp
        If ListInvoices.Count = 0 Then
            Throw New ArgumentNullException("Lista de facturas vacía")
        End If
        If Nit Is String.Empty Then
            Throw New ArgumentNullException("Nit vacío")
        End If
        If Session.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
            If container Is String.Empty Then
                Throw New ArgumentNullException("Nombre Contenedor vacío")
            End If
        End If
        Dim _Result As New ActionResult(Of List(Of GlosaObjectionsReceptionD))
        Dim _listError As New List(Of String)
        Dim ListGlosaDevolutionsReceptionD As New List(Of GlosaObjectionsReceptionD)
        Try
            If Session.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                _Result = ExcelIntegration(ListInvoices, Nit, container, Session.TransactionalContainer, Session.HisContainer)
            Else 'If Session.IndigoGlossesIntegration = EGlossesIntegration.Native Then
                _Result = ExcelNative(ListInvoices, Nit, Session.TransactionalContainer, Session.HisContainer)
            End If
            Return _Result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    '''  Funcion de copiar y pegar facturas en modo nativo
    ''' </summary>
    ''' <param name="ListInvoices"></param>
    ''' <param name="Nit"></param>
    ''' <param name="IndigoCompany"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ExcelNative(ByVal ListInvoices As List(Of String), Nit As String, IndigoCompany As String, HisContainer As String) As ActionResult(Of List(Of GlosaObjectionsReceptionD))
        Dim _Result As New ActionResult(Of List(Of GlosaObjectionsReceptionD))
        Dim _listError As New List(Of String)
        Dim ListGlosaObjectionsReceptionD As New List(Of GlosaObjectionsReceptionD)
        For Each item As String In ListInvoices
            Dim StrMensaje As String = String.Empty
            Dim itemInvoice As New SP_invoiceList_Result
            itemInvoice = _ObjectionsReceptionCRepository.GetInvoice(String.Empty, Nit, item, IndigoCompany, HisContainer, String.Empty, 0) '0=recepcion, - 1 radicacion cunetas de cobro -  2 = devolucion
            Dim nitReal As String
            Dim OBjCliente As Domain.Entities.Customer = _CustomerRepository.GetCustomerById(Nit)
            nitReal = OBjCliente.Nit
            'si no existe la cuenta en configuracion 
            If itemInvoice IsNot Nothing AndAlso itemInvoice.InvoiceNumber IsNot Nothing AndAlso Not itemInvoice.InvoiceNumber.Trim().Equals(String.Empty) Then
                If {3, 15, 16}.Contains(itemInvoice.StateCurrentInvoice) Then
                    'declaro variable  tipo detalle factura
                    Dim _TmpObjectionsReceptionD As New GlosaObjectionsReceptionD
                    'para tener encuenta las facturas para reiteracion
                    If itemInvoice.RadicatedConsecutive Is Nothing Or itemInvoice.Reiterated = 1 Then
                        If itemInvoice.Reiterated = 1 Then
                            'para validar que factura que este en una coniliacion o pago parcial no ingrese como reiteracion
                            If itemInvoice.StatePortfolioGlosada <> 11 AndAlso itemInvoice.StatePortfolioGlosada <> 12 Then
                                StrMensaje = itemInvoice.InvoiceNumber.ToString + " Factura Radicada  en el oficio " + itemInvoice.RadicatedConsecutive.ToString + " Se Encuentra en otro proceso"
                                _listError.Add(StrMensaje)
                            Else
                                With _TmpObjectionsReceptionD
                                    .InvoiceNumber = itemInvoice.InvoiceNumber.Trim
                                    .ObservationInvoiceCode = itemInvoice.StateCurrentInvoice.Trim
                                    .GlosaPortfolioGlosada = New GlosaPortfolioGlosada()
                                    With .GlosaPortfolioGlosada
                                        .InvoiceNumber = itemInvoice.InvoiceNumber.Trim
                                        .BalanceInvoice = itemInvoice.BalanceInvoice.ToString.Trim
                                        .InvoiceValueEntity = itemInvoice.InvoiceValueEntity.ToString.Trim
                                        .InvoiceValuePacient = itemInvoice.InvoiceValuePacient.ToString.Trim
                                        .AccountantAccountCustomers = itemInvoice.AccountantAccountCustomers
                                        .State = 1 'se envia como pendiente confirmado
                                        .PortfolioAge = itemInvoice.PortfolioAge
                                        .InvoiceDate = itemInvoice.InvoiceDate
                                        If itemInvoice.RadicatedNumber IsNot Nothing Then
                                            .RadicatedNumber = itemInvoice.RadicatedNumber.Trim
                                        End If
                                        .RadicatedDate = itemInvoice.RadicatedDate
                                        .PatientCode = itemInvoice.PatientCode
                                        .PatientName = itemInvoice.PatientName
                                        .IngressNumber = itemInvoice.IngressNumber
                                        .IngressDate = itemInvoice.IngressDate
                                        .UserNameInvoice = itemInvoice.UserNameInvoice
                                        .ContractCode = itemInvoice.ContractCode
                                        .ContractName = itemInvoice.ContractName
                                        .Nit = nitReal
                                        .PlanCode = itemInvoice.CodePlan
                                        .ValueGlosado = 0
                                        .ValueAcceptedFirstInstance = 0
                                        .ValueReiterated = 0
                                        .ValueReiterationBalance = 0
                                        .ValueAcceptedSecondInstance = 0
                                        .ValueAcceptedIPSconciliation = 0
                                        .ValueAcceptedEAPBconciliation = 0
                                        .ValuePayments = 0
                                        .BalanceGlosa = 0
                                        .LegalTransferValue = 0
                                        .BalanceLegal = 0
                                        If itemInvoice.OpeningBalance Is Nothing Then
                                            .OpeningBalance = False
                                        Else
                                            .OpeningBalance = itemInvoice.OpeningBalance
                                        End If
                                    End With
                                    .DocumentType = 2 'Reiteramos
                                    .State = 1
                                    .StateSave = True 'guardamos
                                End With
                            End If
                        Else
                            With _TmpObjectionsReceptionD
                                .InvoiceNumber = itemInvoice.InvoiceNumber.Trim
                                .ObservationInvoiceCode = itemInvoice.StateCurrentInvoice.Trim
                                .GlosaPortfolioGlosada = New GlosaPortfolioGlosada()
                                With .GlosaPortfolioGlosada
                                    .InvoiceNumber = itemInvoice.InvoiceNumber.Trim
                                    .BalanceInvoice = itemInvoice.BalanceInvoice.ToString.Trim
                                    .InvoiceValueEntity = itemInvoice.InvoiceValueEntity.ToString.Trim
                                    .InvoiceValuePacient = itemInvoice.InvoiceValuePacient.ToString.Trim
                                    .AccountantAccountCustomers = itemInvoice.AccountantAccountCustomers
                                    .State = 1 'se envia como pendiente confirmado
                                    .PortfolioAge = itemInvoice.PortfolioAge
                                    .InvoiceDate = itemInvoice.InvoiceDate
                                    If itemInvoice.RadicatedNumber IsNot Nothing Then
                                        .RadicatedNumber = itemInvoice.RadicatedNumber.Trim
                                    End If
                                    .RadicatedDate = itemInvoice.RadicatedDate
                                    .PatientCode = itemInvoice.PatientCode
                                    .PatientName = itemInvoice.PatientName
                                    .IngressNumber = itemInvoice.IngressNumber
                                    .IngressDate = itemInvoice.IngressDate
                                    .UserNameInvoice = itemInvoice.UserNameInvoice
                                    .ContractCode = itemInvoice.ContractCode
                                    .ContractName = itemInvoice.ContractName
                                    .Nit = nitReal
                                    .PlanCode = itemInvoice.CodePlan
                                    .ValueGlosado = 0
                                    .ValueAcceptedFirstInstance = 0
                                    .ValueReiterated = 0
                                    .ValueReiterationBalance = 0
                                    .ValueAcceptedSecondInstance = 0
                                    .ValueAcceptedIPSconciliation = 0
                                    .ValueAcceptedEAPBconciliation = 0
                                    .ValuePayments = 0
                                    .BalanceGlosa = 0
                                    .LegalTransferValue = 0
                                    .BalanceLegal = 0
                                    If itemInvoice.OpeningBalance Is Nothing Then
                                        .OpeningBalance = False
                                    Else
                                        .OpeningBalance = itemInvoice.OpeningBalance
                                    End If
                                End With
                                .DocumentType = 1 'glosado
                                .State = 1
                                .StateSave = True 'guardamos
                            End With
                        End If
                    End If

                    Dim obj = ListGlosaObjectionsReceptionD.Find(Function(value As GlosaObjectionsReceptionD) value.InvoiceNumber = _TmpObjectionsReceptionD.InvoiceNumber)
                    If obj Is Nothing AndAlso _TmpObjectionsReceptionD.InvoiceNumber IsNot Nothing AndAlso _TmpObjectionsReceptionD.InvoiceNumber <> "" Then
                        ListGlosaObjectionsReceptionD.Add(_TmpObjectionsReceptionD)
                    End If
                Else
                    StrMensaje = itemInvoice.InvoiceNumber.ToString + " Factura Radicada  en el oficio " + itemInvoice.RadicatedConsecutive.ToString
                    _listError.Add(StrMensaje)
                End If
            Else
                StrMensaje = item + " Factura No Existe o se encuentra en otro proceso "
                _listError.Add(StrMensaje)
            End If
        Next
        _Result.MessageResult = _listError
        _Result.StateResult = True
        _Result.ObjectEmbbeded = ListGlosaObjectionsReceptionD
        Return _Result
    End Function
    ''' <summary>
    ''' Funcion de copiar y pegar facturas en modo integracion
    ''' </summary>
    ''' <param name="ListInvoices"></param>
    ''' <param name="Nit"></param>
    ''' <param name="container"></param>
    ''' <param name="IndigoCompany"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ExcelIntegration(ByVal ListInvoices As List(Of String), Nit As String, container As String, IndigoCompany As String, HisContainer As String) As ActionResult(Of List(Of GlosaObjectionsReceptionD))
        Dim _Result As New ActionResult(Of List(Of GlosaObjectionsReceptionD))
        Dim _listError As New List(Of String)
        Dim ListGlosaObjectionsReceptionD As New List(Of GlosaObjectionsReceptionD)
        Dim ObjInterfaceParameter As GlosasParametersInterface = _IInterfaceParametersRepository.GetInterfacesParameters(container)
        For Each item As String In ListInvoices
            Dim StrMensaje As String = String.Empty
            Dim itemInvoice As New SP_invoiceList_Result
            itemInvoice = _ObjectionsReceptionCRepository.GetInvoice(container, Nit, item, IndigoCompany, HisContainer, String.Empty, 0) '0=recepcion, - 1 radicacion cunetas de cobro -  2 = devolucion
            Dim AccountValidate As Boolean
            If ObjInterfaceParameter.AccountingMethod = eTypeInterface.FoxPrivate Then
                AccountValidate = _IInterfaceParametersRepository.ValidateAccountTableFOxPrivate(itemInvoice.AccountantAccountCustomers, True)
            ElseIf ObjInterfaceParameter.AccountingMethod = eTypeInterface.NETPrivate Then
                AccountValidate = _IInterfaceParametersRepository.ValidateAccountTableNEtPrivate(itemInvoice.AccountantAccountCustomers, True)
            ElseIf ObjInterfaceParameter.AccountingMethod = eTypeInterface.NETPublic Then
                If itemInvoice.TraslateJuridical IsNot Nothing AndAlso itemInvoice.TraslateJuridical > 0 Then
                    _listError.Add("La factura " & itemInvoice.InvoiceNumber & " ya esta en proceso juridico")
                    Continue For
                End If
            Else
                AccountValidate = True 'para metodo Publico no aplica
            End If
            Dim nitReal As String
            Dim OBjCliente As Domain.Entities.Customer = _CustomerRepository.GetCustomerById(Nit)
            nitReal = OBjCliente.Nit
            'si no existe la cuenta en configuracion 
            If itemInvoice IsNot Nothing AndAlso itemInvoice.InvoiceNumber IsNot Nothing AndAlso Not itemInvoice.InvoiceNumber.Trim().Equals(String.Empty) Then
                If AccountValidate = True Then
                    If itemInvoice.StateCurrentInvoice = "2" Or itemInvoice.StateCurrentInvoice = "3" Or itemInvoice.StateCurrentInvoice = "4" Then
                        'para tener encuenta las facturas para reiteracion
                        If itemInvoice.RadicatedConsecutive Is Nothing Or itemInvoice.Reiterated = 1 Then
                            'declaro variable  tipo detalle factura
                            Dim _TmpObjectionsReceptionD As New GlosaObjectionsReceptionD
                            If itemInvoice.Reiterated = 1 Then
                                'para validar que factura que este en una coniliacion o pago parcial no ingrese como reiteracion
                                If itemInvoice.StatePortfolioGlosada = 11 Or itemInvoice.StatePortfolioGlosada = 12 Then
                                    With _TmpObjectionsReceptionD
                                        .InvoiceNumber = itemInvoice.InvoiceNumber.Trim
                                        .ObservationInvoiceCode = itemInvoice.StateCurrentInvoice.Trim
                                        .GlosaPortfolioGlosada = New GlosaPortfolioGlosada()
                                        With .GlosaPortfolioGlosada
                                            .InvoiceNumber = itemInvoice.InvoiceNumber.Trim
                                            .BalanceInvoice = itemInvoice.BalanceInvoice.ToString.Trim
                                            .InvoiceValueEntity = itemInvoice.InvoiceValueEntity.ToString.Trim
                                            .InvoiceValuePacient = itemInvoice.InvoiceValuePacient.ToString.Trim
                                            .AccountantAccountCustomers = itemInvoice.AccountantAccountCustomers.Trim
                                            .State = 1 'se envia como pendiente confirmado
                                            .PortfolioAge = itemInvoice.PortfolioAge
                                            .InvoiceDate = itemInvoice.InvoiceDate
                                            If itemInvoice.RadicatedNumber IsNot Nothing Then
                                                .RadicatedNumber = itemInvoice.RadicatedNumber.Trim
                                            End If
                                            .RadicatedDate = itemInvoice.RadicatedDate
                                            .PatientCode = itemInvoice.PatientCode.Trim
                                            .PatientName = itemInvoice.PatientName.Trim
                                            .IngressNumber = itemInvoice.IngressNumber.Trim
                                            .IngressDate = itemInvoice.IngressDate
                                            .UserNameInvoice = itemInvoice.UserNameInvoice.Trim
                                            .ContractCode = itemInvoice.ContractCode.Trim
                                            .ContractName = itemInvoice.ContractName.Trim
                                            .Nit = nitReal
                                            .PlanCode = itemInvoice.CodePlan
                                            .ValueGlosado = 0
                                            .ValueAcceptedFirstInstance = 0
                                            .ValueReiterated = 0
                                            .ValueReiterationBalance = 0
                                            .ValueAcceptedSecondInstance = 0
                                            .ValueAcceptedIPSconciliation = 0
                                            .ValueAcceptedEAPBconciliation = 0
                                            .ValuePayments = 0
                                            .BalanceGlosa = 0
                                            .LegalTransferValue = 0
                                            .BalanceLegal = 0
                                        End With
                                        .DocumentType = 2 'Reiteramos
                                        .State = 1
                                        .StateSave = True 'guardamos
                                    End With
                                    Dim obj = ListGlosaObjectionsReceptionD.Find(Function(value As GlosaObjectionsReceptionD) value.InvoiceNumber = _TmpObjectionsReceptionD.InvoiceNumber)
                                    If obj Is Nothing Then
                                        ListGlosaObjectionsReceptionD.Add(_TmpObjectionsReceptionD)
                                    End If
                                Else
                                    StrMensaje = itemInvoice.InvoiceNumber.ToString + " Factura Radicada  en el oficio " + itemInvoice.RadicatedConsecutive.ToString + " Se Encuentra en otro proceso"
                                    _listError.Add(StrMensaje)
                                End If
                            Else
                                With _TmpObjectionsReceptionD
                                    .InvoiceNumber = itemInvoice.InvoiceNumber.Trim
                                    .ObservationInvoiceCode = itemInvoice.StateCurrentInvoice.Trim
                                    .GlosaPortfolioGlosada = New GlosaPortfolioGlosada()
                                    With .GlosaPortfolioGlosada
                                        .InvoiceNumber = itemInvoice.InvoiceNumber.Trim
                                        .BalanceInvoice = itemInvoice.BalanceInvoice.ToString.Trim
                                        .InvoiceValueEntity = itemInvoice.InvoiceValueEntity.ToString.Trim
                                        .InvoiceValuePacient = itemInvoice.InvoiceValuePacient.ToString.Trim
                                        .AccountantAccountCustomers = itemInvoice.AccountantAccountCustomers.Trim
                                        .State = 1 'se envia como pendiente confirmado
                                        .PortfolioAge = itemInvoice.PortfolioAge
                                        .InvoiceDate = itemInvoice.InvoiceDate
                                        If itemInvoice.RadicatedNumber IsNot Nothing Then
                                            .RadicatedNumber = itemInvoice.RadicatedNumber.Trim
                                        End If
                                        .RadicatedDate = itemInvoice.RadicatedDate
                                        .PatientCode = itemInvoice.PatientCode.Trim
                                        .PatientName = itemInvoice.PatientName.Trim
                                        .IngressNumber = itemInvoice.IngressNumber.Trim
                                        .IngressDate = itemInvoice.IngressDate
                                        .UserNameInvoice = itemInvoice.UserNameInvoice.Trim
                                        .ContractCode = itemInvoice.ContractCode.Trim
                                        .ContractName = itemInvoice.ContractName.Trim
                                        .Nit = nitReal
                                        .PlanCode = itemInvoice.CodePlan
                                        .ValueGlosado = 0
                                        .ValueAcceptedFirstInstance = 0
                                        .ValueReiterated = 0
                                        .ValueReiterationBalance = 0
                                        .ValueAcceptedSecondInstance = 0
                                        .ValueAcceptedIPSconciliation = 0
                                        .ValueAcceptedEAPBconciliation = 0
                                        .ValuePayments = 0
                                        .BalanceGlosa = 0
                                        .LegalTransferValue = 0
                                        .BalanceLegal = 0
                                    End With
                                    .DocumentType = 1 'glosa
                                    .State = 1
                                    .StateSave = True 'guardamos
                                End With
                                Dim obj = ListGlosaObjectionsReceptionD.Find(Function(value As GlosaObjectionsReceptionD) value.InvoiceNumber = _TmpObjectionsReceptionD.InvoiceNumber)
                                If obj Is Nothing Then
                                    ListGlosaObjectionsReceptionD.Add(_TmpObjectionsReceptionD)
                                End If
                            End If

                        Else
                            StrMensaje = itemInvoice.InvoiceNumber.ToString + " Factura Radicada  en el oficio " + itemInvoice.RadicatedConsecutive.ToString
                            _listError.Add(StrMensaje)
                        End If
                    Else
                        Select Case itemInvoice.StateCurrentInvoice
                            Case "1"
                                StrMensaje = itemInvoice.InvoiceNumber.ToString + " Factura Sin Radicar "
                            Case "T"
                                StrMensaje = itemInvoice.InvoiceNumber.ToString + " Factura Radicada Sin Confirmar "
                            Case "6"
                                StrMensaje = itemInvoice.InvoiceNumber.ToString + " Factura Anulada "
                        End Select
                        _listError.Add(StrMensaje)
                    End If
                Else
                    StrMensaje = "La cuenta No esta Configurada en la columna (Factura Radicar), No se puede Agregar Factura " & itemInvoice.AccountantAccountCustomers & " - " & itemInvoice.InvoiceNumber
                    _listError.Add(StrMensaje)
                End If
            Else
                StrMensaje = item + " Factura No Existe o se encuentra en otro proceso "
                _listError.Add(StrMensaje)
            End If
        Next
        _Result.MessageResult = _listError
        _Result.StateResult = True
        _Result.ObjectEmbbeded = ListGlosaObjectionsReceptionD
        Return _Result
    End Function

    ''' <summary>
    ''' Lista las recepciones de objeciones por su estado
    ''' </summary>
    ''' <param name="status">Estado de las objeciones a listar</param>
    ''' <returns>Lista de objeciones</returns>
    Public Function ListObjectionsReceptionCByStatus(status As String) As List(Of GlosaObjectionsReceptionC) Implements IObjectionsReceptionCAdminService.ListObjectionsReceptionCByStatus
        If status Is Nothing Then
            Throw New ArgumentNullException("status", "Argument can't be null")
        End If
        If status.Trim().Equals(String.Empty) Then
            Throw New ArgumentException("status", "Argument can't be empty")
        End If
        Try
            Dim list = GlosaObjectionsReceptionCService.RefreshStateRecordProperties(Me._ObjectionsReceptionCRepository.ListObjectionsReceptionCByStatus(status.Trim()), GlosaObjectionsReceptionC.EProcess.Coordination)
            Return list
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of Domain.Entities.GlosaObjectionsReceptionC)()
        End Try
    End Function


    ''' <summary>
    ''' Guarda el oficio actualizado por el proceso de coordinacion
    ''' </summary>
    ''' <param name="ObjectionsReceptionC">Oficio a actualizar</param>
    ''' <param name="Session">mensaje de auditoria</param>
    ''' <returns>Resultado de la accion</returns>
    Public Function SaveObjectionsReceptionCInCoordication(ObjectionsReceptionC As GlosaObjectionsReceptionC, ListInvocie As List(Of String), Session As SessionValues) As ActionResult Implements IObjectionsReceptionCAdminService.SaveObjectionsReceptionCInCoordication
        Dim unitWork = _ObjectionsReceptionCRepository.UnitWork
        Dim UnitWorkPortfolioGlosada As IUnitWork = _PortfolioGlosadaRepository.UnitWork
        Dim result As ActionResult = New ActionResult()
        Try
            If ObjectionsReceptionC Is Nothing Then
                Throw New ArgumentNullException("ObjectionsReceptionC", "Argument can't be null")
            End If
            Dim AuxObjC As GlosaObjectionsReceptionC = Nothing
            'para la auditoria
            If ObjectionsReceptionC.ChangeTracker.State = ObjectState.Modified Then
                AuxObjC = _ObjectionsReceptionCRepository.GetObjection(ObjectionsReceptionC.RadicatedConsecutive, False)
                'AuxObjC = Nothing
            End If
            result.MessageResult = New List(Of String)
            'configuro la transaccion
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.MaximumTimeout
            txSettings.IsolationLevel = IsolationLevel.ReadCommitted
            'cargamos informacion del reponsable que confirma
            Dim _objResposible As Responsible = _IResponsibleRepository.GetResponsibleByCodeERP(Session.UserIndigo)

            If _objResposible.Id = 0 Then
                unitWork.RollbackChanges()
                UnitWorkPortfolioGlosada.RollbackChanges()
                Return New ActionResult With {.StateResult = False, .MessageResult = {"El codigo de usuario con el que esta logueado " + Session.UserIndigo + " no esta creado como Responsable para dar tramite de respuesta a una Glosa"}.ToList()}
            End If

            Dim ListPortfolio As List(Of GlosaPortfolioGlosada) = _PortfolioGlosadaRepository.ListPortfolio(ListInvocie)
            'inicio la transaccion
            Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
                For Each item As GlosaPortfolioGlosada In ListPortfolio
                    If item.State = "3" Then   'glosa
                        item.ResponsibleCoordinationGlosa = _objResposible.Id
                        item.CoordinationDateGlosa = Date.Now
                        item.TempState = item.State
                        item.State = "11"     'StatesGlosaPortfolio.GlosaConRespuesta
                    ElseIf item.State = "6" Then
                        item.ResponsibleCoordinationReiteration = _objResposible.Id
                        item.CoordinationDateReiteration = Date.Now
                        item.TempState = item.State
                        item.State = "12"    'StatesGlosaPortfolio.FinGlosa
                    End If
                    _PortfolioGlosadaRepository.SaveEntity(item)
                Next

                UnitWorkPortfolioGlosada.Commit()
                _ObjectionsReceptionCRepository.SaveEntity(ObjectionsReceptionC)
                unitWork.CommitAndRefreshChanges()
                result.StateResult = True
                scope.Complete()
            End Using

            IndigoAuditBasic.Execute("GlosaObjectionsReceptionC", Session.AuditMessageWcf.Functional, ObjectionsReceptionC.Id, Session.AuditMessageWcf.NameUser, Session.AuditMessageWcf.CodeUser, Session.AuditMessageWcf.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Modificar, Session.AuditMessageWcf.Company, Session.AuditMessageWcf.ContainerSecurity)
            Dim auditObject As New IndigoAuditSimpleEntity(Of GlosaObjectionsReceptionC)(ObjectionsReceptionC, Session.AuditMessageWcf, Infrastructure.CrossCutting.Audit.Actions.Update, AuxObjC)
            auditObject.Execute()
            Return result
        Catch ex As OptimisticConcurrencyException
            unitWork.RollbackChanges()
            UnitWorkPortfolioGlosada.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = Utils.GetInnerExceptionMessages(ex)}
        Catch ex As Exception
            unitWork.RollbackChanges()
            UnitWorkPortfolioGlosada.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return New ActionResult With {.StateResult = False, .MessageResult = Utils.GetInnerExceptionMessages(ex)}
        End Try
    End Function
    ''' <summary>
    ''' Funcion para cargar dataset de datos de oficio de respuesta
    ''' </summary>
    ''' <param name="Filter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function OfficeReponse(ByVal Filter As String, ByVal LevelInvoice As Boolean, session As SessionValues) As DataSet Implements IObjectionsReceptionCAdminService.OfficeReponse
        Dim ds As New DataSet
        Dim connectionString = String.Empty
        connectionString = Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, session.TransactionalContainer, False)
        Using conexion As New SqlConnection(connectionString)
            Try
                Dim dtDatos As New DataTable("ViewOfficeResponse")
                Dim query As String = "SELECT [Id],[RadicatedConsecutive],[DocumentDate],[RadicatedDate],[Name],[ReceivesTheSettled],[DocumentCommentRadicated],[PatientCode]
                  ,[PatientName],[RadicatedNumber],[InvoiceNumber],[BalanceInvoice],[ConceptGlosa],[ConceptEvalution],[JustificationGlosaText],[JustificationReiterationText]
                  ,[valueglosado],[ValueAcceptedFirstInstance],[valuereiterated],[ValueAcceptedSecondInstance],[ServiceName],[MainGlosa],[ResponsibleGlosa]
                  ,[ResponsibleReiteration],[DocumentType],[Comments]
              FROM [dbo].[ViewOfficeResponse]"
                If LevelInvoice = True Then
                    query += " where invoicenumber = @filter"
                Else
                    query += " where id = @filter"
                End If
                Dim da As SqlDataAdapter = New SqlDataAdapter(query, conexion)
                da.SelectCommand.Parameters.AddWithValue("@filter", Filter)
                'establezco tiempos
                da.SelectCommand.CommandTimeout = 30000
                da.Fill(ds, "ViewOfficeResponse")
                dtDatos = ds.Tables("ViewOfficeResponse")
                conexion.Close()
                Return ds
            Catch ex As Exception
                'descarto los cambios en la eliminacion del detalle
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
                Return Nothing
            Finally
                conexion.Close()
            End Try
        End Using
    End Function
    ''' <summary>
    ''' Funcion para realizar la exportacion de una recepcion de objeciones a excel
    ''' </summary>
    ''' <param name="IdRecepcion">Id de la recepcion</param>
    ''' <param name="session">valroe de sesion</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReceptionExcelExport(ByVal IdRecepcion As String, session As SessionValues) As DataSet Implements IObjectionsReceptionCAdminService.ReceptionExcelExport
        Dim ds As New DataSet
        Dim connectionString = String.Empty
        connectionString = Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, session.TransactionalContainer, False)
        Using conexion As New SqlConnection(connectionString)
            Try
                Dim dtDatos As New DataTable("ReceptionExcelExport")
                Dim query As String = String.Format("SELECT * FROM Glosas.ViewGlosaObjectionsReceptionD WHERE GlosaObjectionsReceptionCId = {0}", IdRecepcion)
                Dim da As SqlDataAdapter = New SqlDataAdapter(query, conexion)
                'establezco tiempos
                da.SelectCommand.CommandTimeout = 30000
                da.Fill(ds, "ReceptionExcelExport")
                dtDatos = ds.Tables("ReceptionExcelExport")
                conexion.Close()
                Return ds
            Catch ex As Exception
                'descarto los cambios en la eliminacion del detalle
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
                Return Nothing
            Finally
                conexion.Close()
            End Try
        End Using
    End Function
    ''' <summary>
    ''' Funcion para realizar la exportacion de una recepcion de objeciones a excel
    ''' </summary>
    ''' <param name="IdRecepcion">Id de la recepcion</param>
    ''' <param name="session">valroe de sesion</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReceptionExcelExportFull(ByVal IdRecepcion As Integer, session As SessionValues) As DataTable Implements IObjectionsReceptionCAdminService.ReceptionExcelExportFull
        Try
            Dim ParametrosSQL As String

            'Parametros fijos
            ParametrosSQL = "" & IdRecepcion & ""

            Dim SQL As String = "EXEC [Glosas].[SP_ReceptionObjectionExport] " & ParametrosSQL


            Dim INDdtAdmissionReport = Me.GetDatatable(SQL, session, "SPCH_ReportAdmissionStatistics")
            Return INDdtAdmissionReport
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Metodo para obtener datatable.
    ''' </summary>
    ''' <param name="Comando"></param>
    ''' <param name="session"></param>
    ''' <param name="nameDt"></param>
    ''' <returns></returns>
    Private Function GetDatatable(ByVal Comando As String, session As SessionValues, nameDt As String) As System.Data.DataTable
        Dim connectionString = String.Empty
        connectionString = Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, session.TransactionalContainer, False)
        Using conexion As New SqlConnection(connectionString)
            Try
                If conexion.State = ConnectionState.Closed Then
                    conexion.Open()
                End If
                Dim da As SqlDataAdapter = New SqlDataAdapter(Comando, conexion)
                da.SelectCommand.CommandTimeout = 30000
                Dim ds As New DataSet
                da.Fill(ds, nameDt)
                GetDatatable = ds.Tables(nameDt)
                da = Nothing
                ds = Nothing
                conexion.Close()
                Return GetDatatable
            Catch ex As Exception
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
                Return Nothing
            Finally
                conexion.Close()

            End Try
        End Using
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _InterfacePublicFOX.Dispose()
            End If
            _ObjectionsReceptionCRepository = Nothing
            _ObjectionsReceptionDRepository = Nothing
            _PortfolioGlosadaRepository = Nothing
            _MovementGlosaRepository = Nothing
            _CustomerRepository = Nothing
            _ConsecutiveRepository = Nothing
            _IInterfaceParametersRepository = Nothing
            _InterfacePublicFOX = Nothing
            _IResponsibleRepository = Nothing
            _InvoiceDeatilRepository = Nothing
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
