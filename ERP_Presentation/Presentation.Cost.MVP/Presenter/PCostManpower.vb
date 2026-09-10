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
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AccountingRepository

#End Region

Public Class PCostManpower

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As ICostManpower

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As ICostManpower)
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
                    Me.View.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingCostNotFound", "Cost")
                    Exit Sub
                End If
                Me.View.SettingsCost = _settingsCost
            End Using
        Else
            Me.View.SettingsCost = Me.View.SettingsCost
        End If
    End Sub

    ''' <summary>
    ''' Initializes the production center.
    ''' </summary>
    Public Sub InitializeProductionCenter()
        Using mProductionCenter As New MCostProductionCenter(Me.View.MyTag)
            Me.View.ProductionCenterDataSource = mProductionCenter.ListCostProductionCenterByStatus(True)
            Me.View.ProductionCenterDataSourceRerpository = mProductionCenter.ListCostProductionCenterByStatus(True)
        End Using
    End Sub

    ''' <summary>
    ''' Obtiene el Tercero por el Id del Cliente
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetViewCostDistributionManpowerByManpowerTypeId(ManpowerTypeId As Integer) As CostRepository.ViewCostDistributionManpower
        Dim filter As String = "ManpowerTypeId = " & ManpowerTypeId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CostService.GetCollection(Of CostRepository.ViewCostDistributionManpower)(Nothing, filter).FirstOrDefault()
    End Function


    ''' <summary>
    ''' Obtiene la configuracion de la moneda oficial
    ''' </summary>
    ''' <returns></returns>
    Public Function GetOfficialCurrencyFromCompanySettings() As GeneralLedgerCompanySettingsXpo
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.GetXPOObject(Of GeneralLedgerCompanySettingsXpo)(Nothing)
    End Function

    ''' <summary>
    ''' Obtiene el DataSource de la moneda
    ''' </summary>
    Public Sub InitializeCurrencyXpo()
        Me.View.CurrencyXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetCurrency(True)
    End Sub

End Class