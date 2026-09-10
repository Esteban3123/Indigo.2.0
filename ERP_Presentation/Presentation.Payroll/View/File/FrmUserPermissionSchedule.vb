'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 04-12-2019
'
' Last Modified By :
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Payroll.MVP
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls
Imports Domain.Entities
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.CrossCutting.Resources
#End Region


Public Class FrmUserPermissionSchedule
    Implements IUserPermissionSchedule
    Private _Presenter As PUserPermissionsSchedule

    Dim ListPositionRol As New List(Of PositionRoll)
    Dim ListDeletePositionRol As New List(Of PositionRoll)

    Dim ListPositionUser As New List(Of PositionUser)
    Dim ListDeletePositionUser As New List(Of PositionUser)

    Dim ListFunctionalUnitResponisble As New List(Of FunctionalUnitResponsible)
    Dim ListDeleteFunctionalUnitResponsible As New List(Of FunctionalUnitResponsible)

    Public WriteOnly Property SLERoleDataSource As XPInstantFeedbackSource Implements IUserPermissionSchedule.SLERoleDataSource
        Set(value As XPInstantFeedbackSource)
            INDsleRole.Properties.DataSource = value
        End Set
    End Property

    Public WriteOnly Property SLEUserDataSource As LinqInstantFeedbackSource Implements IUserPermissionSchedule.SLEUserDataSource
        Set(value As LinqInstantFeedbackSource)
            INDsleUser.Properties.DataSource = value
        End Set
    End Property

    Public WriteOnly Property SLEUserTypeDataSource As Object Implements IUserPermissionSchedule.SLEUserTypeDataSource
        Set(value As Object)
            INDsleUserType.Properties.DataSource = value
        End Set
    End Property

    Private WriteOnly Property GCFunctionalUnitDataSource As XPInstantFeedbackSource Implements IUserPermissionSchedule.GCFunctionalUnitDataSource
        Set(value As XPInstantFeedbackSource)
            INDgcFunctionalUnit.DataSource = value
        End Set
    End Property

    Public WriteOnly Property SLEPositionDataSource As XPInstantFeedbackSource Implements IUserPermissionSchedule.SLEPositionDataSource
        Set(value As XPInstantFeedbackSource)
            INDslePosition.Properties.DataSource = value
        End Set
    End Property

    Public WriteOnly Property SLEFunctionalUnitDataSource As XPInstantFeedbackSource Implements IUserPermissionSchedule.SLEFunctionalUnitDataSource
        Set(value As XPInstantFeedbackSource)
            INDsleFunctionalUnit.Properties.DataSource = value
        End Set
    End Property

    Private dtFunctionalUnit As New DataTable()
    Private dtPosition As New DataTable()

    Private Sub FrmUserPermissionsSchedule_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _Presenter = New PUserPermissionsSchedule(Me)
        InitDataTables()
        _Presenter.LoadForm()
        INDsleUserType.EditValue = "Roles"


        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView2.SetListAcction(INDgvPosition, ListActions)
        IndigoGridControl2.RefreshGrid(INDgcPosition)

        IndigoGridView1.SetListAcction(INDgvFunctionalUnit, ListActions)
        IndigoGridControl1.RefreshGrid(INDgcFunctionalUnit)
    End Sub

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.ButtonEdit
        btn = DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
        Select Case btn.Text.ToString
            Case "Eliminar"
                DeleteGCRow()
        End Select
    End Sub

    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.ButtonEdit
        btn = DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
        Select Case btn.Text.ToString
            Case "Eliminar"
                DeletePosition()
        End Select
    End Sub

    Private Sub DeleteGCRow()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim ObjFunctionalUnitResponsible = CType(INDgvFunctionalUnit.GetFocusedRow(), FunctionalUnitResponsible)
            ListDeleteFunctionalUnitResponsible.Add(ObjFunctionalUnitResponsible)
            ListFunctionalUnitResponisble.Remove(ObjFunctionalUnitResponsible)
            INDgcFunctionalUnit.DataSource = Nothing
            INDgcFunctionalUnit.DataSource = ListFunctionalUnitResponisble
        End If
    End Sub

    Private Sub DeletePosition()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If INDlciRole.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                Dim ObjPositionRoll = CType(INDgvPosition.GetFocusedRow(), PositionRoll)
                ListDeletePositionRol.Add(ObjPositionRoll)
                ListPositionRol.Remove(ObjPositionRoll)
                INDgcPosition.DataSource = Nothing
                INDgcPosition.DataSource = ListPositionRol
            Else
                Dim ObjPositionUser = CType(INDgvPosition.GetFocusedRow(), PositionUser)
                ListDeletePositionUser.Add(ObjPositionUser)
                ListPositionUser.Remove(ObjPositionUser)
                INDgcPosition.DataSource = Nothing
                INDgcPosition.DataSource = ListPositionUser
            End If
        End If
    End Sub


    Private Sub InitDataTables()
        dtFunctionalUnit.Columns.Add("ID", GetType(Integer))
        dtFunctionalUnit.Columns.Add("Code", GetType(String))
        dtFunctionalUnit.Columns.Add("Name", GetType(String))
        dtPosition.Columns.Add("ID", GetType(Integer))
        dtPosition.Columns.Add("Code", GetType(String))
        dtPosition.Columns.Add("Name", GetType(String))
    End Sub

    Private Sub INDsleUserType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleUserType.EditValueChanged
        dtFunctionalUnit.Rows.Clear()
        dtPosition.Rows.Clear()
        ClearControls()
        Select Case INDsleUserType.EditValue
            Case "Roles"
                ShowRoleLayout()
            Case "Usuario"
                ShowUserLayout()
        End Select
    End Sub

    Private Sub ShowUserLayout()
        ShowEntityLayout(True, False, False)
    End Sub

    Private Sub ShowEntityLayout(v1 As Boolean, v2 As Boolean, v3 As Boolean)
        INDlciRole.HideControl(v1)
        INDlciUser.HideControl(v2)
        INDlcgFunctionalUnit.HideControl(v3)
    End Sub

    Private Sub ShowRoleLayout()
        ShowEntityLayout(False, True, True)
    End Sub

    Private Sub INDsbAgregarFunctionalUnit_Click(sender As Object, e As EventArgs) Handles INDsbAgregarFunctionalUnit.Click
        If INDsleFunctionalUnit.EditValue IsNot String.Empty Then
            AddFunctionalUnitToGC()
        End If
    End Sub

    Private Sub AddFunctionalUnitToGC()
        If INDsleFunctionalUnit.EditValue IsNot Nothing Then
            ListFunctionalUnitResponisble = INDgcFunctionalUnit.DataSource

            If ListFunctionalUnitResponisble IsNot Nothing AndAlso ListFunctionalUnitResponisble.Count > 0 Then
                If ListFunctionalUnitResponisble.Any(Function(x) x.FunctionalUnitId = INDsleFunctionalUnit.EditValue) Then
                    Mensaje(EeventViewerImages.Advertencia) = "La Unidad Funcional ya se encuentra agregada"
                    Exit Sub
                End If

            End If

            If INDsleFunctionalUnit.GetSelectedDataRow Is Nothing Then
                Exit Sub
            End If

            Dim objetoSeleccion = DirectCast(DirectCast(INDsleFunctionalUnit.GetSelectedDataRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.PayrollRepository.PayrollFunctionalUnit)

            Dim ObjFunctionalUnitResponsible As New FunctionalUnitResponsible()
            ObjFunctionalUnitResponsible.CodeFunctionalUnit = objetoSeleccion.Codigo
            ObjFunctionalUnitResponsible.NameFunctionalUnit = objetoSeleccion.Descripcion
            ObjFunctionalUnitResponsible.FunctionalUnitId = INDsleFunctionalUnit.EditValue
            ObjFunctionalUnitResponsible.UserId = INDsleUser.EditValue

            If ListFunctionalUnitResponisble Is Nothing Then
                ListFunctionalUnitResponisble = New List(Of FunctionalUnitResponsible)
            End If

            ListFunctionalUnitResponisble.Add(ObjFunctionalUnitResponsible)

            INDsleFunctionalUnit.Text = String.Empty
            INDgcFunctionalUnit.DataSource = Nothing
            INDgcFunctionalUnit.DataSource = ListFunctionalUnitResponisble

        End If
    End Sub


    Private Sub INDsbAgregarPosition_Click(sender As Object, e As EventArgs) Handles INDsbAgregarPosition.Click
        If INDslePosition.EditValue IsNot String.Empty Then
            AddPositionRoll()
        End If
    End Sub

    Private Sub AddPositionRoll()
        If INDlciRole.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDslePosition.EditValue IsNot Nothing Then
                ListPositionRol = INDgcPosition.DataSource

                If ListPositionRol IsNot Nothing AndAlso ListPositionRol.Count > 0 Then
                    If ListPositionRol.Any(Function(x) x.IdPosition = INDslePosition.EditValue) Then
                        Mensaje(EeventViewerImages.Advertencia) = "El Cargo ya se encuentra agregado"
                        Exit Sub
                    End If

                End If

                If INDslePosition.GetSelectedDataRow Is Nothing Then
                    Exit Sub
                End If

                Dim objetoSeleccion = DirectCast(DirectCast(INDslePosition.GetSelectedDataRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.PayrollRepository.PayrollPositionXpo)
                Dim objetoRol = DirectCast(DirectCast(INDsleRole.GetSelectedDataRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.SecurityRepository.Security_Roll)

                Dim ObjPositionRoll As New PositionRoll()
                ObjPositionRoll.IdPosition = INDslePosition.EditValue
                ObjPositionRoll.CodePosition = objetoSeleccion.Codigo
                ObjPositionRoll.NamePosition = objetoSeleccion.Descripcion
                ObjPositionRoll.CodeRol = objetoRol.RollCode
                ObjPositionRoll.IdRol = INDsleRole.EditValue

                If ListPositionRol Is Nothing Then
                    ListPositionRol = New List(Of PositionRoll)
                End If

                ListPositionRol.Add(ObjPositionRoll)

                INDslePosition.Text = String.Empty
                INDgcPosition.DataSource = Nothing
                INDgcPosition.DataSource = ListPositionRol

            End If
        Else
            If INDslePosition.EditValue IsNot Nothing Then
                ListPositionUser = INDgcPosition.DataSource

                If ListPositionUser IsNot Nothing AndAlso ListPositionUser.Count > 0 Then
                    If ListPositionUser.Any(Function(x) x.IdPosition = INDslePosition.EditValue) Then
                        Mensaje(EeventViewerImages.Advertencia) = "El Cargo ya se encuentra agregado"
                        Exit Sub
                    End If

                End If

                Dim objetoSeleccion = DirectCast(DirectCast(INDslePosition.GetSelectedDataRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.PayrollRepository.PayrollPositionXpo)
                Dim objetoUser = DirectCast(DirectCast(INDsleUser.GetSelectedDataRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Object)

                Dim ObjPositionUser As New PositionUser()
                ObjPositionUser.IdPosition = INDslePosition.EditValue
                ObjPositionUser.CodePosition = objetoSeleccion.Codigo
                ObjPositionUser.NamePosition = objetoSeleccion.Descripcion
                ObjPositionUser.IdUser = INDsleUser.EditValue
                ObjPositionUser.CodeUser = objetoUser.UserCode

                If ListPositionUser Is Nothing Then
                    ListPositionUser = New List(Of PositionUser)
                End If

                ListPositionUser.Add(ObjPositionUser)

                INDslePosition.Text = String.Empty
                INDgcPosition.DataSource = Nothing
                INDgcPosition.DataSource = ListPositionUser

            End If
        End If
    End Sub


    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
        BarraBotones.PrepareToolbar(eAction.OnlyUndoAndAudit)
    End Sub


    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    Private Async Sub Guardar()
        Try

            AsyncLoader(True)

            Dim model As New MUserPermissionSchedule(Me.Tag)
            If INDlciUser.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                Dim ListPermissionUser As List(Of PositionUser) = INDgcPosition.DataSource
                Dim ListFunctionalUnitResponsible As List(Of FunctionalUnitResponsible) = INDgcFunctionalUnit.DataSource
                Dim ObjResult = Await model.SavePermissionUser(ListFunctionalUnitResponsible, ListDeleteFunctionalUnitResponsible, ListPermissionUser, ListDeletePositionUser)
                AsyncLoader(False)
                If ObjResult.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = "La Información se ha Actualizado Correctamente"
                End If
            Else
                Dim ListPermissionRoll As List(Of PositionRoll) = INDgcPosition.DataSource
                Dim ObjResult = Await model.SavePermissionRoll(ListPermissionRoll, ListDeletePositionRol)
                AsyncLoader(False)
                If ObjResult.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = "La Información se ha Actualizado Correctamente"
                End If

            End If
            ClearControls()
            ActionsOnControls = False
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = ex.Message.ToString()
        End Try

    End Sub


    Private Function GetListPositionRollFromGridControl() As List(Of PositionRoll)
        Dim listPR As New List(Of Domain.Payroll.Entities.PositionRoll)
        For Each dRow As DataRow In dtPosition.Rows
            Dim pr As New Domain.Payroll.Entities.PositionRoll
            pr.IdRol = INDsleRole.EditValue
            pr.IdPosition = dRow("ID")
            pr.CodeRol = dRow("Code")
            listPR.Add(pr)
        Next
        Return listPR
    End Function

    Private Function GetListPositionUserFromGridControl() As List(Of PositionUser)
        Dim listPU As New List(Of Domain.Payroll.Entities.PositionUser)
        For Each dRow As DataRow In dtPosition.Rows
            Dim pu As New Domain.Payroll.Entities.PositionUser
            pu.IdUser = INDsleUser.EditValue
            pu.IdPosition = dRow("ID")
            pu.CodeUser = dRow("Code")
            listPU.Add(pu)
        Next
        Return listPU
    End Function

    Private Sub ClearControls()
        INDsleUser.EditValue = ""
        INDsleFunctionalUnit.EditValue = ""
        INDslePosition.EditValue = ""
        INDgcFunctionalUnit.DataSource = Nothing
        INDgcPosition.DataSource = Nothing
    End Sub

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean
        Set(value As Boolean)
            INDlciUserPermission.BeginUpdate()

            INDgcPosition.Enabled = value
            INDgcFunctionalUnit.Enabled = value
            INDslePosition.Enabled = value
            INDsleFunctionalUnit.Enabled = value
            INDsbAgregarFunctionalUnit.Enabled = value
            INDsbAgregarPosition.Enabled = value

            INDlciUserPermission.EndUpdate()

        End Set
    End Property


#Region "EditValueChanged"
    Private Sub INDsleRole_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleRole.EditValueChanged

        If CStr(INDsleRole.EditValue) = String.Empty Then
            Exit Sub
        End If


        If INDsleRole.GetSelectedDataRow Is Nothing Then
            Exit Sub
        End If

        Dim objetoSeleccion = DirectCast(DirectCast(INDsleRole.GetSelectedDataRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.SecurityRepository.Security_Roll)
        LoadGridControlsForRole(INDsleRole.EditValue, objetoSeleccion.RollCode)

        INDLciGridPosition.Enabled = True
    End Sub

    Private Sub INDsleUser_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleUser.EditValueChanged
        If CStr(INDsleUser.EditValue) = String.Empty Then
            Exit Sub
        End If

        LoadGridControlsForUser(INDsleUser.EditValue)

        INDLciGridPosition.Enabled = True
    End Sub

#End Region
#Region "LoadGrid"
    Private Async Sub LoadGridControlsForRole(pRoleID As Integer, RoleCode As String)
        AsyncLoader(True)
        Await LoadGCPosition(pRoleID, RoleCode)
        AsyncLoader(False)
    End Sub

    Private Async Sub LoadGridControlsForUser(UserId As Integer)
        AsyncLoader(True)
        Await LoadGCUser(UserId)
        AsyncLoader(False)
    End Sub
#End Region

#Region "Load"
    Private Async Function LoadGCPosition(RoleId As Integer, RoleCode As String) As Task
        BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
        Using Model As New MUserPermissionSchedule(Me.Tag)
            ListPositionRol = Await Model.GetListPositionByRoleID(RoleId, RoleCode)
            INDgcPosition.DataSource = ListPositionRol
            ActionsOnControls = True
        End Using
    End Function

    Private Async Function LoadGCUser(UserId As Integer) As Task
        BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
        Using Model As New MUserPermissionSchedule(Me.Tag)
            ListPositionUser = Await Model.GetListPositionUser(UserId)
            INDgcPosition.DataSource = ListPositionUser
            ListFunctionalUnitResponisble = Await Model.GetListFunctionalUnitResponsible(UserId)
            INDgcFunctionalUnit.DataSource = ListFunctionalUnitResponisble
            ActionsOnControls = True
        End Using
    End Function
#End Region
#Region "ICRUD"
    Public Sub Buscar() Implements IcrudBase.Buscar
        Throw New NotImplementedException()
    End Sub

    Private Sub IcrudBase_Guardar() Implements IcrudBase.Guardar
        Throw New NotImplementedException()
    End Sub

    Public Sub Nuevo() Implements IcrudBase.Nuevo
        Throw New NotImplementedException()
    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer
        Throw New NotImplementedException()
    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar
        Throw New NotImplementedException()
    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        Throw New NotImplementedException()
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar
        Throw New NotImplementedException()
    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
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

#End Region

End Class