'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Cristian Camilo Bahamon Castaño
' Created          : 18-07-2022
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.Data.Linq
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Billing.MVP
Imports Presentation.Controls
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Base.Entities
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports System.ComponentModel
Imports DevExpress.XtraEditors.Controls
#End Region

Public Class FrmBillingJustificationControl

    Implements IBillingJustificationControl, ICustomizableForm

#Region "Builders"
    Public Sub New()
        InitializeComponent()
    End Sub
#End Region

#Region "Const"
    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Billing"
#End Region

#Region "Variables"



    'Private _billingJustificationControl As JustificationControl
    ''' <summary>
    ''' Referencia al presentador
    ''' </summary>
    Private Presenter As PBillingJustificationControl

    ''' <summary>
    ''' The model
    ''' </summary>
    Dim model As MBillingJustificationControl


    ''' <summary>
    ''' Representa la entidad de autorización de facturacion
    ''' </summary>
    ''' <remarks></remarks>
    Dim billingJustificationControl As BillingJustificationControl

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.BillingSequence

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As BlockRecordBilling

    ''' <summary>
    ''' Identifica si se esta cargando el registro
    ''' </summary>
    ''' <remarks></remarks>
    Private isLoading As Boolean = False

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordBilling

    ''' <summary>
    ''' Variable para controlar si el formulario esta en modo busqueda
    ''' </summary>
    Private _searchMode As Boolean

    ''' <summary>
    ''' Usuario xpo
    ''' </summary>
    ''' <remarks></remarks>
    Private _usersXpo As Infrastructure.Data.Xpo.SecurityRepository.UserXpo

    ''' <summary>
    ''' Listado del detallo de usarios autorizaciones de facturación
    ''' </summary>
    ''' <remarks></remarks>
    Public Property ListBillingJustificationUser As List(Of BillingJustificationControlUser)
        Get
            Return INDGcUsers.DataSource
        End Get
        Set(value As List(Of BillingJustificationControlUser))
            INDGcUsers.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Controla el editvalueChanged del control de usuario
    ''' </summary>
    ''' <remarks></remarks>
    Private ban As Boolean = False


#End Region

#Region "Properties"
    ''' <summary>
    ''' Obtiene o asigna el tag del funcional
    ''' </summary>
    ''' <value>Tag del fucnional</value>
    ''' <returns></returns>
    Public ReadOnly Property MyTag As String Implements IBillingJustificationControl.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que retorna el layout para customizaciones
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IBillingJustificationControl.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Esta Propiedad establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IBillingJustificationControl.ActionsOnControls
        Set(value As Boolean)
            LcRoot.BeginUpdate()

            INDBtnCode.Enabled = Not value
            INDTxtDescription.Enabled = value
            INDTxtObservation.Enabled = value
            INDsleSkipClearance.Enabled = value

            INDSleUsers.Enabled = value
            INDEsbBills.Enabled = value
            INDsbAdd.Enabled = value
            INDGcUsers.Enabled = value

            LcRoot.EndUpdate()
            If value Then
                INDTxtDescription.Focus()
            Else
                INDBtnCode.Focus()
            End If
        End Set
    End Property


    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Public Property Sequense As BillingSequence Implements IBillingJustificationControl.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As BillingSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.BillingSequenceDetail In Me._sequence.BillingSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el codigo de la cuenta hospitalaria
    ''' </summary>
    ''' <value>Codigo de la cuenta hospitalaria</value>
    ''' <returns>El codigo de la cuenta hospitalaria</returns>
    Public Property Code As String Implements IBillingJustificationControl.Code
        Get
            If (INDBtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDBtnCode.Text.Trim()
            End If
        End Get
        Set(value As String)
            INDBtnCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna una descripcion de la cuenta hospitalaria
    ''' </summary>
    ''' <value>Descripcion de la cuenta hospitalaria</value>
    ''' <returns>La descripcion de la cuenta hospitalaria</returns>
    Public Property Description As String Implements IBillingJustificationControl.Description
        Get
            Return INDTxtDescription.EditValue
        End Get
        Set(value As String)
            INDTxtDescription.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna una observacion de la cuenta hospitalaria
    ''' </summary>
    ''' <value>Observacion de la cuenta hospitalaria</value>
    ''' <returns>La observacion de la cuenta hospitalaria</returns>
    Public Property Observation As String Implements IBillingJustificationControl.Observation
        Get
            Return INDTxtObservation.EditValue
        End Get
        Set(value As String)
            INDTxtObservation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna un valor que indique si se le omite la liquidacion o no de la cuenta hospitalaria
    ''' </summary>
    ''' <value>Omitir liquidacion</value>
    ''' <returns>Omitir Liquidacion</returns>
    Public Property SkipClearance As Boolean Implements IBillingJustificationControl.SkipClearance
        Get
            Return INDsleSkipClearance.EditValue
        End Get
        Set(value As Boolean)
            INDsleSkipClearance.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del usuario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdUser As Integer? Implements IBillingJustificationControl.IdUser
        Get
            Return INDSleUsers.EditValue
        End Get
        Set(value As Integer?)
            INDSleUsers.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the list users.
    ''' </summary>
    ''' <value>
    ''' The list users.
    ''' </value>
    Public Property ListJusticationUsers As List(Of BillingJustificationControlUser)
        Get
            Return CType(INDGcUsers.DataSource, List(Of BillingJustificationControlUser))
        End Get
        Set(value As List(Of BillingJustificationControlUser))
            INDGcUsers.DataSource = value
        End Set
    End Property



    ''' <summary>
    ''' Establece el datasource de usuarios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UserXpo As LinqInstantFeedbackSource Implements IBillingJustificationControl.UserXpo
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As LinqInstantFeedbackSource)
            Throw New NotImplementedException()
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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

