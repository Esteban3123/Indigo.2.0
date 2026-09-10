#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base

#End Region

Public Class rptReportProductBarcode
    Implements IReport
    Implements IReportAsync

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

#End Region

#Region "Load Data"

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).InventoryService.LoadDataSourceReportProductBarcode(ParametrosReporte(0), ParametrosReporte(1), ParametrosReporte(2), ParametrosReporte(3))
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Sub

    Public Function CargarDataSourceAsync() As Task Implements IReportAsync.CargarDataSourceAsync
        Return Task.Factory.StartNew(AddressOf CargarDataSource)
    End Function

#End Region

#Region "Methods"

    Private Sub rptTraceabilityInvoice_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Me.INDLblNameCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "Nit : " & IndigoSessionValues.IndigoCompanyNit
        Me.INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName

        Dim dimensions = 40
        If Not String.IsNullOrEmpty(XrBarCode1.Tag.ToString()) Then
            dimensions = CType(XrBarCode1.Tag.ToString(), Integer)
        End If

        If ParametrosReporte(4) = 1 Then
            Me.XrBarCode1.Visible = False
            Me.XrBarCode1.HeightF = 20
            Me.XrTableCell18.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "CalculatedBarCode")})
        ElseIf ParametrosReporte(4) = 2 Then
            If XrTableCell18.DataBindings IsNot Nothing AndAlso XrTableCell18.DataBindings.Count > 0 Then
                XrTableCell18.DataBindings.RemoveAt(0)
            End If

            Me.XrBarCode1.Visible = True
            Me.XrBarCode1.HeightF = dimensions
            Me.XrBarCode1.Symbology = New DevExpress.XtraPrinting.BarCode.Code128Generator() With {.CharacterSet = DevExpress.XtraPrinting.BarCode.Code128Charset.CharsetA}
            Me.XrBarCode1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "CalculatedBarCode")})
        ElseIf ParametrosReporte(4) = 3 Then
            If XrTableCell18.DataBindings IsNot Nothing AndAlso XrTableCell18.DataBindings.Count > 0 Then
                XrTableCell18.DataBindings.RemoveAt(0)
            End If

            XrBarCode1.Visible = True
            Me.XrBarCode1.HeightF = dimensions * 3
            XrBarCode1.Symbology = New DevExpress.XtraPrinting.BarCode.QRCodeGenerator()
            CType(XrBarCode1.Symbology, DevExpress.XtraPrinting.BarCode.QRCodeGenerator).CompactionMode = DevExpress.XtraPrinting.BarCode.QRCodeCompactionMode.Byte
            Me.XrBarCode1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "CalculatedBarCode")})
        End If
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public Function GetExceptionDetails(exception As Exception) As String
        Dim properties = exception.[GetType]().GetProperties()
        Dim fields = properties.[Select](Function([property]) New With {
            Key .Name = [property].Name,
            Key .Value = [property].GetValue(exception, Nothing)
        }).[Select](Function(x) [String].Format("{0} : {1}", x.Name, If(x.Value IsNot Nothing, x.Value.ToString(), [String].Empty)))
        Return [String].Join(vbLf, fields)
    End Function

#End Region

End Class