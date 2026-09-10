'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 09-07-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources

#End Region
''' <summary>
''' Esta presentador captura toda la logica aplicada en el frontal de Tipo de vinculación
''' </summary>
Public Class PTimeUnit

#Region "Fields"

    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz IJobBondingType
    ''' </summary>
    Private _view As ITimeUnit
    ''' <summary>
    ''' Variable que se utilizapa para tratar la unidad de tiempo como un Objeto
    ''' </summary>
    Private _TimeUnit As Object
    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singlenton
    ''' </summary>
    Private _indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Builders"

    ''' <summary>
    ''' Constructor de la clase presentador en el frontal de unidades de tiempo
    ''' </summary>
    ''' <param name="view">Vista de Unidades de tiempo</param>
    Public Sub New(ByRef view As ITimeUnit)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._view = view
        End If
    End Sub

#End Region
End Class
