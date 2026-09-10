'***********************************************************************
' Assembly         : DistributedServices.AccountManagement
' Author           : Felix Camilo Salazar Roldan
' Created          : 12-12-2024
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
#End Region

<ServiceContract()>
Public Interface IAccountManagementBlockRecordAccountManagement

#Region "Methods"
    ''' <summary>
    ''' Gets the block record portfolio by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetBlockRecordAccountManagementByIdformAndIdRecord(ByVal IdForm As String, ByVal IdRecord As String) As BlockRecordAccountManagement

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <returns>ActionResult</returns>
    <OperationContract()>
    Function SaveBlockRecordAccountManagement(ByVal BlockRecordAccountManagement As BlockRecordAccountManagement) As ActionResult(Of BlockRecordAccountManagement)

    ''' <summary>
    ''' Elimina una registro bloqueado
    ''' </summary>
    ''' <returns>ActionResult</returns>
    <OperationContract()>
    Function DeleteBlockRecordAccountManagement(ByVal BlockRecordAccountManagement As BlockRecordAccountManagement) As ActionResult
#End Region
End Interface


