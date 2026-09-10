'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 01-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface IPortfolioEconomicIndicator

#Region "Methods"
    ''' <summary>
    ''' Metodo para obtener un indicador economico por codigo
    ''' </summary>
    <OperationContract()>
    Function GetEconomicIndicatorByCode(code As String, audit As AuditMessage) As EconomicIndicator
    ''' <summary>
    ''' Metodo para listar todos los 
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAllEconomicIndicator(audit As AuditMessage) As List(Of EconomicIndicator)

    ''' <summary>
    ''' Metodo para obtener un indicador economico por codigo
    ''' </summary>
    <OperationContract()>
    Function GetEconomicIndicator(year As String, month As String, audit As AuditMessage) As EconomicIndicator
    ''' <summary>
    ''' metodo para guardar un indicador economico
    ''' </summary>
    ''' <param name="economicIndicator">The economic indicator.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveEconomicIndicator(economicIndicator As EconomicIndicator, idSequense As Int64, audit As AuditMessage) As ActionResult(Of EconomicIndicator)

    ''' <summary>
    ''' metodo para eliminar un indicador economico
    ''' </summary>
    ''' <param name="economicIndicator">The economic indicator.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteEconomicIndicator(economicIndicator As EconomicIndicator, audit As AuditMessage) As ActionResult
    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ChangeStateEconomicIndicator(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.EconomicIndicator)
#End Region
End Interface
