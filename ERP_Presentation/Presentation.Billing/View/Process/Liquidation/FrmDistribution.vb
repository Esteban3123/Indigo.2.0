#Region "Imports"

Imports System.Collections.Concurrent
Imports DevExpress.XtraGrid.Views.Grid
Imports Infrastructure.CrossCutting.Base
Imports System.Dynamic
Imports Presentation.Base.BaseClass
Imports Presentation.Billing.MVP
Imports DevExpress.XtraLayout
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Inventory.MVP
Imports Domain.Entities

#End Region

Public Class FrmDistribution

#Region "Consts"

    ''' <summary>
    ''' Cantidad maxima de salarios mínimos legales vigentes
    ''' a cobrar por aseguradora
    ''' </summary>
    Private Const MAX_SMLV As Decimal = 800

#End Region

#Region "Fields"

    ''' <summary>
    ''' Cursor de carga indigo
    ''' </summary>
    Private _indigoCursor As System.Windows.Forms.Cursor

    ''' <summary>
    ''' Valor que indica si el frontal ya ha sido cargado
    ''' </summary>
    Private _isLoaded As Boolean

    ''' <summary>
    ''' Tipo de distribución seleccionado
    ''' </summary>
    Private _distributionType As DistributionType

    ''' <summary>
    ''' Id del folio origen
    ''' </summary>
    Private _sourceFolioId As Integer

    ''' <summary>
    ''' Valor a distribuir
    ''' </summary> 
    Private _distributeValue As Decimal

    ''' <summary>
    ''' Porcentaje a distribuir
    ''' </summary>
    Private _distributeDiscountValue As Decimal

    ''' <summary>
    ''' Lista de productos y servicios a distribuir
    ''' </summary>
    Private _productsAndServices As List(Of Object)

    ''' <summary>
    ''' Valor máximo a cobrar por aseguradora
    ''' </summary>
    Private _insuranceTopValue As Decimal

    ''' <summary>
    ''' Parámetros usados en la distribución
    ''' </summary>
    Private _objParams As Object

    ''' <summary>
    ''' método de distribucion: 1 - Traslado Folio; 2 - Corte Cuentas
    ''' </summary>
    Private _distributionMethod As eDistributionMethod

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene el Id del folio destino seleccionado
    ''' </summary>
    ''' <returns>Id del folio destino seleccionado</returns>
    Public ReadOnly Property TargetFolioIdSelected As Integer
        Get
            Return Me.GleTargetFolio.EditValue
        End Get
    End Property

    ''' <summary>
    ''' Obtiene los parámetros usados en la distribución
    ''' </summary>
    ''' <returns>Parámetros de la distribución</returns>
    Public ReadOnly Property ObjParams As Object
        Get
            Return Me._objParams
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el método de distribución
    ''' </summary>
    Public Property DistributionMethod As eDistributionMethod
        Get
            Return Me._distributionMethod
        End Get
        Set(value As eDistributionMethod)
            Me._distributionMethod = value
        End Set
    End Property

    Public Property FormOwner As FrmLiquidation

    ''' <summary>
    ''' Muestra un mensaje en el frontal
    ''' </summary>
    ''' <param name="Icon">Icono del tipo de mensaje</param>
    ''' <value>Mensaje a mostrar</value>
    Public WriteOnly Property ShowMessage(ByVal Icon As Base.EeventViewerImages) As String
        Set(value As String)
            If Icon = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icon = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icon = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el Id del folio origen
    ''' </summary>
    ''' <value>Id del folio origen</value>
    ''' <returns>El Id del folio origen</returns>
    Public Property SourceFolioId As Integer
        Get
            Return Me._sourceFolioId
        End Get
        Set(value As Integer)
            Me._sourceFolioId = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el tipo de distribución a usar
    ''' </summary>
    ''' <value>Tipo de distribución a usar</value>
    ''' <returns>Tipo de distribución usado</returns>
    Public Property DistributionType As DistributionType
        Get
            Return Me._distributionType
        End Get
        Set(value As DistributionType)
            Me._distributionType = value
            Me.PrepareForm(Me._distributionType)
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la lista de folios destino
    ''' </summary>
    ''' <value>Lista de folios destino</value>
    ''' <returns>La lista de folios destino</returns>
    Public Property TargetFolios As List(Of Object)
        Get
            Return Me.GleTargetFolio.Properties.DataSource
        End Get
        Set(value As List(Of Object))
            Me.GleTargetFolio.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la lista de productos y servicios a distribuir
    ''' </summary>
    ''' <value>Lista de productos y servicios a distribuir</value>
    ''' <returns>La lista de productos y servicios a distribuir</returns>
    Public Property ProductsAndServices As List(Of Object)
        Get
            Return Me._productsAndServices
        End Get
        Set(value As List(Of Object))
            Dim resultGetValue = Me.GetDistributeValue(value)
            Me._distributeValue = resultGetValue.Item1
            Me._distributeDiscountValue = resultGetValue.Item2

            Me._productsAndServices = value
            Me.GdcProductService.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el LayoutControlGroup del cuerpo de la distribución
    ''' </summary>
    ''' <returns>LayoutControlGroup del cuerpo de la distribución</returns>
    Public ReadOnly Property LayoutBodyDistribution As LayoutControlGroup
        Get
            Return Me.LycgDistribution
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el LayoutControlItem de la barra de carga
    ''' </summary>
    ''' <returns>LayoutControlItem de la barra de carga</returns>
    Public ReadOnly Property LayoutAsyncOperationBar As LayoutControlItem
        Get
            Return Me.LyciAsyncOperationBar
        End Get
    End Property

