'***********************************************************************
' Assembly         : Presentacion.Controles
' Author           : Julian Cardozo
' Created          : 14-11-2011
'
' Last Modified By :
' Last Modified On : 17-11-2011
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"

#End Region
Public Class CtrContactos

#Region "Enumeraciones"

    ''' <summary>
    ''' Tipo de contacto
    ''' </summary>
    Enum TipoContacto As Integer
        ''' <summary>
        ''' Direcciones
        ''' </summary>
        Direcciones = 0
        ''' <summary>
        ''' Telefonos
        ''' </summary>
        Telefonos = 1
        ''' <summary>
        ''' Correos Electronicos
        ''' </summary>
        Correos = 2
    End Enum

#End Region

#Region "Tuples"

    ''' <summary>
    ''' Variable que contiene la lista de tipos de correos electrónicos
    ''' </summary>
    Dim _listEmailType As List(Of Tuple(Of Byte, String))
    ReadOnly Property ListEmailType As List(Of Tuple(Of Byte, String))
        Get
            If _listEmailType Is Nothing Then
                _listEmailType = New List(Of Tuple(Of Byte, String))
                _listEmailType.Add(New Tuple(Of Byte, String)(1, "Notificaciones"))
                _listEmailType.Add(New Tuple(Of Byte, String)(2, "Notificación Factura Electrónica"))
            End If
            Return _listEmailType
        End Get
    End Property

#End Region

#Region "Eventos"
    ''' <summary>
    ''' Evento para cerrar el control
    ''' </summary>
    Public Event ClosePopUp()

    ''' <summary>
    ''' Evento para insertar una nueva Direccion
    ''' </summary>
    Public Event InsertoNuevaDireccion()

    ''' <summary>
    ''' Evento para eliminar una direccion
    ''' </summary>
    Public Event EliminarDireccion()

    ''' <summary>
    ''' Evento para insertar un nuevo telefono 
    ''' </summary>
    Public Event InsertoNuevoTelefono()

    ''' <summary>
    ''' Evento para eliminar un telefono
    ''' </summary>
    Public Event EliminarTelefono()

    ''' <summary>
    ''' Evento para insertar un nuevo Email 
    ''' </summary>
    Public Event InsertoNuevoEmail()

    ''' <summary>
    ''' Evento para eliminar un email
    ''' </summary>
    Public Event EliminarEmail()

    ''' <summary>
    ''' Evento para cambiar el tipo de email
    ''' </summary>
    Public Event ChangeEmailType(Type As Byte)

#End Region

#Region "Propiedades"

    ''' <summary>
    ''' Propiedad que establece el datasource  de las direcciones.
    ''' </summary>
    ''' <value>The establecer data source direccion.</value>
    WriteOnly Property EstablecerDataSourceDireccion As Object
        Set(ByVal value As Object)
            INDgcDireccion.DataSource = value
            INDpcDirecciones.Text = String.Empty
            INDgcDireccion.RefreshDataSource()
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece el datasource  de los telefonos.
    ''' </summary>
    ''' <value>The establecer data source telefono.</value>
    WriteOnly Property EstablecerDataSourceTelefono As Object
        Set(ByVal value As Object)
            INDgcTelefonos.DataSource = value
            INDpcTelefonos.Text = String.Empty
            INDgcTelefonos.RefreshDataSource()
        End Set
    End Property

    ''' <summary>
    ''' propiedad que establece el datasource de los email
    ''' </summary>
    ''' <value>The establecer data source email.</value>
    WriteOnly Property EstablecerDataSourceEmail As Object
        Set(ByVal value As Object)
            INDgcEmail.DataSource = value
            INDpcCorreos.Text = String.Empty
            INDgcEmail.RefreshDataSource()
        End Set
    End Property

    Private _openByFormUser As Boolean = False
    ''' <summary>
    ''' Permite saber si es abierto por el form de usuarios
    ''' </summary>
    ''' <returns></returns>
    Property OpenByFormUser As Boolean
        Get
            Return _openByFormUser
        End Get
        Set(value As Boolean)
            _openByFormUser = value
        End Set
    End Property

    ''' <summary>
    ''' Gets the obtener valor direcciones.
    ''' </summary>
    ''' <value>The obtener valor direcciones.</value>
    ReadOnly Property ObtenerValorDirecciones() As String
        Get
            Return INDgcDireccionView.GetFocusedRowCellValue("Autonumerico").ToString.Trim
        End Get
    End Property

    ''' <summary>
    ''' Obtener el valor del telefono por autonumerico
    ''' </summary>
    ''' <value>The obtener valor telefono.</value>
    ReadOnly Property ObtenerValorTelefono() As String
        Get
            Return INDgcTelefonosView.GetFocusedRowCellValue("Autonumerico").ToString.Trim
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el valor del email por autonumerico 
    ''' </summary>
    ''' <value>The obtener valor email.</value>
    ReadOnly Property ObtenerValorEmail() As String
        Get
            Return INDgcEmailView.GetFocusedRowCellValue("Autonumerico").ToString.Trim
        End Get
    End Property

	''' <summary>
	''' Obtiene el id del departamento
	''' </summary>
	''' <value>The direccion.</value>
	ReadOnly Property DepartmentId As Integer?
		Get
			Dim value As Integer
			If Integer.TryParse(INDsleDepartment.EditValue, value) Then
				Return value
			End If
			Return Nothing
		End Get
	End Property

	''' <summary>
	''' Obtiene el nombre del departamento
	''' </summary>
	''' <value>The direccion.</value>
	ReadOnly Property DepartmentName As String
        Get
            Return INDsleDepartment.Text
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el id de la ciudad
    ''' </summary>
    ''' <value>The direccion.</value>
    ReadOnly Property CityId As Integer?
        Get
            Dim value As Integer
            If Integer.TryParse(INDsleCity.EditValue, value) Then
                Return value
            End If
            Return Nothing
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el nombre de la ciudad
    ''' </summary>
    ''' <value>The direccion.</value>
    ReadOnly Property CityName As String
        Get
            Return INDsleCity.Text
        End Get
    End Property

    ''' <summary>
    ''' Obtiene la direccion
    ''' </summary>
    ''' <value>The direccion.</value>
    ReadOnly Property Direccion As String
        Get
            Return INDpcDirecciones.EditValue.ToString
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el telefono 
    ''' </summary>
    ''' <value>The telefono.</value>
    ReadOnly Property Telefono As String
        Get
            Return INDpcTelefonos.Text
        End Get
    End Property
    ''' <summary>
    ''' Obtiene el Email
    ''' </summary>
    ''' <value>The email.</value>
    ReadOnly Property Email As String
        Get
            Return INDpcCorreos.EditValue.ToString
        End Get
    End Property
    ''' <summary>
    ''' Obtiene el Tipo de Email
    ''' </summary>
    ''' <value>The email.</value>
    ReadOnly Property EmailType As Byte
        Get
            Return 1
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tipo de telefono 
    ''' </summary>
    ''' <value>The tipo telefono.</value>
    ReadOnly Property TipoTelefono As String
        Get
            Return INDcbTipoTelefono.EditValue.ToString
        End Get
    End Property
    ''' <summary>
    ''' Obtiene el iten seleccionado del listado de direcciones
    ''' </summary>
    ''' <value>The item selecionado direccion.</value>
    ReadOnly Property ItemSelecionadoDireccion As Integer
        Get
            Return INDgcDireccionView.FocusedRowHandle
        End Get
    End Property
    ''' <summary>
    ''' obtiene el item seleccionado del listado de telefonos
    ''' </summary>
    ''' <value>The item selecionado telefono.</value>
    ReadOnly Property ItemSelecionadoTelefono As Integer
        Get
            Return INDgcTelefonosView.FocusedRowHandle
        End Get
    End Property
    ''' <summary>
    ''' Obtiene el item seleccionado del listado de email
    ''' </summary>
    ''' <value>The item selecionado email.</value>
    ReadOnly Property ItemSelecionadoEmail As Integer
        Get
            Return INDgcEmailView.FocusedRowHandle
        End Get
    End Property

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Metodo Para establecer el foco inicial desde el frontal
    ''' </summary>
    Public Sub EstablecerFocoInicial()
        Contactos.SelectedTabPageIndex = 0
        INDpcDirecciones.Focus()
    End Sub
    ''' <summary>
    ''' Metodo que me establece un error provider dependiendo del tipo de contacto
    ''' </summary>
    ''' <param name="TipoContacto">The tipo contacto.</param>
    Public Sub EstablecerError(ByVal TipoContacto As TipoContacto)
        Select Case TipoContacto
            Case Is = CtrContactos.TipoContacto.Direcciones
                INDErrores.SetError(INDgcDireccion, "Especifique una Direccion")
            Case Is = CtrContactos.TipoContacto.Telefonos
                INDErrores.SetError(INDgcTelefonos, "Especifique un Telefono")
            Case Is = CtrContactos.TipoContacto.Correos
                INDErrores.SetError(INDgcEmail, "Especifique un Correo Electronico")
        End Select
    End Sub
    ''' <summary>
    ''' Metodo que me verifica si existen datos y si existen me devuelve el primero existente
    ''' </summary>
    ''' <param name="TipoContacto">The tipo contacto.</param>
    ''' <returns></returns>
    Public Function VerificaExisteDatos(ByVal TipoContacto As TipoContacto) As String
        Select Case TipoContacto
            Case Is = CtrContactos.TipoContacto.Direcciones
                If INDgcDireccionView.RowCount > 0 Then
                    INDErrores.SetError(INDgcDireccion, " ")
                    Return INDgcDireccionView.GetRowCellValue(0, "Addresss").ToString
                End If
            Case Is = CtrContactos.TipoContacto.Telefonos
                If INDgcEmailView.RowCount > 0 Then
                    INDErrores.SetError(INDgcTelefonos, " ")
                    Return INDgcEmailView.GetRowCellValue(0, "Phone1").ToString
                End If
            Case Is = CtrContactos.TipoContacto.Correos
                If INDgcEmailView.RowCount > 0 Then
                    INDErrores.SetError(INDgcEmail, " ")
                    Return INDgcEmailView.GetRowCellValue(0, "Email1").ToString
                End If
        End Select
        Return "False"
    End Function
	''' <summary>
	''' Metodo que se utiliza para Limpiar los controles
	''' </summary>
	Public Sub LimpiarControles()
		INDsleDepartment.EditValue = Nothing
		INDsleDepartment.Properties.View.ActiveFilter.Clear()

		INDsleCity.EditValue = Nothing
		INDsleCity.Properties.View.ActiveFilter.Clear()

		INDpcDirecciones.Text = String.Empty
		INDgcDireccion.DataSource = Nothing

		INDpcTelefonos.Text = String.Empty
		INDcbTipoTelefono.EditValue = Nothing
		INDgcTelefonos.DataSource = Nothing
		INDcbTipoTelefono.EditValue = Nothing

		INDpcCorreos.Text = String.Empty
		INDgcEmail.DataSource = Nothing

		INDErrores.Clear()
		Contactos.SelectedTabPageIndex = 0
	End Sub

