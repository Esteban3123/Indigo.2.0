'***********************************************************************
' Assembly         : Infrastructura.Data.ModelRepository.Portfolio
' Author           : Hector Rodriguez Rubiano
' Created          : 09-08-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class PortfolioDemandStatusRepository
    Inherits GenericRepository(Of DemandStatus)
    Implements IPortfolioDemandStatusRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="context">el contexto.</param>
    ''' 
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    Public Function GetDemandStatusByCode(Code As String) As DemandStatus Implements IPortfolioDemandStatusRepository.GetDemandStatusByCode
        If Code Is Nothing OrElse Code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("Code")
        End If
        Dim res = (From d As DemandStatus In _context.DemandStatus Where d.Code.Equals(Code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From d As DemandStatus In Me._context.DemandStatus.AsNoTracking() Where d.Code.Equals(Code.Trim()) Select d).FirstOrDefault()
            Return res
        Else
            Return New DemandStatus()
        End If
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllDemandStatus() As List(Of DemandStatus) Implements IPortfolioDemandStatusRepository.ListAllDemandStatus
        Dim ListDemandStatus = From e In _context.DemandStatus
                                   Select e

        If ListDemandStatus.Count() > 0 Then
            Return ListDemandStatus.ToList()
        Else
            Return New List(Of DemandStatus)
        End If
    End Function
End Class
