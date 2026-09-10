'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Yoe Andres Cardenas
' Created          : 06-06-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base

#End Region

''' <summary>
''' Este presentador captura toda la logica aplicada en el frontal 
''' </summary>
Public Class PDilutionFactors

#Region "Variables"
    ''' <summary>
    ''' Variable para instanciar la interfaz
    ''' </summary>
    Dim _view As IDilutionFactors

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
    Public Sub New(ByVal iview As IDilutionFactors)
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

    Public Async Function GetSequence() As Task
        Using model As New MBlockRecordAndSequenceMixingStation(Me._view.MyTag)
            Me._view.Sequence = Await model.GetSequence()
        End Using
    End Function

    Public Async Sub LoadDefinitionLayout()
        Await Me._view.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Initializes the ATC.
    ''' </summary>
    Public Sub InitializeATC()
        Me._view.ATCDatasource = XpoServiceEx.Instance(_sessionValues.TransactionalContainer).InventoryService.ListATCbyFilter("FormulationType =1 and PharmaceuticalFormId is not null and DiluentProduct=0 and SuitableForReconstitution =1")
    End Sub

    ''' <summary>
    ''' Obtiene el detalle de la stabilidad por medio del ATCId
    ''' </summary>
    ''' <param name="atcId"></param>
    ''' <returns></returns>
    Public Async Function GetStabilityTableDetailByATCId(atcId As Integer) As Task(Of List(Of StabilityTableDetailXpo))
        Dim filter As String = "ATCId.Id = " & atcId
        Return Await Task.Run(Function() XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of StabilityTableDetailXpo)(Nothing, filter).ToList())
    End Function

    ''' <summary>
    ''' Obtiene los detalles tipo 'Dilucion' parametrizados para el medicamento de la cabecera
    ''' </summary>
    Public Async Function GetDilutionStabilityTableDetailByATCIdHeader(_AtcId As Integer) As Task(Of List(Of StabilityTableDetailReconstitutionXpo))
        Dim filter As String = "StabilityTableDetailId.ATCId.Id = " & _AtcId
        Return Await Task.Run(Function() XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of StabilityTableDetailReconstitutionXpo)(Nothing, filter).ToList())
    End Function

    ''' <summary>
    ''' Obtiene el detalle de reconstitucion del medicamento seleccionado segun el medicamento de la cabecera
    ''' </summary>
    Public Async Function GetStabilityTableDetailReconstitutionByAtcId(_AtcId As Integer, _AtcIdHeader As Integer) As Task(Of StabilityTableDetailReconstitutionXpo)
        Dim filter As String = "ATCId.Id = " & _AtcId & " And StabilityTableDetailId.ATCId.Id = " & _AtcIdHeader
        Return Await Task.Run(Function() XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetXPOObject(Of StabilityTableDetailReconstitutionXpo)(filter))
    End Function

    ''' <summary>
    ''' Obtiene el factor de dilucion mediante el Id del medicamento
    ''' </summary>
    ''' <param name="DilutionID"></param>
    ''' <returns></returns>
    Public Function GetAtcDilutionById(DilutionID As Integer) As DilutionFactorsXpo
        Dim filter As String = "ATCId = " & DilutionID
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetXPOObject(Of DilutionFactorsXpo)(filter)
    End Function

    ''' <summary>
    ''' Obtiene el detalle del factor de dilucion mediante el Id del medicamento
    ''' </summary>
    ''' <param name="DilutionDetailID"></param>
    ''' <returns></returns>
    Public Function GetDilutionDetailById(DilutionDetailId As Integer) As DilutionFactorsDetailXpo
        Dim filter As String = "AtcId = " & DilutionDetailId
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetXPOObject(Of DilutionFactorsDetailXpo)(filter)
    End Function

    ''' <summary>
    ''' Obtiene el detalle del factor de dilucion mediante el factor de diluación y el reconstituyente seleccionado
    ''' </summary>
    Public Function GetDilutionDetailByIdDilutionFactorAndAtc(ReconstituyenteId As Integer, dilutionFactorId As Integer) As DilutionFactorsDetailXpo
        Dim filter As String = String.Format("AtcId = {0} AND DilutionFactorsId = {1}", ReconstituyenteId, dilutionFactorId)
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetXPOObject(Of DilutionFactorsDetailXpo)(filter)
    End Function

    Public Async Function DefaultMeasureUnit() As Task
        Me._view.MeasurementUnit = Await Task.Factory.StartNew(Function() As MeasureUnitXpo
                                                                   Dim filter As String = $"Code='002'"
                                                                   Using model As New MDilutionFactors(_view.MyTag)
                                                                       Return model.DefaultMeasureUnit(filter)
                                                                   End Using
                                                               End Function)
    End Function
#End Region

End Class
