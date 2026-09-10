'***********************************************************************
' Assembly         : Presentacion.Treasury
' Author           : Carlos Ernesto Córdoba
' Created          : 22-04-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Inventory.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Domain.Entities
Imports Domain.Base.Entities
Imports DevExpress.Xpo
Imports System.Drawing
Imports Presentation.Base.BaseClass
Imports Infrastructure.Data.Xpo.CommonRepository
Imports System.Text
Imports Presentation.Glosas
Imports Presentation.Maintenance
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Infrastructure.Data.Xpo
Imports DevExpress.XtraLayout.Utils

#End Region

Public Class FrmSupplyDevolution
    Implements ISupplyDevolution, ICustomizableForm

#Region "GLOBALS"
    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Inventory"
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Private _indigoSession As SessionValues
    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.InventorySequence
    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32
    ''' <summary>
    ''' Variable que representa la entidad de parametros
    ''' </summary>
    ''' <remarks></remarks>
    Dim _settingsInventory As SettingInventory
    ''' <summary>
    ''' Prefijo seleccionado
    ''' </summary>
    Private _prefixSelected As String
    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordInventory
    ''' <summary>
    ''' presenter de la remision
    ''' </summary>
    ''' <remarks></remarks>
    Private presenter As PSupplyDevolution
    ''' <summary>
    ''' objeto de la entidad de ingreso de crystal
    ''' </summary>
    ''' <remarks></remarks>
    Dim admission As Object
    ''' <summary>
    ''' listado del detalle de la dispensacion
    ''' </summary>
    ''' <remarks></remarks>
    Dim listPharmaceuticalDispensingDetailBatchSerial As List(Of PharmaceuticalDispensingDetailBatchSerial)
    ''' <summary>
    ''' listado del detalle de la devolucion
    ''' </summary>
    ''' <remarks></remarks>
    Dim listPharmaceuticalDispensingDevolutionDetail As List(Of PharmaceuticalDispensingDevolutionDetail)
    ''' <summary>
    ''' listado del detalle de la devolucion para procesar
    ''' </summary>
    ''' <remarks></remarks>
    Dim listPharmaceuticalDispensingDevolutionDetailToSend As List(Of PharmaceuticalDispensingDevolutionDetail)

    Dim pharmaceuticalDispensingDevolution As PharmaceuticalDispensingDevolution

    Dim _listDetailsTask As Task(Of List(Of PharmaceuticalDispensingDevolutionDetail))

    Private taskWait As Task

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer

    ''' <summary>
    ''' Permite saber si el almacen es virtual o no
    ''' </summary>
    Private VirtualStore As Boolean = False
#End Region

