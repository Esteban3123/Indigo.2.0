'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Application.MedicalFees
Imports Microsoft.Practices.Unity

Partial Class MedicalFeesService

    ''' <summary>
    ''' Obtiene las notas de causacion de honorarios medicos
    ''' </summary>
    ''' <param name="AdmissionNumber"></param>
    ''' <param name="InvoiceId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMedicalFeesNotesByAdmissionNumberAndInvoiceId(AdmissionNumber As String, InvoiceId As Integer) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.MedicalFeesNote)) Implements IMedicalFeesMedicalFeesNote.GetMedicalFeesNotesByAdmissionNumberAndInvoiceId
        Using service As IMedicalFeesNoteAdminService = Container.Current.Resolve(Of IMedicalFeesNoteAdminService)()
            Return service.GetMedicalFeesNotesByAdmissionNumberAndInvoiceId(AdmissionNumber, InvoiceId)
        End Using
        'Return Me._medicalFeesNoteAdminService.GetMedicalFeesNotesByAdmissionNumberAndInvoiceId(AdmissionNumber, InvoiceId)
    End Function

End Class
