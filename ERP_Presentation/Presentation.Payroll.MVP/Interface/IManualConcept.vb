'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 05-03-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries imported"
Imports Presentation.Base
Imports Domain.Payroll.Entities
#End Region

''' <summary>
''' Esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presenter
''' </summary>
''' <remarks></remarks>
Public Interface IManualConcept
    Inherits IcrudBase

    ''' <summary>
    ''' Establece el datasource de los empleados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property Employee_Datasource As Object

    ''' <summary>
    ''' Establece el datasource de los conceptos
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property Concept_Datasource As Object

    ''' <summary>
    ''' Establece el datasource de los conceptos de retencion
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property RetentionConcept_Datasource As Object

End Interface
