'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/09/2014
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
Imports Presentation.Controls.MVP

#End Region

Public Class PHealthAdministrator

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IHealthAdministrator

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IHealthAdministrator)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Methods"

    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Carga el DataSource para el campo de Tercero
    ''' </summary>
    Public Sub InitializeThirdParty()
        Using msearch As New MBusqueda
            View.ThirdPartyXpo = msearch.ConsultarEntidades(eDataSource.ThirdParty)
        End Using
    End Sub
    ''' <summary>
    ''' Carga el DataSource para el campo de Tipo de Entidad
    ''' </summary>
    Public Sub InitializeEntityType()
        Using msearch As New MBusqueda
            View.EntityTypeDataSource = msearch.ConsultarEntidades(eDataSource.ListCompanyType)
        End Using
    End Sub
    ''' <summary>
    ''' Carga el DataSource para el campo Clasificación Cartera
    ''' </summary>
    Public Sub InitializePortfolioClassification()
        View.PortfolioClassificationXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PortfolioService.ListActivePortfolioDeteriorationClassification()
    End Sub

#End Region

End Class
