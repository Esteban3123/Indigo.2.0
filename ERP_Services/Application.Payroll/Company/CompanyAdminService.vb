'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 25-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure

Public Class CompanyAdminService
    Implements ICompanyAdminService

    Private _companyRepository As ICompanyRepository

    Public Sub New(ByVal companyRepository As ICompanyRepository)
        If companyRepository Is Nothing Then
            Throw New ArgumentNullException("companyRepository Vacio")
        End If
        _companyRepository = companyRepository
    End Sub

    ''' <summary>
    ''' Elimina una Compañia
    ''' </summary>
    ''' <param name="company">Compañia</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteCompany(company As Company, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionMessageResult(Of Company) Implements ICompanyAdminService.DeleteCompany
        Dim result As New ActionMessageResult(Of Company)
        result.StateResult = True
        If company Is Nothing Then
            Throw New ArgumentNullException("company Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _companyRepository.UnitWork
        Try
            _companyRepository.DeleteEntity(company)
            UnitOfWork.Commit()

            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute("Company", audit.Functional, company.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            '/*****Auditoria Avanzada ******/
            Dim auditObject As New IndigoAuditSimpleEntity(Of Company)(company, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()

            'IndigoAuditSimpleEntity(Of Company).Execute(company, audit, Infrastructure.CrossCutting.Audit.Actions.Delete, company)
            Return result
        Catch ex As DbUpdateException
            UnitOfWork.RollbackChanges()
            result.StateResult = False
            result.MessageResult.Add(New MessageResult("c-0000", company.Nit))
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            UnitOfWork.RollbackChanges()
            result.StateResult = False
            Return result
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una Compañia en Específico
    ''' </summary>
    ''' <param name="nit">nit de la Compañia</param>
    ''' <returns>Compañia</returns>
    ''' <remarks></remarks>
    Public Function GetCompany(nit As String) As Company Implements ICompanyAdminService.GetCompany
        If String.IsNullOrEmpty(nit) Then
            Throw New ArgumentNullException("Code Vacio")
        End If
        Try
            Return _companyRepository.GetCompany(nit)

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New Company()
        End Try
    End Function

    ''' <summary>
    ''' Lista todas las Compañías
    ''' </summary>
    ''' <returns>Lista de Compañias</returns>
    ''' <remarks></remarks>
    Public Function ListAllCompany() As List(Of Company) Implements ICompanyAdminService.ListAllCompany
        Try
            Return _companyRepository.ListAllCompany()

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Graba o Actualiza una Compañia
    ''' </summary>
    ''' <param name="company">Compañia</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveCompany(company As Company, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements ICompanyAdminService.SaveCompany
        If company Is Nothing Then
            Throw New ArgumentNullException("company Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _companyRepository.UnitWork
        Try


            Dim auditProcess As IndigoAuditSimpleEntity(Of Company)
            Dim AuxCompany As Company = Nothing
            Dim status As Integer


            If company.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                company.ModificationUser = audit.CodeUser
                company.ModificationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                AuxCompany = _companyRepository.GetCompany(company.Nit, False)
            Else
                company.CreationUser = audit.CodeUser
                company.CreationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            End If

            'Valido si se va a guardar o a eliminar
            _companyRepository.SaveEntity(company)
            UnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of Company)(company, audit, status, AuxCompany)
            auditProcess.Execute()
            Return True

        Catch ex As Exception
            UnitOfWork.RollbackChanges()
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
            _companyRepository = Nothing
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
