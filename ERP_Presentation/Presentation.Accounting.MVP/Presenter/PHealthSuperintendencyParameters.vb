#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls.MVP

#End Region

Public Class PHealthSuperParameters

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IHealthSuperParameters

#End Region

#Region "Construct"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IHealthSuperParameters)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Methods"

    Public Async Function GetSequense() As Task
        Using model As New MHealthSuperParameters(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Function

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las cuentas contables
    ''' </summary>
    Public Sub InitializeMainAccount(Optional _ClassAcount As Integer = 0)
        Using modelAccountsXPO As New MBusqueda
            Dim filter() As Object = {_ClassAcount}
            Me.View.MainAccountXpo = modelAccountsXPO.ConsultarEntidades(eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de comprobantes contables
    ''' </summary>
    Public Sub InitializeJournalVoucher(MainAccountId As Integer)
        Using model As New MHealthSuperParameters(Me.View.MyTag)
            Me.View.JournalVoucherXpo = model.JournalVouchersByMainAccount(MainAccountId)
        End Using
    End Sub

    ''' <summary>
    ''' Inicia el datasource de Terceros
    ''' </summary>
    Public Sub InitializeThirdParty()
        Using model As New MHealthSuperParameters(Me.View.MyTag)
            Me.View.ThirdPartyXpo = model.ThirdParty()
        End Using
    End Sub

#End Region

End Class
