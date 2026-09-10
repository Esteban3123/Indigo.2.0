'***********************************************************************
' Assembly         : Infrastructure.Data.ModelRepository
' Author           : Rafael Eduardo Patiño
' Created          : 09-01-2015

' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class ReclassificationRepository
    Inherits GenericRepository(Of PortfolioReclassification)
    Implements IReclassificationRepository

#Region "Context"
    Private _context As IGlobalModelUnitOfWork
#End Region

#Region "Builder"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' metodo para obtener un concepto de nota
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetReclassification(code As String, Optional tracking As Boolean = True) As PortfolioReclassification Implements IReclassificationRepository.GetReclassification
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        If tracking = False Then
            Dim res = (From d As PortfolioReclassification In _context.PortfolioReclassification.AsNoTracking Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault()
            If res IsNot Nothing AndAlso res.Id > 0 Then
                Dim accountReceivable = (From ar As AccountReceivable In _context.AccountReceivable.AsNoTracking.Include("Currency").AsNoTracking() Where ar.Id = res.AccountReceivableId Select ar).FirstOrDefault
                res.AccountReceivableDescription = accountReceivable.Code
                res.CurrencyId = accountReceivable.CurrencyId
                res.CurrencyAbbreviation = accountReceivable?.Currency?.Abbreviation

                Dim accountSource = (From ac As MainAccounts In _context.MainAccounts.AsNoTracking Where ac.Id = res.SourceAccountId Select ac).FirstOrDefault()
                res.SourceAccountDescription = accountSource.Number & " " & accountSource.Name

                Dim accountTarget = (From ac As MainAccounts In _context.MainAccounts.AsNoTracking Where ac.Id = res.TargetAccountId Select ac).FirstOrDefault()
                res.TargetAccountDescription = accountSource.Number & " " & accountSource.Name
                res.OriginalValue = (From d As PortfolioReclassification In _context.PortfolioReclassification.AsNoTracking Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
                Return res
            Else
                Return New PortfolioReclassification()
            End If
        Else
            Dim res = (From d As PortfolioReclassification In _context.PortfolioReclassification Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault()
            If res IsNot Nothing AndAlso res.Id > 0 Then
                Dim accountReceivable = (From ar As AccountReceivable In _context.AccountReceivable.AsNoTracking.Include("Currency").AsNoTracking() Where ar.Id = res.AccountReceivableId Select ar).FirstOrDefault
                res.AccountReceivableDescription = accountReceivable.Code
                res.CurrencyId = accountReceivable.CurrencyId
                res.CurrencyAbbreviation = accountReceivable?.Currency?.Abbreviation

                Dim accountSource = (From ac As MainAccounts In _context.MainAccounts.AsNoTracking Where ac.Id = res.SourceAccountId Select ac).FirstOrDefault()
                res.SourceAccountDescription = accountSource.Number & " " & accountSource.Name

                Dim accountTarget = (From ac As MainAccounts In _context.MainAccounts.AsNoTracking Where ac.Id = res.TargetAccountId Select ac).FirstOrDefault()
                res.TargetAccountDescription = accountSource.Number & " " & accountSource.Name

                res.OriginalValue = (From d As PortfolioReclassification In _context.PortfolioReclassification Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault()
                Return res
            Else
                Return New PortfolioReclassification()
            End If
        End If
        Return New PortfolioReclassification()
    End Function



    ''' <summary>
    ''' Crea un pago parcial glosas con un store procedure y enviando el partialpayment como Xml
    ''' </summary>
    ''' <param name="PartialPaymentsXml"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SavePartialPayments(PartialPaymentsXml As String, codeUser As String) As SP_GeneratePartialPayments_Result Implements IReclassificationRepository.SavePartialPayments
        Return _context.SP_GeneratePartialPayments(PartialPaymentsXml, codeUser).SingleOrDefault
    End Function

#End Region

End Class
