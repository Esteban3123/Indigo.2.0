Imports DevExpress.Data.PLinq
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Presentation.Base

''' <summary>
''' 
''' </summary>
''' <seealso cref="Presentation.Base.IcrudBase" />
Public Interface IRecognition
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
    ''' Gets or sets the recognition datasource.
    ''' </summary>
    ''' <value>
    ''' The recognition datasource.
    ''' </value>
    Property RecognitionDatasource As PLinqServerModeSource

    ''' <summary>
    ''' Gets or sets the date initialize.
    ''' </summary>
    ''' <value>
    ''' The date initialize.
    ''' </value>
    Property DateInit As DateTime
    '''' <summary>
    '''' Gets or sets the date end.
    '''' </summary>
    '''' <value>
    '''' The date end.
    '''' </value>
    'Property DateEnd As DateTime


End Interface
