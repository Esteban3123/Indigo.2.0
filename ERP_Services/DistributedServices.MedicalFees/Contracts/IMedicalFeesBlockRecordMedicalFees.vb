'***********************************************************************
' Assembly         : DistributedServices.Medicalfees
' Author           : Carlos Mario Arias Rubiano
' Created          : 11/12/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IMedicalFeesBlockRecordMedicalFees

    ''' <summary>
    ''' Gets the block record payments by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetBlockRecordMedicalFeesByIdformAndIdRecord(ByVal IdForm As String, ByVal IdRecord As String) As BlockRecordMedicalFees

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <returns>ActionResult</returns>
    <OperationContract()> _
    Function SaveBlockRecordMedicalFees(ByVal BlockRecordMedicalFees As BlockRecordMedicalFees) As ActionResult(Of BlockRecordMedicalFees)

    ''' <summary>
    ''' Elimina una registro bloqueado
    ''' </summary>
    ''' <returns>ActionResult</returns>
    <OperationContract()> _
    Function DeleteBlockRecordMedicalFees(ByVal BlockRecordMedicalFees As BlockRecordMedicalFees) As ActionResult

End Interface
