'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 19-01-2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.FixedAsset.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Domain.Entities
Imports Domain.Base.Entities
Imports DevExpress.Xpo
Imports System.Drawing
Imports Presentation.Maintenance
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Presentation.Base.BaseClass
Imports System.Text

#End Region

Public Class FrmFixedAssetRemissionEntrance
    Implements IEquipmentEntry, ICustomizableForm

#Region "BUILDER"
    Public Sub New()
        InitializeComponent()
        'ctrTmp = New CtrContractTotalInfo()
        'ctrTmp.SetInfoFunction(AddressOf getInfoRemissionEntrance)
        'ctrTmp.PrintInfo()
        'ctrTmp.TextDiscountValue = "NETO:"
        'ctrTmp.TextIvaValue = "IVA:"
        'ctrTmp.MaskTotalValue = "c0"
        'ctrTmp.Dock = System.Windows.Forms.DockStyle.Fill
        'AdditionalControlPanel.Controls.Add(ctrTmp)
    End Sub
#End Region

#Region "GLOBALS"
    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Dim indexEditRecord As Integer
    ''' <summary>
    ''' valor total del iva
    ''' </summary>
    ''' <remarks></remarks>
    Dim _ivaValue As Decimal = 0
    ''' <summary>
    ''' valor total neto 
    ''' </summary>
    ''' <remarks></remarks>
    Dim _value As Decimal = 0
    ''' <summary>
    ''' valor total
    ''' </summary>
    ''' <remarks></remarks>
    Dim _totalValue As Decimal = 0
    ' ''' <summary>
    ' ''' Control para establecer informacion del ingreso
    ' ''' </summary>
    'Private ctrTmp As CtrContractTotalInfo
    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "FixedAsset"

    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Private _indigoSession As SessionValues
    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.FixedAssetSequence
    ''' <summary>
    ''' Prefijo seleccionado
    ''' </summary>
    Private _prefixSelected As String
    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32
    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordFixedAsset
    ' ''' <summary>
    ' ''' presenter de remision de entrada
    ' ''' </summary>
    ' ''' <remarks></remarks>
    Dim presenter As PEquipmentEntry


    ''' <summary>
    ''' id del proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Dim supplierId As Integer
    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer

    ''' <summary>
    ''' flag para el proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Dim flagLoad As Boolean = False


    Dim listInputRemissionEquipmentValidationDelete As List(Of FixedAssetRemissionEntranceItem)

    Dim inputRemissionEquipment As FixedAssetRemissionEntranceItem

    ''' <summary>
    ''' entidad de remision de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Dim InputRemission As FixedAssetRemissionEntrance

    ''' <summary>
    ''' listado del detalla de la remision para validar que los productos no se pepitan con la misma fuente
    ''' </summary>
    ''' <remarks></remarks>
    Dim listInputRemissionEquipment As List(Of FixedAssetRemissionEntranceItem)

#End Region

