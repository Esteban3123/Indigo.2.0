Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AccountingRepository

Public Class PAverageStandardCost
    Public Sub New(ByRef Iview As IAverageStandardCost)
        Me.View = Iview
        Indigo = SessionValues.Instance
    End Sub

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IAverageStandardCost

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues

#End Region

#Region "Funciones"
    Public Async Sub getSequence()
        Using modelCost As New MCommonCost(View.tagForm)
            Me.View.costSequence = Await modelCost.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Obtiene la configuracion de la moneda oficial
    ''' </summary>
    ''' <returns></returns>
    Public Function GetOfficialCurrencyFromCompanySettings() As GeneralLedgerCompanySettingsXpo
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.GetXPOObject(Of GeneralLedgerCompanySettingsXpo)(Nothing)
    End Function
#End Region
End Class
