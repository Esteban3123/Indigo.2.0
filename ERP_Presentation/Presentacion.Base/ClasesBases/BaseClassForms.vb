'***********************************************************************
' Assembly         : Presentacion.Base
' Author           : Julian Cardozo
' Created          : 15-04-2011
'
' Last Modified By : Julian Cardozo
' Last Modified On : 21-11-2011
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"
Imports Presentation.CloudAgent
Imports Presentation.CloudAgent.IndigoReference.Security
Imports Infrastructure.CrossCutting.Base
Imports Domain.Security.Entities
#End Region
''' <summary>
''' Clase Base para los funcionales que contiene todas aquellas funciones y metodos que sean comunes en los diferentes proyectos
''' que se agregen a la solucion
''' </summary>
Public Class BaseClassForms

    Implements IDisposable


    Dim Indigo As SessionValues = SessionValues.Instance

    Private _PermisoCustomizar As Boolean
    Property PermisoCustomizar As Boolean
        Get
            Return _PermisoCustomizar
        End Get
        Set(ByVal value As Boolean)
            _PermisoCustomizar = value
        End Set
    End Property
    ''' <summary>
    ''' Funcion que me permite consultar los permisos del usuario actual
    ''' </summary>
    ''' <returns>true si el usuario tiene permisos o false si el usuario no tiene permisos</returns>
    Public Function ConsultarPermisos(ByVal Tag As String) As Boolean

        'funcion para establecer los permisos del usuario
        Dim permiso As List(Of PermissionUserToolbar) = IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListPermissionsUserToolbar(Indigo.UserIndigo, Indigo.UserRol, Tag, Me.Indigo)
        'Si el usuario tiene permisos lo deja continuar de lo contrario le muestra un mensaje
        If permiso.Where(Function(f) f.TagButton = 40).Count = 0 Then
            Return False
        Else
            Return True
        End If
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
