'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Cristian Salazar
' Created          : 25-04-2013
'
' Last Modified By : Kevin Garay Rodriguez
' Last Modified On : 24-06-2013
' Description      : Refactoring
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"

Imports System.ComponentModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Common.MVP
Imports Presentation.Controls

#End Region


Public Class FrmCostCenter
    Implements ICostCenter, ICustomizableForm

#Region "Variables, propiedades y load"

#Region "Constant"

    ''' <summary>
    ''' Constante que contiene el nombre del modulo
    ''' </summary>
    Private Const NAME_MODULE As String = "Accounting"

#End Region

#Region "Variables"

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.GeneralLedgerSequence
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecord

    Dim dtFieldsCustomizables As DataTable

    ''' <summary>
    ''' Bandera que se utiliza para verificar si existe o no una definicion del funcional 
    ''' </summary>
    Dim ExistDefinitionFront As Boolean
    ''' <summary>
    ''' Variable que contiene la ruta de las definiciones del layout
    ''' </summary>
    Dim PathFunctionalDefinitions As String

    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PCostCenter

    ''' <summary>
    ''' Variable donde se almacena el centro de costo
    ''' </summary>
    Dim costCenter As Domain.Payroll.Entities.CostCenter

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequense As Int64

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

#End Region

    ''' <summary>
    ''' Variable para poder acceder al modelo
    ''' </summary>
    ''' <remarks></remarks>
    Dim Model As New MCostCenter(Me.Tag)


    'Dim ModoBusqueda As Boolean
    ''' <summary>
    ''' Metodo para definir la accion de los controles en el formulario
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ICostCenter.ActionsOnControls
        Set(value As Boolean)
            INDLyCostCenter.BeginUpdate()
            INDBteCodeCostCenter.Enabled = Not value
            INDNameCostCenter.Enabled = value
            INDSleBranchOffice.Enabled = value
            INDLyCostCenter.EndUpdate()
            If value Then
                INDNameCostCenter.Focus()
            Else
                INDBteCodeCostCenter.Focus()
            End If
        End Set
    End Property
    ''' <summary>
    ''' Metodo que define la lógica si se actualiza o se guarda nuevo registro
    ''' </summary>
    ''' <param name="existeDatos"></param>
    ''' <remarks></remarks>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub
    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
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
    ''' Boton Nuevo
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewCostCenter()
        End If
    End Sub

    ''' <summary>
    ''' Propiedad de Secuencia
    ''' </summary>
    ''' <returns></returns>
    Public Property Sequence As GeneralLedgerSequence Implements ICostCenter.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As GeneralLedgerSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.GeneralLedgerSequenceDetail In Me._sequence.GeneralLedgerSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property


    ''' <summary>
    ''' Propiedad que retorna el layout para customizaciones
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements ICostCenter.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property
    ''' <summary>
    ''' Propiedad que devuelve el tag del formulario para hacer interfaz con el presentador
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyTag As Object Implements ICostCenter.MyTag
        Get
            Return Me.Tag
        End Get
    End Property
    ''' <summary>
    ''' Propiedad que contiene el código del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CodeCostCenter As String Implements ICostCenter.CodeCostCenter
        Get
            If (INDBteCodeCostCenter.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDBteCodeCostCenter.Text
            End If
        End Get
        Set(value As String)
            INDBteCodeCostCenter.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el nombre del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NameCostCenter As String Implements ICostCenter.NameCostCenter
        Get
            Return INDNameCostCenter.Text
        End Get
        Set(value As String)
            INDNameCostCenter.Text = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene el estado del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property StatusCostCenter As Boolean Implements ICostCenter.StatusCostCenter
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

    Private Sub FrmCostCenter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDLyCostCenter, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PCostCenter(Me)
        Presenter.GetSequence()
        Presenter.LoadDefinitionLayout()
        LoadStatus()
        Deshacer()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de sucursales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleBranchOffice_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleBranchOffice.QueryPopUp
        If INDSleBranchOffice.Properties.DataSource Is Nothing Then
            INDSleBranchOffice.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).PayrollService.GetBranchOffice()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MCostCenter(CStr(Me.Tag))
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.costCenter IsNot Nothing AndAlso Me.costCenter.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDBteCodeCostCenter.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBteCodeCostCenter.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub


