'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Judy Andrea Díaz Reyes
' Created          : 20-05-2019
'
' Last Modified By :
' Last Modified On :
' Description      :
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Presentation.Accounting.MVP
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.MixingStation.MVP

#End Region

Public Class FrmUnitDoseType
    Implements IUnitDoseType

#Region "Fields"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "MixingStation"

    ''' <summary>
    ''' Variable que contiene la lista de las clases para el tipo de dosis unitaria
    ''' </summary>
    Dim ListmsClass As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Representa la entidad de Tipo de Dosis Unitaria
    ''' </summary>
    Dim _unitDoseTypeEntity As UnitDoseType

    ''' <summary>
    ''' Referencia la presentador
    ''' </summary>
    Private _presenter As PUnitDoseType

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As MixingStationSequence

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Id de la configuración de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As BlockRecordMixingStation

    ''' <summary>
    ''' Variable para saber si el frontal abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean

#End Region

#Region "Propierties IUnitDoseType"

    ''' <summary>
    ''' Obtiene o establece el codigo del turno
    ''' </summary>
    Public Property Code As String Implements IUnitDoseType.Code
        Get
            If (INDbtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDbtnCode.Text
            End If
        End Get
        Set(value As String)
            INDbtnCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado del turno
    ''' </summary>
    ''' <value></value>
    Public Property State As Boolean Implements IUnitDoseType.State
        Get
            Return CBool(BarraBotones.StatusRecord)
        End Get
        Set(value As Boolean)
            If value = True Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la descripción del turno
    ''' </summary>
    ''' <value></value>
    Public Property Description As String Implements IUnitDoseType.Description
        Get
            Return INDtxtDescription.Text
        End Get
        Set(value As String)
            INDtxtDescription.Text = value
        End Set
    End Property

    Public Property msClass As Integer Implements IUnitDoseType.msClass
        Get
            Return CStr(INDsleClass.EditValue)
        End Get
        Set(value As Integer)
            INDsleClass.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value></value>
    Public Property Sequence As MixingStationSequence Implements IUnitDoseType.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As MixingStationSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.MixingStationSequenceDetail In Me._sequence.MixingStationSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para habilitar o deshabilitar controles
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IUnitDoseType.ActionsOnControls
        Set(value As Boolean)
            INDlycBase.BeginUpdate()

            INDbtnCode.Enabled = Not value
            INDtxtDescription.Enabled = value
            INDsleClass.Enabled = value
            INDSleInventoryGroup.Enabled = value
            INDSleInventorySubgroup.Enabled = value
            INDSleIvaCode.Enabled = value
            INDSleBillingGroup.Enabled = value
            INDtxtPrefix.Enabled = value

            INDlycBase.EndUpdate()
            If Not value Then
                INDbtnCode.Focus()
            Else
                INDtxtDescription.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property MyTag As Object Implements IUnitDoseType.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IUnitDoseType.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

#End Region

#Region "Propierties ICrudBase"

    ''' <summary>
    ''' Propiedad que establece los mensajes (Advertencias)
    ''' </summary>
    ''' <param name="icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
        Set(value As String)
            If icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Grupo de inventarios
    ''' </summary>
    ''' <returns></returns>
    Public Property InventoryGroupId As Integer? Implements IUnitDoseType.InventoryGroupId
        Get
            Return INDSleInventoryGroup.EditValue
        End Get
        Set(value As Integer?)
            INDSleInventoryGroup.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Subgrupo de inventarios
    ''' </summary>
    ''' <returns></returns>
    Public Property InventorySubGroupId As Integer? Implements IUnitDoseType.InventorySubGroupId
        Get
            Return INDSleInventorySubgroup.EditValue
        End Get
        Set(value As Integer?)
            INDSleInventorySubgroup.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Código iva
    ''' </summary>
    ''' <returns></returns>
    Public Property IVACodeId As Integer? Implements IUnitDoseType.IVACodeId
        Get
            Return INDSleIvaCode.EditValue
        End Get
        Set(value As Integer?)
            INDSleIvaCode.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' grupo de facturacion
    ''' </summary>
    ''' <returns></returns>
    Public Property BillingGroupId As Integer? Implements IUnitDoseType.BillingGroupId
        Get
            Return INDSleBillingGroup.EditValue
        End Get
        Set(value As Integer?)
            INDSleBillingGroup.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Evento barra de botones Buscar
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Deshace los cambios hechos en el formulario
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Elimina el tipo de dosis unitaria seleccionado
    ''' </summary>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar

        If Me._unitDoseTypeEntity IsNot Nothing AndAlso Me._unitDoseTypeEntity.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MUnitDoseType(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteUnitDoseTypeAsync(Me._unitDoseTypeEntity)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDbtnCode.Enabled = False
                        End If
                        ShowMessage(result.StatusCode) = result.Message
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDbtnCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If

    End Sub

    ''' <summary>
    ''' Guarda el turno
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If Not ValidateControls() Then
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MUnitDoseType(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of UnitDoseType) = Await Model.SaveUnitDoseTypeAsync(Me._unitDoseTypeEntity, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If _unitDoseTypeEntity.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me._unitDoseTypeEntity = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDbtnCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbtnCode.Enabled = False
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' Indica que el dato ya existe y se va a actualizar
    ''' </summary>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
    End Sub

    ''' <summary>
    ''' Limpia el formulario para iniciar
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewUnitDosetype()
        End If
    End Sub

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Evento barra de botones Activo - Inactivo
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        SearchMode = False
        Deshacer()
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
        Deshacer()
        Me.Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.MixingStationSequenceDetail IsNot Nothing Then
                If Not Me._sequence.MixingStationSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmUnitDoseType_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        _presenter = New PUnitDoseType(Me)
        _presenter.GetSequence()
        LoadStatus()
        Deshacer()
        CargarmsClass()

    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ListmsClass = Nothing
        _presenter = Nothing
        _unitDoseTypeEntity = Nothing
        _record = Nothing
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmUnitDoseType_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedrecord()
    End Sub

#End Region

#Region "KwyDown"

    ''' <summary>
    ''' Evento para consultar un turno
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown

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
                    Await Me.NewUnitDosetype()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

    ''' <summary>
    ''' Evento que se dispara al activarse el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmUnitDoseType_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbtnCode.Text Is String.Empty Then
            INDbtnCode.Focus()
        End If
    End Sub

    ''' <summary>
    ''' cargar datasource 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleInventoryGroup_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleInventoryGroup.QueryPopUp
        If INDSleInventoryGroup.Properties.DataSource Is Nothing AndAlso Not INDSleInventoryGroup.ReadOnly Then
            Using model As New Inventory.MVP.MGroup(Tag)
                INDSleInventoryGroup.Properties.DataSource = model.ListProductGroupsByState(True)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' cargar datasource 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleInventorySubgroup_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleInventorySubgroup.QueryPopUp
        If INDSleInventorySubgroup.Properties.DataSource Is Nothing AndAlso Not INDSleInventorySubgroup.ReadOnly Then
            Using model As New Inventory.MVP.MGroup(Tag)
                INDSleInventorySubgroup.Properties.DataSource = model.ListProductSubGroupsByStateAndHandlesBatch(True, True)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' cargar datasource 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleIvaCode_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleIvaCode.QueryPopUp
        If INDSleIvaCode.Properties.DataSource Is Nothing AndAlso Not INDSleIvaCode.ReadOnly Then
            Using model As New MGeneralLedgerIVA(Tag)
                INDSleIvaCode.Properties.DataSource = model.ListGeneralLedgerIvaByState(True)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' cargar datasource 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleBillingGroup_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleBillingGroup.QueryPopUp
        If INDSleBillingGroup.Properties.DataSource Is Nothing AndAlso Not INDSleBillingGroup.ReadOnly Then
            Using model As New Billing.MVP.MBillingGroup(Tag)
                INDSleBillingGroup.Properties.DataSource = model.ListBillingGroupByState(True)
            End Using
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que retorna el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Await DeleteBlockedrecord()
        Code = ReturnValue
        If Code IsNot String.Empty Then
            Await LoadControls()
            If Not INDbtnCode.Enabled Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Metodo que elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedrecord() As Task
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequenceMixingStation(CStr(Me.Tag))
                Await model.DeleteBlockRecord(_record)
                _record = Nothing
            End Using
        End If
    End Function

    ''' <summary>
    ''' Metodo para cargar las clases para los tipos de dosis unitarias
    ''' </summary>
    Private Sub CargarmsClass()
        ListmsClass = New List(Of Tuple(Of Integer, String))
        ListmsClass.Add(New Tuple(Of Integer, String)(2, "NPT"))
        ListmsClass.Add(New Tuple(Of Integer, String)(3, "Antibioticoterapia"))
        ListmsClass.Add(New Tuple(Of Integer, String)(4, "Citostático"))
        ListmsClass.Add(New Tuple(Of Integer, String)(5, "Reempaque"))
        ListmsClass.Add(New Tuple(Of Integer, String)(7, "Reenvase"))
        ListmsClass.Add(New Tuple(Of Integer, String)(9, "Magistral"))
        ListmsClass.Add(New Tuple(Of Integer, String)(10, "Otros estériles"))

        INDsleClass.Properties.DataSource = ListmsClass.ToList
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MUnitDoseType(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetUnitDoseTypeAsync(INDbtnCode.Text.Trim)
                    INDlycBase.BeginUpdate()
                    _unitDoseTypeEntity = resultOperation.ObjectEmbbeded
                    If _unitDoseTypeEntity IsNot Nothing AndAlso _unitDoseTypeEntity.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequenceMixingStation(CStr(Me.Tag))
                            _record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_unitDoseTypeEntity.Id))
                            With _unitDoseTypeEntity
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                'Llenar Entidad
                                Code = .Code
                                Description = .Description
                                State = .State
                                msClass = .MSClass
                                InventoryGroupId = .InventoryGroupId
                                InventorySubGroupId = .InventorySubGroupId
                                IVACodeId = .IVACodeId
                                BillingGroupId = .BillingGroupId

                                INDSleInventoryGroup.Properties.NullText = .InventoryGroupCodeName
                                INDSleInventorySubgroup.Properties.NullText = .InventorySubGroupCodeName
                                INDSleIvaCode.Properties.NullText = .IVaCodeName
                                INDSleBillingGroup.Properties.NullText = .BillingGroupCodeName
                                INDtxtPrefix.EditValue = .Prefix
                            End With
                            'Llenar NullText

                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._unitDoseTypeEntity.Code)
                            If _record.Id = 0 Then
                                _record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordMixingStation With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _unitDoseTypeEntity.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _record.CodUser, _record.NameUser, _record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _record.CodUser)
                            End If
                            Me.BarraBotones.SetDocuments(_unitDoseTypeEntity.Id, Me.Tag.ToString(), Nothing, GetType(AccountPayableConceptNotes).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewUnitDosetype()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbtnCode.Focus()
                        End If
                    End If
                    INDlycBase.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Metodo que prepara los controles y realiza la logica para crear una nueva dependencia
    ''' </summary>
    Private Async Function NewUnitDosetype() As Task
        _unitDoseTypeEntity = New UnitDoseType() With {.State = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.MixingStationSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.MixingStationSequenceDetail.Any(Function(o) o.IdOperatingUnit IsNot Nothing AndAlso o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.MixingStationSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit IsNot Nothing AndAlso o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequenceMixingStation(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenceGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
    End Function

    ''' <summary>
    ''' Metodo que abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {
                              New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Description", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4)},
                              New ColumnInfo() With {.Caption = "Clase", .FieldName = "ClassName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListUnitDoseType
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Metodo que cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task

        If Not String.IsNullOrEmpty(Me._unitDoseTypeEntity.Code) Then
            Try
                Using model As New MUnitDoseType(Me.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not Me._unitDoseTypeEntity.State
                    Dim result As ActionResult(Of UnitDoseType) = Await model.ChangeState(Me._unitDoseTypeEntity.Code, state)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me._unitDoseTypeEntity = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        AsyncLoader(False)
                    Else
                        AsyncLoader(False)
                        INDbtnCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function

    ''' <summary>
    ''' Metodo que asigna los valores
    ''' </summary>
    Private Sub AssigningValues()
        With _unitDoseTypeEntity
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Description = Description
            .MSClass = msClass
            .InventoryGroupId = InventoryGroupId
            .InventorySubGroupId = InventorySubGroupId
            .IVACodeId = IVACodeId
            .BillingGroupId = BillingGroupId
            .Prefix = INDtxtPrefix.EditValue
        End With
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()

        INDlycBase.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing
        State = True

        'Limpiar controles
        Code = String.Empty
        Description = String.Empty
        msClass = 10
        InventoryGroupId = Nothing
        InventorySubGroupId = Nothing
        IVACodeId = Nothing
        BillingGroupId = Nothing
        INDSleInventoryGroup.Properties.NullText = String.Empty
        INDSleInventorySubgroup.Properties.NullText = String.Empty
        INDSleIvaCode.Properties.NullText = String.Empty
        INDSleBillingGroup.Properties.NullText = String.Empty
        INDtxtPrefix.EditValue = Nothing

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlycBase.EndUpdate()
        DeleteBlockedrecord()
    End Sub

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

#End Region

End Class