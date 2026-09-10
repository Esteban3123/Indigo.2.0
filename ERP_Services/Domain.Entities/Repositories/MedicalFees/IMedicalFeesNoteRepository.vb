'************************************************************
' Assembly         : Domain.MedicalFees
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/03/2015
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface IMedicalFeesNoteRepository
    Inherits IRepository(Of MedicalFeesNote)

    ''' <summary>
    ''' Obtiene las notas de causacion de honorarios medicos
    ''' </summary>
    ''' <returns></returns>
    Function GetMedicalFeesNotesByAdmissionNumberAndInvoiceId(AdmissionNumber As String, InvoiceId As Integer) As List(Of MedicalFeesNote)
    
End Interface
