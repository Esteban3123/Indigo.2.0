'***********************************************************************
' Assembly         : Presentation.Payroll.MVP
' Author           : Kevin Garay
' Created          : 28-06-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports DevExpress.Xpo

#End Region

Public Interface IThirdParty
    Inherits ICrudBase

    ''' <summary>
    ''' Propiedad que contiene el nit del tercero
    ''' </summary>
    Property ThirdPartyNit As String

    ''' <summary>
    ''' Propiedad que establece el dígito de verificación
    ''' </summary>
    Property VerificationCode As String

    ''' <summary>
    ''' Propiedad que contiene el nombre o razon social de tercero
    ''' </summary>
    Property ThridPartyName As String

    ''' <summary>
    ''' Propiedad que contiene el tipo de identificacion de persona
    ''' </summary>
    <Obsolete>
    Property IdentificationType As Integer

    ''' <summary>
    ''' Id del tipo de identificacion EHR
    ''' </summary>
    ''' <returns></returns>
    Property IdentificationTypeId As Integer?

    ''' <summary>
    ''' Propiedad que contiene el primer nombre de persona
    ''' </summary>
    Property FirstName As String

    ''' <summary>
    ''' Propiedad que contiene el segundo nombre de persona
    ''' </summary>
    Property SecondName As String

    ''' <summary>
    ''' Propiedad que contiene el primer apellido de persona
    ''' </summary>
    Property FirstLastName As String

    ''' <summary>
    ''' Propiedad que contiene el segundo apellido de persona
    ''' </summary>
    Property SecondLastName As String

    ''' <summary>
    ''' Propiedad que contiene el tipo de retencion
    ''' </summary>
    Property RetentionType As Integer

    ''' <summary>
    ''' Propiedad que contiene el tipo de contribuyente
    ''' </summary>
    Property ContributionType As Integer

    ''' <summary>
    ''' Propiedad que contiene si maneja ICA
    ''' </summary>
    Property Ica As Boolean

    ''' <summary>
    ''' Propiedad que contiene el porcentaje ICA
    ''' </summary>
    Property IcaPercentage As Decimal

    ''' <summary>
    ''' Propiedad que contiene si maneja tope de ICA
    ''' </summary>
    Property IcaTop As Boolean

    ''' <summary>
    ''' Propiedad que contiene el valor del tope ICA
    ''' </summary>
    Property IcaTopValue As Double

    ''' <summary>
    ''' Establece/Obtiene el estado de la persona
    ''' </summary>
    Property StatusPerson As Boolean

    ''' <summary>
    ''' Define el estado de activacion de los controles de terceros
    ''' </summary>
    WriteOnly Property ThirdParty_ActionsOnContros As Boolean

    ''' <summary>
    ''' Define el estado de activacion de los controles de persona
    ''' </summary>
    WriteOnly Property Person_ActionsOnContros As Boolean

    ''' <summary>
    ''' Obtiene o establece el tipo de persona
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PersonType As Integer

    ''' <summary>
    ''' Establece el datasource de ciudades
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CitiesXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Establece el id de ciudades
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CityId As Integer?

    ''' <summary>
    ''' Establece el datasource de actividad economica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EconomicActivityXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id de la actividad economica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EconomicActivityId As Integer?

    ''' <summary>
    ''' Clase del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ClassThirdParty As Integer?

    ''' <summary>
    ''' Esta propiedad que contiene el estado del registro
    ''' </summary>
    Property Status As Integer

    ''' <summary>
    ''' obtiene o establece el id del concepto de cuenta por pagar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IVARetentionAccountPayableConceptId As Integer?

    ''' <summary>
    ''' Obtiene o establece el listado de conceptos de cuentas por pagar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IVARetentionAccountPayableConceptXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Permite saber si maneja sucursales
    ''' </summary>
    ''' <returns></returns>
    Property HandlesBranchOffice As Boolean

    ''' <summary>
    ''' Id de la sucursal
    ''' </summary>
    ''' <returns></returns>
    Property BranchOfficeId As Integer

    ''' <summary>
    ''' Datasource de la sucursal
    ''' </summary>
    ''' <returns></returns>
    Property BranchOfficeXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id de la responsabilidad fiscal
    ''' </summary>
    ''' <returns></returns>
    Property FiscalResponsabilityId As Integer

    ''' <summary>
    ''' Datasource de la responsabilidad fiscal
    ''' </summary>
    ''' <returns></returns>
    Property FiscalResponsabilityXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id del concepto de retención
    ''' </summary>
    ''' <returns></returns>
    Property IVARetentionConceptId As Integer?

    ''' <summary>
    ''' Datasource del concepto de retencion
    ''' </summary>
    ''' <returns></returns>
    Property IVARetentionConceptXpo As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' facturador electronico
    ''' </summary>
    ''' <returns></returns>
    Property ElectronicBiller As Boolean

    ''' <summary>
    ''' datasource de los tipo de identificacion
    ''' </summary>
    ''' <returns></returns>
    Property IdentificationTypeDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' datasource de los tipo de identificacion Juridicos
    ''' </summary>
    ''' <returns></returns>
    Property IdentificationTypeJuridicDatasource As XPInstantFeedbackSource

End Interface