#Region "Handlers"

#Region "Load"
    ''' <summary>
    ''' Handles the Load event of the FrmCategories control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmBillingJustificationControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'me.layoutcontrols.setiscustomizable(me.indlcbillinggroup, true)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PBillingJustificationControl(Me)
        Presenter.GetSequense()

        model = New MBillingJustificationControl(Me.Tag)

        'agregar los usuarios **********
        Dim listactions As New List(Of eAcciones)()
        listactions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDGvUsers, listactions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvUsers.Columns
            If col.Name = "colactions" Then
                col.Width = 150
            End If
        Next
        IndigoGridControl1.RefreshGrid(INDGcUsers)
        INDEsbBills.AddRangeColumns("código usuario")
        'presenter.loaddefinitionlayout()

        Deshacer()
        'Me.LayoutControls.SetIsCustomizable(Me.indlcbillinggroup, True)

    End Sub
#End Region

#Region "KeyDown"

    Private Async Sub INDBtnCode_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles INDBtnCode.KeyDown
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
                    Await Me.NewBillingJustificationControl()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub
#End Region

#Region "EditValueChanged"

    Private Sub INDsleUsers_EditValueChanged(sender As Object, e As ChangingEventArgs) Handles INDSleUsers.EditValueChanged
        If e.NewValue IsNot Nothing AndAlso ban = False Then

            _usersXpo = DirectCast(DirectCast(INDSleUsers.GetSelectedDataRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.SecurityRepository.UserXpo)
        End If
    End Sub

#End Region

#Region "Activated"
    ''' <summary>
    ''' Handles the Activated event of the FrmCategories control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmBillingJustificationControl_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBtnCode.Enabled Then
            INDBtnCode.Focus()
        End If
    End Sub
#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de agregar usuario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddUser_Click(sender As Object, e As EventArgs) Handles INDsbAdd.Click
        If INDSleUsers.EditValue IsNot Nothing Then
            CreateBillingJustificationUser()
        Else
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectedUser", "Inventory")
            INDSleUsers.Focus()
        End If
    End Sub
    ''' <summary>
    ''' Handles the Click event of the INDsbAdd control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAdd_Click(sender As Object, e As EventArgs) Handles INDsbAdd.Click
        If INDSleUsers.EditValue IsNot Nothing Then
            If ListJusticationUsers Is Nothing Then
                ListJusticationUsers = New List(Of BillingJustificationControlUser)()
            End If
            'Dim user As UserXpo = DirectCast(DirectCast(INDGvUser.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, UserXpo) 'CType(INDGvUser.GetFocusedRow(), UserXpo)
            If ListJusticationUsers.Any(Function(x) x.UserId = _usersXpo.Id) Then
                Mensaje(eStatusResult.WARNING) = "Este usuario ya está agregado"
                Exit Sub
            End If

            Dim justificationUser As New BillingJustificationControlUser() With {.UserCode = _usersXpo.UserCode, .UserId = _usersXpo.Id}
            billingJustificationControl.BillingJustificationControlUser.Add(justificationUser)
            ListJusticationUsers = billingJustificationControl.BillingJustificationControlUser.ToList()

            If billingJustificationControl.ChangeTracker.State <> ObjectState.Added Then
                billingJustificationControl.MarkAsModified()
            End If

            INDSleUsers.EditValue = Nothing
            INDSleUsers.Focus()
        End If
    End Sub


#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara al presionar el boton del control de usuarios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleUsers_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleUsers.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New Presentation.Security.FrmUsers With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeUsers()
        End If
    End Sub

#End Region

#Region "QueryPopUp"
    ''' <summary>
    ''' Handles the QueryPopUp event of the INDSleUsers control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleUsers_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleUsers.QueryPopUp
        If INDSleUsers.Properties.DataSource Is Nothing Then
            INDSleUsers.Properties.DataSource = model.ListUsers()
        End If
    End Sub
#End Region

#Region "Disposed"
    ''' <summary>
    ''' Handles the Disposed event of the FrmCategories control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmBillingJustificationControl_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        model.Dispose()
        model = Nothing
        _sequence = Nothing
        Presenter = Nothing
        _usersXpo = Nothing
        _idOperativeUnit = Nothing
        billingJustificationControl = Nothing
        _idCurrentSequence = Nothing
        record = Nothing
    End Sub
#End Region

#Region "Actions"
    ''' <summary>
    ''' Handles the ButtonAction event of the IndigoGridView1_Click control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim justificationUser As BillingJustificationControlUser = CType(INDGvUsers.GetFocusedRow(), BillingJustificationControlUser)
            justificationUser.MarkAsDeleted()

            ListJusticationUsers = billingJustificationControl.BillingJustificationControlUser.ToList()

            If billingJustificationControl.ChangeTracker.State <> ObjectState.Added Then
                billingJustificationControl.MarkAsModified()
            End If
        End If
    End Sub
#End Region

#Region "Identity"
    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded


        Me.ViewModeEditHold = True
        If Me.billingJustificationControl IsNot Nothing AndAlso Me.billingJustificationControl.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDBtnCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBtnCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub
#End Region

#Region "FormClosing"

    Private Sub FrmBillingJustificationControl_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "PasteToGrid"

    'Private Async Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
    '    Await PasteToGrid(sender, e.Rows)
    'End Sub

    ''' <summary>
    ''' Metodo que copia y pega los items a la rejilla de facturas
    ''' </summary>
    ''' <remarks></remarks>
    'Private Async Function PasteToGrid(sender As DevExpress.XtraGrid.GridControl, ListInfo As List(Of List(Of String))) As Task
    '    If sender.Name = INDGcUsers.Name Then
    '        INDGvUsers.ShowLoadingPanel()
    '        Me.Cursor = ChangeCursorIndigo()
    '        Using model As New MBillingJustificationControl(MyTag)
    '            Dim result = Await model.(ListInfo)
    '            If result.StateResult = False Then
    '                Mensaje(EeventViewerImages.MensajeError) = result.Message
    '                Me.Cursor = System.Windows.Forms.Cursors.Default
    '                INDGvUsers.HideLoadingPanel()
    '                Exit Function
    '            End If
    '            If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
    '                If ListBillingJustificationUser IsNot Nothing AndAlso ListBillingJustificationUser.Count > 0 Then
    '                    Dim listBillsTmp = (From s In result.ObjectEmbbeded Select s.UserId).ToList.Distinct.ToList()
    '                    Dim listBillsNotExist As New List(Of String)
    '                    For i As Integer = 0 To listBillsTmp.Count - 1 Step 1
    '                        Dim share = ListBillingJustificationUser.Where(Function(x) x.UserId = listBillsTmp.Item(i)).FirstOrDefault()
    '                        If share IsNot Nothing Then
    '                            If result.ObjectEmbbededAux Is Nothing Then
    '                                result.ObjectEmbbededAux = New List(Of Tuple(Of String, Integer))
    '                            End If
    '                            result.ObjectEmbbededAux.Add(New Tuple(Of String, Integer)("El usuario " + share.UserFullName + " ya existe en la lista", 2))
    '                        Else
    '                            listBillsNotExist.Add(listBillsTmp.Item((i)))
    '                        End If
    '                    Next
    '                    If billingJustificationControl.BillingJustificationControlUser Is Nothing Then
    '                        billingJustificationControl.BillingJustificationControlUser = New Domain.Entities.TrackableCollection(Of BillingJustificationControlUser)
    '                    End If
    '                    For i As Integer = 0 To listBillsNotExist.Count - 1 Step 1
    '                        For Each item In result.ObjectEmbbeded.FindAll(Function(x) x.UserId = listBillsNotExist.Item(i))
    '                            billingJustificationControl.BillingJustificationControlUser.Add(item.MarkAsAdded)
    '                        Next
    '                        ListBillingJustificationUser.AddRange(result.ObjectEmbbeded.FindAll(Function(x) x.UserId = listBillsNotExist.Item(i)))
    '                    Next
    '                    INDGcUsers.RefreshDataSource()
    '                Else
    '                    If billingJustificationControl.BillingJustificationControlUser Is Nothing Then
    '                        billingJustificationControl.BillingJustificationControlUser = New Domain.Entities.TrackableCollection(Of BillingJustificationControlUser)
    '                    End If
    '                    For Each item In result.ObjectEmbbeded
    '                        billingJustificationControl.BillingJustificationControlUser.Add(item.MarkAsAdded)
    '                    Next
    '                    ListBillingJustificationUser = result.ObjectEmbbeded
    '                    INDGcUsers.RefreshDataSource()
    '                End If
    '            End If
    '            If result.ObjectEmbbededAux IsNot Nothing AndAlso result.ObjectEmbbededAux.Count > 0 Then
    '                Using formulario As New FrmListErrors(result.ObjectEmbbededAux)
    '                    formulario.StartPosition = FormStartPosition.CenterParent
    '                    Dim transparent As New FrmTransparent(formulario, False)
    '                    Me.Cursor = System.Windows.Forms.Cursors.Default
    '                    transparent.ShowDialog(Me)
    '                End Using
    '            End If
    '        End Using
    '        'INDGcUsers.DataSource = Nothing
    '        'INDGcUsers.DataSource = ListInvoiceCategoriesUsers
    '        Me.Cursor = System.Windows.Forms.Cursors.Default
    '        INDGvUsers.HideLoadingPanel()
    '    End If
    'End Function

#End Region


#End Region

#Region "Methods"




    ''' <summary>
    ''' Generates the document.
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.billingJustificationControl.Code, billingJustificationControl.Description),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.billingJustificationControl.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.billingJustificationControl.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.billingJustificationControl.Code, Me.billingJustificationControl.Description)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.billingJustificationControl.Code)
            Return Me._doc
        End If
    End Function


    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With billingJustificationControl
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Description = Description
            .Observation = Observation
            .SkipClearance = SkipClearance

            If ListBillingJustificationUser IsNot Nothing Then
                For Each itemUser As BillingJustificationControlUser In ListBillingJustificationUser
                    .BillingJustificationControlUser.Add(itemUser)
                Next
            End If

        End With
    End Sub

    Private Async Function NewBillingJustificationControl() As Task
        billingJustificationControl = New BillingJustificationControl()
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.BillingSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.BillingSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.BillingSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
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
    ''' Cleans the controls.
    ''' </summary>
    Private Sub CleanControls()

        LcRoot.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me._doc = Nothing

        Code = String.Empty
        Description = String.Empty
        Observation = String.Empty
        SkipClearance = False
        'Limpiar controles
        billingJustificationControl = Nothing
        INDGcUsers.DataSource = Nothing

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        LcRoot.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Deletes the blocked record.
    ''' </summary>
    Private Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Returns the value.
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>

    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MBillingJustificationControl(CStr(Me.Tag))
                    AsyncLoader(True)

                    Dim resultOperation = Await Model.GetBillingJustificationControlByCode(INDBtnCode.Text.Trim)
                    billingJustificationControl = resultOperation.ObjectEmbbeded

                    LcRoot.BeginUpdate()

                    If billingJustificationControl IsNot Nothing AndAlso billingJustificationControl.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(billingJustificationControl.Id))
                            With billingJustificationControl
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                isLoading = True

                                'Llenar Entidad
                                Code = .Code
                                Description = .Description
                                Observation = .Observation
                                SkipClearance = .SkipClearance

                                ListBillingJustificationUser = .BillingJustificationControlUser.ToList

                                'ListBillingJustificationUser = billingJustificationControl.BillingJustificationControlUser.ToList()

                                'INDGcUsers.DataSource = Nothing
                                INDGcUsers.DataSource = ListBillingJustificationUser



                                isLoading = False
                            End With
                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.billingJustificationControl.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordBilling With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = billingJustificationControl.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                            Me.BarraBotones.SetDocuments(billingJustificationControl.Id, Me.Tag.ToString(), Nothing, GetType(BillingAuthorization).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewBillingJustificationControl()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDBtnCode.Focus()
                        End If
                    End If
                    LcRoot.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Crea el objeto para el listado de la rejilla
    ''' </summary>
    Private Sub CreateBillingJustificationUser()
        If billingJustificationControl.BillingJustificationControlUser.Any(Function(item) item.UserId = IdUser) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("UserDuplicated", "Inventory")
            INDSleUsers.Focus()
            Exit Sub
        End If

        Dim billingJustificationUser As New BillingJustificationControlUser
        With billingJustificationUser
            .UserId = _usersXpo.Id
            .UserCode = _usersXpo.UserCode
            .UserFullName = _usersXpo.PersonFullName


        End With

        billingJustificationControl.BillingJustificationControlUser.Add(billingJustificationUser)
        ListBillingJustificationUser = billingJustificationControl.BillingJustificationControlUser.ToList()

        If billingJustificationControl.Id > 0 Then
            billingJustificationControl.MarkAsModified()
        End If

        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UserAggregatedSatisfactory", "Inventory")
        INDSleUsers.EditValue = Nothing
        INDSleUsers.Focus()
    End Sub


    ''' <summary>
    ''' Elimina un usuario de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteWarehouseUser()
        Dim bau As BillingJustificationControlUser = CType(INDGvUsers.GetFocusedRow, BillingJustificationControlUser)
        bau.MarkAsDeleted()

        ListBillingJustificationUser = billingJustificationControl.BillingJustificationControlUser.ToList()

        If billingJustificationControl.Id > 0 Then
            billingJustificationControl.MarkAsModified()
        End If
    End Sub



