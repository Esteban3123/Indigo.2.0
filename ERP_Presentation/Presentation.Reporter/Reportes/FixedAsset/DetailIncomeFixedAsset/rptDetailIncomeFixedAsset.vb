#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing
Imports DevExpress.XtraReports.Parameters
Imports Presentation.Base
Imports System.Globalization
#End Region

Public Class rptDetailIncomeFixedAsset
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Variable que indica la Cultura
    ''' </summary>
    Dim _culture As CultureInfo

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim filtroConsulta As String = "Fecha >= '" & Format(Me.ParametrosReporte(0), "yyyy-MM-dd") & "' And Fecha <= '" & Format(Me.ParametrosReporte(1), "yyyy-MM-dd") & "'"
            'filtro por Estado
            If ParametrosReporte(2) IsNot Nothing Then
                If IsNumeric(ParametrosReporte(2)) Then
                    If ParametrosReporte(2) <> 4 Then
                        filtroConsulta &= " AND Status = " & ParametrosReporte(2)
                    End If
                Else
                    If ParametrosReporte(2).item1 <> 4 Then
                        filtroConsulta &= " AND Status = " & ParametrosReporte(2).Item1
                    End If
                End If
            End If

            If ParametrosReporte.Length >= 4 Then
                If ParametrosReporte(3) IsNot Nothing AndAlso ParametrosReporte(4) IsNot Nothing Then
                    filtroConsulta &= " AND Code >= '" & ParametrosReporte(3) & "' AND Code <= '" & ParametrosReporte(4) & "'"
                End If
            End If



            If ParametrosReporte.Length >= 6 Then
                ''Filtro por TipoReport
                If ParametrosReporte(5) IsNot Nothing AndAlso ParametrosReporte(5) <> "T" Then
                    filtroConsulta &= " AND Type = '" & ParametrosReporte(5) & "'"
                End If
            End If

            If ParametrosReporte.Length >= 7 Then
                ' Filtro por Proveedor
                If ParametrosReporte(6) IsNot Nothing AndAlso ParametrosReporte(7) IsNot Nothing Then
                    filtroConsulta &= " AND Proveedor >= '" & ParametrosReporte(6) & "' AND Proveedor <= '" & ParametrosReporte(7) & "'"
                End If
            End If


            If ParametrosReporte.Length >= 9 Then
                ' Filtro por Remission
                If ParametrosReporte(8) IsNot Nothing AndAlso ParametrosReporte(9) IsNot Nothing Then
                    filtroConsulta &= " AND Documento >= '" & ParametrosReporte(8) & "' AND Documento <= '" & ParametrosReporte(9) & "'"
                End If
            End If


            If ParametrosReporte.Length >= 11 Then
                ' Filtro por tipo de adquisición
                If ParametrosReporte(10) IsNot Nothing AndAlso ParametrosReporte(10) <> 0 Then
                    filtroConsulta &= " AND AdquisitionType = " & ParametrosReporte(10)
                End If
            End If


            Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).FixedAsset.GetCollection(Of FixedAssetVReportDetailsFixedAssetsIncomeReportXpo)(Nothing, filtroConsulta)
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

    Private Sub rptDetailIncomeFixedAsset_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName

        INDLblDate.Text = "Informe comprendido entre " & CDate(Me.ParametrosReporte(0)).ToString("dd De MMMM Del yyyy") & " " & CDate(Me.ParametrosReporte(1)).ToString("A dd De MMMM Del yyyy")
    End Sub

    ''' <summary>
    ''' Establece el simbolo de moneda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub XrTableCell11_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell11.BeforePrint
        Dim row As FixedAssetVReportDetailsFixedAssetsIncomeReportXpo = GetCurrentRow()
        XrTableCell11.Text = Utils.GetMoneyWithISO4217(row.TotalValue, If(String.IsNullOrEmpty(row.CurrencyAbbreviation),
                                             row.CodeAbbreviationISO, row.CurrencyAbbreviation))
    End Sub


    ''' <summary>
    ''' Establece el simbolo de moneda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub XrTableCell52_SummaryGetResult(sender As Object, e As SummaryGetResultEventArgs) Handles XrTableCell52.SummaryGetResult
        Dim row As FixedAssetVReportDetailsFixedAssetsIncomeReportXpo = GetCurrentRow()
        e.Result = Utils.GetMoneyWithISO4217(e.CalculatedValues.ToEntityList(Of Decimal).Sum(), If(String.IsNullOrEmpty(row.CurrencyAbbreviation),
                                             row.CodeAbbreviationISO, row.CurrencyAbbreviation))
        e.Handled = True
    End Sub
End Class