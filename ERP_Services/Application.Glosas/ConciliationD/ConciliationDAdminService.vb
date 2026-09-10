'***********************************************************************
' Assembly         : Application.Glosas
' Author           : Juan Diego Diaz
' Created          : 22-05-2013
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
Imports Infrastructure.CrossCutting.Resources
Imports Application.Portfolio

#End Region

''' <summary>
''' Servicio de Conciliación Detalle.
''' </summary>
''' <remarks></remarks>
Public Class ConciliationDAdminService
    Implements IConciliationDAdminService

#Region "fields"

    Private _ConsecutiveRepository As IConsecutiveRepository
    Private _ConciliationCRepository As IConciliationCRepository
    Private _ConciliationDRepository As IConciliationDRepository
    Private _PortFolioGlosaRepository As IPortfolioGlosadaRepository
    Private _MovementGlosaRepository As IMovementGlosaRepository
    Private _IInterfaceParametersRepository As IInterfaceParametersRepository
    Private _IConciliationPartialPaymentsRepository As IPartialPaymentsMovementRepository
    Private _InvoiceDetailRepository As IInvoiceDetailRepository
    Private _InterfaceFox As IInterfaceFOX
    Private _InterfaceNet As IInterfaceNET
    Private _InterfacePublicFOX As IInterfacePublicFOX
    Private _InterfacePublicNET As IInterfacePublicNET
    Private _ResponseHierarchyRepository As IResponseHierarchyRepository
    Private _MovementGlosaConciliationRepository As IGlosaMovementGlosaConciliationRepository
    ''' <summary>
    ''' Servicio de secuencias numericas
    ''' </summary>
    ''' <remarks></remarks>
    Private _PortfolioSequenseAdminService As IPortfolioSequenseAdminService
    ''' <summary>
    ''' Servicio de interface en modo nativo
    ''' </summary>
    ''' <remarks></remarks>
    Private _InterfaceNativeAdminservice As IInterfaceNativeAdminService
    ''' <summary>
    ''' Repositorio de Cuentas por cobrar
    ''' </summary>
    ''' <remarks></remarks>
    Private _accountReceivableRepository As IAccountReceivableRepository
    ''' <summary>
    ''' repositorio Grupo de Atencion
    ''' </summary>
    ''' <remarks></remarks>
    Private _careGroupRepository As ICareGroupRepository
    ''' <summary>
    ''' Repositorio de Estructrura cuentas por cobrar
    ''' </summary>
    ''' <remarks></remarks>
    Private _AccountReceivableAccountingRepository As IAccountReceivableAccountingRepository
    ''' <summary>
    ''' Parametros de glosas
    ''' </summary>
    ''' <remarks></remarks>
    Private _ITimeGlossParametersRepository As ITimeParametersRepository


#End Region

