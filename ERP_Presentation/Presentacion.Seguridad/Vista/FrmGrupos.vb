'***********************************************************************
' Assembly         : Presentacion.Seguridad.MVP
' Author           : Julian Cardozo
' Created          : 03-03-2011
'
' Last Modified By : Julian Cardozo
' Last Modified On : 23-03-2011
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Importaciones"

Imports DevExpress.XtraEditors
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Presentation.Security.MVP
Imports Presentation.Controls
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.DocumentalSystem.Entities
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.CommonRepository
Imports System.Data
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.SecurityRepository
Imports Domain.Security.Entities

#End Region
''' <summary>
''' Clase que controla los comprotamientos del formulario FrmGrupos.
''' </summary>
Public Class FrmGrupos
    Inherits Presentation.Controls.FormBase
    Implements IGrupos

#Region "variables y Load"
    ''' <summary>
    ''' Variable utilizada para instanciar el presentador.
    ''' </summary>
    Dim presenter As PGrupos

    ''' <summary>
    ''' Listado de tipos de grupo
    ''' </summary>
    Dim ListGroupType As List(Of Tuple(Of Byte, String))

    ''' <summary>
    ''' Controla el evento Load del control FrmGrupos.
    ''' </summary>
    Private Sub FrmGrupos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        presenter = New PGrupos(Me)
        ActivarControles = False
        INDGleGroupType_EditValueChanged(Nothing, Nothing)
        IndigoGridView1.SetListAcction(INDgvDetails, {eAcciones.Remove}.ToList())
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

#End Region

