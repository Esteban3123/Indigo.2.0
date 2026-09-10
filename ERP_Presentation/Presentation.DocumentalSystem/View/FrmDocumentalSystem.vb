Imports Presentation.DocumentalSystem.MPV
Imports Domain.DocumentalSystem.Entities
Imports Presentation.Controls
Imports Presentation.Base
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base.BaseClass
Imports DevExpress.Utils
Imports DevExpress.Data.Filtering
Imports System.Linq
Imports DevExpress.XtraEditors.Controls

''' <summary>
''' Frontal para consultas de documentos
''' </summary>
Public Class FrmDocumentalSystem
    Implements IcrudBase

#Region "Fields"

    ''' <summary>
    ''' Modelo del sistema documental
    ''' </summary>
    Private model As MDocumentalSystem
    ''' <summary>
    ''' Variable control para el foco del TreeList
    ''' </summary>
    Private _focusNodeControl As Boolean
    ''' <summary>
    ''' Coleccion de archivos temporales
    ''' </summary>
    Private _tempFileCollection As New System.CodeDom.Compiler.TempFileCollection

#End Region

#Region "Properties"

    ''' <summary>
    ''' Registra un mensaje en el visor de eventos
    ''' </summary>
    ''' <param name="Icono">Tipo de icono del mensaje</param>
    ''' <value>Mensaje a registrar</value>
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


#End Region

#Region "Handlers"

    ''' <summary>
    ''' Evento al cargar el frontal
    ''' </summary>
    Private Sub FrmDocumentalSystem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.model = New MDocumentalSystem
        Me.CtrCircleLoader1.MessageLbl = "Cargando Archivadores"
        Me.CtrCircleLoader1.BorderControl = True
        Me.INDFileContainersLyci.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Permisos) = True
        Me._focusNodeControl = False
        Me.loadForms()
    End Sub

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.RibbonPagEform.Visible = False
    End Sub

    ''' <summary>
    ''' Evento que permite visualizar los documentos
    ''' </summary>
    Private Async Sub RepositoryItemButtonEdit1_Click(sender As Object, e As EventArgs) Handles RepositoryItemButtonEdit1.Click
        AsyncLoader(True)
        Dim Document = CType(LayoutView1.GetRow(LayoutView1.FocusedRowHandle), DocumentsStore)
        'Dim tempFilePath = System.IO.Path.Combine(System.IO.Path.GetTempPath, System.IO.Path.GetRandomFileName)
        Dim tempFilePath = System.IO.Path.Combine(Window.Utils.TemporalFolder(), System.IO.Path.GetRandomFileName)
        tempFilePath = System.IO.Path.ChangeExtension(tempFilePath, System.IO.Path.GetExtension(Document.Name))
        _tempFileCollection.AddFile(tempFilePath, keepFile:=False)
        Using sqlFileStream = Await model.getData(Document.Id.ToString),
                                      localFileStream As New IO.FileStream(tempFilePath, IO.FileMode.Create, IO.FileAccess.Write)
            sqlFileStream.CopyTo(localFileStream)
        End Using
        Process.Start(tempFilePath)
        AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' Evento al cerrar el frontal
    ''' </summary>
    Private Sub FrmDocumentalSystem_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Me._tempFileCollection IsNot Nothing AndAlso Me._tempFileCollection.Count > 0 Then
            Me._tempFileCollection.Delete()
        End If
    End Sub

    ''' <summary>
    ''' Evento para cambiar el valor de las celdas en función una condición
    ''' </summary>
    Private Sub LayoutView1_CustomRowCellEdit(sender As Object, e As DevExpress.XtraGrid.Views.Layout.Events.LayoutViewCustomRowCellEditEventArgs) Handles LayoutView1.CustomRowCellEdit
        Dim view = CType(sender, DevExpress.XtraGrid.Views.Layout.LayoutView)
        Dim data = CType(view.GetRow(e.RowHandle), DocumentsStore)
        Dim lista = (From document As ImageComboBoxItem In Me.INDImageDocumentsRepositoryIcb.Items Select document.Value).ToList()
        If Not lista.Contains(data.Type) Then
            If Not view.GetRowCellValue(e.RowHandle, Me.INDTypeCol) = ".otro" Then
                view.SetRowCellValue(e.RowHandle, Me.INDTypeCol, ".otro")
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento al cambiar el foco sobre los nodos
    ''' </summary>
    Private Async Sub TreeList1_FocusedNodeChanged(sender As Object, e As DevExpress.XtraTreeList.FocusedNodeChangedEventArgs) Handles TreeList1.FocusedNodeChanged
        If (e.Node IsNot Nothing AndAlso e.OldNode Is Nothing) AndAlso Not Me._focusNodeControl Then
            Me._focusNodeControl = True
            Return
        End If
        If (e.Node Is Nothing AndAlso e.OldNode IsNot Nothing) Then
            Return
        End If
        Dim tag = e.Node.Tag
        If tag = "root" Then
            Dim node = e.Node.GetValue("code")
            If Not node = "0" Then
                AsyncLoader(True)
                Dim listDocuments As List(Of DocumentsStore) = Await Me.model.getDocuments("0", node, "", False)
                Me.GridControl1.DataSource = listDocuments
                AsyncLoader(False)
            End If
        ElseIf tag = "child" Then
            Dim node = e.Node.GetValue("code")
            Dim nodeR = e.Node.GetValue("relation")
            AsyncLoader(True)
            Dim listDocuments As List(Of DocumentsStore) = Await Me.model.getDocuments(node, nodeR, "", False)
            Me.GridControl1.DataSource = listDocuments
            AsyncLoader(False)
        End If
    End Sub

    ''' <summary>
    ''' Evento al mover el mouse sobre el layoutView
    ''' </summary>
    Private Sub LayoutView1_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles LayoutView1.MouseMove
        Dim hit As DevExpress.XtraGrid.Views.Layout.ViewInfo.LayoutViewHitInfo = Me.LayoutView1.CalcHitInfo(e.Location)
        If hit IsNot Nothing AndAlso hit.HitField IsNot Nothing AndAlso hit.HitField.FieldName IsNot Nothing AndAlso (hit.HitField.FieldName.Trim().Equals("Name") OrElse hit.HitField.FieldName.Trim().Equals("Type")) Then
            Me.GridControl1.Cursor = System.Windows.Forms.Cursors.Hand
        Else
            Me.GridControl1.Cursor = System.Windows.Forms.Cursors.Default
        End If
    End Sub

    ''' <summary>
    ''' Evento al hacer click sobre los registros del layoutView
    ''' </summary>
    Private Async Sub LayoutView1_Click(sender As Object, e As EventArgs) Handles LayoutView1.Click
        Dim coord As System.Drawing.Point = Me.GridControl1.PointToClient(MousePosition)
        Dim hit As DevExpress.XtraGrid.Views.Layout.ViewInfo.LayoutViewHitInfo = Me.LayoutView1.CalcHitInfo(coord)
        If hit IsNot Nothing AndAlso hit.HitField IsNot Nothing AndAlso hit.HitField.FieldName IsNot Nothing AndAlso (hit.HitField.FieldName.Trim().Equals("Name") OrElse hit.HitField.FieldName.Trim().Equals("Type")) Then
            AsyncLoader(True)
            Dim Document = CType(LayoutView1.GetRow(LayoutView1.FocusedRowHandle), DocumentsStore)
            'Dim tempFilePath = System.IO.Path.Combine(System.IO.Path.GetTempPath, System.IO.Path.GetRandomFileName)
            Dim tempFilePath = System.IO.Path.Combine(Window.Utils.TemporalFolder(), System.IO.Path.GetRandomFileName)
            tempFilePath = System.IO.Path.ChangeExtension(tempFilePath, System.IO.Path.GetExtension(Document.Name))
            _tempFileCollection.AddFile(tempFilePath, keepFile:=False)
            Using sqlFileStream = Await model.getData(Document.Id.ToString),
                                          localFileStream As New IO.FileStream(tempFilePath, IO.FileMode.Create, IO.FileAccess.Write)
                sqlFileStream.CopyTo(localFileStream)
            End Using
            Process.Start(tempFilePath)
            AsyncLoader(False)
        End If
    End Sub

    ''' <summary>
    ''' Evento click sobre el buttonEdit de buscar
    ''' </summary>
    Private Sub INDTextSearchBte_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDTextSearchBte.ButtonClick
        LayoutView1.ApplyFindFilter(Me.INDTextSearchBte.Text)
    End Sub

    ''' <summary>
    ''' Evento al pulsar tecla sobre el buttonEdit de busqueda
    ''' </summary>
    Private Sub INDTextSearchBte_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDTextSearchBte.KeyUp
        LayoutView1.ApplyFindFilter(Me.INDTextSearchBte.Text)
    End Sub

