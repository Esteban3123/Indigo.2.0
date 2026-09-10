'***********************************************************************
' Assembly         : Presentacion.InteropCost.MVP
' Author           : Diego Andrés Roldán
' Created          : 15-01-2015
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

#End Region

Public Class PDistributionIntermediate

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IIntermediateDistribution

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IIntermediateDistribution)
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
        Using Model As New MCommonInteropCost(View.MyTag)
            Me.View.Sequence = Await Model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the production center.
    ''' </summary>
    Public Sub InitializeProductionCenter()
        Me.View.ProductionCenterDataSource = XpoServiceEx.Instance(Indigo.TransactionalContainer).InteropCostService.ListProductionCenterByStatus(True)
        Me.View.ProductionCenterDataSourceRerpository = XpoServiceEx.Instance(Indigo.TransactionalContainer).InteropCostService.ListProductionCenterByStatus(True)
        Me.View.ProductionCenterDataSourceHeader = XpoServiceEx.Instance(Indigo.TransactionalContainer).InteropCostService.ListProductionCenterByStatus(True)
    End Sub

    ''' <summary>
    ''' Carga los parámetros de costos
    ''' </summary>
    Public Sub LoadSettingCost()
        If Me.View.SettingsCost Is Nothing OrElse Me.View.SettingsCost.Id = 0 Then
            Using Model As New MInteropCostSetting(Me.View.MyTag)
                Dim _settingsCost = Model.GetInteropCostSetting()
                If _settingsCost Is Nothing OrElse _settingsCost.Id = 0 Then
                    Me.View.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingCostNotFound", "InteropCost")
                    Exit Sub
                End If
                Me.View.SettingsCost = _settingsCost
            End Using
        End If
    End Sub

End Class
