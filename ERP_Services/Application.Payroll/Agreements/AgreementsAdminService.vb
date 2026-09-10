'***********************************************************************
' Assembly         : Application.Payrol
' Author           : Rafael Patiño
' Created          : 13-01-2014
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Data.Entity.Core
Imports System.Text
Imports System.Transactions
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

#End Region

Public Class AgreementsAdminService
    Implements IAgreementsAdminService

    Private _AgreementsCRepository As IAgreementsCRepository
    Private _ConsecutiveRepository As IConsecutiveRepository

    Public Sub New(ByVal IAgreementsCRepository As IAgreementsCRepository, ByVal ConsecutiveRepository As IConsecutiveRepository)
        If IAgreementsCRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio vacio")
        End If
        _AgreementsCRepository = IAgreementsCRepository
        _ConsecutiveRepository = ConsecutiveRepository
    End Sub
    ''' <summary>
    ''' Obtener un convenio por el consecutivo de radicado
    ''' </summary>
    ''' <param name="consecutive">consecutivo de radicado</param>
    ''' <param name="audit">auditoria</param>
    ''' <returns>Objeto convenio</returns>
    ''' <remarks></remarks>
    Public Function GetAgreementsC(consecutive As String, ByVal audit As AuditMessage) As AgreementsC Implements IAgreementsAdminService.GetAgreementsC
        If String.IsNullOrEmpty(consecutive) Then
            Throw New ArgumentNullException("Consecutivo vacio")
        End If
        Try
            Dim _AgreementsC = _AgreementsCRepository.GetAgreementsC(consecutive, False)
            'If _AgreementsC.Id > 0 Then
            '    IndigoAuditSimpleEntity(Of AgreementsC).Execute(_AgreementsC, audit, Infrastructure.CrossCutting.Audit.Actions.Print, audit.Company)
            'End If
            Return _AgreementsC
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Listar Convenios
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns>lista de convenios</returns>
    ''' <remarks></remarks>
    Public Function ListAgreementsC(ByVal audit As AuditMessage) As List(Of AgreementsC) Implements IAgreementsAdminService.ListAgreementsC
        Try
            Dim ListAgreements = _AgreementsCRepository.ListAgreementsC()
            'If ListAgreements.Count > 0 Then
            '    For Each item As AgreementsC In ListAgreements
            '        IndigoAuditSimpleEntity(Of AgreementsC).Execute(item, audit, Infrastructure.CrossCutting.Audit.Actions.Print, audit.Company)
            '    Next
            'End If
            Return ListAgreements
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Listar Empleados
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns>lista de empleados</returns>
    ''' <remarks></remarks>
    Public Function ListEmployee(ByVal audit As AuditMessage) As List(Of Domain.Payroll.Entities.Employee) Implements IAgreementsAdminService.ListEmployee
        Try
            Dim List = _AgreementsCRepository.ListEmployee()
            'If List.Count > 0 Then
            '    For Each item As Employee In List
            '        IndigoAuditSimpleEntity(Of Employee).Execute(item, audit, Infrastructure.CrossCutting.Audit.Actions.Print, audit.Company)
            '    Next
            'End If
            Return List.ToList()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Eliminar Un Convenio
    ''' </summary>
    ''' <param name="AgreementsC">Obj. convenio</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteAgreementsC(AgreementsC As AgreementsC, audit As AuditMessage) As ActionResult Implements IAgreementsAdminService.DeleteAgreementsC
        If AgreementsC Is Nothing Then
            Throw New ArgumentNullException("Convenio vacio")
        End If
        Dim _unitWork As IUnitWork = _AgreementsCRepository.UnitWork
        Try
            _AgreementsCRepository.SaveEntity(AgreementsC)
            _unitWork.Commit()
            Dim auditObject As New IndigoAuditSimpleEntity(Of AgreementsC)(AgreementsC, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch Ex As OptimisticConcurrencyException
            _unitWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch Ex As UpdateException
            _unitWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"000"})}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)()}
        End Try
    End Function
    ''' <summary>
    ''' Funcion para guardar un convenio
    ''' </summary>
    ''' <param name="AgreementsC">Obj. convenio</param>
    ''' <param name="audit">Auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveAgreementsC(AgreementsC As AgreementsC, audit As AuditMessage) As ActionResult(Of AgreementsC) Implements IAgreementsAdminService.SaveAgreementsC
        If AgreementsC Is Nothing Then
            Throw New ArgumentNullException("Convenio vacio")
        End If
        Dim unitOfWork As IUnitWork = Me._AgreementsCRepository.UnitWork
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim EntityXml As String = AgreementsC.ConvertToXmlAgreement(AgreementsC)
                Dim resultStore = Me._AgreementsCRepository.SP_SaveAgreement(EntityXml, audit.CodeUser)
                If resultStore.CodeResult <> 0 Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of AgreementsC) With {.StateResult = False, .Message = resultStore.MessageResult}
                End If

                AgreementsC.Id = resultStore.Id
                AgreementsC.Consecutive = resultStore.Code
                AgreementsC.MarkAsUnchanged()
                transaction.Complete()
                Return New ActionResult(Of AgreementsC) With {.StateResult = True, .Message = resultStore.MessageResult, .ObjectEmbbeded = AgreementsC}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of AgreementsC) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = ex.Message}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of AgreementsC) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using

    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _AgreementsCRepository = Nothing
            _ConsecutiveRepository = Nothing
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

