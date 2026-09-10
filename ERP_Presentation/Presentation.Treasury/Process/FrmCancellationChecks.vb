'***********************************************************************
' Assembly         : Presentacion.Treasury
' Author           : Diego Andrés Roldán
' Created          : 23-05-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Treasury.MVP
Imports Presentation.Base
Imports Presentation.Common
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Controls
Imports Domain.Base.Entities
Imports System.Text
Imports DevExpress.Xpo
Imports Presentation.Common.MVP

#End Region

Public Class FrmCancellationChecks
    Implements ICancellationCheck, ICustomizableForm

#Region "Properties and Variables"

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Constante con el nombre del modulo
    ''' </summary>
    Public Const NAME_MODULE As String = "Treasury"

    ''' <summary>
    ''' variable utilizada para guardar el id de la chequera activa correspondiente a la cuenta bancaria
    ''' </summary>
    Private _idCheckBook As Integer

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
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
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ICancellationCheck.ActionsOnControls
        Set(value As Boolean)
            INDlycRoot.BeginUpdate()
            INDsleEntityAccount.Enabled = Not value
            INDdeCancellationDate.Enabled = value
            INDtxtCheckNumber.Enabled = value
            INDmeDescription.Enabled = value

            Me.BarraBotones.StatusRecordVisible = value

            INDlycRoot.EndUpdate()

            If value Then
                INDdeCancellationDate.Focus()
                INDdeCancellationDate.Properties.ReadOnly = True
            Else
                INDsleEntityAccount.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha de la cancelacion
    ''' </summary>
    ''' <value>
    ''' The cancellation date.
    ''' </value>
    Public Property CancellationDate As DateTime Implements ICancellationCheck.CancellationDate
        Get
            Return INDdeCancellationDate.EditValue
        End Get
        Set(value As DateTime)
            INDdeCancellationDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el numero del cheque a cancelar
    ''' </summary>
    ''' <value>
    ''' The check number.
    ''' </value>
    Public Property CheckNumber As String Implements ICancellationCheck.CheckNumber
        Get
            Return INDtxtCheckNumber.Text
        End Get
        Set(value As String)
            INDtxtCheckNumber.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la descripcion de la cancelacion del cheque
    ''' </summary>
    ''' <value>
    ''' The description.
    ''' </value>
    Public Property Description As String Implements ICancellationCheck.Description
        Get
            Return INDmeDescription.Text
        End Get
        Set(value As String)
            INDmeDescription.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id de la entidad bancaria que contiene el cheque a cancelar
    ''' </summary>
    ''' <value>
    ''' The identifier entity account.
    ''' </value>
    Public Property IdEntityAccount As Integer Implements ICancellationCheck.IdEntityAccount
        Get
            Return CType(INDsleEntityAccount.EditValue, Integer)
        End Get
        Set(value As Integer)
            INDsleEntityAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de entidades bancarias
    ''' </summary>
    ''' <value>
    ''' The entity bank account datasource.
    ''' </value>
    Public Property EntityBankAccountDatasource As DevExpress.Xpo.XPInstantFeedbackSource Implements ICancellationCheck.EntityBankAccountDatasource
        Get
            Return CType(INDsleEntityAccount.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleEntityAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    Public ReadOnly Property MyLayoutControl As Controls.IndigoLayoutControl Implements ICancellationCheck.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    Public ReadOnly Property MyTag As Object Implements ICancellationCheck.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Variable de tipo de la entidad de cancelacion de cheques
    ''' </summary>
    Dim cancellationCheck As CancellationChecks

    ''' <summary>
    ''' Contiene la cuenta bancaria seleccionada
    ''' </summary>
    Private _entityBankAccount As EntityBankAccounts

    ''' <summary>
    ''' Variable que contiene el presentador
    ''' </summary>
    Dim _presenter As PCancellationCheck

#End Region

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _idOperativeUnit = Nothing
        _idCheckBook = Nothing
        varImp = Nothing
        cancellationCheck = Nothing
        _entityBankAccount = Nothing
        _presenter = Nothing
    End Sub
    ''' <summary>
    ''' Handles the Load event of the FrmCancellationChecks control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmCancellationChecks_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        _presenter = New PCancellationCheck(Me)
        _presenter.InitializeEntityAccount()
        _presenter.LoadDefinitionLayout()
        _idCheckBook = 0
        Deshacer()
    End Sub

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDsleEntityAccount.Enabled Then
            INDsleEntityAccount.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the INDsleEntityAccount control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleEntityAccount_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter And INDsleEntityAccount.EditValue IsNot Nothing Then
            If Not String.IsNullOrEmpty(IdEntityAccount) Then
                If Me.BarraBotones.PermiteConsultar = False Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                    Exit Sub
                End If

                BarraBotones.PrepareToolbar(eAction.OnlySave)
                cancellationCheck = New CancellationChecks()
                ActionsOnControls = True
            End If
        ElseIf e.KeyCode = Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleEntityAccount control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDsleEntityAccount_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleEntityAccount.EditValueChanged
        If Not String.IsNullOrEmpty(INDsleEntityAccount.EditValue) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Sub
            End If
            Using Model As New MEntityAccount(Me.Tag)
                _entityBankAccount = Await Model.GetEntityBankAccountById(CInt(INDsleEntityAccount.EditValue))
                If _entityBankAccount IsNot Nothing AndAlso _entityBankAccount.Id > 0 Then
                    If _entityBankAccount.Checkbooks.ToList().FindAll(Function(x) x.Status = 1).Cast(Of Checkbooks).ToList().Count > 0 Then
                        Dim checkBook As Checkbooks = _entityBankAccount.Checkbooks.ToList().FindAll(Function(x) x.Status = 1).Cast(Of Checkbooks).FirstOrDefault()
                        _idCheckBook = checkBook.Id
                        CheckNumber = checkBook.CurrentNumber
                        Dim setting As ActionResult(Of SettingsTreasury) = Nothing
                        Using ModelSetting As New MSettingsTreasury(Me.Tag)
                            setting = Await ModelSetting.GetSettingsTreasuryByIdUnitOperative(Me._idOperativeUnit)
                            If setting.StateResult = False Then
                                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmSettingsTreasury_DontExists", NAME_MODULE)
                                Exit Sub
                            Else
                                INDtxtCheckNumber.Enabled = Not setting.ObjectEmbbeded.CheckBookControl
                            End If
                        End Using
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CheckActiveNoExist", NAME_MODULE)
                        Exit Sub
                    End If
                    BarraBotones.PrepareToolbar(eAction.OnlySave)
                    cancellationCheck = New CancellationChecks()
                    ActionsOnControls = True
                    CancellationDate = Me.GetDateServer()
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the Leave event of the INDtxtCheckNumber control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDtxtCheckNumber_Leave(sender As Object, e As EventArgs) Handles INDtxtCheckNumber.Leave
        If Not String.IsNullOrEmpty(CheckNumber) Then
            Using Model As New MEntityAccount(Me.Tag)
                If _entityBankAccount IsNot Nothing AndAlso _entityBankAccount.Id > 0 Then
                    Dim checkBook As Checkbooks = _entityBankAccount.Checkbooks.Where(Function(x) x.Status = 1).Cast(Of Checkbooks).FirstOrDefault() 'chequera activa
                    If checkBook IsNot Nothing AndAlso checkBook.Id > 0 Then

                        Using ModelCheck As New MCheck(Me.Tag)
                            Dim resultOperation = Await ModelCheck.GetCheckBlockByIdCheckBookNumber(checkBook.Id, CheckNumber)
                            Dim checkBlock As CheckBlock = resultOperation.ObjectEmbbeded
                            If checkBlock IsNot Nothing AndAlso checkBlock.Id > 0 Then
                                CheckNumber = String.Empty
                                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CheckBlock", NAME_MODULE)
                                'INDtxtCheckNumber.Focus()
                                Exit Sub
                            End If
                            If Not String.IsNullOrEmpty(CheckNumber) AndAlso _entityBankAccount IsNot Nothing Then
                                Using ModelAnullation As New MCancellationCheck(Me.Tag)
                                    Dim resultConsulta = Await ModelAnullation.GetCancellationCheckByEntityAccountAndCheckNumber(_entityBankAccount.Id, CheckNumber)
                                    Dim cancellationCheck As CancellationChecks = resultConsulta.ObjectEmbbeded
                                    If cancellationCheck IsNot Nothing AndAlso cancellationCheck.Id > 0 Then
                                        CheckNumber = String.Empty
                                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CheckAnnulated", NAME_MODULE), checkBook.CurrentNumber) 'Este cheque ya se encuentra anulado"
                                        'INDtxtCheckNumber.Focus()
                                        Exit Sub
                                    End If
                                End Using
                            End If
                        End Using
                        If Not String.IsNullOrEmpty(CheckNumber) Then
                            If Convert.ToInt64(CheckNumber) > checkBook.EndNumber Or Convert.ToInt64(CheckNumber) < checkBook.CurrentNumber Then
                                CheckNumber = String.Empty
                                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CheckNumberInvalid", NAME_MODULE)
                                'INDtxtCheckNumber.Focus()
                            End If
                        End If
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CheckActiveNoExist", NAME_MODULE)
                    End If
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleEntityAccount control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleEntityAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleEntityAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmEntityAccount
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 768)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                _presenter.InitializeEntityAccount()
            End Using
        End If
    End Sub

#End Region

#Region "Methos and Functions"

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
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Sub Eliminar() Implements Base.IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        If ValidateControls() = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MCancellationCheck(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await Model.SaveCancellationCheck(Me.cancellationCheck)
                If Result.StateResult = True Then
                    If cancellationCheck.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                    ElseIf cancellationCheck.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                    Me.cancellationCheck = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    If Result.Message IsNot Nothing Then
                        generateListError(Result.Message)
                    End If
                    'If Result.MessageResult(0) = ErrorConcurrencia Then
                    '    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    'ElseIf Result.MessageResult(0) = "CC0001" Then
                    '    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("CheckActiveNoExist", NAME_MODULE)
                    'ElseIf Result.MessageResult(0) = "CC0002" Then
                    '    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("SaveCheckBookError", NAME_MODULE)
                    'Else
                    '    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    'End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDsleEntityAccount.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Genera el mensaje de error
    ''' </summary>
    ''' <param name="errors">The errors.</param>
    Private Sub generateListError(errors As String)
        Dim listError As New StringBuilder()
        listError.AppendLine(ResourceManager.GetString("ErrorListMessage"))
        listError.AppendLine(errors)
        Mensaje(EeventViewerImages.Advertencia) = listError.ToString()
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo

    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Cuenta", .FieldName = "IdEntityAccount.CodeName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4},
                              New ColumnInfo() With {.Caption = "Fecha Cancelación", .FieldName = "CancellationDate", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3},
                              New ColumnInfo() With {.Caption = "Número Cheque", .FieldName = "CheckNumber", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3}}.ToList
            .ValorSolicitado = "IdEntityAccount.Id"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAllCancellationCheck
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Devuelve el valor del OpenSearch.
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Dim CheckNum As String = FormSearchObjects.GridViewBusquedas.GetRowCellValue(FormSearchObjects.GridViewBusquedas.FocusedRowHandle, "CheckNumber")
        INDsleEntityAccount.EditValue = CInt(ReturnValue)
        If INDsleEntityAccount.EditValue IsNot Nothing Then
            Await LoadControls(CheckNum)
            If INDsleEntityAccount.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDsleEntityAccount.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Cleans the controls.
    ''' </summary>
    Private Sub CleanControls()
        INDlycRoot.BeginUpdate()
        ReadOnlyControls(False)

        ActionsOnControls = False
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me._doc = Nothing

        INDtxtCheckNumber.Text = String.Empty
        INDsleEntityAccount.EditValue = Nothing
        INDdeCancellationDate.EditValue = Nothing
        INDmeDescription.Text = String.Empty
        _idCheckBook = 0

        INDlycRoot.EndUpdate()
        _entityBankAccount = Nothing

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        End If

        Me.cancellationCheck = Nothing
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With cancellationCheck
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .IdEntityAccount = IdEntityAccount
            .CheckNumber = CheckNumber
            .IdCheckBook = _idCheckBook
            .CancellationDate = CancellationDate
            .Description = Description
        End With
    End Sub

    ''' <summary>
    ''' Loads the controls.
    ''' </summary>
    Private Async Function LoadControls(Optional ByVal NumCheck As String = "") As Task
        If Not String.IsNullOrEmpty(IdEntityAccount) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Using Model As New MCancellationCheck(Me.Tag.ToString())
                AsyncLoader(True)
                INDlycRoot.BeginUpdate()
                Dim resultOperation = Await Model.GetCancellationCheckByEntityAccountAndCheckNumber(IdEntityAccount, NumCheck)
                cancellationCheck = resultOperation.ObjectEmbbeded
                If cancellationCheck IsNot Nothing Then
                    If cancellationCheck.Id > 0 Then
                        With cancellationCheck
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                            Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            IdEntityAccount = .IdEntityAccount
                            CancellationDate = .CancellationDate
                            CheckNumber = .CheckNumber
                            Description = .Description
                        End With
                        AsyncLoader(False)
                        ActionsOnControls = True
                        ReadOnlyControls(True)
                        INDsleEntityAccount.Enabled = False
                    End If
                End If
            End Using
        End If
        INDlycRoot.EndUpdate()
    End Function
#End Region

#Region "Bar Button Event"

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar

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
        Deshacer()
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
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
        End If
    End Sub

#End Region

End Class