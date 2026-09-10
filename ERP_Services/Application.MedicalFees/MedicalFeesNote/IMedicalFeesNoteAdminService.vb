'***********************************************************************
' Assembly         : Application.MedicalFees
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/03/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IMedicalFeesNoteAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene las notas de causacion de honorarios medicos
    ''' </summary>
    ''' <returns></returns>
    Function GetMedicalFeesNotesByAdmissionNumberAndInvoiceId(AdmissionNumber As String, InvoiceId As Integer) As ActionResult(Of List(Of MedicalFeesNote))

End Interface