#Region "Builds"
    ''' <summary>
    ''' Inicializa una nueva instancia de la clase <see cref="ConciliationDAdminService" />.
    ''' </summary>
    ''' <param name="ConciliationDRepository">El repositorio para el manejo de los detalles de conciliacion.</param>
    Public Sub New(ByVal ConciliationDRepository As IConciliationDRepository, ByVal ConciliationCRepository As IConciliationCRepository, ByVal ConsecutiveRepository As IConsecutiveRepository,
                   ByVal PortFolioGlosaRepository As IPortfolioGlosadaRepository, ByVal MovementGlosaRepository As IMovementGlosaRepository, ByVal IInterfaceParametersRepository As IInterfaceParametersRepository,
                   InterfaceFox As IInterfaceFOX, InterfaceNET As IInterfaceNET, InterfacePublicFOX As IInterfacePublicFOX, IConciliationPartialPaymentsRepository As IPartialPaymentsMovementRepository,
                   InvoiceDetailRepository As IInvoiceDetailRepository, InterfacePublicNET As IInterfacePublicNET,
                   ByVal InterfaceNativeAdminservice As IInterfaceNativeAdminService, ByVal PortfolioSequenseAdminService As IPortfolioSequenseAdminService,
                   accountReceivableRepository As IAccountReceivableRepository, careGroupRepository As ICareGroupRepository, AccountReceivableAccountingRepository As IAccountReceivableAccountingRepository,
                   ITimeGlossParametersRepository As ITimeParametersRepository, ByVal ResponseHierarchyRepository As IResponseHierarchyRepository, ByVal MovementGlosaConciliationRepository As IGlosaMovementGlosaConciliationRepository)
        If ConciliationDRepository Is Nothing Then
            Throw New ArgumentNullException("Detalle Conciliación Vacia")
        End If
        If ConciliationCRepository Is Nothing Then
            Throw New ArgumentNullException("cabecera Conciliación Vacia")
        End If
        If PortFolioGlosaRepository Is Nothing Then
            Throw New ArgumentNullException("Glosa Cartera Vacia")
        End If
        If MovementGlosaRepository Is Nothing Then
            Throw New ArgumentNullException("Movimiento Glosa Vacío")
        End If
        If IInterfaceParametersRepository Is Nothing Then
            Throw New ArgumentException("Repositorio Interfaz Vacio")
        End If
        If IConciliationPartialPaymentsRepository Is Nothing Then
            Throw New ArgumentException("Repositorio pagos parciales Vacio")
        End If
        If InvoiceDetailRepository Is Nothing Then
            Throw New ArgumentException("Repositorio detalle de factura vacio")
        End If
        If ConsecutiveRepository Is Nothing Then
            Throw New ArgumentNullException("Consecutivo Vacío")
        End If
        If PortfolioSequenseAdminService Is Nothing Then
            Throw New ArgumentNullException("PortfolioSequenseAdminService vacio", "Servicio de secuencias vacio")
        End If
        If InterfaceNativeAdminservice Is Nothing Then
            Throw New ArgumentNullException("InterfaceNativeAdminservice vacio", "Repositorio de interface nativo es vacio")
        End If
        If accountReceivableRepository Is Nothing Then
            Throw New ArgumentNullException("accountReceivableRepository vacio", "repositorio de cuentas por cobrar vacio")
        End If
        If careGroupRepository Is Nothing Then
            Throw New ArgumentNullException("careGroupRepository vacio", "repositorio de centro de atencion vacio")
        End If
        If AccountReceivableAccountingRepository Is Nothing Then
            Throw New ArgumentNullException("AccountReceivableAccountingRepository vacio", "Repositorio de estructura de cuenta de cobro vacio")
        End If
        If ITimeGlossParametersRepository Is Nothing Then
            Throw New ArgumentNullException("_ITimeGlossParametersRepository  vacio", "Repositorio de Parametros es vacio")
        End If
        _ConsecutiveRepository = ConsecutiveRepository
        _ConciliationCRepository = ConciliationCRepository
        _ConciliationDRepository = ConciliationDRepository
        _PortFolioGlosaRepository = PortFolioGlosaRepository
        _MovementGlosaRepository = MovementGlosaRepository
        _InvoiceDetailRepository = InvoiceDetailRepository
        _IInterfaceParametersRepository = IInterfaceParametersRepository
        _IConciliationPartialPaymentsRepository = IConciliationPartialPaymentsRepository
        _InterfaceFox = InterfaceFox
        _InterfaceNet = InterfaceNET
        _InterfacePublicFOX = InterfacePublicFOX
        _InterfacePublicNET = InterfacePublicNET
        _PortfolioSequenseAdminService = PortfolioSequenseAdminService
        _InterfaceNativeAdminservice = InterfaceNativeAdminservice
        _accountReceivableRepository = accountReceivableRepository
        _careGroupRepository = careGroupRepository
        _AccountReceivableAccountingRepository = AccountReceivableAccountingRepository
        _ITimeGlossParametersRepository = ITimeGlossParametersRepository
        _ResponseHierarchyRepository = ResponseHierarchyRepository
        _MovementGlosaConciliationRepository = MovementGlosaConciliationRepository
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Borrar Conciliación Detalle.
    ''' </summary>
    ''' <param name="ConciliacionD">Objeto Conciliación Detalle</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Action Result</returns>
    Public Function DeleteConciliationD(ConciliacionD As List(Of ConciliationD), audit As AuditMessage) As ActionResult Implements IConciliationDAdminService.DeleteConciliationD
        If ConciliacionD Is Nothing Then
            Throw New ArgumentNullException("Detalle Conciliación Vacia")
        End If
        Dim unitOfWork As IUnitWork = _ConciliationDRepository.UnitWork
        Try
            Dim count As Integer = ConciliacionD.Count - 1
            For i As Integer = 0 To count
                'Elimino el detalle de conciliación.
                _ConciliationDRepository.DeleteEntity(ConciliacionD(0))
            Next
            unitOfWork.Commit()
            For i As Integer = 0 To count
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute("ConciliationD", audit.Functional, ConciliacionD(i).Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            Next
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList()}
        End Try
    End Function
    ''' <summary>
    ''' Guardar Conciliación Detalle
    ''' </summary>
    ''' <param name="ConciliacionD">Objeto Conciliacion Detalle</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Action Result</returns>
    Public Function SaveConciliationD(ConciliacionD As List(Of ConciliationD), audit As AuditMessage) As ActionResult Implements IConciliationDAdminService.SaveConciliationD
        If ConciliacionD Is Nothing Then
            Throw New ArgumentNullException("Detalle conciliación vacio")
        End If
        Dim unitOfWork As IUnitWork = _ConciliationDRepository.UnitWork
        Dim unitOfWorkPortfolioGlosa As IUnitWork = _PortFolioGlosaRepository.UnitWork
        Dim unitOfWorkMovementGlosa As IUnitWork = _MovementGlosaRepository.UnitWork
        Try
            Dim j As Integer = 0
            Do
                If ConciliacionD(j).ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    _ConciliationDRepository.UpdateEntity(ConciliacionD(j))
                ElseIf ConciliacionD(j).ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    ConciliacionD(j).GlosaPortfolioGlosada = Nothing
                    _ConciliationDRepository.AddEntity(ConciliacionD(j))
                    'Obtengo los movimientos de glosas según factura
                    Dim movimientoGlosa As List(Of GlosaMovementGlosa) = _MovementGlosaRepository.ListAllMovementGlosaByInvoiceNumber(ConciliacionD(j).InvoiceNumber)
                    For Each itemMov As GlosaMovementGlosa In movimientoGlosa
                        If itemMov.State <> 5 Then
                            Dim stateMov As Integer = itemMov.State
                            itemMov.TempState = stateMov
                        End If
                        itemMov.State = 5
                        _MovementGlosaRepository.UpdateEntity(itemMov)
                    Next
                    'Obtengo el registro de Cartera Glosa y actualizo su estado a 7 "Pendiente Conciliar"
                    Dim _Cartera As GlosaPortfolioGlosada = _PortFolioGlosaRepository.GetPortfolioGlosada(ConciliacionD(j).InvoiceNumber)
                    Dim stateCart As Integer = _Cartera.State
                    'El campo TempState toma el valor que tiene actualmente State
                    _Cartera.TempState = stateCart
                    _Cartera.State = 7
                    _PortFolioGlosaRepository.UpdateEntity(_Cartera)
                ElseIf ConciliacionD(j).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted Then
                    'Obtengo los movimientos de glosas según factura
                    Dim movimientoGlosa As List(Of GlosaMovementGlosa) = _MovementGlosaRepository.ListAllMovementGlosaAndConciliationByInvoiceNumber(ConciliacionD(j).InvoiceNumber)
                    For Each itemMov As GlosaMovementGlosa In movimientoGlosa
                        If itemMov.ConciliationCId = ConciliacionD(j).ConciliationCId Then
                            itemMov.ConciliationCId = Nothing
                        End If
                        Dim currentConciliationMovements As New List(Of GlosaMovementGlosaConciliation)
                        If itemMov.GlosaMovementGlosaConciliation IsNot Nothing Then
                            currentConciliationMovements = itemMov.GlosaMovementGlosaConciliation.
                                Where(Function(d) d.ConciliationCId = ConciliacionD(j).ConciliationCId AndAlso d.State = 1).
                                ToList()

                            For Each Conciliation In currentConciliationMovements
                                _MovementGlosaConciliationRepository.DeleteEntity(Conciliation)
                            Next
                        End If

                        Dim stateMovTemp As Integer = itemMov.TempState
                        Dim stateMov As Integer = itemMov.State
                        itemMov.TempState = stateMov
                        itemMov.State = stateMovTemp

                        itemMov.IdResponseHierarchyConciliation = Nothing
                        itemMov.ConciliationCId = Nothing
                        itemMov.RationaleConciliation = Nothing
                        itemMov.RationaleDateConciliation = Nothing

                        'Solo se reversan valores registrados para la conciliacion pendiente que se esta retirando.
                        Dim valueAcceptedIPSconciliation = currentConciliationMovements.Sum(Function(d) d.ValueAcceptedIPSconciliation)
                        Dim valueAcceptedEAPBconciliation = currentConciliationMovements.Sum(Function(d) d.ValueAcceptedEAPBconciliation)

                        Dim restValueIPSconciliation = If(itemMov.ValueAcceptedIPSconciliation, 0) - valueAcceptedIPSconciliation
                        Dim restValueEAPBconciliation = If(itemMov.ValueAcceptedEAPBconciliation, 0) - valueAcceptedEAPBconciliation

                        If restValueIPSconciliation < 0 OrElse restValueEAPBconciliation < 0 Then
                            unitOfWorkMovementGlosa.RollbackChanges()
                            unitOfWork.RollbackChanges()
                            unitOfWorkPortfolioGlosa.RollbackChanges()
                            Return New ActionResult With {.StateResult = False, .Message = "No se pudo eliminar la factura ya que los valores del movimiento a empalmar son negativos o diferentes a 0."}
                        End If

                        If currentConciliationMovements.Any() Then
                            If restValueIPSconciliation = 0 Then
                                itemMov.ValueAcceptedIPSconciliation = Nothing
                            Else
                                itemMov.ValueAcceptedIPSconciliation = restValueIPSconciliation
                            End If

                            If restValueEAPBconciliation = 0 Then
                                itemMov.ValueAcceptedEAPBconciliation = Nothing
                            Else
                                itemMov.ValueAcceptedEAPBconciliation = restValueEAPBconciliation
                            End If
                        End If

                        _MovementGlosaRepository.UpdateEntity(itemMov)
                    Next
                    'Obtengo el registro de Cartera Glosa y actualizo su estado al estado temporal 
                    Dim _Cartera As GlosaPortfolioGlosada = _PortFolioGlosaRepository.GetPortfolioGlosada(ConciliacionD(j).InvoiceNumber)
                    Dim stateCartTemp As Integer = _Cartera.TempState
                    Dim stateCart As Integer = _Cartera.State
                    'El campo TempState toma el valor que tiene actualmente State
                    _Cartera.TempState = stateCart
                    _Cartera.State = stateCartTemp
                    _PortFolioGlosaRepository.UpdateEntity(_Cartera)
                    _ConciliationDRepository.DeleteEntity(ConciliacionD(j))
                End If
                j = j + 1
            Loop While j <= ConciliacionD.Count - 1
            'Confirmo la unidad de trabajo
            unitOfWork.CommitAndRefreshChanges()
            unitOfWorkPortfolioGlosa.CommitAndRefreshChanges()
            j = 0
            Do
                If ConciliacionD(j).ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    '/***** Auditoria Basica ********/
                    IndigoAuditBasic.Execute("ConciliationD", audit.Functional, ConciliacionD(j).Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                ElseIf ConciliacionD(j).ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    '/***** Auditoria Basica ********/
                    IndigoAuditBasic.Execute("ConciliationD", audit.Functional, ConciliacionD(j).Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                ElseIf ConciliacionD(j).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted Then
                    '/***** Auditoria Basica ********/
                    IndigoAuditBasic.Execute("ConciliationD", audit.Functional, ConciliacionD(j).Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
                End If
                j = j + 1
            Loop While j <= ConciliacionD.Count - 1
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            unitOfWorkPortfolioGlosa.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            unitOfWorkPortfolioGlosa.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList()}
        End Try
    End Function
    ''' <summary>
    ''' Obtiene una conciliación detalle según código.
    ''' </summary>
    ''' <param name="Id">Id Objecion detalle</param>
    ''' <returns>Objeto Conciliación Detalle</returns>
    Public Function GetConciliationDByIdObjectionD(Id As String) As ConciliationD Implements IConciliationDAdminService.GetConciliationDByIdObjectionD
        If String.IsNullOrEmpty(Id) = True Then
            Throw New ArgumentNullException("Id vacío")
        End If
        Try
            Return _ConciliationDRepository.GetConciliationDByIdObjectionD(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Obtiene todos los registros de conciliaciones detalle.
    ''' </summary>
    ''' <returns>Lista Conciliación Detalle</returns>
    Public Function ListAllConciliationD() As List(Of ConciliationD) Implements IConciliationDAdminService.ListAllConciliationD
        Try
            Return _ConciliationDRepository.ListAllConciliationD()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Obtiene una conciliación detalle según código.
    ''' </summary>
    ''' <param name="Id">Id Conciliación Cabecera</param>
    ''' <returns>Objeto Conciliación Detalle</returns>
    Public Function ListConciliationDByIdConciliationC(Id As String) As List(Of ConciliationD) Implements IConciliationDAdminService.ListConciliationDByIdConciliationC
        If String.IsNullOrEmpty(Id) = True Then
            Throw New ArgumentNullException("Id vacío")
        End If
        Try
            Return _ConciliationDRepository.ListConciliationDByIdConciliationC(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Función que obtiene registros de facturas.
    ''' </summary>
    ''' <param name="Factura">Numero Factura</param>
    ''' <param name="Nit"></param>
    ''' <param name="listStatus"></param>
    ''' <returns>Lista Cartera Glosa</returns>
    Public Function ListInvoicesByNumber(Factura As String, Nit As String, ByVal listStatus As List(Of String)) As GlosaPortfolioGlosada Implements IConciliationDAdminService.ListInvoicesByNumber
        Try
            If String.IsNullOrEmpty(Factura) = True Then
                Throw New ArgumentNullException("Factura vacío")
            End If
            If String.IsNullOrEmpty(Nit) = True Then
                Throw New ArgumentNullException("Nit vacío")
            End If
            Return _PortFolioGlosaRepository.ListInvoicesByNumber(Factura, Nit, listStatus)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Obtiene una cartera glosada por Nit
    ''' </summary>
    ''' <param name="Nit">Nit de la objecion</param>
    ''' <returns>Objeto Cartera Glosa</returns>
    Public Function ListConfirmGlosaPortfolio(Nit As String) As List(Of GlosaPortfolioGlosada) Implements IConciliationDAdminService.ListConfirmGlosaPortfolio
        Try
            If String.IsNullOrEmpty(Nit) = True Then
                Throw New ArgumentNullException("Nit vacío")
            End If
            Return _PortFolioGlosaRepository.ListConfirmGlosaPortfolio(Nit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Guardar Objecion en el detalle de factura
    ''' </summary>
    ''' <param name="Movimientos">Objeto Movimiento</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Action Result</returns>
    Public Function SaveConciliationInvoiceDetail(Movimientos As List(Of GlosaMovementGlosa), audit As AuditMessage) As ActionResult Implements IConciliationDAdminService.SaveConciliationInvoiceDetail
        If Movimientos Is Nothing Then
            Throw New ArgumentNullException("Lista de Movimiento vacía")
        End If
        Dim unitOfWork As IUnitWork = _MovementGlosaRepository.UnitWork
        Try
            For Each item As GlosaMovementGlosa In Movimientos
                If item.State <> 5 Then
                    item.TempState = item.State
                End If
                item.RationaleDateConciliation = Date.Now

                'Conciliaciones parciales
                Dim glosaMovementGlosaConciliation = item.GlosaMovementGlosaConciliation.Where(Function(d) d.ConciliationCId = item.ConciliationCId).FirstOrDefault()
                If glosaMovementGlosaConciliation Is Nothing Then
                    glosaMovementGlosaConciliation = New GlosaMovementGlosaConciliation With {.ConciliationCId = item.ConciliationCId}
                    item.GlosaMovementGlosaConciliation.Add(glosaMovementGlosaConciliation)
                End If

                Dim valueAcceptedIPSconciliation = item.GlosaMovementGlosaConciliation.Where(Function(d) d.State = 2).Sum(Function(d) d.ValueAcceptedIPSconciliation)
                Dim valueAcceptedEAPBconciliation = item.GlosaMovementGlosaConciliation.Where(Function(d) d.State = 2).Sum(Function(d) d.ValueAcceptedEAPBconciliation)

                glosaMovementGlosaConciliation.ResponseHierarchyConciliationId = item.IdResponseHierarchyConciliation
                glosaMovementGlosaConciliation.ValueAcceptedIPSconciliation = item.ValueAcceptedIPSconciliation - valueAcceptedIPSconciliation
                glosaMovementGlosaConciliation.ValueAcceptedEAPBconciliation = item.ValueAcceptedEAPBconciliation - valueAcceptedEAPBconciliation
                glosaMovementGlosaConciliation.RationaleConciliation = item.RationaleConciliation
                glosaMovementGlosaConciliation.RationaleDateConciliation = item.RationaleDateConciliation
                glosaMovementGlosaConciliation.State = 1

                _MovementGlosaRepository.UpdateEntity(item)
            Next
            unitOfWork.Commit()
            For Each item As GlosaMovementGlosa In Movimientos
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute("GlosaMovementGlosa", audit.Functional, item.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
            Next
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList()}
        End Try
    End Function
    ''' <summary>
    ''' Confirmar Objecion en el detalle de factura
    ''' </summary>
    ''' <param name="Movimientos">Objeto Movimiento</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Action Result</returns>
    Public Function ConfirmConciliationInvoiceDetail(Movimientos As List(Of GlosaMovementGlosa), audit As AuditMessage) As ActionResult Implements IConciliationDAdminService.ConfirmConciliationInvoiceDetail
        If Movimientos Is Nothing Then
            Throw New ArgumentNullException("Lista de Movimientos vacía")
        End If
        Dim unitOfWork As IUnitWork = _MovementGlosaRepository.UnitWork
        Try
            For Each item As GlosaMovementGlosa In Movimientos
                Dim glosaMovementGlosaConciliation = item.GlosaMovementGlosaConciliation.Where(Function(d) d.ConciliationCId = item.ConciliationCId).FirstOrDefault()
                If glosaMovementGlosaConciliation IsNot Nothing Then
                    glosaMovementGlosaConciliation.State = 2
                End If

                If item.ValuePendingConciliation = 0 Then
                    If item.State <> 6 Then
                        Dim stateMov As Integer = item.State
                        item.TempState = stateMov
                    End If
                    item.State = 6
                Else
                    item.State = item.TempState
                End If

                _MovementGlosaRepository.UpdateEntity(item)
            Next
            unitOfWork.Commit()
            For Each item As GlosaMovementGlosa In Movimientos
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute("GlosaMovementGlosa", audit.Functional, item.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Confirmar, audit.Company, audit.ContainerSecurity)
            Next
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList()}
        End Try
    End Function
    ''' <summary>
    ''' Confirmar factura
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero Factura</param>
    ''' <param name="IndigoSessionValues">Valores de sesion</param>
    ''' <returns>Action Result</returns>
    Public Function ConfirmConciliationInvoice(conciliationId As Integer, InvoiceNumber As String, _IdUnitoperating As Integer, IndigoSessionValues As SessionValues) As ActionResult Implements IConciliationDAdminService.ConfirmConciliationInvoice
        Dim unitOfWorkPortfolioGlosa As IUnitWork = _PortFolioGlosaRepository.UnitWork
        Dim unitOfWorkConciliationD As IUnitWork = _ConciliationDRepository.UnitWork
        Dim unitOfWorkMovementGlosa As IUnitWork = _MovementGlosaRepository.UnitWork
        Dim listStrValidateMessage As New List(Of String)

        Try
            If InvoiceNumber Is Nothing Then
                Throw New ArgumentNullException("Numero factura vacío")
            End If

            Dim _result As New ActionResult
            Dim conciliationD As ConciliationD = Nothing
            Dim portfolio As GlosaPortfolioGlosada = Nothing
            portfolio = _PortFolioGlosaRepository.GetPortfolioGlosadaWithAggregates(InvoiceNumber)
            Dim ObjD As GlosaObjectionsReceptionD = portfolio.GlosaObjectionsReceptionD.Where(Function(c As GlosaObjectionsReceptionD) c.DocumentType = 1 And c.State <> 4).SingleOrDefault()
            If portfolio.GlosaObjectionsReceptionD.Any(Function(c As GlosaObjectionsReceptionD) c.DocumentType = 2 And c.State <> 4) Then
                ObjD.ReiterateId = portfolio.GlosaObjectionsReceptionD.Where(Function(c As GlosaObjectionsReceptionD) c.DocumentType = 2 And c.State <> 4).FirstOrDefault().Id
            End If

            Dim _AccepteIPSConciliationReal As Decimal = 0
            Dim _AccepteEAPBConciliationReal As Decimal = 0
            Dim msgvalueAcceptEAPB As Decimal = 0
            Dim msgvalueAcceptIPS As Decimal = 0

            Dim ListMovAccepted As New List(Of GlosaMovementGlosa)
            For Each item As GlosaInvoiceDetail In ObjD.GlosaInvoiceDetail
                msgvalueAcceptIPS = msgvalueAcceptIPS + item.CalculateAcceptedIPSConciliation_PartialConciliation()
                msgvalueAcceptEAPB = msgvalueAcceptEAPB + item.CalculateAcceptedEAPBConciliation_PartialConciliation()
                If item.GlosaMovementGlosa.Count > 0 Then
                    For Each itemMov As GlosaMovementGlosa In item.GlosaMovementGlosa
                        If itemMov.ValuePendingConciliation IsNot Nothing Then
                            If itemMov.ConciliationCId IsNot Nothing AndAlso itemMov.ConciliationCId = conciliationId Then
                                Dim valueAcceptedIPSconciliation = If(itemMov.ValueAcceptedIPSconciliation Is Nothing, 0, itemMov.ValueAcceptedIPSconciliation)
                                Dim valueAcceptedEAPBconciliation = If(itemMov.ValueAcceptedEAPBconciliation Is Nothing, 0, itemMov.ValueAcceptedEAPBconciliation)
                                itemMov.ValueAcceptedIPSconciliation = valueAcceptedIPSconciliation
                                itemMov.ValueAcceptedEAPBconciliation = valueAcceptedEAPBconciliation

                                'Conciliaciones parciales
                                Dim glosaMovementGlosaConciliation = itemMov.GlosaMovementGlosaConciliation.Where(Function(d) d.ConciliationCId = itemMov.ConciliationCId).FirstOrDefault()
                                If glosaMovementGlosaConciliation IsNot Nothing Then
                                    glosaMovementGlosaConciliation.State = 2

                                    _AccepteIPSConciliationReal += glosaMovementGlosaConciliation.ValueAcceptedIPSconciliation
                                    _AccepteEAPBConciliationReal += glosaMovementGlosaConciliation.ValueAcceptedEAPBconciliation
                                    itemMov.ValuePendingConciliation = itemMov.ValuePendingConciliation - glosaMovementGlosaConciliation.ValueAcceptedIPSconciliation - glosaMovementGlosaConciliation.ValueAcceptedEAPBconciliation
                                Else
                                    _AccepteIPSConciliationReal += itemMov.ValueAcceptedIPSconciliation
                                    _AccepteEAPBConciliationReal += itemMov.ValueAcceptedEAPBconciliation
                                    itemMov.ValuePendingConciliation = itemMov.ValuePendingConciliation - itemMov.ValueAcceptedIPSconciliation - itemMov.ValueAcceptedEAPBconciliation
                                End If
                            End If

                            'si el valor pendiente del detalle de factura es cero, colocamos el estado 6 - conciliado
                            If itemMov.ValuePendingConciliation = 0 Then
                                If itemMov.State <> 6 Then
                                    Dim stateMov As Integer = itemMov.State
                                    itemMov.TempState = stateMov
                                End If
                                itemMov.State = 6
                            Else
                                itemMov.State = itemMov.TempState
                            End If

                            ListMovAccepted.Add(itemMov)
                        End If
                    Next
                End If
            Next

            'en estas variable temporales almcaenamos los valores ya totalizado de las aceptaciones
            Dim TmpAcumulateIPS As Decimal = IIf(portfolio.ValueAcceptedIPSconciliation Is Nothing, 0, portfolio.ValueAcceptedIPSconciliation)
            Dim TmpAcumulateEAPB As Decimal = IIf(portfolio.ValueAcceptedEAPBconciliation Is Nothing, 0, portfolio.ValueAcceptedEAPBconciliation)
            portfolio.ValueAcceptedIPSconciliation = _AccepteIPSConciliationReal
            portfolio.ValueAcceptedEAPBconciliation = _AccepteEAPBConciliationReal
            portfolio.BalanceGlosa = portfolio.BalanceGlosa - _AccepteIPSConciliationReal - _AccepteEAPBConciliationReal
            If portfolio.BalanceGlosa < 0 Then
                portfolio.BalanceGlosa = 0
            End If
            If portfolio.ValueAcceptedIPSconciliation <> msgvalueAcceptIPS OrElse portfolio.ValueAcceptedEAPBconciliation <> msgvalueAcceptEAPB Then
                'No son los mismos valores que se le mostraron al cliente
                listStrValidateMessage.Add("La suma de los detalles aceptados por la IPS (" & portfolio.ValueAcceptedIPSconciliation & ") ó la EAPB (" & portfolio.ValueAcceptedEAPBconciliation & "), no coincide con el valor calculado en el desplegable balance IPS (" & msgvalueAcceptIPS & ") EAPB (" & msgvalueAcceptEAPB & ")")
                Return New ActionResult With {.StateResult = False, .MessageResult = listStrValidateMessage}
            End If
            'configuro la transaccion
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.MaximumTimeout
            txSettings.IsolationLevel = IsolationLevel.ReadCommitted
            Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
                conciliationD = _ConciliationDRepository.GetConciliationDByInvoiceNumber(conciliationId, InvoiceNumber)
                conciliationD.State = 2
                _ConciliationDRepository.SaveEntity(conciliationD)

                If IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                    'Ejecutamos Interfaz de aceptación primera instancia Valores de Radicación
                    If ObjD IsNot Nothing Then
                        _result = ExecuteAcceptanceInterface(ObjD, IndigoSessionValues)
                        If _result.StateResult = True Then
                            unitOfWorkMovementGlosa.Commit()
                            unitOfWorkConciliationD.Commit()
                        Else
                            unitOfWorkMovementGlosa.RollbackChanges()
                            unitOfWorkConciliationD.RollbackChanges()
                            unitOfWorkPortfolioGlosa.RollbackChanges()
                            Return _result
                        End If
                    End If
                ElseIf IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.Native Or IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNet Or IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.IntegractionFox Or IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNATIVE_MIGRATIONS Then
                    Dim _AccountReceivable As AccountReceivable = _accountReceivableRepository.GetAccountReceivableByInvoiceNumberGloss(portfolio.InvoiceNumber) 'consultamos factura en cartera
                    If _AccepteEAPBConciliationReal > 0 AndAlso _AccountReceivable.AccountObjectionRemediedId IsNot Nothing Then
                        Dim accountingTmp = _AccountReceivable.AccountReceivableAccounting.Where(Function(x) x.MainAccountId = _AccountReceivable.AccountObjectionRemediedId).FirstOrDefault()
                        If accountingTmp Is Nothing Then
                            _result.StateResult = False
                            _result.MessageResult = {"Estructura de cartera no existe para la cuenta por cobrar: " & _AccountReceivable.Code & " - cuenta contable de glosa subsanable "}.ToList()
                            Return _result
                        End If
                        'MEDILASER-52646: se aplica una tolerancia de redondeo ya que el pago parcial (Recibo de Caja)
                        'puede afectar la cuenta con centavos que no se muestran en las pantallas de Conciliaciones/
                        'Pagos Parciales (los valores se presentan en pesos enteros), dejando el saldo contable real
                        'unos centavos por debajo del valor que el usuario percibe como conciliado.
                        Dim toleranceValue As Decimal = 1D
                        If accountingTmp.Balance < _AccepteEAPBConciliationReal - toleranceValue Then
                            _result.StateResult = False
                            _result.MessageResult = {"El saldo de la cuenta de glosas subsanable es menor a el valor aceptado por la EAPB"}.ToList()
                            Return _result
                        End If
                    End If
                    _result = ExecuteNativeInterface(ListMovAccepted, portfolio, ObjD, _AccountReceivable, _IdUnitoperating, IndigoSessionValues, conciliationId)
                    If _result.StateResult = True Then
                        unitOfWorkMovementGlosa.Commit()
                        unitOfWorkConciliationD.Commit()
                    Else
                        unitOfWorkMovementGlosa.RollbackChanges()
                        unitOfWorkConciliationD.RollbackChanges()
                        unitOfWorkPortfolioGlosa.RollbackChanges()
                        Return _result
                    End If
                End If

                Dim statePortfolio As Integer = portfolio.State
                If portfolio.BalanceGlosa = 0 Then
                    portfolio.State = 8
                Else
                    portfolio.State = 9 'para las conciliaciones parciales
                End If

                portfolio.ValueAcceptedIPSconciliation += TmpAcumulateIPS
                portfolio.ValueAcceptedEAPBconciliation += TmpAcumulateEAPB
                portfolio.TempState = statePortfolio
                _PortFolioGlosaRepository.SaveEntity(portfolio)
                unitOfWorkPortfolioGlosa.Commit()
                _result.StateResult = True
                scope.Complete()
            End Using
            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute("ConciliationD", IndigoSessionValues.AuditMessageWcf.Functional, conciliationD.Id, IndigoSessionValues.AuditMessageWcf.NameUser, IndigoSessionValues.AuditMessageWcf.CodeUser, IndigoSessionValues.AuditMessageWcf.WindowsUser, DateTime.Now, ActionsAudit.Modificar, IndigoSessionValues.AuditMessageWcf.Company, IndigoSessionValues.AuditMessageWcf.ContainerSecurity)
            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute("GlosaPortfolioGlosada", IndigoSessionValues.AuditMessageWcf.Functional, portfolio.Id, IndigoSessionValues.AuditMessageWcf.NameUser, IndigoSessionValues.AuditMessageWcf.CodeUser, IndigoSessionValues.AuditMessageWcf.WindowsUser, DateTime.Now, ActionsAudit.Modificar, IndigoSessionValues.AuditMessageWcf.Company, IndigoSessionValues.AuditMessageWcf.ContainerSecurity)
            Return _result
        Catch ex As OptimisticConcurrencyException
            unitOfWorkConciliationD.RollbackChanges()
            unitOfWorkPortfolioGlosa.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", IndigoSessionValues)
            Return New ActionResult With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWorkConciliationD.RollbackChanges()
            unitOfWorkPortfolioGlosa.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", IndigoSessionValues)
            Return New ActionResult With {.StateResult = False, .MessageResult = Utils.GetInnerExceptionMessages(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Funcion que ejecuta interfaces en modo nativo dependiendo del tipo de entidad Publica o Privada
    ''' </summary>
    ''' <param name="portfolio"></param>
    ''' <param name="itemD"></param>
    ''' <param name="_AccountReceivable"></param>
    ''' <param name="_IdUnitoperating"></param>
    ''' <param name="IndigoSessionValues"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ExecuteNativeInterface(listMov As List(Of GlosaMovementGlosa), portfolio As GlosaPortfolioGlosada, itemD As GlosaObjectionsReceptionD, _AccountReceivable As AccountReceivable, _IdUnitoperating As Integer, IndigoSessionValues As SessionValues, Optional ConciliationCId As Integer? = Nothing) As ActionResult
        Dim _ConfirmResult As New ActionResult
        Dim listStrMessage As New List(Of String)
        If IndigoSessionValues.IndigoCompanyType = eCompanyType.PrivateCompany Then
            _ConfirmResult = Me.ExecuteNativePrivate(listMov, portfolio, itemD, _AccountReceivable, _IdUnitoperating, IndigoSessionValues, ConciliationCId)
        ElseIf IndigoSessionValues.IndigoCompanyType = eCompanyType.PublicCompany Then
            _ConfirmResult = Me.ExecuteNativePublic(listMov, portfolio, itemD, _AccountReceivable, _IdUnitoperating, IndigoSessionValues, ConciliationCId)
        End If
        Return _ConfirmResult
    End Function

    ''' <summary>
    ''' Creamos Nota credito por lo aceptado en la IPS --- y comprobante contable de reversion de cuentas de orden si no se realizo desde el tramite de glosa y/o reiteracion
    ''' </summary>
    ''' <param name="portfolio"></param>
    ''' <param name="itemD"></param>
    ''' <param name="_AccountReceivable"></param>
    ''' <param name="_IdUnitoperating"></param>
    ''' <param name="IndigoSessionValues"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ExecuteNativePublic(listMov As List(Of GlosaMovementGlosa), portfolio As GlosaPortfolioGlosada, itemD As GlosaObjectionsReceptionD, _AccountReceivable As AccountReceivable, _IdUnitoperating As Integer, IndigoSessionValues As SessionValues, Optional ConciliationCId? As Integer = Nothing) As ActionResult
        Dim _ConfirmResult As New ActionResult
        Dim listStrMessage As New List(Of String)
        Try
            Dim tmpCreditResul As New ActionResult
            Dim tmpVoucherResul As New ActionResult
            Dim GlossParameter As TimeParameters = _ITimeGlossParametersRepository.GetTimeParametersDefault(_IdUnitoperating)
            If GlossParameter Is Nothing Then
                Return New ActionResult With {.StateResult = False, .MessageResult = {"No se encontrarón parametros de glosas"}.ToList()}
            End If
            If portfolio.ValueAcceptedIPSconciliation > 0 Then
                'Nota credito por lo aceptado por IPS
                Dim _sequense As Domain.Entities.PortfolioSequence = Me._PortfolioSequenseAdminService.GetSequenseByIdForm("686") 'tag del formulario de notas C/D
                Dim _idCurrentSequense As Integer
                If _sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
                    _idCurrentSequense = _sequense.PortfolioSequenceDetail(0).Id
                ElseIf _sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                    If _sequense.PortfolioSequenceDetail.Any(Function(S) S.IdOperatingUnit = _IdUnitoperating) Then
                        _idCurrentSequense = _sequense.PortfolioSequenceDetail.Where(Function(s) s.IdOperatingUnit = _IdUnitoperating).SingleOrDefault().Id
                    Else
                        tmpCreditResul.StateResult = False
                        tmpCreditResul.MessageResult = {ResourceManager.GetString("OperatingUnitUnassigned")}.ToList()
                        Return tmpCreditResul
                    End If
                End If
                tmpCreditResul = _InterfaceNativeAdminservice.createCreditNote(ETypeAcceptedIPSModule.AcceptanceConciliationProcessed, portfolio, _AccountReceivable, GlossParameter, _sequense.PortfolioSequenceDetail(0).Id, _IdUnitoperating, IndigoSessionValues, ConciliationCId)
                If tmpCreditResul.StateResult = True Then
                    For Each Item As String In tmpCreditResul.MessageResult
                        listStrMessage.Add(Item)
                    Next
                Else
                    Return tmpCreditResul
                End If
                'logica de descuento de honorarios medicos
                'Dim Objparameters As TimeParameters = _ITimeGlossParametersRepository.GetTimeParametersDefault()
                'Dim tmpFeesMedicalResult As ActionResult = Nothing
                'If Objparameters IsNot Nothing Then
                '    If Objparameters.DiscountedMedicalFees = True Then
                '        tmpFeesMedicalResult = _InterfaceNativeAdminservice.SaveMedicalFeesCausation(ETypeAcceptedIPSModule.AcceptanceConciliationProcessed, listMov, IndigoSessionValues.AuditMessageWcf)
                '        If tmpFeesMedicalResult.StateResult = False Then
                '            Return tmpFeesMedicalResult
                '        End If
                '    End If
                'End If
            End If
            'general vouceher
            'esta opcion es para reversar comprobante contable movimiento de cuentas de orden, en el caso en que la factura no haya pasado por el tramite de glosa "coordinacion o evaluacion" que
            'es donde normalmente se reversa el comprobante de recepcion de objeciones. esto porque se paso de recepcion de glosa o reiteracion directamente a conciliacion
            'se valida con el tempState ya que alli se guarda el estado temporal en el que venia la factura antes de haberse agregado al oficio de conciliacion
            Dim Valorglosado_or_Reiterado As Decimal
            Dim ReverseAccountAccounting As Boolean
            If portfolio.TempState = 2 Or portfolio.TempState = 5 Then
                Dim valueGlosa As Decimal = IIf(portfolio.ValueGlosado Is Nothing, 0, portfolio.ValueGlosado)
                Dim valuereiteration As Decimal = IIf(portfolio.ValueReiterated Is Nothing, 0, portfolio.ValueReiterated)
                If valueGlosa > 0 And valuereiteration > 0 Then
                    Valorglosado_or_Reiterado = portfolio.ValueReiterated
                ElseIf valueGlosa > 0 And valuereiteration = 0 Then
                    Valorglosado_or_Reiterado = portfolio.ValueGlosado
                End If
                ReverseAccountAccounting = True
            End If
            If ReverseAccountAccounting And Valorglosado_or_Reiterado > 0 Then
                tmpVoucherResul = _InterfaceNativeAdminservice.createJournalVouchersCompanyPublic(2, itemD, _AccountReceivable, GlossParameter, Valorglosado_or_Reiterado, IndigoSessionValues)
                If tmpVoucherResul.StateResult = True Then
                    For Each Item As String In tmpVoucherResul.MessageResult
                        listStrMessage.Add(Item)
                    Next
                Else
                    Return tmpVoucherResul
                End If
            End If
            _ConfirmResult.StateResult = True
            _ConfirmResult.MessageResult = listStrMessage
            Return _ConfirmResult
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", IndigoSessionValues)
            Return New ActionResult With {.StateResult = False, .MessageResult = Utils.GetInnerExceptionMessages(ex)}
        End Try
    End Function
    ''' <summary>
    ''' Creamos Nota Credito por lo aceptado por la IPS --- y Documento de reclasificacion 
    ''' </summary>
    ''' <param name="portfolio"></param>
    ''' <param name="_AccountReceivable"></param>
    ''' <param name="_IdUnitoperating"></param>
    ''' <param name="IndigoSessionValues"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ExecuteNativePrivate(listMov As List(Of GlosaMovementGlosa), portfolio As GlosaPortfolioGlosada, itemD As GlosaObjectionsReceptionD, _AccountReceivable As AccountReceivable, _IdUnitoperating As Integer, IndigoSessionValues As SessionValues, Optional ConciliationCId? As Integer = Nothing) As ActionResult
        Dim _ConfirmResult As New ActionResult
        Dim listStrMessage As New List(Of String)
        Dim listStrValidateMessage As New List(Of String)
        Try
            Dim tmpCreditResul As New ActionResult
            Dim tmpclasificationResul As New ActionResult
            Dim GlossParameter As TimeParameters = _ITimeGlossParametersRepository.GetTimeParametersDefault(_IdUnitoperating)
            If GlossParameter.Id = 0 Then
                Return New ActionResult With {.StateResult = False, .MessageResult = {"No se encontrarón parametros de glosas"}.ToList()}
            End If

            If portfolio.ValueAcceptedEAPBconciliation > 0 Then
                Dim _sequense As Domain.Entities.PortfolioSequence = Me._PortfolioSequenseAdminService.GetSequenseByIdForm("1529") 'tag del formulario de notaradicacion de cuentas para cargar secuencia de reclasificacion 
                Dim _idCurrentSequense As Integer
                If _sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
                    _idCurrentSequense = _sequense.PortfolioSequenceDetail(0).Id
                ElseIf _sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                    If _sequense.PortfolioSequenceDetail.Any(Function(S) S.IdOperatingUnit = _IdUnitoperating) Then
                        _idCurrentSequense = _sequense.PortfolioSequenceDetail.Where(Function(s) s.IdOperatingUnit = _IdUnitoperating).SingleOrDefault().Id
                    Else
                        tmpCreditResul.StateResult = False
                        tmpCreditResul.MessageResult = {ResourceManager.GetString("OperatingUnitUnassigned")}.ToList()
                        Return tmpCreditResul
                    End If
                End If
                Dim AccountReceivableAccountingunitOfWork As IUnitWork = _AccountReceivableAccountingRepository.UnitWork
                'reclasificacion
                If _AccountReceivable.AccountConciliationId Is Nothing Then
                    listStrValidateMessage.Add("La cuenta de conciliaciones no esta configurada para la factura: " & _AccountReceivable.InvoiceNumber)
                    Return New ActionResult With {.StateResult = False, .MessageResult = listStrValidateMessage}
                End If
                If GlossParameter.ConciliationJournalVoucherTypeId Is Nothing Then
                    listStrValidateMessage.Add("El tipo de documento contable para conciliaciones no esta configurada para la factura: " & _AccountReceivable.InvoiceNumber)
                    Return New ActionResult With {.StateResult = False, .MessageResult = listStrValidateMessage}
                End If
                'actualizo estructura cuenta de cobro glosa subsanable
                Dim _AccountReceivableAccountingRemedied As AccountReceivableAccounting = _AccountReceivableAccountingRepository.GetAccountReceivableAccounting(_AccountReceivable.Id, _AccountReceivable.AccountObjectionRemediedId)
                If _AccountReceivableAccountingRemedied.Id = 0 Then
                    listStrMessage.Add("Estructura de catera no existe para la cuenta por cobrar: " & _AccountReceivable.Code & " - cuenta contable de la Glosa Subsanable ")
                    Return New ActionResult With {.StateResult = False, .MessageResult = listStrMessage}
                End If

                With _AccountReceivableAccountingRemedied
                    .Balance = .Balance - portfolio.ValueAcceptedEAPBconciliation
                End With
                _AccountReceivableAccountingRepository.SaveEntity(_AccountReceivableAccountingRemedied)

                ' Consulto estructura cuenta conciliada SI ESTA EXISTE, ya que pudo haberse creado como resultado de una reiteracion 
                Dim _AccountReceivableAccountingConciliate As AccountReceivableAccounting = _AccountReceivableAccountingRepository.GetAccountReceivableAccounting(_AccountReceivable.Id, _AccountReceivable.AccountConciliationId)
                If _AccountReceivableAccountingConciliate IsNot Nothing AndAlso _AccountReceivableAccountingConciliate.Id > 0 Then
                    With _AccountReceivableAccountingConciliate
                        .Value = .Value + portfolio.ValueAcceptedEAPBconciliation
                        .Balance = .Balance + portfolio.ValueAcceptedEAPBconciliation
                    End With
                Else
                    'generamos nueva estructura de cuenta de cobro de conciliacion
                    With _AccountReceivableAccountingConciliate
                        .AccountReceivableId = _AccountReceivable.Id
                        .MainAccountId = _AccountReceivable.AccountConciliationId 'Id Cuenta subsanable
                        .ThirdPartyId = _AccountReceivable.ThirdPartyId
                        .CostCenterId = _AccountReceivable.CostCenterId
                        .Value = portfolio.ValueAcceptedEAPBconciliation
                        .Balance = portfolio.ValueAcceptedEAPBconciliation
                    End With
                End If
                _AccountReceivableAccountingRepository.SaveEntity(_AccountReceivableAccountingConciliate)
                'creamos documento de reclasificacion
                tmpclasificationResul = _InterfaceNativeAdminservice.CreatePortfolioReclassification(TypePortfolioReclassification.Conciliation, _idCurrentSequense, _AccountReceivable, portfolio.ValueAcceptedEAPBconciliation, _AccountReceivable.ThirdPartyId, GlossParameter, IndigoSessionValues.AuditMessageWcf)
                If tmpclasificationResul.StateResult = True Then
                    For Each Item As String In tmpclasificationResul.MessageResult
                        listStrMessage.Add(Item)
                    Next
                Else
                    Return tmpclasificationResul
                End If
                AccountReceivableAccountingunitOfWork.Commit()
            End If
            If portfolio.ValueAcceptedIPSconciliation > 0 Then
                'Nota credito por lo aceptado por IPS
                Dim _sequense As Domain.Entities.PortfolioSequence = Me._PortfolioSequenseAdminService.GetSequenseByIdForm("686") 'tag del formulario de notas C/D
                Dim _idCurrentSequense As Integer
                If _sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
                    _idCurrentSequense = _sequense.PortfolioSequenceDetail(0).Id
                ElseIf _sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                    If _sequense.PortfolioSequenceDetail.Any(Function(S) S.IdOperatingUnit = _IdUnitoperating) Then
                        _idCurrentSequense = _sequense.PortfolioSequenceDetail.Where(Function(s) s.IdOperatingUnit = _IdUnitoperating).SingleOrDefault().Id
                    Else
                        tmpCreditResul.StateResult = False
                        tmpCreditResul.MessageResult = {ResourceManager.GetString("OperatingUnitUnassigned")}.ToList()
                        Return tmpCreditResul
                    End If
                End If
                tmpCreditResul = _InterfaceNativeAdminservice.createCreditNote(ETypeAcceptedIPSModule.AcceptanceConciliationProcessed, portfolio, _AccountReceivable, GlossParameter, _sequense.PortfolioSequenceDetail(0).Id, _IdUnitoperating, IndigoSessionValues, ConciliationCId)
                If tmpCreditResul.StateResult = True Then
                    For Each Item As String In tmpCreditResul.MessageResult
                        listStrMessage.Add(Item)
                    Next
                Else
                    Return tmpCreditResul
                End If
                'logica de descuento de honorarios medicos
                'Dim Objparameters As TimeParameters = _ITimeGlossParametersRepository.GetTimeParametersDefault()
                'Dim tmpFeesMedicalResult As ActionResult = Nothing
                'If Objparameters IsNot Nothing Then
                '    If Objparameters.DiscountedMedicalFees = True Then
                '        tmpFeesMedicalResult = _InterfaceNativeAdminservice.SaveMedicalFeesCausation(ETypeAcceptedIPSModule.AcceptanceConciliationProcessed, listMov, IndigoSessionValues.AuditMessageWcf)
                '        If tmpFeesMedicalResult.StateResult = False Then
                '            Return tmpFeesMedicalResult
                '        End If
                '    End If
                'End If
            End If
            _ConfirmResult.StateResult = True
            _ConfirmResult.MessageResult = listStrMessage
            Return _ConfirmResult
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", IndigoSessionValues)
            Return New ActionResult With {.StateResult = False, .MessageResult = Utils.GetInnerExceptionMessages(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Metodo para la ejecucion de SP que realiza la interfaz  Aceptacion por parte de la IPS o EAPB 
    ''' </summary>
    ''' <param name="ObjectionsReceptionD">Objecto Factura que encapsula los datos para enviar como parametro del SP</param>
    ''' <param name="IndigoSessionValues">valores de sesion</param>
    ''' <returns>Lista de Mensaje del SP, un Codigo y un Mensaje </returns>
    ''' <remarks></remarks>
    Private Function ExecuteAcceptanceInterface(ByVal ObjectionsReceptionD As GlosaObjectionsReceptionD, ByVal IndigoSessionValues As SessionValues) As ActionResult
        'variable para almacenar la lista de mensaje a retornar
        Dim _result As New ActionResult
        _result.StateResult = True
        _result.MessageResult = New List(Of String)
        'Cargamos configuraciones
        Dim objParameter As GlosasParametersInterface = _IInterfaceParametersRepository.GetInterfacesParametersById(ObjectionsReceptionD.GlosasParametersInterfaceId)
        'si se aplica interfaz para la empresa
        If objParameter.Interface = True Then
            Dim InterfaceResult As InterfaceResult
            Dim NumeroGlosa As String = ObjectionsReceptionD.GlosaObjectionsReceptionC.RadicatedConsecutive
            Dim Factura As String = ObjectionsReceptionD.InvoiceNumber
            Dim Tercero As String = ObjectionsReceptionD.GlosaObjectionsReceptionC.Customer.Nit.Trim
            Dim CodEmpresaDGH As String = objParameter.ContainerName
            Dim ValorFactura As Decimal
            Dim Anio As Integer
            Anio = Year(ObjectionsReceptionD.GlosaPortfolioGlosada.InvoiceDate)
            Dim IntOpcion As String = "CONCILIACION"
            Dim User As String = IndigoSessionValues.UserInterface
            'Interfaz con valor que acepta la IPS
            Dim Modulo As String = "CON"
            Dim AfectaServicio As Integer
            If objParameter.AffectsService = True Then
                AfectaServicio = 1
            Else
                AfectaServicio = 0
            End If
            Dim plancode As String = ObjectionsReceptionD.GlosaPortfolioGlosada.PlanCode

            If objParameter.AccountingMethod = eTypeInterface.FoxPublic Or objParameter.AccountingMethod = eTypeInterface.NETPublic Then
                'METODO PUBLICO FOX
                'esta opcion es para reversar comprobante contable movimiento de cuentas de orden, en el caso en que la factura no haya pasado por el tramite de glosa "coordinacion o evaluacion" que
                'es donde normalmente se reversa el comprobante de recepcion de objeciones. esto porque se paso de recepcion de glosa o reiteracion directamente a conciliacion
                'se valida con el tempState ya que alli se guarda el estado temporal en el que venia la factura antes de haberse agregado al oficio de conciliacion
                Dim Valorglosado_or_Reiterado As Decimal
                Dim ReverseAccountAccounting As Boolean
                If ObjectionsReceptionD.GlosaPortfolioGlosada.TempState = 2 Or ObjectionsReceptionD.GlosaPortfolioGlosada.TempState = 5 Then
                    Dim ModuloOpcion As String = String.Empty
                    Dim valueGlosa As Decimal = IIf(ObjectionsReceptionD.GlosaPortfolioGlosada.ValueGlosado Is Nothing, 0, ObjectionsReceptionD.GlosaPortfolioGlosada.ValueGlosado)
                    Dim valuereiteration As Decimal = IIf(ObjectionsReceptionD.GlosaPortfolioGlosada.ValueReiterated Is Nothing, 0, ObjectionsReceptionD.GlosaPortfolioGlosada.ValueReiterated)
                    If valueGlosa > 0 And valuereiteration > 0 Then
                        Valorglosado_or_Reiterado = ObjectionsReceptionD.GlosaPortfolioGlosada.ValueReiterated
                    ElseIf valueGlosa > 0 And valuereiteration = 0 Then
                        Valorglosado_or_Reiterado = ObjectionsReceptionD.GlosaPortfolioGlosada.ValueGlosado
                    End If
                    ReverseAccountAccounting = True
                End If
                If objParameter.AccountingMethod = eTypeInterface.FoxPublic Then
                    If ObjectionsReceptionD.GlosaPortfolioGlosada.ValueAcceptedIPSconciliation > 0 Then
                        'para saber que estado dejamos en cartera en dinamica, si la EAPB acepta algo, marcamos estado 3, si no es porque la IPS acepta todo el saldo, marcamos estado 4 - aceptacion total IPS
                        Dim StatePortfolioDGH As String
                        If ObjectionsReceptionD.GlosaPortfolioGlosada.ValueAcceptedEAPBconciliation > 0 Then
                            StatePortfolioDGH = 3 'de lo contrario la dejamos como glosa recepcionada
                        Else
                            StatePortfolioDGH = 4 'si la IPS aceptado todo el valor pendiente, actualizamos el estado en cartera como 4 - Aceptado Total IPS
                        End If

                        ValorFactura = ObjectionsReceptionD.GlosaPortfolioGlosada.ValueAcceptedIPSconciliation
                        InterfaceResult = _InterfacePublicFOX.AcceptanceIPS(IndigoSessionValues.TransactionalContainer, NumeroGlosa, Factura, Tercero, CodEmpresaDGH, ValorFactura, Anio, IntOpcion, User, AfectaServicio, plancode, 0, StatePortfolioDGH)
                        _result.StateResult = InterfaceResult.Result
                        _result.MessageResult.Add(InterfaceResult.Message)
                        If _result.StateResult = False Then 'si hubo error retonto mensaje y no continuamos
                            Return _result
                        End If
                    End If
                    If ReverseAccountAccounting Then
                        InterfaceResult = _InterfacePublicFOX.AceptacionEAPBTotal(IndigoSessionValues.TransactionalContainer, NumeroGlosa, Factura, Tercero, CodEmpresaDGH, Valorglosado_or_Reiterado, Anio, IntOpcion, User, plancode)
                        _result.StateResult = InterfaceResult.Result
                        _result.MessageResult.Add(InterfaceResult.Message)
                    End If
                ElseIf objParameter.AccountingMethod = eTypeInterface.NETPublic Then
                    If ObjectionsReceptionD.GlosaPortfolioGlosada.ValueAcceptedIPSconciliation > 0 Then
                        'para saber que estado dejamos en cartera en dinamica, si la EAPB acepta algo, marcamos estado 3, si no es porque la IPS acepta todo el saldo, marcamos estado 4 - aceptacion total IPS
                        Dim StatePortfolioDGH As String
                        If ObjectionsReceptionD.GlosaPortfolioGlosada.ValueAcceptedEAPBconciliation > 0 Then
                            StatePortfolioDGH = 3 'de lo contrario la dejamos como glosa recepcionada
                        Else
                            StatePortfolioDGH = 4 'si la IPS aceptado todo el valor pendiente, actualizamos el estado en cartera como 4 - Aceptado Total IPS
                        End If

                        ValorFactura = ObjectionsReceptionD.GlosaPortfolioGlosada.ValueAcceptedIPSconciliation
                        InterfaceResult = _InterfacePublicNET.AcceptanceIPS(IndigoSessionValues.TransactionalContainer, NumeroGlosa, Factura, Tercero, CodEmpresaDGH, ValorFactura, Anio, IntOpcion, User, AfectaServicio, plancode, 0, StatePortfolioDGH)
                        _result.StateResult = InterfaceResult.Result
                        _result.MessageResult.Add(InterfaceResult.Message)
                        If _result.StateResult = False Then 'si hubo error retonto mensaje y no continuamos
                            Return _result
                        End If
                    End If
                    If ReverseAccountAccounting Then
                        InterfaceResult = _InterfacePublicNET.AceptacionEAPBTotal(IndigoSessionValues.TransactionalContainer, NumeroGlosa, Factura, Tercero, CodEmpresaDGH, Valorglosado_or_Reiterado, Anio, IntOpcion, User, plancode)
                        _result.StateResult = InterfaceResult.Result
                        _result.MessageResult.Add(InterfaceResult.Message)
                    End If
                End If

            Else
                'METODO PRIVADO FOX
                If ObjectionsReceptionD.GlosaPortfolioGlosada.ValueAcceptedIPSconciliation > 0 And ObjectionsReceptionD.GlosaPortfolioGlosada.ValueAcceptedEAPBconciliation > 0 Then
                    ValorFactura = ObjectionsReceptionD.GlosaPortfolioGlosada.ValueAcceptedIPSconciliation
                    If objParameter.AccountingMethod = eTypeInterface.FoxPrivate Then
                        InterfaceResult = _InterfaceFox.AcceptanceIPS(IndigoSessionValues.TransactionalContainer, NumeroGlosa, Factura, Tercero, CodEmpresaDGH, ValorFactura, Anio, IntOpcion, User, AfectaServicio, Modulo, True, ObjectionsReceptionD.GlosaPortfolioGlosada.ValueAcceptedEAPBconciliation)
                        _result.StateResult = InterfaceResult.Result
                        _result.MessageResult.Add(InterfaceResult.Message)
                    ElseIf objParameter.AccountingMethod = eTypeInterface.NETPrivate Then
                        InterfaceResult = _InterfaceNet.AcceptanceIPS(IndigoSessionValues.TransactionalContainer, NumeroGlosa, Factura, Tercero, CodEmpresaDGH, ValorFactura, Anio, IntOpcion, User, AfectaServicio, Modulo, True, ObjectionsReceptionD.GlosaPortfolioGlosada.ValueAcceptedEAPBconciliation)
                        _result.StateResult = InterfaceResult.Result
                        _result.MessageResult.Add(InterfaceResult.Message)
                    End If
                ElseIf ObjectionsReceptionD.GlosaPortfolioGlosada.ValueAcceptedIPSconciliation > 0 And ObjectionsReceptionD.GlosaPortfolioGlosada.ValueAcceptedEAPBconciliation = 0 Then
                    ValorFactura = ObjectionsReceptionD.GlosaPortfolioGlosada.ValueAcceptedIPSconciliation
                    If objParameter.AccountingMethod = eTypeInterface.FoxPrivate Then
                        InterfaceResult = _InterfaceFox.AcceptanceIPS(IndigoSessionValues.TransactionalContainer, NumeroGlosa, Factura, Tercero, CodEmpresaDGH, ValorFactura, Anio, IntOpcion, User, AfectaServicio, Modulo)
                        _result.StateResult = InterfaceResult.Result
                        _result.MessageResult.Add(InterfaceResult.Message)
                    ElseIf objParameter.AccountingMethod = eTypeInterface.NETPrivate Then
                        InterfaceResult = _InterfaceNet.AcceptanceIPS(IndigoSessionValues.TransactionalContainer, NumeroGlosa, Factura, Tercero, CodEmpresaDGH, ValorFactura, Anio, IntOpcion, User, AfectaServicio, Modulo)
                        _result.StateResult = InterfaceResult.Result
                        _result.MessageResult.Add(InterfaceResult.Message)
                    End If
                ElseIf ObjectionsReceptionD.GlosaPortfolioGlosada.ValueAcceptedEAPBconciliation > 0 And ObjectionsReceptionD.GlosaPortfolioGlosada.ValueAcceptedIPSconciliation = 0 Then
                    ValorFactura = ObjectionsReceptionD.GlosaPortfolioGlosada.ValueAcceptedEAPBconciliation
                    If objParameter.AccountingMethod = eTypeInterface.FoxPrivate Then
                        InterfaceResult = _InterfaceFox.AcceptanceEAPB(IndigoSessionValues.TransactionalContainer, NumeroGlosa, Factura, Tercero, CodEmpresaDGH, ValorFactura, Anio, IntOpcion, User)
                        _result.StateResult = InterfaceResult.Result
                        _result.MessageResult.Add(InterfaceResult.Message)
                    ElseIf objParameter.AccountingMethod = eTypeInterface.NETPrivate Then
                        InterfaceResult = _InterfaceNet.AcceptanceEAPB(IndigoSessionValues.TransactionalContainer, NumeroGlosa, Factura, Tercero, CodEmpresaDGH, ValorFactura, Anio, IntOpcion, User)
                        _result.StateResult = InterfaceResult.Result
                        _result.MessageResult.Add(InterfaceResult.Message)
                    End If
                End If
            End If
        Else
            _result.StateResult = False
            _result.MessageResult.Add("No esta activa la configuración de interfaz contable para la empresa " & objParameter.CompanyName)
        End If
        Return _result
    End Function

    ''' <summary>
    ''' 'Funcion para validar y subir conciliaciones desde excel
    ''' </summary>
    ''' <param name="dtSet"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateExcelDataConciliation(ByVal dtSet As DataSet, ByVal ConciliationC As ConciliationC, ByVal audit As AuditMessage) As ActionResult(Of ConciliationC) Implements IConciliationDAdminService.ValidateExcelDataConciliation
        Dim unitWorkMovement As IUnitWork = _MovementGlosaRepository.UnitWork
        Dim unitWorkPortFolio As IUnitWork = _PortFolioGlosaRepository.UnitWork
        Dim unitWorkConciliation As IUnitWork = _ConciliationCRepository.UnitWork
        Dim unitOfWorkConsecutive As IUnitWork = _ConsecutiveRepository.UnitWork
        Try
            If dtSet Is Nothing AndAlso dtSet.Tables("Datos") Is Nothing Then
                Throw New ArgumentNullException("Dataset de datos glosa excel esta vacio")
            End If
            Dim Dtable As DataTable = dtSet.Tables("Datos")
            Dtable.Columns.Add("Validate", Type.GetType("System.Int32"))
            Dim ListMessage As New List(Of String)
            'consulto las facturas "Distinct"
            Dim DtInvoice As DataTable = Dtable.DefaultView.ToTable(True, "Factura")
            'creamos los detalles de las conciliaciones

            For Each itemInvoice As DataRow In DtInvoice.Rows
                Dim _portfolio As GlosaPortfolioGlosada = _PortFolioGlosaRepository.GetPortfolioGlosadaWithoutAgregates(itemInvoice.Item("Factura").ToString)
                If _portfolio Is Nothing OrElse _portfolio.Id = 0 Then
                    ListMessage.Add("Factura no " & _portfolio.InvoiceNumber & " existe en la tabla cartera glosa")
                Else
                    If _portfolio.State = "7" Then
                        ListMessage.Add("Factura " & _portfolio.InvoiceNumber & " ya se encuentra en una conciliacion pendiente por confirmar, no se puede agregar a esta conciliacion")
                    ElseIf _portfolio.State = "8" Then
                        ListMessage.Add("Factura " & _portfolio.InvoiceNumber & " ya se encuentra en una conciliacion confirmada, no se puede agregar a esta conciliacion")
                    ElseIf _portfolio.State = "9" Then
                        ListMessage.Add("Factura " & _portfolio.InvoiceNumber & " ya se encuentra en una conciliacion parcial, no se puede agregar a esta conciliacion")
                    ElseIf _portfolio.BalanceGlosa = 0 Then
                        ListMessage.Add("Factura " & _portfolio.InvoiceNumber & " no tiene saldo pendiente, no se puede agregar a esta conciliacion")
                    Else
                        Dim ConD As New ConciliationD
                        With ConD
                            .GlosaPortfolioId = _portfolio.Id
                            .InvoiceNumber = _portfolio.InvoiceNumber
                            .State = 1
                        End With
                        ConciliationC.ConciliationD.Add(ConD)
                        'actualizamos estado en cartera de glosa
                        _portfolio.TempState = _portfolio.State
                        _portfolio.State = "7" 'pendiente de confirmar factura conciliacion
                        _PortFolioGlosaRepository.SaveEntity(_portfolio)
                    End If
                End If
            Next

            'procedo a generar cabecera y detalle, es decir el oficio de conciliacion
            Dim auditProcess As IndigoAuditSimpleEntity(Of ConciliationC)
            Dim status As Integer
            'si no hay errores 
            If ListMessage.Count = 0 Then
                'Obtenemos el numero de consecutivo
                ConciliationC.CreationUser = audit.CodeUser
                ConciliationC.CreationDate = Date.Now()
                auditProcess = New IndigoAuditSimpleEntity(Of ConciliationC)(ConciliationC, audit, status)
                auditProcess.Execute()
            End If

            Dim ListResponseHierarchy = _ResponseHierarchyRepository.ListResponseHierarchy

            'recorrer item item
            For Each item As DataRow In Dtable.Rows
                'consulta para saber si el item detalle de factura tiene mas de un movimiento
                'Dim datarow() As DataRow = Dtable.Select("[CodigoItemFactura] = " & item.Item("CodigoItemFactura") & " AND [Validate] IS NULL and ")
                Dim datarow() As DataRow = Dtable.Select("[CodigoItemFactura] = " & item.Item("CodigoItemFactura") & " AND [Validate] IS NULL AND ([Valor IPS conciliacion] >  0 OR [Valor EAPB conciliacion] > 0) ")
                ' Dim dtDataOneMoreMovement As DataTable = Dtable.Clone
                'tiene mas de un movimiento
                If datarow.Count > 1 Then
                    Dim valuePendingTotal As Decimal = 0
                    Dim valueIPSTotal As Decimal = 0
                    Dim valueEAPBTotal As Decimal = 0
                    'totalizamos aceptaciones del conjunto de movimiento pata x detalle de factura
                    For Each itemrow As DataRow In datarow
                        'dtDataOneMoreMovement.ImportRow(itemMov)
                        valuePendingTotal += Convert.ToDecimal(itemrow.Item("Valor Pendiente"))
                        valueIPSTotal += Convert.ToDecimal(itemrow.Item("Valor IPS conciliacion"))
                        valueEAPBTotal += Convert.ToDecimal(itemrow.Item("Valor EAPB conciliacion"))
                    Next
                    'para tener encuenta que cuando el detalle de factura esta glosado por mas de un item, debemos tener encuenta el maximo valor pendiente por aceptarse, para ello
                    'cargamos el detalle de factura para validar contra el maximo valor pendiente de aceptacion y lo registrado en aceptaciones IPS y EAPB del excel
                    Dim ObjDetail As GlosaInvoiceDetail = _InvoiceDetailRepository.GetGlosaInvoiceDetail(item.Item("CodigoItemFactura"))
                    If ObjDetail Is Nothing Then
                        ListMessage.Add("El Codigo Item Detalle de Factura " & item.Item("CodigoItemFactura").ToString & " no existe")
                    Else
                        Dim _ValueMaxPEndingConciliation As Decimal = ObjDetail.CalculateValuePending(2)
                        Dim TmpValuepEnding As Decimal = 0
                        TmpValuepEnding = valueIPSTotal + valueEAPBTotal
                        If TmpValuepEnding > _ValueMaxPEndingConciliation Then
                            ListMessage.Add("la sumatoria de lo (aceptado por la IPS + Aceptado EAPB)(" & FormatCurrency(TmpValuepEnding, 2) & ") es mayor al saldo pendiente (" & FormatCurrency(_ValueMaxPEndingConciliation, 2) & "). para el conjunto de movimientos del servicio " & item.Item("Codigo Servicio").ToString & " de la Factura N° " & item.Item("Factura").ToString)
                        End If
                    End If

                    ' recorro y valido movimiento de glosa por movimiento del conjunto del detalle de factura
                    For Each itemrow As DataRow In datarow
                        Dim ObjMov As GlosaMovementGlosa = _MovementGlosaRepository.GetMovementGlosaById(itemrow.Item("CodigoMovimiento").ToString)
                        If ObjMov Is Nothing OrElse ObjMov.Id = 0 Then
                            ListMessage.Add("El CodigoMovimiento " & itemrow.Item("CodigoMovimiento").ToString & " no existe, no se puede actualizar saldo")
                        Else
                            Dim valuePending As Decimal = Convert.ToDecimal(itemrow.Item("Valor Pendiente"))
                            Dim valueIPS As Decimal = Convert.ToDecimal(itemrow.Item("Valor IPS conciliacion"))
                            Dim valueEAPB As Decimal = Convert.ToDecimal(itemrow.Item("Valor EAPB conciliacion"))
                            Dim responseHierarchyConciliationId As Integer? = Nothing

                            If ObjMov.ValuePendingConciliation <> valuePending Then
                                ListMessage.Add("Los valores pendientes entre datos de excel y datos registrados no son iguales, Registro Movimiento " & itemrow.Item("CodigoMovimiento").ToString)
                            End If

                            If ObjMov.ValueAcceptedIPSconciliation IsNot Nothing And ObjMov.ValueAcceptedEAPBconciliation IsNot Nothing Then
                                If ObjMov.ValueAcceptedIPSconciliation > 0 And ObjMov.ValueAcceptedEAPBconciliation > 0 Then
                                    ListMessage.Add("Ya se han registrado conciliaciones para la factura " & item.Item("Factura").ToString & " Registro Movimiento " & item.Item("CodigoMovimiento").ToString)
                                End If
                            End If

                            If ObjMov.ValuePendingConciliation <= 0 Then
                                ListMessage.Add("El CodigoMovimiento " & itemrow.Item("CodigoMovimiento").ToString & " no tiene saldo pendiente")
                            End If

                            Dim TmpValuepEnding As Decimal = valueIPS + valueEAPB
                            If TmpValuepEnding > ObjMov.ValuePendingConciliation Then
                                ListMessage.Add("la sumatoria de lo (aceptado por la IPS + Aceptado EAPB)(" & FormatCurrency(TmpValuepEnding, 2) & ") es mayor al saldo pendiente (" & FormatCurrency(ObjMov.ValuePendingConciliation, 2) & "). Registro Movimiento " & itemrow.Item("CodigoMovimiento").ToString)
                            End If

                            'si no se presentaron mensajes de validacion porcedo a actualizar los movimiento
                            If ListMessage.Count = 0 Then
                                If valueIPS > 0 Then
                                    Dim conceptCode = itemrow.Item("Concepto de Aceptacion Conciliacion")
                                    If conceptCode IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(conceptCode) Then
                                        Dim responseHierarchy = ListResponseHierarchy.Where(Function(r) r.Code = conceptCode).FirstOrDefault()
                                        If responseHierarchy IsNot Nothing Then
                                            responseHierarchyConciliationId = responseHierarchy.Id
                                        End If
                                    End If
                                End If

                                ConciliationC.GlosaMovementGlosa.Add(ObjMov)
                                ObjMov.ValueAcceptedEAPBconciliation = valueEAPB
                                ObjMov.ValueAcceptedIPSconciliation = valueIPS
                                If ObjMov.State <> "5" Then
                                    ObjMov.TempState = ObjMov.State
                                End If
                                ObjMov.State = "5" 'pendiemnte conciliar
                                ObjMov.RationaleDateConciliation = Date.Now
                                ObjMov.IdResponseHierarchyConciliation = responseHierarchyConciliationId
                                ObjMov.RationaleConciliation = itemrow.Item("Comentario Conciliacion").ToString

                                'Conciliaciones parciales
                                Dim glosaMovementGlosaConciliation = ObjMov.GlosaMovementGlosaConciliation.Where(Function(d) d.ConciliationCId = ObjMov.ConciliationCId).FirstOrDefault()
                                If glosaMovementGlosaConciliation Is Nothing Then
                                    glosaMovementGlosaConciliation = New GlosaMovementGlosaConciliation With {.ConciliationCId = ObjMov.ConciliationCId}
                                    ObjMov.GlosaMovementGlosaConciliation.Add(glosaMovementGlosaConciliation)
                                End If

                                Dim valueAcceptedIPSconciliation = ObjMov.GlosaMovementGlosaConciliation.Where(Function(d) d.State = 2).Sum(Function(d) d.ValueAcceptedIPSconciliation)
                                Dim valueAcceptedEAPBconciliation = ObjMov.GlosaMovementGlosaConciliation.Where(Function(d) d.State = 2).Sum(Function(d) d.ValueAcceptedEAPBconciliation)

                                glosaMovementGlosaConciliation.ResponseHierarchyConciliationId = ObjMov.IdResponseHierarchyConciliation
                                glosaMovementGlosaConciliation.ValueAcceptedIPSconciliation = ObjMov.ValueAcceptedIPSconciliation - valueAcceptedIPSconciliation
                                glosaMovementGlosaConciliation.ValueAcceptedEAPBconciliation = ObjMov.ValueAcceptedEAPBconciliation - valueAcceptedEAPBconciliation
                                glosaMovementGlosaConciliation.RationaleConciliation = ObjMov.RationaleConciliation
                                glosaMovementGlosaConciliation.RationaleDateConciliation = ObjMov.RationaleDateConciliation
                                glosaMovementGlosaConciliation.State = 1
                            End If
                        End If
                        itemrow.Item("Validate") = 1 'item ya evaluado
                    Next
                ElseIf datarow.Count = 1 Then 'si solo es un movimiento
                    'If item.Item("Valor IPS conciliacion") = 0 And item.Item("Valor EAPB conciliacion") = 0 Then
                    '    ListMessage.Add("valor de aceptacion IPS y EAPB no pueden ser 0 o nulos. Registro Movimiento " & item.Item("CodigoMovimiento").ToString)
                    'End If
                    Dim ObjMov As GlosaMovementGlosa = _MovementGlosaRepository.GetMovementGlosaById(item.Item("CodigoMovimiento").ToString)
                    If ObjMov Is Nothing Then
                        ListMessage.Add("El CodigoMovimiento " & item.Item("CodigoMovimiento").ToString & " no existe, no se puede actualizar saldo")
                    Else
                        Dim valuePending As Decimal = Convert.ToDecimal(item.Item("Valor Pendiente"))
                        Dim valueIPS As Decimal = Convert.ToDecimal(item.Item("Valor IPS conciliacion"))
                        Dim valueEAPB As Decimal = Convert.ToDecimal(item.Item("Valor EAPB conciliacion"))
                        Dim responseHierarchyConciliationId As Integer? = Nothing

                        If ObjMov.ValuePendingConciliation <> valuePending Then
                            ListMessage.Add("Los valores pendientes entre datos de excel y datos registrados no son iguales, Registro Movimiento " & item.Item("CodigoMovimiento").ToString)
                        End If
                        If ObjMov.ValueAcceptedIPSconciliation IsNot Nothing Or ObjMov.ValueAcceptedEAPBconciliation IsNot Nothing Then
                            If ObjMov.ValueAcceptedIPSconciliation > 0 Or ObjMov.ValueAcceptedEAPBconciliation > 0 Then
                                ListMessage.Add("Ya se han registrado conciliaciones para la factura " & item.Item("Factura").ToString & " Registro Movimiento " & item.Item("CodigoMovimiento").ToString)
                            End If
                        End If
                        If ObjMov.ValuePendingConciliation <= 0 Then
                            ListMessage.Add("El CodigoMovimiento " & item.Item("CodigoMovimiento").ToString & " no tiene saldo pendiente")
                        End If
                        Dim TmpValuepEnding As Decimal = valueIPS + valueEAPB
                        If TmpValuepEnding > ObjMov.ValuePendingConciliation Then
                            ListMessage.Add("la sumatoria de lo (aceptado por la IPS + Aceptado EAPB)(" & FormatCurrency(TmpValuepEnding, 2) & ") es mayor al saldo pendiente (" & FormatCurrency(ObjMov.ValuePendingConciliation, 2) & "). Registro Movimiento " & item.Item("CodigoMovimiento").ToString)
                        End If

                        If ListMessage.Count = 0 Then
                            If valueIPS > 0 Then
                                Dim conceptCode = item.Item("Concepto de Aceptacion Conciliacion")
                                If conceptCode IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(conceptCode) Then
                                    Dim responseHierarchy = ListResponseHierarchy.Where(Function(r) r.Code = conceptCode).FirstOrDefault()
                                    If responseHierarchy IsNot Nothing Then
                                        responseHierarchyConciliationId = responseHierarchy.Id
                                    End If
                                End If
                            End If

                            ConciliationC.GlosaMovementGlosa.Add(ObjMov)
                            ObjMov.ValueAcceptedEAPBconciliation = valueEAPB
                            ObjMov.ValueAcceptedIPSconciliation = valueIPS
                            If ObjMov.State <> "5" Then
                                ObjMov.TempState = ObjMov.State
                            End If
                            ObjMov.State = "5" 'pendiemnte conciliar
                            ObjMov.RationaleDateConciliation = Date.Now
                            ObjMov.IdResponseHierarchyConciliation = responseHierarchyConciliationId
                            ObjMov.RationaleConciliation = item.Item("Comentario Conciliacion").ToString

                            'Conciliaciones parciales
                            Dim glosaMovementGlosaConciliation = ObjMov.GlosaMovementGlosaConciliation.Where(Function(d) d.ConciliationCId = ObjMov.ConciliationCId).FirstOrDefault()
                            If glosaMovementGlosaConciliation Is Nothing Then
                                glosaMovementGlosaConciliation = New GlosaMovementGlosaConciliation With {.ConciliationCId = ObjMov.ConciliationCId}
                                ObjMov.GlosaMovementGlosaConciliation.Add(glosaMovementGlosaConciliation)
                            End If

                            Dim valueAcceptedIPSconciliation = ObjMov.GlosaMovementGlosaConciliation.Where(Function(d) d.State = 2).Sum(Function(d) d.ValueAcceptedIPSconciliation)
                            Dim valueAcceptedEAPBconciliation = ObjMov.GlosaMovementGlosaConciliation.Where(Function(d) d.State = 2).Sum(Function(d) d.ValueAcceptedEAPBconciliation)

                            glosaMovementGlosaConciliation.ResponseHierarchyConciliationId = ObjMov.IdResponseHierarchyConciliation
                            glosaMovementGlosaConciliation.ValueAcceptedIPSconciliation = ObjMov.ValueAcceptedIPSconciliation - valueAcceptedIPSconciliation
                            glosaMovementGlosaConciliation.ValueAcceptedEAPBconciliation = ObjMov.ValueAcceptedEAPBconciliation - valueAcceptedEAPBconciliation
                            glosaMovementGlosaConciliation.RationaleConciliation = ObjMov.RationaleConciliation
                            glosaMovementGlosaConciliation.RationaleDateConciliation = ObjMov.RationaleDateConciliation
                            glosaMovementGlosaConciliation.State = 1
                        End If
                    End If
                End If
            Next

            Dim result As New ActionResult(Of ConciliationC)
            'inicio la transaccion
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions With {
                        .Timeout = TransactionManager.DefaultTimeout,
                        .IsolationLevel = IsolationLevel.ReadCommitted
                    }
                )

                If Not ListMessage.Any() Then
                    Dim consecutive = _ConsecutiveRepository _
                        .ExecuteQuery(Of Decimal)("UPDATE Common.Consecutive SET NumberConsecutive += 1 OUTPUT inserted.NumberConsecutive WHERE Code = {0}", 2)?.FirstOrDefault()
                    ConciliationC.ConciliationConsecutive = consecutive
                    _ConciliationCRepository.SaveEntity(ConciliationC)
                    unitWorkConciliation.Commit()
                    scope.Complete()
                    ListMessage.Add("Proceso Realizado Exitosamente")
                    result.StateResult = True
                    result.Message = "Proceso Realizado Exitosamente"
                    result.MessageResult = ListMessage
                    result.ObjectEmbbeded = ConciliationC
                Else
                    unitWorkConciliation.RollbackChanges()
                    unitOfWorkConsecutive.RollbackChanges()
                    unitWorkPortFolio.RollbackChanges()
                    unitWorkMovement.RollbackChanges()
                    scope.Dispose()
                    result.StateResult = False
                    result.Message = "Proceso Finalizado con errores"
                    result.MessageResult = ListMessage
                End If
            End Using
            Return result
        Catch ex As Exception
            unitWorkMovement.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ConciliationC) With {.StateResult = False, .Message = ex.Message.ToString}
        End Try

    End Function


#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _InterfaceFox.Dispose()
                _InterfaceNet.Dispose()
                _InterfacePublicFOX.Dispose()
                _InterfacePublicNET.Dispose()
                _PortfolioSequenseAdminService.Dispose()
                _InterfaceNativeAdminservice.Dispose()
            End If
            _ConsecutiveRepository = Nothing
            _ConciliationCRepository = Nothing
            _ConciliationDRepository = Nothing
            _PortFolioGlosaRepository = Nothing
            _MovementGlosaRepository = Nothing
            _InvoiceDetailRepository = Nothing
            _IInterfaceParametersRepository = Nothing
            _IConciliationPartialPaymentsRepository = Nothing
            _InterfaceFox = Nothing
            _InterfaceNet = Nothing
            _InterfacePublicFOX = Nothing
            _InterfacePublicNET = Nothing
            _PortfolioSequenseAdminService = Nothing
            _InterfaceNativeAdminservice = Nothing
            _accountReceivableRepository = Nothing
            _careGroupRepository = Nothing
            _AccountReceivableAccountingRepository = Nothing
            _ITimeGlossParametersRepository = Nothing
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
