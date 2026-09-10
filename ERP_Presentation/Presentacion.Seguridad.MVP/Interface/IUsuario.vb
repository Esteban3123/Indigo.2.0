'***********************************************************************
' Assembly         : Presentacion.Seguridad.MVP
' Author           : Julian Cardozo
' Created          : 03-03-2011
'
' Last Modified By : Julian Cardozo
' Last Modified On : 23-03-2011
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Importaciones"

Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.CloudAgent.IndigoReference.Security
Imports Domain.Security.Entities
Imports Infrastructure.Data.Xpo.SecurityRepository
Imports DevExpress.Data.Linq
#End Region
''' <summary>
''' 	Interface utilizada para establecer las propiedades y metodos en la vista del funcional Usuario
''' </summary>
Public Interface IUsuario
    Inherits ICrudBase
#Region "propiedades"


    ''' <summary>
    ''' esta propiedad contiene el codigo del usuario.
    ''' </summary>
    Property CodigoDelUsuario As String
    ''' <summary>
    ''' esta propiedad contiene el nombre del usuario.
    ''' </summary>
    Property NombreDelUsuario As String
    ''' <summary>
    ''' esta propiedad contiene el template de la huella
    ''' </summary>
    Property Huella As Byte()
    ''' <summary>
    ''' esta propiedad contiene la contraseña del usuario.
    ''' </summary>
    Property ContraseñaDelUsuario As String
    ''' <summary>
    ''' contiene la contraseñaconfirmada de usuario.
    ''' </summary>
    Property ContraseñaConfirmada As String
    ''' <summary>
    ''' contiene el cargo asignado.
    ''' </summary>
    Property Cargo As String
    ''' <summary>
    ''' establece la fuente de datos grupos.
    ''' </summary>
    WriteOnly Property FuenteDEDatosGrupos As Object

    ''' <summary>
    ''' contiene el grupo asignado.
    ''' </summary>
    Property GrupoValueMember As String
    ''' <summary>
    ''' contiene el rol asignado.
    ''' </summary>
    Property RolValueMember As Integer?
    ''' <summary>
    ''' Propiedad para exigir un cambio de contraseña.
    ''' </summary>
    Property ExigirCambioContraseña As Boolean
    ''' <summary>
    ''' Esta propiedad obtiene o establece un valor del tiempo de caducidad de la contraseña
    ''' </summary>
    Property TiempoCaducidadContraseña As String

    ''' <summary>
    ''' Esta propiedad Obtiene o establece un la fecha de caducidad de la contraseña.
    ''' </summary>
    Property FechaCaducidadContraseña As Date
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property AccionesSobreLosControles As Boolean
    ''' <summary>
    ''' establece el valor para activa o desactivar el grupo seguridad.
    ''' </summary>
    Property ActivarGrupoSeguridad As Boolean
    '''' <summary>
    '''' establece el valor PermsisFuenteDeDatos
    '''' </summary>
    'WriteOnly Property DatasourcePermisosUsuarios As Object
    ''' <summary>
    ''' esta propiedad obtiene la fila seleccionada de los centros de atencion.
    ''' </summary>
    Property CentroAtencionFilaSeleccionada As Integer
    ''' <summary>
    ''' Propiedad que obtiene o establece la fecha del servidor.
    ''' </summary>
    ''' <value>The fecha servidor.</value>
    Property FechaServidor As DateTime
    ''' <summary>
    ''' Propiedad que me obtiene la fecha de caducidad de la contraseña del usuario .
    ''' </summary>
    Property FechaCaducidad As DateTime

    ReadOnly Property CorreoElectronicoDelUsuario As String

    WriteOnly Property HabilitarGrupoContraseña As Boolean

    'WriteOnly Property RolDatasource As Object
    Property User As User


    ''' <summary>
    ''' Esta Propiedad establece el foco en el control que mande en el parametro
    ''' </summary>
    WriteOnly Property EstablecerFoco(ByVal NombreControl As String) As Boolean

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property Tenants As DevExpress.Xpo.XPServerCollectionSource

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property TenantRolls As List(Of TenantRollXpo)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property TenantGroups As List(Of TenantGroupXpo)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property TenantId As Short?

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property RollId As Integer?

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property GroupId As Integer?

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property CompanyId As Integer?

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property Ciudad As String

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property Idioma As String

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property LightweightVersion As Boolean

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property RutaReportesPersonalizados As String

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property ShowThemeSkin As Boolean

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property ReporteadorActivo As Boolean

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property Email As String

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property PUserType() As Integer?

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property PProfileType() As Integer?

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property PTypeAlertControl() As Byte?


    ''' <summary>
    ''' Tipo ruta de reporte 
    ''' </summary>
    ''' <returns></returns>
    Property ReportPathType() As Byte?
#End Region

#Region "metodos"
    Sub AsyncLoader(ByVal Value As Boolean)
#End Region



End Interface
