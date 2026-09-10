'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Johan Sebastian Carranza Ramos
' Created          : 01/10/2019
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data
Imports DevExpress.Data.Linq
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Presentation.Base
Imports Presentation.CloudAgent

Public Class PAdmissionsStadistical

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues
    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New()
        Indigo = SessionValues.Instance
    End Sub
    ''' <summary>
    ''' Carga el datasource del tercero
    ''' </summary>
    ''' <returns></returns>
    Public Function LoadDatasourceThirdParty() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListThirdPartyReport()
    End Function

    ''' <summary>
    ''' Carga el datasource de la entidad
    ''' </summary>
    ''' <returns></returns>
    Public Function LoadDatasourceHealthAdministrator() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListHealthAdministrator()
    End Function

    ''' <summary>
    ''' Carga el datasource del grupo de atención
    ''' </summary>
    ''' <returns></returns>
    Public Function LoadDatasourceCareGroup() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListCareGroup()
    End Function

    ''' <summary>
    ''' Carga el datasource de los centros de atención
    ''' </summary>
    ''' <returns></returns>
    Public Function LoadDatasourceCareCenter() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetAllCareCenter()
    End Function

    ''' <summary>
    ''' Carga el datasource de los usuarios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadDatasourceUsers() As LinqInstantFeedbackSource
        'Return XpoServiceEx.Instance(Indigo.SecurityContainer).SecurityService.GetAllUser()
        Return XpoServiceEx.Instance(Indigo.SecurityContainer).SecurityService.ListUserByContainer(Indigo.IndigoContainerId)
    End Function

    ''' <summary>
    ''' Carga el datasource de las unidades funcionales 
    ''' </summary>
    ''' <returns></returns>
    Public Function LoadDatasourceFunctionalUnit() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetAllFunctionalUnit()
    End Function
    ''' <summary>
    ''' Funcion para consultar el Store procedure guardado en BD para traer el estadistico de ingresos.
    ''' </summary>
    ''' <returns></returns>
    Public Async Function LoadDataSourceStatistics(ParametrosString As List(Of String), ParametrosDate As List(Of Date), ByVal session As SessionValues) As Task(Of DataTable)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.SPCH_ReportAdmissionStatisticsAsync(ParametrosString, ParametrosDate, session)
    End Function
End Class
