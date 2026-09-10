'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 18/03/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Presentation.Base
#End Region

''' <summary>
''' esta interfaz contiene las propiedades y metodos que va implementar nuestra vista y va a controlar nuestro presenter
''' </summary>
''' <remarks></remarks>
Public Interface IConfirmationNotes
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Esta propiedad contiene el rango de la cuenta por pagar
    ''' </summary>
    Property Rank As Boolean

    ''' <summary>
    ''' Esta propiedad contiene la fecha inicial
    ''' </summary>
    Property DateInitial As DateTime

    ''' <summary>
    ''' Esta propiedad contiene la fecha final
    ''' </summary>
    Property DateFinal As DateTime

    ''' <summary>
    ''' Esta propiedad contiene la cuenta inicial
    ''' </summary>
    Property AccountInitial As String

    ''' <summary>
    ''' Esta propiedad contiene la cuenta final
    ''' </summary>
    Property AccountFinal As String

    ''' <summary>
    ''' Esta propiedad que contiene el estado del registro
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean


#End Region

End Interface
