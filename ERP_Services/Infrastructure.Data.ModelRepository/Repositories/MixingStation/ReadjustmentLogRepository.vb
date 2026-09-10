'***********************************************************************
' Assembly         : Infrastructure.Data.ReadjustmentLogRepository
' Author           : Diego A. Roldan
' Created          : 2023-01-17
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base

#End Region

''' <summary>
''' Repositorio de la entidad Readecuaciones
''' </summary>
Public Class ReadjustmentLogRepository
    Inherits GenericRepository(Of ReadjustmentLog)
    Implements IReadjustmentLogRepository, Inject

#Region "Fields"

    ''' <summary>
    ''' Contexto de contabilidad
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="acountingContext">Contexto de cintabilidad</param>
    Public Sub New(ByVal acountingContext As IGlobalModelUnitOfWork)
        MyBase.New(acountingContext)
        Me._context = acountingContext
    End Sub

    Public Function GetCampaignDetailIdsByBatchCode(cmConfigurationId As Integer, productionLineId As Integer, batchCode As String) As List(Of Integer) Implements IReadjustmentLogRepository.GetCampaignDetailIdsByBatchCode
        Dim q1 = (From rl In _context.ReadjustmentLog
                  Join rmd In _context.RequestMixingStationDetail On rl.RequestMixingStationDetailId Equals rmd.Id
                  Join cd In _context.CampaignDetail On rmd.CampaignDetailId Equals cd.Id
                  Join c In _context.Campaign On cd.CampaignId Equals c.Id
                  Where rl.BatchCode.StartsWith(batchCode) AndAlso rmd.CampaignDetailId.HasValue _
                    AndAlso c.CMConfigurationId = cmConfigurationId AndAlso cd.ProductionLineId = productionLineId AndAlso cd.CampaignStatus = 6
                  Select rmd.CampaignDetailId.Value).Distinct().ToList()

        Dim q2 = (From rp In _context.RequestPackageDetailStatus
                  Join rd In _context.RequestMixingStationDetail On rp.RequestMixingStationDetailId Equals rd.Id
                  Join cd In _context.CampaignDetail On rd.CampaignDetailId Equals cd.Id
                  Join c In _context.Campaign On cd.CampaignId Equals c.Id
                  Where rp.BatchCode.StartsWith(batchCode) AndAlso rd.CampaignDetailId.HasValue _
                    AndAlso c.CMConfigurationId = cmConfigurationId AndAlso cd.ProductionLineId = productionLineId AndAlso cd.CampaignStatus = 6
                  Select rd.CampaignDetailId.Value).Distinct().ToList()

        Return q1.Union(q2).Distinct().ToList()
    End Function

#End Region

End Class
