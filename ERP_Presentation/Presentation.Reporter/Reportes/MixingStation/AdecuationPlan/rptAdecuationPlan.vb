Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.CloudAgent

Public Class rptAdecuationPlan
    Implements IReport
    Implements IReportAsync

    Private INDUser As SecurityRepository.UserXpo
    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return Nothing
        End Get
    End Property

    Private _batchCodes As List(Of String)
    Private _allBatchCodes As List(Of String)
    Private _datasourceOriginal As List(Of ProductMixingStation)

    ' Constantes de paginación
    Private Const BATCHES_PER_PAGE As Integer = 8
    Private Const ITEMS_PER_PAGE As Integer = 8

    ' Variables de control de paginación
    Private _totalBatchPages As Integer = 1
    Private _currentBatchPageIndex As Integer = 0
    Private _usePagination As Boolean = False

    Dim AdjustLabelGroup As Boolean = True

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Propiedad pública para obtener el número total de páginas de lotes
    ''' </summary>
    Public ReadOnly Property TotalPaginasLotes As Integer
        Get
            Return _totalBatchPages
        End Get
    End Property

    Public Sub CargarDatasource() Implements IReport.CargarDataSource
        _datasourceOriginal = IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ListProductRawMaterial(ParametrosReporte(0), ParametrosReporte(1), ParametrosReporte(2), ParametrosReporte(3), IndigoSessionValues)

        ' Obtener todos los lotes únicos
        _allBatchCodes = _datasourceOriginal.SelectMany(Function(m) If(m.BatchData IsNot Nothing, m.BatchData.Select(Function(o) o.BatchCode), New List(Of String))).Distinct().Where(Function(b) Not String.IsNullOrEmpty(b)).OrderBy(Function(b) b).ToList()

        Dim batchReadjustments As List(Of String) = Nothing

        If ParametrosReporte(4).ToString().Equals("Cerrada") AndAlso _allBatchCodes.Count > 0 Then
            Dim batchCodesIn = String.Join(",", _allBatchCodes.Select(Function(m) $"'{m}'").ToArray())
            Dim readjustmentsDt = IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ExecuteQueryDt($"
    select distinct r.BatchCode
    from MixingStation.Readjustments r (nolock)
    join MixingStation.RequestPackageDetailStatus rp (nolock) on r.RequestPackageDetailStatusId = rp.Id
    where rp.BatchCode in ({batchCodesIn})
", IndigoSessionValues.TransactionalContainer)

            Dim rows = readjustmentsDt.Rows.Cast(Of DataRow)()
            batchReadjustments = rows.Select(Function(m) m("BatchCode").ToString()).ToList()

            If batchReadjustments IsNot Nothing AndAlso batchReadjustments.Count > 0 Then
                _allBatchCodes.AddRange(batchReadjustments)
                _allBatchCodes = _allBatchCodes.Distinct().Where(Function(b) Not String.IsNullOrEmpty(b)).OrderBy(Function(b) b).ToList()
            End If
        End If

        ' Verificar si se debe usar paginación (ParametrosReporte(8))
        If ParametrosReporte.Length > 8 AndAlso ParametrosReporte(8) IsNot Nothing Then
            If TypeOf ParametrosReporte(8) Is Boolean Then
                ' Si es booleano, indica si usar paginación
                _usePagination = CBool(ParametrosReporte(8))
                _currentBatchPageIndex = 0
            ElseIf TypeOf ParametrosReporte(8) Is Integer Then
                ' Si es entero, es el índice de página (compatibilidad con PopUpCampaignBatchRecord)
                _usePagination = True
                _currentBatchPageIndex = CInt(ParametrosReporte(8))
            End If
        Else
            _usePagination = False
        End If

        ' Lógica condicional según paginación
        If _usePagination Then
            ' Con paginacion (Cronograma de Producción)
            ' Calcular el número total de páginas basado en lotes
            _totalBatchPages = Math.Max(1, CInt(Math.Ceiling(_allBatchCodes.Count / CDbl(BATCHES_PER_PAGE))))

            ' Obtener los lotes para la página actual
            Dim startIndex As Integer = _currentBatchPageIndex * BATCHES_PER_PAGE
            _batchCodes = _allBatchCodes.Skip(startIndex).Take(BATCHES_PER_PAGE).ToList()

            ' Dibujar la tabla con los lotes de la página actual
            drawTable(Nothing)

            ' Crear el datasource paginado
            Dim paginatedDatasource = CreatePaginatedDatasource()
            Me.DataSource = paginatedDatasource

            ' Ajustar tamaño de página
            AdjustPageSize(paginatedDatasource.Count)
        Else
            ' Sin paginacion (Campaña - como estaba antes)
            _totalBatchPages = 1
            _currentBatchPageIndex = 0
            _batchCodes = _allBatchCodes ' All batches

            ' Dibujar la tabla con TODOS los lotes
            drawTable(Nothing)

            ' Usar el datasource original completo SIN FILTRAR
            Me.DataSource = _datasourceOriginal

            ' Ajustar tamaño de página para que todo quepa en una página
            AdjustPageSize(_datasourceOriginal.Count)
        End If
    End Sub

    ''' <summary>
    ''' Crea un datasource paginado que incluye:
    ''' - PRIMERO: Productos que TIENEN datos en los lotes de esta página
    ''' - LUEGO: Productos nuevos para completar hasta 8 por grupo
    ''' - Solo los BatchData correspondientes a los lotes de esta página
    ''' </summary>
    Private Function CreatePaginatedDatasource() As List(Of ProductMixingStation)
        Dim result As New List(Of ProductMixingStation)

        ' Procesar grupo Principal - Solo medicamentos con datos en los lotes actuales
        Dim principalsWithData = _datasourceOriginal.Where(Function(p) p.GroupName = "Principal" AndAlso p.BatchData IsNot Nothing AndAlso p.BatchData.Any(Function(b) _batchCodes.Contains(b.BatchCode))).ToList()
        Dim principals = principalsWithData.ToList()

        ' Procesar grupo Otro - Solo medicamentos con datos en los lotes actuales
        Dim othersWithData = _datasourceOriginal.Where(Function(p) p.GroupName = "Otro" AndAlso p.BatchData IsNot Nothing AndAlso p.BatchData.Any(Function(b) _batchCodes.Contains(b.BatchCode))).ToList()
        Dim others = othersWithData.ToList()

        ' Procesar grupo Canasta - Solo items con datos en los lotes actuales
        Dim basketWithData = _datasourceOriginal.Where(Function(p) p.GroupName = "Canasta" AndAlso p.BatchData IsNot Nothing AndAlso p.BatchData.Any(Function(b) _batchCodes.Contains(b.BatchCode))).ToList()
        Dim basket = basketWithData.ToList()

        ' Procesar grupo Solicitudes Manuales - Solo items con datos en los lotes actuales
        Dim manualRequestsWithData = _datasourceOriginal.Where(Function(p) p.GroupName = "Solicitudes Manuales" AndAlso p.BatchData IsNot Nothing AndAlso p.BatchData.Any(Function(b) _batchCodes.Contains(b.BatchCode))).ToList()
        Dim manualRequests = manualRequestsWithData.ToList()

        ' Agregar productos con BatchData filtrado a los lotes de esta página
        For Each product In principals
            Dim productCopy = CloneProduct(product)
            If productCopy.BatchData IsNot Nothing Then
                productCopy.BatchData = productCopy.BatchData.Where(Function(b) _batchCodes.Contains(b.BatchCode)).ToList()
            End If
            'Solo agregar si tiene BatchData después del filtro
            If productCopy.BatchData IsNot Nothing AndAlso productCopy.BatchData.Count > 0 Then
                result.Add(productCopy)
            End If
        Next

        For Each product In others
            Dim productCopy = CloneProduct(product)
            If productCopy.BatchData IsNot Nothing Then
                productCopy.BatchData = productCopy.BatchData.Where(Function(b) _batchCodes.Contains(b.BatchCode)).ToList()
            End If
            'Solo agregar si tiene BatchData después del filtro
            If productCopy.BatchData IsNot Nothing AndAlso productCopy.BatchData.Count > 0 Then
                result.Add(productCopy)
            End If
        Next

        For Each product In basket
            Dim productCopy = CloneProduct(product)
            If productCopy.BatchData IsNot Nothing Then
                productCopy.BatchData = productCopy.BatchData.Where(Function(b) _batchCodes.Contains(b.BatchCode)).ToList()
            End If
            'Solo agregar si tiene BatchData después del filtro
            If productCopy.BatchData IsNot Nothing AndAlso productCopy.BatchData.Count > 0 Then
                result.Add(productCopy)
            End If
        Next

        For Each product In manualRequests
            Dim productCopy = CloneProduct(product)
            If productCopy.BatchData IsNot Nothing Then
                productCopy.BatchData = productCopy.BatchData.Where(Function(b) _batchCodes.Contains(b.BatchCode)).ToList()
            End If
            'Solo agregar si tiene BatchData después del filtro
            If productCopy.BatchData IsNot Nothing AndAlso productCopy.BatchData.Count > 0 Then
                result.Add(productCopy)
            End If
        Next

        Return result
    End Function



    ''' <summary>
    ''' Clona un objeto ProductMixingStation para evitar modificar el original
    ''' </summary>
    Private Function CloneProduct(original As ProductMixingStation) As ProductMixingStation
        Dim copy As New ProductMixingStation()
        copy.ItemId = original.ItemId
        copy.ProductCode = original.ProductCode
        copy.ProductName = original.ProductName
        copy.ProductShortName = original.ProductShortName
        copy.OnlyNameProduct = original.OnlyNameProduct
        copy.QuantityMaterialRaw = original.QuantityMaterialRaw
        copy.Dosis = original.Dosis
        copy.RequiredDosis = original.RequiredDosis
        copy.ItemType = original.ItemType
        copy.GroupName = original.GroupName
        copy.TypeProduct = original.TypeProduct
        copy.NameTypeProduct = original.NameTypeProduct
        copy.BatchCode = original.BatchCode
        copy.MeasureUnitAbbreviation = original.MeasureUnitAbbreviation
        copy.TotalToUseQuantity = original.TotalToUseQuantity
        copy.DosisRequeridaPaquete = original.DosisRequeridaPaquete
        copy.Concentration = original.Concentration
        copy.Vehicle = original.Vehicle
        copy.Thinner = original.Thinner
        copy.FormulationType = original.FormulationType
        copy.NPTItemOrder = original.NPTItemOrder
        copy.RequestMixingStationDetailId = original.RequestMixingStationDetailId
        copy.PackageId = original.PackageId
        copy.MSClass = original.MSClass
        copy.VerifiedFor = original.VerifiedFor
        copy.ConfirmedFor = original.ConfirmedFor
        copy.IsPackagePersonalized = original.IsPackagePersonalized

        ' Clonar BatchData
        If original.BatchData IsNot Nothing AndAlso original.BatchData.Count > 0 Then
            copy.BatchData = New List(Of Domain.Entities.BatchData)()
            For Each b As Domain.Entities.BatchData In original.BatchData
                Dim newBatch As New Domain.Entities.BatchData()
                newBatch.BatchCode = b.BatchCode
                newBatch.Quantity = b.Quantity
                newBatch.MeasurementUnit = b.MeasurementUnit
                copy.BatchData.Add(newBatch)
            Next
        End If

        Return copy
    End Function

    ''' <summary>
    ''' Ajusta el tamaño de página según el contenido
    ''' </summary>
    Private Sub AdjustPageSize(rowCount As Integer)
        ' Usar el ancho real de la tabla en lugar de calcular por número de celdas
        Dim requiredWidth As Integer = XrTable1.Width
        Dim standardHeightPerRow As Integer = 100
        Dim requiredHeight As Integer = rowCount * standardHeightPerRow

        ' Agregar 15% de margen extra para evitar compresión de columnas
        requiredWidth = CInt(requiredWidth * 1.15)

        ' Tamaño vertical
        If requiredHeight > Me.PageHeight Then
            Dim heightDifference As Integer = requiredHeight - Me.PageHeight
            Me.PageFooter.TopF -= heightDifference
            Me.PageHeight = requiredHeight
        Else
            Me.PageHeight = Math.Max(Me.PageHeight, requiredHeight)
        End If

        ' Tamaño horizontal - siempre configurar al ancho requerido para evitar compresión
        Me.PageWidth = Math.Max(Me.PageWidth, requiredWidth)
        Me.Landscape = True ' Forzar modo horizontal cuando hay columnas de lotes
    End Sub

    Private Sub drawTable(additionalBatchCodes As List(Of String))
        If ParametrosReporte(7) = 5 Then
            XrTable2.DeleteColumn(XrTableCell6)
            XrTable1.DeleteColumn(CellRequiredQuantity)
            XrTable2.DeleteColumn(XrTableCell12)
            XrTable1.DeleteColumn(XrTableCell10)
        End If

        XrLabel2.Text = CInt(ParametrosReporte(4)).ToString()
        XrLabel5.Text = (ParametrosReporte(5)).ToString()
        XrLabel7.Text = (ParametrosReporte(6)).ToString()
        XrTable2.SuspendLayout()
        XrTable1.SuspendLayout()

        If additionalBatchCodes IsNot Nothing Then _batchCodes.AddRange(additionalBatchCodes)

        For Each batchCode In _batchCodes

            If IsEmptyValue(batchCode) Then
                Continue For
            End If
            Dim XrTableCellx = XrTable1.InsertColumnToLeft(XrTableCell9)(0)
            XrTableCellx.Multiline = True
            XrTableCellx.Name = batchCode
            XrTableCellx.Tag = batchCode
            XrTableCellx.CanGrow = False
            XrTableCellx.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
            XrTableCellx.Width = 100

            Dim cellHeader = XrTable2.InsertColumnToLeft(XrTableCell11)

            cellHeader(0).Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
            cellHeader(0).BackColor = Color.LightSteelBlue
            cellHeader(0).StylePriority.UseFont = False
            cellHeader(0).Multiline = True
            cellHeader(0).CanGrow = False
            cellHeader(0).CanShrink = True
            cellHeader(0).Width = 100
            cellHeader(0).RowSpan = 2
            cellHeader(0).Text = $"LOTE: {batchCode}"

            cellHeader(1).Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
            cellHeader(1).BackColor = Color.LightSteelBlue
            cellHeader(1).StylePriority.UseFont = False
            cellHeader(1).Multiline = True
            cellHeader(1).Name = batchCode
            cellHeader(1).Tag = batchCode
            cellHeader(1).CanGrow = False
            cellHeader(1).Width = 100

            XrTable1.Width += 100
            XrTable2.Width += 100
        Next

        LblGroup.Width = XrTable2.Width
        XrTable1.ResumeLayout()
        XrTable2.ResumeLayout()

    End Sub

    Private Sub XrTableRow1_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableRow1.BeforePrint
        Dim data As ProductMixingStation = GetCurrentRow()
        Dim row As XRTableRow = sender
        INDUserImp.Text = "Usuario Impresión: " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName

        If data IsNot Nothing AndAlso data.BatchData IsNot Nothing Then
            Dim cells As List(Of XRTableCell) = row.Cells.Cast(Of XRTableCell).ToList()

            For Each cell As XRTableCell In row.Cells
                If cell.Tag IsNot Nothing AndAlso Not String.IsNullOrEmpty(cell.Tag.ToString()) Then
                    Dim batch = data.BatchData.Find(Function(m) m.BatchCode = cell.Tag.ToString())

                    If batch IsNot Nothing Then
                        If data.MSClass = 7 OrElse data.MSClass = 5 Then
                            cell.Text = $"{CInt(Math.Ceiling(data.Concentration))} {batch.MeasurementUnit}"
                        Else
                            cell.Text = $"{batch.Quantity} {batch.MeasurementUnit}"
                        End If
                    Else
                        cell.Text = ""
                    End If
                End If
            Next
        End If
    End Sub

    Public Function CargarDataSourceAsync() As Task Implements IReportAsync.CargarDataSourceAsync
        Return Task.Factory.StartNew(AddressOf CargarDatasource)
    End Function

    Public Sub CargarImagenes() Implements IReport.CargarImagenes
    End Sub

    Private Sub LblGroup_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles LblGroup.BeforePrint
        If Not XrTableCell12.Visible AndAlso AdjustLabelGroup Then
            LblGroup.WidthF = LblGroup.WidthF - XrTableCell12.WidthF
            AdjustLabelGroup = False
        End If

        ' Agregar indicador de lotes en el grupo Principal
        Dim data As ProductMixingStation = GetCurrentRow()
        If data IsNot Nothing AndAlso data.GroupName = "Principal" Then
            Dim lbl As XRLabel = TryCast(sender, XRLabel)
            If lbl IsNot Nothing Then
                If _usePagination Then
                    ' Calcular el rango de lotes según la página actual
                    Dim batchStart As Integer = (_currentBatchPageIndex * BATCHES_PER_PAGE) + 1
                    Dim batchEnd As Integer = Math.Min(batchStart + BATCHES_PER_PAGE - 1, _allBatchCodes.Count)
                    Dim totalBatches As Integer = _allBatchCodes.Count
                    Dim currentPage As Integer = _currentBatchPageIndex + 1

                    If totalBatches > 0 Then
                        lbl.Text = $"Principal - Lotes {batchStart} al {batchEnd} de {totalBatches} (Página {currentPage} de {_totalBatchPages})"
                    Else
                        lbl.Text = "Principal"
                    End If
                Else
                    ' Sin paginación, mostrar título simple
                    lbl.Text = "Principal"
                End If
            End If
        End If
    End Sub

    Private Sub rptAdecuationPlan_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.BeforePrint
        ' Asegurar que el reporte cabe en una sola página para que SingleFilePageByPage funcione
        ' El tamaño de página ya fue ajustado en AjustarTamanioPagina
    End Sub

    Private Sub PageFooter_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles PageFooter.BeforePrint
        ' Si no usa paginación, siempre mostrar información de auditoría
        If Not _usePagination Then
            ' Siempre mostrar (comportamiento original)
            Return
        End If

        ' Si usa paginación, solo mostrar información de auditoría en la última página
        Dim isLastPage As Boolean = (_currentBatchPageIndex = _totalBatchPages - 1)

        ' Ocultar controles de auditoría si no es la última página
        If Not isLastPage Then
            ' Ocultar las etiquetas de auditoría
            For Each control As XRControl In PageFooter.Controls
                If control.Name = "XrLabel15" OrElse control.Name = "XrLabel16" OrElse
                   control.Name = "XrLabel8" OrElse control.Name = "XrLabel9" OrElse
                   control.Name = "XrLabel10" OrElse control.Name = "XrLabel11" OrElse
                   control.Name = "XrLabel12" OrElse control.Name = "XrLabel13" OrElse
                   control.Name = "XrTable4" Then
                    control.Visible = False
                End If
            Next
        Else
            ' Mostrar los controles en la última página
            For Each control As XRControl In PageFooter.Controls
                control.Visible = True
            Next
        End If
    End Sub
End Class
