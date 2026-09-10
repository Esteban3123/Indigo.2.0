'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Carlos Ernesto Córdoba
' Created          : 24-02-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports System.Windows.Forms
Imports Presentation.Controls
Imports Presentation.Inventory.MVP
Imports Presentation.Common.MVP
Imports Domain.Crystal.Entities
Imports DevExpress.XtraGrid.Columns
Imports Domain.Entities
Imports System.Text
Imports Presentation.Payroll.MVP
Imports Domain.Base.Entities
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Xpo
Imports System.Dynamic

#End Region

Public Class FrmPopUpDashBoardPharmacyReturn
    Implements IDashBoardPharmacyDetail

#Region "EVENTS"
    ''' <summary>
    ''' evento que se dispara cuando el formulario se cierre
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event DashBoardPharmacyReturnSuccessEventArgs(sender As Object, e As DashBoardPharmacyEventArgs)
#End Region

#Region "BUILDER"
    Public Sub New()
        InitializeComponent()
        ctrTmp = New CtrMoreInfoDashboardPharmacy()
        ctrTmp.SetInfoFunction(AddressOf getInfoDashBoardPharmacy)
        ctrTmp.PrintInfo()
        ctrTmp.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
        INDGvDashboardPharmacyDetail.OptionsView.ShowAutoFilterRow = False
    End Sub
#End Region

#Region "GLOBALS"
    Private ctrTmp As CtrMoreInfoDashboardPharmacy

    Dim listDashboardPharmacyDetail As List(Of ViewDashboardPharmacyDetailDevolution)
    ''' <summary>
    ''' listado del detalle de la dispensacion farmaceutica
    ''' </summary>
    ''' <remarks></remarks>
    'Dim listPharmaceuticalDispensingDetail As List(Of PharmaceuticalDispensingDetail)
    ' ''' <summary>
    ' ''' listado de la cabecera de la dispensacion
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Dim listPharmaceuticalDispensing As List(Of PharmaceuticalDispensing)

    Dim applyProcedureId As Integer?

    Dim codeNameApplyProcedure As String

    ''' <summary>
    ''' variable que contiene el presentador
    ''' </summary>
    Private _presenter As PDashBoardPharmacyDetail

    ''' <summary>
    ''' Variable que contiene la cabecera de la secuencia
    ''' </summary>
    Private _sequence As Domain.Entities.InventorySequence
    ''' <summary>
    ''' Constante que contiene  el nombre del modulo
    ''' </summary>
    Public Const MODULE_NAME As String = "Inventory"

    ''' <summary>
    ''' Id de la Unidad Operativa Seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Variable que contiene el id de la secuencia detalle
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' filtros para detalles que llegan desde dashboard farmacia
    ''' </summary>
    Public typeFilter As String

    Dim dictionaryPharmaceuticalDispensingDetailBatchSerialCrystalProduct As Dictionary(Of String, List(Of PharmaceuticalDispensingDetailBatchSerial))
    Dim dictionaryProduct As Dictionary(Of Integer, Infrastructure.Data.Xpo.InventoryRepository.InventoryProductXpo)
    Dim dictionaryAtcCode As Dictionary(Of Integer, String)
    Dim dictionarySuppliedCode As Dictionary(Of Integer, String)
#End Region

#Region "PROPERTIES"

    ''' <summary>
    ''' ORIDEVMED : 1 - Traslado Cama, 2 - egreso cama, 5 - Hoja d Gasto Qx, 4 - Enfermeria
    ''' </summary>
    ''' <remarks></remarks>
    Dim _ORIDEVMED As Integer
    Public WriteOnly Property ORIDEVMED As Integer
        Set(value As Integer)
            _ORIDEVMED = value
        End Set
    End Property

    WriteOnly Property SetFocusCUM As Boolean
        Set(value As Boolean)
            If value Then
                INDMeCUM.Focus()
            End If
        End Set
    End Property
    ''' <summary>
    ''' codico del paciente
    ''' </summary>
    ''' <remarks></remarks>
    Dim _patientCode As String
    Public WriteOnly Property PatientCode As String
        Set(value As String)
            _patientCode = value
        End Set
    End Property
    ''' <summary>
    ''' nombre del paciente
    ''' </summary>
    ''' <remarks></remarks>
    Dim _patientName As String
    Public WriteOnly Property PatientName As String
        Set(value As String)
            _patientName = value
        End Set
    End Property
    ''' <summary>
    ''' numero del ingreso
    ''' </summary>
    ''' <remarks></remarks>
    Dim _admission As String
    Public WriteOnly Property Admission As String
        Set(value As String)
            _admission = value
        End Set
    End Property

    ''' <summary>
    ''' fecha de nacimiento
    ''' </summary>
    ''' <remarks></remarks>
    Dim _birthDay As Date?
    Public WriteOnly Property BirthDay As Date?
        Set(value As Date?)
            _birthDay = value
        End Set
    End Property

    ''' <summary>
    ''' consecutivo de la cabecera
    ''' </summary>
    ''' <remarks></remarks>
    Dim _consecutive As Decimal
    Public WriteOnly Property Consecutive As Decimal
        Set(value As Decimal)
            _consecutive = value
        End Set
    End Property
    ''' <summary>
    ''' bodega
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Dim _functionalUnitCode As String
    Public WriteOnly Property FunctionalUnit As String
        Set(value As String)
            INDTxtFunctionalUnit.Text = value
            Dim functionalUnitTmp() = value.Split("-")
            _functionalUnitCode = functionalUnitTmp(0).Trim()
        End Set
    End Property

    Private _store As String
    Public Property Store As String
        Get
            Return _store
        End Get
        Set(value As String)
            _store = value
            Using model As New MDashBoardPharmacy(Me.MyTag)
                Dim listWarehouse = model.ListWarehouseUser(String.Empty)
                INDTxtStore.Properties.DataSource = listWarehouse
                Dim warehouse = (From w In listWarehouse Where w.Code = _store.Split("-").ElementAt(0).Trim() Select w).FirstOrDefault()
                If warehouse IsNot Nothing Then
                    INDTxtStore.EditValue = warehouse.Id
                End If
            End Using
            'INDTxtStore.Text = value
        End Set
    End Property
    ''' <summary>
    ''' codigo del centro de atencion
    ''' </summary>
    ''' <remarks></remarks>
    Dim _careCenter As String
    Public WriteOnly Property CareCenter As String
        Set(value As String)
            _careCenter = value
        End Set
    End Property
    ''' <summary>
    ''' origen de la devolucion
    ''' </summary>
    ''' <remarks></remarks>
    Dim _devolutionOrigin As Char
    Public WriteOnly Property DevolutionOrigin As Char
        Set(value As Char)
            _devolutionOrigin = value
            If value = "2" Then
                INDSleDevolutionOrigin.EditValue = True
            Else
                INDSleDevolutionOrigin.EditValue = False
            End If
        End Set
    End Property

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

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IDashBoardPharmacyDetail.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public ReadOnly Property MyTag As Object Implements IDashBoardPharmacyDetail.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public Property Sequence As Domain.Entities.InventorySequence Implements IDashBoardPharmacyDetail.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As Domain.Entities.InventorySequence)
            _sequence = value
            Me.DicSequense.Clear()
            For Each seq As Domain.Entities.InventorySequenceDetail In Me._sequence.InventorySequenceDetail
                Me.DicSequense.Add(seq.Id, New List(Of String))
            Next
        End Set
    End Property
