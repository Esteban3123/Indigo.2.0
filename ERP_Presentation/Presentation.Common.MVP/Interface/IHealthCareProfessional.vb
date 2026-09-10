'***********************************************************************
' Assembly         : Presentation.Common.MVP
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 11-02-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Presentation.Base
Imports Domain.Payroll.Entities
Imports Presentation.Controls
Imports DevExpress.Xpo
Imports Domain.Crystal.Entities

Public Interface IHealthCareProfessional
    Inherits ICrudBase

    ''' <summary>
    ''' Obtiene o establece el nit
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Nit As String

    ''' <summary>
    ''' Obtiene o establece el primer nombre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FirstName As String

    ''' <summary>
    ''' Obtiene o establece el segundo nombre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SecondName As String

    ''' <summary>
    ''' Obtiene o establece el primer apellido
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FirstLastName As String

    ''' <summary>
    ''' Obtiene o establece el segundo apellido
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SecondLastName As String

    ''' <summary>
    ''' Obtiene o establece la direccion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Address As String

    ''' <summary>
    ''' Obtiene o establece el telefono
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Phone As String

    ''' <summary>
    ''' Obtiene o establece el movil
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Mobile As String

    ''' <summary>
    ''' Obtiene o establece la fecha de ultima liquidación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LastLiquidationDate As DateTime?

    ''' <summary>
    ''' Obtiene o establece el id del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property MedicalFeesContractId As Integer?

    ''' <summary>
    ''' Establece el datasource del contrato profesional de la salud
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property MedicalFeesContractXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id del proveedor linea de distribucion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SupplierDistributionLineId As Integer?

    ''' <summary>
    ''' Establece el datasource del proveedor linea de distribucion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SupplierDistributionLineXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el tipo de vinculacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TypeLinkage As Integer?

    ''' <summary>
    ''' Obtiene o establece si el medico realiza consulta externa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property OutPatient As Boolean

    ''' <summary>
    ''' Obtiene o establece la tarjeta profesional
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ProfessionalCard As String

    ''' <summary>
    ''' Obtiene o establece el tipo de profesion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Profession As Integer?

    ''' <summary>
    ''' Obtiene o establece el perfil de cirugia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SurgeryProfiler As String

    ''' <summary>
    ''' Establece el datasource de la especialidad 1
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SpecialtyXpo1 As XPInstantFeedbackSource

    ''' <summary>
    ''' Establece el datasource de la especialidad 1
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SpecialtyXpo2 As XPInstantFeedbackSource

    ''' <summary>
    ''' Establece el datasource de la especialidad 1
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SpecialtyXpo3 As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id de la especialidad 1
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SpecialtyId1 As String

    ''' <summary>
    ''' Obtiene o establece el id de la especialidad 2
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SpecialtyId2 As String

    ''' <summary>
    ''' Obtiene o establece el id de la especialidad 3
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SpecialtyId3 As String

    ''' <summary>
    ''' Obtiene o establece el codigo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece si el contrato se liquida por defecto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LiquidateDefault As Boolean?

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property ProfessionalEHR As Domain.Crystal.Entities.INPROFSAL

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property ProfessionalERP As Domain.Entities.HealthProfessional

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property HealthProfessionalModel As HealthProfessionalModel

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property IdentificationTypeId As Integer?

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property SpecialtyExCode As String

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property ProfessionalLicenseNumber As String

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property ExternalProfessional As Boolean

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property SpecialtyExtXpo As DevExpress.Xpo.XPInstantFeedbackSource
    Property IdentificationTypeXpo As DevExpress.Xpo.XPInstantFeedbackSource
End Interface
