'***********************************************************************
' Assembly         : Presentacion.AccoungManagement
' Author           : Andres Felipe Quintero Garcia 
' Created          : 09-01-2025
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Infrastructure.Data.Xpo
Imports Presentation.AccountManagement.MVP
Imports Presentation.Controls
Imports System.Text
Imports Domain.Base.Entities
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid
Imports System.Windows.Forms
Imports System.Runtime.CompilerServices
#End Region

Public Class FrmAccountManagementParameters
    Implements IAccountManagementParameters


#Region "Properties"
    ''' <summary>
    ''' Obtiene o establece la validacion automatica.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValidateAutomaticAssignment As Boolean Implements IAccountManagementParameters.ValidateAutomaticAssignment
        Get
            Return INDGleAutomaticAssignment.EditValue
        End Get
        Set(value As Boolean)
            INDGleAutomaticAssignment.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha de inicio asignacion 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property StartDateAssignment As Date? Implements IAccountManagementParameters.StartDateAssignment
        Get
            Return INDDteStartDateAssignment.EditValue
        End Get
        Set(value As Date?)
            INDDteStartDateAssignment.EditValue = value
        End Set
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IAccountManagementParameters.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
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
    ''' Obtiene o establece la Tupla
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ListAutomaticAssignment As List(Of Tuple(Of Byte, String))
        Get
            If _listAutomaticAssignment Is Nothing Then
                _listAutomaticAssignment = New List(Of Tuple(Of Byte, String))
                _listAutomaticAssignment.Add(New Tuple(Of Byte, String)(1, "Si"))
                _listAutomaticAssignment.Add(New Tuple(Of Byte, String)(0, "No"))
            End If
            Return _listAutomaticAssignment
        End Get
    End Property

    Public ReadOnly Property ListTypeIncome As List(Of Tuple(Of Byte, String))
        Get
            If _listTypeIncome Is Nothing Then
                _listTypeIncome = New List(Of Tuple(Of Byte, String))
                _listTypeIncome.Add(New Tuple(Of Byte, String)(1, "Ambulatorio"))
                _listTypeIncome.Add(New Tuple(Of Byte, String)(2, "Hospitalario"))
            End If

            Return _listTypeIncome
        End Get
    End Property

    Public ReadOnly Property ListTypeAssignment As List(Of Tuple(Of Byte, String))
        Get
            If _listTypeAssignment Is Nothing Then
                _listTypeAssignment = New List(Of Tuple(Of Byte, String))
                _listTypeAssignment.Add(New Tuple(Of Byte, String)(1, "Observacion Urgencias"))
                _listTypeAssignment.Add(New Tuple(Of Byte, String)(2, "Recuperacion Post-Quirurgico"))
                _listTypeAssignment.Add(New Tuple(Of Byte, String)(3, "Hospitalario"))
                _listTypeAssignment.Add(New Tuple(Of Byte, String)(4, "Cuna de Observación"))
            End If
            Return _listTypeAssignment
        End Get
    End Property

#End Region

#Region "Variables"
    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' 
    ''' </summary>
    Private _loadingControls As Boolean

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As BlockRecordAccountManagement

    ''' <summary>
    ''' Representa la entidad
    ''' </summary>
    Private _accountManagementParameters As AccountManagementParameters

    ''' <summary>
    ''' Entidad de los usuarios facturadores
    ''' </summary>
    Private _usersAssignment As UsersAssignment

    ''' <summary>
    ''' Lista de los usuarios facturadores para poder gestionar los controles y el guardado
    ''' </summary>
    Private _usersAssignmentList As List(Of UsersAssignment)

    ''' <summary>
    ''' Lista de novedades de cada usuario asignado en gestión de cuentas
    ''' </summary>
    Private _userNoveltiesList As List(Of UserNovelties)

    ''' <summary>
    ''' Lista de usuarios asignados a eliminar
    ''' </summary>
    Private _listDeleteUsersAssignment As List(Of UsersAssignment)

    ''' <summary>
    ''' Variable que contiene la lista asignacion automatica
    ''' </summary>
    Private _listAutomaticAssignment As List(Of Tuple(Of Byte, String))

    ''' <summary>
    ''' Variable que contiene la lista tipo de ingreso
    ''' </summary>
    Dim _listTypeIncome As List(Of Tuple(Of Byte, String))

    ''' <summary>
    ''' Variable que contiene la lista de camas
    ''' </summary>
    Dim _listTypeAssignment As List(Of Tuple(Of Byte, String))

    ''' <summary>
    ''' Variable global para guardar el usuario seleccionado y poder enviarlo al popup de novedades
    ''' </summary>
    Dim _gridSelectedUser As UsersAssignment

    ''' <summary>
    ''' Index de la novedad dentro de la lista que lo contiene
    ''' </summary>
    Dim _noveltyIndex As Integer

