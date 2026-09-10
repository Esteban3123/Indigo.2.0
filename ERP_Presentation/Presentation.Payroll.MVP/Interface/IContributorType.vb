'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 04-07-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Base
Imports Domain.Payroll.Entities
#End Region

''' <summary>
''' Interfaz que maneja el frontal de tipo de contribuyente
''' </summary>

Public Interface IContributorType
    Inherits IcrudBase

    ''' <summary>
    ''' Propiedad que contiene el codigo del tipo de contribuyente
    ''' </summary>
    Property ContributorTypeCode As String

    ''' <summary>
    ''' Propiedad que contiene el nombre del tipo de contribuyente
    ''' </summary>
    Property ContributorTypeName As String

    ''' <summary>
    ''' Propiedad que contiene el estado del tipo de contribuyente
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' Propiedad que contiene el comportamiento de los controles
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

End Interface
