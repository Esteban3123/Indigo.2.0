#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Infrastructure.Data.Xpo
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Presentation.Base
Imports System.Globalization
#End Region

Public Class rptReportValuedInventory
    Implements IReport
    'Implements IReportAsync

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Private _INDValorization As Integer
    Public Property INDValorization As Integer
        Get
            Return Me._INDValorization
        End Get
        Set(value As Integer)
            Me._INDValorization = value
        End Set
    End Property

    Private _INDOrderBy As Integer
    Public Property INDOrderBy As Integer
        Get
            Return Me._INDOrderBy
        End Get
        Set(value As Integer)
            Me._INDOrderBy = value
        End Set
    End Property

    Private _INDAccountingZero As Boolean
    Public Property INDAccountingZero As Boolean
        Get
            Return Me._INDAccountingZero
        End Get
        Set(value As Boolean)
            Me._INDAccountingZero = value
        End Set
    End Property

    Private _INDTypeReport As Integer
    Public Property INDTypeReport As Integer
        Get
            Return Me._INDTypeReport
        End Get
        Set(value As Integer)
            Me._INDTypeReport = value
        End Set
    End Property

    Private _INDProductStart As String
    Public Property INDProductStart As String
        Get
            Return Me._INDProductStart
        End Get
        Set(value As String)
            Me._INDProductStart = value
        End Set
    End Property

    Private _INDProductEnd As String
    Public Property INDProductEnd As String
        Get
            Return Me._INDProductEnd
        End Get
        Set(value As String)
            Me._INDProductEnd = value
        End Set
    End Property

    Private _INDWarehouseEnd As String
    Public Property INDWarehouseEnd As String
        Get
            Return Me._INDWarehouseEnd
        End Get
        Set(value As String)
            Me._INDWarehouseEnd = value
        End Set
    End Property

    Private _INDGroupStart As String
    Public Property INDGroupStart As String
        Get
            Return Me._INDGroupStart
        End Get
        Set(value As String)
            Me._INDGroupStart = value
        End Set
    End Property

    Private _INDGroupEnd As String
    Public Property INDGroupEnd As String
        Get
            Return Me._INDGroupEnd
        End Get
        Set(value As String)
            Me._INDGroupEnd = value
        End Set
    End Property

    Private _INDSubGroupStart As String
    Public Property INDSubGroupStart As String
        Get
            Return Me._INDSubGroupStart
        End Get
        Set(value As String)
            Me._INDSubGroupStart = value
        End Set
    End Property

    Private _INDSubGroupEnd As String
    Public Property INDSubGroupEnd As String
        Get
            Return Me._INDSubGroupEnd
        End Get
        Set(value As String)
            Me._INDSubGroupEnd = value
        End Set
    End Property

    Dim filters As Dictionary(Of String, String)

    Dim range As Dictionary(Of String, String)

    ''' <summary>
    ''' obtiene la moneda a la cual se realiza el reporte 
    ''' </summary>
    ''' <returns></returns>
    Public Property Currency As Currency

    ''' <summary>
    ''' listado de los datos del reporte
    ''' </summary>
    Dim reportData As List(Of SP_ReportValuedInventory_Result)

    Public Sub CargarDataSource() Implements IReport.CargarDataSource

    End Sub

    Public Async Function CargarDataSourceAsync() As Task
        Try
            Dim filtroConsulta As String = String.Empty
            filters = ParametrosReporte(0)
            range = ParametrosReporte(1)

            If reportData Is Nothing Then
                reportData = Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetReportValuedInventoryAsync(filters, range, IndigoSessionValues)
            End If

            Me.DataSource = reportData

        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
            Me.DataSource = Nothing
        End Try

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

    Private Sub rptReportValuedInventory_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint

        Me.INDLblNameCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "Nit : " & IndigoSessionValues.IndigoCompanyNit
        Me.INDUserImp.Text = "Usuario Impresión: " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName

        Me.INDPrmValorization.Value = filters("Valorization")
        Me.INDPrmOrderBy.Value = filters("OrderBy")

        If filters("TypeReport") = 1 And filters("Valorization") = 1 Then
            INDLblSubTitle.Text = "Informe: Agrupado  Valorizacion: Costo Promedio"
            Detail.Visible = False
        ElseIf filters("TypeReport") = 1 And filters("Valorization") = 2 Then
            INDLblSubTitle.Text = "Informe: Agrupado  Valorizacion: Ultimo Costo"
            Detail.Visible = False
        ElseIf filters("TypeReport") = 1 And filters("Valorization") = 3 Then
            INDLblSubTitle.Text = "Informe: Agrupado  Valorizacion: Precio Venta Publico"
            Detail.Visible = False
        ElseIf filters("TypeReport") = 2 And filters("Valorization") = 1 Then
            INDLblSubTitle.Text = "Informe: Detallado  Valorizacion: Costo Promedio"
            INDGrouping.Visible = False
        ElseIf filters("TypeReport") = 2 And filters("Valorization") = 2 Then
            INDLblSubTitle.Text = "Informe: Detallado  Valorizacion: Ultimo Costo"
            INDGrouping.Visible = False
        ElseIf filters("TypeReport") = 2 And filters("Valorization") = 3 Then
            INDLblSubTitle.Text = "Informe: Detallado  Valorizacion: Precio Venta Publico"
            INDGrouping.Visible = False
        ElseIf filters("TypeReport") = 3 And filters("Valorization") = 1 Then
            INDLblSubTitle.Text = "Informe: Resumido  Valorizacion: Costo Promedio"
            INDGrouping.Visible = False
            Detail.Visible = False
        ElseIf filters("TypeReport") = 3 And filters("Valorization") = 2 Then
            INDLblSubTitle.Text = "Informe: Resumido  Valorizacion: Ultimo Costo"
            INDGrouping.Visible = False
            Detail.Visible = False
        ElseIf filters("TypeReport") = 3 And filters("Valorization") = 3 Then
            INDLblSubTitle.Text = "Informe: Resumido  Valorizacion: Precio Venta Publico"
            INDGrouping.Visible = False
            Detail.Visible = False
        End If

        If filters("TypeReport") <> 2 Then
            XrTableCell30.BorderWidth = 0
            XrTableCell31.BorderWidth = 0
            XrTableCell32.BorderWidth = 0

            XrTableCell30.Visible = False
            XrTableCell31.Visible = False
            XrTableCell32.Visible = False

            XrTableCell30.WidthF = 0
            XrTableCell31.WidthF = 0
            XrTableCell32.WidthF = 0
        End If

        If Currency IsNot Nothing AndAlso Not String.IsNullOrEmpty(Currency.Abbreviation) Then
            Dim _culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
            Dim CurrencyAbbreviation As String = Currency.Abbreviation
            _culture.NumberFormat = CurrencyAbbreviation.GetNumberFormat
            ApplyLocalization(_culture)
        End If
    End Sub

End Class