#Region "Propiedades"

    ''' <summary>
    ''' Esta propiedad contiene el codigo del grupo.
    ''' </summary>
    Public Property CodigoDelGrupo As String Implements IGrupos.CodigoDelGrupo
        Get
            Return INDBtnEditCodigoGrupo.Text
        End Get
        Set(ByVal value As String)
            INDBtnEditCodigoGrupo.Text = value
        End Set
    End Property

    ''' <summary>
    ''' esta propiedad contiene el nombre del grupo
    ''' </summary>
    ''' <value></value>
    Public Property NombreDelGrupo As String Implements IGrupos.NombreDelGrupo
        Get
            Return INDTxtNombreGrupo.Text
        End Get
        Set(ByVal value As String)
            INDTxtNombreGrupo.Text = value
        End Set
    End Property

    ''' <summary>
    ''' esta propiedad sirve para activar o desactivar controles
    ''' </summary>
    Public WriteOnly Property ActivarControles As Boolean Implements IGrupos.ActivarControles
        Set(ByVal value As Boolean)
            INDgcBalancedScorecard.Enabled = value
            INDsleBalancedScorecard.Enabled = value
            INDsbAdd.Enabled = value
            INDGleGroupType.Enabled = value
            If value = True Then
                INDBtnEditCodigoGrupo.Enabled = False
                INDTxtNombreGrupo.Enabled = True
                INDTxtNombreGrupo.Focus()
            Else
                INDBtnEditCodigoGrupo.Enabled = True
                INDTxtNombreGrupo.Enabled = False
                INDBtnEditCodigoGrupo.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos.
    ''' </summary>
    ''' <value></value>
    Public WriteOnly Property Mensaje(ByVal Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
        Set(ByVal value As String)

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
    ''' Obtiene o establece un mensaje de desicion
    ''' </summary>
    Private _mensajeDesicion As String
    ''' <summary>
    ''' Obtiene o establece el mensaje de decision.	
    ''' </summary>
    ''' <value>el mensaje decision.</value>
    ''' <remarks></remarks>
    Public Property MesajeDecision As String Implements MVP.IGrupos.MensajeDecision
        Get
            Return _mensajeDesicion
        End Get
        Set(ByVal value As String)

            Dim valor As Integer = XtraMessageBox.Show(value, obtenerRecurso(ComunesIndigoCrystal), MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
            If valor = 2 Then
                Return
            Else
                presenter.EliminarGrupos()
            End If

        End Set
    End Property

    ''' <summary>
    ''' propiedad que establece el focus dependiendo del control asignado
    ''' </summary>
    Public WriteOnly Property EstablecerFoco(ByVal NombreControl As String) As Boolean Implements MVP.IGrupos.EstablecerFoco
        Set(ByVal value As Boolean)


        End Set
    End Property

    ''' <summary>
    ''' Propiedad retorna la instancia del formulario actual
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Instance As FormBase Implements IGrupos.Instance
        Get
            Return Me
        End Get
    End Property

    ''' <summary>
    ''' ´propiedad que retorna el tag del formulario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Tag1 As String Implements IGrupos.Tag
        Get
            Return Me.Tag.ToString
        End Get
    End Property

    ''' <summary>
    ''' propiedad que retorna la barra de botones
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BarraBotones1 As Controls.CtrBarraBotones Implements IGrupos.BarraBotones
        Get
            Return Me.BarraBotones
        End Get
        Set(value As Controls.CtrBarraBotones)
            BarraBotones = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene el tipo de grupo
    ''' </summary>
    ''' <value></value>
    Public Property PGroupType As Byte? Implements IGrupos.PGroupType
        Get
            Return INDGleGroupType.EditValue
        End Get
        Set(ByVal value As Byte?)
            INDGleGroupType.EditValue = value
            If INDGleGroupType.Properties.DataSource Is Nothing AndAlso value IsNot Nothing Then
                INDGleGroupType.Properties.NullText = CType(value, eGroupType).GetEnumDescription()
            End If

        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad hace el data source al control de tenant
    ''' </summary>
    ''' <value></value>
    Public Property TenantDataSource As DevExpress.Xpo.XPServerCollectionSource Implements IGrupos.TenantDataSource
        Get
            Return INDSleTenant.Properties.DataSource
        End Get
        Set(ByVal value As DevExpress.Xpo.XPServerCollectionSource)
            INDSleTenant.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Asigna data source de tenant-group
    ''' </summary>
    Public WriteOnly Property TenantGroupDataSource As IEnumerable(Of TenantGroup) Implements IGrupos.TenantGroupDataSource
        Set(ByVal value As IEnumerable(Of TenantGroup))
            INDGcTenant.DataSource = value
        End Set
    End Property



    ''' <summary>
    ''' Esta propiedad contiene el id de tenant
    ''' </summary>
    ''' <value></value>
    Public Property TenantId As Short? Implements IGrupos.TenantId
        Get
            Return INDSleTenant.EditValue
        End Get
        Set(ByVal value As Short?)
            INDSleTenant.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el tenant seleccionado
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property GetSelectedTenant() As Object Implements IGrupos.GetSelectedTenant
        Get
            Return INDSleTenant.GetSelectedDataRow()
        End Get
    End Property

#End Region

#Region "Metodos Y EVENTOS"
    ''' <summary>
    ''' Metodo para mostrar la barra de progreso 
    ''' </summary>
    ''' <param name="Value"></param>
    ''' <remarks></remarks>
    Public Sub AsyncLoader1(Value As Boolean) Implements IGrupos.AsyncLoader
        AsyncLoader(Value)
    End Sub

    ''' <summary>
    ''' Metodo para generar el documento de indexacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then 'If Me._docIndexed Is Nothing Then
            Me._doc = New IndexedDocument2 With {.Content = String.Format(obtenerRecurso(Eresources.FrmGroupsMetaData, Eform.InfoMetaData), Me.CodigoDelGrupo, Me.NombreDelGrupo), .CreationDate = dateServer, .CreationUser = SessionValues.Instance.UserIndigo & "-" & SessionValues.Instance.UserIndigoName, .DocumentType = IndexedDocumentType.File, .IdEntity = "$#" & Me.Tag.ToString & "_" & Me.CodigoDelGrupo & "#$", .IdForm = Me.Tag.ToString, .Title = String.Format(obtenerRecurso(Eresources.FrmGroupsMetaDataTitle, Eform.InfoMetaData), Me.CodigoDelGrupo), .Update = dateServer, .UpdateUser = SessionValues.Instance.UserIndigo & "-" & SessionValues.Instance.UserIndigoName}
            Return Me._doc

        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = SessionValues.Instance.UserIndigo & "-" & SessionValues.Instance.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmGroupsMetaData, Eform.InfoMetaData), Me.CodigoDelGrupo, Me.NombreDelGrupo)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmGroupsMetaDataTitle, Eform.InfoMetaData), Me.CodigoDelGrupo)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Abrir frontal de busqueda de grupos.
    ''' </summary>
    Public Sub AbrirBusqueda() Implements ICrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            FormSearchObjects.ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code"}, New ColumnInfo With {.Caption = "Grupo", .FieldName = "Description"}, New ColumnInfo With {.Caption = "Tipo", .FieldName = "GroupTypeName"}}.ToList()
            FormSearchObjects.ValorSolicitado = "Code"
            FormSearchObjects.ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.GroupsCRUD
            BarraBotones.PrepareToolbar(eAction.New)
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        INDBtnEditCodigoGrupo.Text = ReturnValue
        If INDBtnEditCodigoGrupo.Text <> String.Empty Then
            presenter.ConsultarNombresGrupos(INDBtnEditCodigoGrupo.Text)
            If INDBtnEditCodigoGrupo.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBtnEditCodigoGrupo.Enabled = False
        End If
    End Sub

    ''' <summary>
    '''Controla el evento KeyDown del control INDBtnCodigo si tiene datos se ejecuta metodo del presente.
    ''' </summary>
    Private Sub INDBtnCodigo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles INDBtnEditCodigoGrupo.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
                Exit Sub
            End If
            If INDBtnEditCodigoGrupo.Text.Trim.Length > 0 Then
                presenter.ConsultarNombresGrupos(INDBtnEditCodigoGrupo.Text)
            Else
                presenter.Deshacer()
                INDBtnEditCodigoGrupo.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Controla el evento ButtonClick del control INDBtnCodigo para abrir el formulario de busqueda de usuarios.
    ''' </summary>
    Private Sub INDBtnEditCodigoGrupo_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBtnEditCodigoGrupo.ButtonClick
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Evento que contiene el manejador del load de la barra de controles
    ''' </summary>
    Private Async Sub BarraBotones_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
    End Sub

    ''' <summary>
    ''' Este metodo permite establecer la logica para los permisos de Guardar y Actualizar
    ''' </summary>
    Public Sub LogicaBotonActualizar(ByVal existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

    ''' <summary>
    ''' Controla el evento Validating del control INDBtnEditCodigoGrupo.
    ''' </summary>
    Private Sub INDBtnEditCodigoGrupo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles INDBtnEditCodigoGrupo.Validating
        If String.IsNullOrEmpty(CodigoDelGrupo) Then

            INDBtnEditCodigoGrupo.ToolTip = String.Concat(obtenerRecurso(ComunesCodigoVacio), " ", obtenerRecurso(GruposMensajeComplemento, Grupos))
        Else
            INDBtnEditCodigoGrupo.ToolTip = String.Empty
        End If
    End Sub

    ''' <summary>
    ''' Controla el evento Validating del control INDTxtNombreGrupo.
    ''' </summary>
    Private Sub INDTxtNombreGrupo_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles INDTxtNombreGrupo.Validating
        If String.IsNullOrEmpty(NombreDelGrupo) Then

            INDTxtNombreGrupo.ToolTip = String.Concat(obtenerRecurso(ComunesNombreVacio), " ", obtenerRecurso(GruposMensajeComplemento, Grupos))
        Else
            INDTxtNombreGrupo.ToolTip = String.Empty
        End If
    End Sub

    ''' <summary>
    ''' Refresca la informacion de la rejilla tenant
    ''' </summary>
    Public Sub RefreshGridTenant()
        INDGcTenant.Invalidate()
        presenter.RefreshGridTenant()
        INDGcTenant.RefreshDataSource()
    End Sub

