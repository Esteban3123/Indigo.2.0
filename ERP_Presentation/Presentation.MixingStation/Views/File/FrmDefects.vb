'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Cristian Camilo Bahamon Castaño
' Created          : 31-08-2022
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports System.ComponentModel
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.MixingStation.MVP

#End Region
Public Class FrmDefects
    Implements IDefects, ICustomizableForm

#Region "Fields"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "MixingStation"

    ''' <summary>
    ''' Representa la entidad de lineas de produccion
    ''' </summary>
    Dim _defectsEntity As DefectClassificationItem

    ''' <summary>
    ''' Representa la entidad de Tipo de Dosis Unitaria
    ''' </summary>
    Dim _defectsUnitDoseTypeEntity As DefectsUnitDoseType

    ''' <summary>
    ''' Referencia la presentador
    ''' </summary>
    Private _presenter As PDefects

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
    ''' Listado de dosis unitaria
    ''' </summary>
    ''' <remarks></remarks>
    Private ListDefectsUnitDoseType As List(Of DefectsUnitDoseType)

    ''' <summary>
    ''' Listado de eliminados dosis unitaria
    ''' </summary>
    ''' <remarks></remarks>
    Private ListDeleteDefectsUnitDoseType As List(Of DefectsUnitDoseType)

    ''' <summary>
    ''' Variable para saber si el frontal abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean

    Dim Id_Defects As Integer
#End Region

#Region "Propierties"

    ''' <summary>
    ''' Obtiene o establece el codigo del turno
    ''' </summary>
    Public Property Code As String Implements IDefects.Code
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

    Private Property Description As String Implements IDefects.Description
        Get
            Return INDtxtNombre.Text
        End Get
        Set(value As String)
            INDtxtNombre.Text = value
        End Set
    End Property

    Public Property DefectClassificationGroupId As Integer Implements IDefects.DefectClassificationGroupId
        Get
            Return CType(INDsleddlCategory.EditValue, Integer)
        End Get
        Set(value As Integer)
            INDsleddlCategory.EditValue = value
        End Set
    End Property

    Public Property Weight As Integer Implements IDefects.Weight
        Get
            Return CType(INDSpinWeight.EditValue, Integer)
        End Get
        Set(value As Integer)
            INDSpinWeight.EditValue = value
        End Set
    End Property

    Public Property Type As Byte Implements IDefects.Type
        Get
            Return INDsleddlType.EditValue
        End Get
        Set(value As Byte)
            INDsleddlType.EditValue = value
        End Set
    End Property

    Public Property State As Boolean Implements IDefects.State
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

    Public Property UnitDoseTypeId As Integer Implements IDefects.UnitDoseTypeId
        Get
            Return CType(INDsleddlDosis.EditValue, Integer)
        End Get
        Set(value As Integer)
            INDsleddlDosis.EditValue = value
        End Set
    End Property

    Public WriteOnly Property ActionsOnControls As Boolean Implements IDefects.ActionsOnControls
        Set(value As Boolean)
            INDlycBase.BeginUpdate()

            INDbtnCode.Enabled = Not value
            INDtxtNombre.Enabled = value
            INDsleddlCategory.Enabled = value
            INDSpinWeight.Enabled = value
            INDsleddlType.Enabled = value
            INDrgProductionChemical.Enabled = value
            INDrgQualityChemical.Enabled = value
            INDsleddlDosis.Enabled = value
            INDlycBase.EndUpdate()

            If value Then
                INDtxtNombre.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    Public Property Sequence As MixingStationSequence Implements IDefects.Sequence
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

    Public ReadOnly Property MyTag As Object Implements IDefects.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IDefects.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public Property UnitDoseTypeDatasource As XPInstantFeedbackSource Implements IDefects.UnitDoseTypeDatasource
        Get
            Return CType(INDsleddlDosis.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleddlDosis.Properties.DataSource = value
        End Set
    End Property

    Public Property DefectClassificationGroup As XPInstantFeedbackSource Implements IDefects.DefectClassificationGroup
        Get
            Return CType(INDsleddlCategory.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleddlCategory.Properties.DataSource = value
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


#End Region

#Region "DataSource"

    Private _FillingLabelType As List(Of Tuple(Of Byte, String))
    ''' <summary>
    ''' Propiedad para capturar el tipo de etiqueta
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property FillingLabelType As List(Of Tuple(Of Byte, String))
        Get
            If _FillingLabelType Is Nothing Then
                _FillingLabelType = New List(Of Tuple(Of Byte, String))
                _FillingLabelType.Add(New Tuple(Of Byte, String)(1, "Critico"))
                _FillingLabelType.Add(New Tuple(Of Byte, String)(2, "Menor"))
            End If
            Return _FillingLabelType
        End Get
    End Property

#End Region

#Region "ICrudBase"
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If Not ValidateControls() Then
            Exit Sub
        End If

        If Not ValidateControl() Then
            Exit Sub
        End If

        AssigningValues()
        Try
            Using Model As New MDefects(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of DefectClassificationItem) = Await Model.SaveDefectClassificationItem(Me._defectsEntity, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If _defectsEntity.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me._defectsEntity = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Id_Defects = _defectsEntity.Id
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

    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewDefects()
        End If
    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        If Me._defectsEntity IsNot Nothing AndAlso Me._defectsEntity.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MDefects(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteDefectClassificationItem(Me._defectsEntity)
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

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
    End Sub
#End Region

#Region "Bar Button Events"

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
    ''' Evento barra de botones Activo - Inactivo
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
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
    Private Sub FrmDefects_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        _presenter = New PDefects(Me)
        _presenter.GetSequence()
        LoadStatus()
        Deshacer()
        Me.LayoutControls.SetIsCustomizable(Me.INDlycBase, True)
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView3.SetListAcction(INDviewUnitDosesType, ListActions)
        INDsleddlType.Properties.DataSource = FillingLabelType
    End Sub
