'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 08-05-2013
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
Imports Infrastructure.Data.Xpo


#End Region

''' <summary>
''' Interfaz que maneja el frontal de talento humano
''' </summary>
''' <remarks></remarks>
Public Interface IEmployee
    Inherits IcrudBase

    ''' <summary>
    ''' Propiedad que contiene el listado de ciudades de expedicion de documento (XPO)
    ''' </summary>
    WriteOnly Property ExpeditionCitiesDataSourceXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de ciudades de nacimiento (XPO)
    ''' </summary>
    WriteOnly Property BirthCitiesDataSourceXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de creencias religiosas (XPO)
    ''' </summary>
    WriteOnly Property ReligiousBeliefsDataSourceXPO As XPInstantFeedbackSource

        ''' <summary>
    ''' Propiedad que contiene el listado de creencias religiosas (XPO)
    ''' </summary>
    WriteOnly Property EthnicGroupsDataSourceXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de discapacidades (XPO)
    ''' </summary>
    WriteOnly Property DisabilitiesDataSourceXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de ciudades de estudio (XPO)
    ''' </summary>
    WriteOnly Property StudyCitiesDataSourceXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de parentezcos (XPO)
    ''' </summary>
    WriteOnly Property KinshipsDataSourceXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de idiomas (XPO)
    ''' </summary>
    WriteOnly Property LanguagesDataSourceXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de tipos de pensionado (XPO)
    ''' </summary>
    WriteOnly Property PensionaryTypesDataSourceXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de profesiones (XPO)
    ''' </summary>
    WriteOnly Property ProfessionsDataSourceXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de centros de estudio (XPO)
    ''' </summary>
    WriteOnly Property StudyCentersDataSourceXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de tipos de estudios (XPO)
    ''' </summary>
    WriteOnly Property StudyTypesDataSourceXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de unidades de tiempo(XPO)
    ''' </summary>
    WriteOnly Property UnitsTimeDataSourceXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de paises para obtener la nacionalidad(XPO)
    ''' </summary>
    WriteOnly Property NationalityDataSourceXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de actividades en tiempo libre (XPO)
    ''' </summary>
    WriteOnly Property FreeTimeUseDataSourceXPO As XPInstantFeedbackSource
    ''' <summary>
    ''' Propiedad que contiene el listado de actividades en tiempo libre (XPO)
    ''' </summary>
    WriteOnly Property DiagnosedDiseaseDataSourceXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de sindicatos
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property tradeUnionDatasourceXpo As XPInstantFeedbackSource


    ''' <summary>
    ''' Propiedad que contiene el comportamiento de los controles
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean
    ''' <summary>
    ''' Propiedad que contiene el listado de rentas exentas 
    ''' </summary>
    WriteOnly Property ExemptIncomeXpo As XPInstantFeedbackSource
    ''' <summary>
    ''' Fecha de la renta exenta
    ''' </summary>
    ''' <returns></returns>
    Property DateExemptIcome As DateTime
    ''' <summary>
    ''' Valor de la renta exenta
    ''' </summary>
    ''' <returns></returns>
    Property ExemptIncomeValue As Decimal
    ''comentario de la renta exents
    Property ExemptIncomeComment As String




End Interface