#End Region

#Region "ICrud"
    ''' <summary>
    ''' Método : Buscar
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar

    End Sub

    ''' <summary>
    ''' Método : Deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Método : Eliminar
    ''' </summary>
    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' Método : Guardar
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        Try

            Dim validate = validatecontrol()
            If Not validate.StateResult Then
                Mensaje(EeventViewerImages.Advertencia) = validate?.Message
                Exit Sub
            End If

            'Se ligan los usuarios asignados y las novedades al objeto general para su guardado posterior
            _accountManagementParameters.usersAssignment = _usersAssignmentList
            _accountManagementParameters.userNovelties = _userNoveltiesList

            AssignValues()
            Using model As New MAccountManagementParameters(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await model.SaveAccountManagementParameters(_accountManagementParameters, Me.indigo.AuditMessageWcf)
                If Result.StateResult Then

                    Me._accountManagementParameters = Result.ObjectEmbbeded
                    AsyncLoader(False)
                    CleanControls()
                    Await LoadControls()
                    INDGleAutomaticAssignment.Focus()


                    Me.BarraBotones.CleanAuditBasic()
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), Result.ObjectEmbbeded.CreationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), Result.ObjectEmbbeded.CreationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), Result.ObjectEmbbeded.ModificationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), Result.ObjectEmbbeded.ModificationDate)
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True

                    If Result.MessageResult?.Count > 0 Then
                        Mensaje(EeventViewerImages.Informacion) = "Parámetros actualizados correctamente." & vbNewLine & String.Join(Environment.NewLine, Result.MessageResult)
                    Else
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                Else
                    AsyncLoader(False)
                    If Result.MessageResult Is Nothing Then
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    ElseIf Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = $"Error: {ex.Message}{Environment.NewLine}{ex.StackTrace}"
        End Try
    End Sub

    ''' <summary>
    ''' Método : Actualizar
    ''' </summary>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Método : Nuevo
    ''' </summary>
    Public Sub Nuevo() Implements ICrudBase.Nuevo
    End Sub

    ''' <summary>
    ''' Método : OpenSearch
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch

    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' valida los controles del formulario
    ''' </summary>
    Private Function validatecontrol() As ActionResult
        If ValidateAutomaticAssignment Then

            If String.IsNullOrEmpty(_selectorEntryType?.GetKeys()) Then
                Return New ActionResult With {.StateResult = False, .Message = "Seleccione el tipo de ingreso."}
            End If

            Dim stringBuilder = New StringBuilder
            If (_selectorEntryType.GetKeys().Contains("1") OrElse _selectorEntryType.GetKeys().Contains("2")) AndAlso StartDateAssignment Is Nothing Then
                stringBuilder.AppendLine("Ingrese la fecha de asignación.")
            End If

            If Me._selectorEntryType.GetKeys().Contains("2") AndAlso String.IsNullOrEmpty(_selectorTypeAssignament.GetKeys()) Then
                stringBuilder.AppendLine("Seleccione el tipo de asignación.")
            End If

            If stringBuilder.Length > 0 Then
                Return New ActionResult With {.StateResult = False, .Message = stringBuilder.ToString()}
            End If

        End If
        Return New ActionResult With {.StateResult = True}
    End Function

    ''' <summary>
    ''' Metodo que inicializa el datasource de las tuplas
    ''' </summary>
    Private Sub InitializeTuples()
        INDGleAutomaticAssignment.Properties.DataSource = ListAutomaticAssignment
        INDGleEntryType.Properties.DataSource = ListTypeIncome
        INDGleTypeAssignment.Properties.DataSource = ListTypeAssignment
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        Using Model As New MAccountManagementParameters(CStr(Me.Tag))
            AsyncLoader(True)
            Try
                Dim resulOperation = Await Model.GetAccountManagementParameters(Me._idOperativeUnit)
                _accountManagementParameters = resulOperation.ObjectEmbbeded

                'Se asignan los datasource respectivos y se inicializan las listas globales
                _usersAssignmentList = If(_accountManagementParameters.usersAssignment, New List(Of UsersAssignment))
                INDGcUsersAssignment_Hosp.DataSource = _usersAssignmentList.Where(Function(x) x.EntryType = 2 AndAlso Not x.IsRemoved)
                INDGcUsersAssignment_Amb.DataSource = _usersAssignmentList.Where(Function(x) x.EntryType = 1 AndAlso Not x.IsRemoved)
                INDGcUsersAssignment_Hosp.RefreshDataSource()
                INDGcUsersAssignment_Amb.RefreshDataSource()

                _userNoveltiesList = _accountManagementParameters.userNovelties

                _listDeleteUsersAssignment = New List(Of UsersAssignment)

                If _accountManagementParameters IsNot Nothing AndAlso _accountManagementParameters.Id > 0 Then
                    Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                        Dim result = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_accountManagementParameters.Id))

                        _loadingControls = True
                        With _accountManagementParameters
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)

                            Me.BarraBotones.CleanAuditBasic()
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                            Me.INDGleEntryType.EditValue = Nothing
                            Me.INDGleTypeAssignment.EditValue = Nothing
                            Me.INDGleAutomaticAssignment.EditValue = .AutomaticAssignment

                            Me.INDDteStartDateAssignment.EditValue = .StartDateAssignment

                            If Not String.IsNullOrEmpty(.EntryType) Then
                                Dim listEntryType = ListTypeIncome.FindAll(Function(x) .EntryType.Split(",").ToList().Contains(x.Item1))

                                For Each item In listEntryType
                                    Me._selectorEntryType.SetValue(item, True)
                                Next
                            End If

                            If Not String.IsNullOrEmpty(.BedClass) Then
                                Dim listBedClass = ListTypeAssignment.FindAll(Function(x) .BedClass.Split(",").ToList().Contains(x.Item1))

                                For Each item In listBedClass
                                    Me._selectorTypeAssignament.SetValue(item, True)
                                Next
                            End If
                            INDGleTypeIncome_EditValueChanged(Nothing, Nothing)

                            Me.INDGleEntryType.Properties.NullText = .EntryTypeName
                            Me.INDGleTypeAssignment.Properties.NullText = .BedClassName

                        End With
                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._accountManagementParameters.Id)
                        If result.Id = 0 Then
                            Dim state = New Domain.Base.Entities.ObjectChangeTracker
                            state.State = Domain.Base.Entities.ObjectState.Added
                            _record = New BlockRecordAccountManagement With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = CInt(Me.Tag), .CodUser = Me.indigo.UserIndigo, .IdRecord = _accountManagementParameters.Id}
                            Dim operation = Await ModelRecord.SaveBlockRecord(_record)
                            _record = operation.ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
                            _record = result
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                        End If
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
                        Me.BarraBotones.SetDocuments(_accountManagementParameters.Id)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
                    End Using
                    _loadingControls = False
                Else
                    _usersAssignmentList = New List(Of UsersAssignment)
                    _usersAssignmentList = New List(Of UsersAssignment)
                    _userNoveltiesList = New List(Of UserNovelties)

                    BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
                End If
            Catch ex As Exception
                Throw ex
            Finally
                AsyncLoader(False)
            End Try
        End Using

    End Function

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssignValues()
        With _accountManagementParameters
            .AutomaticAssignment = ValidateAutomaticAssignment
            .EntryType = _selectorEntryType.GetKeys()
            .BedClass = _selectorTypeAssignament.GetKeys()
            .StartDateAssignment = StartDateAssignment
        End With
    End Sub

    ''' <summary>
    ''' Se configura el estado inicial de visualización de cada layout
    ''' </summary> 
    Private Sub ConfigureLayoutVisibility()
        Dim automaticAssignmentValue As Byte = CByte(INDGleAutomaticAssignment.EditValue)
        If automaticAssignmentValue = 0 Then
            INDLcEntryType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLyDteStartDateAssignment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLcTypeAssignment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlygUsersAssignment_Hosp.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlygUsersAssignment_Amb.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlygNovelties.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            INDLcEntryType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLyDteStartDateAssignment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLcTypeAssignment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    Private Sub CleanControls()
        INDLcRoot.BeginUpdate()
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.BarraBotones.StatusRecordVisible = False
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        StartDateAssignment = Nothing
        _accountManagementParameters = Nothing
        INDLcRoot.EndUpdate()
    End Sub

    ''' <summary>
    ''' Función de previa validación a agregar un usuario a la lista de usuarios asignados tipo hospitalario (2)
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateSleHospControls() As Boolean
        If INDSleHosp.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un usuario para asignación hospitalaria."
            INDSleHosp.Focus()
            Return False
        End If

        If _usersAssignmentList IsNot Nothing AndAlso _usersAssignmentList.Any(Function(x) x.UserCode = INDSleGridViewHosp.GetFocusedRowCellValue("UserCode") AndAlso x.EntryType = 2 AndAlso Not x.IsRemoved) Then
            Mensaje(EeventViewerImages.Advertencia) = "El usuario ya se encuentra en el listado"
            INDSleHosp.Focus()
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Función de previa validación a agregar un usuario a la lista de usuarios asignados tipo ambulatorio (1)
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateSleAmbControls() As Boolean
        If INDSleAmb.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un usuario para asignación ambulatoria."
            INDSleAmb.Focus()
            Return False
        End If

        If _usersAssignmentList IsNot Nothing AndAlso _usersAssignmentList.Any(Function(x) x.UserCode = INDSleGridAmb.GetFocusedRowCellValue("UserCode") AndAlso x.EntryType = 1 AndAlso Not x.IsRemoved) Then
            Mensaje(EeventViewerImages.Advertencia) = "El usuario ya se encuentra en el listado"
            INDSleAmb.Focus()
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Función que crea la columna de acciones para los GridControls que lo requieran.
    ''' </summary>
    Private Sub AddActionsColumns()
        IndigoGridViewHosp.SetListAcction(INDGvUsersAssignment_Hosp, {eAcciones.AddNovelty, eAcciones.Edit, eAcciones.Remove}.ToList())
        IndigoGridViewAmb.SetListAcction(INDGvUsersAssignment_Amb, {eAcciones.AddNovelty, eAcciones.Edit, eAcciones.Remove}.ToList())
        IndigoGridViewNovelty.SetListAcction(INDGvNovelties, {eAcciones.Edit}.ToList())

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvUsersAssignment_Hosp.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvUsersAssignment_Amb.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvNovelties.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

    ''' <summary>
    ''' Función para remover un usuario del GridControl de asignación hospitalaria
    ''' </summary>
    ''' <param name="assignedUserHosp"></param>
    Private Sub RemoveAssignedUserHosp(assignedUserHosp As UsersAssignment)
        Dim entity = _usersAssignmentList.FirstOrDefault(Function(x) x.Id = assignedUserHosp.Id AndAlso x.EntryType = assignedUserHosp.EntryType)
        If entity IsNot Nothing Then
            entity.IsRemoved = True
            entity.MarkAsModified()
            If assignedUserHosp.Id > 0 Then
                If _listDeleteUsersAssignment Is Nothing Then
                    _listDeleteUsersAssignment = New List(Of UsersAssignment)
                End If
                _listDeleteUsersAssignment.Add(entity)
            End If
        End If
        INDGcUsersAssignment_Hosp.DataSource = _usersAssignmentList.Where(Function(x) Not x.IsRemoved AndAlso x.EntryType = 2).ToList()
        INDGcUsersAssignment_Hosp.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Función para remover un usuario del GridControl de asignación ambulatoria
    ''' </summary>
    ''' <param name="assignedUserAmb"></param>
    Private Sub RemoveAssignedUserAmb(assignedUserAmb As UsersAssignment)
        Dim entity = _usersAssignmentList.FirstOrDefault(Function(x) x.Id = assignedUserAmb.Id AndAlso x.EntryType = assignedUserAmb.EntryType)
        If entity IsNot Nothing Then
            entity.IsRemoved = True
            entity.MarkAsModified()
            If assignedUserAmb.Id > 0 Then
                If _listDeleteUsersAssignment Is Nothing Then
                    _listDeleteUsersAssignment = New List(Of UsersAssignment)
                End If
                _listDeleteUsersAssignment.Add(entity)
            End If
        End If
        INDGcUsersAssignment_Amb.DataSource = _usersAssignmentList.Where(Function(x) Not x.IsRemoved AndAlso x.EntryType = 1).ToList()
        INDGcUsersAssignment_Amb.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Función que muestra el PopUp de Novedades
    ''' </summary>
    ''' <param name="selectedUser">Usuario al que se agregará la novedad</param>
    ''' <param name="Novelty">Opcional, si se agrega es porque se desea editar una Novedad</param>
    Private Sub InstantiatePopup(selectedUser As UsersAssignment, Optional Novelty As UserNovelties = Nothing)
        Me.Cursor = ChangeCursorIndigo()

        'Se establece el indice de la novedad seleccionada para ser editada
        If Novelty IsNot Nothing Then
            _noveltyIndex = _userNoveltiesList.IndexOf(Novelty)
        End If

        Using form As New FrmUserNoveltiesPopup(selectedUser, Novelty)
            AddHandler form.AddUserNovelty, AddressOf ReturnPopupNovelty

            form.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            form.ViewModeEditHold = True
            If selectedUser Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Usuario vacío"
                Exit Sub
            End If

            form.StartPosition = FormStartPosition.CenterParent
            form.Size = New System.Drawing.Size(800, 700)
            Dim transparent = New FrmTransparent(form, False)
            Me.Cursor = Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Función que se encarga de recibir la respuesta del evento AddUserNovelty desde el formulario FrmUserNoveltiesPopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="novelty"></param>
    ''' <param name="editFlag"></param>
    Private Sub ReturnPopupNovelty(sender As Object, novelty As UserNovelties, editFlag As Boolean)
        If _userNoveltiesList Is Nothing Then
            _userNoveltiesList = New List(Of UserNovelties)
        End If

        If editFlag = True Then
            'Se actualiza la lista de novedades si fue editada.
            _userNoveltiesList.Remove(novelty)
            _userNoveltiesList.Insert(_noveltyIndex, novelty)
        Else
            _userNoveltiesList.Add(novelty)
        End If

        ' Actualizar el status del usuario basándose en la novedad
        ' La propiedad IsUserActive de la novedad determina si el usuario está activo o no
        If _gridSelectedUser IsNot Nothing Then
            ' Actualizar el status del usuario seleccionado
            _gridSelectedUser.Status = novelty.IsUserActive

            ' Refrescar el GridView para mostrar el cambio de status
            INDGcUsersAssignment_Hosp.RefreshDataSource()
            INDGcUsersAssignment_Amb.RefreshDataSource()
        End If

        INDGcNovelties.DataSource = _userNoveltiesList.Where(Function(x) x.AssignedUserId = _gridSelectedUser.Id)
        INDGcNovelties.RefreshDataSource()
    End Sub

#End Region

#Region "Events"

    Private _selectorEntryType As SelectorCache = New SelectorCache("Item1", "Item2")
    Private _selectorTypeAssignament As SelectorCache = New SelectorCache("Item1", "Item2")

    Private Sub INDGvSelector_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles GridLookUpEdit1View.CustomUnboundColumnData, INDGvTypeAssignament.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "GridLookUpEdit1View" Then
                e.Value = _selectorEntryType.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvTypeAssignament" Then
                e.Value = _selectorTypeAssignament.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    Private Sub INDGvSelector_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles GridLookUpEdit1View.RowCellClick, INDGvTypeAssignament.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)
            If view.Name = "GridLookUpEdit1View" Then
                selector = _selectorEntryType
            ElseIf view.Name = "INDGvTypeAssignament" Then
                selector = _selectorTypeAssignament
            End If

            If e.RowHandle >= 0 Then
                Dim row = view.GetRow(e.RowHandle)
                selector.SetValue(row)
            Else
                selector.Clear()
            End If
            view.RefreshData()
        End If
    End Sub

    Private Sub INDSleSelector_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDGleEntryType.Closed, INDGleTypeAssignment.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.GridLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDGleEntryType" Then
            searchLookupEdit.Properties.NullText = _selectorEntryType.ToString()
        ElseIf searchLookupEdit.Name = "INDGleTypeAssignment" Then
            searchLookupEdit.Properties.NullText = _selectorTypeAssignament.ToString()
        End If
    End Sub

#Region "Load"

    ''' <summary>
    ''' Limpieza de recursos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _idOperativeUnit = Nothing
        _loadingControls = Nothing
        _record = Nothing
        _accountManagementParameters = Nothing
        _usersAssignment = Nothing
        _usersAssignmentList = Nothing
        _userNoveltiesList = Nothing
        _listDeleteUsersAssignment = Nothing
        _listAutomaticAssignment = Nothing
        _listTypeIncome = Nothing
        _listTypeAssignment = Nothing
        _gridSelectedUser = Nothing
        _noveltyIndex = Nothing
        ValidateAutomaticAssignment = Nothing
        StartDateAssignment = Nothing
        _selectorEntryType = Nothing
        _selectorTypeAssignament = Nothing
    End Sub

    ''' <summary>
    ''' Se dispara al cargar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmAccountManagementParameters_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDLcRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        InitializeTuples()
        ConfigureLayoutVisibility()
        AddActionsColumns()
        Await LoadControls()
    End Sub
#End Region

#Region "EditValueChanged"

    Private Sub INDGleAutomaticAssignment_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleAutomaticAssignment.EditValueChanged
        ConfigureLayoutVisibility()

        If INDGleAutomaticAssignment.EditValue Is Nothing OrElse Not ValidateAutomaticAssignment Then

            _selectorEntryType.Clear()
            StartDateAssignment = Nothing
            _selectorTypeAssignament.Clear()
            INDDteStartDateAssignment.EditValue = Nothing
        End If
    End Sub


    Private Sub INDGleTypeIncome_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleEntryType.Closed

        Dim ambulatoryFlag As Boolean = _selectorEntryType.GetKeysToArray.Contains(CByte(1)) ''si contiene 1, true or false
        Dim hospitalaryFlag As Boolean = _selectorEntryType.GetKeysToArray.Contains(CByte(2)) ''si contiene 2, true or false

        INDLyDteStartDateAssignment.HideControl(Not (ambulatoryFlag OrElse hospitalaryFlag))
        INDlygUsersAssignment_Amb.HideControl(Not (ambulatoryFlag OrElse hospitalaryFlag) OrElse Not ambulatoryFlag)
        INDlygUsersAssignment_Hosp.HideControl(Not (ambulatoryFlag OrElse hospitalaryFlag) OrElse Not hospitalaryFlag)
        INDlygNovelties.HideControl(Not (ambulatoryFlag OrElse hospitalaryFlag))

        INDLcTypeAssignment.HideControl(Not hospitalaryFlag)

        If _selectorEntryType.Count > 1 Then
            Exit Sub
        End If

        If ambulatoryFlag Then
            _selectorTypeAssignament.Clear()
            INDGleTypeAssignment.Properties.NullText = Nothing
        End If
    End Sub
#End Region

#Region "QueryPopUp"
    ''' <summary>
    ''' Evento QueryPopUp del SearchLookUpEdit del segmento Asignación de usuarios hospitalarios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleHosp_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleHosp.QueryPopUp
        If INDSleHosp.Properties.DataSource Is Nothing Then
            INDSleHosp.Properties.DataSource = XpoServiceEx.Instance(indigo.SecurityContainer).SecurityService.ListActiveAdministrativeUsers()
        End If
    End Sub

    ''' <summary>
    ''' Evento QueryPopUp del SearchLookUpEdit del segmento Asignación de usuarios ambulatorios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleAmb_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleAmb.QueryPopUp
        If INDSleAmb.Properties.DataSource Is Nothing Then
            INDSleAmb.Properties.DataSource = XpoServiceEx.Instance(indigo.SecurityContainer).SecurityService.ListActiveAdministrativeUsers()
        End If
    End Sub
#End Region

#Region "Click"
    ''' <summary>
    ''' Evento click del botón Añadir en el segmento de Asignación de usuarios Hospitalarios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbAddHosp_Click(sender As Object, e As EventArgs) Handles INDSbAddHosp.Click
        If Not ValidateSleHospControls() Then
            Return
        End If

        Dim userCode = INDSleGridViewHosp.GetFocusedRowCellValue("UserCode")

        'Verificar si el usuario existe en la lista pero está marcado como removed
        Dim existingUser = _usersAssignmentList.FirstOrDefault(Function(x) x.UserCode = userCode AndAlso x.EntryType = 2 AndAlso x.IsRemoved)

        If existingUser IsNot Nothing Then
            'Restaurar el usuario existente
            existingUser.IsRemoved = False
            existingUser.MarkAsModified()
            If _listDeleteUsersAssignment.Any(Function(x) x.UserCode = userCode AndAlso x.EntryType = 2) Then
                _listDeleteUsersAssignment.RemoveAll(Function(x) x.UserCode = userCode AndAlso x.EntryType = 2)
            End If
        Else
            'Agregar nuevo usuario
            Dim userAssigned As New UsersAssignment With {
                .UserCode = userCode,
                .FullName = INDSleGridViewHosp.GetFocusedRowCellValue("PersonFullName"),
                .Status = True,
                .EntryType = 2,
                .IsRemoved = False
            }
            _usersAssignmentList.Add(userAssigned)
        End If

        INDGcUsersAssignment_Hosp.DataSource = _usersAssignmentList.Where(Function(x) x.EntryType = 2 AndAlso Not x.IsRemoved)
        INDGcUsersAssignment_Hosp.RefreshDataSource()
        INDSleHosp.EditValue = Nothing
        INDSleHosp.Focus()
    End Sub

    ''' <summary>
    ''' Evento click del botón Añadir en el segmento de Asignación de usuarios Ambulatorios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbAddAmb_Click(sender As Object, e As EventArgs) Handles INDSbAddAmb.Click
        If Not ValidateSleAmbControls() Then
            Return
        End If

        Dim userCode = INDSleGridAmb.GetFocusedRowCellValue("UserCode")

        'Verificar si el usuario existe en la lista pero está marcado como removed
        Dim existingUser = _usersAssignmentList.FirstOrDefault(Function(x) x.UserCode = userCode AndAlso x.EntryType = 1 AndAlso x.IsRemoved)

        If existingUser IsNot Nothing Then
            'Restaurar el usuario existente
            existingUser.IsRemoved = False
            existingUser.MarkAsModified()
            If _listDeleteUsersAssignment.Any(Function(x) x.UserCode = userCode AndAlso x.EntryType = 1) Then
                _listDeleteUsersAssignment.RemoveAll(Function(x) x.UserCode = userCode AndAlso x.EntryType = 1)
            End If
        Else
            'Agregar nuevo usuario
            Dim userAssigned As New UsersAssignment With {
                .UserCode = userCode,
                .FullName = INDSleGridAmb.GetFocusedRowCellValue("PersonFullName"),
                .Status = True,
                .EntryType = 1,
                .IsRemoved = False
            }
            _usersAssignmentList.Add(userAssigned)
        End If

        INDGcUsersAssignment_Amb.DataSource = _usersAssignmentList.Where(Function(x) x.EntryType = 1 AndAlso Not x.IsRemoved)
        INDGcUsersAssignment_Amb.RefreshDataSource()
        INDSleAmb.EditValue = Nothing
        INDSleAmb.Focus()
    End Sub

    ''' <summary>
    ''' Evento click para el GridControl de asignación hospitalaria.
    ''' Capta el usuario seleccionado, y se recarga el datasource de las novedades con las del usuario seleccionado.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGcUsersAssignment_Hosp_Click(sender As Object, e As EventArgs) Handles INDGcUsersAssignment_Hosp.Click
        INDGcNovelties.DataSource = Nothing
        INDGcNovelties.RefreshDataSource()
        Dim selectedUser = TryCast(INDGvUsersAssignment_Hosp.GetFocusedRow, UsersAssignment)
        _gridSelectedUser = selectedUser
        Dim selectedUserNovelties = _userNoveltiesList.Where(Function(x) x.AssignedUserId = selectedUser?.Id)
        INDGcNovelties.DataSource = selectedUserNovelties
        INDGcNovelties.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Evento click para el GridControl de asignación ambulatoria.
    ''' Capta el usuario seleccionado, y se recarga el datasource de las novedades con las del usuario seleccionado.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGcUsersAssignment_Amb_Click(sender As Object, e As EventArgs) Handles INDGcUsersAssignment_Amb.Click
        INDGcNovelties.DataSource = Nothing
        INDGcNovelties.RefreshDataSource()
        Dim selectedUser = TryCast(INDGvUsersAssignment_Amb.GetFocusedRow, UsersAssignment)
        _gridSelectedUser = selectedUser
        Dim selectedUserNovelties = _userNoveltiesList.Where(Function(x) x.AssignedUserId = selectedUser?.Id)
        INDGcNovelties.DataSource = selectedUserNovelties
        INDGcNovelties.RefreshDataSource()
    End Sub
#End Region

#Region "ContextMenu"

    ''' <summary>
    ''' Evento que captura el click sobre uno de los botones del menú contextual de asignación hospitalaria
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridViewHosp_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridViewHosp.Click_ButtonAction, IndigoGridViewHosp.ContexMenuActions
        Dim assignedUser = TryCast(INDGvUsersAssignment_Hosp.GetFocusedRow, UsersAssignment)
        _gridSelectedUser = assignedUser
        Select Case sender.Tag
            Case "AddNovelty"
                If assignedUser IsNot Nothing Then
                    InstantiatePopup(assignedUser)
                End If
            Case "Edit"
                Mensaje(EeventViewerImages.Advertencia) = "Funcionalidad no implementada aún"
            Case "Remove"
                If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) <> System.Windows.Forms.DialogResult.Yes Then
                    Exit Sub
                End If
                If assignedUser IsNot Nothing Then
                    RemoveAssignedUserHosp(assignedUser)
                End If
        End Select
    End Sub

    ''' <summary>
    ''' Evento que captura el click sobre uno de los botones del menú contextual de asignación ambulatoria
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridViewAmb_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridViewAmb.Click_ButtonAction, IndigoGridViewAmb.ContexMenuActions
        Dim assignedUser = TryCast(INDGvUsersAssignment_Amb.GetFocusedRow, UsersAssignment)
        _gridSelectedUser = assignedUser
        Select Case sender.Tag
            Case "AddNovelty"
                If assignedUser IsNot Nothing Then
                    If assignedUser IsNot Nothing Then
                        InstantiatePopup(assignedUser)
                    End If
                End If
            Case "Edit"
                Mensaje(EeventViewerImages.Advertencia) = "Funcionalidad no implementada aún"
            Case "Remove"
                If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) <> System.Windows.Forms.DialogResult.Yes Then
                    Exit Sub
                End If
                If assignedUser IsNot Nothing Then
                    RemoveAssignedUserAmb(assignedUser)
                End If
        End Select
    End Sub

    ''' <summary>
    ''' Evento que captura el click de los botones del menú contextual de las novedades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridViewNovelty_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridViewNovelty.Click_ButtonAction, IndigoGridViewNovelty.ContexMenuActions
        Dim selectedNovelty = TryCast(INDGvNovelties.GetFocusedRow, UserNovelties)
        If selectedNovelty IsNot Nothing Then
            InstantiatePopup(_gridSelectedUser, selectedNovelty)
        End If
    End Sub

#End Region

#End Region

#Region "Barra Botones"
    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

#End Region
End Class