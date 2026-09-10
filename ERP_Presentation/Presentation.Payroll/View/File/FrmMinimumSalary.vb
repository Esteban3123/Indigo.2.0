'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Juan Pablo Daza Medina
' Created          : 02-11-2022
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Payroll.MVP
Imports Presentation.Base
Imports Presentation.Base.Eresources
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Controls
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Base
Imports Domain.Payroll.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Entities

#End Region
Public Class FrmMinimumSalary

    Implements IMinimumSalary

#Region "Fields"
    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Payroll"

    ''' <summary>
    ''' Entidad que encapsula la infórmacion
    ''' </summary>
    Private _minimumSalary As MinimumSalary

    ''' <summary>
    ''' Referencia la presentador
    ''' </summary>
    Private _presenter As PMinimumSalary

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As PayrollSequence

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>|
    ''' Id de la configuración de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As BlockRecordPayroll


#End Region

#Region "Propierties"
    Public Property MinimumSalaryByYear As String Implements IMinimumSalary.MinimumSalaryByYear
        Get
            If (INDbtnYear.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDbtnYear.Text
            End If
        End Get
        Set(value As String)
            INDbtnYear.Text = value
        End Set
    End Property


    Public Property LegalMinimumSalary As Integer Implements IMinimumSalary.LegalMinimumSalary
        Get
            Return INDtxtMinimumSalary.EditValue
        End Get
        Set(value As Integer)
            INDtxtMinimumSalary.Text = value
        End Set
    End Property

    Public Property TransportHelpValue As Integer Implements IMinimumSalary.TransportHelpValue
        Get
            Return INDtxtTransporHelpValue.EditValue
        End Get
        Set(value As Integer)
            INDtxtTransporHelpValue.Text = value
        End Set
    End Property

    Public WriteOnly Property ActionsOnControls As Boolean Implements IMinimumSalary.ActionsOnControls
        Set(value As Boolean)
            Root.BeginUpdate()

            INDbtnYear.Enabled = Not value
            INDtxtMinimumSalary.Enabled = value
            INDtxtTransporHelpValue.Enabled = value
            Root.EndUpdate()
            If value Then
                INDtxtMinimumSalary.Focus()
            Else
                INDtxtTransporHelpValue.Focus()
            End If
        End Set
    End Property

    Public Property Sequence As PayrollSequence Implements IMinimumSalary.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As PayrollSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.PayrollSequenceDetail In Me._sequence.PayrollSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IMinimumSalary.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public ReadOnly Property MyTag As Object Implements IMinimumSalary.MyTag
        Get
            Return Me.Tag
        End Get
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

#End Region

#Region "ICrudBase"
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If Not ValidateControls() Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If
        AssigningValues()
        Me.AsyncLoader(True)
        Try
            Using model As New MMinimumSalary(Me.Tag.ToString())
                Dim result = Await model.SaveMinimumSalary(_minimumSalary, _idCurrentSequence)
                If result.StateResult Then
                    If _minimumSalary.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._sequence.PayrollSequenceDetail(0).Id).RemoveAt(0)
                        End If
                        If Me._sequence.Sequential Then
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), result.ObjectEmbbeded.Year)
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                        End If
                    ElseIf _minimumSalary.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                    Me._minimumSalary = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Me.AsyncLoader(False)
                    Me.Deshacer()
                Else
                    Me.AsyncLoader(False)
                    If result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    ElseIf Not String.IsNullOrEmpty(result.Message) Then
                        Mensaje(EeventViewerImages.MensajeError) = result.Message
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
        Catch ex As Exception
            Me.AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        End Try

    End Sub

    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
            Deshacer()
        Else
            Await NewMinimumSalary()
        End If
    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        If _minimumSalary IsNot Nothing AndAlso _minimumSalary.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Using model As New MMinimumSalary(Me.Tag.ToString())
                    AsyncLoader(True)
                    Dim result = Await model.DeleteMinimumSalary(_minimumSalary)
                    If result.StateResult Then
                        Me.DeleteDocumentIndexed()
                    AsyncLoader(False)
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                    Me.Deshacer()
                    Else
                    AsyncLoader(False)
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
            End Using
        End If
        End If
    End Sub
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListMinimumSalary
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Año", .FieldName = "Year", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Salario Minimo", .FieldName = "LegalMinimumSalary", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Auxilio De Transporte", .FieldName = "TransportHelpValue", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)}}.ToList
            .ValorSolicitado = "Year"
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub


    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub
#End Region

#Region "Bar Button events"


    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        SearchMode = False
        Deshacer()
    End Sub

    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnYear.ButtonClick
        OpenSearch()
    End Sub

    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub
#End Region

#Region "Events"

#End Region

#Region "Methods"
    Private Async Sub DeleteBlockedRecord()
        Using Model As New MBlockRecordAndSequensePayroll(CStr(Me.Tag))
            If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
                Await Model.DeleteBlockRecord(_record)
                record = Nothing
            End If
        End Using
    End Sub

    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(MinimumSalaryByYear) AndAlso Not String.IsNullOrWhiteSpace(MinimumSalaryByYear) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If

            Me.BarraBotones.StatusRecordVisible = True
            Using Model As New MMinimumSalary(CStr(Me.Tag))
                AsyncLoader(True)
                _minimumSalary = Await Model.GetMinimumSalaryByYear(INDbtnYear.Text.Trim)
                If _minimumSalary IsNot Nothing AndAlso _minimumSalary.Id > 0 Then
                    Using ModelRecord As New MBlockRecordAndSequensePayroll(CStr(Me.Tag))
                        _record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_minimumSalary.Id))
                        With _minimumSalary
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                            MinimumSalaryByYear = .Year
                            LegalMinimumSalary = .LegalMinimumSalary
                            TransportHelpValue = .TransportHelpValue
                        End With

                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._minimumSalary.Year)
                        If _record.Id = 0 Then
                            record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordPayroll With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .Id = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = _minimumSalary.Id})
                                ).ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _record.CodUser, _record.NameUser, _record.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _record.CodUser)
                        End If
                        Me.BarraBotones.SetDocuments(_minimumSalary.Id)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        AsyncLoader(False)
                        ActionsOnControls = True
                    End Using
                Else
                    AsyncLoader(False)
                    If Me._sequence.IsManual Then
                        Await Me.NewMinimumSalary()
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                        Code = String.Empty
                        INDbtnYear.Focus()
                    End If
                End If
            End Using
        End If
    End Function
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        MinimumSalaryByYear = ReturnValue
        If MinimumSalaryByYear <> String.Empty Then
            Await LoadControls()
            If INDbtnYear.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnYear.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewMinimumSalary() As Task
        _mimimumSalary = New MinimumSalary()

        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.PayrollSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id


            ElseIf Me._sequence.Scope.Equals("OU") Then 'Elambito es a nivel de unidad operativa
                If Me._sequence.PayrollSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.PayrollSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.MinimumSalaryByYear = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                Using model As New MBlockRecordAndSequensePayroll(CStr(Me.Tag))
                    Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                End Using
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.MinimumSalaryByYear = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                    End If
                Else
                    Me.MinimumSalaryByYear = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True

                End If

            End If
        End If

    End Function

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub
    Private Sub AssigningValues()
        With _minimumSalary
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Year = MinimumSalaryByYear
            .LegalMinimumSalary = LegalMinimumSalary
            .TransportHelpValue = TransportHelpValue
            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    Private Sub CleanControls()
        Root.BeginUpdate()
        ActionsOnControls = False
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.BarraBotones.StatusRecordVisible = False
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        MinimumSalaryByYear = String.Empty
        LegalMinimumSalary = Nothing
        TransportHelpValue = Nothing
        Root.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._minimumSalary IsNot Nothing AndAlso Me._minimumSalary.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbtnYear.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnYear.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._minimumSalary.Year, Me._minimumSalary.LegalMinimumSalary),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me._minimumSalary.Year & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._minimumSalary.Year),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._minimumSalary.Year, Me._minimumSalary.LegalMinimumSalary)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._minimumSalary.Year)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Establece a  los controles la moneda parametrizada
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    Private Sub SetCurrencyFormat(_currencyAbbreviation As String)
        If String.IsNullOrEmpty(_currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "La abreviación de la moneda está vacía."
            Exit Sub
        End If
        Dim numberFormat = _currencyAbbreviation.GetNumberFormat()
        changeNumericFormatByCurrency(numberFormat)
    End Sub

#End Region

#Region "Handlers"
#Region "Load"
    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmMinimumSalary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.LayoutControl1, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PMinimumSalary(Me)
        Presenter.GetSequence()
        SetCurrencyFormat(Presenter.LoadPayrollSettings().CurrencyId.Abbreviation)
        LoadStatus()
        Deshacer()
    End Sub
#End Region
#Region "KeyDown"

    ''' <summary>
    ''' Enter del campo código
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDbtnYear_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnYear.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then

                If Not String.IsNullOrEmpty(MinimumSalaryByYear.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(MinimumSalaryByYear) Then
                    Await Me.NewMinimumSalary()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
        End If
    End Sub

#End Region
#Region "Shown"
    ''' <summary>
    ''' Se ejecuta al pintarse el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmMinimumSalary_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDbtnYear.Focus()
    End Sub
#End Region
#Region "FormClosing"

    ''' <summary>
    ''' Se ejecuta al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmCategoryDefects_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub



#End Region


#End Region



End Class