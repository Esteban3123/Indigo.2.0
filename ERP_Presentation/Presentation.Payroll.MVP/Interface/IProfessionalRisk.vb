'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 03-07-2013
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
''' Interfaz que maneja el frontal de riesgos profesionales
''' </summary>
Public Interface IProfessionalRisk
    Inherits IcrudBase

    ''' <summary>
    ''' Propiedad que contiene el codigo del riesgo profesional
    ''' </summary>
    Property ProfessionalRiskCode As String

    ''' <summary>
    ''' Propiedad que contiene el Nombre del riesgo profesional
    ''' </summary>
    Property ProfessionalRiskName As String

    ''' <summary>
    ''' Propiedad que contiene el Porcentaje del riesgo profesional
    ''' </summary>
    Property ProfessionalRiskPercentage As String

    ''' <summary>
    ''' Propiedad que contiene el codigo del riesgo profesional
    ''' </summary>
    Property ProfessionalRiskArlCode As String

    ''' <summary>
    ''' Propiedad que contiene el estado del riesgo profesional
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' Propiedad que contiene el comportamiento de los controles
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean


End Interface
