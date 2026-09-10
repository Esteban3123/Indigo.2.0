'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 05-07-2013
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
''' Interfaz que maneja el frontal de tipo de estudio
''' </summary>
Public Interface IStudyType
    Inherits IcrudBase

    ''' <summary>
    ''' Propiedad que contiene el codigo del tipo de estudio
    ''' </summary>
    Property StudyTypeCode As String

    ''' <summary>
    ''' Propiedad que contiene el nombre del tipo de estudio
    ''' </summary>
    Property StudyTypeName As String

    ''' <summary>
    ''' Propiedad que contiene el listado de jerarquia de los estudios
    ''' </summary>
    Property StudyLevel As List(Of Tuple(Of Integer, Integer, String))

    ''' <summary>
    ''' Propiedad que contiene la jerarquia del estudio seleccionado
    ''' </summary>
    Property StudyLevelSelected As Integer

    ''' <summary>
    ''' Propiedad que contiene el estado del tipo de estudio
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' Propiedad que contiene el comportamiento de los controles
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

End Interface
