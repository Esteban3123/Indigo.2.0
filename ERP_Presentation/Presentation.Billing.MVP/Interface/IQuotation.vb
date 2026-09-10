'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Sumit Sarkar
' Created          : 29/04/2019
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Presentation.Base


#End Region

Public Interface IQuotation
    Inherits IcrudBase

    ''' <summary>
    ''' Código del registro
    ''' </summary>
    ''' <returns></returns>
    Property Code As String

    ''' <summary>
    ''' Fecha del documento
    ''' </summary>
    ''' <returns></returns>
    Property DocumentDate As DateTime?

    ''' <summary>
    ''' Tipo de cotización
    ''' </summary>
    ''' <returns></returns>
    Property QuotationType As Integer

    ''' <summary>
    ''' Id del tercero que representa al paciente
    ''' </summary>
    ''' <returns></returns>
    Property PatientThirdPartyId As Integer?

    ''' <summary>
    ''' Datasource del tercero
    ''' </summary>
    ''' <returns></returns>
    Property PatientThirdPartyXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Detalle
    ''' </summary>
    ''' <returns></returns>
    Property Description As String

    ''' <summary>
    ''' Tag del form
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Activa o desactiva los controles
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Secuencia del form
    ''' </summary>
    ''' <returns></returns>
    Property Sequense As BillingSequence

End Interface
