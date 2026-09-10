'***********************************************************************
' Assembly         : MixingStation
' Author           : Giovanny Plazas L
' Created          : 17-09-2021
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Windows.Forms
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.MixingStation.MVP

#End Region

#Region "Enumerations"

Public Enum RemainderClass
    ownRemnant = 1
    usedRemainder = 2
End Enum

''' <summary>
''' Enumerable para identificar el tipo de preparación a realizar: Reconstitución, Dilución o ambas
''' </summary>
Public Enum ePreparationType
    Reconstitution = 1
    ReconstitutionDilution = 2
    Dilution = 3
    None = 4
End Enum

#End Region

Public Class PopupQuantityRemaining

#Region "BUILDER"

    ''' <summary>
    ''' Constructor del modal
    ''' </summary>
    Public Sub New(value As Boolean, Optional FromCampaignDetailId As Integer? = Nothing)
        ' This call is required by the designer.
        InitializeComponent()
        QRemainingOrHarnessed = value
        Me.FromCampaignDetailId = FromCampaignDetailId

        _presenter = New PCampaigns()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

#End Region

#Region "EVENTS"
    ''' <summary>
    ''' evento para ejecutar la accion del Boton Ok, el value si es True es para guardar sobrante si es false es para aprovechamiento
    ''' </summary>
    ''' <param name="senser"></param>
    ''' <param name="e"></param>
    ''' <param name="value"></param>
    Public Event GetItemsResult(senser As Object, e As GetQuantityRemainingEventArgs, value As Boolean, FromCampaignDetailId As Integer?)
#End Region