#Region "Toolbar Events"

    ''' <summary>
    ''' Evento para abrir la customización
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
        Me.OpenCustomize()
    End Sub

    ''' <summary>
    ''' Evento para reestablecer el layout
    ''' </summary>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
        Me.ResetLayout()
    End Sub

#End Region

#End Region

#Region "Methods and Functions"

    ''' <summary>
    ''' Metodo para generar los nodos
    ''' </summary>
    Async Sub loadForms()
        Me.INDCircleLoaderLyci.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Dim dataForms As List(Of VieForm) = BaseClass.GetXmlWithAggregates(Of VieForm)(Base.eDataXml.XMLForms)
        Dim data As List(Of FileContainer) = Await Me.model.getAllFileContainers
        For Each item As FileContainer In data
            For Each item2 As FileContainersForm In item.FileContainersForm
                Dim reg = dataForms.Where(Function(x) x.Id.Equals(item2.IdForm.ToString)).FirstOrDefault
                item2.ObjForm = reg
            Next
        Next

        For Each item As FileContainer In data
            Dim node As DevExpress.XtraTreeList.Nodes.TreeListNode = TreeList1.Nodes.Add(item)
            node.Tag = "root"
            node.SetValue("name", item.Id & "-" & item.Name)
            node.SetValue("code", item.Id)
            node.StateImageIndex = 0
            node.ImageIndex = 0
            Dim nodeNew As DevExpress.XtraTreeList.Nodes.TreeListNode = Nothing
            For Each item2 As FileContainersForm In item.FileContainersForm
                nodeNew = TreeList1.AppendNode(Nothing, node)
                nodeNew.Tag = "child"
                nodeNew.ImageIndex = 1
                nodeNew.StateImageIndex = 1
                nodeNew.SetValue("name", item2.IdForm & "-" & item2.FormName)
                nodeNew.SetValue("code", item2.IdForm)
                nodeNew.SetValue("relation", item2.FileContainer.Id)
            Next
        Next
        TreeList1.SetFocusedNode(Nothing)
        Me.INDCircleLoaderLyci.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.INDFileContainersLyci.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
    End Sub

#Region "CRUD"

    Public Sub AbrirBusqueda() Implements IcrudBase.OpenSearch
        'unimplemented
    End Sub

    Public Sub Buscar() Implements IcrudBase.Buscar
        'unimplemented
    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer
        'unimplemented
    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar
        'unimplemented
    End Sub

    Public Sub Guardar() Implements IcrudBase.Guardar
        'unimplemented
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar
        'unimplemented
    End Sub

    Public Sub Nuevo() Implements IcrudBase.Nuevo
        'unimplemented
    End Sub

#End Region

#End Region

End Class
