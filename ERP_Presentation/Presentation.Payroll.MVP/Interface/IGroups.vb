'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 19-04-2013
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

#End Region

''' <summary>
''' Interfaz que maneja el frontal de grupos
''' </summary>
''' <remarks></remarks>
Public Interface IGroups
    Inherits IcrudBase

    ''' <summary>
    ''' Propiedad que contiene el codigo del grupo
    ''' </summary>
    Property GroupCode As String

    ''' <summary>
    ''' Propiedad que contiene el nombre del grupo
    ''' </summary>
    Property GroupName As String

    ''' <summary>
    ''' Propiedad que contiene el Id de la empresa
    ''' </summary>
    Property CompanyId As String

    ''' <summary>
    ''' Propiedad que contiene el tipo de liquidacion del grupo
    ''' </summary>
    Property GroupLiquidation As String

    ''' <summary>
    ''' Propiedad que contiene la ultima fecha de liquidacion del grupo
    ''' </summary>
    Property GroupLastLiquidationDate As Object

    ''' <summary>
    ''' Propiedad que contiene la proxima fecha de liquidacion del grupo
    ''' </summary>
    Property GroupNextLiquidationDate As Object

    ''' <summary>
    ''' Propiedad que contiene el parametro de meses de liquidacion del grupo
    ''' </summary>
    Property GroupMonths As String

    ''' <summary>
    ''' Propiedad que contiene el parametro de provisiones de liquidacion del grupo
    ''' </summary>
    Property GroupProvisions As Boolean

    ''' <summary>
    ''' Propiedad que contiene el parametro de clases de contratos
    ''' </summary>
    Property ContractClasses As Byte

    ''' <summary>
    ''' Propiedad que contiene el estado del grupo
    ''' </summary>
    Property GroupStatus As Boolean

    ''' <summary>
    ''' Propiedad que contiene el Id de los tipos de comprobantes contables
    ''' </summary>
    ''' <returns></returns>
    Property RetroactiveReceipt As Byte

    ''' <summary>
    ''' Propiedad que contiene el DataSource con el listado de empresas
    ''' </summary>
    WriteOnly Property CompaniesDataSource As List(Of Company)

    ''' <summary>
    ''' Propiedad que contiene el comportamiento de los controles
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Establece el datasource de los conceptos
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property ConceptsDatasource As List(Of Concept)

    ''' <summary>
    ''' Establece el datasource de los conceptos
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property ConceptsAdjustDatasource As List(Of Concept)

    ''' <summary>
    ''' Propiedad que contiene el listado de empresa de parametros interfaces
    ''' </summary>
    Property DataSourceBranch As List(Of Domain.Entities.GlosasParametersInterface)

    ''' <summary>
    ''' Propiedad que contiene el Día 31 de los Parámetros de Nómina
    ''' </summary>
    Property Day31 As Boolean?

    ''' <summary>
    ''' Gets or sets the document type xpo.
    ''' </summary>
    ''' <value>
    ''' The document type xpo.
    ''' </value>
    Property DocumentTypePayrollVoucherXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Gets or sets the document type xpo.
    ''' </summary>
    ''' <value>
    ''' The document type xpo.
    ''' </value>
    Property DocumentTypeProvisionVoucherXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Gets or sets the document type xpo.
    ''' </summary>
    ''' <value>
    ''' The document type xpo.
    ''' </value>
    Property DocumentTypePrestacionVoucherXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Gets or sets the document type xpo.
    ''' </summary>
    ''' <value>
    ''' The document type xpo.
    ''' </value>
    Property DocumentTypeIncentiveVoucherXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Gets or sets the document type xpo.
    ''' </summary>
    ''' <value>
    ''' The document type xpo.
    ''' </value>
    Property DocumentTypeContractLiquidationVoucherXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Gets or sets the document type xpo.
    ''' </summary>
    ''' <value>
    ''' The document type xpo.
    ''' </value>
    ''' 
    Property DocumentUnemploymentVoucherXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que lista los Tipos de comprobantes contables
    ''' </summary>
    ''' <value>
    ''' The document type xpo.
    ''' </value>
    ''' 
    Property DocumentTypeRetroactiveReceiptXpo As XPInstantFeedbackSource


    ''' <summary>
    ''' Gets or sets the document type xpo.
    ''' </summary>
    ''' <value>
    ''' The document type xpo.
    ''' </value>
    Property ListAccountXpo As XPInstantFeedbackSource

    Property ListAccountContractLiquidationXpo As XPInstantFeedbackSource

    Property ListAccountVacationLiquidationXpo As XPInstantFeedbackSource

    Property MaximunPremiumYear As Integer?

    Property MonthVacationCompensation As Byte?

    Property AdditionalVacationDays As Byte?


End Interface
