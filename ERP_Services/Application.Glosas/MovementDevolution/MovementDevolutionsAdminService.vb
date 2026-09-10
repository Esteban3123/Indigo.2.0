'***********************************************************************
' Assembly         : Application.Glosas
' Author           : RafaelPatiño
' Created          : 08-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports System.Transactions
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Common.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports System.Data.Entity.Core
Imports Domain.InterfaceERPGlosa
Imports Application.Accounting
Imports System.Text

Public Class MovementDevolutionsAdminService
    Implements IMovementDevolutionsAdminService

#Region "Fields"
    Private _ObjectionsReceptionCRepository As IObjectionsReceptionCRepository
    Private _MovementDevolutionRepository As IMovementDevolutionsRepository
    Private _DevolutionDRepository As IDevolutionsReceptionDRepository
    Private _IInterfaceParametersRepository As IInterfaceParametersRepository
    Private _InterfaceFox As IInterfaceFOX
    Private _InterfaceNET As IInterfaceNET
    Private _InterfacePublicFOX As IInterfacePublicFOX
    Private _InterfacePublicNET As IInterfacePublicNET
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
    ''' Repositorio de Detalle Radicado
    ''' </summary>
    ''' <remarks></remarks>
    Private _RadicateInvoiceDRepository As IRadicateInvoiceDRepository
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
    ''' Initializa una nueva instancia de la clase <see cref="IMovementDevolutionsRepository" />.
    ''' </summary>
    ''' <param name="MovementDevolutionRepository">el repositorio para el manejo de movimientos devolución.</param>
    Public Sub New(ByVal MovementDevolutionRepository As IMovementDevolutionsRepository, ByVal DevolutionDRepository As IDevolutionsReceptionDRepository, IInterfaceParametersRepository As IInterfaceParametersRepository,
                   ByVal ObjectionsReceptionCRepository As IObjectionsReceptionCRepository, InterfaceFox As IInterfaceFOX, InterfacePublicFOX As IInterfacePublicFOX, InterfacePublicNET As IInterfacePublicNET,
                  ByVal InterfaceNativeAdminservice As IInterfaceNativeAdminService, accountReceivableRepository As IAccountReceivableRepository, careGroupRepository As ICareGroupRepository,
                  AccountReceivableAccountingRepository As IAccountReceivableAccountingRepository, RadicateInvoiceDRepository As IRadicateInvoiceDRepository, InterfaceNET As IInterfaceNET, ITimeGlossParametersRepository As ITimeParametersRepository)
        If ObjectionsReceptionCRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Recepción de Objeciones Vacio")
        End If
        If MovementDevolutionRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Movimientos Devolución Vacío")
        End If
        If DevolutionDRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio Devolución Detalle Vacío")
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
        _MovementDevolutionRepository = MovementDevolutionRepository
        _DevolutionDRepository = DevolutionDRepository
        _IInterfaceParametersRepository = IInterfaceParametersRepository
        _InterfaceFox = InterfaceFox
        _InterfacePublicFOX = InterfacePublicFOX
        _InterfacePublicNET = InterfacePublicNET
        _accountReceivableRepository = accountReceivableRepository
        _careGroupRepository = careGroupRepository
        _AccountReceivableAccountingRepository = AccountReceivableAccountingRepository
        _RadicateInvoiceDRepository = RadicateInvoiceDRepository
        _InterfaceNativeAdminservice = InterfaceNativeAdminservice
        _InterfaceNET = InterfaceNET
        _ITimeGlossParametersRepository = ITimeGlossParametersRepository
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Funcion para eliminar un Movimiento de Devolución
    ''' </summary>
    ''' <param name="MovementDevolution">Movimiento Devolución</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteMovementDevolution(MovementDevolution As GlosaMovementDevolutions, audit As AuditMessage) As ActionResult Implements IMovementDevolutionsAdminService.DeleteMovementDevolution
        If MovementDevolution Is Nothing Then
            Throw New ArgumentNullException("Movimeinto Devolución Vacio")
        End If
        Dim unitOfWork As IUnitWork = _MovementDevolutionRepository.UnitWork
        Try
            'Elimino la cabecera de devolución.
            _MovementDevolutionRepository.DeleteEntity(MovementDevolution)
            unitOfWork.Commit()
            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute("GlosaMovementDevolutions", audit.Functional, MovementDevolution.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            Return New ActionResult With {.StateResult = True}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList()}
        End Try
    End Function
    ''' <summary>
    ''' Obtiene un Movimiento Devolucion especifico.
    ''' </summary>
    ''' <param name="Invoice">Numero de factura</param>
    ''' <param name="IdDevolutionD">Id Devolución Detalle</param>
    ''' <returns>Objeto Movimiento Devolucion</returns>
    Public Function GetMovementDevolutionByInvoiceNumber(Invoice As String, IdDevolutionD As String) As GlosaMovementDevolutions Implements IMovementDevolutionsAdminService.GetMovementDevolutionByInvoiceNumber
        If Invoice Is Nothing AndAlso String.IsNullOrEmpty(Invoice) Then
            Throw New ArgumentNullException("Factura Vacía")
        End If
        Try
            Return _MovementDevolutionRepository.GetMovementDevolutionByInvoiceNumber(Invoice, IdDevolutionD)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Lista todos los Movimientos de Devoluciones.
    ''' </summary>
    ''' <returns>Lista de Movimiento de Devoluciones</returns>
    Public Function ListAllMovementGlosaDevolutions() As List(Of GlosaMovementDevolutions) Implements IMovementDevolutionsAdminService.ListAllMovementGlosaDevolutions
        Try
            Return _MovementDevolutionRepository.ListAllMovementGlosaDevolutions
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Funcion para Guardar un Movimiento de Devolución
    ''' </summary>
    ''' <param name="MovementDevolution">Movimineto Devolución</param>
    ''' <param name="audit"></param>
    Public Function SaveMovementDevolution(MovementDevolution As GlosaMovementDevolutions, audit As AuditMessage) As ActionResult(Of GlosaMovementDevolutions) Implements IMovementDevolutionsAdminService.SaveMovementDevolution
        If MovementDevolution Is Nothing Then
            Throw New ArgumentNullException("Movimiento Devolución Vacio")
        End If
        Dim unitOfWork As IUnitWork = _MovementDevolutionRepository.UnitWork
        Try
            If MovementDevolution.ChangeTracker.State = ObjectState.Added Then
                MovementDevolution.CreationUser = audit.CodeUser
                MovementDevolution.CreationDate = Date.Now()
                _MovementDevolutionRepository.AddEntity(MovementDevolution)
            ElseIf MovementDevolution.ChangeTracker.State = ObjectState.Modified Then
                MovementDevolution.ModificationUser = audit.CodeUser
                MovementDevolution.ModificationDate = Date.Now()
                _MovementDevolutionRepository.UpdateEntity(MovementDevolution)
            End If
            unitOfWork.Commit()
            If MovementDevolution.ChangeTracker.State = ObjectState.Added Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute("GlosaMovementDevolutions", audit.Functional, MovementDevolution.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
            ElseIf MovementDevolution.ChangeTracker.State = ObjectState.Modified Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute("GlosaMovementDevolutions", audit.Functional, MovementDevolution.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
            End If
            Return New ActionResult(Of GlosaMovementDevolutions) With {.StateResult = True, .ObjectEmbbeded = MovementDevolution}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of GlosaMovementDevolutions) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of GlosaMovementDevolutions) With {.StateResult = False, .MessageResult = {ex.Message}.ToList()}
        End Try
    End Function

    ''' <summary>
    ''' Funcion para Confirmar un Movimiento de Devolución
    ''' </summary>
    ''' <param name="listDevolutionD">Movimineto Devolución</param>
    ''' <param name="IndigoSessionValues"></param>
    Public Function ConfirmDevolution(ByVal listDevolutionD As List(Of GlosaDevolutionsReceptionD), ByVal idSequence As Integer, ByVal Injustificate As Boolean, ByVal UserFreeInvoice As Boolean, ByVal IndigoSessionValues As SessionValues) As ActionResult Implements IMovementDevolutionsAdminService.ConfirmDevolution
        If listDevolutionD.Count = 0 Then
            Throw New ArgumentNullException("Movimiento Devolución Vacio")
        End If
        Dim unitOfWorkDevolutionD As IUnitWork = _DevolutionDRepository.UnitWork
        Dim resultTotal As New ActionResult
        Try
            'configuro la transaccion
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.DefaultTimeout
            txSettings.IsolationLevel = IsolationLevel.ReadCommitted
            If IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                'Cargamos configuraciones
                Dim objParameter As New GlosasParametersInterface
                If listDevolutionD.Count > 0 Then
                    objParameter = _IInterfaceParametersRepository.GetInterfacesParametersById(listDevolutionD(0).GlosasParametersInterfaceId)
                End If
                Dim FreeInvoice As Boolean
                If Injustificate = True Then
                    If objParameter.DevolutionInjustificate = "3" Then '3) definido por el usuario
                        If UserFreeInvoice = True Then ' y el usuario dijo que si se libera
                            FreeInvoice = True
                        Else
                            FreeInvoice = False 'si el usuario decidio no liberar
                        End If
                    Else
                        If objParameter.DevolutionInjustificate = "1" Then 'liberar factura del radicado 
                            FreeInvoice = True
                        ElseIf objParameter.DevolutionInjustificate = "2" Then 'mantener factura en el radicado ,NO  liberamos factura de radicado
                            FreeInvoice = False
                        End If
                    End If
                Else
                    FreeInvoice = True ' si es justificada, liberamos factura de radicado
                End If
                'inicio la transaccion
                Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
                    If FreeInvoice = True Then
                        'creacion de comrpobantes contable y eliminacion de facturas en radicados
                        Dim result As New List(Of InterfaceResult)
                        result = Me.ExecuteNotaContable(listDevolutionD, objParameter, IndigoSessionValues)  'ejecutamos interfaz de creacion de nota contable
                        Dim LisStrMessafue As New List(Of String)
                        If result.Count > 0 AndAlso result(0).Result = True Then
                            For Each item As InterfaceResult In result
                                LisStrMessafue.Add(item.Message.ToString)
                            Next
                            For Each itemD As GlosaDevolutionsReceptionD In listDevolutionD
                                Dim DevolutionD As GlosaDevolutionsReceptionD = _DevolutionDRepository.GetDevolutionDById(itemD.Id)
                                DevolutionD.State = 2
                                For Each itemmodev As GlosaMovementDevolutions In DevolutionD.GlosaMovementDevolutions
                                    Dim _result = ValidateRadicated(itemmodev)
                                    If _result Is Nothing OrElse Not _result?.StateResult Then
                                        unitOfWorkDevolutionD.RollbackChanges()
                                        scope.Dispose()
                                        Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({_result?.Message})}
                                    End If
                                    itemmodev.ModificationUser = IndigoSessionValues.AuditMessageWcf.CodeUser
                                    itemmodev.ModificationDate = Date.Now()
                                    itemmodev.State = 2
                                Next
                                _DevolutionDRepository.SaveEntity(DevolutionD)
                                unitOfWorkDevolutionD.Commit()

                            Next
                            'confirmo la transaccion
                            scope.Complete()
                            resultTotal = New ActionResult With {.StateResult = True, .MessageResult = LisStrMessafue}
                        ElseIf result.Count > 0 AndAlso result(0).Result = False Then
                            For Each item As InterfaceResult In result
                                LisStrMessafue.Add(item.Message.ToString)
                            Next
                            resultTotal = New ActionResult With {.StateResult = False, .MessageResult = LisStrMessafue}
                            Return resultTotal
                        End If
                    Else
                        'si no liberamos factura, solo cambiamos estado de factura
                        For Each itemD As GlosaDevolutionsReceptionD In listDevolutionD
                            Dim DevolutionD As GlosaDevolutionsReceptionD = _DevolutionDRepository.GetDevolutionDById(itemD.Id)
                            DevolutionD.State = 2
                            For Each itemmodev As GlosaMovementDevolutions In DevolutionD.GlosaMovementDevolutions
                                Dim _result = ValidateRadicated(itemmodev)
                                If _result Is Nothing OrElse Not _result?.StateResult Then
                                    unitOfWorkDevolutionD.RollbackChanges()
                                    scope.Dispose()
                                    Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({_result?.Message})}
                                End If
                                itemmodev.ModificationUser = IndigoSessionValues.AuditMessageWcf.CodeUser
                                itemmodev.ModificationDate = Date.Now()
                                itemmodev.State = 2
                            Next
                            _DevolutionDRepository.SaveEntity(DevolutionD)
                            unitOfWorkDevolutionD.Commit()
                        Next
                        'confirmo la transaccion
                        scope.Complete()
                        resultTotal = New ActionResult With {.StateResult = True}
                    End If
                End Using
                Return resultTotal
            ElseIf IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.Native Or IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNet Or IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.IntegractionFox Or IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNATIVE_MIGRATIONS Then
                Dim AccountReceivableunitOfWork As IUnitWork = _accountReceivableRepository.UnitWork
                Dim AccountReceivableAccountingunitOfWork As IUnitWork = _AccountReceivableAccountingRepository.UnitWork
                Dim RadicateDunitOfWork As IUnitWork = _RadicateInvoiceDRepository.UnitWork
                Dim TmpReclassificationResult As New ActionResult
                Dim ListStrMessage As New List(Of String)
                Dim _AccountReceivableTmp As AccountReceivable = _accountReceivableRepository.GetAccountReceivableByInvoiceNumberGloss(listDevolutionD(0).InvoiceNumber) 'consultamos factura en cartera
                Dim GlossParameter As TimeParameters = _ITimeGlossParametersRepository.GetTimeParametersDefault(IndigoSessionValues.IndigoOperatingUnitId)
                If GlossParameter.Id = 0 Then
                    Return New ActionResult With {.StateResult = False, .MessageResult = {"No se encontrarón parametros de glosas"}.ToList()}
                End If
                Dim FreeInvoice As Boolean
                If Injustificate = True Then
                    If GlossParameter.DevolutionInjustificate = "3" Then '3) definido por el usuario
                        If UserFreeInvoice = True Then ' y el usuario dijo que si se libera
                            FreeInvoice = True
                        Else
                            FreeInvoice = False 'si el usuario decidio no liberar
                        End If
                    Else
                        If GlossParameter.DevolutionInjustificate = "1" Then 'liberar factura del radicado 
                            FreeInvoice = True
                        ElseIf GlossParameter.DevolutionInjustificate = "2" Then 'mantener factura en el radicado ,NO  liberamos factura de radicado
                            FreeInvoice = False
                        End If
                    End If
                Else
                    FreeInvoice = True ' si es justificada, liberamos factura de radicado
                End If
                'inicio la transaccion
                Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
                    If FreeInvoice = True Then
                        Dim tmpDate As DateTime = DateTime.Now
                        For Each itemD As GlosaDevolutionsReceptionD In listDevolutionD

                            'pongo en estado sin radicar la factura que este en estado 6 cuota moderadora
                            Dim accountReceivableFeeModerator = _accountReceivableRepository.GetAccountReceivableByAdminssionNumberAndAccountReceivableType(itemD.InvoiceNumber, 6)
                            If accountReceivableFeeModerator IsNot Nothing Then
                                accountReceivableFeeModerator.PortfolioStatus = 1 'sin radicar
                                _accountReceivableRepository.SaveEntity(accountReceivableFeeModerator)
                                _accountReceivableRepository.UnitWork.Commit()
                            End If

                            Dim _AccountReceivable As AccountReceivable = _accountReceivableRepository.GetAccountReceivableByInvoiceNumberGloss(itemD.InvoiceNumber) 'consultamos factura en cartera
                            'Actualizamos estado en cartera
                            With _AccountReceivable
                                .PortfolioStatus = 1 ' sin radicar 
                            End With
                            _accountReceivableRepository.SaveEntity(_AccountReceivable)
                            If _AccountReceivable IsNot Nothing AndAlso _AccountReceivable.AccountRadicateId = 0 Then
                                ListStrMessage.Add("La cuenta Radicada esta vacia para la factura : " & _AccountReceivable.InvoiceNumber)
                                Return New ActionResult With {.StateResult = False, .MessageResult = ListStrMessage}
                            End If
                            'actualizo estructura cuenta de cobro Radicada
                            Dim _AccountReceivableAccountingRadicate As AccountReceivableAccounting = _AccountReceivableAccountingRepository.GetAccountReceivableAccounting(_AccountReceivable.Id, _AccountReceivable.AccountRadicateId)
                            If _AccountReceivableAccountingRadicate.Id = 0 Then
                                ListStrMessage.Add("Estructura de cartera no existe para la cuenta por cobrar: " & _AccountReceivable.Code & " - cuenta contable de factura radicada ")
                                Return New ActionResult With {.StateResult = False, .MessageResult = ListStrMessage}
                            End If
                            If _AccountReceivableAccountingRadicate.Balance <> itemD.BalanceInvoice Then
                                ListStrMessage.Add(String.Format("El saldo de la factura en estado radicado({0:C2}) es diferente al valor a reversar({1:C2})", _AccountReceivableAccountingRadicate.Balance, itemD.BalanceInvoice))
                                Return New ActionResult With {.StateResult = False, .MessageResult = ListStrMessage}
                            End If

                            With _AccountReceivableAccountingRadicate
                                .Balance = 0
                            End With
                            _AccountReceivableAccountingRepository.SaveEntity(_AccountReceivableAccountingRadicate)
                            'actualizo estructura cuenta de cobro Sin Radicada
                            Dim _AccountReceivableAccountingWithoutRadicate As AccountReceivableAccounting = _AccountReceivableAccountingRepository.GetAccountReceivableAccounting(_AccountReceivable.Id, _AccountReceivable.AccountWithoutRadicateId)
                            If _AccountReceivableAccountingWithoutRadicate.Id = 0 Then
                                'ListStrMessage.Add("Estructura de cartera no existe para la cuenta por cobrar: " & _AccountReceivable.Code & " - cuenta contable de factura sin radicada ")
                                'Return New ActionResult With {.StateResult = False, .MessageResult = ListStrMessage}
                                'NOTA
                                'generamos nueva estructura de cuenta de cobro, por si en la subida de saldos iniciales no se creo la estructura contable de cuenta sin radicar.
                                With _AccountReceivableAccountingWithoutRadicate
                                    .AccountReceivableId = _AccountReceivable.Id
                                    .MainAccountId = _AccountReceivable.AccountWithoutRadicateId 'Id Cuenta sin radicadar 
                                    .ThirdPartyId = _AccountReceivable.ThirdPartyId
                                    .CostCenterId = _AccountReceivable.CostCenterId
                                    .Value = itemD.BalanceInvoice
                                    .Balance = itemD.BalanceInvoice
                                End With
                            End If
                            With _AccountReceivableAccountingWithoutRadicate
                                .Balance = itemD.BalanceInvoice
                            End With
                            _AccountReceivableAccountingRepository.SaveEntity(_AccountReceivableAccountingWithoutRadicate)
                            'creamos documento de reclasificacion
                            TmpReclassificationResult = _InterfaceNativeAdminservice.CreatePortfolioReclassification(TypePortfolioReclassification.Devolution, idSequence, _AccountReceivable, itemD.BalanceInvoice, _AccountReceivable.ThirdPartyId, GlossParameter, IndigoSessionValues.AuditMessageWcf)
                            If TmpReclassificationResult.StateResult = False Then
                                scope.Dispose()
                                Return TmpReclassificationResult
                            End If
                            'cambiamos estado en RadicateD
                            Dim _RadicateD As RadicateInvoiceD = _RadicateInvoiceDRepository.GetRadicateD(itemD.InvoiceNumber)
                            With _RadicateD
                                .State = 4 'anulado
                            End With
                            _RadicateInvoiceDRepository.SaveEntity(_RadicateD)
                            Dim DevolutionD As GlosaDevolutionsReceptionD = _DevolutionDRepository.GetDevolutionDById(itemD.Id)
                            With DevolutionD
                                .State = 2
                                For Each itemmodev As GlosaMovementDevolutions In .GlosaMovementDevolutions
                                    Dim _result = ValidateRadicated(itemmodev)
                                    If _result Is Nothing OrElse Not _result?.StateResult Then
                                        unitOfWorkDevolutionD.RollbackChanges()
                                        scope.Dispose()
                                        Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({_result?.Message})}
                                    End If
                                    itemmodev.ModificationUser = IndigoSessionValues.AuditMessageWcf.CodeUser
                                    itemmodev.ModificationDate = Date.Now()
                                    itemmodev.State = 2
                                Next
                            End With
                            _DevolutionDRepository.SaveEntity(DevolutionD)
                        Next
                        RadicateDunitOfWork.Commit()
                        unitOfWorkDevolutionD.Commit()
                        AccountReceivableunitOfWork.Commit()
                        AccountReceivableAccountingunitOfWork.Commit()
                        'confirmo la transaccion
                        scope.Complete()
                        resultTotal = New ActionResult With {.StateResult = True, .MessageResult = TmpReclassificationResult.MessageResult}
                        Return resultTotal
                    Else
                        'si no liberamos factura, solo cambiamos estado de factura
                        For Each itemD As GlosaDevolutionsReceptionD In listDevolutionD
                            Dim DevolutionD As GlosaDevolutionsReceptionD = _DevolutionDRepository.GetDevolutionDById(itemD.Id)
                            DevolutionD.State = 2
                            For Each itemmodev As GlosaMovementDevolutions In DevolutionD.GlosaMovementDevolutions
                                Dim _result = ValidateRadicated(itemmodev)
                                If _result Is Nothing OrElse Not _result?.StateResult Then
                                    unitOfWorkDevolutionD.RollbackChanges()
                                    scope.Dispose()
                                    Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({_result?.Message})}
                                End If
                                itemmodev.ModificationUser = IndigoSessionValues.AuditMessageWcf.CodeUser
                                itemmodev.ModificationDate = Date.Now()
                                itemmodev.State = 2
                            Next
                            _DevolutionDRepository.SaveEntity(DevolutionD)
                            unitOfWorkDevolutionD.Commit()
                        Next
                        'confirmo la transaccion
                        scope.Complete()
                        resultTotal = New ActionResult With {.StateResult = True}
                        Return resultTotal
                    End If
                End Using
            End If

            '/***** Auditoria Basica ********/
            For Each itemD As GlosaDevolutionsReceptionD In listDevolutionD
                IndigoAuditBasic.Execute("GlosaDevolutionsReceptionD", IndigoSessionValues.AuditMessageWcf.Functional, itemD.Id, IndigoSessionValues.AuditMessageWcf.NameUser, IndigoSessionValues.AuditMessageWcf.CodeUser, IndigoSessionValues.AuditMessageWcf.WindowsUser, DateTime.Now, ActionsAudit.Confirmar, IndigoSessionValues.AuditMessageWcf.Company, IndigoSessionValues.AuditMessageWcf.ContainerSecurity)
            Next
            Return resultTotal
        Catch ex As IndigoValidationException
            unitOfWorkDevolutionD.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .Message = ex.Message}
        Catch ex As OptimisticConcurrencyException
            unitOfWorkDevolutionD.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As Exception
            unitOfWorkDevolutionD.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", IndigoSessionValues)
            Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' funcion para validar campos de la tabla GlosaMovementDevolutions
    ''' </summary>
    ''' <param name="GlosaMDevolutions"></param>
    ''' <returns></returns>
    Private Function ValidateRadicated(GlosaMDevolutions As GlosaMovementDevolutions) As ActionResult
        Try
            If GlosaMDevolutions Is Nothing Then
                Return New ActionResult With {.StateResult = False, .Message = "Objeto vacio"}
            End If
            Dim StringBuilder = New StringBuilder()
            If GlosaMDevolutions.IdConceptGlosa = 0 Then
                StringBuilder.AppendLine("Concepto de la glosa")
            End If
            If String.IsNullOrEmpty(GlosaMDevolutions.Answer) Then
                StringBuilder.AppendLine("La respuesta IPS esta Vacia")
            End If
            If String.IsNullOrEmpty(GlosaMDevolutions.Comment) Then
                StringBuilder.AppendLine("El motivo EAPB esta vacio")
            End If

            Return New ActionResult With {.StateResult = Not StringBuilder.Length > 0, .Message = $"Se presentaron las siguientes validaciones de la Factura {GlosaMDevolutions.InvoiceNumber} :{StringBuilder.ToString}"}

        Catch ex As Exception
            Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Private Function ExecuteNotaContable(ByVal listDevolutionD As List(Of GlosaDevolutionsReceptionD), ByVal objParameter As GlosasParametersInterface, ByVal IndigoSessionValues As SessionValues) As List(Of InterfaceResult)
        Dim result As New List(Of InterfaceResult)
        'si se aplica interfaz para la empresa
        If objParameter.Interface = True Then
            Dim CodEmpresaDGH As String = objParameter.ContainerName
            Dim IntOpcion As String = "DEVOLUCION"
            Dim User As String = IndigoSessionValues.UserInterface
            If objParameter.AccountingMethod = eTypeInterface.FoxPrivate Then
                result = _InterfaceFox.Devolucion(IndigoSessionValues.TransactionalContainer, listDevolutionD, CodEmpresaDGH, IntOpcion, User)
            ElseIf objParameter.AccountingMethod = eTypeInterface.FoxPublic Then
                result = _InterfacePublicFOX.Devolucion(IndigoSessionValues.TransactionalContainer, listDevolutionD, CodEmpresaDGH, IntOpcion, User)
            ElseIf objParameter.AccountingMethod = eTypeInterface.NETPublic Then
                result = _InterfacePublicNET.Devolucion(IndigoSessionValues.TransactionalContainer, listDevolutionD, CodEmpresaDGH, IntOpcion, User)
            ElseIf objParameter.AccountingMethod = eTypeInterface.NETPrivate Then
                result = _InterfaceNET.Devolucion(IndigoSessionValues.TransactionalContainer, listDevolutionD, CodEmpresaDGH, IntOpcion, User)
            End If
        Else
            result.Add(New InterfaceResult With {.Result = False, .Message = "No esta activa la configuración de interfaz contable para la empresa " & objParameter.CompanyName})
        End If
        Return result
    End Function


    ''' <summary>
    ''' Función para validar y agregar facturas desde el sp
    ''' </summary>
    ''' <param name="ListInvoices">Lista de facturas</param>
    ''' <param name="Nit">Numero de Nit</param>
    ''' <param name="container">nombre Contenedor</param>
    ''' <returns>Lista de Factura </returns>
    Public Function ValidateListInvoiceDevolutionSp(ListInvoices As List(Of String), Nit As String, container As String, IndigoCompany As String, Session As SessionValues) As ActionResult(Of List(Of GlosaDevolutionsReceptionD)) Implements IMovementDevolutionsAdminService.ValidateListInvoiceDevolutionSp
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
        Dim _Result As New ActionResult(Of List(Of GlosaDevolutionsReceptionD))
        Dim _listError As New List(Of String)
        Dim ListGlosaDevolutionsReceptionD As New List(Of GlosaDevolutionsReceptionD)
        Try
            If Session.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                _Result = ExcelIntegration(ListInvoices, Nit, container, IndigoCompany, Session)
            ElseIf Session.IndigoGlossesIntegration = EGlossesIntegration.Native Or Session.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNet Or Session.IndigoGlossesIntegration = EGlossesIntegration.IntegractionFox Or Session.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNATIVE_MIGRATIONS Then
                _Result = ExcelNativeDevolution(ListInvoices, Nit, IndigoCompany, Session)
            End If
            Return _Result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function


    ''' <summary>
    ''' Lista de devoluciones para cargue desde excel en modo nativo
    ''' </summary>
    ''' <param name="ListInvoices"></param>
    ''' <param name="Nit"></param>
    ''' <param name="IndigoCompany"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ExcelNativeDevolution(ByVal ListInvoices As List(Of String), Nit As String, IndigoCompany As String, session As SessionValues) As ActionResult(Of List(Of GlosaDevolutionsReceptionD))
        Dim _Result As New ActionResult(Of List(Of GlosaDevolutionsReceptionD))
        Dim _listError As New List(Of String)
        Dim ListGlosaDevolutionsReceptionD As New List(Of GlosaDevolutionsReceptionD)
        For Each item As String In ListInvoices
            Dim StrMensaje As String = String.Empty
            Dim itemInvoice As New SP_invoiceList_Result
            itemInvoice = _ObjectionsReceptionCRepository.GetInvoice(String.Empty, Nit, item, IndigoCompany, session.HisContainer, String.Empty, 2) '0=recepcion, - 1 radicacion cunetas de cobro -  2 = devolucion
            'si no existe la cuenta en configuracion 
            If itemInvoice IsNot Nothing AndAlso itemInvoice.InvoiceNumber IsNot Nothing AndAlso Not itemInvoice.InvoiceNumber.Trim().Equals(String.Empty) Then
                If itemInvoice.StateCurrentInvoice = "3" Then
                    'declaro variable  tipo detalle factura
                    Dim _TmpObjectionsReceptionD As New GlosaDevolutionsReceptionD
                    With _TmpObjectionsReceptionD
                        .InvoiceNumber = itemInvoice.InvoiceNumber.Trim
                        .BalanceInvoice = itemInvoice.BalanceInvoice.ToString.Trim
                        .InvoiceDate = itemInvoice.InvoiceDate
                        .RadicatedNumber = itemInvoice.RadicatedNumber.Trim
                        .RadicatedDate = itemInvoice.RadicatedDate
                        .PatientCode = itemInvoice.PatientCode.Trim
                        .PatientName = itemInvoice.PatientName.Trim
                        .UserNameInvoice = itemInvoice.UserNameInvoice.Trim
                        .ContractCode = itemInvoice.ContractCode.Trim
                        .ContractName = itemInvoice.ContractName.Trim
                        .PlanCode = itemInvoice.CodePlan
                        .State = 1 'sin confirmar
                        .Ingress = itemInvoice.IngressNumber
                    End With
                    ListGlosaDevolutionsReceptionD.Add(_TmpObjectionsReceptionD)
                Else
                    Select Case itemInvoice.StateCurrentInvoice
                        Case "1"
                            StrMensaje = itemInvoice.InvoiceNumber.ToString + " Factura sin radicar "
                        Case "2"
                            StrMensaje = itemInvoice.InvoiceNumber.ToString + " Factura radicada sin confirmar "
                        Case "7"
                            StrMensaje = itemInvoice.InvoiceNumber.ToString + " Factura certificada parcial "
                        Case "8"
                            StrMensaje = itemInvoice.InvoiceNumber.ToString + " Factura certificada total "
                        Case "14"
                            StrMensaje = itemInvoice.InvoiceNumber.ToString + " Factura devuelta "
                        Case "15"
                            StrMensaje = itemInvoice.InvoiceNumber.ToString + " Factura en cuenta de dificil recaudo "
                        Case "16"
                            StrMensaje = itemInvoice.InvoiceNumber.ToString + " Factura en cobro jurídico "
                    End Select
                    _listError.Add(StrMensaje)
                End If
            Else
                StrMensaje = item + " Factura No Existe o se encuentra en otro proceso "
                _listError.Add(StrMensaje)
            End If
        Next
        _Result.MessageResult = _listError
        _Result.StateResult = True
        _Result.ObjectEmbbeded = ListGlosaDevolutionsReceptionD
        Return _Result
    End Function

    ''' <summary>
    ''' Lista de devoluciones para cargue desde excel en integracion con otro ERP
    ''' </summary>
    ''' <param name="ListInvoices"></param>
    ''' <param name="Nit"></param>
    ''' <param name="container"></param>
    ''' <param name="IndigoCompany"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ExcelIntegration(ByVal ListInvoices As List(Of String), Nit As String, container As String, IndigoCompany As String, session As SessionValues) As ActionResult(Of List(Of GlosaDevolutionsReceptionD))
        Dim _Result As New ActionResult(Of List(Of GlosaDevolutionsReceptionD))
        Dim _listError As New List(Of String)
        Dim ListGlosaDevolutionsReceptionD As New List(Of GlosaDevolutionsReceptionD)
        Dim ObjInterfaceParameter As GlosasParametersInterface = _IInterfaceParametersRepository.GetInterfacesParameters(container)
        For Each item As String In ListInvoices
            Dim StrMensaje As String = String.Empty
            Dim itemInvoice As New SP_invoiceList_Result
            Dim AccountValidate As Boolean
            itemInvoice = _ObjectionsReceptionCRepository.GetInvoice(container, Nit, item, IndigoCompany, session.HisContainer, String.Empty, 2) '0=recepcion, - 1 radicacion cunetas de cobro -  2 = devolucion
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
            'si no existe la cuenta en configuracion 
            If itemInvoice IsNot Nothing AndAlso itemInvoice.InvoiceNumber IsNot Nothing AndAlso Not itemInvoice.InvoiceNumber.Trim().Equals(String.Empty) Then
                If AccountValidate = True Then
                    If itemInvoice.StateCurrentInvoice = "2" Or itemInvoice.StateCurrentInvoice = "4" Then
                        'para tener encuenta las facturas para reiteracion
                        If itemInvoice.RadicatedConsecutive Is Nothing Then
                            'declaro variable  tipo detalle factura
                            Dim _TmpObjectionsReceptionD As New GlosaDevolutionsReceptionD
                            With _TmpObjectionsReceptionD
                                .InvoiceNumber = itemInvoice.InvoiceNumber.Trim
                                .BalanceInvoice = itemInvoice.BalanceInvoice.ToString.Trim
                                .InvoiceDate = itemInvoice.InvoiceDate
                                .RadicatedNumber = itemInvoice.RadicatedNumber.Trim
                                .RadicatedDate = itemInvoice.RadicatedDate
                                .PatientCode = itemInvoice.PatientCode.Trim
                                .PatientName = itemInvoice.PatientName.Trim
                                .UserNameInvoice = itemInvoice.UserNameInvoice.Trim
                                .ContractCode = itemInvoice.ContractCode.Trim
                                .ContractName = itemInvoice.ContractName.Trim
                                .PlanCode = itemInvoice.CodePlan
                                .State = 1 'sin confirmar
                                .Ingress = itemInvoice.IngressNumber
                            End With
                            ListGlosaDevolutionsReceptionD.Add(_TmpObjectionsReceptionD)
                        Else
                            StrMensaje = itemInvoice.InvoiceNumber.ToString + " Factura Radicada  en el oficio de devolución " + itemInvoice.RadicatedConsecutive.ToString
                            _listError.Add(StrMensaje)
                        End If
                    Else
                        If ObjInterfaceParameter.AccountingMethod = eTypeInterface.FoxPrivate Or ObjInterfaceParameter.AccountingMethod = eTypeInterface.FoxPublic Then
                            Select Case itemInvoice.StateCurrentInvoice
                                Case "1"
                                    StrMensaje = itemInvoice.InvoiceNumber.ToString + " Factura Sin Radicar "
                                Case "T"
                                    StrMensaje = itemInvoice.InvoiceNumber.ToString + " Factura Radicada Sin Confirmar "
                                Case "6"
                                    StrMensaje = itemInvoice.InvoiceNumber.ToString + " Factura Anulada "
                            End Select
                            _listError.Add(StrMensaje)
                        ElseIf ObjInterfaceParameter.AccountingMethod = eTypeInterface.NETPrivate Or ObjInterfaceParameter.AccountingMethod = eTypeInterface.NETPublic Then
                            Select Case itemInvoice.StateCurrentInvoice
                                Case "1"
                                    StrMensaje = itemInvoice.InvoiceNumber.ToString + " Factura Sin Radicar "
                                Case "3"
                                    StrMensaje = itemInvoice.InvoiceNumber.ToString + " Factura Objetada "
                                Case "4"
                                    StrMensaje = itemInvoice.InvoiceNumber.ToString + " Factura Contestada Radicada "
                                Case "5"
                                    StrMensaje = itemInvoice.InvoiceNumber.ToString + " Factura Aceptada "
                                Case "6"
                                    StrMensaje = itemInvoice.InvoiceNumber.ToString + " Factura Certificada Parcial "
                                Case "7"
                                    StrMensaje = itemInvoice.InvoiceNumber.ToString + " Factura Certificada Total "
                                Case "8"
                                    StrMensaje = itemInvoice.InvoiceNumber.ToString + " No subsanable "
                                Case "9"
                                    StrMensaje = itemInvoice.InvoiceNumber.ToString + " Difícil Recaudo "
                            End Select
                            _listError.Add(StrMensaje)
                        End If
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
        _Result.ObjectEmbbeded = ListGlosaDevolutionsReceptionD
        Return _Result
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
                _InterfaceNET.Dispose()
            End If
            _ObjectionsReceptionCRepository = Nothing
            _MovementDevolutionRepository = Nothing
            _DevolutionDRepository = Nothing
            _IInterfaceParametersRepository = Nothing
            _InterfaceFox = Nothing
            _InterfacePublicFOX = Nothing
            _InterfacePublicNET = Nothing
            _accountReceivableRepository = Nothing
            _careGroupRepository = Nothing
            _AccountReceivableAccountingRepository = Nothing
            _RadicateInvoiceDRepository = Nothing
            _InterfaceNativeAdminservice = Nothing
            _InterfaceNET = Nothing
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