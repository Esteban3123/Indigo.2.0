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

Public Class rptSubFixedAssetEntryDocument
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

#Region "Propiedades"
    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    ''' <summary>
    ''' obtiene la informacion de la moneda seleccionada
    ''' </summary>
    ''' <returns></returns>
    Public Property Currency As Currency
    ''' <summary>
    ''' abreviacion de la moneda 
    ''' </summary>
    Private CurrencyAbbreviation As String
#End Region

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim filtroConsulta As String = "EntryDate >= #" & Format(Me.ParametrosReporte(0), "yyyy-MM-dd") & "# And EntryDate <= #" & Format(Me.ParametrosReporte(1), "yyyy-MM-dd") & "#"

            'filtro por Estado
            If ParametrosReporte(2) <> 4 Then
                filtroConsulta &= "AND Status = " & ParametrosReporte(2)
            End If

            'filtro por Ingreso
            If ParametrosReporte(3) IsNot Nothing And ParametrosReporte(4) IsNot Nothing Then
                filtroConsulta &= "AND Code >= '" & ParametrosReporte(3) & "' AND Code <= '" & ParametrosReporte(4) & "'"
            End If

            'filtro por Proveedor
            If ParametrosReporte(6) IsNot Nothing And ParametrosReporte(7) IsNot Nothing Then
                filtroConsulta &= "AND SupplierId.Code >= '" & ParametrosReporte(6) & "' AND SupplierId.Code <= '" & ParametrosReporte(7) & "'"
            End If

            'filtro por tipo de adquisición
            If ParametrosReporte(8) <> 0 Then
                filtroConsulta &= " AND AdquisitionType = " & ParametrosReporte(8)
            End If

            'Filtro por moneda 
            If ParametrosReporte(9) <> 0 Then
                filtroConsulta &= " AND CurrencyId = " & ParametrosReporte(9)
            End If

            Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of FixedAssetFixedAssetEntryReportXpo)(Nothing, filtroConsulta)
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
    Private Sub rptSubFixedAssetEntryDocument_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Dim _culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
        CurrencyAbbreviation = Currency?.Abbreviation
        _culture.NumberFormat = CurrencyAbbreviation.GetNumberFormat
        ApplyLocalization(_culture)
    End Sub

End Class