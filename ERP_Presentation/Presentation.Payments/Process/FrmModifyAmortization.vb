'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 12/03/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ComponentModel
Imports DevExpress.Data.Linq
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.Maintenance
Imports Presentation.MedicalFees.MVP
Imports Presentation.Payments.MVP

#End Region

Public Class FrmModifyAmortization
    Implements IModifyAmortization

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el id de la cxp
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountPayableId As Integer? Implements IModifyAmortization.AccountPayableId
        Get
            Return INDsleAccountPayable.EditValue
        End Get
        Set(value As Integer?)
            INDsleAccountPayable.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la cxp
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountPayableXpo As LinqInstantFeedbackSource Implements IModifyAmortization.AccountPayableXpo
        Get
            Return INDsleAccountPayable.Properties.DataSource
        End Get
        Set(value As LinqInstantFeedbackSource)
            INDsleAccountPayable.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IModifyAmortization.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property MyTag As Object Implements IModifyAmortization.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del proveedor linea de distribucion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SupplierId As Integer? Implements IModifyAmortization.SupplierId
        Get
            Return INDsleSupplier.EditValue
        End Get
        Set(value As Integer?)
            INDsleSupplier.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del proveedor linea de distribucion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SupplierXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IModifyAmortization.SupplierXpo
        Get
            Return INDsleSupplier.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleSupplier.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Presentador de modificacion de amortizaciones
    ''' </summary>
    Dim Presenter As PModifyAmortization

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordPayments

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Payments"

    ''' <summary>
    ''' Representa el listado de las cuotas que tiene asociado la cxp
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeferredCausationShare As List(Of DeferredCausationShare)

    ''' <summary>
    ''' Valor total pendiente por amortizar
    ''' </summary>
    ''' <remarks></remarks>
    Dim deferredBalance As Decimal

    ''' <summary>
    ''' Valor que se carga cuando el repositorio de valor
    ''' de la rejilla recibe el foco, y sirve para comparar
    ''' los valores de la rejilla.
    ''' </summary>
    ''' <remarks></remarks>
    Dim valueCompare As Decimal

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
        BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = False
        INDsleSupplier.Focus()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Sub Eliminar() Implements Base.ICrudBase.Eliminar
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        Else
            If ListDeferredCausationShare Is Nothing OrElse ListDeferredCausationShare.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No hay cuotas de causación diferida en la rejilla."
                Exit Sub
            End If
            Dim deferredTotal = ListDeferredCausationShare.Sum(Function(d) d.Value)
            If deferredBalance <> deferredTotal Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format("El total a amortizar ({0}) no puede ser diferente del saldo por amortizar ({1})", deferredTotal.ToString("C2"), deferredBalance.ToString("C2"))
                Exit Sub
            End If
        End If
        Dim ListSaveDeferredCausationShare As List(Of DeferredCausationShare) = AssigningValues()
        If ListSaveDeferredCausationShare Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "No se ha modificado ninguna cuota de causación."
            Exit Sub
        End If
        Try
            Using model As New MModifyAmortization(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await model.SaveListDeferredCausationShare(ListSaveDeferredCausationShare)
                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    If Result.MessageResult Is Nothing Then
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    ElseIf Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.ICrudBase.Nuevo
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga la informacion de las cuotas que tiene asociado la cxp en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadInformationGrid() As Task
        Using model As New MModifyAmortization(Tag)
            deferredBalance = 0
            ListDeferredCausationShare = Nothing
            INDgcAmortizations.DataSource = Nothing
            Dim resultListShares As ActionResult(Of List(Of DeferredCausationShare)) = Await model.GetDeferredCausationShareByAccountPayableId(AccountPayableId)
            If resultListShares.StateResult Then
                ListDeferredCausationShare = resultListShares.ObjectEmbbeded
                deferredBalance = ListDeferredCausationShare.Sum(Function(d) d.Value)
                Dim Abbreviation As String
                Using ModelM As New MGlosaMedicalFees(CStr(Me.Tag))
                    Abbreviation = ModelM.GetAccountPayableById(AccountPayableId)?.Currency?.Abbreviation
                End Using
                Me.INDValueCol = Window.Utils.FormatGrid(INDValueCol, Abbreviation)
            End If
            INDgcAmortizations.DataSource = ListDeferredCausationShare
        End Using
    End Function

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
    End Sub

    ''' <summary>
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IModifyAmortization.ActionsOnControls
        Set(value As Boolean)
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
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Origen", .FieldName = "FilingUnitSourceId.CodeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Destino", .FieldName = "FilingUnitTargetId.CodeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAccountPayableTransfer
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
    End Sub

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
        INDlyModifyAmortization.BeginUpdate()
        ActionsOnControls = False
        SupplierId = Nothing
        AccountPayableId = Nothing
        ListDeferredCausationShare = Nothing
        INDgcAmortizations.DataSource = Nothing
        BarraBotones.CleanAuditBasic()
        INDlyModifyAmortization.EndUpdate()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Function AssigningValues() As List(Of DeferredCausationShare)
        Dim ListSaveDeferredCausationShare As List(Of DeferredCausationShare) = ListDeferredCausationShare.FindAll(Function(item) item.ChangeTracker.State = ObjectState.Modified)
        If ListSaveDeferredCausationShare IsNot Nothing AndAlso ListSaveDeferredCausationShare.Count > 0 Then
            Return ListSaveDeferredCausationShare
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        Using Model As New MBlockRecordAndSequensePayments(CStr(Me.Tag))
            If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End If
        End Using
    End Function

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        record = Nothing
        _idOperativeUnit = Nothing
        ListDeferredCausationShare = Nothing
        valueCompare = Nothing
    End Sub
    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmModifyAmortization_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyModifyAmortization, True)
        Me.indigo = SessionValues.Instance
        Presenter = New PModifyAmortization(Me)
        Deshacer()
        IndigoGridControl1.RefreshGrid(INDgcAmortizations)
        BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = False
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmModifyAmortization_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Await DeleteBlockedRecord()
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' Evento que se dispara al activarse el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmModifyAmortization_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDsleSupplier.Enabled Then
            INDsleSupplier.Focus()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario de proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSupplier_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleSupplier.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmSupplier With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeSupplier()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario de cxp
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAccountPayable_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAccountPayable.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmAccountsPayable With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            AccountPayableXpo = Nothing
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de cxp
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDsleAccountPayable_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAccountPayable.EditValueChanged
        If AccountPayableId IsNot Nothing Then
            Await LoadInformationGrid()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSupplier_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleSupplier.EditValueChanged
        If SupplierId IsNot Nothing Then
            AccountPayableId = Nothing
            AccountPayableXpo = Nothing
            ListDeferredCausationShare = Nothing
            INDgcAmortizations.DataSource = Nothing
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSupplier_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleSupplier.QueryPopUp
        If SupplierXpo Is Nothing Then
            Presenter.InitializeSupplier()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de cxp
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAccountPayable_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAccountPayable.QueryPopUp
        If AccountPayableXpo Is Nothing AndAlso SupplierId IsNot Nothing Then
            Using model As New MModifyAmortization(Tag)
                AccountPayableXpo = model.ListAccountPayableByIdSupplierAndStateWithDeferredCausation(SupplierId)
            End Using
        End If
    End Sub

