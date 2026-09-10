'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 11/07/2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class GroupersRepository

    Inherits GenericRepository(Of Groupers)
    Implements IGroupersRepository

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

    Public Function GetGroupersById(id As Integer) As Groupers Implements IGroupersRepository.GetGroupersById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If

        Dim res = (From d As Groupers In Me._context.Groupers.
                       Include("GroupersCups").
                       Include("GroupersActivities")
                   Where d.Id = id
                   Select d).FirstOrDefault

        If res Is Nothing Then
            Return New Groupers()
        End If

        Return res
    End Function

    Public Function GetGroupers(code As String) As Groupers Implements IGroupersRepository.GetGroupers
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If

        Dim res = (From d As Groupers In Me._context.Groupers.
                       Include("GroupersCups").
                       Include("GroupersActivities")
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault

        If res IsNot Nothing Then
            If res.ParentId IsNot Nothing Then
                Dim parent = (From g In _context.Groupers.AsNoTracking Where g.Id = res.ParentId Select g).FirstOrDefault
                res.ParentCodeName = String.Concat(parent.Code, " - ", parent.Description)
            End If

            'Dictionaries
            Dim dictionaryCUPSEntities As New Dictionary(Of Integer, CUPSEntity)()
            Dim dictionaryCupsSubgroups As New Dictionary(Of Integer, CupsSubgroup)()
            Dim dictionaryCupsGroups As New Dictionary(Of Integer, CupsGroup)()
            Dim dictionaryContractDescriptions As New Dictionary(Of Integer, String)()

            'Items Individuals
            Dim cups As CUPSEntity = Nothing
            Dim subgr As CupsSubgroup = Nothing
            Dim gr As CupsGroup = Nothing
            Dim ContractDescriptionCodeName As String = Nothing

            For Each i In res.GroupersCups
                If Not dictionaryCUPSEntities.ContainsKey(i.CUPSEntityId) Then
                    cups = (From cd In _context.CUPSEntity.AsNoTracking Where cd.Id = i.CUPSEntityId Select cd).FirstOrDefault
                    dictionaryCUPSEntities.Add(i.CUPSEntityId, cups)
                Else
                    cups = dictionaryCUPSEntities(i.CUPSEntityId)
                End If

                If Not dictionaryCupsSubgroups.ContainsKey(cups.CUPSSubGroupId) Then
                    subgr = (From cd In _context.CupsSubgroup.AsNoTracking Where cd.Id = cups.CUPSSubGroupId Select cd).FirstOrDefault
                    dictionaryCupsSubgroups.Add(cups.CUPSSubGroupId, subgr)
                Else
                    subgr = dictionaryCupsSubgroups(cups.CUPSSubGroupId)
                End If

                If Not dictionaryCupsGroups.ContainsKey(subgr.CupsGroupId) Then
                    gr = (From cd In _context.CupsGroup.AsNoTracking Where cd.Id = subgr.CupsGroupId Select cd).FirstOrDefault
                    dictionaryCupsGroups.Add(subgr.CupsGroupId, gr)
                Else
                    gr = dictionaryCupsGroups(subgr.CupsGroupId)
                End If

                i.DescriptionCups = String.Concat(cups.Code, " - ", cups.Description)
                i.CupsSubGroupCodeName = String.Concat(subgr.Code, " - ", subgr.Name)
                i.CupsGroupCodeName = String.Concat(gr.Code, " - ", gr.Name)

                If i.ContractDescriptionId IsNot Nothing Then
                    If Not dictionaryContractDescriptions.ContainsKey(i.ContractDescriptionId) Then
                        ContractDescriptionCodeName = (From cd In _context.ContractDescriptions.AsNoTracking Where cd.Id = i.ContractDescriptionId Select String.Concat(cd.Code, " - ", cd.Name)).FirstOrDefault
                        dictionaryContractDescriptions.Add(i.ContractDescriptionId, ContractDescriptionCodeName)
                    Else
                        ContractDescriptionCodeName = dictionaryContractDescriptions(i.ContractDescriptionId)
                    End If
                    i.ContractDescriptionCodeName = ContractDescriptionCodeName
                End If
            Next

            res.OriginalValue = (From g In _context.Groupers.AsNoTracking Where g.Code.Equals(code.Trim()) Select g).FirstOrDefault
            Return res
        Else
            Return New Groupers()
        End If
    End Function

    Public Function GetGroupersPOCO(code As String) As Groupers Implements IGroupersRepository.GetGroupersPOCO
        Dim grouper = (From e In _context.Groupers Where e.Code = code Select e).FirstOrDefault()
        If grouper Is Nothing Then
            grouper = New Groupers
        End If
        Return grouper
    End Function

    Public Function ValidateIfGrouperIsParent(id As Integer) As Boolean Implements IGroupersRepository.ValidateIfGrouperIsParent
        Return (From g In _context.Groupers.AsNoTracking Where g.ParentId = id).Any()
    End Function

    Public Function GetListGroupersPOCO(listGrouperCode As List(Of String)) As List(Of Groupers) Implements IGroupersRepository.GetListGroupersPOCO
        If listGrouperCode Is Nothing OrElse listGrouperCode.Count = 0 Then
            Return New List(Of Groupers)
        End If
        Return (From e In _context.Groupers.AsNoTracking() Where listGrouperCode.Contains(e.Code) Select e).ToList()
    End Function

End Class