#End Region

#Region "Events"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _indigoCursor = Nothing
        _isLoaded = Nothing
        _distributionType = Nothing
        _sourceFolioId = Nothing
        _distributeValue = Nothing
        _distributeDiscountValue = Nothing
        _productsAndServices = Nothing
        _insuranceTopValue = Nothing
        _objParams = Nothing
        _distributionMethod = Nothing
    End Sub

#End Region

#Region "Builders"

    ''' <summary>
    ''' Incializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New()
        Me.New(Billing.DistributionType.OneByOne, Nothing, New List(Of Object), New ExpandoObject(), 0)
    End Sub

    ''' <summary>
    ''' Incializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="distributionType">Tipo de distribución a aplicar en el formulario</param>
    ''' <param name="targetFolios">Lista de los folios destino</param>
    ''' <param name="smlv">Valor del salario minimo legal vigente</param>
    ''' <param name="targetFolioId">Id del folio por defecto</param>
    Public Sub New(ByVal distributionType As DistributionType, _formOwner As FrmLiquidation, ByVal targetFolios As List(Of Object), ByVal objParams As Object, ByVal smlv As Decimal, Optional ByVal targetFolioId As Integer = -1)
        InitializeComponent()
        Me._objParams = objParams
        Me.FormOwner = _formOwner
        Me._insuranceTopValue = Utils.RoundValue((smlv / 30 * MAX_SMLV), Utils.RoundLevel.Hundred)
        Me.ProductsAndServices = objParams.ProductsAndServices
        Me.SourceFolioId = objParams.SourceFolioId
        Me.TargetFolios = targetFolios
        Me.GleTargetFolio.EditValue = targetFolioId
        Me.DistributionType = distributionType
        Me._isLoaded = True

        If distributionType = Billing.DistributionType.NoPOSProduct Then
            GdcProductService.MainView = INDBgvProducts
            Me.BtnDistribute.Enabled = True
        Else
            GdcProductService.MainView = GdvProductService
        End If

    End Sub

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Aqui se carga la definicion para la rejilla
    ''' </summary>
    Private Sub FrmDistribution_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.LoadGridViewDefinitions()
        Me._indigoCursor = ChangeCursorIndigo()
        Using model As New MCtrFolio()
            INDRptProductsPOS.DataSource = model.ListInventoryProduct()
        End Using
        Me.TxtValue.Properties.MaxValue = 0
        Me._productsAndServices.ForEach(Sub(i) Me.TxtValue.Properties.MaxValue += i.TotalFolioValue)
    End Sub

    ''' <summary>
    ''' Aqui se convierte el valor dado en porcentaje
    ''' </summary>
    Private Sub TxtValue_EditValueChanged(sender As Object, e As EventArgs) Handles TxtValue.EditValueChanged
        If Me._isLoaded Then
            Me._isLoaded = False

            Me.TxtPercentageOrQuantity.EditValue = (Me.TxtValue.EditValue / Me._distributeValue) * 100
            Me.DistributeValues()
            Window.Utils.SetValueToProperty(Me.BtnDistribute, "Enabled", Not (Me.TxtValue.EditValue = 0))

            Me._isLoaded = True
        End If
    End Sub

    ''' <summary>
    ''' Aqui se convierte el porcentaje dado a valor
    ''' </summary>
    Private Sub TxtPercentage_EditValueChanged(sender As Object, e As EventArgs) Handles TxtPercentageOrQuantity.EditValueChanged
        If Me._isLoaded Then
            Me._isLoaded = False
            Me.TxtValue.EditValue = Me._distributeValue * (Me.TxtPercentageOrQuantity.EditValue / 100)
            Me.DistributeValues()
            Window.Utils.SetValueToProperty(Me.BtnDistribute, "Enabled", Not (Me.TxtValue.EditValue = 0))
            Me._isLoaded = True
        End If
    End Sub

    ''' <summary>
    ''' Aqui se le da formato a los datos segun la cultura
    ''' </summary>
    Private Sub GdvProductService_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles GdvProductService.CustomDrawCell
        Dim obj = Me.GdvProductService.GetRow(e.RowHandle)
        If obj IsNot Nothing Then
            If e.Column.Equals(Me.ColItemDate) Then
                e.DisplayText = CDate(obj.ItemDate).ToString(Infrastructure.CrossCutting.Base.SessionValues.Instance.Culture)
            ElseIf e.Column.Equals(Me.ColValTotal) Then
                e.DisplayText = CDec(obj.TotalFolioValue).ToString("C2", Infrastructure.CrossCutting.Base.SessionValues.Instance.Culture)
            ElseIf e.Column.Equals(Me.ColValueInSource) Then
                e.DisplayText = CDec(obj.SourceFolioValue).ToString("C2", Infrastructure.CrossCutting.Base.SessionValues.Instance.Culture)
            ElseIf e.Column.Equals(Me.ColValueInTarget) Then
                e.DisplayText = CDec(obj.TargetFolioValue).ToString("C2", Infrastructure.CrossCutting.Base.SessionValues.Instance.Culture)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Aqui se realiza el ordenamiento segun el tipo seleccionado
    ''' </summary>
    Private Sub IMCCriteria_EditValueChanged(sender As Object, e As EventArgs) Handles IMCCriteria.EditValueChanged
        Me.SortValues()
    End Sub

    ''' <summary>
    ''' Aqui se realiza la persistencia de la distribución
    ''' </summary>
    Private Sub BtnDistribute_Click(sender As Object, e As EventArgs) Handles BtnDistribute.Click
        Me._objParams.TargetFolioId = Me.GleTargetFolio.EditValue
        Me._objParams.EnumType = CInt(Me._distributionType)
        Me._objParams.DistribType = Me.IMCDistributionType.EditValue

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanging event of the GleTargetFolio control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ChangingEventArgs"/> instance containing the event data.</param>
    Private Sub GleTargetFolio_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles GleTargetFolio.EditValueChanging
        'Esta validación ya no se realiza debido a que si el folio destino tiene un caregroup diferente al de origen entonces se realiza una distribución por corte de cuentas

        'If TargetFolioIdSelected <> -1 AndAlso e.NewValue > 0 AndAlso IMCDistributionType.EditValue <> 4 Then
        '    Dim targetFolio = CType(FormOwner.DocumentManager.View.Documents.Where(Function(d) CType(d.Control, CtrFolio).Id = e.NewValue).First().Control, CtrFolio)
        '    Dim sourceFolio = CType(FormOwner.DocumentManager.View.Documents.Where(Function(d) CType(d.Control, CtrFolio).Id = SourceFolioId).First().Control, CtrFolio)

        '    If CType(targetFolio.CareGroupId, Integer) <> CType(sourceFolio.CareGroupId, Integer) Then
        '        e.Cancel = True
        '        ShowMessage(EeventViewerImages.Advertencia) = String.Format("El item seleccionado no se puede distribuir al folio {0} debido a que contiene un grupo de atención distinto", targetFolio.FolioOrder)
        '    End If
        'End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the IMCDistributionType control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IMCDistributionType_EditValueChanged(sender As Object, e As EventArgs) Handles IMCDistributionType.EditValueChanged
        If IMCDistributionType.EditValue IsNot Nothing Then
            If IMCDistributionType.EditValue = 2 Then 'Normal
                Me.TxtPercentageOrQuantity.Properties.MaxValue = 0
                PrepareForm(_distributionType)
            ElseIf IMCDistributionType.EditValue = 4 Then 'Unidad
                PrepareForm(Billing.DistributionType.Unit)
                Me.TxtPercentageOrQuantity.EditValue = 0.0
                Me.TxtPercentageOrQuantity.Properties.MaxValue = _productsAndServices(0).InvoiceQuantity - 1
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se cambia el método de distribución
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDcbmDistributionMethod_EditValueChanged(sender As Object, e As EventArgs) Handles INDcbmDistributionMethod.EditValueChanged
        If INDcbmDistributionMethod.EditValue = 1 Then
            DistributionMethod = eDistributionMethod.TrasladoFolio
        ElseIf INDcbmDistributionMethod.EditValue = 2 Then
            DistributionMethod = eDistributionMethod.CorteCuentas
        End If
        DistributeValues()
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanging event of the INDspnQuantity control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ChangingEventArgs"/> instance containing the event data.</param>
    Private Sub INDspnQuantity_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles TxtPercentageOrQuantity.EditValueChanging
        If IMCDistributionType.EditValue IsNot Nothing AndAlso IMCDistributionType.EditValue = 4 Then
            If Not e.NewValue.Equals("") Then
                If e.NewValue >= _productsAndServices(0).InvoiceQuantity Then
                    ShowMessage(EeventViewerImages.Advertencia) = "La cantidad a dividir del item no debe ser mayor o igual a la cantidad total"
                    e.Cancel = True
                End If
            End If
        End If
    End Sub

#Region "Save Definition To Xml"

    ''' <summary>
    ''' En cada uno de estos eventos se ejecuta el método que persiste
    ''' la definición de la vista a un archivo xml
    ''' </summary>
    Private Async Sub GdvProductService_ColumnWidthChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.ColumnEventArgs) Handles GdvProductService.ColumnWidthChanged
        Await Me.SaveDefinitionToXmlAsync(sender)
    End Sub
    Private Async Sub GdvProductService_ColumnPositionChanged(sender As Object, e As EventArgs) Handles GdvProductService.ColumnPositionChanged
        Await Me.SaveDefinitionToXmlAsync(sender.View)
    End Sub
    Private Async Sub GdvProductService_HideCustomizationForm(sender As Object, e As EventArgs) Handles GdvProductService.HideCustomizationForm
        Await Me.SaveDefinitionToXmlAsync(sender)
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Indica si el folio esta realizando una operación asíncrona
    ''' </summary>
    ''' <param name="isAsyncOperation">Valor que indica si se esta realizando una operación asíncrona</param>
    Public Sub IsAsyncOperation(Optional ByVal isAsyncOperation As Boolean = True)
        If isAsyncOperation Then
            Window.Utils.SetValueToProperty(Me, "Cursor", Me._indigoCursor)
            Window.Utils.SetValueToProperty(Me, "LayoutBodyDistribution.Enabled", False)
            Window.Utils.SetValueToProperty(Me, "LayoutAsyncOperationBar.Visibility", DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
        Else
            Window.Utils.SetValueToProperty(Me, "Cursor", System.Windows.Forms.Cursors.Default)
            Window.Utils.SetValueToProperty(Me, "LayoutBodyDistribution.Enabled", True)
            Window.Utils.SetValueToProperty(Me, "LayoutAsyncOperationBar.Visibility", DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        End If
    End Sub

    ''' <summary>
    ''' Ordena los productos o servicios por el tipo seleccionado
    ''' </summary>
    Public Sub SortValues()
        Me.GdvProductService.ClearSorting()
        Dim val As Byte = Me.IMCCriteria.EditValue
        Select Case val
            Case 1 'Fecha Ascendente
                Me.GdvProductService.Columns("ItemDate").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
            Case 2 'Fecha Descendente
                Me.GdvProductService.Columns("ItemDate").SortOrder = DevExpress.Data.ColumnSortOrder.Descending
            Case 3 'Valor Ascendente
                Me.GdvProductService.Columns("TotalFolioValue").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
            Case 4 'Valor Descendente
                Me.GdvProductService.Columns("TotalFolioValue").SortOrder = DevExpress.Data.ColumnSortOrder.Descending
        End Select
        Me.DistributeValues()
    End Sub

    ''' <summary>
    ''' Ejecuta la lógica para distribuir los valores en cada uno de los productos o servicios
    ''' </summary>
    Private Sub DistributeValues()
        If Me._productsAndServices IsNot Nothing Then
            Me.RowVisibilityHelper.CleanInvisibleRows()
            Me.GdvProductService.ShowLoadingPanel()
            Dim aux As Decimal = 0
            For Each o In Me.GdvProductService.DataController.GetAllFilteredAndSortedRows()
                Select Case Me._distributionType
                    Case Billing.DistributionType.OneByOne

                        If LyciTxtValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                            o.TargetFolioValue = CDec(Utils.GetDistributedValues(_distributeValue, TxtPercentageOrQuantity.EditValue).Item1) ' Me.TxtValue.EditValue ' 
                            o.SourceFolioValue = CDec(Utils.GetDistributedValues(_distributeValue, TxtPercentageOrQuantity.EditValue).Item2) '(o.TotalFolioValue - o.TargetFolioValue)
                        Else
                            o.TargetFolioValue = CDec(Utils.GetDistributedValuesByValue(_distributeValue, TxtValue.EditValue).Item1) ' Me.TxtValue.EditValue ' 
                            o.SourceFolioValue = CDec(Utils.GetDistributedValuesByValue(_distributeValue, TxtValue.EditValue).Item2) '(o.TotalFolioValue - o.TargetFolioValue)
                        End If

                        Dim discountDistributed As Tuple(Of Decimal, Decimal) = Utils.GetDistributedValues(_distributeDiscountValue, Me.TxtPercentageOrQuantity.EditValue)
                        o.TargetFolioGrandTotalDiscountValue = CDec(discountDistributed.Item1)
                        o.SourceFolioGrandTotalDiscountValue = CDec(discountDistributed.Item2)

                        If Me.IMCDistributionType.EditValue = 2 Then 'Normal
                            If Me.TxtPercentageOrQuantity.EditValue = 100 Then
                                o.SourceDistribType = 1
                                o.TargetDistribType = 1
                            Else
                                o.SourceDistribType = 2
                                o.TargetDistribType = 2
                            End If
                        End If

                    Case Billing.DistributionType.All, Billing.DistributionType.OneByMany

                        If DistributionMethod = eDistributionMethod.TrasladoFolio Then
                            If aux < Me.TxtValue.EditValue Then
                                Dim discountDistributed As Tuple(Of Decimal, Decimal) = Utils.GetDistributedValues(_distributeDiscountValue, Me.TxtPercentageOrQuantity.EditValue)
                                o.TargetFolioGrandTotalDiscountValue = CDec(discountDistributed.Item1)
                                o.SourceFolioGrandTotalDiscountValue = CDec(discountDistributed.Item2)

                                If (aux + o.TotalFolioValue) > Me.TxtValue.EditValue Then
                                    o.TargetFolioValue = CDec(Me.TxtValue.EditValue - aux)
                                    o.SourceFolioValue = CDec(o.TotalFolioValue - o.TargetFolioValue)
                                    aux += CDec(o.TargetFolioValue)
                                    o.SourceDistribType = 2
                                    o.TargetDistribType = 2
                                Else
                                    o.TargetFolioValue = CDec(o.TotalFolioValue)
                                    o.SourceFolioValue = CDec(o.TotalFolioValue - o.TargetFolioValue)
                                    aux += CDec(o.TargetFolioValue)
                                    o.SourceDistribType = 1
                                    o.TargetDistribType = 1
                                End If
                            Else
                                o.TargetFolioValue = CDec(0)
                                o.SourceFolioValue = CDec(o.TotalFolioValue - o.TargetFolioValue)
                                o.SourceDistribType = 1
                                o.TargetDistribType = 1
                            End If

                        ElseIf DistributionMethod = eDistributionMethod.CorteCuentas Then

                            Dim discountDistributed As Tuple(Of Decimal, Decimal) = Utils.GetDistributedValues(_distributeDiscountValue, Me.TxtPercentageOrQuantity.EditValue)
                            o.TargetFolioGrandTotalDiscountValue = CDec(discountDistributed.Item1)
                            o.SourceFolioGrandTotalDiscountValue = CDec(discountDistributed.Item2)

                            If (aux + o.TotalFolioValue) > Me.TxtValue.EditValue Then
                                o.SourceFolioValue = CDec(Me.TxtValue.EditValue - aux)
                                o.TargetFolioValue = CDec(o.TotalFolioValue - o.SourceFolioValue)
                                aux += CDec(o.SourceFolioValue)
                            Else
                                o.SourceFolioValue = CDec(o.TotalFolioValue)
                                o.TargetFolioValue = CDec(o.TotalFolioValue - o.SourceFolioValue)
                                aux += CDec(o.SourceFolioValue)
                            End If
                            If o.SourceFolioValue > 0 Then
                                o.SourceDistribType = 2
                                o.TargetDistribType = 2
                            Else
                                o.SourceDistribType = 1
                                o.TargetDistribType = 1
                            End If
                        End If

                    Case Billing.DistributionType.Insurance
                        If aux < Me._insuranceTopValue Then

                            'Dim discountDistributed As Tuple(Of Long, Long) = Utils.GetDistributedValues(_distributeDiscountValue, Me.TxtPercentageOrQuantity.EditValue)
                            'o.TargetFolioGrandTotalDiscountValue = CDec(discountDistributed.Item1)
                            'o.SourceFolioGrandTotalDiscountValue = CDec(discountDistributed.Item2)

                            If (aux + o.TotalFolioValue) > Me._insuranceTopValue Then
                                o.SourceFolioValue = _insuranceTopValue - aux
                                o.TargetFolioValue = CDec(o.TotalFolioValue) - CDec(o.SourceFolioValue)
                                aux += CDec(o.SourceFolioValue)
                                o.SourceDistribType = 2
                                o.TargetDistribType = 2
                            Else
                                o.TargetFolioValue = CDec(0)
                                o.SourceFolioValue = CDec(o.TotalFolioValue) 'CDec(o.TotalFolioValue - o.TargetFolioValue)
                                'Me.RowVisibilityHelper.InvisibleRows.Add(Me._productsAndServices.IndexOf(o))
                                aux += CDec(o.SourceFolioValue)
                                o.SourceDistribType = 1
                                o.TargetDistribType = 1
                            End If
                        Else
                            o.TargetFolioValue = CDec(o.TotalFolioValue)
                            o.SourceFolioValue = CDec(o.TotalFolioValue - o.TargetFolioValue)
                            o.SourceDistribType = 1
                            o.TargetDistribType = 1
                        End If
                        Window.Utils.SetValueToProperty(Me.BtnDistribute, "Enabled", o.TargetFolioValue > 0)
                End Select
            Next
            If IMCDistributionType.EditValue IsNot Nothing AndAlso IMCDistributionType.EditValue = 4 Then
                _objParams.DistributeQuantity = CType(Me.TxtPercentageOrQuantity.EditValue, Integer)
            End If
            Me.GdvProductService.HideLoadingPanel()
            Me.GdvProductService.RefreshData()
        End If
    End Sub

    ''' <summary>
    ''' Obtiene el valor a distribuir
    ''' </summary>
    ''' <param name="productsAndServices">Lista de productos y servicios a distribuir</param>
    ''' <returns>Valor a distribuir</returns>
    Private Function GetDistributeValue(ByVal productsAndServices As List(Of Object)) As Tuple(Of Decimal, Decimal)
        Dim resValue As Decimal = 0
        Dim resDiscountValue As Decimal = 0
        For Each o In productsAndServices
            resValue += o.SourceFolioValue
            resDiscountValue += o.SourceFolioGrandTotalDiscountValue
        Next
        Return New Tuple(Of Decimal, Decimal)(resValue, resDiscountValue)
    End Function

    ''' <summary>
    ''' Prepara el formulario para el tipo de distribución seleccionado
    ''' </summary>
    ''' <param name="type">Tipo de distribución a usar</param>
    Private Sub PrepareForm(ByVal type As DistributionType)
        Select Case type
            Case DistributionType.OneByOne
                Me.Text = Infrastructure.CrossCutting.Resources.ResourceManager.GetString("this_Text_" & DistributionType.OneByOne.ToString(), Me.GetType())
                Me.LyciDistributionType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.LyciTargetFolio.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.LyciTxtValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.vpn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.LyciDistributionMethod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                Me.LyciCriteria.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.vpn.Text = "Porcentaje a Distribuir"
                Me.TxtPercentageOrQuantity.Properties.Mask.EditMask = "P"
                Me.TxtPercentageOrQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric

                ColValueInTarget.Visible = True
                ColValueInSource.Visible = True

                'Case DistributionType.OneByMany
                '    Me.Text = Infrastructure.CrossCutting.Resources.ResourceManager.GetString("this_Text_" & DistributionType.OneByMany.ToString(), Me.GetType())
                '    Me.LyciTargetFolio.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                '    Me.LyciTxtPercentageOrUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                '    Me.LyciCriteria.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                '    Me.LyciTxtValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                '    Me.LyciDistributionType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                '    Me.LyciTxtPercentageOrUnit.Text = "Porcentaje a Distribuir"
                '    Me.TxtPercentageOrQuantity.Properties.Mask.EditMask = "P"
                '    Me.TxtPercentageOrQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric

                '    ColValueInTarget.Visible = True
                '    ColValueInSource.Visible = True

            Case Billing.DistributionType.All, Billing.DistributionType.OneByMany
                Me.Text = Infrastructure.CrossCutting.Resources.ResourceManager.GetString("this_Text_" & DistributionType.All.ToString(), Me.GetType())
                Me.LyciCriteria.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.LyciTargetFolio.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.LyciTxtValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.vpn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.LyciDistributionMethod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                Me.LyciDistributionType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.vpn.Text = "Porcentaje a Distribuir"
                Me.TxtPercentageOrQuantity.Properties.Mask.EditMask = "P"
                Me.TxtPercentageOrQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric

                ColValueInTarget.Visible = True
                ColValueInSource.Visible = True

                Me.SortValues()
            Case DistributionType.Insurance
                Me.Text = String.Format(Infrastructure.CrossCutting.Resources.ResourceManager.GetString("this_Text_" & DistributionType.Insurance.ToString(), Me.GetType()), MAX_SMLV, Me._insuranceTopValue.ToString("C2", Infrastructure.CrossCutting.Base.SessionValues.Instance.Culture))
                Me.LyciCriteria.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.LyciTargetFolio.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                Me.LyciDistributionMethod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.LyciTxtValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.vpn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.LyciDistributionType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.vpn.Text = "Porcentaje a Distribuir"
                Me.TxtPercentageOrQuantity.Properties.Mask.EditMask = "P"
                Me.TxtPercentageOrQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric

                ColValueInTarget.Visible = True
                ColValueInSource.Visible = True

                Me.SortValues()
            Case Billing.DistributionType.Unit

                Me.Text = Infrastructure.CrossCutting.Resources.ResourceManager.GetString("this_Text_" & DistributionType.Unit.ToString(), Me.GetType())
                Me.LyciDistributionType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.vpn.Text = "Cantidad a Distribuir"
                Me.TxtPercentageOrQuantity.Properties.Mask.EditMask = "[0-9]{0,8}"
                Me.TxtPercentageOrQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
                Me.vpn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                Me.LyciDistributionMethod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.LyciCriteria.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.LyciTxtValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.LyciTargetFolio.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                ColValueInSource.Visible = False
                ColValueInTarget.Visible = False

            Case Billing.DistributionType.NoPOSProduct

                Me.Text = "Distribución de Productos No POS"
                Me.LyciTargetFolio.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.LyciDistributionType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.LyciDistributionMethod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.LyciCriteria.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.LyciTxtValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                vpn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDEmpty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End Select
    End Sub

    Public Const MY_TYPE As String = "GridControl"

    ''' <summary>
    ''' Carga las definiciones de todas la vistas en el control
    ''' </summary>
    Public Async Sub LoadGridViewDefinitions()
        Await Me.LoadDefinitionFromXmlAsync(Me.GdvProductService)
    End Sub

    ''' <summary>
    ''' Carga la definición de la vista de un archivo Xml
    ''' </summary>
    ''' <param name="view">Vista a cargar</param>
    Private Function LoadDefinitionFromXmlAsync(ByVal view As GridView) As Task
        Return Task.Factory.StartNew(Sub()
                                         If Me.GdcProductService.InvokeRequired Then
                                             Me.GdcProductService.BeginInvoke(Sub()
                                                                                  Me.LoadDefinitionFromXml(view)
                                                                              End Sub)
                                         Else
                                             Me.LoadDefinitionFromXml(view)
                                         End If
                                     End Sub)
    End Function

    ''' <summary>
    ''' Carga la definición de la vista de un archivo Xml
    ''' </summary>
    ''' <param name="view">Vista a cargar</param>
    Private Sub LoadDefinitionFromXml(ByVal view As GridView)
        If My.Computer.FileSystem.FileExists(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me.Name, MY_TYPE, Me.Name & "." & view.Name & ".IndigoUser_" & SessionValues.Instance.UserIndigo & ".xml")) Then
            view.RestoreLayoutFromXml(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me.Name, MY_TYPE, Me.Name & "." & view.Name & ".IndigoUser_" & SessionValues.Instance.UserIndigo & ".xml"))
        End If
    End Sub

    ''' <summary>
    ''' Graba la definición de la vista a un archivo Xml
    ''' </summary>
    ''' <param name="view">Vista a persistir</param>
    Private Function SaveDefinitionToXmlAsync(ByVal view As GridView) As Task
        Return Task.Factory.StartNew(Sub()
                                         If Me.GdcProductService.InvokeRequired Then
                                             Me.GdcProductService.BeginInvoke(Sub()
                                                                                  Me.SaveDefinitionToXml(view)
                                                                              End Sub)
                                         Else
                                             Me.SaveDefinitionToXml(view)
                                         End If
                                     End Sub)
    End Function

    ''' <summary>
    ''' Graba la definición de la vista a un archivo Xml
    ''' </summary>
    ''' <param name="view">Vista a persistir</param>
    Private Sub SaveDefinitionToXml(ByVal view As GridView)
        If Not My.Computer.FileSystem.DirectoryExists(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me.Name, MY_TYPE, "")) Then
            My.Computer.FileSystem.CreateDirectory(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me.Name, MY_TYPE, ""))
        End If
        view.SaveLayoutToXml(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me.Name, MY_TYPE, Me.Name & "." & view.Name & ".IndigoUser_" & SessionValues.Instance.UserIndigo & ".xml"))
    End Sub

#End Region

    Private Sub INDRptProductsPOS_Popup(sender As Object, e As EventArgs) Handles INDRptProductsPOS.Popup
        'If INDBgvProducts.GetFocusedRow() Is Nothing Then
        '    Exit Sub
        'End If
        'Dim search = CType(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        'Dim view = search.Properties.View
        'view.ActiveFilterString = "ATCId.Code = '" & INDBgvProducts.GetFocusedRow().ATCCodePOS.trim() & "'"
        'view.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    End Sub

    Private Sub INDBgvProducts_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles INDBgvProducts.CustomDrawCell
        If GdcProductService.MainView.Name.Equals(INDBgvProducts.Name) Then
            Dim obj = Me.INDBgvProducts.GetRow(e.RowHandle)
            If obj IsNot Nothing Then
                If e.Column.Name.Equals(ColPOSProductCode.Name) Then
                    'e.DisplayText = obj.ProductDefaultPOSCode
                End If
            End If
        End If
    End Sub

    Private Async Sub INDRptProductsPOS_EditValueChanged(sender As Object, e As EventArgs) Handles INDRptProductsPOS.EditValueChanged
        BtnDistribute.Enabled = False
        Dim search = CType(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        Dim obj = INDBgvProducts.GetFocusedRow()
        If search.EditValue IsNot Nothing AndAlso obj IsNot Nothing Then
            obj.ProductPOSName = DirectCast(DirectCast(search.Properties.View.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.InventoryRepository.InventoryProductXpo).Name
            Using model As New MProductRate(Me.Tag)
                Dim _productRateDetail As ProductRateDetail = (Await model.GetProductRateDetailByCareGroupIdProductIdServiceDate(ObjParams.CareGroupId, search.EditValue, obj.ItemDate)).ObjectEmbbeded
                If _productRateDetail IsNot Nothing AndAlso _productRateDetail.Id > 0 Then
                    obj.ProductPOSUnitValue = _productRateDetail.SalesValue
                    obj.ProductPOSTotalValue = _productRateDetail.SalesValue * CDec(obj.InvoiceQuantity)
                    If _productRateDetail.SalesValue < CDec(obj.UnitValue) Then
                        obj.UnitValueDifference = CDec(obj.UnitValue) - _productRateDetail.SalesValue
                        obj.TargetFolioValue = (CDec(obj.UnitValue) - _productRateDetail.SalesValue) * CDec(obj.InvoiceQuantity)
                    Else
                        obj.UnitValueDifference = 0
                        obj.TargetFolioValue = 0
                    End If
                Else
                    obj.ProductPOSUnitValue = 0
                    obj.ProductPOSTotalValue = 0
                    obj.UnitValueDifference = 0
                    obj.TargetFolioValue = 0
                    ShowMessage(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ProductNoTarifa", "Inventory"), ObjParams.CareGroupCodeName, obj.ItemDate)
                End If
            End Using
        Else
            obj.ProductPOSUnitValue = 0
            obj.ProductPOSTotalValue = 0
            obj.UnitValueDifference = 0
            obj.TargetFolioValue = 0
        End If
        BtnDistribute.Enabled = True
        GdcProductService.RefreshDataSource()
    End Sub
End Class

Public Enum eDistributionMethod
    TrasladoFolio
    CorteCuentas
End Enum

''' <summary>
''' Tipo de distribución para prepara el formulario
''' </summary>
Public Enum DistributionType
    ''' <summary>
    ''' Prepara el formulario para una distribución uno a uno
    ''' </summary>
    OneByOne = 1
    ''' <summary>
    ''' Prepara el formulario para una distribución total
    ''' </summary>
    All = 2
    ''' <summary>
    ''' Prepara el formulario para una distribución para aseguradora
    ''' </summary>
    Insurance = 3
    ''' <summary>
    ''' Prepara el formulario para una distribución uno a uno de selección multiple
    ''' </summary>
    OneByMany = 4
    ''' <summary>
    ''' Distribución por unidad
    ''' </summary>
    Unit = 5

    ''' <summary>
    ''' Distribución para productos No POS
    ''' </summary>
    ''' <remarks></remarks>
    NoPOSProduct = 6

End Enum