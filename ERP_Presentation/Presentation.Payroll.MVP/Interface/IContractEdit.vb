'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 24-09-2013
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
Imports DevExpress.Xpo
Imports CommonEntities = Domain.Entities

#End Region

''' <summary>
''' Interfaz que maneja el control de creacion de contratos
''' </summary>
''' <remarks></remarks>
Public Interface IContractEdit

    ''' <summary>
    ''' Propiedad que contiene el listado de grupos(XPO)
    ''' </summary>
    WriteOnly Property FundDatasourceXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de grupos(XPO)
    ''' </summary>
    WriteOnly Property GroupDatasourceXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de Cargos(XPO)
    ''' </summary>
    WriteOnly Property PositionDatasourceXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de tipos de empleado(XPO)
    ''' </summary>
    WriteOnly Property EmployeeTypeXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de unidades funcionales(XPO)
    ''' </summary>
    WriteOnly Property FunctionalUnitDatasourceXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de tipos de contratos(XPO)
    ''' </summary>
    WriteOnly Property ContractTypeDatasourceXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de tipos de vinculacion(XPO)
    ''' </summary>
    WriteOnly Property JobBondingDatasourceXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de razones de retiro(XPO)
    ''' </summary>
    WriteOnly Property RetirementReasonDatasourceXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de bancos(XPO)
    ''' </summary>
    WriteOnly Property BankDatasourceXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de bancos(XPO)
    ''' </summary>
    WriteOnly Property CostCentersDataSourceXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de bancos(XPO)
    ''' </summary>
    WriteOnly Property WorkCentersSourceXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el comportamiento de los controles
    ''' </summary>
    'WriteOnly Property ActionsOnControls As Boolean

End Interface
