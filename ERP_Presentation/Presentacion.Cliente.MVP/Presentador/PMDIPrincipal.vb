'***********************************************************************
' Assembly         : Presentacion.Cliente.MVP
' Author           : Andres Bonilla
' Created          : 03-03-2011
'
' Last Modified By : Jhon Tovar
' Last Modified On : 01-03-2022
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Clases Importadas"

Imports System.IO
Imports Infrastructure.CrossCutting.Base

#End Region

''' <summary>
''' Esta clase sirve para almacenar toda la logica del MDIPrincipal
''' </summary>
Public Class PmdiPrincipal
    Implements IDisposable

#Region "Variables Globales"

    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz del MDI
    ''' </summary>
    Dim modelo As MmdiPrincipal
    ''' <summary>
    ''' Variable utilizada para instanciar el modelo del MDI
    ''' </summary>
    Public Iview As IMdiPrincipal
    ''' <summary>
    ''' Variable para Valores de Sesion - Singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' Variable utilizada para el objeto de formularios de DB
    ''' </summary>
    ''' 
    Private _Form As List(Of Domain.Security.Entities.VieForm)
    Property ListForm As List(Of Domain.Security.Entities.VieForm)
        Get
            Return _Form
        End Get
        Set(value As List(Of Domain.Security.Entities.VieForm))
            _Form = value
        End Set
    End Property

#End Region

#Region "Metodos y Funciones"

    ''' <summary>
    ''' Metodo que sirve para restaurar los velores de session.
    ''' </summary>
    Sub RestaurarIndigo()
        Indigo = New SessionValues
    End Sub

    ''' <summary>
    ''' Este metodo verifica la existencia de un archivo Xml que contiene el tema seleccionado del usuario,
    ''' si no existe lo crea e inicializa con el tema por default ("Caramel").
    ''' </summary>
    Private Function VerificarConfiguracion(ByVal nombreUsuario As String) As ValidacionPerfil
        Using parametrosGuardados As New DataTable
            Dim devolver As New ValidacionPerfil
            parametrosGuardados.TableName = "LocalSettingsMachine"
            parametrosGuardados.Columns.Add("Tema", GetType(String))
            parametrosGuardados.Columns.Add("VistaSignos", GetType(String))



            'pregunto si existe la carpeta
            'If Directory.Exists(String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "\", "Indigo Technologies", "\", "Indigo Crystal", "\TemporalesSkin")) = False Then
            If Directory.Exists(String.Concat(Window.Utils.LocalFolder(), "\", "Vie HealtTech", "\", "Indigo Vie EHR", "\TemporalesSkin")) = False Then
                'creo la carpeta
                'Directory.CreateDirectory(String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "\", "Indigo Technologies", "\", "Indigo Crystal", "\TemporalesSkin"))
                Directory.CreateDirectory(String.Concat(Window.Utils.LocalFolder(), "\", "Vie HealtTech", "\", "Indigo Vie EHR", "\TemporalesSkin"))
            End If
            'If File.Exists(String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "\", "Indigo Technologies", "\", "Indigo Crystal", "\TemporalesSkin\LocalSettingsMachine_", Environment.UserName, Indigo.UserIndigo, ".xlm")) Then
            If File.Exists(String.Concat(Window.Utils.LocalFolder(), "\", "Vie HealtTech", "\", "Indigo Vie EHR", "\TemporalesSkin\LocalSettingsMachine_", Environment.UserName, Indigo.UserIndigo, ".xlm")) Then
                'parametrosGuardados.ReadXml(String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "\", "Indigo Technologies", "\", "Indigo Crystal", "\TemporalesSkin\LocalSettingsMachine_", Environment.UserName, Indigo.UserIndigo, ".xlm"))
                parametrosGuardados.ReadXml(String.Concat(Window.Utils.LocalFolder(), "\", "Vie HealtTech", "\", "Indigo Vie EHR", "\TemporalesSkin\LocalSettingsMachine_", Environment.UserName, Indigo.UserIndigo, ".xlm"))
                devolver.TemaDevexpress = parametrosGuardados.Rows(0).Item(0).ToString
                Iview.TemaDevexpressGuardadoConfiguracion = devolver.TemaDevexpress
                Return devolver
            Else
                Dim fila As DataRow = parametrosGuardados.NewRow
                fila.Item(0) = "Caramel"
                fila.Item(1) = "Grafico"
                parametrosGuardados.Rows.Add(fila)
                'parametrosGuardados.WriteXml(String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "\", "Indigo Technologies", "\", "Indigo Crystal", "\TemporalesSkin\LocalSettingsMachine_", Environment.UserName, Indigo.UserIndigo, ".xlm"))
                parametrosGuardados.WriteXml(String.Concat(Window.Utils.LocalFolder(), "\", "Vie HealtTech", "\", "Indigo Vie EHR", "\TemporalesSkin\LocalSettingsMachine_", Environment.UserName, Indigo.UserIndigo, ".xlm"))
                devolver.TemaDevexpress = "Caramel"
                Return devolver
            End If
        End Using
    End Function

    ''' <summary>
    ''' Este metodo establece los valores de la sesion en la barra de estado del MDI
    ''' </summary>
    Public Sub EstablecerValoresSesionBarraEstado()
        modelo = New MmdiPrincipal
        Iview.Usuario = Indigo.UserIndigoName
        Iview.Empresa = String.Concat(Indigo.IndigoCompany, "-", Indigo.IndigoCompanyName)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Public Sub LoadGeneralConfiguration()
        Using modelo As New MLogin()
            Dim gc = modelo.GetGeneralConfiguration()
            ApplicationSetting.Instance.SetGeneralConfiguration(gc)
        End Using
    End Sub

    ''' <summary>
    ''' Este metodo consulta el listado de formularios con acciones.
    ''' </summary>
    ''' <param name="codigo">The codigo.</param> 
    Public Async Function ConsultarTodosForms() As Threading.Tasks.Task
        Dim modelo As New MmdiPrincipal
        ListForm = Await modelo.ConsultarTodosForms()
    End Function

#End Region

#Region "Estructura"
    Private Structure ValidacionPerfil
        ''' <summary>
        ''' Tema de devexpress que se esta manipulando
        ''' </summary>
        Dim TemaDevexpress As String

        Public Overloads Overrides Function GetHashCode() As Integer

        End Function

        Public Overloads Overrides Function Equals(ByVal obj As [Object]) As Boolean

        End Function
    End Structure
#End Region

#Region "Constructor del presentador"
    ''' <summary>
    ''' Inicializa una nueva instancia del presentador
    ''' </summary>
    ''' <param name="iview">La Vista.</param>
    Public Sub New(ByRef iview As IMdiPrincipal)
        If iview Is Nothing Then
            Throw New ArgumentException("Iview no puede ser nulo")
        Else
            Me.Iview = iview
        End If
    End Sub

    Public Sub New()

    End Sub

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
