'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 15-07-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Payroll.Entities
Imports Presentation.Payroll.MVP
Imports Domain.Base.Entities
Imports Domain.Entities
Imports DevExpress.Data
Imports DevExpress.XtraEditors.Design
Imports System.Windows.Forms
Imports DevExpress.XtraEditors

#End Region

''' <summary>
''' Clase que controla la visa del frontal
''' </summary>
''' <remarks></remarks>
Public Class FrmAuthorizationConcept
    Implements IAuthorizationConcept

#Region "Fields"


    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecord

    ''' <summary>
    ''' Variable para acceder al presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PAuthorizationConcept

    ''' <summary>
    ''' Variable para acceder al modelo
    ''' </summary>
    ''' <remarks></remarks>
    Dim Model As New MAuthorizationConcept(Me.Tag)

    ''' <summary>
    ''' variable para manejar la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim authorization_concept As AuthorizationConcept

    ''' <summary>
    ''' Lista que contiene el numero de los indices de los conceptos que son autorizados por grupos.
    ''' </summary>
    ''' <remarks></remarks>
    Private _List_AuthorizationConceptGroupIndex As List(Of AuthorizationConcept)

    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigo As SessionValues

#End Region

#Region "Properties"
    ''' <summary>
    ''' Establece y muestra un mensaje
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
    ''' Propiedad que controla la accion de los controles en el frontal
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IAuthorizationConcept.ActionsOnControls
        Set(value As Boolean)
            'INDRgSearchOption.Enabled = Not value
            'INDSleEmployee.Enabled = value
            'INDSleGroup.Enabled = value
            'INDGcAuthorizationConcept.Enabled = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el grid control de la rejilla
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property AuthorizationCGridControl As DevExpress.XtraGrid.GridControl Implements IAuthorizationConcept.AuthorizationCGridControl
        Get
            Return INDGcAuthorizationConcept
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el grid view de la rejilla
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property AuthorizationCGridView As DevExpress.XtraGrid.Views.Grid.GridView Implements IAuthorizationConcept.AuthorizationCGridView
        Get
            Return INDGvAuthorizationConcept
        End Get
    End Property

    ''' <summary>
    ''' Establece el datasource de los empleados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Employee_Datasource As List(Of Domain.Payroll.Entities.Employee) Implements IAuthorizationConcept.Employee_Datasource
        Set(value As List(Of Domain.Payroll.Entities.Employee))
            INDSleEmployee.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el id del empleado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Employee_Id As Integer Implements IAuthorizationConcept.Employee_Id
        Get
            Return INDSleEmployee.EditValue
        End Get
    End Property

    ''' <summary>
    ''' Establece el datasource de los grupos
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Group_Datasource As List(Of Group) Implements IAuthorizationConcept.Group_Datasource
        Set(value As List(Of Group))
            INDSleGroup.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el id de los grupos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Group_Id As Integer Implements IAuthorizationConcept.Group_Id
        Get
            Return INDSleGroup.EditValue
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que establece si es por grupo o por empleado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property AuthorizationConceptBy As Integer Implements IAuthorizationConcept.AuthorizationConceptBy
        Get
            Return INDRgSearchOption.EditValue
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene los indices de conceptos autorizados por grupos
    ''' </summary>
    Public Property List_AuthorizationConceptGroupIndex As List(Of AuthorizationConcept) Implements IAuthorizationConcept.List_AuthorizationConceptGroupIndex
        Get
            Return _List_AuthorizationConceptGroupIndex
        End Get
        Set(value As List(Of AuthorizationConcept))
            _List_AuthorizationConceptGroupIndex = value
        End Set
    End Property

    ''' <summary>
    ''' Establece la accion en el control de seleccionar todos 
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ShowCheckAllControl As Boolean Implements IAuthorizationConcept.ShowCheckAllControl
        Set(value As Boolean)
            If value = True Then
                INDlyItemCheckAll.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                INDlyItemCheckAll.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End Set
    End Property

