'***********************************************************************
' Assembly         : Application.Glosas
' Author           : RafaelPatiño
' Created          : 30-05-2013
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports System.Transactions
Imports Domain.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Common.Entities
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports System.Data.Entity.Core
Imports System.Data.Entity.Infrastructure
Imports Domain.InterfaceERPGlosa
Imports Application.Accounting
Imports Domain.Entities.Service
Imports System.Text

Public Class ObjectionsReceptionDAdminService
    Implements IObjectionsReceptionDAdminService


#Region "Fields"
    Private _ObjectionsReceptionDRepository As IObjectionsReceptionDRepository
    Private _ObjectionsReceptionDRepositoryCommit As IObjectionsReceptionDRepository
    Private _InvoiceDetailRepository As IInvoiceDetailRepository
    Private _InvoiceDetailRepositoryQX As IInvoiceDetailQxRepository
    Private _PortfolioGlosadaRepository As IPortfolioGlosadaRepository
    Private _MovementGlosaRepository As IMovementGlosaRepository
    Private _ConciliationDRepository As IConciliationDRepository
    Private _IInterfaceParametersRepository As IInterfaceParametersRepository
    Private _InterfaceFox As IInterfaceFOX
    Private _InterfaceNet As IInterfaceNET
    Private _InterfacePublicFOX As IInterfacePublicFOX
    Private _InterfacePublicNET As IInterfacePublicNET
    Private _IResponsibleRepository As IResponsibleRepository
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
    ''' Servicio de interface en modo nativo
    ''' </summary>
    ''' <remarks></remarks>
    Private _InterfaceNativeAdminservice As IInterfaceNativeAdminService
    ''' <summary>
    ''' Repositorio de Parametros de Glosas
    ''' </summary>
    ''' <remarks></remarks>
    Private _ITimeGlossParametersRepository As ITimeParametersRepository
#End Region

#Region "builds"

    ''' <summary>
    ''' Initializa una nueva instancia de la clase <see cref="IObjectionsReceptionDRepository" />.
    ''' </summary>
    ''' <param name="ObjectionsReceptionDRepository">el repositorio para el manejo de detalle de oficio.</param>
    Public Sub New(ByVal ObjectionsReceptionDRepository As IObjectionsReceptionDRepository, InvoiceDetailRepository As IInvoiceDetailRepository, InvoiceDetailRepositoryQX As IInvoiceDetailQxRepository,
                   PortfolioGlosadaRepository As IPortfolioGlosadaRepository, MovementGlosaRepository As IMovementGlosaRepository, ConciliationDRepository As IConciliationDRepository,
                   IInterfaceParametersRepository As IInterfaceParametersRepository, InterfaceFox As IInterfaceFOX, InterfaceNET As IInterfaceNET, InterfacePublicFOX As IInterfacePublicFOX,
                   InterfacePublicNET As IInterfacePublicNET, ByVal ObjectionsReceptionDRepositoryCommit As IObjectionsReceptionDRepository, ByVal IResponsibleRepository As IResponsibleRepository,
                   accountReceivableRepository As IAccountReceivableRepository, careGroupRepository As ICareGroupRepository, AccountReceivableAccountingRepository As IAccountReceivableAccountingRepository,
                   ByVal InterfaceNativeAdminservice As IInterfaceNativeAdminService, ITimeGlossParametersRepository As ITimeParametersRepository)
        If ObjectionsReceptionDRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Detalle Oficio Vacio")
        End If
        If PortfolioGlosadaRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Cartera Glosada Vacio")
        End If
        If InvoiceDetailRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Detalle de Factura Vacio")
        End If
        If MovementGlosaRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Movimiento Factura Vacio")
        End If
        If ConciliationDRepository Is Nothing Then
            Throw New ArgumentNullException("Detalle Conciliación Vacia")
        End If
        If IInterfaceParametersRepository Is Nothing Then
            Throw New ArgumentException("Repositorio Interfaz Vacio")
        End If
        If IResponsibleRepository Is Nothing Then
            Throw New ArgumentException("Repositorio Responsables Vacio")
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
        If InterfaceNativeAdminservice Is Nothing Then
            Throw New ArgumentNullException("InterfaceNativeAdminservice vacio", "Repositorio de interface nativo es vacio")
        End If
        If ITimeGlossParametersRepository Is Nothing Then
            Throw New ArgumentNullException("ITimeGlossParametersRepository vacio")
        End If
        _ObjectionsReceptionDRepository = ObjectionsReceptionDRepository
        _ObjectionsReceptionDRepositoryCommit = ObjectionsReceptionDRepositoryCommit
        _PortfolioGlosadaRepository = PortfolioGlosadaRepository
        _InvoiceDetailRepository = InvoiceDetailRepository
        _InvoiceDetailRepositoryQX = InvoiceDetailRepositoryQX
        _MovementGlosaRepository = MovementGlosaRepository
        _ConciliationDRepository = ConciliationDRepository
        _IInterfaceParametersRepository = IInterfaceParametersRepository
        _InterfaceFox = InterfaceFox
        _InterfaceNet = InterfaceNET
        _InterfacePublicFOX = InterfacePublicFOX
        _InterfacePublicNET = InterfacePublicNET
        _IResponsibleRepository = IResponsibleRepository
        _accountReceivableRepository = accountReceivableRepository
        _careGroupRepository = careGroupRepository
        _AccountReceivableAccountingRepository = AccountReceivableAccountingRepository
        _InterfaceNativeAdminservice = InterfaceNativeAdminservice
        _ITimeGlossParametersRepository = ITimeGlossParametersRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' lista del detalle de una objecion
    ''' </summary>
    ''' <param name="codeObjectionReceptionC">codigo de la objecion</param>
    ''' <returns>lista del detalle de una objecion </returns>
    Public Function ListAllObjectionsReceptionD(codeObjectionReceptionC As String) As List(Of Domain.Entities.GlosaObjectionsReceptionD) Implements IObjectionsReceptionDAdminService.ListAllObjectionsReceptionD
        If String.IsNullOrEmpty(codeObjectionReceptionC) = True Then
            Throw New ArgumentNullException("Codigo de objecion Vacio")
        End If
        Try
            Return _ObjectionsReceptionDRepository.ListAllObjectionsReceptionD(codeObjectionReceptionC)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' obtiene una lista de detalles confirmados de una recepcion
    ''' </summary>
    ''' <param name="Nit">Nit de la objecion</param>
    ''' <returns>una lista detalle de oficio</returns>
    Public Function ListConfirmObjectionReceptionD(ByVal Nit As String) As List(Of GlosaObjectionsReceptionD) Implements IObjectionsReceptionDAdminService.ListConfirmObjectionReceptionD
        If String.IsNullOrEmpty(Nit) Then
            Throw New ArgumentNullException("Nit vacío")
        End If
        Try
            Return _ObjectionsReceptionDRepository.ListConfirmObjectionReceptionD(Nit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Funcion para eliminacion masiva
    ''' </summary>
    ''' <param name="tmpList">lista de item a eliminar</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteListObjectionReceptionD(ByVal tmpList As List(Of GlosaObjectionsReceptionD), audit As AuditMessage, company As String) As ActionResult Implements IObjectionsReceptionDAdminService.DeleteListObjectionReceptionD
        Dim result As New Boolean
        Dim ListError As New List(Of String)
        If tmpList.Count > 0 Then
            For i As Integer = 0 To tmpList.Count - 1
                result = DeleteObjectionsReceptionD(tmpList(i), audit, company)
                If result = False Then
                    ListError.Add("Ocurrio un error eliminando factura: " & tmpList(i).InvoiceNumber)
                End If
            Next
        End If
        If ListError.Count = 0 Then
            Return New ActionResult With {.StateResult = True, .MessageResult = New List(Of String)({"OK"})}
        Else
            Return New ActionResult With {.StateResult = False, .MessageResult = ListError}
        End If
    End Function
    ''' <summary>
    ''' elimina un item del detalle de una objecion 
    ''' </summary>
    ''' <param name="ObjectionsReceptionD">el detalle de la objecion</param>
    ''' <param name="audit">mensaje auditoria</param>
    ''' <returns>valor de confirmacion del eliminado</returns>
    Public Function DeleteObjectionsReceptionD(ObjectionsReceptionD As GlosaObjectionsReceptionD, audit As Infrastructure.CrossCutting.Base.AuditMessage, company As String) As Boolean Implements IObjectionsReceptionDAdminService.DeleteObjectionsReceptionD
        If ObjectionsReceptionD Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Recepción Detalle de Objeciones Vacio")
        End If

        'RECARGA / REVALIDACION: se recarga el detalle desde la BD para trabajar con el TimeStamp (rowversion)
        'vigente del registro y de su cartera (el objeto llega desde el cliente y puede venir desfasado, lo que
        'dispara la DbUpdateConcurrencyException al actualizar la cartera). Ademas, si el detalle ya fue eliminado
        'por otra ejecucion (doble clic / doble envio), la operacion se considera exitosa por idempotencia.
        Dim freshItem As GlosaObjectionsReceptionD = _ObjectionsReceptionDRepository.getObjectionReceptionDByIdWithoutObjC(ObjectionsReceptionD.Id)
        If freshItem Is Nothing OrElse freshItem.Id = 0 Then
            Return True
        End If
        ObjectionsReceptionD = freshItem

        'Creamos la conexion
        Dim tx As System.Data.SqlClient.SqlTransaction = Nothing
        Dim committed As Boolean = False
        Using cnx As New System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, company, False))
            cnx.Open()

            Try
                'Toda la eliminacion se realiza sobre UNA sola transaccion (tx) para que sea atomica (todo-o-nada).
                'Ambas rutas (DocumentType 1 y 2) usan SQL crudo sobre esta misma conexion/transaccion; asi se evita
                'mezclar el SqlTransaction con los contextos EF (que abririan otra conexion y promoverian a MSDTC).
                tx = cnx.BeginTransaction()
                Dim command As New System.Data.SqlClient.SqlCommand("", cnx, tx)
                command.CommandTimeout = 30000
                command.CommandType = CommandType.Text

                'valido que la factura a eliminar no tenga movimienos en conciliacion detalle
                If _ConciliationDRepository.CountConciliationD(ObjectionsReceptionD.GlosaPortfolioGlosada.Id) > 0 Then
                    tx.Rollback()
                    Return False
                End If

                Dim validate As Boolean = False
                If ObjectionsReceptionD.DocumentType = 1 Then
                    'eliminamos movimientos y detalles Qx de cada detalle de la factura
                    Dim ListObjInvoiceDetailDelete As List(Of GlosaInvoiceDetail) = _InvoiceDetailRepository.ListGlosaInvoiceDetail(ObjectionsReceptionD.InvoiceNumber)
                    For Each invoiceDetail As GlosaInvoiceDetail In ListObjInvoiceDetailDelete
                        command.CommandText = "DELETE  FROM [Glosas].[GlosaMovementGlosa] WHERE [InvoiceDetailId]=" & invoiceDetail.Id
                        command.ExecuteNonQuery()
                        'Aqui eliminamos los detalles Qx
                        command.CommandText = "DELETE FROM [Glosas].[GlosaInvoiceDetailQX] WHERE [InvoiceDetailId]=" & invoiceDetail.Id
                        command.ExecuteNonQuery()
                    Next
                    'Aqui eliminamos los detalles
                    command.CommandText = "DELETE FROM [Glosas].[GlosaInvoiceDetail] WHERE [ObjectionsReceptionDId]=" & ObjectionsReceptionD.Id
                    command.ExecuteNonQuery()
                    'Aqui eliminamos el detalle de objecion
                    command.CommandText = "DELETE FROM [Glosas].[GlosaObjectionsReceptionD] WHERE [Id]=" & ObjectionsReceptionD.Id
                    command.ExecuteNonQuery()
                    command.CommandText = "select count(*) from glosas.GlosaObjectionsReceptionD WHERE [InvoiceNumber]='" & ObjectionsReceptionD.InvoiceNumber.Trim() & "'"
                    Dim intCount As Integer = command.ExecuteScalar()
                    ' si la factura no esta en la tabla ObjD procedemos a eliminar informacion de cartera
                    If intCount = 0 Then
                        'Aqui eliminamos la cartera
                        command.CommandText = "DELETE FROM [Glosas].[GlosaPortfolioGlosada] WHERE [InvoiceNumber]='" & ObjectionsReceptionD.InvoiceNumber.Trim() & "'"
                        command.ExecuteNonQuery()
                    End If
                    validate = True
                ElseIf ObjectionsReceptionD.DocumentType = 2 Then
                    'reiteracion: se revierte el estado de la cartera y se elimina el detalle de reiteracion. No se borran
                    'los movimientos de glosa (pertenecen a la glosa original). Se hace por SQL sobre la misma tx -> atomico.
                    command.CommandText = "UPDATE [Glosas].[GlosaPortfolioGlosada] SET [State]=11 WHERE [Id]=" & ObjectionsReceptionD.GlosaPortfolioGlosada.Id  '11-Glosa con Respuesta
                    command.ExecuteNonQuery()
                    command.CommandText = "DELETE FROM [Glosas].[GlosaObjectionsReceptionD] WHERE [Id]=" & ObjectionsReceptionD.Id
                    command.ExecuteNonQuery()
                    validate = True
                End If

                If validate Then
                    'confirmo la unica transaccion -> operacion atomica
                    tx.Commit()
                    committed = True
                    '/***** Auditoria Basica ********/
                    IndigoAuditBasic.Execute("GlosaObjectionsReceptionD", audit.Functional, ObjectionsReceptionD.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
                    '/***** Auditoria Avanzada *****/
                    Dim auditObject As New IndigoAuditSimpleEntity(Of GlosaObjectionsReceptionD)(ObjectionsReceptionD, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
                    auditObject.Execute()
                Else
                    tx.Rollback()
                End If
                Return validate
            Catch ex As Exception
                'descarto todos los cambios: al ser una unica transaccion, el rollback deja la BD intacta
                If tx IsNot Nothing AndAlso Not committed Then
                    tx.Rollback()
                End If
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return False
            Finally
                cnx.Close()
            End Try
        End Using

    End Function
    ''' <summary>
    ''' obtiene una lista de detalles de una recepcion
    ''' </summary>
    ''' <param name="GlosaObjectionsReceptionCId">codigo de la recpcion</param>
    ''' <returns>una lista detalle de oficio</returns>
    Public Function ListObjectionReceptionD(ByVal GlosaObjectionsReceptionCId As String) As List(Of GlosaObjectionsReceptionD) Implements IObjectionsReceptionDAdminService.ListObjectionReceptionD
        If String.IsNullOrEmpty(GlosaObjectionsReceptionCId) Then
            Throw New ArgumentNullException("Id Recepcion vacio")
        End If
        Try
            Return _ObjectionsReceptionDRepository.ListObjectionReceptionD(GlosaObjectionsReceptionCId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' obtiene el detalle de una recepcion
    ''' </summary>
    ''' <param name="InvoiceNumber">codigo de factura</param>
    ''' <param name="GlosaObjectionsReceptionCId">codigo de Cabecera</param>
    ''' <returns>un objecto detalle de oficio</returns>
    Public Function getObjectionReceptionD(ByVal InvoiceNumber As String, ByVal GlosaObjectionsReceptionCId As String) As GlosaObjectionsReceptionD Implements IObjectionsReceptionDAdminService.getObjectionReceptionD
        If String.IsNullOrEmpty(InvoiceNumber) Then
            Throw New ArgumentNullException("Numero factura Vacia")
        End If
        Try
            Return _ObjectionsReceptionDRepository.getObjectionReceptionD(InvoiceNumber, GlosaObjectionsReceptionCId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Obtiene el detalle de una recepcion según un responsable
    ''' </summary>
    ''' <param name="CodeResponsible">Id del responsable</param>
    ''' <returns>Una lista de detalle de oficio</returns>
    Public Function ListObjectionReceptionDByResponsable(ByVal CodeResponsible As String, timeParameterAdmin As ITimeParametersAdminService, ByVal _IdIOperatingUnit As Integer) As List(Of GlosaObjectionsReceptionD) Implements IObjectionsReceptionDAdminService.ListObjectionReceptionDByResponsable
        If String.IsNullOrEmpty(CodeResponsible) Then
            Throw New ArgumentNullException("Id responsable vacío")
        End If
        Try
            Dim Busqueda = _ObjectionsReceptionDRepository.ListObjectionReceptionDByResponsable(CodeResponsible)
            For Each item As GlosaObjectionsReceptionD In Busqueda
                Dim time = timeParameterAdmin.ListControlParametersTime(item.InvoiceNumber, item.GlosaObjectionsReceptionC.CustomerId.ToString, _IdIOperatingUnit)
                Dim timeMove As Integer
                Dim timeParameter As Integer

                If item.DocumentType = "1" Then
                    timeMove = time.Where(Function(c) c.Code = "02").SingleOrDefault.RemainingTime
                    timeParameter = time.Where(Function(c) c.Code = "02").SingleOrDefault.TimeParameters
                End If

                If item.DocumentType = "2" Then
                    timeMove = time.Where(Function(c) c.Code = "05").SingleOrDefault.RemainingTime
                    timeParameter = time.Where(Function(c) c.Code = "05").SingleOrDefault.TimeParameters
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
            Return Busqueda
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' obtiene una lista de detalles, cargando la cartera glosada, detalles quirugicos
    ''' </summary>
    ''' <param name="GlosaObjectionsReceptionDId">codigo de la recpcion</param>
    ''' <returns>una lista detalle de oficio</returns>
    Public Function GetListObjectionReceptionD(GlosaObjectionsReceptionDId As String) As List(Of GlosaObjectionsReceptionD) Implements IObjectionsReceptionDAdminService.GetListObjectionReceptionD
        If String.IsNullOrEmpty(GlosaObjectionsReceptionDId) Then
            Throw New ArgumentNullException("Id Detalle Factura vacio")
        End If
        Try
            Return _ObjectionsReceptionDRepository.GetListObjectionReceptionD(GlosaObjectionsReceptionDId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Funcion Para Actualizar el estado de una factura
    ''' </summary>
    ''' <param name="ObjectionsReceptionDId">objeto detalle de oficio</param>
    ''' <param name="IndigoSessionValues">Valores de sesion</param>
    ''' <returns></returns>
    Public Function ConfirmObjectionReceptionD(ObjectionsReceptionDId As Integer, ByVal IdSecuense As Integer, ByVal Nit As String, ByVal RadicateConsecutive As String, ByVal valueGlosa As Decimal, IndigoSessionValues As SessionValues) As ActionResult Implements IObjectionsReceptionDAdminService.ConfirmObjectionReceptionD
        If ObjectionsReceptionDId = 0 Then
            Throw New ArgumentNullException("Repositorio de Recepción Detalle de Objeciones Vacio")
        End If
        Dim _resultConfirm As New ActionResult
        Try
            Dim ObjectionsReceptionD As GlosaObjectionsReceptionD = _ObjectionsReceptionDRepositoryCommit.getObjectionReceptionDByIdWithoutObjC(ObjectionsReceptionDId)

            If ObjectionsReceptionD Is Nothing OrElse ObjectionsReceptionD.Id = 0 Then
                Return New ActionResult With {.StateResult = False, .MessageResult = {"No se encontró el detalle de la objeción a confirmar"}.ToList()}
            End If

            'REVALIDACION DE ESTADO: si el detalle ya quedo confirmado (State = 2) no se vuelve a procesar.
            'Neutraliza la causa mas comun de concurrencia: doble clic / doble envio / reintento en cola,
            'donde una segunda ejecucion intenta confirmar un registro que otra ya confirmo (rowversion desfasado).
            If ObjectionsReceptionD.State = 2 Then
                Return New ActionResult With {.StateResult = False, .MessageResult = {"La objeción ya fue confirmada previamente"}.ToList()}
            End If

            Dim _accountReceivable As AccountReceivable = _accountReceivableRepository.GetAccountReceivableByInvoiceNumber(ObjectionsReceptionD.InvoiceNumber)
            If _accountReceivable IsNot Nothing AndAlso (_accountReceivable.PortfolioStatus = 15 OrElse _accountReceivable.PortfolioStatus = 16) Then
                Dim message = String.Empty
                If _accountReceivable.PortfolioStatus = 15 Then
                    message = "No se puede confirmar la objeción, la factura es una cuenta de dificil recaudo"
                Else
                    message = "No se puede confirmar la objeción, la factura esta en un proceso de cobro jurídico"
                End If
                Return New ActionResult With {.StateResult = False, .MessageResult = {message}.ToList()}
            End If

            If valueGlosa > ObjectionsReceptionD.GlosaPortfolioGlosada.InvoiceValueEntity Then
                Return New ActionResult With {.StateResult = False, .MessageResult = {"El valor glosado no puede ser mayor al valor de la factura"}.ToList()}
            End If

            If IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                _resultConfirm = Me.ConfirmObjectionReceptionDintegration(ObjectionsReceptionD, Nit, RadicateConsecutive, valueGlosa, IndigoSessionValues)
            Else
                _resultConfirm = Me.ConfirmObjectionReceptionDNative(ObjectionsReceptionD, IdSecuense, Nit, RadicateConsecutive, valueGlosa, IndigoSessionValues)
            End If
            If _resultConfirm.StateResult = True Then
                Dim AuxObjD As GlosaObjectionsReceptionD = Nothing
                AuxObjD = _ObjectionsReceptionDRepository.getObjectionReceptionDById(ObjectionsReceptionD.Id, False)
                Dim AuxPortfolio As GlosaPortfolioGlosada = Nothing
                'para la auditoria
                If ObjectionsReceptionD.GlosaPortfolioGlosada.ChangeTracker.State = ObjectState.Modified Then
                    AuxPortfolio = AuxObjD.GlosaPortfolioGlosada
                End If
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute("GlosaObjectionsReceptionD", IndigoSessionValues.AuditMessageWcf.Functional, ObjectionsReceptionD.Id, IndigoSessionValues.AuditMessageWcf.NameUser, IndigoSessionValues.AuditMessageWcf.CodeUser, IndigoSessionValues.AuditMessageWcf.WindowsUser, DateTime.Now, ActionsAudit.Modificar, IndigoSessionValues.AuditMessageWcf.Company, IndigoSessionValues.AuditMessageWcf.ContainerSecurity)
                'Auditoria ObjD
                If ObjectionsReceptionD.GlosaPortfolioGlosada.ChangeTracker.State = ObjectState.Modified Then
                    Dim auditObject As New IndigoAuditSimpleEntity(Of GlosaObjectionsReceptionD)(ObjectionsReceptionD, IndigoSessionValues.AuditMessageWcf, Infrastructure.CrossCutting.Audit.Actions.Update, AuxObjD)
                    auditObject.Execute()
                End If
                'Auditoria GlosaPortfolioGlosada
                If ObjectionsReceptionD.GlosaPortfolioGlosada.ChangeTracker.State = ObjectState.Modified Then
                    Dim auditObject As New IndigoAuditSimpleEntity(Of GlosaPortfolioGlosada)(ObjectionsReceptionD.GlosaPortfolioGlosada, IndigoSessionValues.AuditMessageWcf, Infrastructure.CrossCutting.Audit.Actions.Update, AuxPortfolio)
                    auditObject.Execute()
                End If
            End If
            Return _resultConfirm
        Catch ex As DbUpdateConcurrencyException
            'SaveChanges lanza DbUpdateConcurrencyException (Infrastructure), cuyo inner es OptimisticConcurrencyException (Core).
            'Se atrapa aca explicitamente para devolver el codigo de concurrencia "-999" en vez del stack trace crudo.
            Return New ActionResult With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As OptimisticConcurrencyException
            Return New ActionResult With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", IndigoSessionValues)
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList()}
        End Try
    End Function


    ''' <summary>
    ''' Funcion Para Actualizar el estado de una factura
    ''' </summary>
    ''' <param name="itemD">objeto detalle de oficio</param>
    ''' <param name="IndigoSessionValues">Valores de sesion</param>
    ''' <returns></returns>
    Public Function ConfirmObjectionReceptionDNative(itemD As GlosaObjectionsReceptionD, ByVal idSequence As Integer, ByVal Nit As String, ByVal RadicateConsecutive As String, ByVal valueGlosa As Decimal, IndigoSessionValues As SessionValues) As ActionResult
        If itemD Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Recepción Detalle de Objeciones Vacio")
        End If
        'creo la unidad de trabajo para el detalle de la objeción
        Dim unitWorkObjectionsReceptionD As IUnitWork = TryCast(_ObjectionsReceptionDRepositoryCommit.UnitWork, IUnitWork)
        Dim AccountReceivableAccountingunitOfWork As IUnitWork = _AccountReceivableAccountingRepository.UnitWork
        Dim _resultConfirm As New ActionResult
        Try
            'configuro la transaccion
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.DefaultTimeout
            txSettings.IsolationLevel = IsolationLevel.ReadCommitted
            Dim ListStrMessage As New List(Of String)
            Dim _AccountReceivable As AccountReceivable = _accountReceivableRepository.GetAccountReceivableByInvoiceNumberGloss(itemD.InvoiceNumber) 'consultamos factura en cartera
            Dim GlossParameter As TimeParameters = _ITimeGlossParametersRepository.GetTimeParametersDefault(IndigoSessionValues.IndigoOperatingUnitId)
            If GlossParameter.Id = 0 Then
                Return New ActionResult With {.StateResult = False, .MessageResult = {"No se encontrarón parametros de glosas"}.ToList()}
            End If
            'inicio la transaccion
            Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
                If itemD.DocumentType = 1 Then
                    Dim TmpReclassificationResult As New ActionResult
                    If itemD.GlosaPortfolioGlosada.ValueGlosado <> valueGlosa Then
                        ListStrMessage.Add("El valor a confirmar en cartera " & itemD.GlosaPortfolioGlosada.ValueGlosado & " es diferente al valor en los detalles de la factura " & valueGlosa)
                        Return New ActionResult With {.StateResult = False, .MessageResult = ListStrMessage}
                    End If
                    itemD.State = 2 'confirmamos
                    itemD.GlosaPortfolioGlosada.ValueGlosado = valueGlosa
                    itemD.GlosaPortfolioGlosada.BalanceGlosa = valueGlosa
                    itemD.GlosaPortfolioGlosada.TempState = itemD.GlosaPortfolioGlosada.State
                    itemD.GlosaPortfolioGlosada.State = 2 '2- Pendiente Evaluacion Glosa
                    If IndigoSessionValues.IndigoCompanyType = eCompanyType.PrivateCompany Then
                        If _AccountReceivable.AccountObjectionRemediedId Is Nothing Then
                            ListStrMessage.Add("La cuenta Glosa subsanable esta vacia para la factura: " & _AccountReceivable.InvoiceNumber)
                            Return New ActionResult With {.StateResult = False, .MessageResult = ListStrMessage}
                        End If
                        'actualizo estructura cuenta de cobro Radicada, si existe, ya que pudo haberse creado en el momento de una primer radicacion
                        Dim _AccountReceivableAccountingRadicate As AccountReceivableAccounting = _AccountReceivableAccountingRepository.GetAccountReceivableAccounting(_AccountReceivable.Id, _AccountReceivable.AccountRadicateId)
                        If _AccountReceivableAccountingRadicate.Id = 0 Then
                            ListStrMessage.Add("Estructura de cartera no existe para la cuenta por cobrar: " & _AccountReceivable.Code & " - cuenta contable de factura radicada ")
                            Return New ActionResult With {.StateResult = False, .MessageResult = ListStrMessage}
                        End If
                        With _AccountReceivableAccountingRadicate
                            .Balance = .Balance - valueGlosa
                        End With
                        _AccountReceivableAccountingRepository.SaveEntity(_AccountReceivableAccountingRadicate)

                        'Si la cuenta radicada es diferente a la cuenta glosa subsanable se realiza registro de lo contrario no, 
                        'esto se realiza ya que en Medilaser solo manejan dos cuentas para glosa(Radicada y Sin Radicar)
                        If _AccountReceivable.AccountRadicateId <> _AccountReceivable.AccountObjectionRemediedId Then
                            'Se valida si ya existe estructura de cartera para la cuenta subsanable,
                            'ya que pudo haberse creado previamente en otra objeción/glosa de la misma factura.
                            'Evita el error de llave duplicada en UQ_AccountReceivableAccounting__AccountReceivableId__MainAccountId__INC__Id
                            Dim _AccountReceivableAccountingRemedied As AccountReceivableAccounting = _AccountReceivableAccountingRepository.GetAccountReceivableAccounting(_AccountReceivable.Id, _AccountReceivable.AccountObjectionRemediedId)
                            If _AccountReceivableAccountingRemedied IsNot Nothing AndAlso _AccountReceivableAccountingRemedied.Id > 0 Then
                                'Ya existe estructura para la cuenta subsanable: se actualiza en lugar de crear una nueva
                                With _AccountReceivableAccountingRemedied
                                    .Value += valueGlosa
                                    .Balance += valueGlosa
                                End With
                                _AccountReceivableAccountingRepository.SaveEntity(_AccountReceivableAccountingRemedied)
                            Else
                                'No existe: se crea por primera vez
                                _AccountReceivableAccountingRemedied = New AccountReceivableAccounting
                                With _AccountReceivableAccountingRemedied
                                    .AccountReceivableId = _AccountReceivable.Id
                                    .MainAccountId = _AccountReceivable.AccountObjectionRemediedId 'Id Cuenta subsanable
                                    .ThirdPartyId = _AccountReceivable.ThirdPartyId
                                    .CostCenterId = _AccountReceivable.CostCenterId
                                    .Value = valueGlosa
                                    .Balance = valueGlosa
                                End With
                                _AccountReceivableAccountingRepository.SaveEntity(_AccountReceivableAccountingRemedied)
                            End If
                        End If

                        'creamos documento de reclasificacion
                        TmpReclassificationResult = _InterfaceNativeAdminservice.CreatePortfolioReclassification(TypePortfolioReclassification.ReceptionofGlosas, idSequence, _AccountReceivable, valueGlosa, _AccountReceivable.ThirdPartyId, GlossParameter, IndigoSessionValues.AuditMessageWcf)
                        If TmpReclassificationResult.StateResult = False Then
                            scope.Dispose()
                            Return TmpReclassificationResult
                        End If
                        _ObjectionsReceptionDRepositoryCommit.SaveEntity(itemD)
                        unitWorkObjectionsReceptionD.Commit()
                        AccountReceivableAccountingunitOfWork.Commit()
                        'confirmo la transaccion
                        scope.Complete()
                        _resultConfirm = New ActionResult With {.StateResult = True, .MessageResult = TmpReclassificationResult.MessageResult}
                        Return _resultConfirm
                    ElseIf IndigoSessionValues.IndigoCompanyType = eCompanyType.PublicCompany Then
                        _resultConfirm = _InterfaceNativeAdminservice.createJournalVouchersCompanyPublic(1, itemD, _AccountReceivable, GlossParameter, valueGlosa, IndigoSessionValues)
                        If _resultConfirm.StateResult = False Then
                            Return _resultConfirm
                        End If
                        _ObjectionsReceptionDRepositoryCommit.SaveEntity(itemD)
                        unitWorkObjectionsReceptionD.Commit()
                        'confirmo la transaccion
                        scope.Complete()
                        Return _resultConfirm
                    End If
                ElseIf itemD.DocumentType = "2" Then

                    Dim unitOfWorkMovements As IUnitWork = _MovementGlosaRepository.UnitWork
                    Dim ListmpvalidateMovement As List(Of GlosaMovementGlosa) = _MovementGlosaRepository.ListMovementGlosaByInvoiceNumber(itemD.InvoiceNumber)
                    For Each itemMov As GlosaMovementGlosa In ListmpvalidateMovement
                        Dim ValuePendingConciliation As Decimal = IIf(itemMov.ValuePendingConciliation Is Nothing, 0, itemMov.ValuePendingConciliation)
                        If ValuePendingConciliation > 0 Then
                            itemMov.ValueReiterationBalance = itemMov.ValuePendingConciliation - itemMov.ValueReiterated
                            itemMov.ValuePendingConciliation = itemMov.ValueReiterated  'el valor pendiente a conciliar sera el valor reiterado  
                            itemMov.MarkAsModified()
                            If itemMov.ChangeTracker.State = ObjectState.Modified Then
                                _MovementGlosaRepository.SaveEntity(itemMov)
                            End If
                        End If
                    Next
                    unitOfWorkMovements.CommitAndRefreshChanges()

                    'totalizo los movimientos de esa factura
                    Dim TmpSaveSumValueReiteratedPortfolio As Decimal = ListmpvalidateMovement.Where(Function(c As GlosaMovementGlosa) c.MainGlosa = True).ToList().Sum(Function(c As GlosaMovementGlosa) c.ValueReiterated)
                    Dim _ValueReiterationBalance As Decimal = ListmpvalidateMovement.Where(Function(c As GlosaMovementGlosa) c.MainGlosa = True).ToList().Sum(Function(c As GlosaMovementGlosa) c.ValueReiterationBalance)
                    'actualizamos estado y saldo de cartera glosa
                    itemD.GlosaPortfolioGlosada.ValueReiterated = TmpSaveSumValueReiteratedPortfolio
                    itemD.GlosaPortfolioGlosada.ValueReiterationBalance = _ValueReiterationBalance
                    itemD.GlosaPortfolioGlosada.BalanceGlosa = TmpSaveSumValueReiteratedPortfolio
                    itemD.State = 2 'confirmamos   
                    itemD.GlosaPortfolioGlosada.TempState = itemD.GlosaPortfolioGlosada.State
                    itemD.GlosaPortfolioGlosada.State = 5 '5-pendiente evaluacion reitreacion
                    itemD.GlosaPortfolioGlosada.StatusTotal = 0
                    If IndigoSessionValues.IndigoCompanyType = eCompanyType.PrivateCompany Then
                        'que ralizamos, en ERP nota debito
                        'aplica solo para el metodo privado. 
                        'cuando la EAPB reitera por debajo del valor pendiente, se da a entender que la EAPB acepta el restante valor, por ende este valor se reclasifica de la cuenta
                        'de Glosa subsanable y se lleva a una cuenta conciliada.
                        If _ValueReiterationBalance > 0 Then
                            _resultConfirm = Me.BalanceReiteratedPrivateNativeCompany(idSequence, GlossParameter, itemD.GlosaPortfolioGlosada, _AccountReceivable, IndigoSessionValues)
                            If _resultConfirm.StateResult = False Then
                                Return _resultConfirm
                            End If
                            _ObjectionsReceptionDRepositoryCommit.SaveEntity(itemD)
                            unitWorkObjectionsReceptionD.Commit()
                            scope.Complete()
                            Return _resultConfirm
                        Else
                            _ObjectionsReceptionDRepositoryCommit.SaveEntity(itemD)
                            unitWorkObjectionsReceptionD.Commit()
                            scope.Complete()
                            _resultConfirm = New ActionResult With {.StateResult = True}
                            Return _resultConfirm
                        End If

                    ElseIf IndigoSessionValues.IndigoCompanyType = eCompanyType.PublicCompany Then
                        _resultConfirm = _InterfaceNativeAdminservice.createJournalVouchersCompanyPublic(1, itemD, _AccountReceivable, GlossParameter, valueGlosa, IndigoSessionValues)
                        If _resultConfirm.StateResult = False Then
                            Return _resultConfirm
                        End If
                        _ObjectionsReceptionDRepositoryCommit.SaveEntity(itemD)
                        unitWorkObjectionsReceptionD.Commit()
                        scope.Complete()
                        Return _resultConfirm
                    End If
                End If
            End Using

        Catch ex As DbUpdateConcurrencyException
            unitWorkObjectionsReceptionD.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As OptimisticConcurrencyException
            unitWorkObjectionsReceptionD.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitWorkObjectionsReceptionD.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", IndigoSessionValues)
            Dim Mensaje As New List(Of String)
            Mensaje.Add(ex.Message)
            Return New ActionResult With {.StateResult = False, .MessageResult = Mensaje}
        End Try
    End Function
    ''' <summary>
    ''' Funcion para reclasificar el saldo de reiteraciones para empresas privadas e integracion nativa.
    ''' se genera doc. de reclasificacion llevando el saldo de la reiteraciones a la cuenta conciliada.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function BalanceReiteratedPrivateNativeCompany(ByVal _idCurrentSequense As Integer, ByVal GlossParameter As TimeParameters, ByVal portfolio As GlosaPortfolioGlosada, ByVal _AccountReceivable As AccountReceivable, IndigoSessionValues As SessionValues) As ActionResult
        Dim AccountReceivableAccountingunitOfWork As IUnitWork = _AccountReceivableAccountingRepository.UnitWork
        'reclasificacion
        Dim listStrValidateMessage As New List(Of String)
        Dim tmpclasificationResult As New ActionResult
        Dim listStrMessage As New List(Of String)
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
            .Balance = .Balance - portfolio.ValueReiterationBalance
        End With
        _AccountReceivableAccountingRepository.SaveEntity(_AccountReceivableAccountingRemedied)
        Dim _AccountReceivableAccountingConciliation As New AccountReceivableAccounting
        'generamos nueva estructura de cuenta de cobro de conciliacion
        With _AccountReceivableAccountingConciliation
            .AccountReceivableId = _AccountReceivable.Id
            .MainAccountId = _AccountReceivable.AccountConciliationId 'Id Cuenta subsanable
            .ThirdPartyId = _AccountReceivable.ThirdPartyId
            .CostCenterId = _AccountReceivable.CostCenterId
            .Value = portfolio.ValueReiterationBalance
            .Balance = portfolio.ValueReiterationBalance
        End With
        _AccountReceivableAccountingRepository.SaveEntity(_AccountReceivableAccountingConciliation)
        'creamos documento de reclasificacion
        tmpclasificationResult = _InterfaceNativeAdminservice.CreatePortfolioReclassification(TypePortfolioReclassification.ConciliationBalanceReiterated, _idCurrentSequense, _AccountReceivable, portfolio.ValueReiterationBalance, _AccountReceivable.ThirdPartyId, GlossParameter, IndigoSessionValues.AuditMessageWcf)
        If tmpclasificationResult.StateResult = False Then
            AccountReceivableAccountingunitOfWork.RollbackChanges()
            Return tmpclasificationResult
        End If
        AccountReceivableAccountingunitOfWork.Commit()
        Return tmpclasificationResult
    End Function

    ''' <summary>
    ''' Funcion Para Actualizar el estado de una factura
    ''' </summary>
    ''' <param name="ObjectionsReceptionD">objeto detalle de oficio</param>
    ''' <param name="IndigoSessionValues">Valores de sesion</param>
    ''' <returns></returns>
    Public Function ConfirmObjectionReceptionDintegration(ObjectionsReceptionD As GlosaObjectionsReceptionD, ByVal Nit As String, ByVal RadicateConsecutive As String, ByVal valueGlosa As Decimal, IndigoSessionValues As SessionValues) As ActionResult
        If ObjectionsReceptionD Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Recepción Detalle de Objeciones Vacio")
        End If
        'creo la unidad de trabajo para el detalle de la objeción
        Dim unitWorkObjectionsReceptionD As IUnitWork = TryCast(_ObjectionsReceptionDRepositoryCommit.UnitWork, IUnitWork)
        Dim unitworkPortfolioGlosada As IUnitWork = TryCast(_PortfolioGlosadaRepository.UnitWork, IUnitWork)
        Dim unitWorkInterfaces As IUnitWork = TryCast(_IInterfaceParametersRepository.UnitWork, IUnitWork)
        Dim _resultConfirm As New ActionResult
        Try
            'configuro la transaccion
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.DefaultTimeout
            txSettings.IsolationLevel = IsolationLevel.ReadCommitted
            'inicio la transaccion
            Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
                ObjectionsReceptionD.State = 2 'confirmamos
                _resultConfirm.StateResult = True
                'variable para almacenar la lista de mensaje a retornar
                Dim _Listmessage As New List(Of String)
                Dim objParameter As GlosasParametersInterface = _IInterfaceParametersRepository.GetInterfacesParametersById(ObjectionsReceptionD.GlosasParametersInterfaceId)
                If ObjectionsReceptionD.DocumentType = 1 Then
                    ObjectionsReceptionD.GlosaPortfolioGlosada.ValueGlosado = valueGlosa
                    ObjectionsReceptionD.GlosaPortfolioGlosada.BalanceGlosa = valueGlosa
                    If objParameter.Interface = True Then
                        Dim NumeroGlosa As String = RadicateConsecutive ' ObjectionsReceptionD.GlosaObjectionsReceptionC.RadicatedConsecutive
                        Dim Factura As String = ObjectionsReceptionD.InvoiceNumber
                        Dim Tercero As String = Nit 'ObjectionsReceptionD.GlosaObjectionsReceptionC.Customer.Nit.Trim
                        Dim CodEmpresaDGH As String = objParameter.ContainerName
                        Dim ValorFactura As Decimal = ObjectionsReceptionD.GlosaPortfolioGlosada.ValueGlosado
                        Dim Anio As Integer = Year(ObjectionsReceptionD.GlosaPortfolioGlosada.InvoiceDate)
                        Dim IntOpcion As String = "RADICACION"
                        Dim User As String = IndigoSessionValues.UserInterface
                        Dim result As InterfaceResult = New InterfaceResult()
                        If objParameter.AccountingMethod = eTypeInterface.FoxPrivate Then
                            result = _InterfaceFox.RadicateObjection(IndigoSessionValues.TransactionalContainer, NumeroGlosa, Factura, Tercero, CodEmpresaDGH, ValorFactura, Anio, IntOpcion, User)
                            _resultConfirm.MessageResult = New List(Of String)
                            _resultConfirm.MessageResult.Add(result.Message)
                            _resultConfirm.StateResult = result.Result
                        ElseIf objParameter.AccountingMethod = eTypeInterface.NETPrivate Then
                            result = _InterfaceNet.RadicateObjection(IndigoSessionValues.TransactionalContainer, NumeroGlosa, Factura, Tercero, CodEmpresaDGH, ValorFactura, Anio, IntOpcion, User)
                            _resultConfirm.MessageResult = New List(Of String)
                            _resultConfirm.MessageResult.Add(result.Message)
                            _resultConfirm.StateResult = result.Result
                        ElseIf objParameter.AccountingMethod = eTypeInterface.FoxPublic Then
                            Dim plancode As String = ObjectionsReceptionD.GlosaPortfolioGlosada.PlanCode
                            result = _InterfacePublicFOX.RadicateObjection(IndigoSessionValues.TransactionalContainer, NumeroGlosa, Factura, Tercero, CodEmpresaDGH, ValorFactura, Anio, IntOpcion, User, plancode)
                            _resultConfirm.MessageResult = New List(Of String)
                            _resultConfirm.MessageResult.Add(result.Message)
                            _resultConfirm.StateResult = result.Result
                        ElseIf objParameter.AccountingMethod = eTypeInterface.NETPublic Then
                            Dim plancode As String = ObjectionsReceptionD.GlosaPortfolioGlosada.PlanCode
                            result = _InterfacePublicNET.RadicateObjection(IndigoSessionValues.TransactionalContainer, NumeroGlosa, Factura, Tercero, CodEmpresaDGH, ValorFactura, Anio, IntOpcion, User, plancode)
                            _resultConfirm.MessageResult = New List(Of String)
                            _resultConfirm.MessageResult.Add(result.Message)
                            _resultConfirm.StateResult = result.Result
                        End If
                        'Si el numero de error no es 1 quiere decir que el procedimineto se excepciono o no registro movimineto contable por falta de parametros
                        If _resultConfirm.StateResult = False Then
                            unitWorkObjectionsReceptionD.RollbackChanges()
                            unitworkPortfolioGlosada.RollbackChanges()
                            Return _resultConfirm
                        Else
                            'actualizamos la cuenta de cartera glosa genesis por la del concepto de glosa subsanable 
                            If objParameter.AccountingMethod = eTypeInterface.FoxPrivate Or objParameter.AccountingMethod = eTypeInterface.NETPrivate Then  'para version FOX privada  NO PALICA PARA FOX PUBLICO
                                If result IsNot Nothing AndAlso result.Account IsNot Nothing AndAlso result.Account.ToString <> String.Empty Then
                                    ObjectionsReceptionD.GlosaPortfolioGlosada.AccountantAccountCustomers = result.Account
                                End If
                            End If
                            ObjectionsReceptionD.GlosaPortfolioGlosada.TempState = ObjectionsReceptionD.GlosaPortfolioGlosada.State
                            ObjectionsReceptionD.GlosaPortfolioGlosada.State = 2 '2- Pendiente Evaluacion Glosa
                            ' ObjectionsReceptionD.GlosaObjectionsReceptionC.MarkAsUnchanged()
                            'por control de actualizacion de entity, actualizamos usando un repositorio propio y marcando como eliminado los demas detalles de facturas
                            'For Each detail In ObjectionsReceptionD.GlosaObjectionsReceptionC.GlosaObjectionsReceptionD
                            '    If ObjectionsReceptionD.Id <> detail.Id Then
                            '        detail.MarkAsUnchanged()
                            '    End If
                            'Next
                            _ObjectionsReceptionDRepositoryCommit.SaveEntity(ObjectionsReceptionD)
                            unitWorkObjectionsReceptionD.Commit()
                        End If
                    Else
                        unitWorkObjectionsReceptionD.RollbackChanges()
                        unitworkPortfolioGlosada.RollbackChanges()
                        _Listmessage.Add("222")
                        _Listmessage.Add("No se ecuentra Activa la generación de Interfaz Contable")
                        _resultConfirm.MessageResult = _Listmessage
                        _resultConfirm.StateResult = False
                        Return _resultConfirm
                    End If

                ElseIf ObjectionsReceptionD.DocumentType = "2" Then
                    'ObjectionsReceptionD.GlosaPortfolioGlosada.ValueReiterated = valueGlosa
                    'Dim _ValueReiterationBalance As Decimal
                    'Dim _ValuePending As Decimal
                    'Dim ListDetailMov As List(Of GlosaInvoiceDetail) = _InvoiceDetailRepository.ListGlosaInvoiceDetail(ObjectionsReceptionD.InvoiceNumber)
                    'For Each item As GlosaInvoiceDetail In ListDetailMov
                    '    For Each itemmov As GlosaMovementGlosa In item.GlosaMovementGlosa
                    '        If itemmov.MainGlosa Then
                    '            _ValueReiterationBalance += IIf(itemmov.ValueReiterationBalance Is Nothing, 0, itemmov.ValueReiterationBalance)
                    '            _ValuePending += IIf(itemmov.ValuePendingConciliation Is Nothing, 0, itemmov.ValuePendingConciliation)
                    '        End If
                    '    Next
                    'Next

                    Dim unitOfWorkMovements As IUnitWork = _MovementGlosaRepository.UnitWork
                    Dim ListmpvalidateMovement As List(Of GlosaMovementGlosa) = _MovementGlosaRepository.ListMovementGlosaByInvoiceNumber(ObjectionsReceptionD.InvoiceNumber)
                    For Each itemMov As GlosaMovementGlosa In ListmpvalidateMovement
                        Dim ValuePendingConciliation As Decimal = IIf(itemMov.ValuePendingConciliation Is Nothing, 0, itemMov.ValuePendingConciliation)
                        If ValuePendingConciliation > 0 Then
                            itemMov.ValueReiterationBalance = itemMov.ValuePendingConciliation - itemMov.ValueReiterated
                            itemMov.ValuePendingConciliation = itemMov.ValueReiterated  'el valor pendiente a conciliar sera el valor reiterado  
                            If itemMov.ChangeTracker.State = ObjectState.Modified Then
                                _MovementGlosaRepository.SaveEntity(itemMov)
                            End If
                        End If
                    Next
                    unitOfWorkMovements.CommitAndRefreshChanges()

                    'totalizo los movimientos de esa factura
                    Dim TmpSaveSumValueReiteratedPortfolio As Decimal = ListmpvalidateMovement.Where(Function(c As GlosaMovementGlosa) c.MainGlosa = True).ToList().Sum(Function(c As GlosaMovementGlosa) c.ValueReiterated)
                    Dim _ValueReiterationBalance As Decimal = ListmpvalidateMovement.Where(Function(c As GlosaMovementGlosa) c.MainGlosa = True).ToList().Sum(Function(c As GlosaMovementGlosa) c.ValueReiterationBalance)
                    'actualizamos estado y saldo de cartera glosa
                    ObjectionsReceptionD.GlosaPortfolioGlosada.ValueReiterated = TmpSaveSumValueReiteratedPortfolio
                    ObjectionsReceptionD.GlosaPortfolioGlosada.ValueReiterationBalance = _ValueReiterationBalance
                    ObjectionsReceptionD.GlosaPortfolioGlosada.BalanceGlosa = TmpSaveSumValueReiteratedPortfolio
                    ObjectionsReceptionD.State = 2 'confirmamos   -  'actualizamos estado y saldo de cartera glosa
                    ObjectionsReceptionD.GlosaPortfolioGlosada.TempState = ObjectionsReceptionD.GlosaPortfolioGlosada.State
                    ObjectionsReceptionD.GlosaPortfolioGlosada.State = 5 '5-pendiente evaluacion reitreacion
                    ObjectionsReceptionD.GlosaPortfolioGlosada.StatusTotal = 0

                    Dim result As InterfaceResult = New InterfaceResult()
                    Dim NumeroGlosa As String = RadicateConsecutive ' ObjectionsReceptionD.GlosaObjectionsReceptionC.RadicatedConsecutive
                    Dim Factura As String = ObjectionsReceptionD.InvoiceNumber
                    Dim Tercero As String = Nit 'ObjectionsReceptionD.GlosaObjectionsReceptionC.Customer.Nit.Trim
                    Dim CodEmpresaDGH As String = objParameter.ContainerName
                    Dim Anio As Integer = Year(ObjectionsReceptionD.GlosaPortfolioGlosada.InvoiceDate)
                    Dim IntOpcion As String = "REITERACION"
                    Dim User As String = IndigoSessionValues.UserInterface
                    'aplica solo para el metodo privado 
                    If _ValueReiterationBalance > 0 Then
                        If objParameter.AccountingMethod = eTypeInterface.FoxPrivate Then
                            result = _InterfaceFox.BalanceReiterated(IndigoSessionValues.TransactionalContainer, NumeroGlosa, Factura, Tercero, CodEmpresaDGH, _ValueReiterationBalance, Anio, IntOpcion, User)
                            _resultConfirm.MessageResult = New List(Of String)
                            _resultConfirm.MessageResult.Add(result.Message)
                            _resultConfirm.StateResult = result.Result
                        ElseIf objParameter.AccountingMethod = eTypeInterface.NETPrivate Then
                            result = _InterfaceNet.BalanceReiterated(IndigoSessionValues.TransactionalContainer, NumeroGlosa, Factura, Tercero, CodEmpresaDGH, _ValueReiterationBalance, Anio, IntOpcion, User)
                            _resultConfirm.MessageResult = New List(Of String)
                            _resultConfirm.MessageResult.Add(result.Message)
                            _resultConfirm.StateResult = result.Result
                        End If
                    End If
                    'si es metodo publico
                    If objParameter.AccountingMethod = eTypeInterface.FoxPublic Then
                        Dim plancode As String = ObjectionsReceptionD.GlosaPortfolioGlosada.PlanCode
                        Dim ValorFactura As Decimal = ObjectionsReceptionD.GlosaPortfolioGlosada.ValueReiterated
                        result = _InterfacePublicFOX.RadicateObjection(IndigoSessionValues.TransactionalContainer, NumeroGlosa, Factura, Tercero, CodEmpresaDGH, ValorFactura, Anio, IntOpcion, User, plancode)
                        _resultConfirm.MessageResult = New List(Of String)
                        _resultConfirm.MessageResult.Add(result.Message)
                        _resultConfirm.StateResult = result.Result
                    ElseIf objParameter.AccountingMethod = eTypeInterface.NETPublic Then
                        Dim plancode As String = ObjectionsReceptionD.GlosaPortfolioGlosada.PlanCode
                        Dim ValorFactura As Decimal = ObjectionsReceptionD.GlosaPortfolioGlosada.ValueReiterated
                        result = _InterfacePublicNET.RadicateObjection(IndigoSessionValues.TransactionalContainer, NumeroGlosa, Factura, Tercero, CodEmpresaDGH, ValorFactura, Anio, IntOpcion, User, plancode)
                        _resultConfirm.MessageResult = New List(Of String)
                        _resultConfirm.MessageResult.Add(result.Message)
                        _resultConfirm.StateResult = result.Result
                    End If
                    _ObjectionsReceptionDRepositoryCommit.SaveEntity(ObjectionsReceptionD)
                    unitWorkObjectionsReceptionD.Commit()
                End If
                'confirmo la transaccion
                scope.Complete()
            End Using
            Return _resultConfirm
        Catch ex As DbUpdateConcurrencyException
            unitWorkObjectionsReceptionD.RollbackChanges()
            unitworkPortfolioGlosada.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As OptimisticConcurrencyException
            unitWorkObjectionsReceptionD.RollbackChanges()
            unitworkPortfolioGlosada.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            'descarto los cambios en la eliminacion del detalle
            unitWorkObjectionsReceptionD.RollbackChanges()
            unitworkPortfolioGlosada.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", IndigoSessionValues)
            Dim Mensaje As New List(Of String)
            Mensaje.Add(ex.Message)
            Return New ActionResult With {.StateResult = False, .MessageResult = Mensaje}
        End Try
    End Function

    ''' <summary>
    ''' Funcion Para Actualizar un Objection receptionD
    ''' </summary>
    ''' <param name="ObjectionsReceptionD">objeto detalle de oficio</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveObjectionReceptionD(ObjectionsReceptionD As GlosaObjectionsReceptionD, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult Implements IObjectionsReceptionDAdminService.SaveObjectionReceptionD
        If ObjectionsReceptionD Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Recepción Detalle de Objeciones Vacio")
        End If
        Dim AuxObjD As GlosaObjectionsReceptionD = Nothing
        'para la auditoria
        If ObjectionsReceptionD.ChangeTracker.State = ObjectState.Modified Then
            AuxObjD = ObjectionsReceptionD.OriginalValue
            AuxObjD.ChangeTracker.ChangeTrackingEnabled = False
            AuxObjD.StopTracking()
        End If
        'creo la unidad de trabajo para el detalle de la objeción
        Dim unitWorkObjectionsReceptionD As IUnitWork = TryCast(_ObjectionsReceptionDRepository.UnitWork, IUnitWork)
        Dim unitworkPortfolioGlosada As IUnitWork = TryCast(_PortfolioGlosadaRepository.UnitWork, IUnitWork)
        Try
            _ObjectionsReceptionDRepository.SaveEntity(ObjectionsReceptionD)
            'acepto los cambios de actualizacion
            unitWorkObjectionsReceptionD.Commit()
            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute("GlosaObjectionsReceptionD", audit.Functional, ObjectionsReceptionD.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
            'Auditoria ObjD
            Dim auditObject As New IndigoAuditSimpleEntity(Of GlosaObjectionsReceptionD)(ObjectionsReceptionD, audit, Infrastructure.CrossCutting.Audit.Actions.Update, AuxObjD)
            auditObject.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitWorkObjectionsReceptionD.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            'descarto los cambios en la eliminacion del detalle
            unitWorkObjectionsReceptionD.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False}
        End Try
    End Function


    ''' <summary>
    ''' Cargar saldo de factura dependiendo si es modo integracion o nativa
    ''' </summary>
    ''' <param name="ObjD"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadBalanceInvoice(ByVal ObjD As GlosaObjectionsReceptionD, ByVal session As SessionValues) As Decimal Implements IObjectionsReceptionDAdminService.LoadBalanceInvoice
        If ObjD Is Nothing Then
            Throw New ArgumentNullException("Objeto factura vacio")
        End If
        Try
            Dim Balance As Decimal = 0
            If session.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                If ObjD.GlosasParametersInterface.AccountingMethod = eTypeInterface.FoxPrivate Or ObjD.GlosasParametersInterface.AccountingMethod = eTypeInterface.FoxPublic Then
                    Balance = _InterfaceFox.LoadBalanceInvoice(ObjD.InvoiceNumber, ObjD.GlosasParametersInterface.ContainerName)
                ElseIf ObjD.GlosasParametersInterface.AccountingMethod = eTypeInterface.NETPrivate Or ObjD.GlosasParametersInterface.AccountingMethod = eTypeInterface.NETPublic Then
                    Balance = _InterfaceNet.LoadBalanceInvoice(ObjD.InvoiceNumber, ObjD.GlosasParametersInterface.ContainerName)
                Else
                    Balance = 0
                End If
            ElseIf session.IndigoGlossesIntegration = EGlossesIntegration.Native Or session.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNet Or session.IndigoGlossesIntegration = EGlossesIntegration.IntegractionFox Or session.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNATIVE_MIGRATIONS Then
                Balance = _accountReceivableRepository.LoadBalance(ObjD.InvoiceNumber)
            End If
            Return Balance
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Asignación de responsable de radicacion respuesta EAPB
    ''' </summary>
    ''' <param name="listObjetionReceptionDetail"></param>
    ''' <returns></returns>
    Public Function GlosaObjetionReceptionDetailAssignRadicateResponsible(listObjetionReceptionDetail As List(Of GlosaObjectionsReceptionD)) As ActionResult(Of List(Of GlosaObjectionsReceptionD)) Implements IObjectionsReceptionDAdminService.GlosaObjetionReceptionDetailAssignRadicateResponsible
        Try
            For Each objetionReceptionDetail In listObjetionReceptionDetail
                Dim id = objetionReceptionDetail.Id
                Dim radicateResponsibleId = objetionReceptionDetail.RadicateResponsibleId

                objetionReceptionDetail = _ObjectionsReceptionDRepository.getObjectionReceptionDByIdWithoutObjC(id)
                If objetionReceptionDetail IsNot Nothing AndAlso objetionReceptionDetail.Id > 0 Then
                    objetionReceptionDetail.RadicateResponsibleId = radicateResponsibleId
                    _ObjectionsReceptionDRepository.SaveEntity(objetionReceptionDetail)
                    _ObjectionsReceptionDRepository.UnitWork.Commit()
                End If
            Next
            Return New ActionResult(Of List(Of GlosaObjectionsReceptionD)) With {.StateResult = True, .ObjectEmbbeded = listObjetionReceptionDetail, .Message = "Responsable de radicacion respuesta EAPB asignados correctamente"}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of GlosaObjectionsReceptionD)) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Radicación de la respuesta EAPB
    ''' </summary>
    ''' <param name="listObjetionReceptionDetail"></param>
    ''' <returns></returns>
    Public Function GlosaObjetionReceptionDetailRadicate(listObjetionReceptionDetail As List(Of GlosaObjectionsReceptionD)) As ActionResult(Of List(Of GlosaObjectionsReceptionD)) Implements IObjectionsReceptionDAdminService.GlosaObjetionReceptionDetailRadicate
        Try
            For Each objetionReceptionDetail In listObjetionReceptionDetail
                Dim id = objetionReceptionDetail.Id
                Dim radicatedDate = objetionReceptionDetail.RadicatedDate
                Dim radicatedReceiver = objetionReceptionDetail.RadicatedReceiver
                Dim radicatedObservation = objetionReceptionDetail.RadicatedObservation

                objetionReceptionDetail = _ObjectionsReceptionDRepository.getObjectionReceptionDByIdWithoutObjC(id)
                If objetionReceptionDetail IsNot Nothing AndAlso objetionReceptionDetail.Id > 0 Then
                    objetionReceptionDetail.RadicatedDate = radicatedDate
                    objetionReceptionDetail.RadicatedReceiver = radicatedReceiver
                    objetionReceptionDetail.RadicatedObservation = radicatedObservation
                    _ObjectionsReceptionDRepository.SaveEntity(objetionReceptionDetail)
                    _ObjectionsReceptionDRepository.UnitWork.Commit()
                End If
            Next
            Return New ActionResult(Of List(Of GlosaObjectionsReceptionD)) With {.StateResult = True, .ObjectEmbbeded = listObjetionReceptionDetail, .Message = "Radicación de respuesta EAPB guardados correctamente"}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of GlosaObjectionsReceptionD)) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' 'Funcion para validar y crear moviminetos glosas apartir de la carga masiva de datos desde excel
    ''' </summary>
    ''' <param name="dtSet"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateExcelData(ByVal dtSet As DataSet, ByVal Session As SessionValues) As ActionResult Implements IObjectionsReceptionDAdminService.ValidateExcelData
        If dtSet Is Nothing AndAlso dtSet.Tables("Datos") Is Nothing Then
            Throw New ArgumentNullException("Dataset de datos glosa excel esta vacio")
        End If

        Dim unitOfWork As IUnitWork = Me._MovementGlosaRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim xmlObject As String = dtSet.GetXml()

                Dim resultStore = Me._MovementGlosaRepository.SP_ImportGlosaMovementGlosas(xmlObject)
                If resultStore Is Nothing OrElse resultStore.Count = 0 OrElse resultStore.Any(Function(d) d.StatusField <> 0) Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult With {.StateResult = False, .Message = "Proceso Finalizado con errores", .MessageResult = resultStore.Where(Function(d) d.StatusField <> 0).Select(Function(d) d.MessageField).ToList()}
                End If

                transaction.Complete()
                Return New ActionResult With {.StateResult = True, .Message = resultStore.FirstOrDefault().MessageField, .MessageResult = resultStore.Select(Function(d) d.MessageField).ToList()}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
                Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex), .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList()}
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
                _InterfaceFox.Dispose()
                _InterfaceNet.Dispose()
                _InterfacePublicFOX.Dispose()
                _InterfacePublicNET.Dispose()
                _InterfaceNativeAdminservice.Dispose()
            End If
            _ObjectionsReceptionDRepository = Nothing
            _ObjectionsReceptionDRepositoryCommit = Nothing
            _PortfolioGlosadaRepository = Nothing
            _InvoiceDetailRepository = Nothing
            _InvoiceDetailRepositoryQX = Nothing
            _MovementGlosaRepository = Nothing
            _ConciliationDRepository = Nothing
            _IInterfaceParametersRepository = Nothing
            _InterfaceFox = Nothing
            _InterfaceNet = Nothing
            _InterfacePublicFOX = Nothing
            _InterfacePublicNET = Nothing
            _IResponsibleRepository = Nothing
            _accountReceivableRepository = Nothing
            _careGroupRepository = Nothing
            _AccountReceivableAccountingRepository = Nothing
            _InterfaceNativeAdminservice = Nothing
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
