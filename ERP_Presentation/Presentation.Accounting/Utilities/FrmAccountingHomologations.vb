'***********************************************************************
' Assembly         : Presentacion.Accounting
' Author           : Carlos Mario Arias Rubiano
' Created          : 23/11/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Accounting.MVP
Imports Presentation.Controls
Imports DevExpress.Xpo
Imports Presentation.Controls.MVP
Imports Domain.Entities
Imports Domain.Base.Entities
Imports DevExpress.Utils.Menu
Imports DevExpress.XtraTreeList.Nodes
Imports Presentation.Base
Imports DevExpress.XtraTreeList.Columns
Imports Presentation.Common.MVP
Imports Presentation.Common
Imports Infrastructure.Data.Xpo
Imports DevExpress.Data
Imports System.ComponentModel
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Infrastructure.CrossCutting.Base
Imports System.Text
Imports Infrastructure.CrossCutting.Resources

#End Region

Public Class FrmAccountingHomologations
    Implements IAccountingHomologations

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el datasource del libro de homologacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HomologationBookXpo As XPInstantFeedbackSource Implements IAccountingHomologations.HomologationBookXpo
        Get
            Return INDsleHomologationBook.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleHomologationBook.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del libro de homologacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HomologationBookId As Integer? Implements IAccountingHomologations.HomologationBookId
        Get
            Return INDsleHomologationBook.EditValue
        End Get
        Set(value As Integer?)
            INDsleHomologationBook.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del libro oficial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LegalBookId As Integer? Implements IAccountingHomologations.LegalBookId
        Get
            Return INDsleLegalBook.EditValue
        End Get
        Set(value As Integer?)
            INDsleLegalBook.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource del libro oficial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LegalBookXpo As XPCollection Implements IAccountingHomologations.LegalBookXpo
        Get
            Return INDsleLegalBook.Properties.DataSource
        End Get
        Set(value As XPCollection)
            INDsleLegalBook.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para registar el mensaje en el visor
    ''' </summary>
    ''' <param name="Icono"></param>
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

#End Region

#Region "Variables"

    ''' <summary>
    ''' Presentador de homologación de cuentas
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PAccountingHomologations

    ''' <summary>
    ''' Listado de homologaciones de cuenta
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListHomologationAccount As List(Of HomologationAccount)

#End Region

