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
Imports Domain.Base.Entities
Imports System.Data
Imports Domain.Security.Entities
#End Region
''' <summary>
''' Definicion de interfaz que contiene metodos y propiedades para ser implementada en el formulario FrmGrupos
''' </summary>
Public Interface IGrupos
    Inherits ICrudBase

#Region "Propiedades"
    Property BarraBotones As CtrBarraBotones
    ''' <summary>
    ''' esta propiedad contiene el codigo del grupo
    ''' </summary>
    Property CodigoDelGrupo As String
    ''' <summary>
    ''' esta propiedad contiene el nombre del grupo
    ''' </summary>
    Property NombreDelGrupo As String
    ''' <summary>
    ''' esta propiedad sirve para activar o desactivar controles
    ''' </summary>
    WriteOnly Property ActivarControles As Boolean
    ''' <summary>
    ''' Obtiene o establece un mensaje de desicion .
    ''' </summary> ''' 
    Property MensajeDecision As String
    ''' <summary>
    ''' Establece el foco en un campo especifico.
    ''' </summary>
    WriteOnly Property EstablecerFoco(NombreControl As String) As Boolean

    ReadOnly Property Instance As FormBase

    ReadOnly Property Tag As String
    Property dtDetails As DataTable
    Property ListaEliminados As List(Of Integer)

    ''' <summary>
    ''' Esta propiedad contiene el tipo de grupo
    ''' </summary>
    ''' <returns></returns>
    Property PGroupType As Byte?
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
    ''' <summary>
    ''' Obtiene el tenant seleccionado
    ''' </summary>
    ReadOnly Property GetSelectedTenant As Object
    ''' <summary>
    ''' Asigna data source de tenant-group
    ''' </summary>
    WriteOnly Property TenantGroupDataSource As IEnumerable(Of TenantGroup)

    Sub AsyncLoader(ByVal Value As Boolean)

#End Region

#Region "Funciones"

#End Region

End Interface
