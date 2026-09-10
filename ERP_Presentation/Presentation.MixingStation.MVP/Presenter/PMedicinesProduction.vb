'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 06/01/2021
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
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Infrastructure.Data.Xpo.CommonRepository
Imports Infrastructure.Data.Xpo.FixedAssetRepository
#End Region

''' <summary>
''' Este presentador captura toda la logica aplicada en el frontal 
''' </summary>
Public Class PMedicinesProduction

#Region "Variables"

    ''' <summary>
    ''' Se utiliza para instanciar la clase singleton
    ''' </summary>
    Dim _sessionValues As SessionValues

#End Region

#Region "Builder"

    Public Sub New()
        Me._sessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene el listado de medicamentos de producción de cada central de mezclas
    ''' </summary>
    ''' <param name="cmConfigurationId"></param>
    ''' <returns></returns>
    Public Function ListMedicinesProductionByCMId(cmConfigurationId As Integer) As List(Of ViewListMedicinesProductionXpo)
        Dim filter As String = String.Format("CMConfigurationId = {0}", cmConfigurationId)
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewListMedicinesProductionXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Obtiene las centrales de mezcla a las cuales el usuario tiene permiso
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCMByUser() As List(Of MixinStationCMConfigXpo)
        Dim filter As String = "CMConfigurationUsersXpo[UserCode = '" & _sessionValues.UserIndigo & "']"
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of MixinStationCMConfigXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Obtiene los tipos de dosis unitarias
    ''' </summary>
    ''' <returns></returns>
    Public Function InitializeUnitDoseType() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListUnitDoseTypeByStatus(True)
    End Function

    ''' <summary>
    ''' Obtiene los tipos de dosis unitarias por lineas de Producción 
    ''' </summary>
    ''' <returns></returns>
    Public Function InitializeUnitDoseTypeByProductionLine(Ids As String) As List(Of ProductionLineUnitDoseTypeXpo)
        Dim filter As String = String.Format("Id_ProductionLine IN ({0})", Ids)
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ProductionLineUnitDoseTypeXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Obtiene los centros de atencion propios y externos
    ''' </summary>
    ''' <param name="MixingStationId"></param>
    ''' <returns></returns>
    Public Function ListCenterattention(MixingStationId As Integer) As List(Of ViewListCMCenterAttentionXpo)
        Dim filter As String = String.Format("MixingStation = {0} ", MixingStationId)
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewListCMCenterAttentionXpo)(Nothing, filter).ToList()
    End Function

#End Region

End Class
