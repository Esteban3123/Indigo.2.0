'***********************************************************************
' Assembly         : Application.Glosas
' Author           : RafaelPatiño
' Created          : 17-06-2013
'
' Last Modified By : RafaelPatiño
' Last Modified On : 2013-06-17
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports System.Transactions
Imports Domain.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports System.Data.Entity.Core
Imports Domain.InterfaceERPGlosa
Imports Application.Portfolio
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.SqlClient

Public Class MovementGlosaAdminService
    Implements IMovementGlosaAdminService

#Region "Fields"
    Private _MovementGlosaRepository As IMovementGlosaRepository
    Private _AuxMovementGlosaRepository As IMovementGlosaRepository
    Private _PortFolioGlosaRepository As IPortfolioGlosadaRepository
    Private _IInterfaceParametersRepository As IInterfaceParametersRepository
    Private _BlockRecordRepository As IBlockRecordRepository
    Private _GlosasServiceMovementGlosas As IGlosasMovementGlosasService
    Private _InterfaceFox As IInterfaceFOX
    Private _InterfaceNet As IInterfaceNET
    Private _InterfacePublicFOX As IInterfacePublicFOX
    Private _InterfacePublicNET As IInterfacePublicNET
    ''' <summary>
    ''' Repositorio de secuencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _sequensePortfolioDRepository As ISequensePortfolioDRepository
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
    ''' Parametros de glosas
    ''' </summary>
    ''' <remarks></remarks>
    Private _ITimeGlossParametersRepository As ITimeParametersRepository
    ''' <summary>
    ''' Repositorio a terceros
    ''' </summary>
    Private _thirdParyRepository As IThirdPartyRepository
    ''' <summary>
    ''' Repositorio a conceptos de Jerarquia 
    ''' </summary>
    Private _responseHierarchyRepository As IResponseHierarchyRepository
    ''' <summary>
    ''' Repositorio a conceptos de glosas
    ''' </summary>
    Private _conceptGlosasRepository As IConceptGlosasRepository
#End Region

#Region "Build"
    ''' <summary>
    ''' Initializa una nueva instancia de la clase <see cref="IMovementGlosaRepository" />.
    ''' </summary>
    ''' <param name="MovementGlosaRepository">el repositorio para el manejo de moviminetos glosa.</param>
    Public Sub New(ByVal MovementGlosaRepository As IMovementGlosaRepository, ByVal IInterfaceParametersRepository As IInterfaceParametersRepository, ByVal AUXMovementGlosaRepository As IMovementGlosaRepository,
                   ByVal PortFolioGlosadaRepository As IPortfolioGlosadaRepository, ByVal BlockRecordRepository As IBlockRecordRepository, ByVal GlosasServiceMovementGlosas As IGlosasMovementGlosasService,
                   InterfaceFox As IInterfaceFOX, InterfaceNET As IInterfaceNET, InterfacePublicFOX As IInterfacePublicFOX, InterfacePublicNET As IInterfacePublicNET,
                   accountReceivableRepository As IAccountReceivableRepository, careGroupRepository As ICareGroupRepository, AccountReceivableAccountingRepository As IAccountReceivableAccountingRepository,
                   ByVal InterfaceNativeAdminservice As IInterfaceNativeAdminService, ByVal PortfolioSequenseAdminService As IPortfolioSequenseAdminService, ITimeGlossParametersRepository As ITimeParametersRepository,
                   ByVal thirdParyRepository As IThirdPartyRepository, ByVal responseHierarchyRepository As IResponseHierarchyRepository, ByVal conceptGlosasRepository As IConceptGlosasRepository)
        If MovementGlosaRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Moviminetos Glosa Vacío")
        End If
        If IInterfaceParametersRepository Is Nothing Then
            Throw New ArgumentException("Repositorio Interfaz Vacío")
        End If
        If AUXMovementGlosaRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Moviminetos Glosa Auxiliar Vacío")
        End If
        If PortFolioGlosadaRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Catera Glosa Vacío")
        End If
        If GlosasServiceMovementGlosas Is Nothing Then
            Throw New ArgumentNullException("Servicio de Dominio de Movimentos Glosas Vacío")
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
        If PortfolioSequenseAdminService Is Nothing Then
            Throw New ArgumentNullException("PortfolioSequenseAdminService vacio", "Servicio de secuencias vacio")
        End If
        If InterfaceNativeAdminservice Is Nothing Then
            Throw New ArgumentNullException("InterfaceNativeAdminservice vacio", "Repositorio de interface nativo es vacio")
        End If
        If ITimeGlossParametersRepository Is Nothing Then
            Throw New ArgumentNullException("Parameters  vacio", "Repositorio de Parametros es vacio")
        End If
        _MovementGlosaRepository = MovementGlosaRepository
        _IInterfaceParametersRepository = IInterfaceParametersRepository
        _AuxMovementGlosaRepository = AUXMovementGlosaRepository
        _PortFolioGlosaRepository = PortFolioGlosadaRepository
        _BlockRecordRepository = BlockRecordRepository
        _GlosasServiceMovementGlosas = GlosasServiceMovementGlosas
        _InterfaceFox = InterfaceFox
        _InterfaceNet = InterfaceNET
        _InterfacePublicFOX = InterfacePublicFOX
        _InterfacePublicNET = InterfacePublicNET
        _accountReceivableRepository = accountReceivableRepository
        _careGroupRepository = careGroupRepository
        _AccountReceivableAccountingRepository = AccountReceivableAccountingRepository
        _PortfolioSequenseAdminService = PortfolioSequenseAdminService
        _InterfaceNativeAdminservice = InterfaceNativeAdminservice
        _ITimeGlossParametersRepository = ITimeGlossParametersRepository
        _thirdParyRepository = thirdParyRepository
        _responseHierarchyRepository = responseHierarchyRepository
        _conceptGlosasRepository = conceptGlosasRepository
    End Sub
#End Region

