'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 04-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

#End Region
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IPortfolioBlockRecordPortfolio
#Region "Methods"
    ''' <summary>
    ''' Gets the block record portfolio by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetBlockRecordPortfolioByIdformAndIdRecord(ByVal IdForm As String, ByVal IdRecord As String) As BlockRecordPortfolio

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>ActionResult</returns>
    <OperationContract()> _
    Function SaveBlockRecordPortfolio(ByVal blockRecordPortfolio As BlockRecordPortfolio) As ActionResult(Of BlockRecordPortfolio)

    ''' <summary>
    ''' Elimina una registro bloqueado
    ''' </summary>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>ActionResult</returns>
    <OperationContract()> _
    Function DeleteBlockRecordPortfolio(ByVal blockRecordPortfolio As BlockRecordPortfolio) As ActionResult
#End Region
End Interface
