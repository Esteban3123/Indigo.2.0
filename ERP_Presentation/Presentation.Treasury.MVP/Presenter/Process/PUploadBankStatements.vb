#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Presentation.Controls.MVP

#End Region

Public Class PUploadBankStatements

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IUploadBankStatements

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Constructors"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IUploadBankStatements)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    ''' <summary>
    ''' Lista todas cuentas bancarias de entidades y lo asigna al datasource de cuentas bancarias
    ''' </summary>
    Public Sub InitializeEntityBankAccount()
        Using Model As New MBusqueda
            Dim filter() As Object = {Indigo.UserIndigo, True}
            Me.View.BankAccountDatasource = Model.ConsultarEntidades(eDataSource.ListEntityBankAccountByUser, filter)
        End Using
    End Sub


#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene la secuencia numérica
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub GetSequense()
        Using ModelCommonTreasury As New MCommonTreasury(Me.View.MyTag)
            Me.View.Sequence = Await ModelCommonTreasury.GetSequense()
        End Using
    End Sub

#End Region

End Class
