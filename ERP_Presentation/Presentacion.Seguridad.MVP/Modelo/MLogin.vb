
'***********************************************************************
' Assembly         : Presentacion.Cliente.MVP
' Author           : Andres Bonilla
' Created          : 03-03-2011
'
' Last Modified By : Andres Bonilla
' Last Modified On : 27-03-2011
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"
Imports Domain.Base.Entities
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
#End Region
''' <summary>
''' Clase del Modelo del Login
''' </summary>
Public Class MLogin
    Implements IDisposable
#Region "Variables"

    ''' <summary>
    ''' Variable de session
    ''' </summary>
    Private _indigoSession As SessionValues = SessionValues.Instance
#End Region

#Region "Funciones"


    '''' <summary>
    '''' Funcion que retorna true si el usuario es validado correctamente.
    '''' </summary>
    'Friend Function ValidarUsuario(ByVal codigoUsuario As String, ByVal contraseña As String) As Boolean
    '    Return IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ValidateUser(codigoUsuario, contraseña, Me._indigoSession)
    'End Function

    '''' <summary>
    '''' Funcion que retorna el objeto [Entidad] SeguridadUsuario
    '''' </summary>
    'Friend Function ConsultarUsuario(codigoUsuario As String) As User
    '    Dim objetoSeguridadUsuario As User
    '    objetoSeguridadUsuario = IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetUserByCodeUser(codigoUsuario, Me._indigoSession)
    '    If Not objetoSeguridadUsuario Is Nothing Then
    '        If objetoSeguridadUsuario.Id <> 0 Then
    '            'Elimino los espacion del objeto devuelto por el serivico
    '            objetoSeguridadUsuario.Position = objetoSeguridadUsuario.Position.Trim
    '            objetoSeguridadUsuario.Person.Fullname = objetoSeguridadUsuario.Person.Fullname.Trim
    '            Return objetoSeguridadUsuario
    '        End If
    '    End If
    '    Return Nothing
    'End Function

    ''' <summary>
    ''' Funcion que retorna el Listado del Objeto [Entidad] CentroAtencion
    ''' </summary>
    'Public Function ConsultarCentroAtencion() As List(Of CentroAtencion)
    '    Dim objetoCentroAtencion As New List(Of CentroAtencion)
    '    Return IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetCareCenter()
    'End Function

    ''' <summary>
    ''' Funcion que retorna el Listado del Objeto [Entidad] UnidadFuncional
    ''' </summary>
    'Friend Function ConsultarUnidadFuncional(codigoCentro As String) As List(Of UnidadFuncional)
    '    Dim objetoUnidadFuncional As New List(Of UnidadFuncional)
    '    Return IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetUnitsFunctional(codigoCentro)

    '    For i = 0 To objetoUnidadFuncional.Count - 1
    '        ' objetoUnidadFuncional.Item(i).Nombre = objetoUnidadFuncional.Item(i).Nombre.Trim()

    '    Next
    '    Return objetoUnidadFuncional

    'End Function

    ''' <summary>
    ''' Funcion que retorna el Listado del Objeto [Entidad] Empresa
    ''' </summary>
    'Public Function ConsultarEmpresasExistentes(ByVal Origen As QuerySource) As List(Of CompanyIndigo)
    '    Dim objetoEmpresa As List(Of CompanyIndigo)
    '    objetoEmpresa = IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListCompanies(Origen, Me._indigoSession)
    '    For i = 0 To objetoEmpresa.Count - 1
    '        objetoEmpresa.Item(i).CompanyCode = objetoEmpresa.Item(i).CompanyCode.Trim()
    '        objetoEmpresa.Item(i).CompanyName = objetoEmpresa.Item(i).CompanyName.Trim()
    '    Next
    '    Return objetoEmpresa

    'End Function

    ''' <summary>
    ''' Funcion que Almacena Variables de Login CentroAtencion - UnidadFuncional - PerfilUsuario
    ''' </summary>
    Friend Function GuardarComo(ByVal codigoUsuario As String, ByVal perfil As Integer, ByVal centroAtencion As String, ByVal unidadFuncional As String) As Boolean
        Return IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.SaveLoginLocation(codigoUsuario, perfil, centroAtencion, unidadFuncional, Me._indigoSession)
    End Function

    ''' <summary>
    ''' Funcion que me sirve para consultar la fecha del servidor
    ''' </summary>
    Friend Function ConsultarFechaServidor() As Date
        Return IndigoConecta.Instancia.CurrentCloud.IndigoComunes.GetServerDate
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: eliminar estado administrado (objetos administrados).
            End If
            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el modelo descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)

        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
