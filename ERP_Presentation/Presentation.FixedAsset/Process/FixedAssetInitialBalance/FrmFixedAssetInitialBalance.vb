'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 12/04/2016
'
' Last Modified By :
' Last Modified On :
' Description      :
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Windows.Forms
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.FixedAsset.MVP

#End Region

Public Class FrmFixedAssetInitialBalance
    Implements IFixedAssetInitialBalance, ICustomizableForm

    Public Sub New()
        InitializeComponent()
        INDEsbDetails.AddExcelSheets(New ExcelSheet With {
            .Columns = New List(Of ExcelColumn) From {
                New ExcelColumn With {.Name = "Articulo", .Comment = ""},
                New ExcelColumn With {.Name = "Serie", .Comment = ""},
                New ExcelColumn With {.Name = "Placa", .Comment = ""},
                New ExcelColumn With {.Name = "Responsable", .Comment = ""},
                New ExcelColumn With {.Name = "Valor Histórico", .Comment = ""},
                New ExcelColumn With {.Name = "Valor Razonable", .Comment = ""},
                New ExcelColumn With {.Name = "Marca", .Comment = ""},
                New ExcelColumn With {.Name = "Modelo", .Comment = ""},
                New ExcelColumn With {.Name = "Proveedor", .Comment = ""},
                New ExcelColumn With {.Name = "Localización", .Comment = ""},
                New ExcelColumn With {.Name = "Poliza", .Comment = ""},
                New ExcelColumn With {.Name = "Maneja Garantía", .Comment = ""},
                New ExcelColumn With {.Name = "Fecha Vecimiento Garantía(Solo si maneja garantía)", .Comment = ""},
                New ExcelColumn With {.Name = "Fecha Adquisición", .Comment = ""},
                New ExcelColumn With {.Name = "Tipo Adquisición", .Comment = ""},
                New ExcelColumn With {.Name = "Deprecia/Amortiza", .Comment = "Amortiza si el activo es Intangible y deprecia si es 'Activo fijo'"},
                New ExcelColumn With {.Name = "Valida Menor Cuantía", .Comment = ""},
                New ExcelColumn With {.Name = "Estado del Activo", .Comment = ""},
                New ExcelColumn With {.Name = "Libro", .Comment = ""},
                New ExcelColumn With {.Name = "Vida Útil", .Comment = ""},
                New ExcelColumn With {.Name = "Un. Vida Útil", .Comment = ""},
                New ExcelColumn With {.Name = "Tipo Depreciación", .Comment = ""},
                New ExcelColumn With {.Name = "Días Depreciados", .Comment = ""},
                New ExcelColumn With {.Name = "Valor Depreciado", .Comment = ""},
                New ExcelColumn With {.Name = "Valor Histórico del Libro", .Comment = ""}
            }
        })


    End Sub

