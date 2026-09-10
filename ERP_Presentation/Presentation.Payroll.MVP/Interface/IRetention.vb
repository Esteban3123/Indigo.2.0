'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 08-07-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Presentation.Controls
Imports Presentation.Base
Imports Domain.Payroll.Entities

#End Region

''' <summary>
''' Esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presentador
''' </summary>
Public Interface IRetention
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Esta propiedad contiene el codigo de la retencion
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Esta propiedad contiene el valor minimo de UVT
    ''' </summary>
    Property UVTMin As Double

    ''' <summary>
    ''' Esta propiedad contiene el valor maximo de UVT
    ''' </summary>
    Property UVTMax As Double

    ''' <summary>
    ''' Esta propiedad contiene la tarifa de retencion
    ''' </summary>
    Property Rate As Decimal

    ''' <summary>
    ''' Esta propiedad contiene la tarifa adicional de retencion
    ''' </summary>
    Property AdditionalRate As Decimal

    ''' <summary>
    ''' Esta propiedad contiene el valor de UVT
    ''' </summary>
    Property UVTValue As Double

    ''' <summary>
    ''' Esta propiedad contiene el año de retencion
    ''' </summary>
    Property Year As Integer

    ''' <summary>
    ''' Propiedad que contiene el id del consecutivo
    ''' </summary>
    Property Consecutive As Integer

    ''' <summary>
    ''' Esta propiedad contiene el estado de la retencion
    ''' </summary>
    Property StatusRetention As Boolean

    ''' <summary>
    ''' controla la accion de los controles del frontal
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

#End Region

End Interface
