'***********************************************************************
' Assembly         : Presentacion.InteropCost.MVP
' Author           : Diego Andrés Roldán
' Created          : 30-12-2014
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

#End Region

Public Class PManpower

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IManpower

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IManpower)
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
        Else
            Me.View.SettingsCost = Me.View.SettingsCost
        End If
    End Sub

    ''' <summary>
    ''' Lista los empleados con contrato activo
    ''' </summary>
    Public Sub InitializeEmployee()
        Using Model As New MBusqueda
            Me.View.EmployeeDatasource = Model.ConsultarEntidades(eDataSource.ListEmployeeWithActiveContract)
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the production center.
    ''' </summary>
    Public Sub InitializeProductionCenter()
        Using Model As New MBusqueda
            Dim filter() As Object = {True}
            Me.View.ProductionCenterDataSource = Model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListProductionCenterByStatus, filter)
            Me.View.ProductionCenterDataSourceRerpository = Model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListProductionCenterByStatus, filter)
        End Using
    End Sub

End Class