'************************************************************
' Assembly         : Infrastructure.Data.Billing
' Author           : Miguel Angel Fonseca Castro
' Created          : 2020-01-31
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

Public Class ElectronicDocumentNotificationRepository
    Inherits GenericRepository(Of ElectronicDocumentNotification)
    Implements IElectronicDocumentNotificationRepository

#Region "Fields"

    ' contexto del repositorio de ciudades
    Private _context As IGlobalModelUnitOfWork

#End Region

#Region "Builder"

    ''' <summary>
    '''  Contructor del repositorio coidades el cual instancia una nueva clase
    ''' </summary>
    ''' <param name="context">contexto del repositorio</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene una notificación de un documento electronico usado para la facturación electronica por el id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetElectronicDocumentNotificationById(Id As Integer) As ElectronicDocumentNotification Implements IElectronicDocumentNotificationRepository.GetElectronicDocumentNotificationById
        Dim res As ElectronicDocumentNotification = (From a As ElectronicDocumentNotification In Me._context.ElectronicDocumentNotification.Include("ElectronicDocument")
                                                     Where a.Id = Id
                                                     Select a).FirstOrDefault()
        If res IsNot Nothing AndAlso res.Id > 0 Then
            res.OriginalValue = (From b In _context.ElectronicDocumentNotification.AsNoTracking() Where b.Id = Id Select b).FirstOrDefault()
            Return res
        Else
            Return New ElectronicDocumentNotification
        End If
    End Function

    ''' <summary>
    ''' Obtiene una notificación de un listado de documento electronico de acuerdo con el estado en que se encuentren
    ''' </summary>
    ''' <param name="status">The identifier.</param>
    ''' <returns></returns>
    Public Function GetElectronicDocumentNotificationIdsByStatus(ByVal status As Boolean) As List(Of Integer) Implements IElectronicDocumentNotificationRepository.GetElectronicDocumentNotificationIdsByStatus
        Dim res = (From ed As ElectronicDocumentNotification _
                       In Me._context.ElectronicDocumentNotification
                   Where ed.Status = status
                   Select ed.Id).Take(500).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            Return res
        Else
            Return New List(Of Integer)
        End If
    End Function

    ''' <summary>
    ''' Obtiene el tipo de nota según el documento electrónico asociado
    ''' </summary>
    ''' <param name="electronicDocumentId"></param>
    ''' <returns></returns>
    Public Function GetElectronicNoteType(electronicDocumentId As Integer) As Byte Implements IElectronicDocumentNotificationRepository.GetElectronicNoteType
        Const singleNoteType As Byte = 1
        Const detailedNoteType As Byte = 2

        Dim comesFrom As String = (From ed In _context.ElectronicDocument.AsNoTracking()
                                   Join bn In _context.BillingNote.AsNoTracking() On ed.EntityId Equals bn.Id
                                   Where ed.Id = electronicDocumentId
                                   Select bn.EntityName) _
                        .FirstOrDefault()

        If String.IsNullOrEmpty(comesFrom) OrElse Not comesFrom.Equals("PortfolioNote", StringComparison.OrdinalIgnoreCase) Then
            Return singleNoteType
        End If

        If comesFrom = "PortfolioNote" Then
            Dim portfolioNote = (From ed In _context.ElectronicDocument.AsNoTracking()
                                 Join bn In _context.BillingNote.AsNoTracking() On ed.EntityId Equals bn.Id
                                 Join pn In _context.PortfolioNote.AsNoTracking() On bn.EntityId Equals pn.Id
                                 Where ed.Id = electronicDocumentId
                                 Select pn) _
                        .FirstOrDefault()
            If portfolioNote.NoteType = 6 OrElse String.Equals(portfolioNote.EntityName, "Glosas", StringComparison.OrdinalIgnoreCase) Then
                Return detailedNoteType
            End If
        End If
        Return singleNoteType
    End Function

#End Region

End Class
