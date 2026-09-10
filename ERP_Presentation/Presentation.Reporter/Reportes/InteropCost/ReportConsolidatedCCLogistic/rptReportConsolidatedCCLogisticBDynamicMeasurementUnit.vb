#Region "Librerias Importadas"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InteropCostRepository
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing
Imports DevExpress.XtraReports.Parameters
Imports Infrastructure.Data.Xpo.SecurityRepository
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports DevExpress.Spreadsheet
Imports System.Text
Imports System.Drawing
#End Region

Public Class rptReportConsolidatedCCLogisticBDynamicMeasurementUnit
    Implements IReport
    Implements IReportAsync

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Dim ListReportConsolidatedCCLogistic As List(Of SP_ReportConsolidatedCCLogisticB_Result)

    Private pcDetailStartId As String

    Private pcDetailEndId As String

    Private muStartId As String

    Private muEndId As String

    Private controlByGroup As Integer
    Private Property XrTable2 As XRTable
    Private Property XrTableRow2 As XRTableRow

    Private MeasurementUnitId As Integer

    Private total As Integer

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim pcLogisticId = CType(XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).InteropCostService.GetCollection(Of InteropCostProductionCenterReportXpo)(Nothing, " Code = '" & ParametrosReporte(2) & "'")(0), InteropCostProductionCenterReportXpo).Id
            If ParametrosReporte(3) <> "" Then
                pcDetailStartId = CInt(ParametrosReporte(3))
            End If
            If ParametrosReporte(4) <> "" Then
                pcDetailEndId = CInt(ParametrosReporte(4))
            End If
            If ParametrosReporte(5) <> "" Then
                muStartId = CType(XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).InteropCostService.GetCollection(Of InventoryMeasurementUnitXpo)(Nothing, " Code = '" & ParametrosReporte(5) & "'")(0), InventoryMeasurementUnitXpo).Id
            End If
            If ParametrosReporte(6) <> "" Then
                muEndId = CType(XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).InteropCostService.GetCollection(Of InventoryMeasurementUnitXpo)(Nothing, " Code = '" & ParametrosReporte(6) & "'")(0), InventoryMeasurementUnitXpo).Id
            End If

            ListReportConsolidatedCCLogistic = IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.ListReportConsolidatedCCLogisticB(Format(ParametrosReporte(0), "dd-MM-yyyy"), Format(ParametrosReporte(1), "dd-MM-yyy"), CInt(pcLogisticId), CInt(pcDetailStartId), CInt(pcDetailEndId), CInt(muStartId), CInt(muEndId))

            Me.DataSource = ListReportConsolidatedCCLogistic

        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Sub

    Public Function CargarDataSourceAsync() As Task Implements IReportAsync.CargarDataSourceAsync
        Return Task.Factory.StartNew(AddressOf CargarDataSource)
    End Function

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
            Return Nothing
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptReportConsolidatedCCLogistic_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        INDLblDate.Text = "Informe comprendido entre " & CDate(Me.ParametrosReporte(0)).ToString("dd De MMMM Del yyyy") & " " & CDate(Me.ParametrosReporte(1)).ToString("A dd De MMMM Del yyyy")
    End Sub

    Private Sub GroupHeader1_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles GroupHeader1.BeforePrint
        Dim totalQuantityProduc As Integer
        If controlByGroup <> 1 Then
            CreateTablet()
        End If
        'Por cada iteración del (GroupHeader1) se borran las celdas nuevas de (XrTableRow2)
        If controlByGroup = 1 Then
            Dim table2 As XRTable = CType(XrTable2, XRTable)
            Dim row1 As XRTableRow = table2.Rows(0)

            Dim t = XrTableRow2.Cells.Count()
            For j = 2 To t
                row1.DeleteCell(row1.Cells(1))
                Dim g = XrTableRow2.Cells.Count()
            Next
        End If

        'Se agregan las celdas a (XrTableRow1) tomando como referencia la La cantidad de ítems que contiene (listDetailOnliProductionCenterName) y se le agrega a cada celda en su propiedad (Tag) el código de cada centro de producción.        
        Dim listDetailOnliMeasurementUnitName = (From muName In ListReportConsolidatedCCLogistic
                                           Group mu = muName.MeasurementUnitName By co = muName.MeasurementUnitCode, na = muName.MeasurementUnitName Into mu = Group, Count()
                                           Order By co).ToList

        If controlByGroup <> 1 Then
            Dim indexx = 1
            Dim wi = 1364 / (listDetailOnliMeasurementUnitName.Count + 2)

            For Each i In listDetailOnliMeasurementUnitName
                Me.XrTable1.SizeF = New System.Drawing.SizeF(1364.0!, 20.0!)
                indexx += 1
                Dim cell As New XRTableCell()
                cell.WidthF = wi
                cell.Font = New Font("Arial", 6.0!, FontStyle.Bold)
                cell.Text = i.co & " - " & i.na
                cell.Tag = i.co
                cell.Name = "INDCllPageHeader" & indexx
                XrTableRow1.Cells.Add(cell)
            Next
            Me.XrTable1.SizeF = New System.Drawing.SizeF(1364.0!, 20.0!)
            Dim cellT As New XRTableCell()
            cellT.WidthF = wi
            cellT.Font = New Font("Arial", 8.0!, FontStyle.Bold)
            cellT.Text = "Total"
            cellT.Name = "INDCllPageHeader" & indexx
            XrTableRow1.Cells.Add(cellT)

            For f As Integer = 0 To (listDetailOnliMeasurementUnitName.Count + 1)
                XrTableRow1.Cells(f).WidthF = wi
            Next

            table3Total()
        End If

        'Se agregan las celdas a (XrTableRow2) tomando como referencia la cantidad de celdas que contiene (XrTableRow1).
        Dim indexxx = 1
        Dim wi2 = 1364 / (listDetailOnliMeasurementUnitName.Count + 2)
        For co As Integer = 1 To XrTableRow1.Cells.Count - 1

            Me.XrTable2.SizeF = New System.Drawing.SizeF(1364.0!, 20.0!)
            indexxx += 1
            Dim cellProductQuantity As New XRTableCell()
            cellProductQuantity.WidthF = wi2
            cellProductQuantity.Font = New Font("Arial", 8.0!)
            cellProductQuantity.Text = 0
            cellProductQuantity.Name = "INDCllDetail" & indexxx
            XrTableRow2.Cells.Add(cellProductQuantity)
        Next
        For g As Integer = 0 To (listDetailOnliMeasurementUnitName.Count + 1)
            XrTableRow2.Cells(g).WidthF = wi2
        Next

        'Se agrega a cada columna la cantidad, comparando el Tag de cada celda (Código del centro de producción) con el código de (meUnitGroup) el cual contiene la Tupla que se maneja en esa iteración
        MeasurementUnitId = (GetCurrentColumnValue("INDDetailProductionCenId"))
        Dim meUnitGroup = ListReportConsolidatedCCLogistic.Where(Function(x) x.DetailProductionCenterId = MeasurementUnitId)
        'meUnitGroup = meUnitGroup.OrderBy(Function(x) x.DetailProductionCenterCode)

        For colum As Integer = 1 To XrTableRow1.Cells.Count - 1
            For Each mu In meUnitGroup
                If XrTableRow1.Cells.Item(colum).Tag = mu.MeasurementUnitCode Then
                    XrTableRow2.Cells.Item(colum).Text = String.Format("{0:#,#}", (mu.Cantidad))
                    'Total por centro de producción
                    XrTableRow3.Cells.Item(colum).Text = String.Format("{0:#,#}", CInt(XrTableRow3.Cells.Item(colum).Text) + CInt(String.Format("{0:0}", (mu.Cantidad))))
                End If
            Next
        Next

        ' Total por unidad de medida
        Dim h = XrTableRow2.Cells.Count()
        For vf As Integer = 1 To h - 1
            Dim f = XrTableRow2.Cells.Item(vf).Text()
            totalQuantityProduc = totalQuantityProduc + f
        Next
        XrTableRow2.Cells.Item((h - 1)).Text = String.Format("{0:#,#}", totalQuantityProduc)
        total = total + totalQuantityProduc

        controlByGroup = 1
    End Sub

    Private Sub table3Total()
        Dim indexxx = 1
        Dim wi2 = 1364 / (XrTableRow1.Cells.Count)

        For co As Integer = 1 To XrTableRow1.Cells.Count - 1
            Me.XrTable3.SizeF = New System.Drawing.SizeF(1364.0!, 20.0!)
            indexxx += 1
            Dim cellProductQuantity As New XRTableCell()
            cellProductQuantity.WidthF = wi2
            cellProductQuantity.Font = New Font("Arial", 8.0!)
            cellProductQuantity.Text = 0
            cellProductQuantity.Name = "INDCllTotal" & indexxx
            XrTableRow3.Cells.Add(cellProductQuantity)
        Next

        For g As Integer = 0 To (XrTableRow1.Cells.Count - 1)
            XrTableRow3.Cells(g).WidthF = wi2
        Next
    End Sub

    Private Sub CreateTablet()
        Me.XrTable2 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow2 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell1 = New DevExpress.XtraReports.UI.XRTableCell()

        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupHeader1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable2})

        'XrTable2
        '
        Me.XrTable2.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTable2.Font = New System.Drawing.Font("Arial", 8.0!, FontStyle.Bold)
        Me.XrTable2.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 0.0!)
        Me.XrTable2.Name = "XrTable2"
        Me.XrTable2.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow2})
        Me.XrTable2.SizeF = New System.Drawing.SizeF(1364.0!, 25.0!)
        Me.XrTable2.StylePriority.UseFont = False
        Me.XrTable2.StylePriority.UseTextAlignment = False
        Me.XrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter

        '--------------------------------------------------------------------------------------------
        '
        'XrTableRow2
        '
        Me.XrTableRow2.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell1})
        Me.XrTableRow2.Name = "XrTableRow2"
        Me.XrTableRow2.Weight = 1.0R
        '
        'XrTableCell1
        '
        Me.XrTableCell1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "INDDetailProductionCenName")})
        Me.XrTableCell1.Name = "XrTableCell1"
        Me.XrTableCell1.Font = New System.Drawing.Font("Arial", 6.0!)
        Me.XrTableCell1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.XrTableCell1.StylePriority.UseTextAlignment = False
        Me.XrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell1.Weight = 68
        '
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub

    Private Sub XrTableCell3_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell3.BeforePrint
        XrTableRow3.Cells.Item(XrTableRow1.Cells.Count - 1).Text() = String.Format("{0:#,#}", total)
    End Sub
End Class