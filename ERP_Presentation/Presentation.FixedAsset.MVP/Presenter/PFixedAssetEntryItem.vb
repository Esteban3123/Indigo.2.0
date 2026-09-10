'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 20-01-2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Infrastructure.Data.Xpo.PaymentsRepository

#End Region

Public Class PFixedAssetEntryItem

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IFixedAssetEntryItem

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IFixedAssetEntryItem)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        Me.View = iview
        Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Listado de los equipos por estado
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeItem(Optional AdquisitionType As Integer = 0)
        View.ItemXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListFixedAssetEquipmentByStatus(True, AdquisitionType)
    End Sub

    ''' <summary>
    ''' lista las Marcas
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeTrademark()
        View.TrademarkXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListTrademarkByState(True)
    End Sub

    ''' <summary>
    ''' lista los IVA
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeIVA()
        View.IvaXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListGeneralLedgerIvaByStatus(True)
    End Sub

    ''' <summary>
    ''' lista las Marcas
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializePolicy()
        View.PolicyXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListFixedAssetPolizaByStatus(True)
    End Sub

    ''' <summary>
    ''' Obtiene el articulo por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetFixedAssetItemById(Id As Integer) As FixedAssetEquipmentXpo
        Dim filtroConsulta As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.GetCollection(Of FixedAssetEquipmentXpo)(Nothing, filtroConsulta).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Lista las remisiones de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetAccountPayableConceptById(Id As Integer) As PaymentsAccountPayableConceptsXpoP
        Dim filter As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetCollection(Of PaymentsAccountPayableConceptsXpoP)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Lista las remisiones de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetRetentionConceptById(Id As Integer) As GeneralLedgerRetentionConceptsReportXpo
        Dim filter As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetCollection(Of GeneralLedgerRetentionConceptsReportXpo)(Nothing, filter).FirstOrDefault()
    End Function


    ''' <summary>
    ''' Obtiene los parametros de activos fijos
    ''' </summary>
    ''' <param name="OperatingUnitId"></param>
    Public Async Sub GetSettingFixedAssetByOperatingUnitId(OperatingUnitId As Integer)
        Using model As New MSettingFixedAsset()
            View.SettingsFixedAsset = Await model.GetSettingFixedAssetByOperatingUnitId(OperatingUnitId)
        End Using
    End Sub

#End Region

End Class
