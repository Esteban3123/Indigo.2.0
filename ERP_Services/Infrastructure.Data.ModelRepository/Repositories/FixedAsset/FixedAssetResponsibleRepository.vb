'***********************************************************************
' Assembly         : Infrastructure.Data.MaintenanceRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 15-09-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
#End Region


Public Class FixedAssetResponsibleRepository

    Inherits GenericRepository(Of FixedAssetResponsible)
    Implements IFixedAssetResponsibleRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    ''' 
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Función que obtiene un Tipo de Responsable
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Public Function GetResponsibleTypeByCode(Code As String) As FixedAssetResponsible Implements IFixedAssetResponsibleRepository.GetResponsibleByCode

        If Code Is Nothing OrElse Code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("Code")
        End If
        Dim res = (From d As FixedAssetResponsible In _context.FixedAssetResponsible.Include("ResponsibleFunctionalUnit").Include("ResponsibleFunctionalUnit.FunctionalUnit") Where d.Code.Equals(Code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then

            Dim ObjThirdParty = (From a In _context.ThirdParty.AsNoTracking Where a.Id = res.ThirdPartyId Select a).FirstOrDefault()
            Dim ObjVinculationType = (From b In _context.FixedAssetVinculationType.AsNoTracking Where b.Id = res.VinculationTypeId Select b).FirstOrDefault()
            Dim ObjResponsibleType = (From c In _context.ResponsibleType.AsNoTracking Where c.Id = res.ReponsibleTypeId Select c).FirstOrDefault()

            res.ThirdPartyName = ObjThirdParty.Name
            res.VinculationName = ObjVinculationType.Description
            res.ResponsibleTypeName = ObjResponsibleType.Description

            res.OriginalValue = (From d As FixedAssetResponsible In Me._context.FixedAssetResponsible.AsNoTracking() Where d.Code.Equals(Code.Trim()) Select d).FirstOrDefault()
            Return res
        Else
            Return New FixedAssetResponsible()
        End If
    End Function

    ''' <summary>
    ''' Función que obtiene todas las tipos de Responsables
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    Public Function ListAllResponsibleType() As List(Of FixedAssetResponsible) Implements IFixedAssetResponsibleRepository.ListAllResponsible
        Dim ListResponsible = From e In _context.FixedAssetResponsible
                   Select e

        If ListResponsible.Count() > 0 Then
            Return ListResponsible.ToList()
        Else
            Return New List(Of FixedAssetResponsible)
        End If
    End Function

    Public Function GetFuncionalUnitByCode(Id As String) As FunctionalUnit Implements IFixedAssetResponsibleRepository.GetFuncionalUnitByCode
        Dim FunctionalUnit = From e In _context.FunctionalUnit
                           Where e.Id = Id
                           Select e

        If FunctionalUnit IsNot Nothing Then
            Return FunctionalUnit.FirstOrDefault()
        Else
            Return Nothing
        End If
    End Function

    Public Function GetFuncionalUnitByIdUser(IdResponsible As Integer) As List(Of ResponsibleFunctionalUnit) Implements IFixedAssetResponsibleRepository.GetFuncionalUnitByIdUser
        Dim ObjResponsibleFunctionalUnit = From e In _context.ResponsibleFunctionalUnit.Include("FunctionalUnit")
                           Where e.IdResponsible = IdResponsible
                           Select e

        If ObjResponsibleFunctionalUnit IsNot Nothing Then
            Return ObjResponsibleFunctionalUnit.ToList()
        Else
            Return Nothing
        End If
    End Function
End Class
