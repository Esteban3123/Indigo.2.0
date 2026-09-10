'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 18/05/2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.FixedAsset.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports System.Windows.Forms

#End Region

Public Class FrmFixedAssetActiveOutput
    Implements IFixedAssetActiveOutput, ICustomizableForm

#Region "Builder"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ctrTmp = New CtrActiveOutputValue()
        ctrTmp.SetTotalValues(AddressOf getValue)
        ctrTmp.RefreshTotalValues()
        ctrTmp.Dock = System.Windows.Forms.DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
    End Sub

    ''' <summary>
    ''' Devuelve el listado para mostrar el Total del contrato y sus derivados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getValue() As Tuple(Of Decimal)
        Return New Tuple(Of Decimal)(SumValue)
    End Function

#End Region

#Region "Properties"

    ''' <summary>
    ''' Código
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IFixedAssetActiveOutput.Code
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
    ''' Fecha del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As Date? Implements IFixedAssetActiveOutput.DocumentDate
        Get
            Return INDdteDocumentDate.EditValue
        End Get
        Set(value As Date?)
            INDdteDocumentDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Retorna el layout
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IFixedAssetActiveOutput.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Retorna el tag
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IFixedAssetActiveOutput.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Observaciones
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Observation As String Implements IFixedAssetActiveOutput.Observation
        Get
            Return INDmemoObservations.EditValue
        End Get
        Set(value As String)
            INDmemoObservations.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Secuencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sequense As FixedAssetSequence Implements IFixedAssetActiveOutput.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As FixedAssetSequence)
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
    ''' Representa los parametros del modulo de activos fijos.
    ''' </summary>
    Private _SettingsFixedAsset As SettingFixedAsset
    Public Property SettingsFixedAsset As SettingFixedAsset Implements IFixedAssetActiveOutput.SettingsFixedAsset
        Get
            Return _SettingsFixedAsset
        End Get
        Set(value As SettingFixedAsset)
            _SettingsFixedAsset = value
        End Set
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Sumatoria del valor neto de los articulos
    ''' </summary>
    ''' <remarks></remarks>
    Dim SumValue As Decimal

    ''' <summary>
    ''' Variable que contiene el objeto torre
    ''' </summary>
    Dim FixedAssetActiveOutput As FixedAssetActiveOutput

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PFixedAssetActiveOutput

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordFixedAsset

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "FixedAssets"

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.FixedAssetSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Listado de detalles
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListFixedAssetActiveOutputDetail As List(Of FixedAssetActiveOutputDetail)

    ''' <summary>
    ''' Listado de eliminados
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteFixedAssetActiveOutputDetail As List(Of FixedAssetActiveOutputDetail)

    ''' <summary>
    ''' Variable para la entidad que se va a editar en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Dim FixedAssetActiveOutputDetail As FixedAssetActiveOutputDetail

    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Dim IndexEditRecord As Integer

    ''' <summary>
    ''' Control para establecer informacion del ingreso
    ''' </summary>
    Private ctrTmp As CtrActiveOutputValue

#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        Try
            If ValidateControls() = True Then
                If FixedAssetActiveOutput.Status <> 3 Then
                    If ListFixedAssetActiveOutputDetail Is Nothing OrElse ListFixedAssetActiveOutputDetail.Count = 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar al menos un activo."
                        Exit Sub
                    End If
                    AssigningValues()
                End If
                Using model As New MFixedAssetActiveOutput(MyTag)
                    AsyncLoader(True)
                    Dim Result = Await model.SaveFixedAssetActiveOutput(FixedAssetActiveOutput, _idCurrentSequence)
                    If Result.StateResult = True Then
                        If FixedAssetActiveOutput.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                            'Se descarta la secuencia numerica usada
                            If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                                Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                            End If
                            If FixedAssetActiveOutput.Status = 1 Then
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                            ElseIf FixedAssetActiveOutput.Status = 2 Then
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SaveConfirm"), Result.ObjectEmbbeded.Code)
                            End If
                        ElseIf FixedAssetActiveOutput.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                            If FixedAssetActiveOutput.Status = 3 Then
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                            ElseIf FixedAssetActiveOutput.Status = 2 Then
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateConfirm")
                            ElseIf FixedAssetActiveOutput.Status = 1 Then
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                            End If
                        End If
                        Me.FixedAssetActiveOutput = Result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                        AsyncLoader(False)
                        Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        INDbtnCode.Enabled = False
                        If Result.StatusCode = eStatusResult.EXCEPTION Then
                            Mensaje(EeventViewerImages.MensajeError) = Result.Message
                        ElseIf Result.StatusCode = eStatusResult.WARNING Then
                            Mensaje(EeventViewerImages.Advertencia) = Result.Message
                        End If
                    End If
                End Using
            End If
        Catch ex As Exception
            AsyncLoader(False)
            INDbtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewFixedAssetActiveOutput()
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Confirmar()
        Try
            If ValidateControls() = True Then
                If FixedAssetActiveOutput.Status <> 3 Then
                    If ListFixedAssetActiveOutputDetail Is Nothing OrElse ListFixedAssetActiveOutputDetail.Count = 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar al menos un activo."
                        Exit Sub
                    End If
                    AssigningValues()
                End If
                Using model As New MFixedAssetActiveOutput(MyTag)
                    AsyncLoader(True)
                    Dim Result = Await model.ConfirmFixedAssetActiveOutput(FixedAssetActiveOutput, _idCurrentSequence)
                    If Result.StatusCode = eStatusResult.SUCCESS Then
                        Mensaje(EeventViewerImages.Informacion) = Result.Message
                        AsyncLoader(False)
                        Me.FixedAssetActiveOutput = Result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                        AsyncLoader(False)
                        Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        INDbtnCode.Enabled = False

                        If Result.MessageResult?.Any() Then
                            Using formulario As New FrmListErrors(Result.MessageResult)
                                formulario.Title = "Resultado validación comprobante contable"
                                formulario.StartPosition = FormStartPosition.CenterParent
                                Dim transparent As New FrmTransparent(formulario, False)
                                transparent.ShowDialog(Me)
                                Exit Sub
                            End Using
                        End If

                        If Result.StatusCode = eStatusResult.WARNING Then
                            Mensaje(EeventViewerImages.Advertencia) = Result.Message
                        ElseIf Result.StatusCode = eStatusResult.EXCEPTION Then
                            Mensaje(EeventViewerImages.MensajeError) = Result.Message
                        End If
                    End If
                End Using
            End If
        Catch ex As Exception
            AsyncLoader(False)
            INDbtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Edita el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditDetail()
        FixedAssetActiveOutputDetail = DirectCast(INDviewAssets.GetFocusedRow(), FixedAssetActiveOutputDetail)
        IndexEditRecord = ListFixedAssetActiveOutputDetail.IndexOf(FixedAssetActiveOutputDetail)
        OpenFormAddAssets(True)
    End Sub

    ''' <summary>
    ''' Elimina el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RemoveDetail()
        FixedAssetActiveOutputDetail = DirectCast(INDviewAssets.GetFocusedRow(), FixedAssetActiveOutputDetail)
        ListFixedAssetActiveOutputDetail.Remove(FixedAssetActiveOutputDetail)

        If FixedAssetActiveOutputDetail.Id > 0 Then
            If ListDeleteFixedAssetActiveOutputDetail Is Nothing Then
                ListDeleteFixedAssetActiveOutputDetail = New List(Of FixedAssetActiveOutputDetail)
            End If
            FixedAssetActiveOutputDetail.MarkAsDeleted()
            ListDeleteFixedAssetActiveOutputDetail.Add(FixedAssetActiveOutputDetail)
        End If

        CalculateValue()

        INDgcAssets.DataSource = Nothing
        INDgcAssets.DataSource = ListFixedAssetActiveOutputDetail
    End Sub

    ''' <summary>
    ''' Metodo que abre el form popup donde se escoge los activos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenFormAddAssets(_flagModeEdit As Boolean)
        Using formulario As New FrmFixedAssetActiveOutputDetail(_currency:=New Currency With {.Id = SettingsFixedAsset?.Currency?.Id, .Abbreviation = SettingsFixedAsset?.Currency?.Abbreviation})
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddFixedAssetActiveOutputDetailEventArgs, AddressOf ReturnAddEventArgs
            formulario.EditMode = _flagModeEdit
            formulario.ListCompare = ListFixedAssetActiveOutputDetail
            formulario.FixedAssetActiveOutputDetail = FixedAssetActiveOutputDetail
            formulario.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.7
            formulario.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.7
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que agrega el detalle a la entidad principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnAddEventArgs(sender As Object, e As AddFixedAssetActiveOutputDetail)
        If e.EditMode = False Then 'Se esta ingresando un articulo
            If ListFixedAssetActiveOutputDetail Is Nothing Then
                ListFixedAssetActiveOutputDetail = New List(Of FixedAssetActiveOutputDetail)
            End If
            ListFixedAssetActiveOutputDetail.Add(e.FixedAssetActiveOutputDetail)
            Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente."
        Else 'Se esta modificando un articulo
            ListFixedAssetActiveOutputDetail.Remove(FixedAssetActiveOutputDetail)
            ListFixedAssetActiveOutputDetail.Insert(IndexEditRecord, e.FixedAssetActiveOutputDetail)
            Mensaje(EeventViewerImages.Informacion) = "Detalle modificado correctamente."
        End If

        CalculateValue()

        INDgcAssets.DataSource = Nothing
        INDgcAssets.DataSource = ListFixedAssetActiveOutputDetail
    End Sub

    ''' <summary>
    ''' Calcula el valor del control de la barra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculateValue()
        SumValue = ListFixedAssetActiveOutputDetail.Sum(Function(item) item.HistoricalValue)
        ctrTmp.RefreshTotalValues()
    End Sub

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        'Me.ViewModeEditHold = True
        If Me.FixedAssetActiveOutput IsNot Nothing AndAlso Me.FixedAssetActiveOutput.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbtnCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IFixedAssetActiveOutput.ActionsOnControls
        Set(value As Boolean)
            INDlyActiveOutput.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDdteDocumentDate.Enabled = value
            INDmemoObservations.Enabled = value
            INDbtnAddAssets.Enabled = value
            INDgcAssets.Enabled = value
            INDlyActiveOutput.EndUpdate()
            If value Then
                INDdteDocumentDate.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar

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
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.33)},
                              New ColumnInfo() With {.Caption = "Fecha Documento", .FieldName = "DocumentDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.33)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.34)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetActiveOutput
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDbtnCode.Text = ReturnValue
        If INDbtnCode.Text <> String.Empty Then
            Await LoadControls()
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
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
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.FixedAssetActiveOutput.Code, FixedAssetActiveOutput.DocumentDate),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.FixedAssetActiveOutput.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.FixedAssetActiveOutput.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.FixedAssetActiveOutput.Code, FixedAssetActiveOutput.DocumentDate)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.FixedAssetActiveOutput.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlyActiveOutput.BeginUpdate()
        ReadOnlyControls(False)
        ActionsOnControls = False
        Code = String.Empty
        DocumentDate = Nothing
        Observation = Nothing
        INDgcAssets.DataSource = Nothing
        ListFixedAssetActiveOutputDetail = Nothing
        ListDeleteFixedAssetActiveOutputDetail = Nothing
        BarraBotones.CleanAuditBasic()
        FixedAssetActiveOutput = Nothing
        FixedAssetActiveOutputDetail = Nothing
        INDlyActiveOutput.EndUpdate()

        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
        SumValue = 0
        ctrTmp.RefreshTotalValues()
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With FixedAssetActiveOutput
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .DocumentDate = DocumentDate
            .Observation = Observation
            .OperatingUnitId = Me.BarraBotones.OperatingUnitValue

            .FixedAssetActiveOutputDetail.Clear()
            If ListFixedAssetActiveOutputDetail IsNot Nothing AndAlso ListFixedAssetActiveOutputDetail.Count > 0 Then
                ListFixedAssetActiveOutputDetail.ForEach(Sub(item)
                                                             .FixedAssetActiveOutputDetail.Add(item)
                                                         End Sub)
            End If

            If ListDeleteFixedAssetActiveOutputDetail IsNot Nothing AndAlso ListDeleteFixedAssetActiveOutputDetail.Count > 0 Then
                ListDeleteFixedAssetActiveOutputDetail.ForEach(Sub(item)
                                                                   .FixedAssetActiveOutputDetail.Add(item)
                                                               End Sub)
            End If

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
                Await Model.DeleteBlockRecord(record)
                record = Nothing
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
                Using Model As New MFixedAssetActiveOutput(CStr(Me.Tag))
                    AsyncLoader(True)
                    FixedAssetActiveOutput = (Await Model.GetFixedAssetActiveOutput(INDbtnCode.Text.Trim)).ObjectEmbbeded
                    INDlyActiveOutput.BeginUpdate()
                    If FixedAssetActiveOutput IsNot Nothing AndAlso FixedAssetActiveOutput.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(FixedAssetActiveOutput.Id))
                            With FixedAssetActiveOutput
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)

                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                Code = .Code
                                DocumentDate = .DocumentDate
                                Observation = .Observation
                                BarraBotones.StatusRecord = .Status.ToString

                                ListFixedAssetActiveOutputDetail = .FixedAssetActiveOutputDetail.ToList
                                INDgcAssets.DataSource = Nothing
                                INDgcAssets.DataSource = ListFixedAssetActiveOutputDetail
                                CalculateValue()
                            End With
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.FixedAssetActiveOutput.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordFixedAsset With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = FixedAssetActiveOutput.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            Me.BarraBotones.SetDocuments(FixedAssetActiveOutput.Id, Me.Tag.ToString(), Nothing, GetType(FixedAssetActiveOutput).Name)
                            AsyncLoader(False)
                            ActionsOnControls = True
                            If FixedAssetActiveOutput.Status = 1 Then
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                            Else
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                ReadOnlyControls(True)
                            End If
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, FixedAssetActiveOutput.Id, 0, FixedAssetActiveOutput.Id, _idOperativeUnit)
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewFixedAssetActiveOutput()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbtnCode.Focus()
                        End If
                    End If
                    INDlyActiveOutput.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        End If

        'Me.BarraBotones.StatusRecordVisible = True
        'Try
        '    AsyncLoader(True)
        '    Using Model As New MFixedAssetActiveOutput(MyTag)
        '        Dim resultOperation = Await Model.GetFixedAssetActiveOutput(INDbtnCode.Text.Trim)
        '        INDlyActiveOutput.BeginUpdate()
        '        FixedAssetActiveOutput = resultOperation.ObjectEmbbeded
        '        If Not FixedAssetActiveOutput Is Nothing Then
        '            If FixedAssetActiveOutput.Id > 0 Then
        '                Using ModelRecord As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
        '                    Dim result = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(FixedAssetActiveOutput.Id))
        '                    With FixedAssetActiveOutput
        '                        LayoutControls.SetCustomFieldsValue(.CustomProperties)

        '                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
        '                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
        '                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
        '                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

        '                        Code = .Code
        '                        DocumentDate = .DocumentDate
        '                        Observation = .Observation
        '                        BarraBotones.StatusRecord = .Status.ToString

        '                        ListFixedAssetActiveOutputDetail = .FixedAssetActiveOutputDetail.ToList
        '                        INDgcAssets.DataSource = Nothing
        '                        INDgcAssets.DataSource = ListFixedAssetActiveOutputDetail
        '                        CalculateValue()
        '                    End With
        '                    Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.FixedAssetActiveOutput.Code)
        '                    If result.Id = 0 Then
        '                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
        '                        state.State = Domain.Base.Entities.ObjectState.Added
        '                        record = New BlockRecordFixedAsset With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = CInt(Me.Tag), .CodUser = Me.indigo.UserIndigo, .IdRecord = FixedAssetActiveOutput.Id}
        '                        Dim operation = Await ModelRecord.SaveBlockRecord(record)
        '                        record = operation.ObjectEmbbeded
        '                    Else
        '                        record = result
        '                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
        '                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
        '                    End If
        '                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
        '                    Me.BarraBotones.SetDocuments(FixedAssetActiveOutput.Id)
        '                    AsyncLoader(False)
        '                    ActionsOnControls = True
        '                    If FixedAssetActiveOutput.Status = 1 Then
        '                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
        '                    Else
        '                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '                        ReadOnlyControls(True)
        '                    End If
        '                End Using
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
        '                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
        '                Me.BarraBotones.PrintReport(PrintReportAction.None, FixedAssetActiveOutput.Id, 0, FixedAssetActiveOutput.Id, _idOperativeUnit)
        '            Else
        '                AsyncLoader(False)
        '                If Me._sequence.IsManual Then
        '                    Me.NewFixedAssetActiveOutput()
        '                Else
        '                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '                    Me.Code = String.Empty
        '                    Deshacer()
        '                    INDbtnCode.Focus()
        '                End If
        '            End If
        '        Else
        '            AsyncLoader(False)
        '            If Me._sequence.IsManual Then
        '                Me.NewFixedAssetActiveOutput()
        '            Else
        '                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '                Me.Code = String.Empty
        '                Deshacer()
        '                INDbtnCode.Focus()
        '            End If
        '        End If
        '    End Using
        'Catch ex As Exception
        '    AsyncLoader(False)
        '    Throw ex
        'End Try
        'INDlyActiveOutput.EndUpdate()
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewFixedAssetActiveOutput() As Task
        Me.FixedAssetActiveOutput = New FixedAssetActiveOutput()
        Me.BarraBotones.StatusRecordVisible = True
        Me.BarraBotones.StatusRecord = "1"
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
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
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
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
                End If
            End If
        End If
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False


        'If _sequence Is Nothing OrElse _sequence.Id = 0 Then
        '    Mensaje(EeventViewerImages.Advertencia) = "El formulario no tiene parametrizada la secuencia numérica"
        '    Exit Sub
        'End If
        'Me.FixedAssetActiveOutput = New FixedAssetActiveOutput()
        'Me.BarraBotones.StatusRecordVisible = True
        'Me.BarraBotones.StatusRecord = "1"
        'If Me._sequence.IsManual Then
        '    Me.ActionsOnControls = True
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        'Else
        '    If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
        '        Me._idCurrentSequence = Me._sequence.FixedAssetSequenceDetail(0).Id
        '    ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
        '        If Me.Sequense.FixedAssetSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
        '            Me._idCurrentSequence = Me._sequence.FixedAssetSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
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
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '            Else
        '                Using model As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
        '                    Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
        '                End Using
        '                If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
        '                    Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
        '                    Me.ActionsOnControls = True
        '                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
        '                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '                Else
        '                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
        '                End If
        '            End If
        '        Else
        '            Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
        '            Me.ActionsOnControls = True
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
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
        'End If
        'Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
    End Function

    ''' <summary>
    ''' Importa la información lanzando el form para importar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ImportInfo()
        Using formulario As New FrmImportsPhysicalAsset
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddPhysicalAssetsEventArgs, AddressOf ReturnAddEventArgs
            formulario.ToolBar.Visible = False
            formulario.ListCompare = ListFixedAssetActiveOutputDetail
            formulario.Size = New Drawing.Size(1029, 674)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que agrega el detalle a la entidad principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnAddEventArgs(sender As Object, e As AddPhysicalAssets)
        If e IsNot Nothing Then
            If ListFixedAssetActiveOutputDetail Is Nothing Then
                ListFixedAssetActiveOutputDetail = New List(Of FixedAssetActiveOutputDetail)
            End If
            ListFixedAssetActiveOutputDetail.AddRange(e.ListFixedAssetActiveOutputDetail)
            INDgcAssets.DataSource = Nothing
            INDgcAssets.DataSource = ListFixedAssetActiveOutputDetail
            CalculateValue()
        End If
    End Sub

#End Region

#Region "Events"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        SumValue = Nothing
        FixedAssetActiveOutput = Nothing
        Presenter = Nothing
        record = Nothing
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        ListFixedAssetActiveOutputDetail = Nothing
        ListDeleteFixedAssetActiveOutputDetail = Nothing
        FixedAssetActiveOutputDetail = Nothing
        IndexEditRecord = Nothing
        ctrTmp = Nothing
    End Sub


    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmFixedAssetActiveOutput_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyActiveOutput, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PFixedAssetActiveOutput(Me)

        Await Presenter.GetSettingFixedAssetByOperatingUnitId(_idOperativeUnit)
        SetCurrencyUI(SettingsFixedAsset?.Currency?.Abbreviation)

        IndigoGridControl1.RefreshGrid(INDgcAssets)
        IndigoGridView1.SetListAcction(INDviewAssets, {eAcciones.Edit, eAcciones.Remove}.ToList())
        Presenter.GetSequense()
        Presenter.LoadDefinitionLayout()
        LoadStatus()
        Deshacer()
    End Sub

    ''' <summary>
    ''' Establece el formato moneda en los controles del formulario
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    Private Sub SetCurrencyUI(_currencyAbbreviation As String)

        If String.IsNullOrEmpty(_currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "Está llegando vacia la abreviación de la moneda"
            Exit Sub
        End If

        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = _currencyAbbreviation.GetNumberFormat

        Me.ctrTmp.CodeISO4217 = _currencyAbbreviation
        ctrTmp.RefreshTotalValues()
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmFixedAssetActiveOutput_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento para consultar un concepto de notas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown
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
                    Await Me.NewFixedAssetActiveOutput()
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
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbtnCode.Enabled Then
            INDbtnCode.Focus()
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddAssets_Click(sender As Object, e As EventArgs) Handles INDbtnAddAssets.Click
        OpenFormAddAssets(False)
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
                RemoveDetail()
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
                RemoveDetail()
        End Select
    End Sub

#End Region

#Region "CustomColumnDisplayText"

    ''' <summary>
    ''' Evento que se dispara al pintar las columnas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDviewAssets_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDviewAssets.CustomColumnDisplayText
        If e.Value Is Nothing OrElse e.Value.GetType.ToString = "DevExpress.Data.NotLoadedObject" Then
            Exit Sub
        End If
        If e.Column.Name = INDcolOutputType.Name Then
            Select Case e.Value
                Case 1
                    e.DisplayText = "Baja"
                Case 2
                    e.DisplayText = "Venta"
                Case 3
                    e.DisplayText = "Consignación"
                Case 4
                    e.DisplayText = "Bienes Transferidos"
                Case Else

            End Select
        End If
        If e.Column.Name = INDcolActivePart.Name Then
            Select Case e.Value
                Case 1
                    e.DisplayText = "Activo"
                Case 2
                    e.DisplayText = "Parte"
            End Select
        End If
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Importar información
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ImportarInformacion() Handles BarraBotones.Click_ImportarInformacion
        ImportInfo()
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        FixedAssetActiveOutput.Status = 1
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
        FixedAssetActiveOutput.Status = 1
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        FixedAssetActiveOutput.Status = 2
        Confirmar()
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        FixedAssetActiveOutput.Status = 2
        Confirmar()
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        FixedAssetActiveOutput.Status = 3
        Guardar()
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, FixedAssetActiveOutput.Id, 0, FixedAssetActiveOutput.Id, _idOperativeUnit)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Me.Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
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
#End Region

End Class