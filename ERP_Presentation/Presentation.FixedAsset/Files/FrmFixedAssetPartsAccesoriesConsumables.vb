'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 16-09-2014
'
' Last Modified By :
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.XtraEditors.Controls
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.FixedAsset.MVP

Public Class FrmFixedAssetPartsAccesoriesConsumables

    Implements IFixedAssetPartsAccesoriesConsumables, ICustomizableForm
    Private Const NAME_MODULE As String = "FixedAssets"
    Dim searchMode As Boolean = False

    ''' <summary>
    ''' Varaible que contiene la entidad de los Marcas
    ''' </summary> 
    Dim PartsAccesoriesConsumables As FixedAssetPartsAccesoriesConsumables

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecord

    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As New MFixedAssetPartsAccesoriesConsumables(MyBase.Tag)

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.FixedAssetSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PFixedAssetPartsAccessoriesConsumibles

    ''' <summary>
    ''' Permite saber si esta en modo de edición para el popup
    ''' (True=Edita, False=Guarda)
    ''' </summary>
    ''' <remarks></remarks>
    Dim FlagEditMode As Boolean

    ''' <summary>
    ''' Permite saber si al cambiar el search de tipo de depreciación se limpia los controles o no
    ''' (False=NoLimpia, True=Limpia)
    ''' </summary>
    ''' <remarks></remarks>
    Dim FlagPopup As Boolean

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListUnitLifeUtil As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListDepreciationType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Representa a la entidad del detalle de equipo cuando se esta editando
    ''' </summary>
    ''' <remarks></remarks>
    Dim PartsDetail As FixedAssetPartsAccesoriesConsumablesDetail

    ''' <summary>
    ''' Listado de detalles de equipo
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListPartsDetail As List(Of FixedAssetPartsAccesoriesConsumablesDetail)

    ''' <summary>
    ''' Listado de eliminados de detalles de equipo
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeletePartsDetail As List(Of FixedAssetPartsAccesoriesConsumablesDetail)

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IFixedAssetPartsAccesoriesConsumables.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    '***Estado
    Public Property Status As Boolean Implements IFixedAssetPartsAccesoriesConsumables.Status
        Get
            Return CBool(Me.BarraBotones.StatusRecord)
        End Get
        Set(value As Boolean)
            If value = False Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            End If
        End Set
    End Property
    ''' <summary>
    ''' Establece el datasource del libro oficial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    Public Property LegalBookXpo As XPInstantFeedbackSource Implements IFixedAssetPartsAccesoriesConsumables.LegalBookXpo
        Get
            Return INDsleLegalBook.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleLegalBook.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Permite saber si se deprecia o no el articulo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AllowDepreciate As Boolean? Implements IFixedAssetPartsAccesoriesConsumables.AllowDepreciate
        Get
            Return INDsleAllowDepreciate.EditValue
        End Get
        Set(value As Boolean?)
            INDsleAllowDepreciate.EditValue = value
        End Set
    End Property

    
    ''' <summary>
    ''' Obtiene o establece el porcentaje de salvamento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PercentageRescue As Decimal Implements IFixedAssetPartsAccesoriesConsumables.PercentageRescue
        Get
            Return INDsePercentageRescue.EditValue
        End Get
        Set(value As Decimal)
            INDsePercentageRescue.EditValue = value
        End Set
    End Property


    ''' <summary>
    ''' Obtiene o establece el total de unidades producidas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TotalProductionUnit As Long Implements IFixedAssetPartsAccesoriesConsumables.TotalProductionUnit
        Get
            Return INDseTotalProductionUnit.EditValue
        End Get
        Set(value As Long)
            INDseTotalProductionUnit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del libro oficial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LegalBookId As Integer? Implements IFixedAssetPartsAccesoriesConsumables.LegalBookId
        Get
            Return INDsleLegalBook.EditValue
        End Get
        Set(value As Integer?)
            INDsleLegalBook.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la vida util
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LifeUtil As Integer Implements IFixedAssetPartsAccesoriesConsumables.LifeUtil
        Get
            Return INDseLifeUtil.EditValue
        End Get
        Set(value As Integer)
            INDseLifeUtil.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la unidad de la vida util
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UnitLifeUtilId As Integer? Implements IFixedAssetPartsAccesoriesConsumables.UnitLifeUtilId
        Get
            Return INDsleUnitLifeUtil.EditValue
        End Get
        Set(value As Integer?)
            INDsleUnitLifeUtil.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de depreciación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DepreciationTypeId As Integer? Implements IFixedAssetPartsAccesoriesConsumables.DepreciationTypeId
        Get
            Return INDsleDepreciationType.EditValue
        End Get
        Set(value As Integer?)
            INDsleDepreciationType.EditValue = value
        End Set
    End Property

    Public Property CodePartsAccesoriesConsumables As String Implements IFixedAssetPartsAccesoriesConsumables.CodePartsAccesoriesConsumables
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

    Public Property NamePartsAccesoriesConsumables As String Implements IFixedAssetPartsAccesoriesConsumables.NamePartsAccesoriesConsumables
        Get
            Return INDteName.Text
        End Get
        Set(value As String)
            INDteName.Text = value
        End Set
    End Property

    Public Property Sequence As Domain.Entities.FixedAssetSequence Implements IFixedAssetPartsAccesoriesConsumables.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As Domain.Entities.FixedAssetSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.FixedAssetSequenceDetail In Me._sequence.FixedAssetSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IFixedAssetPartsAccesoriesConsumables.ActionsOnControls
        Set(value As Boolean)
            INDlyTrademark.BeginUpdate()
            INDBteCode.Enabled = Not value
            INDteName.Enabled = value
            INDsleDepreciationType.Enabled = value
            INDsleAllowDepreciate.Enabled = value
            INDpceDetail.Enabled = value
            INDgcDetail.Enabled = value
            INDlyTrademark.EndUpdate()
            If value Then
                INDteName.Focus()
            Else
                INDBteCode.Focus()
            End If
        End Set
    End Property

    Private Sub INDBteCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBteCode.ButtonClick
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub AbrirBusqueda() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code"}, New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Name"}}.ToList
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetPartsAccesoriesConsumibles
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
        Set(ByVal value As String)

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
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDBteCode.Text = ReturnValue
        If INDBteCode.Text <> String.Empty Then
            Await LoadControls()
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
        End If
    End Sub

    Public Sub Buscar() Implements IcrudBase.Buscar
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de Marcas.
    ''' </summary>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
        'If searchMode = False Then
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        'Else
        '    If indigo.UserViewMode = True Then
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        '    End If
        'End If
    End Sub

    Public Async Sub Eliminar() Implements IcrudBase.Eliminar
        'If PartsAccesoriesConsumables IsNot Nothing And INDBteCode.Enabled = False Then
        '    If PartsAccesoriesConsumables.Id > 0 Then
        '        If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '            Try
        '                AsyncLoader(False)
        '                Using Model As New MFixedAssetPartsAccesoriesConsumables(MyBase.Tag)
        '                    If Await Model.DeletePartsAccesoriesConsumablesAsync(PartsAccesoriesConsumables) = True Then
        '                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
        '                        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        '                        searchMode = False
        '                        Deshacer()
        '                        Await Me.DeleteDocumentIndexed()
        '                    Else
        '                        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
        '                    End If
        '                End Using
        '                AsyncLoader(False)
        '            Catch ex As Exception
        '                Throw ex
        '                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
        '                AsyncLoader(False)
        '            End Try
        '        End If
        '    Else
        '        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(SeleccioneTorre, Torres)
        '    End If
        'Else
        '    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(SeleccioneTorre, Torres)
        'End If


        If Me.PartsAccesoriesConsumables IsNot Nothing AndAlso Me.PartsAccesoriesConsumables.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MFixedAssetPartsAccesoriesConsumables(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeletePartsAccesoriesConsumablesAsync(Me.PartsAccesoriesConsumables)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDBteCode.Enabled = False
                        End If
                        ShowMessage(result.StatusCode) = result.Message
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de Fondos.
    ''' </summary>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        'If ValidateControls() = False Then
        '    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
        '    Exit Sub
        'End If
        'AsyncLoader(True)
        'AssigningValues()
        'Using Model As New MFixedAssetPartsAccesoriesConsumables(MyBase.Tag)
        '    If Await Model.SavePartsAccesoriesConsumablesAsync(PartsAccesoriesConsumables, _idCurrentSequense) = True Then
        '        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
        '        If PartsAccesoriesConsumables.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesGuardado)
        '        ElseIf PartsAccesoriesConsumables.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Or PartsAccesoriesConsumables.ChangeTracker.State = Domain.Base.Entities.ObjectState.Unchanged Then
        '            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
        '        End If
        '        AsyncLoader(False)
        '        searchMode = False
        '        Deshacer()
        '    Else
        '        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
        '        AsyncLoader(False)
        '    End If
        'End Using



        If Not ValidateControls() Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MFixedAssetPartsAccesoriesConsumables(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of FixedAssetPartsAccesoriesConsumables) = Await Model.SavePartsAccesoriesConsumablesAsync(Me.PartsAccesoriesConsumables, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If PartsAccesoriesConsumables.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me.PartsAccesoriesConsumables = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        If Not String.IsNullOrEmpty(Me.PartsAccesoriesConsumables.Code) Then
            Try
                Using model As New MFixedAssetPartsAccesoriesConsumables(Me.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = True 'Not Me.PartsAccesoriesConsumables.State
                    Dim result As ActionResult(Of FixedAssetPartsAccesoriesConsumables) = Await model.ChangeState(Me.PartsAccesoriesConsumables.Code, state)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.PartsAccesoriesConsumables = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        AsyncLoader(False)
                    Else
                        AsyncLoader(False)
                        INDBteCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub



    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    Public Async Sub Nuevo() Implements IcrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewPartsAccesoriesConsumibles()
        End If
    End Sub


    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        searchMode = Nothing
        PartsAccesoriesConsumables = Nothing
        record = Nothing
        Model = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        Presenter = Nothing
        FlagEditMode = Nothing
        FlagPopup = Nothing
        ListUnitLifeUtil = Nothing
        ListDepreciationType = Nothing
        PartsDetail = Nothing
        ListPartsDetail = Nothing
        ListDeletePartsDetail = Nothing
        _idOperativeUnit = Nothing
    End Sub



    Private Sub FrmTrademark_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Presenter = New PFixedAssetPartsAccessoriesConsumibles(Me)
        ' Presenter.GetSequense()
        'indigo.AuditMessageWcf.Company = indigo.TransactionalContainer
        'InitializeTuple()
        ' ActionsOnControls = False
        'Dim ListActions As New List(Of eAcciones)
        'ListActions.Add(eAcciones.Remove)
        'ListActions.Add(eAcciones.Edit)
        'IndigoGridView1.SetListAcction(ViewDetail, ListActions)
        'CleanControls()


        'Me.LayoutControls.SetIsCustomizable(Me.INDLcBillingGroup, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PFixedAssetPartsAccessoriesConsumibles(Me)
        Presenter.GetSequense()

        indigo.AuditMessageWcf.Company = indigo.TransactionalContainer
        InitializeTuple()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(ViewDetail, ListActions)
        'Presenter.LoadDefinitionLayout()
        LoadStatus()
        Deshacer()
    End Sub

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        'ActionsOnControls = False
        'BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
        'INDBteCode.Text = String.Empty
        'INDteName.Text = String.Empty
        'INDsleAllowDepreciate.EditValue = False
        'INDgcDetail.DataSource = Nothing
        ' Me.BarraBotones.StatusRecordVisible = False
        'DeleteBlockedRecord()
        'Me._doc = Nothing
        'Me.BarraBotones.EnableBarItems()
        'Me.BarraBotones.DisableBarDocument()
        'Me.BarraBotones.CleanAuditBasic()
        'PartsAccesoriesConsumables = Nothing
        ' INDlygDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never


        INDlyTrademark.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing
        Status = True
        INDBteCode.Text = String.Empty
        INDteName.Text = String.Empty
        INDsleAllowDepreciate.EditValue = False
        INDgcDetail.DataSource = Nothing
        'Limpiar controles
        PartsAccesoriesConsumables = Nothing

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlyTrademark.EndUpdate()
        DeleteBlockedRecord()
    End Sub


    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        'Me.BarraBotones.StatusRecordVisible = True
        'AsyncLoader(True)
        'Using Model As New MFixedAssetPartsAccesoriesConsumables(MyBase.Tag)
        '    PartsAccesoriesConsumables = Await Model.GetPartsAccesoriesConsumablesAsync(INDBteCode.Text)
        'End Using
        'If Not PartsAccesoriesConsumables Is Nothing Then
        '    If PartsAccesoriesConsumables.Id > 0 Then
        '        Dim result = Await Model.GetBlockRecord(Me.Tag, PartsAccesoriesConsumables.Id)
        '        With PartsAccesoriesConsumables
        '            LogicaBotonActualizar(True)
        '            Me.BarraBotones.PrepareToolbar(eAction.UpdateOrDelete)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), PartsAccesoriesConsumables.CreationUser)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), PartsAccesoriesConsumables.CreationDate)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), PartsAccesoriesConsumables.ModificationUser)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), PartsAccesoriesConsumables.ModificationDate)
        '            CodePartsAccesoriesConsumables = .Code
        '            NamePartsAccesoriesConsumables = .Name
        '            INDsleAllowDepreciate.EditValue = .AllowDepreciate

        '            ListPartsDetail = .FixedAssetPartsAccesoriesConsumablesDetail.ToList()
        '            INDgcDetail.DataSource = Nothing
        '            INDgcDetail.DataSource = ListPartsDetail

        '        End With
        '        Me.GetDocumentIndexed(Me.Tag & "_" & Me.PartsAccesoriesConsumables.Code)
        '        If result.Id = 0 Then
        '            Me.BarraBotones.SetDocuments(PartsAccesoriesConsumables.Id)
        '            Dim state = New Domain.Base.Entities.ObjectChangeTracker
        '            state.State = Domain.Base.Entities.ObjectState.Added
        '            record = New BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = PartsAccesoriesConsumables.Id}
        '            Dim operation = Await Model.SaveBlockRecord(record)
        '            record = operation.ObjectEmbbeded
        '        Else
        '            record = result
        '            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
        '            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
        '        End If
        '        ActionsOnControls = True
        '    Else
        '        LogicaBotonActualizar(False)
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    End If
        'Else
        '    PartsAccesoriesConsumables = New FixedAssetPartsAccesoriesConsumables
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        'End If
        'AsyncLoader(False)
        'ActionsOnControls = True



        If Not String.IsNullOrEmpty(CodePartsAccesoriesConsumables) AndAlso Not String.IsNullOrWhiteSpace(CodePartsAccesoriesConsumables) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MFixedAssetPartsAccesoriesConsumables(CStr(Me.Tag))
                    AsyncLoader(True)
                    PartsAccesoriesConsumables = Await Model.GetPartsAccesoriesConsumablesAsync(INDBteCode.Text)
                    INDlyTrademark.BeginUpdate()
                    If PartsAccesoriesConsumables IsNot Nothing AndAlso PartsAccesoriesConsumables.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        ' Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                        record = Await Model.GetBlockRecord(CStr(Me.Tag), CStr(PartsAccesoriesConsumables.Id))
                        With PartsAccesoriesConsumables
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                            'Llenar Entidad
                            CodePartsAccesoriesConsumables = .Code
                            NamePartsAccesoriesConsumables = .Name
                            INDsleAllowDepreciate.EditValue = .AllowDepreciate

                            ListPartsDetail = .FixedAssetPartsAccesoriesConsumablesDetail.ToList()
                            INDgcDetail.DataSource = Nothing
                            INDgcDetail.DataSource = ListPartsDetail
                            Status = True
                        End With
                        'Llenar NullText
                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.PartsAccesoriesConsumables.Code)
                        If record.Id = 0 Then
                            record = (Await Model.SaveBlockRecord(
                                    New BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = PartsAccesoriesConsumables.Id})
                                    ).ObjectEmbbeded
                        Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                            Me.BarraBotones.SetDocuments(PartsAccesoriesConsumables.Id, Me.Tag.ToString(), Nothing, GetType(FrmFixedAssetPartsAccesoriesConsumables).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        ' End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewPartsAccesoriesConsumibles()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            CodePartsAccesoriesConsumables = String.Empty
                            INDBteCode.Focus()
                        End If
                    End If
                    INDlyTrademark.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewPartsAccesoriesConsumibles() As Task
        'Me.PartsAccesoriesConsumables = New Domain.Entities.FixedAssetPartsAccesoriesConsumables()
        'If Me._sequense.IsManual Then
        '    Me.ActionsOnControls = True
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        'Else
        '    If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
        '        Me._idCurrentSequense = Me._sequense.FixedAssetSequenceDetail(0).Id
        '    ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
        '        If Me._sequense.FixedAssetSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
        '            Me._idCurrentSequense = Me._sequense.FixedAssetSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
        '        Else
        '            Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("OperatingUnitUnassigned")
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '            Exit Sub
        '        End If
        '    End If
        '    If Not Me._sequense.Sequential Then
        '        If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
        '            If Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
        '                Me.CodePartsAccesoriesConsumables = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
        '                Me.ActionsOnControls = True
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '            Else
        '                Using model As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
        '                    Me.DicSequense(CInt(Me._idCurrentSequense)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequense))
        '                End Using
        '                If Me.DicSequense(CInt(Me._idCurrentSequense)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
        '                    Me.CodePartsAccesoriesConsumables = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
        '                    Me.ActionsOnControls = True
        '                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '                Else
        '                    Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("InvalidPatternSequense")
        '                End If
        '            End If
        '        Else
        '            Me.CodePartsAccesoriesConsumables = ResourceManager.GetString("LabelOrTextboxNew")
        '            Me.ActionsOnControls = True
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '        End If
        '    Else
        '        Me.CodePartsAccesoriesConsumables = ResourceManager.GetString("LabelOrTextboxNew")
        '        Me.ActionsOnControls = True
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '    End If
        'End If



        PartsAccesoriesConsumables = New FixedAssetPartsAccesoriesConsumables()
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
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
                Me.CodePartsAccesoriesConsumables = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.CodePartsAccesoriesConsumables = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.CodePartsAccesoriesConsumables = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.CodePartsAccesoriesConsumables = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
    End Function


#Region "Metodos Funciones Propiedades"
    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(obtenerRecurso(Eresources.FrmFundMetaData, Eform.InfoMetaData), Me.PartsAccesoriesConsumables.Code, Me.PartsAccesoriesConsumables.Name, INDteName.Text), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity =  "$#" & Me.Tag & "_" & Me.PartsAccesoriesConsumables.Code & "#$", .IdForm = Me.Tag, _
                .Title = String.Format(obtenerRecurso(Eresources.FrmFundMetaDataTitle, Eform.InfoMetaData), Me.PartsAccesoriesConsumables.Code), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmFundMetaData, Eform.InfoMetaData), Me.PartsAccesoriesConsumables.Code, Me.PartsAccesoriesConsumables.Name, INDteName.Text)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmFundMetaDataTitle, Eform.InfoMetaData), Me.PartsAccesoriesConsumables.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MFixedAssetPartsAccesoriesConsumables(CStr(Me.Tag))
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Sub
#End Region

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With PartsAccesoriesConsumables
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = CodePartsAccesoriesConsumables
            .Name = INDteName.EditValue
            .AllowDepreciate = INDsleAllowDepreciate.EditValue
            '.Status = Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active   ''Por si se añade campo de estado
            If INDlygDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then 'Si esta activo el grupo de detalles
                If ListPartsDetail IsNot Nothing AndAlso ListPartsDetail.Count > 0 Then
                    ListPartsDetail.ForEach(Sub(item)
                                                .FixedAssetPartsAccesoriesConsumablesDetail.Add(item)
                                            End Sub)
                End If

                If ListDeletePartsDetail IsNot Nothing AndAlso ListDeletePartsDetail.Count > 0 Then
                    ListDeletePartsDetail.ForEach(Sub(item)
                                                      .FixedAssetPartsAccesoriesConsumablesDetail.Add(item)
                                                  End Sub)
                End If
            End If

            'Select Case Status
            '    Case CBool(eActionsStatusRecords.Active)
            '        .Status = True
            '    Case CBool(eActionsStatusRecords.Inactive)
            '        .Status = False
            'End Select


        End With
    End Sub

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    Private Function ValidateControls() As Boolean
        ValidateControls = True
        If INDBteCode.Text = String.Empty Then
            INDBteCode.Focus()
            ValidateControls = False
            Exit Function
        End If
        If INDteName.Text = String.Empty Then
            INDteName.Focus()
            ValidateControls = False
            Exit Function
        End If
        If INDsleAllowDepreciate.Text = String.Empty Then
            INDsleAllowDepreciate.Focus()
            ValidateControls = False
            Exit Function
        End If
    End Function


    Private Async Sub INDBteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteCode.KeyDown

        'If e.KeyCode = System.Windows.Forms.Keys.Enter Then
        '    If _sequense Is Nothing OrElse _sequense.Id = 0 Then
        '        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
        '        Exit Sub
        '    End If

        '    INDteName.Focus()
        '    If String.IsNullOrEmpty(INDBteCode.Text) Then
        '        Me.NewPartsAccesoriesConsumibles()
        '    Else
        '        Await LoadControls()
        '    End If
        'End If



        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(CodePartsAccesoriesConsumables.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(CodePartsAccesoriesConsumables) Then
                    Await Me.NewPartsAccesoriesConsumibles()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            AbrirBusqueda()
        End If
    End Sub

#Region "bar buttons and events"
    ''' <summary>
    '''Evento load de la barra de fondos.
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
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        searchMode = False
        Me.Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        CleanControls()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click customizar.
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
        CustomizationOpen()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ clic restablecer layout.
    ''' </summary>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
        ResetLayout()
    End Sub

    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me.PartsAccesoriesConsumables IsNot Nothing AndAlso Me.PartsAccesoriesConsumables.Id > 0 Then
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

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBteCode.Enabled Then
            INDBteCode.Focus()
        End If
    End Sub
    Private Sub BarraBotones_ChangeOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
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
    Private Sub Frm_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

#End Region
#Region "Customizacion"
    ''' <summary>
    ''' Metodo para abrir el formulario de customizar el frontal
    ''' </summary>
    Private Sub CustomizationOpen()
        INDlyTrademark.ShowCustomizationForm()
    End Sub

    ''' <summary>
    ''' Metodo para restablecer las definiciones del formulario gridLookUpEdit y Regillas
    ''' </summary>
    Private Sub ResetLayout()
        If My.Computer.FileSystem.DirectoryExists(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
            My.Computer.FileSystem.DeleteDirectory(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name), Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
            INDlyTrademark.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub

#End Region

#Region "CloseUp"
    ''' <summary>
    ''' Limpia los controles del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopup()
        LegalBookId = Nothing
        INDsleLegalBook.Properties.NullText = String.Empty
        LifeUtil = Nothing
        UnitLifeUtilId = Nothing
        INDsleLegalBook.Properties.ReadOnly = False
        DepreciationTypeId = Nothing
        TotalProductionUnit = Nothing
        PercentageRescue = Nothing
        FlagEditMode = False
    End Sub
#End Region


#Region "MenuContext"

    ''' <summary>
    ''' Despliega los botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case btn.Tag.ToString
            Case "Edit"
                EditDetail()
            Case "Remove"
                DeleteDetail()
        End Select
    End Sub

    ''' <summary>
    ''' Accion de click derecho del mouse
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

#Region "CloseUp"
    Private Sub INDpceDetail_CloseUp(sender As Object, e As CloseUpEventArgs) Handles INDpceDetail.CloseUp
        If FlagEditMode = True AndAlso FlagPopup = True Then
            CleanControlsPopup()
        End If
    End Sub
#End Region

    ''' <summary>
    ''' Edita el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditDetail()
        PartsDetail = CType(ViewDetail.GetFocusedRow, FixedAssetPartsAccesoriesConsumablesDetail)
        FlagEditMode = True
        FlagPopup = True
        With PartsDetail
            LegalBookId = .LegalBookId
            INDsleLegalBook.Properties.NullText = .CodeNameLegalBook
            LifeUtil = .LifeTime
            UnitLifeUtilId = .UnitLifeTime
            TotalProductionUnit = .TotalProductionUnit
            PercentageRescue = .PercentageRescue

            INDsleLegalBook.Properties.ReadOnly = True
            INDseLifeUtil.Focus()

            DepreciationTypeId = .DepreciationType
        End With
        INDsleLegalBook.Focus()
    End Sub

    ''' <summary>
    ''' Elimina el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetail()
        PartsDetail = CType(ViewDetail.GetFocusedRow, FixedAssetPartsAccesoriesConsumablesDetail)
        ListPartsDetail.Remove(PartsDetail)

        If PartsDetail.Id > 0 Then
            If ListDeletePartsDetail Is Nothing Then
                ListDeletePartsDetail = New List(Of FixedAssetPartsAccesoriesConsumablesDetail)
            End If
            PartsDetail.MarkAsDeleted()
            ListDeletePartsDetail.Add(PartsDetail)
        End If

        INDgcDetail.DataSource = Nothing
        INDgcDetail.DataSource = ListPartsDetail
    End Sub

    ''' <summary>
    ''' Metodo que agrega un detalle a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddDetail()
        'Se valida que los controles esten diligenciados
        Dim errors As String = ValidateControlsPopup()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If

        If FlagEditMode = False Then 'Si se esta guardando
            If ListPartsDetail Is Nothing Then 'Si no hay registros en la rejilla
                ListPartsDetail = New List(Of FixedAssetPartsAccesoriesConsumablesDetail)
            Else 'Si ya hay registros en la rejilla
                'Se valida que el libro seleccionado en el search no exista en la rejilla
                If (From l In ListPartsDetail Where l.LegalBookId = LegalBookId Select l).Count > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El libro " + INDsleLegalBook.Text + " ya existe en la lista."
                    INDsleLegalBook.Focus()
                    Exit Sub
                End If
            End If

            Dim _partsDetail As New FixedAssetPartsAccesoriesConsumablesDetail
            'Se crea la nueva entidad para agregarlo al listado
            With _partsDetail
                .LegalBookId = LegalBookId
                .CodeNameLegalBook = INDsleLegalBook.Text
                .LifeTime = LifeUtil
                .UnitLifeTime = UnitLifeUtilId
                .DepreciationType = DepreciationTypeId
                If INDlyItemTotalProductionUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .TotalProductionUnit = TotalProductionUnit
                Else
                    .TotalProductionUnit = 0
                End If
                If INDlyItemPercentageRescue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .PercentageRescue = PercentageRescue
                Else
                    .PercentageRescue = 0
                End If
            End With
            Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente."
            ListPartsDetail.Add(_partsDetail)
        Else 'Si se esta editando
            'Se edita el objeto que se obiene cuando se edita
            With PartsDetail
                .LegalBookId = LegalBookId
                .CodeNameLegalBook = INDsleLegalBook.Text
                .LifeTime = LifeUtil
                .UnitLifeTime = UnitLifeUtilId
                .DepreciationType = DepreciationTypeId
                If INDlyItemTotalProductionUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .TotalProductionUnit = TotalProductionUnit
                Else
                    .TotalProductionUnit = 0
                End If
                If INDlyItemPercentageRescue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .PercentageRescue = PercentageRescue
                Else
                    .PercentageRescue = 0
                End If
            End With
            Mensaje(EeventViewerImages.Informacion) = "Detalle editado correctamente."
        End If

        INDgcDetail.DataSource = Nothing
        INDgcDetail.DataSource = ListPartsDetail
        CleanControlsPopup()
        FlagEditMode = False
        INDpceDetail.ShowPopup()
        INDsleLegalBook.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que valida los controles del popup
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup() As String
        Dim listErrors As New StringBuilder
        If LegalBookId Is Nothing Then
            listErrors.AppendLine("Ingrese un Libro Oficial.")
        End If
        If LifeUtil = Nothing OrElse LifeUtil = 0 Then
            listErrors.AppendLine("Ingrese Vida Util.")
        End If
        If UnitLifeUtilId Is Nothing Then
            listErrors.AppendLine("Ingrese una Unidad Vida Util.")
        End If
        If DepreciationTypeId Is Nothing Then
            listErrors.AppendLine("Ingrese Tipo Depreciación.")
        End If
        If INDlyItemTotalProductionUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If TotalProductionUnit = 0 Then
                listErrors.AppendLine("Ingrese Total Unidades.")
            End If
        End If
        If INDlyItemPercentageRescue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If PercentageRescue = 0 Then
                listErrors.AppendLine("Ingrese % Salvamento.")
            End If
        End If
        Return listErrors.ToString
    End Function

    ''' <summary>
    ''' Metodo que inicializa la tupla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuple()
        'Unidad vida util
        ListUnitLifeUtil = New List(Of Tuple(Of Integer, String))
        ListUnitLifeUtil.Add(New Tuple(Of Integer, String)(1, "Año"))
        ListUnitLifeUtil.Add(New Tuple(Of Integer, String)(2, "Mes"))
        ListUnitLifeUtil.Add(New Tuple(Of Integer, String)(3, "Día"))
        INDsleUnitLifeUtil.Properties.DataSource = ListUnitLifeUtil.ToList
        'Tipo de depreciación
        ListDepreciationType = New List(Of Tuple(Of Integer, String))
        ListDepreciationType.Add(New Tuple(Of Integer, String)(1, "Línea Recta"))
        ListDepreciationType.Add(New Tuple(Of Integer, String)(2, "Suma de Dígitos"))
        ListDepreciationType.Add(New Tuple(Of Integer, String)(3, "Reducción de Saldos"))
        ListDepreciationType.Add(New Tuple(Of Integer, String)(4, "Unidades de Producción"))
        INDsleDepreciationType.Properties.DataSource = ListDepreciationType.ToList
    End Sub

    Private Sub INDpceDetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceDetail.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter OrElse e.KeyCode = System.Windows.Forms.Keys.F4 Then
            INDpceDetail.ShowPopup()
            INDsleLegalBook.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para redimensionar el popup dependiendo del tipo de depreciación
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RezizablePopup()
        Dim size As System.Drawing.Size
        If DepreciationTypeId IsNot Nothing Then
            If DepreciationTypeId = 4 Then 'Unidades producidas
                INDlyItemTotalProductionUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemPercentageRescue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                size.Width = 432
                size.Height = 245
            ElseIf DepreciationTypeId = 3 Then 'Reducción de saldos
                INDlyItemTotalProductionUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemPercentageRescue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                size.Width = 432
                size.Height = 245
            Else 'Línea recta o suma de dígitos
                INDlyItemTotalProductionUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemPercentageRescue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                size.Width = 432
                size.Height = 205
            End If
        Else
            INDlyItemTotalProductionUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemPercentageRescue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            size.Width = 432
            size.Height = 205
        End If
        INDpceDetail.Properties.PopupSizeable = True
        INDpopupDetail.Size = size
        INDpceDetail.Properties.PopupSizeable = False
        If DepreciationTypeId IsNot Nothing Then
            INDpceDetail.ShowPopup()
            INDsleDepreciationType.Focus()
        End If
    End Sub

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar del popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddDetail_Click(sender As Object, e As EventArgs) Handles INDbtnAddDetail.Click
        AddDetail()
    End Sub

#End Region

#Region "QueryPopup"
    Private Sub INDsleLegalBook_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleLegalBook.QueryPopUp
        If LegalBookXpo Is Nothing Then
            Presenter.InitializeLegalBook()
        End If
    End Sub
#End Region

#Region "EditValueChanged"
    Private Sub INDsleDepreciationType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleDepreciationType.EditValueChanged
        FlagPopup = False
        RezizablePopup()
        FlagPopup = True
    End Sub

    Private Sub INDsleAllowDepreciate_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAllowDepreciate.EditValueChanged
        If AllowDepreciate IsNot Nothing Then
            If AllowDepreciate Then 'Si permite
                INDlygDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else 'No permite
                INDlygDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End If
    End Sub
#End Region

    Private Sub ViewDetail_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles ViewDetail.CustomColumnDisplayText
        If e.Value Is Nothing OrElse e.Value.GetType.ToString = "DevExpress.Data.NotLoadedObject" Then
            Exit Sub
        End If
        If e.Column.Name = INDcolUnitLifeTime.Name Then
            Select Case e.Value
                Case 1
                    e.DisplayText = "Año"
                Case 2
                    e.DisplayText = "Mes"
                Case 3
                    e.DisplayText = "Día"
                Case Else

            End Select
        End If
        If e.Column.Name = INDcolDepreciationType.Name Then
            Select Case e.Value
                Case 1
                    e.DisplayText = "Línea Recta"
                Case 2
                    e.DisplayText = "Suma de Dígitos"
                Case 3
                    e.DisplayText = "Reducción de Saldos"
                Case 4
                    e.DisplayText = "Unidades de Producción"
                Case Else

            End Select
        End If
    End Sub
End Class