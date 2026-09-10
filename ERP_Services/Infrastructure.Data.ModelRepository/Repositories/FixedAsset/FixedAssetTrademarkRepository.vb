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

Public Class FixedAssetTrademarkRepository

    Inherits GenericRepository(Of FixedAssetTrademark)
    Implements IFixedAssetTrademarkRepository

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
    ''' Función que obtiene una marca por Código
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Public Function GetTrademarkByCode(Code As String, Optional tracking As Boolean = True) As FixedAssetTrademark Implements IFixedAssetTrademarkRepository.GetTrademarkByCode
        If Code Is Nothing OrElse Code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From t In _context.FixedAssetTrademark Where t.Code = Code Select t).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From t In _context.FixedAssetTrademark.AsNoTracking Where t.Code = Code Select t).FirstOrDefault
            Return res
        Else
            Return New FixedAssetTrademark
        End If
    End Function

    ''' <summary>
    ''' Función que obtiene todas las Marcas par alos Equipos
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    Public Function ListAllTrademark() As List(Of FixedAssetTrademark) Implements IFixedAssetTrademarkRepository.ListAllTrademark
        Dim ListTrademark = From e In _context.FixedAssetTrademark
                   Select e

        If ListTrademark.Count() > 0 Then
            Return ListTrademark.ToList()
        Else
            Return New List(Of FixedAssetTrademark)
        End If


    End Function
End Class
