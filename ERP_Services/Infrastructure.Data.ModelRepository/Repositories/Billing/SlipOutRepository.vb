'***********************************************************************
' Assembly         : Infrastructure.Data.Billing
' Author           : Carlos Ernesto Cordoba
' Created          : 13-11-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Entities
#End Region

Public Class SlipOutRepository
    Inherits GenericRepository(Of SlipOut)
    Implements ISlipOutRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' obtiene una boleta de salida por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSlipOutByCode(code As String) As SlipOut Implements ISlipOutRepository.GetSlipOutByCode
        Dim res = (From so In _context.SlipOut Where so.Code = code Select so).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From so In _context.SlipOut.AsNoTracking() Where so.Code = code Select so).FirstOrDefault()
            Return res
        End If
        Return New SlipOut
    End Function

    ''' <summary>
    ''' obtiene una boleta de salida por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSlipOutById(Id As Integer) As SlipOut Implements ISlipOutRepository.GetSlipOutById
        Dim res = (From so In _context.SlipOut Where so.Id = Id Select so).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        End If
        Return New SlipOut
    End Function

    ''' <summary>
    ''' obtiene una boleta de salida por numero de admisión
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSlipOutByAdmissionNumber(admissionNumber As String) As SlipOut Implements ISlipOutRepository.GetSlipOutByAdmissionNumber
        Dim res = (From so In _context.SlipOut Where so.AdmissionNumber = admissionNumber Select so).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        End If
        Return New SlipOut
    End Function

End Class
