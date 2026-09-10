Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.MedicalFeesRepository
Imports Presentation.Base

''' <summary>
''' Interface para el formulario de Causación de Proveedores de Salud
''' </summary>
''' <seealso cref="Presentation.Base.IcrudBase" />
Public Interface ICausation
    Inherits IcrudBase

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property MyTag As String

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Property IdOperativeUnit As Integer

    ''' <summary>
    ''' Gets or sets the causation datasource.
    ''' </summary>
    ''' <value>
    ''' The causation datasource.
    ''' </value>
    Property CausationDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Gets or sets the date initialize.
    ''' </summary>
    ''' <value>
    ''' The date initialize.
    ''' </value>
    Property DateInit As DateTime

End Interface

