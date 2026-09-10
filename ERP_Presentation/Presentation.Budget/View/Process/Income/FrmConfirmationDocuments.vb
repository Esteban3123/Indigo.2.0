'***********************************************************************
' Assembly         : Presentacion.Budget
' Author           : Carlos Ernesto Cordoba
' Created          : 24-08-2015
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Budget.MVP

#End Region

Public Class FrmConfirmationDocuments
    Implements IConfirmationDocuments

#Region "GLOBALS"
    Dim _listDocumentType As List(Of Tuple(Of Integer, String))
    ''' <summary>
    ''' lista los documentos dependiendo del tipo seleccionado
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listDocuments As XPCollection
    ''' <summary>
    ''' listado con los id de los documentos que se van a confirmar (indice del item en la rejilla, id del registro , tipo de documento)
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listDocumentConfirm As List(Of Tuple(Of Integer, Integer, EBudgetDocumentType))
#End Region

#Region "PROPERTIES"
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

    Public ReadOnly Property MyTag As Object Implements IConfirmationDocuments.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ReadOnly Property ListDocumentType As List(Of Tuple(Of Integer, String))
        Get
            If _listDocumentType Is Nothing Then
                _listDocumentType = New List(Of Tuple(Of Integer, String))
                _listDocumentType.Add(New Tuple(Of Integer, String)(1, "Presupuesto Modificaciones"))
                _listDocumentType.Add(New Tuple(Of Integer, String)(2, "Presupuesto Traslados"))
                _listDocumentType.Add(New Tuple(Of Integer, String)(3, "PAC Modificaciones"))
                _listDocumentType.Add(New Tuple(Of Integer, String)(4, "PAC Traslados"))
                _listDocumentType.Add(New Tuple(Of Integer, String)(5, "Reconocimientos"))
                _listDocumentType.Add(New Tuple(Of Integer, String)(6, "Reconocimientos Modificaciones"))
                _listDocumentType.Add(New Tuple(Of Integer, String)(7, "Recaudos"))
                _listDocumentType.Add(New Tuple(Of Integer, String)(8, "Recaudos Modificaciones"))
            End If
            Return _listDocumentType
        End Get
    End Property
#End Region

#Region "CURD"
    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer

    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements IcrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub
#End Region

#Region "METHODS"
    ''' <summary>
    ''' Abre la busqueda en el visor, aunque esta sin usarse
    ''' </summary>
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub
    ''' <summary>
    ''' Limpia los campos del frm
    ''' </summary>
    Private Sub CleanControls()
        LayoutControl1.BeginUpdate()
        INDGleDocumentType.EditValue = Nothing
        INDGcDocuments.DataSource = Nothing
        IndigoGridControl1.RefreshGrid(INDGcDocuments)
        BarraBotones.StatusRecordVisible = False
        _listDocuments = Nothing
        _listDocumentConfirm = Nothing
        INDGleDocumentType.Focus()
        LayoutControl1.EndUpdate()
    End Sub
#End Region

#Region "HANDLES"
    ''' <summary>
    ''' Limpia la memoria del frm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _listDocumentType = Nothing
        _listDocuments = Nothing
        _listDocumentConfirm = Nothing
    End Sub
    ''' <summary>
    ''' Carga el tipo de documentos con el datasource
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmConfirmationDocuments_Load(sender As Object, e As EventArgs) Handles Me.Load
        CleanControls()
        INDGleDocumentType.Properties.DataSource = ListDocumentType
    End Sub
    ''' <summary>
    ''' Enfoca el campo del tipo de documento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmConfirmationDocuments_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        INDGleDocumentType.Focus()
    End Sub
    ''' <summary>
    ''' Evento al modificar el campo del tipo de documento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleDocumentType_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDGleDocumentType.EditValueChanging
        If e.NewValue IsNot Nothing Then
            Me.Cursor = ChangeCursorIndigo()
            Using model As New MConfirmationDocuments(MyTag)
                INDGvDocuments.ClearSorting()
                'agregamos al listado para confirmar los items que se seleccionaron
                If _listDocuments IsNot Nothing AndAlso _listDocuments.Count > 0 Then
                    For Each item In INDGvDocuments.GetSelectedRows()

                        Dim document = _listDocuments(INDGvDocuments.DataController.GetListSourceRowIndex(item))
                        Dim itemAdd = New Tuple(Of Integer, Integer, EBudgetDocumentType)(INDGvDocuments.DataController.GetListSourceRowIndex(item), document.Id, e.OldValue)
                        If _listDocumentConfirm Is Nothing Then
                            _listDocumentConfirm = New List(Of Tuple(Of Integer, Integer, EBudgetDocumentType))
                        End If
                        _listDocumentConfirm.Add(itemAdd)
                    Next
                End If

                'consulto los items segun el tipo de documento
                _listDocuments = model.ListBudgetDocumentsNotConfirmed(e.NewValue)
                If _listDocuments.Count = 0 Then
                    INDGcDocuments.DataSource = Nothing
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
                INDGcDocuments.DataSource = _listDocuments
                INDGcDocuments.RefreshDataSource()
                'si ya tenia items seleccionados los vuelvo a marcar
                If _listDocumentConfirm IsNot Nothing AndAlso _listDocumentConfirm.Count > 0 Then
                    Dim _listDocumentTmp = _listDocumentConfirm.FindAll(Function(x) x.Item3 = e.NewValue)
                    For Each item In _listDocumentTmp
                        INDGvDocuments.SelectRow(item.Item1)
                        _listDocumentConfirm.Remove(item)
                    Next

                End If
            End Using
            Me.Cursor = Cursors.Default
        End If
    End Sub
#End Region

#Region "BAR BUTTONS"
    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
    End Sub
    ''' <summary>
    ''' Evento que confrima los documentos
    ''' </summary>
    Private Async Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        AsyncLoader(True)
        If INDGvDocuments.SelectedRowsCount = 0 Then
            If _listDocumentConfirm Is Nothing OrElse _listDocumentConfirm.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un documento para confirmar"
                AsyncLoader(False)
                Exit Sub
            End If
        End If

        For Each item In INDGvDocuments.GetSelectedRows()
            If _listDocumentConfirm Is Nothing Then
                _listDocumentConfirm = New List(Of Tuple(Of Integer, Integer, EBudgetDocumentType))
            End If

            Dim document = _listDocuments(INDGvDocuments.DataController.GetListSourceRowIndex(item))

            Dim documentAdded = _listDocumentConfirm.Find(Function(x) x.Item2 = document.Id And x.Item3 = INDGleDocumentType.EditValue)
            If documentAdded IsNot Nothing Then
                Continue For
            End If

            Dim itemAdd = New Tuple(Of Integer, Integer, EBudgetDocumentType)(INDGvDocuments.DataController.GetListSourceRowIndex(item), document.Id, INDGleDocumentType.EditValue)

            _listDocumentConfirm.Add(itemAdd)
        Next

        Using model As New MConfirmationDocuments(MyTag)
            Dim result = Await model.ConfirmDocuments(_listDocumentConfirm)
            If result.StateResult = True Then
                If result.Message.Length = 0 Then
                    Mensaje(EeventViewerImages.Informacion) = "Se confirmaron correctamente todos los documentos"
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "Los siguientes documentos no se confirmaron" + Environment.NewLine + result.Message
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = result.Message
            End If
        End Using
        AsyncLoader(False)
        CleanControls()
    End Sub
    ''' <summary>
    ''' Deshace los cambios hechos en el frm 
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
    End Sub
#End Region

End Class