'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Duván Albeiro Mejia Cortes
' Created          : 05/10/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Class MDashboardQualityControl
    Implements IDisposable

#Region "Fields"


    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim _sessionValues As SessionValues

    ''' <summary>
    ''' Tag del formulario
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builder"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(tag As String)
        Me._tagForm = tag
        _sessionValues = SessionValues.Instance
        Me._sessionValues.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Actualiza los estados del control de calidad de los Productos (Paquetes)
    ''' </summary>
    ''' <param name="requestPackageDetailStatusIds"></param>
    ''' <param name="Data"></param>
    ''' <returns></returns>
    Public Async Function UpdateRequestPackageDetailStatusQS(ByVal requestPackageDetailStatusIds As List(Of Integer), Data As Object) As Task(Of ActionResult)
        Dim args As String = Utils.SerializeObjectToJson(Data)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.UpdateRequestPackageDetailStatusQSAsync(requestPackageDetailStatusIds, args, _sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Asigna el almacén segun el tipo de Accion: Solitante o Manual 
    ''' </summary>
    ''' <param name="Data"></param>
    ''' <returns></returns>
    Public Async Function UpdateAssignWarehouse(Data As Tuple(Of Integer, List(Of Integer))) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.RequestPackageDetailStatusUpdateWarehouseAsync(Data)
    End Function

    ''' <summary>
    ''' Genera la Orden de Traslado
    ''' </summary>
    ''' <param name="transferOrderlist"></param>
    ''' <param name="ProductDetailsList"></param>
    ''' <returns></returns>
    Public Async Function SaveOrderTransferAsync(transferOrderlist As TransferOrder, productDetailsList As List(Of ViewListFinalControlProductModel)) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveOrderTransferAsync(transferOrderlist, productDetailsList, Me._sessionValues.AuditMessageWcf)
    End Function

    Public Async Function GetRequestPackageDetailStatusDefectClassification(requestPackageDetailStatusIds As List(Of Integer), unitDoseClass As Integer, Optional Form As Byte = 0) As Task(Of List(Of DefectClassificationModel))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetRequestPackageDetailStatusDefectClassificationAsync(requestPackageDetailStatusIds, unitDoseClass, Form)
    End Function

    ''' <summary>
    ''' Almacena la clasificacion de defectos de las adecuaciones
    ''' </summary>
    ''' <param name="isQuality"></param>
    ''' <param name="requestPackageDetailStatusIds"></param>
    ''' <param name="DefectClassificationHeader"></param>
    ''' <param name="defectClassificationList"></param>
    ''' <param name="ForceSave"></param>
    ''' <returns></returns>
    Public Function SaveDefectClassification(isQuality As Boolean, requestPackageDetailStatusIds As List(Of Integer), DefectClassificationHeader As DefectClassificationHeaderModel, defectClassificationList As List(Of DefectClassificationModel), Optional ForceSave As Boolean = False) As Task(Of ActionResult)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveDefectClassificationByRequestPackageDetailStatusAsync(isQuality, requestPackageDetailStatusIds, DefectClassificationHeader, defectClassificationList, _sessionValues.AuditMessageWcf, ForceSave)
    End Function

    ''' <summary>
    ''' funcion para validar si se puede reprocesar, rechazar o liberar un producto
    ''' </summary>
    ''' <param name="requestPackageStatusIds"></param>
    ''' <param name="TypeAction"></param>
    ''' <returns></returns>
    Public Async Function ValidationDefectClassification(requestPackageStatusIds As List(Of Integer), TypeAction As Integer) As Task(Of ActionResult(Of List(Of Integer)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ValidationDefectClassificationAsync(requestPackageStatusIds, TypeAction)
    End Function

    ''' <summary>
    ''' Valida los almacenes para devolver mensajes especificos de Central de Mezclas
    ''' </summary>
    ''' <param name="args"></param>
    ''' <returns></returns>
    Public Function ValidateWarehouses(args As Object) As Task(Of ActionResult)
        Dim parameter As String = Utils.SerializeObjectToJson(args)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ValidateWarehouseMSAsync(parameter)
    End Function

    Public Function RescheduleRequestMixingStationAsync(requestPackageDetailStatusIds As List(Of Integer)) As Task(Of ActionResult)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.RescheduleRequestMixingStationAsync(requestPackageDetailStatusIds, _sessionValues.AuditMessageWcf)
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

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
