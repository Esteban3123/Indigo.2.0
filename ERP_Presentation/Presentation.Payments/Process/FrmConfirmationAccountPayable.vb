'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/03/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Payments.MVP
Imports Presentation.Controls
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

Public Class FrmConfirmationAccountPayable
    Implements IConfirmationAccountPayable


#Region "Properties and variables"

    Public Property AccountFinal As String Implements IConfirmationAccountPayable.AccountFinal

    Public Property AccountInitial As String Implements IConfirmationAccountPayable.AccountInitial

    ''' <summary>
    ''' Obtiene o establece la fecha final
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DateFinal As Date Implements IConfirmationAccountPayable.DateFinal
        Get
            Return CDate(INDdteFinal.EditValue)
        End Get
        Set(value As Date)
            INDdteFinal.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha Inicial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DateInitial As Date Implements IConfirmationAccountPayable.DateInitial
        Get
            Return CDate(INDdteInitial.EditValue)
        End Get
        Set(value As Date)
            INDdteInitial.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el rango
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Rank As Boolean Implements IConfirmationAccountPayable.Rank
        Get
            Return CBool(INDrgRanges.EditValue)
        End Get
        Set(value As Boolean)
            INDrgRanges.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado de los registros de los conceptos de notas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Boolean Implements IConfirmationAccountPayable.Status
        Get
            Return CBool(BarraBotones.StatusRecord)
        End Get
        Set(value As Boolean)
            BarraBotones.StatusRecord = value
        End Set
    End Property

    ''' <summary>
    ''' Variable para conocer si el formulario abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean = False

    ''' <summary>
    ''' Presentador de concepto de notas
    ''' </summary>
    Dim Presenter As PConfirmationAccountPayable

#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControls()
        If Not SearchMode Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Sub Eliminar() Implements Base.IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Sub Guardar() Implements Base.IcrudBase.Guardar

    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo
        CleanControls()
    End Sub

#End Region

#Region "Methods and Functions"

    ''' <summary>
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IConfirmationAccountPayable.ActionsOnControls
        Set(value As Boolean)
            INDrgRanges.Enabled = Not value
            INDdteInitial.Enabled = value
            INDdteFinal.Enabled = value
            INDgleAccountPayableInitial.Enabled = value
            INDgleAccountPayableFinal.Enabled = value
            If value Then
                INDdteInitial.Focus()
            Else
                INDrgRanges.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
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
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code"}, New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Name"}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ContractModificationReason
            .FormParent = Me
            .ShowSearch()
        End With
        SearchMode = True
    End Sub

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        'INDbteCode.Text = ReturnValue
        'If INDbteCode.Text <> String.Empty Then
        '    LoadControls()
        '    If INDbteCode.Enabled = False Then
        '        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        '    End If
        '    INDbteCode.Enabled = False
        'End If
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        'If Me._doc Is Nothing Then
        '    Me._doc = New IndexedDocument2 With { _
        '        .Content = String.Format(obtenerRecurso(Eresources.FrmContractModificationReasonMetaData, Eform.InfoMetaData), Me.ContractModificationReason.Code, Me.ContractModificationReason.Name, Me.ContractModificationReason.Description), _
        '        .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .JournalVoucher = IndexedDocumentType.File, _
        '        .IdEntity =  "$#" & Me.Tag & "_" & Me.ContractModificationReason.Code & "#$", .IdForm = Me.Tag, _
        '        .Title = String.Format(obtenerRecurso(Eresources.FrmContractModificationReasonMetaDataTitle, Eform.InfoMetaData), Me.ContractModificationReason.Code), _
        '        .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
        '    Return Me._doc
        'Else
        '    Me._doc.Update = dateServer
        '    Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
        '    Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmContractModificationReasonMetaData, Eform.InfoMetaData), Me.ContractModificationReason.Code, Me.ContractModificationReason.Name, Me.ContractModificationReason.Description)
        '    Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmContractModificationReasonMetaDataTitle, Eform.InfoMetaData), Me.ContractModificationReason.Code)
        '    Return Me._doc
        'End If
        Return Me._doc
    End Function

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        'Dim listStates As New List(Of StatusRecord)()
        'listStates.Add(New StatusRecord With {.StatusValue = True, .StatusName = obtenerRecurso(ComunesActivo), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(23, Byte), Integer))})
        'listStates.Add(New StatusRecord With {.StatusValue = False, .StatusName = obtenerRecurso(ComunesInactivo), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(38, Byte), Integer))})
        'Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlycConfirmationAccountPayable.BeginUpdate()
        ActionsOnControls = False
        INDrgRanges.EditValue = Nothing
        INDdteInitial.EditValue = Nothing
        INDdteFinal.EditValue = Nothing
        INDgleAccountPayableInitial.EditValue = Nothing
        INDgleAccountPayableFinal.EditValue = Nothing
        INDlycConfirmationAccountPayable.EndUpdate()

        'Entity = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
    End Sub

    ''' <summary>
    ''' Validates the controls.
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControls() As Boolean
        ' If INDbteCode.EditValue Is Nothing OrElse INDbteCode.Text.Equals(String.Empty) Then
        '     Return False
        ' End If
        ' If INDtxtName.EditValue Is Nothing OrElse INDtxtName.Text.Equals(String.Empty) Then
        '     Return False
        ' End If
        ' If INDmemDescription.EditValue Is Nothing OrElse INDmemDescription.Text.Equals(String.Empty) Then
        '     Return False
        ' End If
        ' Return True
        Return False
    End Function

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        'With ContractModificationReason
        '    .Code = INDbteCode.Text.Trim
        '    .Name = INDtxtName.Text.Trim
        '    .Description = INDmemDescription.Text.Trim
        '    .State = Me.BarraBotones.StatusRecord
        'End With
    End Sub

    Public Sub DeleteBlockedRecord()
        'If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
        '    Using Model As New MContractModificationReason(Me.Tag)
        '        Await Model.DeleteBlockRecord(record)
        '        record = Nothing
        '    End Using
        'End If
    End Sub

    Private Sub LoadControls()
        'Me.BarraBotones.StatusRecordVisible = True
        'Me.BarraBotones.StatusRecord = True
        'INDbteCode.Enabled = False
        'AsyncLoader(True)
        'Using Model As New MContractModificationReason(Me.Tag)
        '    ContractModificationReason = Await Model.GetContractModificationReasonAsync(INDbteCode.Text.Trim)
        '    If Not ContractModificationReason Is Nothing Then
        '        If ContractModificationReason.Id > 0 Then
        '            Dim result = Await Model.GetBlockRecord(Me.Tag, ContractModificationReason.Id)
        '            With ContractModificationReason
        '                'LogicaBotonActualizar(True)
        '                INDbteCode.EditValue = .Code
        '                INDtxtName.EditValue = .Name
        '                INDmemDescription.EditValue = .Description
        '                Me.BarraBotones.StatusRecord = .State
        '            End With
        '            Me.GetDocumentIndexed(Me.Tag & "_" & Me.ContractModificationReason.Code)
        '            If result.Id = 0 Then
        '                Me.BarraBotones.SetDocuments(ContractModificationReason.Id)
        '                Dim state = New Domain.Base.Entities.ObjectChangeTracker
        '                state.State = Domain.Base.Entities.ObjectState.Added
        '                record = New BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = ContractModificationReason.Id}
        '                Dim operation = Await Model.SaveBlockRecord(record)
        '                record = operation.ObjectEmbbeded
        '            Else
        '                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
        '                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning)
        '            End If
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
        '        Else
        '            ContractModificationReason = New ContractModificationReason
        '            'LogicaBotonActualizar(False)
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '        End If
        '    Else
        '        'LogicaBotonActualizar(False)
        '        ContractModificationReason = New ContractModificationReason
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    End If
        'End Using
        'AsyncLoader(False)
        'ActionsOnControls = True
    End Sub

#End Region

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        AccountFinal = Nothing
        AccountInitial = Nothing
        SearchMode = Nothing
        Presenter = Nothing
    End Sub
    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmConfirmationAccountPayable_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PConfirmationAccountPayable(Me)

        LoadStatus()
        Deshacer()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmConfirmationAccountPayable_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        'Me.ViewModeEditHold = True
        'If Me.ContractModificationReason IsNot Nothing AndAlso Me.ContractModificationReason.Id > 0 Then
        '    If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
        '        DeleteBlockedRecord()
        '        Me.INDbteCode.Text = Me.IdEntity.Trim()
        '        Me.LoadControls()
        '    End If
        'Else 'Realiza la consulta normal
        '    Me.INDbteCode.Text = Me.IdEntity.Trim()
        '    Me.LoadControls()
        'End If
        'Me.IdEntity =  String.Empty
    End Sub

#End Region

#Region "Bar Button Events"

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
        Guardar()
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
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        CleanControls()
    End Sub

#End Region

    
    
    
End Class