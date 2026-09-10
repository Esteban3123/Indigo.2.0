'***********************************************************************
' Assembly         : Presentacion.Common
' Author           : Juan F. Tamayo
' Created          : 2013-05-08
'
' Last Modified By : Rafael E. Patiño
' Last Modified On : 2013-06-13
' Description      : Vista del frontal de registro de objeciones
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"


Imports Presentation.Glosas.MVP
Imports Presentation.Base
Imports Presentation.Base.Extension
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Entities
Imports Domain.Base.Entities
Imports DevExpress.Xpo

#End Region

''' <summary>
''' Vista del del frontal de registro de objeciones
''' </summary>
Public Class FrmRegisterObjection
    Implements IRegisterObjection


#Region "Fields"

    ''' <summary>
    ''' Consjuto de datos que contiene los campos personalizables
    ''' </summary>
    Private _customizableFields As DataTable

    ''' <summary>
    ''' Bandera usada para verificar si existe o no una definicion del funcional
    ''' </summary>
    Private _frontDefinicionExists As Boolean

    ''' <summary>
    ''' Ruta de las definiciones del layout
    ''' </summary>
    Private _pathFunctionalDefinitions As String

    ''' <summary>
    ''' Hilo para cargar las definiciones del funcional
    ''' </summary>
    Private WithEvents DefinitionsLoader As BackgroundWorker

    ''' <summary>
    ''' Referencia al modelo
    ''' </summary>
    Private _modelRegister As New MRegisterObjection

    ''' <summary>
    ''' Referencia al presentador
    ''' </summary>
    Private _presenter As PRegisterObjection

    ''' <summary>
    ''' Referencia a los valores de sesion
    ''' </summary>
    Private _indigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Estado de la factura
    ''' </summary>
    Public Property StatusObjectionD As Byte

#End Region

#Region "Properties y variable "

    'variale oara almcenat temporalmente el id del detalle de factura
    Private _InvoiceDetaildId As Long
    'variable que almacena el IdDetalleQX
    Private _InvoiceDetaildQXId As Long
    'variable para almacenar el numero de factura
    Private _InvoiceNumber As String
    ''variable que almacena el mayor valor
    Private _may As Decimal
    'variable que almacena
    Private _conteo As Integer
    'variable que almacena el valor de la factura
    Private _InvoiceValue As Decimal
    'variable que almacena si es una glosa general o no
    Private _GeneralGlosa As Boolean
    'variable que almacena si es una glosa por seleccion multiple
    Private _GeneralGlosaSelection As Boolean
    'variable que almacena si es una glosa por seleccion multiple de tipo qx
    Private _GlosaSelectionqx As Boolean
    'variable para almacenar la lista de detalles de factura
    Private _ListInvoiceDetail As List(Of GlosaInvoiceDetail)
    'variable para almacenar la lista de detalles de factura
    Private _ListInvoiceDetailqx As List(Of GlosaInvoiceDetailQX)
    'variable que almacena el valor a glosar
    Private _ValueObjection As Decimal
    'variable para almacenar el codigo del responsable
    Private _CodeResponsible As Long
    'Vriable para determinar si es una reiteracion
    Private _Reiteration As Boolean
    'creamos una nueva lista para ir adicionando las objeciones
    Dim ListRegisterObjetionsGeneral As New List(Of GlosaMovementGlosa)
    ''' <summary>
    ''' variable que almacena el saldo a reiterar
    ''' </summary>
    ''' <remarks></remarks>
    Private _MultipleSelection As Boolean

    Public Property _ManageDecimals As Boolean?

    ''' <summary>
    ''' Propiedad que obtiene y establece si el tipo de glosa es primera vez o reiteracion
    ''' </summary>
    ''' <value>
    '''   <c>true</c> Si es reiteracion; Primera Vez, <c>false</c>.
    ''' </value>
    Public Property Reiteration As Boolean
        Get
            Return _Reiteration
        End Get
        Set(value As Boolean)
            _Reiteration = value
            If value Then
                ClPrincipal.VisibleIndex = 0
                ClConceptoName.VisibleIndex = 1
                CLResponsable.VisibleIndex = -1
                ClValor.VisibleIndex = 2
                ClValueAcceptFirstInstance.VisibleIndex = 3
                ClValuePendingConciliation.VisibleIndex = 4
                ClComentaryGlosa.VisibleIndex = -1
                ClValueReiteration.VisibleIndex = 5
                ClResponsibleReiteration.VisibleIndex = 6
                ClComentaryReiteration.VisibleIndex = 7
                INDActionColumn.VisibleIndex = 8
            Else
                ClPrincipal.VisibleIndex = 0
                ClConceptoName.VisibleIndex = 1
                CLResponsable.VisibleIndex = 2
                ClValor.VisibleIndex = 3
                ClValueAcceptFirstInstance.VisibleIndex = -1
                ClValuePendingConciliation.VisibleIndex = 4
                ClComentaryGlosa.VisibleIndex = 5
                ClValueReiteration.VisibleIndex = -1
                ClResponsibleReiteration.VisibleIndex = -1
                ClComentaryReiteration.VisibleIndex = -1
                INDActionColumn.VisibleIndex = 6
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna un valor que indica si el frontal ya se cargo inicialmente
    ''' </summary>
    ''' <value>Valor</value>
    ''' <returns>Un valor que indica si ya se cargo el formulario</returns>
    ''' <remarks></remarks>
    Public Property IsLoaded As Boolean Implements IRegisterObjection.IsLoaded

    ''' <summary>
    ''' Registra un mensaje en el visor de eventos
    ''' </summary>
    ''' <param name="Icono">Icono segun el tipo de mensaje</param>
    ''' <value>Mensaje a registrar</value>
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
    ''' Asigna un valor a la propiedad enabled de los controles
    ''' </summary>
    ''' <value>Valor a aplicar</value>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IRegisterObjection.ActionsOnControls
        Set(value As Boolean)
            Me.INDgleGeneralConcept.Enabled = Not value
            Me.INDgleResponsible.Enabled = Not value
            Me.INDtxtValue.Enabled = Not value
            Me.INDmemComment.Enabled = Not value
            Me.INDgleGeneralConcept.Focus()
            BarraBotones.Enabled = Actionstaskbar
            EnabledProcess(False)
        End Set
    End Property

    Private _Actionstaskbar As Boolean
    ''' <summary>
    ''' Asigna el valor de activo o inactivo a la barra de acciones
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property Actionstaskbar As Boolean Implements IRegisterObjection.Actionstaskbar
        Set(value As Boolean)
            _Actionstaskbar = value
        End Set
        Get
            Return _Actionstaskbar
        End Get
    End Property


    ''' <summary>
    ''' Obtiene o asigna el id del detalle de factura
    ''' </summary>
    ''' <value>Id del detalle de factura</value>
    ''' <returns>El id del detalle de factura</returns>
    Public Property InvoiceDetaildId As Long Implements IRegisterObjection.InvoiceDetaildId
        Get
            Return Me._InvoiceDetaildId
        End Get
        Set(value As Long)
            _InvoiceDetaildId = value
        End Set
    End Property


    ''' <summary>
    ''' Obtiene el id detalle de factura QX
    ''' </summary>
    ''' <value>id detalle de factura QX</value>
    ''' <returns>id dettalle de factura QX</returns>
    ''' <remarks></remarks>
    Public Property InvoiceDetaildQXId As Long Implements IRegisterObjection.InvoiceDetaildQXId
        Get
            Return Me._InvoiceDetaildQXId
        End Get
        Set(value As Long)
            _InvoiceDetaildQXId = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el Valor de la factura,valor del item de detalle de factura, valor de la objecion general segunb caso
    ''' </summary>
    ''' <value>valor de la Factura</value>
    ''' <returns>el valor de la factura</returns>
    Public Property InvoiceValue As Decimal Implements IRegisterObjection.InvocieValue
        Get
            Return Me._InvoiceValue
        End Get
        Set(value As Decimal)
            _InvoiceValue = value
            If _ManageDecimals IsNot Nothing AndAlso _ManageDecimals = False Then
                LblValorSeleccion.Text = value.MoneyFormat(numberDecimal:=0)
            Else
                LblValorSeleccion.Text = value.MoneyFormat()
            End If

        End Set
    End Property


    Private _BalanceInvoice As Decimal
    ''' <summary>
    ''' Propiedad para almacenar el saldo de la factura disponible
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BalanceInvoice As Decimal
        Get
            Return _BalanceInvoice
        End Get
        Set(value As Decimal)
            _BalanceInvoice = value
        End Set
    End Property

    Private _SumValueGlosadoTotal As Decimal
    Private _SumValueGlosadoTotalCopy As Decimal
    ''' <summary>
    ''' Propiedad para almcenar el acumuliativo de todos los moviminetos de las facturas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SumValueGlosadoTotal As Decimal
        Get
            Return _SumValueGlosadoTotal
        End Get
        Set(value As Decimal)
            _SumValueGlosadoTotal = value
            _SumValueGlosadoTotalCopy = value
        End Set
    End Property



    ''' <summary>
    ''' Propiedad para asignar el numero de factura del detalle
    ''' </summary>
    ''' <value>Nuemro de factura</value>
    ''' <returns>numero de factura</returns>
    Public Property InvoiceNumber As String Implements IRegisterObjection.InvoiceNumber
        Get
            Return Me._InvoiceNumber
        End Get
        Set(value As String)
            _InvoiceNumber = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para saber el tipo de glosa a realizar general o por cada detalle de factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GeneralGlosa As Boolean Implements IRegisterObjection.GeneralGlosa
        Get
            Return Me._GeneralGlosa
        End Get
        Set(value As Boolean)
            _GeneralGlosa = value
        End Set
    End Property

    ''' <summary>
    ''' propieedad para saber si el tipo de glosa es por seleccion multiple
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property multipleSelection As Boolean
        Get
            Return Me._MultipleSelection
        End Get
        Set(value As Boolean)
            _MultipleSelection = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para saber si el tipo de glosa a realizar es por seleccion multiple 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GeneralGlosaSelection As Boolean Implements IRegisterObjection.GeneralGlosaSelection
        Get
            Return Me._GeneralGlosaSelection
        End Get
        Set(value As Boolean)
            _GeneralGlosaSelection = value
        End Set
    End Property

    ''' <summary>
    ''' Porpiedad para saber si el tipo de glosa a realizar es por seleccion multiple y de tipo qx
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GlosaSelectionqx As Boolean Implements IRegisterObjection.GlosaSelectionqx
        Get
            Return Me._GlosaSelectionqx
        End Get
        Set(value As Boolean)
            _GlosaSelectionqx = value
        End Set
    End Property


    ''' <summary>
    ''' propiedad para asignar la lista de detalles de factura al realizar una objecion general
    ''' </summary>
    ''' <value>lsita de detalle de factura</value>
    ''' <returns>una lista de detalle de factura</returns>
    ''' <remarks></remarks>
    Public Property ListInvoiceDetail As List(Of GlosaInvoiceDetail) Implements IRegisterObjection.ListInvoiceDetail
        Get
            Return Me._ListInvoiceDetail
        End Get
        Set(value As List(Of GlosaInvoiceDetail))
            _ListInvoiceDetail = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para asignar la lista de detalles de factura qx al realizar una objecion por seleccion multiple
    ''' </summary>
    ''' <value>lsita de detalle de factura qx</value>
    ''' <returns>una lista de detalle de factura qx</returns>
    ''' <remarks></remarks>
    Public Property ListInvoiceDetailqx As List(Of GlosaInvoiceDetailQX) Implements IRegisterObjection.ListInvoiceDetailqx
        Get
            Return Me._ListInvoiceDetailqx
        End Get
        Set(value As List(Of GlosaInvoiceDetailQX))
            _ListInvoiceDetailqx = value
        End Set
    End Property

    Public Property ValueObjection As Decimal Implements IRegisterObjection.ValueObjection
        Get
            Return Me._ValueObjection
        End Get
        Set(value As Decimal)
            _ValueObjection = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el codigo del responsable
    ''' </summary>
    ''' <value>Codigo del responsable</value>
    ''' <returns>El codigo del responsable</returns>
    Public Property CodeResponsible As String Implements IRegisterObjection.CodeResponsible
        Get
            Return Me.INDgleResponsible.EditValue
        End Get
        Set(value As String)
            Me.INDgleResponsible.EditValue = value.Trim()
        End Set
    End Property


    ''' <summary>
    ''' Obtiene o asigna el comentario
    ''' </summary>
    ''' <value>Comentario</value>
    ''' <returns>El comentario</returns>
    Public Property Comment As String Implements IRegisterObjection.Comment
        Get
            Return Me.INDmemComment.Text.Trim()
        End Get
        Set(value As String)
            Me.INDmemComment.Text = value.Trim()
        End Set
    End Property

    Dim _listMovementValidation As List(Of GlosaMovementGlosa)

    ''' <summary>
    ''' Obtiene o asigna las objeciones asociadas al detalle de factura
    ''' </summary>
    ''' <value>Objeciones</value>
    ''' <returns>Las objeciones asociadas</returns>
    Public Property MovementGlosaSource As List(Of GlosaMovementGlosa) Implements IRegisterObjection.MovementGlosaSource
        Get
            Return CType(Me.INDgdcObjections.DataSource, List(Of GlosaMovementGlosa))
        End Get
        Set(value As List(Of GlosaMovementGlosa))
            Me.INDgdcObjections.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la fuente de datos de conceptos generales
    ''' </summary>
    Public Property GeneralConceptDataSource As XPInstantFeedbackSource Implements IRegisterObjection.GeneralConceptDataSource
        Get
            Return Me.INDgleGeneralConcept.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            Me.INDgleGeneralConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la fuente de datos de responsables
    ''' </summary>
    Public Property ResponsibleDataSource As List(Of ResponsibleAll) Implements IRegisterObjection.ResponsibleDataSource
        Get
            Return CType(Me.INDgleResponsible.Properties.DataSource, List(Of ResponsibleAll))
        End Get
        Set(value As List(Of ResponsibleAll))
            Me.INDgleResponsible.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para asignar la glosa principal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValueMainObjection As Decimal Implements IRegisterObjection.ValueMainObjection

    ''' <summary>
    ''' Propiedad Para Asignar la descripcion del servicio a registrar movimineto glosa
    ''' </summary>
    Public WriteOnly Property DescripTionService As String Implements IRegisterObjection.DescripTionService
        Set(value As String)
            LblDescripcionServicio.Text = value
        End Set
    End Property

