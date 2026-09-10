Imports Domain.Entities
Imports Domain.Base.Entities
Imports Domain.Common

'***********************************************************************
' Assembly         : Domain.Glosas
' Author           : Juan Diego Diaz Mosquera
' Created          : 28-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

''' <summary>
''' Servicio de Dominio para Movimientos Glosas
''' </summary>
Public Class GlosasMovementGlosasService
    Implements IGlosasMovementGlosasService

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
    ''' Función para transferir responsables en los movimientos
    ''' de glosas
    ''' </summary>
    Public Function TransferResponsibleMovements(ListResponsiblesMovements As List(Of ResponsibleMovements), opt As Integer) As ActionResult(Of List(Of GlosaMovementGlosa)) Implements IGlosasMovementGlosasService.TransferResponsibleMovements
        Dim listPortofolio = Me._PortFolioGlosadaRepository.ListPortfolioWithStateSendDocument()
        Dim listBlockRecord = Me._BlockRecordRepository.ListBlockRecordByIdForm("534") 'Código Formulario Evaluación
        Dim listGlosaMovements As New List(Of GlosaMovementGlosa)
        Dim ListMessage As New List(Of String)
        For Each item As ResponsibleMovements In ListResponsiblesMovements
            If opt = 1 Then
                If Not listPortofolio.Exists(Function(x) x.InvoiceNumber = item.InvoiceNumber) Then
                    LoadData(item, listBlockRecord, listGlosaMovements, ListMessage, opt)
                Else
                    ListMessage.Add("El movimiento No." & item.Id & " con Factura No." & item.InvoiceNumber & " y Responsable " & item.CodeResponsible & "-" & item.NameResponsible & " no puede ser reasignado porque la factura se encuentra en estado pendiente envío de oficio")
                End If
            Else
                LoadData(item, listBlockRecord, listGlosaMovements, ListMessage, opt)
            End If
        Next
        Dim Response As New ActionResult(Of List(Of GlosaMovementGlosa)) With {.ObjectEmbbeded = listGlosaMovements, .StateResult = True, .MessageResult = ListMessage}
        Return Response
    End Function

    Sub LoadData(ByRef item As ResponsibleMovements, ByRef listBlockRecord As List(Of Domain.Entities.BlockRecord), ByRef listGlosaMovements As List(Of GlosaMovementGlosa), ByRef ListMessage As List(Of String), opt As Integer)
        Dim glosaMovement = Me._MovementsGlosaRepository.GetMovementGlosaByIdTransfer(item.Id)
        If Not listBlockRecord.Exists(Function(x) x.IdRecord = glosaMovement.GlosaInvoiceDetail.GlosaObjectionsReceptionD.Id) Then
            If opt = 1 Then
                If item.Proceso = 1 Then
                    glosaMovement.Responsible1 = Nothing
                    glosaMovement.ResponsibleId = item.IdResponsible
                    glosaMovement.MarkAsModified()
                Else
                    glosaMovement.Responsible = Nothing
                    glosaMovement.ResponsibleReiterationId = item.IdResponsible
                    glosaMovement.MarkAsModified()
                End If
            ElseIf opt = 2 Then
                glosaMovement.ConceptGlosas = Nothing
                glosaMovement.CodeGlosaId = item.IdConceptGlosa
                glosaMovement.MarkAsModified()
            ElseIf opt = 3 Then
                glosaMovement.ResponsibleThirdPartyId = item.ResponsibleThirdPartyId
                glosaMovement.MarkAsModified()
            End If
            listGlosaMovements.Add(glosaMovement)
        Else
            ListMessage.Add("El movimiento No." & item.Id & " con Factura No." & item.InvoiceNumber & " y Responsable " & item.CodeResponsible & "-" & item.NameResponsible & " no puede ser reasignado porque el movimiento esta bloqueado")
        End If
    End Sub

End Class