'************************************************************
' Assembly         : Infrastructure.Data.GlosasRepository
' Author           : Oscar Ivan Sierra
' Created          : 08-08-2013
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
Public Class FixedAssetPolicyTypeRepository
    Inherits GenericRepository(Of FixedAssetPolicyType)
    Implements IFixedAssetPolicyTypeRepository

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


    Public Function GetPolizaType(codepolizatype As String, Optional tracking As Boolean = True) As FixedAssetPolicyType Implements IFixedAssetPolicyTypeRepository.GetPolizaType
        If tracking = True Then
            Dim Busqueda = From e In _context.FixedAssetPolicyType
                       Where e.Code = codepolizatype
                       Select e

            If Busqueda.Count = 0 Then
                Return New FixedAssetPolicyType
            Else
                Return Busqueda.Single
            End If
        Else
            Dim Busqueda = From e In _context.FixedAssetPolicyType.AsNoTracking
                       Where e.Code = codepolizatype
                       Select e

            If Busqueda.Count = 0 Then
                Return New FixedAssetPolicyType
            Else
                Return Busqueda.Single
            End If
        End If
    End Function

    Public Function ListAllPolizaType() As List(Of FixedAssetPolicyType) Implements IFixedAssetPolicyTypeRepository.ListAllPolizaType
        Dim Busqueda = From e In _context.FixedAssetPolicyType
                       Select e

        Return Busqueda.ToList
    End Function

   
End Class
