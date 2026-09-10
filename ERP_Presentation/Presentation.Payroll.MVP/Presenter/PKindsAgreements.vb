'***********************************************************************
' Assembly         : Presentacion.payrol.MVP
' Author           : Rafael Eduardo patiño 
' Created          : 07-01-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
#End Region

''' <summary>
''' Esta presentador captura toda la logica aplicada en el frontal 
''' </summary>
Public Class PKindsAgreements


#Region "variables y constructor "
    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz
    ''' </summary>
    Dim View As IKindsAgreements
    ''' <summary>
    ''' Variable que se utilizapa para tratar al Responsable como un Objeto
    ''' </summary>
    Dim Responsible As Object
    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singlenton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz.
    ''' </summary>
    ''' <param name="iview">The iview.</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IKindsAgreements)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
        View.StatusKindsAgreements = True
    End Sub

    Public Sub InitializeExpenseConcept()
        Me.View.ExpenseConceptDatasource = XpoServiceEx.Instance(Indigo.TransactionalContainer).TreasuryService.ListExpenseConceptsByBehavior(6)
    End Sub
    Public Sub InitializeAccountsReceivable()
        Me.View.AccountReceivableConceptDatasource = XpoServiceEx.Instance(Indigo.TransactionalContainer).PortfolioService.GetAllAccountReceivableConceptByStatus(True)
    End Sub
#End Region
End Class
