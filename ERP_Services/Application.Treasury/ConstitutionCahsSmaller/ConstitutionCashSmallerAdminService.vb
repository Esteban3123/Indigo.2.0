'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/01/2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

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
Imports System.Text

Public Class ConstitutionCashSmallerAdminService
    Implements IConstitutionCashSmallerAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de fondos de caja menor
    ''' </summary>
    Private _constitutionCashSmallerRepository As IConstitutionCashSmallerRepository

    ''' <summary>
    ''' repositorio de las secuencias numericas
    ''' </summary>
    Private _sequenceDRepository As ISequenseTreasuryDRepository

#End Region

#Region "Builder"

    Public Sub New(constitutionCashSmallerRepository As IConstitutionCashSmallerRepository, sequenceDRepository As ISequenseTreasuryDRepository)
        If constitutionCashSmallerRepository Is Nothing Then
            Throw New ArgumentNullException("constitutionCashSmallerRepository")
        End If
        If sequenceDRepository Is Nothing Then
            Throw New ArgumentNullException("sequenceDRepository")
        End If
        _constitutionCashSmallerRepository = constitutionCashSmallerRepository
        _sequenceDRepository = sequenceDRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Confirma el fondo de caja menor
    ''' </summary>
    ''' <param name="ConstitutionCashSmaller"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConfirmConstitutionCashSmaller(ConstitutionCashSmaller As ConstitutionCashSmaller, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of ConstitutionCashSmaller) Implements IConstitutionCashSmallerAdminService.ConfirmConstitutionCashSmaller

    End Function

    ''' <summary>
    ''' Obtiene el fondo de caja menor por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetConstitutionCashSmaller(code As String, audit As AuditMessage) As ActionResult(Of ConstitutionCashSmaller) Implements IConstitutionCashSmallerAdminService.GetConstitutionCashSmaller
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        Try
            Dim ConstitutionCashSmaller As ConstitutionCashSmaller = Me._constitutionCashSmallerRepository.GetConstitutionCashSmaller(code.Trim())
            If ConstitutionCashSmaller IsNot Nothing AndAlso ConstitutionCashSmaller.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of ConstitutionCashSmaller)(ConstitutionCashSmaller, audit, status)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of ConstitutionCashSmaller) With {.StateResult = True, .ObjectEmbbeded = ConstitutionCashSmaller}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ConstitutionCashSmaller) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el fondo de caja menor por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetConstitutionCashSmallerById(id As Integer, tracking As Boolean, audit As AuditMessage) As ActionResult(Of ConstitutionCashSmaller) Implements IConstitutionCashSmallerAdminService.GetConstitutionCashSmallerById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Dim ConstitutionCashSmaller As ConstitutionCashSmaller = Me._constitutionCashSmallerRepository.GetConstitutionCashSmallerById(id, tracking)
            If ConstitutionCashSmaller IsNot Nothing AndAlso ConstitutionCashSmaller.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of ConstitutionCashSmaller)(ConstitutionCashSmaller, audit, status)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of ConstitutionCashSmaller) With {.StateResult = True, .ObjectEmbbeded = ConstitutionCashSmaller}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ConstitutionCashSmaller) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza el fondo de caja menor
    ''' </summary>
    ''' <param name="ConstitutionCashSmaller"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveConstitutionCashSmaller(ConstitutionCashSmaller As ConstitutionCashSmaller, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of ConstitutionCashSmaller) Implements IConstitutionCashSmallerAdminService.SaveConstitutionCashSmaller
        If ConstitutionCashSmaller Is Nothing Then
            Throw New ArgumentNullException("ConstitutionCashSmaller")
        End If

        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim XmlObject = ConvertToXml(ConstitutionCashSmaller)
                Dim resultStore = _constitutionCashSmallerRepository.SP_SaveConstitutionCashSmaller(XmlObject, audit.CodeUser)
                If resultStore.CodeMessage <> 0 Then
                    scope.Dispose()
                    Return New ActionResult(Of ConstitutionCashSmaller) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = resultStore.Message}
                End If

                ConstitutionCashSmaller.Id = resultStore.Id
                ConstitutionCashSmaller.Code = resultStore.Code

                'Se marca la entidad como sin cambios
                ConstitutionCashSmaller.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of ConstitutionCashSmaller) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = ConstitutionCashSmaller, .Message = resultStore.Message}
            End Using
        Catch ex As OptimisticConcurrencyException
            Return New ActionResult(Of ConstitutionCashSmaller) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = "-999"}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ConstitutionCashSmaller) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Convierte la entidad en xml
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertToXml(ConstitutionCashSmaller As ConstitutionCashSmaller) As String
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<ConstitutionCashSmaller>")
        builder.Append("<Id>" & ConstitutionCashSmaller.Id & "</Id>")
        builder.Append("<Code>" & ConstitutionCashSmaller.Code & "</Code>")
        builder.Append("<DocumentDate>" & ConstitutionCashSmaller.DocumentDate.ToString("dd/MM/yyyy") & "</DocumentDate>")
        builder.Append("<DocumentType>" & ConstitutionCashSmaller.DocumentType & "</DocumentType>")
        builder.Append("<CashRegisterSmallerId>" & ConstitutionCashSmaller.CashRegisterSmallerId & "</CashRegisterSmallerId>")
        builder.Append("<SourceType>" & ConstitutionCashSmaller.SourceType & "</SourceType>")
        If ConstitutionCashSmaller.CashRegisterId IsNot Nothing Then
            builder.Append("<CashRegisterId>" & ConstitutionCashSmaller.CashRegisterId & "</CashRegisterId>")
        Else
            builder.Append("<CashRegisterId>" & 0 & "</CashRegisterId>")
        End If
        If ConstitutionCashSmaller.EntityBankAccountId IsNot Nothing Then
            builder.Append("<EntityBankAccountId>" & ConstitutionCashSmaller.EntityBankAccountId & "</EntityBankAccountId>")
        Else
            builder.Append("<EntityBankAccountId>" & 0 & "</EntityBankAccountId>")
        End If
        builder.Append("<Value>" & ConstitutionCashSmaller.Value.ToString().Replace(",", ".") & "</Value>")
        builder.Append("<OperatingUnitId>" & ConstitutionCashSmaller.OperatingUnitId & "</OperatingUnitId>")
        builder.Append("<Status>" & ConstitutionCashSmaller.Status & "</Status>")
        builder.Append("</ConstitutionCashSmaller>")

        Return builder.ToString()
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _constitutionCashSmallerRepository = Nothing
            _sequenceDRepository = Nothing
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
