'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Juan Carlos Bermudez
' Created          : 27/07/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Portfolio.MVP
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Presentation.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources

#End Region

Public Class FrmPortfolioReclassification
    Implements IPortfolioReclassification, ICustomizableForm

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el codigo del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IPortfolioReclassification.Code
        Get
            If INDbtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
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
    ''' Obtiene o establece el tipo de documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentType As Byte Implements IPortfolioReclassification.DocumentType
        Get
            Return INDsleDocumentType.EditValue
        End Get
        Set(value As Byte)
            INDsleDocumentType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IPortfolioReclassification.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IPortfolioReclassification.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta de origen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SourceAccount As String Implements IPortfolioReclassification.SourceAccount
        Get
            'Return INDtxtSourceAccountId.EditValue
        End Get
        Set(value As String)
            'INDtxtSourceAccountId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado de la unidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Byte Implements IPortfolioReclassification.Status
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As Byte)
            Me.BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta de destino
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TargetAccount As String Implements IPortfolioReclassification.TargetAccount
        Get
            'Return INDtxtTargetAccountId.EditValue
        End Get
        Set(value As String)
            'INDtxtTargetAccountId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Value As Decimal Implements IPortfolioReclassification.Value
        Get
            Return INDspnValue.EditValue
        End Get
        Set(value As Decimal)
            INDspnValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta por pagar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountReceivable As String Implements IPortfolioReclassification.AccountReceivable
        Get
            'Return INDtxtAccountReceivableId.EditValue
        End Get
        Set(value As String)
            'INDtxtAccountReceivableId.EditValue = value
        End Set
    End Property

#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Buscar() Implements IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
        If _searchMode = True Then
            If indigo.UserViewMode = True Then
                Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
                'Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    ''' <summary>
    '''  METODO: Item Guardar del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Guardar() Implements IcrudBase.Guardar

    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    ''' <remarks></remarks>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
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
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        _searchMode = True
        ' DeleteBlockedRecord()
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue

        Dim listItemsColumnEdit As New List(Of Tuple(Of String, Byte))
        listItemsColumnEdit.Add(New Tuple(Of String, Byte)("Radicacion - Publico y privada", 1))
        listItemsColumnEdit.Add(New Tuple(Of String, Byte)("Devoluciones - Privada", 2))
        listItemsColumnEdit.Add(New Tuple(Of String, Byte)("Recepcion de Glosas Subsanable - Privada", 3))
        listItemsColumnEdit.Add(New Tuple(Of String, Byte)("Conciliaciones - Privada", 4))
        listItemsColumnEdit.Add(New Tuple(Of String, Byte)("Reiteracion - Privada", 5))
        listItemsColumnEdit.Add(New Tuple(Of String, Byte)("Cuentas de Dificil Recaudo", 6))

        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.33)},
                              New ColumnInfo() With {.Caption = "Factura", .FieldName = "InvoiceNumber", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.33)},
                              New ColumnInfo() With {.Caption = "Tipo Documento", .FieldName = "DocumentType", .ColumnEdit = True, .ListItemsDatasourceColumEdit = listItemsColumnEdit, .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.33)},
                              New ColumnInfo() With {.ColumnFormat = "##,##0.00", .ColumnFormatType = DevExpress.Utils.FormatType.Custom, .Caption = "Valor", .FieldName = "Value", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.33)},
                              New ColumnInfo() With {.Caption = "Moneda", .FieldName = "CurrencyAbbreviation", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.33)},
                              New ColumnInfo() With {.ColumnAligment = DevExpress.Utils.HorzAlignment.Center, .Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.33)}}.ToList
            .ValorSolicitado = "Code"

            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListPortfolioReclassification
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IPortfolioReclassification.ActionsOnControls
        Set(value As Boolean)
            INDlcPortfolioReclassification.BeginUpdate()
            INDbtnCode.Enabled = Not value
            BarraBotones.StatusRecordVisible = value
            INDGcDetail.Enabled = value
            INDlcPortfolioReclassification.EndUpdate()
            If value Then
                INDbtnCode.Focus()
            End If
        End Set
    End Property

#End Region