#Region "GLOBALS"
    ''' <summary>
    ''' Presentador de campañas
    ''' </summary>
    Private _presenter As PCampaigns

    ''' <summary>
    ''' codigo del producto de crystal
    ''' </summary>
    ''' <remarks></remarks>
    Dim _productCode As String
    ''' <summary>
    ''' cantidad que se va a entregar
    ''' </summary>
    ''' <remarks></remarks>
    Dim _quantityDeliver As Integer

    ''' <summary>
    ''' Clase de remanente: Propio o no propio
    ''' </summary>
    Public Property RemainderClass As RemainderClass

    ''' <summary>
    ''' Datasource se la rejilla
    ''' </summary>
    Private _dataSource As List(Of QuantityRemaining)

    ''' <summary>
    ''' Inyecta los datos al datasource de la rejilla
    ''' </summary>
    Public WriteOnly Property DataSource() As List(Of QuantityRemaining)
        Set(value As List(Of QuantityRemaining))

            If RemainderClass = RemainderClass.usedRemainder Then
                _dataSource = value.FindAll(Function(x) x.Status = 1 AndAlso x.CampaignDetailId <> FromCampaignDetailId)
            Else
                _dataSource = value.FindAll(Function(x) x.Status = 1)
            End If


            Dim objProduct As New PropertysProducts()

            For Each row In _dataSource
                Dim data = _presenter.ListViewQuantityRemaining(row.CampaignDetailId, row.ProductId, row.BatchSerialId)
                If data IsNot Nothing Then
                    Dim concentrationFormat = Format(data.Concentration, "0.00")
                    If QRemainingOrHarnessed Then
                        row.Id = data.Id
                        row.PreparationType = data.PreparationType
                        row.Concentration = data.Concentration
                        row.ConcentrationWithMeasurementUnit = String.Format("{0} {1}/{2}", concentrationFormat, data.MeasurementUnitMainMedicine, data.MeasurementUnitVolume)
                        row.Stability = data.Stability
                        row.UnitTimeStability = data.UnitTimeStability
                        row.StabilityWithTimeUnit = String.Format("{0} {1}", data.Stability, data.TimeUnitNameStability)
                        row.RemnantVolume = data.RemnantVolume
                        row.UnitRemnantVolume = data.MeasurementUnitVolumeId
                        row.Quantity = data.Quantity
                        row.UnitQuantity = data.MeasurementUnitMainMedicineId
                        row.MaximumVolume = data.MaximumVolume
                    Else
                        row.ConcentrationWithMeasurementUnit = String.Format("{0} {1}/{2}", concentrationFormat, data.MeasurementUnitMainMedicine, data.MeasurementUnitVolume)
                        row.VctoStability = CalculateDateStability(row)
                        row.QuantityHarnessed = data.Quantity
                        INDColDateStability.SortOrder = DevExpress.Data.ColumnSortOrder.Descending
                    End If
                End If

                objProduct.Concentration = row.Concentration
                objProduct.objProduct = _presenter.ListProductsATC(row.ProductId)
                Dim resCalculation = CalculationRemainingFormulationType(objProduct, Nothing)
                If resCalculation IsNot Nothing Then
                    If resCalculation.Concentration > 0 Then
                        row.Concentration = resCalculation.Concentration
                    End If
                    If resCalculation.ConcentrationWithMeasurementUnit IsNot Nothing Then
                        row.ConcentrationWithMeasurementUnit = resCalculation.ConcentrationWithMeasurementUnit
                    End If
                    ''Cambio abreviatura de unidad de medida dinamicamente
                    If resCalculation.AbbreviationVolumen IsNot Nothing Then
                        INDColRemnantVolume.Caption = String.Format("Vol. remanente real ({0})", resCalculation.AbbreviationVolumen)
                    End If
                    If resCalculation.AbbreviationWeight IsNot Nothing Then
                        INDColQuantity.Caption = String.Format("Cantidad real ({0})", resCalculation.AbbreviationWeight)
                    End If
                End If
            Next

            INDGcQuantityRemaining.DataSource = _dataSource
            INDGvQuantityRemaining.HideLoadingPanel()
            If value.IsNotNullAndAny() Then
                INDLciAdd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
        End Set
    End Property

    ''' <summary>
    ''' Fecha actual 
    ''' </summary>
    Dim _currentDate As Date

    ''' <summary>
    ''' true para accion en  sobrante  or false para accionar aprovechamiento
    ''' </summary>
    Private QRemainingOrHarnessed As Boolean

    ''' <summary>
    ''' Campaña desde donde se ejecuta la accion
    ''' </summary>
    Private FromCampaignDetailId As Integer?

#End Region

#Region "PROPERTIES"

    Public WriteOnly Property Product As String
        Set(value As String)
            _productCode = value
        End Set
    End Property

    Public Property RawMaterialQuantityBalance As Decimal

    Public WriteOnly Property Title As String
        Set(value As String)
            INDLcgMain.Text = value
        End Set
    End Property

    Private Property OpeningDate As DateTime
        Get
            Return INDDeOpeningDate.EditValue
        End Get
        Set(value As DateTime)
            INDDeOpeningDate.EditValue = value
        End Set
    End Property

    Public Property Remanent As Boolean

    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

#End Region

#Region "HANDLES"

#Region "Load"
    ''' <summary>
    ''' Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PopupCUM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDGvQuantityRemaining.OptionsView.ShowAutoFilterRow = False
        Using model As New Controls.MVP.MformBase
            OpeningDate = model.GetDateServer().Date
            INDDeOpeningDate.Properties.MinValue = OpeningDate.Date
            INDDeOpeningDate.Properties.MaxValue = OpeningDate.Date.AddHours(24).AddMilliseconds(-1)
        End Using

        If Not Remanent Then
            INDLciAdd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            IndigoGridView1.SetListAcction(INDGvQuantityRemaining, {eAcciones.UseToCampaing, eAcciones.DropRemanent}.ToList())
            IndigoGridView1.RepositoryItemPopupContainerEdit.PopupControl.ResetAutoSizeMode()
            Dim col = INDGvQuantityRemaining.Columns.FirstOrDefault(Function(m) m.Name.Equals("colActions"))
            If col IsNot Nothing Then col.Width = 90
        End If

        INDLblQuantity.Text = $"Cantidad Saldo Campaña: {RawMaterialQuantityBalance}"
        ActionColumn()
        LoadPreparationType()
    End Sub
#End Region

#Region "Click"
    ''' <summary>
    ''' accion en el boton Ok
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBtnOk_Click(sender As Object, e As EventArgs) Handles INDBtnOk.Click
        Dim args As New GetQuantityRemainingEventArgs()
        Dim itemsToValidate = New List(Of QuantityRemaining)

        itemsToValidate = _dataSource.FindAll(Function(x) IIf(QRemainingOrHarnessed, x.Quantity, x.QuantityHarnessed) > 0)
        For Each Item In itemsToValidate
            Item.OpeningDate = OpeningDate
        Next

        args.ListItemDetails = itemsToValidate

        If INDDeOpeningDate.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe registrar una fecha y hora de apertura"
            Exit Sub
        End If

        RaiseEvent GetItemsResult(Nothing, args, QRemainingOrHarnessed, FromCampaignDetailId)
        Me.Close()
    End Sub

    Private Sub UseToCampaing()
        Dim args As New GetQuantityRemainingEventArgs()
        Dim itemsToValidate = New List(Of QuantityRemaining)

        itemsToValidate = (From x In INDGvQuantityRemaining.GetSelectedRows() Where Not INDGvQuantityRemaining.IsGroupRow(x) Select DirectCast(INDGvQuantityRemaining.GetRow(x), QuantityRemaining)).ToList()
        For Each Item In itemsToValidate
            Item.OpeningDate = OpeningDate
        Next

        args.ListItemDetails = itemsToValidate

        If INDDeOpeningDate.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe registrar una fecha y hora de apertura"
            Exit Sub
        End If

        RaiseEvent GetItemsResult(Nothing, args, QRemainingOrHarnessed, FromCampaignDetailId)
        Me.Close()
    End Sub

    Private Sub DropRemanent()
        Using formulario As New PopUpActionQualityControl
            formulario.StartPosition = FormStartPosition.CenterParent
            formulario.TypeAction = 2
            formulario._openPopUpReprocessingCause = False
            formulario._OpenPopUpQuantityRemaining = True
            Dim trasparent As New FrmTransparent(formulario, False)
            Me.Cursor = Cursors.Default
            If trasparent.ShowDialog(Me) = DialogResult.OK Then
                ProcessDropRemaining(formulario)
                RefreshDropDataSource()
            End If
        End Using
    End Sub

    Private Function ProcessDropRemaining(formulario As PopUpActionQualityControl) As List(Of QuantityRemaining)
        Dim itemsToDeleted As New List(Of QuantityRemaining)
        Dim items = SelectedItems
        For Each itemSelected In items
            Mensaje(EeventViewerImages.Informacion) = $"Se genera salida del remanente del medicamento {itemSelected.ProductFullName}"
            itemSelected.Status = 2
            itemSelected.CauseReprocessingRejectionId = formulario.causeRejectionId
            itemSelected.Observations = formulario.observations
            itemsToDeleted.Add(itemSelected)
        Next
        Dim args As New GetQuantityRemainingEventArgs()
        args.ListItemDetails = itemsToDeleted
        RaiseEvent GetItemsResult(Nothing, args, True, FromCampaignDetailId)
    End Function

    Private Sub RefreshDropDataSource()
        _dataSource = _dataSource.FindAll(Function(x) x.Status = 1 AndAlso x.CampaignDetailId <> FromCampaignDetailId)
        INDGcQuantityRemaining.DataSource = _dataSource
        RefreshDatasource()
    End Sub
#End Region

#Region "KeyDown"
    Private Sub PopupCUM_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub
#End Region

#Region "EditValueChanging"
    ''' <summary>
    ''' accion que valida la cantidad registrada como sobrante
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRptSeQuantity_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRptSeQuantity.EditValueChanging
        Dim physicalInventoryTmp = INDGvQuantityRemaining.GetFocusedObject(Of QuantityRemaining)
        Dim NewValue As Decimal = e.NewValue
        If NewValue > physicalInventoryTmp.DeliveredQuantity Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format("La cantidad entregada a la campaña del producto {0} es menor a la cantidad a Poner como sobrante ", physicalInventoryTmp.ProductFullName)
            e.Cancel = True
            Exit Sub
        End If

        physicalInventoryTmp.Quantity = e.NewValue
    End Sub

    ''' <summary>
    ''' accion que valida la cantidad registrada como aprovechamiento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSEQuantityHarnessed_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDSEQuantityHarnessed.EditValueChanging
        Dim physicalInventoryTmp = INDGvQuantityRemaining.GetFocusedObject(Of QuantityRemaining)
        Dim NewValue As Decimal = e.NewValue

        If NewValue > physicalInventoryTmp.Balance Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format("La cantidad Disponible del producto {0} es superior a la cantidad digitada ", physicalInventoryTmp.ProductFullName)
            e.Cancel = True
            Exit Sub
        End If

        physicalInventoryTmp.QuantityHarnessed = e.NewValue
    End Sub

    Private Sub INDRptSeRemnantVolume_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRptSeRemnantVolume.EditValueChanging
        Dim ProductTmp = INDGvQuantityRemaining.GetFocusedObject(Of QuantityRemaining)
        Dim NewValue As Decimal = Convert.ToDecimal(e.NewValue, Globalization.CultureInfo.InvariantCulture)

        If ProductTmp.Concentration <= 0 Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format("La concentración registrada para el medicamento es 0, lo que impide el cálculo de la cantidad real.")
            e.Cancel = True
            Exit Sub
        End If

        If NewValue > 0 Then
            If ProductTmp.MaximumVolume < NewValue Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format("El volumen no puede ser mayor a {0}", ProductTmp.MaximumVolume)
                e.Cancel = True
                Exit Sub
            End If

            Dim quantity As Decimal
            Dim objProduct As New PropertysProducts
            objProduct.Concentration = ProductTmp.Concentration
            objProduct.InputVolume = NewValue

            Dim resCalculation = CalculationRemainingFormulationType(objProduct, objCalculationRemaning)
            If resCalculation IsNot Nothing Then
                If resCalculation.Quantity > 0 Then
                    quantity = resCalculation.Quantity
                End If
                If resCalculation.Concentration > 0 Then
                    ProductTmp.Concentration = resCalculation.Concentration
                End If
                If resCalculation.ConcentrationWithMeasurementUnit IsNot Nothing Then
                    ProductTmp.ConcentrationWithMeasurementUnit = resCalculation.ConcentrationWithMeasurementUnit
                End If
            End If

            If quantity > RawMaterialQuantityBalance Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format("El volumen del remanente digitado genera una cantidad real superior a la cantidad visualizada como saldo campaña")
                e.Cancel = True
                Exit Sub
            End If

            If quantity > ProductTmp.DeliveredQuantity Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format("El volumen del remanente digitado genera una cantidad real superior a la cantidad entregada del medicamento")
                e.Cancel = True
                Exit Sub
            End If

            ProductTmp.RemnantVolume = NewValue
            ProductTmp.Quantity = quantity
        End If

        INDGcQuantityRemaining.RefreshDataSource()
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDRptGlePreparationType_EditValueChanged(sender As Object, e As EventArgs) Handles INDRptGlePreparationType.EditValueChanged
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = Me.INDGcQuantityRemaining.FocusedView
        view.CloseEditor()
        Dim row = CType(view.GetRow(view.FocusedRowHandle), QuantityRemaining)
        Dim lookup = CType(sender, DevExpress.XtraEditors.GridLookUpEdit)
        Dim data = _presenter.ListViewPackageConcentrationByPreparationType(row.CampaignDetailId, lookup.EditValue, row.ProductId)
        If data Is Nothing Then
            lookup.EditValue = 0
            Mensaje(EeventViewerImages.Advertencia) = String.Format("No existe información para la campaña {0}", row.CampaignDetailNumber)
            Exit Sub
        End If

        Dim objProduct As New PropertysProducts
        Dim ConcentrationWithMeasurementUnit As String
        Dim MeasurementUnitVolumeId As Integer
        Dim MeasurementUnitMainMedicineId As Integer
        Dim Concentration As Decimal

        objProduct.Concentration = data.Concentration
        Dim resCalculation = CalculationRemainingFormulationType(objProduct, objCalculationRemaning)
        If resCalculation.Concentration > 0 Then
            Concentration = resCalculation.Concentration
        Else
            Concentration = data.Concentration
        End If
        If resCalculation.ConcentrationWithMeasurementUnit IsNot Nothing Then
            ConcentrationWithMeasurementUnit = resCalculation.ConcentrationWithMeasurementUnit
        Else
            ConcentrationWithMeasurementUnit = String.Format("{0} {1}/{2}", Format(Concentration, "0.00"), data.MeasurementUnitMainMedicine, data.MeasurementUnitVolume)
        End If
        If resCalculation.ObjATC IsNot Nothing And resCalculation.ObjATC.ATCId.WeightMeasureUnit IsNot Nothing And resCalculation.ObjATC.ATCId.VolumeMeasureUnit IsNot Nothing Then
            MeasurementUnitVolumeId = resCalculation.ObjATC.ATCId.VolumeMeasureUnit.Id
            MeasurementUnitMainMedicineId = resCalculation.ObjATC.ATCId.WeightMeasureUnit.Id
        Else
            MeasurementUnitVolumeId = data.MeasurementUnitVolumeId
            MeasurementUnitMainMedicineId = data.MeasurementUnitMainMedicineId
        End If

        Dim validationStabilityByProduct = ValidateStability(row.ProductId)

        If Not validationStabilityByProduct.IsValid Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(validationStabilityByProduct.Message)
        End If

        Dim stabilityFound = ValidateComponentsByStability(row.PreparationType, data, stabilityByProduct)

        If Not stabilityFound.ValidPackage Then
            Mensaje(EeventViewerImages.Advertencia) = stabilityFound.Message
        End If

        row.Stability = stabilityFound.Stability
        row.UnitTimeStability = 1
        row.StabilityWithTimeUnit = String.Format("{0} HORAS", stabilityFound.Stability)

        row.Concentration = Concentration
        row.ConcentrationWithMeasurementUnit = ConcentrationWithMeasurementUnit
        row.RemnantVolume = 0
        row.UnitRemnantVolume = MeasurementUnitVolumeId
        row.UnitQuantity = MeasurementUnitMainMedicineId
        row.MaximumVolume = data.MaximumVolume

        INDGcQuantityRemaining.RefreshDataSource()
    End Sub

#End Region

#Region "RowStyle"
    ''' <summary>
    ''' acciones sobre las columnas de las rejillas
    ''' </summary>
    Private Sub ActionColumn()
        INDGvQuantityRemaining.BeginUpdate()
        If Not QRemainingOrHarnessed Then
            INDColPreparationType.HideColumn(-1)
            INDColStability.HideColumn(-1)
            INDColQuantityAvailable.HideColumn(-1)
            INDColQuantityHarnessed.HideColumn(-1)
            INDColCampaignDetail.HideColumn(-1)
            INDColQuantity.HideColumn(-1)

            INDColProduct.ShowColumn(0)
            INDColBatchSerialCode.ShowColumn(1)
            INDColOpeningDate.ShowColumn(2)
            INDColDateStability.ShowColumn(3)
            INDColExpirationDate.ShowColumn(4)
            INDColRemnantVolume.ShowColumn(5)
            INDColQuantityHarnessed.ShowColumn(6)
            INDColConcentration.ShowColumn(7)

        Else
            INDColProduct.HideColumn(-1)
            INDColBatchSerialCode.HideColumn(-1)
            INDColExpirationDate.HideColumn(-1)
            INDColCampaignDetail.HideColumn(-1)
            INDColQuantityAvailable.HideColumn(-1)
            INDColQuantityHarnessed.HideColumn(-1)
            INDColOpeningDate.HideColumn(-1)
            INDColDateStability.HideColumn(-1)

            INDColBatchSerialCode.ShowColumn(0)
            INDColPreparationType.ShowColumn(1)
            INDColStability.ShowColumn(2)
            INDColRemnantVolume.ShowColumn(3)
            INDColQuantity.ShowColumn(4)
            INDColConcentration.ShowColumn(5)

            INDLciOpeningDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
        INDGvQuantityRemaining.EndUpdate()
    End Sub

    ''' <summary>
    ''' Muestra el icono de cargando de la rejilla
    ''' </summary>
    Public Sub ShowLoadingGrid()
        INDGvQuantityRemaining.ShowLoadingPanel()
    End Sub

    ''' <summary>
    ''' Oculta el cargando de la rejilla
    ''' </summary>
    Public Sub HideLoadingGrid()
        INDGvQuantityRemaining.HideLoadingPanel()
    End Sub

