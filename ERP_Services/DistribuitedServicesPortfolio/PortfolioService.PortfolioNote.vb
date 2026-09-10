'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 01-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Application.Portfolio
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity
#End Region

Partial Class PortfolioService

    ''' <summary>
    ''' metodo para pegar en la rejilla
    ''' </summary>
    ''' <param name="dataImportFile"></param>
    ''' <param name="dataCopyPaste"></param>
    ''' <param name="CompanyType"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function SetCopyPasteOrImportFilePortfolioNote(dataImportFile As List(Of Domain.Base.Entities.ImportFileRow), dataCopyPaste As List(Of List(Of String)), CompanyType As Integer, OperatingUnit As Integer, ParamArray parameters() As Object) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.PortfolioNoteAccountReceivableAdvance)) Implements IPortfolioServicePortfolioNote.SetCopyPasteOrImportFilePortfolioNote
        Using service As IPortfolioNoteAdminService = Container.Current.Resolve(Of IPortfolioNoteAdminService)()
            Return service.SetCopyPasteOrImportFilePortfolioNote(dataImportFile, dataCopyPaste, CompanyType, OperatingUnit, parameters)
        End Using
    End Function

    ''' <summary>
    ''' Gets the portfolio note by identifier.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetPortfolioNoteById(id As Integer) As Domain.Entities.PortfolioNote Implements IPortfolioServicePortfolioNote.GetPortfolioNoteById
        Using service As IPortfolioNoteAdminService = Container.Current.Resolve(Of IPortfolioNoteAdminService)()
            Return service.GetPortfolioNoteById(id)
        End Using
    End Function

    ''' <summary>
    ''' Gets the portfolio note by code.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function GetPortfolioNoteByCode(code As String, audit As AuditMessage) As Domain.Entities.PortfolioNote Implements IPortfolioServicePortfolioNote.GetPortfolioNoteByCode
        Using service As IPortfolioNoteAdminService = Container.Current.Resolve(Of IPortfolioNoteAdminService)()
            Return service.GetPortfolioNoteByCode(code, audit)
        End Using
    End Function

    ''' <summary>
    ''' metodo para obtener los detalles de la nota
    ''' </summary>
    ''' <param name="idPortfolioNote"></param>
    ''' <returns></returns>
    Public Function GetPortfolioNoteDetailByIdPortfolioNote(idPortfolioNote As Integer) As List(Of Domain.Entities.PortfolioNoteDetail) Implements IPortfolioServicePortfolioNote.GetPortfolioNoteDetailByIdPortfolioNote
        Using service As IPortfolioNoteAdminService = Container.Current.Resolve(Of IPortfolioNoteAdminService)()
            Return service.GetPortfolioNoteDetailByIdPortfolioNote(idPortfolioNote)
        End Using
    End Function

    ''' <summary>
    ''' metodo para obtener las facturas o los anticipos asociados a esa nota
    ''' </summary>
    ''' <param name="idPortfolioNote"></param>
    ''' <returns></returns>
    Public Function GetPortfolioNoteAccountReceivableAdvanceByIdPortfolioNote(idPortfolioNote As Integer) As List(Of Domain.Entities.PortfolioNoteAccountReceivableAdvance) Implements IPortfolioServicePortfolioNote.GetPortfolioNoteAccountReceivableAdvanceByIdPortfolioNote
        Using service As IPortfolioNoteAdminService = Container.Current.Resolve(Of IPortfolioNoteAdminService)()
            Return service.GetPortfolioNoteAccountReceivableAdvanceByIdPortfolioNote(idPortfolioNote)
        End Using
    End Function

    ''' <summary>
    ''' obtiene las distribuciones
    ''' </summary>
    ''' <param name="portfolioNoteId"></param>
    ''' <returns></returns>
    Public Function GetPortfolioNoteDistributionByPortfolioNoteId(portfolioNoteId As Integer) As List(Of Domain.Entities.PortfolioNoteDistribution) Implements IPortfolioServicePortfolioNote.GetPortfolioNoteDistributionByPortfolioNoteId
        Using service As IPortfolioNoteAdminService = Container.Current.Resolve(Of IPortfolioNoteAdminService)()
            Return service.GetPortfolioNoteDistributionByPortfolioNoteId(portfolioNoteId)
        End Using
    End Function

    ''' <summary>
    ''' Saves the portfolio note.
    ''' </summary>
    ''' <param name="PortfolioNote">The portfolio note.</param>
    ''' <returns></returns>
    Public Function SavePortfolioNote(PortfolioNote As Domain.Entities.PortfolioNote, idSequence As Int64, session As SessionValues) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PortfolioNote) Implements IPortfolioServicePortfolioNote.SavePortfolioNote
        Using service As IPortfolioNoteAdminService = Container.Current.Resolve(Of IPortfolioNoteAdminService)()
            Return service.SavePortfolioNote(PortfolioNote, session.AuditMessageWcf, session, idSequence)
        End Using
    End Function

    ''' <summary>
    ''' valida la nota que se selcciona en el combo de facturas de Notas de cartera
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    Public Function ValidateSelectedByAccountReceivableAccounting(obj As String) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Invoice) Implements IPortfolioServicePortfolioNote.ValidateSelectedByAccountReceivableAccounting
        Using service As IPortfolioNoteAdminService = Container.Current.Resolve(Of IPortfolioNoteAdminService)()
            Return service.ValidateSelectedByAccountReceivableAccounting(obj)
        End Using
    End Function
End Class
