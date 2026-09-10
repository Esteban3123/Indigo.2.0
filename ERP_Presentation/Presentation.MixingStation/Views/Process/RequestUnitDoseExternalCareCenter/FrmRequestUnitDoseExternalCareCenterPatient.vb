'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 15/02/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraLayout
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Controls
Imports Presentation.MixingStation.MVP

#End Region

Public Class FrmRequestUnitDoseExternalCareCenterPatient

#Region "Event"

    ''' <summary>
    ''' Evento para agregar un detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddRequestUnitDoseExternalCareCenterPatientArgs(sender As Object, e As AddRequestUnitDoseExternalCareCenterPatient)

#End Region

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private Presenter As PRequestUnitDoseExternalCareCenter

    ''' <summary>
    ''' Listado de Pacientes 
    ''' </summary>
    Private NewExternalPatient As PatientExternalCareCenter

    ''' <summary>
    ''' Detalle
    ''' </summary>
    Public RequestUnitDoseExternalCareCenterPatient As RequestUnitDoseExternalCareCenterPatient

    ''' <summary>
    ''' Se utiliza para validar
    ''' </summary>
    Public ListRequestUnitDoseExternalCareCenterPatientCompare As List(Of RequestUnitDoseExternalCareCenterPatient)

    ''' <summary>
    ''' Preparación del paciente externo que se va a editar
    ''' </summary>
    Private ExternalPatientPreparationEdit As ExternalPatientPreparation

    ''' <summary>
    ''' Lista de las preparaciones de los pacientes externos
    ''' </summary>
    Private ListExternalPatientPreparation As List(Of ExternalPatientPreparation)

    ''' <summary>
    ''' Lista de las preparaciones de los pacientes externos a eliminar
    ''' </summary>
    Private ListDeleteExternalPatientPreparation As List(Of ExternalPatientPreparation)

    ''' <summary>
    ''' Control de validación
    ''' </summary>
    Private controlValidate As System.Windows.Forms.Control

    ''' <summary>
    ''' Permite saber si se esta editando el registro actual
    ''' </summary>
    Public EditMode As Boolean

    ''' <summary>
    ''' Permite saber si se esta editando el paciente
    ''' </summary>
    Public EditModePatient As Boolean

    ''' <summary>
    ''' Permite saber el estado de la solicitud
    ''' </summary>
    Public AllowAdd As Boolean

    Private _unitDoseTypeEntity As UnitDoseType

#End Region

#Region "Properties"

    ''' <summary>
    ''' Slide de mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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
    ''' Número de identificación del paciente
    ''' </summary>
    Public Property PatientIdentification As String
        Get
            If TextEditExMoreInfo1.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return TextEditExMoreInfo1.Text
            End If
        End Get
        Set(value As String)
            TextEditExMoreInfo1.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de dosis unitaria
    ''' </summary>
    Public Property UnitDoseTypeId As Integer?
        Get
            Return CType(INDSleUnitDoseType.EditValue, Integer?)
        End Get
        Set(value As Integer?)
            INDSleUnitDoseType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de nutricion (Esto solo aplica si el tipo de dosis unitaria es NPT)
    ''' </summary>
    Public Property NutritionTypeId As Integer?
        Get
            Return CType(INDSleNutritionType.EditValue, Integer?)
        End Get
        Set(value As Integer?)
            INDSleNutritionType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el texto del tipo de dosis unitaria
    ''' </summary>
    Public Property UnitDoseTypetCodeDescription As String
        Get
            If INDSleUnitDoseType.Properties.DataSource Is Nothing Then
                Return INDSleUnitDoseType.Properties.NullText
            End If
            Return INDSleUnitDoseType.Text
        End Get
        Set(value As String)
            INDSleUnitDoseType.Properties.NullText = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el texto del tipo de dosis unitaria
    ''' </summary>
    Public Property NutritionTypetCodeDescription As String
        Get
            If INDSleNutritionType.Properties.DataSource Is Nothing Then
                Return INDSleNutritionType.Properties.NullText
            End If
            Return INDSleNutritionType.Text
        End Get
        Set(value As String)
            INDSleNutritionType.Properties.NullText = value
        End Set
    End Property

