'***********************************************************************
' Assembly         : Presentacion.Maintenance
' Author           : Julian Andres Cardozo Flores
' Created          : 05-08-2013
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
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Exceptions
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.CloudAgent.IndigoReference.Payroll
Imports Domain.Entities
Imports DevExpress.XtraEditors
Imports System.Windows.Forms
Imports Presentation.Payroll.MVP
Imports Presentation.Controls.MVP
Imports System.Windows

#End Region

''' <summary>
''' Clase que contiene todo el comportamiento de la vista en el frontal de ubicaciones
''' </summary>
Public Class FrmFixedLocation

    Implements IFixedAssetLocation


#Region "Variable Globales Propiedades Intefaz y Load"

    Dim dtFieldsCustomizables As DataTable
    ''' <summary>
    ''' Bandera que se utiliza para verificar si existe o no una definicion del funcional 
    ''' </summary>
    Dim ExistDefinitionFront As Boolean
    ''' <summary>
    ''' Variable para modelo de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Dim ModelBusqueda As New MBusqueda


    Dim Counter As Integer
    ''' <summary>
    ''' Esta propiedad contiene el estado del tipo de poliza
    ''' </summary>
    Public Property StateLocation As Boolean Implements IFixedAssetLocation.StateLocation
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As Boolean)
            BarraBotones.StatusRecord = value
        End Set
    End Property

    Public WriteOnly Property LocationDatasource As List(Of FixedAssetLocation) Implements IFixedAssetLocation.LocationDatasource
        Set(value As List(Of FixedAssetLocation))
            INDTreeListLocation.DataSource = value
            'INDTreeListLocation.ExpandAll()
        End Set
    End Property

    ''' <summary>
    ''' Variable que contiene la ruta de las definiciones del layout
    ''' </summary>
    Dim PathFunctionalDefinitions As String
    ''' <summary>
    ''' Variable que contiene el parentesco
    ''' </summary>
    Dim Location As List(Of FixedAssetLocation)
    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker
    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As New MFixedAssetLocation(Me.Tag)
    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PFixedAssetLocation
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigo As SessionValues = SessionValues.Instance

    Dim ObjLocation As FixedAssetLocation

    Dim LocationTypeList As List(Of FixedAssetLocationType)
    ''' <summary>
    ''' Evento Load donde se ejecutan el asincrono para levantar la definicion del layout
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        ModelBusqueda = Nothing
        Counter = Nothing
        PathFunctionalDefinitions = Nothing
        Model = Nothing
		Presenter = Nothing
		ObjLocation = Nothing
        LocationTypeList = Nothing
    End Sub


    Private Async Sub FrmSpecificConcepts_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Cargamos de manera asincrona definiciones del funcional
        PathFunctionalDefinitions = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloMantenimiento.", Me.Name, ".xml")
        LoadhronousDefinitions = New BackgroundWorker
        If LoadhronousDefinitions.IsBusy = False Then
            LoadhronousDefinitions.RunWorkerAsync()
        End If
        INDTreeListLocation.CollapseAll()
		Me.LoadStatus()

		Using Model As New MFixedAssetLocation(Me.Tag)
            LocationTypeList = Await Model.ListAllLocationTypeAsync

            If LocationTypeList.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No tiene creado los Tipos de Localización. Primero parametricelos y luego si podrá crear ubicación"
            End If

        End Using


        Presenter = New PFixedAssetLocation(Me)
        Deshacer()
    End Sub


#End Region

#Region "CRUD Base"
    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Save() Implements IcrudBase.Guardar

        If ValidateControls() = False Then
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If
        If DeletedList.Count > 0 Then
            For i = 0 To Location.Count - 1
                If Location.Item(i).ChangeTracker.State <> Domain.Base.Entities.ObjectState.Added Then
                    Location.Item(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
                End If
            Next
            For i = 0 To DeletedList.Count - 1
                If DeletedList.Item(i).ChangeTracker.State <> Domain.Base.Entities.ObjectState.Added Then
                    DeletedList.Item(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                    Location.Add(DeletedList.Item(i))
                End If
            Next
        End If
        Try
            Using Model As New MFixedAssetLocation(Me.Tag)
                AsyncLoader(True)
                If Await Model.SaveFixedAssetLocation(Location, 0) = True Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                Else
                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                End If
                AsyncLoader(False)
            End Using
            Deshacer()
        Catch ex As Exception
            Throw ex
            AsyncLoader(False)
        End Try

    End Sub

	''' <summary>
	''' METODO: Item Eliminar del control de usuarios.
	''' </summary>
	Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar
	End Sub

	''' <summary>
	''' METODO: Item buscar del control de usuarios.
	''' </summary>
	Public Sub Buscar() Implements Base.IcrudBase.Buscar
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo
        CleanControls()
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub AbrirBusqueda() Implements Base.IcrudBase.OpenSearch
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' este permite establecer la logica para los permisos de Guardar y Actualizar   ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
        Set(ByVal value As String)

            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, Botones.Aceptar, MessageType.Errores, Me.Text)
            End If
        End Set
    End Property
#End Region

#Region "Metodos Funciones Propiedades"

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = True, .StatusName = "Activo", .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(23, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = False, .StatusName = "Inactivo", .StatusColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(38, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub
    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        DeletedList = New List(Of FixedAssetLocation)
        Location = Nothing
        LoadControls()
        INDTreeListLocation.DataSource = Nothing
        INDTreeListLocation.CollapseAll()
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False

    End Sub

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IFixedAssetLocation.ActionsOnControls
        Set(value As Boolean)
        End Set
    End Property


	''' <summary>
	''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
	''' </summary>
	Private Async Sub LoadControls()
		Using Model As New MFixedAssetLocation(Me.Tag)
			Dim loadLocationTypeTask = Model.ListAllLocationTypeAsync()
			Dim loadLocationsTask = Model.ListAllLocation()

			Await Task.WhenAll(loadLocationTypeTask, loadLocationsTask)

            LocationTypeList = Await loadLocationTypeTask
            If LocationTypeList IsNot Nothing AndAlso LocationTypeList.Any() Then
				RepositoryItemImageComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
				RepositoryItemImageComboBox1.Items.Clear()
				For Each item In LocationTypeList
					RepositoryItemImageComboBox1.Items.Add(New DevExpress.XtraEditors.Controls.ImageComboBoxItem(description:=item.Name, value:=CType(item.Id, Int16)))
				Next
			End If

			Location = Await loadLocationsTask
			If Location IsNot Nothing AndAlso Location.Any() Then
				Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
				LocationDatasource = Location
				Counter = Location.Item(Location.Count - 1).Id
			Else
				StateLocation = True
				Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
			End If
		End Using
	End Sub

	''' <summary>
	''' Valida que los campos esten diligenciados
	''' </summary>
	''' <returns></returns>
	Private Function ValidateControls() As Boolean
        ValidateControls = True
    End Function


    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control .
    ''' </summary>
    Private Sub INDBteCodeKindship_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        AbrirBusqueda()
    End Sub



