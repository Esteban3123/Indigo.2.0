'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 01-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
#End Region

<ServiceContract()> _
Public Interface IPortfolioProvisionRanges

#Region "Methods"
    ''' <summary>
    ''' metodo para obtener un rango de provision
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetProvisionRangesByCode(code As String, audit As AuditMessage) As Object

    ''' <summary>
    ''' metodo para guardar un rango de provision
    ''' </summary>    
    <OperationContract()>
    Function SaveProvisionRanges(provisionRanges As ProvisionRanges, idSequense As Int64, audit As AuditMessage) As ActionResult(Of ProvisionRanges)

    ''' <summary>
    ''' metodo para eliminar un rango de provision
    ''' </summary>    
    <OperationContract()>
    Function DeleteProvisionRanges(provisionRanges As ProvisionRanges, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ChangeStateProvisionRanges(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ProvisionRanges)
#End Region
End Interface
