Imports Infrastructure.CrossCutting.Resources

Public Class CtrDatosPacienteNPT

    Private Sub CtrDatosPacienteNPT_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Sub RefreshData(data As DatoPacienteModel)
        With data
            INDLblPaciente.Text = .Paciente
            INDLblIdentificacion.Text = .Identificacion
            INDLblIngreso.Text = .Ingreso
            INDLblDiagnostico.Text = .Diagnostico
            INDLblEdad.Text = Infrastructure.CrossCutting.Base.Utils.AgeToString(data.BirthDate)
            INDLblPeso.Text = If(.Peso = 0, "- kg", $"{ .Peso} kg")
            INDLblTalla.Text = If(.Talla = 0, "- cm", $"{Convert.ToInt32(.Talla)} cm")
        End With
    End Sub

    Public Class DatoPacienteModel
        Public Property Paciente As String
        Public Property Identificacion As String
        Public Property Ingreso As String
        Public Property Diagnostico As String
        Public Property BirthDate As Date
        Public Property Peso As Integer
        Public Property Talla As Decimal
    End Class
End Class
