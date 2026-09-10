'***********************************************************************
' Assembly         : Presentacion.JustificationControl.MVP
' Author           : Cristian Camilo Bahamón Castaño
' Created          : 01-03-2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************


#Region "Imports"

Imports Presentation.Base
Imports Domain.Entities
Imports DevExpress.Xpo
Imports Presentation.Controls

#End Region


Public Interface ILiquidateData

    Inherits ICrudBase

    ''' <summary>
    ''' Propiedad que retorna el layout para customizaciones
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl
    ''' <summary>
    ''' Esta Propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene o asigna el tag del funcional
    ''' </summary>
    ''' <value>Tag del fucnional</value>
    ''' <returns></returns>
    ReadOnly Property MyTag As String

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.BillingSequence

    ''' <summary>
    ''' Obtiene o asigna el numero de ingreso
    ''' </summary>
    Property AdmissionNumber As String

    ''' <summary>
    ''' Obtiene o asigna 
    ''' </summary>
    Property ApplyDiscount As Double

    ''' <summary>
    ''' Obtiene o asigna 
    ''' </summary>
    Property DiscountValueorPercentage As Double

    ''' <summary>
    ''' Obtiene o asigna 
    ''' </summary>
    Property DeductibleValue As Double

    ''' <summary>
    ''' Obtiene o asigna 
    ''' </summary>
    Property ApplyDeductible As Boolean

    ''' <summary>
    ''' Obtiene o asigna 
    ''' </summary>
    Property CopaymentValue As Double

    ''' <summary>
    ''' Obtiene o asigna 
    ''' </summary>
    Property InsuranceCoinsurance As Double

    ''' <summary>
    ''' Obtiene o asigna 
    ''' </summary>
    Property PatientCoinsurance As Double

    ''' <summary>
    ''' Obtiene o asigna si aplica el limite general
    ''' </summary>
    Property ApplyGeneralLimits As Boolean

    ''' <summary>
    ''' Obtiene o asigna si aplica Iva
    ''' </summary>
    Property TaxInclude As Boolean

    ''' <summary>
    ''' Obtiene o asigna valor limite
    ''' </summary>
    Property LimitValue As Decimal

    ''' <summary>
    ''' valor cubierto aseguradora
    ''' </summary>
    ''' <returns></returns>
    Property InsurerCoveredValue As Decimal

    ''' <summary>
    ''' valor limite coaseguro paciente
    ''' </summary>
    ''' <returns></returns>
    Property PatientCoinsuranceLimitValue As Decimal

    ''' <summary>
    ''' valor cubierto aseguradora - especifico
    ''' </summary>
    ''' <returns></returns>
    Property EspecificInsurerCoveredValue As Decimal

    ''' <summary>
    ''' Obtiene o asigna % del coaseguro de la aseguradora por detalle
    ''' </summary>
    Property InsuranceCoinsuranceDetail As Decimal

    ''' <summary>
    ''' Obtiene o asigna % del coaseguro del paciente por detalle
    ''' </summary>
    Property PatientCoinsuranceDetail As Decimal

End Interface
