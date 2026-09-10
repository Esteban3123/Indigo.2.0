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
''' clase para hacer todas las operaciones de persistencia para la fabricante
''' </summary>
''' <remarks></remarks>
Public Class FixedAssetPolicyRepository
    Inherits GenericRepository(Of FixedAssetPolicy)
    Implements IFixedAssetPolicyRepository

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


    Public Function GetPoliza(codePoliza As String, Optional ByVal tracking As Boolean = True) As FixedAssetPolicy Implements IFixedAssetPolicyRepository.GetPoliza
        If codePoliza Is Nothing OrElse codePoliza.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As FixedAssetPolicy In Me._context.FixedAssetPolicy Where d.Code.Equals(codePoliza.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then

            Dim insurance = (From i In _context.FixedAssetInsurance.AsNoTracking Where i.Id = res.InsuranceId Select i).FirstOrDefault
            res.CodeNameInsurance = insurance.Code + " - " + insurance.Name

            Dim polizaType = (From p In _context.FixedAssetPolicyType.AsNoTracking Where p.Id = res.PolicyTypeId Select p).FirstOrDefault
            res.CodeNamePolizaType = polizaType.Code + " - " + polizaType.Name
            
            res.OriginalValue = (From d As FixedAssetPolicy In Me._context.FixedAssetPolicy.AsNoTracking() Where d.Code.Equals(codePoliza.Trim()) Select d).SingleOrDefault()
            Return res
        Else
            Return New FixedAssetPolicy()
        End If
    End Function

    Public Function ListAllPoliza() As List(Of FixedAssetPolicy) Implements IFixedAssetPolicyRepository.ListAllPoliza
        Dim Busqueda = From e In _context.FixedAssetPolicy
                       Select e

        Return Busqueda.ToList
    End Function


End Class
