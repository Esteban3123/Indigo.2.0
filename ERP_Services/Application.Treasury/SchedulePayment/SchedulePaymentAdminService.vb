'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Entities.Service

#End Region

Public Class SchedulePaymentAdminService
    Implements ISchedulePaymentAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de programacion de pagos
    ''' </summary>
    Private _schedulePaymentRepository As ISchedulePaymentRepository
    Private _schedulePaymentDetailRepository As ISchedulePaymentDetailRepository

    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenseDRepository As ISequenseTreasuryDRepository
    Private treasuryService As ITreasuryServices
    Private _supplierTypeReposity As ISupplierTypeRepository
    Private _SupplierRepository As ISupplierRepository

#End Region

#Region "Methods"

    Public Sub New(ByVal schedulePaymentRepository As ISchedulePaymentRepository, ByVal secuenseDRepository As ISequenseTreasuryDRepository, _treasuryService As ITreasuryServices,
                   supplierTypeReposity As ISupplierTypeRepository, schedulePaymentDetailRepository As ISchedulePaymentDetailRepository, SupplierRepository As ISupplierRepository)
        If schedulePaymentRepository Is Nothing Then
            Throw New ArgumentNullException("schedulePaymentRepository")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _schedulePaymentRepository = schedulePaymentRepository
        _secuenseDRepository = secuenseDRepository
        treasuryService = _treasuryService
        _supplierTypeReposity = supplierTypeReposity
        _schedulePaymentDetailRepository = schedulePaymentDetailRepository
        _SupplierRepository = SupplierRepository
    End Sub

#End Region

    ''' <summary>
    ''' Elimina una programacion de pagos
    ''' </summary>
    ''' <param name="schedulePayment">The schedule payment.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">schedulePayment</exception>
    Public Function DeleteSchedulePayment(schedulePayment As SchedulePayment, audit As AuditMessage) As ActionResult Implements ISchedulePaymentAdminService.DeleteSchedulePayment
        If schedulePayment Is Nothing Then
            Throw New ArgumentNullException("schedulePayment")
        End If
        Dim unitOfWork As IUnitWork = Me._schedulePaymentRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                While schedulePayment.SchedulePaymentDetail.Count > 0
                    schedulePayment.SchedulePaymentDetail.Item(schedulePayment.SchedulePaymentDetail.Count - 1).MarkAsDeleted()
                End While
                schedulePayment.MarkAsDeleted()
                schedulePayment.ModificationUser = audit.CodeUser
                schedulePayment.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of SchedulePayment)(schedulePayment, audit, status)
                auditProcess.Execute()

                Me._schedulePaymentRepository.SaveEntity(schedulePayment)
                unitOfWork.Commit()
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-999"}), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una programacion de pagos por codigo
    ''' </summary>
    Public Function GetSchedulePayment(code As String, tracking As Boolean, audit As AuditMessage) As ActionResult(Of SchedulePayment) Implements ISchedulePaymentAdminService.GetSchedulePayment
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim schedulePayment As SchedulePayment = Me._schedulePaymentRepository.GetSchedulePayment(code.Trim(), tracking)
            If schedulePayment IsNot Nothing AndAlso schedulePayment.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of SchedulePayment)(schedulePayment, audit, status)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of SchedulePayment) With {.StateResult = True, .ObjectEmbbeded = schedulePayment}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SchedulePayment) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una dispesión por egreso
    ''' </summary>
    ''' <param name="VoucherTransaction"></param>
    ''' <returns></returns>
    Public Function GetSchedulePaymentByVoucherTransaction(VoucherTransaction As VoucherTransaction) As ActionResult(Of SchedulePayment) Implements ISchedulePaymentAdminService.GetSchedulePaymentByVoucherTransaction
        If VoucherTransaction Is Nothing Then
            Throw New ArgumentNullException("VoucherTransaction")
        End If
        Try
            Dim schedulePayment As SchedulePayment = Me._schedulePaymentRepository.GetSchedulePaymentByVoucherTransaction(VoucherTransaction)
            If schedulePayment?.Id > 0 Then
                Return New ActionResult(Of SchedulePayment) With {.StateResult = True, .ObjectEmbbeded = schedulePayment}
            End If
            Return New ActionResult(Of SchedulePayment) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        Catch ex As Exception
            Return New ActionResult(Of SchedulePayment) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una programacion de pagos por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetSchedulePaymentById(id As Integer) As SchedulePayment Implements ISchedulePaymentAdminService.GetSchedulePaymentById
        Try
            Return _schedulePaymentRepository.GetSchedulePaymentById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene los datos de la programacion de pagos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetSPSchedulePayment(supplierTypeListId As String, Optional paymentD As String = Nothing) As ActionResult(Of List(Of SP_SchedulePayment_Result)) Implements ISchedulePaymentAdminService.GetSPSchedulePayment
        Try
            Dim _schedule As List(Of SP_SchedulePayment_Result) = _schedulePaymentRepository.GetSPSchedulePayment(supplierTypeListId, paymentD)
            Return New ActionResult(Of List(Of SP_SchedulePayment_Result)) With {.StateResult = True, .ObjectEmbbeded = _schedule}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of SP_SchedulePayment_Result)) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el listado de programacion de pagos con pagos hechos
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function GetSchedulePaymentDatasourceWithPayment(code As String, audit As AuditMessage, Optional FlagDispersionFunds As Boolean = False) As ActionResult(Of List(Of SP_SchedulePayment_Result)) Implements ISchedulePaymentAdminService.GetSchedulePaymentDatasourceWithPayment
        Try
            Dim _scheduleLoad = Me.GetSchedulePayment(code, True, audit)
            If Not _scheduleLoad.StateResult Then
                Throw New Exception(_scheduleLoad.Message)
            End If

            Dim _schedule = Me.GetSPSchedulePayment("", _scheduleLoad.ObjectEmbbeded.PaymentDate.Date)

            If Not _schedule.StateResult Then
                Throw New Exception(_schedule.Message)
            End If

            Dim _listSchedulePaymentDetail = _schedulePaymentRepository.ListSchedulePaymentDetailBySchedulePaymentId(_scheduleLoad.ObjectEmbbeded.Id)
            If _listSchedulePaymentDetail IsNot Nothing AndAlso _listSchedulePaymentDetail.Count > 0 Then
                For Each spl As SchedulePaymentDetail In _listSchedulePaymentDetail
                    Dim Schedule As SP_SchedulePayment_Result = _schedule.ObjectEmbbeded.Where(Function(x) x.AccountPayableShareId = spl.AccountPayableShareId AndAlso x.AccountPayableId = spl.AccountPayableId).
                            Cast(Of SP_SchedulePayment_Result).FirstOrDefault()
                    If Schedule IsNot Nothing Then
                        Schedule.SchedulePaymentDetailId = spl.Id
                        Schedule.PayValue = spl.AmountPaid
                        Schedule.PaymentConceptId = spl.PaymentConceptId
                        Schedule.DiscountValue = spl.DiscountValue
                        Schedule.ApplyDiscountValue = spl.DiscountValue
                        Schedule.PaymentPercent = spl.AmountPercent
                        If Schedule.BaseValue Is Nothing OrElse Schedule.BaseValue = 0 Then
                            Schedule.DiscountRate = 0
                        Else
                            Schedule.DiscountRate = Math.Round((100 * spl.DiscountValue) / CDec(Schedule.BaseValue), 2)
                        End If
                    Else
                        Dim sp As SP_SchedulePayment_Result = _schedulePaymentRepository.GetSp_SchedulePayment_ResultByAccountPayableIdAccountPayableShareId(spl.AccountPayableId, spl.AccountPayableShareId)
                        If sp IsNot Nothing Then
                            sp.SchedulePaymentDetailId = spl.Id
                            sp.PayValue = spl.AmountPaid
                            sp.PaymentConceptId = spl.PaymentConceptId
                            sp.DiscountValue = spl.DiscountValue
                            sp.ApplyDiscountValue = spl.DiscountValue
                            sp.PaymentPercent = spl.AmountPercent
                            If sp.BaseValue Is Nothing OrElse sp.BaseValue = 0 Then
                                sp.DiscountRate = 0
                            Else
                                sp.DiscountRate = Math.Round((100 * spl.DiscountValue) / CDec(sp.BaseValue), 2)
                            End If
                            _schedule.ObjectEmbbeded.Add(sp)
                        End If
                    End If
                Next
                Dim resultValidate = treasuryService.ValidateSchedulePayment(_schedule.ObjectEmbbeded)
                If resultValidate.StateResult Then
                    Return New ActionResult(Of List(Of SP_SchedulePayment_Result)) With {.StateResult = True, .ObjectEmbbeded = IIf(FlagDispersionFunds, _schedule.ObjectEmbbeded.Where(Function(x) _listSchedulePaymentDetail.Select(Function(m) m.Id).Contains(x.SchedulePaymentDetailId)).ToList(), _schedule.ObjectEmbbeded)}
                Else
                    Return New ActionResult(Of List(Of SP_SchedulePayment_Result)) With {.StateResult = False, .Message = resultValidate.Message, .ObjectEmbbeded = IIf(FlagDispersionFunds, _schedule.ObjectEmbbeded.Where(Function(x) _listSchedulePaymentDetail.Select(Function(m) m.Id).Contains(x.SchedulePaymentDetailId)).ToList(), _schedule.ObjectEmbbeded)}
                End If
            Else
                Return New ActionResult(Of List(Of SP_SchedulePayment_Result)) With {.StateResult = False, .Message = ResourceManager.GetString("ScheduleDetailNotFound", "Treasury")}
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of SP_SchedulePayment_Result)) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la programación de pagos por tipo de proveedor o todos los hijos (no padres) de éste
    ''' </summary>
    ''' <param name="supplierTypeId">The supplier type identifier.</param>
    ''' <returns></returns>
    Public Function GetSPSchedulePaymentBySupplierTypeId(supplierTypeId As Integer, code As String) As ActionResult(Of List(Of SP_SchedulePayment_Result)) Implements ISchedulePaymentAdminService.GetSPSchedulePaymentBySupplierTypeId
        Try
            Dim _schedule As List(Of SP_SchedulePayment_Result) = Nothing
            Dim _listIdSupplierType As List(Of Integer) = ValidateItemSupplierType(supplierTypeId)
            If _listIdSupplierType.Count > 0 Then
                _schedule = _schedulePaymentRepository.GetSPSchedulePayment(String.Join(",", _listIdSupplierType))
            Else
                _schedule = _schedulePaymentRepository.GetSPSchedulePayment("")
            End If
            If String.IsNullOrEmpty(code) Then
                Return New ActionResult(Of List(Of SP_SchedulePayment_Result)) With {.StateResult = True, .ObjectEmbbeded = _schedule}
            End If
            Dim _scheduleLoad As SchedulePayment = Me._schedulePaymentRepository.GetSchedulePayment(code.Trim(), True)
            If _schedule IsNot Nothing AndAlso _scheduleLoad.Id > 0 Then
                Dim _listSchedulePaymentDetail = _schedulePaymentRepository.ListSchedulePaymentDetailBySchedulePaymentId(_scheduleLoad.Id)
                If _listSchedulePaymentDetail IsNot Nothing AndAlso _listSchedulePaymentDetail.Count > 0 Then
                    For Each spl As SchedulePaymentDetail In _listSchedulePaymentDetail
                        Dim Schedule As SP_SchedulePayment_Result = _schedule.Where(Function(x) x.AccountPayableShareId = spl.AccountPayableShareId _
                                                                                        AndAlso x.AccountPayableId = spl.AccountPayableId AndAlso _listIdSupplierType.Contains(x.SupplierTypeId)).
                            Cast(Of SP_SchedulePayment_Result).FirstOrDefault()
                        If Schedule IsNot Nothing Then
                            Schedule.SchedulePaymentDetailId = spl.Id
                            Schedule.PayValue = spl.AmountPaid
                            Schedule.PaymentConceptId = spl.PaymentConceptId
                            Schedule.DiscountValue = spl.DiscountValue
                            Schedule.ApplyDiscountValue = spl.DiscountValue
                            Schedule.PaymentPercent = spl.AmountPercent
                        Else
                            Dim sp As SP_SchedulePayment_Result = _schedulePaymentRepository.GetSp_SchedulePayment_ResultByAccountPayableIdAccountPayableShareId(spl.AccountPayableId, spl.AccountPayableShareId)
                            If sp IsNot Nothing AndAlso _listIdSupplierType.Contains(sp.SupplierTypeId) Then
                                sp.SchedulePaymentDetailId = spl.Id
                                sp.PayValue = spl.AmountPaid
                                sp.PaymentConceptId = spl.PaymentConceptId
                                sp.DiscountValue = spl.DiscountValue
                                sp.ApplyDiscountValue = spl.DiscountValue
                                sp.PaymentPercent = spl.AmountPercent
                                _schedule.Add(sp)
                            End If
                        End If
                    Next
                    Dim resultValidate = treasuryService.ValidateSchedulePayment(_schedule)
                    If resultValidate.StateResult Then
                        Return New ActionResult(Of List(Of SP_SchedulePayment_Result)) With {.StateResult = True, .ObjectEmbbeded = _schedule}
                    Else
                        Return New ActionResult(Of List(Of SP_SchedulePayment_Result)) With {.StateResult = False, .Message = resultValidate.Message, .ObjectEmbbeded = _schedule}
                    End If
                Else
                    Return New ActionResult(Of List(Of SP_SchedulePayment_Result)) With {.StateResult = False, .Message = ResourceManager.GetString("ScheduleDetailNotFound", "Treasury")}
                End If
            Else
                Return New ActionResult(Of List(Of SP_SchedulePayment_Result)) With {.StateResult = False, .Message = ""}
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of SP_SchedulePayment_Result)) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    Public Function ValidateItemSupplierType(supplierTypeId As Integer) As List(Of Integer)
        Dim _listSoloHijosTypoProveedor As New List(Of Integer)()
        Dim listHijos As List(Of Integer) = _supplierTypeReposity.GetAllSupplierTypeIdListByParentId(supplierTypeId)
        If listHijos Is Nothing OrElse listHijos.Count = 0 Then
            _listSoloHijosTypoProveedor.Add(supplierTypeId)
        Else
            For Each hijo In listHijos
                Dim list2 As List(Of Integer) = ValidateItemSupplierType(hijo)
                _listSoloHijosTypoProveedor.AddRange(list2)
            Next
        End If
        Return _listSoloHijosTypoProveedor
    End Function

    ''' <summary>
    ''' Guarda una programacion de pagos
    ''' </summary>
    Public Function SaveSchedulePayment(schedulePayment As SchedulePayment, audit As AuditMessage, withConfirm As Boolean, Optional idSequence As Long = 0) As ActionResult(Of SchedulePayment) Implements ISchedulePaymentAdminService.SaveSchedulePayment
        If schedulePayment Is Nothing Then
            Throw New ArgumentNullException("schedulePayment")
        End If
        If withConfirm Then
            schedulePayment.Status = 2
        End If
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                Dim MessageResult As String = ""
                Dim IsSave As Boolean = schedulePayment.Id = 0
                Dim xml = schedulePayment.ToXML()
                ''My.Computer.FileSystem.WriteAllText("C:\xml.txt", xml, True)
                'modifico el format de las fechas contenidas en el xml
                Dim xmlFormatDate As String = Replace(xml, ", ", "/")
                CType(_schedulePaymentRepository.UnitWork, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
                Dim result = _schedulePaymentRepository.GenerateSchedulePaymentSP(xmlFormatDate, audit.CodeUser).ToList().ElementAt(0)
                If result.CodeMessage = "999" Then
                    scope.Dispose()
                    Return New ActionResult(Of SchedulePayment) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = result.Message}
                Else
                    _schedulePaymentRepository.UnitWork.RollbackChangesUnitOfWork()
                    Dim voucher = _schedulePaymentRepository.GetSchedulePaymentById(result.IdSchedulePayment)

                    If withConfirm Then
                        MessageResult = String.Format(ResourceManager.GetString("SaveConfirmComplete", "Treasury"), voucher.Code)
                    Else
                        If IsSave Then
                            MessageResult = String.Format(ResourceManager.GetString("SavedWithCode"), voucher.Code)
                        Else
                            MessageResult = ResourceManager.GetString("SaveMessage")
                        End If
                    End If

                    scope.Complete()
                    Return New ActionResult(Of SchedulePayment) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = MessageResult, .ObjectEmbbeded = voucher}
                End If
            End Using
        Catch ex As OptimisticConcurrencyException
            Return New ActionResult(Of SchedulePayment) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As InvalidOperationException
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SchedulePayment) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorUnknown")}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SchedulePayment) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Confirma una programación de pagos
    ''' </summary>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">SchedulePaymentId</exception>
    Public Function ConfirmSchedulePayment(SchedulePaymentId As Integer, audit As AuditMessage) As ActionResult(Of String) Implements ISchedulePaymentAdminService.ConfirmSchedulePayment
        If SchedulePaymentId = 0 Then
            Throw New ArgumentNullException("SchedulePaymentId")
        End If

        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Dim unitOfWork As IUnitWork = Me._schedulePaymentRepository.UnitWork

            Dim schedulePayment As SchedulePayment = _schedulePaymentRepository.GetSchedulePaymentById(SchedulePaymentId)
            Try
                Dim result = SaveSchedulePayment(schedulePayment, audit, True)
                If result.StateResult Then
                    schedulePayment.ModificationUser = audit.CodeUser
                    schedulePayment.ModificationDate = Date.Now
                    schedulePayment.ConfirmationUser = audit.CodeUser
                    schedulePayment.ConfirmationDate = Date.Now
                    Dim auditProcess As New IndigoAuditSimpleEntity(Of SchedulePayment)(schedulePayment, audit, Infrastructure.CrossCutting.Audit.Actions.Confirm, schedulePayment.OriginalValue)
                    auditProcess.Execute()
                    'Se marca la entidad como sin cambios
                    schedulePayment.MarkAsUnchanged()
                    scope.Complete()
                    Return New ActionResult(Of String) With {.StateResult = True, .ObjectEmbbeded = ""}
                End If
                Return New ActionResult(Of String) With {.StateResult = False, .ObjectEmbbeded = ""}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                scope.Dispose()
                Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of String) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Determines whether [is account payable share in schedule payment detail active] [the specified account payable share identifier].
    ''' </summary>
    ''' <param name="AccountPayableShareId">The account payable share identifier.</param>
    ''' <returns></returns>
    Public Function IsAccountPayableShareInSchedulePaymentDetailActive(AccountPayableShareId As Integer) As ActionResult Implements ISchedulePaymentAdminService.IsAccountPayableShareInSchedulePaymentDetailActive
        Try
            Return _schedulePaymentDetailRepository.IsAccountPayableShareInSchedulePaymentDetailActive(AccountPayableShareId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' funcion para calcular decuento de cxp dependiendo de la fecha de pago
    ''' </summary>
    ''' <param name="ListSchedulePayment"></param>
    ''' <param name="PaymentDate"></param>
    ''' <returns></returns>
    Public Function CalculateDiscountByPaymentDate(ListSchedulePayment As List(Of SP_SchedulePayment_Result), PaymentDate As DateTime) As ActionMessageResult(Of List(Of SP_SchedulePayment_Result)) Implements ISchedulePaymentAdminService.CalculateDiscountByPaymentDate
        Try
            If ListSchedulePayment Is Nothing OrElse Not ListSchedulePayment.Any Then
                Throw New Exception(message:="La lista de programación de pagos esta vacia")
            End If
            Dim IdsSupplier = ListSchedulePayment.Select(Function(d) d.SupplierId).ToList()
            Dim listSuppliers = _SupplierRepository.GetByFilter(Function(x) IdsSupplier.Contains(x.Id) And x.PromptPaymentDiscount.Any, False, {"PromptPaymentDiscount"}).ToList()

            For Each item In ListSchedulePayment.Where(Function(t) listSuppliers.Select(Function(i) i.Id).ToList().Contains(t.SupplierId) AndAlso t.CXPValue = (t.BalanceShare + t.ValueNote))
                Dim Supplier = listSuppliers.Where(Function(j) j.Id = item.SupplierId).FirstOrDefault
                Dim _discountDays = DateDiff("d", item.CxPRadicateDate, PaymentDate)

                If Supplier.PromptPaymentDiscount.Where(Function(d) _discountDays >= d.InitialRank And _discountDays <= d.EndRank).ToList().Any Then
                    item.DiscountRate = Supplier.PromptPaymentDiscount.Where(Function(d) _discountDays >= d.InitialRank And _discountDays <= d.EndRank).FirstOrDefault.DiscountRate
                    item.RangeNameDiscount = Supplier.PromptPaymentDiscount.Where(Function(d) _discountDays >= d.InitialRank And _discountDays <= d.EndRank).FirstOrDefault.RangeName
                    item.DiscountValue = (item.BaseValue * (item.DiscountRate / 100))
                Else
                    item.DiscountRate = 0
                    item.DiscountValue = 0
                    item.RangeNameDiscount = ""
                End If
            Next
            Return New ActionMessageResult(Of List(Of SP_SchedulePayment_Result)) With {.StateResult = True, .ObjectEmbbeded = ListSchedulePayment, .Message = "Se calculó correctamente el descuento"}
        Catch ex As Exception
            Return New ActionMessageResult(Of List(Of SP_SchedulePayment_Result)) With {.Message = ex.Message, .StateResult = False}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                treasuryService.Dispose()
            End If
            _schedulePaymentRepository = Nothing
            _secuenseDRepository = Nothing
            treasuryService = Nothing
            _supplierTypeReposity = Nothing
            _schedulePaymentDetailRepository = Nothing
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