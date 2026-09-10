'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Diego Andrés Roldán
' Created          : 26-01-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries imported"
Imports Presentation.Base
Imports Presentation.Controls
Imports DevExpress.Xpo
Imports Domain.Entities
Imports DevExpress.Data.Linq

#End Region

Public Interface IPharmaceuticalDispensing
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Obtiene el layout principal
    ''' </summary>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    Property Sequence As Domain.Entities.InventorySequence

    ''' <summary>
    ''' Obtiene o establece el codigo de la dispensación farmacéutica
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece la unidad operativa de la dispensación farmacéutica
    ''' </summary>
    Property OperationgUnitId As Integer

    ''' <summary>
    ''' Obtiene o establece el número de ingreso al paciente de la dispensación farmacéutica
    ''' </summary>
    Property AdmissionNumber As String

    ''' <summary>
    ''' Fecha del documento
    ''' </summary>
    Property DocumentDate As DateTime?

    ''' <summary>
    ''' Obtiene o establece el estado
    ''' </summary>
    Property Status As String

    ''' <summary>
    ''' Listado del detalle de dispensación farmacéutica
    ''' </summary>
    Property ListPharmaceuticalDispensingDetail As List(Of Domain.Entities.PharmaceuticalDispensingDetail)

    ''' <summary>
    ''' Datasource de Ingreso del Paciente
    ''' </summary>
    Property AdmissionNumberDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Carga los datos en los controles
    ''' </summary>
    Sub LoadControls()

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Sub CleanControls()

    ''' <summary>
    ''' Asigna los valores a los campos de la entidad
    ''' </summary>
    Sub AssigningValues()

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

#End Region

End Interface