'************************************************************
' Assembly         : Infrastructure.Data.FixedAssetRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/05/2016
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Domain.Base
Imports System.Data.Entity.Infrastructure

#End Region

Public Class FixedAssetRetirementTypesRepository
    Inherits GenericRepository(Of FixedAssetRetirementTypes)
    Implements IFixedAssetRetirementTypesRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa el contexto de payments.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    Public Function GetAllFixedAssetRetirementTypes() As List(Of FixedAssetRetirementTypes) Implements IFixedAssetRetirementTypesRepository.GetAllFixedAssetRetirementTypes
        Dim FixedAssetRetirementTypes = From e In _context.FixedAssetRetirementTypes
                                        Select e
        Return FixedAssetRetirementTypes.ToList()
    End Function

    Public Function GetFixedAssetRetirementTypesByCode(code As String, Optional tracking As Boolean = True) As FixedAssetRetirementTypes Implements IFixedAssetRetirementTypesRepository.GetFixedAssetRetirementTypesByCode
        Dim res = (From bg In _context.FixedAssetRetirementTypes Where bg.Code = code Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From bg In _context.FixedAssetRetirementTypes.AsNoTracking() Where bg.Code = code Select bg).FirstOrDefault()
            Return res
        Else
            Return New FixedAssetRetirementTypes()
        End If
    End Function

    Public Function GetFixedAssetRetirementTypesById(id As Integer, Optional tracking As Boolean = True) As FixedAssetRetirementTypes Implements IFixedAssetRetirementTypesRepository.GetFixedAssetRetirementTypesById
        Dim FixedAssetRetirementTypes = From e In _context.FixedAssetRetirementTypes
                                        Where e.Id = id
                                        Select e
        If FixedAssetRetirementTypes.Count > 0 Then
            Dim Objdefects = Nothing
            If tracking = False Then
                Objdefects = (From e In _context.FixedAssetRetirementTypes.AsNoTracking
                              Where e.Id = id
                              Select e).SingleOrDefault
            Else
                Objdefects = FixedAssetRetirementTypes.SingleOrDefault
            End If
            Return Objdefects
        Else
            Return New FixedAssetRetirementTypes()
        End If
    End Function

End Class
