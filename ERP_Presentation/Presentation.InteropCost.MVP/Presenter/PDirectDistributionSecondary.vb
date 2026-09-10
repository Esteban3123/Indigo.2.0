'***********************************************************************
' Assembly         : Presentacion.InteropCost.MVP
' Author           : Diego Andrés Roldán
' Created          : 11-12-2014
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

#End Region

Public Class PDirectDistributionSecondary
    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IDirectDistributionSecondary

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IDirectDistributionSecondary)
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

    Public Sub LoadSettingCost()
        Using Model As New MInteropCostSetting(Me.View.MyTag)
            Dim _settingsCost = Model.GetInteropCostSetting()
            If _settingsCost Is Nothing OrElse _settingsCost.Id = 0 Then
                Me.View.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingCostNotFound", "InteropCost")
                Exit Sub
            End If
            Me.View.SettingsCost = _settingsCost
        End Using
    End Sub

    Public Function GetDataImport(ProductionCenterId As Integer, Month As Integer, Year As Integer) As List(Of LogisticsProductionCenterRecordDetailXpo)
        'Filtro de la consulta
        Dim filtroConsulta As String = "LogisticsProductionCenterRecordId.ProductionCenterId.Id = " & ProductionCenterId & " And GetMonth(LogisticsProductionCenterRecordId.RecordDate) = " & Month.ToString & " And GetYear(LogisticsProductionCenterRecordId.RecordDate) = " & Year.ToString & " And Import = 0"
        'Se retornan los detalles que se obtienen por medio del filtro
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InteropCostService.GetCollection(Of LogisticsProductionCenterRecordDetailXpo)(Nothing, filtroConsulta)
    End Function

    Public Function GetDistributionSecondary(Id As Integer) As DistributionSecondaryXpo
        Dim filtroConsulta As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InteropCostService.GetCollection(Of DistributionSecondaryXpo)(Nothing, filtroConsulta).FirstOrDefault()
    End Function

End Class
