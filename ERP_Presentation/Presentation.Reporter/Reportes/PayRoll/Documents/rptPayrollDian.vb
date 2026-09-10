#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Domain.Entities
Imports Presentation.Base
Imports Presentation.CloudAgent
Imports Presentation.Reporter
Imports DevExpress.XtraReports.UI

#End Region

Public Class rptPayrollDian
    Implements IReport
    Implements IReportAsync

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Dim dtReportIncomeAndWithholding As DataTable

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim ds As DataSet = IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetListReportIncomeAndWithholding(Me.ParametrosReporte(0), Me.ParametrosReporte(1), IndigoSessionValues)
            If ds.Tables(0).Rows.Count > 0 Then
                dtReportIncomeAndWithholding = ds.Tables("ReportIncomeAndWithholding")
                Me.DataSource = dtReportIncomeAndWithholding
                Me.DataMember = "ReportIncomeAndWithholding"
            Else
                Me.DataSource = Nothing
            End If
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Sub

    Public Function CargarDataSourceAsync() As Task Implements IReportAsync.CargarDataSourceAsync
        Return Task.Factory.StartNew(AddressOf CargarDataSource)
    End Function

    Public Function GetExceptionDetails(exception As Exception) As String
        Dim properties = exception.[GetType]().GetProperties()
        Dim fields = properties.[Select](Function([property]) New With {
            Key .Name = [property].Name,
            Key .Value = [property].GetValue(exception, Nothing)
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

    Private Sub rptPayrollDian_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        'INDLblCompanyy.Text = IndigoSessionValues.IndigoCompanyName
        'INDLblNitCompanyy.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        Dim ReportDian = Me.ParametrosReporte(0)

        If ReportDian = 2022 Then
            ReportDian2022()
        Else
            ReportDianOtherYears()
        End If
    End Sub
    ''' <summary>
    ''' Funcion que establece cambios en el reporte Dian para el año 2022
    ''' </summary>
    Private Sub ReportDian2022()
        ' Uso de CanShrink para mover las celdas de su ubicacion original
        XrLabel6.CanShrink = True
        XrLabel9.CanShrink = True
        XrLabel48.CanShrink = True

        ' Ocultar elementos
        XrPictureBox3.Visible = False
        XrLabel10.Visible = False
        XrPictureBox1.Visible = False
        XrTableRow31.Visible = False
        XrTableRow30.Visible = False
        XrTableRow52.Visible = False

        'Asignacion de color a las celdas
        Dim labelCellsTransparent() As XRTableCell = {XrTableCell2, XrTableCell3, XrTableCell4, XrTableCell18, XrTableCell19,
                                              XrTableCell38, XrTableCell111, XrTableCell112, XrTableCell113,
                                              XrTableCell133, XrTableCell134, XrTableCell135, XrTableCell6,
                                              XrTableCell7, XrTableCell9, XrTableCell35, XrTableCell36, XrTableCell37}

        Dim labelCellsWhiteSmoke() As XRTableCell = {XrTableCell12, XrTableCell13, XrTableCell10, XrTableCell90, XrTableCell92,
                                             XrTableCell109, XrTableCell127, XrTableCell129, XrTableCell130,
                                             XrTableCell16, XrTableCell17, XrTableCell21, XrTableCell103,
                                             XrTableCell114, XrTableCell115, XrTableCell39, XrTableCell40, XrTableCell41}

        For Each cellColor As XRTableCell In labelCellsTransparent.Concat(labelCellsWhiteSmoke)
            If labelCellsTransparent.Contains(cellColor) Then
                cellColor.BackColor = Color.Transparent
            ElseIf labelCellsWhiteSmoke.Contains(cellColor) Then
                cellColor.BackColor = Color.WhiteSmoke
            End If
        Next


        ' Asignación de texto a celdas
        Dim labelText() As String = {
        "36", "48", "49", "50", "51", "53", "54", "55", "56", "57",
        "58", "59", "60", "61", "62", "63", "64", "65", "66", "67",
        "68", "69", "70", "73", "71. Identificación de los bienes poseídos",
        "72. Valor patrimonial", "Cesantías e intereses de cesantías efectivamente pagadas al empleado",
        "Total de ingresos brutos (Sume 36 a 48)", "Aportes obligatorios por salud a cargo del trabajador",
        "Aportes obligatorios a fondos de pensiones y solidaridad pensional a cargo del trabajador",
        "Aportes voluntarios a fondos de pensiones", "Aportes a cuentas AFC o AVC",
        "Datos a cargo del trabajador o pensionado",
        "Totales: (Valor recibido: Sume 56 a 61), (Valor retenido: Sume 63 a 68)",
        "Identificación del dependiente económico de acuerdo al párrafo 2 del artículo 387 del Estatuto Tributario"
    }

        Dim cellsText() As XRTableCell = {
        XrTableCell5, XrTableCell15, XrTableCell24, XrTableCell40, XrTableCell36,
        XrTableCell33, XrTableCell137, XrTableCell30, XrTableCell43, XrTableCell46,
        XrTableCell49, XrTableCell66, XrTableCell71, XrTableCell76, XrTableCell81,
        XrTableCell58, XrTableCell59, XrTableCell62, XrTableCell68, XrTableCell73,
        XrTableCell78, XrTableCell83, XrTableCell88, XrTableCell93, XrTableCell53,
        XrTableCell63, XrTableCell6, XrTableCell23, XrTableCell39, XrTableCell35,
        XrTableCell32, XrTableCell136, XrTableCell56, XrTableCell80, XrTableCell110
    }

        Dim cellsIndex As Integer = 0
        For Each cell As XRTableCell In cellsText
            cell.Text = labelText(cellsIndex)
            cellsIndex += 1
        Next
    End Sub
    ''' <summary>
    ''' Funcion que establece cambios en el reporte Dian para años diferentes al año 2022
    ''' </summary>
    Private Sub ReportDianOtherYears()
        ' Ocultar elementos
        XrTableRow33.Visible = False
        XrTableRow36.Visible = False
        XrTableRow37.Visible = False
        XrLabel50.Visible = False
        XrPictureBox4.Visible = False
        XrTableRow38.Visible = False
        XrTableRow39.Visible = False

        ' Modificar texto de las celdas
        XrTableCell5.Text = "37"
        XrLabel9.Text = "   36. Número de agencias, sucursales, filiales o subsidiarias de la empresa retenedora cuyos montos de retención se consolidan"
    End Sub
End Class