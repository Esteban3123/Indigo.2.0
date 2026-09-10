'***********************************************************************
' Assembly         : Infrastructure.Data.PortfolioRepositiry
' Author           : Carlos Mario Arias Rubiano
' Created          : 05/06/2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Entities
#End Region

Public Class LawyerRepository
    Inherits GenericRepository(Of Lawyer)
    Implements ILawyerRepository

    'contexto de cartera
    Private _context As IGlobalModelUnitOfWork

#Region "Builder"
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' metodo para obtener un abogado por codigo
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <returns></returns>
    Public Function GetLawyerByCode(code As String) As Lawyer Implements ILawyerRepository.GetLawyerByCode
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As Lawyer In _context.Lawyer Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault()
        If res IsNot Nothing Then

            res.NitNameThirdParty = (From d In _context.ThirdParty.AsNoTracking Where d.Id = res.ThirdPartyId Select String.Concat(d.Nit, " - ", d.Name)).FirstOrDefault()

            res.OriginalValue = (From d As Lawyer In _context.Lawyer.AsNoTracking Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault()
            Return res
        Else
            Return New Lawyer()
        End If
    End Function

    ''' <summary>
    ''' metodo para obtener un concepto de cuenta x pagar por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLawyerById(id As Integer) As Lawyer Implements ILawyerRepository.GetLawyerById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d As Lawyer In _context.Lawyer Where d.Id = id Select d).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From d As Lawyer In _context.Lawyer.AsNoTracking Where d.Id = id Select d).FirstOrDefault()
            Return res
        Else
            Return New Lawyer()
        End If
    End Function

    ''' <summary>
    ''' Obtiene el abogado por id del tercero
    ''' </summary>
    ''' <param name="thirdPartyId"></param>
    ''' <returns></returns>
    Public Function GetLawyerByThirdPartyId(thirdPartyId As Integer, ByVal registerId As Integer) As Lawyer Implements ILawyerRepository.GetLawyerByThirdPartyId
        Return (From x In _context.Lawyer.AsNoTracking Where x.ThirdPartyId = thirdPartyId AndAlso x.Id <> registerId Select x).FirstOrDefault()
    End Function

#End Region

End Class
