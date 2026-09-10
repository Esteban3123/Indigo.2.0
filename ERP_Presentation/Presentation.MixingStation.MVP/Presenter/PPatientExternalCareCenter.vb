'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/10/2020
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
#End Region

''' <summary>
''' Este presentador captura toda la logica aplicada en el frontal 
''' </summary>
Public Class PPatientExternalCareCenter

#Region "Variables"
    ''' <summary>
    ''' Variable para instanciar la interfaz
    ''' </summary>
    Dim _view As IPatientExternalCareCenter

    ''' <summary>
    ''' Se utiliza para instanciar la clase singleton
    ''' </summary>
    Dim _sessionValues As SessionValues

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Private Indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz.
    ''' </summary>
    ''' <param name="iview">The iview.</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByVal iview As IPatientExternalCareCenter)
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

    Public Async Sub LoadDefinitionLayout()
        Await Me._view.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Método que consulta los tipo de identificacion del ehr y se los asigna al datasource del control
    ''' </summary>
    Public Sub InitializaListIdentificationType()
        _view.IdentificationTypeDatasource = XpoServiceEx.Instance(Indigo.TransactionalContainer).CrystalService.ListADTIPOIDENTIFICAxpoActive()
    End Sub

    ''' <summary>
    ''' Método que consulta los géneros y se los asigna al datasource del control
    ''' </summary>
    Public Function InitializaListGender() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MixingStationService.ListActiveGenderTypes()
    End Function

    ''' <summary>
    ''' Obtiene el género por Id
    ''' </summary>
    ''' <returns></returns>
    Public Function GetGenderTypeById(id As Integer) As MixingStationRepository.GenderTypesXpo
        Dim filter As String = "Id = " & id
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of MixingStationRepository.GenderTypesXpo)(Nothing, filter).FirstOrDefault()
    End Function

#End Region

End Class
