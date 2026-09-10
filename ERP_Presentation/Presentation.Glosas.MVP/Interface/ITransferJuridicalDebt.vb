'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Juan Diego Diaz
' Created          : 15-10-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Base
Imports  Domain.Entities
Imports DevExpress.Xpo
Imports Presentation.Controls

#End Region

''' <summary>
''' Interface que declara las propiedades y metodos que debe implementar el frontal de traslado cobro jurídico
''' </summary>
Public Interface ITransferJuridicalDebt
    Inherits IcrudBase

    ''' <summary>
    ''' Asigna el valor de activo o inactivo a los controles
    ''' </summary>
    WriteOnly Property ActionsOnControls As ActionOnControlsTypeTransferJuridical
    ''' <summary>
    ''' Propiedad que contine los datos de la rejilla traslado cobro jurídico detalles
    ''' </summary>
    Property SelectedInvoices As Object
    ''' <summary>
    ''' retorna o asigna el datasource de factura aptas para traslado a cobro jurídico
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property datasourceXPOInvoice As XPInstantFeedbackSource
    ''' <summary>
    ''' Obtiene o establece el id de la unidad de radicacion inicial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FilingUnitSourceId As Integer?

    ''' <summary>
    ''' Obtiene o establece el datasource de la unidad de radicacion inicial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FilingUnitSourceXpo As List(Of Domain.Entities.FilingUnit)
    ''' <summary>
    ''' Obtiene o establece el id de la unidad de radicacion inicial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FilingUnitTargetId As Integer?

    ''' <summary>
    ''' Obtiene o establece el datasource de la unidad de radicacion inicial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FilingUnitTargetXpo As XPCollection

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property LawyerId As Integer?

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property Lawyers As XPInstantFeedbackSource

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property DemandStatusId As Integer?

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property DemandStatusDataSource As XPInstantFeedbackSource

End Interface

''' <summary>
''' Tipos de accion a ejecutar sobre los controles
''' </summary>
Public Enum ActionOnControlsTypeTransferJuridical

    ''' <summary>
    ''' Listo para consultar
    ''' </summary>
    WaitingQuery
    ''' <summary>
    ''' Nuevo traslado cobro jurídico
    ''' </summary>
    NewJuridical
    ''' <summary>
    ''' Traslado Cobro Jurídico sin confirmar
    ''' </summary>
    UnconfirmedJuridical
    ''' <summary>
    ''' Traslado Cobro Jurídico confirmado
    ''' </summary>
    ConfirmedJuridical
    ''' <summary>
    ''' Traslado Cobro Jurídico anulado
    ''' </summary>
    InvalidateJuridical

End Enum