#End Region

#Region "CRUD Operations"

    ''' <summary>
    ''' Deja el frontal listo para una nueva busqueda o insercion de datos
    ''' </summary>
    Public Sub Nuevo() Implements ICrudBase.Nuevo
        Me.CleanControls()
    End Sub

    Public Sub Buscar() Implements ICrudBase.Buscar

    End Sub

    ''' <summary>
    ''' Deshace los cambios realizados
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        Me.CleanControls()
    End Sub

    ''' <summary>
    ''' Elimina el registro de objecion seleccionado
    ''' </summary>
    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' Funcion para validar los movimientos de glosas registrados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function fnValidateMainRegister() As Boolean
        If INDgdcObjections.DataSource IsNot Nothing Then
            Dim value As Decimal = 0
            For Each item As GlosaMovementGlosa In INDgdcObjections.DataSource
                If item.ValueGlosado = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "La glosa registrada con el concepto " & item.CodeGlosa & " tiene valor en cero, no puede continuar"
                    Return False
                End If
                If item.MainGlosa = True Then
                    value += item.ValueGlosado
                End If
            Next
            If value = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No hay movimientos Principales de glosas, por favor modificar item o adicionar glosas principales"
                Return False
            End If
            If value > InvoiceValue Then
                Mensaje(EeventViewerImages.Advertencia) = "La sumatoria de movimientos Principales de glosas " & value.ToString("C2", indigo.Culture) & " no puede ser superior al valor del item" & InvoiceValue.ToString("C2", indigo.Culture)
                Return False
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = "No hay registros de movimientos glosas registrados"
            Return False
        End If
        Return True
    End Function
    ''' <summary>
    ''' Guarda el registro de objecion
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        Try
            Using Model As New MRegisterObjection
                Dim result As New ActionResult
                If INDgdcObjections.DataSource.count > 0 Then
                    If GeneralGlosa = False Then
                        AsyncLoader(True)
                        If _Reiteration = True Then
                            result = Await Model.SaveReiterationMovementGlosa(INDgdcObjections.DataSource)
                        Else
                            If fnValidateMainRegister() = True Then
                                result = Await Model.SaveMovementGlosa(INDgdcObjections.DataSource)
                            End If
                        End If
                        AsyncLoader(False)
                    Else
                        If GlosaSelectionqx = True Then
                            ObjectionGeneralqx()
                        Else
                            ObjectionGeneral()
                        End If
                        AsyncLoader(True)
                        result = Await Model.SaveMovementGlosa(ListRegisterObjetionsGeneral)
                        AsyncLoader(False)
                    End If
                    If result IsNot Nothing Then
                        ListRegisterObjetionsGeneral = New List(Of GlosaMovementGlosa)
                        If result.StateResult = True Then

                            If GeneralGlosa = False Then
                                If Me.InvoiceDetaildId AndAlso Me.InvoiceDetaildQXId > 0 Then
                                    AsyncLoader(True)
                                    Me.MovementGlosaSource = Await Model.ListMovementGlosaQx(Me.InvoiceDetaildId, Me.InvoiceDetaildQXId)
                                    AsyncLoader(False)
                                Else
                                    AsyncLoader(True)
                                    Me.MovementGlosaSource = Await Model.ListMovementGlosa(Me.InvoiceDetaildId)
                                    AsyncLoader(False)
                                End If
                            Else
                                Me.MovementGlosaSource = New List(Of GlosaMovementGlosa)
                            End If
                            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                        Else
                            If result.MessageResult IsNot Nothing AndAlso result.MessageResult(0) = "-999" Then
                                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorConcurrencia)
                            Else
                                If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                                    Dim strMensaje As String = String.Empty
                                    For Each itemMensaje As String In result.MessageResult
                                        strMensaje += itemMensaje + Environment.NewLine
                                    Next
                                    Mensaje(EeventViewerImages.Informacion) = strMensaje
                                    MovementGlosaSource = New List(Of GlosaMovementGlosa)
                                End If
                            End If
                        End If
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Informacion) = ex.Message.ToString
            Throw ex
        End Try
    End Sub

