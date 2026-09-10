'***********************************************************************
' Assembly         : Presentacion.FixedAsset.MVP
' Author           : Daniel Eduardo Arévalo
' Created          : 26-08-2015
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
Imports System.Data
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo

#End Region

Public Class MFixedAssetPartsAccesoriesConsumables

    Inherits ModelBase
    Implements IDisposable


    Sub New(tag As String)
        MyBase.New(tag)
    End Sub

#Region "Methods"

    ''' <summary>
    ''' Obtener una marca por su codigo, metodo asincrono
    ''' </summary>
    ''' <param name="code">El codigo de la marca</param>
    ''' <returns>objeto accesorio</returns>
    Public Async Function GetPartsAccesoriesConsumablesAsync(ByVal code As String) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetPartsAccesoriesConsumablesByCodeAsync(code, Indigo)
    End Function
    ''' <summary>
    ''' Graba la marca en modo asincrono
    ''' </summary>
    ''' <param name="Trademark">Trademark</param>
    ''' <returns>Un valor que indica si se grabo la marca</returns>
    Public Async Function SavePartsAccesoriesConsumablesAsync(ByVal PartsAccesoriesConsumables As FixedAssetPartsAccesoriesConsumables, ByVal idSequense As Integer) As Task(Of ActionResult(Of FixedAssetPartsAccesoriesConsumables))
        'Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.SavePartsAccesoriesConsumablesAsync(Trademark, Indigo)
        'Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.InnerChannel)
        '    Dim mess1 As New MessageHeader(Of Int64)(idSequense)
        '    Dim header1 As System.ServiceModel.Channels.MessageHeader = mess1.GetUntypedHeader(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
        '    OperationContext.Current.OutgoingMessageHeaders.Add(header1)
        '    Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.SavePartsAccesoriesConsumablesAsync(Indigo.TransactionalContainer, PartsAccesoriesConsumables, Indigo.AuditMessageWcf)
        'End Using
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.SavePartsAccesoriesConsumablesAsync(PartsAccesoriesConsumables, idSequense, Me.Indigo.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Elimina Marca modo asincrono
    ''' </summary>
    ''' <param name="PartsAccesoriesConsumables">PartsAccesoriesConsumables</param>
    ''' <returns>Un valor que indica si se elimino con exito la Marca</returns>
    Public Async Function DeletePartsAccesoriesConsumablesAsync(ByVal PartsAccesoriesConsumables As FixedAssetPartsAccesoriesConsumables) As Task(Of ActionResult)
        '  Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.DeletePartsAccesoriesConsumablesAsync(PartsAccesoriesConsumables, Indigo)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.DeletePartsAccesoriesConsumablesAsync(PartsAccesoriesConsumables, Me.Indigo.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Lista todos los accesorios
    ''' </summary>
    Public Async Function ListAllPartsAccesoriesConsumablesAsync() As Task(Of List(Of FixedAssetPartsAccesoriesConsumables))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.ListAllPartsAccesoriesConsumablesAsync(Indigo)
    End Function

    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetNullFields() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("Accessory", Indigo)
    End Function

    ''' <summary>
    ''' Metodo para cambiar de estado la entidad
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="State"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>


    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of FixedAssetPartsAccesoriesConsumables))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.ChangeFixedAssetPartsAccesoriesConsumablesStateAsync(code, state, Me.Indigo.AuditMessageWcf)
    End Function

    Public Function ListPartsXpo() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListFixedAssetPartsAccesoriesConsumibles()
    End Function

    Public Function ListPartsByEquipmentType(equipmentType As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MaintenanceService.ListFixedAssetPartsAccesoriesConsumiblesByEquipmentType(equipmentType)
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
