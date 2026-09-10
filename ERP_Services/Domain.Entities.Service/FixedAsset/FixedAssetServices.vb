#Region "Imports"
Imports Infrastructure.CrossCutting
Imports Domain.Base
Imports Domain.Base.Entities
Imports System.Text
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Entities
Imports Domain.Payroll
Imports Infrastructure.CrossCutting.Base
Imports System.Data
Imports System.Collections.Concurrent
Imports DevExpress.Spreadsheet
Imports System.Data.SqlClient
#End Region

Public Class FixedAssetServices
    Implements IFixedAssetServices

#Region "Fields"

    ''' <summary>
    ''' Repositorio de ingreso de activos
    ''' </summary>
    ''' <remarks></remarks>
    Private _fixedAssetEntryRepository As IFixedAssetEntryRepository

    Private _fixedAssetPhysical As IFixedAssetPhysicalAssetRepository

    Private _fixedAssetTransactionRepository As IFixedAssetValorizationRepository

#End Region

#Region "Builder"

    Public Sub New(fixedAssetEntryRepository As IFixedAssetEntryRepository, fixedAssetPhysical As IFixedAssetPhysicalAssetRepository, fixedAssetTransactionRepository As IFixedAssetValorizationRepository)
        If fixedAssetEntryRepository Is Nothing Then
            Throw New ArgumentNullException("fixedAssetEntryRepository vacío")
        End If
        _fixedAssetEntryRepository = fixedAssetEntryRepository
        _fixedAssetPhysical = fixedAssetPhysical
        _fixedAssetTransactionRepository = fixedAssetTransactionRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Calcula los días pendientes por depreciar
    ''' </summary>
    ''' <param name="AdquisitionDate"></param>
    ''' <param name="LifeTime"></param>
    ''' <param name="UnitLifeTime"></param>
    ''' <returns></returns>
    Public Function CalculateDaysPendingDepreciate(AdquisitionDate As Date, LifeTime As Integer, UnitLifeTime As Byte) As Integer Implements IFixedAssetServices.CalculateDaysPendingDepreciate
        Dim DateEnd As Date

        Select Case UnitLifeTime
            Case 1
                DateEnd = DateAdd(DateInterval.Year, LifeTime, AdquisitionDate)
            Case 2
                DateEnd = DateAdd(DateInterval.Month, LifeTime, AdquisitionDate)
            Case 3
                DateEnd = DateAdd(DateInterval.Day, LifeTime, AdquisitionDate)
        End Select

        Return DateDiff(DateInterval.Day, AdquisitionDate, DateEnd)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _fixedAssetEntryRepository = Nothing
            _fixedAssetPhysical = Nothing
            _fixedAssetTransactionRepository = Nothing
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
