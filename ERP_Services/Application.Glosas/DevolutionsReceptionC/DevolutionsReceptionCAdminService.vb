'***********************************************************************
' Assembly         : Application.Glosas
' Author           : Juan Diego Diaz
' Created          : 12-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Transactions

Imports Domain.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Entities
Imports Domain.Common.Entities
Imports Application.Base
Imports System.Data.Entity.Core
Imports Domain.InterfaceERPGlosa

#End Region

''' <summary>
''' Servicio de Devolución Cabecera.
''' </summary>
''' <remarks></remarks>
Public Class DevolutionsReceptionCAdminService
    Implements IDevolutionsReceptionCAdminService

    Private _DevolutionCRepository As IDevolutionsReceptionCRepository
    Private _DevolutionDRepository As IDevolutionsReceptionDRepository
    Private _ConsecutiveRepository As IConsecutiveRepository
    Private _PortfolioGlosaRepository As IPortfolioGlosadaRepository
    Private _MovementGlosaRepository As IMovementGlosaRepository
    Private _CustomerRepository As ICustomerRepository

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase <see cref="ConciliationCAdminService" />.
    ''' </summary>
    ''' <param name="DevolutionCRepository">El repositorio para el manejo de las cabeceras de conciliación.</param>
    Public Sub New(ByVal DevolutionCRepository As IDevolutionsReceptionCRepository, ByVal DevolutionDRepository As IDevolutionsReceptionDRepository, ByVal ConsecutiveRepository As IConsecutiveRepository, ByVal PortfolioGlosaRepository As IPortfolioGlosadaRepository, ByVal MovementGlosaRepository As IMovementGlosaRepository, ByVal CustomerRepository As ICustomerRepository)
        If DevolutionCRepository Is Nothing Then
            Throw New ArgumentNullException("Cabecera Devolución Vacia")
        End If
        If DevolutionDRepository Is Nothing Then
            Throw New ArgumentNullException("Cabecera Devolución Detalle Vacia")
        End If
        If ConsecutiveRepository Is Nothing Then
            Throw New ArgumentNullException("Consecutivo Vacío")
        End If
        If PortfolioGlosaRepository Is Nothing Then
            Throw New ArgumentNullException("Cartera Glosa Vacia")
        End If
        If MovementGlosaRepository Is Nothing Then
            Throw New ArgumentNullException("Movimiento Glosa Vacío")
        End If
        If CustomerRepository Is Nothing Then
            Throw New ArgumentNullException("Customer Vacío")
        End If
        _DevolutionCRepository = DevolutionCRepository
        _DevolutionDRepository = DevolutionDRepository
        _ConsecutiveRepository = ConsecutiveRepository
        _PortfolioGlosaRepository = PortfolioGlosaRepository
        _MovementGlosaRepository = MovementGlosaRepository
        _CustomerRepository = CustomerRepository

    End Sub

    ''' <summary>
    ''' Borrar Devolución Cabecera.
    ''' </summary>
    ''' <param name="DevoluciónC">Objeto Devolución Cabecera</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Action Result</returns>
    Public Function DeleteDevolutionC(DevoluciónC As GlosaDevolutionsReceptionC, audit As AuditMessage) As ActionResult Implements IDevolutionsReceptionCAdminService.DeleteDevolutionC
        If DevoluciónC Is Nothing Then
            Throw New ArgumentNullException("Cabecera Conciliación Vacia")
        End If
        Dim unitOfWork As IUnitWork = _DevolutionCRepository.UnitWork
        Try
            'Elimino la cabecera de devolución.
            _DevolutionCRepository.DeleteEntity(DevoluciónC)
            unitOfWork.Commit()
            Dim auditObject As New IndigoAuditSimpleEntity(Of GlosaDevolutionsReceptionC)(DevoluciónC, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList()}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una devolución cabecera según código.
    ''' </summary>
    ''' <param name="Id">Id Devolución Cabecera</param>
    ''' <returns>Objeto Devolución Cabecera</returns>
    Public Function GetDevolutionC(Id As String) As GlosaDevolutionsReceptionC Implements IDevolutionsReceptionCAdminService.GetDevolutionnC
        If String.IsNullOrEmpty(Id) = True Then
            Throw New ArgumentNullException("Id vacío")
        End If
        Try
            Return _DevolutionCRepository.GetDevolutionC(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una devolución cabecera según consecutivo.
    ''' </summary>
    ''' <param name="Consecutive">Consecutivo Devolución Cabecera</param>
    ''' <returns>Objeto Devolución Cabecera</returns>
    Public Function GetDevolutionCByConsecutive(Consecutive As String) As GlosaDevolutionsReceptionC Implements IDevolutionsReceptionCAdminService.GetDevolutionCByConsecutive
        If String.IsNullOrEmpty(Consecutive) = True Then
            Throw New ArgumentNullException("Consecutivo vacío")
        End If
        Try
            Return _DevolutionCRepository.GetDevolutionCByConsecutive(Consecutive)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Listar todas las devoluciones cabecera.
    ''' </summary>
    ''' <returns>Lista de Devoluciones Cabeceras</returns>
    Public Function ListAllDevolutionC() As List(Of GlosaDevolutionsReceptionC) Implements IDevolutionsReceptionCAdminService.ListAllDevolutionC
        Try
            Return _DevolutionCRepository.ListAllDevolutionC
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guardar Devolución Cabecera
    ''' </summary>
    ''' <param name="DevolutionC">Objeto Devolución Cabecera</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Action Result</returns>
    Public Function SaveDevolutionC(DevolutionC As GlosaDevolutionsReceptionC, ListDevolutionD As List(Of GlosaDevolutionsReceptionD), audit As AuditMessage) As ActionResult(Of GlosaDevolutionsReceptionC) Implements IDevolutionsReceptionCAdminService.SaveDevolutionC
        If DevolutionC Is Nothing Then
            Throw New ArgumentNullException("Cabecera devolución vacio")
        End If
        Dim unitOfWork As IUnitWork = TryCast(_DevolutionCRepository.UnitWork, IUnitWork)
        Dim unitOfWorkDevolutionD As IUnitWork = TryCast(_DevolutionDRepository.UnitWork, IUnitWork)
        Dim unitOfWorkConsecutive As IUnitWork = TryCast(_ConsecutiveRepository.UnitWork, IUnitWork)
        Dim idDevolutionC As Integer = 0
        Dim AuxListDevolutionD As New List(Of GlosaDevolutionsReceptionD)
        Dim AuxListDevolutionDDelete As New List(Of GlosaDevolutionsReceptionD)
        Dim AuxDevolutionC As GlosaDevolutionsReceptionC = DevolutionC.OriginalValue

        ' ''si la entidad esta en estado agregado o modificado se porcede a obtener el cliente por medio del nit
        'If DevolutionC.Customer.Id = Nothing Then
        '    'Variable cliente para almacenar la informacion persistida del mismo
        '    Dim _Tmpcustomer As Domain.Entities.Customer
        '    'si la entidad trae customer vacio se procede a consultar y asignar el id del customer
        '    'If ObjectionsReceptionC.Customer Is Nothing Then
        '    _Tmpcustomer = _CustomerRepository.GetCustomer(DevolutionC.Customer.Nit)
        '    DevolutionC.CustomerId = _Tmpcustomer.Id
        '    'End If
        'Else
        '    DevolutionC.CustomerId = DevolutionC.Customer.Id
        '    DevolutionC.Customer = Nothing
        'End If
        Try

            Dim auditProcess As IndigoAuditSimpleEntity(Of GlosaDevolutionsReceptionC)
            Dim AuxGlosaDevolutionsReceptionC As GlosaDevolutionsReceptionC = Nothing
            Dim status As Integer

            'configuro la transaccion
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.MaximumTimeout
            txSettings.IsolationLevel = IsolationLevel.ReadCommitted
            'inicio la transaccion
            Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
                'Determino si el registro es nuevo o modificado
                If DevolutionC.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    AuxGlosaDevolutionsReceptionC = DevolutionC.OriginalValue
                    DevolutionC.ModificationUser = audit.CodeUser
                    DevolutionC.ModificationDate = Date.Now()
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                    _DevolutionCRepository.UpdateEntity(DevolutionC)
                    'Confirmo la unidad de trabajo
                    unitOfWork.Commit()
                    auditProcess = New IndigoAuditSimpleEntity(Of GlosaDevolutionsReceptionC)(DevolutionC, audit, status, AuxGlosaDevolutionsReceptionC)
                    auditProcess.Execute()
                ElseIf DevolutionC.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Dim consecutive As Domain.Entities.Consecutive
                    Using (New TransactionScope(TransactionScopeOption.Suppress))
                        'Obtenemos el numero de consecutivo
                        consecutive = _ConsecutiveRepository.GetConsecutiveByCode("03")
                        If consecutive.Id = 0 Then
                            Return New ActionResult(Of GlosaDevolutionsReceptionC) With {.StateResult = False, .MessageResult = {"No se encontró consecutivo con código 3 para devoluciones"}.ToList()}
                        End If
                    End Using
                    DevolutionC.RadicatedConsecutive = CInt(consecutive.NumberConsecutive) + 1
                    'Adiciono la entidad
                    _DevolutionCRepository.AddEntity(DevolutionC)
                    'Actualizamos el numero de consecutivo
                    consecutive.NumberConsecutive += 1
                    DevolutionC.CreationUser = audit.CodeUser
                    DevolutionC.CreationDate = Date.Now()

                    If consecutive.Id = Nothing Then
                        consecutive.Description = "DEVOLUCION"
                        consecutive.Code = 3
                        _ConsecutiveRepository.AddEntity(consecutive)
                    Else
                        _ConsecutiveRepository.UpdateEntity(consecutive)
                    End If
                    Using (New TransactionScope(TransactionScopeOption.Suppress))
                        'Confirmo la unidad de trabajo
                        unitOfWorkConsecutive.Commit()
                    End Using

                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                    unitOfWork.Commit()
                    auditProcess = New IndigoAuditSimpleEntity(Of GlosaDevolutionsReceptionC)(DevolutionC, audit, status)
                    auditProcess.Execute()
                End If
                idDevolutionC = DevolutionC.Id
                Dim j As Integer = 0
                Do
                    If ListDevolutionD(j).ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        If ListDevolutionD(j).Id > 0 Then
                            Dim AuxDevolutionD = DevolutionC.OriginalValue.GlosaDevolutionsReceptionD.Where(Function(c) c.Id = ListDevolutionD(j).Id).SingleOrDefault
                            AuxListDevolutionD.Add(AuxDevolutionD)
                        End If
                        _DevolutionDRepository.UpdateEntity(ListDevolutionD(j))
                    ElseIf ListDevolutionD(j).ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        ListDevolutionD(j).GlosaDevolutionsReceptionCId = idDevolutionC
                        _DevolutionDRepository.AddEntity(ListDevolutionD(j))
                    ElseIf ListDevolutionD(j).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted Then
                        Dim AuxDevolutionD = _DevolutionDRepository.GetDevolutionDByIdDevolutionD(ListDevolutionD(j).Id, False)
                        AuxListDevolutionDDelete.Add(AuxDevolutionD)
                        _DevolutionDRepository.DeleteEntity(ListDevolutionD(j))
                    End If
                    j = j + 1
                Loop While j <= ListDevolutionD.Count - 1
                'Confirmo la unidad de trabajo
                unitOfWorkDevolutionD.Commit()
                scope.Complete()
            End Using
            Dim mensaje As List(Of String) = New List(Of String)
            mensaje.Add(DevolutionC.RadicatedConsecutive.ToString)
            mensaje.Add(DevolutionC.Id.ToString)
            DevolutionC.MarkAsUnchanged()
            Return New ActionResult(Of GlosaDevolutionsReceptionC) With {.StateResult = True, .MessageResult = mensaje, .ObjectEmbbeded = DevolutionC}
        Catch ex As OptimisticConcurrencyException
            unitOfWorkDevolutionD.RollbackChanges()
            unitOfWork.RollbackChanges()
            unitOfWorkConsecutive.RollbackChanges()
            Return New ActionResult(Of GlosaDevolutionsReceptionC) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWorkDevolutionD.RollbackChanges()
            unitOfWork.RollbackChanges()
            unitOfWorkConsecutive.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of GlosaDevolutionsReceptionC) With {.StateResult = False, .MessageResult = {ex.Message}.ToList()}
        End Try
    End Function


    ''' <summary>
    ''' Confirma la Devolución
    ''' </summary>
    ''' <param name="DevolutionC">Objeto Devolución Cabecera</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Action Result</returns>
    Public Function ConfirmDevolutionC(DevolutionC As GlosaDevolutionsReceptionC, audit As AuditMessage) As ActionResult Implements IDevolutionsReceptionCAdminService.ConfirmDevolutionC
        If DevolutionC Is Nothing Then
            Throw New ArgumentNullException("Cabecera devolución vacio")
        End If
        Dim unitOfWork As IUnitWork = _DevolutionCRepository.UnitWork
        Dim unitOfWorkPortfolio As IUnitWork = _PortfolioGlosaRepository.UnitWork
        Dim unitOfWorkMovement As IUnitWork = _MovementGlosaRepository.UnitWork
        Try
            'Determino si el registro es nuevo o modificado
            If DevolutionC.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                DevolutionC.Customer = Nothing
                DevolutionC.State = 2
                _DevolutionCRepository.SaveEntity(DevolutionC)
                'Confirmo la unidad de trabajo
                unitOfWork.Commit()
            End If
            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute("DevolutionsReceptionC", audit.Functional, DevolutionC.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Confirmar, audit.Company, audit.ContainerSecurity)
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            unitOfWorkPortfolio.RollbackChanges()
            unitOfWorkMovement.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            unitOfWorkPortfolio.RollbackChanges()
            unitOfWorkMovement.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = True, .MessageResult = {ex.Message}.ToList()}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _DevolutionCRepository = Nothing
            _DevolutionDRepository = Nothing
            _ConsecutiveRepository = Nothing
            _PortfolioGlosaRepository = Nothing
            _MovementGlosaRepository = Nothing
            _CustomerRepository = Nothing
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
