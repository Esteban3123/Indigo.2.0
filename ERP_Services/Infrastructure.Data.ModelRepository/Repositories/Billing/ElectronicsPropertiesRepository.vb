'***********************************************************************
' Assembly         : Infrastructure.Data.Billing
' Author           : Giovanny Plazas Lozano
' Created          : 04-03-2024
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.Data.Base
#End Region

Public Class ElectronicsPropertiesRepository
    Inherits GenericRepository(Of ElectronicsProperties)
    Implements IElectronicsPropertiesRepository

    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' retorna si la factura asociada a la nota es de salud 
    ''' </summary>
    ''' <param name="billingNoteId"></param>
    ''' <returns> true - fact salud
    '''   false - fact basica o producto      
    ''' </returns>
    Public Function ValidateOriginNoteToRIPS(billingNoteId As Integer) As Boolean Implements IElectronicsPropertiesRepository.ValidateOriginNoteToRIPS
        If billingNoteId = 0 Then
            Return False
        End If

        Dim billingNote = (From x In _context.BillingNote.AsNoTracking()
                           Where x.Id = billingNoteId)?.FirstOrDefault()

        If billingNote Is Nothing OrElse billingNote.NoteDate.Date < New Date(2025, 2, 1) Then
            Return False
        End If

        Dim flag As Boolean = False
        Dim listDocTypeDontAllow = New List(Of Integer) From {6, 7}

        Select Case billingNote.EntityName
            Case NameOf(Invoice)
                flag = (From y In _context.Invoice.AsNoTracking()
                        Join a In _context.AccountReceivable.AsNoTracking() On a.InvoiceId Equals y.Id
                        Where y.Id = billingNote.EntityId AndAlso Not listDocTypeDontAllow.Contains(y.DocumentType) _
                               AndAlso _context.PortfolioReclassification.Any(Function(r) r.AccountReceivableId = a.Id AndAlso r.EntityName = "RadicateInvoiceC")).Any()
            Case NameOf(PortfolioNote)
                flag = (From y In _context.PortfolioNote.AsNoTracking()
                        Join t In _context.PortfolioNoteAccountReceivableAdvance.AsNoTracking() On y.Id Equals t.PortfolioNoteId
                        Join a In _context.AccountReceivable.AsNoTracking() On a.Id Equals t.AccountReceivableId
                        Where y.Id = billingNote.EntityId AndAlso y.NoteType = 6 AndAlso a.AccountReceivableType = 2).Any()
        End Select

        Return flag
    End Function
End Class