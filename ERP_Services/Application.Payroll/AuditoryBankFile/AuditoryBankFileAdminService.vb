'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Da
' Created          : 09-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports Infrastructure.CrossCutting.Base

Public Class AuditoryBankFileAdminService
    Implements IAuditoryBankFileAdminService

    ''' <summary>
    ''' Repositorio de autorizacion de concepto
    ''' </summary>
    ''' <remarks></remarks>
    Private _auditoryBankFileRepository As IAuditoryBankFileRepository

    ''' <summary>
    ''' contructor el cual creo una instancia del repositorio
    ''' </summary>
    ''' <param name="auditoryBankFileRepository"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal auditoryBankFileRepository As IAuditoryBankFileRepository)
        If auditoryBankFileRepository Is Nothing Then
            Throw New ArgumentNullException("authorizationConceptRepository Vacio")
        End If
        _auditoryBankFileRepository = auditoryBankFileRepository
    End Sub

    Public Function GetAuditoryBankFile(bankId As String, groupId As String, PayrollDate As Date) As List(Of AuditoryBankFile) Implements IAuditoryBankFileAdminService.GetAuditoryBankFile
        Try
            Return _auditoryBankFileRepository.GetAuditoryBankFile(bankId, groupId, PayrollDate)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveAuditoryBankFile(auditoryBankFile As AuditoryBankFile, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IAuditoryBankFileAdminService.SaveAuditoryBankFile
        If auditoryBankFile Is Nothing Then
            Throw New ArgumentNullException("Auditoria Archivo Banco vacio")
        End If
        Dim unitWork As IUnitWork = _auditoryBankFileRepository.UnitWork
        Try
            _auditoryBankFileRepository.SaveEntity(auditoryBankFile)
            unitWork.Commit()

            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _auditoryBankFileRepository = Nothing
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