#End Region

#Region "METHODS"
    ''' <summary>
    ''' metodo para pintar la informacion en el control de usuario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getInfoDashBoardPharmacy() As Tuple(Of String, String, Date?)
        Dim patientPrint = _patientCode + " - " + _patientName
        Return New Tuple(Of String, String, Date?)(patientPrint, _admission, _birthDay)
    End Function

    ''' <summary>
    ''' metodo que obtiene el servicio seleccionado para aplicar a procedimiento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnSelectProcedure(sender As Object, e As SelectProcedureEventArgs)
        If e.StatusResult = True Then
            applyProcedureId = e.ApplyProcedureId
            codeNameApplyProcedure = e.CodeNameApplyProcedure
        End If
    End Sub

    ''' <summary>
    ''' metodo que asigna valores cuando se han seleccionado cantidades a entregar desde el popup de CUM
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnGetCUM(sender As Object, e As GetCUMReturnEventArgs)
        Dim rowDetail = DirectCast(INDGvDashboardPharmacyDetail.GetFocusedRow(), ViewDashboardPharmacyDetailDevolution)
        rowDetail.ListPharmaceuticalDispensingDetailBatchSerial = Nothing
        rowDetail.ListPharmaceuticalDispensingDetailBatchSerial = e.ListPharmaceuticalDispensingDetailBatchSerial
        If rowDetail.ListPharmaceuticalDispensingDetailBatchSerial.Count > 0 Then
            rowDetail.QuantityReceived = e.ListPharmaceuticalDispensingDetailBatchSerial.Sum(Function(x) x.DevolutionQuantity)
            rowDetail.Action = 1
        Else
            rowDetail.QuantityReceived = 0
            rowDetail.Action = Nothing
        End If

        INDGcDashboardPharmacyDetail.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Obtiene el id del detalle de secuencia por el prefijo seleccionado
    ''' </summary>
    ''' <param name="prefix">Prefijo a buscar</param>
    ''' <returns>Id del detalle de secuencia</returns>
    Private Function GetIdSequenceByPrefix(ByVal prefix As String) As Int64
        If Me._sequence IsNot Nothing AndAlso Me._sequence.InventorySequenceDetail IsNot Nothing AndAlso Me._sequence.InventorySequenceDetail.Any(Function(d) d.Prefix IsNot Nothing AndAlso d.Prefix.Trim().Equals(prefix)) Then
            Return Me._sequence.InventorySequenceDetail.Where(Function(d) d.Prefix IsNot Nothing AndAlso d.Prefix.Trim().Equals(prefix)).FirstOrDefault().Id
        Else
            Return 0
        End If
    End Function

    ''' <summary>
    ''' metodo para obtener una secuencia numerica
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub GetNewSequence()
        If Sequence IsNot Nothing AndAlso Sequence.Id > 0 Then
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.InventorySequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                Dim res = (From ou As InventorySequenceDetail In Me._sequence.InventorySequenceDetail Where ou.OperatingUnit.Id = Me._idOperativeUnit Select ou).ToList()
                If res IsNot Nothing AndAlso res.Count > 0 Then
                    Me._idCurrentSequence = res(0).Id
                Else
                    If Me._sequence?.InventorySequenceDetail Is Nothing OrElse Not Me._sequence?.InventorySequenceDetail?.Any() Then
                        Mensaje(EeventViewerImages.Advertencia) = "La devolución de  dispensación no tiene parametrizada la secuencia numerica"
                        Exit Sub
                    End If
                    Me._idCurrentSequence = Me._sequence.InventorySequenceDetail(0).Id
                End If
            End If
            If Not Me._sequence.Sequential Then
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(Me._idCurrentSequence).Count > 0 Then
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Me.SaveAndConfirmObligatory()
                    Else
                        Using model As New MBlockRecordAndSequense(Me.Tag)
                            Me.DicSequense(Me._idCurrentSequence) = Await model.GetNumericSequenseGroup(Me._idCurrentSequence)
                        End Using
                        If Me.DicSequense(Me._idCurrentSequence) IsNot Nothing AndAlso Me.DicSequense(Me._idCurrentSequence).Count > 0 Then
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                            Me.SaveAndConfirmObligatory()
                        Else
                            Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else

                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Me.SaveAndConfirmObligatory()
                End If
            Else
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                Me.SaveAndConfirmObligatory()
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
        End If
    End Sub

    ''' <summary>
    ''' si tiene permisos de guardar y confirmar, se muestra
    ''' </summary>
    Private Sub SaveAndConfirmObligatory()
        If Not Me.BarraBotones.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.GuardaryConfirmar)) Then
            Exit Sub
        End If
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GuardarConfirmar) = Not Me.BarraBotones.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.GuardaryConfirmar))
    End Sub

    ''' <summary>
    ''' metodo para cargar manualmente las cantidades que se van a entregar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ManualDelivery()
        Dim errors As New StringBuilder
        For Each item In listDashboardPharmacyDetail.FindAll(Function(x) x.QuantityReceived < x.CantidadPendiente)
            Using model As New MDashBoardPharmacy(Me.Tag)
                Dim listPharmaceuticalDispensingDetailBatchSerialTmp = model.ListPharmaceuticalDispensingDetailBatchSerial(item.Ingreso, item.UFUCODIGO.Trim(), item.CODPRODUC.Trim(), item.Tipo)
                If listPharmaceuticalDispensingDetailBatchSerialTmp.Count = 0 Then
                    errors.AppendLine("No se encontro cantidad para devolver en el producto " + item.CODPRODUC + "-" + item.Producto)
                    Continue For
                End If
                Dim listPharmaceuticalDispensingDetailBatchSerial As New List(Of PharmaceuticalDispensingDetailBatchSerial)
                'Entregar manual segun el inventario fisico
                Dim outStandingQuantity = If(Not {5, 6, 7}.Contains(_ORIDEVMED), Math.Min(item.CantidadPendiente, item.CantidadFisico), item.CantidadPendiente)

                'Ordenamos la lista priorizando los almacenes de tipo custodia
                Dim listOrdered As List(Of PharmaceuticalDispensingDetailBatchSerial)

                If item.Custody Then
                    Dim conCustodia = listPharmaceuticalDispensingDetailBatchSerialTmp _
                                        .Where(Function(x) x.PhysicalInventoryCustodyId IsNot Nothing)
                    Dim sinCustodia = listPharmaceuticalDispensingDetailBatchSerialTmp _
                                        .Where(Function(x) x.PhysicalInventoryCustodyId Is Nothing)

                    listOrdered = conCustodia.Concat(sinCustodia).ToList()
                Else
                    listOrdered = listPharmaceuticalDispensingDetailBatchSerialTmp
                End If

                For Each itemDevolution In listOrdered
                    If itemDevolution.OutstandingQuantity >= outStandingQuantity Then
                        item.QuantityReceived += outStandingQuantity
                        itemDevolution.DevolutionQuantity = outStandingQuantity
                        listPharmaceuticalDispensingDetailBatchSerial.Add(itemDevolution)
                        Exit For
                    Else
                        item.QuantityReceived = itemDevolution.OutstandingQuantity
                        itemDevolution.DevolutionQuantity = itemDevolution.OutstandingQuantity
                        outStandingQuantity -= itemDevolution.OutstandingQuantity
                        listPharmaceuticalDispensingDetailBatchSerial.Add(itemDevolution)
                    End If
                Next
                item.ListPharmaceuticalDispensingDetailBatchSerial = listPharmaceuticalDispensingDetailBatchSerial
                item.QuantityReceived = item.ListPharmaceuticalDispensingDetailBatchSerial.Sum(Function(x) x.DevolutionQuantity)
            End Using
        Next
        INDGcDashboardPharmacyDetail.RefreshDataSource()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
        End If
    End Sub

    Private Sub ClosePopupInfo(sender As Object, e As EventArgs)
        INDMeCUM.Focus()
    End Sub
