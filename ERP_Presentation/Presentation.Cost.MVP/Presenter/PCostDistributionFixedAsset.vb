'***********************************************************************
' Assembly         : Presentacion.InteropCost.MVP
' Author           : Diego Andrés Roldán
' Created          : 11-12-2014
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
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Infrastructure.Data.Xpo.CostRepository
Imports Infrastructure.Data.Xpo.AccountingRepository

#End Region

Public Class PCostDistributionFixedAsset

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As ICostFixedAssetDistribution

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As ICostFixedAssetDistribution)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Obtiene la secuencia
    ''' </summary>
    Public Async Sub GetSequence()
        Using Model As New MCommonCost(View.MyTag)
            Me.View.Sequence = Await Model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Carga los parámetros de costos
    ''' </summary>
    Public Sub LoadSettingCost()
        If Me.View.SettingsCost Is Nothing OrElse Me.View.SettingsCost.Id = 0 Then
            Using Model As New MCostSetting(Me.View.MyTag)
                Dim _settingsCost = Model.GetCostSetting()
                If _settingsCost Is Nothing OrElse _settingsCost.Id = 0 Then
                    Me.View.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingCostNotFound", "InteropCost")
                    Exit Sub
                End If
                Me.View.SettingsCost = _settingsCost
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Initializes the production center.
    ''' </summary>
    Public Sub InitializeProductionCenter()
        Me.View.ProductionCenterDataSource = XpoServiceEx.Instance(Indigo.TransactionalContainer).CostService.ListCostProductionCenterByStatus(True)
        Me.View.ProductionCenterDataSourceRerpository = XpoServiceEx.Instance(Indigo.TransactionalContainer).CostService.ListCostProductionCenterByStatus(True)
    End Sub

    ''' <summary>
    ''' Obtiene el activo por la placa
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetViewCostDistributionFixedAssetByPhysicalAssetId(PhysicalAssetId As Integer) As CostRepository.ViewCostDistributionFixedAsset
        Dim filter As String = "FixedAssetPhysicalAssetId = " & PhysicalAssetId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CostService.GetCollection(Of CostRepository.ViewCostDistributionFixedAsset)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Lista el costo de las depreciaciones
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListFixedAssetDepreciationDetailCost(FixedAssetDepreciationDetailId As Integer) As List(Of FixedAssetDepreciationDetailCostXpo)
        Dim filtroConsulta As String = "FixedAssetDepreciationDetailId.Id = " & FixedAssetDepreciationDetailId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.GetCollection(Of FixedAssetDepreciationDetailCostXpo)(Nothing, filtroConsulta)
    End Function

    ''' <summary>
    ''' Valida que el centro de costo este en un centro de producción
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ValidateCostCenterInProductionCenter(costCenterId As Integer) As List(Of CostProductionCenterCostCenterXpo)
        Dim filtroConsulta As String = "CostCenterId.Id = " & costCenterId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.GetCollection(Of CostProductionCenterCostCenterXpo)(Nothing, filtroConsulta)
    End Function

    ''' <summary>
    ''' Obtiene la configuracion de la moneda oficial
    ''' </summary>
    ''' <returns></returns>
    Public Function GetOfficialCurrencyFromCompanySettings() As GeneralLedgerCompanySettingsXpo
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.GetXPOObject(Of GeneralLedgerCompanySettingsXpo)(Nothing)
    End Function

End Class