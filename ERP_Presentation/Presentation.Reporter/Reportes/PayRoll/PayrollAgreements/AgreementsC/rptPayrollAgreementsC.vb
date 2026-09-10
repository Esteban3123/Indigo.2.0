#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports Presentation.Base
Imports System.Globalization
#End Region

Public Class rptPayrollAgreementsC
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Const CNameReport = "Payroll.FrmPayrollControl"


    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim filtroConsulta As String = "GetDate(StartingDate) >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "# AND GetDate(StartingDate) <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "#"

            'filtro por Grupo
            If ParametrosReporte(2) IsNot Nothing And ParametrosReporte(3) IsNot Nothing Then
                filtroConsulta &= " And GroupCode >= '" & Me.ParametrosReporte(2) & "' AND GroupCode <= '" & ParametrosReporte(3) & "'"
            End If

            'filtro por Empleado
            If ParametrosReporte(4) IsNot Nothing Then
                filtroConsulta &= " And EmployeeId = " & ParametrosReporte(4)
            End If

            'filtro por estado
            If ParametrosReporte(5) <> 6 Then
                filtroConsulta &= " AND State = '" & ParametrosReporte(5) & "'"
            End If

            'filtro por Entidad
            If ParametrosReporte(7) IsNot Nothing And ParametrosReporte(8) IsNot Nothing Then
                filtroConsulta &= " And CompanyNit >= '" & Me.ParametrosReporte(7) & "' AND CompanyNit <= '" & ParametrosReporte(8) & "'"
            End If

            'filtro por surcursal
            If ParametrosReporte(9) IsNot Nothing AndAlso ParametrosReporte(10) IsNot Nothing Then
                filtroConsulta &= " AND BranchOfficeCode >= '" & ParametrosReporte(9) & "' AND BranchOfficeCode <= '" & ParametrosReporte(10) & "'"
            End If

            'filtro por concepto
            If ParametrosReporte(11) IsNot Nothing AndAlso ParametrosReporte(12) IsNot Nothing Then
                filtroConsulta &= " AND ConceptId >= " & ParametrosReporte(11) & " AND ConceptId <= " & ParametrosReporte(12)
            End If

            Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of PayrollViewAgreementsCReportXpo)(Nothing, filtroConsulta)
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

    Private Sub rptPayrollAgreementsC_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Dim table2 As XRTable = CType(XrTable2, XRTable)
        Dim table3 As XRTable = CType(XrTable3, XRTable)
        Dim table5 As XRTable = CType(XrTable5, XRTable)
        Dim table6 As XRTable = CType(XrTable6, XRTable)

        If ParametrosReporte(2) Is Nothing Then
            INDGroup.Value = 1
            XrTableCell53.Text = "Total"
        End If

        If ParametrosReporte(5) = 6 Then
            table2.Visible = False
            table3.Visible = False
            table5.Visible = True
            table6.Visible = True
        Else
            table2.Visible = True
            table3.Visible = True
            table5.Visible = False
            table6.Visible = False
        End If

        If ParametrosReporte(2) IsNot Nothing And ParametrosReporte(3) IsNot Nothing Then
            XrTableCell16.Visible = True
            INDLblGrupoName.Visible = True
        Else
            XrTableCell16.Visible = False
            INDLblGrupoName.Visible = False

        End If

        If ParametrosReporte(7) IsNot Nothing And ParametrosReporte(8) IsNot Nothing Then
            XrTable5.Visible = False
            XrTableCell16.Visible = True
            XrLabel1.Visible = True
            GroupHeader1.Visible = True
        Else
            XrLabel1.Visible = False
            GroupHeader1.Visible = False
        End If


        INDlblFechaInicio.Text = Me.ParametrosReporte(0)
        INDlblFechaFin.Text = Me.ParametrosReporte(1)
        INDLblState.Text = Me.ParametrosReporte(6)
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName

        ' Inicializar la localización del reporte (formato de moneda)
        UtilitiesReporter.InitializeReportLocalization(Me, IndigoSessionValues, 1)
    End Sub

    Private Sub Detail_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Detail.BeforePrint
        Dim Coment = (GetCurrentColumnValue("INDCFComments"))
        Dim table8 As XRTable = CType(XrTable1, XRTable)
        Dim Detalle As DetailBand = CType(Detail, DetailBand)
        If String.IsNullOrEmpty(Coment.ToString.Trim) Then
            table8.Visible = False
            table8.HeightF = 0
            Detalle.HeightF = 23
        Else
            table8.Visible = True
            table8.HeightF = 20
            Detalle.HeightF = 48
        End If
    End Sub
End Class