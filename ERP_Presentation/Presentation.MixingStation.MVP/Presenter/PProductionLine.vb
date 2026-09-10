'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Ruben Dario Castañeda Giraldo
' Created          : 05-08-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent.IndigoReference.MixingStation
Imports Presentation.Controls.MVP
#End Region
Public Class PProductionLine
#Region "Variables"
    ''' <summary>
    ''' Variable para instanciar la interfaz
    ''' </summary>
    Dim _view As IProductionLine

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
    Public Sub New(ByVal iview As IProductionLine)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._view = iview
            Me._sessionValues = SessionValues.Instance
        End If
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Inicia los tipos de dosis unitarias
    ''' </summary>
    Public Sub InitializeUnitDoseType()
        Using Model As New MBusqueda
            Me._view.UnitDoseTypeDatasource = Model.ConsultarEntidades(eDataSource.ListUnitDoseType)
        End Using
    End Sub
    Public Sub InitializeFuctionalUnit()
        Using Model As New MBusqueda
            Me._view.FuctionalUnitDatasource = Model.ConsultarEntidades(eDataSource.FunctionalUnit)
        End Using
    End Sub
    Public Async Sub GetSequence()
        Using model As New MBlockRecordAndSequenceMixingStation(Me._view.MyTag)
            Me._view.Sequence = Await model.GetSequence()
        End Using
    End Sub

    Public Async Sub LoadDefinitionLayout()
        Await Me._view.MyLayoutControl.LoadDefinitionAsync()
    End Sub
#End Region
End Class