#End Region

#Region "CRUD"
    Public Sub Buscar() Implements ICrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer

    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If BarraBotones.OperatingUnitValue = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se ha seleccionado una unidad operativa"
            Exit Sub
        End If
        Dim errors As New StringBuilder
        If INDTxtStore.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Se debe seleccionar un almacen "
            Exit Sub
        End If
        Dim pharmaceuticalDispensingDevolution As PharmaceuticalDispensingDevolution
        Dim listDevolution = listDashboardPharmacyDetail.FindAll(Function(x) x.QuantityReceived > 0)
        Dim listPharmaceuticalDispensingDevolution As List(Of PharmaceuticalDispensingDevolution) = Nothing
        If listDevolution.Count > 0 Then
            INDMeCUM.Enabled = False
            AsyncLoader(True)
            listPharmaceuticalDispensingDevolution = New List(Of PharmaceuticalDispensingDevolution)

            pharmaceuticalDispensingDevolution = New PharmaceuticalDispensingDevolution
            With pharmaceuticalDispensingDevolution
                .OperatingUnitId = BarraBotones.OperatingUnitValue
                .DocumentDate = GetDateServer()
                .WarehouseId = 0
                .CodeNameWarehouse = Me.INDTxtStore.Text.Split(" - ")(0).Trim()
                .AdmissionNumber = _admission
                .Observation = String.Empty
                .CodePatient = _patientCode.Trim()
                .NamePatient = _patientName.Trim()
                .CareCenterCode = listDevolution(0).CODCENATE
                .FunctionUnitCode = listDevolution(0).UFUCODIGO
                .DevolutionOrigin = _devolutionOrigin
                .ConsecutiveCrystal = listDevolution(0).Consecutivo
                .Status = 2
            End With
            For Each item In listDevolution
                For Each itemBatch In item.ListPharmaceuticalDispensingDetailBatchSerial
                    Dim pharmaceuticalDispensingDevolutionDetail As New PharmaceuticalDispensingDevolutionDetail
                    pharmaceuticalDispensingDevolutionDetail.OrderedHealthProfessionalCode = item.Medico.Split("-").ElementAt(0).Trim()
                    pharmaceuticalDispensingDevolutionDetail.CodeProduct = item.CODPRODUC.Trim()
                    pharmaceuticalDispensingDevolutionDetail.CodeNameProduct = item.Producto.Trim()
                    With pharmaceuticalDispensingDevolutionDetail
                        .CodePharmaceuticalDispensing = itemBatch.CodePharmaceuticalDispensing
                        .PharmaceuticalDispensingDetailBatchSerialId = itemBatch.Id
                        .PharmaceuticalDispensingDetailId = itemBatch.PharmaceuticalDispensingDetailId
                        .Quantity = itemBatch.DevolutionQuantity
                        .ProductId = itemBatch.ProductId
                        .EntityId = item.EntityId
                        .EntityName = item.EntityName
                        .HCDEVMEDDId = item.HCDEVMEDDId
                    End With
                    pharmaceuticalDispensingDevolution.PharmaceuticalDispensingDevolutionDetail.Add(pharmaceuticalDispensingDevolutionDetail)
                Next
            Next
            listPharmaceuticalDispensingDevolution.Add(pharmaceuticalDispensingDevolution)
        End If
        If errors.Length = 0 Then
            Try
                Using model As New MDashBoardPharmacy(MyTag)
                    Dim listAnnular = listDashboardPharmacyDetail.FindAll(Function(x) x.Action IsNot Nothing AndAlso x.Action = 2)
                    If listPharmaceuticalDispensingDevolution IsNot Nothing OrElse listAnnular.Count > 0 Then
                        Dim result = Await model.SaveDashBoardPharmacyDevolution(listPharmaceuticalDispensingDevolution, listAnnular, _idCurrentSequence)
                        INDMeCUM.Enabled = True
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            If pharmaceuticalDispensingDevolution IsNot Nothing Then
                                pharmaceuticalDispensingDevolution.CodeNameWarehouse = Me.INDTxtStore.Text
                                pharmaceuticalDispensingDevolution.Code = result.ObjectEmbbeded
                                pharmaceuticalDispensingDevolution.CodeNameUser = result.MessageResult.ElementAt(0)
                                pharmaceuticalDispensingDevolution.CreationUser = indigo.UserIndigo + " - " + indigo.UserIndigoName
                            End If
                            Mensaje(EeventViewerImages.Informacion) = result.Message
                            AsyncLoader(False)
                            Me.DialogResult = System.Windows.Forms.DialogResult.OK
                            RaiseEvent DashBoardPharmacyReturnSuccessEventArgs(Nothing, New DashBoardPharmacyEventArgs With {.PharmaceuticalDispensingDevolution = pharmaceuticalDispensingDevolution})
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = result.Message
                            AsyncLoader(False)
                            listPharmaceuticalDispensingDevolution = New List(Of PharmaceuticalDispensingDevolution)
                        End If
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = "No se encontraron items para hacer la devolución"
                        AsyncLoader(False)
                    End If

                End Using
            Catch ex As Exception
                INDMeCUM.Enabled = True
                AsyncLoader(False)
                Throw ex
            End Try
        Else
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            listPharmaceuticalDispensingDevolution = New List(Of PharmaceuticalDispensingDevolution)
            AsyncLoader(False)
        End If

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    Public Sub Nuevo() Implements ICrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch

    End Sub