#Region "PROPERTIES"
    ''' <summary>
    ''' codigo de la remision
    ''' </summary>
    Public Property Code As String Implements IEquipmentEntry.Code
        Get
            If INDBteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDBteCode.Text
            End If
        End Get
        Set(value As String)
            INDBteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' detalle de la remision
    ''' </summary>
    Public Property Description As String Implements IEquipmentEntry.Description
        Get
            Return INDMeDetail.Text
        End Get
        Set(value As String)
            INDMeDetail.Text = value
        End Set
    End Property

    ''' <summary>
    ''' fecha de la remision
    ''' </summary>
    Public Property RemissionDate As Date? Implements IEquipmentEntry.RemissionDate
        Get
            Return INDDteDate.EditValue
        End Get
        Set(value As Date?)
            INDDteDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' numero de la remision
    ''' </summary>
    Public Property RemissionNumber As String Implements IEquipmentEntry.RemissionNumber
        Get
            Return INDTxtReferalNumber.Text
        End Get
        Set(value As String)
            INDTxtReferalNumber.Text = value
        End Set
    End Property

    ''' <summary>
    ''' id del proveedor
    ''' </summary>
    Public Property SupplierDistributionLineId As Integer? Implements IEquipmentEntry.SupplierDistributionLineId
        Get
            Return INDSleSupplierDistributionLine.EditValue
        End Get
        Set(value As Integer?)
            INDSleSupplierDistributionLine.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' id del almacen
    ''' </summary>
    Public Property AdquisitionTypeId As Integer? Implements IEquipmentEntry.AdquisitionTypeId
        Get
            Return Integer.Parse(INDSleAdquisitionType.EditValue)
        End Get
        Set(value As Integer?)
            INDSleAdquisitionType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' datasource de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SupplierXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSleSupplierDistributionLine.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleSupplierDistributionLine.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' datasource de Responsable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ResponsibleXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSlResponsible.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlResponsible.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' datasource de Locación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LocationXPO As XPCollection
        Get
            Return CType(INDtreeLocation.Properties.DataSource, XPCollection)
        End Get
        Set(value As XPCollection)
            INDtreeLocation.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' id de la Ubicación
    ''' </summary>
    Public Property LocationId As Integer? Implements IEquipmentEntry.LocationId
        Get
            Return INDtreeLocation.EditValue
        End Get
        Set(value As Integer?)
            INDtreeLocation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id del Responsable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ResponsibleId As Integer? Implements IEquipmentEntry.ResponsibleId
        Get
            Return INDSlResponsible.EditValue
        End Get
        Set(value As Integer?)
            INDSlResponsible.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id del Responsable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GetLocationResponsible As Byte Implements IEquipmentEntry.GetLocationResponsible
        Get
            Return INDSlLocationResponsible.EditValue
        End Get
        Set(value As Byte)
            INDSlLocationResponsible.EditValue = value
        End Set
    End Property



    Public WriteOnly Property ActionsOnControls As Boolean Implements IEquipmentEntry.ActionsOnControls
        Set(value As Boolean)
            INDlcReferralEntry.BeginUpdate()
            INDBteCode.Enabled = Not value
            INDDteDate.Enabled = value
            INDSleSupplierDistributionLine.Enabled = value
            INDTxtReferalNumber.Enabled = value
            INDSleAdquisitionType.Enabled = value
            INDMeDetail.Enabled = value
            INDBtnAdd.Enabled = value
            INDGcReferralEntry.Enabled = value
            INDSlLocationResponsible.Enabled = value
            INDtreeLocation.Enabled = value
            INDSlResponsible.Enabled = value
            INDlcReferralEntry.EndUpdate()
            If value = False Then
                INDBteCode.Focus()
            Else
                INDSleSupplierDistributionLine.Focus()
            End If

        End Set
    End Property

    Public ReadOnly Property MyLayoutControl As Controls.IndigoLayoutControl Implements IEquipmentEntry.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public ReadOnly Property MyTag As Object Implements IEquipmentEntry.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public Property Sequense As Domain.Entities.FixedAssetSequence Implements IEquipmentEntry.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As Domain.Entities.FixedAssetSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As FixedAssetSequenceDetail In Me._sequence.FixedAssetSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    Dim _datasourceacquisition As List(Of Tuple(Of Integer, String))
    ReadOnly Property DatasourceAcquisition As List(Of Tuple(Of Integer, String))
        Get
            If _datasourceacquisition Is Nothing Then
                _datasourceacquisition = New List(Of Tuple(Of Integer, String))
                _datasourceacquisition.Add(New Tuple(Of Integer, String)(1, "Compra Directa"))
                _datasourceacquisition.Add(New Tuple(Of Integer, String)(3, "Comodato"))
                _datasourceacquisition.Add(New Tuple(Of Integer, String)(8, "Comodato Tercerizado"))
                _datasourceacquisition.Add(New Tuple(Of Integer, String)(4, "Donación"))
                _datasourceacquisition.Add(New Tuple(Of Integer, String)(5, "Traspaso de Bienes"))
                _datasourceacquisition.Add(New Tuple(Of Integer, String)(6, "Otro Concepto"))
                _datasourceacquisition.Add(New Tuple(Of Integer, String)(7, "Leasing Financiero"))
                '_datasourceacquisition.Add(New Tuple(Of Integer, String)(9, "Renting Financiero"))
                _datasourceacquisition.Add(New Tuple(Of Integer, String)(10, "Renting Operativo"))
            End If
            Return _datasourceacquisition
        End Get
    End Property

    Dim _datasourceLocationResponsibleOpc As List(Of Tuple(Of Integer, String))
    ReadOnly Property DatasourceLocationResponsibleOpc As List(Of Tuple(Of Integer, String))
        Get
            If _datasourceLocationResponsibleOpc Is Nothing Then
                _datasourceLocationResponsibleOpc = New List(Of Tuple(Of Integer, String))
                _datasourceLocationResponsibleOpc.Add(New Tuple(Of Integer, String)(1, "Responsable y Ubicación General"))
                _datasourceLocationResponsibleOpc.Add(New Tuple(Of Integer, String)(2, "Responsable y Ubicación Específico"))
            End If
            Return _datasourceLocationResponsibleOpc
        End Get
    End Property

#End Region

#Region "CRUD"
    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        OpenSearch()
    End Sub

    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControls()
    End Sub

    Public Sub Eliminar() Implements Base.IcrudBase.Eliminar

    End Sub

    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        If InputRemission IsNot Nothing AndAlso InputRemission.Status < 3 Then
			If ValidateControls() = True Then
				If INDGvReferralEntry.RowCount = 0 Then
					Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar al menos un (1) equipo"
					Exit Sub
				End If
			Else
				Exit Sub
			End If
			If INDLciLocation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
				If ResponsibleId Is Nothing Or LocationId Is Nothing Then
					Mensaje(EeventViewerImages.Advertencia) = "La ubicación y/o responsable no pueden estar vacíos"
					Exit Sub
				End If
			Else
				If listInputRemissionEquipment IsNot Nothing AndAlso
					   listInputRemissionEquipment.Any(Function(entryItem) _
						   entryItem.FixedAssetRemissionEntranceItemDetail IsNot Nothing AndAlso
						   entryItem.FixedAssetRemissionEntranceItemDetail.Any(Function(itemDetail) _
							   itemDetail.ResponsibleId = 0 OrElse itemDetail.LocationId = 0)) Then

					Mensaje(EeventViewerImages.Advertencia) = "Existen detalles sin especificar ubicación y/o responsable"
					Exit Sub
				End If

			End If
			AssigningValues()
        End If
        Try
            Using model As New MEquipmentEntry(MyTag)
                AsyncLoader(True)
                Dim result = Await model.SaveInputRemission(InputRemission, _idCurrentSequence, Me._sequence)
                If result.StateResult = True Then
                    If InputRemission.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(_idCurrentSequence).RemoveAt(0)
                        End If
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), result.ObjectEmbbeded.Code)
                    Else
                        If InputRemission.Status = 3 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                    End If
                    Me.InputRemission = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, InputRemission.Id, 0, InputRemission.Id, _idOperativeUnit)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, InputRemission.Id, 0, InputRemission.Id, _idOperativeUnit)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, InputRemission.Id, 0, InputRemission.Id, _idOperativeUnit)
                        Case 4
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, InputRemission.Id, 0, InputRemission.Id, _idOperativeUnit)
                    End Select

                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                    If result.StatusCode = eStatusResult.EXCEPTION Then
                        Mensaje(EeventViewerImages.MensajeError) = result.Message
                    ElseIf result.StatusCode = eStatusResult.WARNING Then
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

    End Sub

    Public Async Sub Nuevo() Implements Base.IcrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewEquipmentEntry()
        End If
    End Sub

    ''' <summary>
    ''' metodo para guardar y confirmar o para actualizar y confirmar
    ''' </summary>
    ''' <param name="action"></param>
    ''' <remarks></remarks>
    Private Async Sub SaveOrUpdateAndConfirm(action As Integer)
        If InputRemission IsNot Nothing AndAlso InputRemission.Status < 3 Then
            If ValidateControls() = True Then
                If INDGvReferralEntry.RowCount = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar al menos un (1) equipo"
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
            AssigningValues()
        End If
        Try
            Using model As New MEquipmentEntry(MyTag)
                AsyncLoader(True)
                InputRemission.Status = action
                Dim result = Await model.SaveInputRemission(InputRemission, _idCurrentSequence, Me._sequence)
                If result.StateResult = True Then
                    If InputRemission.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), result.ObjectEmbbeded.Code)
                    Else
                        If InputRemission.Status = 3 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                    End If
                    Me.InputRemission = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, InputRemission.Id, 0, InputRemission.Id, _idOperativeUnit)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, InputRemission.Id, 0, InputRemission.Id, _idOperativeUnit)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, InputRemission.Id, 0, InputRemission.Id, _idOperativeUnit)
                        Case 4
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, InputRemission.Id, 0, InputRemission.Id, _idOperativeUnit)
                    End Select

                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                    If result.StateResult = False And result.StateResultAux = False Then
                        Mensaje(EeventViewerImages.MensajeError) = result.Message
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                    If InputRemission.Id > 0 Then
                        InputRemission = Await model.GetInputRemissionAsync(Code)
                    Else
                        InputRemission = New FixedAssetRemissionEntrance
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try

    End Sub