#End Region

#Region "Metodos"


    ''' <summary>
    ''' Cambia el estado del formulario para indicar que se esta llevando a cabo una operacion asincrona
    ''' </summary>
    ''' <param name="State">Valor que indica si se lleva a cabo la operacion</param>
    Public Overrides Sub AsyncLoader(State As Boolean) Implements IRegisterObjection.AsyncLoader
        MyBase.AsyncLoader(State)
    End Sub

    Public Sub AbrirBusqueda() Implements ICrudBase.OpenSearch

    End Sub

    ''' <summary>
    ''' Permite establecer la logica para los permisos de Guardar y Actualizar
    ''' </summary>
    ''' <param name="existeDatos">Valor que indica si existen datos para actualizar</param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        Me.BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub


    ''' <summary>
    ''' Limpia los valores en los controles
    ''' </summary>
    Private Sub CleanControls()
        Me.ActionsOnControls = False
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = False
        CleanPopupControls()
    End Sub



    ''' <summary>
    ''' Metodo para habilitar solo procesos
    ''' </summary>
    ''' <param name="Value">if set to <c>true</c> [value].</param>
    Private Sub EnabledProcess(ByVal Value As Boolean)
        BarraBotones.RibbonPageEdicion.Visible = Value
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
    End Sub

    ''' <summary>
    ''' Abrir el formulario de personalizacion del frontal
    ''' </summary>
    Private Sub OpenCustomize()
        Me.INDlycRegisterObjection.ShowCustomizationForm()
    End Sub
    ''' <summary>
    ''' Restablecer las definiciones del formulario
    ''' </summary>
    Private Sub ResetLayout()
        If My.Computer.FileSystem.DirectoryExists(String.Concat(Me._indigoSessionValues.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
            My.Computer.FileSystem.DeleteDirectory(String.Concat(Me._indigoSessionValues.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name), Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
            Me.INDlycRegisterObjection.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(Eresources.ComunesLayoutRestablecido)
        End If
    End Sub

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControls() As Boolean
        ValidateControls = True
        If Reiteration = False Then
            If INDgleGeneralConcept.EditValue Is Nothing Then
                ValidateControls = False
            End If
        End If
        If INDgleResponsible.EditValue Is Nothing Then
            ValidateControls = False
        End If
        Return ValidateControls
    End Function

    ''' <summary>
    ''' Agrega Un NUevo Registro a la Rejilla de Conceptos a Glosar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDAddBtn_Click(sender As Object, e As EventArgs) Handles INDAddorUpdateBtn.Click
        Dim ObjUpdate As GlosaMovementGlosa = Nothing
        If Reiteration Then

            If _entityToUpdate Is Nothing Then
                AddItemRejilla()
            Else
                updateItemRejilla(_entityToUpdate)
            End If

        Else
            Dim ObjGeneralConcept = CType(CType(INDgleGeneralConcept.GetSelectedDataRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.GlosasRepository.CommonConceptGlosas)
            If MovementGlosaSource IsNot Nothing AndAlso MovementGlosaSource.Count > 0 And ObjGeneralConcept IsNot Nothing Then
                ObjUpdate = MovementGlosaSource.Find(Function(value As GlosaMovementGlosa) value.CodeGlosa = ObjGeneralConcept.Code)
            Else
                ObjUpdate = Nothing
            End If
            If ObjUpdate Is Nothing Then
                AddItemRejilla()
            Else
                updateItemRejilla(ObjUpdate)
            End If
        End If

        CleanPopupControls()
    End Sub

    Dim _ObjRegisterObjection As GlosaMovementGlosa

    ''' <summary>
    ''' Metodo para actualizar un movimiento
    ''' </summary>
    ''' <param name="ObjUpdate"></param>
    ''' <remarks></remarks>
    Private Sub updateItemRejilla(ByVal ObjUpdate As GlosaMovementGlosa)
        If ValidateControls() = False Then
            Exit Sub
        End If

        If Reiteration Then
            If ObjUpdate.ValuePendingConciliation = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(ItemWithoutBalance, RegisterObjection))
                Exit Sub
            End If

            Dim saldo As Decimal = MovementGlosaSource.Sum(Function(cc As GlosaMovementGlosa) cc.ValuePendingConciliation)
            Dim ValueGlosaMovementMain As Decimal = ObjUpdate.ValueGlosado
            Dim valuereiterated As Decimal = FormatCurrency(INDtxtValue.EditValue, 2)

            If saldo = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(ItemWithoutBalance, RegisterObjection))
                Exit Sub
            ElseIf valuereiterated > saldo Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(GlossValueWasAlreadyAccepted, RegisterObjection), FormatCurrency(valuereiterated, 2), FormatCurrency(saldo, 2))
                valuereiterated = 0
                Exit Sub
            End If

            If valuereiterated > ValueGlosaMovementMain Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(ValorReiteracionMayorValorGlosado, Presentation.Base.Eform.RegisterObjection), FormatCurrency(valuereiterated, 2), FormatCurrency(ValueGlosaMovementMain, 2))
                INDtxtValue.EditValue = ValueGlosaMovementMain
                Exit Sub
            End If

            ObjUpdate.ValueReiterated = valuereiterated
            Dim ObjReponsible As Domain.Entities.ResponsibleAll = CType(INDgleResponsible.GetSelectedDataRow(), Domain.Entities.ResponsibleAll)
            ObjUpdate.ResponsibleReiterationId = INDgleResponsible.EditValue
            ObjUpdate.ResponsibleReiterationName = ObjReponsible.responsibleName
            ObjUpdate.RationaleDateReiteration = Date.Now
            ObjUpdate.RationaleReiteration = INDmemComment.Text
            ObjUpdate.State = 3 'Pendiente Evaluar Reiteracion 
        Else

            If INDtxtValue.EditValue IsNot Nothing AndAlso INDtxtValue.EditValue = 0 Then
                ValueObjection = InvoiceValue
            Else
                ValueObjection = FormatCurrency(INDtxtValue.EditValue, 2)
            End If

            'GLOSA
            If ValueObjection > InvoiceValue Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(ValorGlosaMayorValorFactura, Presentation.Base.Eform.RegisterObjection), FormatCurrency(InvoiceValue, 2))
                INDtxtValue.EditValue = InvoiceValue
                Exit Sub
            End If

            'como se esta modificando un valor ya calculado y acumulado, procedo a restar ese valor, para liberar saldo y no genere problema a la hora de validar
            _SumValueGlosadoTotal = _SumValueGlosadoTotal - ObjUpdate.ValueGlosado

            fnAcumulativoValorGlosado(ObjUpdate.ValueGlosado)
            Dim _TmpmainGlosa As Boolean
            If AcumulativoValorGlosado <= InvoiceValue Then
                _TmpmainGlosa = True 'si no supera saldo de factura van como principal
            Else
                _TmpmainGlosa = False ' si supera saldo de factura la marcamos como no principal
            End If

            _SumValueGlosadoTotal += ValueObjection
            If _SumValueGlosadoTotal > BalanceInvoice Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(ValorGlosadoMayorSaldo, RecepcionObjeciones), FormatCurrency(_SumValueGlosadoTotal, 2), FormatCurrency(BalanceInvoice, 2))
                _SumValueGlosadoTotal = _SumValueGlosadoTotalCopy
                Exit Sub
            End If


            With ObjUpdate
                .ValueGlosado = ValueObjection
                .ValuePendingConciliation = .ValueGlosado
                .RationaleGlosa = INDmemComment.Text
                Dim ObjReponsible As Domain.Entities.ResponsibleAll = CType(INDgleResponsible.GetSelectedDataRow(), Domain.Entities.ResponsibleAll)
                .ResponsibleId = INDgleResponsible.EditValue
                .ResponsibleName = ObjReponsible.responsibleCodeName

                Dim ObjGeneralConcept = CType(CType(INDgleGeneralConcept.GetSelectedDataRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.GlosasRepository.CommonConceptGlosas)
                .CodeGlosaId = ObjGeneralConcept.Id
                .ConceptGlosasCodeName = ObjGeneralConcept.NameSpecific
                .CodeGlosa = ObjGeneralConcept.Code
                .MainGlosa = _TmpmainGlosa
            End With

            fnAcumulativoValorGlosado(ObjUpdate.ValueGlosado)
            _TmpmainGlosa = False
            If AcumulativoValorGlosado <= InvoiceValue Then
                _TmpmainGlosa = True 'si no supera saldo de factura van como principal
            Else
                _TmpmainGlosa = False ' si supera saldo de factura la marcamos como no principal
            End If

            With ObjUpdate
                .MainGlosa = _TmpmainGlosa
            End With
        End If
        INDgdcObjections.RefreshDataSource()
        'Limpio los controles
        CleanPopupControls()
    End Sub


    ''' <summary>
    ''' se crea item GlosaMovementGlosa y se agregan  a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddItemRejilla()
        If ValidateControls() = False Then
            Exit Sub
        End If

        'el valor del item no puede ser mayor al valor de la factura
        If Reiteration = True AndAlso _entityToUpdate IsNot Nothing Then
            Dim ValueGlosaMovementMain As Decimal

            If INDgdvObjections.FocusedRowHandle >= 0 Then
                Dim ObjMov As GlosaMovementGlosa = CType(Me.INDgdvObjections.GetRow(Me.INDgdvObjections.FocusedRowHandle), GlosaMovementGlosa)
                If ObjMov IsNot Nothing And ObjMov.MainGlosa = True Then
                    ValueGlosaMovementMain = ObjMov.ValueGlosado

                    Dim valuereiterated As Decimal = FormatCurrency(INDtxtValue.EditValue, 2)

                    If ObjMov.ValuePendingConciliation = 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(ItemWithoutBalance, RegisterObjection))
                        Exit Sub
                    End If

                    'validamos el valor a reiterar
                    Dim saldo As Decimal = MovementGlosaSource.Sum(Function(cc As GlosaMovementGlosa) cc.ValuePendingConciliation)

                    If saldo = 0 Then
                        ' Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(ReiteratedSlopeValueGreaterBalance, RegisterObjection), FormatCurrency(valueGlosado, 2), FormatCurrency(valueAcceptFirtsInstance, 2))
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(ItemWithoutBalance, RegisterObjection))
                        Exit Sub
                    ElseIf valuereiterated > saldo Then
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(GlossValueWasAlreadyAccepted, RegisterObjection), FormatCurrency(valuereiterated, 2), FormatCurrency(saldo, 2))
                        valuereiterated = 0
                        Exit Sub
                    End If

                    If valuereiterated > ValueGlosaMovementMain Then
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(ValorReiteracionMayorValorGlosado, Presentation.Base.Eform.RegisterObjection), FormatCurrency(valuereiterated, 2), FormatCurrency(ValueGlosaMovementMain, 2))
                        INDtxtValue.EditValue = ValueGlosaMovementMain
                        Exit Sub
                    End If

                    If INDtxtValue.EditValue IsNot Nothing AndAlso INDtxtValue.EditValue > 0 Then

                        ObjMov.ValueReiterated = valuereiterated
                        ObjMov.ResponsibleReiterationName = INDgleResponsible.SelectedText
                        ObjMov.RationaleDateReiteration = Date.Now
                        ObjMov.RationaleReiteration = INDmemComment.Text
                        ObjMov.State = 3 'Pendiente Evaluar Reiteracion 
                    End If
                    INDgdcObjections.RefreshDataSource()
                End If
            End If
        Else
            Dim GlosaValueTotal As Boolean
            'al acumulativo general de movimientos valor glosado le sumamos el movimiento que tratamos de adicionar
            If INDtxtValue.EditValue IsNot Nothing AndAlso INDtxtValue.EditValue = 0 Then
                ValueObjection = InvoiceValue
                GlosaValueTotal = True
            Else
                GlosaValueTotal = False
                ValueObjection = FormatCurrency(INDtxtValue.EditValue, 2)
            End If

            'GLOSA
            If ValueObjection > InvoiceValue Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(ValorGlosaMayorValorFactura, Presentation.Base.Eform.RegisterObjection), FormatCurrency(InvoiceValue, 2))
                INDtxtValue.EditValue = InvoiceValue
                Exit Sub
            End If

            fnAcumulativoValorGlosado()

            _SumValueGlosadoTotal += ValueObjection
            If _SumValueGlosadoTotal > BalanceInvoice Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(ValorGlosadoMayorSaldo, RecepcionObjeciones), FormatCurrency(_SumValueGlosadoTotal, 2), FormatCurrency(BalanceInvoice, 2))
                _SumValueGlosadoTotal = _SumValueGlosadoTotalCopy
                Exit Sub
            End If

            If AcumulativoValorGlosado <= InvoiceValue Then
                _ObjRegisterObjection = CreateMovementGlosa(GlosaValueTotal, True) 'si no supera saldo de factura van como principal
            Else
                _ObjRegisterObjection = CreateMovementGlosa(GlosaValueTotal, False) ' si supera saldo de factura la marcamos como no principal
            End If

            If Reiteration Then
                Dim valuereiterated As Decimal = FormatCurrency(INDtxtValue.EditValue, 2)
                Dim ObjReponsible As Domain.Entities.ResponsibleAll = CType(INDgleResponsible.GetSelectedDataRow(), Domain.Entities.ResponsibleAll)
                _ObjRegisterObjection.ResponsibleReiterationId = INDgleResponsible.EditValue
                _ObjRegisterObjection.ValueReiterated = valuereiterated
                _ObjRegisterObjection.ResponsibleReiterationName = ObjReponsible.responsibleName
                _ObjRegisterObjection.RationaleDateReiteration = Date.Now
                _ObjRegisterObjection.ValueAcceptedFirstInstance = 0
                _ObjRegisterObjection.RationaleReiteration = INDmemComment.Text
                _ObjRegisterObjection.IsNormative = False
                _ObjRegisterObjection.State = 3 'Pendiente Evaluar Reiteracion 
            End If

            Dim ObjGeneralConcept = CType(CType(INDgleGeneralConcept.GetSelectedDataRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.GlosasRepository.CommonConceptGlosas)
            Dim found As GlosaMovementGlosa = Nothing  'valido que el movimiento agregar no este repetido en la lista
            If MovementGlosaSource IsNot Nothing AndAlso MovementGlosaSource.Count > 0 Then
                found = MovementGlosaSource.Find(Function(value As GlosaMovementGlosa) value.CodeGlosa = ObjGeneralConcept.Code)
            End If
            If found Is Nothing Then 'si no esta proceso a agregarla
                If MovementGlosaSource Is Nothing Then
                    MovementGlosaSource = New List(Of GlosaMovementGlosa)
                End If
                MovementGlosaSource.Add(_ObjRegisterObjection)
                _listMovementValidation.Add(_ObjRegisterObjection) 'cuando es seleccion multiple o glosa general
            Else
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(CodigoGlosaYaExiste, Presentation.Base.Eform.RegisterObjection)
            End If
        End If

        INDgdcObjections.RefreshDataSource()
    End Sub

    Dim AcumulativoValorGlosado As Decimal
    Private Sub fnAcumulativoValorGlosado(Optional ByVal FreeValue As Decimal = 0)
        AcumulativoValorGlosado = 0
        If GeneralGlosa = True Or GeneralGlosaSelection = True Then
            For Each Item As GlosaMovementGlosa In _listMovementValidation
                If Item.MainGlosa = True Then
                    AcumulativoValorGlosado = AcumulativoValorGlosado + Item.ValueGlosado
                End If
            Next
        Else
            If MovementGlosaSource IsNot Nothing Then
                For Each Item As GlosaMovementGlosa In MovementGlosaSource
                    If Item.MainGlosa = True Then
                        AcumulativoValorGlosado = AcumulativoValorGlosado + Item.ValueGlosado
                    End If
                Next
            End If
        End If
        'para cuando se actualice, liberar saldo
        If FreeValue > 0 Then
            AcumulativoValorGlosado = AcumulativoValorGlosado - FreeValue
        End If
        Dim valorinput As Decimal
        If INDtxtValue.EditValue IsNot Nothing AndAlso INDtxtValue.EditValue > 0 Then
            valorinput = INDtxtValue.EditValue
        Else
            valorinput = InvoiceValue
        End If
        AcumulativoValorGlosado = AcumulativoValorGlosado + valorinput 'sumamos el valor a ingresar
    End Sub

    ''' <summary>
    ''' Crea Un Objeto Movimieno glosa
    ''' </summary>
    ''' <returns>retorna un objeto movimineto glosa</returns>
    ''' <remarks></remarks>
    Private Function CreateMovementGlosa(ByVal GlosaValueTotal As Boolean, ByVal mainGlosa As Boolean) As GlosaMovementGlosa
        Dim ObjGeneralConcept = CType(CType(INDgleGeneralConcept.GetSelectedDataRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.GlosasRepository.CommonConceptGlosas)
        Dim _ObjRegisterObjection As New GlosaMovementGlosa
        With _ObjRegisterObjection
            .IsNormative = Not Reiteration OrElse (_entityToUpdate IsNot Nothing AndAlso _entityToUpdate.CodeGlosaId = ObjGeneralConcept.Id)
            .InvoiceDetailId = Me.InvoiceDetaildId
            If GlosaValueTotal Then
                .ValueGlosado = Me.InvoiceValue
            Else
                .ValueGlosado = FormatCurrency(INDtxtValue.EditValue, 2) 'Convert.ToDecimal(INDtxtValue.EditValue)
            End If
            .ValuePendingConciliation = .ValueGlosado
            .MainGlosa = mainGlosa
            .RationaleGlosa = INDmemComment.Text
            .State = 1 'la envio como estado 1 Pendiente Evaluar Glosa
            .TempState = 1 'la envio como estado 1 Pendiente Evaluar Glosa
            .RationaleDateGlosa = Date.Now
            .ResponsibleName = INDgleResponsible.Text
            .ResponsibleId = INDgleResponsible.EditValue
            .InvoiceNumber = Me.InvoiceNumber
            If InvoiceDetaildQXId > 0 Then
                .InvoiceDetailIdQX = Me.InvoiceDetaildQXId
            End If

            .CodeGlosaId = ObjGeneralConcept.Id
            .ConceptGlosasCodeName = INDgleGeneralConcept.Text
            .TypeConcept = 1
            .CodeGlosa = ObjGeneralConcept.Code
        End With
        Return _ObjRegisterObjection
    End Function

    ''' <summary>
    ''' Crea Un Objeto Movimieno glosa general
    ''' </summary>
    ''' <returns>retorna un objeto movimineto glosa</returns>
    ''' <remarks></remarks>
    Private Function CreateMovementGlosaGeneral(ByVal _value As Decimal, ByVal _InvoiceNumber As String, ByVal _InvoiceDetailId As Integer, ByVal _InvoiceDetaildQXId As Integer, ByVal ObjMov As GlosaMovementGlosa, ByVal Main As Boolean) As GlosaMovementGlosa
        Dim _ObjRegisterObjection As New GlosaMovementGlosa
        With _ObjRegisterObjection
            .IsNormative = Not Reiteration OrElse (_entityToUpdate IsNot Nothing AndAlso _entityToUpdate.CodeGlosaId = ObjMov.ConceptGlosas.Id)
            .ValueGlosado = _value
            .InvoiceNumber = _InvoiceNumber
            .InvoiceDetailId = _InvoiceDetailId
            .MainGlosa = Main
            .RationaleDateGlosa = GetDateServer()
            If _InvoiceDetaildQXId > 0 Then
                .InvoiceDetailIdQX = _InvoiceDetaildQXId
            End If
            .RationaleGlosa = ObjMov.RationaleGlosa
            .State = 1 'la envio como estado 1 Pendiente Evaluar Glosa
            .TempState = 1 'la envio como estado 1 Pendiente Evaluar Glosa
            .ResponsibleId = _CodeResponsible
            .TypeConcept = 1
            .CodeGlosa = ObjMov.CodeGlosa
            .CodeGlosaId = ObjMov.CodeGlosaId
        End With
        Return _ObjRegisterObjection
    End Function

    ''' <summary>
    ''' Funcion de logica para las objeciones generales
    ''' </summary>
    Private Function ObjectionGeneral()
        Dim valueGlosa As Decimal = 0
        If TryCast(INDgdvObjections.GetFocusedRow, Domain.Entities.GlosaMovementGlosa) Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Error en la rejilla"
            Exit Function
        End If
        valueGlosa = TryCast(INDgdvObjections.GetFocusedRow, Domain.Entities.GlosaMovementGlosa).ValueGlosado

        Dim valueGlosaTmp As Decimal
        Dim IdInvoiceValueTmp As Integer = 0

        'por cada detalle repartimos valores
        For Each itemDetail As GlosaInvoiceDetail In ListInvoiceDetail

            Dim newMovement As New GlosaMovementGlosa
            ' si tiene detalle qx empiezo a distribuir el valor glosado en cada item
            If itemDetail.GlosaInvoiceDetailQX.Count > 0 Then
                For Each itemDetailQX As GlosaInvoiceDetailQX In itemDetail.GlosaInvoiceDetailQX
                    'para el control de que si se reparte entre los detalles qx, no reparte el valor al detalle de factura
                    IdInvoiceValueTmp = itemDetail.Id
                    'asigno en una variable temporal el valor actual 
                    valueGlosaTmp = valueGlosa
                    'si el valor es mayor a 0
                    If valueGlosa > 0 Then
                        'que el detalle QX tenga valor de factura mayor a cero
                        If itemDetailQX.InvoicedValue > 0 Then
                            'descuento el valor del detalle qx al saldo que queda por distribuir
                            valueGlosa = valueGlosa - itemDetailQX.InvoicedValue
                            'si no alcanza a cubrir el total valor de detalle registro el valor temporal 

                            'por cada concepyo
                            For Each itemConcept As GlosaMovementGlosa In INDgdvObjections.DataSource
                                If valueGlosa < 0 Then
                                    newMovement = CreateMovementGlosaGeneral(valueGlosaTmp, itemDetail.InvoiceNumber, itemDetail.Id, itemDetailQX.Id, itemConcept, itemConcept.MainGlosa)
                                Else
                                    newMovement = CreateMovementGlosaGeneral(itemDetailQX.InvoicedValue, itemDetail.InvoiceNumber, itemDetail.Id, itemDetailQX.Id, itemConcept, itemConcept.MainGlosa)
                                End If
                                'adiciono a la lista a guardar
                                ListRegisterObjetionsGeneral.Add(newMovement)
                            Next

                        End If
                    Else
                        valueGlosaTmp = 0
                        Exit For
                    End If
                Next
            End If

            valueGlosaTmp = valueGlosa
            'control de que si se distribuye entre los detalles qx, no distribuya el valor al detalle de factura
            If itemDetail.Id <> IdInvoiceValueTmp Then
                If valueGlosa > 0 Then
                    If itemDetail.ValorEntidad = 0 Then itemDetail.ValorEntidad = itemDetail.InvoicedValue
                    If itemDetail.ValorEntidad > 0 Then
                        valueGlosa = valueGlosa - itemDetail.ValorEntidad

                        'por cada concepyo
                        For Each itemConcept As GlosaMovementGlosa In INDgdvObjections.DataSource
                            If valueGlosa < 0 Then
                                newMovement = CreateMovementGlosaGeneral(valueGlosaTmp, itemDetail.InvoiceNumber, itemDetail.Id, 0, itemConcept, itemConcept.MainGlosa)
                            Else
                                newMovement = CreateMovementGlosaGeneral(itemDetail.ValorEntidad, itemDetail.InvoiceNumber, itemDetail.Id, 0, itemConcept, itemConcept.MainGlosa)
                            End If
                            ListRegisterObjetionsGeneral.Add(newMovement)
                        Next

                    End If
                Else
                    valueGlosaTmp = 0
                    valueGlosa = 0
                    Exit For
                End If
            End If
        Next ' fin ciclo movimientos
    End Function

    Private Function ObjectionGeneralqx()
        Dim ValueGlosa As Decimal = TryCast(INDgdvObjections.DataSource, List(Of GlosaMovementGlosa)).Sum(Function(x) x.ValueGlosado)
        Dim valueGlosaTmp As Decimal
        For Each itemDetailQX As GlosaInvoiceDetailQX In ListInvoiceDetailqx
            Dim newMovement As New GlosaMovementGlosa

            valueGlosaTmp = valueGlosa
            'si el valor es mayor a 0
            If valueGlosa > 0 Then
                'que el detalle QX tenga valor de factura mayor a cero
                If itemDetailQX.InvoicedValue > 0 Then
                    'descuento el valor del detalle qx al saldo que queda por distribuir
                    valueGlosa = valueGlosa - itemDetailQX.InvoicedValue

                    'por cada concepyo
                    For Each itemConcept As GlosaMovementGlosa In INDgdvObjections.DataSource
                        'si no alcanza a cubrir el total valor de detalle registro el valor temporal 
                        If valueGlosa < 0 Then
                            newMovement = CreateMovementGlosaGeneral(valueGlosaTmp, Me.InvoiceNumber, Me.InvoiceDetaildId, itemDetailQX.Id, itemConcept, itemConcept.MainGlosa)
                        Else
                            newMovement = CreateMovementGlosaGeneral(itemDetailQX.InvoicedValue, Me.InvoiceNumber, Me.InvoiceDetaildId, itemDetailQX.Id, itemConcept, itemConcept.MainGlosa)
                        End If
                        'adiciono a la lista a guardar
                        ListRegisterObjetionsGeneral.Add(newMovement)
                    Next
                End If
            Else
                valueGlosaTmp = 0
                Exit For
            End If
        Next
    End Function

#End Region

#Region "Handlers"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _customizableFields = Nothing
        _frontDefinicionExists = Nothing
        _pathFunctionalDefinitions = Nothing
        _modelRegister = Nothing
        _presenter = Nothing
    End Sub

    ''' <summary>
    ''' Inicializa la vista del frontal de registro de objeciones
    ''' </summary>
    Private Sub FrmRegisterObjection_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Cargamos de manera asincrona definiciones del funcional
        Me._pathFunctionalDefinitions = String.Concat(Me._indigoSessionValues.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloGlosas.", Me.Name, ".xml")
        Me.DefinitionsLoader = New BackgroundWorker()
        If Me.DefinitionsLoader.IsBusy = False Then
            Me.DefinitionsLoader.RunWorkerAsync()
        End If
        Me._presenter = New PRegisterObjection(Me)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Permisos) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Jerarquia) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = False
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        Me._presenter.LoadDataAsync()
        Me.IndigoGridControl1.SetHotTrack(Me.INDgdcObjections, True)
        IndigoGridControl1.RefreshGrid(Me.INDgdcObjections)
        Me.INDgdcObjections.DataSource = Nothing
        Me.CleanControls()
        If GeneralGlosa = True And GeneralGlosaSelection = False Then
            Using model As New MRegisterObjection()
                AsyncLoader(True)
                ListInvoiceDetail = model.ListGlosaInvoiceDetailByInvoiceNumber(InvoiceNumber, "RA")
                AsyncLoader(False)
            End Using
        End If

        If Reiteration Then
            Text = "Registro de reiteraciones"
            INDlycgObjections.Text = "Reiteraciones"
        End If

        Dim ListMov As New List(Of GlosaMovementGlosa)
        _listMovementValidation = New List(Of GlosaMovementGlosa)
        If ListInvoiceDetail IsNot Nothing AndAlso ListInvoiceDetail.Count > 0 Then

            For Each item As GlosaInvoiceDetail In ListInvoiceDetail
                For Each itemMov In item.GlosaMovementGlosa
                    ListMov.Add(itemMov)
                Next
            Next
            _listMovementValidation = ListMov

        ElseIf ListInvoiceDetailqx IsNot Nothing AndAlso ListInvoiceDetailqx.Count > 0 Then

            For Each item As GlosaInvoiceDetailQX In ListInvoiceDetailqx
                For Each itemMov In item.GlosaMovementGlosa
                    ListMov.Add(itemMov)
                Next
            Next
            _listMovementValidation = ListMov

        End If
        If _ManageDecimals IsNot Nothing AndAlso _ManageDecimals = False Then
            INDtxtValue.Properties.DisplayFormat.FormatString = "c0"
            INDtxtValue.Properties.Mask.EditMask = "c0"
            ClValor.DisplayFormat.FormatString = "c0"
            ClValuePendingConciliation.DisplayFormat.FormatString = "c0"
        End If
        AcumulativoValorGlosado = 0
    End Sub

    ''' <summary>
    ''' Aqui se verifica asincronamente si existe una definicion xml del frontal
    ''' </summary>
    Private Sub DefinitionsLoader_DoWork(sender As Object, e As DoWorkEventArgs) Handles DefinitionsLoader.DoWork
        If CustomizacionFrontales.VerificaExisteDefinicionFrontal(Me._pathFunctionalDefinitions) = True Then
            Me._frontDefinicionExists = True
        End If
    End Sub

    ''' <summary>
    ''' Carga asincronamente la definicion del frontal
    ''' </summary>
    Private Sub DefinitionsLoader_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles DefinitionsLoader.RunWorkerCompleted
        If Me._frontDefinicionExists = True Then
            Me.INDlycRegisterObjection.RestoreLayoutFromXml(Me._pathFunctionalDefinitions)
        End If
    End Sub

    ''' <summary>
    ''' Se guardan las modificaciones realizadas en la definicion del frontal
    ''' </summary>
    Private Sub INDlycAuthorizationHealthCenter_HideCustomization(sender As Object, e As EventArgs) Handles INDlycRegisterObjection.HideCustomization
        'Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
        If Me.INDlycRegisterObjection.IsModified = True Then
            Try
                If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(Me._indigoSessionValues.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
                    Me.INDlycRegisterObjection.SaveLayoutToXml(Me._pathFunctionalDefinitions)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.ComunesErrorGuardarDefinicion)
                End If
            Catch ex As Exception
                IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorGuardarDefinicion)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Abre el frontal de responsables para agregar uno nuevo
    ''' </summary>
    Private Sub INDgleResponsible_Properties_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDgleResponsible.Properties.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using frmResponsible As New FrmResponsible
                If frmResponsible.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    'Recargar los lookUpEdit
                    Me._presenter.LoadResponsibles()
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Elimina un Item de la rejilla de registerObjections
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDDeleteBtn_Click(sender As Object, e As EventArgs) Handles INDDeleteBtn.Click
        If StatusObjectionD <> 2 Then
            Dim ObjResponsable As Responsible
            If Me.INDgdvObjections.SelectedRowsCount Then
                Dim obj As GlosaMovementGlosa = Me.INDgdvObjections.GetRow(Me.INDgdvObjections.FocusedRowHandle)
                If obj IsNot Nothing Then
                    If obj.Id = 0 Then
                        Dim found = Me.MovementGlosaSource.Find(Function(value As GlosaMovementGlosa) value.CodeGlosa = obj.CodeGlosa)
                        Me.MovementGlosaSource.Remove(found)
                        'markMainGlosa()
                    Else
                        If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                            ObjResponsable = obj.Responsible1


                            Dim result As ActionResult = Nothing
                            'Dim result As Boolean

                            Using model As New MRegisterObjection

                                If obj.ValueReiterated > 0 Then
                                    result = Await model.UpdateMovementReiteration(obj)
                                    If result.StateResult = True Then
                                        AsyncLoader(True)
                                        If Me.InvoiceDetaildId AndAlso Me.InvoiceDetaildQXId > 0 Then
                                            Me.MovementGlosaSource = Await model.ListMovementGlosaQx(Me.InvoiceDetaildId, Me.InvoiceDetaildQXId)
                                        Else
                                            Me.MovementGlosaSource = Await model.ListMovementGlosa(Me.InvoiceDetaildId)
                                        End If
                                        AsyncLoader(False)
                                        Me.INDgdcObjections.RefreshDataSource()
                                    Else
                                        If result.MessageResult IsNot Nothing AndAlso result.MessageResult(0) = "-999" Then
                                            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorConcurrencia)
                                        Else
                                            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoPuedeEliminarDatoReiteracion, Presentation.Base.Eform.RecepcionObjeciones)
                                        End If
                                    End If
                                Else
                                    obj.MarkAsDeleted()
                                    Me.INDgdcObjections.RefreshDataSource()
                                    _SumValueGlosadoTotal = _SumValueGlosadoTotal - obj.ValueGlosado
                                    Dim flagresult As Boolean
                                    flagresult = Await model.DeleteMovementGlosa(obj)
                                    If flagresult = True Then
                                        Me.INDgdcObjections.RefreshDataSource()
                                    Else
                                        obj.Responsible1 = ObjResponsable
                                        Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoPuedeEliminarPorMovimiento, Presentation.Base.Eform.RecepcionObjeciones)
                                    End If
                                    AsyncLoader(True)
                                    If Me.InvoiceDetaildId AndAlso Me.InvoiceDetaildQXId > 0 Then
                                        Me.MovementGlosaSource = Await model.ListMovementGlosaQx(Me.InvoiceDetaildId, Me.InvoiceDetaildQXId)
                                    Else
                                        Me.MovementGlosaSource = Await model.ListMovementGlosa(Me.InvoiceDetaildId)
                                    End If
                                    AsyncLoader(False)
                                End If
                            End Using
                        End If
                    End If
                    INDgdcObjections.RefreshDataSource()
                End If
            End If
            fnAcumulativoValorGlosado()

            CleanPopupControls()
        Else
            Mensaje(EeventViewerImages.Advertencia) = "No se pueden eliminar detalles si la factura ya está confirmada"
        End If
    End Sub

    ''' <summary>
    ''' Cambio en el responsable
    ''' </summary>
    Private Sub INDgleResponsible_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleResponsible.EditValueChanged
        If INDgleResponsible.EditValue IsNot Nothing Then
            _CodeResponsible = INDgleResponsible.EditValue
        End If
    End Sub
#End Region

#Region "ToolBar Events"

    ''' <summary>
    ''' Aqui se cargan los permisos que tiene el frontal en la barra
    ''' </summary>
    Private Async Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion deshacer
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Me.CleanControls()
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion eliminar
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Me.Eliminar()
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion guardar
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Me.Guardar()
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion nuevo
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Me.CleanControls()
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion de personalizacion del frontal
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
        Me.OpenCustomize()
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion de actualizar
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Me.Guardar()
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion de reestablecer la definicion del frontal
    ''' </summary>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
        Me.ResetLayout()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceAddItemGloss_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDpceAddItemGloss.QueryPopUp
        If Reiteration = True Then
            If INDgdvObjections.FocusedRowHandle >= 0 Then
                Dim ObjMov As GlosaMovementGlosa = CType(Me.INDgdvObjections.GetRow(Me.INDgdvObjections.FocusedRowHandle), GlosaMovementGlosa)
                If ObjMov IsNot Nothing Then
                    If ObjMov.MainGlosa = True Then
                        INDtxtValue.EditValue = IIf(ObjMov.ValueReiterated Is Nothing, ObjMov.ValuePendingConciliation, ObjMov.ValueReiterated)  'ObjMov.ValueGlosado - IIf(ObjMov.ValueAcceptedFirstInstance Is Nothing, 0, ObjMov.ValueAcceptedFirstInstance)
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = "Seleccione un item Glosa principal para registrar valores a Reiterar" 'obtenerRecurso(NoPuedeEliminarDatoReiteracion, Presentation.Base.Eform.RecepcionObjeciones)
                    End If
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = "Seleccione un item Glosa para registrar valores a Reiterar" 'obtenerRecurso(NoPuedeEliminarDatoReiteracion, Presentation.Base.Eform.RecepcionObjeciones)
            End If
        End If
        Me.INDgleGeneralConcept.Focus()
    End Sub

    Private Sub INDbntUpdate_Click(sender As Object, e As EventArgs) Handles INDbntUpdate.Click
        Me.UpdateMov()
    End Sub

    Private _entityToUpdate As GlosaMovementGlosa
    ''' <summary>
    ''' Click Modificar Movimiento
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateMov()
        If StatusObjectionD <> 2 Then
            If INDgdvObjections.FocusedRowHandle >= 0 Then
                Dim ObjUpdate As GlosaMovementGlosa = CType(INDgdvObjections.GetFocusedRow, GlosaMovementGlosa)
                _entityToUpdate = CType(INDgdvObjections.GetRow(INDgdvObjections.FocusedRowHandle), GlosaMovementGlosa)
                If ObjUpdate IsNot Nothing Then
                    If Reiteration = True Then
                        If ObjUpdate.Responsible IsNot Nothing Then
                            INDgleGeneralConcept.ReadOnly = True
                            INDgleGeneralConcept.EditValue = ObjUpdate.CodeGlosaId
                            INDgleGeneralConcept.Properties.NullText = ObjUpdate.ConceptGlosasCodeName
                            INDgleResponsible.EditValue = ObjUpdate.ResponsibleId
                            INDtxtValue.EditValue = ObjUpdate.ValueReiterated
                            INDmemComment.Text = ObjUpdate.RationaleReiteration
                        End If
                    Else
                        INDgleGeneralConcept.EditValue = ObjUpdate.CodeGlosaId
                        INDgleResponsible.EditValue = ObjUpdate.ResponsibleId
                        INDtxtValue.EditValue = ObjUpdate.ValueGlosado
                        INDmemComment.Text = ObjUpdate.RationaleGlosa
                    End If
                    INDAddorUpdateBtn.Text = "Modificar"
                    INDpceAddItemGloss.ShowPopup()
                End If
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = "El detalle no puede ser modificado si la factura ya se encuentra confirmada"
        End If
    End Sub

    Private Sub CleanPopupControls()
        Me.INDgleGeneralConcept.EditValue = Nothing
        Me.INDgleGeneralConcept.Properties.NullText = String.Empty
        Me.INDgleResponsible.EditValue = Nothing
        Me.INDgleResponsible.Properties.NullText = String.Empty
        Me.INDtxtValue.EditValue = 0
        Me.INDmemComment.Text = String.Empty
        Me.INDgleGeneralConcept.Focus()
        INDgleGeneralConcept.ReadOnly = False
        _entityToUpdate = Nothing
    End Sub

    Public Property CodeGeneralConcept As String Implements IRegisterObjection.CodeGeneralConcept

    Private Sub INDtxtValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtValue.EditValueChanged
        'If INDtxtValue.EditValue IsNot Nothing AndAlso TypeOf INDtxtValue.EditValue Is Decimal AndAlso INDtxtValue.EditValue < 0 Then
        If INDtxtValue.EditValue IsNot Nothing AndAlso INDtxtValue.EditValue < 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "El valor glosado no puede ser negativo"
            INDtxtValue.EditValue = INDtxtValue.OldEditValue
            INDtxtValue.Focus()
        End If
    End Sub

    Private Sub FrmRegisterObjection_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Close()
        End If
    End Sub

    Private Sub INDgdvObjections_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDgdvObjections.CustomColumnDisplayText
        If e.Column.Name = INDColNormative.Name Then
            e.DisplayText = IIf(e.Value, "Normativo", "No normativo")
        End If
    End Sub
#End Region

End Class