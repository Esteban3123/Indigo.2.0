#Region "Imports"

Imports System.Globalization
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Base

#End Region

Public Class rptReportRemissionDevolutionResume
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
            Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).InventoryService.LoadDataSourceReportRemissionDevolution(ParametrosReporte(0), ParametrosReporte(1), ParametrosReporte(2), ParametrosReporte(3), ParametrosReporte(5), ParametrosReporte(6), ParametrosReporte(7))
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Sub

    Public Function CargarDataSourceAsync() As Task Implements IReportAsync.CargarDataSourceAsync
        Return Task.Factory.StartNew(AddressOf CargarDataSource)
    End Function

#End Region

#Region "Methods"

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

    Private Sub rptReportRemissionDevolutionResume_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Dim currencyAbrreviation As String = TryCast(Me.DataSource, XPCollection(Of InventoryRemissionDevolutionReportXpo))?.FirstOrDefault?.CurrencyAbbreviation
        If String.IsNullOrEmpty(currencyAbrreviation) Then currencyAbrreviation = SessionValues.Instance.CurrencyISO4217
        Dim culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
        culture.NumberFormat = currencyAbrreviation.GetNumberFormat
        Me.ApplyLocalization(culture)
    End Sub

#End Region

End Class