#End Region

#Region "METHODS"

    ''' <summary>
    ''' metodo para obtener lo que se retorna del formulario modal de producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnAddRemissionEntranceDetail(sender As Object, e As AddEquipmentRemissionEventArgs)
        If listInputRemissionEquipment Is Nothing Then
            listInputRemissionEquipment = New List(Of FixedAssetRemissionEntranceItem)
        End If

        If e.EditMode = True Then
            listInputRemissionEquipment.Remove(inputRemissionEquipment)
            listInputRemissionEquipment.Insert(indexEditRecord, e.InputRemissionEquipment)
        Else
            listInputRemissionEquipment.Add(e.InputRemissionEquipment)
        End If

        INDGcReferralEntry.DataSource = Nothing
        INDGcReferralEntry.DataSource = listInputRemissionEquipment
    End Sub

    ''' <summary>
    ''' Método que retorna los registros importados al formulario de agregar productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnGetListEntranceVoucherDetail(sender As Object, e As GetListImputRemissionEquipmentEventArgs)


        For Each item In e.ListTrackInputRemissionEquipment
            inputRemissionEquipment = New FixedAssetRemissionEntranceItem()
            inputRemissionEquipment.RemissionSource = item.RemissionSource
            inputRemissionEquipment.SourceCode = item.SourceCode
            inputRemissionEquipment.PurchaseOrderItemId = item.PurchaseOrderItemId
            'inputRemissionEquipment.inventory = item.IdInventoryType
            'inputRemissionEquipment.NameInventoryType = item.NameInventoryType
            inputRemissionEquipment.ItemId = item.ItemId
            'inputRemissionEquipment.NameEquipment = item.NameEquipment
            'inputRemissionEquipment.IdEquipmentType = item.IdEquipmentType
            'inputRemissionEquipment.NameEquipmentType = item.NameEquipmentType
            inputRemissionEquipment.Quantity = item.Quantity
            inputRemissionEquipment.UnitValue = item.UnitValue
            inputRemissionEquipment.TotalValue = item.TotalValue
            inputRemissionEquipment.IvaValue = item.IvaValue
            inputRemissionEquipment.TrademarkId = item.TrademarkId
            'inputRemissionEquipment.NameTrademark = item.NameTrademark
            inputRemissionEquipment.Model = item.Model
            inputRemissionEquipment.PolicyId = item.PolicyId
            'inputRemissionEquipment.NamePoliza = item.NamePoliza
            inputRemissionEquipment.IVAId = item.IVAId
            'inputRemissionEquipment.NameIva = item.NameIva

            If item.FixedAssetRemissionEntranceItemDetail IsNot Nothing And item.FixedAssetRemissionEntranceItemDetail.Count > 0 Then
                For Each itemDetail In item.FixedAssetRemissionEntranceItemDetail
                    Dim inputRemissionEquipmentDetail = New FixedAssetRemissionEntranceItemDetail()

                    With inputRemissionEquipmentDetail
                        .AdquisitionDate = itemDetail.AdquisitionDate
                        '.ComponentDepreciate = itemDetail.ComponentDepreciate
                        .Depreciate = itemDetail.Depreciate
                        '.item = itemDetail.IdEquipment
                        '.fun = itemDetail.IdFunctionalUnit
                        .NameFunctionalUnit = itemDetail.NameFunctionalUnit
                        .LocationId = itemDetail.LocationId
                        .NameLocation = itemDetail.NameLocation
                        .ResponsibleId = itemDetail.ResponsibleId
                        .NameResponsible = itemDetail.NameResponsible
                        .Plate = itemDetail.Plate
                        .Serie = itemDetail.Serie
                    End With

                    If itemDetail.FixedAssetRemissionEntranceItemDetailPart IsNot Nothing And itemDetail.FixedAssetRemissionEntranceItemDetailPart.Count > 0 Then
                        For Each itemDetailParts In itemDetail.FixedAssetRemissionEntranceItemDetailPart
                            Dim inputRemissionEquipmentParts = New FixedAssetRemissionEntranceItemDetailPart()

                            With inputRemissionEquipmentParts
                                '.item = itemDetailParts.IdEquipment
                                .PartAccesoriesConsumiblesId = itemDetailParts.PartAccesoriesConsumiblesId
                                .DepreciatePart = itemDetailParts.DepreciatePart
                                '.NamePartsAccesoriesConsumibles = itemDetailParts.NamePartsAccesoriesConsumibles
                                .Value = itemDetailParts.Value

                            End With

                            inputRemissionEquipmentDetail.FixedAssetRemissionEntranceItemDetailPart.Add(inputRemissionEquipmentParts)
                        Next
                    End If




                    inputRemissionEquipment.FixedAssetRemissionEntranceItemDetail.Add(inputRemissionEquipmentDetail)




                Next
            End If

            'If item.FixedAssetPartAccesoriesConsumiblesInputRemission IsNot Nothing And item.FixedAssetPartAccesoriesConsumiblesInputRemission.Count > 0 Then
            '    For Each itemDetailParts In item.FixedAssetPartAccesoriesConsumiblesInputRemission
            '        Dim inputRemissionEquipmentParts = New FixedAssetPartAccesoriesConsumiblesInputRemission()

            '        With inputRemissionEquipmentParts
            '            .IdEquipment = itemDetailParts.IdEquipment
            '            .IdPartAccesoriesConsumibles = itemDetailParts.IdPartAccesoriesConsumibles
            '            .DepreciatePart = itemDetailParts.DepreciatePart
            '            .NamePartsAccesoriesConsumibles = itemDetailParts.NamePartsAccesoriesConsumibles
            '            .Value = itemDetailParts.Value

            '        End With

            '        inputRemissionEquipment.FixedAssetPartAccesoriesConsumiblesInputRemission.Add(inputRemissionEquipmentParts)
            '    Next
            'End If


            listInputRemissionEquipment.Add(inputRemissionEquipment)
        Next



        'Dim errors As New StringBuilder
        'Dim listEntranceVoucherDetailHandlesBatch As New List(Of EntranceVoucherDetail)
        'Dim listEntranceVoucherDetailNotHandlesBatch As New List(Of EntranceVoucherDetail)
        'Dim supplierTmp As New Domain.Maintenance.Entities.Supplier
        'Dim dictionaryRetentionConcept As Dictionary(Of Integer, Integer) = New Dictionary(Of Integer, Integer)()
        'Dim dictionaryProduct As Dictionary(Of Integer, Domain.Entities.InventoryProduct) = New Dictionary(Of Integer, InventoryProduct)()
        'Using modelSupplier As New MSupplier(Me.Tag)
        '    supplierTmp = modelSupplier.GetSupplierById(_supplierId)
        'End Using
        'For Each Item In e.ListEntranceVoucherDetail
        '    Dim product As New InventoryProduct
        '    If Not dictionaryProduct.ContainsKey(Item.ProductId) Then
        '        Using model As New MInventoryProduct(Me.Tag)
        '            Dim productTmp = model.GetInventoryProductByIdSimpleToGroup(Item.ProductId)
        '            dictionaryProduct.Add(Item.ProductId, productTmp)
        '        End Using
        '    End If
        '    product = dictionaryProduct(Item.ProductId)
        '    If product.FinalProductCost Is Nothing OrElse product.FinalProductCost = 0 Then
        '        errors.AppendLine(String.Format(ResourceManager.GetString("FinalProductCostZero", NAME_MODULE), product.Code + " - " + product.Name))
        '        Continue For
        '    End If
        '    If product.ProductSubGroupId IsNot Nothing Then
        '        If product.ProductSubGroup.HandlesBatch Then
        '            listEntranceVoucherDetailHandlesBatch.Add(Item)
        '        Else
        '            Item.ProductCodeName = product.Code + " - " + product.Name
        '            If Item.UnitValue = 0 Then
        '                Item.UnitValue = product.FinalProductCost
        '            Else
        '                Item.UnitValue = Item.UnitValue
        '            End If
        '            If Item.DiscountPercentage = 0 Then
        '                Item.DiscountPercentage = 0
        '            Else
        '                Item.DiscountPercentage = Item.DiscountPercentage
        '            End If
        '            Item.LastValue = product.FinalProductCost
        '            Item.SubTotalValue = Utils.RoundValue(Item.UnitValue * Item.Quantity, RoundService.Value)
        '            Item.DiscountValue = Utils.RoundValue(Item.SubTotalValue * (Item.DiscountPercentage / 100), RoundService.Value)

        '            If product.IVAId IsNot Nothing Then
        '                Using modelIva As New MGeneralLedgerIVA(Me.Tag)
        '                    Dim iva = modelIva.GetGeneralLedgerIVAById(product.IVAId)
        '                    Item.IvaPercentage = iva.ObjectEmbbeded.Percentage
        '                End Using
        '            End If

        '            'se obtiene el valor de la retencion dependiendo si el proveedor es o no declarante
        '            'Si el tercero del proveedor tiene un tipo de retención 1-Excento de retenciaón o 3-Autoretenedor
        '            'entonces no se calcula la retención
        '            If supplierTmp.ThirdParty IsNot Nothing AndAlso (supplierTmp.ThirdParty.RetentionType <> 1 And supplierTmp.ThirdParty.RetentionType <> 3) Then
        '                Using modelAccountPayableConcept As New MConceptsAccountsPayable(Me.Tag)
        '                    Dim concept As AccountPayableConcepts
        '                    'si el proveedor es declarante
        '                    If supplierTmp.Declarant Then
        '                        concept = modelAccountPayableConcept.GetPaymentConceptById(product.ProductGroup.DeclarantRetentionAccountPayableConceptId).ObjectEmbbeded
        '                    Else
        '                        concept = modelAccountPayableConcept.GetPaymentConceptById(product.ProductGroup.NotDeclarantRetentionAccountPayableConceptId).ObjectEmbbeded
        '                    End If
        '                    Using modelRetentionConcept As New MRetentionConcept(Me.Tag)
        '                        If concept.RetentionConceptId Is Nothing Then
        '                            errors.AppendLine(String.Format("Error en el concepto de retención del grupo del producto"))
        '                            Continue For
        '                            'Return New ActionResult With {.StateResult = False, .Message = "Error en el concepto de retención del grupo del producto"}
        '                        End If
        '                        Dim retentionConcept = modelRetentionConcept.GetRetentionByIdSimple(concept.RetentionConceptId)
        '                        'obtengo el valor de la retencion
        '                        Item.RTFPercentage = retentionConcept.Rate
        '                        Item.RTFValue = Utils.RoundValue(CDec((Item.SubTotalValue - Item.DiscountValue) * (retentionConcept.Rate / 100)), RoundService.Value)
        '                    End Using
        '                End Using
        '            Else 'Dejamos en cero la retención
        '                Item.RTFPercentage = 0
        '                Item.RTFValue = 0
        '            End If

        '            Item.IvaValue = Utils.RoundValue(CDec((Item.SubTotalValue - Item.DiscountValue) * (Item.IvaPercentage / 100)), RoundService.Value)
        '            Item.TotalValue = Item.SubTotalValue + Item.IvaValue - Item.DiscountValue

        '            'Agregamos el lote así no tenga, para la modificacion de las cantidades en la devolucion
        '            Dim entranceVoucherDetailBatchSerial As New EntranceVoucherDetailBatchSerial
        '            entranceVoucherDetailBatchSerial.Quantity = Item.Quantity
        '            entranceVoucherDetailBatchSerial.OutstandingQuantity = Item.Quantity
        '            Item.EntranceVoucherDetailBatchSerial.Add(entranceVoucherDetailBatchSerial)

        '            listEntranceVoucherDetailNotHandlesBatch.Add(Item)
        '        End If
        '    End If
        'Next
        'If errors.Length > 0 Then
        '    Mensaje(EeventViewerImages.Advertencia) = errors.ToString
        '    'Exit Sub
        'End If

        ''agregamos los productos que no manejen lote a la rejilla directamente
        'If listEntranceVoucherDetailNotHandlesBatch IsNot Nothing AndAlso listEntranceVoucherDetailNotHandlesBatch.Count > 0 Then
        '    Dim args As New AddProductEntranceVoucherDetailEventArgs
        '    args.ImportDataMode = True
        '    args.ListEntranceVoucherDetail = listEntranceVoucherDetailNotHandlesBatch
        '    ReturnPopupAddProduct(Nothing, args)
        'End If

        ''abrimos el popup de agregar productos si el producto maneja lote
        'If listEntranceVoucherDetailHandlesBatch IsNot Nothing AndAlso listEntranceVoucherDetailHandlesBatch.Count > 0 Then
        '    Using formulario As New PopUpProductsEntranceVoucher(RoundService)
        '        Me.Cursor = ChangeCursorIndigo()
        '        AddHandler formulario.AddEntranceVoucherDetail, AddressOf ReturnPopupAddProduct
        '        formulario.Size = New Drawing.Size(800, 730)
        '        formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        '        formulario.ListEntranceVoucherDetailImportInfo = listEntranceVoucherDetailHandlesBatch
        '        formulario.ListEntranceVoucherDetailValidation = ListEntranceVoucherDetail.ToList()
        '        formulario.WareHouseId = WarehouseId
        '        formulario.operatingUnitId = BarraBotones.OperatingUnitValue
        '        formulario.ImportDataMode = True
        '        Dim transparent = New Base.FrmTransparent(formulario, False)
        '        Me.Cursor = System.Windows.Forms.Cursors.Default
        '        transparent.ShowDialog(Me)
        '    End Using
        'End If
    End Sub

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.InputRemission IsNot Nothing AndAlso Me.InputRemission.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDBteCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBteCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Obtiene el id del detalle de secuencia por el prefijo seleccionado
    ''' </summary>
    ''' <param name="prefix">Prefijo a buscar</param>
    ''' <returns>Id del detalle de secuencia</returns>
    Private Function GetIdSequenceByPrefix(ByVal prefix As String) As Int64
        'If Me._sequense IsNot Nothing AndAlso Me._sequense.FixedAssetSequenceDetail IsNot Nothing AndAlso Me._sequense.FixedAssetSequenceDetail.Any(Function(d) d.Prefix IsNot Nothing AndAlso d.Prefix.Trim().Equals(prefix)) Then
        '    Return Me._sequense.FixedAssetSequenceDetail.Where(Function(d) d.Prefix IsNot Nothing AndAlso d.Prefix.Trim().Equals(prefix)).FirstOrDefault().Id
        'Else
        '    Return 0
        'End If
    End Function

    ''' <summary>
    ''' retorna la informacion que se establece en el control de la barra
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getInfoRemissionEntrance() As Tuple(Of String, String, String)
        'If listInputRemissionEquipment IsNot Nothing AndAlso listInputRemissionEquipment.Count > 0 Then
        '    _ivaValue = listInputRemissionEquipment.Sum(Function(x) x.iv)
        '    _value = listInputRemissionEquipment.Sum(Function(x) x.SubTotalValue)
        '    _totalValue = _ivaValue + _value
        'Else
        '    _ivaValue = 0
        '    _value = 0
        '    _totalValue = 0
        'End If
        'Return New Tuple(Of String, String, String)(_ivaValue.ToString("c0"), _value.ToString("c0"), _totalValue.ToString("c0"))
    End Function

    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If

        Dim listStatus As New List(Of Tuple(Of String, Byte))
        listStatus.Add(New Tuple(Of String, Byte)("Sin Confirmar", 1))
        listStatus.Add(New Tuple(Of String, Byte)("Confirmado", 2))
        listStatus.Add(New Tuple(Of String, Byte)("Anulado", 3))

        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code"},
                              New ColumnInfo() With {.Caption = "Fecha Remisión", .FieldName = "RemisionDate"},
                              New ColumnInfo() With {.Caption = "Número Remisión", .FieldName = "RemisionNumber"},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "Status", .ColumnEdit = True, .ListItemsDatasourceColumEdit = listStatus}}.ToList
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetInputRemission
            .ValorSolicitado = "Code"
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Metodo para agregar a las rejillas las acciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddActionsColumns()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(INDGvReferralEntry, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvReferralEntry.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Private Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequenceFixedAsset(Me.Tag.ToString())
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        Else
            Me.BarraBotones.EnableBarItems()
        End If
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateRegistered"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' metodo para generar el registro de bloqueo
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub GenerateBlockRecord()
        Using model As New MBlockRecordAndSequenceFixedAsset(MyTag)
            Dim result = Await model.GetBlockRecord(Me.Tag, Me.InputRemission.Id)
            If result IsNot Nothing AndAlso result.Id = 0 Then
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                state.State = Domain.Base.Entities.ObjectState.Added
                record = New BlockRecordFixedAsset With {.BlockDate = DateTime.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .IdForm = Me.Tag, .CodUser = SessionValues.Instance.UserIndigo, .IdRecord = Me.InputRemission.Id}
                Dim operation = Await model.SaveBlockRecord(record)
                record = operation.ObjectEmbbeded
            Else
                Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
                record = result
                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' metodo para generar kla indexacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateDoc() As IndexedDocument2
        Dim content = String.Format(ResourceManager.GetString("FrmEquipmentEntry_IndexContent", MODULE_NAME), InputRemission.Code, If(INDSleSupplierDistributionLine.Text = String.Empty, INDSleSupplierDistributionLine.Properties.NullText, INDSleSupplierDistributionLine.Text), RemissionDate, If(INDSleAdquisitionType.Text = String.Empty, INDSleAdquisitionType.Properties.NullText, INDSleAdquisitionType.Text))
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = content,
                .CreationDate = dateServer,
                .CreationUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName,
                .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.InputRemission.Code & "#$",
                .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me.InputRemission.Code),
                .Update = dateServer,
                .UpdateUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName
            Me._doc.Content = content
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me.InputRemission.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' metodo para mostrar los formulario en el evento buttonclik
    ''' </summary>
    ''' <param name="form"></param>
    ''' <remarks></remarks>
    Private Sub OpenFormDialog(form As FormBase)
        form.ViewModeEditHold = True
        form.Size = New Size(800, 700)
        form.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        form.MaximizeBox = False
        form.MinimizeBox = False
        Dim transparent = New FrmTransparent(form, False)
        transparent.ShowDialog(Me)
    End Sub

    ''' <summary>
    ''' metodo para generar secuencia numerica
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function NewEquipmentEntry() As Task
        Me.InputRemission = New FixedAssetRemissionEntrance()
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.FixedAssetSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.FixedAssetSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.FixedAssetSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
        BarraBotones.StatusRecordVisible = True
        BarraBotones.StatusRecord = "1"


        'Me.InputRemission = New FixedAssetRemissionEntrance()
        'If Me._sequence.IsManual Then
        '    Me.ActionsOnControls = True
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        'Else
        '    If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
        '        Me._idCurrentSequence = Me._sequence.FixedAssetSequenceDetail(0).Id
        '    ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
        '        If Me._sequence.FixedAssetSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
        '            Me._idCurrentSequence = Me._sequence.FixedAssetSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
        '        Else
        '            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '            Exit Sub
        '        End If
        '    End If
        '    If Not Me._sequence.Sequential Then
        '        If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
        '            If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
        '                Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
        '                Me.ActionsOnControls = True
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '            Else
        '                Using model As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
        '                    Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
        '                End Using
        '                If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
        '                    Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
        '                    Me.ActionsOnControls = True
        '                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
        '                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '                Else
        '                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
        '                End If
        '            End If
        '        Else
        '            Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
        '            Me.ActionsOnControls = True
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '        End If
        '    Else
        '        Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
        '        Me.ActionsOnControls = True
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '    End If
        '    BarraBotones.StatusRecordVisible = True
        '    BarraBotones.StatusRecord = "1"
        'End If
    End Function

    ''' <summary>
    ''' carga los controles con la informacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MEquipmentEntry(CStr(Me.Tag))
                    AsyncLoader(True)
                    InputRemission = Await Model.GetInputRemissionAsync(INDBteCode.Text.Trim)
                    INDlcReferralEntry.BeginUpdate()
                    If InputRemission IsNot Nothing AndAlso InputRemission.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(InputRemission.Id))
                            With InputRemission
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)
                                Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.StatusRecordVisible = True

                                listInputRemissionEquipment = .FixedAssetRemissionEntranceItem.ToList

                                Code = .Code
                                RemissionDate = .RemisionDate
                                INDSleAdquisitionType.EditValue = .AdquisitionType
                                SupplierDistributionLineId = .SupplierDistributionLineId
                                INDSleSupplierDistributionLine.Properties.NullText = .NameSuplier
                                RemissionNumber = .RemisionNumber
                                INDMeDetail.EditValue = .Detail
                                INDSlLocationResponsible.EditValue = .GetLocationResponsible
                                INDSlResponsible.EditValue = .ResponsibleId
                                INDtreeLocation.EditValue = .LocationId
                                INDSlResponsible.Properties.NullText = .NameResponsible
                                INDtreeLocation.Properties.NullText = .NameLocation

                                INDGcReferralEntry.DataSource = Nothing
                                INDGcReferralEntry.DataSource = listInputRemissionEquipment

                                BarraBotones.StatusRecord = .Status.ToString()
                            End With
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.InputRemission.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordFixedAsset With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = InputRemission.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            Me.BarraBotones.SetDocuments(InputRemission.Id, Me.Tag.ToString(), Nothing, GetType(FixedAssetRemissionEntrance).Name)
                            INDBtnAdd.Enabled = True
                            AsyncLoader(False)
                            ActionsOnControls = True
                            Select Case InputRemission.Status
                                Case 1
                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                                Case Else
                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                    ReadOnlyControls(True)
                            End Select
                            INDDteDate.Focus()

                            If InputRemission.Status <> 1 Then
                                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
                                INDBtnAdd.Enabled = False
                            Else
                                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
                            End If

                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, InputRemission.Id, 0, InputRemission.Id, _idOperativeUnit)
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewEquipmentEntry()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                            Code = String.Empty
                            INDBteCode.Focus()
                        End If
                    End If
                    INDlcReferralEntry.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        End If



        'If Me.BarraBotones.PermiteConsultar = False Then
        '    'Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
        '    Exit Function
        'End If
        'Using model As New MEquipmentEntry(MyTag)
        '    AsyncLoader(True)
        '    INDlcReferralEntry.BeginUpdate()
        '    InputRemission = Await model.GetInputRemissionAsync(Code)
        '    If InputRemission IsNot Nothing AndAlso InputRemission.Id > 0 Then
        '        'listRemissionEntranceDetail = model.GetRemissionEntranceDetailByRemissionEntranceId(RemissionEntrance.Id)

        '        With InputRemission
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)
        '            Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
        '            Me.BarraBotones.StatusRecordVisible = True

        '            listInputRemissionEquipment = .FixedAssetRemissionEntranceItem.ToList

        '            Code = .Code
        '            RemissionDate = .RemisionDate
        '            INDSleAdquisitionType.EditValue = .AdquisitionType
        '            SupplierDistributionLineId = .SupplierDistributionLineId
        '            INDSleSupplierDistributionLine.Properties.NullText = .NameSuplier
        '            RemissionNumber = .RemisionNumber
        '            INDMeDetail.EditValue = .Detail
        '            INDSlLocationResponsible.EditValue = .GetLocationResponsible
        '            INDSlResponsible.EditValue = .ResponsibleId
        '            INDtreeLocation.EditValue = .LocationId
        '            INDSlResponsible.Properties.NullText = .NameResponsible
        '            INDtreeLocation.Properties.NullText = .NameLocation

        '            INDGcReferralEntry.DataSource = Nothing
        '            INDGcReferralEntry.DataSource = listInputRemissionEquipment

        '            BarraBotones.StatusRecord = .Status.ToString()
        '        End With

        '        BarraBotones.SetDocuments(InputRemission.Id)
        '        Me.GetDocumentIndexed(MyTag & "_" & InputRemission.Code)
        '        GenerateBlockRecord()
        '        INDBtnAdd.Enabled = True
        '        AsyncLoader(False)
        '        ActionsOnControls = True
        '        Select Case InputRemission.Status
        '            Case 1
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
        '                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
        '            Case Else
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '                ReadOnlyControls(True)
        '        End Select
        '        INDDteDate.Focus()

        '        If InputRemission.Status <> 1 Then
        '            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
        '            INDBtnAdd.Enabled = False
        '        Else
        '            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
        '        End If

        '        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
        '        Me.BarraBotones.PrintReport(PrintReportAction.None, InputRemission.Id, 0, InputRemission.Id, _idOperativeUnit)

        '    Else
        '        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
        '        Code = String.Empty
        '        INDBteCode.Focus()
        '        AsyncLoader(False)
        '        If Me._sequence.IsManual Then
        '            Me.NewEquipmentEntry()
        '        Else
        '            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
        '            Me.Code = String.Empty
        '            Deshacer()
        '            INDBteCode.Focus()
        '        End If
        '    End If
        '    INDlcReferralEntry.EndUpdate()
        'End Using
    End Function

    Private Function LoadReport() As Task
        Return Task.Factory.StartNew(Sub()
                                         Me.BarraBotones.SafeInvoke(Sub(x) x.PrintReport(PrintReportAction.None, InputRemission.Id, 0, InputRemission.Id, _idOperativeUnit))
                                     End Sub)
    End Function
    ''' <summary>
    ''' limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlcReferralEntry.BeginUpdate()
        ReadOnlyControls(False)
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.StatusRecordVisible = False
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        InputRemission = Nothing
        Code = String.Empty
        RemissionDate = GetDateServer()
        SupplierDistributionLineId = Nothing
        INDSleSupplierDistributionLine.Properties.NullText = String.Empty
        INDSleSupplierDistributionLine.Properties.ReadOnly = False
        INDSleAdquisitionType.EditValue = 0
		INDTxtReferalNumber.EditValue = String.Empty
		INDSleAdquisitionType.Properties.NullText = String.Empty
		Description = String.Empty
		listInputRemissionEquipment = Nothing
        INDGcReferralEntry.DataSource = Nothing
        IndigoGridControl1.RefreshGrid(INDGcReferralEntry)
        listInputRemissionEquipment = Nothing
        listInputRemissionEquipmentValidationDelete = Nothing
        INDLciLocation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciResponsible.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDSlLocationResponsible.Properties.NullText = String.Empty
        INDSlLocationResponsible.EditValue = 0
		ActionsOnControls = False
		INDlcReferralEntry.EndUpdate()
		ResponsibleId = Nothing
		LocationId = Nothing

		If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' asigna los valroes a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        With InputRemission
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .RemisionDate = RemissionDate
            .AdquisitionType = AdquisitionTypeId
            .SupplierDistributionLineId = INDSleSupplierDistributionLine.EditValue
            .RemisionNumber = RemissionNumber
            .Detail = Description
            .GetLocationResponsible = GetLocationResponsible
            .ResponsibleId = ResponsibleId
            .LocationId = LocationId
            .OperatingUnitId = Me.BarraBotones.OperatingUnit.Id

            If listInputRemissionEquipment IsNot Nothing Then

				If INDLciLocation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
					listInputRemissionEquipment.ForEach(Sub(entryItem)
															If entryItem.FixedAssetRemissionEntranceItemDetail IsNot Nothing AndAlso entryItem.FixedAssetRemissionEntranceItemDetail.Count > 0 Then
																entryItem.FixedAssetRemissionEntranceItemDetail.ToList.ForEach(Sub(entryItemDetail)
																																   entryItemDetail.ResponsibleId = ResponsibleId
																																   entryItemDetail.LocationId = LocationId
																															   End Sub)
															End If
														End Sub)
				End If


				For Each item In listInputRemissionEquipment
					.FixedAssetRemissionEntranceItem.Add(item)
				Next
				If listInputRemissionEquipmentValidationDelete IsNot Nothing Then
                    For Each item In listInputRemissionEquipment
                        .FixedAssetRemissionEntranceItem.Add(item)
                    Next
                End If
            End If

        End With
    End Sub

    ''' <summary>
    ''' Controla el boton de agregar producto
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidateEditValue()
        If INDSleSupplierDistributionLine.EditValue IsNot Nothing AndAlso INDSleAdquisitionType.EditValue IsNot Nothing Then
            INDBtnAdd.Enabled = True
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
        Else
            INDBtnAdd.Enabled = False
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
        End If
    End Sub

    ''' <summary>
    ''' Editar el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditDetail()
        inputRemissionEquipment = DirectCast(INDGvReferralEntry.GetFocusedRow(), FixedAssetRemissionEntranceItem)
        indexEditRecord = listInputRemissionEquipment.IndexOf(inputRemissionEquipment)
        OpenFormFixedAssetRemissionEntranceItem(True)
    End Sub

    Private Sub OpenFormFixedAssetRemissionEntranceItem(EditMode As Boolean)
        Dim errors As New StringBuilder
        If SupplierDistributionLineId Is Nothing Then
            errors.AppendLine("Debe seleccionar " + INDLciSupplierDistributionLine.Text)
        End If
        If AdquisitionTypeId Is Nothing Then
            errors.AppendLine("Debe ingresar " + INDLciAdquisitionType.Text)
        End If
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString
            Exit Sub
        End If
        Using formulario As New FrmFixedAssetRemissionEntranceItem
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddEquipmentRemissionEventArgs, AddressOf ReturnAddRemissionEntranceDetail
            formulario.MyTag = MyTag
            formulario.EditMode = EditMode
            formulario.InputRemissionEquipment = inputRemissionEquipment
            formulario.GetLocationResponsible = INDSlLocationResponsible.EditValue
            formulario.ImportData = False
            formulario.AdquisitionType = AdquisitionTypeId
            formulario.Size = New Drawing.Size(1090, 750)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Eliminar Detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetail()
        inputRemissionEquipment = DirectCast(INDGvReferralEntry.GetFocusedRow(), FixedAssetRemissionEntranceItem)
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If inputRemissionEquipment.Id > 0 Then
                If listInputRemissionEquipmentValidationDelete Is Nothing Then
                    listInputRemissionEquipmentValidationDelete = New List(Of FixedAssetRemissionEntranceItem)
                End If
                While inputRemissionEquipment.FixedAssetRemissionEntranceItemDetail.Count > 0
                    If inputRemissionEquipment.FixedAssetRemissionEntranceItemDetail(0).Id > 0 Then
                        inputRemissionEquipment.FixedAssetRemissionEntranceItemDetail(0).MarkAsDeleted()
                    Else
                        inputRemissionEquipment.FixedAssetRemissionEntranceItemDetail.Remove(inputRemissionEquipment.FixedAssetRemissionEntranceItemDetail(0))
                    End If
                End While
                inputRemissionEquipment.MarkAsDeleted()
                listInputRemissionEquipmentValidationDelete.Add(inputRemissionEquipment)
            End If
            listInputRemissionEquipment.Remove(inputRemissionEquipment)

            INDGcReferralEntry.DataSource = Nothing
            INDGcReferralEntry.DataSource = listInputRemissionEquipment
        End If
    End Sub

    ''' <summary>
    ''' Metodo que importa la orden de compra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ImportInfo()
        Dim errors As New StringBuilder
        If SupplierDistributionLineId Is Nothing Then
            errors.AppendLine("Debe seleccionar " + INDLciSupplierDistributionLine.Text)
        End If
        If AdquisitionTypeId Is Nothing OrElse AdquisitionTypeId = 0 Then
            errors.AppendLine("Debe ingresar " + INDLciAdquisitionType.Text)
        End If
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString
            Exit Sub
        End If
        Using formulario As New FrmImportInfo
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddImportsInfoEventArgs, AddressOf ReturnImportInfo
            formulario.ListCompare = listInputRemissionEquipment
            formulario.SupplierDistributionLineId = SupplierDistributionLineId
            formulario.GetLocationResponsible = INDSlLocationResponsible.EditValue
            formulario.AdquisitionType = AdquisitionTypeId
            formulario.Size = New Drawing.Size(604, 535)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Retorno del evento de importar información
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnImportInfo(sender As Object, e As AddImportsInfo)
        If e IsNot Nothing Then
            If listInputRemissionEquipment Is Nothing Then
                listInputRemissionEquipment = New List(Of FixedAssetRemissionEntranceItem)
            End If
            listInputRemissionEquipment.AddRange(e.ListFixedAssetRemissionEntranceItem)
            INDGcReferralEntry.DataSource = Nothing
            INDGcReferralEntry.DataSource = listInputRemissionEquipment
        End If
    End Sub

