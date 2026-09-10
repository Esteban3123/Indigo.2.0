Imports Infrastructure.CrossCutting.Base
'Imports LaunchDarkly.Client
'Imports LaunchDarkly.Client.User
'Imports LaunchDarkly.Client.UserExtensions

Public Class FeatureFlagSession

    Public Const LOAD_FOLIO_APIREST = "liquidacion-api-rest"

    Private Shared _instance As FeatureFlagSession
    'Private client As LdClient
    Private Shared _productionEnvirontment As Boolean

    Private Sub New(productionEnvirontment As Boolean)
        'If productionEnvirontment Then
        '    Me.client = New LdClient("sdk-4e2a3391-02db-49ee-9b0a-6ef973024140")
        'Else
        '    Me.client = New LdClient("sdk-bc64f0d2-6c20-4f75-85eb-5edc7d6c0d22")
        'End If
        _productionEnvirontment = productionEnvirontment
    End Sub

    Public Shared ReadOnly Property Instance As FeatureFlagSession
        Get
            If _instance Is Nothing Then
                _instance = New FeatureFlagSession(SessionValues.Instance.ProductionCompany)
            ElseIf SessionValues.Instance.ProductionCompany <> _productionEnvirontment Then
                _instance = New FeatureFlagSession(SessionValues.Instance.ProductionCompany)
            End If

            Return _instance
        End Get
    End Property

    'Private Function GetUser() As User
    '    Dim session = SessionValues.Instance

    '    Return User.WithKey(session.UserIndigo) _
    '        .AndEmail(session.UserEmail).AndName(session.UserIndigoName) _
    '        .AndCustomAttribute("CompanyType", session.IndigoCompanyType) _
    '        .AndCustomAttribute("VieVersion", My.Application.Info.Version.ToString()).AndCustomAttribute("IndigoContainer", session.TransactionalContainer) _
    '        .AndCustomAttribute("ArchitectureType", session.ArchitectureType)
    'End Function

    Public Function GetBoolVariation(key As String, defValue As Boolean) As Boolean
        'Dim user = GetUser()

        'Return client.BoolVariation(key, user, defValue)
        Return defValue
    End Function

End Class
