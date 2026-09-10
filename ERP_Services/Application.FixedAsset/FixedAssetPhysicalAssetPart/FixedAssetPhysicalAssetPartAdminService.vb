#Region "Imports"
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports System.Data.Entity.Core
Imports System.Transactions
Imports System.Text
Imports Domain.Entities.Service
Imports Application.Payments
Imports Application.Accounting
Imports Application.Portfolio
Imports Application.FixedAsset
Imports System.Data.SqlClient
Imports System.Data.Entity.Infrastructure

#End Region

Public Class FixedAssetPhysicalAssetPartAdminService
    Implements IFixedAssetPhysicalAssetPartAdminService

#Region "Variables"

    'Repositorios
    Private _fixedAssetPhysicalAssetPartRepository As IFixedAssetPhysicalAssetPartRepository

#End Region

#Region "Builder"

    ''' <summary>
    ''' inicia los repositorios
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(fixedAssetPhysicalAssetPartRepository As IFixedAssetPhysicalAssetPartRepository)
        If fixedAssetPhysicalAssetPartRepository Is Nothing Then
            Throw New ArgumentNullException("fixedAssetPhysicalAssetPartRepository")
        End If

        Me._fixedAssetPhysicalAssetPartRepository = fixedAssetPhysicalAssetPartRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un registro por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetPhysicalAssetPartById(Id As Integer) As FixedAssetPhysicalAssetParts Implements IFixedAssetPhysicalAssetPartAdminService.GetFixedAssetPhysicalAssetPartById
        Try
            Return Me._fixedAssetPhysicalAssetPartRepository.GetFixedAssetPhysicalAssetPartById(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New FixedAssetPhysicalAssetParts
        End Try
    End Function

#End Region

#Region "IDisposable Support"

    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _fixedAssetPhysicalAssetPartRepository = Nothing
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
