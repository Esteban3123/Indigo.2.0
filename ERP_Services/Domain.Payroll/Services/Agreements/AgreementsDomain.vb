'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 07-02-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Common.Entities
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports Infrastructure.CrossCutting.Base

Public Class AgreementsDomain
    Implements IAgreementsDomain

    ''' <summary>
    ''' Calcula las Cuotas de los Convenios
    ''' </summary>
    ''' <param name="AgreementsEmployee">Objeto Conciliaciones Empleado</param>
    ''' <param name="payrollDateLiquidated">Fecha Nómina</param>
    ''' <param name="conceptValue">Valor del Concepto</param>
    ''' <returns>Lista de Convenios</returns>
    ''' <remarks></remarks>
    Public Function AgreementsCalculate(AgreementsEmployee As List(Of AgreementsC), payrollDateLiquidated As Date, conceptTotalValue As Double) As List(Of AgreementsC) Implements IAgreementsDomain.AgreementsCalculate

        Dim ConceptValue As Double = 0
        Dim AgreementsList As New List(Of AgreementsC)
        Dim AgreementsDetailList As New List(Of AgreementsD)
        Dim AgreementsDetail As New AgreementsD

        For i As Integer = 0 To AgreementsEmployee.Count() - 1
            Dim NumAgreement = AgreementsEmployee.Item(i).Consecutive

            If AgreementsEmployee.Item(i).LiquidationType = 1 And AgreementsEmployee.Item(i).TermType = 1 Then ' Fijo - Fijo
                ConceptValue = 0
                If AgreementsEmployee.Item(i).CurrentBalance > 0 Then

                    Dim NumberShares As Integer = 0
                    If AgreementsEmployee.Item(i).NumberShares = 0 Then
                        NumberShares = 1
                    Else
                        NumberShares = AgreementsEmployee.Item(i).NumberShares
                    End If

                    ConceptValue = AgreementsEmployee.Item(i).AgreementValue / NumberShares
                    If AgreementsEmployee.Item(i).CurrentBalance < ConceptValue Then
                        ConceptValue = AgreementsEmployee.Item(i).CurrentBalance
                    End If
                End If
            Else
                ConceptValue = 0
                If AgreementsEmployee.Item(i).LiquidationType = 1 And AgreementsEmployee.Item(i).TermType = 2 Then ' Fijo - Variable
                    ConceptValue = AgreementsEmployee.Item(i).AgreementValue
                End If
            End If

            If conceptTotalValue > ConceptValue Then
                ConceptValue = conceptTotalValue
            End If


            If AgreementsEmployee.Item(i).AgreementsD.Any(Function(x) x.DatePayment = payrollDateLiquidated And x.TypePayment = 3) = False Then

                AgreementsDetail.ShareValuePaid = ConceptValue
                AgreementsDetail.DatePayment = payrollDateLiquidated
                AgreementsDetail.TypePayment = 1 'Pago por Nómina
                AgreementsDetail.StateShare = "Pago registrado por Nómina en la Fecha " & payrollDateLiquidated.ToString()
                AgreementsDetail.MarkAsAdded()

                AgreementsEmployee.Item(i).AgreementsD.Add(AgreementsDetail)


                If AgreementsEmployee.Item(i).LiquidationType <> 2 AndAlso AgreementsEmployee.Item(i).TermType <> 2 Then
                    AgreementsEmployee.Item(i).CurrentBalance = AgreementsEmployee.Item(i).CurrentBalance - ConceptValue
                End If

                AgreementsEmployee.Item(i).MarkAsModified()

                AgreementsList = AgreementsEmployee

            End If

        Next

        Return AgreementsList

    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
