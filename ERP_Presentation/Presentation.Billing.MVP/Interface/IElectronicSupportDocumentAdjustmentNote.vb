'***********************************************************************
' Assembly         : Presentacion.InteropCost.MVP
' Author           : Diego Andrés Roldán
' Created          : 2022-07-27
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Libraries imported"
Imports Domain.Entities
Imports Presentation.Base
Imports Presentation.Controls

#End Region

Public Interface IElectronicSupportDocumentAdjustmentNote
    Inherits ICrudBase

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
    ''' Documento soporte
    ''' </summary>
    Property ElectronicSupportDocumentId As Integer

    ''' <summary>
    ''' Fecha del documento
    ''' </summary>
    Property DocumentDate As DateTime

    ''' <summary>
    ''' Tipo de nota
    ''' </summary>
    ''' <returns></returns>
    Property NoteType As Byte

    ''' <summary>
    ''' Naturaleza
    ''' </summary>
    ''' <returns></returns>
    Property Nature As Byte

    ''' <summary>
    ''' Descripcion del documento soporte
    ''' </summary>
    Property Description As String

    ''' <summary>
    ''' Estado del documento false- registrado, true- confirmado
    ''' </summary>
    Property Status As String

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
    Property Sequence As BillingSequence

    ''' <summary>
    ''' Carga los datos en los controles
    ''' </summary>
    Function LoadControls() As Task

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Sub CleanControls()

    ''' <summary>
    ''' Asigna los valores a los campos de la entidad
    ''' </summary>
    Sub AssigningValues(Optional withConfirm As Boolean = False)

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean
#End Region

End Interface