#End Region

#Region "HANDLES"
#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ctrTmp = Nothing
        listDashboardPharmacyDetail = Nothing
        applyProcedureId = Nothing
        codeNameApplyProcedure = Nothing
        _presenter = Nothing
        _sequence = Nothing
        _idOperativeUnit = Nothing
        _idCurrentSequence = Nothing
        dictionaryPharmaceuticalDispensingDetailBatchSerialCrystalProduct = Nothing
        dictionaryProduct = Nothing
        dictionaryAtcCode = Nothing
        dictionarySuppliedCode = Nothing
    End Sub

    Private Sub FrmPopUpDashBoardPharmacy_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        BarraBotones.StatusRecordVisible = True
        IndigoGridView1.MoreInfoColunmns(INDGvDashboardPharmacyDetail)
        '''se agrega la columna de anular y el contexmenu de anular
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Annular)
        IndigoGridView1.SetListAcction(INDGvDashboardPharmacyDetail, ListActions)
        INDGvDashboardPharmacyDetail.Columns.ColumnByName("colActions").Caption = "Opciones"
        INDGvDashboardPharmacyDetail.Columns.ColumnByName("colActions").Width = 80
        _presenter = New PDashBoardPharmacyDetail(Me)
        _presenter.GetSequence()
        AddHandler ctrTmp.ClosePopupInfo, AddressOf ClosePopupInfo
    End Sub
#End Region

#Region "Shown"
    Private Sub FrmPopUpDashBoardPharmacy_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Execute()
    End Sub
#End Region

#Region "FormClosing"

    Private Sub FrmPopUpDashBoardPharmacyReturn_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.DialogResult = DialogResult.OK Then
            Exit Sub
        End If

        Dim listDevolution = listDashboardPharmacyDetail.FindAll(Function(x) x.QuantityReceived > 0)

        If listDevolution.Count > 0 Then
            If Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub

#End Region

#Region "ContexMenuActions"

    Private Async Sub IndigoGridView1_Click_ButtonActionAsync(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If INDGvDashboardPharmacyDetail.GetSelectedRows().Count = 1 Then
            '''se obtiene la informacion del item a anular
            Dim rowDetail = DirectCast(INDGvDashboardPharmacyDetail.GetFocusedRow(), ViewDashboardPharmacyDetailDevolution)

            'Se obtiene si la forma en como se accede a este evento es con buttonAction o contextMenuActions
            Dim tagGrid As String = ""
            If sender.GetType() Is GetType(DevExpress.XtraEditors.SimpleButton) Or sender.GetType() Is GetType(DevExpress.XtraBars.BarButtonItem) Then
                tagGrid = sender.Tag.ToString()
            Else
                tagGrid = sender.Text
            End If

            '''case para las acciones del item
            Select Case tagGrid
                Case "Annular"
                    '''se ejecuta el popup de la razon de anulaciion
                    listDashboardPharmacyDetail = New List(Of ViewDashboardPharmacyDetailDevolution)
                    Dim ResultAnnulmentReason = ShowAnnulmentReasonsPopup()

                    If ResultAnnulmentReason?.StateResult Then
                        AsyncLoader(True)

                        ''se obtiene informacion de la fila seleccionada
                        Dim rowData = DirectCast(INDGvDashboardPharmacyDetail.GetFocusedRow(), ViewDashboardPharmacyDetailDevolution)
                        Using model As New MDashBoardPharmacy(Me.Tag)

                            '''creo el objeto con la informacion necesaria para su anulacion individual
                            listDashboardPharmacyDetail.Add(New Domain.Crystal.Entities.ViewDashboardPharmacyDetailDevolution With
                                    {
                                        .Consecutivo = rowData.Consecutivo,
                                        .CodigoPacienteDevolucion = rowData.CodigoPacienteDevolucion,
                                        .Ingreso = rowData.Ingreso,
                                        .CODCENATE = rowData.CODCENATE,
                                        .UFUCODIGO = rowData.UFUCODIGO,
                                        .CODPROSAL = rowData.CODPROSAL,
                                        .CODPRODUC = rowData.CODPRODUC,
                                        .CantidadDevuelta = rowData.CantidadDevuelta,
                                        .PROESTADO = rowData.PROESTADO,
                                        .FECRESGIS = rowData.FECRESGIS,
                                        .CODUSUARI = rowData.CODUSUARI,
                                        .NOPOS = rowData.NOPOS,
                                        .Entidad = rowData.Entidad,
                                        .Producto = rowData.Producto,
                                        .ContratoPlan = rowData.ContratoPlan,
                                        .Tipo = IIf(rowData.Custody, "4", rowData.Tipo),
                                        .CantidadPendiente = rowData.CantidadPendiente,
                                        .Medico = rowData.Medico,
                                        .Especialidad = rowData.Especialidad,
                                        .EntityId = rowData.EntityId,
                                        .EntityName = rowData.EntityName,
                                        .CantidadFisico = rowData.CantidadFisico,
                                        .FinalProductId = rowData.FinalProductId,
                                        .FinalProductCode = rowData.FinalProductCode,
                                        .FinalProductName = rowData.FinalProductName,
                                        .HCDEVMEDDId = rowData.HCDEVMEDDId,
                                        .BatchCode = rowData.BatchCode,
                                        .Custody = rowData.Custody,
                                        .HCMOANULBId = ResultAnnulmentReason?.ObjectEmbbeded?.IdHCMOANULB,
                                        .Description = ResultAnnulmentReason?.ObjectEmbbeded?.Description,
                                        .Action = 2
                                     })

                            Dim result = Await model.SaveDashBoardPharmacyDevolution(Nothing, listDashboardPharmacyDetail, 0)
                            If result.StateResult Then
                                Mensaje(EeventViewerImages.Informacion) = result.Message.Trim()
                                '''evento que recarga la informacion despues de la anulacion
                                RaiseEvent DashBoardPharmacyReturnSuccessEventArgs(Nothing, New DashBoardPharmacyEventArgs)
                            Else
                                Mensaje(EeventViewerImages.Advertencia) = result.Message
                            End If
                        End Using

                        AsyncLoader(False)
                    End If
            End Select
        End If
        INDGcDashboardPharmacyDetail.RefreshDataSource()
    End Sub

