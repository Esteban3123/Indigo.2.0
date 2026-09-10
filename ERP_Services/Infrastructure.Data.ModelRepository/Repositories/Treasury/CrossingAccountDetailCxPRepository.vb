'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 29-09-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class CrossingAccountDetailCxPRepository
    Inherits GenericRepository(Of CrossingAccountDetailCxP)
    Implements ICrossingAccountDetailCxPRepository

    'Contexto global
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#Region "Methods"
    ''' <summary>
    ''' Obtiene el detalle del cruce de cuentas de CxP por id
    ''' </summary>
    Public Function GetCrossingAccountDetailCxPById(Id As Integer, Optional tracking As Boolean = False) As CrossingAccountDetailCxP Implements ICrossingAccountDetailCxPRepository.GetCrossingAccountDetailCxPById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query As CrossingAccountDetailCxP = Nothing
        If tracking Then
            query = (From cad As CrossingAccountDetailCxP In _context.CrossingAccountDetailCxP
                     Where cad.Id = Id Select cad).FirstOrDefault()
        Else
            query = (From cad As CrossingAccountDetailCxP In _context.CrossingAccountDetailCxP.AsNoTracking()
                     Where cad.Id = Id Select cad).FirstOrDefault()
        End If
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From cad As CrossingAccountDetailCxP In _context.CrossingAccountDetailCxP.AsNoTracking()
                     Where cad.Id = Id Select cad).FirstOrDefault()
            Return query
        Else
            Return New CrossingAccountDetailCxP()
        End If
    End Function

    ''' <summary>
    ''' Lista los detalles de cruces de cuentas de CxP por el id de cruce de cuenta
    ''' </summary>
    Public Function ListCrossingAccountDetailCxPByCrossingAccountId(crossingAccountId As Integer, Optional tracking As Boolean = False) As List(Of CrossingAccountDetailCxP) Implements ICrossingAccountDetailCxPRepository.ListCrossingAccountDetailCxPByCrossingAccountId
        If crossingAccountId = 0 Then
            Throw New ArgumentNullException("crossingAccountId")
        End If
        Dim query As List(Of CrossingAccountDetailCxP) = Nothing
        If tracking Then
            Dim listCrossingCxP As List(Of CrossingAccountDetailCxP) = (From cad As CrossingAccountDetailCxP In _context.CrossingAccountDetailCxP
                    Where cad.CrossingAccountId = crossingAccountId
                    Select cad).ToList()
            If listCrossingCxP IsNot Nothing AndAlso listCrossingCxP.Count > 0 Then
                For Each crossingCxP As CrossingAccountDetailCxP In listCrossingCxP
                    Dim _accountPayableId As Integer = crossingCxP.AccountPayableId
                    Dim _mainAccountId As Integer = crossingCxP.MainAccountId
                    crossingCxP.MainAccountDescription = (From ma In _context.MainAccounts Where ma.Id = _mainAccountId Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
                    Dim varAccountPayable = (From acp In _context.AccountPayable Where acp.Id = _accountPayableId Select New With {.BillNumber = acp.BillNumber, .Value = acp.Value, .Balance = acp.Balance}).FirstOrDefault()
                    crossingCxP.BillNumber = varAccountPayable.BillNumber
                    crossingCxP.Value = varAccountPayable.Value
                    crossingCxP.Balance = varAccountPayable.Balance
                Next
            End If
            Return listCrossingCxP
        Else
            Return (From cad As CrossingAccountDetailCxP In _context.CrossingAccountDetailCxP.AsNoTracking()
                    Where cad.CrossingAccountId = crossingAccountId
                    Select cad).ToList()
        End If
    End Function
#End Region

End Class