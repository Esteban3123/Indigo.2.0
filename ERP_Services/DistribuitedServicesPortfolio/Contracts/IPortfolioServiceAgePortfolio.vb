'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Diego Andrés Roldán Lozano
' Created          : 30-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface IPortfolioServiceAgePortfolio

    ''' <summary>
    ''' Guarda una edad de cartera
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveAgesPortfolio(agesPortfolio As AgesPortfolio, audit As AuditMessage) As ActionResult(Of AgesPortfolio)

    ''' <summary>
    ''' Elimina una edad de cartera
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteAgesPortfolio(agesPortfolio As AgesPortfolio, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Lista todos las edades de cartera
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()> _
    Function ListAgesPortfolio() As ActionResult(Of List(Of AgesPortfolio))
    ''' <summary>
    ''' Lista todos las edades de cartera por unidad operativa
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()> _
    Function ListAgesPortfolioByIdSettingPortfolio(idSettingPortfolio As Integer) As List(Of AgesPortfolio)

End Interface
