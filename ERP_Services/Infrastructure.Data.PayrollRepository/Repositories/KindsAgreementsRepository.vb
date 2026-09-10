'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Rafael Eduardo Patiño
' Created          : 03-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.Data.Base

Public Class KindsAgreementsRepository
    Inherits GenericRepository(Of KindsAgreements)
    Implements IKindsAgreementsRepository


    ' Contexto de payroll
    Private _contex As IPayrollUnitOfWork

    Public Sub New(ByVal contexto As IPayrollUnitOfWork)
        MyBase.New(contexto)
        _contex = contexto
    End Sub

    ''' <summary>
    ''' Lista las clases de convenios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListKindsAgreements() As List(Of KindsAgreements) Implements IKindsAgreementsRepository.ListKindsAgreements
        Dim Kinds = From e In _contex.KindsAgreements
                    Select e
        Kinds.ToList().ForEach(Sub(x)
                                   x.CodeNameConcatenated = x.Code & " - " & x.Description
                               End Sub)
        Return Kinds.ToList()
    End Function

    ''' <summary>
    ''' Funcion para cargar una clase de convenios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetKindsAgreementes(ByVal code As String, Optional tracking As Boolean = True) As KindsAgreements Implements IKindsAgreementsRepository.GetKindsAgreements
        Dim Kinds = From e In _contex.KindsAgreements
                    Where e.Code = code
                    Select e
        Dim ObjKind = Kinds.SingleOrDefault
        Dim ObjPortfolioNoteConceptParameter = (From d As PayrollSettings In Me._contex.PayrollSettings.AsNoTracking() Select d).FirstOrDefault
        If Kinds.Count > 0 Then
            If ObjPortfolioNoteConceptParameter IsNot Nothing Then
                If ObjPortfolioNoteConceptParameter.PortfolioNoteConceptId IsNot Nothing Then
                    ObjKind.PortfolioNoteConceptParameter = True

                Else
                    ObjKind.PortfolioNoteConceptParameter = False
                End If
            End If
            If ObjKind.AccountId IsNot Nothing Then
                Dim objMainAccount = (From d In _contex.MainAccounts.AsNoTracking Where d.Id = ObjKind.AccountId).FirstOrDefault()
                ObjKind.AccountNumber = If(objMainAccount Is Nothing, Nothing, objMainAccount.Number)
                ObjKind.AccountName = If(objMainAccount Is Nothing, Nothing, objMainAccount.Name)
            End If

            If tracking = False Then
                ObjKind = (From e In _contex.KindsAgreements.AsNoTracking
                           Where e.Code = code
                           Select e).SingleOrDefault
            Else
                ObjKind = Kinds.SingleOrDefault
                ObjKind.KindsAgreementsAux = ObjKind
            End If
            Return ObjKind
        Else
            Return New KindsAgreements
        End If

    End Function
End Class