#End Region

#Region "Click"
    Private Sub INDRptBteCUM_Click(sender As Object, e As EventArgs) Handles INDRptBteCUM.Click
        Me.Cursor = ChangeCursorIndigo()
        Using frm As New PopupCUMReturn
            Dim rowDetail = DirectCast(INDGvDashboardPharmacyDetail.GetFocusedRow(), ViewDashboardPharmacyDetailDevolution)
            frm.StartPosition = FormStartPosition.CenterParent

            frm.Product = If(rowDetail.FinalProductId.HasValue, $"{rowDetail.FinalProductCode} - {rowDetail.FinalProductName}", rowDetail.Producto)
            frm.CodeProduct = rowDetail.CODPRODUC

            frm.QuantityDeliver = rowDetail.CantidadPendiente
            frm.CantidadFisico = rowDetail.CantidadFisico
            frm.ListPharmaceuticalDispensingDetailBatchSerial = rowDetail.ListPharmaceuticalDispensingDetailBatchSerial
            frm.AdmissionNumber = rowDetail.Ingreso
            frm.ProductType = rowDetail.Tipo
            frm.TopMost = True
            frm.FunctionalUnitCode = rowDetail.UFUCODIGO.Trim()
            frm.BatchCode = rowDetail.BatchCode
            frm.ORIDEVMEDCUM = _ORIDEVMED
            AddHandler frm.GetCUMReturn, AddressOf ReturnGetCUM
            Dim trasparent As New FrmTransparent(frm, False)
            Me.Cursor = Cursors.Default
            trasparent.ShowDialog(Me)
            INDMeCUM.Focus()
        End Using
    End Sub
#End Region

