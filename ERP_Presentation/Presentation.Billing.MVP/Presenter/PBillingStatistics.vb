'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 10/07/2019
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Presentation.Base

Public Class PBillingStatistics

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
    Public Function LoadDatasourceThirdParty() As DevExpress.Xpo.XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListThirdPartyReport()
    End Function

    ''' <summary>
    ''' Carga el datasource de la entidad
    ''' </summary>
    ''' <returns></returns>
    Public Function LoadDatasourceHealthAdministrator() As DevExpress.Xpo.XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListHealthAdministrator()
    End Function

    ''' <summary>
    ''' Carga el datasource del grupo de atención
    ''' </summary>
    ''' <returns></returns>
    Public Function LoadDatasourceCareGroup() As DevExpress.Xpo.XPCollection
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListAllCareGroup()
    End Function

    ''' <summary>
    ''' Carga el datasource de los centros de atención
    ''' </summary>
    ''' <returns></returns>
    Public Function LoadDatasourceCareCenter() As DevExpress.Xpo.XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetAllCareCenter()
    End Function

    ''' <summary>
    ''' Carga el datasource de los usuarios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadDatasourceUsers() As DevExpress.Data.Linq.LinqInstantFeedbackSource
        'Return XpoServiceEx.Instance(Indigo.SecurityContainer).SecurityService.GetAllUser()
        Return XpoServiceEx.Instance(Indigo.SecurityContainer).SecurityService.ListUserByContainer(Indigo.IndigoContainerId)
    End Function

    ''' <summary>
    ''' Carga el datasource de las unidades funcionales
    ''' </summary>
    ''' <returns></returns>
    Public Function LoadDatasourceFunctionalUnit() As DevExpress.Xpo.XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetFunctionalUnit()
    End Function

    ''' <summary>
    ''' Carga el datasource de los profesionales
    ''' </summary>
    ''' <returns></returns>
    Public Function LoadDatasourceProfessional() As DevExpress.Xpo.XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListHealthCareProfessionalAll()
    End Function

    ''' <summary>
    ''' Carga el datasource de los servicios ips
    ''' </summary>
    ''' <returns></returns>
    Public Function LoadDatasourceIPSService() As DevExpress.Xpo.XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.ListCUPSEntityAndInventoryProduct()
    End Function

    ''' <summary>
    ''' Obtiene el tercero por nit
    ''' </summary>
    ''' <param name="nit"></param>
    ''' <returns></returns>
    Public Function GetThirdPartyByNit(nit As String) As AccountingRepository.CommonThirdPartyXpo
        Dim filter As String = "Nit = '" + nit + "'"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetCollection(Of AccountingRepository.CommonThirdPartyXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene la entidad por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetHaelthAdministratorByCode(code As String) As HealthAdministratorXpo
        Dim filter As String = "Code = '" + code + "'"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of HealthAdministratorXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene el grupo de atención por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetCareGroupByCode(code As String) As ContractCareGroupXpo
        Dim filter As String = "Code = '" + code + "'"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of ContractCareGroupXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene el grupo de atención por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetCareCenterByCode(code As String) As ADCENATEN
        Dim filter As String = "CODCENATE = '" + code + "'"
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetCollection(Of ADCENATEN)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene la informacion para generar el excel
    ''' </summary>
    ''' <param name="filter"></param>
    ''' <returns></returns>
    Public Async Function GetDataGenerateExcel(filter As String) As Task(Of List(Of BillingRepository.ViewBillingStatisticsWithServicesXpo))
        Dim result = Nothing
        Await Task.Run(Sub()
                           result = XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.GetCollection(Of BillingRepository.ViewBillingStatisticsWithServicesXpo)(Nothing, filter)
                       End Sub)
        Return result
    End Function
End Class
