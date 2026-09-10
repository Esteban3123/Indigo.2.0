'***********************************************************************
' Assembly         : DistributedServices.MedicalFees
' Author           : Carlos Mario Arias Rubiano
' Created          : 16/12/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IMedicalFeesMedicalFeesNote

    ''' <summary>
    ''' Obtiene las notas de la causacion de honorarios medicos
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetMedicalFeesNotesByAdmissionNumberAndInvoiceId(ByVal AdmissionNumber As String, ByVal InvoiceId As Integer) As ActionResult(Of List(Of MedicalFeesNote))

End Interface