#End Region

#Region "Eventos"
    ''' <summary>
    ''' Quita un tenant del grupo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRibeDeleteTenant_Click(sender As Object, e As EventArgs) Handles INDRibeDeleteTenant.Click
        Dim _TenantGroup = TryCast(INDGvTenant.GetFocusedRow, TenantGroup)
        If _TenantGroup IsNot Nothing Then
            If _TenantGroup.Id > 0 Then
                _TenantGroup.ChangeTracker.State = ObjectState.Deleted
                presenter.AddTenantGroup(_TenantGroup)
            Else
                presenter.RemoveTenantGroup(_TenantGroup)
            End If
            RefreshGridTenant()
        End If
    End Sub

    ''' <summary>
    ''' Agrega tenant a la rejilla de de tenant
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSmbAddTenant_Click(sender As Object, e As EventArgs) Handles INDSmbAddTenant.Click
        If presenter.ValidateAddTenant() Then
            RefreshGridTenant()
        End If
    End Sub

    ''' <summary>
    ''' Habilita o no los controles de tenant dependiendo del tipo de grupo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleGroupType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleGroupType.EditValueChanged
        If INDGleGroupType.EditValue IsNot Nothing Then
            Select Case CType(PGroupType.GetValueOrDefault, eRollType)
                Case eRollType.GlobalType
                    INDLcgTenant.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLciTenant.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDSleTenant.Enabled = False
                    INDLciAddTenant.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDSmbAddTenant.Enabled = False
                    INDLciGcTenant.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDGcTenant.Enabled = False
                    TenantDataSource = Nothing
                Case eRollType.ByTenant
                    INDLcgTenant.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLciTenant.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDSleTenant.Enabled = True
                    INDLciAddTenant.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDSmbAddTenant.Enabled = True
                    INDLciGcTenant.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDGcTenant.Enabled = True
                    RefreshGridTenant()
                Case Else
            End Select
        Else
            INDLcgTenant.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciTenant.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDSleTenant.Enabled = False
            INDLciAddTenant.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDSmbAddTenant.Enabled = False
            INDLciGcTenant.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDGcTenant.Enabled = False
            TenantDataSource = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Carga el control de tipos de grupo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleGroupType_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDGleGroupType.QueryPopUp
        If INDGleGroupType.Properties.DataSource Is Nothing Then
            ListGroupType = New List(Of Tuple(Of Byte, String))
            If ApplicationSetting.Instance.LoginAzure Then
                Select Case indigo.UserType
                    Case UserType.GlobalAdmin
                        ListGroupType.Add(New Tuple(Of Byte, String)(1, "Global"))
                        ListGroupType.Add(New Tuple(Of Byte, String)(2, "Por tenant"))
                    Case UserType.TenantAdmin
                        ListGroupType.Add(New Tuple(Of Byte, String)(2, "Por tenant"))
                    Case UserType.CompanyAdmin
                    Case UserType.StandardUser
                End Select
            Else
                Select Case indigo.UserType
                    Case UserType.GlobalAdmin
                        ListGroupType.Add(New Tuple(Of Byte, String)(2, "Por tenant"))
                    Case UserType.TenantAdmin
                        ListGroupType.Add(New Tuple(Of Byte, String)(2, "Por tenant"))
                    Case UserType.CompanyAdmin
                    Case UserType.StandardUser
                End Select
            End If
            INDGleGroupType.Properties.DataSource = ListGroupType
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleTenant_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleTenant.QueryPopUp
        If INDSleTenant.Properties.DataSource Is Nothing Then
            presenter.GetAllTenant(indigo.UserIndigoId, indigo.UserType)
        End If
    End Sub
