'***********************************************************************
' Assembly         : Presentacion.FixedAsset.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/06/2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports Presentation.Base
Imports System.ServiceModel
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports DevExpress.Xpo
Imports System.Dynamic

#End Region

Public Class MFixedAssetPhysicalAsset
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigoSessionValues As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal tag As String)
        Me._tagForm = tag
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    Public Function GetCountFixedAssetDepreciationDetailByFixedAssetPhysicalAssetDetailBookId(listId As List(Of Integer)) As Integer
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).FixedAsset.GetCountFixedAssetDepreciationDetailByFixedAssetPhysicalAssetDetailBookId(listId)
    End Function

    ''' <summary>
    ''' Obtiene un concepto de pago
    ''' </summary>
    ''' <param name="Plate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetFixedAssetPhysicalAssetByPlate(ByVal Plate As String) As Task(Of ActionResult(Of FixedAssetPhysicalAsset))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetFixedAssetPhysicalAssetByPlateAsync(Plate, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Confirma la finalización de contratos leasing
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ConfirmLeasingContractsFinalization(ListIds As List(Of Integer), OperatingUnitId As Integer, Year As Integer, Month As Integer, CompanyNit As String) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.ConfirmLeasingContractsFinalizationAsync(ListIds, OperatingUnitId, Year, Month, CompanyNit, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un concepto de pago
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveFixedAssetPhysicalAsset(ByVal FixedAssetPhysicalAsset As FixedAssetPhysicalAsset) As Task(Of ActionResult(Of FixedAssetPhysicalAsset))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.SaveFixedAssetPhysicalAssetAsync(FixedAssetPhysicalAsset, Me._indigoSessionValues.AuditMessageWcf)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