#Region "PROPERTIES"

    ''' <summary>
    ''' codigo del documento
    ''' </summary>
    Public Property Code As String Implements ISupplyDevolution.Code
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
    ''' fecha del documento
    ''' </summary>
    Public Property DocumentDate As Date Implements ISupplyDevolution.DocumentDate
        Get
            Return INDDteDate.EditValue
        End Get
        Set(value As Date)
            INDDteDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' detalle
    ''' </summary>
    Public Property Observation As String Implements ISupplyDevolution.Observation
        Get
            Return INDMeDetail.EditValue
        End Get
        Set(value As String)
            INDMeDetail.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' id del almacen
    ''' </summary>
    Public Property WarehouseId As Integer Implements ISupplyDevolution.WarehouseId
        Get
            Return INDSleWarehouse.EditValue
        End Get
        Set(value As Integer)
            INDSleWarehouse.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
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

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ISupplyDevolution.ActionsOnControls
        Set(value As Boolean)
            INDLcSupplyDevolution.BeginUpdate()
            INDBteCode.Enabled = Not value
            INDDteDate.Enabled = value
            INDSleWarehouse.Enabled = value
            INDSleAdmissionNumber.Enabled = value
            INDSleAdmissionNumber.Search.Enabled = value
            INDMeDetail.Enabled = value
            INDPceProducts.Enabled = value
            INDGcProducts.Enabled = value
            INDGcProductAddedAndEditing.Enabled = value
            INDBtnAddProduct.Enabled = False
            INDLcSupplyDevolution.EndUpdate()
            If value Then
                INDDteDate.Focus()
            Else
                INDBteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements ISupplyDevolution.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    Public ReadOnly Property MyTag As Object Implements ISupplyDevolution.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    ''' <value>
    ''' The sequense.
    ''' </value>
    Public Property Sequense As InventorySequence Implements ISupplyDevolution.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As InventorySequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.InventorySequenceDetail In Me._sequence.InventorySequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Datasource de Almacenes
    ''' </summary>
    Public Property WareHouseDatasource As DevExpress.Xpo.XPInstantFeedbackSource Implements ISupplyDevolution.WareHouseDatasource
        Get
            Return CType(INDSleWarehouse.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleWarehouse.Properties.DataSource = value
        End Set
    End Property


#End Region

#Region "CRUD"
    Public Sub Buscar() Implements IcrudBase.Buscar
        OpenSearch()
    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If Me.pharmaceuticalDispensingDevolution IsNot Nothing AndAlso Me.pharmaceuticalDispensingDevolution.Status < 3 Then
            If ValidateControls() = True Then
                If admission Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "Ingreso Vacio"
                    Exit Sub
                End If
                If Me.pharmaceuticalDispensingDevolution.Id = 0 Then
                    If listPharmaceuticalDispensingDevolutionDetailToSend Is Nothing OrElse listPharmaceuticalDispensingDevolutionDetailToSend.Count = 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = "Se debe agregar o modificar un producto para devolver"
                        Exit Sub
                    End If
                ElseIf listPharmaceuticalDispensingDevolutionDetailToSend.Where(Function(d) d.ChangeTracker.State = ObjectState.Deleted).ToList().Count = listPharmaceuticalDispensingDevolutionDetail.Count AndAlso Not listPharmaceuticalDispensingDevolutionDetailToSend.Any(Function(d) d.ChangeTracker.State = ObjectState.Added) Then
                    'Si voy a eliminar todo los detalles que ya estaban guardados
                    Mensaje(EeventViewerImages.Advertencia) = "La devolución no puede generarse sin productos"
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
            AssigningValues()
        End If

        Try
            Using model As New MSupplyDevolution(MyTag)
                AsyncLoader(True)
                Dim result = Await model.SavePharmaceuticalDispensingDevolution(Me.pharmaceuticalDispensingDevolution, _idCurrentSequence, Me._sequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If pharmaceuticalDispensingDevolution.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), result.ObjectEmbbeded.Code)
                    Else
                        If pharmaceuticalDispensingDevolution.Status = 3 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                    End If

                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    taskWait = Task.Factory.StartNew(Sub()
                                                         If Me.BarraBotones.InvokeRequired Then
                                                             Me.BarraBotones.BeginInvoke(Sub()
                                                                                             Select Case varImp
                                                                                                 Case 1
                                                                                                     Me.BarraBotones.PrintReport(PrintReportAction.Create, result.ObjectEmbbeded.Id, 0, result.ObjectEmbbeded.Id)
                                                                                                 Case 2
                                                                                                     Me.BarraBotones.PrintReport(PrintReportAction.Update, result.ObjectEmbbeded.Id, 0, result.ObjectEmbbeded.Id)
                                                                                                 Case 3
                                                                                                     Me.BarraBotones.PrintReport(PrintReportAction.Confirm, result.ObjectEmbbeded.Id, 0, result.ObjectEmbbeded.Id)
                                                                                                 Case 4
                                                                                                     Me.BarraBotones.PrintReport(PrintReportAction.Cancel, result.ObjectEmbbeded.Id, 0, result.ObjectEmbbeded.Id)
                                                                                             End Select
                                                                                         End Sub)
                                                         Else
                                                             Select Case varImp
                                                                 Case 1
                                                                     Me.BarraBotones.PrintReport(PrintReportAction.Create, result.ObjectEmbbeded.Id, 0, result.ObjectEmbbeded.Id)
                                                                 Case 2
                                                                     Me.BarraBotones.PrintReport(PrintReportAction.Update, result.ObjectEmbbeded.Id, 0, result.ObjectEmbbeded.Id)
                                                                 Case 3
                                                                     Me.BarraBotones.PrintReport(PrintReportAction.Confirm, result.ObjectEmbbeded.Id, 0, result.ObjectEmbbeded.Id)
                                                                 Case 4
                                                                     Me.BarraBotones.PrintReport(PrintReportAction.Cancel, result.ObjectEmbbeded.Id, 0, result.ObjectEmbbeded.Id)
                                                             End Select
                                                         End If
                                                     End Sub)
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                    If result.StatusCode = eStatusResult.WARNING Then
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = result.Message
                    End If

                    If pharmaceuticalDispensingDevolution.Id > 0 Then
                        'pharmaceuticalDispensingDevolution = Await model.GetPharmaceuticalDispensingDevolutionByCode(Code)
                        pharmaceuticalDispensingDevolution = Me.GetPharmaceuticalDispensingDevolutionFromGrid()
                    Else
                        pharmaceuticalDispensingDevolution = New PharmaceuticalDispensingDevolution
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    Private Async Sub SaveOrUpdateAndConfirm(actions As Integer)
        If ValidateControls() = True Then
            If _listDetailsTask IsNot Nothing AndAlso listPharmaceuticalDispensingDevolutionDetail Is Nothing Then
                listPharmaceuticalDispensingDevolutionDetail = Await _listDetailsTask
            End If
            If Me.pharmaceuticalDispensingDevolution.Id = 0 Then
                If listPharmaceuticalDispensingDevolutionDetailToSend Is Nothing OrElse listPharmaceuticalDispensingDevolutionDetailToSend.Count = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "Se debe agregar o modificar un producto para devolver"
                    Exit Sub
                End If
            ElseIf listPharmaceuticalDispensingDevolutionDetailToSend.Where(Function(d) d.ChangeTracker.State = ObjectState.Deleted).ToList().Count = listPharmaceuticalDispensingDevolutionDetail.Count AndAlso Not listPharmaceuticalDispensingDevolutionDetailToSend.Any(Function(d) d.ChangeTracker.State = ObjectState.Added) Then
                'Si voy a eliminar todo los detalles que ya estaban guardados
                Mensaje(EeventViewerImages.Advertencia) = "La devolución no puede generarse sin productos"
                Exit Sub
            End If
        Else
            Exit Sub
        End If
        AssigningValues()
        Try
            Using model As New MSupplyDevolution(MyTag)
                AsyncLoader(True)
                Dim result = Await model.SaveAndConfirmPharmaceuticalDispensingDevolution(pharmaceuticalDispensingDevolution, _idCurrentSequence, actions, Me._sequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Me.Deshacer()
                ElseIf result.StatusCode = eStatusResult.WARNING Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                    Mensaje(EeventViewerImages.MensajeError) = result.Message
                    If pharmaceuticalDispensingDevolution.Id > 0 Then
                        'pharmaceuticalDispensingDevolution = Await model.GetPharmaceuticalDispensingDevolutionByCode(Code)
                        pharmaceuticalDispensingDevolution = Me.GetPharmaceuticalDispensingDevolutionFromGrid()
                    Else
                        pharmaceuticalDispensingDevolution = New PharmaceuticalDispensingDevolution
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    Public Async Sub Nuevo() Implements IcrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await Me.NewSupplyDevolution()
        End If
    End Sub
#End Region

#Region "METHODS"

    ''' <summary>
    ''' Método utilizado para cargar los parametros
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadParameters() As Task
        Using Model As New MEntranceVoucher(Me.MyTag)
            _settingsInventory = Await Model.GetSettingInventory(_idOperativeUnit)
            If _settingsInventory Is Nothing OrElse _settingsInventory.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingParameter", MODULE_NAME)
                Deshacer()
                Exit Function
            End If

            Me.ValidateDate()
        End Using
    End Function

    ''' <summary>
    ''' Consulta la fecha de los parametros y establece la fecha minima y maxima de la fecha del documento
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidateDate()
        If _settingsInventory Is Nothing OrElse _settingsInventory.Id = 0 Then
            Exit Sub
        End If

        Dim dateMin As DateTime = Convert.ToDateTime(_settingsInventory.Year.ToString + "/" + _settingsInventory.Month.ToString + "/01")
        INDDteDate.Properties.MinValue = dateMin
        INDDteDate.Properties.MaxValue = GetDateServer()
    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = 100},
                              New ColumnInfo With {.Caption = "Fecha", .FieldName = "DocumentDate", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Ingreso", .FieldName = "AdmissionNumber", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Almacen", .FieldName = "WarehouseId.CodeName", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = 200}}.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAllPharmaceuticalDispensingDevolution
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' metodo para limpioar los controles del popup de ingreso
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsAdminssion()
        Me.TxtAdmissionCode.Text = String.Empty
        Me.TxtFunctionalUnitAdmission.Text = String.Empty

        Me.TxtAdmissionDate.Text = String.Empty
        Me.TxtBedStay.Text = String.Empty

        Me.TxtAdmissionType.Text = String.Empty
        Me.TxtPlaceEntry.Text = String.Empty

        Me.TxtLiquidationType.Text = String.Empty
        Me.TxtAuthorization.Text = String.Empty

        Me.TxtAtentionCenter.Text = String.Empty
        Me.TxtEntityNameAdmission.Text = String.Empty

        Me.TxtResponsiblePhone.Text = String.Empty
        Me.TxtResponsibleName.Text = String.Empty

        Me.TxtPatientCode.Text = String.Empty
        Me.TxtPatientName.Text = String.Empty
        Me.TxtPatientBirth.Text = String.Empty
        Me.TxtPatientAge.Text = String.Empty
        Me.TxtCareGroupPatient.Text = String.Empty
        Me.TxtPatientEntityName.Text = String.Empty
        Me.TxtPatientEstrato.Text = String.Empty

        Me.TxtPatientType.EditValue = Nothing
        Me.TxtAfiliationType.EditValue = Nothing
        Me.TxtCareGroupPatient.Text = String.Empty
        Me.TxtCareGroupAdmission.Text = String.Empty
        Me.TxtRiskType.Text = String.Empty
        Me.TxtContact.Text = String.Empty
    End Sub

    ''' <summary>
    ''' limpiar controles del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDLcSupplyDevolution.BeginUpdate()
        ReadOnlyControls(False)
        ActionsOnControls = False
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.StatusRecordVisible = False
        Me._pharmaceuticalDispensingDevolutionLoaded = False
        BarraBotones.CleanAuditBasic()
        BarraBotones.ReassignOperatingUnit()
        Me.ValidateDate()
        INDBteCode.EditValue = Nothing
        INDDteDate.EditValue = GetDateServer()
        INDSleWarehouse.EditValue = Nothing
        INDSleWarehouse.Properties.NullText = String.Empty
        Me.INDSleAdmissionNumber.SetNullText(String.Empty)
        CleanControlsAdminssion()
        CleanControlsPopupProducts()
        FormatEmptyGrid()
        INDliProductAddedAndEditing.Visibility = LayoutVisibility.Always
        INDliProducts.Visibility = LayoutVisibility.Never
        INDSleProduct.Properties.DataSource = Nothing
        INDMeDetail.EditValue = Nothing
        INDGcProducts.DataSource = Nothing
        INDGcProductAddedAndEditing.DataSource = Nothing
        admission = Nothing
        _listDetailsTask = Nothing
        Me.pharmaceuticalDispensingDevolution = Nothing
        listPharmaceuticalDispensingDevolutionDetail = Nothing
        listPharmaceuticalDispensingDevolutionDetailToSend = New List(Of PharmaceuticalDispensingDevolutionDetail)()
        
        INDLcSupplyDevolution.EndUpdate()
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Private Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(Me.Tag.ToString())
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        Else
            Me.BarraBotones.EnableBarItems()
        End If
    End Sub

    Private Async Sub ReturnValue(ReturnValue As String, ReturnObject As Object)
        DeleteBlockedRecord()
        Code = ReturnValue
        If Code <> String.Empty Then
            LoadControls()
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
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
    ''' metodo para generar kla indexacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateDoc() As IndexedDocument2
        Dim content = String.Format(ResourceManager.GetString("FrmSupplyDevolution_IndexContent", MODULE_NAME), Me.pharmaceuticalDispensingDevolution.Code, Me.admission.AdmissionCode.ToString().Trim(), DocumentDate, If(INDSleWarehouse.Text = String.Empty, INDSleWarehouse.Properties.NullText, INDSleWarehouse.Text))
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = content,
                .CreationDate = dateServer,
                .CreationUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName,
                .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.pharmaceuticalDispensingDevolution.Code & "#$",
                .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me.pharmaceuticalDispensingDevolution.Code),
                .Update = dateServer,
                .UpdateUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName
            Me._doc.Content = content
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me.pharmaceuticalDispensingDevolution.Code)
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
    ''' metodo para establecer los datos del ingreso
    ''' </summary>
    ''' <param name="record"></param>
    ''' <remarks></remarks>
    Private Async Sub SetAdmission(record As Object)
        admission = record
        With admission
            Me.INDSleAdmissionNumber.SetNullText(String.Format(ResourceManager.GetString("AdmissionResume", "IndigoCrystalHis"), admission.AdmissionCode.ToString().Trim(), admission.PatientCode.ToString().Trim(), admission.PatientName.ToString().Trim()))
        End With

        'Consultamos de uns sp los ingresos
        Dim _auxAdmissionToReload As Infrastructure.Data.Xpo.CrystalRepository.ViewLiquidationGetAdmissionAll = Nothing
        Await Task.Factory.StartNew(Sub()
                                        _auxAdmissionToReload = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.HisContainer) _
                                                .CrystalService.Liquidation_GetAdmission(admission.AdmissionCode.ToString().Trim())
                                    End Sub)

        Dim documentType As String = String.Empty
        Select Case _auxAdmissionToReload.PatientDocumentType.ToString().Trim()
            Case "1"
                documentType = "CC"
            Case "2"
                documentType = "CE"
            Case "3"
                documentType = "TI"
            Case "4"
                documentType = "RC"
            Case "5"
                documentType = "PA"
            Case "6"
                documentType = "AS"
            Case "7"
                documentType = "MS"
            Case "8"
                documentType = "NU"
        End Select
        Me.TxtPatientCode.Text = If(_auxAdmissionToReload.PatientCode Is Nothing, String.Empty, String.Concat(documentType, " - ", _auxAdmissionToReload.PatientCode.ToString().Trim()))
        Me.TxtPatientName.Text = If(_auxAdmissionToReload.PatientName Is Nothing, String.Empty, _auxAdmissionToReload.PatientName.ToString().Trim())
        Me.TxtPatientBirth.Text = Convert.ToDateTime(_auxAdmissionToReload.PatientBirth).ToString(SessionValues.Instance.Culture)
        Me.TxtPatientAge.Text = Utils.AgeToString(Convert.ToDateTime(_auxAdmissionToReload.PatientBirth))
        Me.TxtPatientType.EditValue = Convert.ToInt32(_auxAdmissionToReload.PatientType)
        Me.TxtAfiliationType.EditValue = Convert.ToInt32(_auxAdmissionToReload.PatientAfiliation)
        Me.TxtPatientEstrato.Text = (_auxAdmissionToReload.NivelCode & " - " & _auxAdmissionToReload.NivelName)
        'Me.TxtContacto

        'DATOS DEL INGRESO
        Me.TxtAdmissionCode.Text = _auxAdmissionToReload.AdmissionCode.ToString().Trim()
        Me.TxtAdmissionDate.Text = Convert.ToDateTime(_auxAdmissionToReload.AdmissionDate).ToString(SessionValues.Instance.Culture)
        Me.TxtEntityNameAdmission.Text = _auxAdmissionToReload.EntityName
        Select Case _auxAdmissionToReload.AdmissionRiskType
            Case "1"
                Me.TxtRiskType.Text = "Enfermedad General y Maternidad"
            Case "2"
                Me.TxtRiskType.Text = "Accidente de Tránsito"
            Case "3"
                Me.TxtRiskType.Text = "Catástrofe"
            Case "4"
                Me.TxtRiskType.Text = "Enfermedad General y Maternidad"
            Case "5"
                Me.TxtRiskType.Text = "Accidente de Trabajo"
            Case "6"
                Me.TxtRiskType.Text = "Enfermedad Profesional"
            Case "7"
                Me.TxtRiskType.Text = "Atención Inicial de Urgencias"
            Case "8"
                Me.TxtRiskType.Text = "Otro Tipo de Accidente"
            Case "9"
                Me.TxtRiskType.Text = "Lesión Por Agresión"
            Case "10"
                Me.TxtRiskType.Text = "Lesión AutoInfligida"
            Case "11"
                Me.TxtRiskType.Text = "Maltrato Físico"
            Case "12"
                Me.TxtRiskType.Text = "Promoción y Prevención"
            Case "13"
                Me.TxtRiskType.Text = "Otro"
            Case "14"
                Me.TxtRiskType.Text = "Accidente Rabico"
            Case "15"
                Me.TxtRiskType.Text = "Accidente Ofídico"
            Case "16"
                Me.TxtRiskType.Text = "Sopecha de Abuso Sexual"
            Case "17"
                Me.TxtRiskType.Text = "Sopecha de Violencia Sexual"
            Case "18"
                Me.TxtRiskType.Text = "Sopecha de Maltrato Emocional"
        End Select
        Me.TxtPlaceEntry.Text = ResourceManager.GetString("PlaceEntry_" & _auxAdmissionToReload.PlaceEntry.ToString(), "IndigoCrystalHis")
        Me.TxtAdmissionType.EditValue = Convert.ToInt32(_auxAdmissionToReload.AdmissionType)
        Me.TxtBedStay.Text = If(_auxAdmissionToReload.BedStay Is Nothing, String.Empty, _auxAdmissionToReload.BedStay.ToString().Trim())
        Me.TxtLiquidationType.EditValue = Convert.ToInt32(_auxAdmissionToReload.LiquidationType)
        Me.TxtAuthorization.Text = _auxAdmissionToReload.AuthorizationNumber.ToString().Trim()
        Me.TxtAtentionCenter.Text = _auxAdmissionToReload.AdmissionCentAtencCodeName
        Me.TxtFunctionalUnitAdmission.Text = _auxAdmissionToReload.AdmissionUniFuncCodeName.ToString().Trim()
        Me.TxtResponsibleName.Text = If(_auxAdmissionToReload.ResponsibleName Is Nothing, String.Empty, _auxAdmissionToReload.ResponsibleName.ToString().Trim())
        Me.TxtResponsiblePhone.Text = _auxAdmissionToReload.ResponsiblePhone

        If _auxAdmissionToReload IsNot Nothing Then
            If CInt(_auxAdmissionToReload.CareGroupTypePatient) <> -1 Then
                Select Case CByte(_auxAdmissionToReload.CareGroupTypePatient)
                    Case 1, 2, 4 'EAPB con contrato
                        Me.TxtPatientEntityName.Text = String.Concat(_auxAdmissionToReload.PatientEntityCode, " - ", _auxAdmissionToReload.PatientEntity)
                    Case 3 'Particulares
                        LiEntity.Text = ResourceManager.GetString("LyciSleThirdPartyText_ThirdParty", "CtrFolio")
                        'tercero
                        Me.TxtPatientEntityName.Text = _auxAdmissionToReload.PatientEntity
                End Select
                Me.TxtCareGroupPatient.Text = _auxAdmissionToReload.CareGroupCodeName
            End If
            Me.TxtCareGroupAdmission.Text = _auxAdmissionToReload.AdmissionCareGroupCodeName
        End If
    End Sub

    ''' <summary>
    ''' metodo para generar secuencia numerica
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function NewSupplyDevolution() As Task

        If Me._settingsInventory Is Nothing OrElse Me._settingsInventory.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingParameter", MODULE_NAME)
            Exit Function
        End If

        Me.pharmaceuticalDispensingDevolution = New PharmaceuticalDispensingDevolution
        Me.listPharmaceuticalDispensingDevolutionDetail = New List(Of PharmaceuticalDispensingDevolutionDetail)()

        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Me.SaveAndConfirmObligatory()
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.InventorySequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.InventorySequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
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
                Me.SaveAndConfirmObligatory()
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Me.SaveAndConfirmObligatory()
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                            Me.SaveAndConfirmObligatory()
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
                    Me.SaveAndConfirmObligatory()
                End If
            End If
        End If

        BarraBotones.StatusRecordVisible = True
        BarraBotones.StatusRecord = "1"


        'Me.pharmaceuticalDispensingDevolution = New PharmaceuticalDispensingDevolution
        'Me.listPharmaceuticalDispensingDevolutionDetail = New List(Of PharmaceuticalDispensingDevolutionDetail)()
        'If Me._sequence.IsManual Then
        '    Me.ActionsOnControls = True
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        'Else
        '    If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
        '        Me._idCurrentSequence = Me._sequence.InventorySequenceDetail(0).Id
        '    ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
        '        If Me._sequence.InventorySequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
        '            Me._idCurrentSequence = Me._sequence.InventorySequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
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
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '            Else
        '                Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
        '                    Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
        '                End Using
        '                If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
        '                    Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
        '                    Me.ActionsOnControls = True
        '                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '                Else
        '                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
        '                End If
        '            End If
        '        Else
        '            Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
        '            Me.ActionsOnControls = True
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
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

    Private Sub SaveAndConfirmObligatory()
        If Not Me.BarraBotones.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.GuardaryConfirmar)) Then
            Exit Sub
        End If
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GuardarConfirmar) = Not Me.BarraBotones.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.GuardaryConfirmar))
    End Sub

    ''' <summary>
    ''' metodo para cargar datos en los controles
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Sub LoadControls()
        If Me.BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If

        'Aqui va el codigo para ocultar o poner visible las rejillas
        INDliProducts.Visibility = LayoutVisibility.Always
        INDliProductAddedAndEditing.Visibility = LayoutVisibility.Never

        INDGcProducts.Focus()
        INDGvProducts.FocusedRowHandle = 0
        Me.INDGcProducts.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.GetPharmaceuticalDispensingDevolution(Me.Code.Trim())
        AsyncLoader(True)
    End Sub

    Private Async Sub QueryBlockrecordAndIndexedDocument()
        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
            With pharmaceuticalDispensingDevolution
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
                BarraBotones.OperatingUnitValue = .OperatingUnitId
                Select Case .Status
                    Case 1
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                    Case Else
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                        ReadOnlyControls(True)
                End Select
            End With

            Me.GetDocumentIndexed(Me.Tag & "_" & Me.pharmaceuticalDispensingDevolution.Code)

            Dim result = Await ModelRecord.GetBlockRecord(Me.Tag, Me.pharmaceuticalDispensingDevolution.Id)
            If result IsNot Nothing AndAlso result.Id = 0 Then
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                state.State = Domain.Base.Entities.ObjectState.Added
                record = New BlockRecordInventory With {.BlockDate = DateTime.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .FormId = Me.Tag, .CodUser = SessionValues.Instance.UserIndigo, .RecordId = Me.pharmaceuticalDispensingDevolution.Id}
                Dim operation = Await ModelRecord.SaveBlockRecord(record)
                record = operation.ObjectEmbbeded
            Else
                Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
                record = result
                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
            End If

            Me.BarraBotones.SetDocuments(pharmaceuticalDispensingDevolution.Id, MyTag, Nothing, GetType(PharmaceuticalDispensingDevolution).Name)
        End Using
    End Sub

    Private _pharmaceuticalDispensingDevolutionLoaded As Boolean = False

    Private Async Sub INDGvProducts_RowLoaded(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowEventArgs) Handles INDGvProducts.RowLoaded
        If e.RowHandle > -1 AndAlso Not _pharmaceuticalDispensingDevolutionLoaded Then
            _pharmaceuticalDispensingDevolutionLoaded = True
            INDGvProducts.FocusedRowHandle = 0

            Try

                AsyncLoader(True)
                pharmaceuticalDispensingDevolution = GetPharmaceuticalDispensingDevolutionFromGrid()
                INDLcSupplyDevolution.BeginUpdate()
                If pharmaceuticalDispensingDevolution IsNot Nothing AndAlso pharmaceuticalDispensingDevolution.Id > 0 Then
                    Me.BarraBotones.StatusRecordVisible = True

                    INDGvProducts.ExpandAllGroups()
                    QueryBlockrecordAndIndexedDocument()
                    'Consultamos los detalles
                    Using model As New MSupplyDevolution(Me.MyTag)
                        _listDetailsTask = model.GetPharmaceuticalDispensingDevolutionDetailByIdPharmaceuticalDispensingDevolution(pharmaceuticalDispensingDevolution.Id)
                    End Using

                    With Me.pharmaceuticalDispensingDevolution
                        If .Status <> 1 Then
                            INDDteDate.Properties.MinValue = .DocumentDate
                        End If

                        Code = .Code
                        DocumentDate = .DocumentDate
                        WarehouseId = .WarehouseId
                        INDSleWarehouse.Properties.NullText = .CodeNameWarehouse
                        Observation = .Observation
                        BarraBotones.StatusRecord = .Status.ToString()
                    End With
                    'Llenar NullText
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                    Await Task.Factory.StartNew(Sub()
                                                    If Me.BarraBotones.InvokeRequired Then
                                                        Me.BarraBotones.BeginInvoke(Sub()
                                                                                        Me.BarraBotones.PrintReport(PrintReportAction.None, pharmaceuticalDispensingDevolution.Id, 0, pharmaceuticalDispensingDevolution.Id)
                                                                                    End Sub)
                                                    Else
                                                        Me.BarraBotones.PrintReport(PrintReportAction.None, pharmaceuticalDispensingDevolution.Id, 0, pharmaceuticalDispensingDevolution.Id)
                                                    End If
                                                End Sub)
                    '--
                    RunSetAdmission()
                    AsyncLoader(False)
                    ActionsOnControls = True
                    If Me.pharmaceuticalDispensingDevolution.Status = 1 Then
                        INDBtnAddProduct.Enabled = True
                    End If
                    INDSleAdmissionNumber.Enabled = False
                    INDDteDate.Focus()
                Else
                    AsyncLoader(False)
                    If Me._sequence.IsManual Then
                        Await Me.NewSupplyDevolution()
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                        Code = String.Empty
                        INDBteCode.Focus()
                    End If
                End If
                INDLcSupplyDevolution.EndUpdate()
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try

        End If


        '    pharmaceuticalDispensingDevolution = Me.GetPharmaceuticalDispensingDevolutionFromGrid()
        '    INDLcSupplyDevolution.BeginUpdate()
        '    If pharmaceuticalDispensingDevolution IsNot Nothing AndAlso pharmaceuticalDispensingDevolution.Id > 0 Then
        '        INDGvProducts.ExpandAllGroups()
        '        QueryBlockrecordAndIndexedDocument()
        '        'Consultamos los detalles
        '        Using model As New MSupplyDevolution(Me.MyTag)
        '            _listDetailsTask = model.GetPharmaceuticalDispensingDevolutionDetailByIdPharmaceuticalDispensingDevolution(pharmaceuticalDispensingDevolution.Id)
        '        End Using

        '        With Me.pharmaceuticalDispensingDevolution
        '            Code = .Code
        '            DocumentDate = .DocumentDate
        '            WarehouseId = .WarehouseId
        '            INDSleWarehouse.Properties.NullText = .CodeNameWarehouse
        '            Observation = .Observation
        '            BarraBotones.StatusRecord = .Status.ToString()
        '        End With
        '        '--
        '        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
        '        Task.Factory.StartNew(Sub()
        '                                  If Me.BarraBotones.InvokeRequired Then
        '                                      Me.BarraBotones.BeginInvoke(Sub()
        '                                                                      Me.BarraBotones.PrintReport(PrintReportAction.None, pharmaceuticalDispensingDevolution.Id, 0, pharmaceuticalDispensingDevolution.Id)
        '                                                                  End Sub)
        '                                  Else
        '                                      Me.BarraBotones.PrintReport(PrintReportAction.None, pharmaceuticalDispensingDevolution.Id, 0, pharmaceuticalDispensingDevolution.Id)
        '                                  End If
        '                              End Sub)
        '        '--
        '        RunSetAdmission()
        '        AsyncLoader(False)
        '        ActionsOnControls = True
        '        If Me.pharmaceuticalDispensingDevolution.Status = 1 Then
        '            INDBtnAddProduct.Enabled = True
        '        End If
        '        INDSleAdmissionNumber.Enabled = False
        '        INDDteDate.Focus()
        '    Else
        '        AsyncLoader(False)
        '        If Me._sequence.IsManual Then
        '            Me.NewSupplyDevolution()
        '        Else
        '            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
        '            Me.Code = String.Empty
        '            Deshacer()
        '            INDBteCode.Focus()
        '        End If
        '    End If
        'End If

        'INDLcSupplyDevolution.EndUpdate()
    End Sub

    Private _popUpAdmissionLoaded As Boolean = False

    ''' <summary>
    ''' Consulta y carga la admisión
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RunSetAdmission() As Task
        Return Task.Factory.StartNew(Sub()
                                         Using modelServiceOrder As New Billing.MVP.MServiceOrder(Me.MyTag)

                                             Dim admissionTmp = modelServiceOrder.GetAdmissionByServiceOrder(pharmaceuticalDispensingDevolution.AdmissionNumber.Trim())
                                             If admissionTmp IsNot Nothing Then
                                                 If Me.INDSleAdmissionNumber.InvokeRequired Then
                                                     Me.INDSleAdmissionNumber.BeginInvoke(Sub()
                                                                                              SetAdmission(admissionTmp)
                                                                                          End Sub)
                                                 Else
                                                     SetAdmission(admissionTmp)
                                                 End If

                                                 Using model As New MSupplyDevolution(Me.MyTag)
                                                     If INDSleProduct.InvokeRequired Then
                                                         INDSleProduct.BeginInvoke(Sub()
                                                                                       INDSleProduct.Properties.DataSource = model.ListPharmaceuticalDispensingDetailBatchSerialByAdmissionNumber(admission.AdmissionCode.ToString().Trim())
                                                                                   End Sub)
                                                     Else
                                                         INDSleProduct.Properties.DataSource = model.ListPharmaceuticalDispensingDetailBatchSerialByAdmissionNumber(admission.AdmissionCode.ToString().Trim())
                                                     End If
                                                 End Using
                                             End If

                                             _popUpAdmissionLoaded = True
                                         End Using
                                     End Sub)
    End Function

    ''' <summary>
    ''' Genera el objeto Entity de la cabecera de la devolución de dispensación
    ''' </summary>
    Private Function GetPharmaceuticalDispensingDevolutionFromGrid() As PharmaceuticalDispensingDevolution
        Dim pdObject As New PharmaceuticalDispensingDevolution()

        pdObject.Id = INDGvProducts.GetRowCellValue(0, "PharmaceuticalDispensingDevolutionId")
        pdObject.Code = INDGvProducts.GetRowCellValue(0, "Code")
        pdObject.OperatingUnitId = INDGvProducts.GetRowCellValue(0, "OperatingUnitId")
        pdObject.DocumentDate = INDGvProducts.GetRowCellValue(0, "DocumentDate")
        pdObject.WarehouseId = INDGvProducts.GetRowCellValue(0, "WarehouseId")
        pdObject.AdmissionNumber = INDGvProducts.GetRowCellValue(0, "AdmissionNumber")
        pdObject.Observation = INDGvProducts.GetRowCellValue(0, "Observation")
        pdObject.Status = INDGvProducts.GetRowCellValue(0, "Status")
        pdObject.CreationUser = INDGvProducts.GetRowCellValue(0, "CreationUser")
        pdObject.CreationDate = INDGvProducts.GetRowCellValue(0, "CreationDate")
        pdObject.ModificationUser = INDGvProducts.GetRowCellValue(0, "ModificationUser")
        pdObject.ModificationDate = INDGvProducts.GetRowCellValue(0, "ModificationDate")
        pdObject.ConfirmationUser = INDGvProducts.GetRowCellValue(0, "ConfirmationUser")
        pdObject.ConfirmationDate = INDGvProducts.GetRowCellValue(0, "ConfirmationDate")
        pdObject.AnnulmentUser = INDGvProducts.GetRowCellValue(0, "AnnulmentUser")
        pdObject.AnnulmentDate = INDGvProducts.GetRowCellValue(0, "AnnulmentDate")
        pdObject.CodeNameWarehouse = INDGvProducts.GetRowCellValue(0, "CodeNameWarehouse")

        pdObject.MarkAsUnchanged()

        Return pdObject
    End Function

    ''' <summary>
    ''' metodo para asignar valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        With Me.pharmaceuticalDispensingDevolution
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .DocumentDate = DocumentDate
            .WarehouseId = WarehouseId
            .CodeNameWarehouse = INDSleWarehouse.Text.Split("-").ElementAt(0).Trim()
            .AdmissionNumber = Me.admission.AdmissionCode.ToString().Trim()
            .Observation = Observation
            .Prefix = Me._prefixSelected
            .Status = 1
            .OperatingUnitId = BarraBotones.OperatingUnitValue

            For Each item In listPharmaceuticalDispensingDevolutionDetailToSend
                .PharmaceuticalDispensingDevolutionDetail.Add(item)
            Next

            For Each item In listPharmaceuticalDispensingDevolutionDetail.Where(Function(d) Not listPharmaceuticalDispensingDevolutionDetailToSend.Any(Function(f) f.Id = d.Id)).ToList()
                .PharmaceuticalDispensingDevolutionDetail.Add(item)
            Next

            If pharmaceuticalDispensingDevolution.Id > 0 Then
                pharmaceuticalDispensingDevolution.MarkAsModified()
            End If
        End With
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
#End Region

#Region "HANDLES"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
        _settingsInventory = Nothing
        _prefixSelected = Nothing
        record = Nothing
        presenter = Nothing
        admission = Nothing
        listPharmaceuticalDispensingDetailBatchSerial = Nothing
        listPharmaceuticalDispensingDevolutionDetail = Nothing
        listPharmaceuticalDispensingDevolutionDetailToSend = Nothing
        pharmaceuticalDispensingDevolution = Nothing
        _listDetailsTask = Nothing
        taskWait = Nothing
        varImp = Nothing
        VirtualStore = Nothing
    End Sub

    Private Async Sub FrmSupplyDevolution_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDLcSupplyDevolution, True)

        Using model As New Billing.MVP.MServiceOrder(MyTag.ToString())
            INDSleAdmissionNumber.Datasource = model.GetViewAdmissionServiceOrder
        End Using
        INDSleAdmissionNumber.PopupContainerControl = INDPccMoreInfoAdmission

        Me._doc = Nothing
        _indigoSession = SessionValues.Instance
        presenter = New PSupplyDevolution(Me)
        presenter.LoadDefinitionLayout()
        presenter.GetSequense()
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        Await Me.LoadParameters()

        Deshacer()
        AddActionsColumns()
        LoadStatus()
        AddHandler Me.INDSleAdmissionNumber.Search.KeyDown, AddressOf INDSleAdmissionNumber_KeyDown
    End Sub
#End Region

#Region "Shown"
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBteCode.Enabled Then
            INDBteCode.Focus()
        End If
    End Sub
#End Region

#Region "FormClosing"
    Private Sub FrmSupplyDevolution_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub
#End Region

#Region "QueryPopUp"
    Private Sub INDSleWarehouse_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleWarehouse.QueryPopUp
        If INDSleWarehouse.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If WareHouseDatasource Is Nothing Then
            presenter.LoadWareHouse()
        End If
    End Sub

#End Region

#Region "ButtonClick"
    Private Sub INDSleWarehouse_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleWarehouse.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmStores
                OpenFormDialog(form)
                presenter.LoadWareHouse()
                INDSleWarehouse.Focus()
            End Using
        End If
    End Sub
#End Region

#Region "IdEntityLoaded"
    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.pharmaceuticalDispensingDevolution IsNot Nothing AndAlso Me.pharmaceuticalDispensingDevolution.Id > 0 Then
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
#End Region

#Region "KeyDown"
    Private Sub INDSleAdmissionNumber_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDMeDetail.Focus()
        End If
    End Sub

    Private Async Sub INDBteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await Me.NewSupplyDevolution()
                Else
                    Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    'Private Sub INDMeDetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDMeDetail.KeyDown
    '    If e.KeyCode = System.Windows.Forms.Keys.Enter Then
    '        INDPceProducts.Focus()
    '        INDPceProducts.ShowPopup()
    '        INDSleProduct.Focus()
    '    End If
    'End Sub
#End Region

#Region "NewSelectedValue"

    Private Sub INDSleAdmissionNumber_NewSelectedValue(sender As Object, e As SearchAdmissionClosingEventArgs) Handles INDSleAdmissionNumber.NewSelectedValue
        SetAdmission(e.AdmissionObject)
        INDBtnAddProduct.Enabled = True

        If pharmaceuticalDispensingDevolution.Id = 0 Then
            Using model As New MSupplyDevolution(MyTag)
                INDSleProduct.Properties.DataSource = model.ListPharmaceuticalDispensingDetailBatchSerialByAdmissionNumber(admission.AdmissionCode.ToString().Trim())
            End Using
        End If
    End Sub
#End Region

#Region "EditValueChanging"
    Private Sub INDRptSeDevolutionQuantity_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRptSeDevolutionQuantity.EditValueChanging
        If e.NewValue Is String.Empty Then
            Exit Sub
        End If
        Dim PharmaceuticalDispensingDetailBatchSerial = DirectCast(INDGvProducts.GetFocusedRow, PharmaceuticalDispensingDetailBatchSerial)

        If e.NewValue > PharmaceuticalDispensingDetailBatchSerial.OutstandingQuantity Then
            Mensaje(EeventViewerImages.Advertencia) = "La cantidad a devolver no puede ser mayor a la cantidad del producto"
            e.Cancel = True
            Exit Sub
        End If
    End Sub

    Private Sub INDSleWarehouse_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleWarehouse.EditValueChanged
        If Me.INDSleWarehouse.EditValue IsNot Nothing Then
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("O") Then
                Dim store = If(INDGdvWarehouse.DataSource IsNot Nothing, DirectCast(DirectCast(INDGdvWarehouse.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.InventoryRepository.WarehouseXpo), Nothing)
                If store Is Nothing Then
                    'Se consulta el almacen por id
                    Dim info = presenter.GetWareHouseById(INDsleWareHouse.EditValue)
                    If info IsNot Nothing Then
                        Me._idCurrentSequence = Me.GetIdSequenceByPrefix(info.Prefix)
                        Me._prefixSelected = info.Prefix
                        Me.VirtualStore = info.VirtualStore
                    End If
                Else
                    Me._idCurrentSequence = Me.GetIdSequenceByPrefix(store.Prefix)
                    Me._prefixSelected = store.Prefix
                    Me.VirtualStore = store.VirtualStore
                End If
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
    Private Async Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso operatingUnit.Id <> Me._idOperativeUnit Then
            Me._idOperativeUnit = operatingUnit.Id
            Await Me.LoadParameters()
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.InventorySequenceDetail IsNot Nothing Then
                If Not Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Me.pharmaceuticalDispensingDevolution.Status = 3
            varImp = 4
            Guardar()
        End If
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        varImp = 3
        SaveOrUpdateAndConfirm(1)
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        SaveOrUpdateAndConfirm(2)
    End Sub

    ''' <summary>
    ''' Click Boton Imprimir
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        taskWait = Task.Factory.StartNew(Sub()
                                             If Me.BarraBotones.InvokeRequired Then
                                                 Me.BarraBotones.BeginInvoke(Sub()
                                                                                 Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, pharmaceuticalDispensingDevolution.Id, 0, pharmaceuticalDispensingDevolution.Id)
                                                                             End Sub)
                                             Else
                                                 Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, pharmaceuticalDispensingDevolution.Id, 0, pharmaceuticalDispensingDevolution.Id)
                                             End If
                                         End Sub)
    End Sub
#End Region

    Private Sub INDGvSleProducts_AsyncCompleted(sender As Object, e As EventArgs) Handles INDGvSleProducts.AsyncCompleted
        INDGvSleProducts.ExpandAllGroups()
    End Sub

    Private Sub CleanControlsPopupProducts()
        INDSleProduct.Properties.Buttons(1).Visible = False
        INDSleProduct.EditValue = Nothing
        INDTxtOutstandingQuantity.EditValue = 0
        product = Nothing
        INDSeQuantity.EditValue = 1
        INDBtnAdd.Enabled = False
    End Sub

    Dim product As PharmaceuticalDispensingDetailBatchSerialXpo

    Private Sub INDSleProduct_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleProduct.EditValueChanged
        If INDSleProduct.EditValue IsNot Nothing Then
            product = DirectCast(DirectCast(INDGvSleProducts.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, PharmaceuticalDispensingDetailBatchSerialXpo)
            INDTxtOutstandingQuantity.EditValue = product.OutstandingQuantity
            INDBtnAdd.Enabled = True
        End If
    End Sub

    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        'If listPharmaceuticalDispensingDevolutionDetail IsNot Nothing AndAlso listPharmaceuticalDispensingDevolutionDetail.Count > 0 Then
        '    Dim PharmaceuticalDispensingDevolutionDetailTmp = listPharmaceuticalDispensingDevolutionDetail.Find(Function(x) x.PharmaceuticalDispensingDetailBatchSerialId = product.Id)
        '    If PharmaceuticalDispensingDevolutionDetailTmp IsNot Nothing Then
        '        Mensaje(EeventViewerImages.Advertencia) = "El producto ya se encuentra agregado "
        '        Exit Sub
        '    End If
        'End If
        'Dim PharmaceuticalDispensingDevolutionDetail = New PharmaceuticalDispensingDevolutionDetail
        'PharmaceuticalDispensingDevolutionDetail.PharmaceuticalDispensingDetailBatchSerialId = product.Id
        'PharmaceuticalDispensingDevolutionDetail.Quantity = INDSeQuantity.EditValue
        'PharmaceuticalDispensingDevolutionDetail.CodePharmaceuticalDispensing = product.PharmaceuticalDispensingDetailId.PharmaceuticalDispensingId.Code
        'PharmaceuticalDispensingDevolutionDetail.ProductId = product.PharmaceuticalDispensingDetailId.ProductId.Id
        'PharmaceuticalDispensingDevolutionDetail.CodeNameProduct = product.PharmaceuticalDispensingDetailId.ProductId.CodeName
        'PharmaceuticalDispensingDevolutionDetail.PharmaceuticalDispensingDetailId = product.PharmaceuticalDispensingDetailId.Id
        'If listPharmaceuticalDispensingDevolutionDetail Is Nothing Then
        '    listPharmaceuticalDispensingDevolutionDetail = New List(Of PharmaceuticalDispensingDevolutionDetail)
        'End If
        'listPharmaceuticalDispensingDevolutionDetail.Add(PharmaceuticalDispensingDevolutionDetail)
        'INDGcProducts.DataSource = listPharmaceuticalDispensingDevolutionDetail
        'INDGcProducts.RefreshDataSource()
        'INDGvProducts.ExpandAllGroups()
        'CleanControlsPopupProducts()
        'INDSleAdmissionNumber.Enabled = False
        'INDSleProduct.Focus()
    End Sub

    Private Sub INDSeQuantity_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDSeQuantity.EditValueChanging
        If product IsNot Nothing Then
            If e.NewValue Is String.Empty Then
                Exit Sub
            End If
            If e.NewValue > product.OutstandingQuantity Then
                Mensaje(EeventViewerImages.Advertencia) = "La cantidad a devolver no puede ser mayor que la cantidad pendiente"
                e.Cancel = True
            End If
        End If
    End Sub

    Private Async Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions, IndigoGridView2.Click_ButtonAction, IndigoGridView2.ContexMenuActions
        If Me.pharmaceuticalDispensingDevolution.Status > 1 Then
            Exit Sub
        End If
        If _listDetailsTask IsNot Nothing AndAlso listPharmaceuticalDispensingDevolutionDetail Is Nothing Then
            listPharmaceuticalDispensingDevolutionDetail = Await _listDetailsTask
        End If
        If DirectCast(DirectCast(sender, DevExpress.XtraEditors.ButtonEdit).Parent, DevExpress.XtraGrid.GridControl).Name.Equals("INDGcProducts") Then 'Xpo
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Dim id As Int32 = INDGvProducts.GetRowCellValue(INDGvProducts.FocusedRowHandle, "Id")

                Dim idx As Int32 = IndexOfById(listPharmaceuticalDispensingDevolutionDetailToSend, id)
                If idx > -1 Then 'Ya existe en la lista a enviar, lo eliminamos
                    listPharmaceuticalDispensingDevolutionDetailToSend.RemoveAt(idx)
                End If

                idx = IndexOfById(listPharmaceuticalDispensingDevolutionDetail, id)
                listPharmaceuticalDispensingDevolutionDetailToSend.Add(CloneDetail(listPharmaceuticalDispensingDevolutionDetail(idx)))
                listPharmaceuticalDispensingDevolutionDetailToSend(listPharmaceuticalDispensingDevolutionDetailToSend.Count - 1).MarkAsDeleted()

                INDliProductAddedAndEditing.Visibility = LayoutVisibility.Always
                INDGcProductAddedAndEditing.DataSource = listPharmaceuticalDispensingDevolutionDetailToSend
                INDGcProductAddedAndEditing.RefreshDataSource()
            End If
        Else
            Dim pharma As PharmaceuticalDispensingDevolutionDetail = CType(INDGvProductAddedAndEditing.GetFocusedRow(), PharmaceuticalDispensingDevolutionDetail)
            listPharmaceuticalDispensingDevolutionDetailToSend.Remove(pharma)

            INDGcProductAddedAndEditing.DataSource = listPharmaceuticalDispensingDevolutionDetailToSend
            INDGcProductAddedAndEditing.RefreshDataSource()
        End If

        If listPharmaceuticalDispensingDevolutionDetailToSend.Count > 0 Then
            FormatEmptyGrid(False)
        Else
            FormatEmptyGrid()
        End If
    End Sub

    ''' <summary>
    ''' Obtiene un objeto nuevo clonado a partir de otro
    ''' </summary>
    ''' <param name="d">Objeto fuente</param>
    Private Function CloneDetail(ByVal d As PharmaceuticalDispensingDevolutionDetail) As PharmaceuticalDispensingDevolutionDetail
        Dim result As New PharmaceuticalDispensingDevolutionDetail()
        result.CodeNameProduct = d.CodeNameProduct
        result.CodePharmaceuticalDispensing = d.CodePharmaceuticalDispensing
        result.CodeProduct = d.CodeProduct
        result.Id = d.Id
        result.OrderedHealthProfessionalCode = d.OrderedHealthProfessionalCode
        result.OriginalValue = d.OriginalValue
        result.PharmaceuticalDispensingDetailBatchSerial = d.PharmaceuticalDispensingDetailBatchSerial
        result.PharmaceuticalDispensingDetailBatchSerialId = d.PharmaceuticalDispensingDetailBatchSerialId
        result.PharmaceuticalDispensingDetailId = d.PharmaceuticalDispensingDetailId
        result.PharmaceuticalDispensingDevolution = d.PharmaceuticalDispensingDevolution
        result.PharmaceuticalDispensingDevolutionId = d.PharmaceuticalDispensingDevolutionId
        result.ProductId = d.ProductId
        result.Quantity = d.Quantity
        Return result
    End Function

    ''' <summary>
    ''' Metodo para agregar a las rejillas las acciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddActionsColumns()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)

        IndigoGridView1.SetListAcction(INDGvProducts, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvProducts.Columns
            If col.Name = "colActions" Then
                col.OptionsColumn.FixedWidth = True
            End If
        Next

        IndigoGridView2.SetListAcction(INDGvProductAddedAndEditing, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvProductAddedAndEditing.Columns
            If col.Name = "colActions" Then
                col.OptionsColumn.FixedWidth = True
            End If
        Next
    End Sub

    ''' <summary>
    ''' Da formato a la rejilla de registros sin guardar cuando no tiene registros
    ''' </summary>
    ''' <param name="isEmpty">Valor que indica si la rejilla esta vacia</param>
    ''' <param name="isNew">Valor que indica si la dispensación es nueva</param>
    Private Sub FormatEmptyGrid(Optional ByVal isEmpty As Boolean = True, Optional ByVal isNew As Boolean = False)
        Dim caption As String = "Registros Sin Guardar"
        Dim gridMaxSize As New Size(828, 250)
        Dim gridMinSize As New Size(828, 0)
        If isEmpty Then
            gridMaxSize = New Size(828, 30)
            gridMinSize = New Size(828, 30)
            caption = "No hay Registros"
        ElseIf isNew Then
            gridMaxSize = New Size(828, 0)
            gridMinSize = New Size(828, 250)
        End If
        INDliProductAddedAndEditing.MinSize = gridMinSize
        INDliProductAddedAndEditing.MaxSize = gridMaxSize
        INDliProductAddedAndEditing.Text = caption
    End Sub

    Private Async Sub INDBtnAddProduct_Click(sender As Object, e As EventArgs) Handles INDBtnAddProduct.Click
        Using form As New PopupProductDevolution
            Me.Cursor = ChangeCursorIndigo()
            If _listDetailsTask IsNot Nothing AndAlso listPharmaceuticalDispensingDevolutionDetail Is Nothing Then
                listPharmaceuticalDispensingDevolutionDetail = Await _listDetailsTask
            End If
            If listPharmaceuticalDispensingDevolutionDetail IsNot Nothing Then
                form.ListPharmaceuticalDispensingDevolutionDetail = listPharmaceuticalDispensingDevolutionDetail
            End If
            form.WarehouseId = WarehouseId
            form.AdmissionNumber = admission.AdmissionCode.ToString().Trim()
            form.Size = New Drawing.Size(800, 700)
            form.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent As New FrmTransparent(form, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            If transparent.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                If listPharmaceuticalDispensingDevolutionDetailToSend IsNot Nothing AndAlso listPharmaceuticalDispensingDevolutionDetailToSend.Count > 0 Then
                    For Each item In form.ListPharmaceuticalDispensingDevolutionDetail.Where(Function(d) d.ChangeTracker.State <> ObjectState.Unchanged).ToList()
                        Dim idx As Int32 = IndexOfById(listPharmaceuticalDispensingDevolutionDetailToSend, item.Id, item.PharmaceuticalDispensingDetailBatchSerialId, item.ProductId)
                        If idx > -1 Then 'Ya existe en la lista a enviar, lo eliminamos
                            listPharmaceuticalDispensingDevolutionDetailToSend.RemoveAt(idx)
                        End If

                        listPharmaceuticalDispensingDevolutionDetailToSend.Add(CloneDetail(item))

                        idx = IndexOfById(listPharmaceuticalDispensingDevolutionDetail, item.Id)
                        If idx > -1 Then 'Ya existe en los detalles de devolución
                            If item.Quantity = 0 Then 'Se devolvió todas las cantidades
                                listPharmaceuticalDispensingDevolutionDetailToSend(listPharmaceuticalDispensingDevolutionDetailToSend.Count - 1).MarkAsDeleted()
                            Else
                                listPharmaceuticalDispensingDevolutionDetailToSend(listPharmaceuticalDispensingDevolutionDetailToSend.Count - 1).MarkAsModified()
                            End If
                        End If
                    Next
                    INDliProductAddedAndEditing.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    FormatEmptyGrid(False, If(INDliProducts.Visibility = LayoutVisibility.Never, True, False))
                    INDGcProductAddedAndEditing.DataSource = listPharmaceuticalDispensingDevolutionDetailToSend
                    INDGcProductAddedAndEditing.RefreshDataSource()
                    INDGvProductAddedAndEditing.ExpandAllGroups()
                    INDSleAdmissionNumber.Enabled = False
                ElseIf form.ListPharmaceuticalDispensingDevolutionDetail.Where(Function(d) d.ChangeTracker.State <> ObjectState.Unchanged).ToList().Count > 0 Then
                    listPharmaceuticalDispensingDevolutionDetailToSend = New List(Of PharmaceuticalDispensingDevolutionDetail)(form.ListPharmaceuticalDispensingDevolutionDetail.Where(Function(d) d.ChangeTracker.State <> ObjectState.Unchanged).ToArray())
                    INDliProductAddedAndEditing.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    FormatEmptyGrid(False, If(INDliProducts.Visibility = LayoutVisibility.Never, True, False))
                    INDGcProductAddedAndEditing.DataSource = listPharmaceuticalDispensingDevolutionDetailToSend
                    INDGcProductAddedAndEditing.RefreshDataSource()
                    INDGvProductAddedAndEditing.ExpandAllGroups()
                    INDSleAdmissionNumber.Enabled = False
                End If

                INDBtnAddProduct.Focus()
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Obtiene el inde
    ''' </summary>
    ''' <param name="list">Lista fuente</param>
    ''' <param name="id">Id del objeto a buscar</param>
    ''' <returns>Indice en la lista</returns>
    Private Function IndexOfById(ByVal list As List(Of PharmaceuticalDispensingDevolutionDetail), ByVal id As Int32, Optional ByVal batchSerialId As Int32 = -1, Optional ByVal productId As Int32 = -1)
        Dim idx As Int32 = -1
        For i = 0 To list.Count - 1
            If id > 0 Then
                If list(i).Id = id Then
                    Return i
                End If
            Else
                If list(i).PharmaceuticalDispensingDetailBatchSerialId = batchSerialId AndAlso list(i).ProductId = productId Then
                    Return i
                End If
            End If
        Next
        Return idx
    End Function

End Class