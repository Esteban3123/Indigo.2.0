#Region "Imports"

Imports System.Drawing.Printing
Imports DevExpress.XtraReports.Parameters
Imports DevExpress.XtraReports.UI
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base

#End Region

Public Class rptMagistralLabel
    Implements IReport
    Implements IReportAsync

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Private _sessionValues As SessionValues = SessionValues.Instance

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
            DataSource = Nothing
            Dim campaignDetailId As String = ParametrosReporte(0)
            Dim data = XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewMagistralLabelXpo)(Nothing, $"CampaignDetailId = {campaignDetailId}")

            If data.Any() Then
                For Each item In data
                    item.MainMedicines = New List(Of MagistralLabels)
                    item.Vehicles = New List(Of MagistralLabels)

                    Dim MainMedicines = item.MagistralLabelDetails.Where(Function(x) x.MainMedicine).ToList()
                    item.MainMedicines.AddRange(GroupingComponents(MainMedicines, 5))

                    Dim Vehicles = item.MagistralLabelDetails.Where(Function(x) x.Vehicle).ToList()
                    item.Vehicles.AddRange(GroupingComponents(Vehicles, 2))
                Next
            End If
            DataSource = data
            AssignSubreportDataSources()
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Sub




    Private Function GroupingComponents(details As List(Of ViewMagistralLabelDetailsXpo), maxRows As Integer)
        Dim result As New List(Of MagistralLabels)

        For i As Integer = 1 To maxRows
            Dim group As New MagistralLabels
            Dim numberRows = (i - 1) * 2

            If details.Count > numberRows Then
                group.Component1 = details(numberRows).AbbreviationNameAtc
                group.Quantity1 = details(numberRows).QuantityWithUnitMeasurement
            End If

            If details.Count > numberRows + 1 Then
                group.Component2 = details(numberRows + 1).AbbreviationNameAtc
                group.Quantity2 = details(numberRows + 1).QuantityWithUnitMeasurement
            End If

            result.Add(group)
        Next

        Return result
    End Function


    Public Function CargarDataSourceAsync() As Task Implements IReportAsync.CargarDataSourceAsync
        Return Task.Factory.StartNew(AddressOf CargarDataSource)
    End Function

#End Region

#Region "Methods"

    Private Sub rpt_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        If Me.Parameters.Count > 0 AndAlso Me.Parameters(0).Value > 0 Then
            Dim ParametrosFilter As ParameterCollection = Me.Parameters
            ParametrosReporte = New Object() {ParametrosFilter("Id").Value}
            CargarDataSource()
        End If
        ApplyInitialLabelFormatting()
    End Sub

    ''' <summary>
    ''' Asigna los DataSource y DataMember manualmente para evitar errores de diseño
    ''' </summary>
    Private Sub AssignSubreportDataSources()
        If DataSource Is Nothing Then Exit Sub
        ' Asignación para Micronutrientes
        DetailReport1.DataSource = DataSource
        DetailReport1.DataMember = "Vehicles"
        ' Asignación para Macronutrientes
        DetailReport2.DataSource = DataSource
        DetailReport2.DataMember = "MainMedicines"
    End Sub


    ''' <summary> 
    '''  Aplica configuración inicial de márgenes y comportamiento visual de celdas para evitar desbordamientos.
    ''' </summary>
    Private Sub ApplyInitialLabelFormatting()
        CompanyName(_sessionValues)
        '' Margins = New Margins(left:=20, right:=10, top:=30, bottom:=0)
        Dim tables As XRTable() = {XrTableHead, XrTableHeadDetail1, XrTableRowDetail1, XrTableHeadDetail2, XrTableRowDetail2, XrTableFooter}
        For Each table As XRTable In tables
            ConfigureTableCells(table)
        Next
    End Sub

    ''' <summary>
    ''' Recorre las tablas y valida cada celda para evitar desbordamiento
    ''' </summary>
    ''' <param name="table"></param>
    Private Sub ConfigureTableCells(table As XRTable)
        For Each row As XRTableRow In table.Rows
            For Each cell As XRTableCell In row.Cells
                With cell
                    .CanGrow = False
                    .CanShrink = False
                    .WordWrap = True
                End With
            Next
        Next
    End Sub

    ''' <summary>
    ''' Carga el nombre de la compania correspondiente
    ''' </summary>
    ''' <param name="Sessions"></param>
    Public Sub CompanyName(Sessions As SessionValues)
        INDXcCompanyName.Text = Sessions.IndigoCompanyName.ToString()
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

    Private Sub Detail_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Detail.BeforePrint
    End Sub

    Private Sub XrTableCell66_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell66.BeforePrint
        Dim label = CType(sender, XRTableCell)
        Dim currentRow = CType(Me.GetCurrentRow(), ViewMagistralLabelXpo)

        If currentRow IsNot Nothing AndAlso currentRow.MainMedicines IsNot Nothing Then
            Dim volumes = currentRow.MainMedicines.
                        Select(Function(m) m.Quantity1).
                        Where(Function(q) Not String.IsNullOrWhiteSpace(q)).
                        ToList()

            label.Text = String.Join(" / ", volumes)
        End If
    End Sub

#End Region
End Class