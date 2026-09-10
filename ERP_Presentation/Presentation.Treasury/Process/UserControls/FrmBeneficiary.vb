Public Class FrmBeneficiary 

#Region "Properties and Variables"

    ''' <summary>
    ''' Obtiene o establece el nombre del beneficiario
    ''' </summary>
    ''' <value>
    ''' The name beneficiary.
    ''' </value>
    Public Property BeneficiaryName As String
        Get
            Return INDtxtName.Text
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la identificacion del propietario
    ''' </summary>
    ''' <value>
    ''' The beneficiary identification.
    ''' </value>
    Public Property BeneficiaryIdentification As String
        Get
            Return INDtxtIdentification.Text
        End Get
        Set(value As String)
            INDtxtIdentification.Text = value
        End Set
    End Property

#End Region

#Region "Events"

    Private Sub FrmBeneficiary_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    ''' <summary>
    ''' Carga los datos en los campos
    ''' </summary>
    ''' <param name="Identification">The identification.</param>
    ''' <param name="NameBeneficiary">The name beneficiary.</param>
    Public Sub LoadData(ByVal Identification As String, ByVal NameBeneficiary As String)
        BeneficiaryIdentification = Identification
        BeneficiaryName = NameBeneficiary
    End Sub

    ''' <summary>
    ''' Handles the Click event of the INDsbAcept control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAcept_Click(sender As Object, e As EventArgs) Handles INDsbAcept.Click
        Me.Close()
    End Sub

#End Region

End Class