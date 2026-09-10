Imports Presentation.Base.Extension
Public Class CtrDebitCredit

    ''' <summary>
    ''' Delegado para especificar una funcion que me devuelva el valor del debito y credito
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Delegate Function DelegateGetDebitAndCredit() As Tuple(Of Decimal, Decimal)

    Private _functionDebitAndCredit As DelegateGetDebitAndCredit

    ''' <summary>
    ''' Asigna el delegado que se ca ejecutar para obtener el debito y el credito
    ''' </summary>
    ''' <param name="functionDebitAndCredit"></param>
    ''' <remarks></remarks>
    Public Sub SetDebitAndCredit(functionDebitAndCredit As DelegateGetDebitAndCredit)
        _functionDebitAndCredit = functionDebitAndCredit
    End Sub

    Public Sub RefreshDebitCredit()
        If _functionDebitAndCredit IsNot Nothing Then
            Dim tuplaDebitCredit = _functionDebitAndCredit()
            If tuplaDebitCredit.Item1 <> tuplaDebitCredit.Item2 Then
                INDlbBalance.BackColor = Color.Orange
            Else
                INDlbBalance.BackColor = System.Drawing.Color.FromArgb(0, 148, 223)
            End If
            INDlbDebit.Text = tuplaDebitCredit.Item1.MoneyFormat(2)
            INDlbCredit.Text = tuplaDebitCredit.Item2.MoneyFormat(2)
        End If
    End Sub

End Class