#Region "methods"

    ''' <summary>
    ''' valida si el item tiene mov. glosas
    ''' </summary>
    ''' <param name="InvoiceDetailId"></param>
    ''' <param name="InvoiceDetailIdQx"></param>
    ''' <param name="CodeGlosa"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidMovementExist(ByVal InvoiceDetailId As Integer, ByVal InvoiceDetailIdQx As Integer, CodeGlosa As String) As Boolean
        'validamos que la glosa agregar no exista 
        Dim _GlosaMovementGlosa As Boolean
        If InvoiceDetailIdQx > 0 Then
            _GlosaMovementGlosa = _MovementGlosaRepository.GetMovementGlosa(InvoiceDetailId, InvoiceDetailIdQx, CodeGlosa)
        Else
            _GlosaMovementGlosa = _MovementGlosaRepository.GetMovementGlosa(InvoiceDetailId, 0, CodeGlosa)
        End If
        Return _GlosaMovementGlosa
    End Function
    ''' <summary>
    ''' Funcion para Guardar un Moviemineto glosa
    ''' </summary>
    ''' <param name="ObjSaveListMovementGlosa">Lista de movimientos glosa</param>
    ''' <param name="audit"></param>
    Public Function SaveMovementGlosa(ObjSaveListMovementGlosa As List(Of GlosaMovementGlosa), audit As AuditMessage) As ActionResult Implements IMovementGlosaAdminService.SaveMovementGlosa
        If ObjSaveListMovementGlosa.Count = 0 Then
            Throw New ArgumentNullException("Registro de Objeción vacío")
        End If
        Dim unitOfWork As IUnitWork = _MovementGlosaRepository.UnitWork
        Dim unitOfWorkPortFolio As IUnitWork = _PortFolioGlosaRepository.UnitWork
        Dim _ListMenssageReturn As New List(Of String)
        Try
            Dim _ActionResult As New ActionResult
            _ActionResult.StateResult = True
            Dim _ValidSave As Boolean = True
            'configuro la transaccion
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.DefaultTimeout
            txSettings.IsolationLevel = IsolationLevel.ReadCommitted
            'inicio la transaccion
            Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
                Dim SumValueGlosadoPortfolio As Decimal
                Dim ListmpvalidateMovement As List(Of GlosaMovementGlosa) = _MovementGlosaRepository.ListMovementGlosaByInvoiceNumber(ObjSaveListMovementGlosa(0).InvoiceNumber)
                For Each itemMov As GlosaMovementGlosa In ObjSaveListMovementGlosa
                    If itemMov.ChangeTracker.State = ObjectState.Added Then
                        Dim Obj As Integer = 0
                        If itemMov.InvoiceDetailIdQX IsNot Nothing Then
                            Obj = ListmpvalidateMovement.Where(Function(c) c.InvoiceDetailId = itemMov.InvoiceDetailId And c.InvoiceDetailIdQX = itemMov.InvoiceDetailIdQX And c.CodeGlosa = itemMov.CodeGlosa).ToList().Count()
                        Else
                            Obj = ListmpvalidateMovement.Where(Function(c) c.InvoiceDetailId = itemMov.InvoiceDetailId And c.CodeGlosa = itemMov.CodeGlosa).ToList().Count()
                        End If
                        If Obj > 0 Then
                            _ListMenssageReturn.Add("Ya Existe Una Glosa Para la factura " + itemMov.InvoiceNumber + " Con Codigo de Concepto " + itemMov.CodeGlosa)
                            unitOfWork.RollbackChanges()
                            _ActionResult.StateResult = False
                            _ActionResult.MessageResult = _ListMenssageReturn
                            Return _ActionResult
                        End If
                    End If
                    'Determino si el registro es nuevo, modificado o eliminado y aplico el CRUD
                    If itemMov.ChangeTracker.State = ObjectState.Modified Then
                        Dim ObjModUpdate As GlosaMovementGlosa = _MovementGlosaRepository.GetMovementGlosaById(itemMov.Id, True)
                        ObjModUpdate.ResponsibleId = If(itemMov.Responsible1?.Id Is Nothing, itemMov.ResponsibleId, itemMov.Responsible1?.Id)
                        ObjModUpdate.CodeGlosaId = If(itemMov.ConceptGlosas?.Id Is Nothing, itemMov.CodeGlosaId, itemMov.ConceptGlosas?.Id)
                        ObjModUpdate.CodeGlosa = itemMov.CodeGlosa
                        ObjModUpdate.RationaleGlosa = itemMov.RationaleGlosa
                        ObjModUpdate.ValueGlosado = itemMov.ValueGlosado
                        ObjModUpdate.ValuePendingConciliation = itemMov.ValueGlosado
                        ObjModUpdate.MainGlosa = itemMov.MainGlosa
                        _MovementGlosaRepository.SaveEntity(ObjModUpdate)
                    ElseIf itemMov.ChangeTracker.State = ObjectState.Added Then
                        itemMov.ResponsibleId = If(itemMov.Responsible1?.Id Is Nothing, itemMov.ResponsibleId, itemMov.Responsible1?.Id)
                        itemMov.Responsible1 = Nothing
                        itemMov.CodeGlosaId = If(itemMov.ConceptGlosas?.Id Is Nothing, itemMov.CodeGlosaId, itemMov.ConceptGlosas?.Id)
                        itemMov.ConceptGlosas = Nothing
                        itemMov.ValuePendingConciliation = itemMov.ValueGlosado
                        _MovementGlosaRepository.SaveEntity(itemMov)
                    End If
                Next

                'Confirmo la unidad de trabajo
                unitOfWork.CommitAndRefreshChanges()
                Dim ListmpvalidateMovementTmp As List(Of GlosaMovementGlosa) = _MovementGlosaRepository.ListMovementGlosaByInvoiceNumber(ObjSaveListMovementGlosa(0).InvoiceNumber)
                SumValueGlosadoPortfolio = ListmpvalidateMovementTmp.Where(Function(c) c.IsNormative AndAlso c.MainGlosa).ToList().Sum(Function(c As GlosaMovementGlosa) c.ValueGlosado)

                If ObjSaveListMovementGlosa.Where(Function(m) m.IsNormative).Any() Then
                    Dim Portfolio As GlosaPortfolioGlosada = _PortFolioGlosaRepository.GetPortfolioGlosada(ObjSaveListMovementGlosa(0).InvoiceNumber)
                    Portfolio.ValueGlosado = SumValueGlosadoPortfolio
                    Portfolio.BalanceGlosa = SumValueGlosadoPortfolio
                    _PortFolioGlosaRepository.SaveEntity(Portfolio)
                    unitOfWorkPortFolio.Commit()
                End If
                'confirmo la transaccion
                scope.Complete()
            End Using
            Return _ActionResult
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            unitOfWorkPortFolio.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            unitOfWorkPortFolio.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            _ListMenssageReturn.Add(Utils.GetInnerExceptionMessageToString(ex))
            Return New ActionResult With {.StateResult = False, .MessageResult = _ListMenssageReturn}
        End Try
    End Function
    ''' <summary>
    ''' Guarda de una lista de movieminetos, las reiteraciones
    ''' </summary>
    ''' <param name="ObjSaveListMovementGlosa"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveReiterationMovementGlosa(ObjSaveListMovementGlosa As List(Of GlosaMovementGlosa), audit As AuditMessage) As ActionResult Implements IMovementGlosaAdminService.SaveReiterationMovementGlosa
        If ObjSaveListMovementGlosa.Count = 0 Then
            Throw New ArgumentNullException("Registro de Objeción vacío")
        End If
        Dim unitOfWork As IUnitWork = _MovementGlosaRepository.UnitWork
        Dim unitOfWorkPortFolio As IUnitWork = _PortFolioGlosaRepository.UnitWork
        Dim listMessage As List(Of String) = New List(Of String)
        Try
            Dim _ListMenssageReturn As New List(Of String)
            Dim _ActionResult As New ActionResult
            _ActionResult.StateResult = True
            'configuro la transaccion
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.DefaultTimeout
            txSettings.IsolationLevel = IsolationLevel.ReadCommitted
            'inicio la transaccion
            Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
                For Each itemMov As GlosaMovementGlosa In ObjSaveListMovementGlosa
                    If itemMov.ChangeTracker.State = ObjectState.Modified OrElse itemMov.ChangeTracker.State = ObjectState.Added Then
                        _MovementGlosaRepository.SaveEntity(itemMov)
                    End If
                Next
                'Confirmo la unidad de trabajo
                unitOfWork.CommitAndRefreshChanges()
                'confirmo la transaccion
                scope.Complete()
            End Using
            Return _ActionResult
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            unitOfWorkPortFolio.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            unitOfWorkPortFolio.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = Nothing}
        End Try
    End Function
    ''' <summary>
    ''' Funcion para eliminar un Movimineto Glosa en este caso un Registro de Objecion
    ''' </summary>
    ''' <param name="MovementGlosa">MovimientoGLosa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteMovementGlosa(ByVal MovementGlosa As GlosaMovementGlosa, audit As AuditMessage) As Boolean Implements IMovementGlosaAdminService.DeleteMovementGlosa
        If MovementGlosa Is Nothing Then
            Throw New ArgumentNullException("Movimiento Glosa Vacio")
        End If
        Dim unitOfWork As IUnitWork = _MovementGlosaRepository.UnitWork
        Dim unitOfWorkPortFolio As IUnitWork = _PortFolioGlosaRepository.UnitWork
        Try
            Dim Result As Boolean
            If _MovementGlosaRepository.ValidDeleteMovement(MovementGlosa.InvoiceNumber) = True Then
                'configuro la transaccion
                Dim txSettings As New TransactionOptions()
                txSettings.Timeout = TransactionManager.DefaultTimeout
                txSettings.IsolationLevel = IsolationLevel.ReadCommitted
                'inicio la transaccion
                Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
                    Dim _valueglosado As Decimal = MovementGlosa.ValueGlosado
                    'elimino el registro glosa
                    _MovementGlosaRepository.DeleteEntity(MovementGlosa)
                    unitOfWork.CommitAndRefreshChanges()

                    'Actualizo el valor glosado de la cabecera
                    Dim ListmpvalidateMovementTmp As List(Of GlosaMovementGlosa) = _MovementGlosaRepository.ListMovementGlosaByInvoiceNumber(MovementGlosa.InvoiceNumber)
                    Dim SumValueGlosadoPortfolio = ListmpvalidateMovementTmp.Where(Function(c As GlosaMovementGlosa) c.MainGlosa = True).ToList().Sum(Function(c As GlosaMovementGlosa) c.ValueGlosado)
                    Dim Portfolio As GlosaPortfolioGlosada = _PortFolioGlosaRepository.GetPortfolioGlosada(MovementGlosa.InvoiceNumber)
                    Portfolio.ValueGlosado = SumValueGlosadoPortfolio
                    Portfolio.BalanceGlosa = SumValueGlosadoPortfolio
                    _PortFolioGlosaRepository.SaveEntity(Portfolio)
                    unitOfWorkPortFolio.Commit()
                    'confirmo la transaccion
                    scope.Complete()
                End Using
                Result = True
            Else
                Result = False
            End If
            Return Result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function
    ''' <summary>
    ''' Guardar lista de movimientos de evaluación
    ''' </summary>
    ''' <param name="ListMov">lista de movimientos</param>
    ''' <param name="IndigoSessionValues">valores de sesion</param> 
    ''' <returns>Boolean</returns>
    Public Function SaveMovEvaluation(ListMov As List(Of GlosaMovementGlosa), IndigoSessionValues As SessionValues, Optional ByVal _IdUnitoperating As Integer = 0) As ActionResult Implements IMovementGlosaAdminService.SaveMovEvaluation
        Dim unitOfWork As IUnitWork = _MovementGlosaRepository.UnitWork
        Dim unitWorkInterfaces As IUnitWork = TryCast(_IInterfaceParametersRepository.UnitWork, IUnitWork)
        Dim _result As New ActionResult
        'variable para almacenar la lista de mensaje a retornar
        '  Dim _Listmessage As New List(Of String)
        Try
            Dim codeResponsible As String = String.Empty
            If ListMov(0).Responsible IsNot Nothing Then
                codeResponsible = ListMov(0).Responsible.CodeUser
            ElseIf ListMov(0).Responsible1 IsNot Nothing Then
                codeResponsible = ListMov(0).Responsible1.CodeUser
            End If
            Dim _ValueAcceptedFirstInstance As Decimal
            Dim _ValueAcceptedSecondInstance As Decimal
            Dim _ValueReiterated As Decimal
            Dim _ValueGlosado As Decimal
            'configuro la transaccion
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.DefaultTimeout
            txSettings.IsolationLevel = IsolationLevel.ReadCommitted

            'inicio la transaccion
            Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
                For Each item As GlosaMovementGlosa In ListMov
                    _ValueAcceptedFirstInstance += IIf(item.ValueAcceptedFirstInstance Is Nothing, 0, item.ValueAcceptedFirstInstance)
                    _ValueAcceptedSecondInstance += IIf(item.ValueAcceptedSecondInstance Is Nothing, 0, item.ValueAcceptedSecondInstance)
                    _ValueReiterated += IIf(item.ValueReiterated Is Nothing, 0, item.ValueReiterated)
                    If item.MainGlosa = True Then
                        _ValueGlosado += item.ValueGlosado
                    End If
                    Dim tmpValueAcceptedFirstInstance As Decimal = IIf(item.ValueAcceptedFirstInstance IsNot Nothing AndAlso item.ValueAcceptedFirstInstance > 0, item.ValueAcceptedFirstInstance, 0)
                    Dim tmpValueAcceptedSecondInstance As Decimal = IIf(item.ValueAcceptedSecondInstance IsNot Nothing AndAlso item.ValueAcceptedSecondInstance > 0, item.ValueAcceptedSecondInstance, 0)
                    Dim tmpValueAcceptedIPSconciliation As Decimal = IIf(item.ValueAcceptedIPSconciliation IsNot Nothing AndAlso item.ValueAcceptedIPSconciliation > 0, item.ValueAcceptedIPSconciliation, 0)
                    Dim tmpValueAcceptedEAPBconciliation As Decimal = IIf(item.ValueAcceptedEAPBconciliation IsNot Nothing AndAlso item.ValueAcceptedEAPBconciliation > 0, item.ValueAcceptedEAPBconciliation, 0)
                    Dim tmpValueReiterated As Decimal = IIf(item.ValueReiterated IsNot Nothing AndAlso item.ValueReiterated > 0, item.ValueReiterated, 0)
                    If item.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "5" Or item.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "6" Then
                        item.ValuePendingConciliation = item.ValueReiterated - (tmpValueAcceptedSecondInstance + tmpValueAcceptedIPSconciliation + tmpValueAcceptedEAPBconciliation)
                    ElseIf item.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "2" Or item.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "3" Then
                        item.ValuePendingConciliation = item.ValueGlosado - (tmpValueAcceptedFirstInstance + tmpValueAcceptedSecondInstance + tmpValueAcceptedIPSconciliation + tmpValueAcceptedEAPBconciliation)
                    End If
                    _MovementGlosaRepository.SaveEntity(item)
                Next
                unitOfWork.CommitAndRefreshChanges()

                If Not ListMov.Any(Function(item) item.StateEvaluation <> 2) Then
                    Dim validate = _MovementGlosaRepository.ValidateInvoicesMovements(ListMov(0).InvoiceNumber)
                    'si evaluo todos los item, procedo a generar interfacez contables
                    If validate Then
                        '  Dim _TmpListmessage As New List(Of String)
                        If ListMov(0).GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "2" Or ListMov(0).GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "3" Then
                            ListMov(0).GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.TempState = ListMov(0).GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State
                            ListMov(0).GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "3"
                            ListMov(0).GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.ResponsibleEvaluationGlosa = ListMov(0).ResponsibleId
                            ListMov(0).GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.EvaluationDateGlosa = Date.Now
                            ListMov(0).GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.ValueAcceptedFirstInstance = _ValueAcceptedFirstInstance
                            ListMov(0).GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.BalanceGlosa = ListMov(0).GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.ValueGlosado - _ValueAcceptedFirstInstance
                            If ListMov(0).GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.ValueGlosado = _ValueAcceptedFirstInstance Then
                                ListMov(0).GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.StatusTotal = 1
                            End If
                            _MovementGlosaRepository.SaveEntity(ListMov(0))
                            If IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                                'Cargamos configuraciones
                                Dim objParameter As GlosasParametersInterface = _IInterfaceParametersRepository.GetInterfacesParametersById(ListMov(0).GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosasParametersInterfaceId)
                                'si se aplica interfaz para la empresa
                                If objParameter.AccountingMethod = eTypeInterface.FoxPublic Or objParameter.AccountingMethod = eTypeInterface.NETPublic Then
                                    'Ejecutamos Interfaz para el metodo publico -- Generacion de comprobantes de cuentas de Orden y Creacion de Nota Credito si Existe Aceptacion 1 Instancia -- Falta interfaz para metodo publico .Net
                                    _result = ExecuteAcceptanceInterfaceIPS(ListMov(0).GlosaInvoiceDetail.GlosaObjectionsReceptionD, IndigoSessionValues, "RA")
                                    If _result IsNot Nothing AndAlso _result.StateResult = False Then
                                        unitOfWork.RollbackChanges()
                                        scope.Dispose()
                                        Return _result
                                    Else
                                        unitOfWork.CommitAndRefreshChanges()
                                    End If
                                ElseIf objParameter.AccountingMethod = eTypeInterface.FoxPrivate Or objParameter.AccountingMethod = eTypeInterface.NETPrivate Then
                                    If _ValueAcceptedFirstInstance > 0 Then
                                        'Ejecutamos Interfaz de aceptación primera instancia Valores de Recepcion -- Nota Credito
                                        _result = ExecuteAcceptanceInterfaceIPS(ListMov(0).GlosaInvoiceDetail.GlosaObjectionsReceptionD, IndigoSessionValues, "RA")
                                        If _result IsNot Nothing AndAlso _result.StateResult = False Then
                                            unitOfWork.RollbackChanges()
                                            scope.Dispose()
                                            Return _result
                                        Else
                                            unitOfWork.CommitAndRefreshChanges()
                                        End If
                                    Else
                                        unitOfWork.CommitAndRefreshChanges()
                                    End If
                                End If
                                ' _result.MessageResult = _result.MessageResult
                            ElseIf IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.Native Or IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNet Or IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.IntegractionFox Or IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNATIVE_MIGRATIONS Then
                                Dim _ConfirmResult As New ActionResult
                                Dim _AccountReceivable As AccountReceivable = _accountReceivableRepository.GetAccountReceivableByInvoiceNumberGloss(ListMov(0).InvoiceNumber) 'consultamos factura en cartera
                                Dim GlossParameter As TimeParameters = _ITimeGlossParametersRepository.GetTimeParametersDefault(_IdUnitoperating)
                                If GlossParameter.Id = 0 Then
                                    Return New ActionResult With {.StateResult = False, .MessageResult = {"No se encontrarón parametros de glosas"}.ToList()}
                                End If
                                If IndigoSessionValues.IndigoCompanyType = eCompanyType.PrivateCompany Then
                                    If ListMov(0).GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.ValueAcceptedFirstInstance > 0 Then
                                        _ConfirmResult = Me.ExecuteNativePrivate(ETypeAcceptedIPSModule.AcceptanceGlossProcessed, ListMov, ListMov(0).GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada, _AccountReceivable, _IdUnitoperating, GlossParameter, IndigoSessionValues)
                                        If _ConfirmResult.StateResult = False Then
                                            Return _ConfirmResult
                                        End If
                                    End If
                                ElseIf IndigoSessionValues.IndigoCompanyType = eCompanyType.PublicCompany Then
                                    _ConfirmResult = Me.ExecuteNativePublic(ETypeAcceptedIPSModule.AcceptanceGlossProcessed, EtypeDocumentGloss.Gloss, ListMov, ListMov(0).GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada, ListMov(0).GlosaInvoiceDetail.GlosaObjectionsReceptionD, _AccountReceivable, _IdUnitoperating, GlossParameter, IndigoSessionValues)
                                    If _ConfirmResult.StateResult = False Then
                                        Return _ConfirmResult
                                    End If
                                End If
                                unitOfWork.CommitAndRefreshChanges()
                                _result.MessageResult = _ConfirmResult.MessageResult
                            End If
                        End If

                        '''''''''''''''  Logica de Aceptacion de Reiteraciones   ''''''''''''''''''''''''''''''''
                        If ListMov(0).GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "5" Or ListMov(0).GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "6" Then
                            Dim _balanceEAPB As Decimal = _ValueReiterated - _ValueAcceptedSecondInstance 'actualizamops saldo 
                            ListMov(0).GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.TempState = ListMov(0).GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State
                            ListMov(0).GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "6"
                            ListMov(0).GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.ResponsibleEvaluationReiteration = ListMov(0).ResponsibleReiterationId
                            ListMov(0).GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.EvaluationDateReiteration = Date.Now
                            ListMov(0).GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.BalanceGlosa = _balanceEAPB
                            ListMov(0).GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.ValueAcceptedSecondInstance = _ValueAcceptedSecondInstance
                            Dim sumGlosa = _ValueGlosado - _ValueAcceptedFirstInstance
                            If sumGlosa = _ValueAcceptedSecondInstance Then
                                ListMov(0).GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.StatusTotal = 1
                            End If
                            _MovementGlosaRepository.SaveEntity(ListMov(0))
                            If IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                                'Ejecucuion de Interfaces
                                Dim objParameter As GlosasParametersInterface = _IInterfaceParametersRepository.GetInterfacesParametersById(ListMov(0).GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosasParametersInterfaceId)
                                If objParameter.AccountingMethod = eTypeInterface.FoxPublic Or objParameter.AccountingMethod = eTypeInterface.NETPublic Then
                                    'Ejecutamos Interfaz para el metodo publico -- Generacion de comprobantes de cuentas de Orden y Creacion de Nota Credito si Existe Aceptacion 1 Instancia -- Falta interfaz para metodo publico .Net
                                    _result = ExecuteAcceptanceInterfaceIPS(ListMov(0).GlosaInvoiceDetail.GlosaObjectionsReceptionD, IndigoSessionValues, "RE")
                                    If _result IsNot Nothing AndAlso _result.StateResult = False Then
                                        unitOfWork.RollbackChanges()
                                        Return _result
                                    Else
                                        unitOfWork.CommitAndRefreshChanges()
                                    End If
                                ElseIf objParameter.AccountingMethod = eTypeInterface.FoxPrivate Or objParameter.AccountingMethod = eTypeInterface.NETPrivate Then
                                    'Ejecutamos Interfaz de aceptación segunda instancia Valores de Recepcion -- Nota Credito
                                    If _ValueAcceptedSecondInstance > 0 Then
                                        _result = ExecuteAcceptanceInterfaceIPS(ListMov(0).GlosaInvoiceDetail.GlosaObjectionsReceptionD, IndigoSessionValues, "RE")
                                        'Si el numero de error no es 1 quiere decir que el procedimineto se excepciono o no registro movimineto contable por falta de parametros
                                        If _result IsNot Nothing AndAlso _result.StateResult = False Then
                                            unitOfWork.RollbackChanges()
                                            Return _result
                                        Else
                                            unitOfWork.CommitAndRefreshChanges()
                                        End If
                                    Else
                                        unitOfWork.CommitAndRefreshChanges()
                                    End If
                                End If
                                ' _result.MessageResult = _result.MessageResult
                            ElseIf IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.Native Or IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNet Or IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.IntegractionFox Or IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNATIVE_MIGRATIONS Then
                                Dim _ConfirmResult As New ActionResult
                                Dim _AccountReceivable As AccountReceivable = _accountReceivableRepository.GetAccountReceivableByInvoiceNumberGloss(ListMov(0).InvoiceNumber) 'consultamos factura en cartera
                                Dim GlossParameter As TimeParameters = _ITimeGlossParametersRepository.GetTimeParametersDefault(_IdUnitoperating)
                                If GlossParameter.Id = 0 Then
                                    Return New ActionResult With {.StateResult = False, .MessageResult = {"No se encontrarón parametros de glosas"}.ToList()}
                                End If
                                If IndigoSessionValues.IndigoCompanyType = eCompanyType.PrivateCompany Then
                                    If ListMov(0).GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.ValueAcceptedSecondInstance > 0 Then
                                        _ConfirmResult = Me.ExecuteNativePrivate(ETypeAcceptedIPSModule.AcceptanceReiterationProcessed, ListMov, ListMov(0).GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada, _AccountReceivable, _IdUnitoperating, GlossParameter, IndigoSessionValues)
                                        If _ConfirmResult.StateResult = False Then
                                            Return _ConfirmResult
                                        End If
                                    End If
                                ElseIf IndigoSessionValues.IndigoCompanyType = eCompanyType.PublicCompany Then
                                    _ConfirmResult = Me.ExecuteNativePublic(ETypeAcceptedIPSModule.AcceptanceReiterationProcessed, EtypeDocumentGloss.Reiteration, ListMov, ListMov(0).GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada, ListMov(0).GlosaInvoiceDetail.GlosaObjectionsReceptionD, _AccountReceivable, _IdUnitoperating, GlossParameter, IndigoSessionValues)
                                    If _ConfirmResult.StateResult = False Then
                                        Return _ConfirmResult
                                    End If
                                End If
                                unitOfWork.CommitAndRefreshChanges()
                                _result.MessageResult = _ConfirmResult.MessageResult
                            End If
                        End If
                    Else
                        Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String) From {"Existen movimientos sin confirmar que no permitan confirmar una factura"}}
                    End If
                End If

                scope.Complete()
            End Using
            _result.StateResult = True
            Return _result
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", IndigoSessionValues)
            Dim Mensaje As New List(Of String)
            Mensaje.Add(ex.Message)
            Return New ActionResult With {.StateResult = False, .MessageResult = Mensaje}
        End Try
    End Function
    ''' <summary>
    ''' Empresa Publica  =  Creamos Nota credito por lo aceptado en la IPS --- y comprobante contable de reversion de cuentas de orden si no se realizo desde el tramite de glosa y/o reiteracion
    ''' </summary>
    ''' <param name="portfolio"></param>
    ''' <param name="itemD"></param>
    ''' <param name="_AccountReceivable"></param>
    ''' <param name="_IdUnitoperating"></param>
    ''' <param name="IndigoSessionValues"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ExecuteNativePublic(ETypeAcceptedIPSModule As ETypeAcceptedIPSModule, EtypeDocumentGloss As EtypeDocumentGloss, listMov As List(Of GlosaMovementGlosa), portfolio As GlosaPortfolioGlosada, itemD As GlosaObjectionsReceptionD, _AccountReceivable As AccountReceivable, _IdUnitoperating As Integer, GlossParameter As TimeParameters, IndigoSessionValues As SessionValues) As ActionResult
        Dim _ConfirmResult As New ActionResult
        Dim listStrMessage As New List(Of String)
        Try
            Dim tmpCreditResul As New ActionResult
            Dim tmpVoucherResul As New ActionResult

            Dim FlagNote As Boolean = False
            If ETypeAcceptedIPSModule = Infrastructure.CrossCutting.Base.ETypeAcceptedIPSModule.AcceptanceGlossProcessed Then
                If portfolio.ValueAcceptedFirstInstance > 0 Then
                    FlagNote = True
                End If
            ElseIf ETypeAcceptedIPSModule = Infrastructure.CrossCutting.Base.ETypeAcceptedIPSModule.AcceptanceReiterationProcessed Then
                If portfolio.ValueAcceptedSecondInstance > 0 Then
                    FlagNote = True
                End If
            ElseIf ETypeAcceptedIPSModule = Infrastructure.CrossCutting.Base.ETypeAcceptedIPSModule.AcceptanceConciliationProcessed Then
                If portfolio.ValueAcceptedIPSconciliation > 0 Then
                    FlagNote = True
                End If
            End If
            'hacemos nota credito
            If FlagNote = True Then
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
                tmpCreditResul = _InterfaceNativeAdminservice.createCreditNote(ETypeAcceptedIPSModule, portfolio, _AccountReceivable, GlossParameter, _sequense.PortfolioSequenceDetail(0).Id, _IdUnitoperating, IndigoSessionValues)
                If tmpCreditResul.StateResult = True Then
                    For Each Item As String In tmpCreditResul.MessageResult
                        listStrMessage.Add(Item)
                    Next
                Else
                    Return tmpCreditResul
                End If
                'logica de descuento de honorarios medicos
                'Dim Objparameters As TimeParameters = _Parameters.GetTimeParametersDefault()
                'Dim tmpFeesMedicalResult As ActionResult = Nothing
                'If Objparameters IsNot Nothing Then
                '    If Objparameters.DiscountedMedicalFees = True Then
                '        tmpFeesMedicalResult = _InterfaceNativeAdminservice.SaveMedicalFeesCausation(ETypeAcceptedIPSModule, listMov, IndigoSessionValues.AuditMessageWcf)
                '        If tmpFeesMedicalResult.StateResult = False Then
                '            Return tmpFeesMedicalResult
                '        End If
                '    End If
                'End If
            End If
            'general vouceher
            Dim valueGlosa As Decimal
            If EtypeDocumentGloss = Infrastructure.CrossCutting.Base.EtypeDocumentGloss.Gloss Then
                valueGlosa = portfolio.ValueGlosado
            ElseIf EtypeDocumentGloss = Infrastructure.CrossCutting.Base.EtypeDocumentGloss.Reiteration Then
                valueGlosa = portfolio.ValueReiterated
            End If
            tmpVoucherResul = _InterfaceNativeAdminservice.createJournalVouchersCompanyPublic(2, itemD, _AccountReceivable, GlossParameter, valueGlosa, IndigoSessionValues)
            If tmpVoucherResul.StateResult = True Then
                For Each Item As String In tmpVoucherResul.MessageResult
                    listStrMessage.Add(Item)
                Next
            Else
                Return tmpVoucherResul
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
    ''' Empresa Privada  = Creamos Nota Credito por lo aceptado por la IPS 
    ''' </summary>
    ''' <param name="portfolio"></param>
    ''' <param name="_AccountReceivable"></param>
    ''' <param name="_IdUnitoperating"></param>
    ''' <param name="IndigoSessionValues"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ExecuteNativePrivate(ETypeAcceptedIPSModule As ETypeAcceptedIPSModule, listMov As List(Of GlosaMovementGlosa), portfolio As GlosaPortfolioGlosada, _AccountReceivable As AccountReceivable, _IdUnitoperating As Integer, GlossParameter As TimeParameters, IndigoSessionValues As SessionValues) As ActionResult
        Dim _ConfirmResult As New ActionResult
        Dim listStrMessage As New List(Of String)
        Try
            Dim tmpCreditResul As New ActionResult
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
            tmpCreditResul = _InterfaceNativeAdminservice.createCreditNote(ETypeAcceptedIPSModule, portfolio, _AccountReceivable, GlossParameter, _sequense.PortfolioSequenceDetail(0).Id, _IdUnitoperating, IndigoSessionValues)
            If tmpCreditResul.StateResult = True Then
                For Each Item As String In tmpCreditResul.MessageResult
                    listStrMessage.Add(Item)
                Next
            Else
                Return tmpCreditResul
            End If
            'logica de descuento de honorarios medicos
            'Dim Objparameters As TimeParameters = _Parameters.GetTimeParametersDefault()
            'Dim tmpFeesMedicalResult As ActionResult = Nothing
            'If Objparameters IsNot Nothing Then
            '    If Objparameters.DiscountedMedicalFees = True Then
            '        tmpFeesMedicalResult = _InterfaceNativeAdminservice.SaveMedicalFeesCausation(ETypeAcceptedIPSModule, listMov, IndigoSessionValues.AuditMessageWcf)
            '        If tmpFeesMedicalResult.StateResult = False Then
            '            Return tmpFeesMedicalResult
            '        End If
            '    End If
            'End If
            _ConfirmResult.StateResult = True
            _ConfirmResult.MessageResult = listStrMessage
            Return _ConfirmResult
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", IndigoSessionValues)
            Return New ActionResult With {.StateResult = False, .MessageResult = Utils.GetInnerExceptionMessages(ex)}
        End Try
    End Function
    ''' <summary>
    ''' Metodo para la ejecucion de SP que realiza la interfaz  Aceptacion por parte de la IPS tanto para Aceptacion 
    ''' </summary>
    ''' <param name="ObjectionsReceptionD">Objecto Factura que encapsula los datos para enviar como parametro del SP</param>
    ''' <param name="IndigoSessionValues">valores de sesion</param>
    ''' <returns>Lista de Mensaje del SP, un Codigo y un Mensaje </returns>
    ''' <remarks></remarks>
    Private Function ExecuteAcceptanceInterfaceIPS(ByVal ObjectionsReceptionD As GlosaObjectionsReceptionD, ByVal IndigoSessionValues As SessionValues, ByVal ModuloOpcion As String) As ActionResult
        'variable para almacenar la lista de mensaje a retornar
        '' Dim _Listmessage As New List(Of String)
        Dim _Actionresult As New ActionResult
        _Actionresult.MessageResult = New List(Of String)
        'Cargamos configuraciones
        Dim objParameter As GlosasParametersInterface = _IInterfaceParametersRepository.GetInterfacesParametersById(ObjectionsReceptionD.GlosasParametersInterfaceId)
        'si se aplica interfaz para la empresa
        If objParameter.Interface = True Then
            Dim NumeroGlosa As String = ObjectionsReceptionD.GlosaObjectionsReceptionC.RadicatedConsecutive
            Dim Factura As String = ObjectionsReceptionD.InvoiceNumber
            Dim Tercero As String = ObjectionsReceptionD.GlosaObjectionsReceptionC.Customer.Nit.Trim
            Dim CodEmpresaDGH As String = objParameter.ContainerName
            Dim ValorFactura As Decimal '= ObjectionsReceptionD.GlosaPortfolioGlosada.InvoiceValueEntity
            Dim Anio As Integer
            Anio = Year(ObjectionsReceptionD.GlosaPortfolioGlosada.InvoiceDate)
            Dim IntOpcion As String = "COORDINACION"
            Dim User As String = IndigoSessionValues.UserInterface
            Dim AfectaServicio As Integer
            If objParameter.AffectsService = True Then
                AfectaServicio = 1
            Else
                AfectaServicio = 0
            End If
            If ModuloOpcion = "RA" Then
                ValorFactura = ObjectionsReceptionD.GlosaPortfolioGlosada.ValueAcceptedFirstInstance
            ElseIf ModuloOpcion = "RE" Then
                ValorFactura = ObjectionsReceptionD.GlosaPortfolioGlosada.ValueAcceptedSecondInstance
            End If
            'bandera para q tome como aceptación el valor de la radicacion - ValueAcceptedFirstInstance
            Dim Modulo As String = ModuloOpcion
            Dim result As InterfaceResult
            If objParameter.AccountingMethod = eTypeInterface.FoxPrivate Then
                result = _InterfaceFox.AcceptanceIPS(IndigoSessionValues.TransactionalContainer, NumeroGlosa, Factura, Tercero, CodEmpresaDGH, ValorFactura, Anio, IntOpcion, User, AfectaServicio, Modulo)
                _Actionresult.StateResult = result.Result
                _Actionresult.MessageResult.Add(result.Message)
            ElseIf objParameter.AccountingMethod = eTypeInterface.NETPrivate Then
                result = _InterfaceNet.AcceptanceIPS(IndigoSessionValues.TransactionalContainer, NumeroGlosa, Factura, Tercero, CodEmpresaDGH, ValorFactura, Anio, IntOpcion, User, AfectaServicio, Modulo)
                _Actionresult.StateResult = result.Result
                _Actionresult.MessageResult.Add(result.Message)
            ElseIf objParameter.AccountingMethod = eTypeInterface.FoxPublic Then
                Dim plancode As String = ObjectionsReceptionD.GlosaPortfolioGlosada.PlanCode
                Dim Valorglosado_or_Reiterado As Decimal = ObjectionsReceptionD.GlosaPortfolioGlosada.ValueGlosado
                If ModuloOpcion = "RA" Then
                    Valorglosado_or_Reiterado = ObjectionsReceptionD.GlosaPortfolioGlosada.ValueGlosado
                ElseIf ModuloOpcion = "RE" Then
                    Valorglosado_or_Reiterado = ObjectionsReceptionD.GlosaPortfolioGlosada.ValueReiterated
                End If
                'si la IPS no acepta nada generamos solo NOTA contable
                If ValorFactura = 0 Then
                    result = _InterfacePublicFOX.AceptacionEAPBTotal(IndigoSessionValues.TransactionalContainer, NumeroGlosa, Factura, Tercero, CodEmpresaDGH, Valorglosado_or_Reiterado, Anio, IntOpcion, User, plancode)
                Else
                    Dim StatePortfolioDGH As String
                    If ObjectionsReceptionD.GlosaPortfolioGlosada.ValueGlosado = ValorFactura Then
                        StatePortfolioDGH = 4 'si la IPS aceptado todo el valor glosado, actualizamos el estado en cartera como 4 - Aceptado Total IPS
                    Else
                        StatePortfolioDGH = 3 'de lo contrario la dejamos como glosa recepcionada
                    End If
                    result = _InterfacePublicFOX.AcceptanceIPS(IndigoSessionValues.TransactionalContainer, NumeroGlosa, Factura, Tercero, CodEmpresaDGH, ValorFactura, Anio, IntOpcion, User, AfectaServicio, plancode, Valorglosado_or_Reiterado, StatePortfolioDGH)
                End If
                _Actionresult.StateResult = result.Result
                _Actionresult.MessageResult.Add(result.Message)
            ElseIf objParameter.AccountingMethod = eTypeInterface.NETPublic Then
                Dim plancode As String = ObjectionsReceptionD.GlosaPortfolioGlosada.PlanCode
                Dim Valorglosado_or_Reiterado As Decimal = ObjectionsReceptionD.GlosaPortfolioGlosada.ValueGlosado
                If ModuloOpcion = "RA" Then
                    Valorglosado_or_Reiterado = ObjectionsReceptionD.GlosaPortfolioGlosada.ValueGlosado
                ElseIf ModuloOpcion = "RE" Then
                    Valorglosado_or_Reiterado = ObjectionsReceptionD.GlosaPortfolioGlosada.ValueReiterated
                End If
                'si la IPS no acepta nada generamos solo NOTA contable
                If ValorFactura = 0 Then
                    result = _InterfacePublicNET.AceptacionEAPBTotal(IndigoSessionValues.TransactionalContainer, NumeroGlosa, Factura, Tercero, CodEmpresaDGH, Valorglosado_or_Reiterado, Anio, IntOpcion, User, plancode)
                Else
                    Dim StatePortfolioDGH As String
                    If ObjectionsReceptionD.GlosaPortfolioGlosada.ValueGlosado = ValorFactura Then
                        StatePortfolioDGH = 4 'si la IPS aceptado todo el valor glosado, actualizamos el estado en cartera como 4 - Aceptado Total IPS
                    Else
                        StatePortfolioDGH = 3 'de lo contrario la dejamos como glosa recepcionada
                    End If
                    result = _InterfacePublicNET.AcceptanceIPS(IndigoSessionValues.TransactionalContainer, NumeroGlosa, Factura, Tercero, CodEmpresaDGH, ValorFactura, Anio, IntOpcion, User, AfectaServicio, plancode, Valorglosado_or_Reiterado, StatePortfolioDGH)
                End If
                _Actionresult.StateResult = result.Result
                _Actionresult.MessageResult.Add(result.Message)
            End If
        Else
            _Actionresult.StateResult = False
            _Actionresult.MessageResult.Add("No esta activa la configuración de interfaz contable para la empresa " & objParameter.CompanyName)
        End If
        Return _Actionresult
    End Function
    ''' <summary>
    ''' Obtiene una lista de movimientos según Numero de factura y Código Responsable
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero Factura</param>
    ''' <param name="CodeResponsible">Código Responsable</param>
    ''' <returns>Lista de Movimientos</returns>
    Public Function ListMovementsByInvoiceAndResponsible(InvoiceNumber As String, Optional CodeResponsible As String = "") As List(Of GlosaMovementGlosa) Implements IMovementGlosaAdminService.ListMovementsByInvoiceAndResponsible
        If String.IsNullOrEmpty(InvoiceNumber) Then
            Throw New ArgumentNullException("Numero de factura vacía")
        End If
        Try
            Return _MovementGlosaRepository.ListMovementsByInvoiceAndResponsible(InvoiceNumber, CodeResponsible)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Funcion que carga una lista de movimientos de glosa
    ''' </summary>
    ''' <param name="InvoiceDetailId">codigo del detalle de factura</param>
    ''' <returns>lista de moviminetos de glosa</returns>
    Public Function ListMovementGlosa(InvoiceDetailId As String) As List(Of GlosaMovementGlosa) Implements IMovementGlosaAdminService.ListMovementGlosa
        Try
            Return _MovementGlosaRepository.ListMovementGlosa(InvoiceDetailId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Funcion para guadar un movimiento glosa
    ''' </summary>
    ''' <param name="MovementGlosa">MovimientoGLosa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateMovementReiteration(ByVal MovementGlosa As GlosaMovementGlosa, audit As AuditMessage) As ActionResult Implements IMovementGlosaAdminService.UpdateMovementReiteration
        If MovementGlosa Is Nothing Then
            Throw New ArgumentNullException("Movimiento Glosa Vacio")
        End If
        Dim AuxMovAudit As GlosaMovementGlosa = Nothing
        'para la auditoria
        If MovementGlosa.ChangeTracker.State = ObjectState.Modified Then
            AuxMovAudit = _MovementGlosaRepository.GetMovementGlosaById(MovementGlosa.Id, False)
            AuxMovAudit.ChangeTracker.ChangeTrackingEnabled = False
            AuxMovAudit.StopTracking()
        End If
        Dim unitOfWork As IUnitWork = _MovementGlosaRepository.UnitWork
        Dim _ActionResult As New ActionResult()
        Try
            If _MovementGlosaRepository.ValidateDeleteReiteration(MovementGlosa.InvoiceNumber) = True Then
                MovementGlosa.ValuePendingConciliation = MovementGlosa.ValueReiterated + IIf(MovementGlosa.ValueReiterationBalance Is Nothing, 0, MovementGlosa.ValueReiterationBalance)
                MovementGlosa.ValueReiterationBalance = Nothing
                MovementGlosa.ValueReiterated = Nothing
                MovementGlosa.RationaleReiteration = Nothing
                MovementGlosa.RationaleDateReiteration = Nothing
                MovementGlosa.ResponsibleReiterationId = Nothing
                Dim AuxState = MovementGlosa.State
                MovementGlosa.State = MovementGlosa.TempState
                MovementGlosa.TempState = AuxState
                _MovementGlosaRepository.SaveEntity(MovementGlosa)
                unitOfWork.CommitAndRefreshChanges()
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute("GlosaMovementGlosa", audit.Functional, MovementGlosa.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                _ActionResult.StateResult = True
            Else
                _ActionResult.StateResult = False
            End If
            '/***** Auditoria Avanzada *****/
            If MovementGlosa.ChangeTracker.State = ObjectState.Modified Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of GlosaMovementGlosa)(MovementGlosa, audit, Infrastructure.CrossCutting.Audit.Actions.Update, AuxMovAudit)
                auditObject.Execute()
            End If
            Return _ActionResult
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = Nothing}
        End Try
    End Function
    ''' <summary>
    ''' Funcion que lista los movimineto glosa por el codigo de detalle de factura
    ''' </summary>
    ''' <param name="InvoiceDetailId">codigo detalle de factura</param>
    '''   <param name="InvoiceDetailQXId">codigo detalle de factura QX</param>
    ''' <returns>lista de moviminetos</returns>
    Public Function ListMovementGlosaQx(InvoiceDetailId As String, InvoiceDetailQXId As String) As List(Of GlosaMovementGlosa) Implements IMovementGlosaAdminService.ListMovementGlosaQx
        Try
            Return _MovementGlosaRepository.ListMovementGlosaQx(InvoiceDetailId, InvoiceDetailQXId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Funcion que lista los movimiento glosa por el codigo de detalle de factura
    ''' </summary>
    ''' <param name="InvoiceDetailId">codigo detalle de factura</param>
    '''   <param name="InvoiceDetailQXId">codigo detalle de factura QX</param>
    ''' <returns>lista de movimientos</returns>
    Public Function ListMovementGlosaByCodes(InvoiceDetailId As String, InvoiceDetailQXId As String) As List(Of GlosaMovementGlosa) Implements IMovementGlosaAdminService.ListMovementGlosaByCodes
        If String.IsNullOrEmpty(InvoiceDetailId) = True Then
            Throw New ArgumentNullException("Id del detalle de factura vacío")
        End If
        If String.IsNullOrEmpty(InvoiceDetailQXId) = True Then
            Throw New ArgumentNullException("Id del detalle de factura QX vacío")
        End If
        Try
            Return _MovementGlosaRepository.ListMovementGlosaByCodes(InvoiceDetailId, InvoiceDetailQXId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Función para transferir responsables en los movimientos de glosas
    ''' </summary>
    ''' <param name="listResponsiblesMovements">Lista de Movimientos por Responsables</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <param name="opt">Opción 1:Responsables 2:Conceptos</param>
    ''' <returns>ActionResult</returns>
    Public Function TransferResponsibleMovements(listResponsiblesMovements As List(Of ResponsibleMovements), audit As AuditMessage, opt As Integer) As ActionResult Implements IMovementGlosaAdminService.TransferResponsibleMovements
        If Not listResponsiblesMovements.Count > 0 Then
            Throw New ArgumentNullException("Lista de Movimientos por Responsables vacia")
        End If
        Dim unitOfWork As IUnitWork = _MovementGlosaRepository.UnitWork
        Try
            Dim listMovementsGlosa = Me._GlosasServiceMovementGlosas.TransferResponsibleMovements(listResponsiblesMovements, opt)
            For Each item As GlosaMovementGlosa In listMovementsGlosa.ObjectEmbbeded
                _MovementGlosaRepository.SaveEntity(item)
            Next
            unitOfWork.CommitAndRefreshChanges()
            Return New ActionResult With {.MessageResult = listMovementsGlosa.MessageResult, .StateResult = True}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False}
        End Try
    End Function
    ''' <summary>
    ''' Funcion para listar movimientos de glosas para las evaluaciones masivas a nivel de facturas
    ''' </summary>
    ''' <param name="ListInvoice">Lista de Facturas</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllMovementGlosabymultipleInvoice(ListInvoice As List(Of String), Optional ByVal codeUser As String = "") As List(Of GlosaMovementGlosa) Implements IMovementGlosaAdminService.ListAllMovementGlosabymultipleInvoice
        If ListInvoice.Count = 0 Then
            Throw New ArgumentNullException("Lista de facturas vacía")
        End If
        Try
            Return _MovementGlosaRepository.ListAllMovementGlosabymultipleInvoice(ListInvoice, codeUser)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Genera la estructura de un oficio desde coordinación
    ''' </summary>
    ''' <param name="IdReciptionObjection"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function ExportCoordinationGlosa(IdReciptionObjection As Integer, session As SessionValues) As DataSet Implements IMovementGlosaAdminService.ExportCoordinationGlosa
        Try
            Dim ds As New DataSet
            Dim query As String = $"SELECT [GlosaObjectionsReceptionCId], [InvoiceNumber], [MovementId], [InvoiceDate], [RadicatedNumber], [RadicatedDate],
                                          [ObjectionRadicatedConsecutive], [ObjectionRadicatedDate], [PatientName], [IngressNumber], [IngressDate],
                                          [ServiceCode], [ServiceName], [ServiceDate], [ServiceCodeQX], [ServiceNameQX], [CostCenterCode], [CostCenterName],
                                          [CodeGlosa], [NameSpecific], [RationaleGlosa], [ValueGlosado], [ValuePayments], [ResponsableName]
                                   FROM [Glosas].[ViewCoordinationGlosaExport]
                                   WHERE [GlosaObjectionsReceptionCId] = @filter"
            Dim dt = Me.GetDatatable(query, IdReciptionObjection, session, "DTCoordinationGlosaExport")
            ds.Tables.Add(dt.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Carga masiva desde coordinación
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function ChargueExcelDataCoordination(dt As DataSet, session As SessionValues) As ActionResult Implements IMovementGlosaAdminService.ChargueExcelDataCoordination
        Try
            If dt Is Nothing AndAlso dt.Tables(0) Is Nothing Then
                Return New ActionResult With {.StateResult = False, .Message = "Dataset vacio"}
            End If
            Dim Dtable As DataTable = dt.Tables(0)
            Dim listMessages As New List(Of String)
            Dim listIdMovement As New List(Of Integer)
            '**** Validaciones
            Dim i As Integer = 1
            Dim msg As String = "Factura: {0}, Servicio: {1} - {2}"

            Dim queryGlosaConcepts = (From row In Dtable.Rows
                                      Where Not String.IsNullOrEmpty(row.Item("Código Concepto Respuesta").ToString())
                                      Select Convert.ToString(row.Item("Código Concepto Respuesta")))?.ToList()

            If queryGlosaConcepts Is Nothing OrElse Not queryGlosaConcepts?.Any() Then
                Return New ActionResult With {.StateResult = False, .Message = "No se ha diligenciado ningún Código Concepto Respuesta"}
            End If

            Dim listConceptGlosa = _conceptGlosasRepository.Query(Function(q) queryGlosaConcepts.ToList().Contains(q.Code), False)?.ToList()

            If listConceptGlosa Is Nothing OrElse Not listConceptGlosa?.Any() Then
                Return New ActionResult With {.StateResult = False, .Message = "Los código concepto respuesta No son validos"}
            End If

            For Each row As DataRow In Dtable.Rows
                'UnitWorks
                Dim glosaMovementGlosaUnitWork = _MovementGlosaRepository.UnitWork

                Dim IdThirdRespnsible As Integer? = Nothing
                Dim IdResponseHierarchy As Integer? = Nothing
                Dim errors As String = String.Empty
                Dim conceptGlosa As ConceptGlosas = Nothing

                If Not String.IsNullOrEmpty(row.Item("Código Concepto Respuesta")) Then
                    Dim codeGlosa = row.Item("Código Concepto Respuesta").ToString()
                    conceptGlosa = listConceptGlosa.Find(Function(q) q.Code = codeGlosa)
                    If conceptGlosa Is Nothing Then
                        listMessages.Add(String.Format(msg, row.Item("Factura"), row.Item("Código Servicio"), "No existe ese concepto de glosa"))
                        Continue For
                    End If
                Else
                    Continue For
                End If

                If String.IsNullOrEmpty(row.Item("Justificación Glosa")) Then
                    listMessages.Add(String.Format(msg, row.Item("Factura"), row.Item("Código Servicio"), "Debe diligenciar una Justificación de Glosa"))
                    Continue For
                End If

                If Not String.IsNullOrEmpty(row.Item("Causante de Glosa")) Then
                    Dim Third = _thirdParyRepository.GetThirdPartyByNit(row.Item("Causante de Glosa"), False)
                    IdThirdRespnsible = Third?.Id
                End If

                If Not String.IsNullOrEmpty(row.Item("Jerarquia de Aceptación")) Then
                    Dim responseHierarchyCode = row.Item("Jerarquia de Aceptación").ToString()
                    Dim ResponseHierarchy = _responseHierarchyRepository.Query(Function(q) q.Code = responseHierarchyCode, False).FirstOrDefault()
                    IdResponseHierarchy = ResponseHierarchy?.Id
                End If

                Dim IdMovement As Integer = Utils.ConvertToInt(row.Item("CodigoMovimiento"))
                Dim valueGlosado As Decimal = Utils.ConvertToDecimal(row.Item("Valor Glosa"))
                Dim valueAcceptIPS As Decimal = Utils.ConvertToDecimal(row.Item("Valor Aceptado"))

                If IdMovement = 0 Then
                    listMessages.Add(String.Format(msg, row.Item("Factura"), row.Item("Código Servicio"), "No se encontro el código del movimiento en el archivo."))
                    Continue For
                End If
                Dim gmg = _MovementGlosaRepository.GetMovementGlosaByIdWithAggregates(IdMovement)
                If gmg?.Id = 0 Then
                    listMessages.Add(String.Format(msg, row.Item("Factura"), row.Item("Código Servicio"), "No se encontro el movimiento del item."))
                    Continue For
                End If
                'Si la evaluación esta confirmada no proceso el movimiento
                If gmg.StateEvaluation = 2 Then
                    Continue For
                End If

                Dim valuePendingGlosa As Decimal = gmg.ValueGlosado - valueAcceptIPS

                '995 - 999 - 996 , type "9", "10", "5", "7"
                If (New List(Of String) From {"9", "10", "5", "7"}).Contains(conceptGlosa.HomologateTypeByCodeAndResponse()) Then
                    If Not valueAcceptIPS = 0 Then
                        errors += String.Format(msg, row.Item("Factura"), row.Item("Código Servicio"), "No se debe aceptar valores cuando el concepto seleccionado es extemporaneo, injustificado o devolución.")
                    End If
                End If

                '997 - type 8
                If conceptGlosa.HomologateTypeByCodeAndResponse() = "8" Then
                    If Not (valueAcceptIPS = gmg.ValueGlosado AndAlso valuePendingGlosa = 0) Then
                        errors += String.Format(msg, row.Item("Factura"), row.Item("Código Servicio"), "Debe Aceptar todo el valor de la glosa.")
                    ElseIf IdResponseHierarchy Is Nothing Then
                        errors += String.Format(msg, row.Item("Factura"), row.Item("Código Servicio"), "Debe diligenciar una Jerarquia de Aceptación.")
                    End If
                End If

                '998 - type 6
                If conceptGlosa.HomologateTypeByCodeAndResponse() = "6" Then
                    If valueAcceptIPS > 0 AndAlso String.IsNullOrEmpty(row.Item("Jerarquia de Aceptación")) Then
                        errors += String.Format(msg, row.Item("Factura"), row.Item("Código Servicio"), "Debe aceptar un valor y diligenciar una Jerarquia de Aceptación.")
                    End If
                End If

                'Capturo el error del item
                If errors.Length > 0 Then
                    listMessages.Add(errors)
                Else
                    'Afecto los movimientos
                    If gmg.ValuePendingConciliation > 0 Then
                        If {"2", "3"}.Contains(gmg.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State) Then
                            'Glosa
                            gmg.ValueAcceptedFirstInstance = valueAcceptIPS
                            gmg.ValuePendingConciliation = gmg.ValuePendingConciliation - valueAcceptIPS
                            gmg.JustificationGlosaText = row.Item("Justificación Glosa")
                            gmg.CodeGlosaEvaluation = row.Item("Código Concepto Respuesta")
                            gmg.IdGlosaEvaluation = conceptGlosa.Id
                            gmg.ResponsibleThirdPartyId = IdThirdRespnsible
                            gmg.IdResponseHierarchyGlosa = IdResponseHierarchy
                        ElseIf {"5", "6"}.Contains(gmg.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State) Then
                            'Reiteracion
                            gmg.ValueAcceptedSecondInstance = valueAcceptIPS
                            gmg.ValuePendingConciliation = gmg.ValueReiterated - valueAcceptIPS
                            gmg.JustificationReiterationText = row.Item("Justificación Glosa")
                            gmg.IdResponseHierarchyReiteration = IdResponseHierarchy
                        End If
                    End If
                    listMessages.Add(String.Format(msg, row.Item("Factura"), row.Item("Código Servicio"), "Se afecto correctamente el movimiento"))
                    _MovementGlosaRepository.SaveEntity(gmg)
                    glosaMovementGlosaUnitWork.Commit()
                End If
            Next
            Return New ActionResult With {.StateResult = True, .MessageResult = listMessages}
        Catch ex As Exception
            Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

#End Region

#Region "Private method"

    ''' <summary>
    ''' Afecta los registro de los movimientos de glosa
    ''' </summary>
    ''' <param name="gmg"></param>
    ''' <returns></returns>
    Private Function AffectMovementByCoordination(gmg As GlosaMovementGlosa) As String
        Dim messag As String = $"Se afecto correctamente el movimiento de la factura {gmg.InvoiceNumber}"
        If gmg.ValuePendingConciliation > 0 Then
            If {"2", "3"}.Contains(gmg.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State) Then
                'Glosa
                gmg.ValuePendingConciliation = gmg.ValuePendingConciliation - gmg.ValueAcceptedCoordination
            ElseIf {"5", "6"}.Contains(gmg.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State) Then
                'Reiteracion
                gmg.ValuePendingConciliation = gmg.ValueReiterated - gmg.ValueAcceptedCoordination
            End If
        End If
        Return messag
    End Function

    ''' <summary>
    ''' Genera el dataTable
    ''' </summary>
    ''' <param name="Comando"></param>
    ''' <param name="session"></param>
    ''' <param name="nameDt"></param>
    ''' <returns></returns>
    Public Function GetDatatable(ByVal Comando As String, filter As Integer, session As SessionValues, nameDt As String) As DataTable
        Dim connectionString = String.Empty
        connectionString = Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, session.TransactionalContainer, False)
        Using conexion As New SqlConnection(connectionString)
            Try
                If conexion.State = ConnectionState.Closed Then
                    conexion.Open()
                End If
                Dim da As SqlDataAdapter = New SqlDataAdapter(Comando, conexion)
                da.SelectCommand.Parameters.AddWithValue("@filter", filter)
                da.SelectCommand.CommandTimeout = 36000
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
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _InterfaceFox.Dispose()
                _InterfacePublicFOX.Dispose()
                _InterfacePublicNET.Dispose()
                _InterfaceNativeAdminservice.Dispose()
                _InterfaceNet.Dispose()
                _PortfolioSequenseAdminService.Dispose()
            End If
            _MovementGlosaRepository = Nothing
            _IInterfaceParametersRepository = Nothing
            _AuxMovementGlosaRepository = Nothing
            _PortFolioGlosaRepository = Nothing
            _BlockRecordRepository = Nothing
            _GlosasServiceMovementGlosas = Nothing
            _InterfaceFox = Nothing
            _InterfaceNet = Nothing
            _InterfacePublicFOX = Nothing
            _InterfacePublicNET = Nothing
            _accountReceivableRepository = Nothing
            _careGroupRepository = Nothing
            _AccountReceivableAccountingRepository = Nothing
            _PortfolioSequenseAdminService = Nothing
            _InterfaceNativeAdminservice = Nothing
            _ITimeGlossParametersRepository = Nothing
            _thirdParyRepository = Nothing
            _responseHierarchyRepository = Nothing
            _conceptGlosasRepository = Nothing
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