#Region "Properties"

    ''' <summary>
    ''' Obtiene el layout
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IFixedAssetInitialBalance.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IFixedAssetInitialBalance.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Public Property Sequense As FixedAssetSequence Implements IFixedAssetInitialBalance.Sequense
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
    ''' Obtiene o establece el codigo de un concepto de notas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IFixedAssetInitialBalance.Code
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
    ''' Obtiene o establece la fecha del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As DateTime? Implements IFixedAssetInitialBalance.DocumentDate
        Get
            Return INDdteDocumentDate.EditValue
        End Get
        Set(value As DateTime?)
            INDdteDocumentDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece las observaciones
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Observations As String Implements IFixedAssetInitialBalance.Observations
        Get
            Return INDmemoObservations.EditValue
        End Get
        Set(value As String)
            INDmemoObservations.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene los parámetros de activos fijos
    ''' </summary>
    Private _SettingsFixedAsset As SettingFixedAsset
    Public Property SettingsFixedAsset As SettingFixedAsset Implements IFixedAssetInitialBalance.SettingsFixedAsset
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
    ''' Variable que contiene el objeto torre
    ''' </summary>
    Dim FixedAssetInitialBalance As FixedAssetInitialBalance

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PFixedAssetInitialBalance

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
    ''' Listado de detalles del saldo inicial
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListFixedAssetInitialBalanceItem As List(Of FixedAssetInitialBalanceItem)

    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Dim IndexEditRecord As Integer

    ''' <summary>
    ''' Variable para la entidad que se va a editar en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Dim FixedAssetInitialBalanceItem As FixedAssetInitialBalanceItem

    ''' <summary>
    ''' Listado de libros oficiales
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteFixedAssetInitialBalanceItem As List(Of FixedAssetInitialBalanceItem)

    ''' <summary>
    ''' Listado de libros oficiales
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteFixedAssetInitialBalanceItemPartsDetailBook As List(Of FixedAssetInitialBalanceItemPartsDetailBook)

    ''' <summary>
    ''' Listado de libros oficiales
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteFixedAssetInitialBalanceItemDetailBook As List(Of FixedAssetInitialBalanceItemDetailBook)

    ''' <summary>
    ''' Listado de libros oficiales
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteFixedAssetInitialBalanceItemParts As List(Of FixedAssetInitialBalanceItemParts)

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
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        Try
            If ValidateControls() = True Then
                If FixedAssetInitialBalance.Status <> 3 Then
                    If ListFixedAssetInitialBalanceItem Is Nothing OrElse ListFixedAssetInitialBalanceItem.Count = 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar al menos un articulo."
                        Exit Sub
                    End If
                    AssigningValues()
                End If
                Using model As New MFixedAssetInitialBalance(MyTag)
                    AsyncLoader(True)
                    Dim Result = Await model.SaveAccountPayable(FixedAssetInitialBalance, ListDeleteFixedAssetInitialBalanceItem, ListDeleteFixedAssetInitialBalanceItemPartsDetailBook, ListDeleteFixedAssetInitialBalanceItemDetailBook, ListDeleteFixedAssetInitialBalanceItemParts, _idCurrentSequence)
                    If Result.StateResult = True Then
                        If FixedAssetInitialBalance.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                            'Se descarta la secuencia numerica usada
                            If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                                Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                            End If
                            If FixedAssetInitialBalance.Status = 1 Then
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                            ElseIf FixedAssetInitialBalance.Status = 2 Then
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SaveConfirm"), Result.ObjectEmbbeded.Code)
                            End If
                        ElseIf FixedAssetInitialBalance.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                            If FixedAssetInitialBalance.Status = 3 Then
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                            ElseIf FixedAssetInitialBalance.Status = 2 Then
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateConfirm")
                            ElseIf FixedAssetInitialBalance.Status = 1 Then
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                            End If
                        End If
                        Me.FixedAssetInitialBalance = Result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        AsyncLoader(False)
                        Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        INDbtnCode.Enabled = False
                        If Result.MessageResult(0) = ErrorConcurrencia Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = Result.MessageResult(0)
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
    Public Async Sub Nuevo() Implements Base.IcrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewInitialBalance()
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Elimina el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RemoveDetail()
        FixedAssetInitialBalanceItem = CType(ViewGridItem.GetFocusedRow, FixedAssetInitialBalanceItem)
        ListFixedAssetInitialBalanceItem.Remove(FixedAssetInitialBalanceItem)

        If FixedAssetInitialBalanceItem.Id > 0 Then
            If ListDeleteFixedAssetInitialBalanceItem Is Nothing Then
                ListDeleteFixedAssetInitialBalanceItem = New List(Of FixedAssetInitialBalanceItem)
            End If
            ListDeleteFixedAssetInitialBalanceItem.Add(FixedAssetInitialBalanceItem)

            'Se recorren los libros del articulo, si hay para eliminarlos
            If FixedAssetInitialBalanceItem.FixedAssetInitialBalanceItemDetailBook IsNot Nothing AndAlso FixedAssetInitialBalanceItem.FixedAssetInitialBalanceItemDetailBook.Count > 0 Then
                For Each item In (From l In FixedAssetInitialBalanceItem.FixedAssetInitialBalanceItemDetailBook Where l.Id > 0 Select l).ToList
                    If ListDeleteFixedAssetInitialBalanceItemDetailBook Is Nothing Then
                        ListDeleteFixedAssetInitialBalanceItemDetailBook = New List(Of FixedAssetInitialBalanceItemDetailBook)
                    End If
                    If ListDeleteFixedAssetInitialBalanceItemDetailBook.Contains(item) Then
                        Continue For
                    End If
                    ListDeleteFixedAssetInitialBalanceItemDetailBook.Add(item)
                Next
            End If

            'Se recorren las partes y los libros de las partes, si hay para eliminarlos
            If FixedAssetInitialBalanceItem.FixedAssetInitialBalanceItemParts IsNot Nothing AndAlso FixedAssetInitialBalanceItem.FixedAssetInitialBalanceItemParts.Count > 0 Then
                For Each itemPart In (From l In FixedAssetInitialBalanceItem.FixedAssetInitialBalanceItemParts Where l.Id > 0 Select l).ToList 'Se recorre las partes

                    If itemPart.FixedAssetInitialBalanceItemPartsDetailBook IsNot Nothing AndAlso itemPart.FixedAssetInitialBalanceItemPartsDetailBook.Count > 0 Then
                        For Each itemPDB In (From l In itemPart.FixedAssetInitialBalanceItemPartsDetailBook Where l.Id > 0 Select l).ToList 'Se recorren los libros de las partes
                            If ListDeleteFixedAssetInitialBalanceItemPartsDetailBook Is Nothing Then
                                ListDeleteFixedAssetInitialBalanceItemPartsDetailBook = New List(Of FixedAssetInitialBalanceItemPartsDetailBook)
                            End If
                            If ListDeleteFixedAssetInitialBalanceItemPartsDetailBook.Contains(itemPDB) Then
                                Continue For
                            End If
                            ListDeleteFixedAssetInitialBalanceItemPartsDetailBook.Add(itemPDB)
                        Next
                    End If

                    If ListDeleteFixedAssetInitialBalanceItemParts Is Nothing Then
                        ListDeleteFixedAssetInitialBalanceItemParts = New List(Of FixedAssetInitialBalanceItemParts)
                    End If
                    If ListDeleteFixedAssetInitialBalanceItemParts.Contains(itemPart) Then
                        Continue For
                    End If
                    ListDeleteFixedAssetInitialBalanceItemParts.Add(itemPart)

                Next
            End If

        End If

        INDgcItem.DataSource = Nothing
        INDgcItem.DataSource = ListFixedAssetInitialBalanceItem
    End Sub

    ''' <summary>
    ''' Edita el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditDetail()
        FixedAssetInitialBalanceItem = DirectCast(ViewGridItem.GetFocusedRow(), FixedAssetInitialBalanceItem)
        IndexEditRecord = ListFixedAssetInitialBalanceItem.IndexOf(FixedAssetInitialBalanceItem)
        OpenFormFixedAssetInitialBalanceItem(True)
    End Sub

    ''' <summary>
    ''' Metodo que abre el form de saldo inicial articulo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenFormFixedAssetInitialBalanceItem(EditMode As Boolean)
        Using formulario As New FrmFixedAssetInitialBalanceItem
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddFixedAssetInitialBalanceItemEventArgs, AddressOf ReturnAddEventArgs
            formulario.SettingsFixedAsset = SettingsFixedAsset
            formulario.EditMode = EditMode
            formulario.ListCompare = ListFixedAssetInitialBalanceItem
            formulario.FixedAssetInitialBalanceItem = FixedAssetInitialBalanceItem
            formulario.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.8
            formulario.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.8
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
    Private Sub ReturnAddEventArgs(sender As Object, e As AddFixedAssetInitialBalanceItem)
        If e.EditMode = False Then 'Se esta ingresando un articulo
            If ListFixedAssetInitialBalanceItem Is Nothing Then
                ListFixedAssetInitialBalanceItem = New List(Of FixedAssetInitialBalanceItem)
            End If
            ListFixedAssetInitialBalanceItem.Add(e.FixedAssetInitialBalanceItem)
            Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente."
        Else 'Se esta modificando un articulo
            ListFixedAssetInitialBalanceItem.Remove(FixedAssetInitialBalanceItem)
            ListFixedAssetInitialBalanceItem.Insert(IndexEditRecord, e.FixedAssetInitialBalanceItem)
            Mensaje(EeventViewerImages.Informacion) = "Detalle modificado correctamente."
        End If

        If e.ListDeleteFixedAssetInitialBalanceItemPartsDetailBook IsNot Nothing Then
            If ListDeleteFixedAssetInitialBalanceItemPartsDetailBook Is Nothing Then
                ListDeleteFixedAssetInitialBalanceItemPartsDetailBook = New List(Of FixedAssetInitialBalanceItemPartsDetailBook)
            End If
            ListDeleteFixedAssetInitialBalanceItemPartsDetailBook.AddRange(e.ListDeleteFixedAssetInitialBalanceItemPartsDetailBook)
        End If

        If e.ListDeleteFixedAssetInitialBalanceItemDetailBook IsNot Nothing Then
            If ListDeleteFixedAssetInitialBalanceItemDetailBook Is Nothing Then
                ListDeleteFixedAssetInitialBalanceItemDetailBook = New List(Of FixedAssetInitialBalanceItemDetailBook)
            End If
            ListDeleteFixedAssetInitialBalanceItemDetailBook.AddRange(e.ListDeleteFixedAssetInitialBalanceItemDetailBook)
        End If

        If e.ListDeleteFixedAssetInitialBalanceItemParts IsNot Nothing Then
            If ListDeleteFixedAssetInitialBalanceItemParts Is Nothing Then
                ListDeleteFixedAssetInitialBalanceItemParts = New List(Of FixedAssetInitialBalanceItemParts)
            End If
            ListDeleteFixedAssetInitialBalanceItemParts.AddRange(e.ListDeleteFixedAssetInitialBalanceItemParts)
        End If

        INDgcItem.DataSource = Nothing
        INDgcItem.DataSource = ListFixedAssetInitialBalanceItem
    End Sub

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        'Me.ViewModeEditHold = True
        If Me.FixedAssetInitialBalance IsNot Nothing AndAlso Me.FixedAssetInitialBalance.Id > 0 Then
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
    Public WriteOnly Property ActionsOnControls As Boolean Implements IFixedAssetInitialBalance.ActionsOnControls
        Set(value As Boolean)
            INDlyInitialBalance.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDdteDocumentDate.Enabled = value
            INDmemoObservations.Enabled = value
            INDbtnAddItem.Enabled = value
            INDgcItem.Enabled = value
            INDEsbDetails.Enabled = value
            INDlyInitialBalance.EndUpdate()
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
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4)},
                              New ColumnInfo() With {.Caption = "Fecha Documento", .FieldName = "DocumentDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetInitialBalance
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
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.FixedAssetInitialBalance.Code, Me.FixedAssetInitialBalance.DocumentDate),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.FixedAssetInitialBalance.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.FixedAssetInitialBalance.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.FixedAssetInitialBalance.Code, Me.FixedAssetInitialBalance.DocumentDate)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.FixedAssetInitialBalance.Code)
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
        INDlyInitialBalance.BeginUpdate()
        ReadOnlyControls(False)
        ActionsOnControls = False
        Code = String.Empty
        DocumentDate = Nothing
        Observations = Nothing
        ListFixedAssetInitialBalanceItem = Nothing
        ListDeleteFixedAssetInitialBalanceItem = Nothing
        ListDeleteFixedAssetInitialBalanceItemDetailBook = Nothing
        ListDeleteFixedAssetInitialBalanceItemParts = Nothing
        ListDeleteFixedAssetInitialBalanceItemPartsDetailBook = Nothing
        INDgcItem.DataSource = Nothing
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        INDlyInitialBalance.EndUpdate()

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
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With FixedAssetInitialBalance
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .DocumentDate = DocumentDate
            .Observations = Observations
            .OperatingUnitId = _idOperativeUnit

            If ListFixedAssetInitialBalanceItem IsNot Nothing AndAlso ListFixedAssetInitialBalanceItem.Count > 0 Then
                .FixedAssetInitialBalanceItem.Clear()
                ListFixedAssetInitialBalanceItem.ForEach(Sub(item)
                                                             .FixedAssetInitialBalanceItem.Add(item)
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
                Me.BarraBotones.StatusRecordVisible = True
                Using Model As New MFixedAssetInitialBalance(CStr(Me.Tag))
                    AsyncLoader(True)
                    FixedAssetInitialBalance = (Await Model.GetFixedAssetInitialBalance(INDbtnCode.Text.Trim)).ObjectEmbbeded
                    INDlyInitialBalance.BeginUpdate()
                    If FixedAssetInitialBalance IsNot Nothing AndAlso FixedAssetInitialBalance.Id > 0 Then
                        Using ModelRecord As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(FixedAssetInitialBalance.Id))
                            With FixedAssetInitialBalance
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)

                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                Code = .Code
                                DocumentDate = .DocumentDate
                                Observations = .Observations
                                BarraBotones.StatusRecord = .Status.ToString

                                ListFixedAssetInitialBalanceItem = .FixedAssetInitialBalanceItem.ToList
                                INDgcItem.DataSource = Nothing
                                INDgcItem.DataSource = ListFixedAssetInitialBalanceItem

                            End With
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.FixedAssetInitialBalance.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordFixedAsset With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = FixedAssetInitialBalance.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            Me.BarraBotones.SetDocuments(FixedAssetInitialBalance.Id, Me.Tag.ToString(), Nothing, GetType(FixedAssetInitialBalance).Name)
                            AsyncLoader(False)
                            ActionsOnControls = True
                            If FixedAssetInitialBalance.Status = 1 Then
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                            Else
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                ReadOnlyControls(True)
                            End If
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewInitialBalance()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbtnCode.Focus()
                        End If
                    End If
                    INDlyInitialBalance.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewInitialBalance() As Task
        Me.FixedAssetInitialBalance = New FixedAssetInitialBalance()
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
    End Function

    ''' <summary>
    ''' Pega los datos enviados al sp a la rejilla
    ''' </summary>
    ''' <returns></returns>
    Private Async Function PasteToGrid(data As List(Of List(Of String))) As Task
        ViewGridItem.ShowLoadingPanel()
        Using model As New MFixedAssetInitialBalance(MyTag)
            Dim result = Await model.SP_CopyAndPasteFixedAssetInitialBalance(data)
            If result.StateResult = False Then
                Mensaje(EeventViewerImages.MensajeError) = result.Message
                Me.Cursor = System.Windows.Forms.Cursors.Default
                ViewGridItem.HideLoadingPanel()
                Exit Function
            End If
            If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                If ListFixedAssetInitialBalanceItem Is Nothing Then
                    ListFixedAssetInitialBalanceItem = New List(Of FixedAssetInitialBalanceItem)
                End If

                If result.ObjectEmbbededAux Is Nothing Then
                    result.ObjectEmbbededAux = New List(Of Tuple(Of String, Integer))
                End If

                Dim listValidateResults As New List(Of FixedAssetInitialBalanceItem)

                For Each fapa In result.ObjectEmbbeded
                    If ListFixedAssetInitialBalanceItem.Any(Function(l) l.Plate = fapa.Plate) Then
                        result.ObjectEmbbededAux.Add(New Tuple(Of String, Integer)(String.Format("El activo con placa {0} ya se encuentra agregado", fapa.Plate), 2))
                    ElseIf listValidateResults.Any(Function(l) l.Plate = fapa.Plate) Then
                        result.ObjectEmbbededAux.Add(New Tuple(Of String, Integer)(String.Format("El activo con placa {0} se encuentra duplicada en el listado con diferentes datos", fapa.Plate), 2))
                    Else
                        listValidateResults.Add(fapa)
                    End If
                Next

                ListFixedAssetInitialBalanceItem.AddRange(listValidateResults)
            End If
            If result.ObjectEmbbededAux IsNot Nothing AndAlso result.ObjectEmbbededAux.Count > 0 Then
                Using formulario As New FrmListErrors(result.ObjectEmbbededAux)
                    formulario.StartPosition = FormStartPosition.CenterParent
                    Dim transparent As New FrmTransparent(formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog(Me)
                End Using
            End If
        End Using
        INDgcItem.DataSource = Nothing
        INDgcItem.DataSource = ListFixedAssetInitialBalanceItem
        Me.Cursor = System.Windows.Forms.Cursors.Default
        ViewGridItem.HideLoadingPanel()
    End Function

#End Region

#Region "Events"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        FixedAssetInitialBalance = Nothing
        Presenter = Nothing
        record = Nothing
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        ListFixedAssetInitialBalanceItem = Nothing
        IndexEditRecord = Nothing
        FixedAssetInitialBalanceItem = Nothing
        ListDeleteFixedAssetInitialBalanceItem = Nothing
        ListDeleteFixedAssetInitialBalanceItemPartsDetailBook = Nothing
        ListDeleteFixedAssetInitialBalanceItemDetailBook = Nothing
        ListDeleteFixedAssetInitialBalanceItemParts = Nothing
    End Sub

    ''' <summary>
    ''' Evento Load donde se ejecutan el asincrono para levantar la definicion del layout
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmFixedAssetInitialBalance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyInitialBalance, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PFixedAssetInitialBalance(Me)
        Presenter.GetSettingFixedAssetByOperatingUnitId(_idOperativeUnit)
        IndigoGridControl1.RefreshGrid(INDgcItem)
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(ViewGridItem, ListActions)
        Presenter.GetSequense()
        Presenter.LoadDefinitionLayout()
        LoadStatus()
        Deshacer()
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmFixedAssetInitialBalance_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
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
                    Await Me.NewInitialBalance()
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
    Private Sub INDbtnAddItem_Click(sender As Object, e As EventArgs) Handles INDbtnAddItem.Click
        FixedAssetInitialBalanceItem = Nothing
        OpenFormFixedAssetInitialBalanceItem(False)
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

#Region "CopyPaste"

    ''' <summary>
    ''' Funcionalidad de copiar y pegar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        Await PasteToGrid(e.Rows)
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
        FixedAssetInitialBalance.Status = 1
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
        FixedAssetInitialBalance.Status = 1
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        FixedAssetInitialBalance.Status = 2
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        FixedAssetInitialBalance.Status = 2
        Guardar()
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        FixedAssetInitialBalance.Status = 3
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