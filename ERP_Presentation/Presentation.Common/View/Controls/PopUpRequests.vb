#Region "Imports"

Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.AuthorizationRepository
Imports Presentation.Authorization.MVP
Imports Presentation.Base
Imports Presentation.Controls

#End Region

Public Class PopUpRequests

#Region "Builder"

    ''' <summary>
    ''' este constructor se utiliza para inicializar los campos de datos dentro de una clase
    ''' con los valores proporcionados en los parámetros entityName, entityId y itemCode (si se proporciona)
    ''' </summary>
    Sub New(entityName As String, entityId As Integer, Optional itemCode As String = Nothing)
        InitializeComponent()

        Me._entityName = entityName
        Me._entityId = entityId
        Me._itemCode = itemCode
    End Sub

#End Region

#Region "Consts"

    Private Const NAME_MODULE As String = "Authorization"

#End Region

#Region "Variables"

    Dim _entityName As String

    Dim _entityId As Integer

    Dim _itemCode As String

    Dim _viewRequests As ViewRequestsXpo

#End Region

#Region "Properties"

    ''' <summary>
    '''  esta propiedad MyTag permite acceder al valor de la propiedad Tag del objeto en el que se está utilizando.
    ''' </summary>
    Private ReadOnly Property MyTag As Object
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' esta propiedad MyLayoutControl permite acceder al valor de la propiedad LayoutControls del objeto en el que se está utilizando.
    ''' </summary>
    Private ReadOnly Property MyLayoutControl As IndigoLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' esta propiedad Mensaje permite mostrar diferentes tipos de mensajes utilizando la clase MessageIndigo según el valor del parámetro Icono.
    ''' </summary>
    Private WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
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

#Region "Datasource"

    ''' <summary>
    ''' Esta variable está destinada a almacenar una lista de tuplas, donde cada tupla contiene un valor entero y una cadena de texto.
    ''' </summary>
    Private _FillingType As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' La propiedad asegura que la lista de tuplas se inicialice y se llene con los valores adecuados antes de devolverla.
    ''' </summary>
    Private ReadOnly Property FillingType As List(Of Tuple(Of Integer, String))
        Get
            If _FillingType Is Nothing Then
                _FillingType = New List(Of Tuple(Of Integer, String))
                _FillingType.Add(New Tuple(Of Integer, String)(1, "Servicio"))
                _FillingType.Add(New Tuple(Of Integer, String)(2, "Producto"))
            End If
            Return _FillingType
        End Get
    End Property


    ''' <summary>
    ''' Esta línea de código declara una variable privada llamada _FillingYesAndNot
    ''' que se espera que sea utilizada en alguna parte del código para almacenar una lista de tuplas
    ''' </summary>
    Private _FillingYesAndNot As List(Of Tuple(Of Boolean, String))

    ''' <summary>
    ''' Esta propiedad es útil para proporcionar una manera fácil y consistente de acceder a estas opciones "Sí" 
    ''' y "No" en el código sin tener que crear la lista manualmente cada vez que sea necesario.
    ''' </summary>
    Private ReadOnly Property FillingYesAndNot As List(Of Tuple(Of Boolean, String))
        Get
            If _FillingYesAndNot Is Nothing Then
                _FillingYesAndNot = New List(Of Tuple(Of Boolean, String))
                _FillingYesAndNot.Add(New Tuple(Of Boolean, String)(True, "Si"))
                _FillingYesAndNot.Add(New Tuple(Of Boolean, String)(False, "No"))
            End If
            Return _FillingYesAndNot
        End Get
    End Property

#End Region

#Region "BarraBotones"

    ''' <summary>
    ''' este manejador de eventos se encarga de asegurarse de que la barra de botones
    ''' muestre y habilite las acciones adecuadas en función de los permisos del usuario
    ''' </summary>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

#End Region

#Region "Handles"

#Region "Load"


    ''' <summary>
    '''  este manejador de eventos se encarga de configurar y cargar datos en controles del formulario,
    '''  y posiblemente ejecuta otras operaciones de inicialización antes de que el formulario esté completamente
    '''  listo para su interacción
    ''' </summary>
    Private Sub PopUpRequests_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyAuthorization, True)
        'Cargar GridLookUpEdit
        Me.INDGleType.Properties.DataSource = FillingType
        Me.INDGleFinancedResourceUPC.Properties.DataSource = FillingYesAndNot
        Me.INDGleCovered.Properties.DataSource = FillingYesAndNot
        Me.INDGleContracted.Properties.DataSource = FillingYesAndNot
        Me.INDGleQuoted.Properties.DataSource = FillingYesAndNot
        Me.INDGleAuthorized.Properties.DataSource = FillingYesAndNot
        '******************************'
        LoadControls()
        '******************************'
        If _viewRequests Is Nothing Then
            Me.Close()
        End If
    End Sub


    ''' <summary>
    '''  este manejador de eventos se asegura de liberar cualquier recurso o referencia a objetos que 
    '''  el formulario haya estado utilizando antes de que el formulario se elimine y se liberen sus recursos en la memoria.
    ''' </summary>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _entityName = Nothing
        _entityId = Nothing
        _itemCode = Nothing
        _viewRequests = Nothing

        _FillingType = Nothing
        _FillingYesAndNot = Nothing
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    '''  este fragmento de código carga los datos de una solicitud en varios controles del formulario
    '''  y muestra mensajes de advertencia si no se encuentra la solicitud o si no se tienen permisos para consultar. 
    ''' </summary>
    Private Sub LoadControls()
        If Me.BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If

        Using Model As New MRequests(Me.MyTag)
            AsyncLoader(True)
            Me._viewRequests = Model.GetRequest(_entityName, _entityId, _itemCode)
            If _viewRequests IsNot Nothing Then
                INDlyAuthorization.BeginUpdate()
                With _viewRequests
                    INDTePacient.EditValue = .Patient
                    INDTeAdmissionNumber.EditValue = .AdmissionNumber
                    INDTeFolio.EditValue = .Folio
                    INDTeCareCenter.EditValue = .CareCenter
                    INDTeFunctionalUnit.EditValue = .FunctionalUnit
                    INDTeCareGroup.EditValue = .CareGroup
                    INDTeHealthAdministrator.EditValue = .HealthAdministrator

                    INDDeRequestDate.EditValue = .RequestDate
                    INDTeProfessional.EditValue = .Professional
                    INDGleType.EditValue = .Type
                    INDTeItem.EditValue = .ItemCodeName
                    INDTeQuantity.EditValue = .Quantity
                    INDGleFinancedResourceUPC.EditValue = .FinancedResourceUPC

                    INDGleCovered.EditValue = .Covered
                    INDGleContracted.EditValue = .Contracted
                    INDGleQuoted.EditValue = .Quoted
                    INDGleAuthorized.EditValue = .Authorized
                    INDTeStatus.EditValue = .StatusName

                    If String.IsNullOrEmpty(.StatusName) Then
                        INDLciStatus.HideControl(True)
                    End If
                End With

                Me.BarraBotones.PrepareToolbar(eAction.None)
                Me.BarraBotones.SetDocuments(_viewRequests.EntityId, Me.MyTag, Nothing, _viewRequests.EntityName)

                INDlyAuthorization.EndUpdate()
                AsyncLoader(False)
            Else
                AsyncLoader(False)
                Mensaje(EeventViewerImages.Advertencia) = "No se encontró la solicitud"
            End If
        End Using
    End Sub

#End Region

End Class