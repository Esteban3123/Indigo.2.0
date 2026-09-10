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
Imports Infrastructure.CrossCutting.Resources

Public Class PartialPaymentsCAdminService
    Implements IPartialPaymentsCAdminService


    Private _ConsecutiveRepository As IConsecutiveRepository
    Private _PartialPaymentsCRepository As IPartialPaymentsCRepository
    Private _PartialPaymentsDRepository As IPartialPaymentsDRepository
    Private _PartialPaymentsMovementRepository As IPartialPaymentsMovementRepository
    Private _MovementGlosaRepository As IMovementGlosaRepository
    Private _PortfolioGlosadaRepository As IPortfolioGlosadaRepository
    Private _IInterfaceParametersRepository As IInterfaceParametersRepository
    Private _InterfaceFox As IInterfaceFOX
    Private _InterfaceNet As IInterfaceNET


    Public Sub New(ByVal ConsecutiveRepository As IConsecutiveRepository, ByVal PartialPaymentsCRepository As IPartialPaymentsCRepository, ByVal PartialPaymentsDRepository As IPartialPaymentsDRepository, ByVal PartialPaymentsMovementRepository As IPartialPaymentsMovementRepository, ByVal PortfolioGlosadaRepository As IPortfolioGlosadaRepository, MovementGlosaRepository As IMovementGlosaRepository, IInterfaceParametersRepository As IInterfaceParametersRepository, InterfacePublicFOX As IInterfacePublicFOX, InterfaceFox As IInterfaceFOX, InterfaceNET As IInterfaceNET)
        If PartialPaymentsCRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de cabecera pagos parciales Vacio")
        End If
        If PartialPaymentsDRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de detalle pagos parciales Vacio")
        End If
        If PartialPaymentsMovementRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de movimientos pagos parciales Vacio")
        End If
        If PortfolioGlosadaRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de catera glosa Vacio")
        End If
        If MovementGlosaRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Movimiento Factura Vacio")
        End If
        If IInterfaceParametersRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de configuración interface Vacio")
        End If
        If InterfacePublicFOX Is Nothing Then
            Throw New ArgumentNullException("Repositorio de interfaz contable Fox - publico Vacio")
        End If
        If InterfaceFox Is Nothing Then
            Throw New ArgumentNullException("Repositorio de interfaz contable Fox - privado Vacio")
        End If
        If InterfaceNET Is Nothing Then
            Throw New ArgumentNullException("Repositorio de interfaz contable Net - privado Vacio")
        End If

        _PartialPaymentsCRepository = PartialPaymentsCRepository
        _PartialPaymentsDRepository = PartialPaymentsDRepository
        _PortfolioGlosadaRepository = PortfolioGlosadaRepository
        _PartialPaymentsMovementRepository = PartialPaymentsMovementRepository
        _MovementGlosaRepository = MovementGlosaRepository
        _ConsecutiveRepository = ConsecutiveRepository
        _IInterfaceParametersRepository = IInterfaceParametersRepository
        _InterfaceFox = InterfaceFox
        _InterfaceNet = InterfaceNET
    End Sub



    Public Function ConfirmPartialPaymentsC(PartialPaymentsC As PartialPaymentsC, IndigoSessionValues As SessionValues) As ActionResult(Of PartialPaymentsC) Implements IPartialPaymentsCAdminService.ConfirmPartialPaymentsC
        If PartialPaymentsC Is Nothing Then
            Throw New ArgumentNullException("Oficio Radicado Vacio")
        End If
        Dim unitOfWorkPartialC As IUnitWork = _PartialPaymentsCRepository.UnitWork
        Dim unitOfWorkGlosaMovement As IUnitWork = _MovementGlosaRepository.UnitWork
        Dim unitofWorkPartialD As IUnitWork = _PartialPaymentsDRepository.UnitWork
        Dim unitofWorkPortfolio As IUnitWork = _PortfolioGlosadaRepository.UnitWork
        Try
            Dim ListGeneral As New List(Of String)

            Dim DateConfirm As Date = Date.Now
            PartialPaymentsC.ConfirmDateSystem = DateConfirm
            PartialPaymentsC.ModificationDate = DateConfirm
            PartialPaymentsC.ModificationUser = IndigoSessionValues.AuditMessageWcf.IdUser

            '    Dim result As New List(Of String)
            Dim AuxRadicateInvoiceC As PartialPaymentsC = Nothing
            If PartialPaymentsC.ChangeTracker.State = ObjectState.Modified Then
                AuxRadicateInvoiceC = PartialPaymentsC.OriginalValue
            End If

            'configuro la transaccion
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.DefaultTimeout
            txSettings.IsolationLevel = IsolationLevel.ReadCommitted

            Dim resultTotal As New ActionResult(Of PartialPaymentsC)
            Dim ListresultInterface As New List(Of InterfaceResult)
            Dim ListresultInterfaceERROR As New List(Of InterfaceResult)
            'inicio la transaccion
            Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
                For Each itemD As PartialPaymentsD In PartialPaymentsC.PartialPaymentsD
                    itemD.State = 2
                    Dim ValuePayments As Decimal = itemD.ValuePayments
                    'Cargamos configuraciones
                    Dim portfolio As GlosaPortfolioGlosada = _PortfolioGlosadaRepository.GetByFilter(Function(x) x.InvoiceNumber = itemD.InvoiceNumber, True, {"GlosaObjectionsReceptionD"}).FirstOrDefault
                    Dim listMOv As List(Of GlosaMovementGlosa) = _MovementGlosaRepository.ListAllMovementGlosaByInvoiceNumber(itemD.InvoiceNumber)

                    For Each itemmov As GlosaMovementGlosa In listMOv
                        'mientra el saldo de pago se mayor de cero
                        If itemmov.MainGlosa = True And itemmov.ValuePendingConciliation > 0 Then 'los principales y que tienen saldo
                            Dim tmpvaluepayment As Decimal = ValuePayments
                            ValuePayments = ValuePayments - itemmov.ValuePendingConciliation
                            Dim value As Decimal = 0
                            If ValuePayments <= 0 Then
                                value = tmpvaluepayment
                                itemmov.ValuePendingConciliation = itemmov.ValuePendingConciliation - tmpvaluepayment
                            ElseIf ValuePayments > 0 Then
                                value = itemmov.ValuePendingConciliation
                                itemmov.ValuePendingConciliation = 0
                            End If
                            itemmov.TempState = itemmov.State
                            itemmov.State = "7" 'por pago parcial 
                            itemmov.ValuePayments = IIf(itemmov.ValuePayments Is Nothing, 0, itemmov.ValuePayments) + value 'acumulamos en movimientos de glosa el pago parcial
                            Dim MovPayments As New PartialPaymentsMovement
                            With MovPayments
                                .PartialPaymentsDId = itemD.Id
                                .GlosaMovementGlosaId = itemmov.Id
                                .ValueEAPB = value
                                .CreationUser = IndigoSessionValues.AuditMessageWcf.CodeUser
                                .CreationDate = DateConfirm
                            End With
                            itemD.PartialPaymentsMovement.Add(MovPayments)
                            _MovementGlosaRepository.SaveEntity(itemmov)
                        End If
                        If ValuePayments <= 0 Then
                            Exit For
                        End If
                    Next ' fin ciclo de moviminetos

                    portfolio.TempState = portfolio.State
                    portfolio.State = 14 'confirmado pago parcial
                    portfolio.ValuePayments = portfolio.ValuePayments + itemD.ValuePayments 'acumulamos pagos parciales
                    portfolio.BalanceGlosa = portfolio.BalanceGlosa - itemD.ValuePayments 'el saldo actual en cartera = saldo - valor de pago parcial
                    _PortfolioGlosadaRepository.SaveEntity(portfolio)

                    If IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                        Dim objParameter As GlosasParametersInterface = _IInterfaceParametersRepository.GetInterfacesParametersById(portfolio.GlosaObjectionsReceptionD(0).GlosasParametersInterfaceId)
                        Dim resultInteface As InterfaceResult
                        'variable para almacenar la lista de mensaje a retornar
                        Dim _Listmessage As New List(Of String)
                        Dim NumeroGlosa As String = PartialPaymentsC.RadicatedConsecutive
                        Dim Factura As String = itemD.InvoiceNumber
                        Dim Tercero As String = PartialPaymentsC.Customer.Nit.Trim
                        Dim CodEmpresaDGH As String = objParameter.ContainerName
                        Dim Anio As Integer = Year(itemD.InvoiceDate)

                        Dim ValorFactura As Decimal = itemD.ValuePayments 'ObjectionsReceptionD.GlosaPortfolioGlosada.ValueAcceptedEAPBconciliation
                        Dim IntOpcion As String = "PAGOPARCIAL"
                        Dim User As String = IndigoSessionValues.UserInterface
                        If objParameter.AccountingMethod = eTypeInterface.FoxPrivate Then
                            resultInteface = _InterfaceFox.AcceptanceEAPB(IndigoSessionValues.TransactionalContainer, NumeroGlosa, Factura, Tercero, CodEmpresaDGH, ValorFactura, Anio, IntOpcion, User)
                            If resultInteface.Result = True Then
                                ListresultInterface.Add(resultInteface)
                            Else
                                ListresultInterfaceERROR.Add(resultInteface)
                            End If
                        ElseIf objParameter.AccountingMethod = eTypeInterface.NETPrivate Then
                            resultInteface = _InterfaceNet.AcceptanceEAPB(IndigoSessionValues.TransactionalContainer, NumeroGlosa, Factura, Tercero, CodEmpresaDGH, ValorFactura, Anio, IntOpcion, User)
                            If resultInteface.Result = True Then
                                ListresultInterface.Add(resultInteface)
                            Else
                                ListresultInterfaceERROR.Add(resultInteface)
                            End If
                        End If
                    End If
                Next 'ciclo de factura

                If ListresultInterfaceERROR.Count = 0 Then
                    _PartialPaymentsCRepository.SaveEntity(PartialPaymentsC) 'actualizo cambios cabecera
                    unitOfWorkGlosaMovement.Commit() 'confirmo cambios en movimientos glosa
                    unitofWorkPortfolio.Commit() 'confirmo cambios en cartera
                    unitOfWorkPartialC.Commit() 'actualizo cambios en la cabecera de pago parcial
                    'confirmo la transaccion
                    scope.Complete()
                Else
                    unitOfWorkGlosaMovement.RollbackChanges()
                    unitofWorkPartialD.RollbackChanges()
                    unitOfWorkPartialC.RollbackChanges()
                    unitofWorkPortfolio.RollbackChanges()
                    scope.Dispose()
                End If
            End Using

            If IndigoSessionValues.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                Dim LisStrMessafue As New List(Of String)
                If ListresultInterfaceERROR.Count = 0 Then
                    If ListresultInterface.Count > 0 Then
                        For Each item As InterfaceResult In ListresultInterface
                            LisStrMessafue.Add(item.Message.ToString)
                        Next
                        resultTotal = New ActionResult(Of PartialPaymentsC) With {.StateResult = True, .MessageResult = LisStrMessafue}
                    End If
                Else
                    For Each item As InterfaceResult In ListresultInterfaceERROR
                        LisStrMessafue.Add(item.Message.ToString)
                    Next
                    resultTotal = New ActionResult(Of PartialPaymentsC) With {.StateResult = False, .MessageResult = LisStrMessafue}
                End If
            Else
                resultTotal = New ActionResult(Of PartialPaymentsC) With {.StateResult = True, .MessageResult = {"Se confirmo correctamente"}.ToList()}
            End If
            Return resultTotal
        Catch ex As OptimisticConcurrencyException
            unitOfWorkGlosaMovement.RollbackChanges()
            unitofWorkPartialD.RollbackChanges()
            unitOfWorkPartialC.RollbackChanges()
            unitofWorkPortfolio.RollbackChanges()
            Return New ActionResult(Of PartialPaymentsC) With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As Exception
            unitOfWorkGlosaMovement.RollbackChanges()
            unitofWorkPartialD.RollbackChanges()
            unitOfWorkPartialC.RollbackChanges()
            unitofWorkPortfolio.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", IndigoSessionValues)
            Dim Message As String = ex.Message
            If ex.InnerException?.Message IsNot Nothing Then
                Message = ex.InnerException.Message
            End If
            Return New ActionResult(Of PartialPaymentsC) With {.StateResult = False, .MessageResult = New List(Of String)({Message})}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un oficio de pago parcial
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPartialPaymentsC(consecutive As String, audit As AuditMessage) As ActionResult(Of PartialPaymentsC) Implements IPartialPaymentsCAdminService.GetPartialPaymentsC
        If String.IsNullOrEmpty(consecutive) = True Then
            Throw New ArgumentNullException("consecutivo de pago parcial Vacio")
        End If
        Dim _result As New ActionResult(Of PartialPaymentsC)
        Try
            Dim objtmp = _PartialPaymentsCRepository.GetPartialPaymentsC(consecutive)
            _result.StateResult = True
            _result.ObjectEmbbeded = objtmp
            Return _result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            _result.StateResult = False
            _result.MessageResult = New List(Of String)({ex.Message})
        End Try
    End Function

    ''' <summary>
    ''' guardar y actualiza pago parcial
    ''' </summary>
    ''' <param name="PartialPaymentsC"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SavePartialPaymentsC(PartialPaymentsC As PartialPaymentsC, audit As AuditMessage) As ActionResult(Of PartialPaymentsC) Implements IPartialPaymentsCAdminService.SavePartialPaymentsC
        If PartialPaymentsC Is Nothing Then
            Throw New ArgumentNullException("Oficio de pago parcial Vacio")
        End If
        Dim unitOfWork As IUnitWork = _PartialPaymentsCRepository.UnitWork
        Dim unitOfWorkConsecutive As IUnitWork = _ConsecutiveRepository.UnitWork
        Dim unitofWorkPortfolio As IUnitWork = _PortfolioGlosadaRepository.UnitWork

        Dim AuxPartialPaymentsC As PartialPaymentsC = Nothing
        If PartialPaymentsC.ChangeTracker.State = ObjectState.Modified Or PartialPaymentsC.PartialPaymentsD.ToList().Exists(Function(item As PartialPaymentsD) item.ChangeTracker.State = ObjectState.Added Or item.ChangeTracker.State = ObjectState.Modified) = True Then
            AuxPartialPaymentsC = PartialPaymentsC.OriginalValue
        End If

        'variable para retornar un ActionResult 
        Dim _actionResult As ActionResult(Of PartialPaymentsC) = New ActionResult(Of PartialPaymentsC)
        'asigno la lista generica de errores al objeto final de retorno
        _actionResult.MessageResult = New List(Of String)

        Dim auditProcess As IndigoAuditSimpleEntity(Of PartialPaymentsC)
        Dim status As Integer

        Try
            'configuro la transaccion
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.DefaultTimeout
            txSettings.IsolationLevel = IsolationLevel.ReadCommitted

            If _actionResult.MessageResult.Count <= 0 Then
                _actionResult.StateResult = True
                Dim consecutive As Domain.Entities.Consecutive = Nothing
                If PartialPaymentsC.ChangeTracker.State = ObjectState.Added Then
                    'Actualizamos el numero de consecutivo
                    consecutive = _ConsecutiveRepository.GetConsecutiveByCode("10")  'PagosParciales
                    If consecutive.Id = 0 Then
                        Return New ActionResult(Of PartialPaymentsC) With {.StateResult = False, .MessageResult = {"No se encontró consecutivo con código 10 para pagos parciales"}.ToList()}
                    End If
                    consecutive.NumberConsecutive += 1
                    _ConsecutiveRepository.SaveEntity(consecutive)
                    unitOfWorkConsecutive.Commit()
                End If

                'inicio la transaccion
                Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
                    'grabo la cabecera de la objecion, el detalle "lista facturas" y el detalle de cada factura 
                    If PartialPaymentsC.ChangeTracker.State = ObjectState.Added Then
                        'si es nuevo incrementamos el consecutivo
                        PartialPaymentsC.RadicatedConsecutive = CInt(consecutive.NumberConsecutive)
                        PartialPaymentsC.CreationUser = audit.CodeUser
                        PartialPaymentsC.CreationDate = Date.Now()
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert
                        _PartialPaymentsCRepository.SaveEntity(PartialPaymentsC)
                        'confirmo la unidad de trabajo  la cabecera de la objecion
                        unitOfWork.Commit()
                        auditProcess = New IndigoAuditSimpleEntity(Of PartialPaymentsC)(PartialPaymentsC, audit, status)
                        auditProcess.Execute()
                    ElseIf PartialPaymentsC.ChangeTracker.State = ObjectState.Modified Or PartialPaymentsC.PartialPaymentsD.ToList().Exists(Function(item As PartialPaymentsD) item.ChangeTracker.State = ObjectState.Added Or item.ChangeTracker.State = ObjectState.Modified) = True Then
                        PartialPaymentsC.ModificationUser = audit.CodeUser
                        PartialPaymentsC.ModificationDate = Date.Now()
                        status = Infrastructure.CrossCutting.Audit.Actions.Update
                        'actualizo la entidad
                        _PartialPaymentsCRepository.SaveEntity(PartialPaymentsC)
                        'confirmo la unidad de trabajo  la cabecera de la objecion
                        unitOfWork.Commit()
                        auditProcess = New IndigoAuditSimpleEntity(Of PartialPaymentsC)(PartialPaymentsC, audit, status, AuxPartialPaymentsC)
                        auditProcess.Execute()
                    End If

                    For Each item As PartialPaymentsD In PartialPaymentsC.PartialPaymentsD
                        If item.ChangeTracker.State = ObjectState.Added Then
                            'actualizamos estado en cartera, marcamos esa factura en estado '13 pendiente confirmar pago parcial
                            Dim InvoicePortfolio As GlosaPortfolioGlosada = _PortfolioGlosadaRepository.GetPortfolioGlosada(item.InvoiceNumber)
                            InvoicePortfolio.TempState = InvoicePortfolio.State
                            InvoicePortfolio.State = 13 'pendiente confirmar pago parcial
                            _PortfolioGlosadaRepository.SaveEntity(InvoicePortfolio)
                        End If
                    Next
                    unitofWorkPortfolio.Commit()
                    'confirmo la transaccion
                    scope.Complete()
                End Using

                'variable lista de string para retornar el numero de factura y el id de la nueva recepción
                Dim Consecutivo As List(Of String) = New List(Of String)
                'agrego a la lista el id de la recepción
                Consecutivo.Add(PartialPaymentsC.Id.ToString)
                Consecutivo.Add(PartialPaymentsC.RadicatedConsecutive.ToString)
                'asignacion a la variable de retorno
                _actionResult.MessageResult = Consecutivo
            Else
                'En caso de error
                _actionResult.StateResult = False
            End If
            _actionResult.ObjectEmbbeded = PartialPaymentsC
            'retorno objeto de respuesta
            Return _actionResult
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of PartialPaymentsC) With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PartialPaymentsC) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Metodo para Guardar y Confirmar un pago parcial generado desde notas de tesoreria, esto cuando esta en modo NATIVO
    ''' </summary>
    ''' <param name="PartialPaymentsC">Objeto de Pago Parcial - Cabecera y detalle (Lista de Facturas) del pago</param>
    ''' <param name="IndigoSessionValues">Variable de Sesión</param>
    ''' <returns>ActionResult</returns>
    ''' <remarks></remarks>
    Public Function SaveAndConfirmGlossPaymentsC(PartialPaymentsC As PartialPaymentsC, IndigoSessionValues As SessionValues) As ActionResult(Of String) Implements IPartialPaymentsCAdminService.SaveAndConfirmGlossPaymentsC
        If PartialPaymentsC Is Nothing Then
            Throw New ArgumentNullException("PartialPaymentsC")
        End If
        Dim resultConfirm As ActionResult(Of PartialPaymentsC)
        Dim result = SavePartialPaymentsC(PartialPaymentsC, IndigoSessionValues.AuditMessageWcf)
        If result.StateResult = True And result.Message Is Nothing Then
            resultConfirm = ConfirmPartialPaymentsC(PartialPaymentsC, IndigoSessionValues)
            If resultConfirm.StateResult = False Then
                Return New ActionResult(Of String) With {.StateResult = False}
            End If
        Else
            If result.ObjectEmbbeded IsNot Nothing Then
                Return New ActionResult(Of String) With {.StateResult = False}
            Else
                Return New ActionResult(Of String) With {.StateResult = False}
            End If
        End If
        Return New ActionResult(Of String) With {.StateResult = True}
    End Function


    ''' <summary>
    ''' Funcion para Anular un oficio
    ''' </summary>
    ''' <param name="PartialPaymentsC"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InvalidatePaymentsC(PartialPaymentsC As PartialPaymentsC, audit As AuditMessage) As ActionResult(Of PartialPaymentsC) Implements IPartialPaymentsCAdminService.InvalidatePaymentsC
        If PartialPaymentsC Is Nothing Then
            Throw New ArgumentNullException("Oficio de pago parcial Vacio")
        End If
        Dim unitOfWork As IUnitWork = _PartialPaymentsCRepository.UnitWork
        Dim unitofWorkPortfolio As IUnitWork = _PortfolioGlosadaRepository.UnitWork

        PartialPaymentsC.State = 4 'anulo cabecera

        Dim AuxPartialPaymentsC As PartialPaymentsC = Nothing
        If PartialPaymentsC.ChangeTracker.State = ObjectState.Modified Or PartialPaymentsC.PartialPaymentsD.ToList().Exists(Function(item As PartialPaymentsD) item.ChangeTracker.State = ObjectState.Added Or item.ChangeTracker.State = ObjectState.Modified) = True Then
            AuxPartialPaymentsC = PartialPaymentsC.OriginalValue
        End If

        'variable para retornar un ActionResult 
        Dim _actionResult As ActionResult(Of PartialPaymentsC) = New ActionResult(Of PartialPaymentsC)
        'asigno la lista generica de errores al objeto final de retorno
        _actionResult.MessageResult = New List(Of String)

        Dim auditProcess As IndigoAuditSimpleEntity(Of PartialPaymentsC)
        Dim status As Integer

        Try
            'configuro la transaccion
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.DefaultTimeout
            txSettings.IsolationLevel = IsolationLevel.ReadCommitted

            If _actionResult.MessageResult.Count <= 0 Then
                _actionResult.StateResult = True

                'inicio la transaccion
                Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
                    'grabo la cabecera de la objecion, el detalle "lista facturas" y el detalle de cada factura 
                    If PartialPaymentsC.ChangeTracker.State = ObjectState.Modified Or PartialPaymentsC.PartialPaymentsD.ToList().Exists(Function(item As PartialPaymentsD) item.ChangeTracker.State = ObjectState.Added Or item.ChangeTracker.State = ObjectState.Modified) = True Then
                        PartialPaymentsC.ModificationUser = audit.CodeUser
                        PartialPaymentsC.ModificationDate = Date.Now()
                        status = Infrastructure.CrossCutting.Audit.Actions.Update

                        For Each item As PartialPaymentsD In PartialPaymentsC.PartialPaymentsD
                            item.State = 4 'anulo factura 
                            'actualizamos estado en cartera, marcamos esa factura en estado '13 pendiente confirmar pago parcial
                            Dim InvoicePortfolio As GlosaPortfolioGlosada = _PortfolioGlosadaRepository.GetPortfolioGlosada(item.InvoiceNumber)
                            Dim State = InvoicePortfolio.State
                            InvoicePortfolio.State = InvoicePortfolio.TempState 'como anulamos, procedemos a devolver estado en el que se encontraba
                            InvoicePortfolio.TempState = State
                            _PortfolioGlosadaRepository.SaveEntity(InvoicePortfolio)
                        Next

                        'actualizo la entidad
                        _PartialPaymentsCRepository.SaveEntity(PartialPaymentsC)
                        'confirmo la unidad de trabajo  la cabecera de la objecion
                        unitOfWork.Commit()
                        unitofWorkPortfolio.Commit()
                        auditProcess = New IndigoAuditSimpleEntity(Of PartialPaymentsC)(PartialPaymentsC, audit, status, AuxPartialPaymentsC)
                        auditProcess.Execute()
                    End If

                    'confirmo la transaccion
                    scope.Complete()
                End Using

            Else
                'En caso de error
                _actionResult.StateResult = False
            End If

            'retorno objeto de respuesta
            Return _actionResult
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of PartialPaymentsC) With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PartialPaymentsC) With {.StateResult = False}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _InterfaceFox.Dispose()
                _InterfaceNet.Dispose()
            End If
            _PartialPaymentsCRepository = Nothing
            _PartialPaymentsDRepository = Nothing
            _PortfolioGlosadaRepository = Nothing
            _PartialPaymentsMovementRepository = Nothing
            _MovementGlosaRepository = Nothing
            _ConsecutiveRepository = Nothing
            _IInterfaceParametersRepository = Nothing
            _InterfaceFox = Nothing
            _InterfaceNet = Nothing
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
