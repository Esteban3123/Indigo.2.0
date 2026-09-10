'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 16/05/2016
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
Imports DevExpress.Xpo
Imports System.Drawing
Imports Presentation.Maintenance
Imports Presentation.Base.BaseClass
Imports System.Text
Imports Presentation.Payments.MVP
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Presentation.Inventory
Imports System.ComponentModel
#End Region

Public Class FrmFixedAssetTransfer
    Implements IFixedAssetTransfer, ICustomizableForm

#Region "Builder"

    Public Sub New()
        InitializeComponent()
        AddHandler bwLoadSearch.DoWork, AddressOf bwLoadSearch_DoWork
        AddHandler bwLoadSearch.RunWorkerCompleted, AddressOf bwLoadSearch_RunWorkerCompleted
    End Sub

#End Region

#Region "BackgroundWorker"

    ''' <summary>
    ''' Listado de localizaciones
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListXpo As XPCollection

    ''' <summary>
    ''' Inicia el backgroundWorker
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bwLoadSearch_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        ListXpo = Presenter.InitializeLocation()
    End Sub

    ''' <summary>
    ''' Termina el backgroundWorker
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bwLoadSearch_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        'SourceLocationXpo = ListXpo
        'TargetLocationXpo = ListXpo
    End Sub

#End Region