#End Region

#Region "HANDLERS"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        indexEditRecord = Nothing
        _ivaValue = Nothing
        _value = Nothing
        _totalValue = Nothing
        _sequence = Nothing
        _prefixSelected = Nothing
        _idOperativeUnit = Nothing
        _idCurrentSequence = Nothing
        record = Nothing
        presenter = Nothing
        supplierId = Nothing
        varImp = Nothing
        flagLoad = Nothing
        listInputRemissionEquipmentValidationDelete = Nothing
        inputRemissionEquipment = Nothing
        InputRemission = Nothing
        listInputRemissionEquipment = Nothing
    End Sub

    Private Sub FrmReferralEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlcReferralEntry, True)
        Me._doc = Nothing
        _indigoSession = SessionValues.Instance
        presenter = New PEquipmentEntry(Me)
        presenter.LoadDefinitionLayout()
        presenter.GetSequense()
        INDDteDate.Properties.MaxValue = Date.Now()
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        INDSleAdquisitionType.Properties.DataSource = DatasourceAcquisition
        INDSlLocationResponsible.Properties.DataSource = DatasourceLocationResponsibleOpc
        AddActionsColumns()
        Deshacer()
        LoadStatus()
        If LocationXPO Is Nothing Then
            Using model As New MEquipmentEntry(MyTag)
                LocationXPO = model.ListLocationXpo()
            End Using
        End If
    End Sub
