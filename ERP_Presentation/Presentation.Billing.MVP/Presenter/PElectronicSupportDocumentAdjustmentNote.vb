'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 2022-08-09
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
#End Region

Public Class PElectronicSupportDocumentAdjustmentNote
    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IElectronicSupportDocumentAdjustmentNote

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IElectronicSupportDocumentAdjustmentNote)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

#Region "Methods"
    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub
    ''' <summary>
    ''' Obtiene la secuencia
    ''' </summary>
    Public Async Sub GetSequence()
        Using modelCommmonTreasury As New MBlockRecordAndSequense(View.MyTag)
            Me.View.Sequence = Await modelCommmonTreasury.GetSequense()
        End Using
    End Sub

    Public Function ListSuppliersDistributionLines() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.ListSuppliersDistributionLines()
    End Function


    Public Function ListAllBillingAuthorizationByUserCode(UserCode As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.ListAllBillingAuthorizationByUserCode(UserCode)
    End Function
#End Region


End Class
