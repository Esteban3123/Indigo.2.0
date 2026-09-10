'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 19/03/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Presentation.Controls.MVP

#End Region

Public Class PConceptsNotes

#Region "Variables and Constructors"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IConceptsNotes

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IConceptsNotes)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequensePayments(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las ciudades
    ''' </summary>
    Public Sub Initialize()
        Using modelAccountsXPO As New MBusqueda
            Dim filter() As Object = {5, True}
            Me.View.AccountsXpo = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub


    ''' <summary>
    ''' Inicializa el datasource de los conceptos de retencion
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeRetentionConcept()
        Using model As New MBusqueda
            Me.View.RetentionConceptXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListXPInstantFeedbackSource(Of AccountingRepository.RetentionConceptXpo)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource  de los conceptos de la retencion 383 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeRetentionConcept383()
        Using model As New MBusqueda
            Me.View.RetentionConcept383Xpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListXPInstantFeedbackSource(Of AccountingRepository.RetentionConceptXpo)($"Status=True And Retention In (2)")
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource  de las cuentas contbale retencion 383 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeRetentionMainAccount383()
        Using model As New MBusqueda
            Me.View.RetentionMainAccount383Xpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListXPInstantFeedbackSource(Of AccountingRepository.PUCServiceXpo)($"RetencionType=1 And FreelancerCategory=True And Status=True And LegalBookId.OfficialBook = 1")
        End Using
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

#End Region

End Class
