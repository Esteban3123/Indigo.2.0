'***********************************************************************
' Assembly         : Infrastructure.Data.ModelRepository
' Author           : Juan David Capera Núñez
' Created          : 26-12-2022
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.Data.Base
#End Region

Public Class DrugInteractionRepository
    Inherits GenericRepository(Of DrugInteraction)
    Implements IDrugInteractionRepository

    ''' <summary>
    ''' Contexto
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene una interacción de medicamento por el Id del DCI padre
    ''' </summary>
    ''' <param name="ParentDCIid"></param>
    ''' <returns></returns>
    Public Function GetDrugInteractionByDCIParentId(ParentDCIid As Integer) As List(Of DrugInteraction) Implements IDrugInteractionRepository.GetDrugInteractionByDCIParentId
        Dim res = (From di In _context.DrugInteraction.Include("DCI").Include("DCI1").Include("ATCEntity")
                   Where di.ParentDCIId = ParentDCIid
                   Select di).ToList()

        If res IsNot Nothing AndAlso res.Count > 0 Then
            For Each item In res
                item.isInherited = True
                item.isInheritedName = "Interacciones heredadas"
                item.DCICode = item.DCI?.Code
                item.DCIName = item.DCI?.Name
                item.ATCCode = item.ATCEntity?.Code
                item.ATCName = item.ATCEntity?.Name
                item.DCIParentCodeName = $"{item.DCI1?.Code} - {item.DCI1?.Name}"
            Next
            Return res
        Else
            Return New List(Of DrugInteraction)
        End If
    End Function

End Class