#Region "ICrud"

    ''' <summary>
    ''' Item buscar del control de usuarios
    ''' </summary>
    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    ''' <summary>
    ''' Item Deshacer del control de usuarios
    ''' </summary>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Item Eliminar del control de usuarios
    ''' </summary>
    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' Guarda una homologacion de cuentas
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        Dim errors As String = ValidateList()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If

        'Se obtiene los items que van a ser guardados
        Dim ListSave As List(Of HomologationAccount) = ListHomologationAccount.FindAll(Function(item) (item.Id = 0 AndAlso item.MainAccountId > 0) OrElse (item.ChangeTracker.State = ObjectState.Modified))

        Using model As New MHomologationAccount(Me.Tag.ToString())
            AsyncLoader(True)
            Dim Result = Await model.SaveHomologationAccount(ListSave)
            AsyncLoader(False)
            If Result.StateResult = True Then
                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                AsyncLoader(False)
            Else
                If Result.MessageResult(0) = ErrorConcurrencia Then
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                Else
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                End If
            End If
        End Using
        GridColumn1.ClearFilter()
        GridColumn2.ClearFilter()
        InitializeDatasourceGrid()
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -> Muestra Guardar | False -> Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    '''  Item Nuevo del control de usuarios
    ''' </summary>
    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

    ''' <summary>
    ''' Abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Valida que el listado se hayan escogido al menos un item de homologación
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateList() As String
        Dim errors As New StringBuilder
        If ListHomologationAccount Is Nothing OrElse ListHomologationAccount.Count = 0 Then
            errors.AppendLine("No existen cuentas para homologar.")
        End If
        If ListHomologationAccount IsNot Nothing AndAlso ListHomologationAccount.Count > 0 Then
            Dim cont = ListHomologationAccount.FindAll(Function(item) (item.MainAccountId <> Nothing AndAlso item.Id = 0) OrElse (item.ChangeTracker.State = ObjectState.Modified)).Count
            If cont = 0 Then
                errors.AppendLine("Debe elegir homologaciones para las cuentas.")
            End If
        End If
        Return errors.ToString
    End Function

	''' <summary>
	''' Metodo que inicializa el datasource de las cuentas
	''' </summary>
	''' <remarks></remarks>
	Private Sub InitializeDatasourceGrid()
		Try
			AsyncLoader(True)

			'Se instancia el listado para el datasource de la rejilla
			ListHomologationAccount = New List(Of HomologationAccount)

			'Se consulta las cuentas contables que esten asociadas al libro oficial
			Dim ListXpo As XPCollection = Presenter.ListViewHomologationAccountByLegalBookIdAndHomologationLegalBookId(LegalBookId, HomologationBookId)

			If ListXpo IsNot Nothing AndAlso ListXpo.Count > 0 Then

				' Se ordenan los elementos, primero van los que corresponden al libro homologado
				Dim sortedList = ListXpo.Cast(Of ViewHomologationAccountXpo)() _
						 .OrderByDescending(Function(x) x.HomologationLegalBookId = HomologationBookId) _
						 .ThenBy(Function(x) x.Id) _
						 .ToList()

				' Se reccoren los elemento y se agregan a la lista
				For Each itemXpo In sortedList
					Dim homologationAccount As HomologationAccount

					' Si pertenecen al libro homologado se insertan de una vez
					If itemXpo.HomologationLegalBookId = HomologationBookId Then
						homologationAccount = CreateHomologationAccount(itemXpo)
						ListHomologationAccount.Add(homologationAccount)
					Else
						' Si no pertenecen al libro homologado, se verifica que no exista en la lista y se crea un nuevo objeto para ser homologado
						If Not (ListHomologationAccount.Exists(Function(existingAccount) existingAccount.OfficialMainAccountId = itemXpo.OfficalMainAccountId)) Then
							homologationAccount = CreateHomologationAccount(itemXpo, True)
							ListHomologationAccount.Add(homologationAccount)
						End If
					End If
				Next

				' Actualizar el DataSource 
				INDgcAccounts.DataSource = Nothing
				INDgcAccounts.DataSource = ListHomologationAccount
			End If

			'Se llena el datasource del repositorySearch de la rejilla que contiene las cuentas contables que estan asociadas a al libro homologacion
			Dim ListMainAccountsXpo As XPCollection = Presenter.InitializeRepositorySearch(HomologationBookId)
			Dim listDatasource As New List(Of MainAccounts)

			For Each item As PUCServiceXpo In ListMainAccountsXpo

				Dim MainAccounts As New MainAccounts
				With MainAccounts
					.Id = item.Id
					.Name = item.Name
					.Number = item.Number
					.NumberName = item.NumberName
					.HandlesThirdParty = item.HandlesThirdParty
					.HandlesCostCenter = item.HandlesCostCenter
				End With

				listDatasource.Add(MainAccounts)
			Next

			INDrepSleMainAccounts.DataSource = Nothing
			INDrepSleMainAccounts.DataSource = listDatasource

			If ListHomologationAccount IsNot Nothing AndAlso ListHomologationAccount.Count > 0 Then
				BarraBotones.PrepareToolbar(eAction.OnlySave)
			End If

			AsyncLoader(False)
		Catch ex As Exception
			AsyncLoader(False)
			Throw ex
		End Try
	End Sub

	''' <summary>
	''' Crea una cuenta homologada, con datos de la BD o con datos null para ser homologada
	''' </summary>
	''' <remarks></remarks>
	Private Function CreateHomologationAccount(itemXpo As ViewHomologationAccountXpo, Optional clearValues As Boolean = False) As HomologationAccount
		Dim homologationAccount As New HomologationAccount

		With homologationAccount
			.Id = If(clearValues, Nothing, itemXpo.Id)
			.NumberNameOfficialMainAccount = itemXpo.NumberNameOfficialMainAccount
			.OfficialMainAccountId = itemXpo.OfficalMainAccountId
			.MainAccountId = If(clearValues, Nothing, itemXpo.MainAccountId)
			.HandlesThirdPartyOfficialMainAccount = itemXpo.HandlesThirdPartyOfficialMainAccount
			.HandlesCostCenterOfficialMainAccount = itemXpo.HandlesCostCenterOfficialMainAccount
			.CreationUser = If(clearValues, Nothing, itemXpo.CreationUser)
			.CreationDate = If(clearValues, Nothing, itemXpo.CreationDate)
		End With

		Return homologationAccount
	End Function

	''' <summary>
	''' Valida que la cuenta elegida para la homologacion tenga la misma configuracion que la cuenta contable
	''' </summary>
	''' <remarks></remarks>
	Private Sub ValidateAccounts(e As DevExpress.XtraEditors.Controls.ChangingEventArgs)
        'Se consulta la cuenta contable que se eligio en el campo de homologacion
        Dim xpo = Presenter.GetAccountById(e.NewValue)

        If xpo IsNot Nothing AndAlso xpo.Count > 0 Then

            'Se captura la primera posicion porque el listado solo trae uno
            Dim mainAccountXpo As PUCServiceXpo = xpo(0)
            If mainAccountXpo IsNot Nothing Then

                'Se captura la entidad a la cual estoy realizando el foco para las validaciones
                Dim officialMainAccount As HomologationAccount = viewAccounts.GetFocusedRow()
                If officialMainAccount IsNot Nothing Then

                    'Listado de errores
                    Dim ListErrors As New StringBuilder

                    'Se valida que la cuenta del foco tenga el mismo HandlesCostCenter que la cuenta elegida en el xpo
                    If officialMainAccount.HandlesCostCenterOfficialMainAccount <> mainAccountXpo.HandlesCostCenter Then
                        ListErrors.AppendLine("La cuenta contable " + officialMainAccount.NumberNameOfficialMainAccount + " no tiene la misma configuración para el centro costo de la cuenta contable " + mainAccountXpo.NumberName)
                    End If

                    'Se valida que la cuenta del foco tenga el mismo HandlesThirdParty que la cuenta elegida en el xpo
                    If officialMainAccount.HandlesThirdPartyOfficialMainAccount <> mainAccountXpo.HandlesThirdParty Then
                        ListErrors.AppendLine("La cuenta contable " + officialMainAccount.NumberNameOfficialMainAccount + " no tiene la misma configuración para el tercero de la cuenta contable " + mainAccountXpo.NumberName)
                    End If

                    'Si hubo error se devuelve el valor
                    If ListErrors.Length > 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = ListErrors.ToString
                        e.Cancel = True
                        Exit Sub
                    End If

                    'Se asigna el valor
                    officialMainAccount.MainAccountId = e.NewValue
                    If officialMainAccount.Id > 0 Then
                        officialMainAccount.MarkAsModified()
                    End If
                    INDgcAccounts.RefreshDataSource()

                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Asigna los valores para enviar a guardar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()

    End Sub

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        HomologationBookId = Nothing
        INDgcAccounts.DataSource = Nothing
        ListHomologationAccount = Nothing
        INDsleHomologationBook.Focus()
    End Sub

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Este evento se activa cuando el formulario es eliminado y sus recursos deben ser liberados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        ListHomologationAccount = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAccountingHomologations_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Presenter = New PAccountingHomologations(Me)
        IndigoGridControl1.RefreshGrid(INDgcAccounts)
        BarraBotones.PrepareToolbar(eAction.OnlyUndo)
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara para abrir el form de libro oficial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleLegalBook_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleLegalBook.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1682, Nothing, True)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form de libro oficial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleHomologationBook_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleHomologationBook.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1682, Nothing, True)
            Presenter.InitializeHomologationBook()
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
    Private Sub FrmAccountingHomologations_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Presenter.InitializeLegalBook()
        If LegalBookXpo IsNot Nothing AndAlso LegalBookXpo.Count > 0 Then
            Dim info As BookXpo = (From b In LegalBookXpo Where b.OfficialBook = True Select b).FirstOrDefault
            If info IsNot Nothing Then
                LegalBookId = info.Id
            Else
                Mensaje(EeventViewerImages.Advertencia) = "No existe un libro oficial."
                LegalBookId = Nothing
            End If
        End If
        INDsleLegalBook.Focus()
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de homologacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleHomologationBook_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleHomologationBook.QueryPopUp
        If HomologationBookXpo Is Nothing Then
            Presenter.InitializeHomologationBook()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del libro de homologacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleHomologationBook_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleHomologationBook.EditValueChanged
        If HomologationBookId IsNot Nothing AndAlso LegalBookId IsNot Nothing Then
            InitializeDatasourceGrid()
        End If
    End Sub

#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de repositorio de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepSleMainAccounts_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepSleMainAccounts.EditValueChanging
        If e IsNot Nothing AndAlso e.NewValue IsNot Nothing Then
            ValidateAccounts(e)
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
        Guardar()
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
        Deshacer()
        Nuevo()
    End Sub

#End Region

End Class