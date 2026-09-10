'***********************************************************************
' Assembly         : Presentacion.InteropCost.MVP
' Author           : Diego Andrés Roldán
' Created          : 17-12-2014
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

#End Region

Public Interface IElectronicSupportDocument
    Inherits IcrudBase

#Region "Properties"
    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Layout principal para customizacion
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Código de la razón
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Proveedor
    ''' </summary>
    Property SupplierThirdPartyId As Integer?

    ''' <summary>
    ''' Fecha del documento
    ''' </summary>
    Property DocumentDate As DateTime

    ''' <summary>
    ''' Fecha de radicacion
    ''' </summary>
    Property RadicationDate As DateTime

    ''' <summary>
    ''' Fecha de expiracion
    ''' </summary>
    Property DueDate As DateTime

    ''' <summary>
    ''' Descripcion del documento soporte
    ''' </summary>
    Property Description As String

    ''' <summary>
    ''' Estado del documento false- registrado, true- confirmado
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' subtotal del documento
    ''' </summary>
    ''' <returns></returns>
    Property SubTotal As Decimal

    ''' <summary>
    ''' iva del documento
    ''' </summary>
    ''' <returns></returns>
    Property TaxValue As Decimal

    ''' <summary>
    ''' suma del subtotal y el iva
    ''' </summary>
    ''' <returns></returns>
    Property TotalValue As Decimal

    ''' <summary>
    ''' Secuencia de facturación
    ''' </summary>
    Property Sequense As BillingSequence

    Property BillingAuthorizationId As Integer?

    '''' <summary>
    '''' Carga los datos en los controles
    '''' </summary>
    'Function LoadControls() As Task

    '''' <summary>
    '''' Limpia los controles
    '''' </summary>
    'Sub CleanControls()

    '''' <summary>
    '''' Asigna los valores a los campos de la entidad
    '''' </summary>
    'Sub AssigningValues()

    '''' <summary>
    '''' Esta propiedad establece el valor ControlAcciones
    '''' </summary>
    'WriteOnly Property ActionsOnControls As Boolean
#End Region

End Interface