#Region "Properties"

    ''' <summary>
    ''' Código
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IFixedAssetTransfer.Code
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
    ''' Fecha documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As Date? Implements IFixedAssetTransfer.DocumentDate
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
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IFixedAssetTransfer.MyLayoutControl
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
    Public ReadOnly Property MyTag As Object Implements IFixedAssetTransfer.MyTag
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
    Public Property Observation As String Implements IFixedAssetTransfer.Observation
        Get
            Return INDmemoObservations.EditValue
        End Get
        Set(value As String)
            INDmemoObservations.EditValue = value
        End Set
    End Property

    Public Property Sequense As FixedAssetSequence Implements IFixedAssetTransfer.Sequense
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
    ''' Localización origen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SourceLocationId As Integer? Implements IFixedAssetTransfer.SourceLocationId
        Get
            Return INDsleSourceLocation.EditValue
        End Get
        Set(value As Integer?)
            INDsleSourceLocation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Responsable origen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SourceResponsibleId As Integer? Implements IFixedAssetTransfer.SourceResponsibleId
        Get
            Return INDsleSourceResponsible.EditValue
        End Get
        Set(value As Integer?)
            INDsleSourceResponsible.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Localización destino
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TargetLocationId As Integer? Implements IFixedAssetTransfer.TargetLocationId
        Get
            Return INDsleTargetLocation.EditValue
        End Get
        Set(value As Integer?)
            INDsleTargetLocation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Responsable destino
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TargetResponsibleId As Integer? Implements IFixedAssetTransfer.TargetResponsibleId
        Get
            Return INDsleTargetResponsible.EditValue
        End Get
        Set(value As Integer?)
            INDsleTargetResponsible.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Tipo traslado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TransferType As Integer? Implements IFixedAssetTransfer.TransferType
        Get
            Return INDsleTransferType.EditValue
        End Get
        Set(value As Integer?)
            INDsleTransferType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource localizacion origen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SourceLocationXpo As XPInstantFeedbackSource Implements IFixedAssetTransfer.SourceLocationXpo
        Get
            Return INDsleSourceLocation.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleSourceLocation.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource responsable origen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SourceResponsibleXpo As XPInstantFeedbackSource Implements IFixedAssetTransfer.SourceResponsibleXpo
        Get
            Return INDsleSourceResponsible.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleSourceResponsible.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource localizacion destino
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TargetLocationXpo As XPInstantFeedbackSource Implements IFixedAssetTransfer.TargetLocationXpo
        Get
            Return INDsleTargetLocation.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleTargetLocation.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource responsable destino
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TargetResponsibleXpo As XPInstantFeedbackSource Implements IFixedAssetTransfer.TargetResponsibleXpo
        Get
            Return INDsleTargetResponsible.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleTargetResponsible.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Variable que contiene el objeto torre
    ''' </summary>
    Dim FixedAssetTransfer As FixedAssetTransfer

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PFixedAssetTransfer

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
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListTransferType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer

    ''' <summary>
    ''' Listado de detalles
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListFixedAssetTransferDetail As List(Of FixedAssetTransferDetail)

    ''' <summary>
    ''' Listado de eliminados
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteFixedAssetTransferDetail As List(Of FixedAssetTransferDetail)

    ''' <summary>
    ''' Asyncrono para cargar los controles de localizacion
    ''' </summary>
    ''' <remarks></remarks>
    Private bwLoadSearch As BackgroundWorker = New BackgroundWorker

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
                If FixedAssetTransfer.Status <> 3 Then
                    If ListFixedAssetTransferDetail Is Nothing OrElse ListFixedAssetTransferDetail.Count = 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar al menos un activo."
                        Exit Sub
                    End If
                    AssigningValues()
                End If
                Using model As New MFixedAssetTransfer(MyTag)
                    AsyncLoader(True)
                    Dim Result = Await model.SaveFixedAssetTransfer(FixedAssetTransfer, _idCurrentSequence)
                    If Result.StateResult = True Then
                        If FixedAssetTransfer.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                            'Se descarta la secuencia numerica usada
                            If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                                Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                            End If
                            If FixedAssetTransfer.Status = 1 Then
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                            ElseIf FixedAssetTransfer.Status = 2 Then
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SaveConfirm"), Result.ObjectEmbbeded.Code)
                            End If
                        ElseIf FixedAssetTransfer.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                            If FixedAssetTransfer.Status = 3 Then
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                            ElseIf FixedAssetTransfer.Status = 2 Then
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateConfirm")
                            ElseIf FixedAssetTransfer.Status = 1 Then
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                            End If
                        End If
                        Me.FixedAssetTransfer = Result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                        Select Case varImp
                            Case 1
                                Me.BarraBotones.PrintReport(PrintReportAction.Create, FixedAssetTransfer.Id, 0, FixedAssetTransfer.Id, _idOperativeUnit)
                            Case 2
                                Me.BarraBotones.PrintReport(PrintReportAction.Update, FixedAssetTransfer.Id, 0, FixedAssetTransfer.Id, _idOperativeUnit)
                            Case 3
                                Me.BarraBotones.PrintReport(PrintReportAction.Confirm, FixedAssetTransfer.Id, 0, FixedAssetTransfer.Id, _idOperativeUnit)
                            Case 4
                                Me.BarraBotones.PrintReport(PrintReportAction.Cancel, FixedAssetTransfer.Id, 0, FixedAssetTransfer.Id, _idOperativeUnit)
                        End Select

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
    Public Async Sub Nuevo() Implements Base.IcrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewFixedAssetTransfer()
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Confirmar()
        Try
            If ValidateControls() = True Then
                If FixedAssetTransfer.Status <> 3 Then
                    If ListFixedAssetTransferDetail Is Nothing OrElse ListFixedAssetTransferDetail.Count = 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar al menos un activo."
                        Exit Sub
                    End If
                    AssigningValues()
                End If
                Using model As New MFixedAssetTransfer(MyTag)
                    AsyncLoader(True)
                    Dim Result = Await model.ConfirmFixedAssetTransfer(FixedAssetTransfer, _idCurrentSequence)
                    If Result.StatusCode = eStatusResult.SUCCESS Then
                        Mensaje(EeventViewerImages.Informacion) = Result.Message
                        AsyncLoader(False)
                        Me.FixedAssetTransfer = Result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                        Select Case varImp
                            Case 1
                                Me.BarraBotones.PrintReport(PrintReportAction.Create, FixedAssetTransfer.Id, 0, FixedAssetTransfer.Id, _idOperativeUnit)
                            Case 2
                                Me.BarraBotones.PrintReport(PrintReportAction.Update, FixedAssetTransfer.Id, 0, FixedAssetTransfer.Id, _idOperativeUnit)
                            Case 3
                                Me.BarraBotones.PrintReport(PrintReportAction.Confirm, FixedAssetTransfer.Id, 0, FixedAssetTransfer.Id, _idOperativeUnit)
                            Case 4
                                Me.BarraBotones.PrintReport(PrintReportAction.Cancel, FixedAssetTransfer.Id, 0, FixedAssetTransfer.Id, _idOperativeUnit)
                        End Select

                        AsyncLoader(False)
                        Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        INDbtnCode.Enabled = False
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
    ''' Inicializa los search que van quemados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuple()
        'Tipo de adquisición
        ListTransferType = New List(Of Tuple(Of Integer, String))
        ListTransferType.Add(New Tuple(Of Integer, String)(1, "Localización"))
        ListTransferType.Add(New Tuple(Of Integer, String)(2, "Responsable"))
        ListTransferType.Add(New Tuple(Of Integer, String)(3, "Localización y Responsable"))
        INDsleTransferType.Properties.DataSource = ListTransferType.ToList
    End Sub

    ''' <summary>
    ''' Elimina el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RemoveDetail()
        Dim FixedAssetTransferDetail As FixedAssetTransferDetail = INDviewAssets.GetFocusedRow()
        If FixedAssetTransferDetail.Id > 0 Then
            If ListDeleteFixedAssetTransferDetail Is Nothing Then
                ListDeleteFixedAssetTransferDetail = New List(Of FixedAssetTransferDetail)
            End If
            FixedAssetTransferDetail.MarkAsDeleted()
            ListDeleteFixedAssetTransferDetail.Add(FixedAssetTransferDetail)
        End If
        ListFixedAssetTransferDetail.Remove(FixedAssetTransferDetail)
        INDgcAssets.DataSource = Nothing
        INDgcAssets.DataSource = ListFixedAssetTransferDetail
        If ListFixedAssetTransferDetail.Count = 0 Then
            INDsleTransferType.Properties.ReadOnly = False
            INDsleSourceLocation.Properties.ReadOnly = False
            INDsleSourceResponsible.Properties.ReadOnly = False
        End If
    End Sub

    ''' <summary>
    ''' Metodo que abre el form popup donde se escoge los activos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenFormAddAssets()
        If TransferType Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un tipo de traslado"
            Exit Sub
        End If
        Dim errors As New StringBuilder
        Select Case TransferType
            Case 1 'Localización
                If SourceLocationId Is Nothing Then
                    errors.AppendLine("Debe seleccionar una localización origen")
                End If
            Case 2 'Responsable
                If SourceResponsibleId Is Nothing Then
                    errors.AppendLine("Debe seleccionar un responsable origen")
                End If
            Case 3 'Localización y Responsable
                If SourceLocationId Is Nothing Then
                    errors.AppendLine("Debe seleccionar una localización origen")
                End If
                If SourceResponsibleId Is Nothing Then
                    errors.AppendLine("Debe seleccionar un responsable origen")
                End If
        End Select
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString
            Exit Sub
        End If
        Using formulario As New FrmAddAssets
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddAssetsEventArgs, AddressOf ReturnAddEventArgs
            formulario.ToolBar.Visible = False
            formulario.TransferType = TransferType
            formulario.SourceLocationId = SourceLocationId
            formulario.SourceResponsibleId = SourceResponsibleId
            formulario.ListCompare = ListFixedAssetTransferDetail
            formulario.Size = New Drawing.Size(784, 535)
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
    Private Sub ReturnAddEventArgs(sender As Object, e As AddAssets)
        If e IsNot Nothing Then
            If ListFixedAssetTransferDetail Is Nothing Then
                ListFixedAssetTransferDetail = New List(Of FixedAssetTransferDetail)
            End If
            ListFixedAssetTransferDetail.AddRange(e.ListFixedAssetTransferDetail)
            INDgcAssets.DataSource = Nothing
            INDgcAssets.DataSource = ListFixedAssetTransferDetail

            'Bloqueo los controles necesarios
            INDsleTransferType.Properties.ReadOnly = True
            INDsleSourceLocation.Properties.ReadOnly = True
            INDsleSourceResponsible.Properties.ReadOnly = True
        End If
    End Sub

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        'Me.ViewModeEditHold = True
        If Me.FixedAssetTransfer IsNot Nothing AndAlso Me.FixedAssetTransfer.Id > 0 Then
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
    Public WriteOnly Property ActionsOnControls As Boolean Implements IFixedAssetTransfer.ActionsOnControls
        Set(value As Boolean)
            INDlyTransfer.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDdteDocumentDate.Enabled = value
            INDmemoObservations.Enabled = value
            INDsleTransferType.Enabled = value
            INDsleSourceLocation.Enabled = value
            INDsleSourceResponsible.Enabled = value
            INDsleTargetLocation.Enabled = value
            INDsleTargetResponsible.Enabled = value
            INDbtnAddAssets.Enabled = value
            INDgcAssets.Enabled = value
            INDlyTransfer.EndUpdate()
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
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.125)},
                              New ColumnInfo() With {.Caption = "Fecha Documento", .FieldName = "DocumentDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.125)},
                              New ColumnInfo() With {.Caption = "Tipo Traslado", .FieldName = "TransferTypeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.125)},
                              New ColumnInfo() With {.Caption = "Responsable Origen", .FieldName = "SourceResponsibleId.CodeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.125)},
                              New ColumnInfo() With {.Caption = "Responsable Destino", .FieldName = "TargetResponsibleId.CodeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.125)},
                              New ColumnInfo() With {.Caption = "Localización Origen", .FieldName = "SourceLocationId.CodeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.125)},
                              New ColumnInfo() With {.Caption = "Localización Destino", .FieldName = "TargetLocationId.CodeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.125)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.125)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetTransfer
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
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.FixedAssetTransfer.Code, FixedAssetTransfer.DocumentDate),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.FixedAssetTransfer.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.FixedAssetTransfer.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.FixedAssetTransfer.Code, FixedAssetTransfer.DocumentDate)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.FixedAssetTransfer.Code)
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
        INDlyTransfer.BeginUpdate()
        ReadOnlyControls(False)
        ActionsOnControls = False
        Code = String.Empty
        DocumentDate = Nothing
        Observation = Nothing
        TransferType = Nothing
        SourceLocationId = Nothing
        INDsleSourceLocation.Properties.NullText = String.Empty
        SourceResponsibleId = Nothing
        INDsleSourceResponsible.Properties.NullText = String.Empty
        TargetLocationId = Nothing
        INDsleTargetLocation.Properties.NullText = String.Empty
        TargetResponsibleId = Nothing
        INDsleTargetResponsible.Properties.NullText = String.Empty
        INDgcAssets.DataSource = Nothing
        ListFixedAssetTransferDetail = Nothing
        ListDeleteFixedAssetTransferDetail = Nothing
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        INDlyItemSourceLocation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemSourceLocation.AllowHide = True
        INDlyItemSourceResponsible.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemSourceResponsible.AllowHide = True
        INDlyItemTargetLocation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemTargetLocation.AllowHide = True
        INDlyItemTargetResponsible.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemTargetResponsible.AllowHide = True
        FixedAssetTransfer = Nothing
        INDsleTransferType.Properties.ReadOnly = False
        INDsleSourceLocation.Properties.ReadOnly = False
        INDsleSourceResponsible.Properties.ReadOnly = False
        INDlyTransfer.EndUpdate()

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
        With FixedAssetTransfer
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .DocumentDate = DocumentDate
            .TransferType = TransferType
            If INDlyItemSourceLocation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .SourceLocationId = SourceLocationId
            Else
                .SourceLocationId = Nothing
            End If
            If INDlyItemSourceResponsible.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .SourceResponsibleId = SourceResponsibleId
            Else
                .SourceResponsibleId = Nothing
            End If
            If INDlyItemTargetLocation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .TargetLocationId = TargetLocationId
            Else
                .TargetLocationId = Nothing
            End If
            If INDlyItemTargetResponsible.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .TargetResponsibleId = TargetResponsibleId
            Else
                .TargetResponsibleId = Nothing
            End If
            .Observation = Observation
            .OperatingUnitId = Me.BarraBotones.OperatingUnit.Id

            .FixedAssetTransferDetail.Clear()
            If ListFixedAssetTransferDetail IsNot Nothing AndAlso ListFixedAssetTransferDetail.Count > 0 Then
                ListFixedAssetTransferDetail.ForEach(Sub(item)
                                                         .FixedAssetTransferDetail.Add(item)
                                                     End Sub)
            End If

            If ListDeleteFixedAssetTransferDetail IsNot Nothing AndAlso ListDeleteFixedAssetTransferDetail.Count > 0 Then
                ListDeleteFixedAssetTransferDetail.ForEach(Sub(item)
                                                               .FixedAssetTransferDetail.Add(item)
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
                Using Model As New MFixedAssetTransfer(CStr(Me.Tag))
                    AsyncLoader(True)
                    FixedAssetTransfer = (Await Model.GetFixedAssetTransfer(INDbtnCode.Text.Trim)).ObjectEmbbeded
                    INDlyTransfer.BeginUpdate()
                    If FixedAssetTransfer IsNot Nothing AndAlso FixedAssetTransfer.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(FixedAssetTransfer.Id))
                            With FixedAssetTransfer
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)

                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                Code = .Code
                                DocumentDate = .DocumentDate
                                TransferType = .TransferType
                                Observation = .Observation

                                SourceLocationId = .SourceLocationId
                                INDsleSourceLocation.Properties.NullText = .SourceLocationCodeName

                                TargetLocationId = .TargetLocationId
                                INDsleTargetLocation.Properties.NullText = .TargetLocationCodeName

                                SourceResponsibleId = .SourceResponsibleId
                                INDsleSourceResponsible.Properties.NullText = .SourceResponsibleCodeName

                                TargetResponsibleId = .TargetResponsibleId
                                INDsleTargetResponsible.Properties.NullText = .TargetResponsibleCodeName

                                BarraBotones.StatusRecord = .Status.ToString

                                ListFixedAssetTransferDetail = .FixedAssetTransferDetail.ToList
                                INDgcAssets.DataSource = Nothing
                                INDgcAssets.DataSource = ListFixedAssetTransferDetail

                            End With
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.FixedAssetTransfer.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordFixedAsset With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = FixedAssetTransfer.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            Me.BarraBotones.SetDocuments(FixedAssetTransfer.Id, Me.Tag.ToString(), Nothing, GetType(FixedAssetTransfer).Name)
                            AsyncLoader(False)
                            ActionsOnControls = True
                            If FixedAssetTransfer.Status = 1 Then
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                            Else
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                ReadOnlyControls(True)
                            End If
                            INDsleTransferType.Properties.ReadOnly = True
                            INDsleSourceLocation.Properties.ReadOnly = True
                            INDsleSourceResponsible.Properties.ReadOnly = True
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, FixedAssetTransfer.Id, 0, FixedAssetTransfer.Id, _idOperativeUnit)
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewFixedAssetTransfer()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbtnCode.Focus()
                        End If
                    End If
                    INDlyTransfer.EndUpdate()
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
        '    Using Model As New MFixedAssetTransfer(MyTag)
        '        Dim resultOperation = Await Model.GetFixedAssetTransfer(INDbtnCode.Text.Trim)
        '        INDlyTransfer.BeginUpdate()
        '        FixedAssetTransfer = resultOperation.ObjectEmbbeded
        '        If Not FixedAssetTransfer Is Nothing Then
        '            If FixedAssetTransfer.Id > 0 Then
        '                Using ModelRecord As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
        '                    Dim result = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(FixedAssetTransfer.Id))
        '                    With FixedAssetTransfer
        '                        LayoutControls.SetCustomFieldsValue(.CustomProperties)

        '                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
        '                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
        '                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
        '                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

        '                        Code = .Code
        '                        DocumentDate = .DocumentDate
        '                        TransferType = .TransferType
        '                        Observation = .Observation

        '                        SourceLocationId = .SourceLocationId
        '                        INDsleSourceLocation.Properties.NullText = .SourceLocationCodeName

        '                        TargetLocationId = .TargetLocationId
        '                        INDsleTargetLocation.Properties.NullText = .TargetLocationCodeName

        '                        SourceResponsibleId = .SourceResponsibleId
        '                        INDsleSourceResponsible.Properties.NullText = .SourceResponsibleCodeName

        '                        TargetResponsibleId = .TargetResponsibleId
        '                        INDsleTargetResponsible.Properties.NullText = .TargetResponsibleCodeName

        '                        BarraBotones.StatusRecord = .Status.ToString

        '                        ListFixedAssetTransferDetail = .FixedAssetTransferDetail.ToList
        '                        INDgcAssets.DataSource = Nothing
        '                        INDgcAssets.DataSource = ListFixedAssetTransferDetail

        '                    End With
        '                    Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.FixedAssetTransfer.Code)
        '                    If result.Id = 0 Then
        '                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
        '                        state.State = Domain.Base.Entities.ObjectState.Added
        '                        record = New BlockRecordFixedAsset With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = CInt(Me.Tag), .CodUser = Me.indigo.UserIndigo, .IdRecord = FixedAssetTransfer.Id}
        '                        Dim operation = Await ModelRecord.SaveBlockRecord(record)
        '                        record = operation.ObjectEmbbeded
        '                    Else
        '                        record = result
        '                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
        '                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
        '                    End If
        '                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
        '                    Me.BarraBotones.SetDocuments(FixedAssetTransfer.Id)
        '                    AsyncLoader(False)
        '                    ActionsOnControls = True
        '                    If FixedAssetTransfer.Status = 1 Then
        '                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
        '                    Else
        '                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '                        ReadOnlyControls(True)
        '                    End If
        '                    INDsleTransferType.Properties.ReadOnly = True
        '                    INDsleSourceLocation.Properties.ReadOnly = True
        '                    INDsleSourceResponsible.Properties.ReadOnly = True
        '                End Using
        '                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
        '                Me.BarraBotones.PrintReport(PrintReportAction.None, FixedAssetTransfer.Id, 0, FixedAssetTransfer.Id, _idOperativeUnit)
        '            Else
        '                AsyncLoader(False)
        '                If Me._sequence.IsManual Then
        '                    Me.NewFixedAssetTransfer()
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
        '                Me.NewFixedAssetTransfer()
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
        'INDlyTransfer.EndUpdate()
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewFixedAssetTransfer() As Task
        Me.FixedAssetTransfer = New FixedAssetTransfer()
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


        'If _sequence Is Nothing OrElse _sequence.Id = 0 Then
        '    Mensaje(EeventViewerImages.Advertencia) = "El formulario no tiene parametrizada la secuencia numérica"
        '    Exit Sub
        'End If
        'Me.FixedAssetTransfer = New FixedAssetTransfer()
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
    End Function

    ''' <summary>
    ''' Metodo que valida que la localizacion destino no sea igual a la localizacion origen
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidateLocation()
        If SourceLocationId IsNot Nothing AndAlso TargetLocationId IsNot Nothing Then
            If SourceLocationId = TargetLocationId Then
                Mensaje(EeventViewerImages.Advertencia) = "La localización origen no puede ser igual a la localización destino"
                TargetLocationId = Nothing
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que valida que el responsable destino no sea igual al responsable origen
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidateResponsible()
        If SourceResponsibleId IsNot Nothing AndAlso TargetResponsibleId IsNot Nothing Then
            If SourceResponsibleId = TargetResponsibleId Then
                Mensaje(EeventViewerImages.Advertencia) = "El responsable origen no puede ser igual al responsable destino"
                INDsleTargetResponsible.Properties.NullText = String.Empty
                TargetResponsibleId = Nothing
            End If
        End If
    End Sub

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        FixedAssetTransfer = Nothing
        Presenter = Nothing
        record = Nothing
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        ListTransferType = Nothing
        varImp = Nothing
        ListFixedAssetTransferDetail = Nothing
        ListDeleteFixedAssetTransferDetail = Nothing
        bwLoadSearch = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmFixedAssetTransfer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyTransfer, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PFixedAssetTransfer(Me)
        IndigoGridControl1.RefreshGrid(INDgcAssets)
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDviewAssets, ListActions)
        Presenter.GetSequense()
        Presenter.LoadDefinitionLayout()
        LoadStatus()
        Deshacer()
        InitializeTuple()
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmFixedAssetTransfer_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
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
                    Await Me.NewFixedAssetTransfer()
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
        OpenFormAddAssets()
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
        RemoveDetail()
    End Sub

    ''' <summary>
    ''' Accion de click derecho del mouse
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        RemoveDetail()
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara para abrir el form de responsable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSourceResponsible_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleSourceResponsible.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1711, Nothing, True)
            Presenter.InitializeSourceResponsible()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form de responsable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleTargetResponsible_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleTargetResponsible.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1711, Nothing, True)
            Presenter.InitializeTargetResponsible()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form de localización
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSourceLocation_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleSourceLocation.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1100, Nothing, True)

        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form de localización
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleTargetLocation_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleTargetLocation.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1100, Nothing, True)

        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara para cargar el datasource responsable origen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSourceResponsible_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleSourceResponsible.QueryPopUp
        If SourceResponsibleXpo Is Nothing Then
            Presenter.InitializeSourceResponsible()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para cargar el datasource responsable destino
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleTargetResponsible_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleTargetResponsible.QueryPopUp
        If TargetResponsibleXpo Is Nothing Then
            Presenter.InitializeTargetResponsible()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de localizacion origen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSourceLocation_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleSourceLocation.QueryPopUp
        If SourceLocationXpo Is Nothing Then
            SourceLocationXpo = Presenter.InitializeLocationXpInstantFeedBackSource()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de localización destino
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleTargetLocation_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleTargetLocation.QueryPopUp
        If TargetLocationXpo Is Nothing Then
            TargetLocationXpo = Presenter.InitializeLocationXpInstantFeedBackSource()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de tipo de traslado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleTransferType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleTransferType.EditValueChanged
        If TransferType IsNot Nothing Then
            Select Case TransferType
                Case 1 'Localización
                    'Muestra los controles de localización
                    INDlyItemSourceLocation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemSourceLocation.AllowHide = False
                    INDlyItemTargetLocation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemTargetLocation.AllowHide = False
                    'Oculta los otros controles
                    INDlyItemSourceResponsible.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemSourceResponsible.AllowHide = True
                    INDlyItemTargetResponsible.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemTargetResponsible.AllowHide = True
                Case 2 'Responsable
                    'Muestra los controles de responsable
                    INDlyItemSourceResponsible.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemSourceResponsible.AllowHide = False
                    INDlyItemTargetResponsible.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemTargetResponsible.AllowHide = False
                    'Oculta los otros controles
                    INDlyItemSourceLocation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemSourceLocation.AllowHide = True
                    INDlyItemTargetLocation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemTargetLocation.AllowHide = True
                Case 3 'Localización y Responsable
                    'Localización
                    INDlyItemSourceLocation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemSourceLocation.AllowHide = False
                    INDlyItemTargetLocation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemTargetLocation.AllowHide = False
                    'Responsable
                    INDlyItemSourceResponsible.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemSourceResponsible.AllowHide = False
                    INDlyItemTargetResponsible.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemTargetResponsible.AllowHide = False
            End Select
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de localizacion destino
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleTargetLocation_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleTargetLocation.EditValueChanged, INDsleSourceLocation.EditValueChanged
        If SourceLocationId IsNot Nothing AndAlso TargetLocationId IsNot Nothing Then
            ValidateLocation()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de responsable destino
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleTargetResponsible_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleTargetResponsible.EditValueChanged, INDsleSourceResponsible.EditValueChanged
        If SourceResponsibleId IsNot Nothing AndAlso TargetResponsibleId IsNot Nothing Then
            ValidateResponsible()
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
        varImp = 2
        FixedAssetTransfer.Status = 1
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
        varImp = 1
        FixedAssetTransfer.Status = 1
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        varImp = 3
        FixedAssetTransfer.Status = 2
        Confirmar()
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        varImp = 3
        FixedAssetTransfer.Status = 2
        Confirmar()
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        varImp = 4
        FixedAssetTransfer.Status = 3
        Guardar()
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, FixedAssetTransfer.Id, 0, FixedAssetTransfer.Id, _idOperativeUnit)
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