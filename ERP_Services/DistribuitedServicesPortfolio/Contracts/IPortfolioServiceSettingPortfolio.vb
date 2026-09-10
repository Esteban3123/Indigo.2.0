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
Public Interface IPortfolioServiceSettingPortfolio
    ''' <summary>
    ''' guarda la configuracion de cartera
    ''' </summary>
    ''' <param name="settingPortfolio"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveSettingPortfolio(settingPortfolio As Domain.Entities.SettingPortfolio, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.SettingPortfolio)
    ''' <summary>
    ''' obtiene la configuracion por id de la unidad operativa
    ''' </summary>
    ''' <param name="idOperatingUnit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetSettingPortfolioByIdOperatingUnit(idOperatingUnit As Integer, audit As AuditMessage) As Domain.Entities.SettingPortfolio
End Interface
