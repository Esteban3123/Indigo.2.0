Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository

Public Class CtrMoreInfoProductControl

#Region "Constructor"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        _dictionaryData = New Dictionary(Of String, List(Of ViewListMasInfoQualityXpo))
    End Sub
#End Region

#Region "Properties"
    Private Property _dictionaryData As IDictionary(Of String, List(Of ViewListMasInfoQualityXpo))

    ''' <summary>
    ''' Items to datasource
    ''' </summary>
    ''' <returns></returns>
    Public Property ElaborationParameters As List(Of ViewListMasInfoQualityXpo)
        Get
            Return INDGcElaborationParameters.DataSource
        End Get
        Set(value As List(Of ViewListMasInfoQualityXpo))
            INDGcElaborationParameters.DataSource = value
            INDGcElaborationParameters.RefreshDataSource()
        End Set
    End Property

    ''' <summary>
    ''' Items to datasource
    ''' </summary>
    ''' <returns></returns>
    Public Property DirectRawMaterial As List(Of ViewListMasInfoQualityXpo)
        Get
            Return INDGcDirectRawMaterial.DataSource
        End Get
        Set(value As List(Of ViewListMasInfoQualityXpo))
            INDGcDirectRawMaterial.DataSource = value
            INDGcDirectRawMaterial.RefreshDataSource()
        End Set
    End Property

    ''' <summary>
    ''' Items to datasource
    ''' </summary>
    ''' <returns></returns>
    Public Property AssociatedLabel As List(Of ViewListMasInfoQualityXpo)
        Get
            Return INDgcAssociatedLabel.DataSource
        End Get
        Set(value As List(Of ViewListMasInfoQualityXpo))
            INDgcAssociatedLabel.DataSource = value
            INDgcAssociatedLabel.RefreshDataSource()
        End Set
    End Property

    Public Property PhysicoChemicalParameters As PhysicoChemicalParameter

#End Region

#Region "Methods"
    ''' <summary>
    ''' Load initial data
    ''' </summary>
    Public Sub LoadData(requestMixingStationDetailId As Integer, BatchCode As String)
        If _dictionaryData.ContainsKey(String.Format("{0}-{1}", requestMixingStationDetailId, BatchCode)) Then
            ElaborationParameters = _dictionaryData(String.Format("{0}-{1}", requestMixingStationDetailId, BatchCode))
            DirectRawMaterial = _dictionaryData(String.Format("{0}-{1}", requestMixingStationDetailId, BatchCode))
            AssociatedLabel = _dictionaryData(String.Format("{0}-{1}", requestMixingStationDetailId, BatchCode)).Take(1).ToList()
            LoadChemicalParameter(_dictionaryData(String.Format("{0}-{1}", requestMixingStationDetailId, BatchCode))(0))
            LoadElaborationMixing(_dictionaryData(String.Format("{0}-{1}", requestMixingStationDetailId, BatchCode))(0))
            LoadObservations(_dictionaryData(String.Format("{0}-{1}", requestMixingStationDetailId, BatchCode))(0))
            Exit Sub
        End If

        INDGvElaborationParameters.ShowLoadingPanel()
        INDGvDirectRawMaterial.ShowLoadingPanel()
        INDGvAssociatedLabel.ShowLoadingPanel()
        Task.Factory.StartNew(Sub() LoadDatasource(requestMixingStationDetailId, BatchCode))
    End Sub

    ''' <summary>
    ''' Carga los items de la rejilla
    ''' </summary>
    ''' <param name="requestMixingStationDetailId"></param>
    Private Sub LoadDatasource(requestMixingStationDetailId As Integer, BatchCode As String)
        Dim data = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer) _
            .MixingStationService.GetCollection(Of ViewListMasInfoQualityXpo)(Nothing, $"RequestMixingStationDetailId={requestMixingStationDetailId} AND BatchCode = '{BatchCode}'")
        Me.SafeInvoke(Sub()
                          ElaborationParameters = data
                          DirectRawMaterial = data
                          AssociatedLabel = data.Take(1).ToList()
                          INDGvElaborationParameters.HideLoadingPanel()
                          INDGvDirectRawMaterial.HideLoadingPanel()
                          INDGvAssociatedLabel.HideLoadingPanel()
                          LoadChemicalParameter(data(0))
                          LoadElaborationMixing(data(0))
                          LoadObservations(data(0))
                          _dictionaryData(String.Format("{0}-{1}", requestMixingStationDetailId, BatchCode)) = data
                      End Sub)
    End Sub

    ''' <summary>
    ''' Carga los datos de los parámetros fisico químicos
    ''' </summary>
    ''' <param name="item"></param>
    Private Sub LoadChemicalParameter(item As ViewListMasInfoQualityXpo)
        INDTxtConcentration.Text = item.Concentration.ToString("F2")
        INDTxtMeasurementUnitConcentration.Text = item.ConcentrationMeasurementUnitCodeName
        INDTxtTotalVolume.Text = item.VolumeTotalOrder
        INDTxtMeasurementUnitTotal.Text = item.VolumeMeasurementUnitCodeName
    End Sub

    ''' <summary>
    ''' Carga los datos de Elaboración de la Mezcla
    ''' </summary>
    ''' <param name="item"></param>
    Private Sub LoadElaborationMixing(item As ViewListMasInfoQualityXpo)
        INDTxtPhotoProtection.Text = item.PhotoProtectionName
        INDTxtPreparationType.Text = item.PreparationTypeName
        INDTxtRefrigeratedHours.Text = item.RefrigeratedTerm
        INDTxtEnvironmentalTempHours.Text = item.EnvironmentalTemperatureTerm
        INDTxtPurge.Text = item.Purge.ToString("F2")
        INDtxtInstructions.Text = item.PreparationInstructions
    End Sub

    Private Sub LoadObservations(item As ViewListMasInfoQualityXpo)
        INDMeDetail.EditValue = item.Observations
    End Sub

#End Region

    ''' <summary>
    ''' Parameters
    ''' </summary>
    Public Class PhysicoChemicalParameter
        Public Property Concentration As Decimal
        Public Property MeasurementUnitConcentration As String
        Public Property TotalVolumeOrdered As Decimal
        Public Property MeasurementUnitTotalVolumeOrdered As String
    End Class


End Class
