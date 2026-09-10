'***********************************************************************
' Assembly         : Presentacion.Seguridad.MVP
' Author           : Julian Cardozo
' Created          : 03-03-2011
'
' Last Modified By : Jose Paez
' Last Modified On : 23-03-2011
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"
Imports Presentation.Base   
Imports Presentation.Controls
Imports Presentation.CloudAgent.IndigoReference.Security
Imports Domain.Security.Entities
#End Region
''' <summary>
''' Interfaz IRoles que contiene metodos y propiedades para el funcional de ROLES
''' </summary>
Public Interface IRoles
    Inherits IcrudBase
#Region "Propiedades"

    ''' <summary>
    ''' Esta propiedad sirve para activar o desactivar controles del frontal
    ''' </summary>
    WriteOnly Property ActivarControles As Boolean
    ''' <summary>
    ''' Esta propiedad contiene el codigo  del rol
    ''' </summary>
    Property Codigo As String
    ''' <summary>
    ''' Esta propiedad contiene en nombre del rol
    ''' </summary>
    Property Nombre As String
    ''' <summary>
    ''' Esta propiedad hace el data source a la rejilla
    ''' </summary>
    WriteOnly Property DataSourceRoles As Object
    ''' <summary>
    ''' Obtiene o establece la lista permiso rol.
    ''' </summary>
    ''' <value>The lista permiso rol.</value>
    Property ListaPermisoRol As List(Of PermissionRoll)
    ''' <summary>
    ''' Obtiene o establece la listar todos permisos rol.
    ''' </summary>
    ''' <value>The listar todos permisos rol.</value>
    Property ListarTodosPermisosRol As List(Of PermissionRoll)
    ''' <summary>
    ''' Esta propiedad contiene el tipo de rol
    ''' </summary>
    ''' <returns></returns>
    Property PRollType As Byte?
    ''' <summary>
    ''' Esta propiedad contiene el id de tenant
    ''' </summary>
    ''' <returns></returns>
    Property TenantId As Short?
    ''' <summary>
    ''' Esta propiedad hace el data source al control de tenant
    ''' </summary>
    ''' <returns></returns>
    Property TenantDataSource As DevExpress.Xpo.XPServerCollectionSource

    Sub AsyncLoader(ByVal Value As Boolean)
#End Region

End Interface
