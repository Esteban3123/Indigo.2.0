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


Public Class FixedAssetResponsibleTypeRepository

    Inherits GenericRepository(Of ResponsibleType)
    Implements IFixedAssetResponsibleTypeRepository

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
    Public Function GetResponsibleTypeByCode(Code As String) As ResponsibleType Implements IFixedAssetResponsibleTypeRepository.GetResponsibleTypeByCode

        If Code Is Nothing OrElse Code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("Code")
        End If
        Dim res = (From d As ResponsibleType In _context.ResponsibleType Where d.Code.Equals(Code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From d As ResponsibleType In Me._context.ResponsibleType.AsNoTracking() Where d.Code.Equals(Code.Trim()) Select d).FirstOrDefault()
            Return res
        Else
            Return New ResponsibleType()
        End If
    End Function

    ''' <summary>
    ''' Función que obtiene todas las tipos de Responsables
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    Public Function ListAllResponsibleType() As List(Of ResponsibleType) Implements IFixedAssetResponsibleTypeRepository.ListAllResponsibleType
        Dim ListResponsibleType = From e In _context.ResponsibleType
                   Select e

        If ListResponsibleType.Count() > 0 Then
            Return ListResponsibleType.ToList()
        Else
            Return New List(Of ResponsibleType)
        End If
    End Function

End Class
