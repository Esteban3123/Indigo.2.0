#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing
Imports DevExpress.XtraReports.Parameters
Imports Infrastructure.Data.Xpo.SecurityRepository
Imports System.Globalization

#End Region

Public Class rptFixedAssetActiveOutput
    Implements IReport
#Region "Globals"
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Private IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Listado para almacenar la data a visualizar
    ''' </summary>
    Private INDList As List(Of FixedAssetFixedAssetActiveOutputDetailReportXpo)

    ''' <summary>
    ''' Variable para inicializar la cultura
    ''' </summary>
    Private _culture As CultureInfo

    ''' <summary>
    ''' Abreviación de la moneda
    ''' </summary>
    Private _currencyAbbreviation As String

    ''' <summary>
    ''' Variable para darle formato de decimales a los valores del reporte
    ''' </summary>
    Private _decimalFormat As Integer

#End Region


    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = "FixedAssetActiveOutputId.Id = " & ParametrosReporte(0)
        INDList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of FixedAssetFixedAssetActiveOutputDetailReportXpo)(Nothing, filtroConsulta)
        If INDList.Count > 0 Then
            Dim INDNameUser = CType(INDList(0), FixedAssetFixedAssetActiveOutputDetailReportXpo).FixedAssetActiveOutputId.CreationUser.Trim()

            Dim INDListUser = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).SecurityService.GetCollection(Of UserXpo)(Nothing, "UserCode = '" & INDNameUser & "'")

            If INDListUser IsNot Nothing Then
                Dim INDCodName = CType(INDListUser(0), UserXpo).CodeName.Trim
                Me.INDUserCreate.Text = INDCodName
            End If
        End If
        Me.DataSource = INDList
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptFixedAssetActiveOutput_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        InitializeReportLocalization()
        If Me.Parameters.Count > 0 And Me.Parameters(0).Value > 0 Then
            Dim ParametrosFilter As ParameterCollection = Me.Parameters
            ParametrosReporte = New Object() {ParametrosFilter("INDIdActiveOutput").Value, ParametrosFilter("INDOperationUnit").Value}
            CargarDataSource()
        End If

        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName


        Dim address, phoneNumber, codeips, city As String
        'cargar direccion, telefono y codigo ips
        If ParametrosReporte(1) IsNot Nothing Then
            Dim operatingUnit = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).CommonService.ListOperatingUnitById(ParametrosReporte(1))
            address = operatingUnit(0).Address
            phoneNumber = operatingUnit(0).Phone
            codeips = operatingUnit(0).IPSCode
            If operatingUnit(0).IdCity IsNot Nothing Then
                city = operatingUnit(0).IdCity.Descripcion
            Else
                city = "No asignada(o)"
            End If
        Else
            address = "No asignada(o)"
            phoneNumber = "No asignada(o)"
            codeips = "No asignada(o)"
            city = "No asignada(o)"
        End If

        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit & " - Dirección: " & address &
                            " - Teléfono: " & phoneNumber & " - Código IPS: " & codeips

        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName

        SetCurrencyFormat()
    End Sub

    ''' <summary>
    ''' Inicializa la cultura del reporte
    ''' </summary>
    Private Sub InitializeReportLocalization()
        'Obtiene el primer registro para obtener su Unidad operativa
        Dim dataReport As FixedAssetFixedAssetActiveOutputDetailReportXpo = TryCast(Me.DataSource, List(Of FixedAssetFixedAssetActiveOutputDetailReportXpo))?.First
        If dataReport Is Nothing Then
            Exit Sub
        End If

        'Se obtiene datos de la moneda
        Dim OperatingUnitId As Integer = dataReport.FixedAssetActiveOutputId.OperatingUnitId.Id
        Dim filter As String = "OperatingUnitId = " & OperatingUnitId
        Dim resSettingFixedAsset = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).FixedAsset.GetCollection(Of SettingFixedAssetXpo)(Nothing, filter).FirstOrDefault()

        'Aplica culturarizacion al reporte
        If resSettingFixedAsset IsNot Nothing Then
            Dim _culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
            _culture.NumberFormat = resSettingFixedAsset.CurrencyId.Abbreviation.GetNumberFormat()
            ApplyLocalization(_culture)
        End If
    End Sub

#Region "Cambio de formato de moneda"
    ''' <summary>
    ''' Evento que obtiene los parámetros para establecer el formato de la moneda
    ''' </summary>
    Private Sub SetCurrencyFormat()
        'Obtenemos los parámetros de activos fijos
        Dim filter = "OperatingUnitId = " & IndigoSessionValues.IndigoOperatingUnitId
        Dim settingFixedAsset = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).FixedAsset.GetXPOObject(Of SettingFixedAssetXpo)(filter)
        'Obtenemos la moneda de los parámetros
        If settingFixedAsset.CurrencyId IsNot Nothing Then
            _currencyAbbreviation = settingFixedAsset.CurrencyId.Abbreviation
        End If
        'Establecemos decimales
        If settingFixedAsset.CurrencyId?.RoundingType IsNot Nothing Then
            FormatValueWithDecimals(settingFixedAsset.CurrencyId.RoundingType)
        End If
    End Sub

    ''' <summary>
    ''' Método para formatear los valores del reporte según el tipo de redondeo parametrizado a la moneda
    ''' </summary>
    ''' <param name="roundingType"></param>
    Private Sub FormatValueWithDecimals(roundingType As Integer)
        Select Case roundingType
            Case 1
                _decimalFormat = 2
            Case 2
                _decimalFormat = 1
            Case >= 3
                _decimalFormat = 0
        End Select
    End Sub

    ''' <summary>
    ''' Método que modifica el formato de las celdas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ChangeFormat(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell4.BeforePrint, XrTableCell32.BeforePrint
        sender.Text = Utils.GetMoneyWithISO4217(sender.Text, _currencyAbbreviation, _decimalFormat)
    End Sub

    Private Sub ChangeFormat_SummaryGetResult(sender As Object, e As SummaryGetResultEventArgs) Handles XrTableCell37.SummaryGetResult, XrTableCell40.SummaryGetResult
        e.Result = Utils.GetMoneyWithISO4217(e.CalculatedValues.ToEntityList(Of Decimal).Sum(), _currencyAbbreviation, _decimalFormat)
        e.Handled = True
    End Sub

#End Region


End Class