#End Region

#Region "Eventos Controles"
    ''' <summary>
    ''' Procedimiento para agregar las direcciones
    ''' </summary>
    Private Sub AgregarDirecciones()
        If Object.Equals(INDpcDirecciones.Text, String.Empty) = False Then
            If _openByFormUser = False Then
                INDErrores.Clear()
                Dim errors = False
                If DepartmentId Is Nothing Then
                    errors = True
                    INDErrores.SetError(INDsleDepartment, "Seleccione un Departamento")
                End If
                If CityId Is Nothing Then
                    errors = True
                    INDErrores.SetError(INDsleCity, "Seleccione una Ciudad")
                End If
                If errors = True Then
                    Exit Sub
                End If
            End If

            RaiseEvent InsertoNuevaDireccion()
        End If
    End Sub
    ''' <summary>
    ''' Agregars the telefonos.
    ''' </summary>
    Private Sub AgregarTelefonos()
        If Object.Equals(INDpcTelefonos.Text, String.Empty) = False Then
            If Object.Equals(INDcbTipoTelefono.EditValue, Nothing) = False Then
                INDErrores.SetError(INDcbTipoTelefono, "")
                RaiseEvent InsertoNuevoTelefono()
                INDpcTelefonos.Focus()
            Else
                INDErrores.SetError(INDcbTipoTelefono, "Seleccione el tipo de Telefono")
            End If
        Else
            Contactos.SelectedTabPageIndex = 2
            INDpcCorreos.Focus()
        End If
    End Sub
    Private Sub AgregarEmails()
        If Object.Equals(INDpcCorreos.Text, String.Empty) = False Then
            RaiseEvent InsertoNuevoEmail()
        End If
    End Sub
    ''' <summary>
    ''' Metodo que activa el evento InsertoNuevaDireccion para agregar un nuevo Item
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs" /> instance containing the event data.</param>
    Private Sub INDpcDirecciones_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDpcDirecciones.ButtonClick
        AgregarDirecciones()
    End Sub

    Private Sub INDpcDirecciones_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles INDpcDirecciones.KeyDown
        If e.KeyCode = Keys.Enter Then
            If INDpcDirecciones.Text = String.Empty Then
                Contactos.SelectedTabPageIndex = 1
                INDpcTelefonos.Focus()
            End If
            If INDpcDirecciones.Text.Length > 0 Then
                AgregarDirecciones()
                INDpcDirecciones.Focus()
            End If
        End If
    End Sub
    ''' <summary>
    ''' Metodo que activa el evento InsertoNuevoTelefono para agregar un nuevo Item
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs" /> instance containing the event data.</param>
    Private Sub INDpTelefonos_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDpcTelefonos.ButtonClick
        AgregarTelefonos()
    End Sub

    Private Sub INDcbTipoTelefono_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles INDcbTipoTelefono.KeyDown
        If e.KeyCode = Keys.Enter Then
            AgregarTelefonos()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que activa el evento InsertoNuevoEmail para agregar un nuevo Item
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs" /> instance containing the event data.</param>
    Private Sub INDpcCorreos_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDpcCorreos.ButtonClick
        AgregarEmails()
    End Sub
    ''' <summary>
    ''' Handles the KeyDown event of the INDpcCorreos control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs" /> instance containing the event data.</param>
    Private Sub INDpcCorreos_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles INDpcCorreos.KeyDown
        If e.KeyCode = Keys.Enter Then
            If INDpcCorreos.Text = String.Empty Then
                Contactos.SelectedTabPageIndex = 0
                RaiseEvent ClosePopUp()
            End If
            AgregarEmails()
            INDpcCorreos.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Handles the Click event of the RepositoryItemPopupContainerEdit1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub RepositoryItemPopupContainerEdit1_Click(sender As Object, e As EventArgs) Handles INDbtnEliminarDireccion.Click
        If INDgcDireccionView.FocusedRowHandle >= 0 Then
            RaiseEvent EliminarDireccion()
        End If
    End Sub

    Private Sub INDbtnEliminarEmail_Click(sender As Object, e As EventArgs) Handles INDbtnEliminarEmail.Click
        If INDgcEmailView.FocusedRowHandle >= 0 Then
            RaiseEvent EliminarEmail()
        End If
    End Sub

    Private Sub INDbtnEliminarTelefono_Click(sender As Object, e As EventArgs) Handles INDbtnEliminarTelefono.Click
        If INDgcTelefonosView.FocusedRowHandle >= 0 Then
            RaiseEvent EliminarTelefono()
        End If
    End Sub

    Private Async Sub CtrContactos_Load(sender As Object, e As EventArgs) Handles Me.Load
        If DesignMode = False Then
            Using Model As New Presentation.Controls.MVP.MCtrContactos
                If _openByFormUser = False Then
                    INDrepSleEmailType.DataSource = ListEmailType
                    RepositoryItemGridLookUpEdit1.DataSource = Await Model.GetPhoneTypesAsync()
                    INDcbTipoTelefono.Properties.DataSource = Await Model.GetPhoneTypesAsync()
                    INDsleDepartment.Properties.DataSource = Await Model.ListAllDepartmentAsync()
                Else
                    RepositoryItemGridLookUpEdit1.DataSource = Await Model.GetPhoneTypesSecurity()
                    INDcbTipoTelefono.Properties.DataSource = Await Model.GetPhoneTypesSecurity()

                    INDlciDepartment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlciCity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    colDepartment.Visible = False
                    colCity.Visible = False
                    colEmailType.Visible = False
                End If
            End Using
        End If
    End Sub

    Private Sub INDsleDepartment_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleDepartment.EditValueChanged
        INDsleCity.Properties.DataSource = Nothing
        If DepartmentId IsNot Nothing Then
            Using Model As New Presentation.Controls.MVP.MCtrContactos
                INDsleCity.Properties.DataSource = Model.ListAllCityDepartment(DepartmentId)
            End Using
        End If
    End Sub

    Private Sub INDrepSleEmailType_EditValueChanged(sender As Object, e As EventArgs) Handles INDrepSleEmailType.EditValueChanged
        If INDgcEmailView.FocusedRowHandle >= 0 Then
            Dim type As Byte = CByte(sender.EditValue)
            RaiseEvent ChangeEmailType(type)
        End If
    End Sub




#End Region

End Class
