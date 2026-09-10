'***********************************************************************
' Assembly         : Application.Portfolio
' Author           : Diego Andrés Roldán Lozano
' Created          : 30-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ISettingPortfolioAdminService
    Inherits IDisposable
    ''' <summary>
    ''' guardar los parametros de cartera
    ''' </summary>
    ''' <param name="settingPortfolio"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveSettingPortfolio(settingPortfolio As SettingPortfolio, audit As AuditMessage) As ActionResult(Of SettingPortfolio)
    ''' <summary>
    ''' obtiene los parametros por unidad operativa
    ''' </summary>
    ''' <param name="idOperatingUnit"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSettinPortfolioByIdOperatingUnit(idOperatingUnit As Integer, audit As AuditMessage) As SettingPortfolio
End Interface
