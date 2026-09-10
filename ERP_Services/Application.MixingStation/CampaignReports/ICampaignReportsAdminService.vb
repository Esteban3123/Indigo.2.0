'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Duván Albeiro Mejia Cortes
' Created          : 08/12/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities


Public Interface ICampaignReportsAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Registra un Documento
    ''' </summary>
    ''' <returns></returns>
    Function SaveReports(Of TEntity As {IObjectWithChangeTracker})(
        campaignDetailId As Integer,
        entityId As Integer,
        Optional ProcessName As String = Nothing
    ) As ActionResult(Of CampaignReports)


    ''' <summary>
    ''' Obtener una Lista Movimientos por CampaignDetail y tipo reporte
    ''' </summary>
    ''' <param name="Data"></param>
    ''' <param name="Audit"></param>
    ''' <returns></returns>
    Function GetListCampaignReportsByAction(Data As Tuple(Of Integer, Integer), Audit As AuditMessage) As List(Of CampaignReports)
End Interface