#End Region

#Region "ICrud"
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    Public Async Sub Guardar() Implements ICrudBase.Guardar

        If Not ValidateControls() Then
            Exit Sub
        End If

        AssigningValues()

        Try
            Using model As New MBillingJustificationControl(MyTag)
                AsyncLoader(True)
                Dim result As ActionResult(Of BillingJustificationControl) = Await model.SaveBillingJustificationControl(billingJustificationControl, _idCurrentSequence)

                If result.StatusCode = eStatusResult.SUCCESS Then
                    If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                        Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                    End If
                    Me.billingJustificationControl = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDBtnCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBtnCode.Enabled = False
            Throw ex
        End Try

    End Sub

    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await Me.NewBillingJustificationControl()
        End If
    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        If Utils.IsFoundational(Me.Tag, indigo) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DeleteFoundational")
            Exit Sub
        End If

        If Me.billingJustificationControl IsNot Nothing AndAlso Me.billingJustificationControl.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MBillingJustificationControl(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteBillingJustificationControl(Me.billingJustificationControl)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Await Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDBtnCode.Enabled = False
                        End If
                        ShowMessage(result.StatusCode) = result.Message
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDBtnCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub


    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        Throw New NotImplementedException()
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3},
                              New ColumnInfo() With {.Caption = "Descripcion", .FieldName = "Description", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4},
                              New ColumnInfo() With {.Caption = "Observacion", .FieldName = "Observation", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4},
                              New ColumnInfo() With {.Caption = "Omitir Liquidacion", .FieldName = "SkipClearance", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListBillingJustificationControl
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
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDBtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBtnCode.Enabled = False
        End If
    End Sub

#End Region

#Region "BarButtons"
    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBtnCode.ButtonClick
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
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub


    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir de la barra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        'Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _invoiceCategories.Id, _invoiceCategories.Id)
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

    Private Sub INDdeResolutionDate_EditValueChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub INDtxtObservationBillingJustification_EditValueChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub INDEsbBills_Click(sender As Object, e As EventArgs) Handles INDEsbBills.Click

    End Sub
#End Region


End Class