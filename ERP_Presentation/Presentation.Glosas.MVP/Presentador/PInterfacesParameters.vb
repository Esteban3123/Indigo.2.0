'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Rafal E. Patiño
' Created          : 2013-09-10
'
' Last Modified By : Rafal E. Patiño
' Last Modified On : 2013-09-10
' Description      : Presentador del frontal de parametros de interfaz de glosas
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.CloudAgent.IndigoReference.Glosas
Imports  Domain.Entities

#End Region

''' <summary>
''' Presentador del frontal de parametros interfaces de glosas
''' </summary>
Public Class PInterfacesParameters


#Region "Fields"

    ''' <summary>
    ''' Referencia a la interfaz del frontal Parametros Interfaces
    ''' </summary>
    Private _view As IInterfacesParameters
    ''' <summary>
    ''' Objeto de los parametros
    ''' </summary>
    Private _authorization As Object
    ''' <summary>
    ''' Referencia a los valores de sesion
    ''' </summary>
    Private _indigoSessionValues As SessionValues = SessionValues.Instance

#End Region

#Region "Builders"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="view">Referencia a la vista</param>
    Public Sub New(ByRef view As IInterfacesParameters)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._view = view
        End If
    End Sub

#End Region


End Class
