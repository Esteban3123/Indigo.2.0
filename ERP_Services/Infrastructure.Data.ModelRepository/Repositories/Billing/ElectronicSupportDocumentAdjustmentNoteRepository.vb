'************************************************************
' Assembly         : Domain.Billing
' Author           : Diego Andrés Roldán Lozano
' Created          : 2022-07-27
'
' Copyright        : (c) . All rights reserved.
'************************************************************

Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class ElectronicSupportDocumentAdjustmentNoteRepository
    Inherits GenericRepository(Of ElectronicSupportDocumentAdjustmentNote)
    Implements IElectronicSupportDocumentAdjustmentNoteRepository, Inject

#Region "Builder"

    ''' <summary>
    ''' Contexto de inventario
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de inventario
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene una nota de ajuste por id
    ''' </summary>
    ''' <param name="AdjustmentNoteId"></param>
    ''' <returns></returns>
    Public Function GetElectronicAdjustmentNoteWithAggregateById(AdjustmentNoteId As Integer) As ElectronicSupportDocumentAdjustmentNote Implements IElectronicSupportDocumentAdjustmentNoteRepository.GetElectronicAdjustmentWithAggregateNoteById
        Dim res As ElectronicSupportDocumentAdjustmentNote
        res = (From esdn In Me._context.ElectronicSupportDocumentAdjustmentNote.Include("ElectronicSupportDocument")
               Where esdn.Id = AdjustmentNoteId
               Select esdn).FirstOrDefault()
        If res IsNot Nothing AndAlso res.Id > 0 Then
            Return res
        Else
            Return New ElectronicSupportDocumentAdjustmentNote
        End If
    End Function
#End Region

End Class
