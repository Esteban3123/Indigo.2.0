'***********************************************************************
' Assembly         : Presentacion.Accounting.MVP
' Author           : Diego Andrés Roldán
' Created          : 17-01-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries imported"
Imports DevExpress.Xpo
Imports Presentation.Base
#End Region

''' <summary>
''' Esta interfaz contiene las propiedades y metodos que va implementar nuestra vista y va a controlar nuestro presenter
''' </summary>
Public Interface IRetentionConcept
    Inherits IcrudBase

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.GeneralLedgerSequence

    ''' <summary>
    ''' Obtiene o establece el codigo del concepto
    ''' </summary>
    ''' <value>
    ''' The code concept.
    ''' </value>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece el nombre del concepto
    ''' </summary>
    ''' <value>
    ''' The name concept.
    ''' </value>
    Property NameConcept As String

    ''' <summary>
    ''' Obtiene o establece la retencion
    ''' </summary>
    ''' <value>
    ''' The retention.
    ''' </value>
    Property Retention As Byte

    ''' <summary>
    ''' Obtiene o establece el tipo de cálculo
    ''' </summary>
    ''' <value>
    ''' The retention.
    ''' </value>
    Property CalculationType As Byte

    ''' <summary>
    ''' Obtiene o establece la base minima
    ''' </summary>
    ''' <value>
    ''' The minimum base.
    ''' </value>
    Property MinBase As Decimal

    ''' <summary>
    ''' Obtiene o establece la tasa de retencion
    ''' </summary>
    ''' <value>
    ''' The rate.
    ''' </value>
    Property Rate As Decimal

    ''' <summary>
    ''' Obtiene o asigna el estado del registro en la tabla
    ''' </summary>
    ''' <value>Estado del registro en la tabla</value>
    ''' <returns>El estado del registro en la tabla</returns>
    Property StateConcept As Boolean

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Datasource de las ciudades
    ''' </summary>
    ''' <returns></returns>
    Property CityXpo As XPInstantFeedbackSource

End Interface
