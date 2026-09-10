'***********************************************************************
' Assembly         : Infrastructure.Data.PaymentsRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class DeferredCausationRepository
    Inherits GenericRepository(Of DeferredCausation)
    Implements IDeferredCausationRepository

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
    ''' Obtiene la lista de las causaciones diferidas
    ''' </summary>
    ''' <param name="idAccountPayable"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDeferredCausationByIdAccountPayable(idAccountPayable As Integer, Optional tracking As Boolean = True) As List(Of DeferredCausation) Implements IDeferredCausationRepository.GetDeferredCausationByIdAccountPayable
        If idAccountPayable = 0 Then
            Throw New ArgumentNullException("idAccountPayable")
        End If
        Dim listDeferredCausation = (From d In Me._context.DeferredCausation.Include("DeferredCausationDetails").Include("DeferredCausationShare") Where d.IdAccountPayable = idAccountPayable Select d).ToList
        If listDeferredCausation.Count > 0 Then

            For Each itemCabecera As DeferredCausation In listDeferredCausation
                Dim account = (From m In _context.MainAccounts.AsNoTracking Where itemCabecera.IdMainAccount = m.Id Select m).FirstOrDefault
                itemCabecera.NumberNameMainAccount = account.Number + " - " + account.Name

                If itemCabecera.IdCostCenter IsNot Nothing Then
                    Dim costCenter = (From cc In _context.CostCenter.AsNoTracking Where itemCabecera.IdCostCenter = cc.Id Select cc).FirstOrDefault
                    itemCabecera.DescriptionCostCenter = costCenter.Code + " - " + costCenter.Name
                End If

                For Each itemDetalle As DeferredCausationDetails In itemCabecera.DeferredCausationDetails
                    Dim accountDetalle = (From ma In _context.MainAccounts.AsNoTracking Where itemDetalle.IdMainAccount = ma.Id Select ma).FirstOrDefault
                    itemDetalle.NumberNameMainAccount = accountDetalle.Number + " - " + accountDetalle.Name

                    If itemDetalle.IdCostCenter IsNot Nothing Then
                        Dim costCenterDetalle = (From ccd In _context.CostCenter.AsNoTracking Where itemDetalle.IdCostCenter = ccd.Id Select ccd).FirstOrDefault
                        itemDetalle.DescriptionCostCenter = costCenterDetalle.Code + " - " + costCenterDetalle.Name
                    End If
                Next

            Next

            Return listDeferredCausation
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene la lista de las causaciones diferidas
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDeferredCausationByAccountPayableCode(code As String) As List(Of DeferredCausation) Implements IDeferredCausationRepository.GetDeferredCausationByAccountPayableCode
        'Dim listDeferredCausation = (From d In Me._context.DeferredCausation.Include("DeferredCausationDetails").Include("DeferredCausationShare")
        '                             Join ap In Me._context.AccountPayable On d.IdAccountPayable Equals ap.Id
        '                             Where ap.Code = code
        '                             Select d).ToList

        Dim listDeferredCausation = (From d In Me._context.DeferredCausation.Include("AccountPayable.Currency").Include("DeferredCausationDetails").Include("DeferredCausationShare")
                                     Where d.AccountPayable.Code = code
                                     Select d).ToList

        If listDeferredCausation.Count > 0 Then
            For Each itemCabecera As DeferredCausation In listDeferredCausation
                Dim account = (From m In _context.MainAccounts.AsNoTracking Where itemCabecera.IdMainAccount = m.Id Select m).FirstOrDefault
                itemCabecera.NumberNameMainAccount = account.Number + " - " + account.Name

                itemCabecera.CurrencyAbbreviation = itemCabecera?.AccountPayable?.Currency?.Abbreviation

                If itemCabecera.IdCostCenter IsNot Nothing Then
                    Dim costCenter = (From cc In _context.CostCenter.AsNoTracking Where itemCabecera.IdCostCenter = cc.Id Select cc).FirstOrDefault
                    itemCabecera.DescriptionCostCenter = costCenter.Code + " - " + costCenter.Name
                End If

                For Each itemDetalle As DeferredCausationDetails In itemCabecera.DeferredCausationDetails
                    Dim accountDetalle = (From ma In _context.MainAccounts.AsNoTracking Where itemDetalle.IdMainAccount = ma.Id Select ma).FirstOrDefault
                    itemDetalle.NumberNameMainAccount = accountDetalle.Number + " - " + accountDetalle.Name

                    If itemDetalle.IdCostCenter IsNot Nothing Then
                        Dim costCenterDetalle = (From ccd In _context.CostCenter.AsNoTracking Where itemDetalle.IdCostCenter = ccd.Id Select ccd).FirstOrDefault
                        itemDetalle.DescriptionCostCenter = costCenterDetalle.Code + " - " + costCenterDetalle.Name
                    End If
                Next

            Next

            Return listDeferredCausation
        Else
            Return Nothing
        End If
    End Function

End Class
