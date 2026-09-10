'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Giovanny Plazas Lozano
' Created          : 29/08/2022
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
Imports System.Globalization
Imports Infrastructure.Data.Xpo.CrystalRepository
#End Region

''' <summary>
''' Este presentador captura toda la logica aplicada en el frontal 
''' </summary>
Public Class PReadjustments

#Region "Variables"
    ''' <summary>
    ''' Variable para instanciar la interfaz
    ''' </summary>
    Dim _view As IReadjustments
    ''' <summary>
    ''' Se utiliza para instanciar la clase singleton
    ''' </summary>
    Dim _sessionValues As SessionValues

#End Region

#Region "Builder"

    Public Sub New(ByVal iview As IReadjustments)
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
    ''' metodo para inicializar la rejilla principal filtrada por la central de mezclas
    ''' </summary>
    Public Async Function InitializeReadjustments() As Task
        Using Model As New MReadjustments(_view.MyTag)
            _view.ListViewReadjustmentsXpo = Await Task.Factory.StartNew(Function() As List(Of ViewReadjustmentsXpo)
                                                                             Return Model.GetReadjustmentsByCMConfigurationId(_view.CMConfigurationId)
                                                                         End Function)

        End Using
    End Function

    ''' <summary>
    ''' metodo para inicializar las centrales de mezclas
    ''' </summary>
    Public Sub InitializeCMConfiguration()
        Dim Presenter = New PDashboardConfirmationUnitDose()
        _view.ListCMConfiguration = Presenter.ListCMByUser()
    End Sub
#End Region

End Class
