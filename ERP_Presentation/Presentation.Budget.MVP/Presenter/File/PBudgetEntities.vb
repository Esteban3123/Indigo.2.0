'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 04-04-2014
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls.MVP

#End Region

''' <summary>
''' Presentador del frontal
''' </summary>
''' <remarks></remarks>
Public Class PBudgetEntities

    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz
    ''' </summary>
    Dim View As IBudgetEntities
    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singlenton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz.
    ''' </summary>
    ''' <param name="iview">The iview.</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IBudgetEntities)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' consulta todos los terceros
    ''' </summary>
    Public Sub InitializeThirdParty()
        Using model As New MBusqueda
            View.ThirdPartyXPO = model.ConsultarEntidades(eDataSource.ThirdParty)
        End Using
    End Sub

    Public Sub InitializeLegalRepresentative()
        Using model As New MBusqueda
            View.LegalRepresentativeXPO = model.ConsultarEntidades(eDataSource.ThirdParty)
        End Using
    End Sub

    Public Sub InitializeFinancialBoss()
        Using model As New MBusqueda
            View.FinancialBossXPO = model.ConsultarEntidades(eDataSource.ThirdParty)
        End Using
    End Sub

    Public Sub InitializeBudgetBoss()
        Using model As New MBusqueda
            View.BudgetBossXPO = model.ConsultarEntidades(eDataSource.ThirdParty)
        End Using
    End Sub

    Public Sub InitializeAllThirdParty()
        InitializeLegalRepresentative()
        InitializeFinancialBoss()
        InitializeBudgetBoss()
    End Sub

End Class
