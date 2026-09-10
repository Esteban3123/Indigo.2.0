Imports Domain.Entities

Public Class CtrMoreInfoExternalPatient

#Region "Constructor"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    Public Sub New()
        InitializeComponent()
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Load initial data
    ''' </summary>
    Public Sub LoadData(ExternalPatient As PatientExternalCareCenter)
        LoadExternalPatientData(ExternalPatient)
    End Sub

    ''' <summary>
    ''' Carga los datos del paciente
    ''' </summary>
    ''' <param name="item"></param>
    Private Sub LoadExternalPatientData(item As PatientExternalCareCenter)
        INDTxtExternalPatientIdentification.Text = item.IdentificationNumber
        INDTxtExternalPatientName.Text = item.Name
        INDTxtExternalPatientLastName.Text = item.LastName
        INDTxtExternalPatientGender.Text = item.GenderCodeName
        INDTxtExternalPatientMobileNumber.Text = item.PatientMobileNumber
        INDTxtExternalPatientEmail.Text = item.PatientEmail
        INDTxtExternalPatientFunctionalUnit.Text = item.ExternalFunctionalUnit
        INDTxtExternalPatientBed.Text = item.PatientBed
    End Sub

    Public Sub FocusTextEdit()
        INDTxtExternalPatientIdentification.Focus()
    End Sub

#End Region

End Class
