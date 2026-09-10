'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 16-04-2013
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
Public Interface IProfessions
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Propiedad que contiene el codigo de la profesion
    ''' </summary>
    Property CodeProfession As String

    ''' <summary>
    ''' Esta propiedad contiene el estado de la profesion
    ''' </summary>
    Property StateProfession As Boolean

    ''' <summary>
    ''' Propiedad que contiene el nombre de la profesion
    ''' </summary>
    Property NameProfession As String

    ''' <summary>
    ''' Propiedad que contiene el listado de jerarquia de los estudios
    ''' </summary>
    Property StudyLevel As List(Of Tuple(Of Integer, Integer, String))

    ''' <summary>
    ''' Propiedad que contiene la jerarquia del estudio seleccionado
    ''' </summary>
    Property StudyLevelSelected As Integer

    ''' <summary>
    ''' Propiedad que contiene todos los niveles de estudio
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    Property Sequence As Domain.Entities.PayrollSequence

    ReadOnly Property MyLayoutControl As IndigoLayoutControl


    ReadOnly Property MyTag As Object
#End Region

End Interface
