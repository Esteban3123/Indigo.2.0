'***********************************************************************
' Assembly         : Application.Glosas
' Author           : Juan Diego Diaz
' Created          : 24-05-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Transactions

Imports Domain.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Entities
Imports Domain.Common.Entities
Imports Application.Base

#End Region

''' <summary>
''' Servicio de Coordinación.
''' </summary>
''' <remarks></remarks>
Public Class CoordinationAdminService
    Implements ICoordinationAdminService

    Private _ConciliationCRepository As IConciliationCRepository
    Private _ConciliationDRepository As IConciliationDRepository
    Private _ConsecutiveRepository As IConsecutiveRepository

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase <see cref="ConciliationCAdminService" />.
    ''' </summary>
    ''' <param name="ConciliationCRepository">El repositorio para el manejo de las compañias.</param>
    Public Sub New(ByVal ConciliationCRepository As IConciliationCRepository, ByVal ConciliationDRepository As IConciliationDRepository, ByVal ConsecutiveRepository As IConsecutiveRepository)
        If ConciliationCRepository Is Nothing Then
            Throw New ArgumentNullException("Cabecera Coordinacion Vacia")
        End If
        _ConciliationCRepository = ConciliationCRepository
        _ConciliationDRepository = ConciliationDRepository
        _ConsecutiveRepository = ConsecutiveRepository
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _ConciliationCRepository = Nothing
            _ConciliationDRepository = Nothing
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