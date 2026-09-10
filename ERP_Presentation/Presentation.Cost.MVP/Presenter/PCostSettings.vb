'***********************************************************************
' Assembly         : Presentacion.InteropCost.MVP
' Author           : Diego Andrés Roldán
' Created          : 30-12-2014
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
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo

#End Region

Public Class PCostSettings

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As ICostSetting

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As ICostSetting)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    Public Sub InitializeAccountPayableConcept()
        Me.View.AccountPayableConceptXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.ListAccountPayableConceptByConceptType(True, 1)
    End Sub
    ''' <summary>
    ''' metodo para trae la informacion de los comprobantes contables
    ''' </summary>
    Public Sub InitializeProvisionJournalVoucherType()
        Me.View.ProvisionJournalVoucherTypeXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListDocumentTypes(True)
    End Sub
    ''' <summary>
    ''' metodo para trae la informacion de los comprobantes contables
    ''' </summary>
    Public Sub InitializeProvisionReversalJournalVoucherType()
        Me.View.ProvisionReversalJournalVoucherTypeXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListDocumentTypes(True)
    End Sub

End Class