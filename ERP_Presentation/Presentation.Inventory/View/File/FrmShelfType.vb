'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Judy Andrea Díaz Reyes
' Created          : 24-05-2019
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
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Inventory.MVP

#End Region

Public Class FrmShelfType
	Implements IShelfType

#Region "Fields"

	''' <summary>
	''' Nombre del módulo al que pertenece el frontal
	''' </summary>
	Private Const NAME_MODULE As String = "Inventory"

	''' <summary>
	''' Representa la entidad de Tipo de Estante
	''' </summary>
	Dim _shelfTypeEntity As ShelfType

	''' <summary>
	''' Referencia la presentador
	''' </summary>
	Private _presenter As PShelfType

	''' <summary>
	''' Secuencia numerica del formulario
	''' </summary>
	Private _sequence As InventorySequence

	''' <summary>
	''' Id del Tipo de Estante
	''' </summary>
	Private _idOperativeUnit As Integer

	''' <summary>
	''' Id de la configuración de secuencia seleccionada
	''' </summary>
	Private _idCurrentSequence As Int64

	''' <summary>
	''' Objeto registro bloqueado
	''' </summary>
	Private _record As BlockRecordInventory

	''' <summary>
	''' Variable para saber si el frontal abre por modo busqueda
	''' </summary>
	Dim SearchMode As Boolean

#End Region

