'***********************************************************************
' Assembly         : Domain.Glosas
' Author           : Juan Diego Diaz Mosquera
' Created          : 27-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Common

''' <summary>
''' Servicio de Dominio para Responsables
''' </summary>
Public Class GlosasResponsiblesService
    Implements IGlosasResponsiblesService

    ''' <summary>
    ''' Interfaz repository de Movimiento Glosa
    ''' </summary>
    ''' <remarks></remarks>
    Private _MovementsGlosaRepository As IMovementGlosaRepository

    ''' <summary>
    ''' Interfaz repositorio de Cartera Glosa
    ''' </summary>
    ''' <remarks></remarks>
    Private _PortFolioGlosadaRepository As IPortfolioGlosadaRepository

    ''' <summary>
    ''' Interfaz repositorio de Cartera Glosa
    ''' </summary>
    ''' <remarks></remarks>
    Private _BlockRecordRepository As IBlockRecordRepository

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Sub New(MovementsGlosaRepository As IMovementGlosaRepository, PortfolioGlosadaRepository As IPortfolioGlosadaRepository, BlockRecordRepository As IBlockRecordRepository)
        Me._MovementsGlosaRepository = MovementsGlosaRepository
        Me._PortFolioGlosadaRepository = PortfolioGlosadaRepository
        Me._BlockRecordRepository = BlockRecordRepository
    End Sub

    ''' <summary>
    ''' Listar todos los movimientos por responsables
    ''' </summary>
    Public Function listAllResponsiblesTransfer(IdResponsible As Integer) As List(Of ResponsibleMovements) Implements IGlosasResponsiblesService.listAllResponsiblesTransfer
        Dim listMovements = Me._MovementsGlosaRepository.ListMovementGlosaByIdResponsible(IdResponsible)
        Dim listPortofolio = Me._PortFolioGlosadaRepository.ListPortfolioWithStateSendDocument()
        Dim listBlockRecord = Me._BlockRecordRepository.ListBlockRecordByIdForm("534") 'Código Formulario Evaluación
        Dim listResponsibleMovements As New List(Of ResponsibleMovements)
        For Each item As GlosaMovementGlosa In listMovements
            If Not listBlockRecord.Exists(Function(x) x.IdRecord = item.GlosaInvoiceDetail.GlosaObjectionsReceptionD.Id) Then
                If Not listPortofolio.Exists(Function(x) x.InvoiceNumber = item.InvoiceNumber) Then
                    Dim glosaMovement
                    If item.ResponsibleReiterationId Is Nothing Then
                        glosaMovement = New ResponsibleMovements With {.Proceso = 1,
                                                                        .CodeNameResponsible = item.Responsible1.Code & " - " & item.Responsible1.Name,
                                                                        .CodeResponsible = item.Responsible1.Code,
                                                                        .Id = item.Id,
                                                                        .IdResponsible = item.ResponsibleId,
                                                                        .InvoiceNumber = item.InvoiceNumber,
                                                                        .NameResponsible = item.Responsible1.Name,
                                                                        .IdConceptGlosa = item.CodeGlosaId,
                                                                        .ConceptGlosaCodeName = item.ConceptGlosas.Code & " _ " & item.ConceptGlosas.NameSpecific,
                                                                        .Entity = item?.GlosaInvoiceDetail?.GlosaObjectionsReceptionD?.GlosaObjectionsReceptionC?.Customer?.Name,
                                                                        .Radicated = item?.GlosaInvoiceDetail?.GlosaObjectionsReceptionD?.GlosaObjectionsReceptionC?.RadicatedConsecutive}
                    Else
                        glosaMovement = New ResponsibleMovements With {.Proceso = 2,
                                                                        .CodeNameResponsible = item.Responsible.Code & " - " & item.Responsible.Name,
                                                                        .CodeResponsible = item.Responsible.Code,
                                                                        .Id = item.Id,
                                                                        .IdResponsible = item.ResponsibleReiterationId,
                                                                        .InvoiceNumber = item.InvoiceNumber,
                                                                        .NameResponsible = item.Responsible.Name,
                                                                        .IdConceptGlosa = item.CodeGlosaId,
                                                                        .ConceptGlosaCodeName = item.ConceptGlosas.Code & " _ " & item.ConceptGlosas.NameSpecific,
                                                                        .Entity = item?.GlosaInvoiceDetail?.GlosaObjectionsReceptionD?.GlosaObjectionsReceptionC?.Customer?.Name,
                                                                        .Radicated = item?.GlosaInvoiceDetail?.GlosaObjectionsReceptionD?.GlosaObjectionsReceptionC?.RadicatedConsecutive}
                    End If
                    listResponsibleMovements.Add(glosaMovement)
                End If
            End If
        Next
        Return listResponsibleMovements
    End Function

End Class