#End Region

#Region "Methods"

#Region "Validations"

    ''' <summary>
    '''  Guarda lista con los detalles de la estabilidad por producto
    ''' </summary>
    Dim stabilityByProduct As List(Of ViewStabilityTableDetailXpo)

    ''' <summary>
    '''  Clase que identifica si el producto tiene estabilidad o no
    ''' </summary>
    Public Class ValidationResultStability
        Public Property IsValid As Boolean
        Public Property Message As String

        Public Sub New(isValid As Boolean, Optional message As String = "")
            Me.IsValid = isValid
            Me.Message = message
        End Sub
    End Class

    ''' <summary>
    '''  Clase que identifica si los componentes del paquete son validos
    '''  Segùn la tabla de estabilidad
    ''' </summary>
    Public Class ComponentValidation
        Public Property Stability As Integer
        Public Property ValidPackage As Boolean
        Public Property Message As String

        Public Sub New(stability As Integer, validPackage As Boolean, Optional message As String = "")
            Me.Stability = stability
            Me.ValidPackage = validPackage
            Me.Message = message
        End Sub
    End Class

    ''' <summary>
    ''' Método que valida que el producto tenga estabilidad
    ''' </summary>
    Private Function ValidateStability(ProductId As Integer) As ValidationResultStability
        stabilityByProduct = _presenter.GetStabilityDetailByProduct(ProductId)

        If Not stabilityByProduct.Any() Then
            Dim product = _presenter.ListProductsATC(ProductId)
            If product Is Nothing Then
                Return New ValidationResultStability(False, "El producto no existe")
            End If

            If product.ATCId Is Nothing Then
                Return New ValidationResultStability(False, $"El producto {product.CodeName} no tiene asociado un medicamento")
            End If

            Dim stability = _presenter.GetStabilityDetailByATC(product.ATCId.Id)
            If Not stability.Any() Then
                Return New ValidationResultStability(False, $"El medicamento {product.ATCId.CodeName} no tiene parametrizada tabla de estabilidad")
            End If

            Return New ValidationResultStability(False, $"El producto {product.CodeName} no tiene parametrizada tabla de estabilidad al medicamento {product.ATCId.CodeName}")
        End If

        Return New ValidationResultStability(True) ' Retorna un resultado válido sin mensaje.
    End Function

    ''' <summary>
    ''' Método que establece la estabilidad del producto
    ''' dependiendo el tipo de preparación
    ''' </summary>
    Private Function ValidateComponentsByStability(preparationType As Integer, data As ViewPackageConcentrationByPreparationTypeXpo, listStability As List(Of ViewStabilityTableDetailXpo)) As ComponentValidation
        If listStability Is Nothing OrElse Not listStability.Any() OrElse data Is Nothing Then
            Return New ComponentValidation(0, False, $"Estabilidad no encontrada para el producto seleccionado")
        End If

        For Each item In listStability
            Select Case preparationType
                Case ePreparationType.Reconstitution
                    If item.AtcIdReconstituent IsNot Nothing AndAlso item.AtcIdReconstituent = data.AtcIdReconstituent Then
                        Return New ComponentValidation(item.StabilityReconstituent, True)
                    End If

                Case ePreparationType.ReconstitutionDilution
                    If item.AtcIdVehicle IsNot Nothing AndAlso item.AtcIdVehicle = data.AtcIdVehicle Then
                        Return New ComponentValidation(item.StabilityVehicle, True)
                    End If

                Case ePreparationType.Dilution
                    Return New ComponentValidation(item.StabilityMainMedicine, True)
            End Select
        Next

        Dim atcIdComponent As Integer
        Dim component As String

        Select Case preparationType
            Case ePreparationType.Reconstitution
                atcIdComponent = data.AtcIdReconstituent
                component = "Reconstituyente"
            Case ePreparationType.ReconstitutionDilution
                atcIdComponent = data.AtcIdVehicle
                component = "Diluyente"
            Case Else
                Return New ComponentValidation(0, False, "Tipo de preparación no válido.")
        End Select

        Dim atcComponent As ATCXpo = _presenter.GetAtcById(atcIdComponent)
        Dim product = _presenter.ListProductsATC(data.ProductId)

        If atcComponent Is Nothing Then
            Return New ComponentValidation(0, False, "Atc no encontrado")
        End If

        Return New ComponentValidation(0, False, $"El {component} {atcComponent.CodeName} no está asociado a la tabla de estabilidad del producto {product.CodeName}")
    End Function

#End Region

    ''' <summary>
    ''' Retorno del modal
    ''' </summary>
    Private Sub RefreshDatasource()
        INDGvQuantityRemaining.RefreshData()
    End Sub

    Private Sub LoadPreparationType()
        INDRptGlePreparationType.DataSource = {
            New Tuple(Of Byte, String)(1, "Reconstituido"),
            New Tuple(Of Byte, String)(2, "Reconstituido - Diluido"),
            New Tuple(Of Byte, String)(3, "Presentación original")
        }.ToList()
    End Sub

    Private Function CalculateDateStability(QuantityRemaining As QuantityRemaining)
        Dim VctoStability As Date
        Dim Horas, Dias As Integer

        Dim data = _presenter.ListViewQuantityRemaining(QuantityRemaining.CampaignDetailId, QuantityRemaining.ProductId, QuantityRemaining.BatchSerialId)
        If data IsNot Nothing Then

            If data.UnitTimeStability Then
                Horas = data.Stability
                VctoStability = QuantityRemaining.OpeningDate.Date.AddHours(Horas)
            Else
                Dias = data.Stability
                VctoStability = QuantityRemaining.OpeningDate.Date.AddDays(Dias)
            End If
        End If

        Return VctoStability
    End Function

    Private Sub IndigoGridView1_QueryPopUpActionButtons(sender As Object, e As QueryPopUpActionButtonsEventArgs) Handles IndigoGridView1.QueryPopUpActionButtons
        e.Buttons.ToList().ForEach(Sub(i) i.Enabled = False)
        Dim items = SelectedItems
        If SelectedItems IsNot Nothing AndAlso SelectedItems.Count > 0 Then

            Dim buttonDropRemanent = e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.DropRemanent)))
            If buttonDropRemanent IsNot Nothing Then
                buttonDropRemanent.Enabled = True
            End If

            If items.All(Function(item) Date.Compare(OpeningDate, item.VctoStability) < 1) Then
                Dim buttonUseToCampaing = e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.UseToCampaing)))
                If buttonUseToCampaing IsNot Nothing Then
                    buttonUseToCampaing.Enabled = True
                End If
            End If
        Else
            Mensaje(EeventViewerImages.Informacion) = $"Seleccione al menos un producto para habilitar el menú"
        End If

    End Sub

    Private Sub INDGvQuantityRemaining_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDGvQuantityRemaining.PopupMenuShowing
        If Not Remanent Then
            If e.HitInfo.InRow Then
                If Not INDGvQuantityRemaining.GetSelectedRows().Contains(e.HitInfo.RowHandle) Then
                    IndigoGridView1.BarManagerActions.Items.ToList().ForEach(Sub(i) i.Visibility = DevExpress.XtraBars.BarItemVisibility.Never)
                    Return
                End If

                Dim items = SelectedItems
                If SelectedItems IsNot Nothing AndAlso SelectedItems.Count > 0 Then
                    Dim dropRemaining = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.DropRemanent)))
                    Dim useToCampaing = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.UseToCampaing)))
                    dropRemaining.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    If items.All(Function(item) Date.Compare(OpeningDate, item.VctoStability) < 1) Then
                        IndigoGridView1.BarManagerActions.Items.ToList().ForEach(Sub(i) i.Visibility = DevExpress.XtraBars.BarItemVisibility.Never)
                        useToCampaing.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    Else
                        useToCampaing.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        Select Case sender.Tag
            Case "UseToCampaing"
                UseToCampaing()
            Case "DropRemanent"
                DropRemanent()
        End Select
    End Sub

    ''' <summary>
    ''' Retorna los items seleccionados
    ''' </summary>
    Private ReadOnly Property SelectedItems() As List(Of QuantityRemaining)
        Get
            Return (From x In INDGvQuantityRemaining.GetSelectedRows()
                    Where Not INDGvQuantityRemaining.IsGroupRow(x)
                    Select DirectCast(INDGvQuantityRemaining.GetRow(x), QuantityRemaining)).ToList()
        End Get
    End Property

    ''' <summary>
    '''  Clase que proporciona información sobre la cantidad restante del medicamento,
    '''  así como su concentración y unidades de medida.
    ''' </summary>
    Public Class CalculationRemaning
        Public Property ObjATC As Object
        Public Property Concentration As Decimal
        Public Property ConcentrationFormat As String
        Public Property ConcentrationWithMeasurementUnit As String
        Public Property Quantity As Decimal
        Public Property AbbreviationVolumen As String
        Public Property AbbreviationWeight As String
    End Class

    ''' <summary>
    ''' Clase que proporciona información sobre el producto, incluida su concentración y volumen inicial.
    ''' </summary>
    Public Class PropertysProducts
        Public Property objProduct As Object
        Public Property Concentration As Decimal
        Public Property InputVolume As Decimal
    End Class

    ''' <summary>
    ''' Instancia de la clase
    ''' </summary>
    Dim objCalculationRemaning As New CalculationRemaning


    ''' <summary>
    ''' Calcula la cantidad restante de un medicamento en función del volumen de entrada,la concentración y el tipo de formulación.
    ''' También formatea los valores de concentración y cantidad y establece la abreviatura adecuada para las unidades de medida de volumen y peso.
    ''' </summary>
    Public Function CalculationRemainingFormulationType(objProduct As PropertysProducts, objRemainingCalculation As CalculationRemaning) As CalculationRemaning
        Dim dataProduct As Object
        Dim InputVolume = objProduct.InputVolume
        Dim Concentration = objProduct.Concentration

        If objRemainingCalculation IsNot Nothing Then
            dataProduct = objRemainingCalculation.ObjATC
        Else
            dataProduct = objProduct.objProduct
        End If
        If dataProduct.ATCId IsNot Nothing AndAlso dataProduct.ATCId.FormulationType = 3 Then
            objCalculationRemaning.Concentration = (dataProduct.ATCId.Weight / dataProduct.ATCId.Volume) ''Concentración = Peso del medicamento/Volumen del medicamento
            objCalculationRemaning.ConcentrationFormat = Format(objCalculationRemaning.Concentration, "0.00")
            objCalculationRemaning.ConcentrationWithMeasurementUnit = String.Format("{0} {1}/{2}", objCalculationRemaning.ConcentrationFormat, dataProduct.ATCId.WeightMeasureUnit.Abbreviation, dataProduct.ATCId.VolumeMeasureUnit.Abbreviation)
            objCalculationRemaning.Quantity = (InputVolume * objCalculationRemaning.Concentration) ''Cantidad real = Volumen remanente real (mL) X Concentración
        Else ''Otro tipo de unidad de volumen
            objCalculationRemaning.Concentration = Concentration
            objCalculationRemaning.ConcentrationFormat = Format(Concentration, "0.00")
            If dataProduct.ATCId.WeightMeasureUnit IsNot Nothing And dataProduct.ATCId.VolumeMeasureUnit IsNot Nothing Then
                objCalculationRemaning.ConcentrationWithMeasurementUnit = String.Format("{0} {1}/{2}", objCalculationRemaning.ConcentrationFormat, dataProduct.ATCId.WeightMeasureUnit.Abbreviation, dataProduct.ATCId.VolumeMeasureUnit.Abbreviation)
            End If
            If Concentration = 0 Then
                objCalculationRemaning.Quantity = 0
            Else
                objCalculationRemaning.Quantity = InputVolume / Concentration
            End If
        End If
        If dataProduct.ATCId.VolumeMeasureUnit IsNot Nothing Then
            objCalculationRemaning.AbbreviationVolumen = dataProduct.ATCId.VolumeMeasureUnit.Abbreviation.ToString()
        End If
        If dataProduct.ATCId.WeightMeasureUnit IsNot Nothing Then
            objCalculationRemaning.AbbreviationWeight = dataProduct.ATCId.WeightMeasureUnit.Abbreviation.ToString()
        End If
        objCalculationRemaning.ObjATC = dataProduct
        Return objCalculationRemaning
    End Function

#End Region

#End Region

End Class

''' <summary>
''' clase para retornar en el evento de seleccionar productos
''' </summary>
''' <remarks></remarks>
Public Class GetQuantityRemainingEventArgs
    Inherits EventArgs

    Property ListItemDetails As List(Of QuantityRemaining)

End Class