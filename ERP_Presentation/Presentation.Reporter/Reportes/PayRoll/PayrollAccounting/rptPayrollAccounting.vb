#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Domain.Entities
Imports Presentation.Base
#End Region

Public Class rptPayrollAccounting
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance


    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim filtroConsulta As String = "PayrollId.PayrollDateLiquidated >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "# And PayrollId.PayrollDateLiquidated <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "#"

            'filtro por estado
            If ParametrosReporte(2) IsNot Nothing Then
                filtroConsulta &= " AND ConceptId.ConceptType = " & ParametrosReporte(2)
            End If

            'filtro por empleado
            If ParametrosReporte(3) IsNot Nothing Then
                filtroConsulta &= " AND PayrollId.EmployeeId.EmployeeTypeId.Id = " & ParametrosReporte(3)
            End If

            'filtro por grupo
            If Not String.IsNullOrEmpty(ParametrosReporte(4)) AndAlso Not String.IsNullOrEmpty(ParametrosReporte(5)) Then
                filtroConsulta &= String.Format(" AND (PayrollId.GroupId >= {0} AND PayrollId.GroupId <= {1})", ParametrosReporte(4), ParametrosReporte(5))
            End If

            'filtro por sucursal
            If Not String.IsNullOrEmpty(ParametrosReporte(6)) AndAlso Not String.IsNullOrEmpty(ParametrosReporte(7)) Then
                filtroConsulta &= String.Format(" AND (PayrollId.ContractId.FunctionalUnitId.BranchOfficeId >= {0} AND PayrollId.ContractId.FunctionalUnitId.BranchOfficeId <= {1})", ParametrosReporte(6), ParametrosReporte(7))
            End If

            Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of PayrollLiquidationDetail)(Nothing, filtroConsulta)
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

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Private Sub rptControlLiquidation_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDlblFechaFin.Text = Me.ParametrosReporte(1)
        INDlblFechaInicio.Text = Me.ParametrosReporte(0)
        Me.INDLblNombreEmpresaCliente.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCliente.Text = IndigoSessionValues.IndigoCompanyNit
        Me.INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        ' Inicializar la localización del reporte (formato de moneda)
        UtilitiesReporter.InitializeReportLocalization(Me, IndigoSessionValues, 1)
    End Sub
End Class