#End Region

#Region "Activated"
    Private Sub FrmReferralEntry_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If INDBteCode.Enabled Then
            INDBteCode.Focus()
        End If
    End Sub
#End Region

#Region "FormClosing"
    Private Sub FrmReferralEntry_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub
#End Region

#Region "Click"
    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        inputRemissionEquipment = Nothing
        OpenFormFixedAssetRemissionEntranceItem(False)
    End Sub
#End Region

#Region "QueryPopUp"

    Private Sub INDSleSupplier_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleSupplierDistributionLine.QueryPopUp
        If INDSleSupplierDistributionLine.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If SupplierXPO Is Nothing Then
            Using model As New MEquipmentEntry(MyTag)
                SupplierXPO = model.ListSuppliersDistributionLines()
            End Using
        End If
    End Sub

    Private Sub INDtreeLocation_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs)
        If INDtreeLocation.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If LocationXPO Is Nothing Then
            Using model As New MEquipmentEntry(MyTag)
                LocationXPO = model.ListLocationXpo()
            End Using
        End If
    End Sub

    Private Sub INDSlResponsible_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlResponsible.QueryPopUp
        If INDSlResponsible.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If ResponsibleXPO Is Nothing Then
            Using model As New MEquipmentEntry(MyTag)
                ResponsibleXPO = model.ListResponsible()
            End Using
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara para abrir el form de proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleSupplier_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleSupplierDistributionLine.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(558, Nothing, True)
            Using model As New MEquipmentEntry(MyTag)
                SupplierXPO = model.ListSuppliersDistributionLines()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form de responsable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSlResponsible_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSlResponsible.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1711, Nothing, True)
            Using model As New MEquipmentEntry(MyTag)
                ResponsibleXPO = model.ListResponsible()
            End Using
        End If
    End Sub

