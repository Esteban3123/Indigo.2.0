#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports Presentation.Base

#End Region

Public Class rptCertificate
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim filtroConsulta As String = "DatePayment >= '" & Format(Me.ParametrosReporte(0), "yyyyMMdd") & "' And DatePayment <= '" & Format(Me.ParametrosReporte(1), "yyyyMMdd") & "'"

            ''filtro por Grupo
            'If ParametrosReporte(2) IsNot Nothing And ParametrosReporte(3) IsNot Nothing Then
            '    filtroConsulta &= " And AgreementsCId.GroupId >= '" & Me.ParametrosReporte(2) & "' AND AgreementsCId.GroupId <= '" & ParametrosReporte(3) & "'"
            'End If

            ''filtro por Empleado
            'If ParametrosReporte(4) IsNot Nothing Then
            '    filtroConsulta &= " And AgreementsCId.EmployeeId = " & ParametrosReporte(4)
            'End If

            ''filtro por estado
            'If ParametrosReporte(5) <> 6 Then
            '    filtroConsulta &= " AND AgreementsCId.State = '" & ParametrosReporte(5) & "'"
            'End If

            ''filtro por Entidad
            'If ParametrosReporte(7) IsNot Nothing And ParametrosReporte(8) IsNot Nothing Then
            '    filtroConsulta &= " And AgreementsCId.CompanyId.Nit >= " & Me.ParametrosReporte(7) & " AND AgreementsCId.CompanyId.Nit <= " & ParametrosReporte(8)
            'End If

            Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of PayrollAgreementsDReportXpo)(Nothing, filtroConsulta)
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Sub

    Public Function GetExceptionDetails(exception As Exception) As String
        Dim properties = exception.[GetType]().GetProperties()
        Dim fields = properties.[Select](Function([property]) New With { _
            Key .Name = [property].Name, _
            Key .Value = [property].GetValue(exception, Nothing) _
        }).[Select](Function(x) [String].Format("{0} : {1}", x.Name, If(x.Value IsNot Nothing, x.Value.ToString(), [String].Empty)))
        Return [String].Join(vbLf, fields)
    End Function

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptCertificate_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
    End Sub
End Class