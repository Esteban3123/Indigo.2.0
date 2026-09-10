Imports Presentation.CloudAgent.IndigoReference.CommonERP
Imports Presentation.CloudAgent.IndigoReference.Security
Imports Presentation.CloudAgent
Imports Domain.Security.Entities
Imports Domain.Entities

'***********************************************************************
' Assembly         : Presentacion.Seguridad.MVP
' Author           : Andres Bonilla
' Created          : 03-03-2011
'
' Last Modified By : Andres Bonilla
' Last Modified On : 27-03-2011
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Domain.Base.Entities
Imports Infrastructure.Data.Xpo.SecurityRepository

''' <summary>
''' Clase Modelo de la Barra de Botones
''' </summary>
Public Class MBarraBotones
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Variable de session
    ''' </summary>
    Private _indigoSession As SessionValues = SessionValues.Instance

    Private _listForms As List(Of VieForm) = Nothing

#End Region

    ''' <summary>
    ''' Lista los formularios disponibles
    ''' </summary>
    ''' <param name="moduleId">Id del modulo</param>
    ''' <returns>Lista de formularios</returns>
    Public Function ListForms(ByVal IdForm As String) As VieForm
        'Return BaseClass.GetXmlWithAggregates(Of VieForm)(eDataXml.XMLForms).Where(Function(f) f.IdModuleSource = moduleId AndAlso f.HasSequence).ToList()
        If _listForms Is Nothing Then
            _listForms = IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListForms(Me._indigoSession)
        End If

        Return _listForms.Where(Function(f) f.Id = IdForm).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Lista la auditoria basica
    ''' </summary>
    ''' <returns>Lista de Auditoria</returns>
    Public Function ListAuditBasic(Month As String, Year As String, IdForm As String, IdEntity As String) As DevExpress.Xpo.XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSession.SecurityContainer).SecurityService.ListBasicAudit(Month, Year, IdForm, IdEntity, _indigoSession.IndigoCompany)
    End Function

    ''' <summary>
    ''' Lista la auditoria basica eliminada
    ''' </summary>
    ''' <returns>Lista de Auditoria</returns>
    Public Function ListAuditBasicDelete(Month As String, Year As String, IdForm As String) As DevExpress.Xpo.XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSession.SecurityContainer).SecurityService.ListBasicAuditDelete(Month, Year, IdForm, _indigoSession.IndigoCompany)
    End Function

    ''' <summary>
    ''' Consultar los permisos que tiene el usuario.
    ''' </summary>
    Friend Function ConsultarPermisosUsuario(ByVal codigoUsuario As String, ByVal codigoRol As String, ByVal codigoFormulario As String) As List(Of PermissionUserToolbar)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListPermissionsUserToolbar(codigoUsuario, codigoRol, codigoFormulario, Me._indigoSession)
    End Function

    ''' <summary>
    ''' Consultar la auditoria basica de un registro.
    ''' </summary>
    Public Async Function GetAuditBasicByEntity(ByVal IdForm As String, ByVal IdEntity As String) As Threading.Tasks.Task(Of List(Of BasicAudit))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListBasicAuditByIdIdFormAndIdEntityAsync(IdForm, IdEntity, _indigoSession)
    End Function

    ''' <summary>
    ''' Consultar la auditoria basica con estado eliminado según formulario.
    ''' </summary>
    Public Async Function GetDeleteAuditBasicByTag(ByVal IdForm As String) As Threading.Tasks.Task(Of List(Of BasicAudit))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListBasicAuditByTagAsync(IdForm, Me._indigoSession)
    End Function

    ''' <summary>
    ''' Guardar Auditoria Basica.
    ''' </summary>
    Public Async Function SaveBasicAudit(Id As String, NameEntity As String, Parameters As String, ReportName As String, ActionAudit As ActionsAudit, Optional ByVal count As Integer = 1) As Threading.Tasks.Task(Of ActionResult(Of BasicAudit))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveBasicAuditAsync(Id, NameEntity, Parameters, ReportName, ActionAudit, Me._indigoSession, count)
    End Function

    Public Async Function SaveWeightPatient(weight As Integer, codePatient As String) As Threading.Tasks.Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.SaveWeightPatientAsync(weight, codePatient)
    End Function

    ''' <summary>
    ''' Funcion Para Consultar Los Permisos del Usuario en todos los formularios de la aplicacion
    ''' </summary>
    ''' <param name="CodigoRol"> codigo rol.</param>
    ''' <param name="CodigoUsuario"> codigo usuario.</param>
    ''' <returns></returns>
    Public Async Function ConsultarPermisos(ByVal CodigoRol As String, ByVal CodigoUsuario As String) As Threading.Tasks.Task(Of List(Of PermissionUserToolbar))
        Dim PermisosBarraUsuario As PermissionUserToolbar
        Dim EncontroRegisto As Boolean
        Dim _ListadoPermisosBarraUsuario As List(Of PermissionUserToolbar)
        Dim _ListadoPermisosRoles As List(Of PermissionRoll) = Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListPermissionsRoleByCodeRoleAsync(CodigoRol, Me._indigoSession)
        Dim _ListadoPermisosUsuarios As List(Of PermissionUser) = Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListPermissionsUserByCodeUserAsync(CodigoUsuario, Me._indigoSession)

        _ListadoPermisosBarraUsuario = New List(Of PermissionUserToolbar)
        For Usu As Integer = 0 To _ListadoPermisosUsuarios.Count - 1
            'cargo en la lista primero lo permisos del rol
            For Rol As Integer = 0 To _ListadoPermisosRoles.Count - 1
                If _ListadoPermisosUsuarios.Item(Usu).Action.ToString.Trim = _ListadoPermisosRoles.Item(Rol).Action.ToString.Trim Then
                    EncontroRegisto = True
                    Exit For
                Else
                    EncontroRegisto = False
                End If
            Next

            'cuando el rol y el usuario no tienen el mismo permiso, 
            'lo adiciono a la lista de permisos de la barra de usuarios
            If EncontroRegisto = False Then
                PermisosBarraUsuario = New PermissionUserToolbar
                PermisosBarraUsuario.TagButton = CInt(_ListadoPermisosUsuarios.Item(Usu).Action)
                PermisosBarraUsuario.TagForm = CInt(_ListadoPermisosUsuarios.Item(Usu).IdForm)
                _ListadoPermisosBarraUsuario.Add(PermisosBarraUsuario)
            End If
        Next
        'adicionar los pemrisos encontrados en el rol
        ' a la lista de permisos de la barra
        For Rol As Integer = 0 To _ListadoPermisosRoles.Count - 1
            PermisosBarraUsuario = New PermissionUserToolbar
            PermisosBarraUsuario.TagButton = CInt(_ListadoPermisosRoles.Item(Rol).Action)
            PermisosBarraUsuario.TagForm = CInt(_ListadoPermisosRoles.Item(Rol).IdForm)
            _ListadoPermisosBarraUsuario.Add(PermisosBarraUsuario)
        Next
        Return _ListadoPermisosBarraUsuario
    End Function

    ''' <summary>
    ''' funcion para obtener un usuario con el codigo
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    Public Function GetUserbyCode(Code As String) As Security_Users
        Return XpoServiceEx.Instance(Me._indigoSession.SecurityContainer).SecurityService.GetXPOObject(Of Security_Users)($"UserCode='{Code}'")
    End Function

    ''' <summary>
    '''  Obtiene la ruta del informe para un usuario específico, un contenedor dado y una unidad operativa.
    ''' </summary>
    ''' <param name="idUser"></param>
    ''' <param name="idContainer"></param>
    ''' <param name="idOperatingUnit"></param>
    ''' <returns>Ruta del informe como cadena.</returns>
    Public Function GetReportPathByUser(idUser As Integer, Optional idContainer As Integer = 0, Optional idOperatingUnit As Integer = 0) As String
        Return IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetReportPathByUser(idUser, idContainer, idOperatingUnit)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
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
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
