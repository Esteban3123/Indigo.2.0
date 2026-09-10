'***********************************************************************
' Assembly         : Presentation.Controls.MVP.PBusquedas
' Author           : WalterSierra
' Created          : 27-03-2011
'
' Last Modified By : AndresBonilla
' Last Modified On : 18-04-2011
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Presentation.Base

''' <summary>
''' Clase Presentador del funcional de Busquedas
''' </summary>
Public Class PBusquedas

#Region "variables globales"
    ''' <summary>
    ''' instanciamos la interfaz IBusquedas
    ''' </summary>
    Dim Iview As IBusqueda

#End Region

#Region "metodos y constructor"

    ''' <summary>
    ''' Constructor que permite la comunicacion con la interfaz IBusqueda
    ''' </summary>
    Public Sub New(ByRef iview As IBusqueda)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.Iview = iview
        End If
    End Sub

    ''' <summary>
    ''' metodo que resuelve las entidades y consume el servicio del modelo
    ''' </summary>
    Public Overloads Sub ConsultarEntidades()
        Dim modelo As New MBusqueda
        Iview.OrigendeDatos = modelo.ConsultarEntidades(CType(Iview.ListadoOrigenDatos, Infrastructure.CrossCutting.Base.eDataSource))
    End Sub

    Public Overloads Sub ConsultarEntidades(ByVal Filtro As String)
        Dim modelo As New MBusqueda
        Iview.OrigendeDatos = modelo.ConsultarEntidades(CType(Iview.ListadoOrigenDatos, Infrastructure.CrossCutting.Base.eDataSource), Filtro)
    End Sub

    Public Overloads Sub ConsultarEntidades(ByVal Filtro As Object())
        Using modelo As New MBusqueda
            Iview.OrigendeDatos = modelo.ConsultarEntidades(CType(Iview.ListadoOrigenDatos, Infrastructure.CrossCutting.Base.eDataSource), Filtro)
        End Using
    End Sub

#End Region

End Class
