'***********************************************************************
' Assembly         : Presentacion.Common.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 10-04-2013
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
Imports Domain.Entities

#End Region

''' <summary>
''' Esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presentador
''' </summary>
Public Interface IDepartaments
    Inherits IcrudBase


#Region "Properties"
    ''' <summary>
    ''' Esta propiedad contiene el codigo del Departamento
    ''' </summary>
    Property CodeDepartament As String
    ''' <summary>
    ''' Esta propiedad contiene el nombre del Departamento
    ''' </summary>
    Property NameDepartament As String
    ''' <summary>
    ''' Esta propiedad lista todos los paises
    ''' </summary>
    WriteOnly Property ListAllCountry As List(Of Country)
    ''' <summary>
    ''' Esta Propiedad contiene el codigo del pais
    ''' </summary>
    Property GetIdCountry As Integer
    ''' <summary>
    ''' Esta propiedad contiene el estado del Departamento
    ''' </summary>
    Property StateDepartament As Boolean
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Propiedad que retorna el layout para customizaciones
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl
#End Region
End Interface
