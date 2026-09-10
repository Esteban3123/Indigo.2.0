'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 28-07-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls.MVP

#End Region
Public Class PCostDistribution
#Region "Variables and Constructors"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As ICostDistribution

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As ICostDistribution)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de los controles del formulario
    ''' </summary>
    Public Sub Initializes()
        Using modelBusqueda As New MBusqueda
            Me.View.Datasource_Groups = modelBusqueda.ConsultarEntidades(eDataSource.GroupsPayroll)
        End Using

    End Sub

#End Region
End Class
