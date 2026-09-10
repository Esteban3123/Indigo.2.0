'***********************************************************************
' Assembly         : Presentacion.Seguridad.MVP
' Author           : Andres Bonilla
' Created          : 03-03-2011
'
' Last Modified By : AndresBonilla
' Last Modified On : 18-04-2011
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"
Imports Presentation.CloudAgent.IndigoReference.Security
Imports Presentation.Base.BaseClass
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Domain.Security.Entities
#End Region

''' <summary>
''' Clase Presentador Barra de Botones
''' </summary>
Public Class PBarraBotones

#Region "variables globales"
    ''' <summary>
    ''' instanciamos la interfaz IBarraBotones
    ''' </summary>
    Dim vista As IBarraBotones
    ''' <summary>
    ''' Clase de Variables de Sesion Singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

#End Region

#Region "metodos y constructor"

    ''' <summary>
    ''' Metodo que itera para establecer permisos en la barra de botones
    ''' </summary>
    ''' <param name="codigoFormulario">Recibe el TAG del funcional a verificar permisos</param>
    Public Sub ConsultarPermisos(ByVal codigoFormulario As String)
        If Indigo.UserIndigo IsNot Nothing AndAlso Indigo.UserIndigo <> String.Empty Then
            Using modelo As New MBarraBotones
                '*********Metodo Para Consultar Permisos del formulario***********************************************************
                Dim permisoUsuarioFrm As New List(Of PermissionUserToolbar)
                permisoUsuarioFrm = modelo.ConsultarPermisosUsuario(Indigo.UserIndigo, Indigo.UserRol, codigoFormulario)
                vista.PermissionsForm = (From a In permisoUsuarioFrm
                                         Select Action = a.TagButton, Name = [Enum].GetName(GetType(PermissionsActionsForm), a.TagButton)).ToDictionary(Function(x) x.Action, Function(y) y.Name)
                If permisoUsuarioFrm.Where(Function(e) e.TagButton = 11).Count > 0 Then
                    vista.PermitirCustomizarFuncional = True
                Else
                    vista.PermitirCustomizarFuncional = False
                End If
                For i As Integer = 0 To permisoUsuarioFrm.Count - 1
                    vista.Boton(permisoUsuarioFrm.Item(i).TagButton) = True
                Next
                '******************************************************************************************************************
            End Using
        End If
    End Sub


    ''' <summary>
    ''' Actualiza la ruta de reportes por usuario 
    ''' </summary>
    ''' <param name="IdUser"></param>
    ''' <param name="IdContainer"></param>
    ''' <param name="IdOperatingUnit"></param>
    Public Sub UpdateReportPath(idUser As Integer, idContainer As Integer, idOperatingUnit As Integer)
        If ApplicationSetting.Instance.LoginAzure Then
            Dim cache As ReportCache = ReportCache.GetInstance()
            Dim cacheKey As String = $"{idUser}-{idContainer}-{idOperatingUnit}"
            Dim cachedReportPath As String = cache.GetValue(cacheKey)
            If String.IsNullOrEmpty(cachedReportPath) Then
                Using modelo As New MBarraBotones
                    Dim reportPath = modelo.GetReportPathByUser(idUser, idContainer, idOperatingUnit)
                    cache.SetValue(cacheKey, reportPath)
                    ConfigurationFile.Instance.ReportsPath = reportPath
                End Using
            Else
                ConfigurationFile.Instance.ReportsPath = cachedReportPath
            End If
        End If
    End Sub

    ''' <summary>
    ''' Constructor que permite la comunicacion con la interfaz IBarraBotones
    ''' </summary>
    Public Sub New(ByRef iview As IBarraBotones)
        If iview Is Nothing Then
            Throw New ArgumentException(obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.vista = iview
        End If
    End Sub
#End Region


End Class
