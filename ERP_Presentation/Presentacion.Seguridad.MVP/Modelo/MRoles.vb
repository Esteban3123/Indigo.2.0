'***********************************************************************
' Assembly         : Presentacion.Seguridad.MVP
' Author           : Julian Cardozo
' Created          : 03-03-2011
'
' Last Modified By : Julian Cardozo
' Last Modified On : 27-03-2011
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Importaciones"

Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent.IndigoReference.Security
Imports Domain.Security.Entities
Imports Presentation.CloudAgent
Imports Presentation.Base

#End Region
''' <summary>
''' Clase Modelos que me comunica con los servicios para poder consumirlos
''' </summary>
Public Class MRoles
    Implements IDisposable


    ''' <summary>
    ''' Inicializa una nueva instancia de MfrmUnidadFuncional.
    ''' </summary>
    Sub New()
        'igualamos a los valores de sesion el nombre del formulario para poder auditar.
        Dim Mensaje As New Infrastructure.CrossCutting.Base.AuditMessage
        Mensaje.Functional = Eform.Roles.ToString

    End Sub
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

#Region "Funciones"
    Public Function GetRolByIdAndPermissionRollByIdForm(idRol As Integer, idform As String) As Roll
        Return IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetRolByIdAndPermissionRollByIdForm(idRol, idform)
    End Function
    ''' <summary>
    ''' Funcion para consultar el ROL dependiendo del codigo envido a la capa de servicios.
    ''' </summary>
    Public Async Function ConsultarNombreRol(ByVal codigo As String) As Threading.Tasks.Task(Of Roll)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetRoleByCodeRoleAsync(codigo, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para Obtener todos los roles .
    ''' </summary>
    Friend Async Function ObtenerTodosLosRoles() As Threading.Tasks.Task(Of IEnumerable(Of Roll))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListRolesAsync(Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para Consultar los Permisos de los Roles.
    ''' </summary>
    Friend Async Function ConsultarPermisosRoles(ByVal codigoRol As String, ByVal codigoMenu As String) As Threading.Tasks.Task(Of List(Of PermissionRoll))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListPermissionsRoleByRoleAndMenuAsync(codigoRol, codigoMenu, Me.Indigo)
    End Function

    ''' <summary>
    ''' Guardar el listado de objetos roles.
    ''' </summary>
    Friend Async Function GuardarListadoRoles(ByVal rol As Roll) As Threading.Tasks.Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.SavePermissionsRoleAsync(rol, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para Eliminar Rol Seleccionado .
    ''' </summary>
    Friend Async Function EliminarRoles(ByVal rol As Roll) As Threading.Tasks.Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.DeleteRoleAsync(rol, Me.Indigo)
    End Function

    ''' <summary>
    ''' Consultar Todos los permisos del rol
    ''' </summary>
    Public Async Function ConsultarTodosPermisosRol(ByVal codigoRol As String) As Threading.Tasks.Task(Of List(Of PermissionRoll))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListPermissionsRoleByCodeRoleAsync(codigoRol.ToString.Trim, Me.Indigo)
    End Function

    ''' <summary>
    ''' Este metodo consulta el listado de formularios con acciones.
    ''' </summary>
    Public Async Function ConsultarTodosForms() As Threading.Tasks.Task(Of List(Of VieForm))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListFormsAsync(Me.Indigo)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