#End Region

#Region "Disposed"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ListDefectsUnitDoseType = Nothing
        _presenter = Nothing
        _defectsEntity = Nothing
        _defectsUnitDoseTypeEntity = Nothing
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

#Region "KeyDown"

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
                    Await Me.NewDefects()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "Shown"
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
#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' Carga el datasource de dosis unitarias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleddlDosis_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleddlDosis.QueryPopUp
        If INDsleddlDosis.Properties.DataSource Is Nothing Then
            _presenter.InitializeUnitDoseType()
        End If
    End Sub

    Private Sub INDsleddlCategory_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleddlCategory.QueryPopUp
        If INDsleddlCategory.Properties.DataSource Is Nothing Then
            _presenter.InitializeCategory()
        End If
    End Sub

#End Region

#Region "Click"
    ''' <summary>
    ''' Se ejecuta al dar click sobre agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAdd_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click

        If INDsleddlDosis.EditValue Is Nothing OrElse INDsleddlDosis.EditValue = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una dosis unitaria"
            Exit Sub
        End If

        If ListDefectsUnitDoseType Is Nothing Then
            ListDefectsUnitDoseType = New List(Of DefectsUnitDoseType)
        Else
            If (From x In ListDefectsUnitDoseType Where x.Id_UnitDoseType = INDsleddlDosis.EditValue Select x).Count > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "El tipo de dosis unitaria ya se encuentra agregado"
                Exit Sub
            End If
        End If

        Dim entity As New DefectsUnitDoseType
        With entity
            .Id_UnitDoseType = INDsleddlDosis.EditValue
            .UnitDoseTypeCode = INDsleddlDosis.Text.Split("-")(0)
            .UnitDoseTypeName = INDsleddlDosis.Text.Split("-")(1)
        End With

        ListDefectsUnitDoseType.Add(entity)
        INDgcUnitDoseType.DataSource = ListDefectsUnitDoseType
        INDgcUnitDoseType.RefreshDataSource()
        INDsleddlDosis.EditValue = Nothing
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Abre el form de dosis unitarias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleddlDosis_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleddlDosis.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(2067, Nothing, True)
        End If
    End Sub

    ''' <summary>
    ''' Abre el form de unidades funcionales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleddlCategory_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleddlCategory.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(89004, Nothing, True)
        End If
    End Sub

#End Region

#Region "MenuContextual"

    ''' <summary>
    ''' Rejilla dosis unitaria
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView3_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView3.Click_ButtonAction
        DeleteDosesUnitType()
    End Sub

#End Region

#End Region