#End Region

#Region "Builder"
    Public Sub New()
        InitializeComponent()
    End Sub
#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmRequestUnitDoseExternalCareCenterPatient_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ToolBar.Visible = False
        Presenter = New PRequestUnitDoseExternalCareCenter()

        IndigoGridView1.SetListAcction(INDViewExternalPatientPreparation, {eAcciones.Edit, eAcciones.Remove}.ToList())
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDViewExternalPatientPreparation.Columns
            If col.Name = "colActions" Then
                col.Width = 50
            End If
        Next

        If EditMode Then
            LoadControls()
        End If

        If Not AllowAdd Then
            INDbtnAddDetail.Enabled = False
            INDbtnAddPreparation.Enabled = False
            TextEditExMoreInfo1.Enabled = False
            INDSleUnitDoseType.Enabled = False
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Abre el form de Tipo de dosis unitarias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleUnitDoseType_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleUnitDoseType.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(2067, Nothing, True)
            INDSleUnitDoseType.Properties.DataSource = Presenter.InitializeUnitDoseType()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    '''' <summary>
    '''' Evento que se ejecuta al abrir el select del campo Tipo de dosis unitaria
    '''' </summary>
    Private Sub INDSleUnitDoseType_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleUnitDoseType.QueryPopUp
        If INDSleUnitDoseType.Properties.DataSource Is Nothing Then
            INDSleUnitDoseType.Properties.DataSource = Presenter.InitializeUnitDoseType()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de tipo de nutricion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleNutritionType_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleNutritionType.QueryPopUp
        If INDSleNutritionType.Properties.DataSource Is Nothing Then
            INDSleNutritionType.Properties.DataSource = Presenter.InitializeNPT()
        End If
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Para cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmRequestUnitDoseExternalCareCenterPatient_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If PopupContainerControl1.Visible Then
                PopupContainerControl1.Visible = False
                Exit Sub
            End If

            Me.Close()
        End If
    End Sub

    Private Sub TextEditExMoreInfo1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TextEditExMoreInfo1.OnKeyPressed
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If String.IsNullOrWhiteSpace(TextEditExMoreInfo1.EditValue) Then
                Mensaje(EeventViewerImages.Advertencia) = "Ingrese el número de documento del paciente"
                Exit Sub
            End If
            If TextEditExMoreInfo1.EditValue Is Nothing OrElse String.IsNullOrWhiteSpace(TextEditExMoreInfo1.EditValue) Then
                Mensaje(EeventViewerImages.Advertencia) = "Ingrese el número de documento del paciente"
                Exit Sub
            End If

            If NewExternalPatient Is Nothing Then
                EditModePatient = False
            Else
                EditModePatient = True
            End If

            Using formulario As New FrmPatientExternalCareCenter(TextEditExMoreInfo1.EditValue, NewExternalPatient, EditModePatient)
                Me.Cursor = ChangeCursorIndigo()
                AddHandler formulario.AddPatientExternalCareCenterArgs, AddressOf ReturnAddEventArgsPatients
                formulario.ToolBar.Visible = False
                formulario.Size = New System.Drawing.Size(1100, 600)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent = New Base.FrmTransparent(formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Metodo que agrega el detalle a la entidad principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnAddEventArgsPatients(sender As Object, e As AddPatientExternalCareCenter)
        If e IsNot Nothing Then
            If NewExternalPatient Is Nothing Then
                NewExternalPatient = New PatientExternalCareCenter
            End If

            NewExternalPatient = e.PatientExternalCareCenterDetail
            Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente"
            TextEditExMoreInfo1.ShowButtonMoreInfo = True

            If TextEditExMoreInfo1.EditValue <> e.PatientExternalCareCenterDetail.IdentificationNumber Then
                TextEditExMoreInfo1.EditValue = e.PatientExternalCareCenterDetail.IdentificationNumber
            End If

            CtrMoreInfoExternalPatient1.LoadData(NewExternalPatient)
        Else
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Agrega el detalle al form principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAddDetail_Click(sender As Object, e As EventArgs) Handles INDbtnAddDetail.Click
        Dim res = ValidateField(INDlyRoot)
        If Not res.ResultStatus Then
            Me.Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("InvalidFields"), res.ToString())
            Exit Sub
        End If

        'Se valida que el rias que se va a agregar no exista en el formulario principal
        If ListRequestUnitDoseExternalCareCenterPatientCompare IsNot Nothing AndAlso ListRequestUnitDoseExternalCareCenterPatientCompare.Count > 0 Then
            If (From x In ListRequestUnitDoseExternalCareCenterPatientCompare Where x.PatientExternalCareCenter.IdentificationNumber = TextEditExMoreInfo1.EditValue Select x).Count > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "El medicamento seleccionado ya existe en la rejilla principal"
                TextEditExMoreInfo1.Focus()
                Exit Sub
            End If
        End If

        If EditMode = False Then
            RequestUnitDoseExternalCareCenterPatient = New RequestUnitDoseExternalCareCenterPatient
        End If

        With RequestUnitDoseExternalCareCenterPatient
            '===================================================================
            'Lista para guardar el paciente externo creado para la solicitud
            .PatientExternalCareCenter = NewExternalPatient
            .PatientExternalCareCenterNitName = $"{NewExternalPatient.IdentificationNumber} - {NewExternalPatient.Name} {NewExternalPatient.LastName}"
            .UnitDoseTypeId = UnitDoseTypeId
            .NptId = NutritionTypeId
            .UnitDoseTypeCodeName = UnitDoseTypetCodeDescription
            '===================================================================
            .RequestUnitDoseExternalCareCenterPatientDetails.Clear()

            If ListExternalPatientPreparation IsNot Nothing AndAlso ListExternalPatientPreparation.Count > 0 Then
                ListExternalPatientPreparation.ForEach(Sub(x) .ExternalPatientPreparation.Add(x))
            End If

            If ListDeleteExternalPatientPreparation IsNot Nothing AndAlso ListDeleteExternalPatientPreparation.Count > 0 Then
                ListDeleteExternalPatientPreparation.ForEach(Sub(x) .ExternalPatientPreparation.Add(x.MarkAsDeleted()))
            End If
        End With

        Dim args As New AddRequestUnitDoseExternalCareCenterPatient
        args.RequestUnitDoseExternalCareCenterPatient = RequestUnitDoseExternalCareCenterPatient
        args.EditMode = EditMode
        RaiseEvent AddRequestUnitDoseExternalCareCenterPatientArgs(Nothing, args)
        Me.Close()
    End Sub

#End Region

#Region "ContextMenu"

    ''' <summary>
    ''' Rejilla reconstitución
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        ExternalPatientPreparationEdit = DirectCast(INDViewExternalPatientPreparation.GetFocusedRow(), ExternalPatientPreparation)

        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditRequest()
            Case "Remove"
                DeleteExternalPreparation()
        End Select
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Se ejecuta al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmRequestUnitDoseExternalCareCenterPatient_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If Not EditMode Then
            ' TODO: validar que deberia hacer en edicion
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que ocurre cuando se cambia el valor del tipo de dosis unitaria
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSleUnitDoseType_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleUnitDoseType.EditValueChanged
        If IsNumeric(UnitDoseTypeId) And UnitDoseTypeId > 0 Then
            Using Model As New MPackage(CStr(Me.Tag))
                Dim resultOperation = Await Model.GetUnitDoseTypeById(UnitDoseTypeId)
                _unitDoseTypeEntity = resultOperation.ObjectEmbbeded
            End Using
        End If

        If _unitDoseTypeEntity IsNot Nothing Then
            Select Case _unitDoseTypeEntity.MSClass
                Case EUnitDoseTypeClass.Antibiotics, EUnitDoseTypeClass.OtherSterile, EUnitDoseTypeClass.Cytostatic
                    NutritionTypeId = Nothing
                    INDbtnAddPreparation.Enabled = True
                    INDLciNutritionType.HideLayout()
                Case EUnitDoseTypeClass.ParenteralNutrition
                    INDbtnAddPreparation.Enabled = False
                    INDLciNutritionType.ShowLayout()
                Case Else
                    NutritionTypeId = Nothing
                    INDbtnAddPreparation.Enabled = False
                    INDLciNutritionType.HideLayout()
            End Select
        End If

    End Sub
#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga los controles cuando se esta editando
    ''' </summary>
    Private Sub LoadControls()
        With RequestUnitDoseExternalCareCenterPatient
            NewExternalPatient = .PatientExternalCareCenter

            TextEditExMoreInfo1.EditValue = .PatientExternalCareCenter.IdentificationNumber
            UnitDoseTypeId = .UnitDoseTypeId
            UnitDoseTypetCodeDescription = .UnitDoseTypeCodeName
            NutritionTypeId = .NptId
            NutritionTypetCodeDescription = .NutritionTypeCodeName

            If NewExternalPatient IsNot Nothing Then
                NewExternalPatient.GenderCodeName = Presenter.GetGenderTypeById(NewExternalPatient.GenderTypeId).CodeName
                CtrMoreInfoExternalPatient1.LoadData(NewExternalPatient)
            End If

            ListExternalPatientPreparation = (From x In .ExternalPatientPreparation Where x.ChangeTracker.State <> ObjectState.Deleted Select x).ToList()
            RefreshDataSourcePreparation(ListExternalPatientPreparation)
        End With
    End Sub

    ''' <summary>
    ''' Edita una regla
    ''' </summary>
    Private Sub EditRequest()
        OpenDetailRequestAntibiotic(True)
    End Sub

    ''' <summary>
    ''' Elimina los registros seleccionados
    ''' </summary>
    Private Async Sub DeleteExternalPreparation()
        Dim listSelected = SelectedItems

        If listSelected Is Nothing OrElse listSelected.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione los items a eliminar"
            Exit Sub
        End If

        If Not AllowAdd Then
            Mensaje(EeventViewerImages.Advertencia) = "El medicamento no se puede eliminar, la solicitud ya se encuentra confirmada"
            Exit Sub
        End If

        Try
            If MessageIndigo.Show("¿Desea eliminar los medicamentos seleccionados?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

            AsyncLoader(True)

            For Each item In listSelected
                If ExternalPatientPreparationEdit.Id > 0 Then
                    If ListDeleteExternalPatientPreparation Is Nothing Then
                        ListDeleteExternalPatientPreparation = New List(Of ExternalPatientPreparation)
                    End If
                    ListDeleteExternalPatientPreparation.Add(item)
                End If
                ListExternalPatientPreparation.Remove(item)
            Next

            RefreshDataSourcePreparation(ListExternalPatientPreparation)
            Mensaje(EeventViewerImages.Informacion) = "Registro(s) eliminado(s) de la rejilla correctamente"

        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Limpia y refresta el datasource de la rejilla de los medicamentos personalizados por paciente externo
    ''' </summary>
    Private Sub RefreshDataSourcePreparation(ListExternalPatientPreparation As List(Of ExternalPatientPreparation))
        INDGcExternalPatientPreparations.DataSource = Nothing
        INDGcExternalPatientPreparations.DataSource = ListExternalPatientPreparation
    End Sub

    ''' <summary>
    ''' Obtiene un valor que indica si el tipo pasado tiene como base
    ''' un control BaseEdit
    ''' </summary>
    ''' <param name="type">Tipo pasado a consultar</param>
    Private Function GetIsBaseEditBaseType(ByVal type As Type) As Boolean
        Dim result As Boolean = False
        IsBaseEditBaseType(type, result)
        Return result
    End Function

    Private Sub IsBaseEditBaseType(ByVal type As Type, ByRef result As Boolean)
        If type.BaseType.Equals(GetType(DevExpress.XtraEditors.BaseEdit)) Then
            result = True
        Else
            If Not type.BaseType.Equals(GetType(Object)) Then
                IsBaseEditBaseType(type.BaseType, result)
            Else
                result = False
            End If
        End If
    End Sub

    ''' <summary>
    ''' Valida los controles de un layout
    ''' </summary>
    ''' <param name="layoutControl"></param>
    ''' <returns></returns>
    Public Function ValidateField(layoutControl As LayoutControl) As ValidateResult
        Dim result As New ValidateResult()
        Dim refCtr As System.Windows.Forms.Control = Nothing
        'Recorremos los LayoutControlItem
        For Each item As BaseLayoutItem In (From i As BaseLayoutItem In layoutControl.Items Where i.GetType().Equals(GetType(LayoutControlItem)) Select i).ToList()
            Dim it As LayoutControlItem = DirectCast(item, LayoutControlItem)
            Dim nn As String = it.Name
            If TypeOf it.Control Is IValidable OrElse Me.GetIsBaseEditBaseType(it.Control.GetType()) Then
                Dim ctr As Object = it.Control
                If Not it.ShowInCustomizationForm OrElse Not it.AllowHide Then
                    If ctr.EditValue IsNot Nothing Then
                        If ctr.EditValue.GetType().Equals(GetType(String)) AndAlso ctr.EditValue.ToString().Trim().Equals(String.Empty) Then
                            If result.EmptyFieldNames.Count = 0 Then
                                refCtr = ctr
                            End If
                            result.EmptyFieldNames.Add(it.Text)
                        End If
                    Else
                        If result.EmptyFieldNames.Count = 0 Then
                            refCtr = ctr
                        End If
                        result.EmptyFieldNames.Add(it.Text)
                    End If
                End If
            End If
        Next
        If refCtr IsNot Nothing Then
            controlValidate = refCtr
            refCtr.Focus()
        End If
        result.ResultStatus = (result.EmptyFieldNames.Count = 0)
        Return result
    End Function

    Private Sub TextEditExMoreInfo1_EditValueChanged(sender As Object, e As EventArgs) Handles TextEditExMoreInfo1.EditValueChanged
        If TextEditExMoreInfo1.EditValue IsNot Nothing Then
            If NewExternalPatient IsNot Nothing Then
                If TextEditExMoreInfo1.EditValue <> NewExternalPatient.IdentificationNumber Then
                    NewExternalPatient = Nothing
                    EditModePatient = False
                    Exit Sub
                End If
            End If

            EditModePatient = True
            TextEditExMoreInfo1.ShowButtonMoreInfo = True
        End If
    End Sub

    ''' <summary>
    '''Evento que se dispara al ejecutar el clic + para añadir el detalle de la preparación
    ''' </summary>
    Private Sub INDbtnAddPreparation_Click(sender As Object, e As EventArgs) Handles INDbtnAddPreparation.Click
        ExternalPatientPreparationEdit = Nothing
        If {EUnitDoseTypeClass.Antibiotics, EUnitDoseTypeClass.OtherSterile, EUnitDoseTypeClass.Cytostatic}.Contains(_unitDoseTypeEntity.MSClass) Then
            OpenDetailRequestAntibiotic(False)
        End If
    End Sub

    ''' <summary>
    '''Método que apertura el PopUp para adicionar los detalles de las preparaciones de tipo de dosis antibióticos
    ''' </summary>
    Private Sub OpenDetailRequestAntibiotic(EditMode As Boolean)
        Using formulario As New FrmDetailRequestAntibiotic
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddExternalPatientPreparation, AddressOf ReturnAddExternalPatientPreparation
            formulario.Size = New System.Drawing.Size(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.7, System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.5)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.NewExternalPatientPreparation = ExternalPatientPreparationEdit
            formulario.UnitDoseType = _unitDoseTypeEntity
            formulario.AllowAdd = AllowAdd
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' metodo para obtener lo que se retorna del formulario modal de detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnAddExternalPatientPreparation(sender As Object, e As AddExternalPatientPreparationEventArgs)
        Try
            AsyncLoader(True)

            If ListExternalPatientPreparation Is Nothing Then
                ListExternalPatientPreparation = New List(Of ExternalPatientPreparation)
            End If

            If ExternalPatientPreparationEdit Is Nothing Then
                ListExternalPatientPreparation.Add(e.ExternalPatientPreparation)
            Else
                Dim indexEditRecord = ListExternalPatientPreparation.IndexOf(ExternalPatientPreparationEdit)
                ListExternalPatientPreparation.Remove(ExternalPatientPreparationEdit)
                ListExternalPatientPreparation.Insert(indexEditRecord, e.ExternalPatientPreparation)
            End If

            RefreshDataSourcePreparation(ListExternalPatientPreparation)

        Catch ex As Exception
            Throw ex
        Finally
            AsyncLoader(False)
        End Try
    End Sub

#End Region

#Region "Select"

    ''' <summary>
    ''' items seleccionados
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property SelectedItems As List(Of ExternalPatientPreparation)
        Get
            Return INDViewExternalPatientPreparation.GetSelectedRows() _
                   .Select(Function(m) CType(INDViewExternalPatientPreparation.GetRow(m), ExternalPatientPreparation)) _
                   .ToList()
        End Get
    End Property

    ''' <summary>
    ''' Menú contextual visualización de acciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDViewExternalPatientPreparation_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDViewExternalPatientPreparation.PopupMenuShowing
        If e.HitInfo.InRow Then
            If Not INDViewExternalPatientPreparation.GetSelectedRows().Contains(e.HitInfo.RowHandle) Then
                IndigoGridView1.BarManagerActions.Items.ToList().ForEach(Sub(i) i.Visibility = DevExpress.XtraBars.BarItemVisibility.Never)
                Return
            End If

            Dim items = SelectedItems
            Dim editAction = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.Edit)))
            Dim RemoveAction = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.Remove)))

            editAction.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            RemoveAction.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

            If items.Count = 1 Then
                editAction.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                RemoveAction.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Else
                RemoveAction.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            End If
        End If
    End Sub

    ''' <summary>
    ''' Acciones visualizadas en el bot{on de la columna acciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_QueryPopUpActionButtons(sender As Object, e As QueryPopUpActionButtonsEventArgs) Handles IndigoGridView1.QueryPopUpActionButtons
        Dim items = SelectedItems
        Dim actionsQuantiy = 0
        Dim popUp = CType(sender, RepositoryItemPopupContainerEdit)
        'Se ocultan los botones inicialmente
        e.Buttons.ToList().ForEach(Sub(i) i.Visible = False)

        If Not INDViewExternalPatientPreparation.GetSelectedRows().Contains(INDViewExternalPatientPreparation.FocusedRowHandle) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un item para ejecutar alguna acción"
            Return
        End If

        Dim editAction = e.Buttons.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.Edit)))
        Dim RemoveAction = e.Buttons.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.Remove)))

        If RemoveAction IsNot Nothing Then
            RemoveAction.Visible = True
            actionsQuantiy += 1
        End If

        If editAction IsNot Nothing AndAlso items.Count > 1 Then
            editAction.Visible = False
        Else
            editAction.Visible = True
            actionsQuantiy += 1
        End If

        popUp.PopupControl.Size = New Size(200, 36 * actionsQuantiy)
    End Sub

#End Region

End Class

Public Class AddRequestUnitDoseExternalCareCenterPatient
    Inherits EventArgs

    Property RequestUnitDoseExternalCareCenterPatient As RequestUnitDoseExternalCareCenterPatient

    Property EditMode As Boolean

End Class