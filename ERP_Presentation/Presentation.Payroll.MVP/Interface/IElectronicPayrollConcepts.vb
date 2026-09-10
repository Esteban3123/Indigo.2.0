'***********************************************************************
' Assembly         : Presentacion.Payroll.File.MVP
' Author           : Andres Felipe Quintero Garcia
' Created          : 10-02-2025
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports Presentation.Controls
#End Region
Public Interface IElectronicPayrollConcepts
    Inherits ICrudBase
#Region "Fields"
    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    ''' <value>
    ''' The sequense.
    ''' </value>
    Property Sequence As Domain.Entities.PayrollSequence

#End Region
#Region "Properties"
    ''' <summary>
    ''' Contiene el codigo de los Conceptos de nómina electrónica
    ''' </summary>
    Property Code As String
    ''' <summary>
    ''' Contiene el nombre de los Conceptos de nómina electrónica
    ''' </summary>
    Property Name As String
    ''' <summary>
    ''' Contiene el tipo de concepto de los Conceptos de nómina electrónica
    ''' </summary>
    Property ConceptType As Integer
    ''' <summary>
    ''' Contiene el estado de los Conceptos de nómina electrónica
    ''' </summary>
    Property StateElectronicPayrollConcept As Boolean

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean
#End Region
End Interface
