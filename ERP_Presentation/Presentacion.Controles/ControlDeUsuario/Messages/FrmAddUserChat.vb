'***********************************************************************
' Assembly         : Presentacion.Controles
' Author           : Jorge Leonardo Vernaza
' Created          : 1-11-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Presentation.Controls.MVP
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base

#End Region
''' <summary>
''' Clase con la funcionalidad del frontal para agregar usuarios al grupo
''' </summary>
Public Class FrmAddUserChat

#Region "Variables Propiedades"
    ''' <summary>
    ''' Objeto para instanciar el modelo
    ''' </summary>
    Dim Model As MAddUserChat
    ''' <summary>
    ''' Objeto con la instancia de las variables de session
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' Listado que contiene el listado
    ''' </summary>
    Dim ListGroups As List(Of GroupUser)
#End Region

#Region "Metodos"
    ''' <summary>
    ''' Evento load del formulario.
    ''' </summary>
    Private Async Sub FrmAddUserChat_Load(sender As Object, e As EventArgs) Handles Me.Load
        Model = New MAddUserChat
        ListGroups = Await Model.ListGroupsAsync
        GetUsersByName(String.Empty)
        INDgcGroups.DataSource = ListGroups
        IndigoGridControl1.SetHoldSize(INDgcGroups, True)
        IndigoGridControl1.SetHoldSize(INDgcUsers, True)
    End Sub

    ''' <summary>
    ''' Evento clic del boton agregar.
    ''' </summary>
    Private Async Sub INDbtnAdd_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click
        Await Save()
    End Sub

    ''' <summary>
    ''' Evento TextChange para cambiar si la rejilla de los grupos se habilita si no hay ningun grupo para crear o se desabilita si va crear un grupo.
    ''' </summary>
    Private Sub INDtxtGroupName_TextChanged(sender As Object, e As EventArgs) Handles INDtxtGroupName.TextChanged
        If INDtxtGroupName.Text <> String.Empty Then
            INDgcGroups.Enabled = False
        Else
            INDgcGroups.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Evento TextChanged donde enviamos a consultar los usuarios por nombre.
    ''' </summary>
    Private Sub INDbtnUserName_TextChanged(sender As Object, e As System.EventArgs) Handles INDbtnUserName.TextChanged
        INDgcvUsers.ApplyFindFilter(INDbtnUserName.Text)
    End Sub

    ''' <summary>
    ''' Evento ButtonClick donde enviamos a consultar los usuarios por nombre.
    ''' </summary>
    Private Sub INDbtnUserName_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbtnUserName.ButtonClick
      INDgcvUsers.ApplyFindFilter(INDbtnUserName.Text)
    End Sub

    ''' <summary>
    ''' Metodo para obtener el listado de usuarios por nombre
    ''' </summary>
    ''' <param name="UserName">Name of the user.</param>
    Private Sub GetUsersByName(ByVal UserName As String)
        INDgcUsers.DataSource = Model.GetUserByName(UserName)
    End Sub

    ''' <summary>
    ''' Metodo para guardar el usuario en un grupo determinado.
    ''' </summary>
    Private Async Function Save() As Threading.Tasks.Task
        If INDtxtGroupName.Text <> String.Empty Then
            Dim GroupUser As New GroupUser
            With GroupUser
                .IdUser = Indigo.UserIndigoId
                .Name = INDtxtGroupName.Text
                Dim UserAdd As New UsersGroupUser
                UserAdd.IdUser = INDgcvUsers.GetFocusedRowCellValue("Id")
                If ValidatingUser(UserAdd.IdUser) = False Then
                    MessageIndigo.Show("El usuario ya esta en uno de sus grupos", MessageType.Warning, "Agregar Usuario")
                    Exit Function
                End If
                .UsersGroupUser = New Domain.Base.Entities.TrackableCollection(Of UsersGroupUser)
                .UsersGroupUser.Add(UserAdd)
            End With
            Await Model.SaveGroup(GroupUser)
            Me.Close()
        Else
            If INDgcvUsers.FocusedRowHandle >= 0 Then
                Dim UserAdd As New UsersGroupUser
                UserAdd.IdUser = INDgcvUsers.GetFocusedRowCellValue("Id")
                UserAdd.IdGroupUser = INDgcvGroups.GetFocusedRowCellValue("Id")
                If ValidatingUser(UserAdd.IdUser) = False Then
                    MessageIndigo.Show("El usuario ya esta en uno de sus grupos", MessageType.Warning, "Agregar Usuario")
                    Exit Function
                End If
                Await Model.AddUserGroup(UserAdd)
                Me.Close()
            End If
        End If
    End Function

    ''' <summary>
    ''' Validar si el usuari ya existe en un grupo
    ''' </summary>
    ''' <param name="IdUser">The id user.</param>
    ''' <returns></returns>
    Private Function ValidatingUser(ByVal IdUser As String) As Boolean
        For Each Group As GroupUser In ListGroups
            If Group.UsersGroupUser.Where(Function(x) x.IdUser = IdUser).ToList.Count > 0 Then
                Return False
            End If
        Next
        Return True
    End Function

#End Region




End Class