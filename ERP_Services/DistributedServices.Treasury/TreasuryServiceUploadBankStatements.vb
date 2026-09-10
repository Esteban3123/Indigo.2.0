'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Johan Sebastian Cuellar Esquivel
' Created          : 13-04-2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Security
Imports Infrastructure.CrossCutting.IOC
Imports Application.Treasury
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity
#End Region

Partial Public Class TreasuryService
    Public Function GetUploadBankStatementsByCode(code As String, audit As AuditMessage) As UploadBankStatements Implements ITreasuryServiceUploadBankStatements.GetUploadBankStatementsByCode
        Using service As IUploadBankStatementsAdminService = Container.Current.Resolve(Of IUploadBankStatementsAdminService)()
            Return service.GetUploadBankStatementsByCode(code, audit)
        End Using
    End Function

    Public Function SaveUploadBankStatements(UploadBankStatements As UploadBankStatements, audit As AuditMessage) As ActionResult(Of UploadBankStatements) Implements ITreasuryServiceUploadBankStatements.SaveUploadBankStatements
        Using service As IUploadBankStatementsAdminService = Container.Current.Resolve(Of IUploadBankStatementsAdminService)()
            Return service.SaveUploadBankStatements(UploadBankStatements, audit)
        End Using
    End Function


    Public Function GetUploadBankStatementsDetailByUploadBankStatementsId(UploadBankStatementsId As Integer) As List(Of UploadBankStatementsDetail) Implements ITreasuryServiceUploadBankStatements.GetUploadBankStatementsDetailByUploadBankStatementsId
        Using service As IUploadBankStatementsAdminService = Container.Current.Resolve(Of IUploadBankStatementsAdminService)()
            Return service.GetUploadBankStatementsDetailByUploadBankStatementsId(UploadBankStatementsId)
        End Using
    End Function

    Public Function setcopypasteorimportfilesetbankstatementsdetail(session As SessionValues, dataimportfile As List(Of ImportFileRow), datacopypaste As List(Of List(Of String))) As ActionResult(Of List(Of UploadBankStatementsDetail)) Implements ITreasuryServiceUploadBankStatements.SetCopyPasteOrImportFileSetBankStatementsDetail
        Using service As IUploadBankStatementsAdminService = Container.Current.Resolve(Of IUploadBankStatementsAdminService)()
            Return service.SetCopyPasteOrImportFileSetBankStatementsDetail(dataimportfile, datacopypaste)
        End Using
    End Function
    ''' <summary>
    ''' Función encargada de obtener los Conceptos de Conciliación acorde con la entidad Bancaria
    ''' </summary>
    ''' <param name="EntityBankAccountId"></param>
    ''' <returns></returns>
    Public Function GetBankConciliationConceptsByEntityBankAccountId(EntityBankAccountId As Integer) As ActionResult(Of List(Of BankConciliationConcepts)) Implements ITreasuryServiceUploadBankStatements.GetBankConciliationConceptsByEntityBankAccountId
        Using service As IUploadBankStatementsAdminService = Container.Current.Resolve(Of IUploadBankStatementsAdminService)()
            Return service.GetBankConciliationConceptsByEntityBankAccountId(EntityBankAccountId)
        End Using
    End Function
    ''' <summary>
    ''' Función encargada de asignar el DocumentType a los Detalles del extracto acorde con sus conceptos
    ''' </summary>
    ''' <param name="ConciliationConcepts"></param>
    ''' <param name="UploadBankStatementDetails"></param>
    ''' <returns></returns>
    Public Function MatchConciliationConceptsWithDescriptionTransaction(ConciliationConcepts As List(Of BankConciliationConcepts), UploadBankStatementDetails As List(Of UploadBankStatementsDetail)) As ActionResult(Of List(Of UploadBankStatementsDetail)) Implements ITreasuryServiceUploadBankStatements.MatchConciliationConceptsWithDescriptionTransaction
        Using Service As IUploadBankStatementsAdminService = Container.Current.Resolve(Of IUploadBankStatementsAdminService)()
            Return Service.MatchConciliationConceptsWithDescriptionTransaction(ConciliationConcepts, UploadBankStatementDetails)
        End Using
    End Function
    ''' <summary>
    ''' Función que obtiene el EndPoint para la API de Extractos Bancarios
    ''' </summary>
    ''' <param name="fileBytes"></param>
    ''' <param name="fileName"></param>
    ''' <param name="year"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ExtractBankStatementDetail(fileBytes As Byte(), fileName As String, year As String, audit As AuditMessage) As ActionResult(Of String) Implements ITreasuryServiceUploadBankStatements.ExtractBankStatementDetail
        Dim containerCode = Container.Current.Resolve(Of ICommonVariables)().getContainer()
        Dim endpoint = Container.Current.Resolve(Of IEndpointsRepository)().GetEndpointsByContainerCode(containerCode, "bank-statement")
        If endpoint Is Nothing Then
            Return New ActionResult(Of String) With {.StateResult = False, .Message = "Endpoint 'bank-statement' no configurado para el contenedor."}
        End If
        Using service As IUploadBankStatementsAdminService = Container.Current.Resolve(Of IUploadBankStatementsAdminService)()
            Return service.ExtractBankStatementDetail(fileBytes, fileName, year, endpoint.UrlBase)
        End Using
    End Function

End Class