#End Region

#Region "CustomColumnDisplayText"

    Private Sub viewAmortizations_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles viewAmortizations.CustomColumnDisplayText
        If e.Column.FieldName = "PaymentMonth" Then
            Select Case CInt(e.Value)
                Case 1
                    e.DisplayText = "Enero"
                Case 2
                    e.DisplayText = "Febrero"
                Case 3
                    e.DisplayText = "Marzo"
                Case 4
                    e.DisplayText = "Abril"
                Case 5
                    e.DisplayText = "Mayo"
                Case 6
                    e.DisplayText = "Junio"
                Case 7
                    e.DisplayText = "Julio"
                Case 8
                    e.DisplayText = "Agosto"
                Case 9
                    e.DisplayText = "Septiembre"
                Case 10
                    e.DisplayText = "Octubre"
                Case 11
                    e.DisplayText = "Noviembre"
                Case 12
                    e.DisplayText = "Diciembre"
                Case Else
                    e.DisplayText = String.Empty
            End Select
        End If
    End Sub

#End Region

#Region "Leave"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del repositorio valor de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepTxtValue_Leave(sender As Object, e As EventArgs) Handles INDrepTxtValue.Leave
        Dim _deferredCausationShare As DeferredCausationShare = CType(viewAmortizations.GetFocusedRow, DeferredCausationShare)
        If _deferredCausationShare IsNot Nothing Then
            If _deferredCausationShare.Value <> valueCompare Then
                _deferredCausationShare.MarkAsModified()
            Else
                _deferredCausationShare.MarkAsUnchanged()
            End If
        End If
    End Sub

#End Region

#Region "Enter"

    ''' <summary>
    ''' Evento que se dispara al recibir el foco en el repositorio de valor de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepTxtValue_Enter(sender As Object, e As EventArgs) Handles INDrepTxtValue.Enter
        Dim _deferredCausationShare As DeferredCausationShare = CType(viewAmortizations.GetFocusedRow, DeferredCausationShare)
        If _deferredCausationShare IsNot Nothing AndAlso _deferredCausationShare.ChangeTracker.State = ObjectState.Unchanged Then
            valueCompare = _deferredCausationShare.Value
        End If
    End Sub

#End Region

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
        Deshacer()
        Me.Nuevo()
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir de la barra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir

    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
    End Sub

    ''' <summary>
    ''' Barras the botones_ActualizarConfirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_Anular
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular

    End Sub

#End Region

End Class