#Region "Librerias Importadas"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Domain.Entities
Imports System.Globalization
#End Region

Public Class rptListFixedAssetR
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Abreviación de la moneda
    ''' </summary>
    Private currencyAbbreviation As String

    ''' <summary>
    ''' Variable para darle formato de decimales a los valores del reporte
    ''' </summary>
    Private decimalFormat As String

    ''' <summary>
    ''' Parámetros de activo fijo
    ''' </summary>
    Private settingFixedAsset As Object

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim filtroConsulta As String = Nothing

            'filtro por fechas
            If ParametrosReporte(0) IsNot Nothing And ParametrosReporte(1) IsNot Nothing Then
                filtroConsulta &= "AdquisitionDate >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "# AND AdquisitionDate <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "# AND isnull(OutputDate,'9999-01-01') > #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "#"
            Else
                'filtro por productos que no se han dado de baja
                filtroConsulta &= "HasOutput = 0 "
            End If

            'filtro por responsable
            If ParametrosReporte(2) IsNot Nothing Then
                If filtroConsulta Is Nothing Then
                    filtroConsulta &= " ResponsibleId.ThirdPartyId.Nit = '" & ParametrosReporte(2) & "'"
                Else
                    filtroConsulta &= " AND ResponsibleId.ThirdPartyId.Nit = '" & ParametrosReporte(2) & "'"
                End If
            End If

            'filtro por Catalogo
            If ParametrosReporte(3) IsNot Nothing Then
                If filtroConsulta Is Nothing Then
                    filtroConsulta &= " ItemId.ItemCatalogId.Code = '" & ParametrosReporte(3) & "'"
                Else
                    filtroConsulta &= " AND ItemId.ItemCatalogId.Code = '" & ParametrosReporte(3) & "'"
                End If
            End If

            'filtro por Estados de Activos
            If ParametrosReporte(4) IsNot Nothing Then
                If filtroConsulta Is Nothing Then
                    filtroConsulta &= " StatusAssetId.Id = " & ParametrosReporte(4)
                Else
                    filtroConsulta &= " AND StatusAssetId.Id = " & ParametrosReporte(4)
                End If
            End If

            'filtro por Tipo de Equipo
            If ParametrosReporte(5) IsNot Nothing And ParametrosReporte(6) IsNot Nothing Then
                If filtroConsulta Is Nothing Then
                    filtroConsulta &= " ItemId.ItemTypeId.Code >= '" & ParametrosReporte(5) & "' AND ItemId.ItemTypeId.Code <= '" & ParametrosReporte(6) & "'"
                Else
                    filtroConsulta &= " AND ItemId.ItemTypeId.Code >= '" & ParametrosReporte(5) & "' AND ItemId.ItemTypeId.Code <= '" & ParametrosReporte(6) & "'"
                End If
            End If

            'filtro por Placa
            If ParametrosReporte(7) IsNot Nothing And ParametrosReporte(8) IsNot Nothing Then
                If filtroConsulta Is Nothing Then
                    filtroConsulta &= " Plate >= '" & ParametrosReporte(7) & "' AND Plate <= '" & ParametrosReporte(8) & "'"
                Else
                    filtroConsulta &= " AND Plate >= '" & ParametrosReporte(7) & "' AND Plate <= '" & ParametrosReporte(8) & "'"
                End If
            End If

            'filtro por Ubicación
            If ParametrosReporte(9) IsNot Nothing And ParametrosReporte(10) IsNot Nothing Then
                If filtroConsulta Is Nothing Then
                    filtroConsulta &= " LocationId.Code >= '" & ParametrosReporte(9) & "' AND LocationId.Code <= '" & ParametrosReporte(10) & "'"
                Else
                    filtroConsulta &= " AND LocationId.Code >= '" & ParametrosReporte(9) & "' AND LocationId.Code <= '" & ParametrosReporte(10) & "'"
                End If
            End If

            'filtro por Ubicación
            If ParametrosReporte(11) IsNot Nothing And ParametrosReporte(12) IsNot Nothing Then
                If filtroConsulta Is Nothing Then
                    filtroConsulta &= " ItemId.Code >= '" & ParametrosReporte(11) & "' AND ItemId.Code <= '" & ParametrosReporte(12) & "'"
                Else
                    filtroConsulta &= " AND ItemId.Code >= '" & ParametrosReporte(11) & "' AND ItemId.Code <= '" & ParametrosReporte(12) & "'"
                End If
            End If

            'filtro por tipo de adquisición
            If ParametrosReporte(15) <> 0 Then
                If filtroConsulta Is Nothing Then
                    filtroConsulta &= " AdquisitionType = " & ParametrosReporte(15)
                Else
                    filtroConsulta &= " AND AdquisitionType = " & ParametrosReporte(15)
                End If
            End If

            Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).FixedAsset.GetCollection(Of FixedAssetPhysicalAssetReportXpo)(Nothing, filtroConsulta)
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

    Private Sub rptListFixedAsset_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint

        Me.INDGroupBy.Value = ParametrosReporte(13)
        Dim owner, nit As String
        If ParametrosReporte(14) IsNot Nothing Then
            settingFixedAsset = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).FixedAsset.ListSettingFixedAssetByOperatingUnitId(ParametrosReporte(14))
            currencyAbbreviation = If(String.IsNullOrEmpty(settingFixedAsset(0).CurrencyId.Abbreviation), IndigoSessionValues.CurrencyISO4217, settingFixedAsset(0).CurrencyId.Abbreviation)
            Dim roundingType = settingFixedAsset(0).CurrencyId.RoundingType
            nit = settingFixedAsset(0).IdThirdPartyResponsible.Nit
            owner = settingFixedAsset(0).IdThirdPartyResponsible.Name
            NitJefe.Text = nit
            jefe.Text = owner

            If roundingType IsNot Nothing Then
                FormatValueWithDecimals(roundingType)
            End If
        End If

        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDLblDate.Text = "Informe comprendido entre " & CDate(ParametrosReporte(0)).ToString("dd De MMMM Del yyyy") & " " & CDate(ParametrosReporte(1)).ToString("A dd De MMMM Del yyyy")
        Me.INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        Me.XrTableCell45.Text = Utils.Num2Text(Convert.ToDouble(GetCurrentColumnValue("INDCfTotalValue"))).ToString & " PESOS M/Cte."
    End Sub

    ''' <summary>
    ''' Método para formatear los valores del reporte según el tipo de redondeo parametrizado a la moneda
    ''' </summary>
    ''' <param name="roundingType"></param>
    Private Sub FormatValueWithDecimals(roundingType As Integer)
        Select Case roundingType
            Case 1
                decimalFormat = 2
            Case 2
                decimalFormat = 1
            Case >= 3
                decimalFormat = 0
        End Select
    End Sub

    ''' <summary>
    ''' Eventos que controlan la visualización de los valores del reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
#Region "Cambio Símbolo Moneda"
    Private Sub XrTableCell41_SummaryGetResult(sender As Object, e As DevExpress.XtraReports.UI.SummaryGetResultEventArgs) Handles XrTableCell41.SummaryGetResult
        e.Result = Utils.GetMoneyWithISO4217(e.CalculatedValues.ToEntityList(Of Decimal).Sum(), currencyAbbreviation, decimalFormat)
        e.Handled = True
    End Sub

    Private Sub XrTableCell40_SummaryGetResult(sender As Object, e As DevExpress.XtraReports.UI.SummaryGetResultEventArgs) Handles XrTableCell40.SummaryGetResult
        e.Result = Utils.GetMoneyWithISO4217(e.CalculatedValues.ToEntityList(Of Decimal).Sum(), currencyAbbreviation, decimalFormat)
        e.Handled = True
    End Sub
#End Region
End Class