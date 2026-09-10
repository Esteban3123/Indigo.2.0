'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 07-07-2013
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
''' Interfaz que maneja el frontal
''' </summary>
Public Interface IPosition
    Inherits IcrudBase

    ''' <summary>
    ''' Propiedad que contiene el codigo del tipo de pensionado
    ''' </summary>
    Property PositionCode As String

    ''' <summary>
    ''' Propiedad que contiene el nombre del tipo de pensionado
    ''' </summary>
    Property PositionName As String

    ''' <summary>
    ''' Propiedad que contiene el nombre del tipo de pensionado
    ''' </summary>
    Property PositionLevelId As String

    ''' <summary>
    ''' Propiedad que contiene el nombre del tipo de pensionado
    ''' </summary>
    Property PositionLevels As List(Of PositionLevel)

    ''' <summary>
    ''' Propiedad que contiene el nombre del tipo de pensionado
    ''' </summary>
    Property ProfessionalRiskLevelId As String

    ''' <summary>
    ''' Propiedad que contiene el nombre del tipo de pensionado
    ''' </summary>
    Property ProfessionalRiskLevels As List(Of ProfessionalRisk)

    ''' <summary>
    ''' Propiedad que contiene el nombre del tipo de pensionado
    ''' </summary>
    Property MinBasicSalary As String

    ''' <summary>
    ''' Propiedad que contiene el nombre del tipo de pensionado
    ''' </summary>
    Property MaxBasicSalary As String

    ''' <summary>
    ''' Propiedad que contiene el nombre del tipo de pensionado
    ''' </summary>
    Property MinHourAmount As String

    ''' <summary>
    ''' Propiedad que contiene el nombre del tipo de pensionado
    ''' </summary>
    Property MaxHourAmount As String

    ''' <summary>
    ''' Propiedad que contiene el nombre del tipo de pensionado
    ''' </summary>
    Property Simulation As Boolean

    ''' <summary>
    ''' Propiedad que contiene el nombre del tipo de pensionado
    ''' </summary>
    Property HandlesTurnsChart As Boolean

    ''' <summary>
    ''' Propiedad que contiene el nombre del tipo de pensionado
    ''' </summary>
    Property NightlyChargeAuthorization As Boolean

    ''' <summary>
    ''' Propiedad que contiene los Gastos de Representación
    ''' </summary>
    Property RepresentationCost As Boolean

    ''' <summary>
    ''' Propiedad que contiene el estado del tipo de pensionado
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' Propiedad que contiene el Código INS
    ''' </summary>
    Property INSCode As String

    ''' <summary>
    ''' Propiedad que contiene el Código CCSS
    ''' </summary>
    Property CCSSCode As String

    ''' <summary>
    ''' Propiedad que contiene el comportamiento de los controles
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

End Interface
