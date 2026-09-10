'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/12/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent.IndigoReference.MixingStation
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Infrastructure.Data.Xpo.CommonRepository
Imports Infrastructure.Data.Xpo.FixedAssetRepository
#End Region

''' <summary>
''' Este presentador captura toda la logica aplicada en el frontal 
''' </summary>
Public Class PTransportation

#Region "Variables"
    ''' <summary>
    ''' Variable para instanciar la interfaz
    ''' </summary>
    Dim _view As ITransportation

    ''' <summary>
    ''' Se utiliza para instanciar la clase singleton
    ''' </summary>
    Dim _sessionValues As SessionValues
#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz.
    ''' </summary>
    ''' <param name="iview">The iview.</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByVal iview As ITransportation)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._view = iview
            Me._sessionValues = SessionValues.Instance
        End If
    End Sub

    Public Sub New()
        Me._sessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    Public Async Sub GetSequence()
        Using model As New MBlockRecordAndSequenceMixingStation(Me._view.MyTag)
            Me._view.Sequense = Await model.GetSequence()
        End Using
    End Sub

    Public Async Sub LoadDefinitionLayout()
        Await Me._view.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Inicializa el inventario transportes
    ''' </summary>
    ''' <returns></returns>
    Public Function InitializeItemType() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).FixedAsset.ListFixedAssetItemTypeByStatusAndInventoryType(True, 9)
    End Function

    ''' <summary>
    ''' Inicializa los empleados
    ''' </summary>
    ''' <param name="itemTypeId"></param>
    ''' <returns></returns>
    Public Function InitializePhysicalAssetByItemTypeId(itemTypeId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).FixedAsset.ListFixedAssetPhysicalAssetByItemTypeId(itemTypeId)
    End Function

    ''' <summary>
    ''' Inicializa los empleados
    ''' </summary>
    ''' <returns></returns>
    Public Function InitializeSupplier() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).CommonService.ListSupplierByStatus(True)
    End Function

    ''' <summary>
    ''' Inicializa los empleados
    ''' </summary>
    ''' <returns></returns>
    Public Function InitializeTrademark() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).FixedAsset.ListTrademarkByState(True)
    End Function

    ''' <summary>
    ''' Obtiene el empleado por id
    ''' </summary>
    ''' <param name="physicalAssetId"></param>
    ''' <returns></returns>
    Public Function GetPhysicalAssetById(physicalAssetId As Integer) As FixedAssetPhysicalAssetXpo
        Dim filter As String = "Id = " & physicalAssetId
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).FixedAsset.GetCollection(Of FixedAssetPhysicalAssetXpo)(Nothing, filter).FirstOrDefault()
    End Function

#End Region

End Class