#End Region

#Region "Eventos Barra Botones"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
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
        CleanControls()
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
        Save()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        CleanControls()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click customizar.
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
        CustomizationOpen()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Save()
    End Sub

    ''' <summary>
    ''' Barras the botones_ clic restablecer layout.
    ''' </summary>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
        ResetLayout()
    End Sub
#End Region

#Region "Customizar"

    ''' <summary>
    ''' Metodo para abrir el formulario de customizar el frontal
    ''' </summary>
    Private Sub CustomizationOpen()
        INDLyCtrLocation.ShowCustomizationForm()
    End Sub

    ''' <summary>
    ''' Evento DoWork que se utiliza para verificar si existe una definicion del xml del frontal
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs" /> instance containing the event data.</param>
    Private Sub CargarDefinicionesAsincronas_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles LoadhronousDefinitions.DoWork
        If CustomizacionFrontales.VerificaExisteDefinicionFrontal(PathFunctionalDefinitions) = True Then
            ExistDefinitionFront = True
        End If
    End Sub
	''' <summary>
	''' Evento RunWorkerCompleted que se utiliza para preguntar si encontro una definicion del frontal para posteriormente cargarla
	''' </summary>
	''' <param name="sender">The source of the event.</param>
	''' <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs" /> instance containing the event data.</param>
	Private Sub CargarDefinicionesAsincronas_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles LoadhronousDefinitions.RunWorkerCompleted
		If ExistDefinitionFront = True Then
			INDLyCtrLocation.RestoreLayoutFromXml(PathFunctionalDefinitions)
		End If
	End Sub

	''' <summary>
	''' Evento que se dispara cuando se cierra el formulario de customizacion y que guarda la definicion del layout en la ruta especificada de cada usuario
	''' </summary>
	''' <param name="sender">The source of the event.</param>
	''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
	Private Sub INDLyCentroAtencion_HideCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDLyCtrLocation.HideCustomization
        'Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
        If INDLyCtrLocation.IsModified = True Then
			Try
				If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
					INDLyCtrLocation.SaveLayoutToXml(PathFunctionalDefinitions)
				Else
					Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesErrorGuardarDefinicion)
				End If
			Catch ex As Exception
				Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorGuardarDefinicion)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Metodo para restablecer las definiciones del formulario gridLookUpEdit y Regillas
    ''' </summary>
    Private Sub ResetLayout()
        If My.Computer.FileSystem.DirectoryExists(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
            My.Computer.FileSystem.DeleteDirectory(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name), Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
            INDLyCtrLocation.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub

#End Region



	Dim DeletedList As List(Of FixedAssetLocation)

	'pendiente documentar
	Private Async Sub INDRepositoryItemButtonEdit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDRepositoryItemButtonEdit.ButtonClick
        Dim locationParent = DirectCast(INDTreeListLocation.GetDataRecordByNode(INDTreeListLocation.FocusedNode), Domain.Entities.FixedAssetLocation)
        Dim LocationTypeId = locationParent.LocationTypeId

        Dim CurrentLocationType = LocationTypeList.Where(Function(x) x.Id = LocationTypeId).FirstOrDefault()

        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
			Using formulario As New FrmFixedAssetAddLocation
				formulario.Size = New System.Drawing.Size(838, 450)
				formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent

                formulario.INDBteCode.Text = locationParent.Code
                formulario.LocationTypeDataSource = LocationTypeList.Where(Function(x) x.OrderLocation >= CurrentLocationType.OrderLocation).ToList()
                Dim transparent = New Base.FrmTransparent(formulario, False)
				Me.Cursor = System.Windows.Forms.Cursors.Default
				transparent.ShowDialog(Me)
				If formulario.INDAceptar = True Then
					If Location.Where(Function(x) x.Code = formulario.INDBteCode.Text.Trim).Count = 0 Then
						locationParent.FixedAssetLocation1.Add(New FixedAssetLocation With {.Code = formulario.INDBteCode.Text.Trim, .Name = formulario.INDTxtName.Text.Trim, .LocationTypeId = formulario.INDCbeLocationType.EditValue, .RiskLevel = formulario.INDgleRiskLevel.EditValue, .Class = formulario.INDSlClasification.EditValue, .FunctionalUnitId = formulario.INDSlFunctionalUnit.EditValue, .NameFunctionalUnit = formulario.INDSlFunctionalUnit.Name, .NameLocationType = formulario.INDCbeLocationType.Text, .UseTime = formulario.INDseUseTime.EditValue})
						Counter += 1
					Else
						Mensaje(EeventViewerImages.Advertencia) = "Ya existe un registro con el mismo código"
					End If
				End If

			End Using
		ElseIf e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis Then

			'Editar
			Using formulario As New FrmFixedAssetAddLocation
				formulario.Size = New System.Drawing.Size(838, 420)
				formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent

				Dim ModifyLocation As FixedAssetLocation = locationParent

				formulario.INDCbeLocationType.Enabled = False

				If locationParent.CityId IsNot Nothing Then
					'Es Ciudad
					formulario.INDSearlookCity.Properties.DataSource = ModelBusqueda.ConsultarEntidades(eDataSource.AllCity)
					formulario.INDVarHide = "Ciudad"
					formulario.INDSearlookCity.EditValue = ModifyLocation.CityId
				Else
					formulario.INDBteCode.EditValue = locationParent.Code
				End If

				formulario.INDBteCode.EditValue = ModifyLocation.Code
				formulario.INDTxtName.EditValue = ModifyLocation.Name
				formulario.INDCbeLocationType.EditValue = ModifyLocation.LocationTypeId

				formulario.INDgleRiskLevel.EditValue = ModifyLocation.RiskLevel
				formulario.INDSlClasification.EditValue = ModifyLocation.Class
				formulario.INDSlFunctionalUnit.EditValue = ModifyLocation.FunctionalUnitId
				formulario.INDSlFunctionalUnit.Properties.NullText = ModifyLocation.NameFunctionalUnit
				formulario.INDseUseTime.EditValue = ModifyLocation.UseTime

				formulario.LocationTypeDataSource = LocationTypeList.Where(Function(x) x.Id = ModifyLocation.LocationTypeId).ToList()
				formulario.INDCbeLocationType.EditValue = ModifyLocation.LocationTypeId
				formulario.INDCbeLocationType.Properties.NullText = ModifyLocation.NameLocationType
				Dim transparent = New Base.FrmTransparent(formulario, False)
				Me.Cursor = System.Windows.Forms.Cursors.Default
				transparent.ShowDialog(Me)

				If formulario.INDAceptar = True Then

					If locationParent.CityId IsNot Nothing Then
						ModifyLocation.CityId = formulario.INDSearlookCity.EditValue
						ModifyLocation.Name = formulario.INDSearlookCity.Text
					Else
						ModifyLocation.Name = formulario.INDTxtName.Text.Trim
					End If

					ModifyLocation.Code = formulario.INDBteCode.Text.Trim
					ModifyLocation.Class = formulario.INDSlClasification.EditValue
					ModifyLocation.FunctionalUnitId = formulario.INDSlFunctionalUnit.EditValue
					ModifyLocation.UseTime = formulario.INDseUseTime.EditValue
					ModifyLocation.RiskLevel = formulario.INDgleRiskLevel.EditValue

					locationParent = ModifyLocation

				End If
			End Using
		Else
            If MessageIndigo.Show("Seguro Desea Eliminar el Registro y sus Hijos?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Dim NumeroEliminados As Integer = 0
                ' y campturamos el valor del nodo seleccionado
                Dim ID As String = INDTreeListLocation.FocusedNode.GetValue("Id").ToString.Trim
                'Iteramos sobre el Listado completo
                For i = 0 To Location.Count - 1
                    'Preguntamos si el valor del nodo seleccionado es igual a la posicion del objeto
                    If ID = Location.Item(i - NumeroEliminados).ParentId.ToString Then
                        ID = Location.Item(i - NumeroEliminados).Id
                        DeletedList.Add(Location.Item(i - NumeroEliminados))
                        Location.RemoveAt(i - NumeroEliminados)
                        NumeroEliminados = NumeroEliminados + 1
                    End If
                Next
                For i As Integer = 0 To Location.Count - 1
                    ID = INDTreeListLocation.FocusedNode.GetValue("Id").ToString.Trim
                    'Eliminamos La unidad de Mercadeo Seleccionada
                    If Location.Item(i).Id.ToString.Trim = ID.ToString.Trim Then
                        DeletedList.Add(Location.Item(i))
                        Location.RemoveAt(i)
                        Exit For
                    End If
                Next

            End If
        End If
        LocationDatasource = Nothing
		LocationDatasource = Location

	End Sub

	Private Sub INDbtnAddRoot_Click(sender As Object, e As EventArgs) Handles INDbtnAddRoot.Click

		Using formulario As New FrmFixedAssetAddLocation
			formulario.Size = New System.Drawing.Size(838, 420)
			formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent

			formulario.INDSearlookCity.Properties.DataSource = ModelBusqueda.ConsultarEntidades(eDataSource.AllCity)


			If LocationTypeList Is Nothing Then
				Mensaje(EeventViewerImages.Advertencia) = "No se ha cargado el Tipo de Ubicaciones"
				Exit Sub
			End If

			Dim LocationTypeDataSource = LocationTypeList.Where(Function(x) x.OrderLocation = 1).ToList()
			formulario.LocationTypeDataSource = LocationTypeDataSource
			formulario.INDVarHide = "Ciudad"
			Dim transparent = New Base.FrmTransparent(formulario, False)
			Me.Cursor = System.Windows.Forms.Cursors.Default
			transparent.ShowDialog(Me)

			If formulario.INDAceptar = True Then
				If Location.Where(Function(x) x.Code = formulario.INDBteCode.Text.Trim).Count = 0 Then
					Location.Add(New FixedAssetLocation With {.Code = formulario.INDBteCode.Text.Trim, .Name = formulario.INDSearlookCity.Text, .CodeNameLocationType = formulario.INDCbeLocationType.Text, .LocationTypeId = LocationTypeDataSource.Item(0).Id, .RiskLevel = formulario.INDgleRiskLevel.EditValue, .CityId = formulario.INDSearlookCity.EditValue, .Class = formulario.INDSlClasification.EditValue, .FunctionalUnitId = formulario.INDSlFunctionalUnit.EditValue, .NameFunctionalUnit = formulario.INDSlFunctionalUnit.Text, .NameLocationType = formulario.INDCbeLocationType.Text, .UseTime = formulario.INDseUseTime.EditValue})
					Counter += 1
				Else
					Mensaje(EeventViewerImages.Advertencia) = "Ya existe un registro con el mismo codigo"
				End If
			End If

			LocationDatasource = Nothing
			LocationDatasource = Location

			Save()

		End Using
	End Sub
End Class