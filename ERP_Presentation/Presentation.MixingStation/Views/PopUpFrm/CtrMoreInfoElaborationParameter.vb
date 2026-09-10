Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository

Public Class CtrMoreInfoElaborationParameter

#Region "Constructor"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        _dictionaryData = New Dictionary(Of Integer, List(Of ViewListRawMaterialXpo))
    End Sub
#End Region

#Region "Properties"
    Private Property _dictionaryData As IDictionary(Of Integer, List(Of ViewListRawMaterialXpo))

    ''' <summary>
    ''' Items to datasource
    ''' </summary>
    ''' <returns></returns>
    Public Property ElaborationParameters As List(Of ViewListRawMaterialXpo)
        Get
            Return INDGcElaborationParameters.DataSource
        End Get
        Set(value As List(Of ViewListRawMaterialXpo))
            INDGcElaborationParameters.DataSource = value
            INDGcElaborationParameters.RefreshDataSource()
        End Set
    End Property

    Public Property PhysicoChemicalParameters As PhysicoChemicalParameter
#End Region

#Region "Methods"
    ''' <summary>
    ''' Load initial data
    ''' </summary>
    Public Sub LoadData(data As List(Of ViewListRawMaterialXpo))
        ElaborationParameters = data
        Dim mainData = data.Where(Function(d) d.MainMedicine = True).FirstOrDefault
        If mainData Is Nothing Then
            mainData = data(0)
        End If

        LoadChemicalParameter(mainData, If(data.FindAll(Function(m) m.Vehicle OrElse m.Thinner)?.Sum(Function(m) m.Quantity), 0))
        LoadElaborationMixing(mainData)
    End Sub

    ''' <summary>
    ''' Load initial data
    ''' </summary>
    Public Sub SetData(requestMixingStationDetailId As Integer)
        If _dictionaryData.ContainsKey(requestMixingStationDetailId) Then
            LoadData(_dictionaryData(requestMixingStationDetailId))
            Exit Sub
        End If

        INDGvElaborationParameters.ShowLoadingPanel()
        Task.Factory.StartNew(Sub() LoadDatasource(requestMixingStationDetailId))
    End Sub

    ''' <summary>
    ''' Carga los items de la rejilla
    ''' </summary>
    ''' <param name="requestMixingStationDetailId"></param>
    Private Sub LoadDatasource(requestMixingStationDetailId As Integer)
        Dim data = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer) _
            .MixingStationService.GetCollection(Of ViewListRawMaterialXpo)(Nothing, $"RequestMixingStationDetailId={requestMixingStationDetailId}")
        Me.SafeInvoke(Sub()
                          LoadData(data)
                          INDGvElaborationParameters.HideLoadingPanel()
                          _dictionaryData(requestMixingStationDetailId) = data
                      End Sub)
    End Sub

    ''' <summary>
    ''' Carga los datos de los parámetros fisico químicos
    ''' </summary>
    ''' <param name="item"></param>
    Private Sub LoadChemicalParameter(item As ViewListRawMaterialXpo, volumen As Decimal)
        INDTxtConcentration.Text = item.Concentration
        INDTxtMeasurementUnitConcentration.Text = item.ConcentrationMeasurementUnitCodeName
        INDTxtTotalVolume.Text = volumen 'item.VolumeTotalOrder
        INDTxtMeasurementUnitTotal.Text = item.VolumeMeasurementUnitCodeName
    End Sub

    ''' <summary>
    ''' Carga los datos de Elaboración de la Mezcla
    ''' </summary>
    ''' <param name="item"></param>
    Private Sub LoadElaborationMixing(item As ViewListRawMaterialXpo)
        INDTxtPhotoProtection.Text = item.PhotoProtectionName
        INDTxtPreparationType.Text = item.PreparationTypeName
        INDTxtRefrigeratedHours.Text = item.RefrigeratedTerm
        INDTxtEnvironmentalTempHours.Text = item.EnvironmentalTemperatureTerm
        INDTxtPurge.Text = item.Purge.ToString("F2")
        INDtxtInstructions.Text = item.PreparationInstructions
        INDtxtAdditionalInstructions.Text = item.SpecialConsiderations
    End Sub
#End Region

#Region "Events"
    ''' <summary>
    ''' Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub CtrMoreInfoElaborationParameter_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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
