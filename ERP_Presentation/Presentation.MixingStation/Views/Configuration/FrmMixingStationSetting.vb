'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Diego A. Roldán Lozano
' Created          : 2021-09-16
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Inventory.MVP
Imports Presentation.MixingStation.MVP

Public Class FrmMixingStationSetting
    Implements IMixingStationSetting

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "MixingStation"

#Region "Constructor"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    Public Sub New()
        InitializeComponent()
    End Sub
#End Region

#Region "Properties"
    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Integer

    ''' <summary>
    ''' Entidad
    ''' </summary>
    Private _mixingStationSetting As MixingStationSetting

    ''' <summary>
    ''' Listado de centro de atencion
    ''' </summary>
    Private _attentionCentersList As List(Of MixingStationSettingAttentionCenter)

    ''' <summary>
    ''' Presenter
    ''' </summary>
    Private _presenter As PMixingStationSetting

    ''' <summary>
    ''' block record
    ''' </summary>
    Private _record As BlockRecordMixingStation

    ''' <summary>
    ''' Mensaje
    ''' </summary>
    ''' <param name="Icono"></param>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
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

    ''' <summary>
    ''' Concepto de inventario tipo salida materia prima
    ''' </summary>
    ''' <returns></returns>
    Public Property InventoryAdjustmentConceptOutputId As Integer? Implements IMixingStationSetting.InventoryAdjustmentConceptOutputId
        Get
            Return INDSleInventoryAdjustmentConceptOutput.EditValue
        End Get
        Set(value As Integer?)
            INDSleInventoryAdjustmentConceptOutput.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' código centro de atencion
    ''' </summary>
    ''' <returns></returns>
    Private Property CODCENATE As String
        Get
            Return INDSleAttentionCenter.EditValue
        End Get
        Set(value As String)
            INDSleAttentionCenter.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' código unidad funcional
    ''' </summary>
    ''' <returns></returns>
    Private Property UFUCODIGO As String
        Get
            Return INDSleFuncionalUnit.EditValue
        End Get
        Set(value As String)
            INDSleFuncionalUnit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' centros de atencion datasource
    ''' </summary>
    Private WriteOnly Property AttentionCenters As List(Of MixingStationSettingAttentionCenter)
        Set(value As List(Of MixingStationSettingAttentionCenter))
            INDGcAttentionCenter.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Concepto de inventario tipo entrada
    ''' </summary>
    ''' <returns></returns>
    Public Property InventoryAdjustmentConceptInputId As Integer? Implements IMixingStationSetting.InventoryAdjustmentConceptInputId
        Get
            Return INDSleInventoryAdjustmentConceptInput.EditValue
        End Get
        Set(value As Integer?)
            INDSleInventoryAdjustmentConceptInput.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Fabricante
    ''' </summary>
    ''' <returns></returns>
    Public Property ManufacturerId As Integer? Implements IMixingStationSetting.ManufacturerId
        Get
            Return INDSleManufacturer.EditValue
        End Get
        Set(value As Integer?)
            INDSleManufacturer.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Días de expiración
    ''' </summary>
    ''' <returns></returns>
    Public Property ExpirationDays As Integer Implements IMixingStationSetting.ExpirationDays
        Get
            Return INDSpnExpirationDays.EditValue
        End Get
        Set(value As Integer)
            INDSpnExpirationDays.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Unidad de medida
    ''' </summary>
    ''' <returns></returns>
    Public Property MeasurementUnitId As Integer? Implements IMixingStationSetting.MeasurementUnitId
        Get
            Return INDSleMeasurementUnit.EditValue
        End Get
        Set(value As Integer?)
            INDSleMeasurementUnit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Tipo de producto
    ''' </summary>
    ''' <returns></returns>
    Public Property ProductTypeId As Integer? Implements IMixingStationSetting.ProductTypeId
        Get
            Return INDSleProductType.EditValue
        End Get
        Set(value As Integer?)
            INDSleProductType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Unidad de empaque
    ''' </summary>
    ''' <returns></returns>
    Public Property PackageUnitId As Integer? Implements IMixingStationSetting.PackageUnitId
        Get
            Return INDSlePackageUnit.EditValue
        End Get
        Set(value As Integer?)
            INDSlePackageUnit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Almacén de Transito 
    ''' </summary>
    ''' <returns></returns>
    Public Property TransitWarehouseId As Integer? Implements IMixingStationSetting.TransitWarehouseId
        Get
            Return INDsleTransitWarehouse.EditValue
        End Get
        Set(value As Integer?)
            INDsleTransitWarehouse.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Activar Central de Mezclas
    ''' </summary>
    ''' <returns></returns>
    Public Property ActivateMS As Boolean Implements IMixingStationSetting.ActivateMS
        Get
            Return False 'INDCtrYesNoActivateMS.EditValue
        End Get
        Set(value As Boolean)
            'INDCtrYesNoActivateMS.EditValue = value
        End Set
    End Property
#End Region

#Region "Events"
    ''' <summary>
    ''' Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub FrmMixingStationSetting_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Await InitForm()
    End Sub

    ''' <summary>
    ''' Evento cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmMixingStationSetting_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Focus
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmMixingStationSetting_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        INDSleInventoryAdjustmentConceptOutput.Focus()
    End Sub

    ''' <summary>
    ''' Query popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleInventoryAdjustmentConceptOutput_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleInventoryAdjustmentConceptOutput.QueryPopUp
        If INDSleInventoryAdjustmentConceptOutput.Properties.DataSource Is Nothing AndAlso Not INDSleInventoryAdjustmentConceptOutput.ReadOnly Then
            Using model As New MAdjustmentConcept(Tag)
                INDSleInventoryAdjustmentConceptOutput.Properties.DataSource = model.ListAdjustmentConcept(True, 1, 2, True)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Query popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleInventoryAdjustmentConceptInput_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleInventoryAdjustmentConceptInput.QueryPopUp
        If INDSleInventoryAdjustmentConceptInput.Properties.DataSource Is Nothing AndAlso Not INDSleInventoryAdjustmentConceptInput.ReadOnly Then
            Using model As New MAdjustmentConcept(Tag)
                INDSleInventoryAdjustmentConceptInput.Properties.DataSource = model.ListAdjustmentConcept(True, 1, 1, True)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Query popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleManufacturer_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleManufacturer.QueryPopUp
        If INDSleManufacturer.Properties.DataSource Is Nothing AndAlso Not INDSleManufacturer.ReadOnly Then
            Using model As New MManufacturer(Tag)
                INDSleManufacturer.Properties.DataSource = model.ListManufacturer(True)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Query popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSlePackageUnit_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlePackageUnit.QueryPopUp
        If INDSlePackageUnit.Properties.DataSource Is Nothing AndAlso Not INDSlePackageUnit.ReadOnly Then
            Using model As New MPackagingUnit(Tag)
                INDSlePackageUnit.Properties.DataSource = model.ListPackageUnits(True)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Query popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleMeasurementUnit_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleMeasurementUnit.QueryPopUp
        If INDSleMeasurementUnit.Properties.DataSource Is Nothing AndAlso Not INDSleMeasurementUnit.ReadOnly Then
            Using model As New MMeasureUnit(Tag)
                INDSleMeasurementUnit.Properties.DataSource = model.ListMeasurementUnit()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Query popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleProductType_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleProductType.QueryPopUp
        If INDSleProductType.Properties.DataSource Is Nothing AndAlso Not INDSleProductType.ReadOnly Then
            Using model As New MProductType(Tag)
                INDSleProductType.Properties.DataSource = model.ListProductTypes(5)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' querypopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleAttentionCenter_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleAttentionCenter.QueryPopUp
        If INDSleAttentionCenter.Properties.DataSource Is Nothing Then
            INDSleAttentionCenter.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).MixingStationService.ListXPInstantFeedbackSource(Of CentersXpo)()
        End If
    End Sub

    ''' <summary>
    ''' query popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleFuncionalUnit_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleFuncionalUnit.QueryPopUp
        If INDSleFuncionalUnit.Properties.DataSource Is Nothing Then
            Dim filter = $"INCENUNFUs[CODCENATE='{CODCENATE}']"
            INDSleFuncionalUnit.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).MixingStationService.ListXPInstantFeedbackSource(Of INUNIFUNCXpo)(filter)
        End If
    End Sub

    ''' <summary>
    ''' editvalue
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleAttentionCenter_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleAttentionCenter.EditValueChanged
        INDSleFuncionalUnit.EditValue = Nothing
        INDSleFuncionalUnit.Properties.DataSource = Nothing
    End Sub

    ''' <summary>
    ''' actions
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        RemoveAttentionCenter()
    End Sub

    ''' <summary>
    ''' Query popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleTransitWarehouse_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleTransitWarehouse.QueryPopUp
        If INDsleTransitWarehouse.Properties.DataSource Is Nothing AndAlso Not INDsleTransitWarehouse.ReadOnly Then
            INDsleTransitWarehouse.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListTransitWarehouseByStatusAndUser(True, Me.indigo.UserIndigo)
        End If
    End Sub

    ''' <summary>
    ''' Agrega un centro de atencion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbAdd_Click(sender As Object, e As EventArgs) Handles INDSbAdd.Click
        If CODCENATE Is Nothing OrElse UFUCODIGO Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = $"{IIf(CODCENATE Is Nothing And UFUCODIGO Is Nothing _
                                                             , "Los Campos Centro de Atencion y Unidad Funcional están Vacios", $"El Campo {IIf(CODCENATE Is Nothing _
                                                             , "Centro de atencion", "Unidad Funcional")} esta vacio")}"
            Return
        End If


        If _attentionCentersList Is Nothing Then
            _attentionCentersList = New List(Of MixingStationSettingAttentionCenter)()
        End If

        If _attentionCentersList.Any(Function(m) m.CODCENATE = CODCENATE AndAlso m.UFUCODIGO = UFUCODIGO) Then
            Mensaje(EeventViewerImages.Advertencia) = "Ya existe esta configuración en el listado"
            Return
        End If

        _attentionCentersList.Add(New MixingStationSettingAttentionCenter With {
            .CODCENATE = CODCENATE,
            .UFUCODIGO = UFUCODIGO,
            .AttentionCenterCodeName = INDSleAttentionCenter.Text,
            .FunctionalUnitCodeName = INDSleFuncionalUnit.Text
        })
        AttentionCenters = _attentionCentersList.FindAll(Function(m) m.ChangeTracker.State <> ObjectState.Deleted)
        CleanControlsPopupAttentionCenter()
    End Sub
#End Region

#Region "Functions"
    ''' <summary>
    ''' Elimina un registro bloqueado
    ''' </summary>
    Public Async Sub DeleteBlockedRecord() Implements IMixingStationSetting.DeleteBlockedRecord
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequenceMixingStation(CStr(Me.Tag))
                Await model.DeleteBlockRecord(_record)
                _record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Public Sub CleanControls() Implements IMixingStationSetting.CleanControls
        INDLcRoot.BeginUpdate()
        InventoryAdjustmentConceptOutputId = Nothing
        InventoryAdjustmentConceptInputId = Nothing
        ManufacturerId = Nothing
        ExpirationDays = 0
        MeasurementUnitId = Nothing
        ProductTypeId = Nothing
        PackageUnitId = Nothing
        ActivateMS = False
        INDSleInventoryAdjustmentConceptOutput.Properties.NullText = String.Empty
        INDSleInventoryAdjustmentConceptInput.Properties.NullText = String.Empty
        INDSleManufacturer.Properties.NullText = String.Empty
        INDSleMeasurementUnit.Properties.NullText = String.Empty
        INDSleProductType.Properties.NullText = String.Empty
        INDSlePackageUnit.Properties.NullText = String.Empty
        INDsleTransitWarehouse.Properties.NullText = String.Empty

        _mixingStationSetting = Nothing
        AttentionCenters = Nothing
        CleanControlsPopupAttentionCenter()

        INDLcRoot.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Estados iniciales del formulario
    ''' </summary>
    Private Async Function InitForm() As Task
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        _presenter = New PMixingStationSetting(Me)

        setActionsColumn()
        Await LoadControls()
    End Function

    ''' <summary>
    ''' Establece la columna de acciones
    ''' </summary>
    Private Sub setActionsColumn()
        IndigoGridView1.SetListAcction(INDGvAttentionCenter, {eAcciones.Remove}.ToList())
        Dim col = INDGvAttentionCenter.Columns.FirstOrDefault(Function(m) m.Name = "colActions")
        If col IsNot Nothing Then
            col.Width = 90
        End If
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._mixingStationSetting.Id, Me._mixingStationSetting.OperativeUnitId),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me._mixingStationSetting.Id & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._mixingStationSetting.OperativeUnitId),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._mixingStationSetting.Id, Me._mixingStationSetting.OperativeUnitId)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._mixingStationSetting.OperativeUnitId)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Guardar
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
    End Sub

    ''' <summary>
    ''' Guardar
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If ValidateControls() = False Then Return
        AssigningValues()

        Try
            Using model As New MMixingStationSetting(Tag)
                AsyncLoader(True)
                Dim result As ActionResult(Of MixingStationSetting) = Await model.SaveMixingStationSettingAsync(_mixingStationSetting, _attentionCentersList)
                AsyncLoader(False)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    LoadControls()
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Asigna los valores a la entidad
    ''' </summary>
    Private Sub AssigningValues()
        With _mixingStationSetting
            .OperativeUnitId = _idOperativeUnit
            .InventoryAdjustmentConceptOutputId = InventoryAdjustmentConceptOutputId
            .InventoryAdjustmentConceptInputId = InventoryAdjustmentConceptInputId
            .ManufacturerId = ManufacturerId
            .ExpirationDays = ExpirationDays
            .MeasurementUnitId = MeasurementUnitId
            .ProductTypeId = ProductTypeId
            .PackageUnitId = PackageUnitId
            .TransitWarehouseId = TransitWarehouseId
        End With
    End Sub

    ''' <summary>
    ''' Nuevo
    ''' </summary>
    Public Sub Nuevo() Implements ICrudBase.Nuevo
    End Sub

    ''' <summary>
    ''' Deshacer
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Eliminar
    ''' </summary>
    Public Sub Eliminar() Implements ICrudBase.Eliminar
    End Sub

    ''' <summary>
    ''' Formulario de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch

    End Sub

    ''' <summary>
    ''' Carga los controles
    ''' </summary>
    Public Async Function LoadControls() As Task Implements IMixingStationSetting.LoadControls
        If Me.BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Function
        End If
        Try
            Using model As New MMixingStationSetting(Tag)
                AsyncLoader(True)
                Dim result = Await model.GetMixingStationSettingByOperativeUnitIdAsync(_idOperativeUnit)
                _mixingStationSetting = result.ObjectEmbbeded
                INDLcRoot.BeginUpdate()

                If _mixingStationSetting IsNot Nothing AndAlso _mixingStationSetting.Id > 0 Then
                    Using modelRecord As New MBlockRecordAndSequenceMixingStation(Tag)
                        _record = Await modelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_mixingStationSetting.Id))

                        With _mixingStationSetting
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            Me.BarraBotones.CleanAuditBasic()
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                            InventoryAdjustmentConceptOutputId = .InventoryAdjustmentConceptOutputId
                            InventoryAdjustmentConceptInputId = .InventoryAdjustmentConceptInputId
                            ManufacturerId = .ManufacturerId
                            ExpirationDays = .ExpirationDays
                            MeasurementUnitId = .MeasurementUnitId
                            ProductTypeId = .ProductTypeId
                            PackageUnitId = .PackageUnitId
                            TransitWarehouseId = .TransitWarehouseId
                            INDSleInventoryAdjustmentConceptOutput.Properties.NullText = .InventoryAdjustmentConceptOutputCodeName
                            INDSleInventoryAdjustmentConceptInput.Properties.NullText = .InventoryAdjustmentConceptInputCodeName
                            INDSleManufacturer.Properties.NullText = .ManufacturerCodeName
                            INDSleMeasurementUnit.Properties.NullText = .MeasurementUnitCodeName
                            INDSleProductType.Properties.NullText = .ProductTypeCodeName
                            INDSlePackageUnit.Properties.NullText = .PackageUnitCodeName
                            INDsleTransitWarehouse.Properties.NullText = .TransitWarehouseCodeName
                        End With

                        Me.GetDocumentIndexed($"{Tag}_{_mixingStationSetting.Id}")
                        LoadAttentionCenters()
                        If _record.Id = 0 Then
                            _record = (Await modelRecord.SaveBlockRecord(
                                    New BlockRecordMixingStation With {
                                        .BlockDate = Date.Now,
                                        .ChangeTracker = New ObjectChangeTracker With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName,
                                        .IdForm = Me.Tag,
                                        .CodUser = Me.indigo.UserIndigo,
                                        .IdRecord = _mixingStationSetting.Id
                                    })
                                ).ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _record.CodUser, _record.NameUser, _record.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _record.CodUser)
                        End If

                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
                        Me.BarraBotones.SetDocuments(_mixingStationSetting.Id, Me.Tag.ToString(), Nothing, GetType(MixingStationSetting).Name)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
                    End Using
                Else
                    _mixingStationSetting = New MixingStationSetting()
                    BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
                End If
                INDLcRoot.EndUpdate()
            End Using
        Catch ex As Exception
            Throw ex
        Finally
            AsyncLoader(False)
        End Try
    End Function

    ''' <summary>
    ''' limpia el popup de centros de atencion
    ''' </summary>
    Private Sub CleanControlsPopupAttentionCenter()
        CODCENATE = Nothing
        UFUCODIGO = Nothing
        INDSleAttentionCenter.Focus()
    End Sub

    ''' <summary>
    ''' Elimina un centro de atencion
    ''' </summary>
    Private Sub RemoveAttentionCenter()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) <> System.Windows.Forms.DialogResult.Yes Then
            Return
        End If

        Dim row = INDGvAttentionCenter.GetFocusedObject(Of MixingStationSettingAttentionCenter)()

        If row IsNot Nothing Then
            row.MarkAsDeleted()
            AttentionCenters = _attentionCentersList.FindAll(Function(m) m.ChangeTracker.State <> ObjectState.Deleted)
        End If
    End Sub

    ''' <summary>
    ''' Carga los centros de atencion
    ''' </summary>
    ''' <returns></returns>
    Private Async Function LoadAttentionCenters() As Task
        INDGvAttentionCenter.ShowLoadingPanel()
        Using model As New MMixingStationSetting(Tag)
            _attentionCentersList = Await model.ListMixingStationSettignAttentionCenters()
            AttentionCenters = _attentionCentersList
        End Using
        INDGvAttentionCenter.HideLoadingPanel()
    End Function

    ''' <summary>
    ''' LogicaBotonActualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub
#End Region

#Region "Bar Button"
    ''' <summary>
    ''' Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(Tag)
    End Sub

    ''' <summary>
    ''' Guardar
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Buscar
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Deshacer
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Eliminar
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Guardar
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Nuevo
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Async Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            BarraBotones.StatusRecordVisible = False
            DeleteBlockedRecord()
            CleanControls()
            Await LoadControls()
        End If
    End Sub
#End Region
End Class