#Region "KeyDown"


    Private Sub INDMeCUM_KeyDown(sender As Object, e As KeyEventArgs) Handles INDMeCUM.KeyDown
        If e.KeyCode = Keys.Enter Then
            If INDMeCUM.EditValue Is Nothing Then
                Exit Sub
            End If

            Dim listPharmaceuticalDispensingDetailBatchSerialCrystalProduct As List(Of PharmaceuticalDispensingDetailBatchSerial)
            Dim result As ActionResult(Of List(Of PharmaceuticalDispensingDetailBatchSerial))

            Dim productATC = INDMeCUM.EditValue.ToString.ToUpper().Trim().Split("*IND*")

            If dictionaryPharmaceuticalDispensingDetailBatchSerialCrystalProduct.ContainsKey(productATC.ElementAt(0)) Then
                listPharmaceuticalDispensingDetailBatchSerialCrystalProduct = dictionaryPharmaceuticalDispensingDetailBatchSerialCrystalProduct(productATC.ElementAt(0))
            Else
                Using model As New MDashBoardPharmacy(Me.Tag)
                    If productATC.Length > 1 Then
                        result = model.GetPharmaceuticalDispensingDetailBatchSerialDevolutionBarCode(_admission, productATC.ElementAt(0), productATC.ElementAt(2))
                    Else
                        result = model.GetPharmaceuticalDispensingDetailBatchSerialDevolutionBarCode(_admission, productATC.ElementAt(0), "")
                    End If

                    If result.StateResult = False Then
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                        INDMeCUM.EditValue = Nothing
                        INDMeCUM.Focus()
                        e.SuppressKeyPress = True
                        Exit Sub
                    End If
                End Using

                listPharmaceuticalDispensingDetailBatchSerialCrystalProduct = result.ObjectEmbbeded
                dictionaryPharmaceuticalDispensingDetailBatchSerialCrystalProduct.Add(productATC.ElementAt(0), listPharmaceuticalDispensingDetailBatchSerialCrystalProduct)
            End If



            Dim ATCProductCode As String
            Dim product As Infrastructure.Data.Xpo.InventoryRepository.InventoryProductXpo

            If dictionaryProduct.ContainsKey(listPharmaceuticalDispensingDetailBatchSerialCrystalProduct.ElementAt(0).ProductId) Then
                product = dictionaryProduct(listPharmaceuticalDispensingDetailBatchSerialCrystalProduct.ElementAt(0).ProductId)
            Else
                Using model As New MDashBoardPharmacy(MyTag)
                    product = model.GetInventoryProductXpo(listPharmaceuticalDispensingDetailBatchSerialCrystalProduct.ElementAt(0).ProductId)
                    dictionaryProduct.Add(listPharmaceuticalDispensingDetailBatchSerialCrystalProduct.ElementAt(0).ProductId, product)
                End Using
            End If

            Dim ATCTemp As ViewDashboardPharmacyDetailDevolution

            If product.ATCId IsNot Nothing Then
                If dictionaryAtcCode.ContainsKey(product.ATCId.Id) Then
                    ATCProductCode = dictionaryAtcCode(product.ATCId.Id)
                Else
                    Using model As New MDashBoardPharmacy(MyTag)
                        ATCProductCode = model.GetATCXpo(product.ATCId.Id).Code.Trim()
                        dictionaryAtcCode.Add(product.ATCId.Id, ATCProductCode)
                    End Using
                End If
            ElseIf product.SupplieId IsNot Nothing Then
                If dictionarySuppliedCode.ContainsKey(product.SupplieId.Id) Then
                    ATCProductCode = dictionarySuppliedCode(product.SupplieId.Id)
                Else
                    Using model As New MDashBoardPharmacy(MyTag)
                        ATCProductCode = model.GetInventorySuppliedXpo(product.SupplieId.Id).Code.Trim()
                        dictionarySuppliedCode.Add(product.SupplieId.Id, ATCProductCode)
                    End Using
                End If
            Else
                ATCProductCode = product.Code
            End If
            ATCTemp = listDashboardPharmacyDetail.Find(Function(x) x.CODPRODUC.Trim() = ATCProductCode)

            If ATCTemp IsNot Nothing Then

                If ATCTemp.ListPharmaceuticalDispensingDetailBatchSerial IsNot Nothing AndAlso ATCTemp.ListPharmaceuticalDispensingDetailBatchSerial.Count > 0 Then
                    ' Validar que no se exceda la cantidad pendiente
                    Dim quantityDevolution = ATCTemp.ListPharmaceuticalDispensingDetailBatchSerial.Sum(Function(x) x.DevolutionQuantity) + 1
                    If quantityDevolution > ATCTemp.CantidadPendiente Then
                        Mensaje(EeventViewerImages.Advertencia) = "No se puede agregar mas cantidad del producto " + ATCTemp.Producto + " porque supera la cantidad solicitada"
                        INDMeCUM.EditValue = Nothing
                        INDMeCUM.Focus()
                        e.SuppressKeyPress = True
                        Exit Sub
                    End If

                    ' Obtener los IDs de lotes ya asignados
                    Dim listAssignedIds = (From p In ATCTemp.ListPharmaceuticalDispensingDetailBatchSerial Select p.Id).ToList()

                    ' PRIORIDAD 1: Buscar lote del PRODUCTO ESCANEADO ya asignado con capacidad disponible
                    Dim existingWithCapacity = ATCTemp.ListPharmaceuticalDispensingDetailBatchSerial.FirstOrDefault(Function(x) _
                        listPharmaceuticalDispensingDetailBatchSerialCrystalProduct.Any(Function(p) p.Id = x.Id) AndAlso
                        x.DevolutionQuantity < x.OutstandingQuantity)

                    ' PRIORIDAD 2: Buscar lote NUEVO del PRODUCTO ESCANEADO que no esté asignado
                    Dim availableNew = listPharmaceuticalDispensingDetailBatchSerialCrystalProduct.FirstOrDefault(Function(p) _
                        Not listAssignedIds.Contains(p.Id) AndAlso p.OutstandingQuantity > 0)

                    If existingWithCapacity IsNot Nothing Then
                        ' CASO 1: Incrementar lote ya asignado del producto escaneado
                        existingWithCapacity.DevolutionQuantity += 1
                        ATCTemp.QuantityReceived += 1
                    ElseIf availableNew IsNot Nothing Then
                        ' CASO 2: Agregar nuevo lote del producto escaneado
                        availableNew.DevolutionQuantity = 1
                        ATCTemp.QuantityReceived += 1
                        ATCTemp.ListPharmaceuticalDispensingDetailBatchSerial.Add(availableNew)
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = "No existe mas cantidades para devolver del producto escaneado: " + ATCTemp.Producto
                        INDMeCUM.EditValue = Nothing
                        INDMeCUM.Focus()
                        e.SuppressKeyPress = True
                        Exit Sub
                    End If
                Else
                    ' Primera vez que se agrega un lote para este ATC
                    ' Buscar el primer lote disponible del producto escaneado
                    Dim firstAvailable = listPharmaceuticalDispensingDetailBatchSerialCrystalProduct.FirstOrDefault(Function(x) x.OutstandingQuantity > 0)

                    If firstAvailable IsNot Nothing Then
                        ATCTemp.QuantityReceived = 1
                        firstAvailable.DevolutionQuantity = 1
                        ATCTemp.ListPharmaceuticalDispensingDetailBatchSerial = New List(Of PharmaceuticalDispensingDetailBatchSerial)
                        ATCTemp.ListPharmaceuticalDispensingDetailBatchSerial.Add(firstAvailable)
                        ATCTemp.Action = 1
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = "No existe cantidades disponibles para devolver del producto escaneado"
                        INDMeCUM.EditValue = Nothing
                        INDMeCUM.Focus()
                        e.SuppressKeyPress = True
                        Exit Sub
                    End If
                End If
                INDGcDashboardPharmacyDetail.RefreshDataSource()
                INDGvDashboardPharmacyDetail.ClearSelection()

                ''obtengo el item que se le asigno la cantidad para que se posicione en el
                Dim listTmp = INDGvDashboardPharmacyDetail.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of ViewDashboardPharmacyDetailDevolution)()

                Dim item = listTmp.Find(Function(x) x.Producto.Split(" - ")(0) = ATCTemp.Producto.Split(" - ")(0))

                INDGvDashboardPharmacyDetail.FocusedRowHandle = listTmp.IndexOf(item)

                INDGvDashboardPharmacyDetail.SelectRow(INDGvDashboardPharmacyDetail.FocusedRowHandle)

                INDMeCUM.EditValue = Nothing
                INDMeCUM.Focus()
                e.SuppressKeyPress = True
            Else
                Mensaje(EeventViewerImages.Advertencia) = "No se encontro el producto en el listado"
                INDMeCUM.EditValue = Nothing
                INDMeCUM.Focus()
                e.SuppressKeyPress = True
            End If
        End If
    End Sub
