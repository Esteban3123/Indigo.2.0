'***********************************************************************
' Assembly         : Application.Glosas
' Author           : RafaelPatiño
' Created          : 11-04-2014
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
Imports Application.Base
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Base
Imports Domain.InterfaceERPGlosa
Imports Domain.Entities.Service
Imports Application.Accounting
Imports System.Text
Imports System.Data.SqlClient
Imports System.Resources
Imports Infrastructure.CrossCutting.Resources


Public Class RadicateInvoiceAdminService
    Implements IRadicateInvoiceAdminService


#Region "Fields"
    Private _ObjectionsReceptionCRepository As IObjectionsReceptionCRepository
    Private _RadicateInvoiceCRepository As IRadicateInvoiceCRepository
    Private _RadicateInvoiceDRepository As IRadicateInvoiceDRepository
    Private _CustomerRepository As ICustomerRepository
    Private _ConsecutiveRepository As IConsecutiveRepository
    Private _IInterfaceParametersRepository As IInterfaceParametersRepository
    Private _InterfaceFox As IInterfaceFOX
    Private _InterfacePublicFOX As IInterfacePublicFOX
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
    '********************presupuesto
    Dim _budgetSequenceRepository As IBudgetSequenceRepository
    Dim _budgetRepository As IBudgetRepository

    Dim _categoryRepository As IBudgetItemRepository

    Dim sequence As BudgetSequence = Nothing
    Dim _recognitionRepository As IRecognitionRepository
#End Region

