'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 10-09-2015
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
Imports Presentation.Controls
Imports DevExpress.Data.Linq
Imports Domain.Entities
#End Region

Public Interface IProductInvoice
    Inherits IcrudBase
    ''' <summary>
    ''' Codigo de la orden de servicio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String
    ''' <summary>
    ''' fecha del documneto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DocumentDate As DateTime
    ''' <summary>
    ''' id del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ThirdPartyId As Integer
    ''' <summary>
    ''' id del almacen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property WareHouseId As Integer
    ''' <summary>
    ''' Id de la condicion de ventas
    ''' </summary>
    Property ConditionSalesId As Integer?
    ''' <summary>
    ''' observaciones
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Observations As String
    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    ReadOnly Property MyTag As Object
    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl
    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean
    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    ''' <value>
    ''' The sequense.
    ''' </value>
    Property Sequence As Domain.Entities.BillingSequence

    ''' <summary>
    ''' Id de la autorizacion de la facturacion
    ''' </summary>
    ''' <returns></returns>
    Property BillingAuthorizationId As Integer

    ''' <summary>
    ''' Id de la unidad Funcional
    ''' </summary>
    ''' <returns></returns>
    Property FunctionalUnitId As Integer

    ''' <summary>
    ''' Id de la sucursal
    ''' </summary>
    ''' <returns></returns>
    Property BranchOfficeId As Integer?

    ''' <summary>
    ''' Datasource de la sucursal
    ''' </summary>
    ''' <returns></returns>
    Property BranchOfficeXpo As LinqInstantFeedbackSource
    Property RetentionConceptBranchTask As Task(Of RetentionConcepts)
    Property RetentionConceptThirdTask As Task(Of RetentionConcepts)

    ''' <summary>
    ''' Propiedad del contrato de centro de atencion externo
    ''' </summary>
    ''' <returns></returns>
    Property ContractExternalClientsId As Integer?
End Interface