#Region "Methods"
    ''' <summary>
    ''' Elimina una dosis unitaria
    ''' </summary>
    Private Sub DeleteDosesUnitType()
        Dim entity = CType(INDviewUnitDosesType.GetFocusedRow, DefectsUnitDoseType)

        If entity.Id > 0 Then
            If ListDeleteDefectsUnitDoseType Is Nothing Then
                ListDeleteDefectsUnitDoseType = New List(Of DefectsUnitDoseType)
            End If
            ListDeleteDefectsUnitDoseType.Add(entity)
        End If

        ListDefectsUnitDoseType.Remove(entity)
        INDgcUnitDoseType.DataSource = Nothing
        INDgcUnitDoseType.DataSource = ListDefectsUnitDoseType
        INDgcUnitDoseType.RefreshDataSource()
    End Sub

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
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MDefects(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetDefectClassificationItemByCode(INDbtnCode.Text.Trim)
                    INDlycBase.BeginUpdate()
                    _defectsEntity = resultOperation.ObjectEmbbeded
                    If _defectsEntity IsNot Nothing AndAlso _defectsEntity.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequenceMixingStation(CStr(Me.Tag))
                            _record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_defectsEntity.Id))
                            With _defectsEntity
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                'Llenar Entidad
                                Code = .Code
                                Description = .Description
                                DefectClassificationGroupId = .DefectClassificationGroupId
                                INDsleddlCategory.Properties.NullText = .CategoryDefectsName
                                Weight = .Weight
                                State = .State
                                Id_Defects = .Id
                                Type = CByte(IIf(.Critical, 1, 2))
                                INDrgProductionChemical.EditValue = IIf(.ProductionChemical IsNot Nothing, .ProductionChemical, False)
                                INDrgQualityChemical.EditValue = True
                                ListDefectsUnitDoseType = .DefectsUnitDoseType.ToList()
                                INDgcUnitDoseType.DataSource = ListDefectsUnitDoseType
                            End With

                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._defectsEntity.Code)
                            If _record.Id = 0 Then
                                _record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordMixingStation With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _defectsEntity.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _record.CodUser, _record.NameUser, _record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _record.CodUser)
                            End If
                            Me.BarraBotones.SetDocuments(_defectsEntity.Id, Me.Tag.ToString(), Nothing, GetType(AccountPayableConceptNotes).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True

                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewDefects()
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
    Private Async Function NewDefects() As Task
        _defectsEntity = New DefectClassificationItem() With {.State = True}
        _defectsUnitDoseTypeEntity = New DefectsUnitDoseType()
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
                              New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3)},
                              New ColumnInfo() With {.Caption = "Defecto", .FieldName = "Description", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListDefects
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Metodo que cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(Me._defectsEntity.Code) Then
            Try
                Using model As New MDefects(Me.Tag)
                    AsyncLoader(True)
                    Dim _state As Boolean = Not Me._defectsEntity.State
                    Dim result As ActionResult(Of DefectClassificationItem) = Await model.ChangeState(Me._defectsEntity.Code, _state)
                    AsyncLoader(False)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me._defectsEntity = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
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

        With _defectsEntity
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Description = Description
            .DefectClassificationGroupId = DefectClassificationGroupId
            .Weight = Weight
            .Critical = Type = 1
            .Less = Type = 2
            .ProductionChemical = INDrgProductionChemical.EditValue
            .QualityChemical = INDrgQualityChemical.EditValue

            If ListDefectsUnitDoseType IsNot Nothing AndAlso ListDefectsUnitDoseType.Count > 0 Then
                ListDefectsUnitDoseType.ForEach(Sub(x) .DefectsUnitDoseType.Add(x))
            End If

            If ListDeleteDefectsUnitDoseType IsNot Nothing AndAlso ListDeleteDefectsUnitDoseType.Count > 0 Then
                ListDeleteDefectsUnitDoseType.ForEach(Sub(x) .DefectsUnitDoseType.Add(x.MarkAsDeleted()))
            End If

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

        'Limpiar controles
        State = True
        Code = String.Empty
        Description = String.Empty
        UnitDoseTypeId = Nothing
        DefectClassificationGroupId = Nothing
        INDrgProductionChemical.EditValue = False
        INDrgQualityChemical.EditValue = True
        Weight = 1

        INDgcUnitDoseType.DataSource = Nothing
        ListDefectsUnitDoseType = Nothing
        ListDeleteDefectsUnitDoseType = Nothing

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
        INDlycBase.EndUpdate()
        DeleteBlockedrecord()
    End Sub

    Private Function ValidateControl()

        If ListDefectsUnitDoseType Is Nothing OrElse ListDefectsUnitDoseType.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe asignar un tipo de dosis unitaria"
            Return False
            Exit Function
        End If

        Return True
    End Function

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

#End Region
End Class