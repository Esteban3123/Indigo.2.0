'***********************************************************************
' Assembly         : Presentacion.Seguridad
' Author           : Hector Rodriguez Rubiano
' Created          : 19-04-2021
'
' Last Modified By :
' Last Modified On :
' Description      : Formulario para asignar permisos por tenant
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports System.Threading.Tasks
Imports Domain.Security.Entities
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraEditors.Controls
Imports Presentation.Security.MVP
#End Region

Public Class PopupPermissionsTenant
    'Inherits Presentation.Controls.FormBase

#Region "Constructor"
    ''' <summary>
    ''' Inicia una instancia del formulario
    ''' </summary>
    Public Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        IndigoGridControl1.SetHoldSize(INDgcPermission, True)
    End Sub
#End Region

#Region "Variables"
    Private _Roll As Roll
    Dim _listModulesForms As List(Of VieForm)
#End Region

#Region "Propiedades"
    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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
    Private _OriginalUser As User
    Public Property OriginalUser() As User
        Get
            Return _OriginalUser
        End Get
        Set(ByVal value As User)
            _OriginalUser = value
            _User = value.Clone()
        End Set
    End Property

    Private _User As User
    Public Property PUser() As User
        Get
            Return _User
        End Get
        Set(ByVal value As User)
            _User = value
        End Set
    End Property

    Private _TenantId As Short
    Public Property TenantId() As Short
        Get
            Return _TenantId
        End Get
        Set(ByVal value As Short)
            _TenantId = value
        End Set
    End Property

    Private _RollId As Integer
    Public Property RollId() As Integer
        Get
            Return _RollId
        End Get
        Set(ByVal value As Integer)
            _RollId = value
        End Set
    End Property

    Private listModulesForm As List(Of VieForm)
    Public Property listModulesForms() As List(Of VieForm)
        Get
            Return listModulesForm
        End Get
        Set(ByVal value As List(Of VieForm))
            listModulesForm = value
        End Set
    End Property

#End Region

#Region "Procesos"
    ''' <summary>
    ''' metodo recursivo para ir cambiando valores a cada grupo cuado se quitan permisos
    ''' </summary>
    ''' <param name="view"></param>
    ''' <param name="groupRowHandle"></param>
    ''' <remarks></remarks>
    Private Sub GetChildsRowsUnSelect(view As GridView, groupRowHandle As Integer)
        If Not view.IsGroupRow(groupRowHandle) Then
            Return
        End If

        Dim childCount As Integer = view.GetChildRowCount(groupRowHandle)
        For i As Integer = 0 To childCount - 1
            Dim childHandle As Integer = view.GetChildRowHandle(groupRowHandle, i)
            If view.IsGroupRow(childHandle) Then
                GetChildsRowsUnSelect(view, childHandle)
            Else
                Dim listActionTmp = PUser.PermissionUser.Where(Function(x) x.TenantId = TenantId AndAlso x.Id = 0).ToList()
                While listActionTmp.Count > 0
                    PUser.PermissionUser.Remove(listActionTmp(0))
                    listActionTmp.Remove(listActionTmp(0))
                End While
                If PUser.PermissionUser.Count > 0 Then
                    Dim form = DirectCast(INDgcvPermission.GetRow(childHandle), VieForm)
                    For Each action In PUser.PermissionUser.Where(Function(x) x.TenantId = TenantId AndAlso x.IdForm = form.Id AndAlso x.ActionValue = True)
                        action.ActionValue = False
                    Next
                End If
            End If

        Next
    End Sub

    ''' <summary>
    '''metodo recursivo para ir cambiando valores cuando se quieren dar los permisos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetChildsRowsSelect(view As GridView, groupRowHandle As Integer)
        If Not view.IsGroupRow(groupRowHandle) Then
            Return
        End If

        Dim childCount As Integer = view.GetChildRowCount(groupRowHandle)
        For i As Integer = 0 To childCount - 1
            Dim childHandle As Integer = view.GetChildRowHandle(groupRowHandle, i)
            If view.IsGroupRow(childHandle) Then
                GetChildsRowsSelect(view, childHandle)
            Else
                Dim form = DirectCast(INDgcvPermission.GetRow(childHandle), VieForm)
                For Each action In form.Permissions
                    Dim actionForm = PUser.PermissionUser.Where(Function(x) x.TenantId = TenantId AndAlso x.IdForm = form.Id AndAlso x.Action = action.Id).FirstOrDefault()
                    If actionForm IsNot Nothing Then
                        actionForm.ActionValue = True
                    Else
                        Dim permissionUser As New PermissionUser
                        With permissionUser
                            .IdUser = PUser.Id
                            .IdForm = form.Id
                            .Action = action.Id
                            .ActionValue = True
                            .TenantId = TenantId
                        End With
                        PUser.PermissionUser.Add(permissionUser)
                    End If
                Next
            End If

        Next
    End Sub
#End Region

#Region "Funciones"
    ''' <summary>
    ''' Funcion para asignar todos los permisos de un tenant
    ''' </summary>
    ''' <returns></returns>
    Private Function SetAllPermissions() As Task
        _listModulesForms.ForEach(Sub(f) f.Permissions.ForEach(Sub(p) p.Value = True))
        PUser.PermissionUser.Where(Function(_PermissionUser) _PermissionUser.TenantId = TenantId).ToList().ForEach(Sub(p) p.ActionValue = True)
        Dim linq = From form In _listModulesForms'.Where(Function(f) f.Modules IsNot Nothing AndAlso f.Modules.Count > 0)
                   From action In form.Permissions
                   Group Join _PermissionUser In PUser.PermissionUser.Where(Function(_PermissionUser) _PermissionUser.TenantId = TenantId) On _PermissionUser.IdForm Equals form.Id And _PermissionUser.Action Equals action.Id Into Group
                   From _PermissionUser In Group.DefaultIfEmpty
                   Where _PermissionUser Is Nothing
                   Select New PermissionUser() With {
                        .IdUser = PUser.Id,
                        .IdForm = form.Id,
                        .Action = action.Id,
                        .ActionValue = True,
                       .TenantId = TenantId}
        Return Task.Factory.StartNew(Sub()
                                         For Each reg In (From _PermissionUser In linq
                                                          Group _PermissionUser By _PermissionUser.IdForm, _PermissionUser.Action Into _Group = Group
                                                          Select _Group)
                                             PUser.PermissionUser.Add(reg.FirstOrDefault)
                                         Next
                                     End Sub)

    End Function

    ''' <summary>
    ''' Funcion para quitar todos los permisos de un tenant
    ''' </summary>
    ''' <returns></returns>
    Private Function RemoveAllPermissions() As Task
        Dim listActionTmp = PUser.PermissionUser.Where(Function(x) x.TenantId = TenantId AndAlso x.Id = 0).ToList()
        While listActionTmp.Count > 0
            PUser.PermissionUser.Remove(listActionTmp(0))
            listActionTmp.Remove(listActionTmp(0))
        End While
        Dim objLock As New Object()

        Return Task.Factory.StartNew(Sub()
                                         Parallel.ForEach(PUser.PermissionUser.Where(Function(x) x.TenantId = TenantId AndAlso x.ActionValue = True).ToList(), Sub(action As PermissionUser)
                                                                                                                                                                   SyncLock objLock
                                                                                                                                                                       action.ActionValue = False
                                                                                                                                                                   End SyncLock
                                                                                                                                                               End Sub)
                                     End Sub)
    End Function
#End Region

#Region "Eventos"
    ''' <summary>
    ''' Evento cuando se carga el control
    ''' </summary>
    Private Sub PopupPermissionsTenant_Load() Handles Me.Load
        INDgcvPermission.OptionsView.ShowAutoFilterRow = False
        INDgcvPermission.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  INDgcPermission.SafeInvoke(Sub()
                                                                 _listModulesForms = listModulesForms
                                                                 INDgcPermission.DataSource = _listModulesForms '.Where(Function(f) f.Modules IsNot Nothing AndAlso f.Modules.Count > 0).ToList()
                                                                 INDgcvPermission.HideLoadingPanel()
                                                             End Sub)
                              End Sub)

    End Sub

    ''' <summary>
    ''' Clic en dar todos los permisos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnGiveAllPermissions_Click(sender As Object, e As EventArgs) Handles INDbtnGiveAllPermissions.Click
        INDgcPermission.BeginUpdate()
        Await SetAllPermissions()
        INDgcPermission.RefreshDataSource()
        INDgcPermission.EndUpdate()
    End Sub

    ''' <summary>
    ''' Clic en quitar todos los permisos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnRemoveAllPermissions_Click(sender As Object, e As EventArgs) Handles INDbtnRemoveAllPermissions.Click
        INDgcPermission.BeginUpdate()
        Await RemoveAllPermissions()
        INDgcPermission.RefreshDataSource()
        INDgcPermission.EndUpdate()
    End Sub

    ''' <summary>
    ''' Muestra un menu para seleccionar o quitar permisos por grupo 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgcvPermission_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs) Handles INDgcvPermission.PopupMenuShowing
        If e.HitInfo IsNot Nothing Then
            e.Allow = True
            PopupMenuActions.Manager = BarManager
            PopupMenuActions.ShowPopup(INDgcvPermission.GridControl.PointToScreen(e.Point))
        End If
    End Sub

    ''' <summary>
    ''' Evento cuando se selecciona un permiso
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbarButtonSelectAll_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbarButtonSelectAll.ItemClick
        'si se esta seleccionando un formulario obtengo la vista del detalle
        Dim detailView = TryCast(INDgcvPermission.GetDetailView(INDgcvPermission.FocusedRowHandle, INDgcvPermission.GetRelationIndex(INDgcvPermission.FocusedRowHandle, "Permissions")), GridView)

        If detailView IsNot Nothing Then
            'valido las filas seleccionadas
            Dim form = DirectCast(INDgcvPermission.GetRow(detailView.SourceRowHandle), VieForm)
            If detailView.GetSelectedRows.Length > 0 Then
                For Each index In detailView.GetSelectedRows
                    Dim action = CType(detailView.GetRow(index), ViePermission)
                    Dim actionForm = PUser.PermissionUser.Where(Function(x) x.TenantId = TenantId AndAlso x.IdForm = form.Id AndAlso x.Action = action.Id).FirstOrDefault()
                    'si el rol ya tenia el permiso lo marco como true
                    If actionForm IsNot Nothing Then
                        actionForm.ActionValue = True
                    Else
                        'sino agrego el permiso
                        Dim permissionUser As New PermissionUser
                        With permissionUser
                            .IdUser = PUser.Id
                            .IdForm = form.Id
                            .Action = action.Id
                            .ActionValue = True
                            .TenantId = TenantId
                        End With
                        PUser.PermissionUser.Add(permissionUser)

                    End If
                Next
            Else
                'si no tiene filas seleccionadas marco  o agrego todas todas las acciones
                For Each action In form.Permissions
                    Dim actionForm = PUser.PermissionUser.Where(Function(x) x.TenantId = TenantId AndAlso x.IdForm = form.Id AndAlso x.Action = action.Id).FirstOrDefault()
                    If actionForm IsNot Nothing Then
                        actionForm.ActionValue = True
                    Else
                        Dim permissionUser As New PermissionUser
                        With permissionUser
                            .IdUser = PUser.Id
                            .IdForm = form.Id
                            .Action = action.Id
                            .ActionValue = True
                            .TenantId = TenantId
                        End With
                        PUser.PermissionUser.Add(permissionUser)
                    End If
                Next
            End If
        Else
            'si esta sobre los grupos los recorro para marcar las acciones
            For Each item In INDgcvPermission.GetSelectedRows()
                If INDgcvActionPermission.IsGroupRow(item) Then
                    GetChildsRowsSelect(INDgcvPermission, item)
                Else
                    Dim form = DirectCast(INDgcvPermission.GetRow(item), VieForm)
                    For Each action In form.Permissions
                        Dim actionForm = PUser.PermissionUser.Where(Function(x) x.TenantId = TenantId AndAlso x.IdForm = form.Id AndAlso x.Action = action.Id).FirstOrDefault()
                        If actionForm IsNot Nothing Then
                            actionForm.ActionValue = True
                        Else
                            Dim permissionUser As New PermissionUser
                            With permissionUser
                                .IdUser = PUser.Id
                                .IdForm = form.Id
                                .Action = action.Id
                                .ActionValue = True
                                .TenantId = TenantId
                            End With
                            PUser.PermissionUser.Add(permissionUser)
                        End If
                    Next
                End If
            Next
        End If
        INDgcPermission.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbarButtonUnSelectAll_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbarButtonUnSelectAll.ItemClick
        'si se esta seleccionando un formulario obtengo la vista del detalle
        Dim detailView = TryCast(INDgcvPermission.GetDetailView(INDgcvPermission.FocusedRowHandle, INDgcvPermission.GetRelationIndex(INDgcvPermission.FocusedRowHandle, "Permissions")), GridView)

        If detailView IsNot Nothing Then
            'valido cuantas acciones tiene seleccionadas para cambiar el valor
            Dim form = DirectCast(INDgcvPermission.GetRow(detailView.SourceRowHandle), VieForm)
            If detailView.GetSelectedRows.Length > 0 Then
                For Each index In detailView.GetSelectedRows
                    Dim action = CType(detailView.GetRow(index), ViePermission)
                    Dim actionForm = PUser.PermissionUser.Where(Function(x) x.TenantId = TenantId AndAlso x.IdForm = form.Id And x.Action = action.Id).FirstOrDefault()
                    If actionForm IsNot Nothing AndAlso actionForm.Id > 0 Then
                        actionForm.ActionValue = False
                    Else
                        PUser.PermissionUser.Remove(actionForm)
                    End If
                Next
            Else
                'cambio el valor a todas las acciones
                If PUser.PermissionUser.Count > 0 Then
                    For Each action In form.Permissions
                        Dim actionForm = PUser.PermissionUser.Where(Function(x) x.TenantId = TenantId AndAlso x.IdForm = form.Id AndAlso x.Action = action.Id).FirstOrDefault()
                        If actionForm IsNot Nothing AndAlso actionForm.Id > 0 Then
                            actionForm.ActionValue = False
                        Else
                            PUser.PermissionUser.Remove(actionForm)
                        End If
                    Next
                End If
            End If

        Else
            'reccorro todos los grupos para cambiar el valor de las acciones
            For Each item In INDgcvPermission.GetSelectedRows()
                If INDgcvActionPermission.IsGroupRow(item) Then
                    GetChildsRowsUnSelect(INDgcvPermission, item)
                Else
                    Dim listActionTmp = PUser.PermissionUser.Where(Function(x) x.TenantId = TenantId AndAlso x.Id = 0).ToList()
                    While listActionTmp.Count > 0
                        PUser.PermissionUser.Remove(listActionTmp(0))
                        listActionTmp.Remove(listActionTmp(0))
                    End While
                    If PUser.PermissionUser.Count > 0 Then
                        Dim form = DirectCast(INDgcvPermission.GetRow(item), VieForm)
                        For Each action In PUser.PermissionUser.Where(Function(x) x.TenantId = TenantId AndAlso x.IdForm = form.Id AndAlso x.ActionValue = True)
                            action.ActionValue = False
                        Next
                    End If
                End If
            Next
        End If
        INDgcPermission.RefreshDataSource()
    End Sub


    ''' <summary>
    ''' Metodo para cambiar el valor de un permiso
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrbgActionValue_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles INDrbgActionValue.EditValueChanging

        Dim detailView = TryCast(INDgcvPermission.GetDetailView(INDgcvPermission.FocusedRowHandle, INDgcvPermission.GetRelationIndex(INDgcvPermission.FocusedRowHandle, "Permissions")), GridView)
        Dim form = DirectCast(INDgcvPermission.GetRow(detailView.SourceRowHandle), VieForm)
        Dim action = CType(detailView.GetFocusedRow, ViePermission)
        If action Is Nothing Then
            Exit Sub
        End If
        'busco en el agregado de rol si existe la accion para el formulario seleccionado
        Dim permisionFormRoll = _Roll.PermissionRoll.Where(Function(x) x.IdForm = form.Id AndAlso x.Action = action.Id).FirstOrDefault()
        If permisionFormRoll IsNot Nothing Then
            If permisionFormRoll.ActionValue Then
                Mensaje(EeventViewerImages.Advertencia) = "No puede quitar un permiso cuando a sido asignado al rol"
                e.Cancel = True
                Exit Sub
            End If
        End If

        'busco en el agregado de usuario si existe la accion para el formulario seleccionado
        Dim permisionFormUser = PUser.PermissionUser.Where(Function(x) x.TenantId = TenantId AndAlso x.IdForm = form.Id AndAlso x.Action = action.Id).FirstOrDefault()
        If permisionFormUser IsNot Nothing Then
            If e.NewValue = True Then
                'cambio el valor a true
                permisionFormUser.ActionValue = True
            Else
                'si cambia a false la accion y no esta en la BD la elimino del agreagado, si ya esta guardada la cambio a false
                If permisionFormUser.Id = 0 Then
                    PUser.PermissionUser.Remove(permisionFormUser)
                Else
                    permisionFormUser.ActionValue = False
                End If
            End If
        Else
            'si no existe la accion la creo y la agrego 
            Dim permissionUser = New PermissionUser
            With permissionUser
                .IdUser = PUser.Id
                .IdForm = form.Id
                .Action = action.Id
                .ActionValue = True
                .TenantId = TenantId
            End With
            PUser.PermissionUser.Add(permissionUser)
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgcvPermission_MasterRowExpanding(sender As Object, e As MasterRowCanExpandEventArgs) Handles INDgcvPermission.MasterRowExpanding
        Dim form = DirectCast(INDgcvPermission.GetFocusedRow(), VieForm)

        If _Roll Is Nothing Then
            Dim modelRol = New MRoles
            _Roll = modelRol.GetRolByIdAndPermissionRollByIdForm(RollId, form.Id)
        Else
            Dim formExits = _Roll.PermissionRoll.Where(Function(x) x.IdForm = form.Id).FirstOrDefault()
            If formExits Is Nothing Then
                Dim modelRol = New MRoles
                _Roll = modelRol.GetRolByIdAndPermissionRollByIdForm(RollId, form.Id)
            End If
        End If

        For Each action In form.Permissions
            'obtenemos las acciones del rol
            Dim actionFormRoll = _Roll.PermissionRoll.Where(Function(x) x.IdForm = form.Id AndAlso x.Action = action.Id).FirstOrDefault()
            If actionFormRoll IsNot Nothing Then
                action.Value = CBool(actionFormRoll.ActionValue)
            Else
                action.Value = False
            End If
            'obtenemos las acciones del usuario y cambiamos solo si el valor es falso ya que el rol puede tener la accion
            If action.Value = False Then
                Dim actionFormUser = PUser.PermissionUser.Where(Function(x) x.TenantId = TenantId AndAlso x.IdForm = form.Id AndAlso x.Action = action.Id).FirstOrDefault()
                If actionFormUser IsNot Nothing Then
                    action.Value = CBool(actionFormUser.ActionValue)
                Else
                    action.Value = False
                End If
            End If

        Next

    End Sub

    ''' <summary>
    ''' Actualiza los permisos modificados en el objeto usuario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSmbAccept_Click(sender As Object, e As EventArgs) Handles INDSmbAccept.Click
        Dim _PermissionUsers = From _PermissionUserMod In PUser.PermissionUser.Where(Function(pum) pum.TenantId = TenantId)
                               Group Join _PermissionUser In OriginalUser.PermissionUser.Where(Function(pu) pu.TenantId = TenantId) On
                                   _PermissionUser.IdForm Equals _PermissionUserMod.IdForm And _PermissionUser.Action Equals _PermissionUserMod.Action Into Group
                               From _PermissionUser In Group.DefaultIfEmpty()
                               Select _PermissionUserMod, _PermissionUser

        'Modifica permisos existentes
        If _PermissionUsers.Count > 0 AndAlso _PermissionUsers.Any(Function(_ExistingPermissionUser) _ExistingPermissionUser._PermissionUser IsNot Nothing) Then
            For Each _ExistingPermissionUserAux In _PermissionUsers.Where(Function(_ExistingPermissionUser) _ExistingPermissionUser._PermissionUser IsNot Nothing)
                _ExistingPermissionUserAux._PermissionUser.ActionValue = _ExistingPermissionUserAux._PermissionUserMod.ActionValue
            Next
        End If

        'Agrega permisos nuevos
        If _PermissionUsers.Count > 0 AndAlso _PermissionUsers.Any(Function(_NewPermissionUser) _NewPermissionUser._PermissionUser Is Nothing) Then
            For Each _NewPermissionUserAux In _PermissionUsers.Where(Function(_NewPermissionUser) _NewPermissionUser._PermissionUser Is Nothing).ToList
                OriginalUser.PermissionUser.Add(New PermissionUser() With {
                                                .IdUser = _NewPermissionUserAux._PermissionUserMod.IdUser,
                                                .IdForm = _NewPermissionUserAux._PermissionUserMod.IdForm,
                                                .Action = _NewPermissionUserAux._PermissionUserMod.Action,
                                                .ActionValue = _NewPermissionUserAux._PermissionUserMod.ActionValue,
                                                .TenantId = _NewPermissionUserAux._PermissionUserMod.TenantId})
            Next
        End If

        'Permisos eliminados que no estan guardados en base de datos
        Dim _DeletePermissionUsers = From _PermissionUser In OriginalUser.PermissionUser.Where(Function(pu) pu.TenantId = TenantId)
                                     Group Join _PermissionUserMod In PUser.PermissionUser.Where(Function(pum) pum.TenantId = TenantId) On
                                         _PermissionUserMod.IdForm Equals _PermissionUser.IdForm And
                                        _PermissionUserMod.Action Equals _PermissionUser.Action Into Group
                                     From _PermissionUserMod In Group.DefaultIfEmpty()
                                     Where _PermissionUserMod Is Nothing
                                     Select _PermissionUser
        If _DeletePermissionUsers.Count > 0 AndAlso _DeletePermissionUsers.Any() Then
            For Each _DeletePermissionUser In _DeletePermissionUsers.ToList
                OriginalUser.PermissionUser.Remove(_DeletePermissionUser)
            Next
        End If

        PUser = Nothing
        Me.Close()
    End Sub

    ''' <summary>
    ''' Cancela la edicion de permisos y cierra el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSmbCancel_Click(sender As Object, e As EventArgs) Handles INDSmbCancel.Click
        PUser = Nothing
        Me.Close()
    End Sub

#End Region


End Class