#End Region

#Region "MenuContext"

    ''' <summary>
    ''' Evento que da la opcion de eliminar o modificar los regitros de productos de la orden de traslado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim button As DevExpress.XtraEditors.SimpleButton = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case button.Tag.ToString
            Case "Edit"
                EditDetail()
            Case "Remove"
                DeleteDetail()
        End Select
    End Sub

    ''' <summary>
    ''' Crea el menu contextual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditDetail()
            Case "Remove"
                DeleteDetail()
        End Select
    End Sub

#End Region

#Region "KeyDown"

    Private Async Sub INDBteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await Me.NewEquipmentEntry()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "EditValueChanged"
    Private Sub INDSleSupplierDistributionLine_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleSupplierDistributionLine.EditValueChanged
        If INDSleSupplierDistributionLine.EditValue IsNot Nothing Then
            ValidateEditValue()
            If SupplierXPO IsNot Nothing Then
                If Not INDGvSleSupplier.GetFocusedRow().GetType().Equals((GetType(DevExpress.Data.NotLoadedObject))) Then
                    'Dim supplierDistributionLine = DirectCast(DirectCast(INDGvSleSupplier.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, CommonSuppliersDistibutionLineXpo)
                    'supplierId = supplierDistributionLine.IdSupplier.Id
                Else
                    If InputRemission IsNot Nothing AndAlso InputRemission.Id > 0 Then
                        supplierId = InputRemission.SupplierDistributionLineId
                    End If
                End If
            End If
        Else
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
        End If
    End Sub

    Private Sub INDSlLocationResponsible_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlLocationResponsible.EditValueChanged
        If INDSlLocationResponsible.EditValue = 1 Then
            INDLciResponsible.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciLocation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLciResponsible.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciLocation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