#Region "Propierties IShelfType"

	''' <summary>
	''' Obtiene o establece el codigo del estante
	''' </summary>
	Public Property Code As String Implements IShelfType.Code
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
	''' Obtiene o establece el estado del estante
	''' </summary>
	''' <value></value>
	Public Property State As Boolean Implements IShelfType.State
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
	''' Obtiene o establece la descripción del estante
	''' </summary>
	''' <value></value>
	Public Property Description As String Implements IShelfType.Description
		Get
			Return INDtxtDescription.Text
		End Get
		Set(value As String)
			INDtxtDescription.Text = value
		End Set
	End Property

	Public Property Large As Decimal Implements IShelfType.Large
		Get
			Return CStr(INDtxtLarge.EditValue)
		End Get
		Set(value As Decimal)
			INDtxtLarge.EditValue = value
		End Set
	End Property

	Public Property Wide As Decimal Implements IShelfType.Wide
		Get
			Return CStr(INDtxtWide.EditValue)
		End Get
		Set(value As Decimal)
			INDtxtWide.EditValue = value
		End Set
	End Property

	Public Property Deep As Decimal Implements IShelfType.Deep
		Get
			Return CStr(INDtxtDeep.EditValue)
		End Get
		Set(value As Decimal)
			INDtxtDeep.EditValue = value
		End Set
	End Property

	Public Property PartitionXDeep As Decimal Implements IShelfType.PartitionXDeep
		Get
			Return CStr(INDtxtPartitionXDeep.EditValue)
		End Get
		Set(value As Decimal)
			INDtxtPartitionXDeep.EditValue = value
		End Set
	End Property

	Public Property Partitions As Decimal Implements IShelfType.Partitions
		Get
			Return CStr(INDtxtPartitions.EditValue)
		End Get
		Set(value As Decimal)
			INDtxtPartitions.EditValue = value
		End Set
	End Property

	Public Property LocationXPartition As Decimal Implements IShelfType.LocatioXPartition
		Get
			Return CStr(INDtxtLocationXPartition.EditValue)
		End Get
		Set(value As Decimal)
			INDtxtLocationXPartition.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o asigna la secuencia numerica del formulario
	''' </summary>
	''' <value></value>
	Public Property Sequence As InventorySequence Implements IShelfType.Sequence
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
	''' Propiedad para habilitar o deshabilitar controles
	''' </summary>
	Public WriteOnly Property ActionsOnControls As Boolean Implements IShelfType.ActionsOnControls
		Set(value As Boolean)
			INDlycBase.BeginUpdate()

			INDbtnCode.Enabled = Not value
			INDtxtDescription.Enabled = value
			INDtxtLarge.Enabled = value
			INDtxtWide.Enabled = value
			INDtxtDeep.Enabled = value
			INDtxtPartitionXDeep.Enabled = value
			INDtxtPartitions.Enabled = value
			INDtxtLocationXPartition.Enabled = value

			INDlycBase.EndUpdate()
			If value Then
				INDbtnCode.Focus()
			End If
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el tag del formulario
	''' </summary>
	''' <returns>Tag del formulario</returns>
	Public ReadOnly Property MyTag As Object Implements IShelfType.MyTag
		Get
			Return Me.Tag
		End Get
	End Property

	Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IShelfType.MyLayoutControl
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
	Public WriteOnly Property Mensaje(icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
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
	''' Evento barra de botones Buscar
	''' </summary>
	''' <remarks></remarks>
	Public Sub Buscar() Implements IcrudBase.Buscar
		OpenSearch()
	End Sub

	''' <summary>
	''' Deshace los cambios hechos en el formulario
	''' </summary>
	Public Sub Deshacer() Implements IcrudBase.Deshacer
		CleanControls()
	End Sub

	''' <summary>
	''' Elimina el tipo de estante seleccionado
	''' </summary>
	Public Async Sub Eliminar() Implements IcrudBase.Eliminar

		If Me._shelfTypeEntity IsNot Nothing AndAlso Me._shelfTypeEntity.Id > 0 Then
			If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
				Try
					Using Model As New MShelfType(Me.Tag.ToString())
						AsyncLoader(True)
						Dim result = Await Model.DeleteShelfTypeAsync(Me._shelfTypeEntity)
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
	''' Guarda el tipo de estante
	''' </summary>
	Public Async Sub Guardar() Implements IcrudBase.Guardar
		If Not ValidateControls() Then
			Exit Sub
		End If
		AssigningValues()
		Try
			Using Model As New MShelfType(Me.Tag.ToString())
				AsyncLoader(True)
				Dim result As ActionResult(Of ShelfType) = Await Model.SaveShelfType(Me._shelfTypeEntity, Me._idCurrentSequence)
				If result.StatusCode = eStatusResult.SUCCESS Then
					If _shelfTypeEntity.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
						If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
							Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
						End If
					End If
					Me._shelfTypeEntity = result.ObjectEmbbeded
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
	Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar
	End Sub

	''' <summary>
	''' Limpia el formulario para iniciar
	''' </summary>
	Public Async Sub Nuevo() Implements IcrudBase.Nuevo
		If Me._sequence.IsManual Then
			Deshacer()
		Else
			Await NewShelftype()
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
			If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.InventorySequenceDetail IsNot Nothing Then
				If Not Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
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
	Private Sub FrmShelfType_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
		'****Inicializar variables*****'
		Me._doc = Nothing
		Me.indigo = SessionValues.Instance
		_presenter = New PShelfType(Me)
		_presenter.GetSequence()
		LoadStatus()
		Deshacer()

	End Sub

	Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
		_presenter = Nothing
		_shelfTypeEntity = Nothing
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
	Private Sub FrmShelfType_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
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
					Await Me.NewShelftype()
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
	Private Sub FrmShelfType_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
		If INDbtnCode.Text Is String.Empty Then
			INDbtnCode.Focus()
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
			Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
				Await model.DeleteBlockRecord(_record)
				_record = Nothing
			End Using
		End If
	End Function

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
				Using Model As New MShelfType(CStr(Me.Tag))
					AsyncLoader(True)
					Dim resultOperation = Await Model.GetShelfTypeAsync(INDbtnCode.Text.Trim)
					INDlycBase.BeginUpdate()
					_shelfTypeEntity = resultOperation.ObjectEmbbeded
					If _shelfTypeEntity IsNot Nothing AndAlso _shelfTypeEntity.Id > 0 Then
						Me.BarraBotones.StatusRecordVisible = True

						Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
							_record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_shelfTypeEntity.Id))
							With _shelfTypeEntity
								LayoutControls.SetCustomFieldsValue(.CustomProperties)
								Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
								Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
								Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
								Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

								'Llenar Entidad
								Code = .Code
								Description = .Description
								State = .State
								Large = .Large
								Wide = .Wide
								Deep = .Deep
								PartitionXDeep = .PartitionXDeep
								Partitions = .Partitions
								LocationXPartition = .LocationXPartition
							End With
							'Llenar NullText
							Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._shelfTypeEntity.Code)
							If _record.Id = 0 Then
								_record = (Await ModelRecord.SaveBlockRecord(
									New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
										.NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .Id = _shelfTypeEntity.Id})
									).ObjectEmbbeded
							Else
								Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _record.CodUser, _record.NameUser, _record.BlockDate)
								Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _record.CodUser)
							End If
							Me.BarraBotones.SetDocuments(_shelfTypeEntity.Id, Me.Tag.ToString(), Nothing, GetType(AccountPayableConceptNotes).Name)
							Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
							AsyncLoader(False)
							ActionsOnControls = True
						End Using
					Else
						AsyncLoader(False)
						If Me._sequence.IsManual Then
							Await Me.NewShelftype()
							INDtxtAvailableLocations.Focus()
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
	Private Async Function NewShelftype() As Task
        _shelfTypeEntity = New ShelfType() With {.State = True}
        If Me._sequence.IsManual Then
			Me.ActionsOnControls = True
			Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
			Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
			Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
		Else
			If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
				Me._idCurrentSequence = Me._sequence.InventorySequenceDetail(0).Id
			ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
				If Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit IsNot Nothing AndAlso o.IdOperatingUnit = Me._idOperativeUnit) Then
					Me._idCurrentSequence = Me._sequence.InventorySequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit IsNot Nothing AndAlso o.IdOperatingUnit = Me._idOperativeUnit).Id
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
						Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
							Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
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
	Public Sub OpenSearch() Implements IcrudBase.OpenSearch
		If BarraBotones.PermiteConsultar = False Then
			Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
			Exit Sub
		End If
		FormSearchObjects = New FrmBusqueda
		AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
		With FormSearchObjects
			.ListaColumnas = {
							  New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
							  New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Description", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4)}}.ToList
			.ValorSolicitado = "Code"
			.ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListShelfType
			.FormParent = Me
			.ShowSearch()
		End With
	End Sub

	''' <summary>
	''' Metodo que cambia el estado de la entidad
	''' </summary>
	''' <remarks></remarks>
	Private Async Function ChangeState() As Task

		If Not String.IsNullOrEmpty(Me._shelfTypeEntity.Code) Then
			Try
                Using model As New MShelfType(Me.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not _shelfTypeEntity.State
                    Dim result As ActionResult(Of ShelfType) = Await model.UpdateState(Me._shelfTypeEntity.Code, State)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me._shelfTypeEntity = result.ObjectEmbbeded
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
		With _shelfTypeEntity
			.CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Description = Description
			.Large = Large
			.Wide = Wide
			.Deep = Deep
			.PartitionXDeep = PartitionXDeep
			.Partitions = Partitions
			.LocationXPartition = LocationXPartition
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
		Large = 0
		Wide = 0
		Deep = 1
		PartitionXDeep = 1
		Partitions = 0
		LocationXPartition = 0
		Me.INDtxtAvailableLocations.Text = ""

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

	Private Sub INDtxtLocationXPartition_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtLocationXPartition.EditValueChanged
		Me.INDtxtAvailableLocations.Text = PartitionXDeep * Partitions * LocationXPartition
	End Sub

	Private Sub INDtxtPartitionXDeep_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtPartitionXDeep.EditValueChanged
		Me.INDtxtAvailableLocations.Text = PartitionXDeep * Partitions * LocationXPartition
	End Sub

	Private Sub INDtxtPartitions_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtPartitions.EditValueChanged
		Me.INDtxtAvailableLocations.Text = PartitionXDeep * Partitions * LocationXPartition
	End Sub

#End Region

End Class