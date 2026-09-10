'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Andres Alarcon
' Created          : 18-05-2023
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo

#End Region

Public Class PServiceFeeDetail

#Region "Variables"

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IServiceFeeDetail

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IServiceFeeDetail)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Lista los conceptos de facturación que son de tipo "Facturación Basica"
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InitializeServices() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.ListBillingConcept()
    End Function

#End Region

End Class