#End Region

#End Region

#Region "BAR BUTTONS"
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
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso Me._sequence IsNot Nothing AndAlso Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.InventorySequenceDetail IsNot Nothing Then
            If Me._sequence.InventorySequenceDetail.Any(Function(S) S.IdOperatingUnit = operatingUnit.Id) Then
                Me._idCurrentSequence = Me._sequence.InventorySequenceDetail.Where(Function(s) s.IdOperatingUnit = operatingUnit.Id).SingleOrDefault().Id
                _idOperativeUnit = BarraBotones.OperatingUnitValue
            Else
                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
            End If
        End If
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        Anulate()
    End Sub

    'Funcion utilizada para anular los productos
    Private Function Anulate()
        'Informacion del popup de la razon de anulzaion
        Dim ResultAnnulmentReason = ShowAnnulmentReasonsPopup()
        If ResultAnnulmentReason?.StateResult Then
            AsyncLoader(True)

            listDashboardPharmacyDetail.ForEach(Sub(item)
                                                    item.Action = 2
                                                    item.QuantityReceived = 0
                                                    item.HCMOANULBId = ResultAnnulmentReason?.ObjectEmbbeded?.IdHCMOANULB
                                                    item.Description = ResultAnnulmentReason?.ObjectEmbbeded?.Description
                                                End Sub)

            INDGcDashboardPharmacyDetail.RefreshDataSource()
            Guardar()
        End If
    End Function

    ''' <summary>
    ''' popup para determinar el motivo de la anulacion
    ''' </summary>
    Private Function ShowAnnulmentReasonsPopup() As ActionResult(Of Object)
        Using formulario As New FrmPopupObservations()
            formulario.Size = New System.Drawing.Size(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.41, System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height * 0.5)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.Text = ResourceManager.GetString("AnnularMessage")
            formulario.FilterType = 28
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            If transparent.ShowDialog(Me) = DialogResult.OK Then
                Dim AnnulmentReasons As Object = New ExpandoObject()
                AnnulmentReasons.IdHCMOANULB = formulario.IdHCMOANULB
                AnnulmentReasons.Description = formulario.Description

                'variable donde se almacena el mensaje que muestra la alerta
                Dim concatMessages As String

                'segun el tipo de filtro elejido asi se muestra el mensaje
                Select Case typeFilter
                    Case 1 'medicamentos
                        concatMessages = "Se va a realizar la anulación de los medicamentos filtrado, ¿está seguro que desea anular?"
                        If MessageIndigo.Show(concatMessages, MessageType.Warning, Me.Text, Botones.AceptarCancelar, "", {"Si", "No"}) <> DialogResult.Yes Then
                            Return New ActionResult(Of Object) With {.StateResult = False}
                        End If
                    Case 2 'insumos
                        concatMessages = "Se va a realizar la anulación de los insumos filtrado, ¿está seguro que desea anular?"
                        If MessageIndigo.Show(concatMessages, MessageType.Warning, Me.Text, Botones.AceptarCancelar, "", {"Si", "No"}) <> DialogResult.Yes Then
                            Return New ActionResult(Of Object) With {.StateResult = False}
                        End If
                    Case 3 'medicamentos tipos insumos
                        concatMessages = "Se va a realizar la anulación de los Medicamentos tipo insumo filtrado, ¿está seguro que desea anular?"
                        If MessageIndigo.Show(concatMessages, MessageType.Warning, Me.Text, Botones.AceptarCancelar, "", {"Si", "No"}) <> DialogResult.Yes Then
                            Return New ActionResult(Of Object) With {.StateResult = False}
                        End If
                    Case Else 'en caso de elegir todos o no seleccionar ninguno no se muestra la alerta
                End Select
                Return New ActionResult(Of Object) With {.StateResult = True, .ObjectEmbbeded = AnnulmentReasons}
            Else
                Return New ActionResult(Of Object) With {.StateResult = False}
            End If
        End Using
    End Function

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_EntregaManual() Handles BarraBotones.Click_EntregaManual
        Me.Cursor = ChangeCursorIndigo()
        AsyncLoader(True)
        ManualDelivery()
        AsyncLoader(False)
        Me.Cursor = Cursors.Default
        INDMeCUM.Focus()
    End Sub
#End Region

    Private Function GetDetail() As Task
        Return Task.Factory.StartNew(Sub()
                                         Using model As New MDashBoardPharmacy(Me.Tag)
                                             listDashboardPharmacyDetail = New List(Of ViewDashboardPharmacyDetailDevolution)
                                             Dim collect As XPCollection(Of Infrastructure.Data.Xpo.CrystalRepository.ViewDashboardPharmacyDetailDevolution)
                                             If String.IsNullOrEmpty(typeFilter) Then
                                                 collect = model.ListDashBoardPharmacyDetailDevolutionCollection(_consecutive, _patientCode, _admission)
                                             Else
                                                 collect = model.ListDashBoardPharmacyDetailDevolutionByTypeCollection(_consecutive, _patientCode, _admission, typeFilter)
                                             End If
                                             For Each item In collect
                                                 Dim detail As New Domain.Crystal.Entities.ViewDashboardPharmacyDetailDevolution
                                                 With detail
                                                     .Consecutivo = item.Consecutivo
                                                     .CodigoPacienteDevolucion = item.CodigoPacienteDevolucion
                                                     .Ingreso = item.Ingreso
                                                     .CODCENATE = item.CODCENATE
                                                     .UFUCODIGO = item.UFUCODIGO
                                                     .CODPROSAL = item.CODPROSAL
                                                     .CODPRODUC = item.CODPRODUC
                                                     .CantidadDevuelta = item.CantidadDevuelta
                                                     .PROESTADO = item.PROESTADO
                                                     .FECRESGIS = item.FECRESGIS
                                                     .CODUSUARI = item.CODUSUARI
                                                     .NOPOS = item.NOPOS
                                                     .Entidad = item.Entidad
                                                     .Producto = item.Producto
                                                     .ContratoPlan = item.ContratoPlan
                                                     .Tipo = IIf(item.Custody, "4", item.Tipo)
                                                     .CantidadPendiente = item.CantidadPendiente
                                                     .Medico = item.Medico
                                                     .Especialidad = item.Especialidad
                                                     .EntityId = item.EntityId
                                                     .EntityName = item.EntityName
                                                     .CantidadFisico = item.CantidadFisico
                                                     .FinalProductId = item.FinalProductId
                                                     .FinalProductCode = item.FinalProductCode
                                                     .FinalProductName = item.FinalProductName
                                                     .HCDEVMEDDId = item.HCDEVMEDDId
                                                     .BatchCode = item.BatchCode
                                                     .Custody = item.Custody
                                                 End With
                                                 listDashboardPharmacyDetail.Add(detail)
                                             Next

                                             'listDashboardPharmacyDetail = model.ListDashboardPharmacyDetailDevolution(_consecutive, _patientCode, _admission)
                                         End Using

                                     End Sub)
    End Function

    Public Async Sub Execute()
        dictionaryPharmaceuticalDispensingDetailBatchSerialCrystalProduct = New Dictionary(Of String, List(Of PharmaceuticalDispensingDetailBatchSerial))
        dictionaryProduct = New Dictionary(Of Integer, Infrastructure.Data.Xpo.InventoryRepository.InventoryProductXpo)
        dictionaryAtcCode = New Dictionary(Of Integer, String)
        dictionarySuppliedCode = New Dictionary(Of Integer, String)
        INDGvDashboardPharmacyDetail.ShowLoadingPanel()
        ctrTmp.PopupContainerControl = INDPccMoreInfo
        ctrTmp.PrintInfo()

        If {5, 6, 7}.Contains(_ORIDEVMED) Then
            INDGvDashboardPharmacyDetail.Columns("CantidadFisico").Visible = False
        Else
            INDGvDashboardPharmacyDetail.Columns("CantidadFisico").Visible = True
        End If

        Await GetDetail()
        INDGcDashboardPharmacyDetail.DataSource = listDashboardPharmacyDetail

        Using model As New MDashBoardPharmacy(Me.Tag)
            Dim stay = model.GetStayType(_patientCode, _admission)
            If stay Is Nothing OrElse stay Is String.Empty Then
                INDTxtStayType.Text = "N/A"
            Else
                INDTxtStayType.Text = stay.Trim()
            End If
        End Using

        Using model As New MPatient(Me.Tag)
            Dim result = Await model.GetPacientByIdentification(_patientCode.Trim())
            Dim patient = result.ObjectEmbbeded
            If patient.GENCONENTITY IsNot Nothing Then
                Using modelHealth As New Presentation.Contract.MVP.MHealthAdministrator(Me.Tag)
                    Dim healthTmp = modelHealth.GetHealthAdministratorByIdSimple(patient.GENCONENTITY).ObjectEmbbeded
                    If healthTmp IsNot Nothing AndAlso healthTmp.Id > 0 Then
                        INDTxtEntity.Text = healthTmp.Code + " - " + healthTmp.Name
                    Else
                        INDTxtEntity.Text = String.Empty
                    End If
                End Using
            Else
                INDTxtEntity.Text = String.Empty
            End If
            INDTxtBirthday.Text = Utils.AgeToString(patient.IPFECNACI)
            If patient.IPSEXOPAC = 1 Then
                INDTxtGenus.Text = "Masculino"
            Else
                INDTxtGenus.Text = "Femenino"
            End If
            INDTxtBloodGroup.Text = patient.IPGRUPSAN
            INDTxtAddress.Text = patient.IPDIRECCI
            INDTxtPhone.Text = patient.IPTELEFON
        End Using
        INDGvDashboardPharmacyDetail.ExpandAllGroups()
        GetNewSequence()
        If BarraBotones.PermissionsForm.ContainsKey(75) Then
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.EntregaManual) = False
            ColCUM.OptionsColumn.AllowEdit = True
            ColCUM.OptionsColumn.AllowFocus = True
        Else
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.EntregaManual) = True
            ColCUM.OptionsColumn.AllowEdit = False
            ColCUM.OptionsColumn.AllowFocus = False
        End If
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = False
        INDGvDashboardPharmacyDetail.HideLoadingPanel()
        INDMeCUM.Focus()
    End Sub

    Private Sub INDGvDashboardPharmacyDetail_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvDashboardPharmacyDetail.CustomUnboundColumnData
        Dim view As GridView = sender
        Dim row As ViewDashboardPharmacyDetailDevolution = e.Row
        If e.Column.Name = INDColProduct.Name AndAlso e.IsGetData AndAlso row IsNot Nothing Then
            If row.FinalProductId.HasValue Then
                e.Value = $"{row.FinalProductCode} - {row.FinalProductName}"
            Else
                e.Value = row.Producto
            End If
        End If
    End Sub

    Private Sub btnKardex_Click(sender As Object, e As EventArgs) Handles btnKardex.Click
        Using Kardex As New IndigoComponents.frmHCKardexProductos(_patientCode, _admission, _careCenter, _functionalUnitCode, IndigoComponents.frmHCKardexProductos.eTipoProducto.Medicamentos, IndigoComponents.frmHCKardexProductos.eTipoKardex.Todos)
           Kardex.ShowDialog()
           Kardex.Dispose()
        End Using
    End Sub
End Class