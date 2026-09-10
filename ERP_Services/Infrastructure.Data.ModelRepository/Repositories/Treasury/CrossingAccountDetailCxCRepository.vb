'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 29-09-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class CrossingAccountDetailCxCRepository
    Inherits GenericRepository(Of CrossingAccountDetailCxC)
    Implements ICrossingAccountDetailCxCRepository

    'Contexto global
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#Region "Methods"
    ''' <summary>
    ''' Obtiene el detalle del cruce de cuentas de CxC por id
    ''' </summary>
    Public Function GetCrossingAccountDetailCxCById(Id As Integer, Optional tracking As Boolean = False) As CrossingAccountDetailCxC Implements ICrossingAccountDetailCxCRepository.GetCrossingAccountDetailCxCById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query As CrossingAccountDetailCxC = Nothing
        If tracking Then
            query = (From cad As CrossingAccountDetailCxC In _context.CrossingAccountDetailCxC
                     Where cad.Id = Id Select cad).FirstOrDefault()
        Else
            query = (From cad As CrossingAccountDetailCxC In _context.CrossingAccountDetailCxC.AsNoTracking()
                     Where cad.Id = Id Select cad).FirstOrDefault()
        End If
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From cad As CrossingAccountDetailCxC In _context.CrossingAccountDetailCxC.AsNoTracking()
                     Where cad.Id = Id Select cad).FirstOrDefault()
            Return query
        Else
            Return New CrossingAccountDetailCxC()
        End If
    End Function

    ''' <summary>
    ''' Lista los detalles de cruces de cuentas de CxC por el id de cruce de cuenta
    ''' </summary>
    Public Function ListCrossingAccountDetailCxCByCrossingAccountId(crossingAccountId As Integer, Optional tracking As Boolean = False) As List(Of CrossingAccountDetailCxC) Implements ICrossingAccountDetailCxCRepository.ListCrossingAccountDetailCxCByCrossingAccountId
        If crossingAccountId = 0 Then
            Throw New ArgumentNullException("crossingAccountId")
        End If
        Dim query As List(Of CrossingAccountDetailCxC) = Nothing
        If tracking Then
            Dim listCrossingCxC As List(Of CrossingAccountDetailCxC) = (From cad As CrossingAccountDetailCxC In _context.CrossingAccountDetailCxC
                    Where cad.CrossingAccountId = crossingAccountId
                    Select cad).ToList()
            If listCrossingCxC IsNot Nothing AndAlso listCrossingCxC.Count > 0 Then
                For Each crossingCxC As CrossingAccountDetailCxC In listCrossingCxC
                    Dim _accountReceivableAccountingId As Integer = crossingCxC.AccountReceivableAccountingId
                    Dim _mainAccountId As Integer = crossingCxC.MainAccountId
                    crossingCxC.MainAccountDescription = (From ma In _context.MainAccounts
                                                          Where ma.Id = _mainAccountId
                                                          Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
                    Dim varAccountPayable = (From ara In _context.AccountReceivableAccounting
                                             Join ar In _context.AccountReceivable On ara.AccountReceivableId Equals ar.Id
                                             Where ara.Id = _accountReceivableAccountingId
                                             Select New With {.BillNumber = ar.InvoiceNumber, .Value = ar.Value, .Balance = ar.Balance}).FirstOrDefault()
                    'Dim varAccountPayable = (From acp In _context.AccountPayable Where acp.Id = _accountReceivableId Select New With {.BillNumber = acp.BillNumber, .Value = acp.Value, .Balance = acp.Balance}).FirstOrDefault()
                    crossingCxC.BillNumber = varAccountPayable.BillNumber
                    crossingCxC.Value = varAccountPayable.Value
                    crossingCxC.Balance = varAccountPayable.Balance
                Next
            End If
            Return listCrossingCxC
        Else
            Return (From cad As CrossingAccountDetailCxC In _context.CrossingAccountDetailCxC.AsNoTracking()
                    Where cad.CrossingAccountId = crossingAccountId
                    Select cad).ToList()
        End If
    End Function
#End Region

End Class