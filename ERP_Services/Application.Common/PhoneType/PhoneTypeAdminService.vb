'***********************************************************************
' Assembly         : Application.Common
' Author           : Cristhian Mauricio Salazar
' Created          : 26-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Common
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base
Imports Application.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure

Public Class PhoneTypeAdminService
    Implements IPhoneTypeAdminService

    ' Repositorio de tipo de telefono
    Private _phoneTypeRepository As IPhoneTypeRepository

    Public Sub New(ByVal phoneTypeRepository As IPhoneTypeRepository)
        If phoneTypeRepository Is Nothing Then
            Throw New ArgumentNullException("PhoneRepository vacio")
        End If
        _phoneTypeRepository = phoneTypeRepository
    End Sub

    ''' <summary>
    ''' Elimina un tipo de telefono
    ''' </summary>
    ''' <param name="phoneType">Tipo de telefono</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeletePhoneType(ByVal phoneType As PhoneType, ByVal audit As AuditMessage) As ActionMessageResult(Of PhoneType) Implements IPhoneTypeAdminService.DeletePhoneType
        Dim result As New ActionMessageResult(Of PhoneType)
        result.StateResult = True
        If phoneType Is Nothing Then
            Throw New ArgumentNullException("PhoneType vacio")
        End If
        Dim UnitOfWork As IUnitWork = _phoneTypeRepository.UnitWork
        Try

            _phoneTypeRepository.DeleteEntity(phoneType)
            UnitOfWork.Commit()
            'IndigoAuditSimpleEntity(Of PhoneType).Execute(phoneType, audit, Infrastructure.CrossCutting.Audit.Actions.Delete, phoneType)
            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute(phoneType.GetType.Name, audit.Functional, phoneType.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            Return result
        Catch ex As DbUpdateException
            result.StateResult = False
            result.MessageResult.Add(New MessageResult("c-0000", phoneType.Code))
            UnitOfWork.RollbackChanges()
            Return result
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            result.StateResult = False
            Return result
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un tipo de codigo especifico
    ''' </summary>
    ''' <param name="code">Codigo del tipo de telefono</param>
    ''' <returns>Tipo de telefono</returns>
    ''' <remarks></remarks>
    Public Function GetPhoneType(code As String) As Domain.Entities.PhoneType Implements IPhoneTypeAdminService.GetPhoneType
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code Vacio")
        End If
        Try
            Return _phoneTypeRepository.GetPhoneType(code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los tipos de niveles
    ''' </summary>
    ''' <returns>Lista de tipos de niveles</returns>
    ''' <remarks></remarks>
    Public Function ListAllPhoneType() As List(Of Domain.Entities.PhoneType) Implements IPhoneTypeAdminService.ListAllPhoneType
        Try
            Return _phoneTypeRepository.ListAllPhoneType()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Graba o actualiza un tipo de telefono
    ''' </summary>
    ''' <param name="phoneType">Tipo de telefono</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SavePhoneType(phoneType As Domain.Entities.PhoneType, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IPhoneTypeAdminService.SavePhoneType
        If phoneType Is Nothing Then
            Throw New ArgumentNullException("phoneType Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _phoneTypeRepository.UnitWork
        Try

            Dim auditProcess As IndigoAuditSimpleEntity(Of PhoneType)
            Dim AuxPhoneType As PhoneType = Nothing
            Dim status As Integer

            If phoneType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                phoneType.ModificationUser = audit.CodeUser
                phoneType.ModificationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                AuxPhoneType = _phoneTypeRepository.GetPhonetypeByCode(phoneType.Code, False)
            Else
                phoneType.CreationUser = audit.CodeUser
                phoneType.CreationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            End If

            'Valido si se va a guardar o a eliminar
            _phoneTypeRepository.SaveEntity(phoneType)
            UnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of PhoneType)(phoneType, audit, status, AuxPhoneType)
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
            _phoneTypeRepository = Nothing
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
