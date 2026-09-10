'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 04-04-2014
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
Public Interface IBillingBlockRecordBilling

#Region "Methods"
    ''' <summary>
    ''' Gets the block record portfolio by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetBlockRecordBillingByIdformAndIdRecord(ByVal IdForm As String, ByVal IdRecord As String) As BlockRecordBilling

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <returns>ActionResult</returns>
    <OperationContract()> _
    Function SaveBlockRecordBilling(ByVal BlockRecordBilling As BlockRecordBilling) As ActionResult(Of BlockRecordBilling)

    ''' <summary>
    ''' Elimina una registro bloqueado
    ''' </summary>
    ''' <returns>ActionResult</returns>
    <OperationContract()> _
    Function DeleteBlockRecordBilling(ByVal BlockRecordBilling As BlockRecordBilling) As ActionResult
#End Region
End Interface
