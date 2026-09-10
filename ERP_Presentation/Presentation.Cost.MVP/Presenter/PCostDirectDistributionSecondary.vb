'***********************************************************************
' Assembly         : Presentacion.Cost.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 14/12/2016
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
Imports Infrastructure.Data.Xpo
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.InteropCostRepository
Imports Infrastructure.Data.Xpo.CostRepository
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.Data.Xpo.AccountingRepository

#End Region

Public Class PCostDirectDistributionSecondary

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As ICostDirectDistributionSecondary

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As ICostDirectDistributionSecondary)
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

    Public Sub LoadSettingCost()
        Using Model As New MCostSetting(Me.View.MyTag)
            Dim _settingsCost = Model.GetCostSetting()
            If _settingsCost Is Nothing OrElse _settingsCost.Id = 0 Then
                Me.View.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingCostNotFound", "InteropCost")
                Exit Sub
            End If
            Me.View.SettingsCost = _settingsCost
        End Using
    End Sub

    Public Async Function GetDataImport(Year As Integer, Month As Integer, ProductionCenterId As Integer) As Task(Of DataTable)
        Try
            Using model As New MCostDirectDistributionSecondary(Me.View.MyTag)
                Dim query As String = String.Format(
"SELECT	dds.ProductionCenterId,
		CONCAT(cpc.Code, ' - ', cpc.Name) ProductionCenterCodeName,
		dds.MeasurementUnitId,
		CONCAT(imu.Code, ' - ', imu.Name) MeasurementUniCodeName,
		dds.Percentage,
		dds.Value
FROM [Cost].[GetLogisticsProductionCenterDistribution]({0}, {1}, {2}) dds
JOIN Cost.CostProductionCenter cpc ON dds.ProductionCenterId = cpc.Id
LEFT JOIN Inventory.InventoryMeasurementUnit imu ON dds.MeasurementUnitId = imu.Id", Year, Month, ProductionCenterId)

                Return Await model.ExecuteQueryDt(query)
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    Public Function GetDistributionSecondary(Id As Integer) As CostDistributionSecondaryXpo
        Dim filtroConsulta As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CostService.GetCollection(Of CostDistributionSecondaryXpo)(Nothing, filtroConsulta).FirstOrDefault()
    End Function


    ''' <summary>
    ''' Obtiene la configuracion de la moneda oficial
    ''' </summary>
    ''' <returns></returns>
    Public Function GetOfficialCurrencyFromCompanySettings() As GeneralLedgerCompanySettingsXpo
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.GetXPOObject(Of GeneralLedgerCompanySettingsXpo)(Nothing)
    End Function
End Class
