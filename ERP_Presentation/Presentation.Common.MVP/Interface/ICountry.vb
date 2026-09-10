'***********************************************************************
' Assembly         : Presentacion.Common.MVP
' Author           : Jose Luis Rojas
' Created          : 07-04-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries imported"
Imports Domain.Entities
Imports Presentation.Base
Imports Presentation.Controls
#End Region

''' <summary>
''' Esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presenter
''' </summary>
Public Interface ICountry
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Propiedad que contiene el codigo del pais
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Propiedad que contiene el nombre del pais
    ''' </summary>
    Property CountryName As String

    ''' <summary>
    ''' Propiedad que contiene el gentilicio del pais
    ''' </summary>
    Property Nationality As String

    ''' <summary>
    ''' Propiedad que contiene el estado del pais
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean
    Property Sequence As GeneralLedgerSequence

    ReadOnly Property MyTag As String

    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Código estandard
    ''' </summary>
    ''' <returns></returns>
    Property StandardCodeNumeric As String

#End Region


End Interface
