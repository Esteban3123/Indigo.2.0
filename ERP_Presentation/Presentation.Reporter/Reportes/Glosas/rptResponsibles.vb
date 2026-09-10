Imports Infrastructure.Data.Xpo.GlosasRepository
Imports System.IO
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraPrinting
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo

Public Class rptResponsibles
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Const CNameReport = "Reporte2"

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    ''' <summary>
    ''' Metodo para cargar imagenes
    ''' </summary>
    Public Sub CargarImagenes() Implements IReport.CargarImagenes
        Dim Posicion As System.Drawing.PointF
        Dim currentDirectory As String = System.IO.Directory.GetCurrentDirectory()
        If File.Exists(currentDirectory & "\ReporterImages\LogoReporter.png") Then
            Dim INDPictureBoxDerecha As New XRPictureBox
            INDPictureBoxDerecha.Name = "INDPictureBoxDerechaLogo"
            INDPictureBoxDerecha.ImageUrl = currentDirectory & "\ReporterImages\LogoReporter.png"
            INDPictureBoxDerecha.Sizing = ImageSizeMode.AutoSize
            INDPictureBoxDerecha.LockedInUserDesigner = True
            Posicion.X = INDTitleRptLbl.WidthF
            Posicion.Y = INDTitleRptLbl.LocationF.Y
            Me.CabeceraReporte.Controls.Add(INDPictureBoxDerecha)
        End If
    End Sub

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Me.INDTitleRptLbl.Text = "Responsables"
        Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).GlosasService.GetCollection(Of GlosasResponsibleXpo)(Nothing, Nothing) '"CodeERP ='767'"
    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return rptResponsibles.CNameReport
        End Get
    End Property

End Class