#End Region

#Region "Metodos"


    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        INDLyCostCenter.BeginUpdate()
        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing
        StatusCostCenter = True
        'Limpiar controles
        INDBteCodeCostCenter.Text = String.Empty
        INDNameCostCenter.Text = String.Empty
        INDSleBranchOffice.EditValue = Nothing
        INDSleBranchOffice.Properties.NullText = String.Empty

        costCenter = Nothing
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
        INDLyCostCenter.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Load del barra botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(Me.Tag))
    End Sub

    ''' <summary>
    ''' Boton buscar del barra botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Boton Deshacer del barrabotones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Esta función se utiliza para cambiar el estado (activo o inactivo) de la entidad "City"
    ''' </summary>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

    ''' <summary>
    ''' Reestablecer layout principal de customizacion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
        ResetLayout()
    End Sub

    ''' <summary>
    ''' borra el registro bloqueado antes de cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmCostCenter_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Evento key down para capturar enter en el codigo y buscar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDBECodigo_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteCodeCostCenter.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(CodeCostCenter.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(CodeCostCenter) Then
                    Await Me.NewCostCenter()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            AbrirBusqueda()
        End If
    End Sub

    Private Async Function NewCostCenter() As Task
        costCenter = New Domain.Payroll.Entities.CostCenter() With {.State = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequense = Me._sequence.GeneralLedgerSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.GeneralLedgerSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequense = Me._sequence.GeneralLedgerSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.CodeCostCenter = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                        Me.CodeCostCenter = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MCostCenter(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequense)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequense))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequense)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                            Me.CodeCostCenter = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.CodeCostCenter = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task

        If Not String.IsNullOrEmpty(CodeCostCenter) AndAlso Not String.IsNullOrWhiteSpace(CodeCostCenter) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Me.BarraBotones.StatusRecordVisible = True
                Using Model As New MCostCenter(CStr(Me.Tag))
                    AsyncLoader(True)
                    costCenter = Await Model.GetCostCenterAsync(INDBteCodeCostCenter.Text.Trim)
                    INDLyCostCenter.BeginUpdate()
                    If costCenter IsNot Nothing AndAlso costCenter.Id > 0 Then
                        'Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                        record = Await Model.GetBlockRecord(CStr(Me.Tag), CStr(costCenter.Id))
                        With costCenter
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                            CodeCostCenter = .Code
                            NameCostCenter = .Name
                            INDSleBranchOffice.EditValue = .BranchOfficeId
                            INDSleBranchOffice.Properties.NullText = .BranchOfficeCodeName
                            StatusCostCenter = .State
                        End With
                        'Llenar NullText

                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.costCenter.Code)
                        If record.Id = 0 Then
                            record = (Await Model.SaveBlockRecord(
                                    New BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = costCenter.Id})
                                    ).ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                        End If
                        'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                        Me.BarraBotones.SetDocuments(costCenter.Id, Me.Tag.ToString(), Nothing, GetType(Domain.Payroll.Entities.CostCenter).Name)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        AsyncLoader(False)
                        ActionsOnControls = True
                        'End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewCostCenter()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            CodeCostCenter = String.Empty
                            INDNameCostCenter.Focus()
                        End If
                    End If
                    INDLyCostCenter.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                Throw ex
            End Try
        End If


    End Function

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    Private Function ValidateControls() As Boolean
        ValidateControls = True
        If INDBteCodeCostCenter.Text = String.Empty Then
            INDBteCodeCostCenter.Focus()
            ValidateControls = False
            Exit Function
        End If
        If INDNameCostCenter.Text = String.Empty Then
            INDNameCostCenter.Focus()
            ValidateControls = False
            Exit Function
        End If
        Return ValidateControls
    End Function

    ''' <summary>
    ''' Metodo para asignara valores en la entidad principal
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        With costCenter
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = CodeCostCenter
            .Name = NameCostCenter
            .BranchOfficeId = INDSleBranchOffice.EditValue
        End With
    End Sub
#End Region

#Region "Methods Icrud"

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(CodeCostCenter) Then
            Using model As New MCostCenter(Me.Tag.ToString())
                AsyncLoader(True)
                Dim state As Boolean = Not Me.costCenter.State
                Dim Result = Await model.ChangeState(CodeCostCenter, state)
                AsyncLoader(False)
                If Result.StatusCode = eStatusResult.SUCCESS Then
                    costCenter = Result.ObjectEmbbeded
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                Else
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                End If
            End Using
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function

    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
        CustomizationOpen()
    End Sub
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Boton Actualizar del barra botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Boton Nuevo del barra botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Nuevo()
    End Sub

    ''' <summary>
    ''' Boton Nuevo del barra botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Boton del button edit de codigo para buscar en el frm busqueda con XPO
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ButtonEdit1_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBteCodeCostCenter.ButtonClick
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Metodo para Abrir el Frm de busqueda que se aplica con xpo
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AbrirBusqueda() Implements Base.ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.CostCenter
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Codigo"}, New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Descripcion"}}.ToList
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
        CodeCostCenter = ReturnValue
        If CodeCostCenter IsNot String.Empty Then
            Await LoadControls()
            If Not INDBteCodeCostCenter.Enabled Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCodeCostCenter.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Metodo para Buscar
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Buscar() Implements Base.ICrudBase.Buscar

    End Sub

    ''' <summary>
    ''' Metodo que deshace el ultimol proceso realizado
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Metodo para eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar
        'Dim actionResult As ActionMessageResult(Of Domain.Payroll.Entities.CostCenter)
        'If costCenter IsNot Nothing Then
        '    If costCenter.Id > 0 Then
        '        If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '            Using Model As New MCostCenter(Me.Tag)
        '                AsyncLoader(True)
        '                actionResult = Await Model.DeleteCostCenterAsync(costCenter)
        '                If actionResult.StateResult = True Then
        '                    Await Me.DeleteDocumentIndexed()
        '                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
        '                    AsyncLoader(False)
        '                Else
        '                    For Each action As MessageResult In actionResult.MessageResult
        '                        If action.CodeMessage = "c-0000" Then
        '                            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesErrorDependencia)
        '                            AsyncLoader(False)
        '                        Else
        '                            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
        '                        End If
        '                    Next
        '                End If
        '                ModoBusqueda = False
        '                Deshacer()
        '            End Using
        '        End If
        '    Else
        '        Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesSeleccioneRegistroEliminar, Eform.Comunes)
        '    End If
        'Else
        '    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesSeleccioneRegistroEliminar, Eform.Comunes)
        'End If

        '/////////////////////

        'Valido si es un formulario Fundacional y si está Activo el sistema de Fundacionales
        If Utils.IsFoundational(Me.Tag, indigo) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DeleteFoundational")
            Exit Sub
        End If

        If Me.costCenter IsNot Nothing AndAlso Me.costCenter.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    AsyncLoader(True)
                    Using Model As New MCostCenter(Me.Tag)
                        Dim result = Await Model.DeleteCostCenterAsync(Me.costCenter)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDBteCodeCostCenter.Enabled = False
                        End If
                        ShowMessage(result.StatusCode) = result.Message
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDBteCodeCostCenter.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo para guardar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        If Not ValidateControls() Then
            Exit Sub
        End If
        AssigningValues()
        Try
            AsyncLoader(True)
            Using Model As New MCostCenter(Me.Tag)
                Dim result As ActionResult(Of Domain.Payroll.Entities.CostCenter) = Await Model.SaveCostCenterAsync(Me.costCenter, Me._idCurrentSequense)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If Me.costCenter.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(_idCurrentSequense).RemoveAt(0)
                        End If
                    End If
                    Me.costCenter = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDBteCodeCostCenter.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCodeCostCenter.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(obtenerRecurso(Eresources.FrmCostCenterMetaData, Eform.InfoMetaData), Me.costCenter.Code, Me.costCenter.Name), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity = "$#" & Me.Tag & "_" & Me.costCenter.Code & "#$", .IdForm = Me.Tag, _
                .Title = String.Format(obtenerRecurso(Eresources.FrmCostCenterMetaDataTitle, Eform.InfoMetaData), Me.costCenter.Code), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmCostCenterMetaData, Eform.InfoMetaData), Me.costCenter.Code, Me.costCenter.Name)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmCostCenterMetaDataTitle, Eform.InfoMetaData), Me.costCenter.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub


    ''' <summary>
    ''' Evento cuando se abre el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBteCodeCostCenter.Text Is String.Empty Then
            INDBteCodeCostCenter.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Cambiar Unidad Operativa
    ''' </summary>
    ''' <param name="operatingUnit"></param>
    Private Sub BarraBotones_ChangeOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.GeneralLedgerSequenceDetail IsNot Nothing Then
                If Not Me._sequence.GeneralLedgerSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub
#End Region

#Region "Customize"

    ''' <summary>
    '''  Su propósito es realizar la limpieza y liberación de recursos
    '''  asociados con el formulario antes de que se elimine por completo.
    ''' </summary>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _sequence = Nothing
        record = Nothing
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        PathFunctionalDefinitions = Nothing
        Presenter = Nothing
        costCenter = Nothing
        _idCurrentSequense = Nothing
        _idOperativeUnit = Nothing
        Model = Nothing
    End Sub

    ''' <summary>
    ''' Metodo para abrir el formulario de customizar el frontal
    ''' </summary>
    Private Sub CustomizationOpen()
        INDLyCostCenter.ShowCustomizationForm()
    End Sub

    ''' <summary>
    ''' Evento DoWork que se utiliza para verificar si existe una definicion del xml del frontal
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs" /> instance containing the event data.</param>
    Private Sub CargarDefinicionesAsincronas_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles LoadhronousDefinitions.DoWork
        If CustomizacionFrontales.VerificaExisteDefinicionFrontal(PathFunctionalDefinitions) = True Then
            ExistDefinitionFront = True
        End If
    End Sub

    ''' <summary>
    ''' Evento RunWorkerCompleted que se utiliza para preguntar si encontro una definicion del frontal para posteriormente cargarla
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs" /> instance containing the event data.</param>
    Private Sub CargarDefinicionesAsincronas_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles LoadhronousDefinitions.RunWorkerCompleted
        If ExistDefinitionFront = True Then
            INDLyCostCenter.RestoreLayoutFromXml(PathFunctionalDefinitions)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se utiliza para abrir el frontal de customizacion de funcionales verifica si tiene permisos para posteriormente permitir customizar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub INDLyCostCenter_ShowCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDLyCostCenter.ShowCustomization
        Try
            'Ejecuatamos la consulta
            Using model As New MCostCenter(Me.Tag)
                Dim dsFields As DataSet = model.GetNullFields()
                If dsFields IsNot Nothing Then
                    dtFieldsCustomizables = dsFields.Tables(0)
                    For j As Integer = 0 To INDLyCostCenter.Items.Count - 1
                        INDLyCostCenter.Items.Item(j).AllowHide = False
                        For i As Integer = 0 To dtFieldsCustomizables.Rows.Count - 1
                            If Object.Equals(INDLyCostCenter.Items.Item(j).Tag, Nothing) = False Then
                                If dtFieldsCustomizables.Rows(i).Item("NAME").ToString.Trim = INDLyCostCenter.Items.Item(j).Tag.ToString.Trim Then
                                    INDLyCostCenter.Items.Item(j).AllowHide = True
                                End If
                            End If
                        Next
                    Next
                End If
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorGuardarDefinicion)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando se cierra el formulario de customizacion y que guarda la definicion del layout en la ruta especificada de cada usuario
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub INDLyCentroAtencion_HideCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDLyCostCenter.HideCustomization
        'Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
        If INDLyCostCenter.IsModified = True Then
            Try
                If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
                    INDLyCostCenter.SaveLayoutToXml(PathFunctionalDefinitions)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesErrorGuardarDefinicion)
                End If
            Catch ex As Exception
                IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorGuardarDefinicion)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Metodo para restablecer las definiciones del formulario gridLookUpEdit y Regillas
    ''' </summary>
    Private Sub ResetLayout()
        If My.Computer.FileSystem.DirectoryExists(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
            My.Computer.FileSystem.DeleteDirectory(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name), Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
            INDLyCostCenter.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub



#End Region

End Class