'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/07/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Presentation.Base
Imports Domain.Entities
Imports DevExpress.Xpo

#End Region

Public Interface IPopupDeferredCausation
    Inherits IcrudBase

    ''' <summary>
    ''' Propiedad que contiene el listado de ciudades xpo
    ''' </summary>
    Property CostCenterXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de ciudades xpo
    ''' </summary>
    Property MainAccountXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de ciudades xpo
    ''' </summary>
    Property CostCenterRepositoryXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de ciudades xpo
    ''' </summary>
    Property MainAccountRepositoryXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de terceros xpo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ThirdPartyXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el numero de factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BillNumber As String

    ''' <summary>
    ''' Obtiene o establece el nombre de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property MainAccount As String

    ''' <summary>
    ''' Obtiene o establece el nombre del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CostCenters As String

    ''' <summary>
    ''' Obtiene o establece el valor de la cabecera 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Value As Decimal

    ''' <summary>
    ''' Obtiene o establece el valor del popup
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValuePopup As Decimal

    ''' <summary>
    ''' Obtiene o establece el id del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdThirdParty As Integer

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdMainAccount As Integer

    ''' <summary>
    ''' Obtiene o establece el id del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdCostCenter As Integer

    ''' <summary>
    ''' Obtiene o establece el numero de periodos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PeriodNumbers As Integer

    ''' <summary>
    ''' Obtiene o establece la entidad compleja de cuentas por pagar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property deferredCausation As DeferredCausation

    ''' <summary>
    ''' Obtiene o establece el id del proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdSupplier As Integer

    ''' <summary>
    ''' Contiene la entidad de tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ThirdParty As Domain.Entities.ThirdParty

    ''' <summary>
    ''' Contiene la entidad de tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ThirdPartyCommon As Domain.Entities.ThirdParty

    ''' <summary>
    ''' Obtiene o establece la fecha de la factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DateBill As DateTime

End Interface
