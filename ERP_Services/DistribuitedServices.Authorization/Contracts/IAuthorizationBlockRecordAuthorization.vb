'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Carlos Mario Arias Rubiano
' Created          : 28/02/2020
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
Public Interface IAuthorizationBlockRecordAuthorization

#Region "Methods"
    ''' <summary>
    ''' Gets the block record portfolio by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetBlockRecordAuthorizationByIdformAndIdRecord(ByVal IdForm As String, ByVal IdRecord As String) As BlockRecordAuthorization

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <returns>ActionResult</returns>
    <OperationContract()>
    Function SaveBlockRecordAuthorization(ByVal BlockRecordAuthorization As BlockRecordAuthorization) As ActionResult(Of BlockRecordAuthorization)

    ''' <summary>
    ''' Elimina una registro bloqueado
    ''' </summary>
    ''' <returns>ActionResult</returns>
    <OperationContract()>
    Function DeleteBlockRecordAuthorization(ByVal BlockRecordAuthorization As BlockRecordAuthorization) As ActionResult
#End Region

End Interface
