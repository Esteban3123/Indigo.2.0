'***********************************************************************
' Assembly         : Infrastructure.Data.PaymentsRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 21-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class OpeningBalanceRepository
    Inherits GenericRepository(Of InitialBalance)
    Implements IOpeningBalanceRepository

    ''' <summary>
    ''' Contexto de payments
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un saldo inicial
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetOpeningBalance(code As String, Optional tracking As Boolean = True) As InitialBalance Implements IOpeningBalanceRepository.GetOpeningBalance
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If

        Dim res = (From d As InitialBalance In Me._context.InitialBalance.Include("InitialBalanceAccountPayable").Include("InitialBalanceAdvance")
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault
        If res IsNot Nothing Then

            For Each itemIBAP As InitialBalanceAccountPayable In res.InitialBalanceAccountPayable

                Dim sdl = (From s In _context.SuppliersDistributionLines.AsNoTracking.Include("Supplier").AsNoTracking.Include("DistributionLines").AsNoTracking Where s.Id = itemIBAP.SupplierDistributionLinesID Select s).FirstOrDefault
                itemIBAP.SupplierDescription = sdl.Supplier.Code + " - " + sdl.Supplier.Name + " - " + sdl.DistributionLines.Name

                If itemIBAP.CostCenterId IsNot Nothing Then
                    Dim cc = (From cos In _context.CostCenter.AsNoTracking Where cos.Id = itemIBAP.CostCenterId Select cos).FirstOrDefault
                    itemIBAP.CostCenterDescription = cc.Code + " - " + cc.Name
                End If

                Dim ma = (From mad In _context.MainAccounts.AsNoTracking Where mad.Id = itemIBAP.MainAccountId Select mad).FirstOrDefault
                itemIBAP.MainAccountDescription = ma.Number + " - " + ma.Name

                Dim filingUnit = (From fu In _context.FilingUnit.AsNoTracking Where fu.Id = itemIBAP.FilingUnitId Select fu).FirstOrDefault
                itemIBAP.FilingUnitDescription = filingUnit.Code + " - " + filingUnit.Name

                Dim supplierType = (From st In _context.SupplierType.AsNoTracking Where st.Id = itemIBAP.SupplierTypeId Select st).FirstOrDefault
                itemIBAP.SupplierTypeDescription = supplierType.Code + " - " + supplierType.Name

            Next

            For Each itemIBAD As InitialBalanceAdvance In res.InitialBalanceAdvance

                Dim sdl = (From s In _context.SuppliersDistributionLines.AsNoTracking.Include("Supplier").AsNoTracking.Include("DistributionLines").AsNoTracking Where s.Id = itemIBAD.SupplierDistributionLinesId Select s).FirstOrDefault
                itemIBAD.SupplierDescription = sdl.Supplier.Code + " - " + sdl.Supplier.Name + " - " + sdl.DistributionLines.Name

                If itemIBAD.CostCenterId IsNot Nothing Then
                    Dim cc = (From cos In _context.CostCenter.AsNoTracking Where cos.Id = itemIBAD.CostCenterId Select cos).FirstOrDefault
                    itemIBAD.CostCenterDescription = cc.Code + " - " + cc.Name
                End If

                Dim ma = (From mad In _context.MainAccounts.AsNoTracking Where mad.Id = itemIBAD.MainAccountId Select mad).FirstOrDefault
                itemIBAD.MainAccountDescription = ma.Number + " - " + ma.Name

            Next


            res.OriginalValue = (From d As InitialBalance In Me._context.InitialBalance.AsNoTracking.Include("InitialBalanceAccountPayable").AsNoTracking.Include("InitialBalanceAdvance").AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault()
            Return res


        Else
            Return New InitialBalance
        End If
    End Function


    
End Class
