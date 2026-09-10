'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           :
' Created          : 05-06-2026
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports Domain.Base.Entities

Public Class ContributorTypeSubtypeRepository
    Inherits GenericRepository(Of ContributorTypeSubtype)
    Implements IContributorTypeSubtypeRepository

    Private _context As IPayrollUnitOfWork

    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Lista todas las combinaciones Tipo+Subtipo de cotizante con nombres desnormalizados
    ''' </summary>
    Public Function ListAllContributorTypeSubtype() As List(Of ContributorTypeSubtype) Implements IContributorTypeSubtypeRepository.ListAllContributorTypeSubtype
        Dim raw = (From cts In _context.ContributorTypeSubtype
                   Join ct In _context.ContributorType On cts.ContributorTypeId Equals ct.Id
                   Join cs In _context.ContributorSubtype On cts.ContributorSubtypeId Equals cs.Id
                   Where cts.State = True
                   Select New With {
                       .Id = cts.Id,
                       .ContributorTypeId = cts.ContributorTypeId,
                       .ContributorSubtypeId = cts.ContributorSubtypeId,
                       .State = cts.State,
                       .ContributorTypeName = ct.Name,
                       .ContributorSubtypeName = cs.Name,
                           .ContributorSubtypeCode = cs.Code
                   }).ToList()

        Return raw.Select(Function(x) New ContributorTypeSubtype With {
                       .Id = x.Id,
                       .ContributorTypeId = x.ContributorTypeId,
                       .ContributorSubtypeId = x.ContributorSubtypeId,
                       .State = x.State,
                       .ContributorTypeName = x.ContributorTypeName,
                       .ContributorSubtypeName = x.ContributorSubtypeName,
                        .ContributorSubtypeCode = x.ContributorSubtypeCode
                   }).ToList()
    End Function

    ''' <summary>
    ''' Lista los subtipos válidos para un tipo de cotizante
    ''' </summary>
    Public Function ListByContributorTypeId(contributorTypeId As Integer) As List(Of ContributorTypeSubtype) Implements IContributorTypeSubtypeRepository.ListByContributorTypeId
        Dim raw = (From cts In _context.ContributorTypeSubtype
                   Join ct In _context.ContributorType On cts.ContributorTypeId Equals ct.Id
                   Join cs In _context.ContributorSubtype On cts.ContributorSubtypeId Equals cs.Id
                   Where cts.ContributorTypeId = contributorTypeId AndAlso cts.State = True
                   Select New With {
                       .Id = cts.Id,
                       .ContributorTypeId = cts.ContributorTypeId,
                       .ContributorSubtypeId = cts.ContributorSubtypeId,
                       .State = cts.State,
                       .ContributorTypeName = ct.Name,
                       .ContributorSubtypeName = cs.Name,
                       .ContributorSubtypeCode = cs.Code
                   }).ToList()

        Return raw.Select(Function(x) New ContributorTypeSubtype With {
                       .Id = x.Id,
                       .ContributorTypeId = x.ContributorTypeId,
                       .ContributorSubtypeId = x.ContributorSubtypeId,
                       .State = x.State,
                       .ContributorTypeName = x.ContributorTypeName,
                       .ContributorSubtypeName = x.ContributorSubtypeName,
                       .ContributorSubtypeCode = x.ContributorSubtypeCode
                   }).ToList()
    End Function

    ''' <summary>
    ''' Obtiene el Id del pivote dado tipo + subtipo
    ''' </summary>
    Public Function GetIdByTypeAndSubtype(contributorTypeId As Integer, contributorSubtypeId As Integer) As Integer Implements IContributorTypeSubtypeRepository.GetIdByTypeAndSubtype
        Dim record = (From cts In _context.ContributorTypeSubtype
                      Where cts.ContributorTypeId = contributorTypeId AndAlso
                            cts.ContributorSubtypeId = contributorSubtypeId AndAlso
                            cts.State = True
                      Select cts.Id).FirstOrDefault()
        Return record
    End Function

    ''' <summary>
    ''' Guarda una nueva combinacion Tipo+Subtipo de cotizante
    ''' </summary>
    Public Function SaveContributorTypeSubtype(entity As ContributorTypeSubtype) As Boolean Implements IContributorTypeSubtypeRepository.SaveContributorTypeSubtype
        Try
            entity.MarkAsAdded()
            SaveEntity(entity)
            Me.UnitWork.Commit()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Elimina una combinacion Tipo+Subtipo de cotizante por Id
    ''' </summary>
    Public Function DeleteContributorTypeSubtype(id As Integer) As Boolean Implements IContributorTypeSubtypeRepository.DeleteContributorTypeSubtype
        Try
            Dim entity = (From cts In _context.ContributorTypeSubtype.AsNoTracking()
                          Where cts.Id = id
                          Select cts).FirstOrDefault()
            If entity IsNot Nothing Then
                DeleteEntity(entity)
                Me.UnitWork.Commit()
                Return True
            End If
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

End Class
