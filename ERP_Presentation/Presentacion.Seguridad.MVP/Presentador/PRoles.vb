'***********************************************************************
' Assembly         : Presentacion.Seguridad.MVP
' Author           : Julian Cardozo
' Created          : 03-03-2011
'
' Last Modified By : Julian Cardozo
' Last Modified On : 1-05-2011
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Importaciones"
Imports Presentation.Controls
Imports Presentation.CloudAgent.IndigoReference.Security
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.Utils.Extensions
#End Region
''' <summary>
''' Este presentador captura toda la logica aplicada en el frontal FrmRoles
''' </summary>
Public Class PRoles

#Region "variable y constructor"

    ''' <summary>
    ''' Constructor creado para permitir comunicacion con la vista 
    ''' </summary>
    ''' <param name="iview">The iview.</param>
    Public Sub New(ByRef iview As IRoles)
        If iview Is Nothing Then
            Throw New ArgumentException(obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.iview = iview
        End If
    End Sub

    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz IRoles
    ''' </summary>
    Dim iview As IRoles

    ''' <summary>
    ''' Variable utilizada para instanciar la entidad SeguridadRolesUsuario y hacer los respectivos insert
    ''' </summary>
    Dim RolInsert As New Roll

    ''' <summary>
    ''' Variable utilizada para el objeto SeguridadRolesUsuario
    ''' </summary>
    ''' 
    Private _Rol As Roll
    Property Rol As Roll
        Get
            Return _Rol
        End Get
        Set(value As Roll)
            _Rol = value
        End Set
    End Property

    ''' <summary>
    ''' Variable utilizada para el objeto de formularios de DB
    ''' </summary>
    ''' 
    Private _Form As List(Of VieForm)
    Property ListForm As List(Of VieForm)
        Get
            Return _Form
        End Get
        Set(value As List(Of VieForm))
            _Form = value
        End Set
    End Property

#End Region

#Region "metodos"

    ''' <summary>
    ''' Este metodo elimina el rol de la base de datos.
    ''' </summary>
    ''' <param name="codigo"></param>
    ''' <remarks></remarks>
    Public Async Sub EliminarRoles(ByVal codigo As String)
        If Rol Is Nothing Then
            iview.Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesDigiteDatos)
        Else
            Dim modelo As New MRoles
            Dim resultado As Boolean
            iview.AsyncLoader(True)
            resultado = Await modelo.EliminarRoles(Rol)

            If resultado = True Then
                iview.Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                iview.AsyncLoader(False)
                iview.ActivarControles = True
                iview.Deshacer()
                iview.OpenSearch()
            Else
                iview.Mensaje(EeventViewerImages.MensajeError) = "Se presento un error, el objeto no se pudo borrar"
                iview.AsyncLoader(False)
                iview.ActivarControles = True
            End If
        End If

    End Sub

    ''' <summary>
    ''' Consultar Todos los permisos del rol .
    ''' </summary>
    Public Async Function ConsultarTodosPermisosRol() As Threading.Tasks.Task
        Dim modelo As New MRoles
        If Rol Is Nothing Then
            Exit Function
        End If
        iview.AsyncLoader(True)
        iview.ListarTodosPermisosRol = Await modelo.ConsultarTodosPermisosRol(Rol.Id.ToString)
        iview.AsyncLoader(False)
    End Function

    ''' <summary>
    ''' este metodo sirve para dejar vacios todos los campos del frontal y bloquear los controles
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer()
        'Limpio todos los controles de texto
        iview.Codigo = String.Empty
        iview.Nombre = String.Empty
        iview.Codigo = String.Empty
        iview.Nombre = String.Empty
        iview.PRollType = Nothing
        'Desactiva todos los controles
        iview.ActivarControles = False
    End Sub

    ''' <summary>
    ''' Este metodo Consulta el nombre.
    ''' </summary>
    ''' <param name="codigo">The codigo.</param> 
    Public Async Function ConsultarNombreRol(ByVal codigo As String) As Threading.Tasks.Task
        Dim modelo As New MRoles

        Rol = Await modelo.ConsultarNombreRol(codigo)


    End Function

    ''' <summary>
    ''' este metodo sirve para guardar un nuevo rol
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function GuardarLosRoles() As Threading.Tasks.Task
        Dim resultado As Boolean
        Dim modelo As New MRoles
        Rol.RollCode = iview.Codigo.ToString.Trim
        Rol.Description = iview.Nombre.ToString.Trim
        Rol.RollType = iview.PRollType
        If CType(Rol.RollType, eRollType) = eRollType.GlobalType AndAlso Rol.TenantRoll IsNot Nothing AndAlso Rol.TenantRoll.Count > 0 AndAlso Rol.TenantRoll.Any(Function(tr) tr.ChangeTracker.State <> Domain.Base.Entities.ObjectState.Deleted) Then
            Rol.TenantRoll.ToList.ForEach(Sub(tr)
                                              tr.ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                                              Rol.TenantRoll.Add(tr)
                                          End Sub)
        End If
        iview.AsyncLoader(True)
        resultado = Await modelo.GuardarListadoRoles(Rol)
        iview.AsyncLoader(False)
        If resultado = True Then
            iview.Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
            Deshacer()
        Else
            iview.Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
        End If
    End Function

    ''' <summary>
    ''' Consulta todos los tenants activos
    ''' </summary>
    ''' <param name="UserId"></param>
    ''' <param name="pUserType"></param>
    Public Sub GetAllTenant(ByVal UserId As Integer, ByVal pUserType As UserType)
        Using modelo As New MUsuario
            iview.TenantDataSource = modelo.GetAllTenant(UserId, pUserType)
        End Using
    End Sub

    ''' <summary>
    ''' Este metodo consulta el listado de formularios con acciones.
    ''' </summary>
    ''' <param name="codigo">The codigo.</param> 
    Public Async Function ConsultarTodosForms() As Threading.Tasks.Task
        Dim modelo As New MRoles
        ListForm = Await modelo.ConsultarTodosForms()
    End Function

#End Region
End Class