#End Region

#Region "DataSourceChanged"
    Private Sub INDGcReferralEntry_DataSourceChanged(sender As Object, e As EventArgs) Handles INDGcReferralEntry.DataSourceChanged
        'Permitir editar si no existen datos en la rejilla
        If listInputRemissionEquipment Is Nothing OrElse listInputRemissionEquipment.Count = 0 Then
            INDSleAdquisitionType.Properties.ReadOnly = False
            INDSleSupplierDistributionLine.Properties.ReadOnly = False
        Else
            INDSleAdquisitionType.Properties.ReadOnly = True
            INDSleSupplierDistributionLine.Properties.ReadOnly = True
        End If
    End Sub
#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmEquipmentEntry_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDBteCode.Focus()
    End Sub

#End Region

#End Region

#Region "BAR BUTTONS"

    ''' <summary>
    ''' Evento que dispara el formulario de importar productos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ImportarInformacion() Handles BarraBotones.Click_ImportarInformacion
        ImportInfo()
    End Sub

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBteCode.ButtonClick
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        BarraBotones.Focus()
        varImp = 1
        InputRemission.Status = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        BarraBotones.Focus()
        varImp = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.FixedAssetSequenceDetail IsNot Nothing Then
                If Not Me._sequence.FixedAssetSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            InputRemission.Status = 3
            varImp = 4
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, InputRemission.Id, 0, InputRemission.Id, _idOperativeUnit)
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        InputRemission.Status = 2
        varImp = 3
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        InputRemission.Status = 2
        varImp = 3
        Guardar()
    End Sub

#End Region

End Class