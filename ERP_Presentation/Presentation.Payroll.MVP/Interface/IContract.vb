'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 17-07-2013
'
' Last Modified By : Jose Luis Rojas
' Last Modified On : 24-09-2013
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
''' Interfaz padre de las diferentes interfaces de creacion y modificacion contrato 
''' </summary>
''' <remarks></remarks>
Public Interface IContract

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
    ''' Propiedad que contiene el listado de motivos de modificaciones de contrato(XPO)
    ''' </summary>
    WriteOnly Property ContractModificationReasonsXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de bancos(XPO)
    ''' </summary>
    WriteOnly Property BankDatasourceXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de bancos(XPO)
    ''' </summary>
    WriteOnly Property WorkCentersSourceXPO As XPInstantFeedbackSource

    Property Contingency As Byte?

End Interface
