'************************************************************
' Assembly         : Infrastructure.Data.GlosasRepository
' Author           : Oscar Ivan Sierra
' Created          : 04-08-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Domain.Base

#End Region


''' <summary>
''' clase para hacer todas las operaciones de persistencia para la entidad aseguradora
''' </summary>
''' <remarks></remarks>
Public Class FixedAssetInsuranceRepository
    Inherits GenericRepository(Of FixedAssetInsurance)
    Implements IFixedAssetInsuranceRepository


    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub


    ''' <summary>
    ''' funcion para consultar una aseguradora por codigo
    ''' </summary>
    ''' <param name="codeInsurance"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInsurance(codeInsurance As String, Optional Tracking As Boolean = False) As FixedAssetInsurance Implements IFixedAssetInsuranceRepository.GetInsurance

        If codeInsurance Is Nothing OrElse codeInsurance.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("codeInsurance")
        End If
        Dim res = (From d As FixedAssetInsurance In _context.FixedAssetInsurance.Include("Person").Include("Person.Address").Include("Person.Phone").Include("Person.Email") Where d.Code.Equals(codeInsurance.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then
            Dim ObjThirdParty = (From a In _context.ThirdParty.AsNoTracking Where a.Id = res.IdThirdParty Select a).FirstOrDefault()
            res.NameThirdParty = ObjThirdParty.Name
            res.OriginalValue = (From d As FixedAssetInsurance In Me._context.FixedAssetInsurance.AsNoTracking() Where d.Code.Equals(codeInsurance.Trim()) Select d).SingleOrDefault()

            If res.Person IsNot Nothing Then
                If res.Person.Address IsNot Nothing AndAlso  res.Person.Address.Count > 0 Then
                    For Each address In res.Person.Address
                        If address.DepartmentId IsNot Nothing Then
                            address.DepartmentName = (From d In _context.Department Where d.Id = address.DepartmentId Select d.Name).FirstOrDefault()
                            If address.CityId IsNot Nothing Then
                                address.CityName = (From d In _context.City Where d.Id = address.CityId Select d.Name).FirstOrDefault()
                            End If
                        End If
                    Next
                End If
            End If

            Return res
        Else
            Return New FixedAssetInsurance()
        End If


    End Function

    ''' <summary>
    ''' funcion para listar todas las aseguradores activas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllInsurance() As List(Of FixedAssetInsurance) Implements IFixedAssetInsuranceRepository.ListAllInsurance


        Dim Busqueda = From e In _context.FixedAssetInsurance
                                      Select e

        Return Busqueda.ToList
    End Function

   
End Class
