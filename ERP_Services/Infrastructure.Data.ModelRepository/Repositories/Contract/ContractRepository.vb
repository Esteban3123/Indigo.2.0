'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class ContractRepository
    Inherits GenericRepository(Of Contract)
    Implements IContractRepository

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
    ''' Obtiene un contrato por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetContract(code As String) As Contract Implements IContractRepository.GetContract
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As Contract In Me._context.Contract
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault
        If res IsNot Nothing Then

            Dim health = (From h In _context.HealthAdministrator.AsNoTracking Where h.Id = res.HealthAdministratorId Select h).FirstOrDefault
            res.HealthAdministratorDescription = health.Code + " - " + health.Name

            If res.ContractEntityId IsNot Nothing Then
                Dim contractEntity = (From ce In _context.ContractEntity.AsNoTracking Where res.ContractEntityId = ce.Id Select ce).FirstOrDefault
                res.ContractEntityDescription = contractEntity.Code + " - " + contractEntity.Name
            End If

            res.OriginalValue = (From g In _context.Contract.AsNoTracking
                                 Where g.Code.Equals(code.Trim())
                                 Select g).FirstOrDefault

            Return res
        Else
            Return New Contract()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un contrato por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetContractById(id As Integer) As Contract Implements IContractRepository.GetContractById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.Contract Where d.Id = id Select d).ToList
        If res.Count > 0 Then
            res.SingleOrDefault.OriginalValue = (From d As Contract In Me._context.Contract.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
            Return res.SingleOrDefault
        Else
            Return New Contract()
        End If
    End Function

    Public Function GetContractByIdWithAggregates(id As Integer) As Contract Implements IContractRepository.GetContractByIdWithAggregates
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.Contract.Include("HealthAdministrator") Where d.Id = id Select d).ToList
        If res.Count > 0 Then
            res.SingleOrDefault.OriginalValue = (From d As Contract In Me._context.Contract.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
            Return res.SingleOrDefault
        Else
            Return New Contract()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un cupsEntity por id del careGroup
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetContractByCareGroupIdForMedicalFees(CareGroupId As Integer) As Contract Implements IContractRepository.GetContractByCareGroupIdForMedicalFees
        If CareGroupId = 0 Then
            Throw New ArgumentNullException("CareGroupId")
        End If
        Dim careGroup = (From cg In _context.CareGroup.AsNoTracking Where cg.Id = CareGroupId Select cg).FirstOrDefault
        If careGroup IsNot Nothing Then
            If careGroup.ContractId Is Nothing Then
                Return Nothing
            End If
            Dim contract = (From c In _context.Contract.AsNoTracking Where c.Id = careGroup.ContractId Select c).FirstOrDefault
            If contract Is Nothing Then
                Return Nothing
            Else
                Return contract
            End If
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Proceso de contratos
    ''' </summary>
    ''' <param name="xmlData"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    Public Function SP_SaveContract(xmlData As String, codeUser As String) As SP_SaveContract_Result Implements IContractRepository.SP_SaveContract
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveContract(xmlData, codeUser).SingleOrDefault
    End Function

End Class
