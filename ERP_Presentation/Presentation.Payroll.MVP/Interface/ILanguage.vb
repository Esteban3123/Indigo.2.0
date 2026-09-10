'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 25-04-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Base
Imports Domain.Entities

#End Region
''' <summary>
''' Interfaz que maneja el frontal de idiomas
''' </summary>
''' <remarks></remarks>
Public Interface ILanguage
    Inherits IcrudBase

    ''' <summary>
    ''' Propiedad del codigo del idioma
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Propiedad del nombre del idioma
    ''' </summary>
    Property LanguageName As String

    ''' <summary>
    ''' Propiedad del estado del idioma
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' Propiedad que contiene el comportamiento de los controles
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean
    ReadOnly Property MyTag As Object
    Property Sequence As PayrollSequence
End Interface
