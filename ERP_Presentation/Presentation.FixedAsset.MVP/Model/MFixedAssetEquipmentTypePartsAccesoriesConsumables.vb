'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Daniel Eduardo Arévalo
' Created          : 16-09-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Presentation.Base

#End Region

Public Class MFixedAssetEquipmentTypePartsAccesoriesConsumables

    Inherits ModelBase
    Implements IDisposable

    Sub New(tag As String)
        MyBase.New(tag)
    End Sub

#Region "Methods"
    ''' <summary>
    ''' Graba la marca en modo asincrono
    ''' </summary>
    ''' <param name="ListEquipmentTypePartsAccesoriesConsumables">Lista de ListEquipmentTypePartsAccesoriesConsumables</param>
    ''' <returns>Un valor que indica si se grabo la marca</returns>
    Public Async Function SaveEquipmentTypePartsAccesoriesConsumablesAsync(ByVal ListEquipmentTypePartsAccesoriesConsumables As List(Of FixedAssetItemTypePartsAccesories)) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.SaveEquipmentTypePartsAccesoriesConsumablesServiceAsync(ListEquipmentTypePartsAccesoriesConsumables, Indigo)
    End Function

    ''' <returns>Un valor que indica si se grabo la marca</returns>
    Public Async Function DeleteEquipmentTypePartsAccesoriesConsumablesAsync(ByVal ListEquipmentTypePartsAccesoriesConsumables As List(Of FixedAssetItemTypePartsAccesories)) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.DeleteEquipmentTypePartsAccesoriesConsumablesServiceAsync(ListEquipmentTypePartsAccesoriesConsumables, Indigo)
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