#End Region

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        record = Nothing
        Presenter = Nothing
        Model = Nothing
        authorization_concept = Nothing
        _List_AuthorizationConceptGroupIndex = Nothing
    End Sub
    ''' <summary>
    ''' Evento load del frontal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAuthorizationConcept_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PAuthorizationConcept(Me)
        'Me.Funct = AddressOf GenerateDoc
        Presenter.inizialites()
        Deshacer()
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Await Model.DeleteBlockRecord(record)
            record = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Evento que controla cuando cambian un grupo en el search look up
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSleGroup_EditValueChanged(sender As Object, e As EventArgs)
        If INDSleGroup.EditValue <> Nothing Then
            Await Load_AuthorizationConcept()
        End If
    End Sub

    ''' <summary>
    ''' Evento que controla cuando cambian un empleado en el search look up
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSleEmployee_EditValueChanged(sender As Object, e As EventArgs)
        If INDSleEmployee.EditValue <> Nothing Then
            Await Load_AuthorizationConcept()
        End If
    End Sub

    ''' <summary>
    ''' Evento para controlar la visibilidad de los search look up grupos y empleados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDRgSearchOption_SelectedIndexChanged(sender As Object, e As EventArgs) Handles INDRgSearchOption.SelectedIndexChanged
        Clean_Controls()
        If INDRgSearchOption.EditValue = PAuthorizationConcept.EAuthorizationConceptBy.Group Then
            INDLyItemGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLyItemEmployee.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        ElseIf INDRgSearchOption.EditValue = PAuthorizationConcept.EAuthorizationConceptBy.Employee Then
            INDLyItemEmployee.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLyItemGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    ''' <summary>
    ''' Evento para controlar los click en la columna aplica cuando el empleado tiene conceptos autorizados por su grupo
    ''' </summary>
    Private Sub INDGvAuthorizationConcept_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvAuthorizationConcept.RowCellClick
        If List_AuthorizationConceptGroupIndex.Count > 0 And e.Column.Name = INDColApply.Name Then
            If List_AuthorizationConceptGroupIndex.FindAll(Function(x) x.Id = CType(AuthorizationCGridView.GetRow(e.RowHandle), AuthorizationConcept).Id).Count > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ConceptoAutorizadoPorGrupo, Eform.AutorizacionConceptos)
            Else
                AuthorizationCGridView.SetRowCellValue(e.RowHandle, e.Column, Not e.CellValue)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento, para seleccionar todos los registros de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDchkCheckAll_CheckedChanged(sender As Object, e As EventArgs) Handles INDchkCheckAll.CheckedChanged
        Dim autConByGroup As Boolean = False 'bandera que se usa para indicar si no se desautorizaon algunos conceptos por estar autorizados a nivel de grupo
        For Each item As AuthorizationConcept In INDGcAuthorizationConcept.DataSource
            If INDchkCheckAll.EditValue = False Then
                If List_AuthorizationConceptGroupIndex.Count > 0 AndAlso List_AuthorizationConceptGroupIndex.Find(Function(x) x.Id = item.Id) IsNot Nothing Then
                    autConByGroup = True
                Else
                    item.Apply = INDchkCheckAll.EditValue
                End If
            Else
                item.Apply = INDchkCheckAll.EditValue
            End If
        Next
        If autConByGroup = True Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ConceptosAutorizadoPorGrupo, Eform.AutorizacionConceptos)
        End If
        INDGcAuthorizationConcept.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDSleGroup control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleGroup_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleGroup.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmGroups
                Formulario.ViewModeEditHold = True
                Formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Presenter.inizialites()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDSleEmployee control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleEmployee_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleEmployee.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmEmployee
                Formulario.ViewModeEditHold = True
                Formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Presenter.inizialites()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me.authorization_concept IsNot Nothing AndAlso Me.authorization_concept.Id > 0 Then
            If Not (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDRgSearchOption.EditValue = 1
                Me.INDSleGroup.EditValue = IdEntity.Trim()
            End If
        Else 'Realiza la consulta normal
            Me.INDRgSearchOption.EditValue = 1
            Me.INDSleGroup.EditValue = IdEntity.Trim()
        End If
        Me.IdEntity =  String.Empty
    End Sub

#End Region

#Region "ICRUD"
    Public Sub AbrirBusqueda() Implements IcrudBase.OpenSearch

    End Sub

    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer
        Clean_Controls()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If ValidateControls() = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If
        INDRgSearchOption.Focus()
        AsyncLoader(True)
        If Await Presenter.Save_AuthorizationConcept = True Then
            Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ProcesoRealizadoConExito, Eform.AutorizacionConceptos)
            Deshacer()
        Else
            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
        End If
        AsyncLoader(False)
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

    Public Sub Nuevo() Implements IcrudBase.Nuevo
        Clean_Controls()
    End Sub

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(obtenerRecurso(Eresources.FrmAuthorizationConceptMetaData, Eform.InfoMetaData), Me.INDSleGroup.Text, Me.INDSleEmployee.Text), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity =  "$#" & Me.Tag.ToString & "_" & Me.INDSleGroup.Text & "#$", .IdForm = Me.Tag.ToString, _
                .Title = String.Format(obtenerRecurso(Eresources.FrmAuthorizationConceptMetaDataTitle, Eform.InfoMetaData), Me.INDSleGroup.Text), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmAuthorizationConceptMetaData, Eform.InfoMetaData), Me.INDSleGroup.Text, Me.INDSleEmployee.Text)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmAuthorizationConceptMetaDataTitle, Eform.InfoMetaData), Me.INDSleGroup.Text)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = True, .StatusName = obtenerRecurso(ComunesActivo), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(23, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = False, .StatusName = obtenerRecurso(ComunesInactivo), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(38, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

#End Region

#Region "bar buttons and events"
    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
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
        Clean_Controls()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click customizar.
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
        'CustomizationOpen()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ clic restablecer layout.
    ''' </summary>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
        'ResetLayout()
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo para validar los controles del formulario
    ''' </summary>
    Public Function ValidateControls() As Boolean
        ValidateControls = True
        If INDRgSearchOption.EditValue = Presentation.Payroll.MVP.PAuthorizationConcept.EAuthorizationConceptBy.Group Then
            If INDSleGroup.EditValue = Nothing Then
                ValidateControls = False
                INDSleGroup.Focus()
                Exit Function
            End If
        ElseIf INDRgSearchOption.EditValue = Presentation.Payroll.MVP.PAuthorizationConcept.EAuthorizationConceptBy.Employee Then
            If INDSleEmployee.EditValue = Nothing Then
                ValidateControls = False
                INDSleEmployee.Focus()
                Exit Function
            End If
        End If
    End Function

    ''' <summary>
    ''' Metodo para cargar los conceptos autorizados
    ''' </summary>
    Public Async Function Load_AuthorizationConcept() As Task
        AsyncLoader(True)
        Await Presenter.Load_AuthorizationConcept()
        If List_AuthorizationConceptGroupIndex.Count > 0 Then
            INDColApply.OptionsColumn.AllowEdit = False
        Else
            INDColApply.OptionsColumn.AllowEdit = True
        End If
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        AsyncLoader(False)
    End Function

    ''' <summary>
    ''' metodo para limpiar los controles
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Clean_Controls()
        RemoveHandler INDSleGroup.EditValueChanged, AddressOf INDSleGroup_EditValueChanged
        RemoveHandler INDSleEmployee.EditValueChanged, AddressOf INDSleEmployee_EditValueChanged
        'INDRgSearchOption.EditValue = False
        INDSleEmployee.EditValue = Nothing
        INDSleGroup.EditValue = Nothing
        AuthorizationCGridControl.DataSource = Nothing
        ShowCheckAllControl = False
        AddHandler INDSleGroup.EditValueChanged, AddressOf INDSleGroup_EditValueChanged
        AddHandler INDSleEmployee.EditValueChanged, AddressOf INDSleEmployee_EditValueChanged

        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        INDRgSearchOption.Focus()
    End Sub
#End Region

End Class