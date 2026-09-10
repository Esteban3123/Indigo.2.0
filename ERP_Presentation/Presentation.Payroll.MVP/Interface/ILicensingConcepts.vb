'***********************************************************************
' Assembly         : Presentation.Payroll.MVP
' Author           : Andrés Steven Rojas 
' Created          : 27-09-2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Base
Imports Presentation.Controls
Imports Domain.Entities
#End Region
Public Interface ILicensingConcepts
    Inherits ICrudBase

#Region "Properties"

    ''' <summary>
    ''' Nombre del concepto de licencia
    ''' </summary>
    ''' <returns></returns>
    Property Name As String

    ''' <summary>
    ''' Código del concepto de licencia
    ''' </summary>
    ''' <returns></returns>
    Property Code As String

    ''' <summary>
    ''' Clase del Concepto de Licencia
    ''' </summary>
    ''' <returns></returns>
    Property LicensingConceptsClass As String

#End Region
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
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    ''' <value>
    ''' The sequense.
    ''' </value>
    Property Sequence As PayrollSequence
#End Region

End Interface