#End Region

#Region "Metodos y eventos control de usuario "

    ''' <summary>
    ''' CONTROLUSUARIO: click boton nuevo llamamos el metodo Nuevo() que va al presentador.
    ''' </summary>
    Private Sub CtrBarraBotones_Click_Nuevo() Handles BarraBotones.ClickNuevo
        Nuevo()
    End Sub

    ''' <summary>
    ''' CONTROLUSUARIO: click boton deshacer llamamos el metodo Deshacer() que va al presentador.
    ''' </summary>
    Private Sub CtrBarraBotones_Click_Deshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' CONTROLUSUARIO: click boton eliminar llamamos el metodo Eliminar() que va al presentador.
    ''' </summary>
    Private Sub CtrBarraBotones_Click_Eliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' CONTROLUSUARIO: click boton buscar llamamos el metodo Buscar() que va al presentador.
    ''' </summary>
    Private Sub CtrBarraBotones_Click_Buscar() Handles BarraBotones.ClickBuscar
        Buscar()
    End Sub

    ''' <summary>
    ''' CONTROLUSUARIO: click boton guardar llamamos el metodo Guardar() que va al presentador.
    ''' </summary>
    Private Sub CtrBarraBotones_Click_Guardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' CONTROLUSUARIO: click boton guardar llamamos el metodo Actualizar() que va al presentador.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements IGrupos.Buscar
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.e.
    ''' </summary>
    Public Sub Deshacer() Implements IGrupos.Deshacer
        'presenter.Deshacer()
        INDgcBalancedScorecard.DataSource = Nothing
        INDsleBalancedScorecard.EditValue = Nothing

        CodigoDelGrupo = String.Empty
        NombreDelGrupo = String.Empty
        ActivarControles = False
        Instance._doc = Nothing
        dtDetails = Nothing
        INDGleGroupType.Properties.NullText = ""
        PGroupType = Nothing

        INDBtnEditCodigoGrupo.Focus()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Sub Eliminar() Implements IGrupos.Eliminar
        If Not String.IsNullOrEmpty(INDBtnEditCodigoGrupo.Text) Then
            If XtraMessageBox.Show(obtenerRecurso(ComunesEliminarRegistro), obtenerRecurso(ComunesIndigoCrystal), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                presenter.EliminarGrupos()
                Deshacer()
                INDBtnEditCodigoGrupo.Focus()
            End If
        End If

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Sub Guardar() Implements IGrupos.Guardar
        presenter.GuardarGrupos()
        INDBtnEditCodigoGrupo.Focus()
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements IGrupos.Nuevo
        presenter.Deshacer()
        INDBtnEditCodigoGrupo.Focus()
        If FormSearchObjects IsNot Nothing Then FormSearchObjects.Close()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    Public Property BalancedSelected As CommonBalancedScorecardXpo

    Public Property dtDetails As DataTable Implements IGrupos.dtDetails
        Get
            Return INDgcBalancedScorecard.DataSource
        End Get
        Set(value As DataTable)
            INDgcBalancedScorecard.DataSource = value
            INDgcBalancedScorecard.RefreshDataSource()
        End Set
    End Property

    Public Property ListaEliminados As List(Of Integer) Implements IGrupos.ListaEliminados

    Private Sub INDsleBalancedScorecard_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleBalancedScorecard.QueryPopUp
        If INDsleBalancedScorecard.Properties.DataSource Is Nothing Then
            INDsleBalancedScorecard.Properties.DataSource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer) _
                .CommonService.GetBalancedScorecard()
        End If
    End Sub

    Private Sub INDsleBalancedScorecard_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDsleBalancedScorecard.EditValueChanging
        If e.NewValue Is Nothing OrElse e.NewValue.ToString().Equals("") Then
            BalancedSelected = Nothing
        Else
            BalancedSelected = CType(CType(INDgvBalancedScorecard.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, CommonBalancedScorecardXpo)
        End If
    End Sub

    Private Sub INDsbAdd_Click(sender As Object, e As EventArgs) Handles INDsbAdd.Click
        If BalancedSelected IsNot Nothing Then
            If dtDetails.Select($"BalancedScorecardId={BalancedSelected.Id}").Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "El item ya esta agregado"
                Exit Sub
            End If
            Dim rownew As DataRow = dtDetails.NewRow()
            With rownew
                .Item("Id") = DBNull.Value
                .Item("BalancedScorecardId") = BalancedSelected.Id
                .Item("BalancedScorecardName") = BalancedSelected.Name
                .Item("UserGroup") = SessionValues.Instance.UserGroup
            End With
            dtDetails.Rows.Add(rownew)
            INDgcBalancedScorecard.DataSource = dtDetails
            INDgcBalancedScorecard.RefreshDataSource()
        End If
        INDsleBalancedScorecard.EditValue = Nothing
        INDsleBalancedScorecard.Focus()
    End Sub

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim FilaSeleccionada As DataRowView = CType(INDgvDetails.GetRow(INDgvDetails.FocusedRowHandle), DataRowView)
            If FilaSeleccionada.Item("Id") IsNot DBNull.Value AndAlso CInt(FilaSeleccionada.Item("Id")) > 0 Then
                If ListaEliminados Is Nothing Then
                    ListaEliminados = New List(Of Integer)()
                End If
                ListaEliminados.Add(CInt(FilaSeleccionada.Item("Id")))
            End If
            FilaSeleccionada.Delete()
        End If
    End Sub
#End Region

End Class