#Region "Globals"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Portfolio"

    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Private _indigoSession As SessionValues

    ''' <summary>
    ''' Representa el presentador 
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PPortfolioReclassification

    ''' <summary>
    ''' Representa el modelo 
    ''' </summary>
    ''' <remarks></remarks>
    Dim Model As MPortfolioReclassification

    ''' <summary>
    ''' Representa la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim PortfolioReclassification As PortfolioReclassification

    ''' <summary>
    ''' define la unidad operativa
    ''' </summary>
    ''' <remarks></remarks>
    Dim _idOperativeUnit As Integer

    ''' <summary>
    ''' Variable para controlar si el formulario esta en modo busqueda
    ''' </summary>
    Private _searchMode As Boolean

    ''' <summary>
    ''' Variable que contiene la lista de tipos de orden
    ''' </summary>
    Dim ListDocumentType As New List(Of Tuple(Of Integer, String))

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que inicializa el datasource de las tuplas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuples()
        ListDocumentType = New List(Of Tuple(Of Integer, String))
        ListDocumentType.Add(New Tuple(Of Integer, String)(1, "Radicacion"))
        ListDocumentType.Add(New Tuple(Of Integer, String)(2, "Devoluciones"))
        ListDocumentType.Add(New Tuple(Of Integer, String)(3, "Recepcion de Glosas Subsanable"))
        ListDocumentType.Add(New Tuple(Of Integer, String)(4, "Conciliaciones"))
        ListDocumentType.Add(New Tuple(Of Integer, String)(5, "Reiteracion"))
        ListDocumentType.Add(New Tuple(Of Integer, String)(6, "Cuentas de Dificil Recaudo"))
        INDsleDocumentType.Properties.DataSource = ListDocumentType.ToList
        INDsleDocumentType.Properties.Buttons(1).Visible = False
    End Sub

    ''' <summary>
    ''' Carga los estados de contrato
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "0", .StatusName = String.Empty, .StatusColor = System.Drawing.Color.White})
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateRegistered"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            INDspnValue.EditValue = ReturnObject.Total
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Limpia los controles y las variables
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlcPortfolioReclassification.BeginUpdate()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.StatusRecordVisible = False

        PortfolioReclassification = Nothing
        Code = String.Empty
        DocumentType = Nothing
        AccountReceivable = String.Empty
        SourceAccount = String.Empty
        TargetAccount = String.Empty
        Value = Nothing
        Status = 0
        Me.SetCurrencyUI(indigo.CurrencyISO4217)
        INDGcDetail.DataSource = Nothing
        IndigoGridControl1.RefreshGrid(INDGcDetail)

        ActionsOnControls = False
        INDlcPortfolioReclassification.EndUpdate()

    End Sub

    ''' <summary>
    ''' Método que carga los controles de la orden de traslado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function LoadControls() As Task
        If Me.BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Function
        End If
        Try
            Using Model As New MPortfolioReclassification(Me.Tag)
                AsyncLoader(True)
                Dim resultOperation = Await Model.GetReclassificationByCode(INDbtnCode.Text.Trim)
                INDlcPortfolioReclassification.BeginUpdate()
                PortfolioReclassification = resultOperation
                If PortfolioReclassification IsNot Nothing AndAlso PortfolioReclassification.Id > 0 Then
                    With PortfolioReclassification
                        LayoutControls.SetCustomFieldsValue(.CustomProperties)

                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)

                        Code = .Code
                        DocumentType = .DocumentType
                        AccountReceivable = .AccountReceivableDescription
                        SourceAccount = .SourceAccountDescription
                        TargetAccount = .TargetAccountDescription
                        Me.SetCurrencyUI(If(String.IsNullOrEmpty(.CurrencyAbbreviation), Me.indigo.CurrencyISO4217, .CurrencyAbbreviation))
                        Me.Status = .Status

                        IndigoGridControl1.AcceptXPO = True
                        INDGcDetail.DataSource = Model.GetPorfolioReclasificationById(.Code)

                    End With
                    ActionsOnControls = True
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "No se encontró el codigo ingresado"
                End If
            End Using
            AsyncLoader(False)
            INDlcPortfolioReclassification.EndUpdate()
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
        End Try
    End Function

    ''' <summary>
    ''' metodo que se encarga de establecer la moneda del formulario
    ''' </summary>
    ''' <param name="abbreviation"></param>
    Private Sub SetCurrencyUI(abbreviation As String)

        If String.IsNullOrEmpty(abbreviation) Then
            Exit Sub
        End If

        Me.changeNumericFormatByCurrency(abbreviation.GetNumberFormat)
        Me.GridColumn5.SummaryItem.Format = abbreviation.GetNumberFormat

    End Sub

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        Model = Nothing
        PortfolioReclassification = Nothing
        _idOperativeUnit = Nothing
        _searchMode = Nothing
        ListDocumentType = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando incia el form, carga los controles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPortfolioReclassification_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlcPortfolioReclassification, True)
        Me._doc = Nothing
        _indigoSession = SessionValues.Instance
        Presenter = New PPortfolioReclassification(Me)
        Presenter.LoadDefinitionLayout()
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        Deshacer()
        LoadStatus()
        InitializeTuples()
        _searchMode = False
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento que se dispara cuando el formulario se va a cerrar, elimina el registro bloqueado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPortfolioReclassification_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
       
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' se dispara al activarse el formulario
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

#Region "KeyDown"

    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbtnCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Not String.IsNullOrEmpty(INDbtnCode.Text.Trim()) Then
                Await Me.LoadControls()
            End If
        ElseIf e.KeyCode = Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "IdEntityLoaded"
    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    ''' 
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.PortfolioReclassification IsNot Nothing AndAlso Me.PortfolioReclassification.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("OpenFromVituelContent"), MessageType.Question, ResourceManager.GetString("OpenFromVituelTitle"), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                'DeleteBlockedRecord()
                Code = Me.IdEntity.Trim()
                LoadControls()
            End If
        Else 'Realiza la consulta normal
            Code = Me.IdEntity.Trim()
            LoadControls()
        End If
        Me.IdEntity =  String.Empty
        If INDbtnCode.Enabled = False Then
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        End If
    End Sub
#End Region

#End Region

#Region "BAR BUTTONS"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        _searchMode = False
        Deshacer()
    End Sub

#End Region

End Class