#Region "Builder"
    ''' <summary>
    ''' Initializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="ConsecutiveRepository"></param>
    ''' <param name="RadicateInvoiceCRepository"></param>
    ''' <param name="customerRepository"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal ConsecutiveRepository As IConsecutiveRepository, ByVal RadicateInvoiceCRepository As IRadicateInvoiceCRepository, ByVal customerRepository As ICustomerRepository,
                   ByVal ObjectionsReceptionCRepository As IObjectionsReceptionCRepository, InterfaceFox As IInterfaceFOX, IInterfaceParametersRepository As IInterfaceParametersRepository,
                   InterfacePublicFOX As IInterfacePublicFOX, RadicateInvoiceDRepository As IRadicateInvoiceDRepository, accountReceivableRepository As IAccountReceivableRepository,
                   careGroupRepository As ICareGroupRepository, AccountReceivableAccountingRepository As IAccountReceivableAccountingRepository, ByVal InterfaceNativeAdminservice As IInterfaceNativeAdminService,
                   ITimeGlossParametersRepository As ITimeParametersRepository, budgetSequenceRepository As IBudgetSequenceRepository, budgetRepository As IBudgetRepository, categoryRepository As IBudgetItemRepository,
                   recognitionRepository As IRecognitionRepository)
        If RadicateInvoiceCRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Radicacion de facturas Vacio")
        End If
        If ConsecutiveRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de consecutivo vacio")
        End If
        If customerRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de terceros vacio")
        End If
        If ObjectionsReceptionCRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Recepción de Objeciones Vacio")
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
        _ObjectionsReceptionCRepository = ObjectionsReceptionCRepository
        _RadicateInvoiceCRepository = RadicateInvoiceCRepository
        _CustomerRepository = customerRepository
        _ConsecutiveRepository = ConsecutiveRepository
        _IInterfaceParametersRepository = IInterfaceParametersRepository
        _InterfaceFox = InterfaceFox
        _InterfacePublicFOX = InterfacePublicFOX
        _RadicateInvoiceDRepository = RadicateInvoiceDRepository
        _accountReceivableRepository = accountReceivableRepository
        _careGroupRepository = careGroupRepository
        _AccountReceivableAccountingRepository = AccountReceivableAccountingRepository
        _accountReceivableRepository = accountReceivableRepository
        _InterfaceNativeAdminservice = InterfaceNativeAdminservice
        _ITimeGlossParametersRepository = ITimeGlossParametersRepository
        _budgetSequenceRepository = budgetSequenceRepository
        _budgetRepository = budgetRepository
        _categoryRepository = categoryRepository
        _recognitionRepository = recognitionRepository
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Obtener un oficio de facturas radicadas por medio del consecutivo
    ''' </summary>
    ''' <param name="consecutive">consecutivo del oficio</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInvocieRadicate(ByVal consecutive As String, ByVal audit As AuditMessage) As RadicateInvoiceC Implements IRadicateInvoiceAdminService.GetInvocieRadicate
        If String.IsNullOrEmpty(consecutive) = True Then
            Throw New ArgumentNullException("consecutivo Vacio")
        End If
        Try
            Return _RadicateInvoiceCRepository.GetInvocieRadicate(consecutive)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Listar Oficio de Factura Radicadas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListInvocieRadicate(ByVal audit As AuditMessage) As List(Of RadicateInvoiceC) Implements IRadicateInvoiceAdminService.ListInvocieRadicate
        Try
            Return _RadicateInvoiceCRepository.ListInvocieRadicate()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function


    ''' <summary>
    ''' Elimina  un oficio de radicacion de cuentas
    ''' </summary>
    ''' <param name="RadicateInvoiceC">objeto radicacion de cuentas</param>
    ''' <returns></returns>
    Public Function DeleteSaveRadicateInvoiveC(RadicateInvoiceC As RadicateInvoiceC, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult Implements IRadicateInvoiceAdminService.DeleteSaveRadicateInvoiveC
        If RadicateInvoiceC Is Nothing Then
            Throw New ArgumentNullException("Oficio Radicación Vacio")
        End If
        Dim unitOfWork As IUnitWork = _RadicateInvoiceCRepository.UnitWork
        Try
            'Elimino el responsable
            _RadicateInvoiceCRepository.SaveEntity(RadicateInvoiceC)
            unitOfWork.Commit()
            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute("RadicateInvoiceC", audit.Functional, RadicateInvoiceC.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            '/*****Auditoria Avanzada ******/
            Dim auditObject As New IndigoAuditSimpleEntity(Of RadicateInvoiceC)(RadicateInvoiceC, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
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


    ''' <summary>
    ''' Funcion para guardar y confirmar
    ''' </summary>
    ''' <param name="ObjRadicateInvoiceC"></param>
    ''' <param name="idSequence"></param>
    ''' <param name="IndigoSessionValues"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveAndConfirm(ObjRadicateInvoiceC As RadicateInvoiceC, idSequence As Integer, IndigoSessionValues As SessionValues) As ActionResult(Of RadicateInvoiceC) Implements IRadicateInvoiceAdminService.SaveAndConfirm
        If ObjRadicateInvoiceC Is Nothing Then
            Throw New ArgumentNullException("ObjRadicateInvoiceC ")
        End If
        Dim resultConfirm As ActionResult
        Dim result = SaveRadicateInvoiveC(ObjRadicateInvoiceC, IndigoSessionValues)
        If result.StateResult = True Then
            result.ObjectEmbbeded.State = 2
            resultConfirm = ConfirmInvoiceRadicateC(result.ObjectEmbbeded, idSequence, IndigoSessionValues)
            If resultConfirm.StateResult = False Then
                Return New ActionResult(Of RadicateInvoiceC) With {.StateResult = False, .Message = String.Format(Infrastructure.CrossCutting.Resources.ResourceManager.GetString("SavedButNotConfirmed", "Treasury"), result.ObjectEmbbeded.RadicatedConsecutive, resultConfirm.Message)}
            End If
        Else
            Return New ActionResult(Of RadicateInvoiceC) With {.StateResult = False, .Message = result.Message}
        End If
        Return New ActionResult(Of RadicateInvoiceC) With {.StateResultAux = True, .StateResult = True, .ObjectEmbbeded = result.ObjectEmbbeded, .Message = String.Format(Infrastructure.CrossCutting.Resources.ResourceManager.GetString("DocumentGlossSaveWithConsecutive"), result.ObjectEmbbeded.RadicatedConsecutive, resultConfirm.Message)}
    End Function


    ''' <summary>
    ''' guardar un oficio de radicacion de cuentas
    ''' </summary>
    ''' <param name="RadicateInvoiceC">objeto radicacion de cuentas</param>
    ''' <param name="IndigoSessionValues">mensaje de auditoria</param>
    ''' <returns>valor de guardado</returns>
    Public Function SaveRadicateInvoiveC(RadicateInvoiceC As RadicateInvoiceC, IndigoSessionValues As SessionValues) As ActionResult(Of RadicateInvoiceC) Implements IRadicateInvoiceAdminService.SaveRadicateInvoiveC
        If RadicateInvoiceC Is Nothing Then
            Throw New ArgumentNullException("Oficio Radicación Vacio")
        End If
        Dim unitOfWork As IUnitWork = _RadicateInvoiceCRepository.UnitWork
        Dim unitOfWorkInvoiceDRepository As IUnitWork = _RadicateInvoiceDRepository.UnitWork
        Dim unitOfWorkConsecutive As IUnitWork = TryCast(_ConsecutiveRepository.UnitWork, IUnitWork)
        Dim AccountReceivableunitOfWork As IUnitWork = _accountReceivableRepository.UnitWork

        Dim AuxRadicateInvoiceC As RadicateInvoiceC = Nothing
        If RadicateInvoiceC.ChangeTracker.State = ObjectState.Modified Or RadicateInvoiceC.RadicateInvoiceD.ToList().Exists(Function(item As RadicateInvoiceD) item.ChangeTracker.State = ObjectState.Added Or item.ChangeTracker.State = ObjectState.Modified) = True Then
            AuxRadicateInvoiceC = RadicateInvoiceC.OriginalValue
        End If
        Dim auxGlosaObjectionsReceptionC As RadicateInvoiceC = Nothing
        Dim status As Integer

        Dim cnx As System.Data.SqlClient.SqlConnection = Nothing

        'configuro la transaccion
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = IsolationLevel.ReadCommitted
        Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim consecutive As Domain.Entities.Consecutive = Nothing

                ' validacion para que las facturas ingresadas No esten ya en otro oficio
                Dim ListValidation = (From e As RadicateInvoiceD In RadicateInvoiceC.RadicateInvoiceD Select e.InvoiceNumber).ToList()
                Dim listandInvoicesaved As List(Of String) = _RadicateInvoiceDRepository.ListValidateRadicateD(ListValidation)
                If listandInvoicesaved.Count > 0 Then
                    Dim stringBuilder As New StringBuilder
                    For Each item As String In listandInvoicesaved
                        stringBuilder.AppendLine("la factura " & item & " ya esta en otro oficio, no se puede guardar oficio")
                    Next
                    Return New ActionResult(Of RadicateInvoiceC) With {.StateResult = False, .Message = stringBuilder.ToString()}
                End If

                'Valido estado del registro
                If RadicateInvoiceC.Id > 0 Then
                    Dim RadicateInvoiceCTmp = _RadicateInvoiceCRepository.GetInvocieRadicateByIdSimple(RadicateInvoiceC.Id, False)
                    If RadicateInvoiceCTmp IsNot Nothing AndAlso Not RadicateInvoiceCTmp.State = "1" Then
                        Return New ActionResult(Of RadicateInvoiceC) With {.StateResult = False, .Message = String.Format("La radicación se encuentra en estado: {0}", If(RadicateInvoiceCTmp.State = "2", "Confirmado", "Anulado"))}
                    End If
                End If

                'Desde esta opción no se confirma un registro, por lo tanto, si viene en estado 2 se cambia a estado 1 la radicación
                RadicateInvoiceC.State = If(RadicateInvoiceC.State = "2", "1", RadicateInvoiceC.State)

                'grabo la cabecera de la objecion, el detalle "lista facturas" y el detalle de cada factura 
                If RadicateInvoiceC.ChangeTracker.State = ObjectState.Added Then
                    'Actualizamos el numero de consecutivo
                    consecutive = _ConsecutiveRepository.GetConsecutiveByCode("9")  'RADICACIONFACTURAS
                    If consecutive.Id > 0 Then
                        consecutive.NumberConsecutive += 1
                        _ConsecutiveRepository.SaveEntity(consecutive)
                        unitOfWorkConsecutive.Commit()
                    Else
                        Return New ActionResult(Of RadicateInvoiceC) With {.StateResult = False, .Message = "No existe secuencia numérica con Código 9"}
                    End If

                    'si es nuevo incrementamos el consecutivo
                    RadicateInvoiceC.RadicatedConsecutive = CInt(consecutive.NumberConsecutive)
                    RadicateInvoiceC.CreationUser = IndigoSessionValues.AuditMessageWcf.CodeUser
                    RadicateInvoiceC.CreationDate = Date.Now()
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                    _RadicateInvoiceCRepository.SaveEntity(RadicateInvoiceC)
                    'confirmo la unidad de trabajo  la cabecera de la objecion
                    unitOfWork.Commit()
                ElseIf RadicateInvoiceC.ChangeTracker.State = ObjectState.Modified Or RadicateInvoiceC.RadicateInvoiceD.ToList().Exists(Function(item As RadicateInvoiceD) item.ChangeTracker.State = ObjectState.Added Or item.ChangeTracker.State = ObjectState.Modified) = True Then
                    RadicateInvoiceC.ModificationUser = IndigoSessionValues.AuditMessageWcf.CodeUser
                    RadicateInvoiceC.ModificationDate = Date.Now()
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                    'actualizo la entidad
                    _RadicateInvoiceCRepository.SaveEntity(RadicateInvoiceC)
                    'confirmo la unidad de trabajo  la cabecera de la objecion
                    unitOfWork.Commit()
                End If

                'Interaz Nativa o Integracion
                If IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.Native Or IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNet Or IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.IntegractionFox Or IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNATIVE_MIGRATIONS Then
                    If RadicateInvoiceC.State = "4" Then ' si esta anulado procedo a liberar facturas
                        Dim ListRadicateD As List(Of RadicateInvoiceD) = _RadicateInvoiceDRepository.GetListRadicateDByRadicateCId(RadicateInvoiceC.Id)
                        If ListRadicateD.Count > 0 Then
                            For Each item As RadicateInvoiceD In ListRadicateD
                                With item
                                    .State = 4 'anulo
                                End With
                                _RadicateInvoiceDRepository.SaveEntity(item)
                                Dim _AccountReceivable As AccountReceivable = _accountReceivableRepository.GetAccountReceivableByInvoiceNumberGloss(item.InvoiceNumber) 'consultamos factura en cartera
                                If _AccountReceivable IsNot Nothing Then
                                    'Actualizamos estado en cartera
                                    With _AccountReceivable
                                        .PortfolioStatus = 1 'Sin Radicar
                                    End With
                                    _accountReceivableRepository.SaveEntity(_AccountReceivable)
                                Else
                                    Return New ActionResult(Of RadicateInvoiceC) With {.StateResult = False, .Message = "No existe la cuenta por cobrar para la factura" & item.InvoiceNumber}
                                End If
                            Next
                            AccountReceivableunitOfWork.Commit()
                            unitOfWorkInvoiceDRepository.Commit()
                        End If
                    Else
                        If RadicateInvoiceC.RadicateInvoiceD.Count > 0 Then
                            For Each item As RadicateInvoiceD In RadicateInvoiceC.RadicateInvoiceD
                                Dim _AccountReceivable As AccountReceivable = _accountReceivableRepository.GetAccountReceivableByInvoiceNumberGloss(item.InvoiceNumber) 'consultamos factura en cartera
                                If _AccountReceivable IsNot Nothing Then
                                    'Actualizamos estado en cartera
                                    With _AccountReceivable
                                        .PortfolioStatus = 2 ' Radicada Sin ConFirmar 
                                    End With
                                    _AccountReceivable.MarkAsModified()
                                    _accountReceivableRepository.SaveEntity(_AccountReceivable)

                                Else
                                    Return New ActionResult(Of RadicateInvoiceC) With {.StateResult = False, .Message = "No existe la cuenta por cobrar para la factura" & item.InvoiceNumber}
                                End If
                            Next
                            AccountReceivableunitOfWork.Commit()
                        End If
                    End If
                ElseIf IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                    If RadicateInvoiceC.State = "4" Then ' si esta anulado procedo a liberar facturas
                        cnx = New System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, IndigoSessionValues.TransactionalContainer, False))
                        Dim ListRadicateD As List(Of RadicateInvoiceD) = _RadicateInvoiceDRepository.GetListRadicateDByRadicateCId(RadicateInvoiceC.Id)
                        If ListRadicateD.Count > 0 Then
                            Dim OblParameters As GlosasParametersInterface = _IInterfaceParametersRepository.GetInterfacesParametersById(ListRadicateD(0).GlosasParametersInterfaceId)
                            If OblParameters IsNot Nothing Then
                                If OblParameters.AccountingMethod = eTypeInterface.FoxPublic Or OblParameters.AccountingMethod = eTypeInterface.FoxPrivate Then
                                    Dim Strmensaje As New StringBuilder
                                    Dim IntResult As Integer
                                    Dim builder As New StringBuilder
                                    cnx.Open()
                                    Dim command As New System.Data.SqlClient.SqlCommand("", cnx)
                                    command.CommandTimeout = 30000
                                    command.CommandType = CommandType.Text
                                    For Each item As RadicateInvoiceD In ListRadicateD
                                        With item
                                            .State = 4 'anulo
                                        End With
                                        _RadicateInvoiceDRepository.SaveEntity(item)
                                    Next
                                    command.CommandText = "UPDATE " & OblParameters.ContainerName & "..crcarter SET cemestado = '1' WHERE cemnumfac IN (select invoicenumber from  [" & IndigoSessionValues.TransactionalContainer & "].[Portfolio].[RadicateInvoiceD] where RadicateInvoiceCId = " & RadicateInvoiceC.Id & ");"
                                    IntResult = command.ExecuteNonQuery()
                                    If IntResult <= 0 Then
                                        Strmensaje.AppendLine("Error Actualizando Estado de Cartera ERP")
                                        scope.Dispose()
                                        Return New ActionResult(Of RadicateInvoiceC) With {.StateResult = False, .Message = Strmensaje.ToString()}
                                    End If
                                    cnx.Close()
                                    unitOfWorkInvoiceDRepository.Commit()
                                Else
                                    For Each item As RadicateInvoiceD In ListRadicateD
                                        With item
                                            .State = 4 'anulo
                                        End With
                                        _RadicateInvoiceDRepository.SaveEntity(item)
                                    Next
                                    unitOfWorkInvoiceDRepository.Commit()
                                End If
                            End If
                        End If
                    Else
                        If RadicateInvoiceC.RadicateInvoiceD.Count > 0 Then
                            If RadicateInvoiceC.RadicateInvoiceD.ToList().Exists(Function(item As RadicateInvoiceD) item.ChangeTracker.State = ObjectState.Added) = True Then
                                cnx = New System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, IndigoSessionValues.TransactionalContainer, False))
                                Dim OblParameters As GlosasParametersInterface = _IInterfaceParametersRepository.GetInterfacesParametersById(RadicateInvoiceC.RadicateInvoiceD(0).GlosasParametersInterfaceId)
                                'para el metodo fox publico debemos actualizar estado de cartera para la reiteracion
                                If OblParameters IsNot Nothing Then
                                    If OblParameters.AccountingMethod = eTypeInterface.FoxPublic Or OblParameters.AccountingMethod = eTypeInterface.FoxPrivate Then
                                        Dim Strmensaje As New StringBuilder
                                        Dim IntResult As Integer
                                        Dim builder As New StringBuilder
                                        cnx.Open()
                                        Dim command As New System.Data.SqlClient.SqlCommand("", cnx)
                                        command.CommandTimeout = 30000
                                        command.CommandType = CommandType.Text
                                        For Each item As RadicateInvoiceD In RadicateInvoiceC.RadicateInvoiceD
                                            builder.Append("UPDATE " & OblParameters.ContainerName & "..crcarter SET cemestado = 'T' WHERE cemnumfac = '" & item.InvoiceNumber & "'; ")
                                        Next
                                        command.CommandText = builder.ToString() ' "UPDATE " & OblParameters.ContainerName & "..crcarter SET cemestado = 'T' WHERE cemnumfac IN (select invoicenumber from  [" & IndigoSessionValues.TransactionalContainer & "].[Glosas].[RadicateInvoiceD] where RadicateInvoiceCId = " & RadicateInvoiceC.Id & ");"
                                        IntResult = command.ExecuteNonQuery()
                                        If IntResult <= 0 Then
                                            Strmensaje.AppendLine("Error Actualizando Estado de Cartera ERP")
                                            scope.Dispose()
                                            Return New ActionResult(Of RadicateInvoiceC) With {.StateResult = False, .Message = Strmensaje.ToString()}
                                        End If
                                        cnx.Close()
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If

                scope.Complete()

                'RadicateInvoiceC = _RadicateInvoiceCRepository.GetInvocieRadicate(RadicateInvoiceC.RadicatedConsecutive)
                'retorno objeto de respuesta
                Return New ActionResult(Of RadicateInvoiceC) With {.StateResult = True, .Message = RadicateInvoiceC.RadicatedConsecutive.ToString(), .ObjectEmbbeded = RadicateInvoiceC}
            Catch ex As OptimisticConcurrencyException
                scope.Dispose()
                unitOfWork.RollbackChanges()
                Return New ActionResult(Of RadicateInvoiceC) With {.StateResult = False, .Message = ex.Message.ToString}
            Catch ex As Exception
                scope.Dispose()
                unitOfWork.RollbackChanges()

                Dim message As String = ex.Message.ToString
                If ex.InnerException IsNot Nothing AndAlso Not String.IsNullOrEmpty(ex.InnerException.Message) Then
                    message = ex.InnerException.Message
                    If ex.InnerException.InnerException IsNot Nothing AndAlso Not String.IsNullOrEmpty(ex.InnerException.InnerException.Message) Then
                        message = ex.InnerException.InnerException.Message
                    End If
                End If

                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", IndigoSessionValues)
                Return New ActionResult(Of RadicateInvoiceC) With {.StateResult = False, .Message = message}
            Finally
                If cnx IsNot Nothing Then
                    If cnx.State = ConnectionState.Open Then
                        cnx.Close()
                    End If
                End If
            End Try
        End Using
    End Function



    ''' <summary>
    ''' Función para validar y agregar facturas desde el sp
    ''' </summary>
    ''' <param name="ListInvoices">Lista de facturas</param>
    ''' <param name="Nit">Numero de Nit</param>
    ''' <param name="container">nombre Contenedor</param>
    ''' <returns>Lista de Factura </returns>
    Public Function ValidateListInvoiceRadicateDSp(ListInvoices As List(Of String), Nit As String, container As String, IndigoCompany As String, session As SessionValues) As ActionResult(Of List(Of RadicateInvoiceD)) Implements IRadicateInvoiceAdminService.ValidateListInvoiceRadicateDSp
        If ListInvoices.Count = 0 Then
            Throw New ArgumentNullException("Lista de facturas vacía")
        End If
        If Nit Is String.Empty Then
            Throw New ArgumentNullException("Nit vacío")
        End If
        If session.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
            If container Is String.Empty Then
                Throw New ArgumentNullException("Nombre Contenedor vacío")
            End If
        End If
        Dim _Result As New ActionResult(Of List(Of RadicateInvoiceD))
        Dim _listError As New List(Of String)
        Dim ListRadicateInvoiceD As New List(Of RadicateInvoiceD)
        Try
            Dim ObjInterfaceParameter As GlosasParametersInterface
            If session.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                ObjInterfaceParameter = _IInterfaceParametersRepository.GetInterfacesParameters(container)
            End If
            For Each item As String In ListInvoices
                Dim StrMensaje As String = String.Empty
                Dim itemInvoice As New SP_invoiceList_Result
                Dim AccountValidate As Boolean
                If session.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                    itemInvoice = _ObjectionsReceptionCRepository.GetInvoice(container, Nit, item, IndigoCompany, session.HisContainer, String.Empty, 1)
                    If ObjInterfaceParameter.AccountingMethod = eTypeInterface.FoxPrivate Then
                        AccountValidate = _IInterfaceParametersRepository.ValidateAccountTableFOxPrivate(itemInvoice.AccountantAccountCustomers, False)
                    Else
                        AccountValidate = True
                    End If
                Else
                    AccountValidate = True
                    itemInvoice = _ObjectionsReceptionCRepository.GetInvoice(String.Empty, Nit, item, IndigoCompany, session.HisContainer, String.Empty, 1)
                End If
                'si no existe la cuenta en configuracion 
                If AccountValidate = True Then
                    If itemInvoice.InvoiceNumber IsNot Nothing AndAlso Not itemInvoice.InvoiceNumber.Trim().Equals(String.Empty) Then
                        If itemInvoice.StateCurrentInvoice = "1" Then 'no confirmada radicada
                            'declaro variable  tipo detalle factura
                            Dim _TmpRadicateInvoiceD As New RadicateInvoiceD
                            With _TmpRadicateInvoiceD
                                .InvoiceNumber = itemInvoice.InvoiceNumber.Trim
                                .InvoiceNumber = itemInvoice.InvoiceNumber.Trim
                                .BalanceInvoice = itemInvoice.BalanceInvoice.ToString.Trim
                                .InvoiceValueEntity = itemInvoice.InvoiceValueEntity.ToString.Trim
                                .InvoiceValuePacient = itemInvoice.InvoiceValuePacient.ToString.Trim
                                .InvoiceValueFacade = .InvoiceValueEntity + IIf(.InvoiceValuePacient Is Nothing, 0, .InvoiceValuePacient)
                                .AccountantAccountCustomers = itemInvoice.AccountantAccountCustomers.Trim
                                .InvoiceDate = itemInvoice.InvoiceDate
                                .PatientCode = itemInvoice.PatientCode.Trim
                                .PatientName = itemInvoice.PatientName.Trim
                                .IngressNumber = itemInvoice.IngressNumber.Trim
                                .IngressDate = itemInvoice.IngressDate
                                .UserNameInvoice = itemInvoice.UserNameInvoice.Trim
                                .ContractCode = itemInvoice.ContractCode.Trim
                                .PlanCode = itemInvoice.CodePlan
                                .ContractEntity = itemInvoice.ContractEntity
                                .State = "1"
                                .CreditNoteValue = itemInvoice.CreditNoteValue
                                .DebitNoteValue = itemInvoice.DebitNoteValue
                                .Devolution = itemInvoice.Devolution
                                .ConceptDevolution = itemInvoice.ConceptDevolution
                                If .Devolution = 1 Then
                                    StrMensaje = "Factura Radicada por Devolucion: " + item
                                    _listError.Add(StrMensaje)
                                End If
                                .InvoiceDocumentType = itemInvoice.DocumentType
                            End With
                            Dim tmpObj = ListRadicateInvoiceD.Find(Function(value As RadicateInvoiceD) value.InvoiceNumber = _TmpRadicateInvoiceD.InvoiceNumber)
                            If tmpObj Is Nothing Then
                                ListRadicateInvoiceD.Add(_TmpRadicateInvoiceD)
                            End If
                        Else
                            Select Case itemInvoice.StateCurrentInvoice
                                Case "2"
                                    StrMensaje = itemInvoice.InvoiceNumber.ToString + " Factura  Radicada "
                                Case "T"
                                    StrMensaje = itemInvoice.InvoiceNumber.ToString + " Factura Radicada Sin Confirmar "
                                Case "6"
                                    StrMensaje = itemInvoice.InvoiceNumber.ToString + " Factura Anulada "
                            End Select
                            _listError.Add(StrMensaje)
                        End If
                    Else
                        StrMensaje = item + " Factura No Existe "
                        _listError.Add(StrMensaje)
                    End If
                Else
                    StrMensaje = "La cuenta No esta Configurada en la columna (Factura Sin Radicar), No se puede Agregar Factura " & itemInvoice.AccountantAccountCustomers & " - " & itemInvoice.InvoiceNumber
                    _listError.Add(StrMensaje)
                End If
            Next

            Dim listValidationType = (From e In ListRadicateInvoiceD Select e.InvoiceDocumentType).Distinct().ToList()
            If listValidationType.Count > 1 Then
                Dim itemCapital = listValidationType.Find(Function(x) x.Value = 4)
                If itemCapital IsNot Nothing Then
                    _listError = New List(Of String)
                    _listError.Add("No se pueden mezclar facturas capitadas con facturas de ventas")
                    _Result.MessageResult = _listError
                    _Result.StateResult = False
                    ''_Result.ObjectEmbbeded = ListRadicateInvoiceD
                Else
                    _Result.MessageResult = _listError
                    _Result.StateResult = True
                    _Result.ObjectEmbbeded = ListRadicateInvoiceD
                End If
            Else
                _Result.MessageResult = _listError
                _Result.StateResult = True
                _Result.ObjectEmbbeded = ListRadicateInvoiceD
            End If

            
            Return _Result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Confirmar un oficio de radicado
    ''' </summary>
    ''' <param name="InvoiceRadicateC"></param>
    ''' <param name="IndigoSessionValues"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConfirmInvoiceRadicateC(InvoiceRadicateC As RadicateInvoiceC, idSequence As Integer, IndigoSessionValues As SessionValues) As ActionResult Implements IRadicateInvoiceAdminService.ConfirmInvoiceRadicateC
        If InvoiceRadicateC Is Nothing Then
            Throw New ArgumentNullException("Oficio Radicado Vacio")
        End If
        Dim unitOfWork As IUnitWork = _RadicateInvoiceCRepository.UnitWork
        Dim AccountReceivableunitOfWork As IUnitWork = _accountReceivableRepository.UnitWork
        Dim AccountReceivableAccountingunitOfWork As IUnitWork = _AccountReceivableAccountingRepository.UnitWork
        Dim RadicateCunitOfWork As IUnitWork = _RadicateInvoiceCRepository.UnitWork
        Dim RadicateDunitOfWork As IUnitWork = _RadicateInvoiceDRepository.UnitWork
        Dim sequenceBudgetUnitOfWork = _budgetSequenceRepository.UnitWork
        Try
            Dim resultTotal As New ActionResult
            If IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.Native Or IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNet Or IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.IntegractionFox Or IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNATIVE_MIGRATIONS Then
                Dim txSettings As New TransactionOptions()
                txSettings.Timeout = TransactionManager.MaximumTimeout
                txSettings.IsolationLevel = IsolationLevel.ReadCommitted
                Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
                    Dim resultReclasification = _InterfaceNativeAdminservice.GeneratePortfolioReclasification(InvoiceRadicateC.OperatingUnitId, InvoiceRadicateC.Id, IndigoSessionValues.UserIndigo)
                    If resultReclasification.StateResult = False Then
                        scope.Dispose()
                        Return New ActionResult With {.StateResult = False, .Message = resultReclasification.Message}
                    End If
                    scope.Complete()
                    Return New ActionResult With {.StateResult = True, .Message = resultReclasification.Message}
                End Using
            ElseIf IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                Dim txSettings As New TransactionOptions()
                txSettings.Timeout = TransactionManager.MaximumTimeout
                txSettings.IsolationLevel = IsolationLevel.ReadCommitted
                Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
                    Dim objParameter As New GlosasParametersInterface
                    Dim ListRadicateD As List(Of RadicateInvoiceD) = _RadicateInvoiceDRepository.GetListRadicateDByRadicateCId(InvoiceRadicateC.Id)
                    If ListRadicateD IsNot Nothing AndAlso ListRadicateD.Count > 0 Then
                        objParameter = _IInterfaceParametersRepository.GetInterfacesParametersById(ListRadicateD(0).GlosasParametersInterfaceId)
                    End If
                    Dim result As New List(Of InterfaceResult)
                    result = Me.ExecuteNotaContable(InvoiceRadicateC, ListRadicateD, objParameter, IndigoSessionValues)  'ejecutamos interfaz de creacion de nota contable
                    Dim StrMessafue As New StringBuilder
                    If result.Count > 0 AndAlso result(0).Result = True Then
                        For Each item As InterfaceResult In result
                            StrMessafue.AppendLine(item.Message.ToString)
                        Next
                        resultTotal = New ActionResult With {.StateResult = True, .Message = StrMessafue.ToString()}
                    ElseIf result.Count > 0 AndAlso result(0).Result = False Then
                        For Each item As InterfaceResult In result
                            StrMessafue.AppendLine(item.Message.ToString)
                        Next
                        resultTotal = New ActionResult With {.StateResult = False, .Message = StrMessafue.ToString()}
                        scope.Dispose()
                        Return resultTotal
                    End If
                    scope.Complete()
                End Using
            End If
            Return resultTotal
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .Message = "-999"}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", IndigoSessionValues)
            Return New ActionResult With {.StateResult = False, .Message = ex.Message.ToString()}
        End Try
    End Function


    ''' <summary>
    ''' metodo para generar la secuencia del reconocimiento
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateSequence() As ActionResult

        sequence = _budgetSequenceRepository.GetSequenseByIdForm("213")

        If sequence IsNot Nothing AndAlso sequence.Id > 0 AndAlso sequence.Sequential Then
            Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(sequence.BudgetSequenceDetail(0).Sequense.Pattern, sequence.BudgetSequenceDetail(0).Next)
            If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                Return New ActionResult With {.Message = res, .StateResult = True}
            Else
                Return New ActionResult With {.StateResult = False, .Message = "La secuencia para el reconocimiento alcanzo su valor maximo"}
            End If

        Else
            Return New ActionResult With {.StateResult = False, .Message = "La secuencia para reconocimentos no esta parametrizada o no es secuencial"}
        End If
    End Function

    ''' <summary>
    ''' Metodo para la ejecucion de SP que realiza la interfaz  Aceptacion por parte de la IPS tanto para Aceptacion 
    ''' </summary>
    ''' <param name="Objc">Oficio radicado</param>
    ''' <param name="IndigoSessionValues">valores de sesion</param>
    ''' <returns>Lista de Mensaje del SP, un Codigo y un Mensaje </returns>
    ''' <remarks></remarks>
    Private Function ExecuteNotaContable(ByVal Objc As RadicateInvoiceC, ByVal listD As List(Of RadicateInvoiceD), ByVal objParameter As GlosasParametersInterface, ByVal IndigoSessionValues As SessionValues) As List(Of InterfaceResult)
        Dim result As New List(Of InterfaceResult)
        'si se aplica interfaz para la empresa
        If objParameter.Interface = True Then
            Dim CodEmpresaDGH As String = objParameter.ContainerName
            Dim IntOpcion As String = "RADICACIONFACTURAS"
            Dim User As String = IndigoSessionValues.UserInterface
            '  Dim listD As List(Of RadicateInvoiceD) = (From e In Objc.RadicateInvoiceD Select e).ToList()
            If objParameter.AccountingMethod = eTypeInterface.FoxPrivate Then
                result = _InterfaceFox.RadicateInvoice(Objc, listD, IndigoSessionValues.TransactionalContainer, CodEmpresaDGH, IntOpcion, User, Objc.ConfirmComment)
            ElseIf objParameter.AccountingMethod = eTypeInterface.FoxPublic Then
                result = _InterfacePublicFOX.RadicateInvoice(Objc, listD, IndigoSessionValues.TransactionalContainer, CodEmpresaDGH, IntOpcion, User, Objc.ConfirmComment)
            End If
        Else
            result.Add(New InterfaceResult With {.Result = False, .Message = "No esta activa la configuración de interfaz contable para la empresa " & objParameter.CompanyName})
        End If
        Return result
    End Function


    ''' <summary>
    ''' Actualizar fecha de confirmacion de x numero radicado
    ''' </summary>
    ''' <param name="NumberRadicate"></param>
    ''' <param name="NewDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateDateConfirm(NumberRadicate As String, NewDate As Date, IndigoSessionValues As SessionValues) As actionresult Implements IRadicateInvoiceAdminService.UpdateDateConfirm
        If String.IsNullOrEmpty(NumberRadicate) = True Then
            Throw New ArgumentNullException("NumberRadicate vacío")
        End If
        Dim result As New ActionResult
        Dim unitOfWork As IUnitWork = _RadicateInvoiceCRepository.UnitWork
        Try
            Dim namecontainer As String = String.Empty
            If IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                NumberRadicate = fncConcatenar("0", NumberRadicate, 10)
            End If
            Dim ObjInvoiceD As RadicateInvoiceD = _RadicateInvoiceCRepository.GetInvocieDRadicate(NumberRadicate)
            'configuro la transaccion
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.DefaultTimeout
            txSettings.IsolationLevel = IsolationLevel.ReadCommitted
            'inicio la transaccion
            Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
                If ObjInvoiceD.Id = 0 Then
                    result = New ActionResult With {.MessageResult = New List(Of String)({"El numero radicado no existeen ERP"}), .StateResult = False}
                    Return result
                Else
                    If IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                        Dim objParameter As GlosasParametersInterface = _IInterfaceParametersRepository.GetInterfacesParametersById(ObjInvoiceD.GlosasParametersInterfaceId)
                        Dim r As Boolean = _InterfacePublicFOX.UpdateDateConfirm(NumberRadicate, objParameter.ContainerName, NewDate)
                        If r = False Then
                            result = New ActionResult With {.MessageResult = New List(Of String)({"Error actualizando numero radicado"}), .StateResult = False}
                            Return result
                        End If
                    End If
                    ObjInvoiceD.RadicateInvoiceC.ConfirmDate = NewDate
                    ObjInvoiceD.RadicateInvoiceC.RadicatedDate = NewDate
                    ObjInvoiceD.RadicateInvoiceC.ConfirmUser = IndigoSessionValues.UserIndigoId
                    _RadicateInvoiceCRepository.SaveEntity(ObjInvoiceD.RadicateInvoiceC)
                    unitOfWork.Commit()
                    scope.Complete()
                End If
            End Using
            result = New ActionResult With {.StateResult = True, .MessageResult = New List(Of String)({"ok"})}
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", IndigoSessionValues)
            Return New ActionResult With {.MessageResult = New List(Of String)({ex.Message.ToString}), .StateResult = False}
        End Try
    End Function


    Public Function fncConcatenar(ByVal strCaracter As String, ByVal strCadena As String, ByVal intNumero As Integer, Optional ByVal strPrefijo As String = "") As String
        Dim intCant As Integer
        intCant = strCadena.Length
        For i = intCant To intNumero - 1
            strCadena = strCaracter & strCadena
        Next
        fncConcatenar = strPrefijo.Trim & strCadena
        Return fncConcatenar
    End Function

    ''' <summary>
    ''' Obtener la trazabilidad de una factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_InvoiceTraceability(container As String, invoice As String, IndigoSessionValues As SessionValues) As SP_InvoiceTraceability_Result Implements IRadicateInvoiceAdminService.SP_InvoiceTraceability
        If IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
            If String.IsNullOrEmpty(container) = True Then
                Throw New ArgumentNullException("Contenedor vacío")
            End If
        End If
        Dim result As New ActionResult
        Try
            'Dim a = dtsTrazabilityReports(container, invoice, IndigoSessionValues)
            Return _RadicateInvoiceCRepository.SP_InvoiceTraceability(container, IndigoSessionValues.SecurityContainer, invoice)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", IndigoSessionValues)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtener la trazabilidad de una factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_InvoiceTraceabilityConciliation(invoiceNumber As String, IndigoSessionValues As SessionValues) As List(Of SP_InvoiceTraceabilityConciliation_Result) Implements IRadicateInvoiceAdminService.SP_InvoiceTraceabilityConciliation
        If String.IsNullOrEmpty(invoiceNumber) = True Then
            Throw New ArgumentNullException("factura vacío")
        End If
        Dim result As New ActionResult
        Try

            Return _RadicateInvoiceCRepository.SP_InvoiceTraceabilityConciliation(invoiceNumber)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", IndigoSessionValues)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtener la trazabilidad de una factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_InvoiceTraceabilityDevolution(invoiceNumber As String, IndigoSessionValues As SessionValues) As List(Of SP_InvoiceTraceabilityDevolution_Result) Implements IRadicateInvoiceAdminService.SP_InvoiceTraceabilityDevolution
        If String.IsNullOrEmpty(invoiceNumber) = True Then
            Throw New ArgumentNullException("factura vacío")
        End If
        Dim result As New ActionResult
        Try
            Return _RadicateInvoiceCRepository.SP_InvoiceTraceabilityDevolution(invoiceNumber)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", IndigoSessionValues)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtener la trazabilidad de una factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_InvoiceTraceabilityRadication(invoiceNumber As String, IndigoSessionValues As SessionValues) As List(Of SP_InvoiceTraceabilityRadication_Result) Implements IRadicateInvoiceAdminService.SP_InvoiceTraceabilityRadication
        If String.IsNullOrEmpty(invoiceNumber) = True Then
            Throw New ArgumentNullException("factura vacío")
        End If
        Dim result As New ActionResult
        Try

            Return _RadicateInvoiceCRepository.SP_InvoiceTraceabilityRadication(invoiceNumber)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", IndigoSessionValues)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Carga info para la generación del reporte de trazabilidad
    ''' </summary>
    ''' <param name="container"></param>
    ''' <param name="invoice"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function dtsTrazabilityReports(container As String, invoice As String, session As SessionValues) As DataSet Implements IRadicateInvoiceAdminService.dtsTrazabilityReports
        If session.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
            If String.IsNullOrEmpty(container) = True Then
                Throw New ArgumentNullException("Contenedor vacío")
            End If
        End If
        Dim ds As New DataSet
        Try
            Dim query1 As String = "exec [Glosas].SP_InvoiceTraceability '" & container & "','" & session.SecurityContainer & "','" & invoice & "'"
            Dim query2 As String = "exec [Glosas].[SP_InvoiceTraceabilityConciliation] '" & invoice & "'"
            Dim query3 As String = "exec [Glosas].[SP_InvoiceTraceabilityDevolution] '" & invoice & "'"
            Dim query4 As String = "exec [Glosas].[SP_InvoiceTraceabilityRadication] '" & invoice & "'"
            Dim query5 As String = "exec [Glosas].[SP_InvoiceTraceabilityResponsibles] '" & invoice & "'"
            Dim dt1 = Me.GetDatatable(query1, session, "InvoiceTraceability")
            Dim dt2 = Me.GetDatatable(query2, session, "InvoiceTraceabilityConciliation")
            Dim dt3 = Me.GetDatatable(query3, session, "InvoiceTraceabilityDevolution")
            Dim dt4 = Me.GetDatatable(query4, session, "InvoiceTraceabilityRadication")
            Dim dt5 = Me.GetDatatable(query5, session, "InvoiceTraceabilityResponsibles")
            ds.Tables.Add(dt1.Copy())
            ds.Tables.Add(dt2.Copy())
            ds.Tables.Add(dt3.Copy())
            ds.Tables.Add(dt4.Copy())
            ds.Tables.Add(dt5.Copy())
            Return ds
        Catch ex As Exception
            'descarto los cambios en la eliminacion del detalle
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return Nothing
            'Finally
            '    conexion.Close()
        End Try
    End Function


    Public Function GetDatatable(ByVal Comando As String, session As SessionValues, nameDt As String) As System.Data.DataTable
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
#End Region

    ''' <summary>
    ''' Obtener un oficio de facturas radicadas por el id sin agregados
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetInvocieRadicateByIdSimple(Id As Integer) As RadicateInvoiceC Implements IRadicateInvoiceAdminService.GetInvocieRadicateByIdSimple
        Try
            Return _RadicateInvoiceCRepository.GetInvocieRadicateByIdSimple(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New RadicateInvoiceC
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _InterfaceFox.Dispose()
                _InterfacePublicFOX.Dispose()
                _InterfaceNativeAdminservice.Dispose()
            End If
            _ObjectionsReceptionCRepository = Nothing
            _RadicateInvoiceCRepository = Nothing
            _CustomerRepository = Nothing
            _ConsecutiveRepository = Nothing
            _IInterfaceParametersRepository = Nothing
            _InterfaceFox = Nothing
            _InterfacePublicFOX = Nothing
            _RadicateInvoiceDRepository = Nothing
            _accountReceivableRepository = Nothing
            _careGroupRepository = Nothing
            _AccountReceivableAccountingRepository = Nothing
            _accountReceivableRepository = Nothing
            _InterfaceNativeAdminservice = Nothing
            _ITimeGlossParametersRepository = Nothing
            _budgetSequenceRepository = Nothing
            _budgetRepository = Nothing
            _categoryRepository = Nothing
            _recognitionRepository = Nothing
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
