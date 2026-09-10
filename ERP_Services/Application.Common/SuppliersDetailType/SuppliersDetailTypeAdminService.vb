'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 08/01/2015
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

Public Class SuppliersDetailTypeAdminService
    Implements ISuppliersDetailTypeAdminService

    ''' <summary>
    ''' Representa el repositorio de tipo de proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Private _supplierDetailTypeRepository As ISuppliersDetailTypeRepository

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal supplierDetailTypeRepository As ISuppliersDetailTypeRepository)
        If supplierDetailTypeRepository Is Nothing Then
            Throw New ArgumentNullException("supplierDetailTypeRepository Vacio")
        End If
        _supplierDetailTypeRepository = supplierDetailTypeRepository
    End Sub

    ''' <summary>
    ''' Lista un tipo de proveedor que tiene asociado al proveedor
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSuppliersDetailTypeById(id As Integer, audit As AuditMessage) As SupplierDetailType Implements ISuppliersDetailTypeAdminService.GetSuppliersDetailTypeById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim supplierDetailType As SupplierDetailType = Me._supplierDetailTypeRepository.GetSuppliersDetailTypeById(id)
            If supplierDetailType IsNot Nothing AndAlso supplierDetailType.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of SupplierDetailType)(supplierDetailType, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return supplierDetailType
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New SupplierDetailType
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los tipos de proveedores que tiene asociado el proveedor
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSuppliersDetailTypeByIdSupplier(id As Integer, audit As AuditMessage) As List(Of SupplierDetailType) Implements ISuppliersDetailTypeAdminService.GetSuppliersDetailTypeByIdSupplier
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim ListSupplierDetailType As List(Of SupplierDetailType) = Me._supplierDetailTypeRepository.GetSuppliersDetailTypeByIdSupplier(id)
            Return ListSupplierDetailType
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _supplierDetailTypeRepository = Nothing
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
